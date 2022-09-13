using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using NutriHub.Models;
using System.Net.Mail;
using System.Data.Entity;
using System.IO;

namespace NutriHub.Controllers
{
    public class ReporteIngestaController : Controller
    {
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        [HttpGet]
        public ActionResult ReporteIngesta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReporteIngesta(ReporteIngesta ri, int id)
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
                    msg.From = new MailAddress(ri.correo, ri.nombre);
                    msg.To.Add(correoNut);
                    msg.Subject = "Reporte de Ingesta de " + ri.nombre;
                    msg.Body = ri.descripcionIngesta;
                    if (ri.archivo.ContentLength > 0)
                    {
                        string nombreArchivo = Path.GetFileName(ri.archivo.FileName);
                        msg.Attachments.Add(new Attachment(ri.archivo.InputStream, nombreArchivo));
                    }
                    msg.IsBodyHtml = false;

                    // Configuración del servidor
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(ri.correo, ri.password);
                    smtp.Send(msg);

                    ModelState.Clear();
                    ViewBag.Message = "Reporte enviado";
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
