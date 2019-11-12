﻿using Microsoft.AspNet.Identity;
using ShapeShift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShapeShift.Controllers
{
    public class ShiftController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            return View();
        }

        // We may need seperate methods for the following:
            // Manager adds shift to an employee's schedule
            // Manager adds shift to shift exchange
            // Employee adds shift to shift exchange
        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            //In shift view, UserId must be hidden based on creation method
            try
            {
                Shift newShift = new Shift();
               
                // If a shift was created by an employee it will have status 3, taken. If created by owner it will 
                // have shift status 1, not taken, as found by the AppUser it is connected to. Remember that GetUserId
                // can not always be used, as different people use the app.

                newShift.position = shift.position;
                newShift.start = shift.start;
                newShift.end = shift.end;
                newShift.additionalInfo = shift.additionalInfo;
                newShift.UserId = shift.UserId;
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
