using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace NutriHub.Models
{
    public class ReporteIngesta
    {
        [Required(ErrorMessage = "El campo Su nombre es obligatorio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo Su correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo Su correo electrónico no es una dirección electrónica válida")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo Su contraseña es obligatorio")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "El campo Descripción de ingesta es obligatorio")]
        public string descripcionIngesta { get; set; }

        public HttpPostedFileBase archivo { get; set; }
    }
}