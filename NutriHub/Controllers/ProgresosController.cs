using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutriHub.Models;

namespace NutriHub.Controllers
{
    public class ProgresosController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();
        int idu = new int();
        // GET: Progresos
        public ActionResult Index(int usuario)
        {
            ViewBag.id = usuario;
            var progreso = db.Progreso.Include(p => p.Paciente1);
            return View(progreso.ToList());
        }

        public ActionResult IndexPaciente(/*int id*/)
        {
            /*var progreso = db.Progreso.Where(p => p.paciente == id).Include(p => p.Paciente1);
            ViewBag.id = id;
            return View(progreso.ToList());*/
            var progreso = db.Progreso.Include(p => p.Paciente1).ToList();
            return View(progreso);
        }

        // GET: Progresos/Details/5
        public /*async*/ ActionResult Details(/*decimal idNutr,*/ decimal id, int usuario)
        {
            ViewBag.id = usuario;
            if (/*idNutr == null ||*/ id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var progreso = await db.Progreso.Where(x => x.Paciente1.cedula == idPac).FirstOrDefaultAsync();
            Progreso progreso = db.Progreso.Find(id);
            if (progreso == null)
            {
                return HttpNotFound();
            }
            return View(progreso);
        }

        // GET: Progresos/Create
        public ActionResult Create(int usuario)
        {
            ViewBag.id = usuario;
            idu = usuario;
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion");
            return View();
        }

        // POST: Progresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProgreso,paciente,talla,peso,porcentAgua,porcentMusculo,porcentGrasa,circunfBraquial,circunfPantorrilla,edad")] Progreso progreso)
        {   
            progreso.fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                
                db.Progreso.Add(progreso);
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu});
            }

            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", progreso.paciente);
            return View(progreso);
        }

        // GET: Progresos/Edit/5
        public ActionResult Edit(decimal id, int user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Progreso progreso = db.Progreso.Find(id);
            if (progreso == null)
            {
                return HttpNotFound();
            }
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", progreso.paciente);
            ViewBag.id = user;
            idu = user;
            return View(progreso);
        }

        // POST: Progresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProgreso,paciente,talla,peso,porcentAgua,porcentMusculo,porcentGrasa,circunfBraquial,circunfPantorrilla,edad,fecha")] Progreso progreso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(progreso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu});
            }
            ViewBag.paciente = new SelectList(db.Paciente, "cedula", "ocupacion", progreso.paciente);
            return View(progreso);
        }

        // GET: Progresos/Delete/5
        public ActionResult Delete(decimal idProgreso, int usuario)
        {
            idu = usuario;
            ViewBag.id = usuario;
            if (idProgreso == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Progreso progreso = db.Progreso.Find(idProgreso);
            if (progreso == null)
            {
                return HttpNotFound();
            }
            return View(progreso);
        }

        // POST: Progresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Progreso progreso = db.Progreso.Find(id);
            db.Progreso.Remove(progreso);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = idu});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetDatos()
        {
            var query = db.Progreso.Include(p => p.Paciente1)
                .Select(g => new { paciente= g.paciente ,peso = g.peso, porcAgua = g.porcentAgua,
                                   porcentMusculo = g.porcentMusculo, porcentGrasa = g.porcentGrasa,
                                   circunBranquial = g.circunfBraquial, circunPantorrilla = g.circunfPantorrilla 
                                   }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
