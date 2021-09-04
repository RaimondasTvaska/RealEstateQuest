using RealEstateQuest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Services
{
    public class CompanyDBService
    {
        private SqlConnection _connection;


        public CompanyDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<CompanyModel> AllCompanies()
        {
            List<CompanyModel> companies = new();

            _connection.Open();
            using var command = new SqlCommand($@"SELECT * FROM dbo.Companies;", _connection);
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
        public int AddCompany(RealEstateModel realEstate)
        {
            int companyId = 0;
            _connection.Open();

            string insertText = @$"insert into dbo.Companies (Company_Name, City, Street, House_Flat_Number) 
                values(N'{realEstate.CompanyAddInformation.CompanyName}',
                 N'{realEstate.CompanyAddInformation.City}',
                 N'{realEstate.CompanyAddInformation.Street}',
                '{realEstate.CompanyAddInformation.HouseFlatNumber}') ";

            SqlCommand command = new SqlCommand(insertText, _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                companyId = Convert.ToInt32(reader.GetDecimal(0));
            }
            reader.Close();
            foreach (var brokerId in realEstate.Brokers)
            {
                command = new($"insert into dbo.CompanyBroker (CompanyId, BrokerId) " +
                    $"values('{companyId}'," +
                    $" '{brokerId}') ;", _connection);
                command.ExecuteNonQuery();

            }
            _connection.Close();
            
            return companyId;
        }

        public void DeleteBrokerCompanies(int id)
        {
            _connection.Open();

            string insertText = @$"delete from dbo.CompanyBroker 
                                where BrokerId = '{id}';";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();
            _connection.Close();

        }

        //public List<ApartmentModel> GetCompanyApartments (int companyId)
        //{
        //    List<ApartmentModel> companyApartments = new();
        //    _connection.Open();
        //    SqlCommand command = new($@"SELECT *, CONCAT(City, ' ', Street, ' ', HouseNo, ' ', FlatNo, ' ' )
        //                                FROM Apartments
        //                                WHERE CompanyId = {companyId};", _connection);
        //}
    }
}
