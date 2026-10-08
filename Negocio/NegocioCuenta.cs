using Datos;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioCuenta
    {
        private DatosCuentas datosCuentas;

        public NegocioCuenta()
        {
            datosCuentas = new DatosCuentas();
        }

        public List<Cuenta> Leer()
        {
            return datosCuentas.Leer();
        }

        public bool Insertar(Cuenta Cuenta)
        {
            if (ValidarUsuario(Cuenta.Usuario))
            {
                return false;
            }
            return datosCuentas.Insertar(Cuenta);

        }

        public bool Eliminar(Cuenta Cuenta)
        {
            if (Cuenta == null)
            {
                throw new ArgumentNullException(nameof(Cuenta), "EL objeto de cliente no puede ser eliminado");

            }
            return datosCuentas.Eliminar(Cuenta);
        }

        public int Modificar(Cuenta Cuenta)
        {
            if (ValidarUsuario(Cuenta.Usuario) && !ValidarContrasena(Cuenta.Contrasena))
            {
                return 0;
            }
            return datosCuentas.Modificar(Cuenta);

        }

        public DataTable Buscar(string nombreCuenta)
        {
            return datosCuentas.Buscar(nombreCuenta);
        }

        public bool ValidarUsuario(string nombreUsuario)
        {
            if (nombreUsuario.Length < 4 && nombreUsuario.Length > 25 && datosCuentas.ExisteUsuario(nombreUsuario))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidarContrasena(string contrasenaUsuario)
        {

            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&#.$($)$-$_])[A-Za-z\d$@$!%*?&#.$($)$-$_]{8,15}$";

            return Regex.IsMatch(contrasenaUsuario, regex, RegexOptions.IgnoreCase);

        }

        public string ControlRegistro(Clientes cliente, Cuenta cuenta, Tipo_Cuenta tipo_Cuenta)
        {
            NegocioCliente negocioCliente = new NegocioCliente();
            NegocioTipo_Cuenta negocioTipo_Cuenta = new NegocioTipo_Cuenta();
            string respuesta = "";

            if (string.IsNullOrEmpty(cuenta.Usuario) || string.IsNullOrEmpty(cuenta.Contrasena) ||
                string.IsNullOrEmpty(cliente.Nombre) || string.IsNullOrEmpty(cliente.Apellido) ||
                string.IsNullOrEmpty(cliente.Correo) || string.IsNullOrEmpty(cliente.Numero_celular) ||
                string.IsNullOrEmpty(cliente.Identificacion))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                if(negocioCliente.ValidarNombre(cliente.Nombre, cliente.Apellido) && negocioCliente.ValidarCedula(cliente.Identificacion) && negocioCliente.ValidarCorreo(cliente.Correo) && negocioCliente.ValidarTelefonos(cliente.Numero_celular))
                {
                    if (ValidarContrasena(cuenta.Contrasena))
                    {
                        if (datosCuentas.ExisteUsuario(cuenta.Usuario))
                        {
                            respuesta = "El usuario ya ha sido registrado";
                        }
                        else
                        {
                            cuenta.Contrasena = GenerarSHAI(cuenta.Contrasena);
                            negocioCliente.Insertar(cliente);
                            Insertar(cuenta);
                            negocioTipo_Cuenta.Insertar(tipo_Cuenta);

                        }
                    }
                    else
                    {
                        respuesta = "Favor verificar la contraseña";
                    }
                }
                else
                {
                    respuesta = "Favor verificar los datos del cliente";
                }
                
            }
            return respuesta;
        }

        public string ControlInicio(string usuario, string contrasena)
        {
            Cuenta cuenta = null;
            string respuesta = "";
            DatosCuentas datosCuentas = new DatosCuentas();
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                respuesta = "Debe rellenar todos los campos";
            }
            else
            {
                cuenta = datosCuentas.PorUsuario(usuario);
                if (cuenta == null)
                {
                    respuesta = "El usuario no existe";
                }
                else
                {
                    if (cuenta.Contrasena != GenerarSHAI(contrasena))
                    {
                        respuesta = "El usuario y/o la contraseña no coinciden";
                    }
                }
            }
            return respuesta;
        }
        public string GenerarSHAI(string cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(cadena);
            byte[] result;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

            result = sha.ComputeHash(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
