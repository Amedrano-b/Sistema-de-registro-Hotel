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
using System.Windows.Forms.VisualStyles;

namespace PaginaPrincipal
{
    public partial class InicioSesion : Form
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pFileview, uint cjSize, IntPtr pvResrved, [In] ref uint pNumfonts);
        public InicioSesion()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(125, Color.Black);
            Font fuente_PaintBrush = new Font(CargarFuente(Properties.Resources.PaintBrushScript_Regular), 36, FontStyle.Regular);
            lblInicioSesion.Font = fuente_PaintBrush;
     
         
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

        private void lblInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;
            try
            {
                NegocioCuenta negocioCuenta = new NegocioCuenta();
                string respuesta = negocioCuenta.ControlInicio(usuario, contrasena);
                if(respuesta.Length > 0)
                {
                    MessageBox.Show(respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PantallaInicio pantallaInicio = new PantallaInicio();
                    pantallaInicio.Show();
                   
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
  
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registrarse registrarse = new Registrarse();
            registrarse.Show();
            
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            
        }
    } 
}
