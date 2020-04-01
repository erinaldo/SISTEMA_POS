using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmOpcionesConsumoAlimentos : Form
    {
        public int iSeleccion;
        int iConsumoAlimentos;
        public frmOpcionesConsumoAlimentos(int iConsumoAlimentos)
        {
            this.iConsumoAlimentos = iConsumoAlimentos;
            InitializeComponent();
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnOrdenCompleta_Click(object sender, EventArgs e)
        {
            if (iConsumoAlimentos == 0)
            {
                iSeleccion = 1;
            }

            else
            {
                iSeleccion = 0;
            }

            this.DialogResult = DialogResult.Yes;
        }

        private void frmOpcionesConsumoAlimentos_Load(object sender, EventArgs e)
        {
            if (iConsumoAlimentos == 0)
            {
                btnOrdenCompleta.Text = "Aplicar a la Orden Completa";
            }

            else
            {
                btnOrdenCompleta.Text = "Remover a la Orden Completa";
            }
        }
    }
}
