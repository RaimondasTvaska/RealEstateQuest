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
            using var command = new SqlCommand(@"SELECT * from Company_Apartments ", _connection);
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
                    Company_Id = reader.GetInt32(8),
                    Broker_Id = reader.GetInt32(9)

                });
            }
            _connection.Close();

            return apartments;
        }
        public void AddApartment(RealEstateModel realEstate)
        {
            _connection.Open();

            string insertText = @$"insert into dbo.Apartments (City, Street, HouseNo, FlatNo, FlatFloor, BuildingFloors, Area, Company_Id)
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

