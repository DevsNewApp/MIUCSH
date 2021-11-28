using System;
namespace MIUCSH
{
    public class Oferta
    {
        public string Codigo { get; set; }
        public string Asignatura { get; set; }

        public string Seccion{ get; set; }
        public string Cupo { get; set; }
        public string Jornada { get; set; }
        public string Visible { get; set; }
        public override string ToString()
        {
            return Asignatura;
        }
    }
}
