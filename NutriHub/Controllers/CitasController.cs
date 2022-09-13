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
    public class CitasController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Citas
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            var cita = db.Cita.Where(x => x.idNutricionista == id ).Include(c => c.Nutricionista).Include(c => c.Paciente);
            return View(cita.ToList());
        }

        public ActionResult IndexCitaPaciente(int id)
        {
            var cita = db.Cita.Where(p => p.idPaciente == id).Include(c => c.Nutricionista).Include(c => c.Paciente);
            ViewBag.id = id;
            return View(cita.ToList());
        }
        
        // GET: Citas/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }

            ViewBag.id = id;
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            ViewBag.idNutricionista = new SelectList(db.Nutricionista, "cedula", "cedula");
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "Usuario.nombreUsuario");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCita,idNutricionista,idPaciente,fecha,hora,estado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cita.idNutricionista });
            }

            ViewBag.idNutricionista = new SelectList(db.Nutricionista, "cedula", "cedula", cita.idNutricionista);
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "Usuario.nombreUsuario", cita.idPaciente);
          
            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.idNutricionista = new SelectList(db.Nutricionista, "cedula", "cedula", cita.idNutricionista);
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "Usuario.nombreUsuario", cita.idPaciente);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCita,idNutricionista,idPaciente,fecha,hora,estado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cita.idNutricionista });
            }
            ViewBag.idNutricionista = new SelectList(db.Nutricionista, "cedula", "cedula", cita.idNutricionista);
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "ocupacion", cita.idPaciente);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Cita cita = db.Cita.Find(id);
            db.Cita.Remove(cita);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = cita.idNutricionista });
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
