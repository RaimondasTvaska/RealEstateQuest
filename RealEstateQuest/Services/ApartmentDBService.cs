using RealEstateQuest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Services
{
    public class ApartmentDBService
    {
        private SqlConnection _connection;


        public ApartmentDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<ApartmentModel> AllApartments()
        {
            List<ApartmentModel> apartments = new();

            _connection.Open();
            using var command = new SqlCommand(@"SELECT dbo.Apartments.Id, dbo.Apartments.City, dbo.Apartments.Street,
                    dbo.Apartments.House_No, dbo.Apartments.Flat_No,
                    dbo.Apartments.Flat_Floor, dbo.Apartments.Building_Floors, dbo.Apartments.Area, dbo.Apartments.Company_Id,
                    dbo.Apartments.Broker_Id,
                    dbo.Companies.Company_Name, dbo.Brokers.First_Name, dbo.Brokers.Surname
                    FROM dbo.Apartments
                    LEFT OUTER JOIN dbo.CompanyBroker
                    ON dbo.Apartments.Broker_Id = dbo.CompanyBroker.BrokerId
                    LEFT OUTER JOIN dbo.Brokers
                    ON dbo.CompanyBroker.BrokerId = dbo.Brokers.Id
                    LEFT OUTER JOIN dbo.Companies
                    ON dbo.CompanyBroker.CompanyId = dbo.Companies.Id ", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                apartments.Add(new ApartmentModel()
                {
                    Id = reader.GetInt32(0),
                    City = reader.GetString(1),
                    Street = reader.GetString(2),
                    HouseNo = reader.GetString(3),
                    FlatNo = reader.GetInt32(4),
                    FlatFloor = reader.GetInt32(5),
                    BuildingFloors = reader.GetInt32(6),
                    Area = reader.GetInt32(7),
                    Company_Id = reader.IsDBNull(9) ? 0 : reader.GetInt32(8),
                    Broker_Id = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)

                });
            }
            _connection.Close();

            return apartments;
        }
        public void AddApartment(RealEstateModel realEstate)
        {
            _connection.Open();

            string insertText = @$"insert into dbo.Apartments (City, Street, House_No, Flat_No, Flat_Floor, Building_Floors, Area, Company_Id)
                values(N'{realEstate.ApartmentAddInformation.City}',
                N'{realEstate.ApartmentAddInformation.Street}',
                '{realEstate.ApartmentAddInformation.HouseNo}',
                '{realEstate.ApartmentAddInformation.FlatNo}',
                '{realEstate.ApartmentAddInformation.FlatFloor}',
                '{realEstate.ApartmentAddInformation.BuildingFloors}',
                '{realEstate.ApartmentAddInformation.Area}', 
                '{realEstate.ApartmentAddInformation.Company_Id}') ;";
            

            SqlCommand command = new SqlCommand(insertText, _connection);

            command.ExecuteNonQuery();

            _connection.Close();


        }

        
    }
}

