
namespace NutriHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    public class Contacto
    {
        [Required(ErrorMessage = "El campo Su nombre es obligatorio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo Su correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo Su correo electrónico no es una dirección electrónica válida")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo Su contraseña es obligatorio")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "El campo Asunto es obligatorio")]
        public string asunto { get; set; }

        [Required(ErrorMessage = "El campo Mensaje es obligatorio")]
        public string mensaje { get; set; }
    }
}