using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cuenta
    {
        public int Id_cuenta { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public int Id_cliente { get; set; } 
        public int Id_cuentaTipo { get; set; }
    }
}
