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
    public class recibirConsejoController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: recibirConsejo
        public ActionResult Index()
        {
            var recibirConsejo = db.recibirConsejo.Include(r => r.Consejo).Include(r => r.Usuario);
            return View(recibirConsejo.ToList());
        }

        // GET: recibirConsejo/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibirConsejo recibirConsejo = db.recibirConsejo.Find(id);
            if (recibirConsejo == null)
            {
                return HttpNotFound();
            }
            return View(recibirConsejo);
        }

        // GET: recibirConsejo/Create
        public ActionResult Create()
        {
            ViewBag.idConsejo = new SelectList(db.Consejo, "idConsejo", "tituloConsejo");
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario");
            return View();
        }

        // POST: recibirConsejo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConsejo,cedula,tipoConsejo")] recibirConsejo recibirConsejo)
        {
            if (ModelState.IsValid)
            {
                db.recibirConsejo.Add(recibirConsejo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idConsejo = new SelectList(db.Consejo, "idConsejo", "tituloConsejo", recibirConsejo.idConsejo);
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", recibirConsejo.cedula);
            return View(recibirConsejo);
        }

        // GET: recibirConsejo/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibirConsejo recibirConsejo = db.recibirConsejo.Find(id);
            if (recibirConsejo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idConsejo = new SelectList(db.Consejo, "idConsejo", "tituloConsejo", recibirConsejo.idConsejo);
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", recibirConsejo.cedula);
            return View(recibirConsejo);
        }

        // POST: recibirConsejo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConsejo,cedula,tipoConsejo")] recibirConsejo recibirConsejo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recibirConsejo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idConsejo = new SelectList(db.Consejo, "idConsejo", "tituloConsejo", recibirConsejo.idConsejo);
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", recibirConsejo.cedula);
            return View(recibirConsejo);
        }

        // GET: recibirConsejo/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibirConsejo recibirConsejo = db.recibirConsejo.Find(id);
            if (recibirConsejo == null)
            {
                return HttpNotFound();
            }
            return View(recibirConsejo);
        }

        // POST: recibirConsejo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            recibirConsejo recibirConsejo = db.recibirConsejo.Find(id);
            db.recibirConsejo.Remove(recibirConsejo);
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
