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

namespace Palatium.Pedidos
{
    public partial class frmCambiarFormasCobros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        ValidarCedula validarCedula = new ValidarCedula();

        Button[,] boton = new Button[5, 2];

        int iIdCaja;
        int iCgEstadoDctoPorCobrar = 7461;
        int iIdTipoEmision = 0;
        int iIdTipoAmbiente = 0;

        Orden ord;
        Button bpagar;

        string sSql;
        string sCiudad;
        string sNumeroFactura;
        string sIdOrden;
        string sTabla;
        string sCampo;
        string sFecha;
        string sFacturaRecuperada;
        string sMovimiento;
        string sSecuencial;
        string sNumeroOrden;
        string sEstablecimiento;
        string sPuntoEmision;
        string sClaveAcceso;
        string sCorreoElectronicoCF;
        string sLoteRecuperado;
        string sNumeroLote;

        long iMaximo;

        DataTable dtConsulta;
        DataTable dtFormasPago;
        DataTable dtComanda;
        DataTable dtAuxiliar;
        DataTable dtTarjetasT;
        DataTable dtAgrupado;
        DataTable dtAlmacenar;
        DataTable dtOriginal;

        bool bRespuesta;
        int iCuentaFormasPagos;
        int iCuentaAyudaFormasPagos;
        int iPosXFormasPagos;
        int iPosYFormasPagos;
        int iIdLocalidadImpresora;
        int iNumeroMovimientoCaja;
        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iBanderaDomicilio;
        int iTercerDigito;
        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPago;
        int iIdDocumentoPagado;
        int iCgTipoDocumento;
        int iIdDocumentoPago;
        int iNumeroPago;
        int iEjecutarActualizacionIVA;
        int iEjecutarActualizacionTarjetas;
        int iIdPosMovimientoCaja;
        int iOpCambiarEstadoOrden;
        int iNumeroMovimiento;
        int iIdTipoComprobante;
        int iCerrarCuenta;
        int iNumeroPedido;
        int iIdFactura;
        int iManejaFE;
        int iBanderaGeneraFactura;
        int iIdListaMinorista_P;
        int iBanderaRemoverIvaBDD;
        int iBanderaRecargoBDD;
        int iBanderaRemoverIvaBoton;
        int iBanderaRecargoBoton;
        int iNumeroPedido_P;
        int iNumeroCuenta_P;
        int iIdFormaPago_1;
        int iIdFormaPago_2;
        int iIdFormaPago_3;
        int iConciliacion;
        int iBanderaInsertarLote;
        int iOperadorTarjeta;
        int iTipoTarjeta;

        Decimal dTotal;
        Decimal dSubtotal;
        Decimal dValor;
        Decimal dbSumaIva;
        Decimal dbRecalcularPrecioUnitario;
        Decimal dbRecalcularDescuento;
        Decimal dbRecalcularIva;
        Decimal dbValorGrid;
        Decimal dbValorRecuperado;
        Decimal dbPropina;
        Decimal dbCambio;
        Decimal dbIVAPorcentaje;
        Decimal dServicio;
        Decimal dbTotalAyuda;
        Decimal dbSubTotalRecargo;
        Decimal dbValorRecargo;
        Decimal dbPorcentajeRecargo;
        Decimal dbSubTotalNetoRecargo;
        Decimal dbIVARecargo;
        Decimal dbTotalRecargo;

