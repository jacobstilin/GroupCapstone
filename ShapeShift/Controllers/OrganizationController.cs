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
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }

        public void LoadOwner()
        {
            LoadAllLocations();
            LoadAllPositions();
            LoadAllAppUsers();
        }
        public ActionResult LoadAllAppUsers()
        {
            IList<AppUser> users = db.AppUsers.ToList();
            return PartialView("_CurrentAppUsers", users);
        }
        public ActionResult LoadAllLocations()
        {
            IList<Location> locations = db.Locations.ToList();
            return PartialView("_EditLocation", locations);
        }
        public ActionResult LoadAllPositions()
        {
            IList<Position> positions = db.Positions.ToList();
            return PartialView("_EditPosition", positions);
        }
        
        public ActionResult Index()
        {
            LoadOwner();
            
            ViewBag.Name1 = new SelectList(db.Locations.ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Positions.ToList(), "locationName", "LocationId");
         
            //bool isRole = User.IsInRole("Owner");
            //if (isRole == true)
            // {
            AppUser boss = GetLoggedInUser();

                return View(db.AppUsers.ToList());
            //}
            //return RedirectToAction("Index", "Home");

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

        //public ActionResult EditPositions()
        //{
        //    return View();
        //}


        //// Takes array passed from positions edit view and assigns it as the positions for the array
        //[HttpPost]
        //public ActionResult EditPositions(string[] positions)
        //{
        //    AppUser appUser = GetLoggedInUser();
        //    Organization organization = db.Organizations.FirstOrDefault(o => o.OrganizationId == appUser.OrganizationId);
           
        //    db.SaveChanges();
        //    return View();
        //}

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
        public ActionResult DeleteEmployee(int id)
        {
            Organization organization = db.Organizations.Find(id);
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        public ActionResult DeleteOrganizationConfirmed(int id)
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
