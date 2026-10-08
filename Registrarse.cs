using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaginaPrincipal
{
    public partial class Registrarse : Form
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pFileview, uint cjSize, IntPtr pvResrved, [In] ref uint pNumfonts);
        public Registrarse()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(125, Color.DarkGoldenrod);
            Font fuente_PaintBrush = new Font(CargarFuente(Properties.Resources.PaintBrushScript_Regular), 25, FontStyle.Regular);
            lblDClientes.Font = fuente_PaintBrush;
            label6.Font = fuente_PaintBrush;
        }

        private FontFamily CargarFuente(byte[] fuente)
        {
            FontFamily fuenteNueva;

            IntPtr data = Marshal.AllocCoTaskMem(fuente.Length);
            Marshal.Copy(fuente, 0, data, fuente.Length);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fuente.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddMemoryFont(data, fuente.Length);
            Marshal.FreeCoTaskMem(data);

            fuenteNueva = fontCollection.Families[0];
            return fuenteNueva;
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            string clienteNombre = txtNombreRegistro.Text;
            string apellidoCliente = txtApellidoRegistro.Text;
            string correoCliente = txtCorreoRegistro.Text;
            string numCliente = txtNumeroCelRegistro.Text;
            string identificacionCliente = txtCedulaRegistro.Text;
            Clientes clientes = new Clientes
            {
                Nombre = clienteNombre,
                Apellido = apellidoCliente,
                Correo = correoCliente,
                Numero_celular = numCliente,
                Identificacion = identificacionCliente
            };

            int idCliente = clientes.Id_cliente;
            MessageBox.Show(idCliente.ToString());
            Cuenta cuenta = new Cuenta {
                Usuario = txtUsuarioRegistro.Text,
                Contrasena = txtContrasenaRegistro.Text,
                Id_cliente = idCliente,
                Id_cuentaTipo = cmbxTipoCuenta.TabIndex    
            };

            Tipo_Cuenta tipo_Cuenta = new Tipo_Cuenta
            {
                Tipo = cmbxTipoCuenta.Text
            };

            for (int i = 0; i < txtContrasenaRegistro.Text.Length; i++)
            {
                if (Char.IsUpper(txtContrasenaRegistro.Text, i))
                {
                    chbxMayus.Checked = true;
                }
                else if (Char.IsDigit(txtContrasenaRegistro.Text, i))
                {
                    chbxNumero.Checked = true;
                }
                else
                {
                    chbxcaracter.Checked = true;
                }
            }
            try
            {

                NegocioCuenta negocioCuenta = new NegocioCuenta();
                string respuesta = negocioCuenta.ControlRegistro(clientes, cuenta, tipo_Cuenta);

                if (respuesta.Length > 0)
                {
                    MessageBox.Show(respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Usuario Registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
