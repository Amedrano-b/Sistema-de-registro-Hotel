using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Habitacion
    {
        public int Id_habitacion { get; set; }
        public float Precio_habitacion { get; set; }
        public string Superficie { get; set; }
        public string Ocupacion { get; set; }
        public string Cama_matrimonial { get; set; }
        public string Cama_individual { get; set; }
        public bool Aireacondicionado { get; set; }
        public bool Desayuno { get; set; }
        public bool Almuerzo { get; set; }
        public bool Cena { get; set; }
        public int Id_cuenta { get; set; }
    }
}
