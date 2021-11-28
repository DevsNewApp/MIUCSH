using System;
namespace MIUCSH
{
    public class Horarios
    {
        public string Dia { get; set; }
        public string Materia { get; set; }
        public string Horario { get; set; }
        public string Docente { get; set; }
        public string Seccion { get; set; }
        public string Sala { get; set; }
        public string Visible { get; set; }
        public override string ToString()
        {
            return Dia;
        }
    }
}
