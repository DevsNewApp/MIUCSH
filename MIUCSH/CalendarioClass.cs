using System;
namespace MIUCSH
{
    public class CalendarioClass
    {
      
            public string mes { get; set; }
        public string area { get; set; }
        public string ini { get; set; }
        public string fin { get; set; }
        public string actividad { get; set; }

        public override string ToString()
        {
            return actividad;
        }
    }
    
}
