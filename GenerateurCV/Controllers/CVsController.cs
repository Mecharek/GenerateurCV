using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenerateurCV.Models;

namespace GenerateurCV.Controllers
{
    public class CVsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: CVs
        public ActionResult Index()
        {
            var cVS = db.CVS.Include(c => c.User);
            return View(cVS.ToList());
        }

        // GET: CVs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // GET: CVs/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nom");
            return View();
        }

        // POST: CVs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,UserId")] CV cV)
        {
            if (ModelState.IsValid)
            {
                db.CVS.Add(cV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Nom", cV.UserId);
            return View(cV);
        }

        // GET: CVs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nom", cV.UserId);
            return View(cV);
        }

        // POST: CVs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,UserId")] CV cV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nom", cV.UserId);
            return View(cV);
        }

        // GET: CVs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CV cV = db.CVS.Find(id);
            db.CVS.Remove(cV);
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

        public ActionResult Apercu(int? id)
        {
            CV cv = db.CVS.Find(id);
            return View(cv);
        }

        public ActionResult PDF(int? id)
        {
            byte[] tab = null;
            CV cv = db.CVS.Find(id);
            using (MemoryStream stream = new MemoryStream())
            {
                string html = GenerateHtml(this, "~/Views/CVs/Apercu.cshtml", cv);
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(stream);
                tab = stream.ToArray();
            }
            
            return File(tab, "application/pdf");
        }

        //Méthode qui génére du html à partir d'une vue cshtml
        public string GenerateHtml(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            var viewEngine = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
            if (viewEngine != null)
            {
                using (var sw = new StringWriter())
                {
                    var viewContext = new ViewContext(controller.ControllerContext, viewEngine.View, controller.ViewData,controller.TempData, sw);
                    viewEngine.View.Render(viewContext, sw);
                    viewEngine.ViewEngine.ReleaseView(controller.ControllerContext, viewEngine.View);
                    return sw.GetStringBuilder().ToString();
                }
            }
            else
            {
                return "Vue introuvable....";
            }
        }
    }
}
