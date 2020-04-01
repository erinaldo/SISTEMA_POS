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
    public partial class FDatosCliente : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        
        public FDatosCliente(string sIdOrden)
        {
            InitializeComponent();            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.iBanderaCliente = 0;
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNombreMesa.Text == "")
            {
                ok.LblMensaje.Text = "Debe ingresar el nombre del cliente para asignar en la mesa";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                //Program.sNOMBREMESA = txtNombreMesa.Text;
                //Program.iBanderaCliente = 1;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FDatosCliente_Load(object sender, EventArgs e)
        {
            
        }
    }
}
