using System;
namespace MIUCSH
{
    public class HorariosClases
    {
        public string modulo { get; set; }
        public string lunes { get; set; }
        public string martes { get; set; }
        public string miercoles { get; set; }
        public string jueves { get; set; }
        public string viernes { get; set; }
        public string sabado { get; set; }
        public override string ToString()
        {
            return modulo;
        }
    }
}
