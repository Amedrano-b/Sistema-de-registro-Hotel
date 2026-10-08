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
using System.Windows.Forms;

namespace PaginaPrincipal.UsuarioControles
{
    public partial class EditarClientes : Form
    {
        DataGridView padreDGV;
        public EditarClientes(DataGridView dgv, int idCliente)
        {
            padreDGV = dgv;
            InitializeComponent();
            Buscar(idCliente);
        }
        

        private void Buscar(int idCliente)
        {
            txtNombreModificar.Text = padreDGV.CurrentRow.Cells["Nombre"].Value.ToString();
            txtApellidoModificar.Text = padreDGV.CurrentRow.Cells["Apellido"].Value.ToString();
            txtCorreoModificar.Text = padreDGV.CurrentRow.Cells["Correo"].Value.ToString();
            txtNumeroModificar.Text = padreDGV.CurrentRow.Cells["Numero_celular"].Value.ToString();
            txtCedulaModificar.Text = padreDGV.CurrentRow.Cells["Identificacion"].Value.ToString();
        }
        public void Modificar()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombreModificar.Text) && String.IsNullOrEmpty(txtApellidoModificar.Text) && String.IsNullOrEmpty(txtCorreoModificar.Text) && String.IsNullOrEmpty(txtNumeroModificar.Text) && String.IsNullOrEmpty(txtCedulaModificar.Text))
                {
                    MessageBox.Show("Por favor rellenar los campos");
                }
                else
                {

                    NegocioCliente negocioCliente = new NegocioCliente();
                    if (negocioCliente.ValidarNombre(txtNombreModificar.Text.Trim(), txtApellidoModificar.Text.Trim()) && negocioCliente.ValidarCorreo(txtCorreoModificar.Text.Trim()) &&
                        negocioCliente.ValidarTelefonos(txtNumeroModificar.Text.Trim()) && negocioCliente.ValidarCedula(txtCedulaModificar.Text.Trim()))
                    {
                        int id = Convert.ToInt32(padreDGV.CurrentRow.Cells["Id_cliente"].Value.ToString());
                        string clienteNombre = txtNombreModificar.Text;
                        string apellidoCliente = txtApellidoModificar.Text;
                        string correoCliente = txtCorreoModificar.Text;
                        string numCliente = txtNumeroModificar.Text;
                        string identificacionCliente = txtCedulaModificar.Text;

                        Clientes clientes = new Clientes
                        {
                            Id_cliente = id,
                            Nombre = clienteNombre,
                            Apellido = apellidoCliente,
                            Correo = correoCliente,
                            Numero_celular = numCliente,
                            Identificacion = identificacionCliente
                        };

                        int modificado = negocioCliente.Modificar(clientes);
                        if (modificado > 0)
                        {
                            MessageBox.Show("Cliente modificado exitosamente.");


                        }
                        else
                        {
                            MessageBox.Show("Error al modificar el usuario");
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
                MessageBox.Show("Ocurrio un error al modificar el cliente" + ex.Message);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
