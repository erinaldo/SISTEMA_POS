using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmFacturaElectronicaExterno : Form
    {
        public frmFacturaElectronicaExterno(DataTable dt)
        {
            InitializeComponent();
            Facturacion_Electronica.rptFacturaEletronicaRepartidores reporte = new Facturacion_Electronica.rptFacturaEletronicaRepartidores();
            reporte.SetDataSource(dt);
            this.rptFactura.ReportSource = reporte;
            this.rptFactura.Refresh();
        }
    }
}
