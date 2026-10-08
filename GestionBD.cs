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
    public partial class GestionBD : UserControl
    {
        public GestionBD()
        {
            InitializeComponent();
            FormClientes formClientes = new FormClientes();
            addUsarioControl(formClientes);

        }
        private void addUsarioControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btnclientes_Click(object sender, EventArgs e)
        {
            FormClientes formClientes = new FormClientes();
            addUsarioControl(formClientes);
            
        }
    }
}
