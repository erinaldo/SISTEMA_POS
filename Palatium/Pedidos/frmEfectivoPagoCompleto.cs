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
    public partial class frmEfectivoPagoCompleto : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        Pedidos.ClasePagoCompleto pagoCompleto = new Pedidos.ClasePagoCompleto();

        string origen, saldo, total;
        float suma = 0.00f;
        int id_pago;
        string sNombrePago;

        DataTable dtConsulta;
        
        string sSql;
        
        int iIdOrden;

        double dbTotal;
        double dbRecibido;
        double dbCambio;
        
        bool bRespuesta;

        int iIdFacturaGenerada_P;
        int iBanderaComandaPendiente;

        public frmEfectivoPagoCompleto(string sIdOrden_P, double dbTotal_P, int iBanderaComandaPendiente_P)
        {
            this.iIdOrden = Convert.ToInt32(sIdOrden_P);
            this.dbTotal = dbTotal_P;
            this.iBanderaComandaPendiente = iBanderaComandaPendiente_P;
            InitializeComponent();            
        }

        #region FUNCIONES DEL USUARIO

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

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                if ((txt_valor.Text == "0") && (sValor == "0"))
                {
                    return;
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

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txt_valor.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ingrese valor.";
                ok.ShowDialog();
            }

            else if (Convert.ToDouble(txt_valor.Text.Trim()) < dbTotal)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "El pago ingresado es inferior al valor a pagar. Favor corregir.";
                ok.ShowDialog();
            }

            else
            {
                dbRecibido = Convert.ToDouble(txt_valor.Text.Trim());
                dbCambio = dbRecibido - dbTotal;

                if (pagoCompleto.insertarPagoCompleto(iIdOrden, dbTotal, dbRecibido, dbCambio, iBanderaComandaPendiente) == true)
                {
                    if (Program.iEjecutarImpresion == 1)
                    {
                        iIdFacturaGenerada_P = pagoCompleto.iIdFactura;
                        ReportesTextBox.frmVerNotaVenta notaVenta = new ReportesTextBox.frmVerNotaVenta(iIdFacturaGenerada_P, 1);
                        notaVenta.ShowDialog();

                        if (notaVenta.DialogResult == DialogResult.OK)
                        {
                            Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                            ok.ShowDialog();
                            Program.dbValorPorcentaje = 0;
                            Program.dbDescuento = 0;
                            this.DialogResult = DialogResult.OK;
                        }
                    }

                    else
                    {
                        Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                        ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                        ok.ShowDialog();
                        Program.dbValorPorcentaje = 0;
                        Program.dbDescuento = 0;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void btnValorSugerido_Click(object sender, EventArgs e)
        {
            txt_valor.Text = string.Format("{0:0.00}", btnValorSugerido.Text);
        }

        private void btn10dolar_Click(object sender, EventArgs e)
        {
            suma += 10.00f;
            txt_valor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn5dolar_Click(object sender, EventArgs e)
        {
            suma += 5.00f;
            txt_valor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (txt_valor.Text.Trim().Contains('.') == true)
            {

            }

            else if (txt_valor.Text == "")
            {
                txt_valor.Text = txt_valor.Text + "0" + btnPunto.Text;
            }

            else
            {
                txt_valor.Text = txt_valor.Text + btnPunto.Text;
            }
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
                if (txt_valor.Text == "")
                {
                    suma = 0;
                }

                else
                {
                    suma = (float)Convert.ToDouble(txt_valor.Text);
                }
            }

            else
            {
                suma = 0;
            }
        }

        private void frmEfectivoPagoCompleto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
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

        private void btn2dolar_Click(object sender, EventArgs e)
        {
            suma += 2.00f;
            txt_valor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn1dolar_Click(object sender, EventArgs e)
        {
            suma += 1.00f;
            txt_valor.Text = string.Format("{0:0.00}", +suma).ToString();
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
        }

        private void frmEfectivoPagoCompleto_Load(object sender, EventArgs e)
        {
            btnValorSugerido.Text = dbTotal.ToString("N2");
            lblValor.Text = "$ " + dbTotal.ToString("N2");

            btnPagoJusto.Text = "Pago Justo" + Environment.NewLine + "$ " + dbTotal.ToString("N2");
            btnPagoJusto.AccessibleName = dbTotal.ToString("N2");
        }

        private void btnPagoJusto_Click(object sender, EventArgs e)
        {
            txt_valor.Text = btnPagoJusto.AccessibleName;

            dbRecibido = Convert.ToDouble(txt_valor.Text.Trim());
            dbCambio = dbRecibido - dbTotal;

            if (pagoCompleto.insertarPagoCompleto(iIdOrden, dbTotal, dbRecibido, dbCambio, iBanderaComandaPendiente) == true)
            {
                if (Program.iEjecutarImpresion == 1)
                {
                    iIdFacturaGenerada_P = pagoCompleto.iIdFactura;
                    ReportesTextBox.frmVerNotaVenta notaVenta = new ReportesTextBox.frmVerNotaVenta(iIdFacturaGenerada_P, 1);
                    notaVenta.ShowDialog();

                    if (notaVenta.DialogResult == DialogResult.OK)
                    {
                        Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                        ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                        ok.ShowDialog();
                        Program.dbValorPorcentaje = 0;
                        Program.dbDescuento = 0;
                        this.DialogResult = DialogResult.OK;
                    }
                }

                else
                {
                    Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                    ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                    ok.ShowDialog();
                    Program.dbValorPorcentaje = 0;
                    Program.dbDescuento = 0;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
