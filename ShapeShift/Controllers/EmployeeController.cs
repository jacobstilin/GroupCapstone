﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShapeShift.Controllers
{
    public class EmployeeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();


        //Employee LOAD
        public ActionResult LoadMyShifts()
        {
            try
            {
                AppUser appUser = GetLoggedInUser();

                IList<Shift> shift = db.Shifts.Where(e => e.UserId == appUser.UserId).ToList();
                return PartialView("_CurrentSchedule.cshtml", shift);
            }
            catch
            {
                return PartialView("_CurrentSchedule.cshtml");
            }
        }

        public ActionResult LoadMyAvailability()
        {
            try
            {
                AppUser appUser = GetLoggedInUser();

                IList<Availability> availability = db.Availabilities.Where(e => e.UserId == appUser.UserId).ToList();
                return PartialView("_ViewMyAvailability.cshtml", availability);
            }

            catch
            {
                return PartialView("_ViewMyAvailability.cshtml");
            }
        }

        public void LoadEmployee()
        {
            LoadMyShifts();
            LoadMyAvailability();
            
        }

        // GET: Employee
        public AppUser GetLoggedInUser()//Gets current user
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }

        public ActionResult Index()//shows a list of the shifts attached to this employees id 
            {
            LoadEmployee();
            DateTime today = DateTime.Today;
            Shift newShift = new Shift();
            Availability availability = new Availability();
            Shift newShift1 = new Shift();
            Availability availability1 = new Availability();
            db.Availabilities.Add(availability);
            db.Availabilities.Add(availability1);
            db.Shifts.Add(newShift);
            db.Shifts.Add(newShift1);
            ViewBag.Name1 = new SelectList(db.Locations.ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Positions.ToList(), "locationName", "LocationId");
            ViewBag.Name4 = new SelectList(db.Shifts.ToList(), "ShiftId", "ShiftId");
            ViewBag.Name5 = new SelectList(db.Availabilities.ToList(), "Id", "Availability");
            IList<Shift> shifts = db.Shifts.Where(e => e.start >= today).ToList();

            AppUser user = GetLoggedInUser();
            ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Owner")).ToList(), "Name", "Name");

            return View(shifts);
            }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)//Allows you to edit the employees information 
        {
            bool isRoleOwner = User.IsInRole("Owner");
            bool isRoleManager = User.IsInRole("Admin");
            if (isRoleOwner == true || isRoleManager == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AppUser appUser)//post action to change the employees information 
        {
            try
            {
                AppUser newUser = db.AppUsers.FirstOrDefault(a => a.UserId == id);
                newUser.firstName = appUser.firstName;
                newUser.middleName = appUser.middleName;
                newUser.lastName = appUser.lastName;
                newUser.phoneNumber = appUser.phoneNumber;

                db.SaveChanges();

                return RedirectToAction("Index", "Organization");

            }
            catch
            {
                return View();

            }
        }


        //public ActionResult EditEmployeePositions()//Called when you 
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult EditEmployeePositions(int id, ICollection<Position> positions)
        //{
        //    AppUser appUser = db.AppUsers.FirstOrDefault(u => u.UserId == id);
        //    appUser.Positions = positions;
        //    db.SaveChanges();

        //    return View();
        //}

        // GET: Employee/Delete/5
        public ActionResult EditEmployee(int id)//Allows you to edit the employees information 
        {
           AppUser employee = db.AppUsers.Where(e => e.UserId == id).SingleOrDefault();
                return PartialView();
            
        }

      


        public ActionResult DeleteEmployee(int id)
        {
            bool isRoleOwner = User.IsInRole("Owner");
            bool isRoleManager = User.IsInRole("Admin");
            if (isRoleOwner == true || isRoleManager == true)
            {
                AppUser appUser = db.AppUsers.Where(e => e.UserId == id).SingleOrDefault();
                db.AppUsers.Remove(appUser);
                return PartialView();

            }
            return RedirectToAction("Index", "Home");

           
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult DeleteEmployee(int id, AppUser appUser)
        {
            try
            {
                // TODO: Add delete logic here
                AppUser appUser1 = db.AppUsers.Find(id);
                db.AppUsers.Remove(appUser1);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
