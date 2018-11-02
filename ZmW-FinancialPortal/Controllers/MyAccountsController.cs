using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Controllers
{
    public class MyAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyAccounts
        public ActionResult Index()
        {
            var myAccounts = db.MyAccounts.Include(m => m.Household);
            return View(myAccounts.ToList());
        }

        // GET: MyAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myAccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return HttpNotFound();
            }
            return View(myAccount);
        }

        // GET: MyAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: MyAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance")] MyAccount myAccount)
        {
            if (ModelState.IsValid)
            {
                db.MyAccounts.Add(myAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", myAccount.HouseholdId);
            return View(myAccount);
        }

        // GET: MyAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myAccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", myAccount.HouseholdId);
            return View(myAccount);
        }

        // POST: MyAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance")] MyAccount myAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", myAccount.HouseholdId);
            return View(myAccount);
        }

        // GET: MyAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAccount myAccount = db.MyAccounts.Find(id);
            if (myAccount == null)
            {
                return HttpNotFound();
            }
            return View(myAccount);
        }

        // POST: MyAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyAccount myAccount = db.MyAccounts.Find(id);
            db.MyAccounts.Remove(myAccount);
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
