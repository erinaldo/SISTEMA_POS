using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Utilitarios
{
    public partial class frmSeleccionConsumoInternoEmpleados : Form
    {

        public string sCodigo;
        public frmSeleccionConsumoInternoEmpleados()
        {
            InitializeComponent();
        }

        private void btnConsumoInterno_Click(object sender, EventArgs e)
        {
            sCodigo = "14";
            this.DialogResult = DialogResult.OK;
        }

        private void btnConsumoEmpleados_Click(object sender, EventArgs e)
        {
            sCodigo = "06";
            this.DialogResult = DialogResult.OK;
        }
    }
}
