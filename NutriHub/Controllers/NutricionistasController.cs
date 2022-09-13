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
    public class NutricionistasController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Nutricionistas
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            var nutricionista = db.Nutricionista.Include(n => n.Usuario);
            return View(nutricionista.ToList());
        }

        // GET: Nutricionistas/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionista.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            return View(nutricionista);
        }

        // GET: Nutricionistas/Create
        public ActionResult Create()
        {
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario");
            return View();
        }

        // POST: Nutricionistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,licencia,sexo")] Nutricionista nutricionista)
        {
            if (ModelState.IsValid)
            {
                db.Nutricionista.Add(nutricionista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", nutricionista.cedula);
            return View(nutricionista);
        }

        // GET: Nutricionistas/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionista.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", nutricionista.cedula);
            return View(nutricionista);
        }

        // POST: Nutricionistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula,licencia,sexo")] Nutricionista nutricionista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutricionista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", nutricionista.cedula);
            return View(nutricionista);
        }

        // GET: Nutricionistas/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionista.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            return View(nutricionista);
        }

        // POST: Nutricionistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Nutricionista nutricionista = db.Nutricionista.Find(id);
            db.Nutricionista.Remove(nutricionista);
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
