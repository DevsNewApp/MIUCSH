using System;
namespace MIUCSH
{
    public class OfertasClass
    {
        public string asig_codi { get; set; }
        public string asig_desc { get; set; }
        public string asig_secc { get; set; }
        public string asig_cupo { get; set; }
        public string asig_modu { get; set; }
        public string asig_jorn { get; set; }
        public string asig_acad { get; set; }
        public string asig_ticr { get; set; }
        public string lifo_coej { get; set; }
        public string lifo_deej { get; set; }
        public string lifo_coli { get; set; }
    

        public override string ToString()
        {
            return asig_desc;
        }
    }
}
