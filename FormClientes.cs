using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PaginaPrincipal.UsuarioControles
{
    public partial class FormClientes : UserControl
    {
        public FormClientes()
        {
            InitializeComponent();
            lblCamposObligatorios.Visible = false;
        }

        private void CargarGrid()
        {
            try
            {
                databaseCliente.DataSource = "";
                NegocioCliente negocioCliente = new NegocioCliente();
                List<Clientes> clientesLista = negocioCliente.Leer();
                databaseCliente.DataSource = clientesLista;
                databaseCliente.Columns["Id_cliente"].DisplayIndex = 0;
                databaseCliente.Columns["Nombre"].DisplayIndex = 1;
                databaseCliente.Columns["Apellido"].DisplayIndex = 2;
                databaseCliente.Columns["Correo"].DisplayIndex = 3;
                databaseCliente.Columns["Numero_celular"].DisplayIndex = 4;
                databaseCliente.Columns["Identificacion"].DisplayIndex = 5;
                databaseCliente.Columns["Modificar"].DisplayIndex = 6;
                databaseCliente.Columns["Eliminar"].DisplayIndex = 7;
                databaseCliente.Columns["Modificar"].Width = 60;
                databaseCliente.Columns["Eliminar"].Width = 60;
                databaseCliente.Columns["Id_cliente"].Width = 60;

                if (clientesLista.Count <= 0)
                {
                    MessageBox.Show("No existen registros");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrid();
                //DGVDisenio dv = new DGVDisenio();
                //dv.Formato(databaseCliente, 1);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Text) && String.IsNullOrEmpty(txtApellido.Text) && String.IsNullOrEmpty(txtCorreo.Text) && String.IsNullOrEmpty(txtNumCelular.Text) && String.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    MessageBox.Show("Por favor rellenar los campos obligatorios");
                    lblCamposObligatorios.Visible = true;
                }
                else
                {
                    lblCamposObligatorios.Visible = false;
                    NegocioCliente negocioCliente = new NegocioCliente();
                    if (negocioCliente.ValidarNombre(txtNombre.Text.Trim(), txtApellido.Text.Trim()) && negocioCliente.ValidarCorreo(txtCorreo.Text.Trim()) &&
                        negocioCliente.ValidarTelefonos(txtNumCelular.Text.Trim()) && negocioCliente.ValidarCedula(txtIdentificacion.Text.Trim()))
                    {
                        string clienteNombre = txtNombre.Text;
                        string apellidoCliente = txtApellido.Text;
                        string correoCliente = txtCorreo.Text;
                        string numCliente = txtNumCelular.Text;
                        string identificacionCliente = txtIdentificacion.Text;

                        Clientes clientes = new Clientes
                        {
                            Nombre = clienteNombre,
                            Apellido = apellidoCliente,
                            Correo = correoCliente,
                            Numero_celular = numCliente,
                            Identificacion = identificacionCliente
                        };

                        bool insertado = negocioCliente.Insertar(clientes);
                        if (insertado)
                        {
                            MessageBox.Show("Cliente insertado exitosamente.");
                            txtNombre.Clear();
                            txtApellido.Clear();
                            txtCorreo.Clear();
                            txtNumCelular.Clear();
                            txtIdentificacion.Clear();
                            CargarGrid();

                        }
                        else
                        {
                            MessageBox.Show("Error al insertar el usuario");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Favor verificar los datos ingresados");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al insertar el cliente" + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Digite un número de ID válido");
                }
                else
                {
                    int id = Int32.Parse(txtId.Text);
                    Clientes cliente = new Clientes
                    {
                        Id_cliente = id
                    };

                    NegocioCliente negocioCliente = new NegocioCliente();
                    bool eliminado = negocioCliente.Eliminar(cliente);

                    if (eliminado)
                    {

                        MessageBox.Show("Cliente eliminado exitosamente.");
                        txtId.Clear();
                        CargarGrid();

                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el cliente");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al eliminar el cliente" + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreBuscar.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para buscar");
                }
                else
                {

                    NegocioCliente negocioCliente = new NegocioCliente();
                    databaseCliente.DataSource = negocioCliente.Buscar(txtNombreBuscar.Text);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al buscar el cliente" + ex.Message);
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            

        }
        public int ID;
        
        private void databaseCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }
        
        private void databaseCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (databaseCliente.Columns[e.ColumnIndex].Name == "Modificar")
            {
                
                ID = Convert.ToInt32(databaseCliente.CurrentRow.Cells["Id_cliente"].Value.ToString());
                EditarClientes editarCliente = new EditarClientes(databaseCliente, ID);
                editarCliente.ShowDialog();
                CargarGrid();
            }

            if (databaseCliente.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                ID = Convert.ToInt32(databaseCliente.CurrentRow.Cells["Id_cliente"].Value.ToString());
                Clientes cliente = new Clientes
                {
                    Id_cliente = ID
                };

                NegocioCliente negocioCliente = new NegocioCliente();
                bool eliminado = negocioCliente.Eliminar(cliente);

                if (eliminado)
                {

                    MessageBox.Show("Cliente eliminado exitosamente.");
                    CargarGrid();

                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente");
                }
            }
        }
    }
}
