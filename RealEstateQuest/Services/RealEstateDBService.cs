using RealEstateQuest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Services
{
    public class RealEstateDBService
    {
        private ApartmentDBService _apartmentDB;
        private CompanyDBService _companyDB;
        private BrokerDBService _brokerDB;
        private SqlConnection _connection;



        public RealEstateDBService(ApartmentDBService apartmentDB, CompanyDBService companyDB, BrokerDBService brokerDB, SqlConnection connection)
        {
            _apartmentDB = apartmentDB;
            _companyDB = companyDB;
            _brokerDB = brokerDB;
            _connection = connection;
        }

        public RealEstateModel AllRealEstates()
        {

            RealEstateModel realEstates = new RealEstateModel()

            {
                Companies = _companyDB.AllCompanies(),
                Apartments = _apartmentDB.AllApartments(),
                Brokers = _brokerDB.AllBrokers()
            };

            return realEstates;
        }
        public RealEstateModel AddNewRealEstate()
        {
            List<ApartmentModel> apartmentsList = new();
            apartmentsList.Add(new ApartmentModel());

            RealEstateModel realEstates = new()

            {
                Apartments = apartmentsList,
                Companies = _companyDB.AllCompanies(),
                Brokers = _brokerDB.AllBrokers()

            };

            return realEstates;
        }
    }
}
