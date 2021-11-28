using System;
namespace MIUCSH
{
    public class HistoricoNotasClass
    {
        public string vAgno { get; set; }
        public string vVepe { get; set; }
        public string vAsig { get; set; }
        public string vSecc { get; set; }
        public string vDesc { get; set; }
        public string vNotaP { get; set; }
        public string vNotaX { get; set; }
        public string vNotaF { get; set; }
        public string vSitF { get; set; }
        public string vDescF { get; set; }
        public string vCRed { get; set; }
        public override string ToString()
            {
                return vDesc;
            }
    }
}
