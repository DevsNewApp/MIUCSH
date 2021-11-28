using System;
namespace MIUCSH
{
    public class FotoClass
    {
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
       
    }
}
