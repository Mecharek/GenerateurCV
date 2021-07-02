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
    public class FormationDiplomesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: FormationDiplomes
        public ActionResult Index()
        {
            var formationDiplomes = db.FormationDiplomes.Include(f => f.CV);
            return View(formationDiplomes.ToList());
        }

        // GET: FormationDiplomes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormationDiplome formationDiplome = db.FormationDiplomes.Find(id);
            if (formationDiplome == null)
            {
                return HttpNotFound();
            }
            return View(formationDiplome);
        }

        // GET: FormationDiplomes/Create
        public ActionResult Create()
        {
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom");
            return View();
        }

        // POST: FormationDiplomes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Intitule,Date,CVid")] FormationDiplome formationDiplome)
        {
            if (ModelState.IsValid)
            {
                db.FormationDiplomes.Add(formationDiplome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", formationDiplome.CVid);
            return View(formationDiplome);
        }

        // GET: FormationDiplomes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormationDiplome formationDiplome = db.FormationDiplomes.Find(id);
            if (formationDiplome == null)
            {
                return HttpNotFound();
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", formationDiplome.CVid);
            return View(formationDiplome);
        }

        // POST: FormationDiplomes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Intitule,Date,CVid")] FormationDiplome formationDiplome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formationDiplome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", formationDiplome.CVid);
            return View(formationDiplome);
        }

        // GET: FormationDiplomes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormationDiplome formationDiplome = db.FormationDiplomes.Find(id);
            if (formationDiplome == null)
            {
                return HttpNotFound();
            }
            return View(formationDiplome);
        }

        // POST: FormationDiplomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormationDiplome formationDiplome = db.FormationDiplomes.Find(id);
            db.FormationDiplomes.Remove(formationDiplome);
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
