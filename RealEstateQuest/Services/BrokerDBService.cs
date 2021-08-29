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
        public void EditBroker(RealEstateModel realEstate)
        {
            _connection.Open();

            string insertText = @$"update dbo.Brokers set First_Name = N'{realEstate.Broker.First_Name}' , Surname = N'{realEstate.Broker.Surname}' 
                                where Id = '{realEstate.Broker.Id}';";

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
        public List<BrokerModel> GetCompanyBrokers(int companyId)
        {
            _connection.Open();
            List<BrokerModel> brokers = new();
            using var command = new SqlCommand (@$"SELECT Brokers.* from ((Companies inner join CompanyBroker
                                on Companies.Id = CompanyBroker.CompanyId)
                                inner join Brokers on Brokers.Id = CompanyBroker.BrokerId)
                                where Companies.Id = '{companyId}';", _connection);

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
    }
}

