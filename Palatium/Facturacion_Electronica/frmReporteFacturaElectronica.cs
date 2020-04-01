using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmReporteFacturaElectronica : Form
    {
        public frmReporteFacturaElectronica(DataTable dt)
        {
            InitializeComponent();

            Facturacion_Electronica.rptFacturaEletronica reporte = new Facturacion_Electronica.rptFacturaEletronica();
            reporte.SetDataSource(dt);
            this.rptFactura.ReportSource = reporte;
            this.rptFactura.Refresh();
            //reporte.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
            //this.Close();
            //reporte.Close();
        }
    }
}
