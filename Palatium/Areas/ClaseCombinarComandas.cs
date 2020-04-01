using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Areas
{
    class ClaseCombinarComandas
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;

        DataTable dtConsulta;
        DataTable dtPedidos;
        DataTable dtProductos;

        bool bRespuesta;

        int iIdMesa;
        int iIdPedidoCombinar;
        int iIdPedidoEliminar;

        //VARIABLES DE LA RECETA
        //--------------------------------------------------------------------------
        double dbValorActual;

        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sAnio;
        string sMes;
        string sReferenciaExterna_Sub;
        string sNombreSubReceta;
        string sOrdenCombinar;
        string sFecha;

        int iBanderaDescargaStock;
        int iIdMovimientoStock;
        int iIdBodega;
        int iIdPedido;
        int iCgClienteProveedor_Sub;
        int iCgTipoMovimiento_Sub;
        int iIdPosSubReceta;
        int iIdPosReceta;

        int[] iRespuesta;

        DataTable dtReceta;
        DataTable dtSubReceta;

        //--------------------------------------------------------------------------

        //VARIABLES PARA INSERTAR EN LA BDD
        int iIdOrden_P;
        int iIdProducto_P;
        int iSecuenciaImpresion;
        int iSecuenciaEntrega;
        int iIdMascaraItem;
        int iCgClienteProveedor;
        int iTipoMovimiento;
                    
        double dPrecioUnitario_P;
        double dCantidad_P;        
        double dValorDescuento;
        double dServicio;
        double dIVA_P;
        double dDescuento_P;
        double dbTotalCobrar;

        string sGuardarComentario;

        //--------------------------------------------------------------------------

        public bool recibirParametrosCombinacion(int iIdMesa_R, int iIdPedidoCombinar_R, string sOrdenCombinar_R, DataTable dtPedidos_R, DataTable dtProductos_R)
        {
            try
            {
                this.iIdMesa = iIdMesa_R;
                this.dtPedidos = dtPedidos_R;
                this.sOrdenCombinar = sOrdenCombinar_R;
                this.dtProductos = dtProductos_R;
                this.iIdPedidoCombinar = iIdPedidoCombinar_R;

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    return false;
                }

                if (actualizarComandaPrincipal() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                if (eliminarComandasCombinadas() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                if (eliminarMovimientosBodegaComandasCombinadas() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #region FUNCIONES DE INSERCION DE LA ORDEN

        //FUNCION PARA ACTUALIZAR LA COMANDA
        private bool actualizarComandaPrincipal()
        {
            try
            {   
                iSecuenciaEntrega = 0;
                iSecuenciaImpresion = 1;
                dbTotalCobrar = 0;

                //ELIMINAR LOS ITEMS DE LA ORDEN CONTENEDORA
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoCombinar;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //FUNCIONES PARA LA BODEGA
                //--------------------------------------------------------------------------------
                if (eliminarMovimientos(iIdPedidoCombinar) == false)
                {
                    return false;
                }
                
                //AQUI REVISAR DESCUENTOS A TODA LA ORDEN

                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_DET_PEDIDOS
                //=======================================================================================================
                for (int i = 0; i < dtProductos.Rows.Count; i++)
                {
                    /* SE REALIZA UNA ACTUALIZACION DE CODIGO PARA MEJOR ENTENDIMIENTO Y ORDEN
                     * OBJETIVO: OBTENER LAS VARIABLES PARA REALIZAR UN INSERT MAS EFECTIVO
                     */
                    iIdOrden_P = iIdPedido;
                    iIdProducto_P = Convert.ToInt32(dtProductos.Rows[i]["id_producto"].ToString());
                    dPrecioUnitario_P = Convert.ToDouble(dtProductos.Rows[i]["precio_unitario"].ToString());
                    dCantidad_P = Convert.ToDouble(dtProductos.Rows[i]["cantidad"].ToString());
                    dValorDescuento = Convert.ToDouble(dtProductos.Rows[i]["valor_dscto"].ToString());
                    iIdMascaraItem = Convert.ToInt32(dtProductos.Rows[i]["id_pos_mascara_item"].ToString());
                    dServicio = Convert.ToDouble(dtProductos.Rows[i]["valor_otro"].ToString());
                    dIVA_P = (dPrecioUnitario_P - dValorDescuento) * Program.iva;

                    dbTotalCobrar += dCantidad_P * (dPrecioUnitario_P - dValorDescuento + dServicio + dIVA_P);

                    //CONTROL DE CONSUMO ALIMENTOS,CORTESIAS Y CANCELACION ITEM
                    if ((dtProductos.Rows[i]["id_pos_mascara_item"].ToString() != "0") && (dtProductos.Rows[i]["id_pos_mascara_item"].ToString() != ""))
                    {
                        sGuardarComentario = dtProductos.Rows[i]["nombre"].ToString();
                        iIdMascaraItem = Convert.ToInt32(dtProductos.Rows[i]["id_pos_mascara_item"].ToString());
                    }

                    else if (dtProductos.Rows[i]["cortesia"].ToString() == "1")
                    {
                        sGuardarComentario = dtProductos.Rows[i]["nombre"].ToString();
                    }

                    else if (dtProductos.Rows[i]["cancelacion"].ToString() == "1")
                    {
                        sGuardarComentario = dtProductos.Rows[i]["nombre"].ToString();
                    }

                    else if (dtProductos.Rows[i]["comentario"].ToString() != "")
                    {
                        sGuardarComentario = dtProductos.Rows[i]["comentario"].ToString();
                    }

                    else
                    {
                        sGuardarComentario = null;
                    }

                    //INSTRUCCION SQL PARA GUARDAR EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia, " + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado,numero_replica_trigger,numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedidoCombinar + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdMascaraItem + "," + Environment.NewLine;
                    sSql += iSecuenciaImpresion + ", " + Environment.NewLine;

                    if (iSecuenciaEntrega == 0)
                    {
                        sSql = sSql + "null, ";
                    }

                    else
                    {
                        sSql = sSql + iSecuenciaEntrega + ", ";
                    }

                    sSql = sSql + "'A', 0, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }                    
                }

                //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "valor = " + dbTotalCobrar + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoCombinar + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ELIMINAR COMANDAS COMBINADAS
        private bool eliminarComandasCombinadas()
        {
            try
            {
                for (int i = 0; i < dtPedidos.Rows.Count; i++)
                {
                    iIdPedidoEliminar = Convert.ToInt32(dtPedidos.Rows[i][1].ToString());

                    if (iIdPedidoEliminar != iIdPedidoCombinar)
                    {
                        //QUERY PARA PONER EN ESTADO 'E' LA TABLA CV403_CAB_PEDIDOS
                        sSql = "";
                        sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                        sSql += "estado = 'E', " + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                        sSql += "estado_orden = 'Combinada'" + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedidoEliminar;

                        //EJECUCION DE INSTRUCCION SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }

                        //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                        sSql = "";
                        sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                        sSql += "estado = 'E'" + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedidoEliminar + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        //EJECUCION DE INSTRUCCION SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }

                        //FUNCIONES PARA LA BODEGA
                        //--------------------------------------------------------------------------------
                        if (eliminarMovimientos(iIdPedidoEliminar) == false)
                        {
                            return false;
                        }
                        //--------------------------------------------------------------------------------

                        //QUERY PARA BUSCAR LOS DETALLES DE LOS ITEMS DEL PEDIDO Y PONERLOS EN ESTADO 'E'
                        sSql = "";
                        sSql += "select DPD.* from cv403_det_pedidos DP," + Environment.NewLine;
                        sSql += "cv403_cab_pedidos CP, pos_det_pedido_detalle DPD" + Environment.NewLine;
                        sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                        sSql += "and DPD.id_det_pedido = DP.id_det_pedido" + Environment.NewLine;
                        sSql += "and DP.estado = 'A'" + Environment.NewLine;
                        sSql += "and CP.estado = 'A'" + Environment.NewLine;
                        sSql += "and DPD.estado = 'A'" + Environment.NewLine;
                        sSql += "and CP.id_pedido = " + iIdPedidoEliminar;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtConsulta.Rows.Count; j++)
                                {
                                    //QUERY PARA CAMBIAR A ESTADO 'E' LOS DETALLES DE LOS ITEMS DE LA ORDEN
                                    sSql = "";
                                    sSql += "update pos_det_pedido_detalle set" + Environment.NewLine;
                                    sSql += "estado = 'E'," + Environment.NewLine;
                                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                                    sSql += "where id_pos_det_pedido_detalle" + Convert.ToInt32(dtConsulta.Rows[j][0].ToString()) + Environment.NewLine;
                                    sSql += "and estado = 'A'";

                                    //EJECUCION DE INSTRUCCION SQL
                                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                    {
                                        catchMensaje.LblMensaje.Text = sSql;
                                        catchMensaje.ShowDialog();
                                        return false;
                                    }
                                }
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }

                        //QUERY PARA VERIFICAR LOS DESCUENTOS INGRESADOS
                        sSql = "";
                        sSql += "select * from pos_descuento" + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedidoEliminar + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                //INSTRUCCION SQL PARA CAMBIAR A ESTADO 'E' LOS DESCUENTOS DE LA ORDEN
                                sSql = "";
                                sSql += "update pos_descuento set" + Environment.NewLine;
                                sSql += "estado = 'E'," + Environment.NewLine;
                                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                                sSql += "where id_pedido = " + iIdPedidoEliminar;

                                //EJECUCION DE INSTRUCCION SQL
                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                {
                                    catchMensaje.LblMensaje.Text = sSql;
                                    catchMensaje.ShowDialog();
                                    return false;
                                }
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ELIMINAR COMANDAS COMBINADAS
        private bool eliminarMovimientosBodegaComandasCombinadas()
        {
            try
            {
                //RECORRER EL DATAGRID EN CASO DE QUE EL SISTEMA ESTÉ HABILITADO PARA DESCARGAR EL INVENTARIO
                //if (Program.iUsarReceta == 1)
                //{
                    iIdBodega = obtenerIdBodega(Program.iIdLocalidad);

                    if (iIdBodega == 0)
                    {
                        goto continuar_proceso;
                    }

                    iCgClienteProveedor = obtenerCgClienteProveedor();
                    iTipoMovimiento = obtenerCorrelativoTipoMovimiento();

                    if (iCgClienteProveedor == 0 || iTipoMovimiento == 0)
                    {
                        goto continuar_proceso;
                    }

                    iRespuesta = buscarDatos();

                    if (iRespuesta[0] == 0)
                    {
                        goto continuar_proceso;
                    }

                    for (int i = 0; i < dtProductos.Rows.Count; i++)
                    {
                        string sNombreProducto_P = dtProductos.Rows[i]["nombre"].ToString().Trim();
                        iIdProducto_P = Convert.ToInt32(dtProductos.Rows[i]["id_producto"].ToString());
                        dCantidad_P = Convert.ToDouble(dtProductos.Rows[i]["cantidad"].ToString());
                        iIdPosReceta = obteneridReceta(iIdProducto_P);

                        if (iIdPosReceta == -1)
                        {
                            return false;
                        }

                        else
                        {
                            if (crearEgreso(sNombreProducto_P + " - ORDEN " + sOrdenCombinar, iCgClienteProveedor,
                                        iTipoMovimiento, iIdPosReceta, iIdProducto_P, dCantidad_P) == false)
                            {
                                return false;
                            }
                        }
                    }
                //}

                iIdMovimientoStock = 0;
                iBanderaDescargaStock = 0;
                continuar_proceso: { }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        #region NUEVAS FUNCIONES DE LA RECETA

        //FUNCION PARA CONSULTAR EL ID DE LA RECETA POR PRODUCTO
        private int obteneridReceta(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(id_pos_receta, 0) id_pos_receta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PAEA OBTENER DATOS DE ID_AUXILIAR, MOTIVMO_MOVIMIENTO, ID_PERSONA
        private int[] buscarDatos()
        {

            int[] iRespuesta = new int[3];
            iRespuesta[0] = 0;
            iRespuesta[1] = 0;
            iRespuesta[2] = 0;

            sSql = "";
            sSql += "select id_responsable, id_auxiliar, cg_motivo_movimiento_bodega" + Environment.NewLine;
            sSql += "from tp_localidades" + Environment.NewLine;
            sSql += "where id_localidad = " + Program.iIdLocalidad;

            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            if (conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql) == true)
            {
                if (dtAyuda.Rows.Count > 0)
                {
                    iRespuesta[0] = Convert.ToInt32(dtAyuda.Rows[0][0].ToString());
                    iRespuesta[1] = Convert.ToInt32(dtAyuda.Rows[0][1].ToString());
                    iRespuesta[2] = Convert.ToInt32(dtAyuda.Rows[0][2].ToString());
                }
            }

            return iRespuesta;

        }

        //FUNCION PARA OBTENER EL ID DE LA BODEGA
        private int obtenerIdBodega(int iIdLocalidad)
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad_insumo" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        int iIdLocalidadBodega = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        sSql = "";
                        sSql += "select id_bodega from tp_localidades" + Environment.NewLine;
                        sSql += "where id_localidad = " + iIdLocalidadBodega + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        DataTable dtAyuda = new DataTable();
                        dtAyuda.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtAyuda.Rows.Count > 0)
                            {
                                return Convert.ToInt32(dtAyuda.Rows[0][0].ToString());
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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //Función para obtener cg_cliente_proveedor
        private int obtenerCgClienteProveedor()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00642'" + Environment.NewLine;
                sSql += "and codigo = '02'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //Función para obtener tipo de movimiento
        private int obtenerCorrelativoTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00648'" + Environment.NewLine;
                sSql += "and codigo = 'EMP'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA ELIMINAR LOS MOVIMIENTOS PARA ACTUALIZAR LA ORDEN
        private bool eliminarMovimientos(int iIdPedido_P)
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION  PARA CREAR EL NUMERO DE MOVIMIENTO
        private string devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAnio, string sMes, string sCodigoCorrelativo)
        {
            dbValorActual = 0;
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

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    sCodigo = dtConsulta.Rows[0][0].ToString();
                }
            }

            else
            {
                return "Error";
            }

            string sReferencia;

            sReferencia = sTipoMovimiento + sCodigo + "_" + sAnio + "_" + sMesCorto + "_" + Program.iCgEmpresa;

            sSql = "";
            sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
            sSql += "where referencia = '" + sReferencia + "'" + Environment.NewLine;
            sSql += "and codigo_correlativo = '" + sCodigoCorrelativo + "'";

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dbValorActual = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual =  " + (dbValorActual + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAnioCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                }
                else
                {
                    int iCorrelativo = 4979;
                    dbValorActual = 1;

                    sSql = "";
                    sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                    sSql += "where codigo = 'BD'" + Environment.NewLine;
                    sSql += "and tabla = 'SYS$00022'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        }
                    }
                    else
                        return "Error";

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
                    sSql += "'" + sFechaDesde + "','" + sFechaHasta + "', " + (dbValorActual + 1) + "," + Environment.NewLine;
                    sSql += "0, 0, 'A', 1," + (dbValorActual + 1).ToString("N0") + ", 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAnioCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                }
            }
            else
            {
                return "Error";
            }
        }

        //FUNCION PARA INSERTAR EL EGRERO
        private bool crearEgreso(string sReferenciaExterna_P, int iCgClienteProveedor_P, int iCgTipoMovimiento_P,
                                 int iIdPosReceta_P, int iIdProducto_P, double dbCantidad_P)
        {
            try
            {
                string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = sFecha.Substring(0, 4);
                sMes = sFecha.Substring(5, 2);

                if ((iBanderaDescargaStock == 1) && (iIdPosReceta_P == 0))
                {
                    sSql = "";
                    sSql += "insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_P + ", " + iIdMovimientoStock + ", 546," + (dbCantidad_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    goto retornar;
                }

                string sNumeroMovimiento = devuelveCorrelativo("EG", iIdBodega, sAnio, sMes, "MOV");

                if (sNumeroMovimiento == "Error")
                {
                    return false;
                }

                if (iIdPosReceta_P == 0)
                {
                    sReferenciaExterna_P = "ITEMS - ORDEN " + sOrdenCombinar;
                }

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa,cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", " + iIdBodega + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimiento + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedidoCombinar + ", " + iRespuesta[2] + ", '', '', '', '', " + iRespuesta[1] + ", " + Environment.NewLine;
                sSql += iRespuesta[0] + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL MÁXIMO DE LA CABECERA
                int iMaximo_P = 0;

                sSql = "";
                sSql += "select max(Id_Movimiento_Bodega) New_Codigo" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iMaximo_P = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = "No se pudo obtener el identificador de la tabla cv402_cabecera_movimientos";
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }


                /* VARIABLE IRECETAINSUMO
                    ESTA VARIABLE PERMITE VERIFICAR SI ES RECETA O UN ITEM PARA DESCARGAR 
                    1 - MANEJA RECETA
                    0 - MANEJA INSUMO
                */

                if (iIdPosReceta_P != 0)
                {
                    iCgClienteProveedor_Sub = iCgClienteProveedor_P;
                    iCgTipoMovimiento_Sub = iCgTipoMovimiento_P;
                    sReferenciaExterna_Sub = sReferenciaExterna_P;

                    if (insertarComponentesReceta(iIdPosReceta_P, iMaximo_P, dbCantidad_P) == false)
                    {
                        return false;
                    }
                }

                else
                {
                    sSql = "";
                    sSql += "insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_P + ", " + iMaximo_P + ", 546," + (dbCantidad_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iBanderaDescargaStock = 1;
                    iIdMovimientoStock = iMaximo_P;
                }

            retornar: { }
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR LOS DATOS DE LA RECETA EN LOS MOVIMIENTOS DE BODEGA
        private bool insertarComponentesReceta(int iIdPosReceta_P, int iIdMovimientoBodega_P, double dbCantidadPedida_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_producto, cantidad_bruta" + Environment.NewLine;
                sSql += "from pos_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdPosReceta_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtReceta = new DataTable();
                dtReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtReceta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtReceta.Rows[i][0].ToString());
                    double dbCantidad_R = Convert.ToDouble(dtReceta.Rows[i][1].ToString());
                    iIdPosSubReceta = 0;

                    //VARIABLE PARA COCNSULTAR SI TIENE SUBRECETA
                    int iSubReceta_R = consultarSubReceta(iIdProducto_R);

                    if (iSubReceta_R == 0)
                    {
                        sSql = "";
                        sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                        sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                        sSql += "Values (" + Environment.NewLine;
                        sSql += iIdProducto_R + ", " + iIdMovimientoBodega_P + ", 546," + (dbCantidad_R * dbCantidadPedida_P * -1) + ", 'A')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }

                    else if (iSubReceta_R == 1)
                    {
                        if (insertarComponentesSubReceta(iIdPosSubReceta, iIdMovimientoBodega_P, dbCantidadPedida_P) == false)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR LOS ITEMS DE LA SUBRECETA
        private bool insertarComponentesSubReceta(int iIdPosSubReceta_P, int iIdMovimientoBodega_P, double dbCantidadPedida_P)
        {
            try
            {
                string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = sFecha.Substring(0, 4);
                sMes = sFecha.Substring(5, 2);

                string sNumeroMovimiento_R = devuelveCorrelativo("EG", iIdBodega, sAnio, sMes, "MOV");

                if (sNumeroMovimiento_R == "Error")
                {
                    return false;
                }

                int iIdMaximoCabMov = crearCabeceraMovimiento(sNumeroMovimiento_R, iCgClienteProveedor_Sub, iCgTipoMovimiento_Sub, sNombreSubReceta + " - " + sReferenciaExterna_Sub);

                if (iIdMaximoCabMov == -1)
                {
                    return false;
                }

                sSql = "";
                sSql += "select id_producto, cantidad_bruta" + Environment.NewLine;
                sSql += "from pos_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdPosSubReceta_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtSubReceta = new DataTable();
                dtSubReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtSubReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtSubReceta.Rows[i][0].ToString());
                    double dbCantidad_R = Convert.ToDouble(dtSubReceta.Rows[i][1].ToString());
                    iIdPosSubReceta = 0;

                    if (crearMovimientosBodega(iIdProducto_R, iIdMaximoCabMov, dbCantidad_R, dbCantidadPedida_P) == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR LA CABECERA Y RECUPERAR EL ID DEL MOVIMIENTO
        private int crearCabeceraMovimiento(string sNumeroMovimiento_P, int iCgClienteProveedor_P, int iCgTipoMovimiento_P, string sReferenciaExterna_P)
        {
            try
            {
                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa,cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", " + iIdBodega + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimiento_P + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iRespuesta[2] + ", '', '', '', '', " + iRespuesta[1] + ", " + Environment.NewLine;
                sSql += iRespuesta[0] + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                //OBTENER EL MÁXIMO DE LA CABECERA
                sSql = "";
                sSql += "select max(Id_Movimiento_Bodega) New_Codigo" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
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
                        catchMensaje.LblMensaje.Text = "No se pudo obtener el identificador de la tabla cv402_cabecera_movimientos";
                        catchMensaje.ShowDialog();
                        return -1;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR EL DETALLE DEL MOVIMIENTO
        private bool crearMovimientosBodega(int iIdProducto_R, int iIdMovimientoBodega_P, double dbCantidad_R, double dbCantidadPedida_P)
        {
            try
            {
                sSql = "";
                sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdProducto_R + ", " + iIdMovimientoBodega_P + ", 546," + (dbCantidad_R * dbCantidadPedida_P * -1) + ", 'A')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion
    }
}
