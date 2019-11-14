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
                newPosition.PositionId = position.PositionId;
                // TODO: Add insert logic here

                db.Positions.Add(newPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Position/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Position position)
        {
            try
            {

                Position newPosition = db.Positions.FirstOrDefault(p => p.PositionId == id);
                newPosition.title = position.title;
                newPosition.PositionId = position.PositionId;
                db.SaveChanges();


                return RedirectToAction("Index", "Organization");
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Delete/5
        public ActionResult Delete(int? id)
        {
            Position position = db.Positions.Find(id);
            return View();
        }

        // POST: Position/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                
               
                    Position position = db.Positions.Find(id);
                    db.Positions.Remove(position);
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
