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
    public partial class frmDescuentos : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public frmDescuentos()
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
                if ((txt_valor.Text == "0") && (sValor == "0"))
                {
                    goto fin;
                }

                else if ((txt_valor.Text == "0") && (sValor != "0"))
                {
                    txt_valor.Clear();
                }

                if (txt_valor.Text.Trim().Contains('.') == true)
                {
                    int longi = txt_valor.Text.Trim().Length;
                    int band = 0, cont = 0;

                    for (int i = 0; i < longi; i++)
                    {
                        if (band == 1)
                            cont++;

                        if (txt_valor.Text.Substring(i, 1) == ".")
                            band = 1;
                    }

                    if (cont < 2)
                    {
                        txt_valor.Text = txt_valor.Text + sValor;
                    }
                }

                else
                {
                    txt_valor.Text = txt_valor.Text + sValor;
                }

            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowDialog();
            }
        fin: { }
        }

        #endregion

        private void frmDescuentos_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_valor;
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txt_valor.Text.Length > 0)
            {
                str = txt_valor.Text.Substring(txt_valor.Text.Length - 1);
                loc = txt_valor.Text.Length;
                txt_valor.Text = txt_valor.Text.Remove(loc - 1, 1);
            }

            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
            txt_valor.Focus();
            txt_valor.SelectionStart = txt_valor.Text.Trim().Length;
        }

        private void frmDescuentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txt_valor.Text.Length; i++)
            {
                if (txt_valor.Text[i] == '.')
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
                btnIngresar_Click(sender, e);
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txt_valor.Text == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el valor de porcentaje para realizar el descuento";
                ok.ShowDialog();
                txt_valor.Focus();
            }

            else if (Convert.ToDouble(txt_valor.Text) != 0)
            {
                Program.dbDescuento = 0;
                Program.dbValorPorcentaje = 0;
                //string resultado;
                Program.dbValorPorcentaje = Convert.ToDouble(txt_valor.Text);
                Program.dbDescuento = Program.dbValorPorcentaje / 100;

                Descuentos.frmMotivoDescuento descuentos = new Descuentos.frmMotivoDescuento();
                descuentos.ShowInTaskbar = false;
                descuentos.ShowDialog();

                if (descuentos.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            else if (Convert.ToDouble(txt_valor.Text) == 0)
            {
                Program.dbDescuento = 0;
                Program.dbValorPorcentaje = 0;
                Program.sMotivoDescuento = "";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (txt_valor.Text.Trim().Contains('.') == true)
            {

            }

            else if (txt_valor.Text == "")
            {
                txt_valor.Text = txt_valor.Text + "0" + btnIngresar.Text;
            }

            else
            {
                txt_valor.Text = txt_valor.Text + btnIngresar.Text;
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

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
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
    }
}
