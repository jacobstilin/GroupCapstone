using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class LocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public AppUser GetLoggedInUser()//Gets current user
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }

        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(db.Locations.Where(l => !l.locationName.Contains("")).ToList(), "locationName", "LocationId");
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            AppUser boss = GetLoggedInUser();
            try
            {

                // TODO: Add insert logic here
                Location newLocation = new Location();
                
                newLocation.locationName = location.locationName;
                
                newLocation.UserId = boss.UserId;
                db.Locations.Add(newLocation);
                //boss.Location.Add(newLocation);
                db.SaveChanges();

                return RedirectToAction("Index", "Organization");
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Name1 = new SelectList(db.Locations.ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Positions.ToList(), "locationName", "LocationId");
            Location location = new Location();
            location = db.Locations.Where(e => e.LocationId == id).SingleOrDefault();
            return PartialView("_EditIndividualLocation", location );
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(Location location)
        {
            try
            {
                //Location newlocation = boss.Location.Where(e => e.locatiationId == id).SingleOrDefault();
                Location newlocation = db.Locations.FirstOrDefault(e => e.LocationId == location.LocationId);
                newlocation.locationName = location.locationName;
                newlocation.LocationId = location.LocationId;
                newlocation.UserId = location.UserId;
                db.SaveChanges();

                return PartialView("_EditLocation");
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {

            // Location newlocation = boss.Location.Where(e => e.locatiationId == id).SingleOrDefault();


            Location location = new Location();
            location = db.Locations.Where(e => e.LocationId == id).SingleOrDefault();
            db.Locations.Remove(location);
            return PartialView("_EditLocation");
       
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Location location)
        {

            return PartialView();
         
        }
    }
}
