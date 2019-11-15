﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShapeShift.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public OrganizationController()
        {

        }

        public OrganizationController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Organization

        public AppUser GetLoggedInUser()
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == User.Identity.GetUserId());
            //could we add a get role inside of this to attach to app user for authentication purposes
            return (appUser);
        }
        public ActionResult Index()
        {
            var theId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == theId);
            bool isRole = Roles.IsUserInRole(user.UserName, "Owner");
            if (isRole == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Organization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Organization/Create
        public ActionResult Create()
        {


            return View();
        }

        // Not currently used as creation occurs during register
        [HttpPost]
        public ActionResult Create([Bind(Include = "OrganizationId,Organization Name")] Organization organization)
        {
            try
            {
                db.Organizations.Add(organization);
                AppUser appUser = new AppUser();
                appUser.ApplicationId = User.Identity.GetUserId();
                appUser.OrganizationId = organization.OrganizationId;
                db.AppUsers.Add(appUser);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditPositions()
        {
            return View();
        }


        // Takes array passed from positions edit view and assigns it as the positions for the array
        [HttpPost]
        public ActionResult EditPositions(string[] positions)
        {
            AppUser appUser = GetLoggedInUser();
            Organization organization = db.Organizations.FirstOrDefault(o => o.OrganizationId == appUser.OrganizationId);
           
            db.SaveChanges();
            return View();
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(int id)
        {


            return View();
        }

        // POST: Organization/Edit/5
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

        // GET: Organization/Delete/5
        public ActionResult Delete(int id)
        {
            Organization organization = db.Organizations.Find(id);
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Organization organization = db.Organizations.Find(id);
                db.Organizations.Remove(organization);
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
