using System;
using System.Data;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmVerReporteRevisar : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        Clases.ClasePrecuentaTextBox precuenta = new Clases.ClasePrecuentaTextBox();
        Clases.ClasePrecuentaTextbox2 precuenta2 = new Clases.ClasePrecuentaTextbox2();
        Clases.ClaseImprimirFacturaNormal factura = new Clases.ClaseImprimirFacturaNormal();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases.ClaseNotaVenta notaVenta = new Clases.ClaseNotaVenta();
        Clases_Factura_Electronica.ClaseImprimirFacturaElectronica facturaElectronica = new Clases_Factura_Electronica.ClaseImprimirFacturaElectronica();

        DataTable dtConsulta;
        DataTable dtEmpresa;
        DataTable dtEstado;
        DataTable dt;
        DataTable dtPago;
        DataTable dtImprimir;

        string sSql;
        string sIdOrden;
        string sEstado;
        string sSecuencial;
        string sFechaApertura;
        string sFechaCierre;
        string sFechaOrden;
        string sFechaCierreOrden;
        string sTexto;
        string sRetorno;
        string sDescripcionOrigen;
        string sNombreMesero;
        string sCodigoOrigenOrden;

        bool bRespuesta;

        Double dbTotal;
        Double dCambio;

        int iBandera;
        int iJornadaOrdenRegistrada;
        int iNumeroPersonas;
        int iIdMesa;
        int iIdPersona;
        int iIdOrigenOrden;
        int iIdCajero;
        int iIdMesero;
        int iIdTipoFactura;
        int iIdFactura;
        int iIdTipoComprobante;

        //=========================================================================================


        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmVerReporteRevisar(string sIdOrden)
        {
            this.sIdOrden = sIdOrden;
            InitializeComponent();
        }       

        #region FUNCIONES PARA MOSTRAR LA PRECUENTA Y FACTURA EN UN TEXTBOX

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden(int iOp, int iTipoComprobante)
        {
            try
            {
                sSql = "";
                

                if (iTipoComprobante == 2)
                {
                    sSql = sSql + "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                    sSql = sSql + "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                    sSql = sSql + "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                    sSql = sSql + "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                    sSql = sSql + "and FF.estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "and FF.id_pos_formato_factura = " + iOp;
                }

                else
                {
                    sSql = sSql + "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                    sSql = sSql + "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                    sSql = sSql + "from pos_impresora I, pos_formato_precuenta FP" + Environment.NewLine;
                    sSql = sSql + "where FP.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                    sSql = sSql + "and FP.estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "and FP.id_pos_formato_precuenta = " + iOp;
                }

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt32(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();

                        //PRECUENTA
                        if (iTipoComprobante == 1)
                        {
                            imprimir.iniciarImpresion();
                            imprimir.escritoEspaciadoCorto(precuenta2.llenarPrecuentaDatos(dtConsulta, sIdOrden, sEstado, dtPago));
                            imprimir.escritoFuenteAlta("TOTAL:" + precuenta2.dbTotalOrden.ToString("N2").PadLeft(27, ' ') + Environment.NewLine);
                            imprimir.escritoEspaciadoCorto(precuenta2.llenarDetallePrecuenta(dtConsulta, sIdOrden, sEstado, dtPago));
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                            sRetorno = "";
                        }

                        //FACTURA
                        //else if (iIdTipoComprobante == 2)
                        else if (iTipoComprobante == 2)
                        {
                            if (iIdTipoFactura == 1)
                            {
                                Program.iCortar = 0;
                                imprimir.iniciarImpresion();
                                imprimir.AbreCajon();
                                imprimir.imprimirReporte(sNombreImpresora);
                                imprimir.iniciarImpresion();
                                imprimir.escritoEspaciadoCorto(facturaElectronica.sCrearFactura(iIdFactura));
                                imprimir.cortarPapel();
                                imprimir.imprimirReporte(sNombreImpresora);
                            }

                            else
                            {
                                Program.iCortar = 1;
                                imprimir.iniciarImpresion();
                                imprimir.AbreCajon();
                                imprimir.imprimirReporte(sNombreImpresora);
                                imprimir.iniciarImpresion();
                                imprimir.escritoEspaciadoCorto(factura.sCrearFactura(iIdFactura));
                                imprimir.escritoFuenteAlta("".PadLeft(12, ' ') + "TOTAL:" + factura.dTotal.ToString("N2").PadLeft(15, ' '));
                                imprimir.cortarPapel();
                                imprimir.imprimirReporte(sNombreImpresora);
                            }
                        }

                        //else if (iIdTipoComprobante == 3)
                        else if (iTipoComprobante == 3)
                        {
                            imprimir.iniciarImpresion();
                            imprimir.escritoEspaciadoCorto(notaVenta.llenarNota(iIdFactura));
                            imprimir.escritoFuenteAlta("TOTAL:" + notaVenta.dbTotal.ToString("N2").PadLeft(27, ' ') + Environment.NewLine);
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                        }

                        sRetorno = "";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LOS DATOS DE LA EMPRESA
        private bool consultarDatosEmpresa()
        {
            try
            {
                txtReporte.Clear();

                sTexto = "";
                sTexto = sTexto + Program.local + Environment.NewLine;
                sTexto = sTexto + Program.direccion + Environment.NewLine;
                sTexto = sTexto + Program.telefono1;

                if (Program.telefono2 != "")
                {
                    sTexto = sTexto + " - " + Program.telefono2 + Environment.NewLine;
                }

                else
                {
                    sTexto = Environment.NewLine;
                }

                return true;
            }

            catch (Exception)
            {
                goto reversa;
            }

            reversa: { return false; }
        }

        //FUNCION PARA CARGAR LA PRECUENTA EN UN TEXTBOX
        private void verPrecuentaTextBox()
        {
            try
            {
                if (consultarDatosEmpresa() == true)
                {
                    if (llenarDataTable(0) == true)
                    {
                        if (Program.iFormatoPrecuenta == 1)
                        {
                            sRetorno = precuenta.llenarPrecuenta(dtConsulta, sIdOrden, sEstado, dtPago);
                        }

                        else
                        {
                            sRetorno = precuenta2.llenarPrecuenta(dtConsulta, sIdOrden, sEstado, dtPago);
                            dbTotal = precuenta2.dbTotal;
                        }

                        if (sRetorno == "")
                        {
                            goto reversa;
                        }
                        else
                        {
                            sTexto = sTexto + sRetorno;
                        }

                        txtReporte.Text = sTexto;
                        sTexto = "";
                        goto fin;
                    }

                    else
                    {
                        goto reversa;
                    }
                    
                }

                else
                {
                    goto reversa;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }
        reversa:
            {
                //ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                //ok.ShowInTaskbar = false;
                //ok.ShowDialog();
            }

        fin: { }
        }

        //FUNCION PARA OBENTER EL ID TIPO FACTURA, ID FACTURA Y TIPO COMPROBANTE
        private void consultarTipoFactura()
        {
            try
            {
                sSql = "";
                sSql += "select F.facturaelectronica, FP.id_factura, F.idtipocomprobante" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos FP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_facturas F ON F.id_factura = FP.id_factura" + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "where FP.id_pedido = " + sIdOrden + Environment.NewLine;
                //sSql += "and F.id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdTipoFactura = Convert.ToInt32(dtConsulta.Rows[0]["facturaelectronica"].ToString());
                    iIdFactura = Convert.ToInt32(dtConsulta.Rows[0]["id_factura"].ToString());
                    iIdTipoComprobante = Convert.ToInt32(dtConsulta.Rows[0]["idtipocomprobante"].ToString());
                }

                else
                {
                    iIdTipoFactura = -1;
                }
            }

            catch (Exception ex)
            {
                iIdTipoFactura = -1;
            }
        }

        //FUNCION PARA CARGAR LA FACTURA EN UN TEXTBOX
        private void verFacturaTextBox()
        {
            try
            {
                consultarTipoFactura();

                if (iIdTipoFactura == -1)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al visualizar la factura";
                    ok.ShowDialog();
                }

                else
                {
                    if (iIdTipoFactura == 0)
                    {
                        if (iIdTipoComprobante == 1)
                        {
                            sRetorno = factura.sCrearFactura(iIdFactura);
                            sRetorno += Environment.NewLine + "TOTAL:".PadLeft(28, ' ') + factura.dTotal.ToString("N2").PadLeft(12, ' ') + Environment.NewLine + Environment.NewLine;
                            sRetorno += "PROPINA: " + factura.dbPropina.ToString("N2");
                        }
                        else
                        {
                            sRetorno = notaVenta.llenarNota(iIdFactura);
                            sRetorno += "".PadLeft(22, ' ') + "TOTAL:" + notaVenta.dbTotal.ToString("N2").PadLeft(12, ' ');
                        }
                    }

                    else if (iIdTipoFactura == 1)
                    {
                        if (iIdTipoComprobante == 1)
                        {
                            sRetorno = facturaElectronica.sCrearFactura(iIdFactura);
                        }
                        else
                        {
                            sRetorno = notaVenta.llenarNota(iIdFactura);
                            sRetorno += "".PadLeft(22, ' ') + "TOTAL:" + notaVenta.dbTotal.ToString("N2").PadLeft(12, ' ');
                        }
                    }

                    if (sRetorno == "")
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al generar la vista previa de la factura.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        sTexto += Environment.NewLine;
                        sTexto += sRetorno;
                    }

                    txtReporte.Text = sTexto;
                    sTexto = "";
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
                
        //FUNCION PARA LLENAR LOS DATATABLES
        private bool llenarDataTable(int op)
        {
            try
            {
                //OPCION CERO   : PARA PRECUENTA
                //OPCION UNO    : PARA FACTURA

                if (op == 0)
                {
                    sSql = "";
                    sSql = sSql + "select * from pos_vw_det_pedido" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql = sSql + "and estado in ('A', 'N')" + Environment.NewLine;
                    sSql = sSql + "order by id_det_pedido";
                }

                else
                {
                    sSql = "";
                    sSql = sSql + "select * from pos_vw_factura" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql = sSql + "order by id_det_pedido"; 
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sSql = "";
                        sSql = sSql + "select descripcion, sum(valor) valor, cambio,  count(*) cuenta," + Environment.NewLine;
                        //sSql = sSql + "isnull(valor_recibido, valor) valor_recibido" + Environment.NewLine;
                        sSql = sSql + "sum(isnull(valor_recibido, valor)) valor_recibido" + Environment.NewLine;
                        sSql = sSql + "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                        sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                        sSql = sSql + "group by descripcion, valor, cambio, valor_recibido" + Environment.NewLine;
                        sSql = sSql + "having count(*) >= 1";

                        //sSql = "";
                        //sSql = sSql + "select descripcion, sum(valor) valor, cambio,  count(*) cuenta," + Environment.NewLine;
                        //sSql = sSql + "sum(isnull(valor_recibido, valor)) valor_recibido" + Environment.NewLine;
                        //sSql = sSql + "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                        //sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                        //sSql = sSql + "group by descripcion, valor, cambio, valor_recibido" + Environment.NewLine;
                        //sSql = sSql + "having count(*) >= 1";
                        
                        dtPago = new DataTable();
                        dtPago.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPago, sSql);

                        if (bRespuesta == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }                        
                    }
                    else
                        return false;
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void frmVerReporteRevisar_Load(object sender, EventArgs e)
        {
            try
            {
                dbTotal = 0;
                
                //sSql = "";
                //sSql += "select CP.estado_orden, CP.id_pos_origen_orden, CP.porcentaje_dscto," + Environment.NewLine;
                //sSql += "CP.id_pos_modo_delivery, CP.id_persona, CP.fecha_orden," + Environment.NewLine;
                //sSql += "CP.fecha_cierre_orden, CP.id_pos_jornada, OO.descripcion," + Environment.NewLine;
                //sSql += conexion.GFun_St_esnulo() + "(CP.numero_personas, 0) numero_personas," + Environment.NewLine;
                //sSql += conexion.GFun_St_esnulo() + "(CP.id_pos_mesa, 0) id_pos_mesa, CP.id_pos_cajero," + Environment.NewLine;
                //sSql += "CP.id_pos_mesero, MS.descripcion, " + conexion.GFun_St_esnulo() + "(PM.descripcion, '') descripcion_mesa" + Environment.NewLine;
                //sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                //sSql += "pos_origen_orden OO ON CP.id_pos_origen_orden = OO.id_pos_origen_orden" + Environment.NewLine;
                //sSql += "and CP.estado in ('A', 'N')" + Environment.NewLine;
                //sSql += "and OO.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                //sSql += "pos_mesa PM ON PM.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                //sSql += "and PM.estado = 'A' INNER JOIN" + Environment.NewLine;
                //sSql += "pos_mesero MS ON MS.id_pos_mesero = CP.id_pos_mesero" + Environment.NewLine;
                //sSql += "and MS.estado = 'A'" + Environment.NewLine;
                //sSql += "where CP.id_pedido = " + Convert.ToInt32(sIdOrden);

                sSql = "";
                sSql += "select * from pos_vw_encabezado_reporte_revisar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden;

                dtEstado = new DataTable();
                dtEstado.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtEstado, sSql);

                if (bRespuesta == true)
                {
                    if (dtEstado.Rows.Count > 0)
                    {
                        sEstado = dtEstado.Rows[0]["estado_orden"].ToString();
                        sFechaOrden = (Convert.ToDateTime(dtEstado.Rows[0]["fecha_pedido"].ToString())).ToString("yyyy/MM/dd");
                        Program.dbValorPorcentaje = Convert.ToDouble(dtEstado.Rows[0]["porcentaje_dscto"].ToString());

                        iIdOrigenOrden = Convert.ToInt32(dtEstado.Rows[0]["id_pos_origen_orden"].ToString());
                        iIdPersona = Convert.ToInt32(dtEstado.Rows[0]["id_persona"].ToString());
                        sDescripcionOrigen = dtEstado.Rows[0]["tipo_comanda"].ToString();
                        iNumeroPersonas = Convert.ToInt32(dtEstado.Rows[0]["numero_personas"].ToString());
                        iIdMesa = Convert.ToInt32(dtEstado.Rows[0]["id_pos_mesa"].ToString());
                        iIdCajero = Convert.ToInt32(dtEstado.Rows[0]["id_pos_cajero"].ToString());
                        iIdMesero = Convert.ToInt32(dtEstado.Rows[0]["id_pos_mesero"].ToString());
                        sNombreMesero = dtEstado.Rows[0]["mesero"].ToString();
                        sCodigoOrigenOrden = dtEstado.Rows[0]["codigo_origen_orden"].ToString().Trim();
                        Program.sNombreMesa = dtEstado.Rows[0]["descripcion_mesa"].ToString();

                        if ((dtEstado.Rows[0]["fecha_cierre_orden"].ToString() == null) || (dtEstado.Rows[0]["fecha_cierre_orden"].ToString() == ""))
                        {
                            sFechaCierreOrden = "";
                        }
                        else
                        {
                            sFechaCierreOrden = (Convert.ToDateTime(dtEstado.Rows[0]["fecha_cierre_orden"].ToString())).ToString("yyyy/MM/dd HH:mm:ss");
                        }

                        iJornadaOrdenRegistrada = Convert.ToInt32(dtEstado.Rows[0]["id_pos_jornada"].ToString());

                        if ((dtEstado.Rows[0]["id_pos_modo_delivery"].ToString() == null) || (dtEstado.Rows[0]["id_pos_modo_delivery"].ToString()) == "")
                        {
                            Program.iDomicilioEspeciales = 0;
                        }
                        else
                        {
                            Program.iDomicilioEspeciales = 1;
                        }

                        if ((dtEstado.Rows[0]["estado_orden"].ToString() == "Pagada") || (dtEstado.Rows[0]["estado_orden"].ToString() == "Cerrada"))
                        {
                            btnEditar.Visible = false;
                            btnReabrir.Visible = true;
                            rbdVerFactura.Enabled = true;
                            verPrecuentaTextBox();
                        }

                        else if (dtEstado.Rows[0]["estado_orden"].ToString() == "Cancelada")
                        {
                            btnEditar.Visible = false;
                            btnReabrir.Visible = true;
                            rbdVerFactura.Enabled = false;
                            //abrirPrecuentaCancelada();
                            verPrecuentaTextBox();
                        }

                        else
                        {
                            btnEditar.Visible = true;
                            btnReabrir.Visible = false;
                            rbdVerFactura.Enabled = false;
                            //abrirPrecuentaAbierta();
                            verPrecuentaTextBox();
                        }

                        if (sCodigoOrigenOrden == "12")
                            rbdVerFactura.Enabled = false;

                        sSql = "";
                        sSql = sSql + "select descripcion, genera_factura, id_persona," + Environment.NewLine;
                        sSql = sSql + "id_pos_modo_delivery, presenta_opcion_delivery, codigo " + Environment.NewLine;
                        sSql = sSql + "from pos_origen_orden" + Environment.NewLine;
                        sSql = sSql + "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                        sSql = sSql + "and estado = 'A'";

                        dtEstado = new DataTable();
                        dtEstado.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtEstado, sSql);

                        if (bRespuesta == true)
                        {
                                Program.sDescripcionOrigenOrden = dtEstado.Rows[0][0].ToString();
                                Program.iGeneraFactura = Convert.ToInt32(dtEstado.Rows[0][1].ToString());

                            if ((dtEstado.Rows[0][2].ToString() == null) || (dtEstado.Rows[0][2].ToString() == ""))
                            {
                                Program.iIdPersonaOrigenOrden = 0;
                            }

                            else
                            {
                                Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtEstado.Rows[0][2].ToString());
                            }                            

                            //if ((Program.iGeneraFactura == 1) && (sEstado == "Pagada"))
                            //{
                            //    rbdVerFactura.Enabled = true;
                            //}

                            //else
                            //{
                            //    rbdVerFactura.Enabled = false;
                            //}

                            Program.iIdPosModoDelivery = Convert.ToInt32(dtEstado.Rows[0][3].ToString());
                            Program.iPresentaOpcionDelivery = Convert.ToInt32(dtEstado.Rows[0][4].ToString());
                            Program.sCodigoAsignadoOrigenOrden = dtEstado.Rows[0][5].ToString();
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSecuencial;
                            catchMensaje.Show();
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void rbdVerPrecuenta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdVerPrecuenta.Checked == true)
            {
                rbdVerFactura.Checked = false;
                verPrecuentaTextBox();
            }
        }

        private void rbdVerFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdVerFactura.Checked == true)
            {
                rbdVerPrecuenta.Checked = false;
                verFacturaTextBox();
            }
        }

        private void frmVerReporteRevisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnReabrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.iPuedeCobrar == 1)
                {
                    if ((sFechaOrden == Program.sFechaAperturaCajero) && (iJornadaOrdenRegistrada == Program.iJornadaCajero) && (Program.sEstadoCajero == "Abierta"))
                    {
                        if (sEstado.ToUpper() == "CANCELADA")
                        {
                            ok.LblMensaje.Text = "No se puede reabrir una comanda que ha sido cancelada o eliminada.";
                            ok.ShowDialog();
                        }

                        else
                        {
                            frmOpcionesReabrir r = new frmOpcionesReabrir(sIdOrden, dbTotal);
                            AddOwnedForm(r);
                            r.ShowDialog();

                            if (r.DialogResult == DialogResult.OK)
                            {
                                if (Program.iBanderaReabrir == 1)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    Program.iBanderaReabrir = 0;
                                }

                                else
                                {
                                    frmVerReporteRevisar_Load(sender, e);
                                }
                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ya se encuentra un cierre de caja registrado para esta orden.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Su usuario no le permite reabrir la cuenta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }      
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //ACTUALIZACION ELVIS COMANDA
            //=======================================================================================================================
            ComandaNueva.frmComanda o = new ComandaNueva.frmComanda(Convert.ToInt32(sIdOrden), "OK");
            o.ShowDialog();
            this.DialogResult = DialogResult.OK;
            //=======================================================================================================================

            //Orden o = new Orden(iIdOrigenOrden, sDescripcionOrigen, iNumeroPersonas, iIdMesa, Convert.ToInt32(sIdOrden), "OK", iIdPersona, iIdCajero, iIdMesero, sNombreMesero, 0, 0);
            //o.ShowDialog();
            //this.DialogResult = DialogResult.OK;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (rbdVerPrecuenta.Checked == true)
            {
                //PRECUENTA
                consultarImpresoraTipoOrden(Program.iFormatoPrecuenta, 1);
                rbdVerFactura.Checked = false;
            }

            else
            {
                if (iIdTipoComprobante == 1)
                {
                    SiNo.LblMensaje.Text = "¿Está seguro que desea reimprimir la factura?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        //consultarImpresoraTipoOrden(Program.iFormatoFactura, 2);
                        ReportesTextBox.frmVistaFactura factura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 0);
                        factura.ShowDialog();
                    }
                }

                else
                {
                    consultarImpresoraTipoOrden(Program.iFormatoPrecuenta, 3);
                }

                rbdVerPrecuenta.Checked = false;
            }
        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
