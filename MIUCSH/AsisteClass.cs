using System;
namespace MIUCSH
{
    public class AsisteClass
    {
        public string curs_sasi { get; set; }
        public string asis_req { get; set; }
        public string asis_ing { get; set; }
        public string asis_ina { get; set; }
        public string sesiones { get; set; }
        public string asistencias { get; set; }
        public override string ToString()
        {
            return curs_sasi;
        }
    }
}
