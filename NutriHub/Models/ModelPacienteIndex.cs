
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NutriHub.Models
{
    public class ModelPacienteIndex
    {
        public Paciente Paciente { set; get; }
        public List<Consejo> Consejos { set; get; }
        public List<Progreso> progresos { set; get; }
        public List<ListaCompras> listas { set; get; }
        public Cita cita { set; get; }
    }
}