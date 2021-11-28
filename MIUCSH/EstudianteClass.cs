using System;
using System.Collections.Generic;
using System.Text;

namespace MIUCSH
{
    class EstudianteClass
    {
        public string alum_rut { get; set; }
        public string alum_nmat { get; set; }
        public string nombre { get; set; }
        public string alum_carr { get; set; }
        public string carr_desc { get; set; }
        public string alum_nive { get; set; }
        public string mail_mail { get; set; }

        public string foto { get; set; }
        public override string ToString()
        {
            return nombre;
        }

    }
}
