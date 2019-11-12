using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            var Roles = db.Roles.ToList();
            return View(Roles);

            
        }
    }
}