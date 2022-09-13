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
    public class ListaComprasController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: ListaCompras
        public ActionResult Index()
        {
            var listaCompras = db.ListaCompras.Include(l => l.Alimento);
            return View(listaCompras.ToList());
        }

        // GET: ListaCompras/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaCompras listaCompras = db.ListaCompras.Find(id);
            if (listaCompras == null)
            {
                return HttpNotFound();
            }
            return View(listaCompras);
        }

        // GET: ListaCompras/Create
        public ActionResult Create()
        {
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre");
            return View();
        }

        // POST: ListaCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLista,idAlimento,nutriente,nombre")] ListaCompras listaCompras)
        {
            if (ModelState.IsValid)
            {
                db.ListaCompras.Add(listaCompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", listaCompras.idAlimento);
            return View(listaCompras);
        }

        // GET: ListaCompras/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaCompras listaCompras = db.ListaCompras.Find(id);
            if (listaCompras == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", listaCompras.idAlimento);
            return View(listaCompras);
        }

        // POST: ListaCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLista,idAlimento,nutriente,nombre")] ListaCompras listaCompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaCompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", listaCompras.idAlimento);
            return View(listaCompras);
        }

        // GET: ListaCompras/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaCompras listaCompras = db.ListaCompras.Find(id);
            if (listaCompras == null)
            {
                return HttpNotFound();
            }
            return View(listaCompras);
        }

        // POST: ListaCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ListaCompras listaCompras = db.ListaCompras.Find(id);
            db.ListaCompras.Remove(listaCompras);
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
