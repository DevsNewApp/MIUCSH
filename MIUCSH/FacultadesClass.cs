using System;
namespace MIUCSH
{
    public class FacultadesClass
    {
        public string codigo { get; set; }
        public string facultad { get; set; }

        public override string ToString()
        {
            return facultad;
        }
    }
}
