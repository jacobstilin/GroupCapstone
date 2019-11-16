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

namespace ShapeShift.Controllers
{
    public class EmployeeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();





        // GET: Employee
        public AppUser GetLoggedInUser()//Gets current user
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }

        public ActionResult Index()//shows a list of the shifts attached to this employees id 
            {
            AppUser user = GetLoggedInUser();
            IList<Shift> shifts = db.Shifts.Where(e => e.UserId == user.UserId).ToList();
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
