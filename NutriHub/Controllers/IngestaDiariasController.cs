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
    public class IngestaDiariasController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: IngestaDiarias
        public ActionResult Index(int usuario)
        {
            ViewBag.id = usuario;
            var ingestaDiaria = db.IngestaDiaria.Include(i => i.Nutriente1);
            return View(ingestaDiaria.ToList());
        }

        public ActionResult VistaIngestaPaciente(int usuario)
        {
            ViewBag.id = usuario;
            var ingestaDiaria = db.IngestaDiaria.Include(i => i.Nutriente1);
            return View(ingestaDiaria.ToList());
        }

        // GET: IngestaDiarias/Details/5
        public ActionResult Details(decimal id)
        {
            if (id < 1)
                return RedirectToAction("Index");
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            IngestaDiaria ingestaDiaria = db.IngestaDiaria.Find(id);
            if (ingestaDiaria == null)
            {
                return HttpNotFound();
            }
            return View(ingestaDiaria);
        }

        // GET: IngestaDiarias/Create
        public ActionResult Create()
        {
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre");
            return View();
        }

        // POST: IngestaDiarias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idIngesta,nutriente,vet,porcentaje")] IngestaDiaria ingestaDiaria)
        {
            if (ModelState.IsValid)
            {
                db.IngestaDiaria.Add(ingestaDiaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", ingestaDiaria.nutriente);
            return View(ingestaDiaria);
        }

        // GET: IngestaDiarias/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngestaDiaria ingestaDiaria = db.IngestaDiaria.Find(id);
            if (ingestaDiaria == null)
            {
                return HttpNotFound();
            }
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", ingestaDiaria.nutriente);
            return View(ingestaDiaria);
        }

        // POST: IngestaDiarias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idIngesta,nutriente,vet,porcentaje")] IngestaDiaria ingestaDiaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingestaDiaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", ingestaDiaria.nutriente);
            return View(ingestaDiaria);
        }

        // GET: IngestaDiarias/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngestaDiaria ingestaDiaria = db.IngestaDiaria.Find(id);
            if (ingestaDiaria == null)
            {
                return HttpNotFound();
            }
            return View(ingestaDiaria);
        }

        // POST: IngestaDiarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            IngestaDiaria ingestaDiaria = db.IngestaDiaria.Find(id);
            db.IngestaDiaria.Remove(ingestaDiaria);
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
