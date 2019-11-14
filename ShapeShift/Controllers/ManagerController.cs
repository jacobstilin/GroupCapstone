using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.Rest.Api.V2010.Account;

namespace ShapeShift.Controllers
{
    public class ManagerController : Controller
    {
        
        
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendText(string phoneNumber, string Message)
        {
            const string accountSid = "AC3b1a400c4343537508f47488b4542f97";
            const string authToken = "aa474c6417dfce7a1c98c64aba6f16e6";
            Twilio.TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: Message,
                from: new Twilio.Types.PhoneNumber(phoneNumber),
                to: new Twilio.Types.PhoneNumber("+12628047192")
            );
            Console.WriteLine(message.Sid);
            return View();
        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
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

        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
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

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
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
