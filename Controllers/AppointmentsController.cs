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
    public class AppointmentsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.doc).Include(a => a.pt).Include(a => a.spec);
            return View(appointment.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.docid = new SelectList(db.AddDoctor, "DID", "DoctorName");
            ViewBag.ptid = new SelectList(db.Patient, "id", "FirstName");
            ViewBag.specid = new SelectList(db.Specialization, "id", "Specname");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ptid,specid,docid,sTime,eTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                bool check = db.Appointment.Where(x => x.docid == appointment.docid && x.sTime <= appointment.sTime && x.eTime >= appointment.eTime).Count() > 0;
                if (!check)
                {
                    db.Appointment.Add(appointment);
                    db.SaveChanges();
                }
                else
                {
                    TempData["msg"] = "Already appointment booked for this time range";
                }
                return RedirectToAction("Index");
            }

            ViewBag.docid = new SelectList(db.AddDoctor, "DID", "DoctorName", appointment.docid);
            ViewBag.ptid = new SelectList(db.Patient, "id", "FirstName", appointment.ptid);
            ViewBag.specid = new SelectList(db.Specialization, "id", "Specname", appointment.specid);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.docid = new SelectList(db.AddDoctor, "DID", "DoctorName", appointment.docid);
            ViewBag.ptid = new SelectList(db.Patient, "id", "FirstName", appointment.ptid);
            ViewBag.specid = new SelectList(db.Specialization, "id", "Specname", appointment.specid);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ptid,specid,docid,dt")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.docid = new SelectList(db.AddDoctor, "DID", "DoctorName", appointment.docid);
            ViewBag.ptid = new SelectList(db.Patient, "id", "FirstName", appointment.ptid);
            ViewBag.specid = new SelectList(db.Specialization, "id", "Specname", appointment.specid);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointment.Find(id);
            db.Appointment.Remove(appointment);
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
