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
    public class ExperiencesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Experiences
        public ActionResult Index()
        {
            var experiences = db.Experiences.Include(e => e.CV);
            return View(experiences.ToList());
        }

        // GET: Experiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom");
            return View();
        }

        // POST: Experiences/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Poste,Description,Periode,CVid")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Experiences.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", experience.CVid);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", experience.CVid);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Poste,Description,Periode,CVid")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CVid = new SelectList(db.CVS, "Id", "Nom", experience.CVid);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Experiences.Find(id);
            db.Experiences.Remove(experience);
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
