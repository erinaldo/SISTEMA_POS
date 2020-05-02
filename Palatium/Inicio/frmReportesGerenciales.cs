using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Inicio
{
    public partial class frmReportesGerenciales : Form
    {
        public frmReportesGerenciales()
        {
            InitializeComponent();
        }

        private void btnEstadosCuentasRepartidoresExternos_Click(object sender, EventArgs e)
        {
            Reportes_Formas.frmEstadoCuentaRepExterno cuentas = new Reportes_Formas.frmEstadoCuentaRepExterno();
            cuentas.ShowDialog();
        }

        private void frmReportesGerenciales_Load(object sender, EventArgs e)
        {
            if (Program.sLogo != "")
            {
                if (File.Exists(Program.sLogo))
                {
                    logo.Image = Image.FromFile(Program.sLogo);
                }
            }

            if (Program.iVersionDemo == 1)
                lblVersionDemo.Visible = true;
            else
                lblVersionDemo.Visible = false;

            lblNombreEquipo.Text = Program.sNombreEquipo;
        }
    }
}
