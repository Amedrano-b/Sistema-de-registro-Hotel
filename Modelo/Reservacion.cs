using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Reservacion
    {
        public int Id_reservacion { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public DateTime Fecha_salida { get; set; }
        public int Cantidad_personas { get; set; }
        public float Pago_total { get; set; }
        public int Id_cuenta { get; set; }
        public int Id_cliente { get; set; }
        public int Id_pago { get; set; }
        public int Id_transaccion { get; set; }
        public int Id_habitacion { get; set; }
    }
}
