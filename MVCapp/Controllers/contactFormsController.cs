using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVCapp.Models;

namespace MVCapp.Controllers
{
    public class contactFormsController : Controller
    {
        private MVCappContext db = new MVCappContext();

        // GET: contactForms
        public ActionResult Index()
        {
            return View(db.contactForms.ToList());
        }

        [HttpGet]
        public PartialViewResult pullUpContactForm()
        {
            return PartialView("Edit");
        }

        // GET: contactForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactForm contactForm = db.contactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // GET: contactForms/Create
        public ActionResult Create()
        {
            return View();
        }
        protected void SendMail()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(contactForm model)
        {
            string ReciverEmail = "ramialhussein98@gmail.com";
            string password = "Hassan111";
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: " + model.email;
                var message = new MailMessage();
                message.To.Add(new MailAddress(ReciverEmail));
                message.From = new MailAddress(model.email);
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.firstName, model.msg);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = ReciverEmail,
                        Password = password,

                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    return RedirectToAction("Create");
                }
            }
            return View("ThankYou");
        }

        public ActionResult Sent()
        {
            return View("ThankYou");
        }

        // GET: contactForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactForm contactForm = db.contactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: contactForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contactFormId,firstName,email,Subject,msg")] contactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactForm);
        }

        // GET: contactForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contactForm contactForm = db.contactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: contactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contactForm contactForm = db.contactForms.Find(id);
            db.contactForms.Remove(contactForm);
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
