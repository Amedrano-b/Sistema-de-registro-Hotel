using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Metodo_Pago
    {
        public int Id_metodopago { get; set; }
        public bool Tarjeta_credito { get; set; }
        public bool Tarjeta_debito { get; set; }
        public bool Paypal { get; set; }
       
    }
}
