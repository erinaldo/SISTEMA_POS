using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Bar
{
    public partial class frmCobrarCuenta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Button[] boton = new Button[4];
        Button bpagar;
        Button botonMemoria;

        string sSql;

        bool bRespuesta;

        public DataTable dtPagosDevolver;
        DataTable dtFormasPago;

        int iCuentaFormasPagos;
        int iCuentaAyudaFormasPagos;
        int iPosXFormasPagos;
        int iIdFormaPago;
        int iIdSriFormaPago;

        Decimal dbValorTotal;
        Decimal dbValorRecuperado;
        Decimal dSubtotal;

        float suma = 0.00f;

        public frmCobrarCuenta(Decimal dbValorTotal_P)
        {
            this.dbValorTotal = dbValorTotal_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL TECLADO NUMERICO

        public void cargarPrecios()
        {
            btnOp1.Text = cambio(btnValorSugerido.Text);
            btnOp2.Text = cambio(btnOp1.Text);
            btnOp3.Text = cambio(btnOp2.Text);
            btnOp4.Text = cambio(btnOp3.Text);
        }

        static string cambio(string a)
        {

            string resultadox;
            double x = Convert.ToDouble(a);

            int totalEntero = (int)x;

            if (x - totalEntero != 0)
            {
                resultadox = string.Format("{0:0.00}", +Math.Ceiling(x)).ToString();
            }

            else if (x % 10 == 0)
            {

                if (x >= 1 && x < 5)
                    resultadox = string.Format("{0:0.00}", (5)).ToString();
                else if (x >= 5 && x < 10)
                    resultadox = string.Format("{0:0.00}", (10)).ToString();
                else if (x >= 10 && x < 20)
                    resultadox = string.Format("{0:0.00}", (20)).ToString();
                else if (x >= 20 && x < 40)
                    resultadox = string.Format("{0:0.00}", (40)).ToString();
                else if (x >= 40 && x < 50)
                    resultadox = string.Format("{0:0.00}", (50)).ToString();
                else if (x >= 50 && x < 100)
                    resultadox = string.Format("{0:0.00}", (100)).ToString();
                else if (x >= 100 && x < 200)
                    resultadox = string.Format("{0:0.00}", (200)).ToString();
                else if (x >= 200 && x < 300)
                    resultadox = string.Format("{0:0.00}", (300)).ToString();
                else if (x >= 300 && x < 500)
                    resultadox = string.Format("{0:0.00}", (500)).ToString();
                else if (x >= 500 && x < 1000)
                    resultadox = string.Format("{0:0.00}", (1000)).ToString();
                else if (x >= 1000 && x < 5000)
                    resultadox = string.Format("{0:0.00}", (5000)).ToString();

                else
                {
                    resultadox = "0";

                }

            }
            else if (x % 10 != 0)
            {
                if (x % 5 == 0)
                {
                    resultadox = string.Format("{0:0.00}", +Math.Ceiling(x + 5)).ToString();
                }
                else
                {
                    double valor = x / 5;
                    int r = ((int)valor) + 1;
                    int multiploCinco = r * 5;
                    resultadox = string.Format("{0:0.00}", +multiploCinco).ToString();
                }

            }
            else
            {
                resultadox = "0";
            }

            return resultadox;
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
                        txtValor.Text += sValor;
                    }
                }

                else
                {
                    txtValor.Text += sValor;
                }

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //CREAR DATATABLE PARA EL GRID DE DEUDA
        private void llenarDetalleDeuda()
        {
            try
            {
                dgvDetalleDeuda.Rows.Add("ABONO", "0.00");
                dgvDetalleDeuda.Rows.Add("SALDO", "0.00");
                dgvDetalleDeuda.Rows.Add("CAMBIO", "0.00");
                dgvDetalleDeuda.Rows.Add("PROPINA", "0.00");

                dgvDetalleDeuda.Rows[1].DefaultCellStyle.BackColor = Color.FromArgb(128, 255, 128);
                dgvDetalleDeuda.Rows[3].DefaultCellStyle.BackColor = Color.FromArgb(128, 255, 128);

                dgvDetalleDeuda.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR TODAS LAS FORMAS DE PAGO
        private void cargarFormasPagos()
        {
            try
            {
                sSql = "";
                sSql += "select FC.id_pos_tipo_forma_cobro, MP.codigo, FC.descripcion," + Environment.NewLine;
                sSql += "isnull(FC.imagen, '') imagen, MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                sSql += "and FC.estado = 'A'" + Environment.NewLine;
                sSql += "and MP.estado = 'A'";

                dtFormasPago = new DataTable();
                dtFormasPago.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtFormasPago, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaFormasPagos = 0;

                if (dtFormasPago.Rows.Count > 0)
                {
                    if (dtFormasPago.Rows.Count > 8)
                    {
                        btnSiguiente.Enabled = true;
                    }

                    else
                    {
                        btnSiguiente.Enabled = false;
                    }

                    if (crearBotonesFormasPagos() == true)
                    { }

                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONS DE TODAS LAS FORMAS DE PAGO
        private bool crearBotonesFormasPagos()
        {
            try
            {
                pnlFormasCobros.Controls.Clear();
                iPosXFormasPagos = 0;
                iCuentaAyudaFormasPagos = 0;

                for (int i = 0; i < 4; i++)
                {
                    boton[i] = new Button();
                    boton[i].Name = "btnPago_" + dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                    boton[i].Cursor = Cursors.Hand;
                    boton[i].Click += new EventHandler(boton_clic);
                    boton[i].Size = new Size(197, 60);
                    boton[i].Location = new Point(iPosXFormasPagos, 0);
                    boton[i].FlatAppearance.BorderSize = 0;
                    boton[i].FlatAppearance.MouseOverBackColor = Color.Magenta;
                    boton[i].FlatStyle = FlatStyle.Flat;
                    boton[i].Font = new Font("Century Gothic", 11.25F, FontStyle.Regular);
                    boton[i].BackColor = Color.FromArgb(45, 45, 48);
                    boton[i].ForeColor = Color.White;
                    boton[i].Image = Properties.Resources.producto;
                    boton[i].ImageAlign = ContentAlignment.MiddleLeft;
                    boton[i].Tag = dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                    boton[i].Text = dtFormasPago.Rows[iCuentaFormasPagos]["descripcion"].ToString();
                    boton[i].AccessibleDescription = dtFormasPago.Rows[iCuentaFormasPagos]["id_sri_forma_pago"].ToString();
                    boton[i].AccessibleName = dtFormasPago.Rows[iCuentaFormasPagos]["codigo"].ToString();

                    if (botonMemoria == null)
                    {
                        boton[i].BackColor = Color.FromArgb(45, 45, 48);
                    }

                    else
                    {
                        if (Convert.ToInt32(botonMemoria.Tag) == Convert.ToInt32(dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString()))
                        {
                            botonMemoria = boton[i];
                            boton[i].BackColor = Color.DodgerBlue;
                        }

                        else
                        {
                            boton[i].BackColor = Color.FromArgb(45, 45, 48);
                        }
                    }

                    pnlFormasCobros.Controls.Add(boton[i]);

                    iCuentaFormasPagos++;
                    iCuentaAyudaFormasPagos++;
                    iPosXFormasPagos += 197;

                    if (dtFormasPago.Rows.Count == iCuentaFormasPagos)
                    {
                        btnSiguiente.Enabled = false;
                        break;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EVENTO CLIC DE LAS FORMAS DE PAGO
        public void boton_clic(object sender, EventArgs e)
        {
            try
            {
                bpagar = sender as Button;

                if (Convert.ToDecimal(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                    ok.ShowDialog();
                    return;
                }

                if (botonMemoria != null)
                {
                    botonMemoria.BackColor = Color.FromArgb(45, 45, 48);
                }

                bpagar.BackColor = Color.DodgerBlue;
                botonMemoria = bpagar;

                iIdFormaPago = Convert.ToInt32(bpagar.Tag);
                iIdSriFormaPago = Convert.ToInt32(bpagar.AccessibleDescription);
                lblNombrePago.Text = bpagar.Text.ToString().Trim().ToUpper();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULAR LOS VALORES
        private void recalcularValores()
        {
            try
            {
                if (dgvPagos.Rows.Count == 0)
                {
                    dgvDetalleDeuda.Rows[0].Cells[1].Value = "0.00";
                    dgvDetalleDeuda.Rows[1].Cells[1].Value = dbValorTotal.ToString("N2");
                    dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";

                    btnValorSugerido.Text = dbValorTotal.ToString("N2");
                }

                else
                {
                    dSubtotal = 0;

                    for (int i = 0; i < dgvPagos.Rows.Count; i++)
                    {
                        dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells["valor"].Value);
                    }

                    dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
                    

                    if (dSubtotal > dbValorTotal)
                    {
                        dgvDetalleDeuda.Rows[1].Cells[1].Value = "0.00";
                        dgvDetalleDeuda.Rows[2].Cells[1].Value = (dSubtotal - dbValorTotal).ToString("N2");
                    }

                    else
                    {
                        dgvDetalleDeuda.Rows[1].Cells[1].Value = (dbValorTotal - dSubtotal).ToString("N2");
                        dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                    }
                }

                botonMemoria = null;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Inicio.IForm frmInterface = this.Owner as Inicio.IForm;

            if (frmInterface != null)
            {
                frmInterface.mostrarOcultar(3);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Inicio.IForm frmInterface = this.Owner as Inicio.IForm;

            if (frmInterface != null)
            {
                frmInterface.mostrarOcultar(3);
            }
        }

        private void frmCobrarCuenta_Load(object sender, EventArgs e)
        {
            btnValorSugerido.Text = dbValorTotal.ToString("N2");
            cargarPrecios();
            cargarFormasPagos();
            llenarDetalleDeuda();

            lblTotal.Text = "$ " + dbValorTotal.ToString("N2");
            dgvDetalleDeuda.Rows[1].Cells[1].Value = dbValorTotal.ToString("N2");
            dgvPagos.Columns[0].Visible = false;
            dgvPagos.ClearSelection();
            dgvDetalleDeuda.ClearSelection();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotonesFormasPagos();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaFormasPagos -= iCuentaAyudaFormasPagos;

            if (iCuentaFormasPagos <= 4)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaFormasPagos -= 4;
            crearBotonesFormasPagos();
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
                if (txtValor.Text == "")
                {
                    suma = 0;
                }

                else
                {
                    suma = (float)Convert.ToDouble(txtValor.Text);
                }
            }

            else
            {
                suma = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void btn1dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            suma += 1.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn2dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            suma += 2.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn5dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            suma += 5.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn10dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            suma += 10.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn20dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            suma += 20.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btn50dolar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }

            suma += 50.00f;
            txtValor.Text = string.Format("{0:0.00}", +suma).ToString();
        }

        private void btnOp1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }

            txtValor.Text = btnOp1.Text;
        }

        private void btnOp2_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            txtValor.Text = btnOp2.Text; cargarPrecios();
        }

        private void btnOp3_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            txtValor.Text = btnOp3.Text;
        }

        private void btnOp4_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }
            
            txtValor.Text = btnOp4.Text;
        }

        private void btnValorSugerido_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }

            if (iIdFormaPago == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la forma de pago.";
                ok.ShowDialog();
                return;
            }

            txtValor.Text = btnValorSugerido.Text.Trim();

            dgvPagos.Rows.Add(iIdFormaPago.ToString(), lblNombrePago.Text.Trim(), btnValorSugerido.Text.Trim(),
                              iIdSriFormaPago.ToString(), "0", "0", "0", "", "0");

            dbValorRecuperado = Convert.ToDecimal(txtValor.Text.Trim());

            btnValorSugerido.Text = "0.00";
            iIdFormaPago = 0;
            iIdSriFormaPago = 0;
            lblNombrePago.Text = "SELECCIONE PAGO";
            txtValor.Clear();
            botonMemoria.BackColor = Color.FromArgb(45, 45, 48);
            recalcularValores();
            dgvPagos.ClearSelection();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
                return;
            }

            if (Convert.ToDecimal(txtValor.Text.Trim()) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Debe ingresar un valor superior a cero";
                ok.ShowDialog();
                txtValor.Clear();
                txtValor.Focus();
                return;
            }

            if (txtValor.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Debe ingresar un valor";
                ok.ShowDialog();
                txtValor.Clear();
                txtValor.Focus();
                return;
            }

            if (iIdFormaPago == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la forma de pago.";
                ok.ShowDialog();
                return;
            }

            dgvPagos.Rows.Add(iIdFormaPago.ToString(), lblNombrePago.Text.Trim(), Convert.ToDecimal(txtValor.Text.Trim()).ToString("N2"),
                              iIdSriFormaPago.ToString(), "0", "0", "0", "", "0");

            suma = 0;
            iIdFormaPago = 0;
            iIdSriFormaPago = 0;
            lblNombrePago.Text = "SELECCIONE PAGO";
            txtValor.Clear();
            botonMemoria.BackColor = Color.FromArgb(45, 45, 48);
            recalcularValores();
            btnValorSugerido.Text = dgvDetalleDeuda.Rows[1].Cells[1].Value.ToString();
            dgvPagos.ClearSelection();
        }

        private void btnRemoverPago_Click(object sender, EventArgs e)
        {
            if (dgvPagos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay formas de pago ingresados para remover del registro";
                ok.ShowDialog();
                return;
            }

            if (dgvPagos.SelectedRows.Count > 0)
            {
                dgvPagos.Rows.Remove(dgvPagos.CurrentRow);

                dSubtotal = 0;

                for (int i = 0; i < dgvPagos.Rows.Count; i++)
                {
                    dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value.ToString());
                }                

                dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
                dgvDetalleDeuda.Rows[1].Cells[1].Value = (dbValorTotal - Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value)).ToString("N2");

                btnValorSugerido.Text = dgvDetalleDeuda.Rows[1].Cells[1].Value.ToString();

                if (dbValorTotal > dSubtotal)        //AQUI REVISAR LA CONDICION
                {
                    dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                }

                else
                {
                    dgvDetalleDeuda.Rows[2].Cells[1].Value = (dSubtotal - dbValorTotal).ToString("N2");
                }

                if (dgvPagos.Rows.Count == 0)
                {
                    dgvDetalleDeuda.Rows[3].Cells[1].Value = "0.00";
                }

                dgvPagos.ClearSelection();
                recalcularValores();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se ha seleccionado una línea para remover.";
                ok.ShowDialog();
            }
        }

        private void btnPropina_Click(object sender, EventArgs e)
        {
            Propina.frmPropina propina = new Propina.frmPropina();
            propina.ShowDialog();

            if (propina.DialogResult == DialogResult.OK)
            {
                Decimal dbPropina_P = propina.dbPropina;
                dgvDetalleDeuda.Rows[3].Cells[1].Value = dbPropina_P.ToString("N2");
                propina.Close();
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            dtPagosDevolver = new DataTable();
            dtPagosDevolver.Clear();

            dtPagosDevolver.Columns.Add("id_pos_tipo_forma_cobro");
            dtPagosDevolver.Columns.Add("descripcion");
            dtPagosDevolver.Columns.Add("valor");
            dtPagosDevolver.Columns.Add("id_forma_pago");
            dtPagosDevolver.Columns.Add("conciliacion");
            dtPagosDevolver.Columns.Add("id_pos_operador_tarjeta");
            dtPagosDevolver.Columns.Add("id_pos_tipo_tarjeta");
            dtPagosDevolver.Columns.Add("numero_lote");
            dtPagosDevolver.Columns.Add("bandera_conciliacion");

            for (int i = 0; i < dgvPagos.Rows.Count; i++)
            {
                DataRow row = dtPagosDevolver.NewRow();
                row["id_pos_tipo_forma_cobro"] = dgvPagos.Rows[i].Cells["id_forma_cobro"].Value.ToString();
                row["descripcion"] = dgvPagos.Rows[i].Cells["fpago"].Value.ToString();
                row["valor"] = dgvPagos.Rows[i].Cells["valor"].Value.ToString();
                row["id_forma_pago"] = dgvPagos.Rows[i].Cells["id_sri"].Value.ToString();
                row["conciliacion"] = dgvPagos.Rows[i].Cells["conciliacion"].Value.ToString();
                row["id_pos_operador_tarjeta"] = dgvPagos.Rows[i].Cells["id_operador_tarjeta"].Value.ToString();
                row["id_pos_tipo_tarjeta"] = dgvPagos.Rows[i].Cells["id_tipo_tarjeta"].Value.ToString();
                row["numero_lote"] = dgvPagos.Rows[i].Cells["numero_lote"].Value.ToString();
                row["bandera_conciliacion"] = dgvPagos.Rows[i].Cells["bandera_insertar_lote"].Value.ToString();

                dtPagosDevolver.Rows.Add(row);
            }
        }
    }
}
