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
    public class PacientePlanNutricionalController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: PacientePlanNutricional
        public ActionResult Index(int usuario)
        {
            ViewBag.id = usuario;
            var pacientePlanNutricional = db.PacientePlanNutricional.Where(p => p.idPaciente == usuario).Include(p => p.Paciente).Include(p => p.PlanNutricional).OrderBy(p => p.PlanNutricional.tiempoComida);
            return View(pacientePlanNutricional.ToList());
        }

        // GET: PacientePlanNutricional
        public ActionResult IndexPaciente(int usuario)
        {
            ViewBag.id = usuario;
            var pacientePlanNutricional = db.PacientePlanNutricional.Where(p => p.idPaciente == usuario).Include(p => p.Paciente).Include(p => p.PlanNutricional).OrderBy(p => p.PlanNutricional.tiempoComida);
            return View(pacientePlanNutricional.ToList());
        }

        // GET: PacientePlanNutricional/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PacientePlanNutricional pacientePlanNutricional = db.PacientePlanNutricional.Find(id);
            if (pacientePlanNutricional == null)
            {
                return HttpNotFound();
            }
            return View(pacientePlanNutricional);
        }

        // GET: PacientePlanNutricional/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "Usuario.nombreUsuario");
            ViewBag.idPlan = new SelectList(db.PlanNutricional, "idPlan", "proposito");
            return View();
        }

        // POST: PacientePlanNutricional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPaciente,idPlan,fecha,estado,kcal")] PacientePlanNutricional pacientePlanNutricional)
        {
            if (ModelState.IsValid)
            {
                db.PacientePlanNutricional.Add(pacientePlanNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Usuario, "nombreUsuario", "ocupacion", pacientePlanNutricional.idPaciente);
            ViewBag.idPlan = new SelectList(db.PlanNutricional, "idPlan", "proposito", pacientePlanNutricional.idPlan);
            return View(pacientePlanNutricional);
        }

        // GET: PacientePlanNutricional/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PacientePlanNutricional pacientePlanNutricional = db.PacientePlanNutricional.Find(id);
            if (pacientePlanNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "ocupacion", pacientePlanNutricional.idPaciente);
            ViewBag.idPlan = new SelectList(db.PlanNutricional, "idPlan", "proposito", pacientePlanNutricional.idPlan);
            return View(pacientePlanNutricional);
        }

        // POST: PacientePlanNutricional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,idPlan,fecha,estado,kcal")] PacientePlanNutricional pacientePlanNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacientePlanNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "ocupacion", pacientePlanNutricional.idPaciente);
            ViewBag.idPlan = new SelectList(db.PlanNutricional, "idPlan", "proposito", pacientePlanNutricional.idPlan);
            return View(pacientePlanNutricional);
        }

        // GET: PacientePlanNutricional/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PacientePlanNutricional pacientePlanNutricional = db.PacientePlanNutricional.Find(id);
            if (pacientePlanNutricional == null)
            {
                return HttpNotFound();
            }
            return View(pacientePlanNutricional);
        }

        // POST: PacientePlanNutricional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PacientePlanNutricional pacientePlanNutricional = db.PacientePlanNutricional.Find(id);
            db.PacientePlanNutricional.Remove(pacientePlanNutricional);
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
