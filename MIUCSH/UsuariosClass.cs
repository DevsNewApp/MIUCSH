using System;
namespace MIUCSH
{
    public class UsuariosClass
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string tipo { get; set; }
        public string create_date { get; set; }
  
        public override string ToString()
        {
            return nombre;
        }
    }
}
