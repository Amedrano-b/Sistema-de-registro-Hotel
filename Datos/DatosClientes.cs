using Modelo;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos
{
    public class DatosClientes
    {
        private DatosConexion conexion;
        public DatosClientes()
        {
            this.conexion = new DatosConexion();
        }

        public List<Clientes> Leer()
        {
            List<Clientes> listaclientes = new List<Clientes>();
            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cliente, nombre, apellido, correo, numero_celular, identificacion FROM Clientes";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Clientes mostrarclientes = new Clientes
                        {
                            Id_cliente = reader.GetInt32("id_cliente"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Correo = reader.GetString("correo"),
                            Numero_celular = reader.GetString("numero_celular"),
                            Identificacion = reader.GetString("identificacion")
                        };
                        listaclientes.Add(mostrarclientes);
                    }
                }
                conexion.CloseConnection();
            }
            return listaclientes;
        }

        public bool Insertar(Clientes Cliente)
        {
            if (conexion.OpenConnection())
            {
                string query = "INSERT INTO Clientes (nombre, apellido, correo, numero_celular, identificacion) VALUES (@nombre, @apellido, @correo, @numero_celular, @identificacion)";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                command.Parameters.AddWithValue("@nombre", Cliente.Nombre);
                command.Parameters.AddWithValue("@apellido", Cliente.Apellido);
                command.Parameters.AddWithValue("@correo", Cliente.Correo);
                command.Parameters.AddWithValue("@numero_celular", Cliente.Numero_celular);
                command.Parameters.AddWithValue("@identificacion", Cliente.Identificacion);
                int filasAfectadas = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return filasAfectadas > 0;
            }
            return false;
        }

        public bool Eliminar(Clientes clientes)
        {
            if (conexion.OpenConnection())
            {
                string query = "DELETE FROM Clientes where id_cliente=" + clientes.Id_cliente;
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                int affectedRows = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return affectedRows > 0;
            }
            return false;
        }

        public int Modificar(Clientes clientes)
        {
            int retorno = 0;
            if(conexion.OpenConnection())
            {
                string query = string.Format("UPDATE Clientes set nombre='{0}', apellido='{1}', correo='{2}', numero_celular='{3}', identificacion='{4}' where id_cliente='{5}'", clientes.Nombre, clientes.Apellido, clientes.Correo, clientes.Numero_celular, clientes.Identificacion, clientes.Id_cliente);
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                retorno = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return retorno;
            }
            return retorno;
        }

        public DataTable Buscar(string nombreCliente)
        {
            
            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cliente, nombre, apellido, correo, numero_celular, identificacion FROM Clientes where nombre LIKE '%" + nombreCliente + "%'";
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
    }
}
