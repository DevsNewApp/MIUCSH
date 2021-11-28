using System;
namespace MIUCSH
{
    public class Academica
    {
        public string CodAsig { get; set; }
        public string Asignatura { get; set; }
        public string Docente { get; set; }
        public string Funcion { get; set; }
        public override string ToString()
        {
            return Asignatura;
        }
    }
}
