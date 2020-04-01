using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseCierreCajero
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        DataTable dtConsulta;
        DataTable dtContar;
        DataTable dtSumaDetalle;
        DataTable dtTipoVenta;
        bool bRespuesta = false;

        string sTexto = "";
        string sTextoCuentas;
        string sFecha;
        string sNombreEmpresa;
        string sValorEmpresa;
        string sRetorno_P;
        string sValorAhorroProductos;
        string sCajaInicial = "0.00";
        string sCajaFinal = "0.00";

        int iIdTipoVenta;
        int iContador;
        int iIdLocalidad;
        int iIdCierreCajero;
        
        object iCuentaRegistros;

        double dSumaTotalProductosCancelados;
        double dSumaTotalCuentasAnuladas;
        double dSumaTotalCuentasPorCobrar;
        double dSumaTotalCortesias;
        double dSumaTotalOrdenasCanceladas;
        double dSumaPagos;
        double dbSumaDescuentos;
        double dbPorcentajeIva;
        double dbPorcentajeServicio;

        Decimal dbTotalCuentasPorCobrar;
        Decimal dbValorEmpresa_P;
        Decimal dbAhorroEmergencia;
        Decimal dbTotalEntradasManuales;
        Decimal dbTotalSalidasManuales;

        #region FUNCIONES DEL USUARIO PARA COMPLETAR EL CIERRE DE CAJERO

        //FUNCION PARA CARGAR LOS TIPOS DE VENTA
        private void cargarTipoVenta()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_venta, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_venta" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_tipo_venta";

                dtTipoVenta = new DataTable();
                dtTipoVenta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtTipoVenta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA DETALLAR LAS FORMAS DE PAGOS RECIBIDAS
        private void llenarPagos()
        {
            try
            {
                dSumaPagos = 0;
                iContador = 0;

                cargarTipoVenta();

                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(sum(FP.valor),10,2)) valor," + Environment.NewLine;
                sSql += "FP.cg_estado_dcto, FP.cg_tipo_documento, FP.id_pos_tipo_venta," + Environment.NewLine;
                sSql += "FP.descripcion_venta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTipoVenta.Rows.Count; i++)
                        {                            
                            iIdTipoVenta = Convert.ToInt32(dtTipoVenta.Rows[i]["id_pos_tipo_venta"]);

                            //CONTAR REGISTROS DEL DATATABLE
                            //iCuentaRegistros = dtConsulta.AsEnumerable().Count(x => x.Field<int>("id_pos_tipo_venta") == iIdTipoVenta);
                            iCuentaRegistros = dtConsulta.Compute("Count(id_pos_tipo_venta)", "id_pos_tipo_venta = " + iIdTipoVenta);

                            if (Convert.ToInt32(iCuentaRegistros) == 0)
                            {
                                goto continuar;
                            }

                            sTexto += dtTipoVenta.Rows[i]["descripcion"] + Environment.NewLine + Environment.NewLine;

                            for (int j = 0; j < dtConsulta.Rows.Count; j++)
                            {
                                if (Convert.ToInt32(dtConsulta.Rows[j]["id_pos_tipo_venta"]) == iIdTipoVenta)
                                {
                                    sTexto += (dtConsulta.Rows[j]["descripcion"] + ":").PadRight(30, ' ') +
                                               dtConsulta.Rows[j]["valor"].ToString().PadLeft(10, ' ') + Environment.NewLine;

                                    dSumaPagos = dSumaPagos + Convert.ToDouble(dtConsulta.Rows[j]["valor"].ToString());
                                    iContador++;
                                }                                
                            }

                            if (iContador != 0)
                            {
                                sTexto += Environment.NewLine;
                                sTexto += ("TOTAL " + dtTipoVenta.Rows[i]["descripcion"].ToString()).PadRight(30, ' ') + dSumaPagos.ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                                dSumaPagos = 0;
                            }

                            iContador = 0;

                        continuar: { }
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA DETALLAR LOS TIPOS DE ORDENES
        private void llenarOrdenes()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and cuenta_por_cobrar = 0" + Environment.NewLine;
                sSql += "and pago_anticipado = 0";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        sSql = "";
                        sSql += "select count(*) cuenta" + Environment.NewLine;
                        sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                        sSql += "where fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                        sSql += "and estado = 'A'" + Environment.NewLine;
                        sSql += "and estado_orden = 'Pagada'" + Environment.NewLine;
                        sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                        sSql += "and id_pos_origen_orden = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                        sSql += "and id_localidad = " + iIdLocalidad;

                        dtContar = new DataTable();
                        dtContar.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtContar, sSql);

                        if (bRespuesta == true)
                        {
                            sSql = "";
                            sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) suma" + Environment.NewLine;
                            sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                            sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                            sSql += "and CP.estado = 'A'" + Environment.NewLine;
                            sSql += "and DP.estado = 'A'" + Environment.NewLine;
                            sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                            sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                            sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                            sSql += "and CP.id_pos_origen_orden = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                            sSql += "and CP.id_localidad = " + iIdLocalidad;

                            dtSumaDetalle = new DataTable();
                            dtSumaDetalle.Clear();

                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSumaDetalle, sSql);

                            if (bRespuesta == true)
                            {
                                sTexto += ((dtConsulta.Rows[i][1].ToString() + ":").PadRight(21, ' ') + 
                                          dtContar.Rows[0][0].ToString().PadLeft(6, ' ') + 
                                          dtSumaDetalle.Rows[0][0].ToString().PadLeft(13, ' ')) + Environment.NewLine;
                            }

                            else
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el total de personas que ocupan las mesas
        private double calcularTotalPersonas(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(CP.numero_personas),0) numero" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_origen_orden ORI" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
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

        //Función para calcular el valor de formas de pago
        private double calcularTotalPago(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                        return Convert.ToDouble(dtConsulta.Rows[0][1].ToString());
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
        
        //Función para calcular las propinas
        private double calcularPropinas()
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
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                        return Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
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

        //Función para calcular el total de descuentos
        private double calcularDescuentos()
        {
            try
            {
                dbSumaDescuentos = 0;

                sSql = "";
                sSql += "select ltrim(str(isnull(DP.cantidad,0), 10, 2)) cantidad, ltrim(str(isnull(DP.valor_dscto,0), 10, 2)) valor_dscto" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                sSql += "where CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                            dbSumaDescuentos = dbSumaDescuentos + (Convert.ToDouble(dtConsulta.Rows[i][0].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][1].ToString()));
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

        //==================================================================================================================

        //funcion para llenar las cuentas canceladas
        private void llenarCuentasCanceladas()
        {
            try
            {
                sSql = "";
                sSql += "select CP.id_pedido, CP.cuenta, CP.estado_orden," + Environment.NewLine;
                sSql += "c.motivo_cancelacion, isnull(CP.valor_cancelado, 0) valor_cancelado" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos as CP, pos_cancelacion AS C" + Environment.NewLine;
                sSql += "where C.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cancelada'" + Environment.NewLine;
                sSql += "and CP.estado = 'N'" + Environment.NewLine;
                sSql += "and CP.fecha_orden = '" + sFecha + "'" + Environment.NewLine;
                //sSql += "and CP.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    double suma = 0;
                    Double valor;
                    if (dtConsulta.Rows.Count != 0)
                        //dgvCierre.Rows.Add("Cuentas Canceladas:");

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            valor = Convert.ToDouble(dtConsulta.Rows[i][4].ToString());
                            //dgvCierre.Rows.Add("No. Orden: " + dtConsulta.Rows[i][0].ToString(), "" + "\t Valor de la Orden: " + valor.ToString("N2"));
                            //dgvCierre.Rows.Add(" Motivo: ", " " + dtConsulta.Rows[i][3].ToString());
                            suma = suma + valor;
                        }
                    //dSumaTotalProductosCancelados = 0;
                    dSumaTotalCuentasAnuladas = suma;
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    dSumaTotalProductosCancelados = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                dSumaTotalProductosCancelados = 0;
            }
        }

        //Función para llenar los productos cancelados
        private void llenarProductosCancelados()
        {
            try
            {
                sSql = "";
                sSql += "select NP.nombre, CAN.motivo_cancelacion," + Environment.NewLine;
                sSql += "DP.precio_unitario, DP.cantidad," + Environment.NewLine;
                sSql += "ORI.descripcion, CP.porcentaje_iva, CP.porcentaje_servicio" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_cancelacion_productos CAN ON DP.id_det_pedido = CAN.id_det_pedido INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON CP.id_pos_origen_orden = ORI.id_pos_origen_orden INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

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
                            dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[i][5].ToString());
                            dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[i][6].ToString());
                            suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * (1 + (dbPorcentajeIva/100) + (dbPorcentajeServicio/100));
                        }
                    }
                        
                    dSumaTotalProductosCancelados = 0;
                    dSumaTotalProductosCancelados = suma;
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    dSumaTotalProductosCancelados = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                dSumaTotalProductosCancelados = 0;
            }
        }

        //Función para llenar los productos de cortesía
        private void llenarProductosCortesia()
        {
            try
            {
                sSql = "";
                sSql += "select NP.nombre, CORT.motivo_cortesia, DP.precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, ORI.descripcion, CP.porcentaje_iva, CP.porcentaje_descuento" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON CP.id_pos_origen_orden = ORI.id_pos_origen_orden INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_cortesia CORT ON DP.id_det_pedido = CORT.id_det_pedido" + Environment.NewLine;
                sSql += "and CORT.estado ='A'" + Environment.NewLine;
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    double suma = 0;
                    if (dtConsulta.Rows.Count != 0)
                        //dgvCierre.Rows.Add("Productos de Cortesía:");

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[i][5].ToString());
                            dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[i][6].ToString());
                            suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * (1 + (dbPorcentajeIva/100) + (dbPorcentajeServicio/100));
                        }

                    dSumaTotalCortesias = 0;
                    dSumaTotalCortesias = suma;
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    dSumaTotalCortesias = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                dSumaTotalCortesias = 0;
            }
        }


        //Función para calcular el valor de las tarjetas de crédito
        private double calcularTotalPagoTarjetas()
        {
            try
            {
                double sumaTarjetas = 0;
                sSql = "";
                sSql += "select FP.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.fecha_pedido ='" + sFecha + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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
                            sumaTarjetas += Convert.ToDouble(dtConsulta.Rows[i][1].ToString());
                        }
                        return sumaTarjetas;
                    }
                    else
                    {
                        return 0.00;
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0.00;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0.00;
            }
        }

        //FUNCION PARA  LAS CUENTAS POR COBRAR
        private string cargarCuentasPorCobrar()
        {
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
            sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
            sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
            sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
            //sSql += "and CP.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
            sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
            sSql += "group by TP.nombres, TP.apellidos";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == false)
            {
                return "ERROR";
            }

            sTextoCuentas = "";

            if (dtConsulta.Rows.Count > 0)
            {
                dbTotalCuentasPorCobrar = 0;
                sTextoCuentas += "CLIENTE EMPRESARIAL" + Environment.NewLine;
                sTextoCuentas += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNombreEmpresa = dtConsulta.Rows[i]["empresa"].ToString().Trim();
                    sValorEmpresa = dtConsulta.Rows[i]["valor"].ToString().Trim();
                    dbValorEmpresa_P = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());

                    if (sNombreEmpresa.Length > 32)
                    {
                        sNombreEmpresa = sNombreEmpresa.Substring(0, 32);
                    }

                    sTextoCuentas += sNombreEmpresa.PadRight(32, ' ') + sValorEmpresa.PadLeft(8, ' ') + Environment.NewLine;
                    dbTotalCuentasPorCobrar += dbValorEmpresa_P;
                }

                sTextoCuentas += "".PadLeft(40, '-') + Environment.NewLine;
                sTextoCuentas += "TOTAL CLIENTE EMPRESARIAL" + dbTotalCuentasPorCobrar.ToString("N2").PadLeft(15, ' ') + Environment.NewLine;
            }

            return sTextoCuentas;
        }

        //FUNCION PARA LAS TARJETAS DE ALMUERZOS
        private string cargarCuentasTarjetaAlmuerzos()
        {
            sSql = "";
            sSql += "select O.descripcion, count(*) cuenta," + Environment.NewLine;
            sSql += "ltrim(str(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 10, 2)) valor" + Environment.NewLine;
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
            sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
            sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
            sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
            sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
            sSql += "group by O.descripcion";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == false)
            {
                return "ERROR";
            }

            sTextoCuentas = "";

            if (dtConsulta.Rows.Count > 0)
            {
                sTextoCuentas += dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper().PadRight(21, ' ') + 
                                 dtConsulta.Rows[0]["cuenta"].ToString().Trim().PadLeft(6, ' ') + dtConsulta.Rows[0]["valor"].ToString().Trim().PadLeft(13, ' ');
            }

            return sTextoCuentas;
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
                    sValorAhorroProductos= dtConsulta.Rows[0][0].ToString();
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

        //FUNCION PARA CONSULTAR LOS VALORES DE LA CAJA INICIAL Y FINAL
        private void consultarValoresCaja()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(caja_inicial, 0), 10, 2)) caja_inicial," + Environment.NewLine;
                sSql += "ltrim(str(isnull(caja_final, 0), 10, 2)) caja_final" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and id_jornada = " + Program.iJornadaRecuperada;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    sCajaInicial = dtConsulta.Rows[0]["caja_inicial"].ToString();
                    sCajaFinal = dtConsulta.Rows[0]["caja_final"].ToString();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONTAR LAS MONEDAS
        private string recuperarMonedas()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_monedas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and tipo_ingreso = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Decimal iMonedas;
                        Decimal dbValorCalculo;
                        Decimal dbSumaMonedas_P = 0;

                        string sMoneda = "";
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
                        return sMoneda;
                    }

                    else
                    {
                        return "";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return "";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
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
                //sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada;

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


        #endregion


        public string llenarCierreCajero(string sFecha_P, int iIdLocalidad_P, Decimal dbAhorroEmergencia_R, int iIdCierreCajero_P)
        {
            try
            {
                this.sFecha = sFecha_P;
                this.iIdLocalidad = iIdLocalidad_P;
                this.dbAhorroEmergencia = dbAhorroEmergencia_R;
                this.iIdCierreCajero = iIdCierreCajero_P;

                sTexto = "";
                sTexto += Environment.NewLine;
                sTexto += ("INFORME DEL CIERRE DEL CAJERO".PadLeft(34, ' ')) + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                //sTexto += Environment.NewLine;
                sTexto += ("LOCAL: ".PadRight(9, ' ') + Program.local) + Environment.NewLine;
                sTexto += ("FECHA: ".PadRight(9, ' ') + sFecha) + Environment.NewLine;

                if (Program.horaSalida != "")
                {
                    sTexto += ("HORA: ".PadRight(9, ' ') + Program.horaSalida) + Environment.NewLine;
                }

                if (Program.iManejaJornada == 1)
                {
                    sTexto += ("JORNADA: ".PadRight(9, ' ') + Program.iJornadaRecuperada) + Environment.NewLine;
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                consultarValoresCaja();

                sTexto += "VALOR CAJA INICIAL: " + sCajaInicial + Environment.NewLine;

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += Environment.NewLine;
                sTexto += "TOTALES" + Environment.NewLine;
                sTexto += Environment.NewLine;
                sTexto += "NUMERO DE PERSONAS ATENDIDADS:".PadRight(30, ' ') + calcularTotalPersonas("01").ToString().PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                sTexto += "TIPOS DE ORDENES DEL SISTEMA:" + Environment.NewLine + Environment.NewLine;
                llenarOrdenes();
                sRetorno_P = cargarCuentasTarjetaAlmuerzos();

                if (sRetorno_P == "ERROR")
                {
                    sTexto += "ERROR AL CARGAR LAS TARJETAS DE ALMUERZO." + Environment.NewLine;
                }

                else
                {
                    sTexto += sRetorno_P + Environment.NewLine;
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sRetorno_P = cargarCuentasPorCobrar();

                if (sRetorno_P == "ERROR")
                {
                    sTexto += "ERROR AL CARGAR LAS CUENTAS POR COBRAR.";
                }

                else
                {
                    sTexto += sRetorno_P;
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                sTexto += Environment.NewLine;
                llenarPagos();
                //sTexto += ("TOTAL EFECTIVO:".PadRight(26, ' ') + calcularTotalPago(1).ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                //sTexto += ("TOTAL CHEQUES:".PadRight(26, ' ') + calcularTotalPago(2).ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                //sTexto += ("TOTAL TRANSFERENCIAS:".PadRight(26, ' ') + calcularTotalPago(10).ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                //sTexto += ("TOTAL TARJETAS:".PadRight(26, ' ') + calcularTotalPagoTarjetas().ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                //sTexto += ("TOTAL DINERO ELECTRONICO:".PadRight(26, ' ') + calcularTotalPago(11).ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                //sTexto += Environment.NewLine;
                //sTexto += ("TOTAL ACTIVIDADES:".PadRight(26, ' ') + ((calcularTotalPago(1) + calcularTotalPago(2) + calcularTotalPago(10) + calcularTotalPago(11) + calcularTotalPagoTarjetas())).ToString("N2").PadLeft(14, ' ')) + Environment.NewLine;
                sTexto += Environment.NewLine;
                //sTexto += "".PadLeft(40, '-') + Environment.NewLine;
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
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                double total = 0;
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += ("PRODUCTOS DE CORTESIA SIN IMPUESTOS:") + Environment.NewLine;
                        sTexto += Environment.NewLine;
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {

                            total = total + (Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][3].ToString()));
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
                sSql += "where CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;                
                sSql += "and CP.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
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

                            total = total + Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
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
                sSql += "and CP.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
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
                        sTexto += ("ORDENES CANCELADAS: ".PadRight(30, ' '))  + Environment.NewLine;
                        sTexto += ("****************************************") + Environment.NewLine;

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

                //AQUI AHORRO DE EMERGENCIA
                sTexto += Environment.NewLine;
                consultaProductosAhorro();    
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "AHORRO DE EMERGENCIA".PadLeft(30, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "AHORRO TOTAL EN PRODUCTOS:".PadRight(30, ' ') + sValorAhorroProductos.PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "AHORRO INGRESO MANUAL    :".PadRight(30, ' ') + dbAhorroEmergencia.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "VALOR CAJA FINAL: " + sCajaFinal + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine;
                sTexto += recuperarMonedas();
                sTexto += Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

                return sTexto;
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }
    }
}
