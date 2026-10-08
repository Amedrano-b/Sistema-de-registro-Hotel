
using Modelo;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;

namespace Negocio
{
    public class NegocioCliente
    {
        private DatosClientes datosClientes;

        public NegocioCliente()
        {
            datosClientes = new DatosClientes();
        }
        public List<Clientes> Leer()
        {
            return datosClientes.Leer();
        }

        public bool Insertar(Clientes Cliente)
        {
            if(!ValidarNombre(Cliente.Nombre, Cliente.Apellido) && !ValidarCorreo(Cliente.Correo) && !ValidarTelefonos(Cliente.Numero_celular) && !ValidarCedula(Cliente.Identificacion))
            {
                return false;
            }
            return datosClientes.Insertar(Cliente);
        }

        public bool Eliminar(Clientes Cliente)
        {
            if (Cliente == null)
            {
                throw new ArgumentNullException(nameof(Cliente), "EL objeto de cliente no puede ser eliminado");

            }
            return datosClientes.Eliminar(Cliente);
        }

        public int Modificar(Clientes Cliente)
        {
            if (!ValidarNombre(Cliente.Nombre, Cliente.Apellido) && !ValidarCorreo(Cliente.Correo) && !ValidarTelefonos(Cliente.Numero_celular) && !ValidarCedula(Cliente.Identificacion))
            {
                return 0;
            }
            return datosClientes.Modificar(Cliente);
        }

        public DataTable Buscar(string nombreCliente)
        {
            return datosClientes.Buscar(nombreCliente);
        }

        public bool ValidarNombre(string nombreCliente, string apellidoCliente)
        {
            bool mensaje;
            if (nombreCliente == "" && apellidoCliente == "")
            {
                mensaje = false;
            }
            else if (nombreCliente.Length > 25 && apellidoCliente.Length > 25)
            {
                mensaje = false;
            }
            else
            {
                mensaje = true;
            }
            return mensaje;
        }

        public bool ValidarCorreo(string correoCLiente)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(correoCLiente, regex, RegexOptions.IgnoreCase);
            

        }

        public bool ValidarTelefonos(string telefonoCliente)
        {
            string regex = @"^[0-9]{8}$";
            
            return Regex.IsMatch(telefonoCliente, regex, RegexOptions.IgnoreCase);
        }

        public bool ValidarCedula(string cedulaCliente)
        {
            string regex = @"^[1-9]{3}-?\d{6}-?\d{4}[A-Z]{1}$";
             return Regex.IsMatch(cedulaCliente, regex, RegexOptions.IgnoreCase);  
        }

       
    }
}
