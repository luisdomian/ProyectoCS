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
    public class AlimentoController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();
        private int idu = new int();
        // GET: Alimento
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            idu = id;
            var alimento = db.Alimento.Include(a => a.Nutriente1);
            return View(alimento.ToList());
        }

        // GET: Alimento/Details/5
        public ActionResult Details(decimal id)
        {
            ViewBag.id = idu;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alimento alimento = db.Alimento.Where(p => p.idAlimento == id).SingleOrDefault();
            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // GET: Alimento/Create
        public ActionResult Create()
        {
            ViewBag.id = idu;
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre");
            return View();
        }

        // POST: Alimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlimento,nutriente,nombre,descripcion")] Alimento alimento)
        {
            if (ModelState.IsValid)
            {
                db.Alimento.Add(alimento);
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu });
            }

            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", alimento.nutriente);
            return View(alimento);
        }

        // GET: Alimento/Edit/5
        public ActionResult Edit(decimal id)
        {
            ViewBag.id = idu;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimento alimento = db.Alimento.Where(p => p.idAlimento == id).SingleOrDefault();
            if (alimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", alimento.nutriente);
            return View(alimento);
        }

        // POST: Alimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlimento,nutriente,nombre,descripcion")] Alimento alimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { usuario = idu });
            }
            ViewBag.nutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", alimento.nutriente);
            return View(alimento);
        }

        // GET: Alimento/Delete/5
        public ActionResult Delete(decimal id)
        {
            ViewBag.id = idu;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimento alimento = db.Alimento.Where(p => p.idAlimento == id).SingleOrDefault();
            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // POST: Alimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Alimento alimento = db.Alimento.Where(p => p.idAlimento == id).SingleOrDefault();
            db.Alimento.Remove(alimento);
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
