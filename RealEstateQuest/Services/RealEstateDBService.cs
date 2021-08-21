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
                Companies = _companyDB.AllCompanies()
            };

            return realEstates;
        }
        public RealEstateModel AddNewRealEstate()
        {
            List<ApartmentModel> apartments = new();
            apartments.Add(new ApartmentModel());

            RealEstateModel realEstate = new RealEstateModel()

            {
                Apartments = _apartmentDB.AllApartments(),
                Companies = _companyDB.AllCompanies(),
                Brokers = _brokerDB.AllBrokers()

            };

            return realEstate;
        }
    }
}
