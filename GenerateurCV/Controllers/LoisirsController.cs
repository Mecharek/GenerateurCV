using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenerateurCV.Models;

namespace GenerateurCV.Controllers
{
    public class LoisirsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Loisirs
        public ActionResult Index()
        {
            var loisir = db.Loisir.Include(l => l.CV);
            return View(loisir.ToList());
        }

        // GET: Loisirs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loisir loisir = db.Loisir.Find(id);
            if (loisir == null)
            {
                return HttpNotFound();
            }
            return View(loisir);
        }

        // GET: Loisirs/Create
        public ActionResult Create()
        {
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom");
            return View();
        }

        // POST: Loisirs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,CVid")] Loisir loisir)
        {
            if (ModelState.IsValid)
            {
                db.Loisir.Add(loisir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", loisir.CVid);
            return View(loisir);
        }

        // GET: Loisirs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loisir loisir = db.Loisir.Find(id);
            if (loisir == null)
            {
                return HttpNotFound();
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", loisir.CVid);
            return View(loisir);
        }

        // POST: Loisirs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,CVid")] Loisir loisir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loisir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", loisir.CVid);
            return View(loisir);
        }

        // GET: Loisirs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loisir loisir = db.Loisir.Find(id);
            if (loisir == null)
            {
                return HttpNotFound();
            }
            return View(loisir);
        }

        // POST: Loisirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loisir loisir = db.Loisir.Find(id);
            db.Loisir.Remove(loisir);
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
