using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            return View();
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUsers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUsers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
