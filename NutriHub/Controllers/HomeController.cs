using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutriHub.Models;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace NutriHub.Controllers
{
    public class HomeController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        //[HttpPost]
        public ActionResult Index(int id)
        {
            var usuario = db.Usuario.Where(i => i.cedula == id).SingleOrDefault();
            var nutricionista = db.Nutricionista.Find(id);
            var Consejos = db.Consejo.ToList();
            var pacientes = db.Paciente.ToList();
            var cita = db.Cita.Where(p => p.idNutricionista == id).ToList();
            ModelNutricionistaIndex datos = new ModelNutricionistaIndex();
            datos.Usuario = usuario;
            datos.Pacientes = pacientes;
            datos.Nutricionista = nutricionista;
            datos.Citas = cita;
            datos.Consejos = Consejos;
            return View(datos);
        }

        //[HttpPost]
        public ActionResult IndexPaciente(int id)
        {
            var paciente = db.Paciente.Find(id);
            var consejos = db.Consejo.ToList();
            var cita = db.Cita.Where( p => p.idPaciente == id).OrderBy( p=> p.fecha ).FirstOrDefault();
            ModelPacienteIndex datos = new ModelPacienteIndex();
            datos.Paciente = paciente;
            datos.Consejos = consejos;
            datos.cita = cita;

            return View(datos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}