using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using NutriHub.Models;

namespace NutriHub.Controllers
{
    public class RegistrarUsuarioController :  Controller{
        private exped_nutricionalEntities db = new exped_nutricionalEntities();

        // GET: RegistrarUsuario
        public ActionResult Index(){
            return View();
        }


        // Iniciar rSesion
        [HttpPost]
        public ActionResult registrarUsuario(registrarUsuario model) {
            try {
                Usuario usuario = new Usuario();
                usuario.cedula = model.cedula;
                usuario.nombreUsuario = model.nombreUsuario;
                usuario.correo = model.correo;
                usuario.contrasenna = model.contrasenna;

                if (ModelState.IsValid) {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();

                    if (model.ocupacion != null) {
                        Paciente paciente = new Paciente();
                        paciente.cedula = usuario.cedula;
                        paciente.sexo = model.sexo;
                        paciente.ocupacion = model.ocupacion;
                        paciente.fechaNacimiento = model.fechaNacimiento;
                        paciente.edad = model.edad == (DateTime.Now.Year - model.fechaNacimiento.Year) ? model.edad : DateTime.Now.Year - model.fechaNacimiento.Year;
                        db.Paciente.Add(paciente);
                        db.SaveChanges();
                        return RedirectToAction("IndexPaciente", "Home", new { id = usuario.cedula });
                    } else {
                        Nutricionista nutricionista = new Nutricionista();
                        nutricionista.cedula = usuario.cedula;
                        nutricionista.sexo = model.sexo;
                        db.Nutricionista.Add(nutricionista);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home", new { id = usuario.cedula });
                    }
                } else {
                    ViewBag.ErrorMessage = "Ooops! Ha ocurrido un error, intentelo de nuevo";
                    return View(new registrarUsuario());
                }
            } catch (Exception e) {
                ViewBag.ErrorMessage = "Ooops! Ha ocurrido un error, intentelo de nuevo";
                return View(new registrarUsuario());
            }
        }
    }
}