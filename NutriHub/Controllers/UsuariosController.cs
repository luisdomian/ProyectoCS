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
    public class UsuariosController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Nutricionista).Include(u => u.Paciente);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(usuario);
        }


        public ActionResult DetailsPaciente(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.cedula = new SelectList(db.Nutricionista, "cedula", "cedula");
            ViewBag.cedula = new SelectList(db.Paciente, "cedula", "ocupacion");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,cedula,nombreUsuario,correo,contrasenna")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula = new SelectList(db.Nutricionista, "cedula", "cedula", usuario.cedula);
            ViewBag.cedula = new SelectList(db.Paciente, "cedula", "ocupacion", usuario.cedula);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            ViewBag.cedula = new SelectList(db.Nutricionista, "cedula", "cedula", usuario.cedula);
            ViewBag.cedula = new SelectList(db.Paciente, "cedula", "ocupacion", usuario.cedula);
            return View(usuario);
        }

        public ActionResult EditPaciente(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            ViewBag.cedula = new SelectList(db.Nutricionista, "cedula", "cedula", usuario.cedula);
            ViewBag.cedula = new SelectList(db.Paciente, "cedula", "ocupacion", usuario.cedula);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,cedula,nombreUsuario,correo,contrasenna")] Usuario usuario)
        {
            if (ModelState.IsValid)
            { 
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                if(usuario.Nutricionista != null)
                {
                    return RedirectToAction("DetailsPaciente", "Usuarios", new { id = usuario.cedula });
                }
                else
                {
                    return RedirectToAction("Details", "Usuarios", new { id = usuario.cedula}); 
                }
                  
            }
            ViewBag.cedula = new SelectList(db.Nutricionista, "cedula", "cedula", usuario.cedula);
            ViewBag.cedula = new SelectList(db.Paciente, "cedula", "ocupacion", usuario.cedula);
            return View(usuario);
        }



        // GET: Usuarios/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
