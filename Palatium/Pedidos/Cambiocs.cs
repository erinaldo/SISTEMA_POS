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
    public partial class Cambiocs : Form
    {
        string cambio;
        int iCuenta = 10;

        public Cambiocs(string cambio)
        {
            InitializeComponent();
            this.cambio = cambio;
        }

        #region FUNCIONES DEL USUARIO

        private void llenarBarra()
        {
            pgBarra.Increment(1);
            //lblMensaje.Text = pgBarra.Value.ToString() + "%";

            if (pgBarra.Value == pgBarra.Maximum)
            {
                timerBarra.Stop();
                Program.dCambioPantalla = 0;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void contarSegundos()
        {
            lblMensaje.Text = "La ventana se cerrará en " + iCuenta.ToString() + " segundos.";
            iCuenta--;
        }

        #endregion

        public void Cambiocs_Load(object sender, EventArgs e)
        {
            lblCambio.Text = cambio;            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            timerBarra.Enabled = false;
            timer1.Enabled = false;
            this.DialogResult = DialogResult.OK;
            Program.dCambioPantalla = 0;
            this.Close();
        }

        private void timerBarra_Tick(object sender, EventArgs e)
        {
            llenarBarra();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            contarSegundos();
        }
    }
}
