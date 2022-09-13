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
    public class ConsejoController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Consejo
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return View(db.Consejo.ToList());
        }

        public ActionResult IndexConsejoPaciente()
        {
            return View(db.Consejo.ToList());
        }

        // GET: Consejo/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejo.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            
            return View(consejo);
        }

        // GET: Consejo/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public Consejo GetConsejo()
        {
            bool existe = false;
            Random rand = new Random();
            int id = rand.Next(1,2);
            //SELECT TOP 1 column FROM  ORDER BY NEWID()
            Consejo consejo = db.Consejo.Find(2);
            //consejo.idConsejo = 1;
            //consejo.mensajeConsejo= " mensaje del consejo";
            //consejo.tituloConsejo = " titulo del consejo";
            //consejo.referencia = "google";

            return consejo;

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConsejo,tituloConsejo,mensajeConsejo,referencia")] Consejo consejo)
        {
            if (ModelState.IsValid)
            {
                db.Consejo.Add(consejo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consejo);
        }

        // GET: Consejo/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejo.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            return View(consejo);
        }

        // POST: Consejo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConsejo,tituloConsejo,mensajeConsejo,referencia")] Consejo consejo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consejo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consejo);
        }

        // GET: Consejo/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejo.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            return View(consejo);
        }

        // POST: Consejo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Consejo consejo = db.Consejo.Find(id);
            db.Consejo.Remove(consejo);
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
