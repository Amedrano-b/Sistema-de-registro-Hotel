using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosTipo_Cuentas
    {
        private DatosConexion conexion;
        public DatosTipo_Cuentas()
        {
            this.conexion = new DatosConexion();
        }

        public List<Tipo_Cuenta> Leer()
        {
            List<Tipo_Cuenta> listaTipocuentas = new List<Tipo_Cuenta>();
            if (conexion.OpenConnection())
            {
                string query = "SELECT id_cuentaTipo, tipo FROM Tipo_Cuenta";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Tipo_Cuenta mostrarTipocuentas = new Tipo_Cuenta
                        {
                            Id_cuentaTipo = reader.GetInt32("id_cuentaTipo"),
                            Tipo = reader.GetString("tipo")
                            
                        };
                        listaTipocuentas.Add(mostrarTipocuentas);
                    }
                }
                conexion.CloseConnection();
            }
            return listaTipocuentas;
        }

        public bool Insertar(Tipo_Cuenta tipo_Cuenta)
        {
            if (conexion.OpenConnection())
            {
                string query = "INSERT INTO Tipo_Cuenta (tipo) VALUES (@tipo)";
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());
                command.Parameters.AddWithValue("@tipo", tipo_Cuenta.Tipo);
              
                int filasAfectadas = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return filasAfectadas > 0;
            }
            return false;
        }

        public bool Eliminar(Tipo_Cuenta tipo_Cuenta)
        {
            if (conexion.OpenConnection())
            {
                string query = "DELETE FROM Tipo_Cuenta where id_cuentaTipo=" + tipo_Cuenta.Id_cuentaTipo;
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                int affectedRows = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return affectedRows > 0;
            }
            return false;
        }

        public int Modificar(Tipo_Cuenta tipo_Cuenta)
        {
            int retorno = 0;
            if (conexion.OpenConnection())
            {
                string query = string.Format("UPDATE Tipo_Cuenta set tipo='{0}' where id_cuentaTipo='{1}'", tipo_Cuenta.Tipo, tipo_Cuenta.Id_cuentaTipo);
                MySqlCommand command = new MySqlCommand(query, conexion.GetConnection());

                retorno = command.ExecuteNonQuery();
                conexion.CloseConnection();
                return retorno;
            }
            return retorno;
        }
    }
}
