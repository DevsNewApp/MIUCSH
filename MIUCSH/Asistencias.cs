using System;
namespace MIUCSH
{
    public class Asistencias
    {
        public string Materia { get; set; }
        public string Asiste { get; set; }
        public string Noasiste { get; set; }
        public string Porceng { get; set; }
        public string Imagen { get; set; }
        public string Visible { get; set; }
        public override string ToString()
        {
            return Materia;
        }
    }
}
