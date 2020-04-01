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
    public partial class tecladoNumericoDividirPrecio : Form
    {
        string sSaldo;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public tecladoNumericoDividirPrecio(string sSaldo)
        {
            this.sSaldo = sSaldo; 
            InitializeComponent();

        }
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        private void concatenarValores(string sValor)
        {
            try
            {
                if (this.txt_valor.Text == "0" && sValor == "0")
                    return;
                if (this.txt_valor.Text == "0" && sValor != "0")
                    this.txt_valor.Clear();
                if (this.txt_valor.Text.Trim().Contains<char>('.'))
                {
                    int length = this.txt_valor.Text.Trim().Length;
                    int num1 = 0;
                    int num2 = 0;
                    for (int startIndex = 0; startIndex < length; ++startIndex)
                    {
                        if (num1 == 1)
                            ++num2;
                        if (this.txt_valor.Text.Substring(startIndex, 1) == ".")
                            num1 = 1;
                    }
                    if (num2 < 2)
                        this.txt_valor.Text += sValor;
                }
                else
                    this.txt_valor.Text += sValor;
                this.txt_valor.Focus();
                this.txt_valor.SelectionStart = this.txt_valor.Text.Trim().Length;
            }
            catch (Exception ex)
            {
                this.ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                this.ok.ShowInTaskbar = false;
                int num = (int)this.ok.ShowDialog();
            }
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            if (this.txt_valor.Text.Length > 0)
            {
                this.txt_valor.Text.Substring(this.txt_valor.Text.Length - 1);
                this.txt_valor.Text = this.txt_valor.Text.Remove(this.txt_valor.Text.Length - 1, 1);
            }
            this.txt_valor.Focus();
            this.txt_valor.SelectionStart = this.txt_valor.Text.Trim().Length;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int num = (int)new DividirPrecio(Convert.ToInt32(this.txt_valor.Text), this.sSaldo).ShowDialog();
            this.Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tecladoNumericoDividirPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
            this.Close();
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            this.concatenarValores(this.btn0.Text);
        }

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btn9);
        }

        private void btnRetroceder_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btnRetroceder);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btnCancelar);
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            this.ingresaBoton(this.btnIngresar);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn0);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn1);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn2);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn3);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn4);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn5);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn6);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn7);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn8);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btn9);
        }

        private void btnRetroceder_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btnRetroceder);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btnCancelar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            this.salidaBoton(this.btnIngresar);
        }
    }
}
