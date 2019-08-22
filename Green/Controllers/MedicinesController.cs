using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;
//https://www.codeproject.com/Articles/786085/ASP-NET-MVC-List-Editor-with-Bootstrap-Modals

namespace Green.Controllers
{
    public class MedicinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index(int id)
        {
            ViewBag.PersonID = id;

            var ai = new List<Medicine>();
            
            var addresses = db.Medicines.Where(a => a.PrescriptionID == id).OrderBy(a => a.MedicineName);

            return PartialView("_Index", addresses.ToList());
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.PersonID = id;
            var addresses = db.Medicines.Where(a => a.PrescriptionID == id);

            return PartialView("_List", addresses.ToList());
        }

        public ActionResult Create(int PersonID)
        {
            Medicine address = new Medicine();
            address.PrescriptionID = PersonID;

            return PartialView("_Create", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,City,Street,Phone,PersonID")] Medicine address)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(address);
                db.SaveChanges();

                string url = Url.Action("Index", "Medicines", new { id = address.PrescriptionID });
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", address);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine address = db.Medicines.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,City,Street,Phone,PersonID")] Medicine address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "Medicines", new { id = address.PrescriptionID });
                return Json(new { success = true, url = url });
            }


            return PartialView("_Edit", address);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine address = db.Medicines.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine address = db.Medicines.Find(id);
            db.Medicines.Remove(address);
            db.SaveChanges();

            string url = Url.Action("Index", "Medicines", new { id = address.PrescriptionID });
            return Json(new { success = true, url = url });

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
