using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.VentanasMensajes
{
    public partial class frmMensajeNuevoCatch : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        public frmMensajeNuevoCatch()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCopiar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(lblMensaje.Text, true);
                this.Close();
                ok.lblMensaje.Text = "Texto copiado al portapapeles de Windows.";
                ok.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        private void frmMensajeNuevoCatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
