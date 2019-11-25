using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class PositionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public AppUser GetLoggedInUser()
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }
        // GET: Position
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPositionToEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPositionToEmployee(AppUser appUser)
        {
            return View();
        }

        // GET: Position/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Position/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Position/Create
        [HttpPost]
        public ActionResult Create(Position position)
        {
            try
            {
                Position newPosition = new Position();
                AppUser appUser = GetLoggedInUser();
                newPosition.title = position.title;
                newPosition.UserId = appUser.UserId;
                

                db.Positions.Add(newPosition);
                db.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Name1 = new SelectList(db.Locations.ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Positions.ToList(), "locationName", "LocationId");
            Position newPosition = db.Positions.FirstOrDefault(p => p.PositionId == id);

            return PartialView("~/Views/Position/_EditIndividualPosition.cshtml", newPosition);

        }

        // POST: Position/Edit/5
        [HttpPost]
        public ActionResult Edit(Position position)
        {
            try
            {


                Position newPosition = db.Positions.FirstOrDefault(p => p.PositionId == position.PositionId);
                newPosition.title = position.title;
                newPosition.PositionId = position.PositionId;
                db.SaveChanges();


                return PartialView("_EditPosition");
                
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: Position/Delete/5
        public ActionResult Delete(int? id)
        {
            Position position = db.Positions.Find(id);
            db.Positions.Remove(position);
            return View();
        }

   
    }
}
