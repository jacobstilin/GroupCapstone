using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ShapeShift.Controllers
{
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult SendText(string phoneNumber,string Message)
        {
            const string accountSid = "AC3b1a400c4343537508f47488b4542f97";
            const string authToken = "aa474c6417dfce7a1c98c64aba6f16e6";
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body:Message,
                from: new Twilio.Types.PhoneNumber(phoneNumber),
                to: new Twilio.Types.PhoneNumber("+12628047192")
            );
            Console.WriteLine(message.Sid);
            return View();
        }
        public AppUser GetLoggedInUser()
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }



        // In this method and the corresponding view we need to make the status a readable string and convert the 
        // UserId to the name of the person or something like Admin if it's posted by a manager.
        public ActionResult ShiftExchange()
        {

            return View(db.Shifts.Where(s => s.status == 1 || s.status == 2).ToList());
        }

        public ActionResult ViewAllEmployees()
        {
            AppUser appUser = GetLoggedInUser();
            ICollection<AppUser> allEmployees = db.AppUsers.Where(u => u.OrganizationId == appUser.OrganizationId && u.UserId != appUser.UserId).ToList();
            return View(allEmployees);
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


        // GET: AppUsers/Delete/5
      
        public ActionResult DeleteUser(int? id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            return View();
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
