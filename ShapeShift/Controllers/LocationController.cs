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
        public ActionResult CreateLocationName(Location location)
        {
            ViewBag.Name = new SelectList(db.Locations.Where(l => !l.locationName.Contains("")).ToList(), "locationName", "LocationId");
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            try
            {
                // TODO: Add insert logic here
                Location newLocation = new Location();
                
                newLocation.locationName = location.locationName;
                newLocation.LocationId = location.LocationId;
                db.Locations.Add(newLocation);
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
            return View();
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Location location)
        {
            try
            {

                Location newlocation = db.Locations.FirstOrDefault(l => l.LocationId == id);
                newlocation.locationName = location.locationName;
                newlocation.LocationId = location.LocationId;
                db.SaveChanges();

                return RedirectToAction("Index", "Organization");
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult DeleteLocation(int id)
        {
            Location location = db.Locations.Find(id);
            return View();
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult DeleteLocationConfirmed(int id)
        {
            try
            {
                Location location = db.Locations.Find(id);
                db.Locations.Remove(location);
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
