using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmAumentaRemueveItems : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        double dbCantidad;

        int iIndice;

        public string sValorRetorno;

        public frmAumentaRemueveItems(int iIndice_P)
        {
            this.iIndice = iIndice_P;
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese la cantidad.";
                ok.ShowDialog();
                txtCantidad.Text = lblCantidad.Text;
            }

            else if (Convert.ToDouble(txtCantidad.Text.Trim()) == 0)
            {
                ok.LblMensaje.Text = "La cantidad no puede ser cero.";
                ok.ShowDialog();
            }

            else
            {
                sValorRetorno = txtCantidad.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtCantidad.Text.Trim()) > 1)
            {
                dbCantidad = Convert.ToDouble(txtCantidad.Text.Trim()) - 1;
                txtCantidad.Text = dbCantidad.ToString("N0");
                txtCantidad.Focus();
                txtCantidad.SelectionStart = txtCantidad.Text.Trim().Length;
            }
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtCantidad.Text.Trim()) >= 0)
            {
                dbCantidad = Convert.ToDouble(txtCantidad.Text.Trim()) + 1;
                txtCantidad.Text = dbCantidad.ToString("N0");
                txtCantidad.Focus();
                txtCantidad.SelectionStart = txtCantidad.Text.Trim().Length;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void frmAumentaRemueveItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
