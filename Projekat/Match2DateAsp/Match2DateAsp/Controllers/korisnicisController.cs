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
    public class korisnicisController : Controller
    {
        private M2DbazaEntities db = new M2DbazaEntities();

        // GET: korisnicis
        public ActionResult Index()
        {
            return View(db.korisnicis.ToList());
        }

        // GET: korisnicis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            korisnici korisnici = db.korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        // GET: korisnicis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: korisnicis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ime,prezime,email,sifra,spol,grad,opis,aktivan,datumRodjenja,ocjena,deleted,version,createdAt,updatedAt,slika1,slika2,slika3")] korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                db.korisnicis.Add(korisnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(korisnici);
        }

        // GET: korisnicis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            korisnici korisnici = db.korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        // POST: korisnicis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ime,prezime,email,sifra,spol,grad,opis,aktivan,datumRodjenja,ocjena,deleted,version,createdAt,updatedAt,slika1,slika2,slika3")] korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(korisnici);
        }

        // GET: korisnicis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            korisnici korisnici = db.korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        // POST: korisnicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            korisnici korisnici = db.korisnicis.Find(id);
            db.korisnicis.Remove(korisnici);
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
