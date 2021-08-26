using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateQuest.Models;
using RealEstateQuest.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Controllers
{
    public class ApartmentController : Controller
    {
        private ApartmentDBService _apartmentDB;
        private CompanyDBService _companyDB;
        private BrokerDBService _brokerDB;
        private RealEstateDBService _realEstateDB;
        private SqlConnection _connection;

        public ApartmentController(ApartmentDBService apartmentDB, CompanyDBService companyDB, BrokerDBService brokerDB, RealEstateDBService realEstateDB, SqlConnection connection)
        {
            _apartmentDB = apartmentDB;
            _companyDB = companyDB;
            _brokerDB = brokerDB;
            _realEstateDB = realEstateDB;
            _connection = connection;
        }




        // GET: ApartmentsController
        public ActionResult Index()
        {
            return View(_realEstateDB.AllRealEstates());
        }

        // GET: ApartmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApartmentsController/Create
        public ActionResult Create()
        {
            RealEstateModel realEstates = new()
            {
                Companies = _companyDB.AllCompanies(),
                Apartments = _apartmentDB.AllApartments(),
            };
            return View(realEstates);
        }

        // POST: ApartmentsController/Create
        [HttpPost]
        public ActionResult Create(RealEstateModel realEstates)
        {
            
            _apartmentDB.AddApartment(realEstates);

            return RedirectToAction("Index");
        }

        // GET: ApartmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApartmentsController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApartmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApartmentsController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
