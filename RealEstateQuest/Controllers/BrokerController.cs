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

        public BrokerController(BrokerDBService brokerDB)
        {
            _brokerDB = brokerDB;
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
            return View();
        }

        // POST: BrokerController/Create
        [HttpPost]
        public ActionResult Create(BrokerModel broker)
        {
            _brokerDB.AddBroker(broker);

            return RedirectToAction("Index");
        }

        // GET: BrokerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrokerController/Edit/5
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
