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
    [Authorize]
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
 




        public AppUser GetLoggedInUser()//Gets the curent logged in user
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);


        }
        public ActionResult LoadAllAvailableShifts(IList<Shift> shifts)
        {
           IList<Shift> availableShifts =  db.Shifts.Where(e => e.status == 1).ToList();
            return PartialView("_ViewAllAvailableShifts.cshtml", availableShifts);
        }
        public ActionResult LoadMyShifts()
        {
            IList<Shift> myShifts = db.Shifts.Where(e => e.status == 3).ToList();
            return PartialView("_MyShifts.cshtml", myShifts);
        }


        public ActionResult RequestShift(int id)
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;
            Shift shift = db.Shifts.Where(e => e.ShiftId == id).SingleOrDefault();

            shift.status = 2;
            shift.User = appUser;
            shift.ShiftId = id;
            db.SaveChanges();
            IList<Shift> refreshShift = db.Shifts.Where(e => e.status == 1).ToList();

            return PartialView("_ViewAllAvailableShifts", refreshShift);
        }

            // In this method and the corresponding view we need to make the status a readable string and convert the 
            // UserId to the name of the person or something like Admin if it's posted by a manager.
            
            public ActionResult ShiftExchange()
            {
                AppUser user = GetLoggedInUser();
                //bool isEmployee = User.IsInRole("Employee");
                //bool isRoleManager = User.IsInRole("Admin");

                //if (isEmployee == true)//Provides validation to check if user is an employee
                //{
                //call calls function that loads viewallavailableshifts with all shifts
                //    IList<Shift> employeeShifts = db.Shifts.Where(s => s.UserId == user.UserId).ToList(); //shows all shifts no matter what positions
                //    IList<Position> employeePositions = db.Positions.Where(e => e.UserId == user.UserId).ToList(); 

                //    ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
                //    ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");
                //    return View(employeeShifts);

                //    //no matter who is in the shift exchange they should have the ability too 
                //}

                //if (isRoleManager == true)//Checks to see if user is in the role of manager 
                //{
                IList<Shift> allShifts = db.Shifts.ToList();
                ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
                ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");

                return View(allShifts);

                //}

                //return RedirectToAction("Index", "Home");
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
            ViewBag.Name1 = new SelectList(db.Positions.Where(u => !u.title.Contains("")).ToList(), "title", "PositionId");
            ViewBag.Name2 = new SelectList(db.Locations.Where(u => !u.locationName.Contains("")).ToList(), "locationName", "LocationId");
            Shift shift = db.Shifts.Where(e=> e.ShiftId == shiftId).SingleOrDefault();
            // when clicking accept shift; if user is Employee then change status to 3, and add to database.  When manager loads page it pop up pending shifts div, user ___ has requested shift---, with dynamically added buttons
            //manager cannot accept shifts, in shift exchange in a hidden div only has pending shifts from pending shift partial view, when they accept it changes status to 2 and adds to the db, and clears pending shifts view
            return PartialView("~/Views/Manager/_EditIndividualShift.cshtml", shift);


        }

        [HttpPost]
        public ActionResult EditShift(int id, Shift shift)
        {
            AppUser appUser = new AppUser();
            appUser = GetLoggedInUser();
            Shift newShift = db.Shifts.Where(e => e.ShiftId == id).SingleOrDefault();
            newShift.position = shift.position;
            newShift.start = shift.start;
            newShift.end = shift.end;
            newShift.additionalInfo = shift.additionalInfo;
            newShift.UserId = appUser.UserId;
            newShift.status = shift.status;
            newShift.LocationId = shift.LocationId;
            db.SaveChanges();
            return View();
            //return PartialView("~/Views/Shift/_EditShift.cshtml", db.Shifts.ToList());
        }
        //TWO DIFFERENT PARTIAL VIEWS:: ONE FOR THE AVAILABLE SHIFTS AND OTHER FOR YOUR SHIFTS 
        public ActionResult ViewShifts(string position)
        {
            //create a list of shift objects with this position to be rendering in the partial view, on select event
            return PartialView();
        }

        [HttpPost]
        public ActionResult ViewShift(string userId, string code)
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


        public ActionResult DeleteShift(int Id)
        {
            Shift shift = new Shift();
           shift = db.Shifts.Where(e => e.ShiftId == Id).SingleOrDefault();
            db.Shifts.Remove(shift);
            db.SaveChanges();
            return RedirectToAction("Index", "Manager");

        
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

            //if user = Role send to their home index
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


        [HttpPost]
        public ActionResult EditAvailability(ICollection<Availability> availability)
        {
            AppUser appUser = GetLoggedInUser();
            int avaibsId = db.Availabilities.FirstOrDefault(a => a.UserId == appUser.UserId).Id;
            for (int i = 0; i < availability.Count(); i++)
            {
                var avaibChange = db.Availabilities.FirstOrDefault(a => a.Id == avaibsId);
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

        public ActionResult AddMyShiftToExchange(int shiftId)
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;
            Shift shift = db.Shifts.Where(e => e.ShiftId == shiftId).SingleOrDefault();

            shift.status = 1;
            shift.User = null;
            shift.ShiftId = shiftId;
            db.SaveChanges();
            return PartialView("_MyShift", db.Shifts.Where(e => e.status == 3).ToList());

        }
        public ActionResult AddShiftToExchange(int shiftId)
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;
            Shift shift = db.Shifts.Where(e => e.ShiftId == shiftId).SingleOrDefault();

            shift.status = 1;
            shift.User = null;
            shift.ShiftId = shiftId;
            db.SaveChanges();
            return PartialView("_EditShift", db.Shifts.ToList());

        }

        [HttpPost]
        public ActionResult AddShiftToExchange(Shift shift)
        {
            AppUser appUser = GetLoggedInUser();
            DateTime today = DateTime.Today;

            return View(db.Shifts.Where(s => s.UserId == appUser.UserId).ToList());

        }


        public ActionResult EditUser(int id)
        {
            AppUser appUser = db.AppUsers.Where(e => e.UserId == id).SingleOrDefault();
            // when clicking accept shift; if user is Employee then change status to 3, and add to database.  When manager loads page it pop up pending shifts div, user ___ has requested shift---, with dynamically added buttons
            //manager cannot accept shifts, in shift exchange in a hidden div only has pending shifts from pending shift partial view, when they accept it changes status to 2 and adds to the db, and clears pending shifts view
            return View(appUser);


        }

        [HttpPost]
        public ActionResult EditUser(int id, AppUser user)
        {
            AppUser appUser = new AppUser();
            appUser = GetLoggedInUser();
            AppUser new1 = db.AppUsers.Where(e => e.UserId == id).SingleOrDefault();
            new1.UserId = id;
            new1.Positions = user.Positions;
            new1.phoneNumber = user.phoneNumber;
            new1.OrganizationId = appUser.OrganizationId;
            new1.Organization = appUser.Organization;
            new1.middleName = user.middleName;
            new1.Location = user.Location;
            new1.lastName = user.lastName;
            new1.firstName = user.firstName;
            new1.Availability = user.Availability;
            new1.ApplicationUser = user.ApplicationUser;
            new1.ApplicationId = user.ApplicationId;
            db.SaveChanges();

            return PartialView("_EditShift.cshtml", db.Shifts.ToList());

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
                db.AppUsers.Remove(appUser);
                db.SaveChanges();
                return PartialView("_CurrentAppUsers", db.AppUsers.ToList());

            }
            return PartialView("_CurrentAppUsers", db.AppUsers.ToList());
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



