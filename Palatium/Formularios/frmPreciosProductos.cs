using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Formularios
{
    public partial class frmPreciosProductos : Form
    {
        public frmPreciosProductos()
        {
            InitializeComponent();
        }

        private void btnListCategoria_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaPrecioProducCatego ayuda1 = new Formularios.FAyudaPrecioProducCatego();
            ayuda1.ShowDialog();
        }
    }
}
