using System;
namespace MIUCSH
{
    public class PlanEstudioClass
    { 
        public string rut { get; set; }
        public string plan { get; set; }
        public string nivel { get; set; }
        public string codigo { get; set; }
        public string actividad { get; set; }
        public string credito { get; set; }
        public string tipoCredito { get; set; }
        public string anyo { get; set; }
        public string periodo { get; set; }
        public string situacion { get; set; }
        public string nota { get; set; }

        public override string ToString()
        {
            return actividad;
        }
    }
}
