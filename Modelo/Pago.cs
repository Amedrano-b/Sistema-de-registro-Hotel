using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Pago
    {
        public int Id_pago { get; set; }
        public float Cantidad_dinero { get; set; }
        public string Datos_metodo { get; set; }
        public DateTime Fecha_pago { get; set; }
        public int Id_cuenta { get; set; }
        public int Id_metodopago { get; set; }
    }
}
