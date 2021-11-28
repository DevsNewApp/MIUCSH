using System;
namespace MIUCSH
{
    public class datosPersonales
    {
        public string numMat { get; set; }
        public string apellPat { get; set; }
        public string apellMat { get; set; }
        public string nombres { get; set; }
        public string codCar { get; set; }
        public string alum_plan { get; set; }
        public string alum_nive { get; set; }
        public string anoa_plan { get; set; }
        public string carrera { get; set; }
        public override string ToString()
        {
            return numMat;
        }
    }
}
