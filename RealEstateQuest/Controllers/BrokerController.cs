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
    public class BrokerController : Controller
    {
        private BrokerDBService _brokerDB;
        private CompanyDBService _companyDB;

        public BrokerController(BrokerDBService brokerDB, CompanyDBService companyDB)
        {
            _brokerDB = brokerDB;
            _companyDB = companyDB;
        }
        // GET: BrokerController
        public ActionResult Index()
        {
            return View(_brokerDB.AllBrokers());
        }

        // GET: BrokerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrokerController/Create
        public ActionResult Create()
        {
            RealEstateModel realEstate = new()
            {
                Companies = _companyDB.AllCompanies(),
                

            };
            return View(realEstate);
        }

        // POST: BrokerController/Create
        [HttpPost]
        public ActionResult Create(RealEstateModel realEstate)
        {
            _brokerDB.AddBroker(realEstate);

            return RedirectToAction("Index");
        }

        // GET: BrokerController/Edit/5
        public ActionResult Edit(int id)
        {
            BrokerModel broker = _brokerDB.AllBrokers().FirstOrDefault(b => b.Id == id);
            List<CompanyModel> companies = _companyDB.AllCompanies();
            RealEstateModel realEstateModel = new()
            {
                Broker = broker,
                Companies = companies
            };
            return View(realEstateModel);
        }

        // POST: BrokerController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RealEstateModel realEstate)
        {
            
            _brokerDB.EditBroker(realEstate);

            return RedirectToAction("Index");
        }

        // GET: BrokerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrokerController/Delete/5
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
