using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using NutriHub.Models;

namespace NutriHub.Controllers
{
    public class PacientesController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Pacientes
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            var total = db.PacientePlanNutricional.Include(p => p.PlanNutricional).Include(p => p.Paciente).ToList();
            /* List<Paciente> pacientes = new List<Paciente>();
             foreach (var item in total)
             {
                 if(item.PlanNutricional.nutricionista == id && !pacientes.Contains(item.Paciente))
                 {
                     pacientes.Add(item.Paciente);
                 }
             }*/
            return View(total);
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario");
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,sexo,ocupacion,fechaNacimiento,edad")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    db.Paciente.Add(paciente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 
                catch (Exception e )
                {
                    ModelState.AddModelError("cedula", "Este paciente ya se encuentra registrado");
                    return View(paciente);
                }
            }

            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", paciente.cedula);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", paciente.cedula);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula,sexo,ocupacion,fechaNacimiento,edad")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula = new SelectList(db.Usuario, "cedula", "nombreUsuario", paciente.cedula);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
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
