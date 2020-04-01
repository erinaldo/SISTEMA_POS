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
    public partial class frmMensajeOK : Form
    {
        int iCuenta;

        public frmMensajeOK()
        {            
            InitializeComponent();            
        }

        #region FUNCIONES DEL USUARIO

        private void contarSegundos()
        {
            lblVerMensaje.Text = "La ventana se cerrará en " + (iCuenta -1).ToString() + " segundos.";
            iCuenta--;

            if (iCuenta == -1)
            {
                timer1.Enabled = false;
                timer1.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmMensajeOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            contarSegundos();
        }

        private void frmMensajeOK_Load(object sender, EventArgs e)
        {
            lblVerMensaje.Text = "La ventana se cerrará en 3 segundos.";
            timer1.Enabled = true;
            timer1.Start();
            iCuenta = 3;
            
        }
    }
}
