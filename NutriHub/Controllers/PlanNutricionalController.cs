using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutriHub.Models;

namespace NutriHub.Controllers
{
    public class PlanNutricionalController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: PlanNutricional
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.id = id;
            var planNutricional = db.PlanNutricional.Include(p => p.Alimento).Include(p => p.IngestaDiaria).Include(p => p.Nutricionista1).Include(p => p.Nutriente).Include(p => p.semana).Include(p => p.tiempoComida1);
            return View(await planNutricional.ToListAsync());
        }

        // GET: PlanNutricional/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanNutricional planNutricional = await db.PlanNutricional.FindAsync(id);
            if (planNutricional == null)
            {
                return HttpNotFound();
            }
            return View(planNutricional);
        }

        // GET: PlanNutricional/Create
        public ActionResult Create()
        {
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre");
            ViewBag.idIngesta = new SelectList(db.IngestaDiaria, "idIngesta", "idIngesta");
            ViewBag.idNutriente = new SelectList(db.Nutriente, "idNutriente", "nombre");
            ViewBag.dia = new SelectList(db.semana, "idDia", "nombre");
            ViewBag.tiempoComida = new SelectList(db.tiempoComida, "idTiempoComida", "nombre");
            return View();
        }

        // POST: PlanNutricional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idPlan,nutricionista,idIngesta,idAlimento,idNutriente,vet,tiempoComida,porcion,proposito,dia")] PlanNutricional planNutricional)
        {
            if (ModelState.IsValid)
            {
                db.PlanNutricional.Add(planNutricional);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new {id = planNutricional.nutricionista});
            }

            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", planNutricional.idAlimento);
            ViewBag.idIngesta = new SelectList(db.IngestaDiaria, "idIngesta", "idIngesta", planNutricional.idIngesta);
            ViewBag.id = new SelectList(db.Nutricionista, "cedula", "cedula", planNutricional.nutricionista);
            ViewBag.idNutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", planNutricional.idNutriente);
            ViewBag.dia = new SelectList(db.semana, "idDia", "nombre", planNutricional.dia);
            ViewBag.tiempoComida = new SelectList(db.tiempoComida, "idTiempoComida", "nombre", planNutricional.tiempoComida);
            return View(planNutricional);
        }

        // GET: PlanNutricional/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanNutricional planNutricional = await db.PlanNutricional.FindAsync(id);
            if (planNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", planNutricional.idAlimento);
            ViewBag.idIngesta = new SelectList(db.IngestaDiaria, "idIngesta", "idIngesta", planNutricional.idIngesta);
            ViewBag.nutricionista = new SelectList(db.Nutricionista, "cedula", "cedula", planNutricional.nutricionista);
            ViewBag.idNutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", planNutricional.idNutriente);
            ViewBag.dia = new SelectList(db.semana, "idDia", "nombre", planNutricional.dia);
            ViewBag.tiempoComida = new SelectList(db.tiempoComida, "idTiempoComida", "nombre", planNutricional.tiempoComida);
            return View(planNutricional);
        }

        // POST: PlanNutricional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPlan,nutricionista,idIngesta,idAlimento,idNutriente,vet,tiempoComida,porcion,proposito,dia")] PlanNutricional planNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planNutricional).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre", planNutricional.idAlimento);
            ViewBag.idIngesta = new SelectList(db.IngestaDiaria, "idIngesta", "idIngesta", planNutricional.idIngesta);
            ViewBag.nutricionista = new SelectList(db.Nutricionista, "cedula", "cedula", planNutricional.nutricionista);
            ViewBag.idNutriente = new SelectList(db.Nutriente, "idNutriente", "nombre", planNutricional.idNutriente);
            ViewBag.dia = new SelectList(db.semana, "idDia", "nombre", planNutricional.dia);
            ViewBag.tiempoComida = new SelectList(db.tiempoComida, "idTiempoComida", "nombre", planNutricional.tiempoComida);
            return View(planNutricional);
        }

        // GET: PlanNutricional/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanNutricional planNutricional = await db.PlanNutricional.FindAsync(id);
            if (planNutricional == null)
            {
                return HttpNotFound();
            }
            return View(planNutricional);
        }

        // POST: PlanNutricional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            PlanNutricional planNutricional = await db.PlanNutricional.FindAsync(id);
            db.PlanNutricional.Remove(planNutricional);
            await db.SaveChangesAsync();
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
