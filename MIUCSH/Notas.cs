using System;
namespace MIUCSH
{
    public class Notas
    {
        public string Asignatura { get; set; }

        public string Fecha { get; set; }
        public string Tipo { get; set; }
        public string Ponderacion { get; set; }
        public string Nota { get; set; }
        public string Visible { get; set; }
        public override string ToString()
        {
            return Asignatura;
        }
    }
}
