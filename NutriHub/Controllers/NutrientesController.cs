using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutriHub.Models;

namespace NutriHub.Controllers
{
    public class NutrientesController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Nutrientes
        public ActionResult Index(decimal id)
        {
            ViewBag.id = id;
            return View(db.Nutriente.ToList());
        }

        // GET: Nutrientes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // GET: Nutrientes/Create
        public ActionResult Create( int id )
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Nutrientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNutriente,nombre")] Nutriente nutriente)
        {
            if (ModelState.IsValid)
            {
                db.Nutriente.Add(nutriente);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = nutriente.idNutriente });
            }

            return View(nutriente);
        }

        // GET: Nutrientes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // POST: Nutrientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idNutriente,nombre")] Nutriente nutriente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutriente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = nutriente.idNutriente });
            }
            return View(nutriente);
        }

        // GET: Nutrientes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // POST: Nutrientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Nutriente nutriente = db.Nutriente.Find(id);
            db.Nutriente.Remove(nutriente);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = nutriente.idNutriente });
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
