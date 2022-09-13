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
    public class PadecimientosController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();
        private int idu = new int();
        // GET: Padecimientos
        public ActionResult Index(int usuario)
        {
            ViewBag.id = usuario;
            idu = usuario;
            return View(db.Padecimiento.ToList());
        }

        public ActionResult VistaPadecimientosPaciente(int user)
        {
            var datos = db.PadecimientoPaciente.Where(p => p.paciente == user);
            List<Padecimiento> padecimientos = new List<Padecimiento>();

            foreach (var item in datos)
            {
                padecimientos.Add(item.Padecimiento);
            }

            ViewBag.id = user;

            return View(padecimientos);
        }

        // GET: Padecimientos/Details/5
        public ActionResult Details(decimal id)
        {
            ViewBag.id = idu;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padecimiento padecimiento = db.Padecimiento.Find(id);
            if (padecimiento == null)
            {
                return HttpNotFound();
            }
            return View(padecimiento);
        }

        // GET: Padecimientos/Create
        public ActionResult Create()
        {
            ViewBag.id = idu;
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPadecimiento,nombre,descripcion,tratamiento")] Padecimiento padecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Padecimiento.Add(padecimiento);
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu });
            }

            return View(padecimiento);
        }
        public ActionResult CreatePaciente(decimal id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaciente([Bind(Include = "idPadecimiento,nombre,descripcion,tratamiento")] Padecimiento padecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Padecimiento.Add(padecimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(padecimiento);
        }

        // GET: Padecimientos/Edit/5
        public ActionResult Edit(decimal id)
        {
            ViewBag.id = idu;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padecimiento padecimiento = db.Padecimiento.Find(id);
            if (padecimiento == null)
            {
                return HttpNotFound();
            }
            return View(padecimiento);
        }

        // POST: Padecimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPadecimiento,nombre,descripcion,tratamiento")] Padecimiento padecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(padecimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu });
            }
            return View(padecimiento);
        }

        // GET: Padecimientos/Delete/5
        public ActionResult Delete(decimal id)
        {
            ViewBag.id = idu;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padecimiento padecimiento = db.Padecimiento.Find(id);
            if (padecimiento == null)
            {
                return HttpNotFound();
            }
            return View(padecimiento);
        }

        // POST: Padecimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Padecimiento padecimiento = db.Padecimiento.Find(id);
            db.Padecimiento.Remove(padecimiento);
            db.SaveChanges();
            return RedirectToAction("Index", new { usuario = idu });
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