        public frmCambiarFormasCobros(string sIdOrden_P)
        {
            sIdOrden = sIdOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

                if (!bRespuesta)
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
                    return;
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

                for (int i = 0; i < 5; ++i)
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
                        boton[i, j].Tag = (object)dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                        boton[i, j].Text = dtFormasPago.Rows[iCuentaFormasPagos]["descripcion"].ToString();
                        boton[i, j].AccessibleDescription = dtFormasPago.Rows[iCuentaFormasPagos]["id_sri_forma_pago"].ToString();
                        boton[i, j].AccessibleName = dtFormasPago.Rows[iCuentaFormasPagos]["codigo"].ToString();
                        boton[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        boton[i, j].FlatStyle = FlatStyle.Flat;
                        boton[i, j].FlatAppearance.BorderSize = 1;
                        boton[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);

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

            if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0.0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
            }

            else
            {
                Efectivo efectivo = new Efectivo(bpagar.Tag.ToString(), dgvDetalleDeuda.Rows[1].Cells[1].Value.ToString(), "", bpagar.Text.ToString(), bpagar.AccessibleName);
                efectivo.ShowDialog();

                if (efectivo.DialogResult == DialogResult.OK)
                {
                    dbValorGrid = efectivo.dbValorGrid;
                    dbValorRecuperado = efectivo.dbValorIngresado;
                    dbPropina = efectivo.dbValorPropina;
                    sNumeroLote = efectivo.sNumeroLote;
                    iConciliacion = efectivo.iConciliacion;
                    iOperadorTarjeta = efectivo.iOperadorTarjeta;
                    iTipoTarjeta = efectivo.iTipoTarjeta;
                    iBanderaInsertarLote = efectivo.iBanderaInsertarLote;

                    dgvPagos.Rows.Add(efectivo.sIdPago, efectivo.sNombrePago, dbValorGrid.ToString("N2"),
                                      bpagar.AccessibleDescription, iConciliacion.ToString(), iOperadorTarjeta.ToString(),
                                      iTipoTarjeta.ToString(), sNumeroLote, iBanderaInsertarLote.ToString());

                    dgvDetalleDeuda.Rows[3].Cells[1].Value = dbPropina.ToString("N2");
                    dgvPagos.ClearSelection();
                    efectivo.Close();
                    recalcularValores();
                }
            }
        }

