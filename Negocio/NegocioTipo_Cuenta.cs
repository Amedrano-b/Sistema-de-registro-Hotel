using Datos;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioTipo_Cuenta
    {
        private DatosTipo_Cuentas datosTipo_Cuentas;

        public NegocioTipo_Cuenta()
        {
            datosTipo_Cuentas = new DatosTipo_Cuentas();
        }

        public List< Tipo_Cuenta> Leer()
        {
            return datosTipo_Cuentas.Leer();
        }

        public bool Insertar(Tipo_Cuenta tipo_Cuenta)
        {
            if (tipo_Cuenta == null)
            {
                return false;
            }
            return datosTipo_Cuentas.Insertar(tipo_Cuenta);
        }

        public bool Eliminar(Tipo_Cuenta tipo_Cuenta)
        {
            if (tipo_Cuenta == null)
            {
                throw new ArgumentNullException(nameof(tipo_Cuenta), "EL objeto de cliente no puede ser eliminado");

            }
            return datosTipo_Cuentas.Eliminar(tipo_Cuenta);
        }

        public int Modificar(Tipo_Cuenta tipo_Cuenta)
        {
            if (tipo_Cuenta == null)
            {
                return 0;
            }
            return datosTipo_Cuentas.Modificar(tipo_Cuenta);
        }
    }
}
