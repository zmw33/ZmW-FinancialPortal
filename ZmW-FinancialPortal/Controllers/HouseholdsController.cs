﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZmW_FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using ZmW_FinancialPortal.Helpers;

namespace ZmW_FinancialPortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseholdHelp HouseholdHelper = new HouseholdHelp();
        private MembersHelp MembersHelper = new MembersHelp();

        //GET: Households1
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var me = db.Users.Find(userId);
            var myHouseId = me.HouseholdId;
            if (myHouseId != null)
            {
                return View(db.Households.Find(myHouseId));
            }
            
            return RedirectToAction("Index", "Home");
        }

        //GET: Households/Members
        [Authorize]
        public ActionResult Members()
        {            
            var userId = User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;           
            return View(db.Users.Where(u => u.HouseholdId == householdId).ToList());
        }

        // GET: Households1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households1/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CreatedByUser = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Households1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();

                var houseHead = User.Identity.GetUserId();
                HouseholdHelper.AddUserToHousehold(houseHead, household.Id);
                RoleHelper.AddUserToRole(houseHead, "HoH");
                household.CreatedByUserId = houseHead;
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        //// GET: Leave Household
        //public ActionResult Leave()
        //{
        //    return View();
        //}

        //// POST: Leave Household
        //public ActionResult Leave(Household household)
        //{
        //    if (User.IsInRole("HoH") == false)
        //    {
        //        var userId = User.Identity.GetUserId();
        //        var houseHead = User.Identity.GetUserId();
        //        HouseholdHelper.RemoveUserFromHouse(houseHead, household.Id);
        //        RoleHelper.RemoveUserFromRole(userId, "HoH, Member");
        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        // GET: Households1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