        //FUNCION PARA RECALCULAR LOS VALORES
        private void recalcularValores()
        {
            try
            {
                dgvDetalleDeuda.Rows[0].Cells[1].Value = (Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value) + dbValorRecuperado).ToString("N2");
                dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value)).ToString("N2");

                if (Convert.ToDecimal(dgvDetalleDeuda.Rows[1].Cells[1].Value) < 0)
                {
                    dgvDetalleDeuda.Rows[1].Cells[1].Value = "0.00";
                }

                dgvDetalleDeuda.Rows[2].Cells[1].Value = (Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value) - dTotal).ToString("N2");

                if (Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) < 0)
                {
                    dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                }

                if (Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) <= 0)
                {
                    return;
                }

                Program.dCambioPantalla = Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CREAR DATATABLE ´PARA EL GRID DE DEUDA
        private void llenarDetalleDeuda()
        {
            try
            {
                dgvDetalleDeuda.Rows.Add("ABONO", "0.00");
                dgvDetalleDeuda.Rows.Add("SALDO", "0.00");
                dgvDetalleDeuda.Rows.Add("CAMBIO", "0.00");
                dgvDetalleDeuda.Rows.Add("PROPINA", "0.00");
                dgvDetalleDeuda.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER EL TOTAL
        private void obtenerTotal()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(sum(cantidad * (precio_unitario + valor_iva + valor_otro - valor_dscto)), 10, 2)) total" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dTotal = Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                    lblTotal.Text = "$ " + dTotal.ToString("N2");
                    dbTotalAyuda = Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL GRID DE PAGOS
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if ((bRespuesta == false) || (dtConsulta.Rows.Count <= 0))
                {
                    return;
                }

                Decimal num = new Decimal(0);

                for (int i = 0; i < dtConsulta.Rows.Count; ++i)
                {
                    string sCodigo_R = dtConsulta.Rows[i]["codigo"].ToString().Trim().ToUpper();

                    if ((sCodigo_R == "TD") || (sCodigo_R == "TC"))
                    {
                        iConciliacion = 1;
                    }

                    else
                    {
                        iConciliacion = 0;
                    }

                    dgvPagos.Rows.Add();
                    dgvPagos.Rows[i].Cells["id"].Value = dtConsulta.Rows[i]["id_pos_tipo_forma_cobro"].ToString();
                    dgvPagos.Rows[i].Cells["fpago"].Value = dtConsulta.Rows[i]["descripcion"].ToString();
                    dValor = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                    dgvPagos.Rows[i].Cells["valor"].Value = dValor.ToString("N2");
                    dgvPagos.Rows[i].Cells["id_sri"].Value = dtConsulta.Rows[i]["id_sri_forma_pago"].ToString();
                    dgvPagos.Rows[i].Cells["conciliacion"].Value = iConciliacion.ToString();
                    dgvPagos.Rows[i].Cells["id_operador_tarjeta"].Value = dtConsulta.Rows[i]["id_pos_operador_tarjeta"].ToString();
                    dgvPagos.Rows[i].Cells["id_tipo_tarjeta"].Value = dtConsulta.Rows[i]["id_pos_tipo_tarjeta"].ToString();
                    dgvPagos.Rows[i].Cells["numero_lote"].Value = dtConsulta.Rows[i]["lote_tarjeta"].ToString();
                    dgvPagos.Rows[i].Cells["bandera_insertar_lote"].Value = "1";

                    num += dValor;
                }

                dgvPagos.Columns[0].Visible = false;

                for (int i = 0; i < dgvPagos.Rows.Count; i++)
                {
                    if (dgvPagos.Rows[i].Cells["fpago"].Value == null)
                    {
                        dgvPagos.Rows[i].Cells["fpago"].Value = 0;
                    }

                    dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value);
                }

                dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
                dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - dSubtotal).ToString("N2");
                dgvPagos.Columns[0].Visible = false;
                dgvPagos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CAMBIAR LAS FORMAS DE PAGO
        private void cambiarFormasPagos()
        {
            try
            {
                if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) != 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No ha realizado el cobro completo de la comanda.";
                    ok.ShowDialog();
                    return;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "select id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                iCuenta = 0;

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                if (iCuenta > 0)
                {
                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    }

                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += dTotal + ", " + Convert.ToDouble(dgvDetalleDeuda.Rows[3].Cells[1].Value) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPago = Convert.ToInt32(iMaximo);
                }

                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                for (int i = 0; i < dgvPagos.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select cg_tipo_documento" + Environment.NewLine;
                    sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                    sSql += "where id_pos_tipo_forma_cobro = " + dgvPagos.Rows[i].Cells[0].Value;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    iConciliacion = Convert.ToInt32(dgvPagos.Rows[i].Cells[4].Value.ToString());
                    iOperadorTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[5].Value.ToString());
                    iTipoTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[6].Value.ToString());
                    iBanderaInsertarLote = Convert.ToInt32(dgvPagos.Rows[i].Cells[8].Value.ToString());
                    sNumeroLote = dgvPagos.Rows[i].Cells[7].Value.ToString();

                    int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                    if (iRespuestaNumeroLote == -1)
                    {
                        goto reversa;
                    }

                    if (iRespuestaNumeroLote == 0)
                    {
                        if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                        {
                            goto reversa;
                        }
                    }

                    sSql = "";
                    sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                    sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                    sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                    sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                    sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                    if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) == 1)
                    {
                        sSql += (Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + dbCambio) + ", ";
                    }

                    else
                    {
                        sSql += "null, ";
                    }

                    if (iConciliacion == 1)
                    {
                        sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta;
                    }

                    else
                    {
                        sSql += "null, null, null";
                    }

                    sSql += ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) != 1 || Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) != 11)
                    {
                        sTabla = "cv403_documentos_pagos";
                        sCampo = "id_documento_pago";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdDocumentoPago = Convert.ToInt32(iMaximo);
                        }
                    }
                }

                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotal + ", 'A', 1, 0," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (actualizarMovimientosCaja() == false)
                {
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Las formas de pago se han actualizado éxitosamente";
                ok.ShowDialog();
                this.DialogResult = DialogResult.OK;
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA INSERTAR EL NUMERO DE LOTE EN LA TABLA POS_NUMERO_LOTE
        private bool insertarNumeroLote(string sNumeroLote_P, int iOperadorTarjeta_P)
        {
            try
            {
                sSql = "";
                sSql += "insert into pos_numero_lote (" + Environment.NewLine;
                sSql += "id_localidad, id_pos_jornada, id_pos_operador_tarjeta, lote," + Environment.NewLine;
                sSql += "fecha_apertura, estado_lote, id_pos_cierre_cajero, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", " + Program.iJORNADA + ", " + iOperadorTarjeta_P + ", ";
                sSql += "'" + sNumeroLote_P + "', '" + sFecha + "', 'Abierta', " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
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

        //FUNCION PARA CONTAR LOS NUMEROS DE LOTES
        private int contarNumeroLote(int iOperadorTarjeta_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_numero_lote" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado_lote = 'Abierta'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_pos_operador_tarjeta = " + iOperadorTarjeta_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA ACTUALIZAR EL MOVIMIENTO DE CAJA
        private bool actualizarMovimientosCaja()
        {
            try
            {
                //SELECCIONAR EL ID DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select id_caja" + Environment.NewLine;
                sSql += "from cv405_cajas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and cg_tipo_caja = 8906";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_caja"].ToString());

                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iNumeroMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                sSql = "";
                sSql += "select id_factura" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iIdFactura = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                sSql = "";
                sSql += "select id_persona, numero_pedido, establecimiento," + Environment.NewLine;
                sSql += "punto_emision, numero_factura, idtipocomprobante" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                sFacturaRecuperada = dtConsulta.Rows[0][2].ToString() + "-" + dtConsulta.Rows[0][3].ToString() + "-" + dtConsulta.Rows[0][4].ToString().PadLeft(9, '0');
                iIdTipoComprobante = Convert.ToInt32(dtConsulta.Rows[0][5].ToString());

                sSql = "";
                sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "isnull(valor_recibido, valor) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago " + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtAuxiliar.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No existe formas de pagos realizados. Couníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return false;
                }

                if (iIdTipoComprobante == 1)
                {
                    sFacturaRecuperada = "FACT. No. " + sFacturaRecuperada;
                }

                else
                {
                    sFacturaRecuperada = "N. ENTREGA. No. " + sFacturaRecuperada;
                }

                for (int index = 0; index < dtAuxiliar.Rows.Count; ++index)
                {
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                    sSql += "tipo_movimiento, idempresa, id_localidad, " + Environment.NewLine;
                    sSql += "id_persona, id_cliente, id_caja, id_pos_cargo," + Environment.NewLine;
                    sSql += "fecha, hora, cg_moneda, valor, concepto, " + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                    sSql += Program.iIdPersonaMovimiento + ", " + iIdPersona + ", " + iIdCaja + ", 1, " + Environment.NewLine;
                    sSql += "'" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtAuxiliar.Rows[index][1].ToString()) + "," + Environment.NewLine;
                    sSql += "'COBRO No. CUENTA " + iNumeroPedido.ToString() + " (" + dtAuxiliar.Rows[index][0].ToString() + ")', '" + Environment.NewLine;
                    sSql += sFacturaRecuperada + "', " + Environment.NewLine;
                    sSql += Convert.ToInt32(dtAuxiliar.Rows[index][5].ToString()) + ", " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A', " + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sTabla = "pos_movimiento_caja";
                    sCampo = "id_pos_movimiento_caja";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        return false;
                    }

                    iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimiento + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iNumeroMovimiento++;
                }

                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = " + iNumeroMovimiento + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
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

        //FUNCION PARA EXTRAER EL NUMERO DE LOTE
        private void numeroLote()
        {
            try
            {
                sSql = "";
                sSql += "select lote" + Environment.NewLine;
                sSql += "from pos_numero_lote" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado_lote = 'Abierta'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;

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
                    sLoteRecuperado = "";
                    iBanderaInsertarLote = 1;
                }

                else
                {
                    sLoteRecuperado = dtConsulta.Rows[0]["lote"].ToString().Trim();
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

        private void frmCambiarFormasCobros_Load(object sender, EventArgs e)
        {
            obtenerTotal();
            cargarFormasPagos();
            llenarDetalleDeuda();
            llenarGrid();
        }

        private void btnGrabarPagos_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
                {
                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        cambiarFormasPagos();
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se ha realizado el cobro total de la orden.";
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnRemoverPago_Click(object sender, EventArgs e)
        {
            if (dgvPagos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay formas de pago ingresados para remover del registro";
                ok.ShowDialog();
            }

            else if (dgvPagos.SelectedRows.Count > 0)
            {
                if (Program.iPuedeCobrar == 1)
                {
                    dgvPagos.Rows.Remove(dgvPagos.CurrentRow);

                    dSubtotal = 0;

                    for (int i = 0; i < dgvPagos.Rows.Count; i++)
                    {
                        dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value.ToString());
                    }

                    dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
                    dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value)).ToString("N2");

                    if (dTotal > dSubtotal)        //AQUI REVISAR LA CONDICION
                    {
                        dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                    }

                    else
                    {
                        dgvDetalleDeuda.Rows[2].Cells[1].Value = (dSubtotal - dTotal).ToString("N2");
                    }

                    dgvPagos.ClearSelection();
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Su usuario no le permite remover el ítem. Póngase en contacto con el administrador.";
                    ok.ShowDialog();
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No se ha seleccionado una línea para remover.";
                ok.ShowDialog();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotonesFormasPagos();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaFormasPagos -= iCuentaAyudaFormasPagos;

            if (iCuentaFormasPagos <= 10)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaFormasPagos -= 10;

            crearBotonesFormasPagos();
        }

        private void frmCambiarFormasCobros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
