using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactsApp;

namespace ContactsApp.Controllers
{
    public class ContactsController : Controller
    {
        private ContactModel db = new ContactModel();

        // GET: Contacts
        public ActionResult Index()
        {
            var model = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).ToList();
            return View(model);
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Number,EmailAddress")] Contact contact, string Number, string EmailAddress)
        {
            if (ModelState.IsValid)
            {

                var cntct = new Contact
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    ContactNumbers = new List<ContactNumber>() { new ContactNumber() { Number = Number } },
                    EmailAddresses = new List<Email>() { new Email() { EmailAddress = EmailAddress } }
                };

                db.Contacts.Add(cntct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).ToList();
            Contact contact = model.FirstOrDefault(m => m.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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

        [HttpPost]
        public ActionResult AddContactNumber(int contactId, string contactNumber)
        {
            var contact = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).FirstOrDefault(c => c.Id == contactId);
            var item = new ContactNumber() { Number = contactNumber };
            contact?.ContactNumbers.Add(item);
            db.SaveChanges();

            var id = item.Id;

            const string message = "Saved Successfully!";
            const bool status = true;

            return Json(new { status = status, message = message, id = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteContactNumber(int contactId, int contactNumberId)
        {
            var contact = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).FirstOrDefault(c => c.Id == contactId);
            var item = contact?.ContactNumbers.FirstOrDefault(x => x.Id == contactNumberId);

            const string message = "Record has been deleted successfully.";
            const bool status = true;

            if (item == null) return Json(status);

            item.Contact = null;

            var id = item.Id;

            db.ContactNumbers.Remove(item);
            db.Entry(item).State = EntityState.Deleted;

            db.SaveChanges();
            return Json(new { status, message, id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEmailAddress(int contactId, string emailAddress)
        {
            var contact = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).FirstOrDefault(c => c.Id == contactId);
            var item = new Email() { EmailAddress = emailAddress };
            contact?.EmailAddresses.Add(item);
            db.SaveChanges();

            var id = item.Id;

            const string message = "Saved Successfully!";
            const bool status = true;

            return Json(new { status = status, message = message, id = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmailAddress(int contactId, int emailAddressId)
        {
            var contact = db.Contacts.Include(cn => cn.ContactNumbers).Include(cn => cn.EmailAddresses).FirstOrDefault(c => c.Id == contactId);
            var item = contact?.EmailAddresses.FirstOrDefault(x => x.Id == emailAddressId);

            const string message = "Record has been deleted successfully.";
            const bool status = true;

            if (item == null) return Json(status);

            item.Contact = null;

            var id = item.Id;

            db.Emails.Remove(item);
            db.Entry(item).State = EntityState.Deleted;

            db.SaveChanges();
            return Json(new { status, message, id }, JsonRequestBehavior.AllowGet);
        }
    }
}
