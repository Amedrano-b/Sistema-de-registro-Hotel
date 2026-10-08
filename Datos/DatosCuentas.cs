using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosCuentas
    {
        private DatosConexion conexion;
        public DatosCuentas()
        {
            this.conexion = new DatosConexion();
        }

        public List<Cuenta> Leer()
        {
            List<Cuenta> listacuentas = new List<Cuenta>();
            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cuenta, usuario, contrasena, id_cliente, id_cuentaTipo FROM Cuenta";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Cuenta mostrarcuentas = new Cuenta
                        {
                            Id_cuenta = reader.GetInt32("id_cuenta"),
                            Usuario = reader.GetString("usuario"),
                            Contrasena = reader.GetString("contrasena"),
                            Id_cliente = reader.GetInt32("id_cliente"),
                            Id_cuentaTipo = reader.GetInt32("id_cuentaTipo"),
                            
                        };
                        listacuentas.Add(mostrarcuentas);
                    }
                }
                conexion.CloseConnection();
            }
            return listacuentas;
        }

        public bool Insertar(Cuenta Cuenta)
        {
            if (conexion.OpenConnection())
            {
                string query = "INSERT INTO Cuenta (usuario, contrasena, id_cliente, id_cuentaTipo) VALUES (@usuario, @contrasena, @id_cliente, @id_cuentaTipo)";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                command.Parameters.AddWithValue("@usuario", Cuenta.Usuario);
                command.Parameters.AddWithValue("@contrasena", Cuenta.Contrasena);
                command.Parameters.AddWithValue("@id_cliente", Cuenta.Id_cliente);
                command.Parameters.AddWithValue("@id_cuentaTipo", Cuenta.Id_cuentaTipo);
                int filasAfectadas = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return filasAfectadas > 0;
            }
            return false;
        }

        public bool Eliminar(Cuenta Cuenta)
        {
            if (conexion.OpenConnection())
            {
                string query = "DELETE FROM Cuenta where id_cuenta=" + Cuenta.Id_cuenta;
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                int affectedRows = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return affectedRows > 0;
            }
            return false;
        }

        public int Modificar(Cuenta Cuenta)
        {
            int retorno = 0;
            if (conexion.OpenConnection())
            {
                string query = string.Format("UPDATE Cuenta set usuario='{0}', contrasena='{1}' where id_cuenta='{2}'", Cuenta.Usuario, Cuenta.Contrasena, Cuenta.Id_cuenta);
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                retorno = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return retorno;
            }
            return retorno;
        }

        public DataTable Buscar(string nombreUsuario)
        {

            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cuenta, usuario, contrasena, id_cliente, id_cuentaTipo FROM Cuenta where usuario LIKE '%" + nombreUsuario + "%'";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexion.GetConnection());
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                MySqlDataReader lector;
                lector = command.ExecuteReader();
                conexion.CloseConnection();
                return dataTable;
            }
            return null;
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            MySqlDataReader lector;
            if (conexion.OpenConnection())
            {
                string query ="SELECT id_cuenta FROM Cuenta WHERE usuario LIKE @usuario";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                command.Parameters.AddWithValue("@usuario", nombreUsuario);
                lector = command.ExecuteReader();

                if (lector.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Cuenta PorUsuario(string nombreUsuario)
        {
            MySqlDataReader lector;
            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cuenta, usuario, contrasena, id_cliente, id_cuentaTipo FROM Cuenta WHERE usuario LIKE @usuario";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                command.Parameters.AddWithValue("@usuario", nombreUsuario);
                lector = command.ExecuteReader();

                Cuenta cuenta = null;

                while (lector.Read())
                {
                    cuenta = new Cuenta();
                    cuenta.Id_cuenta = Convert.ToInt32(lector["id_cuenta"].ToString());
                    cuenta.Usuario = lector["usuario"].ToString();
                    cuenta.Contrasena = lector["contrasena"].ToString();
                    cuenta.Id_cliente = Convert.ToInt32(lector["id_cliente"].ToString());
                    cuenta.Id_cuentaTipo = Convert.ToInt32(lector["id_cuentaTipo"].ToString());
                }
                return cuenta;
            }
            return null;
        }
    }
}
