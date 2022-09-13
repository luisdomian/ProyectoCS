using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutriHub.Models;
using System.Net.Mail;
using System.Data.Entity;

namespace NutriHub.Controllers
{
    public class ContactoController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Contacto c, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.id = id;
                    var pp = db.PacientePlanNutricional.Where(p => p.idPaciente == id).Include(p => p.PlanNutricional).ToList();
                    decimal n = pp[0].PlanNutricional.nutricionista;

                    var usuario = db.Usuario.Where(u => u.cedula == n).ToList();
                    string correoNut = usuario[0].correo;

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(c.correo, c.nombre);
                    msg.To.Add(correoNut);
                    msg.Subject = c.asunto;
                    msg.Body = c.mensaje;

                    // Configuración del servidor
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(c.correo, c.password);
                    smtp.Send(msg);

                    ModelState.Clear();
                    ViewBag.Message = "Gracias por contactarnos";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Error: {ex.Message}";
                }
            }

            return View();
        }
    }

}