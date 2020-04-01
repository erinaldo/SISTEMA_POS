using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class MotivoCancelacionProducto : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public MotivoCancelacionProducto()
        {
            InitializeComponent();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MotivoCancelacionProducto_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtMotivoCancelacionProducto;
        }

        private void MotivoCancelacionProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMotivoCancelacionProducto.Text == "")
                {
                    ok.LblMensaje.Text = "Ingrese el motivo de la cancelación.";
                    ok.Show();
                }
                else
                {
                    Program.sMotivoProductoCancelado = txtMotivoCancelacionProducto.Text;
                    CancelarPedido cancelacion = Owner as CancelarPedido;
                    int f = Convert.ToInt32(cancelacion.dgvPedido.CurrentRow.Index);
                    cancelacion.dgvPedido.Rows[f].Cells["guardada"].Value = cancelacion.dgvPedido.Rows[f].Cells["valuni"].Value;
                    cancelacion.dgvPedido.Rows[f].Cells["valor"].Value = 0;
                    cancelacion.dgvPedido.Rows[f].Cells["producto"].Value = cancelacion.dgvPedido.Rows[f].Cells["producto"].Value + "/CANCELADO";
                    cancelacion.dgvPedido.Rows[f].Cells["motivoCancelacion"].Value = Program.sMotivoProductoCancelado;
                    cancelacion.dgvPedido.Rows[f].Cells["cancelar"].Value = 1;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
