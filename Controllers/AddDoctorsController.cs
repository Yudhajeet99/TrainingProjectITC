using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCAppointment.Models;

namespace MVCAppointment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddDoctorsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: AddDoctors
        public ActionResult Index()
        {
            var addDoctor = db.AddDoctor.Include(a => a.spec);
            return View(addDoctor.ToList());
        }

        // GET: AddDoctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDoctor addDoctor = db.AddDoctor.Find(id);
            if (addDoctor == null)
            {
                return HttpNotFound();
            }
            return View(addDoctor);
        }

        // GET: AddDoctors/Create
        public ActionResult Create()
        {
            ViewBag.specialization = new SelectList(db.Specialization, "id", "Specname");
            return View();
        }

        // POST: AddDoctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DID,DoctorName,specialization,RoomNo,PhoneNo,Email,Gender")] AddDoctor addDoctor)
        {
            if (ModelState.IsValid)
            {
                db.AddDoctor.Add(addDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.specialization = new SelectList(db.Specialization, "id", "Specname", addDoctor.specialization);
            return View(addDoctor);
        }

        // GET: AddDoctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDoctor addDoctor = db.AddDoctor.Find(id);
            if (addDoctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.specialization = new SelectList(db.Specialization, "id", "Specname", addDoctor.specialization);
            return View(addDoctor);
        }

        // POST: AddDoctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DID,DoctorName,specialization,RoomNo,PhoneNo,Email,Gender")] AddDoctor addDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.specialization = new SelectList(db.Specialization, "id", "Specname", addDoctor.specialization);
            return View(addDoctor);
        }

        // GET: AddDoctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDoctor addDoctor = db.AddDoctor.Find(id);
            if (addDoctor == null)
            {
                return HttpNotFound();
            }
            return View(addDoctor);
        }

        // POST: AddDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddDoctor addDoctor = db.AddDoctor.Find(id);
            db.AddDoctor.Remove(addDoctor);
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
