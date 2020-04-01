using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cancelar_Orden
{
    public partial class frmMotivoEliminacionComanda : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        public frmMotivoEliminacionComanda()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        private void btnAceptar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAceptar);
        }

        private void btnAceptar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAceptar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == "")
            {
                ok.lblMensaje.Text = "Debe ingresar un motivo de la cancelación del pedido.";
                ok.ShowDialog();
                txtMotivo.Focus();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtMotivo.Text == "")
                {
                    ok.lblMensaje.Text = "Debe ingresar un motivo de la cancelación del pedido.";
                    ok.ShowDialog();
                    txtMotivo.Focus();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
