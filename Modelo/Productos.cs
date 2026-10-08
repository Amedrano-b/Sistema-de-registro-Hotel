using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Productos
    {
        public int Id_productos { get; set; }
        public float Gaseosa { get; set; }
        public float Agua { get; set; }
        public float Snack { get; set; }
        public float Nachos { get; set; }
        public float Alcohol { get; set; }
        public float Total { get; set; }
        public int Id_cuenta { get; set; }

    }
}
