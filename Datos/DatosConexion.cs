using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Datos
{
    public class DatosConexion
    {
        private string connectionString;
        private MySqlConnection connection;
        private string server = "127.0.0.1";
        private string port = "3306";
        private string user = "root";
        private string password = "BryancitoyYo";
        private string database = "hotel";

        public DatosConexion()
        {
            connectionString = $"Server={server};Port={port};Database={database};User={user};password={password}";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public bool ConnectionStatus()
        {
            //True conexion abierta
            //false conexion cerrada
            return (connection.State == System.Data.ConnectionState.Open);
        }
    }
}
