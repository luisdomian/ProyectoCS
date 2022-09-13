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
    public class PadecimientoPacientesController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: PadecimientoPacientes
        public ActionResult Index()
        {
            var padecimientoPaciente = db.PadecimientoPaciente.Include(p => p.Paciente1).Include(p => p.Padecimiento);
            return View(padecimientoPaciente.ToList());
        }

        // GET: PadecimientoPacientes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PadecimientoPaciente padecimientoPaciente = db.PadecimientoPaciente.Find(id);
            if (padecimientoPaciente == null)
            {
                return HttpNotFound();
            }
            return View(padecimientoPaciente);
        }

        // GET: PadecimientoPacientes/Create
        public ActionResult Create()
        {
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion");
            ViewBag.idPadecimiento = new SelectList(db.Padecimiento, "idPadecimiento", "nombre");
            return View();
        }

        // POST: PadecimientoPacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPadecimiento,paciente,fechaDiagnostico")] PadecimientoPaciente padecimientoPaciente)
        {
            if (ModelState.IsValid)
            {
                db.PadecimientoPaciente.Add(padecimientoPaciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", padecimientoPaciente.paciente);
            ViewBag.idPadecimiento = new SelectList(db.Padecimiento, "idPadecimiento", "nombre", padecimientoPaciente.idPadecimiento);
            return View(padecimientoPaciente);
        }

        // GET: PadecimientoPacientes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PadecimientoPaciente padecimientoPaciente = db.PadecimientoPaciente.Find(id);
            if (padecimientoPaciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", padecimientoPaciente.paciente);
            ViewBag.idPadecimiento = new SelectList(db.Padecimiento, "idPadecimiento", "nombre", padecimientoPaciente.idPadecimiento);
            return View(padecimientoPaciente);
        }

        // POST: PadecimientoPacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPadecimiento,paciente,fechaDiagnostico")] PadecimientoPaciente padecimientoPaciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(padecimientoPaciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", padecimientoPaciente.paciente);
            ViewBag.idPadecimiento = new SelectList(db.Padecimiento, "idPadecimiento", "nombre", padecimientoPaciente.idPadecimiento);
            return View(padecimientoPaciente);
        }

        // GET: PadecimientoPacientes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PadecimientoPaciente padecimientoPaciente = db.PadecimientoPaciente.Find(id);
            if (padecimientoPaciente == null)
            {
                return HttpNotFound();
            }
            return View(padecimientoPaciente);
        }

        // POST: PadecimientoPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PadecimientoPaciente padecimientoPaciente = db.PadecimientoPaciente.Find(id);
            db.PadecimientoPaciente.Remove(padecimientoPaciente);
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
