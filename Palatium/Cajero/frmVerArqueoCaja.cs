using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmVerArqueoCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Clases.ClaseCierreCajero arqueo = new Clases.ClaseCierreCajero();
        Clases.ClaseArqueoCaja2 arqueo2 = new Clases.ClaseArqueoCaja2();
        Clases.ClaseReporteVendido reporte = new Clases.ClaseReporteVendido();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        bool bRespuesta;
        bool bRespuestaEnvioMail;

        DataTable dtConsulta;
        string sFecha;
        double dTotal;
        double dSumaTarjetas = 0, dSumaCheques = 0, dSumaEfectivo = 0, dSumaTransferencias = 0;

        string sSql;
        string sFechaCierre;
        string sHoraCierre;
        string sCorreoEmisor;
        string sCorreoCopia1;
        string sCorreoCopia2;
        string sPalabraClave;
        string sSmtp;
        string sPuerto;
        string sManejaSSL;
        string sNombreComercial;
        string sRazonSocial;
        string sMensajeEnviar;
        string sFacturaActual;
        string sAsuntoMail;
        string sMensajeRetorno;
        string sTextoDesglose;

        //int iPuedeGuardar;
        int iOp;
        //int iJornada;
        int iIdCierreCajero;
        int iIdLocalidad;

        int iMoneda01, iMoneda05, iMoneda10, iMoneda25, iMoneda50;
        int iBillete1, iBillete2, iBillete5, iBillete10, iBillete20, iBillete50, iBillete100;

        double dTotalPagadoCortesiaP;
        double dTotalProductosCortesiaP;

        Decimal dbCajaInicial;
        Decimal dbCajaFinal;

        public frmVerArqueoCaja(int iIdCierreCaja_P)
        {
            this.iIdCierreCajero = iIdCierreCaja_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO
        
        //FUNCION PARA CONSULTAR LAS PROPINAS RECAUDADAS
        private void consultarPropinas()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(sum(isnull(propina, 0)), 10, 2)) propinas" + Environment.NewLine;
                sSql += "from pos_vw_propinas_recaudadas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                txtPropinas.Text = dtConsulta.Rows[0][0].ToString();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL ESTADO DE LA CAJA A ABRIR
        private void consultarEstadoCaja()
        {
            try
            {
                sSql = "";
                sSql += "select estado_cierre_cajero, isnull(ahorro_emergencia, '0.00') ahorro_emergencia," + Environment.NewLine;
                sSql += "isnull(caja_inicial, 0) caja_inicial, id_pos_cierre_cajero, id_localidad, fecha_apertura" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtAhorroManual.Text = Convert.ToDecimal(dtConsulta.Rows[0]["ahorro_emergencia"].ToString()).ToString("N2");
                        txtCajaInicial.Text = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString()).ToString("N2");
                        dbCajaInicial = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString());
                        iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                        sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("dd-MM-yyyy");
                        lblFechaCaja.Text = sFecha;
                    }

                    else
                    {
                        txtAhorroManual.Text = "0.00";
                        txtCajaInicial.Text = "0.00";
                        dbCajaInicial = 0;
                        iIdLocalidad = 0;
                        sFecha = DateTime.Now.ToString("dd-MM-yyyy");
                        lblFechaCaja.Text = sFecha;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void extraerOtrosValoresCortesia(string sCodigo)
        {
            try
            {
                sTextoDesglose = "";
                dTotalPagadoCortesiaP = 0;

                sSql = "";
                sSql += "select isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_otro + DP.valor_iva - DP.valor_dscto)), 0) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "pos_origen_orden OO" + Environment.NewLine;
                sSql += "where OO.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and OO.codigo = '" + sCodigo + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotalPagadoCortesiaP = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        dTotalPagadoCortesiaP = 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el grid de cheques y tarjetas
        private void cargarGridChequesTarjetas()
        {
            try
            {
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");
                dSumaCheques = 0;
                dSumaEfectivo = 0;
                dSumaTarjetas = 0;
                dSumaTransferencias = 0;

                dgvCheques.Rows.Clear();
                dgvCheques.Refresh();

                dgvTarjetas.Rows.Clear();
                dgvTarjetas.Refresh();

                sSql = "";
                sSql += "select NP.numero_pedido, FP.descripcion, FP.valor, FP.codigo " + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP " + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "order by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i]["codigo"].ToString() == "CH")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaCheques = dSumaCheques + dTotal;
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                dgvCheques.Rows.Add(false, dtConsulta.Rows[i]["numero_pedido"].ToString() + " - CHEQUE", dTotal.ToString("N2"));
                            }

                            else if (dtConsulta.Rows[i]["codigo"].ToString() == "EF")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaEfectivo = dSumaEfectivo + dTotal;
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][1].ToString(), dTotal.ToString("N2"));
                            }

                            else if ((dtConsulta.Rows[i]["codigo"].ToString() == "TC") || (dtConsulta.Rows[i]["codigo"].ToString() == "TD"))
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaTarjetas = dSumaTarjetas + dTotal;
                                //dgvTarjetas.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                dgvTarjetas.Rows.Add(false, dtConsulta.Rows[i]["descripcion"].ToString(), dTotal.ToString("N2"));
                            }

                            else if (dtConsulta.Rows[i]["codigo"].ToString() == "TR")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaTransferencias = dSumaTransferencias + dTotal;
                            }
                        }

                        dgvCheques.ClearSelection();
                        dgvTarjetas.ClearSelection();

                        txtCobradoEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtTotalEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtCobradoTarjetas.Text = dSumaTarjetas.ToString("N2");
                        txtCobradoCheques.Text = dSumaCheques.ToString("N2");
                        txtCobradoTransferencia.Text = dSumaTransferencias.ToString("N2");
                    }

                    else
                    {
                        dgvCheques.Rows.Clear();
                        dgvCheques.Refresh();

                        dgvTarjetas.Rows.Clear();
                        dgvTarjetas.Refresh();

                        txtCobradoEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtTotalEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtCobradoTarjetas.Text = dSumaTarjetas.ToString("N2");
                        txtCobradoCheques.Text = dSumaCheques.ToString("N2");
                        txtCobradoTransferencia.Text = dSumaTransferencias.ToString("N2");
                    }

                    txtTotalVendido.Text = (dSumaCheques + dSumaTarjetas + dSumaEfectivo + dSumaTransferencias).ToString("N2");
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LAS ENTRADAS Y SALIDAS 
        private void sumaEntradasSalidas(int op)
        {
            try
            {
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select ltrim(str(isnull(sum(valor),0), 10, 2)) suma" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where tipo_movimiento = " + op + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and id_documento_pago is null" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotal = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());

                        if (op == 1)
                        {
                            txtEntradasManuales.Text = dTotal.ToString("N2");
                        }

                        else
                        {
                            txtSalidasManuales.Text = dTotal.ToString("N2");
                        }
                    }

                    else
                    {
                        if (op == 1)
                        {
                            txtEntradasManuales.Text = "0.00";
                        }

                        else
                        {
                            txtSalidasManuales.Text = "0.00";
                        }
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER EL VALOR TOTAL DE CORTESIAS
        private void sumarProductosCortesia()
        {
            try
            {
                sSql = "";
                sSql += "select NP.nombre, PC.motivo_cortesia," + Environment.NewLine;
                sSql += "ltrim(str(DP.precio_unitario, 10, 2)) precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, O.descripcion" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN " + Environment.NewLine;
                sSql += "pos_cortesia PC ON DP.id_det_pedido = PC.id_det_pedido" + Environment.NewLine;
                sSql += "and PC.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_Cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    double suma = 0;
                    if (dtConsulta.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * (1 + Program.iva + Program.recargo);
                            suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
                        }

                        dTotalProductosCortesiaP = suma;
                    }

                    else
                    {
                        dTotalProductosCortesiaP = 0;
                    }


                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el total de personas que ocupan las mesas
        private void calcularTotalPersonas(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(CP.numero_personas),0) numero " + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_origen_orden ORI " + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido " + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden " + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtTotalPersonas.Text = dtConsulta.Rows[0][0].ToString();
                    }
                    else
                    {
                        txtTotalPersonas.Text = "0";
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el número de órdenes (Mesa, Llevar, Domicilio, Consumo, etc)
        //private int calcularNumeroOrdenes(int iIdPosOrigenOrden)
        private int calcularNumeroOrdenes(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and O.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //Función para calcular el total de descuentos
        private void calcularDescuentos()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(DP.cantidad,0), 10, 2)) cantidad, ltrim(str(isnull(DP.valor_dscto,0), 10, 2)) valor_dscto" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                sSql += "where CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and DP.valor_dscto <> 0";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotal = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dTotal = dTotal + (Convert.ToDouble(dtConsulta.Rows[i][0].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][1].ToString()));
                        }

                        txtTotalDescuentos.Text = dTotal.ToString("N2");
                    }
                    else
                    {
                        txtTotalDescuentos.Text = "0.00";
                    }
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el total de orden (Mesa, Llevar, Domicilio, Consumo, etc)
        private double calcularTotalOrigenOrden(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select ORI.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_origen_orden ORI, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "group by ORI.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0][1].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA EXTRAER EL IVA COBRADO
        private void extraerIva()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(ltrim(str(sum(DP.cantidad * DP.valor_iva), 10, 2)), 0), 10, 2)) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_facturas_pedidos FP ON CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_facturas F ON F.id_factura = FP.id_factura" + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "where O.genera_factura = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtImpuestoIVA.Text = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "No se pudo extraer el total del IVA cobrado." + Environment.NewLine + "Comuníquese con el administrador del sistema.";
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER LAA CUENTAS POR COBRAR
        private void cargarCuentasPorCobrar()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 10, 2)), 0.00) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_dctos_por_cobrar XC ON CP.id_pedido = XC.id_pedido" + Environment.NewLine;
                sSql += "and XC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where XC.cg_estado_dcto = 7460" + Environment.NewLine;
                sSql += "and O.cuenta_por_cobrar = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtCuentasPorCobrar.Text = dtConsulta.Rows[0][0].ToString();
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LAS TARJETAS DE ALMUERZOS
        private void cargarCuentasTarjetaAlmuerzos()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where O.cuenta_por_cobrar = 0" + Environment.NewLine;
                sSql += "and O.pago_anticipado = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtTarjetasAlmuerzos.Text = dtConsulta.Rows[0][0].ToString().Trim();
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL VALOR COBRADO EN PRODUCTOS DE EMERGENCIA
        private void consultaProductosAhorro()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 0), 10, 2)) suma_ahorro" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and P.ahorro_emergencia = 1" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtAhorroProductos.Text = dtConsulta.Rows[0][0].ToString();
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULO
        private void recalcularValores()
        {
            try
            {
                Decimal dbEfectivoCobrado_P;
                Decimal dbAhorroProducto_P;
                Decimal dbAhorroManual_P;
                Decimal dbEntradasManuales_P;
                Decimal dbSalidasManuales_P;
                //Decimal dbIvaCobrado_P;
                Decimal dbTotal_P;

                dbEfectivoCobrado_P = Convert.ToDecimal(txtTotalEfectivo.Text.Trim());
                dbAhorroProducto_P = Convert.ToDecimal(txtAhorroProductos.Text.Trim());
                dbAhorroManual_P = Convert.ToDecimal(txtAhorroManual.Text.Trim());
                dbEntradasManuales_P = Convert.ToDecimal(txtEntradasManuales.Text.Trim());
                dbSalidasManuales_P = Convert.ToDecimal(txtSalidasManuales.Text.Trim());
                //dbIvaCobrado_P = Convert.ToDecimal(txtImpuestoIVA.Text.Trim());

                //dbTotal_P = dbEfectivoCobrado_P - dbAhorroProducto_P - dbAhorroManual_P + dbEntradasManuales_P - dbSalidasManuales_P - dbIvaCobrado_P;
                dbTotal_P = dbEfectivoCobrado_P - dbAhorroProducto_P - dbAhorroManual_P + dbEntradasManuales_P - dbSalidasManuales_P + dbCajaInicial;

                txtTotalCaja.Text = dbTotal_P.ToString("N2");
                txtTotalCaja.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS
        private void cargarValores()
        {
            dSumaTarjetas = 0;
            dSumaCheques = 0;
            dSumaEfectivo = 0;
            dSumaTransferencias = 0;
            cargarGridChequesTarjetas();
            sumaEntradasSalidas(1);
            sumaEntradasSalidas(0);
            sumarProductosCortesia();
            extraerOtrosValoresCortesia("04");
            calcularTotalPersonas("01");
            calcularDescuentos();
            txtParaMesa.Text = calcularNumeroOrdenes("01").ToString();
            txtParaLlevar.Text = calcularNumeroOrdenes("02").ToString();
            txtParaDomicilio.Text = calcularNumeroOrdenes("03").ToString();
            txtVentaExpress.Text = calcularNumeroOrdenes("10").ToString();

            txtTotalParaMesa.Text = calcularTotalOrigenOrden("01").ToString("N2");
            txtTotalParaLlevar.Text = calcularTotalOrigenOrden("02").ToString("N2");
            txtTotalParaDomicilio.Text = calcularTotalOrigenOrden("03").ToString("N2");
            txtTotalVentaExpress.Text = calcularTotalOrigenOrden("10").ToString("N2");

            txtTotalCortesias.Text = dTotalPagadoCortesiaP.ToString("N2");
            consultaProductosAhorro();
            extraerIva();
            cargarCuentasPorCobrar();
            cargarCuentasTarjetaAlmuerzos();
            recalcularValores();
            consultarPropinas();
        }

        #endregion

        private void frmVerArqueoCaja_Load(object sender, EventArgs e)
        {
            consultarEstadoCaja();
            cargarValores();
        }

        private void frmVerArqueoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDetallarVentas_Click(object sender, EventArgs e)
        {
            Cajero.frmDetalleVentas detalle = new frmDetalleVentas(iIdLocalidad, iIdCierreCajero);
            detalle.ShowDialog();
        }

        private void btnEntradas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Oficina.frmEntradasSalidasManuales movimiento = new Oficina.frmEntradasSalidasManuales(1, sFecha, iIdCierreCajero);
            movimiento.ShowDialog();
        }

        private void btnSalidas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Oficina.frmEntradasSalidasManuales movimiento = new Oficina.frmEntradasSalidasManuales(0, sFecha, iIdCierreCajero);
            movimiento.ShowDialog();
        }
    }
}
