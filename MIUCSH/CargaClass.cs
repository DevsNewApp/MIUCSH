using System;
namespace MIUCSH
{
    public class CargaClass
    {
        public string asig { get; set; }
        public string seccion { get; set; }
        public string funcion { get; set; }
        public string grupo { get; set; }
        public string desc_asig { get; set; }
        public string docente { get; set; }
        public string ticr { get; set; }
        public string cod_func { get; set; }
        public string version { get; set; }
        public override string ToString()
        {
            return desc_asig;
        }
    }
}
