using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmRenombrarMesa : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;

        public string sNombreMesa;

        public frmRenombrarMesa(string sNombreMesa_P)
        {
            this.sNombreMesa = sNombreMesa_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VALIDAR
        private void validar()
        {
            if (txtNombreMesa.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Debe ingresar un nombre para identificar la mesa.";
                ok.ShowDialog();
                txtNombreMesa.Focus();
                return;
            }

            sNombreMesa = txtNombreMesa.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            validar();
        }

        private void txtNombreMesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                validar();
            }
        }

        private void frmRenombrarMesa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmRenombrarMesa_Load(object sender, EventArgs e)
        {
            txtNombreMesa.Text = sNombreMesa;
            txtNombreMesa.Focus();
        }
    }
}
