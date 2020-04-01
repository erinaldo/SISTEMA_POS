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
    public partial class frmVerDatosConfig : Form
    {        
        public frmVerDatosConfig()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private void llenarInformacion()
        {
            lblConexion.Text = Program.SQLCONEXION;
            lblServidor.Text = Program.SQLSERVIDOR;
            lblDNS.Text = Program.SQLDNS;
            lblBaseDatos.Text = Program.SQLBDATOS;
            btnAceptar.Focus();
        }

        #endregion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVerDatosConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVerDatosConfig_Load(object sender, EventArgs e)
        {
            llenarInformacion();
        }
    }
}
