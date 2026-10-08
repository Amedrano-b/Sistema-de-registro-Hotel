using PaginaPrincipal.UsuarioControles;
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
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;

namespace PaginaPrincipal
{
    public partial class PantallaInicio : Form
    {
        [DllImport("gdi32.dll")]

       
        private static extern IntPtr AddFontMemResourceEx(IntPtr pFileview, uint cjSize, IntPtr pvResrved, [In] ref uint pNumfonts);
        public PantallaInicio()
        {
            InitializeComponent();
            Font fuente_PaintBrush = new Font(CargarFuente(Properties.Resources.PaintBrushScript_Regular), 36, FontStyle.Regular);
            lblNombreHotel.Font = fuente_PaintBrush;

            
        }

        private FontFamily CargarFuente(byte[] fuente)
        {
            FontFamily fuenteNueva;

            IntPtr data = Marshal.AllocCoTaskMem(fuente.Length);    
            Marshal.Copy(fuente,0,data,fuente.Length);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fuente.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddMemoryFont(data, fuente.Length);
            Marshal.FreeCoTaskMem(data);
            
            fuenteNueva = fontCollection.Families[0];
            return fuenteNueva;
        }

        private void addUsarioControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btngestion_Click(object sender, EventArgs e)
        {
            btngestion.CheckedState.Equals(true);
            GestionBD gestionBD = new GestionBD();
            addUsarioControl(gestionBD );
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Inicio inicio = new Inicio();
            addUsarioControl(inicio);
        }

        private void btnOferta_Click(object sender, EventArgs e)
        {
            btnOferta.CheckedState.Equals (true);
            FormOfertas formOfertas = new FormOfertas();
            addUsarioControl(formOfertas);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
