using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutriHub.Models {
    public class PlanNutricionalPaciente {
        public Alimento alimento { set; get; }
        public Nutriente nutriente { set; get; }
        public Paciente paciente { set; get; }
        public semana dia { set; get; }
        public tiempoComida tiempoComida { set; get; }
        public IngestaDiaria IngestaDiaria { set; get;}

    }
}
