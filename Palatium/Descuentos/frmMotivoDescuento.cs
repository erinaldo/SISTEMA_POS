using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Descuentos
{
    public partial class frmMotivoDescuento : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public frmMotivoDescuento()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == "")
            {
                ok.LblMensaje.Text = "Debe ingresar un motivo de la cancelación del pedido.";
                ok.ShowDialog();
                txtMotivo.Focus();
            }
            else
            {
                Program.sMotivoDescuento = txtMotivo.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMotivoDescuento_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtMotivo;
        }
    }
}
