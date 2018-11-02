using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZmW_FinancialPortal.Models;
using ZmW_FinancialPortal.ViewModels;
using static ZmW_FinancialPortal.Models.Email;

namespace ZmW_FinancialPortal.Controllers
{
    public class InvitationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invite
        //public ActionResult Invite()
        //{
        //    return View(db.Households.ToList());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Invite(InviteEmailModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var to = model.ToEmail;
        //            var from = "Financial<zacharywilsonm@gmail.com>";
        //            var email = new MailMessage(from, to)
        //            {
        //                Subject = model.Subject,
        //                Body = $"<p> Email From: <bold>{model.FromName}</bold> ({model.FromEmail})</p><p> Subject:</p><p>{model.Subject}</p><p> Message:</p><p>{model.Body}</p>",
        //                IsBodyHtml = true
        //            };


        //            var svc = new Email ();
        //            //await svc.SendAsync(email);

        //            return View(new EmailModel());
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            await Task.FromResult(0);
        //        }
        //    }
        //    return View(model);
        //}

        // GET: Invitations
        public ActionResult Index()
        {
            return View(db.Invitations.ToList());
        }

        // GET: Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Invite(int Id)
        {
            var invitation = new InvitationViewModel
            {
                HouseholdId = Id
            };
            return View(invitation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Invite(InvitationViewModel invitation)
        {
            var newInvitation = new Invitation
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(3),
                HouseholdId = invitation.HouseholdId,
                Email = invitation.Email,
                Code = Guid.NewGuid()
            };
            db.Invitations.Add(newInvitation);
            db.SaveChanges();

            try
            {
                var from = ConfigurationManager.AppSettings["emailfrom"];

                var callbackUrl = Url.Action("InvitationRegistration", "Account", new { email = newInvitation.Email, code = newInvitation.Code }, protocol: Request.Url.Scheme);

                var email = new MailMessage(from, newInvitation.Email)
                {
                    Subject = "Household Invitation",
                    Body = "You have been invited to join a household at https://zmw-financialportal.azurewebsites.net/. Please click <a href=\"" + callbackUrl + "\">here</a> to join the household.",
                    IsBodyHtml = true
                };
                var svc = new PersonalEmail();
                await svc.SendAsync(email);

                db.Invitations.Add(newInvitation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return RedirectToAction("Index",  new { id = invitation.HouseholdId });
        }


        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Created,HouseholdId,Code,Accepted,Email")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Invitations.Add(invitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invitation);
        }

        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,HouseholdId,Code,Accepted,Email")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
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
