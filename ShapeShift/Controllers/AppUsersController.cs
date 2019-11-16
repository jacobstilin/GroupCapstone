using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ShapeShift.Controllers
{
    //[Authorize]
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        





      
        public AppUser GetLoggedInUser()//Gets the curent logged in user
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);


        }



        // In this method and the corresponding view we need to make the status a readable string and convert the 
        // UserId to the name of the person or something like Admin if it's posted by a manager.
        public ActionResult ShiftExchange()
        {
            AppUser user = GetLoggedInUser();
            bool isEmployee = User.IsInRole("Employee");
            bool isRoleManager = User.IsInRole("Admin");

            if (isEmployee == true)//Provides validation to check if user is an employee
            {
                IList<Shift> employeeShifts = db.Shifts.Where(s => s.UserId == user.UserId).ToList(); //shows all shifts no matter what positions
                IList<Position> employeePositions = db.Positions.Where(e => e.UserId == user.UserId).ToList(); 
                
                ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
                ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");
                return View(employeeShifts);
               
                //no matter who is in the shift exchange they should have the ability too 
            }

            if (isRoleManager == true)//Checks to see if user is in the role of manager 
            {

                IList<Shift> allShifts = db.Shifts.Where(e => e.UserId == user.UserId).ToList();
                ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
                ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");

                return View(allShifts);

            }
    
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AddShift()
        {
            //check user role then return avaiability partial view and status of three in post
            return PartialView();

        }

        [HttpPost]
        public ActionResult AddShift(string userId, string code)
        {
            return PartialView();

        }

        public ActionResult EditShift(int shiftId)
        {
            // when clicking accept shift; if user is Employee then change status to 3, and add to database.  When manager loads page it pop up pending shifts div, user ___ has requested shift---, with dynamically added buttons
            //manager cannot accept shifts, in shift exchange in a hidden div only has pending shifts from pending shift partial view, when they accept it changes status to 2 and adds to the db, and clears pending shifts view
            return PartialView();


        }

        [HttpPost]
        public async Task<ActionResult> EditShift(string userId, string code)
        {
            return PartialView();

        }
        //TWO DIFFERENT PARTIAL VIEWS:: ONE FOR THE AVAILABLE SHIFTS AND OTHER FOR YOUR SHIFTS 
        public ActionResult ViewShifts(string position)
        {
            //create a list of shift objects with this position to be rendering in the partial view, on select event
            return PartialView();
        }

        [HttpPost]
        public ActionResult ViewtShift(string userId, string code)
        {
            return PartialView();

        }

        public ActionResult ViewAvailShifts(string position)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ViewAvailShifts(string userId, string code)
        {
            return PartialView();

        }


        public async Task<ActionResult> DeleteShift(string userId, string code)
        {
            return PartialView();

        }

        public async Task<ActionResult> DeletetShift(string userId, string code)
        {
            return PartialView();

        }


        public ActionResult ViewAllEmployees()


        {
            bool isRoleOwner = User.IsInRole("Owner");
            bool isRoleManager = User.IsInRole("Admin");
            
                if (isRoleOwner == true || isRoleManager == true)//Shows all employees linked to the org
                { 
                    AppUser appUser = GetLoggedInUser();
                    ICollection<AppUser> allEmployees = db.AppUsers.Where(u => u.OrganizationId == appUser.OrganizationId && u.UserId != appUser.UserId).ToList();
                    return View(allEmployees);
                }
            return RedirectToAction("Index", "Home");
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

        public ActionResult ViewAvailability(int id)
        {
            bool isRoleOwner = User.IsInRole("Owner");
            bool isRoleManager = User.IsInRole("Admin");
            if (isRoleOwner == true || isRoleManager == true)
            {
                AppUser appUser = db.AppUsers.FirstOrDefault(u => u.UserId == id);
                ICollection<Availability> availability = db.Availabilities.Where(u => u.UserId == appUser.UserId).ToList();
                return View(availability);
            }
                return RedirectToAction("Index", "Home");
            }

       



        public ActionResult EditAvailability()
        {
            AppUser appUser = GetLoggedInUser();
            ICollection<Availability> availability = db.Availabilities.Where(u => u.UserId == appUser.UserId).ToList();
            return View(availability);
        }

        // Availability as it stands is made of strings and is for display purposes only. Availability may eventually
        // need to be readable by the application for comparing it against shifts on the shift exchange.

        [HttpPost]
        public ActionResult EditAvailability(ICollection<Availability> availability)
        {
            
            AppUser appUser = GetLoggedInUser();
            int avaibsId = db.Availabilities.FirstOrDefault(a => a.UserId == appUser.UserId).Id;
            for (int i = 0; i < availability.Count(); i++)
            {
                var avaibChange = db.Availabilities.FirstOrDefault(a => a.Id == avaibsId); //getting id of the availability list being edited
                Availability setAvaib = availability.ElementAt(i);
                avaibChange.weekday = setAvaib.weekday;
                avaibChange.start = setAvaib.start;
                avaibChange.end = setAvaib.end;
                avaibsId++;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
      
        public ActionResult DeleteUser(int? id)
        {
            var theId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == theId);
            bool isRoleOwner = Roles.IsUserInRole(user.UserName, "Owner");
            if (isRoleOwner == true)
            {
                AppUser appUser = db.AppUsers.Find(id);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        // POST: AppUsers/Delete/
        // to be used in view

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                AppUser appUser = db.AppUsers.Find(id);
                db.AppUsers.Remove(appUser);
                db.SaveChanges();
                // TODO: Add delete logic here
                return RedirectToAction("Index", "Organization");
            }
            catch
            {
                return View();
            }
        }





    }

    
  
}
//bool isRoleOwner = Roles.IsUserInRole(user.UserName, "Owner");
//bool isEmployee = Roles.IsUserInRole(user.UserName, "Employee");
//bool isRoleManager = Roles.IsUserInRole(user.UserName, "Admin");
//            if (isRoleOwner == true)
//            {
//                ViewBag.ShowUser = "Owner";
//            }
//            if (isRoleManager == true)
//            {
//                ViewBag.ShowUser = "Admin";
//            }

//            if (isEmployee == true)
//            {
//                ViewBag.ShowUser = "Employee";
           // }