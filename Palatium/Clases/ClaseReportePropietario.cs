using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReportePropietario
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta;
        DataTable dtPagosDesglose;
        
        string sSql;
        string sTexto;
        string sFecha;
        string sRetorno;
        string sTextoDevuelta;
        string sNombreProducto;

        bool bRespuesta;

        int iIdLocalidad;

        Decimal dbAhorroEmergencia;
        Decimal dbAhorroEmergenciaManual;
        Decimal dbTotalCobradoEfectivo;
        Decimal dbTotalCobradoTransferencias;
        Decimal dbTotalCobradoCheques;
        Decimal dbTotalCobradoTarjetas;
        Decimal dbTotalEntradasManuales;
        Decimal dbTotalSalidasManuales;
        Decimal dbIvaCobrado;
        Decimal dbTotalVendido;
        Decimal dbTotalEntrega;
        Decimal dbTotalCortesia;
        Decimal dbPrecioProducto;
        //------------------------------------------------------------------

        public string crearReporte(string sFecha_P, int iIdLocalidad_P)
        {
            try
            {
                this.sFecha = sFecha_P;
                this.iIdLocalidad = iIdLocalidad_P;

                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE ENTREGA DE EFECTIVO".PadLeft(35, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                
                sTextoDevuelta = extraerDatosCierre();

                if (sTextoDevuelta == "ERROR")
                {
                    return "ERROR";
                }

                if (sTextoDevuelta == "VACIO")
                {
                    return sTextoDevuelta;
                }

                sTexto += sTextoDevuelta;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                //EXTRAER LOS PAGOS
                //------------------------------------------------------------------------------
                dbTotalCobradoEfectivo = calcularTotalPago("EF");
                dbTotalCobradoTransferencias = calcularTotalPago("TR");
                dbTotalCobradoCheques = calcularTotalPago("CH");
                dbTotalCobradoTarjetas = calcularTotalPagoTarjetas();

                dbTotalEntradasManuales = sumarEntradasSalidasManuales(1);
                dbTotalSalidasManuales = sumarEntradasSalidasManuales(0);

                //dbIvaCobrado = extraerIva();
                dbAhorroEmergencia = consultaProductosAhorro();

                dbTotalVendido = dbTotalCobradoEfectivo + dbTotalCobradoTransferencias + dbTotalCobradoCheques + dbTotalCobradoTarjetas;

                dbTotalEntrega = dbTotalVendido + dbTotalEntradasManuales - dbTotalSalidasManuales -
                                 dbAhorroEmergencia - dbAhorroEmergenciaManual;

                //dbTotalEntrega = dbTotalVendido + dbTotalEntradasManuales - (dbTotalCobradoTransferencias + 
                //                 dbTotalCobradoCheques + dbTotalCobradoTarjetas + dbTotalSalidasManuales + 
                //                 dbAhorroEmergencia + dbAhorroEmergenciaManual);
                //------------------------------------------------------------------------------

                sTexto += "COBROS REALIZADOS".PadLeft(28, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "* TOTAL EFECTIVO".PadRight(30, '_') + dbTotalCobradoEfectivo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "* TOTAL CHEQUES".PadRight(30, '_') + dbTotalCobradoCheques.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "* TOTAL TRANSFERENCIA".PadRight(30, '_') + dbTotalCobradoTransferencias.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "* TOTAL TARJETAS".PadRight(30, '_') + dbTotalCobradoTarjetas.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL VENDIDO :" + dbTotalVendido.ToString("N2").PadLeft(25, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                
                sTexto += "+ INGRESOS MANUALES".PadRight(30, '_') + dbTotalEntradasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "- SALIDAS MANUALES".PadRight(30, '_') + dbTotalSalidasManuales.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                //sTexto += "- IVA REPORTADO".PadRight(30, '_') + dbIvaCobrado.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "- PRODUCTOS AHORRO EMERGENCIA".PadRight(30, '_') + dbAhorroEmergencia.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "- AHORRO EMERGENCIA CAJA".PadRight(30, '_') + dbAhorroEmergenciaManual.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL A ENTREGAR:" + dbTotalEntrega.ToString("N2").PadLeft(23, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "SECCION INFORMATIVA".PadLeft(29, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                sTextoDevuelta = extraerValoresCortesias("04");

                if (sTextoDevuelta == "ERROR")
                {
                    return "ERROR";
                }

                sTexto += sTextoDevuelta + Environment.NewLine + Environment.NewLine + ".";

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER LOS DATOS DE LA CAJA
        private string extraerDatosCierre()
        {
            try
            {
                sRetorno = "";

                sSql = "";
                sSql += "select CC.fecha_apertura, CC.hora_apertura, isnull(CC.fecha_cierre, GETDATE()) fecha_cierre," + Environment.NewLine;
                sSql += "isnull(CC.hora_cierre, '') hora_cierre, isnull(CC.ahorro_emergencia, 0) ahorro_emergencia," + Environment.NewLine;
                sSql += "J.descripcion jornada, C.descripcion cajero" + Environment.NewLine;
                sSql += "from pos_cierre_cajero CC INNER JOIN" + Environment.NewLine;
                sSql += "pos_jornada J ON J.id_pos_jornada = CC.id_jornada" + Environment.NewLine;
                sSql += "and J.estado = 'A'" + Environment.NewLine;
                sSql += "and CC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_cajero C ON C.id_pos_cajero  = CC.id_cajero" + Environment.NewLine;
                sSql += "and C.estado = 'A'" + Environment.NewLine;
                sSql += "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and id_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "VACIO";
                }

                sRetorno += "FECHA APERTURA: " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("yyyy-MM-dd") + " " + dtConsulta.Rows[0]["hora_apertura"].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0]["hora_cierre"].ToString() == "")
                {
                    sRetorno += "FECHA CIERRE  : " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine;
                }
                else
                {
                    sRetorno += "FECHA CIERRE  : " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre"].ToString()).ToString("yyyy-MM-dd") + " " + dtConsulta.Rows[0]["hora_cierre"].ToString() + Environment.NewLine;
                }

                sRetorno += "CAJERO        : " + dtConsulta.Rows[0]["cajero"].ToString() + Environment.NewLine;
                sRetorno += "JORNADA       : " + dtConsulta.Rows[0]["jornada"].ToString() + Environment.NewLine;

                dbAhorroEmergenciaManual = Convert.ToDecimal(dtConsulta.Rows[0]["ahorro_emergencia"].ToString());

                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER EL TOTAL DE EFECTIVO, TARHETAS Y TRANSFERENCIAS
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

        //FUNCION PARA EXTRAER EL TOTAL DE TARJETAS DE CREDITO Y DEBITO
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

            catch (Exception)
            {
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
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where O.genera_factura = 1" + Environment.NewLine;
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
                        return Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = "No se pudo extraer el total del IVA cobrado." + Environment.NewLine + "Comuníquese con el administrador del sistema.";
                        catchMensaje.ShowDialog();
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
        private Decimal consultaProductosAhorro()
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
                    return Convert.ToDecimal(dtConsulta.Rows[0]["suma_ahorro"].ToString());
                }
                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        //FUNCION PARA CARGAR EL TOTAL EN CORTESIAS
        private string extraerValoresCortesias(string sCodigo_P)
        {
            try
            {
                sRetorno = "";
                dbTotalCortesia = 0;

                sSql = "";
                sSql += "select NP.nombre, ltrim(str(isnull((DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_dscto + valor_otro)), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and O.codigo = '" + sCodigo_P + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sRetorno += "LISTA DE PRODUCTOS CORTESIA".PadLeft(32, ' ') + Environment.NewLine;
                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sNombreProducto = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();
                            dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString());
                            dbTotalCortesia += dbPrecioProducto;

                            if (sNombreProducto.Length > 30)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 30);
                            }

                            sRetorno += sNombreProducto.PadRight(30, ' ') + dbPrecioProducto.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        }

                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TOTAL EN CORTESIAS:".PadRight(30, ' ') + dbTotalCortesia.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        sRetorno += "".PadLeft(40, '-');

                    }

                    else
                    {
                        sRetorno += "TOTAL EN CORTESIAS:".PadRight(30, ' ') + dbTotalCortesia.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        sRetorno += "".PadLeft(40, '-');
                    }

                    return sRetorno;
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
    }
}
