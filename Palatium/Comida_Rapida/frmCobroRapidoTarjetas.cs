using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Comida_Rapida
{
    public partial class frmCobroRapidoTarjetas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        string sSql;
        string sLoteRecuperado;
        string sFecha;
        public string sNumeroLote;

        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtFormasPago;
        public DataTable dtValores = new DataTable();

        int iCuentaFormasPagos;
        int iCuentaAyudaFormasPagos;
        int iPosXFormasPagos;
        int iPosYFormasPagos;
        public int iBanderaInsertarLote;
        public int iConciliacion;
        public int iOperadorTarjeta;
        public int iTipoTarjeta;

        public int iBanderaRecargo;
        public int iIdFormaPago;

        Button[,] boton = new Button[3, 2];
        Button bpagar;

        public Decimal dbPagar;
        Decimal dbPagarAuxiliar;
        public Decimal dbValorPropina;

        public frmCobroRapidoTarjetas(Decimal dbPagar_P, DataTable dtValores_P)
        {
            this.dbPagar = dbPagar_P;
            this.dbPagarAuxiliar = dbPagar_P;
            this.dtValores = dtValores_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR FORMAS DE PAGO CON RECARGO
        private void cargarFormasPagosRecargo()
        {
            try
            {
                sSql = "";
                sSql += "select FC.id_pos_tipo_forma_cobro, FC.codigo, FC.descripcion," + Environment.NewLine;
                sSql += "isnull(FC.imagen, '') imagen, MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                sSql += "and FC.estado = 'A'" + Environment.NewLine;
                sSql += "and MP.estado = 'A'" + Environment.NewLine;
                sSql += "where MP.codigo in ('TC', 'TD')";

                dtFormasPago = new DataTable();
                dtFormasPago.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtFormasPago, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
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
                iPosYFormasPagos = 0;
                iCuentaAyudaFormasPagos = 0;

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Cursor = Cursors.Hand;
                        boton[i, j].Click += new EventHandler(boton_clic);
                        boton[i, j].Size = new Size(153, 71);
                        boton[i, j].Location = new Point(iPosXFormasPagos, iPosYFormasPagos);
                        boton[i, j].BackColor = Color.Lime;
                        boton[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Bold);
                        boton[i, j].Tag = dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                        boton[i, j].Text = dtFormasPago.Rows[iCuentaFormasPagos]["descripcion"].ToString();
                        boton[i, j].AccessibleDescription = dtFormasPago.Rows[iCuentaFormasPagos]["id_sri_forma_pago"].ToString();
                        boton[i, j].TextAlign = ContentAlignment.MiddleCenter;

                        if (dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim() != "" && File.Exists(dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim()))
                        {
                            boton[i, j].TextAlign = ContentAlignment.MiddleRight;
                            boton[i, j].Image = Image.FromFile(dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim());
                            boton[i, j].ImageAlign = ContentAlignment.MiddleLeft;
                            boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        pnlFormasCobros.Controls.Add(boton[i, j]);
                        ++iCuentaFormasPagos;
                        ++iCuentaAyudaFormasPagos;

                        if (j + 1 == 2)
                        {
                            iPosXFormasPagos = 0;
                            iPosYFormasPagos += 71;
                        }

                        else
                        {
                            iPosXFormasPagos += 153;
                        }

                        if (dtFormasPago.Rows.Count == iCuentaFormasPagos)
                        {
                            btnSiguiente.Enabled = false;
                            break;
                        }
                    }

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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EVENTO CLIC DE LAS FORMAS DE PAGO
        public void boton_clic(object sender, EventArgs e)
        {
            bpagar = sender as Button;

            if (dgvPagos.Rows.Count == 0)
            {
                dgvPagos.Rows.Add(bpagar.Tag.ToString(), bpagar.Text.ToUpper(), dbPagar.ToString("N2"), bpagar.AccessibleDescription);
                dgvPagos.ClearSelection();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ya se ha ingresado una forma de cobro.";
                ok.ShowDialog();
            }            
        }

        //VERIFICAR EL RECARGO DE TARJETAS
        private void verificaValorRecargo()
        {
            try
            {
                int iPagaIva;
                Decimal dbCantidad;
                Decimal dbValorUnitario;
                Decimal dbValorRecargo;
                Decimal dbSumaRecargo;
                Decimal dbValorIva;
                Decimal dbSumaIva;
                Decimal dbSumaTotal = 0;

                if (dbPagar <= Program.dbValorMaximoRecargoTarjetas)
                {
                    for (int i = 0; i < dtValores.Rows.Count; i++)
                    {
                        iPagaIva = Convert.ToInt32(dtValores.Rows[i]["paga_iva"].ToString());
                        dbCantidad = Convert.ToDecimal(dtValores.Rows[i][0].ToString());
                        dbValorUnitario = Convert.ToDecimal(dtValores.Rows[i][1].ToString());
                        dbValorRecargo = dbValorUnitario * Program.dbPorcentajeRecargoTarjeta;
                        dbSumaRecargo = dbValorUnitario + dbValorRecargo;

                        if (iPagaIva == 1)
                        {
                            dbValorIva = dbSumaRecargo * Convert.ToDecimal(Program.iva);                            
                        }

                        else
                        {
                            dbValorIva = 0;
                        }

                        dbSumaIva = dbCantidad * (dbSumaRecargo + dbValorIva);

                        dtValores.Rows[i]["valor_recargo"] = dbSumaRecargo;
                        dtValores.Rows[i]["valor_iva"] = dbValorIva;
                        dtValores.Rows[i]["total"] = dbSumaIva;

                        dbSumaTotal += dbSumaIva;
                    }

                    dbPagar = dbSumaTotal;
                    lblTotal.Text = dbSumaTotal.ToString("N2");

                    iBanderaRecargo = 1;
                }

                else
                {
                    iBanderaRecargo = 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ASIGNAR LOS IDENTIFICADORES A LOS RADIO BUTTON
        private void valoresIdentificadores()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_operador_tarjeta, datafast, medianet," + Environment.NewLine;
                sSql += "valor_default, visible" + Environment.NewLine;
                sSql += "from pos_operador_tarjeta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[i]["datafast"].ToString()) == 1)
                    {
                        rdbDatafast.Tag = dtConsulta.Rows[i]["id_pos_operador_tarjeta"].ToString();
                        
                        if (Convert.ToInt32(dtConsulta.Rows[i]["valor_default"].ToString()) == 1)
                        {
                            rdbDatafast.Checked = true;
                        }

                        else
                        {
                            rdbMedianet.Checked = true;
                        }

                        if (Convert.ToInt32(dtConsulta.Rows[i]["visible"].ToString()) == 1)
                        {
                            rdbDatafast.Visible = true;
                        }

                        else
                        {
                            rdbDatafast.Visible = false;
                        }

                    }

                    else if (Convert.ToInt32(dtConsulta.Rows[i]["medianet"].ToString()) == 1)
                    {
                        rdbMedianet.Tag = dtConsulta.Rows[i]["id_pos_operador_tarjeta"].ToString();

                        if (Convert.ToInt32(dtConsulta.Rows[i]["valor_default"].ToString()) == 1)
                        {
                            rdbMedianet.Checked = true;
                        }

                        else
                        {
                            rdbDatafast.Checked = true;
                        }

                        if (Convert.ToInt32(dtConsulta.Rows[i]["visible"].ToString()) == 1)
                        {
                            rdbMedianet.Visible = true;
                        }

                        else
                        {
                            rdbMedianet.Visible = false;
                        }
                    }
                }

                if (rdbDatafast.Checked == true)
                {
                    numeroLote("01");
                }

                else
                {
                    numeroLote("02");
                }

                sSql = "";
                sSql += "select id_pos_tipo_tarjeta, credito, debito" + Environment.NewLine;
                sSql += "from pos_tipo_tarjeta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[i]["credito"].ToString()) == 1)
                    {
                        rdbCredito.Tag = dtConsulta.Rows[i]["id_pos_tipo_tarjeta"].ToString();
                    }

                    else if (Convert.ToInt32(dtConsulta.Rows[i]["debito"].ToString()) == 1)
                    {
                        rdbDebito.Tag = dtConsulta.Rows[i]["id_pos_tipo_tarjeta"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE LOTE
        private void numeroLote(string sCodigo_P)
        {
            try
            {                
                sSql = "";
                sSql += "select NL.lote" + Environment.NewLine;
                sSql += "from pos_numero_lote NL INNER JOIN" + Environment.NewLine;
                sSql += "pos_operador_tarjeta OP ON OP.id_pos_operador_tarjeta = NL.id_pos_operador_tarjeta" + Environment.NewLine;
                sSql += "and NL.estado = 'A'" + Environment.NewLine;
                sSql += "and OP.estado = 'A'" + Environment.NewLine;
                sSql += "where NL.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and NL.estado_lote = 'Abierta'" + Environment.NewLine;
                sSql += "and NL.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and OP.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and NL.id_pos_jornada = " + Program.iJORNADA;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    txtNumeroLote.ReadOnly = false;
                    sLoteRecuperado = "";
                    txtNumeroLote.Text = sLoteRecuperado;
                    iBanderaInsertarLote = 1;
                }

                else
                {
                    sLoteRecuperado = dtConsulta.Rows[0]["lote"].ToString().Trim();
                    txtNumeroLote.Text = sLoteRecuperado;
                    txtNumeroLote.ReadOnly = true;
                    iBanderaInsertarLote = 0;
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

        private void frmCobroRapidoTarjetas_Load(object sender, EventArgs e)
        {
            lblTotal.Text = dbPagar.ToString("N2");

            //EXTRAER LA FECHA DEL SISTEMA
            sSql = "";
            sSql += "select getdate() fecha";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == false)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.ShowDialog();
                return;
            }

            sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

            cargarFormasPagosRecargo();
            verificaValorRecargo();
            rdbDatafast.CheckedChanged -= new EventHandler(rdbDatafast_CheckedChanged);
            rdbMedianet.CheckedChanged -= new EventHandler(rdbMedianet_CheckedChanged);
            valoresIdentificadores();
            rdbDatafast.CheckedChanged += new EventHandler(rdbDatafast_CheckedChanged);
            rdbMedianet.CheckedChanged += new EventHandler(rdbMedianet_CheckedChanged);            
        }

        private void btnRemoverPago_Click(object sender, EventArgs e)
        {
            if (dgvPagos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay formas de pago ingresados para remover del registro";
                ok.ShowDialog();
            }

            else
            {
                dgvPagos.Rows.Clear();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaFormasPagos -= iCuentaAyudaFormasPagos;

            if (iCuentaFormasPagos <= 6)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaFormasPagos -= 6;

            crearBotonesFormasPagos();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotonesFormasPagos();
        }

        private void frmCobroRapidoTarjetas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (dgvPagos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No ha realizado el cobro de la comanda.";
                ok.ShowDialog();
            }

            else
            {
                iIdFormaPago = Convert.ToInt32(dgvPagos.Rows[0].Cells[0].Value);

                dbValorPropina = Convert.ToDecimal(txtPropina.Text.Trim());
                sNumeroLote = txtNumeroLote.Text.Trim();

                if (rdbDatafast.Checked == true)
                {
                    iOperadorTarjeta = Convert.ToInt32(rdbDatafast.Tag);
                }

                else
                {
                    iOperadorTarjeta = Convert.ToInt32(rdbMedianet.Tag);
                }

                if (rdbCredito.Checked == true)
                {
                    iTipoTarjeta = Convert.ToInt32(rdbCredito.Tag);
                }

                else
                {
                    iTipoTarjeta = Convert.ToInt32(rdbDebito.Tag);
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void rdbDatafast_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDatafast.Checked == true)
            {
                rdbMedianet.CheckedChanged -= new EventHandler(rdbMedianet_CheckedChanged);
                rdbCredito.CheckedChanged -= new EventHandler(rdbCredito_CheckedChanged);
                rdbDebito.CheckedChanged -= new EventHandler(rdbDebito_CheckedChanged);
                rdbMedianet.Checked = false;
                rdbMedianet.CheckedChanged += new EventHandler(rdbMedianet_CheckedChanged);
                rdbCredito.CheckedChanged += new EventHandler(rdbCredito_CheckedChanged);
                rdbDebito.CheckedChanged += new EventHandler(rdbDebito_CheckedChanged);

                numeroLote("01");
                iOperadorTarjeta = Convert.ToInt32(rdbDatafast.Tag);
            }
        }

        private void rdbMedianet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMedianet.Checked == true)
            {
                rdbDatafast.CheckedChanged -= new EventHandler(rdbDatafast_CheckedChanged);
                rdbCredito.CheckedChanged -= new EventHandler(rdbCredito_CheckedChanged);
                rdbDebito.CheckedChanged -= new EventHandler(rdbDebito_CheckedChanged);
                rdbDatafast.Checked = false;
                rdbDatafast.CheckedChanged += new EventHandler(rdbDatafast_CheckedChanged);
                rdbCredito.CheckedChanged += new EventHandler(rdbCredito_CheckedChanged);
                rdbDebito.CheckedChanged += new EventHandler(rdbDebito_CheckedChanged);

                numeroLote("02");
                iOperadorTarjeta = Convert.ToInt32(rdbMedianet.Tag);
            }
        }

        private void rdbCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCredito.Checked == true)
            {
                rdbDebito.CheckedChanged -= new EventHandler(rdbDebito_CheckedChanged);
                rdbDatafast.CheckedChanged -= new EventHandler(rdbDatafast_CheckedChanged);
                rdbMedianet.CheckedChanged -= new EventHandler(rdbMedianet_CheckedChanged);
                rdbDebito.Checked = false;
                rdbDebito.CheckedChanged += new EventHandler(rdbDebito_CheckedChanged);
                rdbDatafast.CheckedChanged += new EventHandler(rdbDatafast_CheckedChanged);
                rdbMedianet.CheckedChanged += new EventHandler(rdbMedianet_CheckedChanged);

                iTipoTarjeta = Convert.ToInt32(rdbCredito.Tag);
            }
        }

        private void rdbDebito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDebito.Checked == true)
            {
                rdbCredito.CheckedChanged -= new EventHandler(rdbCredito_CheckedChanged);
                rdbDatafast.CheckedChanged -= new EventHandler(rdbDatafast_CheckedChanged);
                rdbMedianet.CheckedChanged -= new EventHandler(rdbMedianet_CheckedChanged);
                rdbCredito.Checked = false;
                rdbCredito.CheckedChanged += new EventHandler(rdbCredito_CheckedChanged);
                rdbDatafast.CheckedChanged += new EventHandler(rdbDatafast_CheckedChanged);
                rdbMedianet.CheckedChanged += new EventHandler(rdbMedianet_CheckedChanged);

                iTipoTarjeta = Convert.ToInt32(rdbDebito.Tag);
            }
        }
    }
}
