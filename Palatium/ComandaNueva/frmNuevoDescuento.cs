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
    public partial class frmNuevoDescuento : Form
    {
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        public Decimal dbPorcentajeDescuento;

        public frmNuevoDescuento()
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

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnBackSpace_Click(object sender, EventArgs e)
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

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void frmDescuentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
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


            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAceptar_Click(sender, e);
            }
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (txtValor.Text.Trim().Contains('.') == true)
            {

            }

            else if (txtValor.Text == "")
            {
                txtValor.Text = txtValor.Text + "0.";
            }

            else
            {
                txtValor.Text = txtValor.Text + ".";
            }
        }

        private void btnBackSpace_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnBackSpace);
        }

        private void btnBackSpace_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnBackSpace);
        }

        private void btnPunto_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnPunto);
        }

        private void btnPunto_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnPunto);
        }

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn0);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn1);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn2);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn3);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn4);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn5);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn6);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn7);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn8);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn9);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn9);
        }

        private void frmNuevoDescuento_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtValor;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtValor.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No ha ingresado un valor para aplicar el descuento.";
                ok.ShowDialog();
                txtValor.Focus();
                return;
            }

            if (Convert.ToDecimal(txtValor.Text.Trim()) == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ha ingresado un valor cero como descuento.";
                ok.ShowDialog();
                txtValor.Focus();
                return;
            }

            if (Convert.ToDecimal(txtValor.Text.Trim()) >= 100)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ha ingresado un valor igual o superior al 100%.";
                ok.ShowDialog();
                txtValor.Clear();
                txtValor.Focus();
                return;
            }

            if (txtMotivo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese un motivo por el descuento.";
                ok.ShowDialog();
                txtMotivo.Focus();
                return;
            }

            dbPorcentajeDescuento = Convert.ToDecimal(txtValor.Text.Trim());
            this.DialogResult = DialogResult.OK;
        }
    }
}
