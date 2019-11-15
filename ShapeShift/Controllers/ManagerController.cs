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
    [Authorize]
    public class ManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult PrivateMessage()
        {
            AppUser appUser = GetLoggedInUser();
            IList<AppUser> coWorkers = db.AppUsers.Where(e => e.OrganizationId == appUser.OrganizationId).ToList();
            return View(coWorkers);
        }
        
        public ActionResult Composs(int id)
        {
            TextMessage textMessage = new TextMessage();
            textMessage.id = id; 
            return View(textMessage);
        }
        [HttpPost]
        public ActionResult Composs(TextMessage textMessage)
        {
            var recipient = db.AppUsers.Where(e => e.UserId == textMessage.id).FirstOrDefault(); 
            var number = recipient.phoneNumber;
            number = "+1" + number;
            const string accountSid = "AC3b1a400c4343537508f47488b4542f97";
            const string authToken = "aa474c6417dfce7a1c98c64aba6f16e6";
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
            body: textMessage.BodyOfMessage,
            from: new Twilio.Types.PhoneNumber("+12622179385"),
            to: new Twilio.Types.PhoneNumber(number)
            );
            Console.WriteLine(message.Sid);
            return RedirectToAction("Index");
        }
        public ActionResult SendGroupText()
        {
            return View(); 
        }
        [HttpPost]
         public ActionResult GroupMessage(TextMessage textMessage)
        {
             if(textMessage.Postion != null)
            {
                var userList = db.AppUsers.Where(e => e.Positions.Where(b => b.title == textMessage.Postion).FirstOrDefault().ToString() == textMessage.Postion);
                foreach (var item in userList)
                {
                    var number = item.phoneNumber;
                    number = "+1" + number;  
                    const string accountSid = "AC3b1a400c4343537508f47488b4542f97";
                    const string authToken = "aa474c6417dfce7a1c98c64aba6f16e6";
                    TwilioClient.Init(accountSid, authToken);
                    var message = MessageResource.Create(
                    body: textMessage.BodyOfMessage,
                    from: new Twilio.Types.PhoneNumber("+12622179385"),
                    to: new Twilio.Types.PhoneNumber(number)
                    );
                    Console.WriteLine(message.Sid);
                   
                }
               
            }
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
            //sends to register manager page
            return View();
        }

        // POST: Manager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {                  //return to manager edit for first and last name pass id as well
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
        public AppUser GetLoggedInUser()
        {
            string currentId = User.Identity.GetUserId();
            AppUser appUser = db.AppUsers.FirstOrDefault(u => u.ApplicationId == currentId);
            return (appUser);
        }

    }
}
