using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Transaccion
    {
        public int Id_transaccion { get; set; }
        public DateTime Fecha_transaccion { get; set; }
        public int Id_cuenta { get; set; }
        public int Id_pago { get; set; }
    }
}
