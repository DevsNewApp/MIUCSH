using System;
namespace MIUCSH
{
    public class ProgramasClass
    {
        public string codigo { get; set; }
        public string programa { get; set; }
        public string jornada { get; set; }
        public override string ToString()
        {
            return programa;
        }
    }
}
