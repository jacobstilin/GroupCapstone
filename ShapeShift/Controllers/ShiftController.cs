using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ShapeShift.Controllers
{
    [Authorize]
    public class ShiftController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      
public AppUser GetLoggedInUser()
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }
        // Returns the AppUser that is currently logged in


        public ActionResult AddShiftToSchedule()
        {
            //Add employee to shift, and add shift to database
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;

            return View(db.Shifts.Where(s => s.UserId == appUser.UserId).ToList());

        }

        [HttpPost]
        public ActionResult AddShiftToSchedule(Shift shift)
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;

            return View(db.Shifts.Where(s => s.UserId == appUser.UserId).ToList());

        }
        public ActionResult ApproveShift(int shiftId)
        {

            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;
            Shift shift = db.Shifts.Where(e => e.ShiftId == shiftId).SingleOrDefault();

            if (shift.UserId != 0 && shift.status != 1)//means there is a user who is requesting to exchange but its just pending
            {
                shift.status = 1;
                shift.User = null;
                shift.UserId = 0;
                shift.ShiftId = shiftId;
                db.SaveChanges();


            }
            else //its pending and theres no user id, and its in shift exchange
            {
             
                shift.status = 3;
                shift.ShiftId = shiftId;

                db.SaveChanges();
            }
            db.SaveChanges();
            return PartialView("~/Views/Manager/_PendingShifts.cshtml", db.Shifts.Where(e => e.status == 2));

        }

        public ActionResult RejectShift(int shiftId)
        {
            Shift shift = db.Shifts.Where(e => e.ShiftId == shiftId).SingleOrDefault();

            if (shift.UserId != 0 && shift.status != 1)//means there is a user who is requesting to exchange but its just pending
            {
                shift.status = 3;
                shift.ShiftId = shiftId;
                db.SaveChanges();


            }
            else //its pending and theres no user id, and its in shift exchange
            {

                shift.status = 1;
                shift.User = null;
                shift.UserId = 0;
                shift.ShiftId = shiftId;

                db.SaveChanges();
            }
            db.SaveChanges();
            return PartialView("~/Views/Manager/_PendingShifts.cshtml", db.Shifts.Where(e => e.status == 2));


        }



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
            // When a shift is created the viewbag displaymenu is passed the role of the user

            var theId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == theId);
            bool isRoleOwner = Roles.IsUserInRole(user.UserName, "Owner");
            bool isRoleManager = Roles.IsUserInRole(user.UserName, "Admin");
            ViewBag.displayMenu = "Employee";
            if (isRoleOwner == true)
            {
                var ownerRole = db.Roles.Where(e => e.Name == "Owner").SingleOrDefault();
                var ownerOfCompany = db.Users.Where(e => e.Roles == ownerRole).SingleOrDefault();
                AppUser theBoss = db.AppUsers.Where(e => e.ApplicationId == ownerOfCompany.Id).SingleOrDefault();
                var ownerPositions = theBoss.Positions.ToList();
                var ownerLocations = db.Locations.ToList();
                ViewBag.Name1 = new SelectList(ownerPositions, "title", "PositionId");
                ViewBag.Name2 = new SelectList(ownerLocations, "locationName", "LocationId");
            }
            if (isRoleManager == true)
            {
                var ownerRole = db.Roles.Where(e => e.Name == "Owner").SingleOrDefault();
                var ownerOfCompany = db.Users.Where(e => e.Roles == ownerRole).SingleOrDefault();
                AppUser theBoss = db.AppUsers.Where(e => e.ApplicationId == ownerOfCompany.Id).SingleOrDefault();
                var ownerPositions = theBoss.Positions.ToList();
                var ownerLocations = db.Locations.ToList();
                IList<AppUser> AppUsers = db.AppUsers.Where(e => e.OrganizationId == theBoss.OrganizationId).ToList();
                ViewBag.Name1 = new SelectList(ownerPositions, "title", "PositionId");
                ViewBag.Name2 = new SelectList(ownerLocations, "locationName", "LocationId");
                ViewBag.Name3 = new SelectList(AppUsers, "firstName", "UserId");
                ViewBag.displayMenu = "Admin";
            }
                return View();
        }  //Is not a partial view

       
        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            //In shift view, UserId must be hidden based on creation method
            
                Shift newShift = new Shift();
                AppUser appUser = GetLoggedInUser();
                // If a shift was created by an employee it will have status 3, taken. If created by owner it will 
                // have shift status 1, not taken, as found by the AppUser it is connected to. Remember that GetUserId
                // can not always be used, as different people use the app.

                newShift.position = shift.position;
                newShift.start = shift.start;
                newShift.end = shift.end;
                newShift.additionalInfo = shift.additionalInfo;
                newShift.UserId = appUser.UserId;
                newShift.status = shift.status;
            newShift.LocationId = shift.LocationId;
                db.Shifts.Add(newShift);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            
            
        }

        
        // Method gets a list of all shifts for the logged in user
        public ActionResult GetEmployeeSchedule()
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;

            return View(db.Shifts.Where(s => s.UserId == appUser.UserId && s.start >= today).ToList());
            
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

        public ActionResult ShiftExchange()
        {

            return View(db.Shifts.Where(s => s.status == 1 || s.status == 2).ToList());
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
