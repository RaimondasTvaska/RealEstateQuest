using RealEstateQuest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Services
{
    public class BrokerDBService
    {
        private SqlConnection _connection;
        public CompanyDBService _companyDBService;

        public BrokerDBService(SqlConnection connection, CompanyDBService companyDBService)
        {
            _connection = connection;
            _companyDBService = companyDBService;
        }

        public List<BrokerModel> AllBrokers()
        {
            List<BrokerModel> brokers = new();

            _connection.Open();
            using var command = new SqlCommand("SELECT * FROM dbo.Brokers;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brokers.Add(new BrokerModel()
                {
                    Id = reader.GetInt32(0),
                    First_Name = reader.GetString(1),
                    Surname = reader.GetString(2),

                });
            }
            _connection.Close();

            return brokers;
        }
        public void AddBroker(RealEstateModel realEstate)
        {
            List<CompanyModel> companies = _companyDBService.AllCompanies();
            _connection.Open();

            string insertText = $"insert into dbo.Brokers (First_Name, Surname) " +
                $"values(N'{realEstate.Broker.First_Name}'," +
                $" N'{realEstate.Broker.Surname}'); SELECT SCOPE_IDENTITY() ";

            SqlCommand command = new SqlCommand(insertText, _connection);
            int brokerId = Convert.ToInt32(command.ExecuteScalar());
            
            foreach (var companyId in realEstate.CompaniesIds)
            {
                command = new($"insert into dbo.CompanyBroker (CompanyId, BrokerId) " +
                    $"values('{companyId}'," +
                    $" '{brokerId}') ;", _connection);
                command.ExecuteNonQuery();

            }
                _connection.Close();

        }
        public void EditBroker(int id, RealEstateModel realEstate)
        {
            _connection.Open();

            string insertText = @$"
                update dbo.Brokers 
                set 
                    First_Name = N'{realEstate.Broker.First_Name}' , 
                    Surname = N'{realEstate.Broker.Surname}' 
                where Id = '{realEstate.Broker.Id}';";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();
            _connection.Close();

            _companyDBService.DeleteBrokerCompanies(realEstate.Broker.Id);

            _connection.Open();

            if (realEstate.Companies != null)
            {
                foreach (var companyId in realEstate.CompaniesIds)
                {
                    command = new($"insert into dbo.CompanyBroker (CompanyId, BrokerId) " +
                        $"values('{companyId}'," +
                        $" '{id}') ;", _connection);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                EditBroker();
                
            }
            _connection.Close();
        }

        public List<CompanyModel> GetCompaniesByBrokerId(int brokerId)
        {
            _connection.Open();
            List<CompanyModel> companies = new();
            using var command = new SqlCommand(@$"select Companies.* 
                from ((Brokers 
                inner join CompanyBroker on Brokers.Id = CompanyBroker.BrokerId)
				inner join Companies
                on Companies.Id = CompanyBroker.CompanyId)
                where Brokers.Id = '{brokerId}';", _connection);
             using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                companies.Add(new CompanyModel()
                {
                    Id = reader.GetInt32(0),
                    CompanyName = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    HouseFlatNumber = reader.GetString(4)

                });
            }
            _connection.Close();
            return companies;
        }



        public void DeleteBroker(int id,RealEstateModel realEstate)
        {
            _connection.Open();

            string insertText = @$"delete from dbo.Brokers 
                                where Id = '{realEstate.Broker.Id}';";

            SqlCommand command = new SqlCommand(insertText, _connection);

            foreach (var companyId in realEstate.CompaniesIds)
            {
                command = new($"insert into dbo.CompanyBroker (CompanyId, BrokerId) " +
                    $"values('{companyId}'," +
                    $" '{id}') ;", _connection);
                command.ExecuteNonQuery();

            }

            _connection.Close();
        }

        //public List<BrokerModel> GetBrokerCompanies(int companyId)
        //{
        //    _connection.Open();
        //    List<BrokerModel> brokers = new();
        //    using var command = new SqlCommand (@$"SELECT Brokers.* from ((Companies inner join CompanyBroker
        //                        on Companies.Id = CompanyBroker.CompanyId)
        //                        inner join Brokers on Brokers.Id = CompanyBroker.BrokerId)
        //                        where Companies.Id = '{companyId}';", _connection);

        //    using var reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        brokers.Add(new BrokerModel()
        //        {
        //            Id = reader.GetInt32(0),
        //            First_Name = reader.GetString(1),
        //            Surname = reader.GetString(2),

        //        });
        //    }
        //    _connection.Close();
        //    return brokers;
        //}
    }
}

