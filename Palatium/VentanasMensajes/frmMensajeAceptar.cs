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
    public partial class frmMensajeAceptar : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql = "";
        string sFecha = "";
        string sHora = "";

        public frmMensajeAceptar()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                //VentanasMensajes.frmMensajeOK ok = new frmMensajeOK();
                //ok.LblMensaje.Text = "El cierre de cajero se ha registrado con éxito.";
                //ok.ShowDialog();

                //if (ok.DialogResult == DialogResult.OK)
                //{
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                //}
            }

            catch (Exception)
            {
               
            }
        }

        private void frmMensajeAceptar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
