using NutriHub.Models;
using System.Data.Entity;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

namespace NutriHub.Controllers
{
    public class PlanNutricionistaPacienteController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();
        private PlanNutricionalPaciente datos = new PlanNutricionalPaciente();
        // GET: PlanNutricionistaPaciente
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            ViewBag.idPaciente = new SelectList(db.Paciente, "cedula", "Usuario.nombreUsuario");
            ViewBag.idAlimento = new SelectList(db.Alimento, "idAlimento", "nombre");

            /*
            datos.alimentos = db.Alimento.ToList();
            datos.dias = db.semana.ToList();
            datos.nutrientes = db.Nutriente.ToList();
            datos.tiempoComida = db.tiempoComida.ToList();
            datos.usuarios = db.Usuario.ToList();
            datos.IngestaDiaria = db.IngestaDiaria.ToList();            
            */
            return View();
        }

        // POST: PlanNutricionistaPaciente
        [HttpPost]
        public ActionResult crearPlan(int idPaciente, int vet) {
            ViewBag.paciente = idPaciente;
            ViewBag.vet = vet;
            return View();
        }
    }
}