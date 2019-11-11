using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EmployeeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public async Task<ActionResult> Create(AppUser appUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = appUser.Email, Email = appUser.Email };
                    var result = await UserManager.CreateAsync(user, appUser.Password);
                    
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        AppUser newUser = new AppUser();
                        var ownerId = User.Identity.GetUserId();
                        AppUser owner = db.AppUsers.FirstOrDefault(a => a.ApplicationId == ownerId);
                        newUser.firstName = appUser.firstName;
                        newUser.middleName = appUser.middleName;
                        newUser.lastName = appUser.lastName;
                        newUser.ApplicationId = user.Id; // if it doesn't work, improve
                        newUser.OrganizationId = owner.OrganizationId;
                        // organization ID of person logged in is assigned to new employee

                        // add additional lines as AppUsers model is expanded

                        db.AppUsers.Add(newUser);
                        db.SaveChanges();

                        return RedirectToAction("Create", "OrganizationController");

                        // Organization create can only be reached after registration OR upon login if creation has not occured
                    }
                    AddErrors(result);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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
