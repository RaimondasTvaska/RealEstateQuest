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
            using var command = new SqlCommand("SELECT * FROM dbo.Companies;", _connection);
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
        public void AddCompany(CompanyModel company)
        {
            _connection.Open();

            string insertText = $"insert into dbo.Companies (Company_Name, City, Street, House_Flat_Number) " +
                $"values(N'{company.CompanyName}'," +
                $" N'{company.City}'" +
                $" N'{company.Street}'" +
                $" '{company.HouseFlatNumber}') ";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();


        }
    }
}
