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


        public BrokerDBService(SqlConnection connection)
        {
            _connection = connection;
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
                    FirstName = reader.GetString(1),
                    Surname = reader.GetString(2),

                });
            }
            _connection.Close();

            return brokers;
        }
        public void AddBroker(BrokerModel broker)
        {
            _connection.Open();

            string insertText = $"insert into dbo.Brokers (First_Name, Surname) " +
                $"values(N'{broker.FirstName}'," +
                $" N'{broker.Surname}') ";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();


        }
        public void EditBroker(BrokerModel broker)
        {
            _connection.Open();

            string insertText = @$"update dbo.Brokers set First_Name = N'{broker.FirstName}' , Surname = N'{broker.Surname}' 
                where Id = '{broker.Id}';";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

           
        }
    }
}

