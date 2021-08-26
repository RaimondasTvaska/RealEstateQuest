using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateQuest.Models;
using RealEstateQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyDBService _companyDB;
        private RealEstateDBService _realEstateDB;
        private BrokerDBService _brokerDB;

        public CompanyController(CompanyDBService companyDB, RealEstateDBService realEstateDB, BrokerDBService brokerDB)
        {
            _companyDB = companyDB;
            _realEstateDB = realEstateDB;
            _brokerDB = brokerDB;
        }


        // GET: BrokerController
        public ActionResult Index()
        {
            var realEstateModel = new RealEstateModel();
            realEstateModel.Companies = _companyDB.AllCompanies();
            realEstateModel.Brokers = _brokerDB.AllBrokers();
            return View(_companyDB.AllCompanies());
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            RealEstateModel realEstate = new()
            {
                Companies = _companyDB.AllCompanies(),
            };
            return View(realEstate);
        }

        // POST: CompanyController/Create
        [HttpPost]
        public ActionResult Create(RealEstateModel realEstate)
        {
            _companyDB.AddCompany(realEstate);

            return RedirectToAction("Index");
        }

        // GET: CompanyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompanyController/Edit/5
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

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
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
