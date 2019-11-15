using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShapeShift.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       

        

        


        

        
        // GET: Organization

        public AppUser GetLoggedInUser()
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == User.Identity.GetUserId());
            //could we add a get role inside of this to attach to app user for authentication purposes
            return (appUser);
        }
        public ActionResult Index()
        {

            
            bool isRole = User.IsInRole("Owner");
            if (isRole == true)
            {
            ViewBag.Name = new SelectList(db.AppUsers.Where(u => !u.firstName.Contains("")).ToList(), "UserId", "firstName");
            ViewBag.displayMenu = ViewBag.Name;
            return View();
            }
            return RedirectToAction("Index", "Home");

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

        // Not currently used as creation occurs during register
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

        public ActionResult EditPositions()
        {
            return View();
        }


        // Takes array passed from positions edit view and assigns it as the positions for the array
        [HttpPost]
        public ActionResult EditPositions(string[] positions)
        {
            AppUser appUser = GetLoggedInUser();
            Organization organization = db.Organizations.FirstOrDefault(o => o.OrganizationId == appUser.OrganizationId);
           
            db.SaveChanges();
            return View();
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
            Organization organization = db.Organizations.Find(id);
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Organization organization = db.Organizations.Find(id);
                db.Organizations.Remove(organization);
                db.SaveChanges();
                return RedirectToAction("Index", "Organization");
            }
            catch
            {
                return View();
            }
        }
    }
}
