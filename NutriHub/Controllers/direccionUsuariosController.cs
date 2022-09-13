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
    public class direccionUsuariosController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: direccionUsuarios
        public ActionResult Index()
        {
            var direccionUsuario = db.direccionUsuario.Include(d => d.Usuario);
            return View(direccionUsuario.ToList());
        }

        // GET: direccionUsuarios/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direccionUsuario direccionUsuario = db.direccionUsuario.Find(id);
            if (direccionUsuario == null)
            {
                return HttpNotFound();
            }
            return View(direccionUsuario);
        }

        // GET: direccionUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario");
            return View();
        }

        // POST: direccionUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,provincia,canton,distrito")] direccionUsuario direccionUsuario)
        {
            if (ModelState.IsValid)
            {
                db.direccionUsuario.Add(direccionUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", direccionUsuario.cedula);
            return View(direccionUsuario);
        }

        // GET: direccionUsuarios/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direccionUsuario direccionUsuario = db.direccionUsuario.Find(id);
            if (direccionUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", direccionUsuario.cedula);
            return View(direccionUsuario);
        }

        // POST: direccionUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula,provincia,canton,distrito")] direccionUsuario direccionUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccionUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", direccionUsuario.cedula);
            return View(direccionUsuario);
        }

        // GET: direccionUsuarios/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            direccionUsuario direccionUsuario = db.direccionUsuario.Find(id);
            if (direccionUsuario == null)
            {
                return HttpNotFound();
            }
            return View(direccionUsuario);
        }

        // POST: direccionUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            direccionUsuario direccionUsuario = db.direccionUsuario.Find(id);
            db.direccionUsuario.Remove(direccionUsuario);
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
