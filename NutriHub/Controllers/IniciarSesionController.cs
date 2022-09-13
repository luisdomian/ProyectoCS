using NutriHub.Models;
using System.Data.Entity;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//Testing made with LINQ Methods
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;


namespace NutriHub.Controllers {
    public class IniciarSesionController : Controller {

        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // Registrar usuario (Paciente/ Nutricionista)
        public ActionResult registrarUsuario() {
            return View();
        }

        // Iniciar rSesion
        [HttpGet]
        public ActionResult iniciarSesion() {
            return View(new IniciarSesionMetaData());
        }

        // Iniciar rSesion
        [HttpPost]
        public ActionResult iniciarSesion(IniciarSesionMetaData model) {
            try {
                bool validacion = validarUsuario(model.inputValidacion, model.contrasenna);
                if (validacion) {
                    var usuario = db.Usuario.Find(ViewBag.id);

                    if (usuario.Nutricionista == null)
                    {
                        return RedirectToAction("IndexPaciente", "Home", new { id = usuario.cedula });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { id = usuario.cedula });
                    }
                } else {
                    // Mostrar error en la contraseña del Cx
                    return View(new IniciarSesionMetaData());
                }
            } catch (Exception e) {
                ViewBag.ErrorMessage = "Ooops! Ha ocurrido un error, intentelo de nuevo";
                return View(new IniciarSesionMetaData());
            }
        }

        /*
         * Efect: valida que el usuario y la contraseña coincidan
         * Requiere: Parametros -> usuario (inputValidacion) + contraseña
         * Modifica: N/A
         */
        public bool validarUsuario(string validacion, string contrasenna) {
            
            var usuario = db.Usuario.Where(x => x.nombreUsuario == validacion || x.correo == validacion).FirstOrDefault();

            if (usuario != null) {
                if (usuario.contrasenna.Equals(contrasenna)) {
                    ViewBag.id = usuario.cedula;
                    return true;
                } else {
                    ViewBag.ErrorMessage = "La contraseña ingresado no es correcta!";
                    return false;
                }
            } else {
                ViewBag.ErrorMessage = "El correo o nombre de usuario no existe!";
                return false;
            }
        }

    }
}