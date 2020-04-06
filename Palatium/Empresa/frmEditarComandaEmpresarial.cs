﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmEditarComandaEmpresarial : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        Button[,] boton = new Button[2, 4];
        Button[,] botonProductos = new Button[5, 5];

        string sSql;
        string sPagaIva_P;
        string sNombreProducto_P;
        string sNombreEmpresa;
        string sNombreEmpleado;
        string sFecha;
        string sTabla;
        string sCampo;
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sNumeroMovimientoSecuencial;
        string sAnio;
        string sMes;
        string sCodigoClaseProducto;
        string sNombreSubReceta;

        long iMaximo;

        bool bRespuesta;

        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;

        DataTable dtConsulta;
        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtLocalidad;
        DataTable dtReceta;
        DataTable dtSubReceta;

        int iPosXProductos;
        int iPosYProductos;
        int iCuentaAyudaProductos;
        int iCuentaCategorias;
        int iPosXCategorias;
        int iPosYCategorias;
        int iCuentaAyudaCategorias;
        int iCuentaProductos;
        int iIdPersona;
        int iIdPersonaEmpresa;
        int iIdOrigenOrden;
        int iIdPedido;
        int iCuentaDiaria;
        int iNumeroPedidoOrden;
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

        Decimal dPrecioUnitario_P;
        Decimal dCantidad_P;
        Decimal dIVA_P;
        Decimal dbIva_P;
        Decimal dbTotal_P;
        Decimal dTotalDebido;
        Decimal dbCantidadRecalcular;
        Decimal dbPrecioRecalcular;
        Decimal dbValorTotalRecalcular;

        public frmEditarComandaEmpresarial(int iIdPersona_P, string sNombreEmpresa_P, string sNombreEmpleado_P, int iIdPersonaEmpresa_P, int iIdPedido_P)
        {
            this.iIdPersona = iIdPersona_P;
            this.sNombreEmpresa = sNombreEmpresa_P;
            this.sNombreEmpleado = sNombreEmpleado_P;
            this.iIdPersonaEmpresa = iIdPersonaEmpresa_P;
            this.iIdPedido = iIdPedido_P;
            InitializeComponent();
        }

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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtLocalidad, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                sNumeroMovimientoSecuencial = sTipoMovimiento + sCodigo + sAnioCorto + sMes + iValorActualMovimiento.ToString().PadLeft(4, '0');

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
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

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdPosReceta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_receta"].ToString());

                if (iIdPosReceta == 0)
                {
                    return true;
                }

                sSql = "";
                sSql += "select * from pos_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdPosReceta + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtReceta = new DataTable();
                dtReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtReceta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtReceta.Rows[i]["cantidad_bruta"].ToString());
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
                        sSql += iIdProducto_R + ", " + iIdCabeceraMovimiento + ", 546," + (dbCantidadMateriaPrima_R * dbCantidadProductos_P * -1) + ", 'A')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR LOS ITEMS DE LA SUBRECETA
        private bool insertarComponentesSubReceta(int iIdPosSubReceta_P, Decimal dbCantidadPedida_P, string sNombreProducto_P)
        {
            try
            {
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                int iIdCabeceraMovimientoSubReceta = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtSubReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtSubReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtSubReceta.Rows[i]["cantidad_bruta"].ToString());

                    sSql = "";
                    sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_R + ", " + iIdCabeceraMovimientoSubReceta + ", 546," + (dbCantidadMateriaPrima_R * dbCantidadPedida_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
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

        #endregion

        #region FUNCIONES EL USUARIO

        //FUNCION PARA CARGAR LAS CATEGORIAS
        private void cargarCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva," + Environment.NewLine;
                sSql += "P.subcategoria" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.maneja_almuerzos = 1" + Environment.NewLine;
                sSql += "and id_producto_padre in" + Environment.NewLine;
                sSql += "(select id_producto from cv401_productos where codigo ='2')" + Environment.NewLine;
                sSql += "and modificador = 0" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "order by P.secuencia";

                dtCategorias = new DataTable();
                dtCategorias.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCategorias, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaCategorias = 0;

                if (dtCategorias.Rows.Count > 0)
                {
                    if (dtCategorias.Rows.Count > 8)
                    {
                        btnSiguiente.Enabled = true;
                        btnAnterior.Visible = true;
                        btnSiguiente.Visible = true;
                    }

                    else
                    {
                        btnSiguiente.Enabled = false;
                        btnAnterior.Visible = false;
                        btnSiguiente.Visible = false;
                    }

                    if (crearBotonesCategorias() == false)
                    { }

                }

                else
                {
                    ok.LblMensaje.Text = "No se encuentra ítems de categorías en el sistema.";
                    ok.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES DE CATEGORIAS
        private bool crearBotonesCategorias()
        {
            try
            {
                pnlCategorias.Controls.Clear();

                iPosXCategorias = 0;
                iPosYCategorias = 0;
                iCuentaAyudaCategorias = 0;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Cursor = Cursors.Hand;
                        boton[i, j].Click += new EventHandler(boton_clic_categorias);
                        boton[i, j].Size = new Size(130, 71);
                        boton[i, j].Location = new Point(iPosXCategorias, iPosYCategorias);
                        boton[i, j].BackColor = Color.Lime;
                        boton[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Bold);
                        boton[i, j].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                        boton[i, j].Text = dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString();
                        boton[i, j].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                        pnlCategorias.Controls.Add(boton[i, j]);

                        iCuentaCategorias++;
                        iCuentaAyudaCategorias++;

                        if (j + 1 == 4)
                        {
                            iPosXCategorias = 0;
                            iPosYCategorias += 71;
                        }

                        else
                        {
                            iPosXCategorias += 130;
                        }

                        if (dtCategorias.Rows.Count == iCuentaCategorias)
                        {
                            btnSiguiente.Enabled = false;
                            break;
                        }
                    }

                    if (dtCategorias.Rows.Count == iCuentaCategorias)
                    {
                        btnSiguiente.Enabled = false;
                        break;
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

        //EVENTO CLIC DEL BOTON CATEGORIAS
        private void boton_clic_categorias(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                botonSeleccionadoCategoria = sender as Button;

                lblProductos.Text = botonSeleccionadoCategoria.Text.Trim().ToUpper();

                if (Convert.ToInt32(botonSeleccionadoCategoria.AccessibleDescription) == 0)
                {
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 3);
                }

                else
                {
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 4);
                }

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PRODUCTOS
        private void cargarProductos(int iIdProducto_P, int iNivel_P)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, PP.valor, CP.codigo" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_clase_producto CP ON CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = " + iNivel_P + Environment.NewLine;
                sSql += "and P.is_active = 1" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4" + Environment.NewLine;
                sSql += "and P.id_producto_padre = " + iIdProducto_P + Environment.NewLine;
                sSql += "order by P.secuencia";

                dtProductos = new DataTable();
                dtProductos.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtProductos, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaProductos = 0;

                if (dtProductos.Rows.Count > 0)
                {
                    if (dtProductos.Rows.Count > 16)
                    {
                        btnSiguienteProducto.Enabled = true;
                        btnSiguienteProducto.Visible = true;
                        btnAnteriorProducto.Visible = true;
                    }
                    else
                    {
                        btnSiguienteProducto.Enabled = false;
                        btnSiguienteProducto.Visible = false;
                        btnAnteriorProducto.Visible = false;
                    }
                    if (crearBotonesProductos() == false)
                    { }
                }

                else
                {
                    ok.LblMensaje.Text = "No se encuentra ítems de categorías en el sistema.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES DE PRODUCTOS
        private bool crearBotonesProductos()
        {
            try
            {
                pnlProductos.Controls.Clear();

                iPosXProductos = 0;
                iPosYProductos = 0;
                iCuentaAyudaProductos = 0;

                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        botonProductos[i, j] = new Button();
                        botonProductos[i, j].Cursor = Cursors.Hand;
                        botonProductos[i, j].Click += new EventHandler(boton_clic_productos);
                        botonProductos[i, j].Size = new Size(130, 71);
                        botonProductos[i, j].Location = new Point(iPosXProductos, iPosYProductos);
                        botonProductos[i, j].BackColor = Color.FromArgb(255, 255, 128);
                        botonProductos[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Bold);
                        botonProductos[i, j].Name = dtProductos.Rows[iCuentaProductos]["id_producto"].ToString();
                        botonProductos[i, j].Text = dtProductos.Rows[iCuentaProductos]["nombre"].ToString();
                        botonProductos[i, j].Tag = dtProductos.Rows[iCuentaProductos]["paga_iva"].ToString();
                        botonProductos[i, j].AccessibleDescription = dtProductos.Rows[iCuentaProductos]["codigo"].ToString();
                        botonProductos[i, j].AccessibleName = dtProductos.Rows[iCuentaProductos]["valor"].ToString();
                        pnlProductos.Controls.Add(botonProductos[i, j]);

                        iCuentaProductos++;
                        iCuentaAyudaProductos++;

                        if (j + 1 == 4)
                        {
                            iPosXProductos = 0;
                            iPosYProductos += 71;
                        }

                        else
                        {
                            iPosXProductos += 130;
                        }

                        if (dtProductos.Rows.Count == iCuentaProductos)
                        {
                            btnSiguienteProducto.Enabled = false;
                            break;
                        }
                    }

                    if (dtProductos.Rows.Count == iCuentaProductos)
                    {
                        btnSiguienteProducto.Enabled = false;
                        break;
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

        //EVENTO CLIC DE LOS BOTONES DE PRODUCTOS
        private void boton_clic_productos(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                botonSeleccionadoProducto = sender as Button;

                int iExiste_R = 0;

                Decimal num2 = 0;
                Decimal num3;

                Decimal dbCantidad_R;
                Decimal dbValorUnitario_R;
                Decimal dbSubtotal_R;
                Decimal dbValorIVA_R;
                Decimal dbTotal_R;


                for (int i = 0; i < dgvPedido.Rows.Count; ++i)
                {
                    if (dgvPedido.Rows[i].Cells["idProducto"].Value.ToString() == botonSeleccionadoProducto.Name.ToString())
                    {
                        dbCantidad_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                        dbCantidad_R += 1;
                        dgvPedido.Rows[i].Cells["cantidad"].Value = dbCantidad_R;
                        dbValorUnitario_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value);
                        dbSubtotal_R = dbCantidad_R * dbValorUnitario_R;
                        dgvPedido.Rows[i].Cells["subtotal"].Value = dbSubtotal_R.ToString("N2");
                        dbValorIVA_R = dbSubtotal_R * Convert.ToDecimal(Program.iva);
                        dbTotal_R = dbSubtotal_R + dbValorIVA_R;
                        dgvPedido.Rows[i].Cells["valor"].Value = dbTotal_R.ToString("N2");
                        iExiste_R = 1;
                    }
                }

                if (iExiste_R == 0)
                {
                    int i = dgvPedido.Rows.Add();

                    dgvPedido.Rows[i].Cells["cantidad"].Value = "1";
                    dgvPedido.Rows[i].Cells["producto"].Value = botonSeleccionadoProducto.Text.ToString().Trim();
                    sNombreProducto_P = botonSeleccionadoProducto.Text.ToString().Trim();
                    dgvPedido.Rows[i].Cells["idProducto"].Value = botonSeleccionadoProducto.Name;
                    sPagaIva_P = botonSeleccionadoProducto.Tag.ToString().Trim();
                    dgvPedido.Rows[i].Cells["pagaIva"].Value = sPagaIva_P;
                    dgvPedido.Rows[i].Cells["tipoProducto"].Value = botonSeleccionadoProducto.AccessibleDescription;

                    if (sPagaIva_P == "1")
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                        dgvPedido.Rows[i].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                        dgvPedido.Rows[i].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                        dgvPedido.Rows[i].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                    }
                    else
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.ForeColor = Color.Purple;
                        dgvPedido.Rows[i].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                        dgvPedido.Rows[i].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                        dgvPedido.Rows[i].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                    }

                    dbCantidad_R = 1;
                    dgvPedido.Rows[i].Cells["valuni"].Value = botonSeleccionadoProducto.AccessibleName;
                    dbValorUnitario_R = Convert.ToDecimal(botonSeleccionadoProducto.AccessibleName);
                    dbSubtotal_R = dbCantidad_R * dbValorUnitario_R;
                    dgvPedido.Rows[i].Cells["subtotal"].Value = dbSubtotal_R.ToString("N2");
                    dbValorIVA_R = dbSubtotal_R * Convert.ToDecimal(Program.iva);
                    dbTotal_R = dbSubtotal_R + dbValorIVA_R;
                    dgvPedido.Rows[i].Cells["valor"].Value = dbTotal_R.ToString("N2");
                }

                calcularTotales();
                dgvPedido.ClearSelection();

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR TOTALES
        public void calcularTotales()
        {
            Decimal dSubtotalConIva = 0;
            Decimal dSubtotalCero = 0;
            Decimal dbValorIva;
            Decimal dbSumaIva = 0;
            dTotalDebido = 0;

            for (int i = 0; i < dgvPedido.Rows.Count; ++i)
            {
                if (dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString() == "0")
                {
                    dSubtotalCero += Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString());
                }

                else
                {
                    dSubtotalConIva += Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString());
                    dbValorIva = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString()) * Convert.ToDecimal(Program.iva);
                    dbSumaIva += dbValorIva;
                }
            }

            //dTotalDebido = num1 + num2 - num3 - num4 + (num1 - num3) * Convert.ToDecimal(Program.iva) + num7;
            dTotalDebido = dSubtotalConIva + dSubtotalCero + dbSumaIva;
            lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
        }

        //FUNCION PARA CARGAR LA COMANDA GUARDADA
        private void cargarComanda()
        {
            try
            {
                sSql = "";
                sSql += "select cantidad, nombre, precio_unitario," + Environment.NewLine;
                sSql += "ltrim(str(valor, 10, 2)) valor," + Environment.NewLine;
                sSql += "subtotal, paga_iva, id_producto, codigo" + Environment.NewLine;
                sSql += "from pos_vw_comandas_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

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

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sPagaIva_P = dtConsulta.Rows[i]["paga_iva"].ToString().Trim();

                    dgvPedido.Rows.Add(
                                            dtConsulta.Rows[i]["cantidad"].ToString(),
                                            dtConsulta.Rows[i]["nombre"].ToString(),
                                            dtConsulta.Rows[i]["precio_unitario"].ToString(),
                                            dtConsulta.Rows[i]["valor"].ToString(),
                                            dtConsulta.Rows[i]["subtotal"].ToString(),
                                            sPagaIva_P,
                                            dtConsulta.Rows[i]["id_producto"].ToString(),
                                            dtConsulta.Rows[i]["codigo"].ToString()
                                      );

                    if (sPagaIva_P == "1")
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }

                    else
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.ForeColor = Color.Purple;
                    }
                }

                calcularTotales();
                dgvPedido.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA ACTUALIZAR LA COMANDA
        private void actualizarComanda()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

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

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    return;
                }

                //INICIO DE TRANSACCION EN LA BASE DE  DATOS
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                //ELIMINAR LOS MOVIMIENTOS DE BODEGA
                if (eliminarMovimientosBodega(iIdPedido) == false)
                {
                    goto reversa;
                }

                //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "valor = " + dTotalDebido + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    sPagaIva_P = dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString();
                    sCodigoClaseProducto = dgvPedido.Rows[i].Cells["tipoProducto"].Value.ToString();
                    sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString();

                    if (sPagaIva_P == "1")
                    {
                        dIVA_P = dPrecioUnitario_P * Convert.ToDecimal(Program.iva);
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, id_empleado_cliente_empresarial)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedido + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", 0, 0, " + dIVA_P + ", 0, " + Environment.NewLine;
                    sSql += "null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 0, 1, null, 'A', 0, 0, " + iIdPersonaEmpresa + ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    if (sCodigoClaseProducto.Trim() == "02")
                    {
                        if (Program.iDescargarProductosNoProcesados == 1)
                        {
                            if (insertarMovimientoProductoNoProcesado(Convert.ToDecimal(dCantidad_P)) == false)
                            {
                                goto reversa;
                            }
                        }
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-05
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                    if (sCodigoClaseProducto.Trim() == "03")
                    {
                        if (Program.iDescargarReceta == 1)
                        {
                            if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                            {
                                goto reversa;
                            }
                        }
                    }

                    sSql = "";
                    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDespachoPedido + ", " + dgvPedido.Rows[i].Cells["idProducto"].Value + "," + Environment.NewLine;
                    sSql += dgvPedido.Rows[i].Cells["cantidad"].Value + ", 'A', 1, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Comanda actualizada éxitosamente.";
                ok.ShowDialog();
                Cursor = Cursors.Default;
                this.DialogResult = DialogResult.OK;
                return;
            }

            catch (Exception ex)
            { 
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }        

        #endregion

        private void frmEditarComandaEmpresarial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }

        private void frmEditarComandaEmpresarial_Load(object sender, EventArgs e)
        {
            lblEmpresa.Text = sNombreEmpresa;
            lblEmpleado.Text = sNombreEmpleado;
            cargarCategorias();
        }

        private void dgvPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iFila = dgvPedido.CurrentCell.RowIndex;

            Pedidos.frmAumentaRemueveItems sumar = new Pedidos.frmAumentaRemueveItems(iFila);
            sumar.lblItem.Text = dgvPedido.CurrentRow.Cells["producto"].Value.ToString();
            sumar.lblCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.txtCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.ShowDialog();

            if (sumar.DialogResult == DialogResult.OK)
            {
                dgvPedido.Rows[iFila].Cells["cantidad"].Value = sumar.sValorRetorno;
                dbCantidadRecalcular = Convert.ToDecimal(dgvPedido.Rows[iFila].Cells["cantidad"].Value.ToString());
                dbPrecioRecalcular = Convert.ToDecimal(dgvPedido.Rows[iFila].Cells["valuni"].Value.ToString());
                dbValorTotalRecalcular = dbCantidadRecalcular * dbPrecioRecalcular;
                dgvPedido.Rows[iFila].Cells["valor"].Value = dbValorTotalRecalcular.ToString("N2");
                calcularTotales();
                sumar.Close();
                dgvPedido.ClearSelection();
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay ítems en la comanda.";
                    ok.ShowDialog();
                }

                else if (dgvPedido.SelectedRows.Count > 0)
                {
                    VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                    NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        dgvPedido.Rows.Remove(dgvPedido.CurrentRow);
                        calcularTotales();
                        NuevoSiNo.Close();
                        dgvPedido.ClearSelection();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se ha seleccionado una línea para remover.";
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
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No hay ítems para generar la comanda.";
                ok.ShowDialog();
            }

            else
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar la comanda.?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    actualizarComanda();
                }
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotonesCategorias();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaCategorias -= iCuentaAyudaCategorias;

            if (iCuentaCategorias <= 8)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaCategorias -= 8;
            crearBotonesCategorias();
        }

        private void btnSiguienteProducto_Click(object sender, EventArgs e)
        {
            btnAnteriorProducto.Enabled = true;
            crearBotonesProductos();
        }

        private void btnAnteriorProducto_Click(object sender, EventArgs e)
        {
            iCuentaProductos -= iCuentaAyudaProductos;

            if (iCuentaProductos <= 16)
            {
                btnAnteriorProducto.Enabled = false;
            }

            btnSiguienteProducto.Enabled = true;
            iCuentaProductos -= 16;
            crearBotonesProductos();
        }

        private void frmEditarComandaEmpresarial_Load_1(object sender, EventArgs e)
        {
            lblEmpresa.Text = sNombreEmpresa;
            lblEmpleado.Text = sNombreEmpleado;
            cargarCategorias();
            cargarComanda();
            lblEmpresa.Text = sNombreEmpresa;
            lblEmpleado.Text = sNombreEmpleado;
        }
    }
}