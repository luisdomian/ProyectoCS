using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutriHub.Models {
    public class registrarUsuario {
        [Display(Name = "Cédula")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este es un campo requerido.")]
        public decimal cedula { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es inválida, revisar el nombre del usuario y la contraseña agregada.")]
        public string nombreUsuario { get; set; }

        [Display(Name = "Correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este es un campo requerido.")]
        public string correo { get; set; }
        
        [Display(Name = "Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este es un campo requerido.")]
        [DataType(DataType.Password)]
        public string contrasenna { get; set; }

        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este es un campo requerido.")]
        public bool sexo { get; set; }

        #region paciente
        [Display(Name = "Ocupación")]
        public string ocupacion { get; set; }
        
        [Display(Name = "Fecha de nacimiento")]
        public DateTime fechaNacimiento { get; set; }
        
        [Display(Name = "Edad")]
        public int edad { get; set; }
        #endregion

        #region nutricionista
        [Display(Name = "Licencia")]
        public decimal licencia { get; set; }
        #endregion
    }
}