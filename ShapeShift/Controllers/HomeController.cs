using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        
    }
}