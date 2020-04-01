using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseArqueoCaja2
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
                
        DataTable dtConsulta;
        DataTable dtCuentas;

        bool bRespuesta;
        
        Decimal dbTotalEntregado = 0;
        Decimal dbTotalPendiente = 0;
        Decimal dTotalPagadoP = 0;
        Decimal dSumaCuentasCobrar;
        Decimal dbPorcentajeIva;
        Decimal dbPorcentajeServicio;

        string sSql;
        string sFecha;
        string sTexto;
        string sFechaApertura;
        string sHoraApertura;
        string sFechaCierre;
        string sHoraCierre;
        string sTextoDesglose = "";
        string sTextoDevuelta;
        string sIvaCobrado;
        string sValorAhorroProductos;

        int iIdLocalidad;

        Decimal dbAhorroEmergencia;
        Decimal dbAhorroEmergenciaManual;
        Decimal dbTotalCobradoEfectivo;
        Decimal dbTotalCobradoTransferencias;
        Decimal dbTotalCobradoCheques;
        Decimal dbTotalCobradoTarjetas;
        Decimal dbTotalEntradasManuales;
        Decimal dbTotalSalidasManuales;

        //FUNCION PARA CONSULTAR FECHA Y HORA
        private bool consultarFechaHora()
        {
            try
            {
                sSql = "";
                sSql += "select fecha_apertura, hora_apertura, isnull(fecha_cierre, fecha_apertura) fecha_apertura," + Environment.NewLine;
                sSql += "isnull(hora_cierre, '') hora_cierre, porcentaje_iva, porcentaje_servicio, isnull(ahorro_emergencia, 0.00) ahorro_emergencia" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sFechaApertura = Convert.ToDateTime(dtConsulta.Rows[0][0].ToString()).ToString("dd/MM/yyyy");
                        sHoraApertura = dtConsulta.Rows[0][1].ToString();
                        sFechaCierre = Convert.ToDateTime(dtConsulta.Rows[0][2].ToString()).ToString("dd/MM/yyyy");

                        if (dtConsulta.Rows[0]["hora_cierre"].ToString() == "")
                        {
                            sHoraCierre = DateTime.Now.ToString("HH:mm:dd");
                        }

                        else
                        {
                            sHoraCierre = Convert.ToDateTime(dtConsulta.Rows[0]["hora_cierre"].ToString()).ToString("HH:mm:ss");
                        }

                        dbPorcentajeIva = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_iva"]);
                        dbPorcentajeServicio = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_servicio"]);
                        dbAhorroEmergenciaManual = Convert.ToDecimal(dtConsulta.Rows[0]["ahorro_emergencia"]);

                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
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
                sSql += "where fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A')" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select numero_pedido from cv403_numero_cab_pedido" + Environment.NewLine;
                sSql += "where id_pedido = (select max(id_pedido)from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //FUNCION PARA EXTRAER EL RANGO DE NOTAS DE VENTA HECHAS EN LA FECHA
        private void verRangoNotasVenta()
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
                sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = " + Program.iComprobanteNotaEntrega + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                sSql += "and F.fecha_factura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = " + Program.iComprobanteNotaEntrega + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                            sTexto = sTexto + "N. Venta desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[0][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                        }

                        else if (dtConsulta.Rows.Count > 1)
                        {
                            sTexto = sTexto + "N. Venta desde: " + dtConsulta.Rows[0][0].ToString().PadLeft(8, ' ') + "  hasta: " + dtConsulta.Rows[1][0].ToString().PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                }

                else
                {
                    sTexto = sTexto + "N. Venta desde:" + "".PadLeft(8, ' ') + "   hasta:" + "".PadLeft(8, ' ') + Environment.NewLine;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and XC.cg_estado_dcto = 7460" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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

        //Función para llenar el informa de arqueo de caja
        public string llenarInforme(string sFecha_P, int iIdLocalidad_P)
        {
            try
            {
                this.sFecha = sFecha_P;
                this.iIdLocalidad = iIdLocalidad_P;

                if (consultarFechaHora() == false)
                {
                    sTexto = "";
                }

                else
                {
                    sTexto = "";
                    sTexto = sTexto + "ARQUEO DE CAJA".PadLeft(27, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;
                    sTexto = sTexto + "Fecha:".PadRight(8, ' ') + sFechaApertura + " - " + sFechaCierre + Environment.NewLine;
                    sTexto = sTexto + "Desde las " + sHoraApertura + Environment.NewLine; //OBTENER DE POS_CIERRE_CAJERO
                    sTexto = sTexto + "Hasta las " + sHoraCierre + Environment.NewLine; //OBTENER DE POS_CIERRE_CAJERO
                    sTexto = sTexto + "Caja: <Todas>" + Environment.NewLine;
                    sTexto = sTexto + "Cajero: ".PadRight(8, ' ') + Program.sNombreCajero + Environment.NewLine + Environment.NewLine; //OBTENER DE POS_CIERRE_CAJERO

                    dbTotalCobradoEfectivo = calcularTotalPago("EF");
                    dbTotalCobradoTransferencias = calcularTotalPago("TR");
                    dbTotalCobradoCheques = calcularTotalPago("CH");
                    dbTotalCobradoTarjetas = calcularTotalPagoTarjetas();

                    sTexto = sTexto + "Total Vendido.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalCobradoTransferencias +dbTotalCobradoCheques + dbTotalCobradoTarjetas).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "Cobrado Efectivo..:".PadRight(30, ' ') + dbTotalCobradoEfectivo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "Cobrado Transferen:".PadRight(30, ' ') + dbTotalCobradoTransferencias.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "Cobrado Cheques...:".PadRight(30, ' ') + dbTotalCobradoCheques.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "Cobrado Tarjetas..:".PadRight(30, ' ') + dbTotalCobradoTarjetas.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    llenarDesgloseTarjetas();
                    sTexto = sTexto + "Total Cobrado.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalCobradoTransferencias + dbTotalCobradoCheques + dbTotalCobradoTarjetas).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    
                    //MOSTRAR VALORES EN CORTESIAS, VALES FUNCIONARIOS
                    //CONTAR LAS CORTESIA

                    sTextoDevuelta = extraerOtrosValores("04");

                    if (sTextoDevuelta != "")
                    {
                        sTexto = sTexto + "Ordenes Cortesias.:".PadRight(30, ' ') + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                    }

                    //VALES FUNCIONARIOS
                    sTextoDevuelta = extraerOtrosValores("05");

                    if (sTextoDevuelta != "")
                    {
                        sTexto = sTexto + "T. Funcionarios...:".PadRight(30, ' ') + sTextoDevuelta.PadLeft(10, ' ') + Environment.NewLine;
                    }

                    dbTotalEntradasManuales = sumarEntradasSalidasManuales(1);
                    dbTotalSalidasManuales = sumarEntradasSalidasManuales(0);

                    sTexto = sTexto + "Entradas Manuales.:".PadRight(30, ' ') + dbTotalEntradasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "Salidas Manuales..:".PadRight(30, ' ') + dbTotalSalidasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                    sTexto = sTexto + "Total Efectivo....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;

                    sTexto = sTexto + "Saldo en Caja.....:".PadRight(30, ' ') + (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                    sTexto = sTexto + "Total Entregado...:".PadRight(30, ' ') + dbTotalEntregado.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                    sTexto = sTexto + "Diferencia........:".PadRight(30, ' ') + (dbTotalEntregado - (dbTotalCobradoEfectivo + dbTotalEntradasManuales - dbTotalSalidasManuales)).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + "Total Pendiente...:".PadRight(30, ' ') + dbTotalPendiente.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

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
                    sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                    sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and estado_orden = 'Pagada'" + Environment.NewLine;
                    sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada;
                    
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

                    sIvaCobrado = "0.00";
                    extraerIva();
                    //sTexto = sTexto + "I.V.A. Cobrado....:".PadRight(30, ' ') + (dbTotalCobrado - (dbTotalCobrado / (1 + (dbPorcentajeIva/100)))).ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + "IVA en Facturas...:".PadRight(30, ' ') + sIvaCobrado.PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + "Personas Atendidas:".PadRight(30, ' ') + calcularTotalPersonas("01").ToString().PadLeft(10, ' ') + Environment.NewLine;
                    verRangoComandas();
                    verRangoFacturas();
                    verRangoNotasVenta();

                    string sCuentaCliente = cuentasClienteEmpresarial();

                    if ((sCuentaCliente != "ERROR") && (sCuentaCliente != ""))
                    {
                        sTexto += sCuentaCliente;
                    }

                    sTexto += Environment.NewLine;
                    consultaProductosAhorro();

                    //AQUI AHORRO DE EMERGENCIA
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "AHORRO DE EMERGENCIA".PadLeft(30, ' ') + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "AHORRO TOTAL EN PRODUCTOS:".PadRight(30, ' ') + sValorAhorroProductos.PadLeft(10, ' ') + Environment.NewLine;
                    sTexto += "AHORRO INGRESO MANUAL    :".PadRight(30, ' ') + dbAhorroEmergencia.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";
                }

                return sTexto;

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }
        
        //Función para calcular el total
        private Decimal calcularTotalPago(string sCodigo_P)
        {
            try
            {
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(sum(isnull(FP.valor, 0)), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_numero_cab_pedido NP ON CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_vw_pedido_forma_pago FP ON CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and FP.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
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
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Función para calcular el total de tarjetas
        private Decimal calcularTotalPagoTarjetas()
        {
            try
            {
                Decimal sumaTarjetas = 0;
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select FP.descripcion,sum(FP.valor) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.fecha_pedido ='" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo in('TC', 'TD')" + Environment.NewLine;
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
                        //MessageBox.Show(sumaTarjetas.ToString());
                        return sumaTarjetas;
                    }
                    else
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al calcular el total de tarjetas";
                ok.ShowDialog();
                return 0;
            }
        }

        //Función para mostrar las tarjetas de crédito
        private void llenarDesgloseTarjetas()
        {
            try
            {
                sSql = "";
                sSql += "select FP.descripcion,sum(FP.valor) valor, count (CP.id_pedido) Numero" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP,  pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado_orden= 'Pagada'" + Environment.NewLine;
                sSql += "and FP.codigo in ('TC','TD')" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                            sTexto = sTexto + " ".PadRight(6, ' ') + sNombreTarjeta.PadRight(15, '.') + ":" + dbValorTarjeta.ToString("N2").PadLeft(15, ' ') + Environment.NewLine;
                        }
                    }
                    else
                    {
                        sTexto = sTexto + "No hay datos para ser mostrados" + Environment.NewLine;
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Error al mostrar las tarjetas de crédito";
                    ok.ShowDialog();
                }


            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al mostrar las tarjetas de crédito";
                ok.ShowDialog();
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
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al calcular el numero de personas";
                ok.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA SACAR EL TOTAL DE CUENTAS POR COBRAR
        private Decimal sumarCuentasPorCobrar()
        {
            try
            {
                sSql = "";
                sSql += "select XC.id_pedido, CP.fecha_pedido" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar XC, cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where XC.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and XC.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and XC.cg_estado_dcto = 7460";

                dtCuentas = new DataTable();
                dtCuentas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCuentas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }

                

                if (dtCuentas.Rows.Count > 0)
                {
                    dSumaCuentasCobrar = 0;

                    for (int i = 0; i < dtCuentas.Rows.Count; i++)
                    {
                        sSql = "";
                        sSql += "select ltrim(str(isnull(sum(cantidad * (precio_unitario + valor_iva + valor_otro - valor_dscto)), 0), 10, 2)) suma" + Environment.NewLine;
                        sSql += "from pos_vw_det_pedido " + Environment.NewLine;
                        sSql += "where id_pedido = " + Convert.ToInt32(dtCuentas.Rows[i]["id_pedido"]) + Environment.NewLine;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            dSumaCuentasCobrar = dSumaCuentasCobrar + Convert.ToDecimal(dtConsulta.Rows[0]["suma"]);
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            dSumaCuentasCobrar = dSumaCuentasCobrar + 0;
                        }
                    }

                    return dSumaCuentasCobrar;
                }

                else
                {
                    return 0;
                }
            }

            catch(Exception)
            {
                ok.LblMensaje.Text = "Error al extraer los valores de las cuentas por cobrar.";
                ok.ShowDialog();
                return 0;
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

                sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and fecha = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada;

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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
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
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and P.ahorro_emergencia = 1" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                //sSql += "and CP.id_localidad = " + Program.iIdLocalidad;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    sValorAhorroProductos = dtConsulta.Rows[0][0].ToString();
                }
                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public string extraerOtrosValores(string sCodigo)
        {
            try
            {
                sTextoDesglose = "";
                dTotalPagadoP = 0;

                sSql = "";
                sSql += "select isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_dscto)), 0) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "pos_origen_orden OO" + Environment.NewLine;
                sSql += "where OO.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and OO.codigo = '" + sCodigo + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotalPagadoP = Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());

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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }

        //FUNCION PARA EXTRAER EL IVA COBRADO
        private void extraerIva()
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
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sIvaCobrado = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = "No se pudo extraer el total del IVA cobrado." + Environment.NewLine + "Comuníquese con el administrador del sistema.";
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
                
    }
}
