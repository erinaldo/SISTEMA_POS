using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Areas
{
    class ClaseCombinarComandas_V2
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseLimpiarArreglos limpiar;

        string sSql;
        string sTabla;
        string sCampo;
        public string sRespuesta;

        int iIdPedidoContenedor;
        int iBandera2;
        int iCuenta;
        int p, q;
        int iIdDetPedido;
        int iIdLocalidadBodega;
        int iIdBodegaInsumos;
        int iValorActualMovimiento;
        int iIdCabeceraMovimiento;
        int iIdPosReceta;
        int iIdPosSubReceta;
        int iIdDocumentoCobrar;
        int iIdEventoCobro;
        int iIdPago;
        int iIdDocumentoPago;
        int iIdPosMovimientoCaja;
        int iIdCabDespacho;
        int iIdDespachoPedido;
        
        string sCodigo;
        string sAnioCorto;
        string sAnio;
        string sMesCorto;
        string sFecha;
        string sMes;
        string sHistoricoOrden;
        string sNumeroMovimientoSecuencial;
        string sNombreSubReceta;

        long iMaximo;

        DataTable dtConsulta;
        DataTable dtComandaCombinada;
        DataTable dtInformacion;
        DataTable dtLocalidad;
        DataTable dtReceta;
        DataTable dtSubReceta;
        DataTable dtAyuda;

        bool bRespuesta;

        //VARIABLES PARA CREAR LA INSTRUCCION DE CV403_DET_PEDIDOS
        Double dPrecioUnitario_P;
        Double dCantidad_P;
        Double dDescuento_P;
        Double dbPorcentajePorLinea_P;
        Double dServicio;
        Double dValorDescuento;
        Double dIVA_P;
        Double dbTotal;

        int iIdProducto_P;
        int iPagaIva_P;
        int iBanderaCortesia_P;
        int iBanderaDescuento_P;
        int iBanderaComentario_P;
        int iIdMascaraItem;
        int iSecuenciaEntrega_P;
        int iSecuenciaImpresion_P;

        string sMotivoCortesia_P;
        string sMotivoDescuento_P;
        string sCodigoProducto_P;
        string sNombreProducto_P;
        string sGuardarComentario;

        public bool recibirInformacion(DataTable dtInformacion_P, int iIdPedidoContenedor_P, string sHistoricoOrden_P)
        {
            try
            {
                this.dtInformacion = dtInformacion_P;
                this.iIdPedidoContenedor = iIdPedidoContenedor_P;
                this.sHistoricoOrden = sHistoricoOrden_P;

                if (crearDataTable() == false)
                {
                    return false;
                }

                if (llenarTablaCombinar() == false)
                {
                    return false;
                }

                if (recuperarDatosLocalidad() == false)
                {
                    return false;
                }

                if (actualizarComanda() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA CREAR EL DATATABLE 
        private bool crearDataTable()
        {
            try
            {
                dtComandaCombinada = new DataTable();
                dtComandaCombinada.Clear();
                dtComandaCombinada.Columns.Add("id_producto");
                dtComandaCombinada.Columns.Add("cantidad");
                dtComandaCombinada.Columns.Add("precio_unitario");
                dtComandaCombinada.Columns.Add("valor_dscto");
                dtComandaCombinada.Columns.Add("valor_ice");
                dtComandaCombinada.Columns.Add("valor_iva");
                dtComandaCombinada.Columns.Add("valor_otro");
                dtComandaCombinada.Columns.Add("comentario");
                dtComandaCombinada.Columns.Add("id_pos_mascara_item");
                dtComandaCombinada.Columns.Add("id_pos_secuencia_entrega");
                dtComandaCombinada.Columns.Add("bandera_cortesia");
                dtComandaCombinada.Columns.Add("motivo_cortesia");
                dtComandaCombinada.Columns.Add("bandera_descuento");
                dtComandaCombinada.Columns.Add("motivo_descuento");
                dtComandaCombinada.Columns.Add("porcentaje_descuento_info");
                dtComandaCombinada.Columns.Add("codigo_producto");
                dtComandaCombinada.Columns.Add("secuencia");

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA LLENAR LA TABLA
        private bool llenarTablaCombinar()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto, isnull(sum(cantidad), 0) cantidad, precio_unitario," + Environment.NewLine;
                sSql += "valor_dscto, valor_ice, valor_iva, valor_otro, comentario, id_pos_mascara_item," + Environment.NewLine;
                sSql += "id_pos_secuencia_entrega, bandera_cortesia, motivo_cortesia, bandera_descuento," + Environment.NewLine;
                sSql += "motivo_descuento, porcentaje_descuento_info, codigo_producto, secuencia" + Environment.NewLine;
                sSql += "from pos_vw_detalle_comanda_combinas" + Environment.NewLine;
                sSql += "where id_pedido in (";

                for (int a = 0; a < dtInformacion.Rows.Count; a++)
                {
                    sSql += dtInformacion.Rows[a]["id_pedido"].ToString();

                    if (a + 1 == dtInformacion.Rows.Count)
                        sSql += ")" + Environment.NewLine;
                    else
                        sSql += ", ";
                }

                sSql += "group by id_producto, precio_unitario, valor_dscto, valor_ice, valor_iva," + Environment.NewLine;
                sSql += "valor_otro, comentario, id_pos_mascara_item, id_pos_secuencia_entrega," + Environment.NewLine;
                sSql += "bandera_cortesia, motivo_cortesia, bandera_descuento, motivo_descuento," + Environment.NewLine;
                sSql += "porcentaje_descuento_info, codigo_producto, secuencia";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                for (int j = 0; j < dtConsulta.Rows.Count; j++)
                {
                    dtComandaCombinada.Rows.Add(dtConsulta.Rows[j]["id_producto"].ToString(),
                                                dtConsulta.Rows[j]["cantidad"].ToString(),
                                                dtConsulta.Rows[j]["precio_unitario"].ToString(),
                                                dtConsulta.Rows[j]["valor_dscto"].ToString(),
                                                dtConsulta.Rows[j]["valor_ice"].ToString(),
                                                dtConsulta.Rows[j]["valor_iva"].ToString(),
                                                dtConsulta.Rows[j]["valor_otro"].ToString(),
                                                dtConsulta.Rows[j]["comentario"].ToString(),
                                                dtConsulta.Rows[j]["id_pos_mascara_item"].ToString(),
                                                dtConsulta.Rows[j]["id_pos_secuencia_entrega"].ToString(),
                                                dtConsulta.Rows[j]["bandera_cortesia"].ToString(),
                                                dtConsulta.Rows[j]["motivo_cortesia"].ToString(),
                                                dtConsulta.Rows[j]["bandera_descuento"].ToString(),
                                                dtConsulta.Rows[j]["motivo_descuento"].ToString(),
                                                dtConsulta.Rows[j]["porcentaje_descuento_info"].ToString(),
                                                dtConsulta.Rows[j]["codigo_producto"].ToString(),
                                                dtConsulta.Rows[j]["secuencia"].ToString()
                                                );
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA ACTUALIZAR LA ORDEN
        private bool actualizarComanda()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    sRespuesta = "Error al abrir transacción";
                    return false;
                }

                if (eliminarMovimientosBodega(iIdPedidoContenedor) == false)
                {
                    return false;
                }

                //QUERY PARA ACTUALIZAR LA ORDEN EN CASO DE QUE SOLICITEN CONSUMO DE ALIMENTOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0," + Environment.NewLine;
                sSql += "estado_orden = 'Abierta'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoContenedor + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoContenedor + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                //QUERY PARA BUSCAR LOS DETALLES DE LOS ITEMS DEL PEDIDO Y PONERLOS EN ESTADO 'E'
                sSql = "";
                sSql += "select DPD.* from cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP, pos_det_pedido_detalle DPD" + Environment.NewLine;
                sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DPD.id_det_pedido = DP.id_det_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DPD.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + iIdPedidoContenedor;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        //QUERY PARA CAMBIAR A ESTADO 'E' LOS DETALLES DE LOS ITEMS DE LA ORDEN
                        sSql = "";
                        sSql += "update pos_det_pedido_detalle set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pos_det_pedido_detalle = " + Convert.ToInt32(dtConsulta.Rows[i]["id_pos_det_pedido_detalle"].ToString()) + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        //EJECUCION DE INSTRUCCION SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                            return false;
                        }
                    }
                }

                if (insertarDetPedidos() == false)
                {
                    return false;
                }

                //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "valor = " + dbTotal + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoContenedor + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                //INVOCAR A FUNCION PARA ELIMINAR LAS COMANDAS A ELIMINAR
                for (int j = 0; j < dtInformacion.Rows.Count; j++)
                {
                    int iIdPedido_Aux = Convert.ToInt32(dtInformacion.Rows[j]["id_pedido"].ToString());

                    if (iIdPedido_Aux != iIdPedidoContenedor)
                    {
                        if (eliminarComandas(iIdPedido_Aux) == false)
                            return false;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                limpiar = new Clases.ClaseLimpiarArreglos();
                limpiar.limpiarArregloComentarios();

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA RECORRER EL DATAGRID CUANDO SE ESTÁ INSERTANDO LA INFORMACIÓN
        private bool insertarDetPedidos()
        {
            try
            {
                dbTotal = 0;

                //INSTRUCCIONES PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                for (int i = 0; i < dtComandaCombinada.Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dtComandaCombinada.Rows[i]["id_producto"].ToString());
                    dPrecioUnitario_P = Convert.ToDouble(dtComandaCombinada.Rows[i]["precio_unitario"].ToString());
                    dCantidad_P = Convert.ToDouble(dtComandaCombinada.Rows[i]["cantidad"].ToString());
                    dDescuento_P = Convert.ToDouble(dtComandaCombinada.Rows[i]["valor_dscto"].ToString());
                    dIVA_P = Convert.ToDouble(dtComandaCombinada.Rows[i]["valor_iva"].ToString());
                    dServicio = Convert.ToDouble(dtComandaCombinada.Rows[i]["valor_otro"].ToString());
                    sGuardarComentario = dtComandaCombinada.Rows[i]["comentario"].ToString();
                    iIdMascaraItem = Convert.ToInt32(dtComandaCombinada.Rows[i]["id_pos_mascara_item"].ToString());
                    iSecuenciaImpresion_P = Convert.ToInt32(dtComandaCombinada.Rows[i]["id_pos_secuencia_entrega"].ToString());
                    iSecuenciaEntrega_P = Convert.ToInt32(dtComandaCombinada.Rows[i]["secuencia"].ToString());
                    iBanderaCortesia_P = Convert.ToInt32(dtComandaCombinada.Rows[i]["bandera_cortesia"].ToString().ToString());
                    sMotivoCortesia_P = dtComandaCombinada.Rows[i]["motivo_cortesia"].ToString().ToString();
                    iBanderaDescuento_P = Convert.ToInt32(dtComandaCombinada.Rows[i]["bandera_descuento"].ToString().ToString());
                    sMotivoDescuento_P = dtComandaCombinada.Rows[i]["motivo_descuento"].ToString().ToString();
                    dbPorcentajePorLinea_P = Convert.ToDouble(dtComandaCombinada.Rows[i]["porcentaje_descuento_info"].ToString());
                    sCodigoProducto_P = dtComandaCombinada.Rows[i]["codigo_producto"].ToString();

                    dbTotal += dCantidad_P * (dPrecioUnitario_P - dDescuento_P + dIVA_P + dServicio);

                    //INSTRUCCION SQL PARA GUARDAR EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "insert into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "id_Pedido, id_producto, cg_Unidad_Medida, precio_unitario, cantidad," + Environment.NewLine;
                    sSql += "valor_dscto, valor_ice, valor_iva, valor_otro, comentario," + Environment.NewLine;
                    sSql += "id_definicion_combo, id_pos_mascara_item, secuencia, id_pos_secuencia_entrega," + Environment.NewLine;
                    sSql += "bandera_cortesia, motivo_cortesia, bandera_descuento, motivo_descuento," + Environment.NewLine;
                    sSql += "porcentaje_descuento_info, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedidoContenedor + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, " + iIdMascaraItem + ", " + iSecuenciaImpresion_P + "," + Environment.NewLine;
                    sSql += iSecuenciaEntrega_P + ", " + iBanderaCortesia_P + ", '" + sMotivoCortesia_P + "', " + iBanderaDescuento_P + "," + Environment.NewLine;
                    sSql += "'" + sMotivoDescuento_P + "', " + dbPorcentajePorLinea_P + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    if (sCodigoProducto_P.Trim() == "02")
                    {
                        if (Program.iDescargarProductosNoProcesados == 1)
                        {
                            if (insertarMovimientoProductoNoProcesado(Convert.ToDecimal(dCantidad_P)) == false)
                            {
                                return false;
                            }
                        }
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-05
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                    if (sCodigoProducto_P.Trim() == "03")
                    {
                        if (Program.iDescargarReceta == 1)
                        {
                            if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                            {
                                return false;
                            }
                        }
                    }

                    iBandera2 = 0;
                    iCuenta = 0;

                    ////INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    //for (p = 0; p < Program.iContadorDetalle; p++)
                    //{
                    //    if (Program.sDetallesItems[p, 0] == dgvPedido.Rows[i].Cells["id_producto"].Value.ToString())
                    //    {
                    //        iBandera2 = 1;
                    //        break;
                    //    }
                    //}

                    if (iBandera2 == 1)
                    {
                        //INSERTAMOS LOS ITEMS EN LA TABLA pos_det_pedido_detalle

                        for (q = 1; q < Program.iContadorDetalleMximoY; q++)
                        {
                            if (Program.sDetallesItems[p, q] == null)
                            {
                                break;
                            }
                            else
                            {
                                iCuenta++;
                            }
                        }

                        //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                        sTabla = "cv403_det_pedidos";
                        sCampo = "id_det_pedido";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                            return false;
                        }

                        iIdDetPedido = Convert.ToInt32(iMaximo);

                        for (q = 1; q <= iCuenta; q++)
                        {
                            //QUERY PARA INSERTAR LOS DETALLES DE CADA ITEM EN CASO DE QUE SE HAYA INGRESADO
                            sSql = "";
                            sSql += "insert into pos_det_pedido_detalle " + Environment.NewLine;
                            sSql += "(id_det_pedido, detalle, estado, fecha_ingreso," + Environment.NewLine;
                            sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                            sSql += "values(" + Environment.NewLine;
                            sSql += iIdDetPedido + ", '" + Program.sDetallesItems[p, q] + "', " + Environment.NewLine;
                            sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                            //EJECUCION DE INSTRUCCION SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                                return false;
                            }
                        }
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA DAR DE BAJA LOS REGISTROS COMBINADOS
        //FUNCION PARA RECUPERAR LOS VALORES
        private bool eliminarComandas(int iIdPedido_Eliminar)
        {
            try
            {
                //BUSCAR EL ID_DOCUMENTO_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar, id_evento_cobro" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Eliminar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                iIdEventoCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_evento_cobro"].ToString());

                //BUSCAMOS LOS PAGOS
                sSql = "";
                sSql += "select id_pago" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iIdPago = Convert.ToInt32(dtConsulta.Rows[0]["id_pago"].ToString());

                    if (eliminarPagos() == false)
                    {
                        return false;
                    }
                }

                if (eliminarPedido(iIdPedido_Eliminar) == false)
                {
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE PAGOS
        private bool eliminarPagos()
        {
            try
            {
                sSql = "";
                sSql += "update cv403_pagos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
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
                    return false;
                }

                sSql = "";
                sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                //BUSCAMOS LOS DOCUMENTOS PAGOS
                sSql = "";
                sSql += "select id_documento_pago" + Environment.NewLine;
                sSql += "from cv403_documentos_pagos" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdDocumentoPago = Convert.ToInt32(dtAyuda.Rows[i]["id_documento_pago"].ToString());

                    sSql = "";
                    sSql += "select id_pos_movimiento_caja" + Environment.NewLine;
                    sSql += "from pos_movimiento_caja" + Environment.NewLine;
                    sSql += "where id_documento_pago = " + iIdDocumentoPago + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return false;
                    }

                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPosMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_movimiento_caja"].ToString());

                        sSql = "";
                        sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja;

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            return false;
                        }

                        sSql = "";
                        sSql += "update pos_numero_movimiento_caja set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja;

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            return false;
                        }
                    }
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
                    return false;
                }


                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE COMANDAS
        private bool eliminarPedido(int iIdPedido_Recibir)
        {
            try
            {
                //SELECCIONAR EL ID MOVIMIENTO BODEGA
                sSql = "";
                sSql += "select id_movimiento_bodega" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    int iIdCabeceraMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }
                }

                sSql = "";
                sSql += "update cv403_eventos_cobros set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_evento_cobro = " + iIdEventoCobro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                //BUSCAMOS LAS CANTIDADES DESPACHADAS
                sSql = "";
                sSql += "select id_despacho_pedido, id_despacho" + Environment.NewLine;
                sSql += "from cv403_despachos_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                iIdCabDespacho = Convert.ToInt32(dtAyuda.Rows[0]["id_despacho"].ToString());

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdDespachoPedido = Convert.ToInt32(dtAyuda.Rows[i]["id_despacho_pedido"].ToString());

                    sSql = "";
                    sSql += "update cv403_cantidades_despachadas set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_despacho_pedido = " + iIdDespachoPedido;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }
                }

                sSql = "";
                sSql += "update cv403_despachos_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_cab_despachos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_despacho = " + iIdCabDespacho;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Cancelada'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_Recibir;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA RECUPERAR LOS DATOS DE LA LOCALIDAD
        private bool recuperarDatosLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select * from tp_localidades" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad;

                dtLocalidad = new DataTable();
                dtLocalidad.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtLocalidad, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                //AQUI SE RECUPERA LA LOCALIDAD INSUMO
                sSql = "";
                sSql += "select id_localidad_insumo" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidadBodega = 0;
                }

                else
                {
                    iIdLocalidadBodega = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                //AQUI SE RECUPERA EL ID DE LA BODEGA DE INSUMOS
                sSql = "";
                sSql += "select id_bodega" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidadBodega + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdBodegaInsumos = 0;
                }

                else
                {
                    iIdBodegaInsumos = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;

            }
        }

        #region FUNCION PARA CREAR UN EGRESO DE PRODUCTO TERMINADO       

        //FUNCION PARA CREAR EL NUMERO DE MOVIMIENTO
        private bool devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAnio, string sMes, string sCodigoCorrelativo)
        {
            try
            {
                iValorActualMovimiento = 0;
                sCodigo = "";
                sAnioCorto = sAnio.Substring(2, 2);

                if (sMes.Substring(0, 1) == "0")
                {
                    sMesCorto = sMes.Substring(1, 1);
                }

                else
                {
                    sMesCorto = sMes;
                }

                sSql = "";
                sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
                sSql += "where id_bodega = " + iIdBodega;

                dtConsulta = new DataTable();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                sCodigo = dtConsulta.Rows[0][0].ToString();

                string sReferencia;

                sReferencia = sTipoMovimiento + sCodigo + "_" + sAnio + "_" + sMesCorto + "_" + Program.iCgEmpresa;

                sSql = "";
                sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
                sSql += "where referencia = '" + sReferencia + "'" + Environment.NewLine;
                sSql += "and codigo_correlativo = '" + sCodigoCorrelativo + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    int iCorrelativo;

                    sSql = "";
                    sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                    sSql += "where codigo = 'BD'" + Environment.NewLine;
                    sSql += "and tabla = 'SYS$00022'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }

                    iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    iValorActualMovimiento = 1;
                    string sFechaDesde = sAnio + "-01-01";
                    string sFechaHasta = sAnio + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                    sSql = "";
                    sSql += "insert into tp_correlativos (" + Environment.NewLine;
                    sSql += "cg_sistema, codigo_correlativo, referencia, valido_desde," + Environment.NewLine;
                    sSql += "valido_hasta, valor_actual, desde, hasta, estado, origen_dato," + Environment.NewLine;
                    sSql += "numero_replica_trigger, estado_replica, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iCorrelativo + ",'" + sCodigoCorrelativo + "','" + sReferencia + "'," + Environment.NewLine;
                    sSql += "'" + sFechaDesde + "','" + sFechaHasta + "', " + (iValorActualMovimiento + 1) + "," + Environment.NewLine;
                    sSql += "0, 0, 'A', 1," + (iValorActualMovimiento + 1).ToString("N0") + ", 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }
                }

                else
                {
                    iValorActualMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual = " + (iValorActualMovimiento + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }
                }

                sNumeroMovimientoSecuencial = sTipoMovimiento + sCodigo + sAnioCorto + sMes + iValorActualMovimiento.ToString().PadLeft(4, '0');

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA ELIMINAR LOS MOVIMIENTOS DE BODEGA
        private bool eliminarMovimientosBodega(int iIdPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_movimiento_bodega" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    int iIdRegistroMovimiento = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());

                    sSql = "";
                    sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR LOS MOVIMIENTOS DE PRODUCTO TERMINADO
        private bool insertarMovimientoProductoNoProcesado(Decimal dbCantidad_P)
        {
            try
            {
                sAnio = Convert.ToDateTime(sFecha).ToString("yyyy");
                sMes = Convert.ToDateTime(sFecha).ToString("MM");
                int iIdBodega_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_bodega"].ToString());

                if (devuelveCorrelativo("EG", iIdBodega_P, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }

                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_PT"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_PT"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_PT"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_PT"].ToString());
                string sReferenciaExterna_P = "ITEMS - ORDEN " + sHistoricoOrden;

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", " + iIdBodega_P + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencial + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedidoContenedor + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);

                sSql = "";
                sSql += "insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdProducto_P + ", " + iIdCabeceraMovimiento + ", 546," + (dbCantidad_P * -1) + ", 'A')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        #endregion

        #region FUNCIONES CREAR UN EGRESO DE MATERIA PRIMA

        //FUNCION PARA OBTENER EL ID DE LA RECETA
        private bool consultarIdReceta(int iIdProducto_P, Decimal dbCantidadProductos_P, string sNombreProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(id_pos_receta, 0) id_pos_receta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                iIdPosReceta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_receta"].ToString());

                if (iIdPosReceta == 0)
                {
                    return true;
                }

                //sSql = "";
                //sSql += "select * from pos_detalle_receta" + Environment.NewLine;
                //sSql += "where id_pos_receta = " + iIdPosReceta + Environment.NewLine;
                //sSql += "and estado = 'A'";

                sSql = "";
                sSql += "select DR.id_producto, DR.cantidad_bruta, U.cg_unidad" + Environment.NewLine;
                sSql += "from pos_detalle_receta DR INNER JOIN" + Environment.NewLine;
                sSql += "pos_unidad U ON U.id_pos_unidad = DR.id_pos_unidad" + Environment.NewLine;
                sSql += "and DR.estado = 'A'" + Environment.NewLine;
                sSql += "and U.estado = 'A'" + Environment.NewLine;
                sSql += "where DR.id_pos_receta = " + iIdPosReceta;

                dtReceta = new DataTable();
                dtReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtReceta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

                //string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = Convert.ToDateTime(sFecha).ToString("yyyy");
                sMes = Convert.ToDateTime(sFecha).ToString("MM");

                if (devuelveCorrelativo("EG", iIdBodegaInsumos, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }

                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_receta"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_receta"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega_receta"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_receta"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_receta"].ToString());
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + sHistoricoOrden;

                string sNumeroMovimientoSecuencialOriginal = sNumeroMovimientoSecuencial;

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdLocalidadBodega + ", " + iIdBodegaInsumos + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencialOriginal + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedidoContenedor + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtReceta.Rows[i]["cantidad_bruta"].ToString());
                    int iCgUnidad = Convert.ToInt32(dtReceta.Rows[i]["cg_unidad"].ToString());
                    iIdPosSubReceta = 0;

                    //VARIABLE PARA COCNSULTAR SI TIENE SUBRECETA
                    int iSubReceta_R = consultarSubReceta(iIdProducto_R);

                    if (iSubReceta_R == -1)
                    {
                        return false;
                    }

                    if (iSubReceta_R == 0)
                    {
                        sSql = "";
                        sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                        sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                        sSql += "Values (" + Environment.NewLine;
                        sSql += iIdProducto_R + ", " + iIdCabeceraMovimiento + ", " + iCgUnidad + "," + (dbCantidadMateriaPrima_R * dbCantidadProductos_P * -1) + ", 'A')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                            return false;
                        }
                    }

                    else
                    {
                        if (insertarComponentesSubReceta(iSubReceta_R, dbCantidadProductos_P, sNombreProducto_P) == false)
                        {
                            return false;
                        }
                    }

                }


                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        //FUNCION PARA VERIFICAR SI EL ITEM TIENE SUBRECETA
        private int consultarSubReceta(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select TR.complementaria, R.id_pos_receta, R.descripcion" + Environment.NewLine;
                sSql += "from cv401_productos P, pos_receta R," + Environment.NewLine;
                sSql += "pos_tipo_receta TR" + Environment.NewLine;
                sSql += "where P.id_pos_receta = R.id_pos_receta" + Environment.NewLine;
                sSql += "and R.id_pos_tipo_receta = TR.id_pos_tipo_receta" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and R.estado = 'A'" + Environment.NewLine;
                sSql += "and TR.estado = 'A'" + Environment.NewLine;
                sSql += "and P.id_producto = " + iIdProducto_P;

                dtSubReceta = new DataTable();
                dtSubReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == true)
                {
                    if (dtSubReceta.Rows.Count > 0)
                    {
                        iIdPosSubReceta = Convert.ToInt32(dtSubReceta.Rows[0][1].ToString());
                        sNombreSubReceta = dtSubReceta.Rows[0][2].ToString().ToUpper();
                        return Convert.ToInt32(dtSubReceta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return -1;
                }
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return -1;
            }
        }

        //FUNCION PARA INSERTAR LOS ITEMS DE LA SUBRECETA
        private bool insertarComponentesSubReceta(int iIdPosSubReceta_P, Decimal dbCantidadPedida_P, string sNombreProducto_P)
        {
            try
            {
                //sSql = "";
                //sSql += "select id_producto, cantidad_bruta" + Environment.NewLine;
                //sSql += "from pos_detalle_receta" + Environment.NewLine;
                //sSql += "where id_pos_receta = " + iIdPosSubReceta_P + Environment.NewLine;
                //sSql += "and estado = 'A'";

                sSql = "";
                sSql += "select DR.id_producto, DR.cantidad_bruta, U.cg_unidad" + Environment.NewLine;
                sSql += "from pos_detalle_receta DR INNER JOIN" + Environment.NewLine;
                sSql += "pos_unidad U ON U.id_pos_unidad = DR.id_pos_unidad" + Environment.NewLine;
                sSql += "and DR.estado = 'A'" + Environment.NewLine;
                sSql += "and U.estado = 'A'" + Environment.NewLine;
                sSql += "where DR.id_pos_receta = " + iIdPosSubReceta_P;

                dtSubReceta = new DataTable();
                dtSubReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                if (dtSubReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

                //string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = Convert.ToDateTime(sFecha).ToString("yyyy");
                sMes = Convert.ToDateTime(sFecha).ToString("MM");

                if (devuelveCorrelativo("EG", iIdBodegaInsumos, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }

                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_receta"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_receta"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega_receta"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_receta"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_receta"].ToString());
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + sHistoricoOrden;

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdLocalidadBodega + ", " + iIdBodegaInsumos + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencial + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedidoContenedor + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                    return false;
                }

                int iIdCabeceraMovimientoSubReceta = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtSubReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtSubReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtSubReceta.Rows[i]["cantidad_bruta"].ToString());
                    int iCgUnidad = Convert.ToInt32(dtSubReceta.Rows[i]["cg_unidad"].ToString());

                    sSql = "";
                    sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_R + ", " + iIdCabeceraMovimientoSubReceta + ", " + iCgUnidad + "," + (dbCantidadMateriaPrima_R * dbCantidadPedida_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sRespuesta = "No se pudo consultar información en el servidor. Comuníquese con el administrador.";
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
