using Microsoft.AspNet.Identity;
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

        public AppUser GetLoggedInUser()
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == User.Identity.GetUserId());
            return (appUser);
        }


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


        
        public ActionResult EditAvailability()
        {
            return View();
        }

        // Availability as it stands is made of strings and is for display purposes only. Availability may eventually
        // need to be readable by the application for comparing it against shifts on the shift exchange.

        [HttpPost]
        public ActionResult EditAvailability(ICollection<Availability> availability)
        {
            AppUser appUser = GetLoggedInUser();
            appUser.Availability = availability;

            return View();
        }


        // The following method is used only by the owner to add and edit positions at the company. It assigns these positions
        // to the organization's AppUser. This may need to be reworked later.
        public ActionResult AddEditOrgPositions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEditOrgPositions(ICollection<Position> positions)
        {
            AppUser appUser = GetLoggedInUser();
            appUser.Positions = positions;
            db.SaveChanges();
            return View();
        }



        // The following methods are used by the owner to add and edit positions that an employee can work. 
        public ActionResult AddEditEmployeePositions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEditEmployeePositions(int id, ICollection<Position> positions)
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.UserId == id);
            appUser.Positions = positions;
            db.SaveChanges();

            return View();
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
