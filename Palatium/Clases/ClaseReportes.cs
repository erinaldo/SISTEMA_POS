using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReportes
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sOrigenOrden;
        string sNombreProducto;
        string sTotal;
        string sTexto;
        string sNombreEmpresa;
        string sValorEmpresa;
        string sNumeroOrden_P; 
        string sNumeroFactura_P;
        string sTextoDevuelta;
        string sTextoDesglose;
        string sCodigoCobro;
        string sNombreTarjeta_P;
        string sValorAhorroProductos;
        string sNumeroFacturaAnula;
        string sFechaHoraAnula;
        string sMotivoAnula;
        string sAyudaMotivoAnula;
        string sContarCaracteres;
        string sCodigoOrigenOrden;

        string sNumeroComanda;
        string sUsuarioAnula;

        DataTable dtConsulta;
        DataTable dtAyuda;
        DataTable dtPagosDesglose;

        bool bRespuesta;

        public int iIdPosCierreCajero;
        int iIdOrigenOrden;
        int iIdLocalidad;
        int iCuenta;
        int iContador;
        int iIdTipoVenta;
        int iCantidad;
        int iIdOrden_P;
        int iTipoComprobante;
        int iBandera;
        int iLongi;
        int iIdPosCierreCajeroParametro;

        object iCuentaRegistros;

        public Decimal dbAhorroEmergencia;
        public Decimal dbCajaInicial;
        public Decimal dbCajaFinal;
        Decimal dbTotal;
        Decimal dbCantidadProductos;
        Decimal dbTotalCuentasPorCobrar;
        Decimal dbValorEmpresa_P;
        Decimal dSumaPagos;
        Decimal dbTotalEntradasManuales;
        Decimal dbTotalSalidasManuales;
        Decimal dSumaTotalCuentasAnuladas;
        Decimal dSumaTotalProductosCancelados;
        Decimal dbSumarPorTipoProducto;
        Decimal dbSuma;
        Decimal dbTotalOrdenes;
        Decimal dbTotalTarjetas;
        Decimal dbTotalEfectivo;
        Decimal dbTotalCheques;
        Decimal dbAuxiliar;
        Decimal dTotalPagadoP;
        Decimal dEfectivoCheque_P;
        Decimal dTarjeta_P;
        Decimal dTotal;
        Decimal dTotalDescuentos;
        Decimal dbTotalCobradoEfectivo;
        Decimal dbTotalCobradoTransferencias;
        Decimal dbTotalCobradoCheques;
        Decimal dbTotalCobradoTarjetas;
        Decimal dbTotalEntregado = 0;
        Decimal dbTotalPendiente = 0;

        //FUNCION PARA CREAR EL ENCABEZADO
        public string encabezadoReporte(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajero = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select * from pos_vw_encabezado_reporte_cierre" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero_P + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "INFORME DEL CIERRE DEL CAJERO".PadLeft(34, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "FECHA    : " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("dd-MM-yyyy") + Environment.NewLine;
                sTexto += "DESDE    : " + dtConsulta.Rows[0]["hora_apertura"].ToString().Trim() + Environment.NewLine;

                if (dtConsulta.Rows[0]["hora_cierre"].ToString().Trim() == "SH")
                {
                    sTexto += "HASTA    : " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre"].ToString()).ToString("HH:mm:ss") + Environment.NewLine;
                }

                else
                {
                    sTexto += "HASTA    : " + dtConsulta.Rows[0]["hora_cierre"].ToString().Trim() + Environment.NewLine;
                }


                sTexto += "CAJERO   : " + dtConsulta.Rows[0]["cajero"].ToString().Trim() + Environment.NewLine;
                sTexto += "JORNADA  : " + dtConsulta.Rows[0]["jornada"].ToString().Trim() + Environment.NewLine;

                dbAhorroEmergencia = Convert.ToDecimal(dtConsulta.Rows[0]["ahorro_emergencia"].ToString());
                dbCajaInicial = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString());
                dbCajaFinal = Convert.ToDecimal(dtConsulta.Rows[0]["caja_final"].ToString());

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CREAR EL RESUMEN DEL SISTEMA
        public string resumenSistema(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "RESUMEN DEL SISTEMA".PadLeft(29, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdOrigenOrden = Convert.ToInt32(dtAyuda.Rows[i]["id_pos_origen_orden"].ToString());
                    sOrigenOrden = dtAyuda.Rows[i]["descripcion"].ToString().Trim().ToUpper();

                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    sSql += "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and id_localidad = " + iIdLocalidad;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    if (iCuenta > 0)
                    {
                        sSql = "";
                        sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto + DP.valor_iva + DP.valor_otro)), 0), 10, 2)) total" + Environment.NewLine;
                        sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                        sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                        sSql += "and CP.estado = 'A'" + Environment.NewLine;
                        sSql += "and DP.estado = 'A'" + Environment.NewLine;
                        sSql += "where CP.id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                        sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                        sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                        sSql += "and CP.id_localidad = " + iIdLocalidad;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == false)
                        {
                            return "ERROR";
                        }

                        sTotal = dtConsulta.Rows[0][0].ToString().Trim();

                        sTexto += sOrigenOrden.PadRight(25, ' ') + iCuenta.ToString("N0").PadLeft(5, ' ') + sTotal.PadLeft(10, ' ') + Environment.NewLine;
                    }
                }

                //AQUI SE MOSTRARAN LAS CUENTAS POR COBRAR DEL CLIENTE EMPRESARIAL
                //-------------------------------------------------------------------------------------------------------------------------
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                sSql = "";
                sSql += "select ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) empresa," + Environment.NewLine;
                sSql += "ltrim(str(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 10, 2)) valor" + Environment.NewLine;
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
                sSql += "and CP.id_pos_origen_orden = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by TP.nombres, TP.apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    dbTotalCuentasPorCobrar = 0;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "TOTALES POR CLIENTE EMPRESARIAL".PadLeft(36, ' ') + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        sNombreEmpresa = dtConsulta.Rows[i]["empresa"].ToString().Trim();
                        sValorEmpresa = dtConsulta.Rows[i]["valor"].ToString().Trim();
                        dbValorEmpresa_P = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());

                        if (sNombreEmpresa.Length > 32)
                        {
                            sNombreEmpresa = sNombreEmpresa.Substring(0, 32);
                        }

                        sTexto += sNombreEmpresa.PadRight(32, ' ') + sValorEmpresa.PadLeft(8, ' ') + Environment.NewLine;
                        dbTotalCuentasPorCobrar += dbValorEmpresa_P;
                    }

                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "TOTAL CLIENTE EMPRESARIAL" + dbTotalCuentasPorCobrar.ToString("N2").PadLeft(15, ' ') + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                }
                //-------------------------------------------------------------------------------------------------------------------------

                //AQUI SE MOSTRARÁ LOS PAGOS PAGOS
                //-------------------------------------------------------------------------------------------------------------------------

                sSql = "";
                sSql += "select id_pos_tipo_venta, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_venta" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_tipo_venta";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                dSumaPagos = 0;
                iContador = 0;

                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(sum(FP.valor),10,2)) valor," + Environment.NewLine;
                sSql += "FP.cg_estado_dcto, FP.cg_tipo_documento, FP.id_pos_tipo_venta," + Environment.NewLine;
                sSql += "FP.descripcion_venta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by FP.descripcion, FP.cg_estado_dcto," + Environment.NewLine;
                sSql += "FP.cg_tipo_documento, FP.id_pos_tipo_venta," + Environment.NewLine;
                sSql += "FP.descripcion_venta" + Environment.NewLine;
                sSql += "order by FP.cg_tipo_documento";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdTipoVenta = Convert.ToInt32(dtAyuda.Rows[i]["id_pos_tipo_venta"]);

                    //CONTAR REGISTROS DEL DATATABLE
                    iCuentaRegistros = dtConsulta.Compute("Count(id_pos_tipo_venta)", "id_pos_tipo_venta = " + iIdTipoVenta);

                    if (Convert.ToInt32(iCuentaRegistros) == 0)
                    {
                        goto continuar;
                    }

                    sTexto += dtAyuda.Rows[i]["descripcion"] + Environment.NewLine + Environment.NewLine;

                    for (int j = 0; j < dtConsulta.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dtConsulta.Rows[j]["id_pos_tipo_venta"]) == iIdTipoVenta)
                        {
                            sTexto += (dtConsulta.Rows[j]["descripcion"] + ":").PadRight(30, ' ') +
                                       dtConsulta.Rows[j]["valor"].ToString().PadLeft(10, ' ') + Environment.NewLine;

                            dSumaPagos = dSumaPagos + Convert.ToDecimal(dtConsulta.Rows[j]["valor"].ToString());
                            iContador++;
                        }
                    }

                    if (iContador != 0)
                    {
                        sTexto += Environment.NewLine;
                        sTexto += ("TOTAL " + dtAyuda.Rows[i]["descripcion"].ToString()).PadRight(30, ' ') + dSumaPagos.ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        dSumaPagos = 0;
                    }

                    iContador = 0;

                continuar: { }
                }

                //-------------------------------------------------------------------------------------------------------------------------

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CREAR EL RESUMEN DE PAGOS PRIORITARIOS
        public string pagosPrioritarios(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "RESUMEN DE PAGOS PRIORITARIOS" + Environment.NewLine + Environment.NewLine;
                sTexto += ("TOTAL EFECTIVO:".PadRight(26, ' ') + calcularTotalPago("EF").ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                sTexto += ("TOTAL TRANSFERENCIAS:".PadRight(26, ' ') + calcularTotalPago("TR").ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;

                sTexto += ("TOTAL CHEQUES:".PadRight(26, ' ') + calcularTotalPago("CH").ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                sTexto += ("TOTAL TARJETAS:".PadRight(26, ' ') + calcularTotalPagoTarjetas().ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;

                sSql = "";
                sSql += "select NP.nombre, PC.motivo_cortesia," + Environment.NewLine;
                sSql += "ltrim(str(DP.precio_unitario, 10, 2)) precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, O.descripcion" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto and NP.estado = 'A' INNER JOIN " + Environment.NewLine;
                sSql += "pos_cortesia PC ON (DP.id_det_pedido = PC.id_det_pedido and PC.estado='A')" + Environment.NewLine;
                sSql += "where CP.id_pos_origen_orden = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                Decimal total = 0;

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += ("PRODUCTOS DE CORTESIA SIN IMPUESTOS:") + Environment.NewLine;
                        sTexto += Environment.NewLine;
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {

                            total = total + (Convert.ToDecimal(dtConsulta.Rows[i][2].ToString()) * Convert.ToDecimal(dtConsulta.Rows[i][3].ToString()));
                            double iTotalCortesia = Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][3].ToString());
                            string sTotalCortesia = iTotalCortesia.ToString("N2");
                            //dtCortesia.Rows.Add(drCortesia);

                            if (dtConsulta.Rows[i][0].ToString().Length < 25)
                            {
                                sTexto += (dtConsulta.Rows[i][3].ToString().PadLeft(5, ' ') + "".PadLeft(1, ' ') + dtConsulta.Rows[i][0].ToString().PadRight(25, ' ') + sTotalCortesia.PadLeft(9, ' ')) + Environment.NewLine;
                                sTexto += ("MOTIVO:".PadRight(8, ' ') + dtConsulta.Rows[i][1].ToString()) + Environment.NewLine;
                                sTexto += Environment.NewLine;
                            }
                            else
                            {
                                sTexto += (dtConsulta.Rows[i][3].ToString().PadLeft(5, ' ') + "".PadLeft(1, ' ') + dtConsulta.Rows[i][0].ToString().Substring(0, 25).PadRight(25, ' ') + sTotalCortesia.PadLeft(9, ' ')) + Environment.NewLine;
                                sTexto += (dtConsulta.Rows[i][0].ToString().Substring(25).PadLeft((5 + dtConsulta.Rows[i][0].ToString().Substring(25).Length), ' ') + Environment.NewLine);
                                sTexto += ("MOTIVO:".PadRight(8, ' ') + dtConsulta.Rows[i][1].ToString()) + Environment.NewLine;
                                sTexto += Environment.NewLine;
                            }
                        }

                        sTexto += Environment.NewLine;
                        sTexto += ("TOTAL PRODUCTOS DE CORTESIA: ".PadRight(30, ' ') + total.ToString("N2").PadLeft(10, ' ')) + Environment.NewLine + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    }
                    else
                    {
                        //drCortesia = dtCortesia.NewRow();
                        //drCortesia["TotalProductoCortesia"] = "0.00";
                    }
                }

                sSql = "";
                sSql += "SELECT NP.nombre, CAN.motivo_cancelacion, DP.precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, ORI.descripcion" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_cancelacion_productos CAN ON DP.id_det_pedido = CAN.id_det_pedido INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON CP.id_pos_origen_orden = ORI.id_pos_origen_orden INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                total = 0;

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += ("PRODUCTOS CANCELADOS SIN IMPUESTOS:") + Environment.NewLine;
                        sTexto += Environment.NewLine;
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {

                            total = total + Convert.ToDecimal(dtConsulta.Rows[i][2].ToString());
                            double iTotalCortesia = Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
                            string sTotalCortesia = iTotalCortesia.ToString("N2");
                            //dtCortesia.Rows.Add(drCortesia);

                            if (dtConsulta.Rows[i][0].ToString().Length < 25)
                            {
                                sTexto += (dtConsulta.Rows[i][3].ToString().PadLeft(5, ' ') + "".PadLeft(1, ' ') + dtConsulta.Rows[i][0].ToString().PadRight(25, ' ') + sTotalCortesia.PadLeft(9, ' ')) + Environment.NewLine;
                                sTexto += ("MOTIVO:".PadRight(8, ' ') + dtConsulta.Rows[i][1].ToString()) + Environment.NewLine;
                                sTexto += Environment.NewLine;
                            }
                            else
                            {
                                sTexto += (dtConsulta.Rows[i][3].ToString().PadLeft(5, ' ') + "".PadLeft(1, ' ') + dtConsulta.Rows[i][0].ToString().Substring(0, 25).PadRight(25, ' ') + sTotalCortesia.PadLeft(9, ' ')) + Environment.NewLine;
                                sTexto += (dtConsulta.Rows[i][0].ToString().Substring(25).PadLeft((5 + dtConsulta.Rows[i][0].ToString().Substring(25).Length), ' ') + Environment.NewLine);
                                sTexto += ("MOTIVO:".PadRight(8, ' ') + dtConsulta.Rows[i][1].ToString());
                                sTexto += Environment.NewLine;
                            }


                        }
                        sTexto += Environment.NewLine;
                        sTexto += ("TOTAL PRODUCTOS CANCELADOS: ".PadRight(30, ' ') + total.ToString("N2").PadLeft(10, ' ')) + Environment.NewLine + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    }

                    else
                    {
                        //drCortesia = dtCortesia.NewRow();
                        //drCortesia["TotalProductoCortesia"] = "0.00";
                    }
                }

                sSql = "";
                sSql += "select NCP.numero_pedido, C.motivo_cancelacion" + Environment.NewLine;
                sSql += "from pos_cancelacion C, cv403_numero_cab_pedido NCP, cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where C.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and NCP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and C.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "order by NCP.numero_pedido";


                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += Environment.NewLine;
                        sTexto += ("ORDENES CANCELADAS: ".PadRight(30, ' ')) + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sTexto += "Numero de Ticket: " + dtConsulta.Rows[i][0].ToString() + Environment.NewLine;
                            sTexto += "MOTIVO DE CANCELACION DE TICKET:" + Environment.NewLine;
                            sTexto += dtConsulta.Rows[i][1].ToString().Trim() + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }


                sTexto += Environment.NewLine;
                //sTexto += ("****************************************") + Environment.NewLine;
                sTexto += ("TOTAL PROPINAS: ".PadRight(31, ' ') + calcularPropinas().ToString("N2").PadLeft(9, ' ')) + Environment.NewLine;
                sTexto += ("TOTAL DESCUENTOS: ".PadRight(31, ' ') + calcularDescuentos().ToString("N2").PadLeft(9, ' ')) + Environment.NewLine;
                llenarCuentasCanceladas();
                sTexto += ("TOTAL CUENTAS CANCELADAS: ".PadRight(31, ' ') + dSumaTotalCuentasAnuladas.ToString("N2").PadLeft(9, ' ')) + Environment.NewLine;

                //AQUI LAS ENTRADAS Y SALIDAS MANUALES

                dbTotalEntradasManuales = sumarEntradasSalidasManuales(1);
                dbTotalSalidasManuales = sumarEntradasSalidasManuales(0);

                sTexto += Environment.NewLine;

                sTexto += "ENTRADAS MANUALES: ".PadRight(30, ' ') + dbTotalEntradasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "SALIDAS MANUALES : ".PadRight(30, ' ') + dbTotalSalidasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA MOSTRAR LAS SALIDAS DE PRODUCTOS
        public string productosDespachados(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "REPORTE DE PRODUCTOS DESPACHADOS".PadLeft(36, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select NOM.nombre, sum(DET.cantidad) CANTIDAD," + Environment.NewLine;
                    sSql += "sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL" + Environment.NewLine;
                    sSql += "from cv403_det_pedidos DET inner join" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto" + Environment.NewLine;
                    sSql += "and NOM.estado = 'A' inner join" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CAB on CAB.id_pedido = DET.id_pedido" + Environment.NewLine;
                    sSql += "and CAB.estado = 'A'" + Environment.NewLine;
                    sSql += "and DET.estado = 'A' inner join" + Environment.NewLine;
                    sSql += "cv401_productos PROD on NOM.id_producto = PROD.id_producto" + Environment.NewLine;
                    sSql += "and PROD.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CAB.id_pos_origen_orden" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "where CAB.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and CAB.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and CAB.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and PROD.id_pos_tipo_producto = " + Convert.ToInt32(dtAyuda.Rows[i][0].ToString()) + Environment.NewLine;
                    sSql += "group by NOM.nombre" + Environment.NewLine;
                    sSql += "order by sum(DET.cantidad)";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbSumarPorTipoProducto = 0;

                        sTexto += "TIPO DE PRODUCTO: " + dtAyuda.Rows[i][1].ToString().ToUpper().Trim() + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += "DESCRIPCION".PadRight(25, ' ') + "CANT.".PadLeft(5, ' ') + "TOTAL".PadLeft(10, ' ') + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j][0].ToString();
                            iCantidad = Convert.ToInt32(dtConsulta.Rows[j][1].ToString());
                            dbTotal = Convert.ToDecimal(dtConsulta.Rows[j][2].ToString());
                            dbSumarPorTipoProducto += Convert.ToDecimal(dtConsulta.Rows[j][2].ToString());
                            dbSuma += dbTotal;

                            if (sNombreProducto.Length > 25)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 25);
                            }

                            sTexto += sNombreProducto.PadRight(25, ' ') + iCantidad.ToString().PadLeft(5, ' ') + dbTotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        }

                        sTexto += "".PadLeft(40, '-') + Environment.NewLine + ("TOTAL " + dtAyuda.Rows[i][1].ToString().ToUpper().Trim()).PadRight(30, ' ') + dbSumarPorTipoProducto.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        sTexto += Environment.NewLine + "".PadLeft(40, '-') + Environment.NewLine;
                    }
                }

                sTexto += "TOTALES".PadRight(30, ' ') + dbSuma.ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                sTexto += "NOTA: EN EL TOTAL INCLUYE LOS VALORES DE" + Environment.NewLine;
                sTexto += "CUENTAS POR COBRAR Y CORTESÍAS" + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CREAR EL REPORTE DETALLADO POR ORIGEN
        public string detalleVentasOrigen(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select id_pos_origen_orden, codigo, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_origen_orden";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdOrigenOrden = Convert.ToInt32(dtAyuda.Rows[i]["id_pos_origen_orden"].ToString());
                    sOrigenOrden = dtAyuda.Rows[i]["descripcion"].ToString().Trim().ToUpper();

                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    sSql += "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and id_localidad = " + iIdLocalidad;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    if (iCuenta > 0)
                    {
                        sTexto += "ORIGEN: " + sOrigenOrden + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                        sSql = "";
                        sSql += "select * from pos_vw_detallar_productos_origen_orden" + Environment.NewLine;
                        sSql += "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                        sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                        sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                        sSql += "order by cantidad";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == false)
                        {
                            return "ERROR";
                        }

                        dbTotal = 0;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j]["nombre"].ToString().Trim().ToUpper();
                            dbCantidadProductos = Convert.ToDecimal(dtConsulta.Rows[j]["cantidad"].ToString().Trim().ToUpper());
                            sTotal = dtConsulta.Rows[j]["total"].ToString().Trim();
                            dbTotal += Convert.ToDecimal(dtConsulta.Rows[j]["total"].ToString().Trim());

                            if (sNombreProducto.Length > 25)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 25);
                            }

                            sTexto += sNombreProducto.PadRight(25, ' ') + dbCantidadProductos.ToString("N0").PadLeft(5, ' ') + sTotal.PadLeft(10, ' ') + Environment.NewLine;
                        }

                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += "TOTAL REPORTADO:".PadRight(30, ' ') + dbTotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    }
                }
                
                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CREAR DOCUMENTOS DE VENTA REALIZADO
        public string reporteCantidadPagos(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                dbTotalOrdenes = 0;
                dbTotalTarjetas = 0;
                dbTotalEfectivo = 0;
                dbTotalCheques = 0;

                sSql = "";
                sSql += "select NCP.numero_pedido, NF.numero_factura, FP.id_pedido, NF.idtipocomprobante" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos FP, cv403_numeros_facturas NF," + Environment.NewLine;
                sSql += "cv403_numero_cab_pedido NCP, cv403_facturas F, cv403_cab_pedidos CP," + Environment.NewLine;
                sSql += "pos_origen_orden O" + Environment.NewLine;
                sSql += "where NF.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and NCP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and NCP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NCP.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and NF.estado = 'A'" + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                //sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and O.genera_factura = 1" + Environment.NewLine;
                sSql += "order by FP.id_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadRight(40, '=')+ Environment.NewLine;
                sTexto += "DOCUMENTOS DE VENTA REALIZADOS".PadLeft(35, ' ') + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;
                sTexto += " TICKET  DOCUM.   TOTAL   EF/CH. TARJETA" + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNumeroOrden_P = dtConsulta.Rows[i][0].ToString();
                    sNumeroFactura_P = dtConsulta.Rows[i][1].ToString(); ;
                    iIdOrden_P = Convert.ToInt32(dtConsulta.Rows[i][2].ToString());
                    iTipoComprobante = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());

                    if (iTipoComprobante == 1)
                    {
                        sNumeroOrden_P = sNumeroOrden_P + "F";
                    }

                    else
                    {
                        sNumeroOrden_P = sNumeroOrden_P + "N";
                    }

                    sTextoDevuelta = desglosePagos(iIdOrden_P);

                    if (sTextoDevuelta != "")
                    {
                        sTexto += "T" + sNumeroOrden_P.PadLeft(8, ' ') + sNumeroFactura_P.PadLeft(7, ' ') + sTextoDevuelta;
                    }
                }

                sTexto += Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL VENDIDO  : " + dbTotalOrdenes.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbAuxiliar = dbTotalOrdenes;
                sTexto += "TOTAL EN FACT. : " + dbTotalOrdenes.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                sTexto += "IVA EN FACTURAS: " + extraerIva().ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;

                //MOSTRAR VALORES EN CORTESIAS, VALES FUNCIONARIOS
                //CONTAR LAS CORTESIA

                sTextoDevuelta = extraerOtrosValores("04");

                if (sTextoDevuelta != "")
                {
                    sTexto += "TOT. CORTESIAS : " + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                }

                //VALES FUNCIONARIOS
                sTextoDevuelta = extraerOtrosValores("05");

                if (sTextoDevuelta != "")
                {
                    sTexto += "T. FUNCIONARIOS: " + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTextoDevuelta = "";
                sTextoDevuelta = calcularDescuentosVentas();

                if (sTextoDevuelta != "")
                {
                    sTexto += "TOT. DESCUENTOS: " + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                }


                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL REPORTADO: " + dbTotalOrdenes.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                //TOTALES DESGLOSADOS
                if (dbTotalEfectivo != 0)
                {
                    sTexto += "TOTAL EFECTIVO : " + dbTotalEfectivo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine; ;
                }

                if (dbTotalCheques != 0)
                {
                    sTexto += "TOTAL CHEQUES  : " + dbTotalCheques.ToString("N2").PadLeft(10, ' ') + Environment.NewLine; ;
                }

                if (dbTotalTarjetas != 0)
                {
                    sTexto += "TOTAL TARJETAS : " + dbTotalTarjetas.ToString("N2").PadLeft(10, ' ') + Environment.NewLine; ;
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL COBRADO  : " + dbAuxiliar.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        private string desglosePagos(int iIdPedido)
        {
            try
            {
                sSql = "";
                //sSql += "select descripcion, valor, cambio, count(*) cuenta, codigo" + Environment.NewLine;
                sSql += "select descripcion, sum(valor) valor, cambio, count(*) cuenta, codigo" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "group by descripcion, valor, codigo, cambio" + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtPagosDesglose = new DataTable();
                dtPagosDesglose.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPagosDesglose, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtPagosDesglose.Rows.Count == 0)
                {
                    return "SN";
                }

                dTotalPagadoP = 0;
                iBandera = 0;
                sTextoDesglose = "";

                //CICLO PARA OBTENER EL TOTAL FACTURADO EN LA ORDEN

                for (int i = 0; i < dtPagosDesglose.Rows.Count; i++)
                {
                    dTotalPagadoP += Convert.ToDecimal(dtPagosDesglose.Rows[i][1].ToString());
                }

                dbTotalOrdenes = dbTotalOrdenes + dTotalPagadoP;

                //CICLO PARA DETALLAR LOS PAGOS REALIZADOS EN LA ORDEN

                for (int i = 0; i < dtPagosDesglose.Rows.Count; i++)
                {
                    if (iBandera == 0)
                    {
                        sCodigoCobro = dtPagosDesglose.Rows[i][4].ToString();

                        if ((sCodigoCobro == "EF") || (sCodigoCobro == "CH"))
                        {
                            dEfectivoCheque_P = Convert.ToDecimal(dtPagosDesglose.Rows[i][1].ToString());
                            sTextoDesglose = sTextoDesglose + dTotalPagadoP.ToString("N2").PadLeft(8, ' ') + dEfectivoCheque_P.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;

                            if (sCodigoCobro == "EF")
                            {
                                dbTotalEfectivo = dbTotalEfectivo + dEfectivoCheque_P;
                            }

                            else if (sCodigoCobro == "CH")
                            {
                                dbTotalCheques = dbTotalCheques + dEfectivoCheque_P;
                            }
                        }

                        else
                        {
                            sNombreTarjeta_P = dtPagosDesglose.Rows[i][0].ToString();
                            dTarjeta_P = Convert.ToDecimal(dtPagosDesglose.Rows[i][1].ToString());
                            //sTextoDesglose = sTextoDesglose + dTotalPagadoP.ToString("N2").PadLeft(8, ' ') + dTarjeta_P.ToString("N2").PadLeft(16, ' ') + Environment.NewLine;

                            if (sNombreTarjeta_P.Length > 11)
                            {
                                sNombreTarjeta_P = sNombreTarjeta_P.Substring(0, 11);
                            }

                            if (dtPagosDesglose.Rows.Count == 1)
                            {
                                //sTextoDesglose = sTextoDesglose + "".PadLeft(19, ' ') + sNombreTarjeta_P.PadRight(12, ' ') + ":" + dTarjeta_P.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                                sTextoDesglose = sTextoDesglose + dTotalPagadoP.ToString("N2").PadLeft(8, ' ') + dTarjeta_P.ToString("N2").PadLeft(16, ' ') + Environment.NewLine;
                                sTextoDesglose = sTextoDesglose + "".PadLeft(19, ' ') + sNombreTarjeta_P.PadRight(12, ' ') + ":" + dTarjeta_P.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                            }
                            else
                            {
                                sTextoDesglose = sTextoDesglose + dTotalPagadoP.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                                sTextoDesglose = sTextoDesglose + "".PadLeft(19, ' ') + sNombreTarjeta_P.PadRight(12, ' ') + ":" + dTarjeta_P.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                            }

                            dbTotalTarjetas = dbTotalTarjetas + dTarjeta_P;
                        }

                        iBandera = 1;
                    }

                    else
                    {
                        sCodigoCobro = dtPagosDesglose.Rows[i][4].ToString();

                        if ((sCodigoCobro == "EF") || (sCodigoCobro == "CH"))
                        {
                            dEfectivoCheque_P = Convert.ToDecimal(dtPagosDesglose.Rows[i][1].ToString());
                            sTextoDesglose = sTextoDesglose + dEfectivoCheque_P.ToString("N2").PadLeft(32, ' ') + Environment.NewLine;

                            if (sCodigoCobro == "EF")
                            {
                                dbTotalEfectivo = dbTotalEfectivo + dEfectivoCheque_P;
                            }

                            else if (sCodigoCobro == "CH")
                            {
                                dbTotalCheques = dbTotalCheques + dEfectivoCheque_P;
                            }
                        }

                        else
                        {
                            dTarjeta_P = Convert.ToDecimal(dtPagosDesglose.Rows[i][1].ToString());
                            sNombreTarjeta_P = dtPagosDesglose.Rows[i][0].ToString();
                            sTextoDesglose = sTextoDesglose + "".PadLeft(19, ' ') + sNombreTarjeta_P.PadRight(12, ' ') + ":" + dTarjeta_P.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;

                            dbTotalTarjetas = dbTotalTarjetas + dTarjeta_P;
                        }
                    }
                }

                return sTextoDesglose;
            }

            catch (Exception ex)
            {
                return "";
            }
        }

        //Función para calcular el valor de efectivo, cheques y transferencias
        private Decimal calcularTotalPago(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dtConsulta.Rows[0][1].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {                    
                    return 0;
                }

            }

            catch (Exception)
            {                
                return 0;
            }
        }

        //Función para calcular el valor de las tarjetas de crédito
        private Decimal calcularTotalPagoTarjetas()
        {
            try
            {
                Decimal sumaTarjetas = 0;

                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo in ('TC', 'TD')" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sumaTarjetas += Convert.ToDecimal(dtConsulta.Rows[i][1].ToString());
                        }

                        return sumaTarjetas;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        //Función para calcular las propinas
        private Decimal calcularPropinas()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(PAG.propina),0) propina" + Environment.NewLine;
                sSql += "from cv403_pagos PAG , cv403_documentos_pagados DPAGA," + Environment.NewLine;
                sSql += "CV403_DCTOS_POR_COBRAR AS XC, cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where DPAGA.id_pago = PAG.id_pago" + Environment.NewLine;
                sSql += "and DPAGA.id_documento_cobrar = XC.id_documento_cobrar " + Environment.NewLine;
                sSql += "and XC.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado = 'A'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }

            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        //Función para calcular el total de descuentos
        private Decimal calcularDescuentos()
        {
            try
            {
                Decimal dbSumaDescuentos = 0;

                sSql = "";
                sSql += "select ltrim(str(isnull(DP.cantidad,0), 10, 2)) cantidad, ltrim(str(isnull(DP.valor_dscto,0), 10, 2)) valor_dscto" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                sSql += "where CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and DP.valor_dscto <> 0" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dbSumaDescuentos = dbSumaDescuentos + (Convert.ToDecimal(dtConsulta.Rows[i][0].ToString()) * Convert.ToDecimal(dtConsulta.Rows[i][1].ToString()));
                        }

                        return dbSumaDescuentos;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        //funcion para llenar las cuentas canceladas
        private void llenarCuentasCanceladas()
        {
            try
            {
                sSql = "";
                sSql += "select CP.id_pedido, CP.cuenta, CP.estado_orden," + Environment.NewLine;
                sSql += "c.motivo_cancelacion, CP.valor_cancelado" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos as CP, pos_cancelacion AS C" + Environment.NewLine;
                sSql += "where C.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cancelada'" + Environment.NewLine;
                sSql += "and CP.estado = 'N'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    Decimal suma = 0;
                    Decimal valor;

                    if (dtConsulta.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            valor = Convert.ToDecimal(dtConsulta.Rows[i][4].ToString());
                            suma = suma + valor;
                        }
                        dSumaTotalCuentasAnuladas = suma;
                    }
                }

                else
                {
                    dSumaTotalProductosCancelados = 0;
                }
            }

            catch (Exception ex)
            {
                dSumaTotalProductosCancelados = 0;
            }
        }

        //FUNCION PARA OBTENER EL VALOR DE LA ENTRADAS Y SALIDAS MANUALES
        private Decimal sumarEntradasSalidasManuales(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(valor), 0) suma" + Environment.NewLine;
                sSql += "from pos_movimiento_caja  " + Environment.NewLine;
                sSql += "where estado = 'A' " + Environment.NewLine;
                sSql += "and tipo_movimiento = " + iOp + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and id_documento_pago is null" + Environment.NewLine;
                }

                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        //FUNCION PARA EXTRAER EL IVA COBRADO
        private Decimal extraerIva()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * DP.valor_iva), 0), 10, 2)) suma" + Environment.NewLine;
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
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception)
            {
                return 0;
            }
        }

        public string extraerOtrosValores(string sCodigo)
        {
            try
            {
                sTextoDesglose = "";
                dTotalPagadoP = 0;

                sSql = "";
                sSql += "select ltrim(str(" + conexion.GFun_St_esnulo() + "(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "pos_origen_orden OO" + Environment.NewLine;
                sSql += "where OO.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and OO.codigo = '" + sCodigo + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotalPagadoP = Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                        dbTotalOrdenes = dbTotalOrdenes + dTotalPagadoP;

                        if (dTotalPagadoP != 0)
                        {
                            sTextoDesglose = dTotalPagadoP.ToString("N2");
                        }
                    }

                    else
                    {
                        return "";
                    }
                }

                else
                {
                    return "";
                }


                return sTextoDesglose;
            }

            catch (Exception ex)
            {
                return "";
            }
        }

        //FUNCION PARA EXTRAER LOS DESCUENTOS
        private string calcularDescuentosVentas()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(DP.cantidad,0), 10, 2)) cantidad," + Environment.NewLine;
                sSql += "ltrim(str(isnull(DP.valor_dscto,0), 10, 2)) valor_dscto" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                sSql += "where CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and DP.estado='A'" + Environment.NewLine;
                sSql += "and CP.estado='A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
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
                            dTotalDescuentos = dTotalDescuentos + (Convert.ToDecimal(dtConsulta.Rows[i][0].ToString()) * Convert.ToDecimal(dtConsulta.Rows[i][1].ToString()));
                        }

                        return dTotalDescuentos.ToString("N2");
                    }
                    else
                    {
                        return "0.00";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //FUNCION PARA CREAR EL REPORTE ARQUEO DE CAJA
        public string arqueoCaja(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";
                sTexto += "".PadRight(40, '=') + Environment.NewLine;
                sTexto += "ARQUEO DE CAJA".PadLeft(27, ' ') + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;

                dbTotalCobradoEfectivo = calcularTotalPago("EF");
                dbTotalCobradoTransferencias = calcularTotalPago("TR");
                dbTotalCobradoCheques = calcularTotalPago("CH");
                dbTotalCobradoTarjetas = calcularTotalPagoTarjetas();

                sTexto += "Total Vendido.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalCobradoTransferencias + dbTotalCobradoCheques + dbTotalCobradoTarjetas).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "Cobrado Efectivo..:".PadRight(30, ' ') + dbTotalCobradoEfectivo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "Cobrado Transferen:".PadRight(30, ' ') + dbTotalCobradoTransferencias.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "Cobrado Cheques...:".PadRight(30, ' ') + dbTotalCobradoCheques.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "Cobrado Tarjetas..:".PadRight(30, ' ') + dbTotalCobradoTarjetas.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += llenarDesgloseTarjetas();
                sTexto += "Total Cobrado.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalCobradoTransferencias + dbTotalCobradoCheques + dbTotalCobradoTarjetas).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                //MOSTRAR VALORES EN CORTESIAS, VALES FUNCIONARIOS
                //CONTAR LAS CORTESIA

                sTextoDevuelta = extraerOtrosValores("04");

                if (sTextoDevuelta != "")
                {
                    sTexto += "Ordenes Cortesias.:".PadRight(30, ' ') + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                }

                //VALES FUNCIONARIOS
                sTextoDevuelta = extraerOtrosValores("05");

                if (sTextoDevuelta != "")
                {
                    sTexto += "T. Funcionarios...:".PadRight(30, ' ') + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                }

                dbTotalEntradasManuales = sumarEntradasSalidasManuales(1);
                dbTotalSalidasManuales = sumarEntradasSalidasManuales(0);

                sTexto += "Entradas Manuales.:".PadRight(30, ' ') + dbTotalEntradasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "Salidas Manuales..:".PadRight(30, ' ') + dbTotalSalidasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "Total Efectivo....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;

                sTexto += "Saldo en Caja.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "Total Entregado...:".PadRight(30, ' ') + dbTotalEntregado.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "Diferencia........:".PadRight(30, ' ') + (dbTotalEntregado - (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales)).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine + Environment.NewLine;
                sTexto += "Total Pendiente...:".PadRight(30, ' ') + dbTotalPendiente.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                #region Llenar Cortesías

                sSql = "";
                sSql += "select NP.nombre, PC.motivo_cortesia," + Environment.NewLine;
                sSql += "ltrim(str(DP.precio_unitario, 10, 2)) precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, O.descripcion" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN " + Environment.NewLine;
                sSql += "pos_cortesia PC ON DP.id_det_pedido = PC.id_det_pedido" + Environment.NewLine;
                sSql += "and PC.estado='A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado_orden = 'Pagada'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                double total = 0;

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            total = total + (Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][3].ToString()));
                        }

                        sTexto = sTexto + "Total Items Cortesia: ".PadRight(30, ' ') + total.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                    }
                    else
                    {
                        sTexto = sTexto + "Total Items Cortesia: ".PadRight(30, ' ') + "0.00".PadLeft(10, ' ') + Environment.NewLine;
                    }
                }

                #endregion

                //sTexto = sTexto + "I.V.A. Cobrado....:".PadRight(30, ' ') + (dbTotalCobrado - (dbTotalCobrado / (1 + (dbPorcentajeIva/100)))).ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "IVA en Facturas...:".PadRight(30, ' ') + extraerIva().ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "Personas Atendidas:".PadRight(30, ' ') + calcularTotalPersonas("01").ToString().PadLeft(10, ' ') + Environment.NewLine;
                verRangoComandas();
                verRangoFacturas();
                verRangoNotasEntrega();

                string sCuentaCliente = cuentasClienteEmpresarial();

                if ((sCuentaCliente != "ERROR") && (sCuentaCliente != ""))
                {
                    sTexto += sCuentaCliente;
                }

                sTexto += Environment.NewLine;

                return sTexto;

            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //Función para mostrar las tarjetas de crédito
        private string llenarDesgloseTarjetas()
        {
            try
            {
                sTextoDesglose = "";

                sSql = "";
                sSql += "select FP.descripcion,sum(FP.valor) valor, count (CP.id_pedido) Numero" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP,  pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado_orden= 'Pagada'" + Environment.NewLine;
                sSql += "and FP.codigo in ('TC','TD')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by FP.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreTarjeta = dtConsulta.Rows[i][0].ToString();

                            if (sNombreTarjeta.Length > 15)
                            {
                                sNombreTarjeta = sNombreTarjeta.Substring(0, 15);
                            }

                            double dbValorTarjeta = Convert.ToDouble(dtConsulta.Rows[i][1].ToString());
                            sTextoDesglose += "".PadRight(6, ' ') + sNombreTarjeta.PadRight(15, '.') + ":" + dbValorTarjeta.ToString("N2").PadLeft(15, ' ') + Environment.NewLine;
                        }
                    }
                    else
                    {
                        sTextoDesglose += "No hay datos para ser mostrados" + Environment.NewLine;
                    }

                    return sTextoDesglose;
                }

                else
                {
                    return "ERROR";
                }


            }
            catch (Exception)
            {
                return "ERROR";
            }
        }

        //Función para calcular el total de personas que ocupan las mesas
        private int calcularTotalPersonas(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(CP.numero_personas),0) numero" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_origen_orden ORI" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'";

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
                    return 0;
                }
            }

            catch (Exception)
            {                
                return 0;
            }
        }

        //FUNCION PARA EXTRAER EL RANGO DE COMANDAS HECHAS EN LA FECHA
        private void verRangoComandas()
        {
            try
            {
                sSql = "";
                sSql += "select numero_pedido from cv403_numero_cab_pedido" + Environment.NewLine;
                sSql += "where id_pedido = (select min(id_pedido)from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A')" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select numero_pedido from cv403_numero_cab_pedido" + Environment.NewLine;
                sSql += "where id_pedido = (select max(id_pedido)from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 1)
                    {
                        sTexto = sTexto + "Tickets desde:  " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[0][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                    }

                    else if (dtConsulta.Rows.Count > 1)
                    {
                        sTexto = sTexto + "Tickets desde:  " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[1][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                    }
                }

                else
                {
                    sTexto = sTexto + "Tickets desde:  " + "".PadLeft(8, ' ') + " hasta: " + "".PadLeft(8, ' ') + Environment.NewLine;
                }
            }

            catch (Exception ex)
            {
                return;
            }
        }

        //FUNCION PARA EXTRAER EL RANGO DE FACTURAS HECHAS EN LA FECHA
        private void verRangoFacturas()
        {
            try
            {
                sSql = "";
                sSql += "select numero_factura" + Environment.NewLine;
                sSql += "from cv403_numeros_facturas" + Environment.NewLine;
                sSql += "where id_factura =" + Environment.NewLine;
                sSql += "(select min(F.id_factura)" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_facturas_pedidos FP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                //sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1" + Environment.NewLine;
                sSql += "and F.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A')" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select numero_factura" + Environment.NewLine;
                sSql += "from cv403_numeros_facturas" + Environment.NewLine;
                sSql += "where id_factura =" + Environment.NewLine;
                sSql += "(select max(F.id_factura)" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_facturas_pedidos FP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                //sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1" + Environment.NewLine;
                sSql += "and F.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 1)
                    {
                        sTexto = sTexto + "Facturas desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[0][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                    }

                    else if (dtConsulta.Rows.Count > 1)
                    {
                        sTexto = sTexto + "Facturas desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[1][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                    }
                }

                else
                {
                    sTexto = sTexto + "Facturas desde:" + "".PadLeft(8, ' ') + "   hasta:" + "".PadLeft(8, ' ') + Environment.NewLine;
                }
            }

            catch (Exception ex)
            {
                return;
            }
        }

        //FUNCION PARA EXTRAER EL RANGO DE NOTAS DE VENTA HECHAS EN LA FECHA
        private void verRangoNotasEntrega()
        {
            try
            {
                sSql = "";
                sSql += "select numero_factura" + Environment.NewLine;
                sSql += "from cv403_numeros_facturas" + Environment.NewLine;
                sSql += "where id_factura =" + Environment.NewLine;
                sSql += "(select min(F.id_factura)" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_facturas_pedidos FP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                //sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and F.idtipocomprobante = " + Program.iComprobanteNotaEntrega + Environment.NewLine;
                sSql += "and F.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A')" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select numero_factura" + Environment.NewLine;
                sSql += "from cv403_numeros_facturas" + Environment.NewLine;
                sSql += "where id_factura =" + Environment.NewLine;
                sSql += "(select max(F.id_factura)" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_facturas_pedidos FP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                //sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and F.idtipocomprobante = " + Program.iComprobanteNotaEntrega + Environment.NewLine;
                sSql += "and F.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count != 0)
                    {
                        if (dtConsulta.Rows.Count == 1)
                        {
                            sTexto = sTexto + "N. Entrega desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(6, ' ') + "  hasta: " + dtConsulta.Rows[0][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                        }

                        else if (dtConsulta.Rows.Count > 1)
                        {
                            sTexto = sTexto + "N. Entrega desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(6, ' ') + "  hasta: " + dtConsulta.Rows[1][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                }

                else
                {
                    sTexto = sTexto + "N. Entrega desde: " + "".PadLeft(6, ' ') + " hasta:" + "".PadLeft(8, ' ') + Environment.NewLine;
                }
            }

            catch (Exception ex)
            {
                return;
            }
        }

        //FUNCION PARA CARGAR LAS CUENTA SPOR COBRAR DE CLIENTE EMPRESARIAL
        private string cuentasClienteEmpresarial()
        {
            try
            {
                string sCliente_P = "";
                string sNombreEmpresa;
                string sPrecioEmpresa;
                Decimal dbTotalEmpresa = 0;

                sSql = "";
                sSql += "select ltrim(isnull(nombres, '') + ' ' + apellidos) cliente," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_dctos_por_cobrar XC ON CP.id_pedido = XC.id_pedido" + Environment.NewLine;
                sSql += "and XC.estado = 'A'" + Environment.NewLine;
                sSql += "where O.cuenta_por_cobrar = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and XC.cg_estado_dcto = 7460" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by TP.nombres, TP.apellidos" + Environment.NewLine;
                sSql += "order by TP.apellidos" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    sCliente_P += "".PadLeft(40, '-') + Environment.NewLine;
                    sCliente_P += " CUENTAS POR COBRAR CLIENTE EMPRESARIAL" + Environment.NewLine;
                    sCliente_P += "".PadLeft(40, '-') + Environment.NewLine;

                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        sNombreEmpresa = dtConsulta.Rows[i]["cliente"].ToString().Trim().ToUpper();
                        sPrecioEmpresa = dtConsulta.Rows[i]["total"].ToString().Trim().ToUpper();
                        dbTotalEmpresa += Convert.ToDecimal(sPrecioEmpresa);

                        if (sNombreEmpresa.Length > 30)
                        {
                            sNombreEmpresa = sNombreEmpresa.Substring(0, 30);
                        }

                        sCliente_P += sNombreEmpresa.PadRight(30, ' ') + sPrecioEmpresa.PadLeft(10, ' ') + Environment.NewLine;
                    }

                    sCliente_P += Environment.NewLine + "TOTAL CUENTAS POR COBRAR:" + dbTotalEmpresa.ToString("N2").PadLeft(15, ' ') + Environment.NewLine;
                    sCliente_P += "".PadLeft(40, '-') + Environment.NewLine;
                }

                return sCliente_P;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER LAS VENTAS POR MESERO
        public string ventasMesero(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select mesero, ltrim(str(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 10,2)) suma" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero  = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad_P + Environment.NewLine;
                sSql += "group by mesero" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "VENTAS POR MESERO".PadLeft(29, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                dbSuma = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sTexto += dtConsulta.Rows[i]["mesero"].ToString().PadRight(30, ' ') + dtConsulta.Rows[i]["suma"].ToString().PadLeft(10, ' ') + Environment.NewLine;
                    dbSuma += Convert.ToDecimal(dtConsulta.Rows[i]["suma"].ToString());
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "".PadRight(23, ' ') + "Total:" + dbSuma.ToString("N2").PadLeft(11, ' ');
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA DETALLAR LAS ENTRADAS Y SALIDAS MANUALES
        public string llenarMovimientosAgrupados(int iTipoMovimiento, int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select convert(varchar, hora, 108) hora, concepto, ltrim(str(valor, 8, 2)) valor" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where tipo_movimiento = " + iTipoMovimiento + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_documento_pago is null" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_movimiento_caja";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                dbSuma = 0;

                sTexto = "";
                sTexto += "".PadRight(40, '=') + Environment.NewLine;

                if (iTipoMovimiento == 1)
                {
                    sTexto += "".PadRight(8, ' ') + "MANUALES MANUALES (CAJA)" + Environment.NewLine;
                }

                else
                {
                    sTexto += "".PadRight(8, ' ') + "SALIDAS MANUALES (CAJA)" + Environment.NewLine;
                }

                sTexto += "".PadRight(40, '-') + Environment.NewLine;
                sTexto += "HORA  " + "CONCEPTO".PadRight(27, ' ') + "VALOR" + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (dtConsulta.Rows[i][1].ToString().Length <= 25)
                    {
                        sTexto = sTexto + dtConsulta.Rows[i][0].ToString().Substring(0, 5).PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().PadRight(27, ' ') + dtConsulta.Rows[i][2].ToString().PadLeft(7, ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto = sTexto + dtConsulta.Rows[i][0].ToString().Substring(0, 5).PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(0, 25).PadRight(27, ' ') + dtConsulta.Rows[i][2].ToString().PadLeft(7, ' ') + Environment.NewLine;

                        for (int j = 25; j < dtConsulta.Rows[i][1].ToString().Length; j++)
                        {
                            iLongi = dtConsulta.Rows[i][1].ToString().Substring(j).Length;

                            if (iLongi > 25)
                            {
                                sTexto = sTexto + "".PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(j, 25) + Environment.NewLine;
                                j = j + 25;
                            }

                            else
                            {
                                sTexto = sTexto + "".PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(j, iLongi) + Environment.NewLine;
                                break;
                            }
                        }
                    }

                    dbSuma = dbSuma + Convert.ToDecimal(dtConsulta.Rows[i][2].ToString());
                }

                sTexto += "".PadRight(40, '-') + Environment.NewLine;
                sTexto += "".PadRight(23, ' ') + "Total:" + dbSuma.ToString("N2").PadLeft(11, ' ');
                sTexto += Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER EL AHORRO DE EMERGENCIA
        public string ahorroEmergencia(int iIdLocalidad_P, int iIdPosCierreCajero_P, Decimal dbAhorroEmergencia_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

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
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and P.ahorro_emergencia = 1" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sValorAhorroProductos = dtConsulta.Rows[0][0].ToString();

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "AHORRO DE EMERGENCIA".PadLeft(30, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "AHORRO TOTAL EN PRODUCTOS:".PadRight(30, ' ') + sValorAhorroProductos.PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "AHORRO INGRESO MANUAL    :".PadRight(30, ' ') + dbAhorroEmergencia_P.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CONTAR LAS MONEDAS
        public string contarMonedas(int iIdPosCierreCajero_P, int iIdLocalidad_P, Decimal dbCajaInicial_P, Decimal dbCajaFinal_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select * from pos_monedas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and tipo_ingreso = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                Decimal iMonedas;
                Decimal dbValorCalculo;
                Decimal dbSumaMonedas_P = 0;

                string sMoneda = "" + Environment.NewLine;
                sMoneda += "".PadLeft(40, '=') + Environment.NewLine;
                sMoneda += "RESUMEN DE MONEDAS Y BILLETES".PadLeft(35, ' ') + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda01"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.01");
                sMoneda += "1   CENTAVO".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda05"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.05");
                sMoneda += "5   CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda10"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.10");
                sMoneda += "10  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda25"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.25");
                sMoneda += "25  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda50"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.50");
                sMoneda += "50  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete1"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("1");
                sMoneda += "1   DOLAR".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete2"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("2");
                sMoneda += "2   DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete5"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("5");
                sMoneda += "5   DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete10"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("10");
                sMoneda += "10  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete20"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("20");
                sMoneda += "20  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete50"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("50");
                sMoneda += "50  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete100"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("100");
                sMoneda += "100 DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;
                sMoneda += "TOTAL EN EFECTIVO DE CAJA:".PadRight(30, ' ') + dbSumaMonedas_P.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;

                sTexto += sMoneda;

                sTexto += "CAJA INICIAL: " + dbCajaInicial_P.ToString("N2") + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "CAJA FINAL  : " + dbCajaFinal_P.ToString("N2") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER LOS COMPROBANTES EMITIDOS EN ESTADO ELIMINADO
        public string comprobantesAnulados(int iIdCierreCajero_P, int iIdTipoComprobante_P)
        {
            try
            {
                sTexto = "";

                sSql = "";
                sSql += "select * from pos_vw_comprobantes_emitidos" + Environment.NewLine;
                //sSql += "where estado_factura = 'E'" + Environment.NewLine;
                sSql += "where estado_factura in ('E', 'N')" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdCierreCajero_P + Environment.NewLine;
                sSql += "and idtipocomprobante = " + iIdTipoComprobante_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                if (iIdTipoComprobante_P == 1)
                {
                    sTexto += "REPORTE DE FACTURAS ANULADAS".PadLeft(34, ' ') + Environment.NewLine;
                }

                else
                {
                    sTexto += "REPORTE DE NOTAS DE ENTREGA ANULADAS".PadLeft(38, ' ') + Environment.NewLine;
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                //sTexto += "  No. COMPROBANTE  FECHA Y HORA ANULACIÓN" + Environment.NewLine; 
                //sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNumeroFacturaAnula = dtConsulta.Rows[i]["establecimiento"].ToString().Trim() + "-" + dtConsulta.Rows[i]["punto_emision"].ToString().Trim() + "-" + dtConsulta.Rows[i]["numero_factura"].ToString().Trim().PadLeft(9, '0');
                    sFechaHoraAnula = Convert.ToDateTime(dtConsulta.Rows[i]["fecha_anula"].ToString().Trim()).ToString("dd-MM-yyyy HH:mm:ss");
                    sMotivoAnula = dtConsulta.Rows[i]["comentarios"].ToString().Trim().ToUpper();

                    if (sMotivoAnula.Length > 38)
                    {
                        sAyudaMotivoAnula = "";
                        
                        for (int j = 0; j < sMotivoAnula.Length; j = j + 38)
                        {
                            sContarCaracteres = sMotivoAnula.Substring(j);

                            if (sContarCaracteres.Length > 38)
                            {
                                sAyudaMotivoAnula += "  " + sMotivoAnula.Substring(j, 38) + Environment.NewLine;
                            }

                            else
                            {
                                sAyudaMotivoAnula += "  " + sMotivoAnula.Substring(j) + Environment.NewLine;
                                break;
                            }
                        }

                        sMotivoAnula = sAyudaMotivoAnula;
                    }

                    else
                    {
                        sMotivoAnula = "  " + sMotivoAnula;
                    }

                    sTexto += "- " + sNumeroFacturaAnula + "  " + sFechaHoraAnula + Environment.NewLine;

                    if (sMotivoAnula.Trim() != "")
                    {
                        sTexto += sMotivoAnula + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += Environment.NewLine;
                    }
                }

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        public string consultaPropinas(int iIdCierreCajero_P)
        {
            try
            {
                sTexto = "";

                sSql = "";
                sSql += "select ltrim(str(sum(isnull(propina, 0)), 10, 2)) propinas" + Environment.NewLine;
                sSql += "from pos_vw_propinas_recaudadas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL PROPINAS: " + dtConsulta.Rows[0][0].ToString() + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CONSULTAR EL RESUMEN DE CONSUMO DE EMPLEADOS
        public string consultaConsumoEmpleados(int iIdCierreCajero_P)
        {
            try
            {
                sTexto = "";

                sSql = "";
                sSql += "select identificacion, persona, numero_pedido," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(total), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from pos_vw_detalle_vale_consumo_empleados" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero_P + Environment.NewLine;
                sSql += "and codigo = '06'" + Environment.NewLine;
                //sSql += "and cg_estado_dcto = 7460" + Environment.NewLine;
                sSql += "group by identificacion, persona, numero_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "RESUMEN CONSUMO EMPLEADOS:".PadLeft(33, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                string sNombrePersona;
                Decimal dbSumaValores = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNombrePersona = dtConsulta.Rows[i]["persona"].ToString().Trim().ToUpper();

                    if (sNombrePersona.Length > 40)
                        sTexto += sNombrePersona.Substring(0, 40) + Environment.NewLine;
                    else
                        sTexto += sNombrePersona + Environment.NewLine;

                    sTexto += "PEDIDO: " + dtConsulta.Rows[i]["numero_pedido"].ToString().Trim().PadRight(17, ' ') +
                              "TOTAL: " + dtConsulta.Rows[i]["total"].ToString().Trim().PadLeft(8, ' ') + Environment.NewLine + Environment.NewLine;

                    dbSumaValores += Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString());
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL REPORTADO: " + dbSumaValores.ToString("N2") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                
                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA CONSULTAR EL CONSUMO INTERNO
        public string consultarConsumoInterno(int iIdCierreCajero_P)
        {
            try
            {
                sTexto = "";

                sSql = "";
                sSql += "select nombre, sum(cantidad) cantidad, ltrim(str(isnull(sum(valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_vw_reporte_consumo_interno" + Environment.NewLine;
                sSql += "where codigo = '14'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdCierreCajero_P + Environment.NewLine;
                sSql += "group by nombre";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "RESUMEN CONSUMO INTERNO".PadLeft(31, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                string sNombreProducto;
                Double dbCantidadProducto;
                string sTotalPrecioProducto;
                Decimal dbSumaValores = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dbCantidadProducto = Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString());
                    sNombreProducto = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();
                    sTotalPrecioProducto = dtConsulta.Rows[i]["valor"].ToString().Trim();

                    dbSumaValores += Convert.ToDecimal(sTotalPrecioProducto);                    

                    if (sNombreProducto.Length > 25)
                        sTexto += sNombreProducto.Substring(0, 25);
                    else
                        sTexto += sNombreProducto.PadRight(25, ' ');

                    sTexto += dbCantidadProducto.ToString().PadLeft(6, ' ') + sTotalPrecioProducto.PadLeft(9, ' ') + Environment.NewLine;

                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL REPORTADO: " + dbSumaValores.ToString("N2") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER LAS COMANDAS ELIMINADAS
        public string comandasAnulados(int iIdCierreCajero_P)
        {
            try
            {
                sTexto = "";

                sSql = "";
                sSql += "select * from pos_vw_comandas_emitidas" + Environment.NewLine;
                sSql += "where estado in ('E', 'N')" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdCierreCajero_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE COMANDAS ANULADAS".PadLeft(34, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNumeroComanda = "No." + dtConsulta.Rows[i]["numero_pedido"].ToString().Trim();
                    sFechaHoraAnula = Convert.ToDateTime(dtConsulta.Rows[i]["fecha_anula"].ToString().Trim()).ToString("dd-MM-yyyy HH:mm:ss");
                    sMotivoAnula = dtConsulta.Rows[i]["comentarios"].ToString().Trim().ToUpper();
                    sUsuarioAnula = dtConsulta.Rows[i]["usuario_anula"].ToString().Trim().ToUpper();

                    if (sMotivoAnula.Length > 38)
                    {
                        sAyudaMotivoAnula = "";

                        for (int j = 0; j < sMotivoAnula.Length; j = j + 38)
                        {
                            sContarCaracteres = sMotivoAnula.Substring(j);

                            if (sContarCaracteres.Length > 38)
                            {
                                sAyudaMotivoAnula += "  " + sMotivoAnula.Substring(j, 38) + Environment.NewLine;
                            }

                            else
                            {
                                sAyudaMotivoAnula += "  " + sMotivoAnula.Substring(j) + Environment.NewLine;
                                break;
                            }
                        }

                        sMotivoAnula = sAyudaMotivoAnula;
                    }

                    else
                    {
                        sMotivoAnula = "  " + sMotivoAnula;
                    }

                    sTexto += "- " + sNumeroComanda.PadRight(19, ' ') + sFechaHoraAnula + Environment.NewLine;
                    sTexto += "  USUARIO ANULA: " + sUsuarioAnula + Environment.NewLine;

                    if (sMotivoAnula.Trim() != "")
                    {
                        sTexto += sMotivoAnula + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += Environment.NewLine;
                    }
                }

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA MOSTRAR LAS SALIDAS DE PRODUCTOS
        public string listaVentas(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "REPORTE DE PRODUCTOS DESPACHADOS".PadLeft(36, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select NOM.nombre, sum(DET.cantidad) CANTIDAD," + Environment.NewLine;
                    sSql += "sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL" + Environment.NewLine;
                    sSql += "from cv403_det_pedidos DET inner join" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto" + Environment.NewLine;
                    sSql += "and NOM.estado = 'A' inner join" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CAB on CAB.id_pedido = DET.id_pedido" + Environment.NewLine;
                    sSql += "and CAB.estado = 'A'" + Environment.NewLine;
                    sSql += "and DET.estado = 'A' inner join" + Environment.NewLine;
                    sSql += "cv401_productos PROD on NOM.id_producto = PROD.id_producto" + Environment.NewLine;
                    sSql += "and PROD.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CAB.id_pos_origen_orden" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "where CAB.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and CAB.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and CAB.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and PROD.id_pos_tipo_producto = " + Convert.ToInt32(dtAyuda.Rows[i][0].ToString()) + Environment.NewLine;
                    sSql += "group by NOM.nombre" + Environment.NewLine;
                    sSql += "order by sum(DET.cantidad)";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbSumarPorTipoProducto = 0;

                        sTexto += "TIPO DE PRODUCTO: " + dtAyuda.Rows[i][1].ToString().ToUpper().Trim() + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += "".PadRight(5, ' ') + "DESCRIPCION".PadRight(30, ' ') + "CANT.".PadLeft(5, ' ') + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j][0].ToString();
                            iCantidad = Convert.ToInt32(dtConsulta.Rows[j][1].ToString());
                            dbTotal = Convert.ToDecimal(dtConsulta.Rows[j][2].ToString());
                            dbSuma += dbTotal;

                            if (sNombreProducto.Length > 25)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 25);
                            }

                            sTexto += sNombreProducto.PadRight(35, '_') + iCantidad.ToString().PadLeft(5, ' ') + Environment.NewLine;
                        }
                    }

                    sTexto += Environment.NewLine + Environment.NewLine;
                }

                sTexto += Environment.NewLine;
                sTexto += "NOTA: EN EL TOTAL INCLUYE LOS VALORES DE" + Environment.NewLine;
                sTexto += "CUENTAS POR COBRAR Y CORTESÍAS" + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA LISTAR LAS CORTESIAS, CANJES, VALE FUNIONARIOS - ORIGEN
        public string listaOrigenesSInFactura(int iIdLocalidad_P, int iIdPosCierreCajero_P, string sCodigoOrigen_P, string sEncabezadoReporte_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;
                this.sCodigoOrigenOrden = sCodigoOrigen_P;

                sTexto = "";

                sSql = "";
                sSql += "select persona, numero_pedido, comentarios," + Environment.NewLine;
                sSql += "isnull(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 0) valor" + Environment.NewLine;
                sSql += "from pos_vw_detalle_pedidos_origen_orden" + Environment.NewLine;
                sSql += "where codigo = '" + sCodigoOrigenOrden + "'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by persona, numero_pedido, comentarios" + Environment.NewLine;
                sSql += "order by persona";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += sEncabezadoReporte_P + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                string sPersona_P;
                string sNumeroPedido_P;
                string sComentarios_P;
                string sTotal;

                Decimal dbTotal = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sPersona_P = dtConsulta.Rows[i]["persona"].ToString().Trim().ToUpper();
                    sNumeroPedido_P = dtConsulta.Rows[i]["numero_pedido"].ToString().Trim();
                    sComentarios_P = dtConsulta.Rows[i]["comentarios"].ToString().Trim();
                    sTotal = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString()).ToString("N2");

                    dbTotal += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());

                    if (sPersona_P.Length > 40)
                    {
                        int iSuma = 0;

                        for (int j = 0; j < sPersona_P.Length; j = j + 40)
                        {
                            sTexto += sPersona_P.Substring(j, 40) + Environment.NewLine;
                            iSuma += 40;

                            if (sPersona_P.Length - iSuma <= 40)
                            {
                                sTexto += sPersona_P.Substring(iSuma) + Environment.NewLine;
                                break;
                            }
                        }
                    }

                    else
                    {
                        sTexto += sPersona_P + Environment.NewLine;
                    }

                    sTexto += "PEDIDO: " + sNumeroPedido_P.Trim().PadRight(16, ' ') + "TOTAL: " + sTotal.Trim().PadLeft(9, ' ') + Environment.NewLine;
                    sTexto += "MOTIVO:" + Environment.NewLine;

                    if (sComentarios_P.Length > 40)
                    {
                        int iSuma = 0;

                        for (int k = 0; k < sComentarios_P.Length; k = k + 40)
                        {
                            sTexto += sComentarios_P.Substring(k, 40) + Environment.NewLine;
                            iSuma += 40;

                            if (sComentarios_P.Length - iSuma <= 40)
                            {
                                sTexto += sComentarios_P.Substring(iSuma) + Environment.NewLine;
                                break;
                            }
                        }
                    }

                    else
                    {
                        sTexto += sComentarios_P + Environment.NewLine;
                    }

                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                }

                sTexto += "TOTAL REPORTADO: " + dbTotal.ToString("N2") + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA MOSTRAR LAS SALIDAS DE PRODUCTOS
        public string listaPropinasPorTarjetas(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                sTexto = "";

                sSql = "";
                sSql += "select descripcion, codigo, sum(propina_recibida) propina_recibida" + Environment.NewLine;
                sSql += "from pos_vw_reporte_propinas_formas_pago" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero_P + Environment.NewLine;
                sSql += "and estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and codigo in ('TD', 'TC')" + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad_P + Environment.NewLine;
                sSql += "group by descripcion, codigo";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                Decimal dbTotal_P = 0;
                Decimal dbPropina_P;

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "REPORTE DE PROPINAS POR TARJETAS".PadLeft(36, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    dbPropina_P = Convert.ToDecimal(dtAyuda.Rows[i]["propina_recibida"].ToString());
                    sTexto += dtAyuda.Rows[i]["descripcion"].ToString().Trim().ToUpper().PadRight(30, ' ') + dbPropina_P.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    dbTotal_P += dbPropina_P;
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL PROPINAS RECAUDADAS:".PadRight(30, ' ') + dbTotal_P.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }
    }
}


