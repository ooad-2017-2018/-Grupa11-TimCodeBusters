using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Match2DateAsp.Models;

namespace Match2DateAsp.Controllers
{
    public class KontaktisController : Controller
    {
        private M2DbazaEntities db = new M2DbazaEntities();

        // GET: Kontaktis
        public ActionResult Index()
        {
            var kontaktis = db.Kontaktis.Include(k => k.korisnici);
            return View(kontaktis.ToList());
        }

        // GET: Kontaktis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kontakti kontakti = db.Kontaktis.Find(id);
            if (kontakti == null)
            {
                return HttpNotFound();
            }
            return View(kontakti);
        }

        // GET: Kontaktis/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.korisnicis, "id", "ime");
            return View();
        }

        // POST: Kontaktis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,facebook,instagram,brojTelefona,version,createdAt,updatedAt,deleted")] Kontakti kontakti)
        {
            if (ModelState.IsValid)
            {
                db.Kontaktis.Add(kontakti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.korisnicis, "id", "ime", kontakti.id);
            return View(kontakti);
        }

        // GET: Kontaktis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kontakti kontakti = db.Kontaktis.Find(id);
            if (kontakti == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.korisnicis, "id", "ime", kontakti.id);
            return View(kontakti);
        }

        // POST: Kontaktis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,facebook,instagram,brojTelefona,version,createdAt,updatedAt,deleted")] Kontakti kontakti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kontakti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.korisnicis, "id", "ime", kontakti.id);
            return View(kontakti);
        }

        // GET: Kontaktis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kontakti kontakti = db.Kontaktis.Find(id);
            if (kontakti == null)
            {
                return HttpNotFound();
            }
            return View(kontakti);
        }

        // POST: Kontaktis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kontakti kontakti = db.Kontaktis.Find(id);
            db.Kontaktis.Remove(kontakti);
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
