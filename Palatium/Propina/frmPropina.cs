using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Propina
{
    public partial class frmPropina : Form
    {
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        public Decimal dbPropina;

        public frmPropina()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                if ((txtValor.Text == "0") && (sValor == "0"))
                {
                    return;
                }

                else if ((txtValor.Text == "0") && (sValor != "0"))
                {
                    txtValor.Clear();
                }

                if (txtValor.Text.Trim().Contains('.') == true)
                {
                    int longi = txtValor.Text.Trim().Length;
                    int band = 0, cont = 0;

                    for (int i = 0; i < longi; i++)
                    {
                        if (band == 1)
                            cont++;

                        if (txtValor.Text.Substring(i, 1) == ".")
                            band = 1;
                    }

                    if (cont < 2)
                    {
                        txtValor.Text = txtValor.Text + sValor;
                    }
                }

                else
                {
                    txtValor.Text = txtValor.Text + sValor;
                }

                txtValor.Focus();
                txtValor.SelectionStart = txtValor.Text.Trim().Length;

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        #endregion

        private void Propina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (txtValor.Text.Trim().Contains('.') == true)
            {

            }

            else if (txtValor.Text == "")
            {
                txtValor.Text = txtValor.Text + "0" + btnPunto.Text;
            }

            else
            {
                txtValor.Text = txtValor.Text + btnPunto.Text;
            }
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtValor.Text.Length > 0)
            {

                str = txtValor.Text.Substring(txtValor.Text.Length - 1);
                loc = txtValor.Text.Length;
                txtValor.Text = txtValor.Text.Remove(loc - 1, 1);
            }

            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtValor.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ingrese un valor.";
                ok.ShowDialog();
            }
            else
            {
                dbPropina = Convert.ToDecimal(txtValor.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnIngresar_Click(sender, e);
            }

            else
            {
                if (e.KeyChar == 8)
                {
                    e.Handled = false;
                    return;
                }

                bool IsDec = false;
                int nroDec = 0;

                for (int i = 0; i < txtValor.Text.Length; i++)
                {
                    if (txtValor.Text[i] == '.')
                        IsDec = true;

                    if (IsDec && nroDec++ >= 2)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                if (e.KeyChar >= 48 && e.KeyChar <= 57)
                    e.Handled = false;
                else if (e.KeyChar == 46)
                    e.Handled = (IsDec) ? true : false;
                else
                    e.Handled = true;
            }
        }
    }
}
