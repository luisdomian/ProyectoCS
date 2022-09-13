using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutriHub.Models {

    public class IniciarSesionMetaData {
        [Display(Name = "Usuario:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo o el nombre del usuario es nesario para ingresar.")]
        public string inputValidacion { get; set; }

        [Display(Name = "Contraseña:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es inválida, revisar el nombre del usuario y la contraseña agregada.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimo 6 caracteres para la entrevista")]
        public string contrasenna { get; set; }
    }

}