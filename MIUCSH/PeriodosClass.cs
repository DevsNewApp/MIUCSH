using System;
namespace MIUCSH
{
    public class PeriodosClass
    {
        public string total { get; set; }
        public string anyo { get; set; }
        public string sem { get; set; }
        public override string ToString()
        {
            return total;
        }
    }
}
