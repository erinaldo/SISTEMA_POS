using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Palatium.Clases_Crear_Comandas
{
    class ClaseCrearComanda
    {
        ConexionBD.ConexionBD conexionM;

        int iIdMesa;
        int iIdCajero;
        int iNumeroPersonas;        
        int iIdMesero;
        int iIdPosTerminal; 
        
        int iConsumoAlimentos;
        int iIdPromotor;
        int iIdRepartidor;
        int iIdPosCierreCajero;
        int iBanderaCortesia_P;
        int iBanderaDescuento_P;
        int iBanderaComentario_P;
        int iIdMascaraItem_P;
        int iSecuenciaEntrega_P;
        int iSecuenciaImpresion_P;
        int iPagaServicio_P;
        int iBanderaInsertarDetalle;
        int iIdDetPedido;
        int p, q;

        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPago;
        int iIdDocumentoPagado;
        int iNumeroPago;
        int iConciliacion;
        int iOperadorTarjeta;
        int iTipoTarjeta;
        int iBanderaInsertarLote;
        int iCgTipoCaja = 8906;
        int iCgUnidadMedida = 546;

        int iIdTipoComprobante;
        int iFacturaElectronica;
        int iIdFormaPago_1;
        int iIdFormaPago_2;
        int iIdFormaPago_3;
        int iIdTipoAmbiente;
        int iIdTipoEmision;
        int iCgEstadoDctoPorCobrar = 7461;
        int iIdCaja;
        int iIdPosMovimientoCaja;
        int iNumeroMovimientoCaja;
        public int iIdFactura;

        string sSql;
        public string sFecha;
        string sTabla;
        string sCampo;
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sNumeroMovimientoSecuencial;
        string sAnio;
        string sNombreSubReceta;
        string sMes;
        string sFechaConsulta;
        string sNombreProducto_P;
        string sEstadoOrden;
        string sNumeroLote;
        string sCodigoTipoFormaCobro;
        string sClaveAcceso;
        public string sEstablecimiento;
        public string sPuntoEmision;
        public string sNumeroComprobante;
        string sCorreoElectronico;
        string sDireccionCliente;
        string sCiudad;
        string sTelefonoCliente;
        string sMovimiento;
        string sMotivoCortesia_P;
        string sMotivoDescuento_P;
        string sCodigoProducto_P;
        string sGuardarComentario;
        string sDetalleProductoItem;
        string sComentarios; 

        public string sMensajeError;

        long iMaximo;

        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtLocalidad;
        DataTable dtSubReceta;
        DataTable dtReceta;
        DataTable dtItems;
        DataTable dtPagos;
        DataTable dtAlmacenar;
        DataTable dtAgrupado;
        DataTable dtAuxiliar;

        int iIdPersonaEmpleado;
        int iIdPersona_o_Empresa;
        int iIdOrigenOrden;
        public int iIdPedido;
        int iCuentaDiaria;
        public int iNumeroPedidoOrden;
        int iIdEventoCobro;
        int iIdCabDespachos;
        int iIdDespachoPedido;
        int iIdProducto_P;
        int iCgTipoDocumento = 2725;
        int iIdCabeceraMovimiento;
        int iIdLocalidadBodega;
        int iValorActualMovimiento;
        int iIdBodegaInsumos;
        int iIdPosReceta;
        int iIdPosSubReceta;
        int iPagaIva_P;

        Decimal dbTotalDebido;
        Decimal dbPrecioUnitario_P;
        Decimal dbCantidad_P;
        Decimal dbIVA_P;
        Decimal dbCambio;
        Decimal dbPropina;
        Decimal dbPropinaRecibidaFormaPago;
        Decimal dbDescuento_P;
        Decimal dbPorcentajePorLinea_P;
        Decimal dbServicio_P;
        Decimal dbPorcentajeServicio;
        Decimal dbPorcentajeDescuento;

        SqlParameter[] parametro;

        #region FUNCION PARA CREAR UN EGRESO DE PRODUCTO TERMINADO

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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtLocalidad, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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
                sMensajeError = ex.Message;
                return false;

            }
        }

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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
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

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
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

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                sNumeroMovimientoSecuencial = sTipoMovimiento + sCodigo + sAnioCorto + sMes + iValorActualMovimiento.ToString().PadLeft(4, '0');

                return true;
            }

            catch (Exception ex)
            {
                sMes = ex.Message;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = conexionM.sMensajeError;
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
                string sReferenciaExterna_P = "ITEMS - ORDEN " + iNumeroPedidoOrden.ToString();

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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);

                sSql = "";
                sSql += "insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdProducto_P + ", " + iIdCabeceraMovimiento + ", 546," + (dbCantidad_P * -1) + ", 'A')";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtReceta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                if (dtReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

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
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + iNumeroPedidoOrden.ToString();

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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
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

                        if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            sMensajeError = conexionM.sMensajeError;
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
                sMensajeError = ex.Message;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

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
                    sMensajeError = conexionM.sMensajeError;
                    return -1;
                }
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                if (dtSubReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

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
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + iNumeroPedidoOrden.ToString();

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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
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

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        #endregion

        //FUNCION PARA MANIPULAR EL PROCESO COMPLETO A LA BASE DE DATOS
        public bool manejadorTransaccionalComandas()
        {
            try
            {
                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE CUENTA
        private bool extraerNumeroCuenta()
        {
            try
            {
                sFechaConsulta = sFecha;

                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + sFechaConsulta + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR CV403_DET_PEDIDOS
        private bool insertarDetPedidos(DataTable dtItems_P, DataTable dtDetalleItems_P)
        {
            try
            {
                int j;

                for (int i = 0; i < dtItems_P.Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dtItems_P.Rows[i]["id_producto"].ToString());
                    dbPrecioUnitario_P = Convert.ToDecimal(dtItems_P.Rows[i]["valor_unitario"].ToString());
                    dbCantidad_P = Convert.ToDecimal(dtItems_P.Rows[i]["cantidad"].ToString());
                    dbDescuento_P = Convert.ToDecimal(dtItems_P.Rows[i]["valor_descuento"].ToString());
                    iPagaIva_P = Convert.ToInt32(dtItems_P.Rows[i]["paga_iva"].ToString());
                    iBanderaCortesia_P = Convert.ToInt32(dtItems_P.Rows[i]["bandera_cortesia"].ToString());
                    iBanderaDescuento_P = Convert.ToInt32(dtItems_P.Rows[i]["bandera_descuento"].ToString());
                    iBanderaComentario_P = Convert.ToInt32(dtItems_P.Rows[i]["bandera_comentario"].ToString());
                    iIdMascaraItem_P = Convert.ToInt32(dtItems_P.Rows[i]["id_mascara"].ToString());
                    iSecuenciaEntrega_P = Convert.ToInt32(dtItems_P.Rows[i]["id_ordenamiento"].ToString());
                    iSecuenciaImpresion_P = Convert.ToInt32(dtItems_P.Rows[i]["secuencia_impresion"].ToString());
                    sMotivoCortesia_P = dtItems_P.Rows[i]["motivo_cortesia"].ToString();
                    sMotivoDescuento_P = dtItems_P.Rows[i]["motivo_descuento"].ToString();
                    sCodigoProducto_P = dtItems_P.Rows[i]["codigo_producto"].ToString();
                    sNombreProducto_P = dtItems_P.Rows[i]["nombre_producto"].ToString();
                    dbPorcentajePorLinea_P = Convert.ToDecimal(dtItems_P.Rows[i]["porcentaje_descuento"].ToString());
                    iPagaServicio_P = Convert.ToInt32(dtItems_P.Rows[i]["paga_servicio"].ToString());

                    if (iBanderaComentario_P == 1)
                        sGuardarComentario = sNombreProducto_P;
                    else
                        sGuardarComentario = "";

                    if (iPagaServicio_P == 1)
                        dbServicio_P = (dbPrecioUnitario_P - dbDescuento_P) * Convert.ToDecimal(Program.servicio);
                    else
                        dbServicio_P = 0;

                    if (iPagaIva_P == 1)
                        dbIVA_P = (dbPrecioUnitario_P - dbDescuento_P) * Convert.ToDecimal(Program.iva);
                    else
                        dbIVA_P = 0;

                    //INSTRUCCIÓN PARA INSERTAR ENLA TABLA CV403_DET_PEDIDOS
                    //--------------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "insert into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "id_Pedido, id_producto, cg_Unidad_Medida, precio_unitario, cantidad," + Environment.NewLine;
                    sSql += "valor_dscto, valor_ice, valor_iva, valor_otro, comentario," + Environment.NewLine;
                    sSql += "id_definicion_combo, id_pos_mascara_item, secuencia, id_pos_secuencia_entrega," + Environment.NewLine;
                    sSql += "bandera_cortesia, motivo_cortesia, bandera_descuento, motivo_descuento," + Environment.NewLine;
                    sSql += "porcentaje_descuento_info, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += "@id_pedido, @id_producto, @cg_unidad_medida, @precio_unitario, @cantidad," + Environment.NewLine;
                    sSql += "@valor_dscto, @valor_ice, @valor_iva, @valor_otro, @comentario," + Environment.NewLine;
                    sSql += "null, @id_pos_mascara_item, @secuencia, @id_pos_secuencia_entrega," + Environment.NewLine;
                    sSql += "@bandera_cortesia, @motivo_cortesia, @bandera_descuento, @motivo_descuento," + Environment.NewLine;
                    sSql += "@porcentaje_descuento_info, @estado, getdate(), @usuario_ingreso," + Environment.NewLine;
                    sSql += "@terminal_ingreso, @numero_replica_trigger, @numero_control_replica)";
                    
                    #region PARAMETROS

                    j = 0;
                    parametro = new SqlParameter[23];
                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_pedido";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdPedido;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_producto";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdProducto_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@cg_unidad_medida";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iCgUnidadMedida;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@precio_unitario";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbPrecioUnitario_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@cantidad";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbCantidad_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor_dscto";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbDescuento_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor_ice";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor_iva";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbIVA_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor_otro";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbServicio_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@comentario";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sGuardarComentario;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_pos_mascara_item";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdMascaraItem_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@secuencia";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iSecuenciaImpresion_P;
                    j++;                    

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_pos_secuencia_entrega";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iSecuenciaEntrega_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@bandera_cortesia";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iBanderaCortesia_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@motivo_cortesia";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sMotivoCortesia_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@bandera_descuento";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iBanderaDescuento_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@motivo_descuento";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sMotivoDescuento_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@porcentaje_descuento_info";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbPorcentajePorLinea_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@estado";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = "A";
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@usuario_ingreso";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = Program.sDatosMaximo[0];
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@terminal_ingreso";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = Program.sDatosMaximo[1];
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_replica_trigger";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_control_replica";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;
                    
                    #endregion

                    if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                    //--------------------------------------------------------------------------------------------------------

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    if (sCodigoProducto_P.Trim() == "02")
                    {
                        if (Program.iDescargarProductosNoProcesados == 1)
                        {
                            if (insertarMovimientoProductoNoProcesado(dbCantidad_P) == false)
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
                            if (consultarIdReceta(iIdProducto_P, dbCantidad_P, sNombreProducto_P) == false)
                            {
                                return false;
                            }
                        }
                    }

                    //INSTRUCCIÓN PARA INSERTAR ENLA TABLA CV403_CANTIDADES_DESPACHADAS
                    //--------------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "@id_despacho_pedido, @id_producto, @cantidad, @estado," + Environment.NewLine;
                    sSql += "@numero_replica_trigger, @numero_control_replica)";

                    #region PARAMETROS

                    j = 0;
                    parametro = new SqlParameter[6];
                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_despacho_pedido";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdDespachoPedido;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_producto";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdProducto_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@cantidad";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbCantidad_P;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@estado";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = "A";
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_replica_trigger";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_control_replica";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;

                    #endregion

                    if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                    //--------------------------------------------------------------------------------------------------------

                    //INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    //--------------------------------------------------------------------------------------------------------
                    iBanderaInsertarDetalle = 0;

                    if (dtDetalleItems_P.Rows.Count > 0)
                    {
                        //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                        //--------------------------------------------------------------------------------------------------------
                        sTabla = "cv403_det_pedidos";
                        sCampo = "id_det_pedido";

                        iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                            return false;
                        }

                        iIdDetPedido = Convert.ToInt32(iMaximo);

                        //--------------------------------------------------------------------------------------------------------

                        //PROCEDIMIENTO PARA CONSULTAR EL DETALLE DEL PRODUCTO
                        //--------------------------------------------------------------------------------------------------------
                        for (int b = 0; b < dtDetalleItems_P.Rows.Count; b++)
                        {
                            if (Convert.ToInt32(dtDetalleItems_P.Rows[b]["id_producto"].ToString()) == iIdProducto_P)
                            {
                                sDetalleProductoItem = dtDetalleItems_P.Rows[b]["detalle"].ToString().Trim().ToUpper();

                                sSql = "";
                                sSql += "insert into pos_det_pedido_detalle (" + Environment.NewLine;
                                sSql += "id_det_pedido, detalle, estado, fecha_ingreso," + Environment.NewLine;
                                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                                sSql += "values(" + Environment.NewLine;
                                sSql += "@id_det_pedido, @detalle, @estado, getdate()," + Environment.NewLine;
                                sSql += "@usuario_ingreso, @terminal_ingreso)";

                                #region PARAMETROS

                                j = 0;
                                parametro = new SqlParameter[5];
                                parametro[j] = new SqlParameter();
                                parametro[j].ParameterName = "@id_det_pedido";
                                parametro[j].SqlDbType = SqlDbType.Int;
                                parametro[j].Value = iIdDetPedido;
                                j++;

                                parametro[j] = new SqlParameter();
                                parametro[j].ParameterName = "@detalle";
                                parametro[j].SqlDbType = SqlDbType.VarChar;
                                parametro[j].Value = sDetalleProductoItem;
                                j++;

                                parametro[j] = new SqlParameter();
                                parametro[j].ParameterName = "@estado";
                                parametro[j].SqlDbType = SqlDbType.VarChar;
                                parametro[j].Value = "A";
                                j++;

                                parametro[j] = new SqlParameter();
                                parametro[j].ParameterName = "@usuario_ingreso";
                                parametro[j].SqlDbType = SqlDbType.VarChar;
                                parametro[j].Value = Program.sDatosMaximo[0];
                                j++;

                                parametro[j] = new SqlParameter();
                                parametro[j].ParameterName = "@terminal_ingreso";
                                parametro[j].SqlDbType = SqlDbType.VarChar;
                                parametro[j].Value = Program.sDatosMaximo[0];

                                #endregion

                                //EJECUCION DE INSTRUCCION SQL
                                if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                                {
                                    sMensajeError = conexionM.sMensajeError;
                                    return false;
                                }
                            }
                        }

                        //--------------------------------------------------------------------------------------------------------
                    }                    
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR LA COMANDA
        public bool insertarComanda(int iIdPedido_P, int iIdPersona_o_Empresa_P, int iIdPersonaEmpleado_P,
                                    int iIdOrigenOrden_P, Decimal dbTotalDebido_P,
                                    string sEstadoOrden_P, Decimal dbPorcentajeDescuento_P, int iIdMesa_P, 
                                    int iIdCajero_P, int iNumeroPersonas_P, string sComentarios_P, int iIdMesero_P,
                                    int iIdPosTerminal_P, Decimal dbPorcentajeServicio_P, int iConsumoAlimentos_P,
                                    int iIdPromotor_P, int iIdRepartidor_P, int iIdPosCierreCajero_P, DataTable dtItems_P,
                                    DataTable dtDetalleItems_P, int iBanderaActualizarPedido_P, int iIdLocalidad_P,                                    
                                    ConexionBD.ConexionBD conexionM_P)
        {
            try
            {
                this.iIdPedido = iIdPedido_P;
                this.iIdPersona_o_Empresa = iIdPersona_o_Empresa_P;
                this.iIdPersonaEmpleado = iIdPersonaEmpleado_P;
                this.iIdOrigenOrden = iIdOrigenOrden_P;
                this.dbTotalDebido = dbTotalDebido_P;
                this.sEstadoOrden = sEstadoOrden_P;
                this.dbPorcentajeDescuento = dbPorcentajeDescuento_P;
                this.iIdMesa = iIdMesa_P;
                this.iIdCajero = iIdCajero_P;
                this.iNumeroPersonas = iNumeroPersonas_P;
                this.sComentarios = sComentarios_P;
                this.iIdMesero = iIdMesero_P;
                this.iIdPosTerminal = iIdPosTerminal_P;
                this.dbPorcentajeServicio = dbPorcentajeServicio_P;
                this.iConsumoAlimentos = iConsumoAlimentos_P;
                this.iIdPromotor = iIdPromotor_P;
                this.iIdRepartidor = iIdRepartidor_P;
                this.iIdPosCierreCajero = iIdPosCierreCajero_P;
                this.conexionM = conexionM_P;

                dtItems = new DataTable();
                dtItems.Clear();
                dtItems = dtItems_P.Clone();

                foreach(DataRow row in dtItems_P.Rows)
                {
                    dtItems.ImportRow(row);
                }

                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                extraerNumeroCuenta();

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    return false;
                }

                if (iBanderaActualizarPedido_P == 0)
                {
                    #region INSERTANDO UNA NUEVA COMANDA

                    sSql = "";
                    sSql += "insert into cv403_cab_pedidos(" + Environment.NewLine;
                    sSql += "idempresa, cg_empresa, id_localidad, fecha_pedido, id_persona, " + Environment.NewLine;
                    sSql += "cg_tipo_cliente, cg_moneda, porcentaje_iva, id_vendedor, cg_estado_pedido, porcentaje_dscto, " + Environment.NewLine;
                    sSql += "cg_facturado, fecha_ingreso, usuario_ingreso, terminal_ingreso, cuenta, id_pos_mesa, id_pos_cajero, " + Environment.NewLine;
                    sSql += "id_pos_origen_orden, id_pos_orden_dividida, id_pos_jornada, fecha_orden, fecha_apertura_orden, " + Environment.NewLine;
                    sSql += "fecha_cierre_orden, estado_orden, numero_personas, origen_dato, numero_replica_trigger, " + Environment.NewLine;
                    sSql += "estado_replica, numero_control_replica, estado, idtipoestablecimiento, comentarios, id_pos_modo_delivery," + Environment.NewLine;
                    sSql += "id_pos_mesero, id_pos_terminal, porcentaje_servicio, consumo_alimentos, id_pos_cierre_cajero) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + "," + Program.iCgEmpresa + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                    sSql += "'" + sFecha + "', " + iIdPersona_o_Empresa + ",8032," + Program.iMoneda + "," + Environment.NewLine;
                    sSql += (Program.iva * 100) + "," + Program.iIdVendedor + ",6967, " + dbPorcentajeDescuento + ", 7471," + Environment.NewLine;
                    sSql += "GETDATE(),'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + iCuentaDiaria + ", " + Environment.NewLine;
                    sSql += iIdMesa + ", " + iIdCajero + "," + iIdOrigenOrden + ", 0," + Program.iJORNADA + "," + Environment.NewLine;
                    sSql += "'" + sFecha + "', GETDATE(), null, '" + sEstadoOrden_P + "', " + iNumeroPersonas + ", " + Environment.NewLine;
                    sSql += "1, 0, 0, 0, 'A', 1, '" + sComentarios + "', null," + Environment.NewLine;
                    sSql += iIdMesero + ", " + iIdPosTerminal + ", " + dbPorcentajeServicio_P + ", " + iConsumoAlimentos_P + ", ";
                    sSql += iIdPosCierreCajero + ")";

                    Program.iBanderaCliente = 0;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "insert into cv403_cab_despachos (" + Environment.NewLine;
                    sSql += "idempresa, id_persona, cg_empresa, id_localidad, fecha_despacho," + Environment.NewLine;
                    sSql += "cg_motivo_despacho, id_destinatario, punto_partida, cg_ciudad_entrega," + Environment.NewLine;
                    sSql += "direccion_entrega, id_transportador, fecha_inicio_transporte," + Environment.NewLine;
                    sSql += "fecha_fin_transporte, cg_estado_despacho, punto_venta, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + iIdPersona_o_Empresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                    sSql += "'" + sFecha + "', " + Program.iCgMotivoDespacho + ", " + iIdPersona_o_Empresa + "," + Environment.NewLine;
                    sSql += "'" + Program.sPuntoPartida + "', " + Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "'," + Environment.NewLine;
                    sSql += "'" + Program.iIdPersona + "', '" + sFecha + "', '" + sFecha + "', " + Program.iCgEstadoDespacho + "," + Environment.NewLine;
                    sSql += "1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_cab_pedidos";
                    sCampo = "Id_Pedido";
                    iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        //sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iIdPedido = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "select numero_pedido" + Environment.NewLine;
                    sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                    sSql += "where estado = 'A'" + Environment.NewLine;
                    sSql += "and id_localidad = " + Program.iIdLocalidad;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iNumeroPedidoOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                    sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                    sSql += "where id_localidad = " + Program.iIdLocalidad;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                    sSql += "idtipocomprobante,id_pedido, numero_pedido," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "1, " + iIdPedido + ", " + iNumeroPedidoOrden + ", GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_cab_despachos";
                    sCampo = "id_despacho";
                    iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        //sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iIdCabDespachos = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                    sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_despachos_pedidos";
                    sCampo = "id_despacho_pedido";
                    iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iIdDespachoPedido = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                    sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdPersona_o_Empresa + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                    sSql += "7466, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_eventos_cobros";
                    sCampo = "id_evento_cobro";
                    iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        //sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iIdEventoCobro = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                    sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda," + Environment.NewLine;
                    sSql += "valor, cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + "," + Environment.NewLine;
                    sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + dbTotalDebido + "," + Environment.NewLine;
                    sSql += "7460, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    if (insertarDetPedidos(dtItems, dtDetalleItems_P) == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    #region INSERCION PARAMETRIZADA EN CV403_DET_PEDIDOS
                    //for (int i = 0; i < dtItems.Rows.Count; i++)
                    //{
                    //    iIdProducto_P = Convert.ToInt32(dtItems.Rows[i]["id_producto"].ToString());
                    //    dbPrecioUnitario_P = Convert.ToDecimal(dtItems.Rows[i]["valor_unitario"].ToString());
                    //    dbCantidad_P = Convert.ToDecimal(dtItems.Rows[i]["cantidad"].ToString());
                    //    iPagaIva_P = Convert.ToInt32(dtItems.Rows[i]["paga_iva"].ToString());
                    //    sCodigoClaseProducto = dtItems.Rows[i]["codigo_producto"].ToString();
                    //    sNombreProducto_P = dtItems.Rows[i]["nombre_producto"].ToString();

                    //    if (iPagaIva_P == 1)
                    //        dbIVA_P = dbPrecioUnitario_P * Convert.ToDecimal(Program.iva);
                    //    else
                    //        dbIVA_P = 0;

                    //    sSql = "";
                    //    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    //    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    //    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    //    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    //    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                    //    sSql += "id_pos_secuencia_entrega, Estado, numero_replica_trigger," + Environment.NewLine;
                    //    sSql += "numero_control_replica, id_empleado_cliente_empresarial)" + Environment.NewLine;
                    //    sSql += "values(" + Environment.NewLine;
                    //    sSql += iIdPedido + ", " + iIdProducto_P + ", 546, " + dbPrecioUnitario_P + ", " + Environment.NewLine;
                    //    sSql += dbCantidad_P + ", 0, 0, " + dbIVA_P + ", 0, " + Environment.NewLine;
                    //    sSql += "null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    //    sSql += "'" + Program.sDatosMaximo[1] + "', 0, 1, null, 'A', 0, 0, " + iIdPersonaEmpleado + ")";

                    //    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    //    {
                    //        sMensajeError = conexionM.sMensajeError;
                    //        return false;
                    //    }

                    //    //ACTUALIZACION
                    //    //FECHA: 2019-10-04
                    //    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    //    if (sCodigoClaseProducto.Trim() == "02")
                    //    {
                    //        if (Program.iDescargarProductosNoProcesados == 1)
                    //        {
                    //            if (insertarMovimientoProductoNoProcesado(dbCantidad_P) == false)
                    //            {
                    //                return false;
                    //            }
                    //        }
                    //    }

                    //    //ACTUALIZACION
                    //    //FECHA: 2019-10-05
                    //    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                    //    if (sCodigoClaseProducto.Trim() == "03")
                    //    {
                    //        if (Program.iDescargarReceta == 1)
                    //        {
                    //            if (consultarIdReceta(iIdProducto_P, dbCantidad_P, sNombreProducto_P) == false)
                    //            {
                    //                return false;
                    //            }
                    //        }
                    //    }

                    //    sSql = "";
                    //    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    //    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    //    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    //    sSql += "values (" + Environment.NewLine;
                    //    sSql += iIdDespachoPedido + ", " + iIdProducto_P + ", " + dbCantidad_P + ", 'A', 0, 0)";

                    //    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    //    {
                    //        sMensajeError = conexionM.sMensajeError;
                    //        return false;
                    //    }
                    //}
                    #endregion

                    #endregion
                }

                else
                {
                    #region ACTUALIZANDO UNA COMANDA

                    if (eliminarMovimientosBodega(iIdPedido) == false)
                    {
                        return false;
                    }

                    //QUERY PARA ACTUALIZAR LA ORDEN EN CASO DE QUE SOLICITEN CONSUMO DE ALIMENTOS
                    sSql = "";
                    sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                    if (iIdMesa != 0)
                    {
                        sSql += "id_pos_mesa = " + iIdMesa + "," + Environment.NewLine;
                        sSql += "numero_personas = " + iNumeroPersonas + "," + Environment.NewLine;
                    }

                    sSql += "id_persona = " + iIdPersona_o_Empresa + "," + Environment.NewLine;
                    sSql += "porcentaje_dscto = " + dbPorcentajeDescuento + "," + Environment.NewLine;
                    sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                    sSql += "remover_iva = 0," + Environment.NewLine;
                    sSql += "estado_orden = 'Abierta'," + Environment.NewLine;
                    sSql += "consumo_alimentos = " + iConsumoAlimentos + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                    sSql = "";
                    sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                    sSql += "valor = " + dbTotalDebido + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                    sSql = "";
                    sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
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
                    sSql += "and CP.id_pedido = " + iIdPedido;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
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
                            if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                sMensajeError = conexionM.sMensajeError;
                                return false;
                            }
                        }
                    }

                    if (insertarDetPedidos(dtItems, dtDetalleItems_P) == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    #endregion
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        
        //FUNCION PARA INSERTAR LOS PAGOS
        public bool insertarPagos(int iIdPedido_P, DataTable dtPagos_P, Decimal dbTotal_P, Decimal dbCambio_P,
                                  Decimal dbPropina_P, int iIdPersona_P, string sFecha_P, 
                                  int iIdLocalidad_P, int iBanderaSoloEliminarPagos_P, ConexionBD.ConexionBD conexionM_P)
        {
            try
            {
                this.dtPagos = dtPagos_P;
                this.dbTotalDebido = dbTotal_P;
                this.dbCambio = dbCambio_P;
                this.dbPropina = dbPropina_P;
                this.iIdPersona_o_Empresa = iIdPersona_P;
                this.conexionM = conexionM_P;

                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                iCuenta = 0;

                //FUNCION PARA ELIMINAR LOS MOVIMIENTOS DE CAJA
                //--------------------------------------------------------------------------------------------------------

                sSql = "";
                sSql += "select id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = @id_pedido";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pedido";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPedido_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    int iIdDocumentoPago_A;

                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        iIdDocumentoPago_A = Convert.ToInt32(dtConsulta.Rows[i]["id_documento_pago"].ToString());

                        sSql = "";
                        sSql += "select id_pos_movimiento_caja" + Environment.NewLine;
                        sSql += "from pos_movimiento_caja" + Environment.NewLine;
                        sSql += "where id_documento_pago = @id_documento_pago" + Environment.NewLine;
                        sSql += "and estado = @estado";

                        parametro = new SqlParameter[2];
                        parametro[0] = new SqlParameter();
                        parametro[0].ParameterName = "@id_documento_pago";
                        parametro[0].SqlDbType = SqlDbType.Int;
                        parametro[0].Value = iIdDocumentoPago_A;

                        parametro[1] = new SqlParameter();
                        parametro[1].ParameterName = "@estado";
                        parametro[1].SqlDbType = SqlDbType.VarChar;
                        parametro[1].Value = "A";

                        dtAuxiliar = new DataTable();
                        dtAuxiliar.Clear();

                        bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtAuxiliar, sSql, parametro);

                        if (bRespuesta == false)
                        {
                            sMensajeError = conexionM.sMensajeError;
                            return false;
                        }

                        if (dtAuxiliar.Rows.Count > 0)
                        {
                            //ELIMINAR EL REGISTRO DE LA TABLA POS_MOVIMIENTO_CAJA
                            sSql = "";
                            sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                            sSql += "estado = @estado," + Environment.NewLine;
                            sSql += "fecha_anula = getdate()," + Environment.NewLine;
                            sSql += "usuario_anula = @usuario_anula" + Environment.NewLine;
                            sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                            sSql += "where id_pos_movimiento_caja = @id_pos_movimiento_caja";

                            parametro = new SqlParameter[4];
                            parametro[0] = new SqlParameter();
                            parametro[0].ParameterName = "@estado";
                            parametro[0].SqlDbType = SqlDbType.VarChar;
                            parametro[0].Value = "A";

                            parametro[1] = new SqlParameter();
                            parametro[1].ParameterName = "@usuario_anula";
                            parametro[1].SqlDbType = SqlDbType.VarChar;
                            parametro[1].Value = Program.sDatosMaximo[0];

                            parametro[2] = new SqlParameter();
                            parametro[2].ParameterName = "@terminal_anula";
                            parametro[2].SqlDbType = SqlDbType.VarChar;
                            parametro[2].Value = Program.sDatosMaximo[1];

                            parametro[3] = new SqlParameter();
                            parametro[3].ParameterName = "@id_pos_movimiento_caja";
                            parametro[3].SqlDbType = SqlDbType.Int;
                            parametro[3].Value = Convert.ToInt32(dtAuxiliar.Rows[0]["id_pos_movimiento_caja"].ToString());

                            if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                            {
                                sMensajeError = conexionM.sMensajeError;
                                return false;
                            }

                            //ELIMINAR EL REGISTRO DE LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                            sSql = "";
                            sSql += "update pos_numero_movimiento_caja set" + Environment.NewLine;
                            sSql += "estado = @estado," + Environment.NewLine;
                            sSql += "fecha_anula = getdate()," + Environment.NewLine;
                            sSql += "usuario_anula = @usuario_anula" + Environment.NewLine;
                            sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                            sSql += "where id_pos_movimiento_caja = @id_pos_movimiento_caja";

                            //REUSO LA VARIABLE DE PARAMETROS PARA EJECUTAR LA INSTRUCCION SQL
                            if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                            {
                                sMensajeError = conexionM.sMensajeError;
                                return false;
                            }
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iCuenta = Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString());

                if (iCuenta > 0)
                {
                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
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

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                if (iBanderaSoloEliminarPagos_P == 1)
                    return true;


                //INSERTAR EN LA TABLA CV403_PAGOS
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona_o_Empresa + ", '" + sFecha_P + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += dbTotalDebido + ", " + dbPropina + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdLocalidad_P + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 0, 0, " + dbCambio + ")";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                    return false;
                }

                iIdPago = Convert.ToInt32(iMaximo);

                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                for (int i = 0; i < dtPagos.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select cg_tipo_documento, codigo" + Environment.NewLine;
                    sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                    sSql += "where id_pos_tipo_forma_cobro = " + Convert.ToInt32(dtPagos.Rows[i]["id_pos_tipo_forma_cobro"].ToString());

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0]["cg_tipo_documento"].ToString());
                    sCodigoTipoFormaCobro = dtConsulta.Rows[0]["codigo"].ToString().Trim();
                    iConciliacion = Convert.ToInt32(dtPagos.Rows[i]["conciliacion"].ToString());
                    iOperadorTarjeta = Convert.ToInt32(dtPagos.Rows[i]["id_operador_tarjeta"].ToString());
                    iTipoTarjeta = Convert.ToInt32(dtPagos.Rows[i]["id_tipo_tarjeta"].ToString());
                    iBanderaInsertarLote = Convert.ToInt32(dtPagos.Rows[i]["bandera_insertar_lote"].ToString());
                    sNumeroLote = dtPagos.Rows[i]["numero_lote"].ToString();
                    dbPropinaRecibidaFormaPago = Convert.ToDecimal(dtPagos.Rows[i]["propina"].ToString());

                    if (iConciliacion == 1)
                    {
                        int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                        if (iRespuestaNumeroLote == -1)
                        {
                            return false;
                        }

                        if (iRespuestaNumeroLote == 0)
                        {
                            if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                            {
                                return false;
                            }
                        }
                    }

                    sSql = "";
                    sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                    sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                    sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                    sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha_P + "', " + Environment.NewLine;
                    sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dtPagos.Rows[i]["valor"].ToString()) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(dtPagos.Rows[i]["id_pos_tipo_forma_cobro"].ToString()) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                    if (sCodigoTipoFormaCobro == "01")
                    {
                        sSql += (Convert.ToDecimal(dtPagos.Rows[i]["valor"].ToString()) + dbCambio) + ", ";
                    }

                    else
                    {
                        sSql += "null, ";
                    }

                    if (iConciliacion == 1)
                    {
                        sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                    }

                    else
                    {
                        sSql += "null, null, null, ";
                    }

                    sSql += dbPropinaRecibidaFormaPago + ")";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dbTotalDebido + ", 'A', 0, 0, " + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR LA FACTURA
        public bool insertarFactura(int iIdPedido_P, int iIdTipoComprobante_P, int iFacturaElectronica_P,
                                    int iIdPersona_P, int iIdLocalidad_P,  DataTable dtPagos_P, Decimal dbTotalDebido_P, 
                                    int iBanderaRecargoBoton_P, int iBanderaRemoverIvaBoton_P, int iBanderaComandaPendiente_P, 
                                    string sFecha_P, ConexionBD.ConexionBD conexionM_P)
        {
            try
            {
                this.iIdTipoComprobante = iIdTipoComprobante_P;
                this.iFacturaElectronica = iFacturaElectronica_P;
                this.conexionM = conexionM_P;

                //OBTENER LOS DATOS DEL CLIENTE
                //-------------------------------------------------------------------------------------------
                if (consultarRegistro(iIdPersona_P) == false)
                    return false;
                //-------------------------------------------------------------------------------------------

                //OBTENER EL ESTABLECIMIENTO Y PUNTO DE EMISION
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select establecimiento, punto_emision" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdLocalidad_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sEstablecimiento = dtConsulta.Rows[0]["establecimiento"].ToString();
                sPuntoEmision = dtConsulta.Rows[0]["punto_emision"].ToString();
                //-------------------------------------------------------------------------------------------

                //OBTENER EL NUMERO DE COMPROBANTE (FACTURA - NOTA DE ENTREGA)
                //-------------------------------------------------------------------------------------------
                sSql = "";

                if (iIdTipoComprobante == 1)
                    sSql += "select numero_factura numero_comprobante" + Environment.NewLine;
                else
                    sSql += "select numeronotaentrega numero_comprobante" + Environment.NewLine;

                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdLocalidad_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                sNumeroComprobante = dtConsulta.Rows[0]["numero_comprobante"].ToString().Trim();
                //-------------------------------------------------------------------------------------------

                //ACTUALIZAR EL NUMERO DE COMPROBANTE (FACTURA - NOTA DE ENTREGA)
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;

                if (iIdTipoComprobante == 1)
                    sSql += "numero_factura = numero_factura + 1" + Environment.NewLine;
                else
                    sSql += "numeronotaentrega = numeronotaentrega + 1" + Environment.NewLine;

                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdLocalidad_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //FUNCION PARA GENERAR O NO GENERAR LA CLAVE DE ACCESO PARA FACTURACION ELECTRONICA
                //-------------------------------------------------------------------------------------------
                if (iIdTipoComprobante == 1)
                {
                    if (iFacturaElectronica_P == 1)
                    {
                        if (configuracionFacturacion() == false)
                            return false;

                        sClaveAcceso = sGenerarClaveAcceso(sNumeroComprobante, sFecha_P);
                    }                        
                }
                //-------------------------------------------------------------------------------------------

                //LLAMAR LA FUNCION PARA OBTENER LOS REGISTROS DEL SRI PARA LA TABLA CV403_FACTURAS
                //-------------------------------------------------------------------------------------------
                if (llenarDataTable(dtPagos_P) == false)
                    return false;
                //-------------------------------------------------------------------------------------------

                //OBTENER LA SUMA DEL VALOR DE SERVICIO COBRADO
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select isnull(sum(valor_otro), 0) valor_otro" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = @id_pedido";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pedido";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPedido_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                dbServicio_P = Convert.ToDecimal(dtConsulta.Rows[0]["valor_otro"].ToString());
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA ENVIAR DATOS A LA TABLA CV403_FACTURAS
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, id_forma_pago2, id_forma_pago3," + Environment.NewLine;
                sSql += "fecha_factura, fecha_vcto, cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, " + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio," + Environment.NewLine;
                sSql += "facturaelectronica, id_tipo_emision, id_tipo_ambiente, clave_acceso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona_P + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdTipoComprobante + "," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago_1 + ", " + Environment.NewLine;

                if (iIdFormaPago_2 == 0)
                    sSql += "null, ";
                else
                    sSql += iIdFormaPago_2 + ", ";

                if (iIdFormaPago_3 == 0)
                    sSql += "null, ";
                else
                    sSql += iIdFormaPago_3 + ", ";

                sSql += "'" + sFecha_P + "', '" + sFecha_P + "', " + Program.iMoneda + ", " + dbTotalDebido_P + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                sSql += "'" + sDireccionCliente + "', '" + sTelefonoCliente + "', '" + sCiudad + "'," + Environment.NewLine;
                sSql += "'" + sCorreoElectronico + "', " + dbServicio_P + ", " + iFacturaElectronica_P + ", " + iIdTipoEmision + ", " + iIdTipoAmbiente + "," + Environment.NewLine;

                if (iFacturaElectronica_P == 1)
                    sSql += "'" + sClaveAcceso + "')";
                else
                    sSql += "null)";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA OBTENER EL ID_FACTURA DE LA TABLA CV403_FACTURAS
                //-------------------------------------------------------------------------------------------
                sTabla = "cv403_facturas";
                sCampo = "id_factura";

                iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                    return false;
                }

                iIdFactura = Convert.ToInt32(iMaximo);
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA INSERTAR DATOS EN LA TABLA CV403_NUMEROS_FACTURAS
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + sNumeroComprobante + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA INSERTAR DATOS EN LA TABLA CV403_FACTURAS_PEDIDOS
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdPedido_P + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA ACTUALIZAR DATOS EN LA TABLA CV403_DCTOS_POR_COBRAR
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrar + "," + Environment.NewLine;
                sSql += "numero_documento = " + sNumeroComprobante + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P;

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA ACTUALIZAR DATOS EN LA TABLA CV403_CAB_PEDIDOS
                //-------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                if (iBanderaComandaPendiente_P == 1)
                {
                    sSql += "id_pos_cierre_cajero_por_cobrar = " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                }

                sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona_P + "," + Environment.NewLine;
                sSql += "fecha_cierre_orden = GETDATE()," + Environment.NewLine;
                sSql += "recargo_tarjeta = " + iBanderaRecargoBoton_P + "," + Environment.NewLine;
                sSql += "remover_iva = " + iBanderaRemoverIvaBoton_P + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P;

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }
                //-------------------------------------------------------------------------------------------

                //INSTRUCCION PARA ACTUALIZAR DATOS EN LA TABLA CV403_NUMERO_CAB_PEDIDO
                //-------------------------------------------------------------------------------------------
                if (iIdTipoComprobante != 1)
                {
                    sSql = "";
                    sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                    sSql += "idtipocomprobante = " + iIdTipoComprobante + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido_P;

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }
                //-------------------------------------------------------------------------------------------

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR LOS MOVIMIENTOS DE CAJA
        public bool insertarMovimientosCaja(string sNumeroComprobante_P, int iIdPedido_P, int iIdTipoComprobante_P,
                                            int iIdPersona_P, int iNumeroPedido_P, int iIdLocalidad_P,
                                            ConexionBD.ConexionBD conexionM_P)
        {
            try
            {
                this.conexionM = conexionM_P;
                //SELECCIONAR EL ID DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select id_caja" + Environment.NewLine;
                sSql += "from cv405_cajas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and cg_tipo_caja = @cg_tipo_caja";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdLocalidad_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@cg_tipo_caja";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iCgTipoCaja;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iIdCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_caja"].ToString());

                sSql = "";
                sSql += "select descripcion, sum(valor), cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = @id_pedido" + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pedido";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPedido_P;

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtAuxiliar, sSql, parametro);                

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                if (dtAuxiliar.Rows.Count == 0)
                {
                    sMensajeError = "No existe formas de pagos realizados. Comuníquese con el administrador del sistema.";
                    return false;
                }

                if (iIdTipoComprobante_P == 1)
                    sMovimiento = "FACT. No. " + sNumeroComprobante_P;
                else
                    sMovimiento = "N. ENTREGA. No. " + sNumeroComprobante_P;

                for (int i = 0; i < dtAuxiliar.Rows.Count; ++i)
                {
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                    sSql += "tipo_movimiento, idempresa, id_localidad, id_persona, id_cliente," + Environment.NewLine;
                    sSql += "id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto," + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += ", " + Program.iIdPersonaMovimiento + ", " + iIdPersona_P + ", " + iIdCaja + ", 1," + Environment.NewLine;
                    sSql += "'" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDecimal(dtAuxiliar.Rows[i][1].ToString()) + "," + Environment.NewLine;
                    sSql += "'COBRO No. CUENTA " + iNumeroPedido_P.ToString() + " (" + dtAuxiliar.Rows[i][0].ToString() + ")'," + Environment.NewLine;
                    sSql += "'" + sMovimiento.Trim() + "', " + Convert.ToInt32(dtAuxiliar.Rows[i][5].ToString()) + ", " + Program.iJORNADA + "," + Environment.NewLine;
                    sSql += Program.iIdPosCierreCajero + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sTabla = "pos_movimiento_caja";
                    sCampo = "id_pos_movimiento_caja";

                    iMaximo = conexionM.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        sMensajeError = "No se pudo obtener el codigo de la tabla " + sTabla;
                        return false;
                    }

                    iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "select numeromovimientocaja" + Environment.NewLine;
                    sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                    sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                    sSql += "and estado = @estado";

                    parametro = new SqlParameter[2];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_localidad";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iIdLocalidad_P;

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@estado";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = "A";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimientoCaja + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    sSql = "";
                    sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                    sSql += "numeromovimientocaja = numeromovimientocaja + 1" + Environment.NewLine;
                    sSql += "where id_localidad = @id_localidad";

                    parametro = new SqlParameter[1];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_localidad";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iIdLocalidad_P;

                    if (!conexionM.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA CREAR LA CLAVE DE ACCESO
        private string sGenerarClaveAcceso(string sNumeroComprobante_P, string sFecha_P)
        {
            //GENERAR CLAVE DE ACCESO
            string sClaveAcceso_R = "";
            string sFecha_R = Convert.ToDateTime(sFecha_P).ToString("ddMMyyyy");
            string TipoComprobante = "01";
            string NumeroRuc = Program.sNumeroRucEmisor;
            string TipoAmbiente = Program.iTipoAmbiente.ToString();
            string TipoEmision = Program.iTipoEmision.ToString();
            string Serie = sEstablecimiento + sPuntoEmision;
            string NumeroComprobante = sNumeroComprobante_P.Trim().PadLeft(9, '0');
            string DigitoVerificador = "";
            string CodigoNumerico = "12345678";

            sClaveAcceso_R += sFecha_R + TipoComprobante + NumeroRuc + TipoAmbiente;
            sClaveAcceso_R += Serie + NumeroComprobante + CodigoNumerico + TipoEmision;

            DigitoVerificador = sDigitoVerificarModulo11(sClaveAcceso_R);
            sClaveAcceso_R += DigitoVerificador;
            return sClaveAcceso_R;
            //FIN CALVE ACCESO
        }

        //FUNCION PARA EL DIGITO VERIFICADOR MODULO 11
         private string sDigitoVerificarModulo11(string sClaveAcceso)
        {
            Int32 suma = 0;
            int inicio = 7;

            for (int i = 0; i < sClaveAcceso.Length; i++)
            {
                suma = suma + Convert.ToInt32(sClaveAcceso.Substring(i, 1)) * inicio;
                inicio--;
                if (inicio == 1)
                    inicio = 7;
            }

            Decimal modulo = suma % 11;
            suma = 11 - Convert.ToInt32(modulo);

            if (suma == 11)
            {
                suma = 0;
            }
            else if (suma == 10)
            {
                suma = 1;
            }
            //sClaveAcceso = sClaveAcceso + Convert.ToString(suma);

            return suma.ToString();
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

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return -1;
            }
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

                if (!conexionM.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA CREAR UN DATATABLE
        private bool crearDataTable()
        {
            try
            {
                dtAlmacenar = new DataTable();
                dtAlmacenar.Columns.Add("id_forma_pago");
                dtAlmacenar.Columns.Add("valor");
                return true;
            }
            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA LLENAR EL DATATABLE
        private bool llenarDataTable(DataTable dtPagos_P)
        {
            try
            {
                if (crearDataTable() == false)
                    return false;

                for (int i = 0; i < dtPagos_P.Rows.Count; i++)
                {
                    DataRow row = dtAlmacenar.NewRow();
                    row["id_forma_pago"] = dtPagos_P.Rows[i]["id_sri_forma_pago"].ToString();
                    row["valor"] = dtPagos_P.Rows[i]["valor"].ToString();
                    dtAlmacenar.Rows.Add(row);
                }

                IEnumerable<IGrouping<string, DataRow>> query = from item in dtAlmacenar.AsEnumerable()
                                                                group item by item["id_forma_pago"].ToString() into g
                                                                select g;

                dtAgrupado = Transformar(query);

                DataColumn id = new DataColumn("id");
                id.DataType = System.Type.GetType("System.String");
                dtAgrupado.Columns.Add(id);

                for (int i = 0; i < dtAgrupado.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select id_forma_pago" + Environment.NewLine;
                    sSql += "from cv403_formas_pagos" + Environment.NewLine;
                    sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += "and id_sri_forma_pago = " + Convert.ToInt32(dtAgrupado.Rows[i]["id_forma_pago"].ToString()) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        sMensajeError = conexionM.sMensajeError;
                        return false;
                    }

                    if (dtAgrupado.Rows.Count > 0)
                    {
                        dtAgrupado.Rows[i]["id"] = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        dtAgrupado.Rows[i]["id"] = 0;
                    }
                }

                iIdFormaPago_1 = 0;
                iIdFormaPago_2 = 0;
                iIdFormaPago_3 = 0;

                iIdFormaPago_1 = Convert.ToInt32(dtAgrupado.Rows[0]["id"].ToString());

                if (dtAgrupado.Rows.Count > 1)
                {
                    iIdFormaPago_2 = Convert.ToInt32(dtAgrupado.Rows[1]["id"].ToString());
                }

                if (dtAgrupado.Rows.Count > 2)
                {
                    iIdFormaPago_3 = Convert.ToInt32(dtAgrupado.Rows[2]["id"].ToString());
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA TRANSFORMAR EL DATATABLE
        private DataTable Transformar(IEnumerable<IGrouping<string, DataRow>> datos)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id_forma_pago");
                dt.Columns.Add("valor");

                foreach (IGrouping<string, DataRow> item in datos)
                {
                    DataRow row = dt.NewRow();
                    row["id_forma_pago"] = item.Key;
                    row["valor"] = item.Sum<DataRow>(x => Convert.ToDecimal(x["valor"]));
                    dt.Rows.Add(row);
                }

                return dt;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return null;
            }
        }

        //FUNCION PARA CONSULTAR DATOS DEL CLIENTE
        private bool consultarRegistro(int iIdPersona_P)
        {
            try
            {
                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion direccion_cliente," + Environment.NewLine;
                sSql += conexionM.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM tp_personas TP LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = @estado_1" + Environment.NewLine;
                sSql += "and TD.estado = @estado_2 LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = @estado_3" + Environment.NewLine;
                sSql += "WHERE TP.id_persona = @id_persona";

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_3";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@id_persona";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = iIdPersona_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString();
                    sDireccionCliente = dtConsulta.Rows[0]["direccion_cliente"].ToString();
                    sCiudad = dtConsulta.Rows[0]["direccion"].ToString();

                    if (dtConsulta.Rows[0]["telefono_domicilio"].ToString() != "")
                        sTelefonoCliente = dtConsulta.Rows[0]["telefono_domicilio"].ToString();

                    else if (dtConsulta.Rows[0]["celular"].ToString() != "")
                        sTelefonoCliente = dtConsulta.Rows[0]["celular"].ToString();

                    else
                        sTelefonoCliente = "";
                }

                else
                {
                    sMensajeError = "No se encuentran los datos del cliente. Favor comuníquese con el administrador.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA CARGAR LA CONFIGURACION DE LA FACTURACION ELECTRONICA
        private bool configuracionFacturacion()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_ambiente, id_tipo_emision" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = @id_empresa";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_empresa";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == true)
                {
                    iIdTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
                    iIdTipoEmision = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_emision"].ToString());
                }

                else
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        //FUNCION PARA INSERTAR LOS PAGOS
        public bool eliminarPagos(int iIdPedido_P, ConexionBD.ConexionBD conexionM_P)
        {
            try
            {
                this.conexionM = conexionM_P;

                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexionM.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexionM.sMensajeError;
                    return false;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                iCuenta = 0;

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }
    }
}
