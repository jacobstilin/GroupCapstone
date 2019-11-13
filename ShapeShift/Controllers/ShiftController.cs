using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class ShiftController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public AppUser GetLoggedInUser()
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == User.Identity.GetUserId());
            return (appUser);
        }
        // Returns the AppUser that is currently logged in


        // GET: Shift
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shift/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shift/Create
        public ActionResult Create()
        {
            return View();
        }

        // We may need seperate methods for the following:
            // Manager adds shift to an employee's schedule
            // Manager adds shift to shift exchange
            // Employee adds shift to shift exchange
        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            //In shift view, UserId must be hidden based on creation method
            try
            {
                Shift newShift = new Shift();
               
                // If a shift was created by an employee it will have status 3, taken. If created by owner it will 
                // have shift status 1, not taken, as found by the AppUser it is connected to. Remember that GetUserId
                // can not always be used, as different people use the app.

                newShift.position = shift.position;
                newShift.start = shift.start;
                newShift.end = shift.end;
                newShift.additionalInfo = shift.additionalInfo;
                newShift.UserId = shift.UserId;
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        // Method gets a list of all shifts for the logged in user
        public ActionResult GetEmployeeSchedule()
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;

            return View(db.Shifts.Where(s => s.UserId == appUser.UserId).ToList());
            
        }
        // Figure out how to use DateTime.Today to only show the current week or today and the next six days

        // The following method filters the entire list of shifts based employee, start date, status and position
        // Make sure this works when not all parameters are passed

        // This definitely needs to be improve
        public ActionResult FilterShiftList(int id, DateTime date, int status, string position)
        {
            // string value = date.ToShortDateString();

            return View(db.Shifts.Where(s => s.UserId == id && s.start == date && s.status == status && s.position == position));
        }


        // GET: Shift/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shift/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Shift shift)
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

        // GET: Shift/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shift/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Shift shift)
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
