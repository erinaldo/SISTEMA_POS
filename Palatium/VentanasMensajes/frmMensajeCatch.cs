using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.VentanasMensajes
{
    public partial class frmMensajeCatch : Form
    {
        VentanasMensajes.frmMensajeOK ok = new frmMensajeOK();

        public frmMensajeCatch()
        {
            InitializeComponent();
        }

        private void btnCopiar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(LblMensaje.Text, true);

                ok.LblMensaje.Text = "Texto copiado al portapapeles de Windows.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmMensajeCatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
