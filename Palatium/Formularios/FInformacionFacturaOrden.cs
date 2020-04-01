using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InicioAplicacion.Formularios
{
    public partial class FInformacionFacturaOrden : Form
    {
        public FInformacionFacturaOrden()
        {
            InitializeComponent();
        }

        private void btnBuscClient_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaFacturaOrden ayuda1 = new Formularios.FAyudaFacturaOrden();
            ayuda1.ShowDialog();

            txtCodigoOrde.Text = ayuda1.Id;
            txtCliente.Text = ayuda1.Descripcion;
            //txtMoneda.Text = ayuda1.Moneda;
            //txtFecIniVa.Text = ayuda1.fechaIni;
            //txtFecFinVa.Text = ayuda1.fechaFin;
        }

        private void btnNuevoPosOrd_Click(object sender, EventArgs e)
        {

        }
    }
}
