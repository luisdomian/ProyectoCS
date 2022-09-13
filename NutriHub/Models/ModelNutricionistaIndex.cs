using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutriHub.Models
{
    public class ModelNutricionistaIndex
    {
        public Usuario Usuario { set; get; }
        public List <Paciente> Pacientes  { set; get; }
        public List<Consejo> Consejos { set; get; }
        public List<Cita> Citas{ set; get; }
        public Nutricionista Nutricionista { set; get; }
    }
}