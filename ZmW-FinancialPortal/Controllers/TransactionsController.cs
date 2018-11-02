﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZmW_FinancialPortal.Models;

namespace ZmW_FinancialPortal.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.MyAccount).Include(t => t.EnteredBy).Include(t => t.TransactionType);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            //ViewBag.BankAccountId = new SelectList(db.MyAccounts, "Id", "Name");
            //ViewBag.EnteredById = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,TransactionTypeId,BudgetItemId,EnteredById,Description,Date,Amount,Reconciled,ReconciledAmount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var houseId = db.MyAccounts.Find(transaction.MyAccountId).HouseholdId;
                transaction.Date = DateTime.Now;
                transaction.EnteredById = User.Identity.GetUserId();
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index", "Households", new { id = houseId });
            }

            ViewBag.MyAccountId = new SelectList(db.MyAccounts, "Id", "Name", transaction.MyAccountId);
            //ViewBag.EnteredById = new SelectList(db.ApplicationUser, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyAccountId = new SelectList(db.MyAccounts, "Id", "Name", transaction.MyAccountId);
            //ViewBag.EnteredById = new SelectList(db.ApplicationUser, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,TransactionTypeId,BudgetItemId,EnteredById,Description,Date,Amount,Reconciled,ReconciledAmount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MyAccountId = new SelectList(db.MyAccounts, "Id", "Name", transaction.MyAccountId);
            //ViewBag.EnteredById = new SelectList(db.ApplicationUser, "Id", "FirstName", transaction.EnteredById);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
