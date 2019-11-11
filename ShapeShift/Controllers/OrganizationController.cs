using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class OrganizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        // GET: Organization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Organization/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "OrganizationId,Organization Name")] Organization organization)
        {
            try
            {
                db.Organizations.Add(organization);
                AppUser appUser = new AppUser();
                appUser.ApplicationId = User.Identity.GetUserId();
                appUser.OrganizationId = organization.OrganizationId;
                db.AppUsers.Add(appUser);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(int id)
        {


            return View();
        }

        // POST: Organization/Edit/5
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

        // GET: Organization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Organization/Delete/5
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
