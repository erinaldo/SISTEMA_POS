using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmReporteListadoReceta : Form
    {
        DataTable dtConsulta;
        public frmReporteListadoReceta(DataTable dt)
        {
            this.dtConsulta = dt;
            InitializeComponent();
            cargarReporte();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR EL REPORTE
        private void cargarReporte()
        {
            Receta.rptListaRecetas reporte = new Receta.rptListaRecetas();
            reporte.SetDataSource(dtConsulta);

            CRReporte.ReportSource = reporte;
            CRReporte.Refresh();
        }

        #endregion
    }
}
