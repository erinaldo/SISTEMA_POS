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
    public partial class MotivoCancelacionCortesias : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public MotivoCancelacionCortesias()
        {
            InitializeComponent();
        }

        //FUNCION ACTIVA TECLADO

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMotivoCancelacionCortesia.Text == "")
                {
                    ok.LblMensaje.Text = "Ingrese el motivo de la cortesía.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
                else
                {
                    Program.sMotivoCortesia = txtMotivoCancelacionCortesia.Text;
                    Cortesias cortesia = Owner as Cortesias;
                    int f = Convert.ToInt32(cortesia.dgvPedido.CurrentRow.Index);
                    cortesia.dgvPedido.Rows[f].Cells["guardada"].Value = cortesia.dgvPedido.Rows[f].Cells["valuni"].Value;
                    cortesia.dgvPedido.Rows[f].Cells["valor"].Value = 0;
                    cortesia.dgvPedido.Rows[f].Cells["producto"].Value = cortesia.dgvPedido.Rows[f].Cells["producto"].Value + "/CORTESIA";
                    cortesia.dgvPedido.Rows[f].Cells["motivoCortesia"].Value = Program.sMotivoCortesia;
                    cortesia.dgvPedido.Rows[f].Cells["cortesia"].Value = 1;
                    this.Close();
                }
            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ha Ocurrido un Problema en el Proceso";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MotivoCancelacionCortesias_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtMotivoCancelacionCortesia;
        }

        private void MotivoCancelacionCortesias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
