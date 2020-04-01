using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmComanda : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeOK ok_2;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseLimpiarArreglos limpiar = new Clases.ClaseLimpiarArreglos();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        string sSql;
        string reabrir;
        string sNombreParaMesa = "";
        string sNumeroMovimientoSecuencial;
        string sMotivoCortesia_P = "";
        string sMotivoDescuento_P = "";
        string sCodigoProducto_P;
        string sNombreProducto_P;
        string sNombreMesa;
        string sCodigoOrigenOrden;

        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtConsulta;
        DataTable dtItems;
        DataTable dtReceta;
        DataTable dtSubReceta;
        DataTable dtLocalidad;

        DataTable dtCortesiaDescuento;

        long iMaximo;

        bool bRespuesta;

        Button[,] boton = new Button[2, 4];
        Button[,] botonProductos = new Button[5, 5];
        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;

        int iVersionImpresionComanda;
        int contadorCodigo = 0;
        int iIdPedido;
        int iBandera = 0;
        int iConsumoAlimentos;
        int iIdMesa;
        int iPagaIva_P;
        int iBanderaCortesia_P;
        int iBanderaDescuento_P;
        int iBanderaComentario_P;
        int iIdOrdenamiento;
        int iBanderaAbrirPagos;
        int iIdGeneraFactura;
        
        decimal dbCantidadProductoFactor = 1;
        decimal dbCantidadClic = 1;

        //VARIABLES DE LAS CATEGORIAS
        int iCuentaCategorias;
        int iCuentaAyudaCategorias;
        int iPosXCategorias;
        int iPosYCategorias;

        //VARIABLES DE LOS PRODUCTOS
        int iCuentaProductos;
        int iCuentaAyudaProductos;
        int iPosXProductos;
        int iPosYProductos;

        //INTEGRANDO CON LA VERSION ANTERIOR
        int iSecuenciaOrden;
        int iSecuenciaImpresion_P;
        int iSecuenciaEntrega_P;
        int iNumeroPedido;
        int iNumeroPedidoOrden;
        int iIdCabDespachos;
        int iIdEventoCobro;
        int iIdDespachoPedido;
        int iCgTipoDocumento = 2725;
        int icg_estado_dcto = 7460;
        int iCuentaDiaria;
        int iIdMascaraItem;
        int iIdCabPedido_M;
        int iIdCabDespacho_M;
        int iIdDespachoPedido_M;
        int iIdEventoCobro_M;
        int iControlarSecuencia = 0;
        int controlPagoTarjetas = 0;
        int iIdOrigenOrden;
        int iIdDetPedido;
        int iNumeroPersonas;
        int iLongi;
        int iIdMovimientoBodega;
        int iIdPosReceta;
        int iIdBodega;
        int iCgClienteProveedor;
        int iTipoMovimiento;
        int iIdLocalidadBodega;
        int iValorActualMovimiento;
        int iIdCabeceraMovimiento;
        int iIdBodegaInsumos;
        int iIdPromotor;
        int iIdRepartidor;
        int iCategoriaDelivery;

        int iIdProducto;
        int iIdOrden_P;
        int iIdProducto_P;
        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPago;
        int iIdDocumentoPagado;
        int iIdPersona;
        int iIdCajero;
        int iIdMesero;
        int iIdPosSubReceta;
        int iBanderaDescargaStock;
        int iIdMovimientoStock;
        int iCgClienteProveedor_Sub;
        int iCgTipoMovimiento_Sub;
        int p, q;
        int iBandera2;

        int[] iRespuesta;

        Int32 iIdCliente;

        string sTabla;
        string sCampo;
        //string sFechaPedido;
        string sFecha;
        //string sFechaConsulta;
        string sDescripcionOrigen;
        string sNombreMesero;

        string sGuardarComentario;
        string sAnio;
        string sMes;
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sNombreSubReceta;
        string sReferenciaExterna_Sub;
        string sHistoricoOrden;
        string sLlenarInformacionCuenta;
        
        double dPrecioUnitario_P;
        double dCantidad_P;
        double dDescuento_P;
        double dIVA_P;
        double dServicio;
        double dValorDescuento;
        double dbCantidadRecalcular;
        double dbPrecioRecalcular;
        double dbValorTotalRecalcular;

        Double dPorcentajeDescuento;
        Decimal dbTotalDebido_REC;

        Double dPorcentajeCalculado;
        Double dbPorcentajePorLinea_P;

        public frmComanda(int iIdOrigenOrden, string sDescripcionOrigen, int iNumeroPersonas, 
                          int iIdMesa, int iIdPedido, string reabrir, int iIdCajero, int iIdMesero,
                          string sNombreMesero, string sNombreMesa_P, int iIdRepartidor_P, 
                          int iIdPromotor_P, int iIdPersona_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden;
            this.sDescripcionOrigen = sDescripcionOrigen;
            this.iNumeroPersonas = iNumeroPersonas;
            this.iIdMesa = iIdMesa;
            this.iIdPedido = iIdPedido;
            this.reabrir = reabrir;
            this.iIdCajero = iIdCajero;
            this.iIdMesero = iIdMesero;
            this.sNombreMesero = sNombreMesero;
            this.sNombreMesa = sNombreMesa_P;
            this.iIdRepartidor = iIdRepartidor_P;
            this.iIdPromotor = iIdPromotor_P;
            this.iIdPersona = iIdPersona_P;

            InitializeComponent();

            descuentoEmpleados();
        }

        public frmComanda(int iIdPedido, string reabrir)
        {
            this.iIdPedido = iIdPedido;
            this.reabrir = reabrir;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                sNumeroMovimientoSecuencial = sTipoMovimiento + sCodigo + sAnioCorto + sMes + iValorActualMovimiento.ToString().PadLeft(4, '0');

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
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
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
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
                    int iCgUnidad = Convert.ToInt32(dtSubReceta.Rows[i]["cg_unidad"].ToString());

                    sSql = "";
                    sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_R + ", " + iIdCabeceraMovimientoSubReceta + ", " + iCgUnidad + "," + (dbCantidadMateriaPrima_R * dbCantidadPedida_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VERIFICAR SI ES UN DESCUENTO DE EMPLEADOS
        private void descuentoEmpleados()
        {
            try
            {
                sSql = "";
                sSql += "select codigo from pos_origen_orden" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                if (dtConsulta.Rows[0][0].ToString().Trim() == "06")
                {
                    dPorcentajeDescuento = Program.descuento_empleados * 100;
                    sCodigoOrigenOrden = "06";
                }

                else
                {
                    dPorcentajeDescuento = 0;
                    sCodigoOrigenOrden = "0";
                }

                lblPorcentajeDescuento.Text = dPorcentajeDescuento.ToString("N2") + "%";         
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL PRODUCTO MOVILIZACION
        private void consultarMovilizacion()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre, PP.valor, P.paga_iva" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.id_producto = " + Program.iIdProductoDomicilio + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra configurado el registro de Domicilio.";
                    ok.ShowDialog();
                    this.Close();
                    return;
                }

                Decimal dbValUni_P = Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString().Trim());

                iPagaIva_P = Convert.ToInt32(dtConsulta.Rows[0]["paga_iva"].ToString().Trim());

                dgvPedido.Rows.Add("1",
                                    dtConsulta.Rows[0]["nombre"].ToString().Trim().ToUpper(),
                                    dbValUni_P.ToString(),
                                    dbValUni_P.ToString("N2"),
                                    dtConsulta.Rows[0]["id_producto"].ToString().Trim(),
                                    iPagaIva_P.ToString(),
                                    "00",
                                    iVersionImpresionComanda.ToString(),
                                    "0", "", "0", "", "0", "0", "0", "0", "0", "0");

                recalcularValores();

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE VERSION DE LA COMANDA
        private void versionImpresion()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(max(isnull(secuencia, 1)), 0) maximo" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    this.Close();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iVersionImpresionComanda = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
                }

                else
                {
                    iVersionImpresionComanda = 0;
                }

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CONSULTA DE DATOS PARA LLENAR LAS CAJAS DE TEXTO
        private bool consultarDatosOrden()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_cabecera_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                sDescripcionOrigen = dtConsulta.Rows[0]["tipo_orden"].ToString().Trim().ToUpper();
                iNumeroPersonas = Convert.ToInt32(dtConsulta.Rows[0]["numero_personas"].ToString());
                iIdMesa = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_mesa"].ToString());
                iIdCajero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cajero"].ToString());
                iIdMesero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_mesero"].ToString());
                sNombreMesero = dtConsulta.Rows[0]["nombre_mesero"].ToString();
                sNombreMesa = dtConsulta.Rows[0]["descripcion_mesa"].ToString();
                iIdRepartidor = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_repartidor"].ToString());
                iIdPromotor = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_promotor"].ToString());

                sHistoricoOrden = dtConsulta.Rows[0]["numero_pedido"].ToString();                
                iConsumoAlimentos = Convert.ToInt32(dtConsulta.Rows[0]["consumo_alimentos"].ToString());
                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                lblCliente.Text = dtConsulta.Rows[0]["cliente"].ToString();

                if (cargarDetalleGrid() == false)
                {
                    return false;
                }

                recalcularValores();
                pintarDataGridView();
                dgvPedido.ClearSelection();

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CARGAR EL DETALLE DE LA ORDEN EN EL DATAGRID
        private bool cargarDetalleGrid()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_detalle_comanda" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvPedido.Rows.Add(Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString()).ToString(),
                                       dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                       dtConsulta.Rows[i]["precio_unitario"].ToString().Trim(),
                                       dtConsulta.Rows[i]["precio_total"].ToString().Trim(),
                                       dtConsulta.Rows[i]["id_producto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["paga_iva"].ToString().Trim(),
                                       dtConsulta.Rows[i]["codigo_producto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["secuencia"].ToString().Trim(),
                                       dtConsulta.Rows[i]["bandera_cortesia"].ToString().Trim(),
                                       dtConsulta.Rows[i]["motivo_cortesia"].ToString().Trim(),
                                       dtConsulta.Rows[i]["bandera_descuento"].ToString().Trim(),
                                       dtConsulta.Rows[i]["motivo_descuento"].ToString().Trim(),
                                       dtConsulta.Rows[i]["id_pos_mascara_item"].ToString().Trim(),
                                       dtConsulta.Rows[i]["id_pos_secuencia_entrega"].ToString().Trim(),
                                       dtConsulta.Rows[i]["ordenamiento"].ToString().Trim(),
                                       dtConsulta.Rows[i]["porcentaje_descuento_info"].ToString().Trim(),
                                       dtConsulta.Rows[i]["bandera_comentario"].ToString().Trim(),
                                       dtConsulta.Rows[i]["valor_dscto"].ToString().Trim()
                                       );

                    //LLENAR LA MATRIZ DE DETALLE ITEMS CON LOS DATOS INGRESADOS EN LOS DETALLES EN CASO DE QUE SI HAYA
                    sSql = "";
                    sSql += "select PD.detalle, P.id_producto" + Environment.NewLine;
                    sSql += "from pos_det_pedido_detalle PD, cv403_det_pedidos DP, cv401_productos P" + Environment.NewLine;
                    sSql += "where PD.id_det_pedido = DP.id_det_pedido " + Environment.NewLine;
                    sSql += "and DP.id_producto = P.id_producto " + Environment.NewLine;
                    sSql += "and PD.id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and DP.estado = 'A'" + Environment.NewLine;
                    sSql += "and PD.estado = 'A'";

                    dtItems = new DataTable();
                    dtItems.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtItems, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    if (dtItems.Rows.Count > 0)
                    {
                        Program.sDetallesItems[Program.iContadorDetalle, 0] = dtItems.Rows[0][1].ToString();

                        for (int j = 1; j <= dtItems.Rows.Count; j++)
                        {
                            Program.sDetallesItems[Program.iContadorDetalle, j] = dtItems.Rows[j - 1][0].ToString();
                        }

                        Program.iContadorDetalle++;
                    }
                }

                if (Program.iReimprimirCocina == 0)
                {
                    chkImprimirCocina.Checked = false;
                }

                else
                {
                    chkImprimirCocina.Checked = true;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }
        
        //FUNCION PARA LLAMAR A LA FORMA DE DESCUENTOS
        private void invocarFormaDescuentos(int iOp)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay ítems ingresados.";
                    ok.ShowDialog();
                    return;
                }

                construirDataTable();

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    DataRow row = dtCortesiaDescuento.NewRow();
                    row["cantidad"] = dgvPedido.Rows[i].Cells["cantidad"].Value.ToString();
                    row["nombre_producto"] = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                    row["valor_unitario"] = dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString();
                    row["valor_total"] = dgvPedido.Rows[i].Cells["valor_total"].Value.ToString();
                    row["id_producto"] = dgvPedido.Rows[i].Cells["id_producto"].Value.ToString();
                    row["paga_iva"] = dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString();
                    row["codigo_producto"] = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                    row["secuencia_impresion"] = dgvPedido.Rows[i].Cells["secuencia_impresion"].Value.ToString();
                    row["bandera_cortesia"] = dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString();
                    row["motivo_cortesia"] = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    row["bandera_descuento"] = dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString();
                    row["motivo_descuento"] = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                    row["id_mascara"] = dgvPedido.Rows[i].Cells["id_mascara"].Value.ToString();
                    row["id_ordenamiento"] = dgvPedido.Rows[i].Cells["id_ordenamiento"].Value.ToString();
                    row["ordenamiento"] = dgvPedido.Rows[i].Cells["ordenamiento"].Value.ToString();
                    row["porcentaje_descuento"] = dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value.ToString();
                    row["bandera_comentario"] = dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString();
                    row["valor_descuento"] = dgvPedido.Rows[i].Cells["valor_descuento"].Value.ToString();

                    dtCortesiaDescuento.Rows.Add(row);
                }

                ComandaNueva.frmCortesiasDescuentos descuentos = new frmCortesiasDescuentos(dtCortesiaDescuento, iOp);
                descuentos.ShowDialog();

                if (descuentos.DialogResult == DialogResult.OK)
                {
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    dtConsulta = descuentos.dt;
                    descuentos.Close();

                    dgvPedido.Rows.Clear();

                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        dgvPedido.Rows.Add(dtConsulta.Rows[i]["cantidad"].ToString(),
                                           dtConsulta.Rows[i]["nombre_producto"].ToString(),
                                           dtConsulta.Rows[i]["valor_unitario"].ToString(),
                                           dtConsulta.Rows[i]["valor_total"].ToString(),
                                           dtConsulta.Rows[i]["id_producto"].ToString(),
                                           dtConsulta.Rows[i]["paga_iva"].ToString(),
                                           dtConsulta.Rows[i]["codigo_producto"].ToString(),
                                           dtConsulta.Rows[i]["secuencia_impresion"].ToString(),
                                           dtConsulta.Rows[i]["bandera_cortesia"].ToString(),
                                           dtConsulta.Rows[i]["motivo_cortesia"].ToString(),
                                           dtConsulta.Rows[i]["bandera_descuento"].ToString(),
                                           dtConsulta.Rows[i]["motivo_descuento"].ToString(),
                                           dtConsulta.Rows[i]["id_mascara"].ToString(),
                                           dtConsulta.Rows[i]["id_ordenamiento"].ToString(),
                                           dtConsulta.Rows[i]["ordenamiento"].ToString(),
                                           dtConsulta.Rows[i]["porcentaje_descuento"].ToString(),
                                           dtConsulta.Rows[i]["bandera_comentario"].ToString(),
                                           dtConsulta.Rows[i]["valor_descuento"].ToString()

                            );
                    }

                    pintarDataGridView();
                    recalcularValores();
                    dgvPedido.ClearSelection();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL DATATABLE PARA ENVIAR A CORTESIAS O DESCUENTOS
        private void construirDataTable()
        {
            try
            {
                dtCortesiaDescuento = new DataTable();
                dtCortesiaDescuento.Clear();

                dtCortesiaDescuento.Columns.Add("cantidad");
                dtCortesiaDescuento.Columns.Add("nombre_producto");
                dtCortesiaDescuento.Columns.Add("valor_unitario");
                dtCortesiaDescuento.Columns.Add("valor_total");
                dtCortesiaDescuento.Columns.Add("id_producto");
                dtCortesiaDescuento.Columns.Add("paga_iva");
                dtCortesiaDescuento.Columns.Add("codigo_producto");
                dtCortesiaDescuento.Columns.Add("secuencia_impresion");
                dtCortesiaDescuento.Columns.Add("bandera_cortesia");
                dtCortesiaDescuento.Columns.Add("motivo_cortesia");
                dtCortesiaDescuento.Columns.Add("bandera_descuento");
                dtCortesiaDescuento.Columns.Add("motivo_descuento");
                dtCortesiaDescuento.Columns.Add("id_mascara");
                dtCortesiaDescuento.Columns.Add("id_ordenamiento");
                dtCortesiaDescuento.Columns.Add("ordenamiento");
                dtCortesiaDescuento.Columns.Add("porcentaje_descuento");
                dtCortesiaDescuento.Columns.Add("bandera_comentario");
                dtCortesiaDescuento.Columns.Add("valor_descuento");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LA ETIQUETA DE USUARIO
        private void llenarDatosInformativosComanda()
        {
            try
            {
                string sEtiqueta = "";
                sEtiqueta += "INFORMACIÓN DE LA COMANDA" + Environment.NewLine;
                sEtiqueta += "ORDEN: " + sDescripcionOrigen.ToUpper() + Environment.NewLine;
                sEtiqueta += "Mesero: " + sNombreMesero + Environment.NewLine;
                sEtiqueta += "# Orden: " + sHistoricoOrden + Environment.NewLine;

                if (sNombreParaMesa.Trim() == "")
                {
                    sEtiqueta += "# Mesa: " + sNombreMesa + Environment.NewLine;
                }

                else
                {
                    sEtiqueta += "# Mesa: " + sNombreMesa + " - " + sNombreParaMesa + Environment.NewLine;
                }

                sEtiqueta += "# Personas: " + iNumeroPersonas;

                txtDatosComanda.Text = sEtiqueta;                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO HISTÓRICO DE ORDEN
        private void extraerNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" +  sSql;
                    catchMensaje.ShowDialog();
                    this.Close();
                    return;
                }

                if (dtConsulta.Rows.Count  == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ocurrió un problema al realizar la extraer el número de pedido";
                    ok.ShowDialog();
                    this.Close();
                    return;
                }

                sHistoricoOrden = dtConsulta.Rows[0][0].ToString();

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
                return;
            }
        }

        //FUNCION PARA EXTRAER EL ULTIMO NUMERO DE DE CUENTA INGRESADO
        private bool extraerNumeroCuenta()
        {
            try
            {
                //sFechaConsulta = DateTime.Now.ToString("yyyy/MM/dd");
                //sFechaConsulta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString()) + 1;
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CARGAR LOS BOTONES DE CATEGORIA
        private void cargarCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva," + Environment.NewLine;
                sSql += "P.subcategoria, isnull(P.categoria_delivery, 0) categoria_delivery" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "and modificador = 0" + Environment.NewLine;
                sSql += "order by P.secuencia";

                dtCategorias = new DataTable();
                dtCategorias.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCategorias, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (iCategoriaDelivery == 0)
                {
                    for (int i = dtCategorias.Rows.Count -1; i >= 0; i--)
                    {
                        if (Convert.ToInt32(dtCategorias.Rows[i]["categoria_delivery"].ToString()) == 1)
                        {
                            dtCategorias.Rows.RemoveAt(i);
                        }
                    }
                }

                iCuentaCategorias = 0;

                if (dtCategorias.Rows.Count > 0)
                {
                    if (dtCategorias.Rows.Count > 8)
                    {
                        btnSiguiente.Enabled = true;
                    }

                    else
                    {
                        btnSiguiente.Enabled = false;
                    }

                    if (crearBotonesCategorias() == false)
                    {
                        
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES
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
                        boton[i, j].Click += boton_clic_categorias;
                        boton[i, j].Size = new Size(115, 71);
                        boton[i, j].Location = new Point(iPosXCategorias, iPosYCategorias);                        
                        boton[i, j].BackColor = Color.Lime;
                        boton[i, j].Font = new Font("Maiandra GD", 8.25F, FontStyle.Bold);
                        boton[i, j].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                        boton[i, j].Text = dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString();
                        boton[i, j].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                        boton[i, j].FlatStyle = FlatStyle.Flat;
                        boton[i, j].FlatAppearance.BorderSize = 1;
                        boton[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);
                                                
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
                            iPosXCategorias += 115;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EVENTO CLIC DE LOS BOTONES DE LAS CATEGORÍAS
        private void boton_clic_categorias(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                botonSeleccionadoCategoria = sender as Button;

                if (Convert.ToInt32(botonSeleccionadoCategoria.AccessibleDescription) == 0)
                {
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 3, botonSeleccionadoCategoria.Text.Trim().ToUpper());
                }
                else
                {
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 4, botonSeleccionadoCategoria.Text.Trim().ToUpper());
                }

                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA CARGAR LOS BOTONES DE PRODUCTOS
        private void cargarProductos(int iIdProducto_P, int iNivel_P, string sNombreCategoria_P)
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaProductos = 0;

                if (dtProductos.Rows.Count > 0)
                {
                    lblProductos.Text = sNombreCategoria_P;

                    if (dtProductos.Rows.Count > 20)
                    {
                        btnSiguienteProducto.Enabled = true;
                    }

                    else
                    {
                        btnSiguienteProducto.Enabled = false;
                    }

                    if (crearBotonesProductos() == false)
                    {

                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES
        private bool crearBotonesProductos()
        {
            try
            {
                pnlProductos.Controls.Clear();
                iPosXProductos = 0;
                iPosYProductos = 0;
                iCuentaAyudaProductos = 0;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        botonProductos[i, j] = new Button();
                        botonProductos[i, j].Cursor = Cursors.Hand;
                        botonProductos[i, j].Click += boton_clic_productos;
                        botonProductos[i, j].Size = new Size(115, 71);
                        botonProductos[i, j].Location = new Point(iPosXProductos, iPosYProductos);
                        botonProductos[i, j].BackColor = Color.FromArgb(255, 255, 128);
                        botonProductos[i, j].Font = new Font("Maiandra GD", 8.25F, FontStyle.Bold);
                        botonProductos[i, j].Name = dtProductos.Rows[iCuentaProductos]["id_producto"].ToString();
                        botonProductos[i, j].Text = dtProductos.Rows[iCuentaProductos]["nombre"].ToString();
                        botonProductos[i, j].AccessibleDescription = dtProductos.Rows[iCuentaProductos]["codigo"].ToString();
                        botonProductos[i, j].AccessibleName = dtProductos.Rows[iCuentaProductos]["valor"].ToString();
                        botonProductos[i, j].Tag = dtProductos.Rows[iCuentaProductos]["paga_iva"].ToString();
                        botonProductos[i, j].FlatStyle = FlatStyle.Flat;
                        botonProductos[i, j].FlatAppearance.BorderSize = 1;
                        botonProductos[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);

                        pnlProductos.Controls.Add(botonProductos[i, j]);
                        iCuentaProductos++;
                        iCuentaAyudaProductos++;

                        if (j + 1 == 5)
                        {
                            iPosXProductos = 0;
                            iPosYProductos += 71;
                        }

                        else
                        {
                            iPosXProductos += 115;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EVENTO CLIC DE LOS BOTOTNES DE PRODUCTOS
        private void boton_clic_productos(object sender, EventArgs e)
        {
            try
            {
                int iIdProductoGrid;
                int iBanderaCortesiaGrid;
                int iBanderaDescuentoGrid;
                int iVersionImpresionGrid;
                Double dbCantidadGrid;
                Decimal dbPrecioProductoGrid;

                this.Cursor = Cursors.WaitCursor;

                botonSeleccionadoProducto = sender as Button;

                dbPrecioProductoGrid = Convert.ToDecimal(botonSeleccionadoProducto.AccessibleName);

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iIdProductoGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["id_producto"].Value);
                    iBanderaCortesiaGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_cortesia"].Value);
                    iBanderaDescuentoGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_descuento"].Value);
                    iVersionImpresionGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["secuencia_impresion"].Value);

                    if ((iIdProductoGrid == Convert.ToInt32(botonSeleccionadoProducto.Name)) && 
                        (iBanderaCortesiaGrid == 0) && (iBanderaDescuentoGrid == 0) &&
                        (iVersionImpresionGrid == iVersionImpresionComanda))
                    {
                        dbCantidadGrid = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                        dbCantidadGrid += Convert.ToDouble(dbCantidadProductoFactor);
                        dgvPedido.Rows[i].Cells["cantidad"].Value = dbCantidadGrid.ToString();
                        dgvPedido.Rows[i].Cells["valor_total"].Value = (Convert.ToDecimal(dbCantidadGrid) * dbPrecioProductoGrid).ToString("N2");
                        this.Cursor = Cursors.Default;
                        pintarDataGridView();
                        recalcularValores();
                        dgvPedido.ClearSelection();
                        dbCantidadProductoFactor = 1;
                        btnMitad.BackColor = Color.FromArgb(192, 255, 192);
                        return;
                    }
                }

                if (sCodigoOrigenOrden == "06")
                {
                    Double dbAuxiliarDesc = Convert.ToDouble(dbPrecioProductoGrid) * Program.descuento_empleados;

                    dgvPedido.Rows.Add(dbCantidadProductoFactor,
                                        botonSeleccionadoProducto.Text.Trim().ToUpper(),
                                        botonSeleccionadoProducto.AccessibleName,
                                        (dbPrecioProductoGrid * dbCantidadProductoFactor).ToString("N2"),
                                        botonSeleccionadoProducto.Name.ToString(),
                                        botonSeleccionadoProducto.Tag.ToString(),
                                        botonSeleccionadoProducto.AccessibleDescription,
                                        iVersionImpresionComanda.ToString(),
                                        "0", "", "0", "", "0", "0", "0", Program.descuento_empleados * 100, "0", dbAuxiliarDesc);
                }

                else
                {
                    dgvPedido.Rows.Add(dbCantidadProductoFactor,
                                        botonSeleccionadoProducto.Text.Trim().ToUpper(),
                                        botonSeleccionadoProducto.AccessibleName,
                                        (dbPrecioProductoGrid * dbCantidadProductoFactor).ToString("N2"),
                                        botonSeleccionadoProducto.Name.ToString(),
                                        botonSeleccionadoProducto.Tag.ToString(),
                                        botonSeleccionadoProducto.AccessibleDescription,
                                        iVersionImpresionComanda.ToString(),
                                        "0", "", "0", "", "0", "0", "0", "0", "0", "0");
                }

                pintarDataGridView();
                dgvPedido.ClearSelection();
                dbCantidadProductoFactor = 1;
                btnMitad.BackColor = Color.FromArgb(192, 255, 192);
                recalcularValores();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA PINTAR EL DATAGRID
        private void pintarDataGridView()
        {
            try
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                    }

                    else
                    {
                        dgvPedido.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULAR
        private void recalcularValores()
        {
            try
            {
                int iPagaIva_REC;

                Decimal dbCantidad_REC;
                Decimal dbPrecioUnitario_REC;
                Decimal dbValorDescuento_REC;
                Decimal dbValorIva_REC;

                Decimal dbSumaSubtotalConIva_REC = 0;
                Decimal dbSumaSubtotalSinIva_REC = 0;
                Decimal dbSumaDescuentoConIva_REC = 0;
                Decimal dbSumaDescuentoSinIva_REC = 0;

                Decimal dbSumaSubtotales_REC;
                Decimal dbSumaDescuentos_REC;

                Decimal dbSubtotalNeto_REC = 0;
                Decimal dbSumaIva_REC = 0;
                Decimal dbSumaServicio_REC = 0;

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iPagaIva_REC = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_iva"].Value);

                    dbCantidad_REC = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    dbPrecioUnitario_REC = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valor_unitario"].Value);
                    dbValorDescuento_REC = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valor_descuento"].Value);

                    if (iPagaIva_REC == 0)
                    {
                        dbSumaSubtotalSinIva_REC += dbCantidad_REC * dbPrecioUnitario_REC;
                        dbSumaDescuentoSinIva_REC += dbCantidad_REC * dbValorDescuento_REC;                        
                    }

                    else
                    {
                        dbSumaSubtotalConIva_REC += dbCantidad_REC * dbPrecioUnitario_REC;
                        dbSumaDescuentoConIva_REC += dbCantidad_REC * dbValorDescuento_REC;
                        dbValorIva_REC = (dbPrecioUnitario_REC - dbValorDescuento_REC) * Convert.ToDecimal(Program.iva);
                        dbSumaIva_REC += dbCantidad_REC * dbValorIva_REC;
                    }                    
                }

                dbSumaSubtotales_REC = dbSumaSubtotalConIva_REC + dbSumaSubtotalSinIva_REC;
                dbSumaDescuentos_REC = dbSumaDescuentoConIva_REC + dbSumaDescuentoSinIva_REC;

                dbSubtotalNeto_REC = dbSumaSubtotalConIva_REC + dbSumaSubtotalSinIva_REC - dbSumaDescuentoConIva_REC - dbSumaDescuentoSinIva_REC;
                dbTotalDebido_REC = dbSubtotalNeto_REC + dbSumaIva_REC;

                lblSubtotal.Text = "$ " + dbSumaSubtotales_REC.ToString("N2");
                lblDescuento.Text = "$ " + dbSumaDescuentos_REC.ToString("N2");
                lblImpuestos.Text = "$ " + dbSumaIva_REC.ToString("N2");
                lblTotal.Text = "$ " + dbTotalDebido_REC.ToString("N2");

                //FUNCION PARA OBTENER EL PORCENTAJE DE DESCUENTO
                Decimal dbSumaPrecioUnitario_D = 0;
                Decimal dbSumaDescuentos_D = 0;
                Decimal dbCantidad_D;
                Decimal dbPrecioUnitario_D;
                Decimal dbValorDescuento_D;
                //Decimal dbResultado_D;

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dbCantidad_D = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    dbPrecioUnitario_D = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valor_unitario"].Value);
                    dbValorDescuento_D = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valor_descuento"].Value);

                    dbSumaPrecioUnitario_D += dbCantidad_D * dbPrecioUnitario_D;
                    dbSumaDescuentos_D += dbCantidad_D * dbValorDescuento_D;
                }

                dPorcentajeDescuento = Convert.ToDouble((dbSumaDescuentos_D * 100) / dbSumaPrecioUnitario_D);

                lblPorcentajeDescuento.Text = dPorcentajeDescuento.ToString("N2") + "%";                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULAR CONSUMO EMPLEADOS
        private void recalcularValoresConsumoEmpleados(Button btnConsumo_Empleados)
        {
            try
            {
                int iIdProductoGrid;
                int iBanderaCortesiaGrid;
                int iBanderaDescuentoGrid;
                int iVersionImpresionGrid;
                Double dbCantidadGrid;
                Decimal dbPrecioProductoGrid;

                this.Cursor = Cursors.WaitCursor;

                dbPrecioProductoGrid = Convert.ToDecimal(btnConsumo_Empleados.AccessibleName);

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iIdProductoGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["id_producto"].Value);
                    iBanderaCortesiaGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_cortesia"].Value);
                    iBanderaDescuentoGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_descuento"].Value);
                    iVersionImpresionGrid = Convert.ToInt32(dgvPedido.Rows[i].Cells["secuencia_impresion"].Value);

                    if ((iIdProductoGrid == Convert.ToInt32(btnConsumo_Empleados.Name)) &&
                        (iBanderaCortesiaGrid == 0) && (iBanderaDescuentoGrid == 0) &&
                        (iVersionImpresionGrid == iVersionImpresionComanda))
                    {
                        dbCantidadGrid = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                        dbCantidadGrid += Convert.ToDouble(dbCantidadProductoFactor);
                        dgvPedido.Rows[i].Cells["cantidad"].Value = dbCantidadGrid.ToString();
                        dgvPedido.Rows[i].Cells["valor_total"].Value = (Convert.ToDecimal(dbCantidadGrid) * dbPrecioProductoGrid).ToString("N2");
                        this.Cursor = Cursors.Default;
                        pintarDataGridView();
                        recalcularValores();
                        dgvPedido.ClearSelection();
                        dbCantidadProductoFactor = 1;
                        btnMitad.BackColor = Color.FromArgb(192, 255, 192);
                        return;
                    }
                }

                dgvPedido.Rows.Add(dbCantidadProductoFactor,
                                    btnConsumo_Empleados.Text.Trim().ToUpper(),
                                    btnConsumo_Empleados.AccessibleName,
                                    (dbPrecioProductoGrid * dbCantidadProductoFactor).ToString("N2"),
                                    btnConsumo_Empleados.Name.ToString(),
                                    btnConsumo_Empleados.Tag.ToString(),
                                    btnConsumo_Empleados.AccessibleDescription,
                                    iVersionImpresionComanda.ToString(),
                                    "0", "", "0", "", "0", "0", "0", "0", "0", "0");

                pintarDataGridView();
                dgvPedido.ClearSelection();
                dbCantidadProductoFactor = 1;
                btnMitad.BackColor = Color.FromArgb(192, 255, 192);
                recalcularValores();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES PARA INSERTAR EN LA BASE DE DATOS

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarComanda(int iOp)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                iBanderaAbrirPagos = 0;

                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

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

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                //AQUI EXTRAEMOS EL NUMERO DE CUENTA DIARIA
                if (extraerNumeroCuenta() == false)
                {
                    return;
                }

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    return;
                }

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                /*  LA VARIABLE IOP CONTROLA UNA NUEVA ORDEN O ACTUALIZA
                 *  IOP 1: ACTUALIZA EL PEDIDO
                 *  IOP 0: INSERTAR UN NUEVO PEDIDO
                 */
                if (iOp == 1)
                {
                    if (actualizarComanda() == false)
                    {
                        goto reversa;
                    }

                    this.Cursor = Cursors.Default;
                }

                else
                {
                    if (insertarNuevaComanda() == false)
                    {
                        goto reversa;
                    }

                    reabrir = "OK";

                    if (Program.iReimprimirCocina == 0)
                    {
                        chkImprimirCocina.Checked = false;
                    }

                    else
                    {
                        chkImprimirCocina.Checked = true;
                    }

                    this.Cursor = Cursors.Default;
                }

                iBanderaAbrirPagos = 1;
                return;
            }

            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); Cursor = Cursors.Default; }

        }

        //FUNCION PARA INSERTAR UNA NUEVA ORDEN
        private bool insertarNuevaComanda()
        {
            try
            {    
                //QUERY PARA INSERTAR UNA NUEVA ORDEN EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "insert into cv403_cab_pedidos(" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, fecha_pedido, id_persona, cg_tipo_cliente," + Environment.NewLine;
                sSql += "cg_moneda, porcentaje_iva, id_vendedor, cg_estado_pedido, porcentaje_dscto," + Environment.NewLine;
                sSql += "cg_facturado, cuenta, id_pos_mesa, id_pos_cajero, id_pos_origen_orden," + Environment.NewLine;
                sSql += "id_pos_orden_dividida, id_pos_jornada, fecha_orden, fecha_apertura_orden," + Environment.NewLine;
                sSql += "fecha_cierre_orden, estado_orden, numero_personas, idtipoestablecimiento," + Environment.NewLine;
                sSql += "comentarios, id_pos_modo_delivery, id_pos_mesero, id_pos_terminal," + Environment.NewLine;
                sSql += "porcentaje_servicio, consumo_alimentos, id_pos_promotor, id_pos_repartidor," + Environment.NewLine;
                sSql += "id_pos_cierre_cajero, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "origen_dato, numero_replica_trigger, estado_replica, numero_control_replica) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + "," + Program.iCgEmpresa + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + iIdPersona + ", 8032," + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + ", " + Program.iIdVendedor + ",6967," + dPorcentajeDescuento + ", 7471," + Environment.NewLine; //AQUI OBTENER EL PORCENTAJE DE DESCUENTO
                sSql += iCuentaDiaria + ", " + iIdMesa + ", " + iIdCajero + "," + iIdOrigenOrden + ", 0, ";
                sSql += Program.iJORNADA + ", '" + sFecha + "', GETDATE(), null, 'Abierta'," + Environment.NewLine;
                sSql += iNumeroPersonas + ", 1, '" + sNombreParaMesa + "', " + Program.iModoDelivery + ", ";
                sSql += iIdMesero + ", " + Program.iIdPosTerminal + ", " + (Program.servicio * 100) + ", " + iConsumoAlimentos + ", " + Environment.NewLine;
                sSql += iIdPromotor + ", " + iIdRepartidor + ", " + Program.iIdPosCierreCajero + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA INSERTAR EN CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "insert into cv403_cab_despachos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, cg_empresa, id_localidad, fecha_despacho," + Environment.NewLine;
                sSql += "cg_motivo_despacho, id_destinatario, punto_partida, cg_ciudad_entrega," + Environment.NewLine;
                sSql += "direccion_entrega, id_transportador, fecha_inicio_transporte," + Environment.NewLine;
                sSql += "fecha_fin_transporte, cg_estado_despacho, punto_venta, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iCgMotivoDespacho + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += "'" + Program.sPuntoPartida + "', " + Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "'," + Environment.NewLine;
                sSql += "'" + Program.iIdPersona + "', '" + sFecha + "', '" + sFecha + "', " + Program.iCgEstadoDespacho + "," + Environment.NewLine;
                sSql += "1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENEMOS EL MAX ID DE LA TABLA CV403_CAB_PEDIDOS
                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPedido = Convert.ToInt32(iMaximo);

                //PROCEDIMIENTO PARA EXTRAER EL NUMERO DE PEDIDO
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iNumeroPedidoOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                //QUERY PARA ACTUALIZAR EL NUMERO DE PEDIDO EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA PODER INSERTAR REGISTRO EN LA TABLA CV403_NUMERO_CAB_PEDIDO
                sSql = "";
                sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1, " + iIdPedido + ", " + iNumeroPedidoOrden + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                sTabla = "cv403_cab_despachos";
                sCampo = "id_despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdCabDespachos = Convert.ToInt32(iMaximo);

                //QUERY PARA INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS_PEDIDOS
                sTabla = "cv403_despachos_pedidos";
                sCampo = "id_despacho_pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdDespachoPedido = Convert.ToInt32(iMaximo);

                //QUERY PARA INSERTAR UN NUEVO REGISTRO EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdPersona + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "7466, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                sTabla = "cv403_eventos_cobros";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdEventoCobro = Convert.ToInt32(iMaximo);

                //QUERY PARA INSERTAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda," + Environment.NewLine;
                sSql += "valor, cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + dbTotalDebido_REC + "," + Environment.NewLine;
                sSql += icg_estado_dcto + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (insertarDetPedidos() == false)
                {
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                limpiar.limpiarArregloComentarios();

                if (Program.iHabilitarDestinosImpresion == 1)
                {
                    if (chkImprimirCocina.Checked == true)
                    {
                        if (Program.iEjecutarImpresion == 1)
                        {
                            Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion_P);
                            cocina.ShowDialog();
                        }
                    }
                }

                if (Program.iImprimeOrden == 1)
                {
                    Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(iIdPedido.ToString(), 1, "Abierta");
                    precuenta.ShowDialog();
                }

                ok_2 = new VentanasMensajes.frmMensajeOK();
                ok_2.LblMensaje.Text = "Guardado en la orden: " + sHistoricoOrden + ".";
                ok_2.ShowDialog();

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ACTUALIZAR LA ORDEN
        private bool actualizarComanda()
        {
            try
            {
                //ACTUALIZACION
                //FECHA: 2019-10-04
                //ELIMINACION DE MOVIMIENTOS DE BODEGA

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

                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "porcentaje_dscto = " + dPorcentajeDescuento + "," + Environment.NewLine;
                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0," + Environment.NewLine;
                sSql += "estado_orden = 'Abierta'," + Environment.NewLine;
                sSql += "consumo_alimentos = " + iConsumoAlimentos + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "valor = " + dbTotalDebido_REC + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }
                }

                if (insertarDetPedidos() == false)
                {
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                limpiar.limpiarArregloComentarios();

                if (Program.iHabilitarDestinosImpresion == 1)
                {
                    if (chkImprimirCocina.Checked == true)
                    {
                        if (Program.iEjecutarImpresion == 1)
                        {
                            Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion_P);
                            cocina.ShowDialog();
                        }
                    }
                }

                ok_2 = new VentanasMensajes.frmMensajeOK();
                ok_2.LblMensaje.Text = "Guardado en la orden: " + sHistoricoOrden + ".";
                ok_2.ShowDialog();
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA RECORRER EL DATAGRID CUANDO SE ESTÁ INSERTANDO LA INFORMACIÓN
        private bool insertarDetPedidos()
        {
            try
            {
                //INSTRUCCIONES PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["id_producto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["valor_unitario"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    dDescuento_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["valor_descuento"].Value);
                    iPagaIva_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString());
                    iBanderaCortesia_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString());
                    iBanderaDescuento_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString());
                    iBanderaComentario_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString());
                    iIdMascaraItem = Convert.ToInt32(dgvPedido.Rows[i].Cells["id_mascara"].Value);
                    iSecuenciaEntrega_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["id_ordenamiento"].Value);
                    iSecuenciaImpresion_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["secuencia_impresion"].Value);
                    sMotivoCortesia_P = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    sMotivoDescuento_P = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                    sCodigoProducto_P = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                    sNombreProducto_P = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                    dbPorcentajePorLinea_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value);

                    if (iBanderaComentario_P == 1)
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                    }

                    else
                    {
                        sGuardarComentario = "";
                    }

                    //ACTUALIZACION DE CODIGO PARA RECALCULAR EL PORCENTAJE DE SERVICIO
                    if (Program.iManejaServicio == 1)
                    {
                        //dServicio = (dPrecioUnitario_P - dValorDescuento) * Program.servicio;

                        if (dCantidad_P < 1)
                        {
                            dServicio = ((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) * Program.servicio;
                        }

                        else
                        {
                            //dServicio = (dPrecioUnitario_P - dValorDescuento) * Program.servicio;
                            dServicio = (((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) / dCantidad_P) * Program.servicio;
                        }
                    }

                    if (iPagaIva_P == 1)
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

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
                    sSql += iIdPedido + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, " + iIdMascaraItem + ", " + iSecuenciaImpresion_P + "," + Environment.NewLine;
                    sSql += iSecuenciaEntrega_P + ", " + iBanderaCortesia_P + ", '" + sMotivoCortesia_P + "', " + iBanderaDescuento_P + "," + Environment.NewLine;
                    sSql += "'" + sMotivoDescuento_P + "', " + dbPorcentajePorLinea_P + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
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

                    //INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    for (p = 0; p < Program.iContadorDetalle; p++)
                    {
                        if (Program.sDetallesItems[p, 0] == dgvPedido.Rows[i].Cells["id_producto"].Value.ToString())
                        {
                            iBandera2 = 1;
                            break;
                        }
                    }

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
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
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
                                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CONSULTAR EL CLIENTE
        private void consultarClienteInicio()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona;

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

                if (dtConsulta.Rows.Count == 0)
                {
                    lblCliente.Text = "CONSUMIDOR FINAL";
                    iIdPersona = Program.iIdPersona;
                }

                else
                {
                    lblCliente.Text = dtConsulta.Rows[0][0].ToString().Trim().ToUpper();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR SI GENERA FACTURA
        private void consultarGeneraFactura()
        {
            try
            {
                sSql = "";
                sSql += "select genera_factura " + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdGeneraFactura = 0;
                }

                else
                {
                    iIdGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0]["genera_factura"].ToString());
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmComanda_Load(object sender, EventArgs e)
        {
            //this.Text = Program.sEtiqueta;

            if (Program.iManejaMitad == 1)
            {
                btnMitad.Enabled = true;
            }

            else
            {
                btnMitad.Enabled = false;
            }          

            if (reabrir == "OK" || reabrir == "DIVIDIDO")
            {
                if (consultarDatosOrden() == false)
                {
                    this.Close();
                    return;
                }

                consultarGeneraFactura();
                versionImpresion();
            }

            else
            {
                iVersionImpresionComanda = 1;
                extraerNumeroOrden();

                consultarGeneraFactura();
                consultarClienteInicio();                

                if (Program.sCodigoAsignadoOrigenOrden == "03")
                {
                    if (Program.iManejaDeliveryVariable == 1)
                    {
                        iCategoriaDelivery = 1;
                    }

                    else
                    {
                        consultarMovilizacion();
                    }
                }
            }

            cargarCategorias();
            llenarDatosInformativosComanda();
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

        private void btnAnteriorProducto_Click(object sender, EventArgs e)
        {
            iCuentaProductos -= iCuentaAyudaProductos;

            if (iCuentaProductos <= 20)
            {
                btnAnteriorProducto.Enabled = false;
            }

            btnSiguienteProducto.Enabled = true;
            iCuentaProductos -= 20;

            crearBotonesProductos();
        }

        private void btnSiguienteProducto_Click(object sender, EventArgs e)
        {
            btnAnteriorProducto.Enabled = true;
            crearBotonesProductos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMitad_Click(object sender, EventArgs e)
        {
            if (dbCantidadProductoFactor == 1)
            {
                btnMitad.BackColor = Color.Red;
                dbCantidadProductoFactor = Convert.ToDecimal(0.5);
            }

            else
            {
                btnMitad.BackColor = Color.FromArgb(192, 255, 192);
                dbCantidadProductoFactor = 1;
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay ítems en la comanda.";
                    ok.ShowDialog();
                    return;
                }

                if (dgvPedido.SelectedRows.Count > 0)
                {

                    if (Program.iPuedeCobrar == 1)
                    {
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            dgvPedido.Rows.Remove(dgvPedido.CurrentRow);

                            if (dgvPedido.Rows.Count == 0)
                            {
                                lblSubtotal.Text = "$ 0.00";
                                lblDescuento.Text = "$ 0.00";
                                lblImpuestos.Text = "$ 0.00";
                                lblTotal.Text = "$ 0.00";

                                dPorcentajeDescuento = 0;

                                lblPorcentajeDescuento.Text = dPorcentajeDescuento.ToString("N2") + "%";      
                            }

                            else
                            {
                                recalcularValores();
                            }

                            pintarDataGridView();
                            NuevoSiNo.Close();
                            dgvPedido.ClearSelection();
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Su usuario no le permite remover el ítem. Póngase en contacto con el administrador.";
                        ok.ShowDialog();
                        return;
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se ha seleccionado una línea para remover.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        private void btnEditarItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay ítems en la comanda.";
                    ok.ShowDialog();
                    return;
                }

                if (dgvPedido.Rows.Count > 0)
                {
                    ComandaNueva.frmModificarItemsPedido item = new ComandaNueva.frmModificarItemsPedido();
                    item.txtProducto.Text = dgvPedido.CurrentRow.Cells["nombre_producto"].Value.ToString();
                    item.txtCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
                    item.txtTotal.Text = dgvPedido.CurrentRow.Cells["valor_total"].Value.ToString();
                    item.iIdProducto = Convert.ToInt32(dgvPedido.CurrentRow.Cells["id_producto"].Value);
                    item.ShowDialog();
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay ningún item ingresado para realizar variaciones.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        private void btnModificadores_Click(object sender, EventArgs e)
        {
            ok = new VentanasMensajes.frmMensajeNuevoOk();
            ok.lblMensaje.Text = "Módulo en desarrollo.";
            ok.ShowDialog();
        }

        private void btnCortesias_Click(object sender, EventArgs e)
        {
            invocarFormaDescuentos(0);
        }

        private void btnDescuentos_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay ítems ingresados.";
                ok.ShowDialog();
                return;
            }

            ComandaNueva.frmNuevoDescuento descuento = new ComandaNueva.frmNuevoDescuento();
            descuento.ShowDialog();

            if (descuento.DialogResult == DialogResult.OK)
            {
                Decimal dbPorcentajeDescuento_R = descuento.dbPorcentajeDescuento;
                descuento.Close();

                dPorcentajeDescuento = Convert.ToDouble(dbPorcentajeDescuento_R);

                Decimal dbPrecioUnitario_R;
                Decimal dbResultado_R;

                //RECORRER EL DATAGRID
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dbPrecioUnitario_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valor_unitario"].Value);
                    dbResultado_R = dbPrecioUnitario_R * (dbPorcentajeDescuento_R / 100);

                    dgvPedido.Rows[i].Cells["valor_descuento"].Value = dbResultado_R.ToString();
                    dgvPedido.Rows[i].Cells["bandera_comentario"].Value = "0";
                    dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value = dbPorcentajeDescuento_R.ToString();
                }

                recalcularValores();
            }
        }

        private void btnNuevoItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.iIdProductoNuevoItem == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra configurado la sección Ítem. Favor comúniquese con el administrador.";
                    ok.ShowDialog();
                    return;
                }

                ComandaNueva.frmCrearNuevoItem item = new frmCrearNuevoItem();
                item.ShowDialog();

                if (item.DialogResult == DialogResult.OK)
                {
                    Decimal dbCantidad_I = item.dCantidad;
                    Decimal dbPrecioUnitario_I = item.dValorUnitario;
                    string sNombreItem_I = item.sNombreProducto;
                    item.Close();

                    dgvPedido.Rows.Add(dbCantidad_I,
                                    sNombreItem_I,
                                    dbPrecioUnitario_I,
                                    (dbPrecioUnitario_I * dbCantidad_I).ToString("N2"),
                                    Program.iIdProductoNuevoItem,
                                    "1", "00",
                                    iVersionImpresionComanda.ToString(), 
                                    "0", "", "0", "", "0", "0", "0", "0", "1", "0");

                    recalcularValores();
                    pintarDataGridView();
                    dgvPedido.ClearSelection();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnConsumoAlimentos_Click(object sender, EventArgs e)
        {
            try
            {
                Pedidos.frmOpcionesConsumoAlimentos consumo = new Pedidos.frmOpcionesConsumoAlimentos(iConsumoAlimentos);
                consumo.ShowDialog();

                //PARA CONVERTIR CADA LINEA DE PRODUCTO EN CONSUMO DE ALIMENTOS
                if (consumo.DialogResult == DialogResult.OK)
                {
                    consumo.Close();
                    construirDataTable();

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        DataRow row = dtCortesiaDescuento.NewRow();
                        row["cantidad"] = dgvPedido.Rows[i].Cells["cantidad"].Value.ToString();
                        row["nombre_producto"] = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                        row["valor_unitario"] = dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString();
                        row["valor_total"] = dgvPedido.Rows[i].Cells["valor_total"].Value.ToString();
                        row["id_producto"] = dgvPedido.Rows[i].Cells["id_producto"].Value.ToString();
                        row["paga_iva"] = dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString();
                        row["codigo_producto"] = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                        row["secuencia_impresion"] = dgvPedido.Rows[i].Cells["secuencia_impresion"].Value.ToString();
                        row["bandera_cortesia"] = dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString();
                        row["motivo_cortesia"] = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                        row["bandera_descuento"] = dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString();
                        row["motivo_descuento"] = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                        row["id_mascara"] = dgvPedido.Rows[i].Cells["id_mascara"].Value.ToString();
                        row["id_ordenamiento"] = dgvPedido.Rows[i].Cells["id_ordenamiento"].Value.ToString();
                        row["ordenamiento"] = dgvPedido.Rows[i].Cells["ordenamiento"].Value.ToString();
                        row["porcentaje_descuento"] = dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value.ToString();
                        row["bandera_comentario"] = dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString();
                        row["valor_descuento"] = dgvPedido.Rows[i].Cells["valor_descuento"].Value.ToString();

                        dtCortesiaDescuento.Rows.Add(row);
                    }

                    ComandaNueva.frmMascaraItems mascara = new frmMascaraItems(dtCortesiaDescuento);
                    mascara.ShowDialog();

                    if (mascara.DialogResult == DialogResult.OK)
                    {
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();
                        dtConsulta = mascara.dt;
                        mascara.Close();

                        dgvPedido.Rows.Clear();

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvPedido.Rows.Add(dtConsulta.Rows[i]["cantidad"].ToString(),
                                               dtConsulta.Rows[i]["nombre_producto"].ToString(),
                                               dtConsulta.Rows[i]["valor_unitario"].ToString(),
                                               dtConsulta.Rows[i]["valor_total"].ToString(),
                                               dtConsulta.Rows[i]["id_producto"].ToString(),
                                               dtConsulta.Rows[i]["paga_iva"].ToString(),
                                               dtConsulta.Rows[i]["codigo_producto"].ToString(),
                                               dtConsulta.Rows[i]["secuencia_impresion"].ToString(),
                                               dtConsulta.Rows[i]["bandera_cortesia"].ToString(),
                                               dtConsulta.Rows[i]["motivo_cortesia"].ToString(),
                                               dtConsulta.Rows[i]["bandera_descuento"].ToString(),
                                               dtConsulta.Rows[i]["motivo_descuento"].ToString(),
                                               dtConsulta.Rows[i]["id_mascara"].ToString(),
                                               dtConsulta.Rows[i]["id_ordenamiento"].ToString(),
                                               dtConsulta.Rows[i]["ordenamiento"].ToString(),
                                               dtConsulta.Rows[i]["porcentaje_descuento"].ToString(),
                                               dtConsulta.Rows[i]["bandera_comentario"].ToString(),
                                               dtConsulta.Rows[i]["valor_descuento"].ToString()

                                );
                        }

                        pintarDataGridView();
                        recalcularValores();
                        dgvPedido.ClearSelection();
                    }
                }

                //PARA APLICAR CONSUMO DE ALIMENTOS A TODA LA ORDEN
                else if (consumo.DialogResult == DialogResult.Yes)
                {
                    iConsumoAlimentos = consumo.iSeleccion;

                    if (iConsumoAlimentos == 1)
                    {
                        btnConsumoAlimentos.BackColor = Color.Yellow;
                    }

                    else
                    {
                        btnConsumoAlimentos.BackColor = Color.FromArgb(192, 255, 192);
                    }

                    consumo.Close();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnReimprimirCocina_Click(object sender, EventArgs e)
        {
            try
            {
                if ((reabrir == "OK") || (reabrir == "DIVIDIDO"))
                {
                    if ((iVersionImpresionComanda - 1) == 1)
                    {
                        Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iVersionImpresionComanda - 1);
                        cocina.ShowDialog();
                    }

                    else
                    {
                        Pedidos.frmVersionesCocina cocina = new Pedidos.frmVersionesCocina(iIdPedido, iVersionImpresionComanda - 1);
                        cocina.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La orden aún no ha sido guardada.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnCambiarMesa_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdMesa == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El tipo de comanda seleccionado no maneja la sección de mesas.";
                    ok.ShowDialog();
                    return;
                }

                Areas.frmCambioSeccionMesa mesas_1 = new Areas.frmCambioSeccionMesa();
                mesas_1.ShowDialog();

                if (mesas_1.DialogResult == DialogResult.OK)
                {
                    iIdMesa = mesas_1.iIdMesa;
                    sNombreMesa = mesas_1.sDescripcionMesa.ToUpper();
                    mesas_1.Close();
                    llenarDatosInformativosComanda();
                }

                //Áreas.frmCambioMesa mesas = new Áreas.frmCambioMesa();
                //AddOwnedForm(mesas);
                //mesas.ShowDialog();

                //if (mesas.DialogResult == DialogResult.OK)
                //{
                //    iIdMesa = mesas.iIdMesa;
                //    sNombreMesa = mesas.sDescripcionMesa.ToUpper();
                //    mesas.Close();
                //    llenarDatosInformativosComanda();
                //}
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnRenombrarMesa_Click(object sender, EventArgs e)
        {
            ComandaNueva.frmRenombrarMesa nombre = new frmRenombrarMesa(sNombreParaMesa);
            nombre.ShowDialog();

            if (nombre.DialogResult == DialogResult.OK)
            {
                sNombreParaMesa = nombre.sNombreMesa;
                nombre.Close();
                llenarDatosInformativosComanda();
            }
        }

        private void btnNumeroPersonas_Click(object sender, EventArgs e)
        {
            agregarPersonas personas = new agregarPersonas(iNumeroPersonas.ToString());
            personas.ShowDialog();

            if (personas.DialogResult == DialogResult.OK)
            {
                iNumeroPersonas = Convert.ToInt32(personas.txt_valor.Text.Trim());
                llenarDatosInformativosComanda();
                personas.Close();
            }
        }

        private void btnDividirComanda_Click(object sender, EventArgs e)
        {
            try
            {
                if ((reabrir == "OK") || (reabrir == "DIVIDIDO"))
                {
                    if (dgvPedido.Rows.Count == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No hay ítems ingresados.";
                        ok.ShowDialog();
                        return;
                    }

                    construirDataTable();

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        Double dbCantidadGrid = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString());
                        Double dbAuxiliarGrid = 1;
                        Double dbPrecioUnitarioGrid = Convert.ToDouble(dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString());

                        for (Double j = dbCantidadGrid; j > 0; j--)
                        {
                            if (dbCantidadGrid < 1)
                            {
                                dbAuxiliarGrid = dbCantidadGrid;
                            }

                            DataRow row = dtCortesiaDescuento.NewRow();
                            //row["cantidad"] = dgvPedido.Rows[i].Cells["cantidad"].Value.ToString();
                            row["cantidad"] = dbAuxiliarGrid.ToString();
                            row["nombre_producto"] = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                            row["valor_unitario"] = dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString();
                            //row["valor_total"] = dgvPedido.Rows[i].Cells["valor_total"].Value.ToString();
                            row["valor_total"] = (dbAuxiliarGrid * dbPrecioUnitarioGrid).ToString("N2");
                            row["id_producto"] = dgvPedido.Rows[i].Cells["id_producto"].Value.ToString();
                            row["paga_iva"] = dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString();
                            row["codigo_producto"] = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                            row["secuencia_impresion"] = dgvPedido.Rows[i].Cells["secuencia_impresion"].Value.ToString();
                            row["bandera_cortesia"] = dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString();
                            row["motivo_cortesia"] = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                            row["bandera_descuento"] = dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString();
                            row["motivo_descuento"] = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                            row["id_mascara"] = dgvPedido.Rows[i].Cells["id_mascara"].Value.ToString();
                            row["id_ordenamiento"] = dgvPedido.Rows[i].Cells["id_ordenamiento"].Value.ToString();
                            row["ordenamiento"] = dgvPedido.Rows[i].Cells["ordenamiento"].Value.ToString();
                            row["porcentaje_descuento"] = dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value.ToString();
                            row["bandera_comentario"] = dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString();
                            row["valor_descuento"] = dgvPedido.Rows[i].Cells["valor_descuento"].Value.ToString();

                            dtCortesiaDescuento.Rows.Add(row);

                            dbCantidadGrid -= 1;                            
                        }                        
                    }

                    ComandaNueva.frmDivisionCuentas d = new ComandaNueva.frmDivisionCuentas(dtCortesiaDescuento, iIdPedido, "0", iIdCajero,
                                                                                            iIdMesero, iIdMesa, iIdOrigenOrden,
                                                                                            Convert.ToInt32(sHistoricoOrden), iIdPromotor, iIdRepartidor);
                    d.ShowDialog();

                    if (d.DialogResult == DialogResult.OK)
                    {
                        d.Close();
                        this.Close();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La orden aún no ha sido guardada.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay productos ingresados en la comanda.";
                    ok.ShowDialog();
                    return;
                }

                //AQUI ACTUALIZA LA COMANDA 
                if (reabrir == "OK" || reabrir == "DIVIDIDO")
                {
                    insertarComanda(1);
                    this.Close();
                }

                //AQUI INSERTA UNA NUEVA ORDEN
                else
                {
                    //INSERCION DE PROMOTOR
                    if (Program.iManejaPromotor == 1)
                    {
                        Promotores.frmSeleccionarPromotor promotor = new Promotores.frmSeleccionarPromotor();
                        promotor.ShowDialog();

                        if (promotor.DialogResult == DialogResult.OK)
                        {
                            iIdPromotor = promotor.iIdPromotor;
                            promotor.Close();
                        }
                    }

                    insertarComanda(0);
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.iPuedeCobrar == 1)
                {
                    if (dgvPedido.Rows.Count == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No hay pedidos para realizar el cobro.";
                        ok.ShowDialog();
                        return;
                    }

                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido;

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

                    //AQUI ACTUALIZA LA COMANDA 
                    if (reabrir == "OK" || reabrir == "DIVIDIDO")
                    {
                        insertarComanda(1);                        
                    }

                    //AQUI INSERTA UNA NUEVA ORDEN
                    else
                    {
                        //INSERCION DE PROMOTOR
                        if (Program.iManejaPromotor == 1)
                        {
                            Promotores.frmSeleccionarPromotor promotor = new Promotores.frmSeleccionarPromotor();
                            promotor.ShowDialog();

                            if (promotor.DialogResult == DialogResult.OK)
                            {
                                iIdPromotor = promotor.iIdPromotor;
                                promotor.Close();
                            }
                        }

                        insertarComanda(0);                        
                    }

                    if (iBanderaAbrirPagos == 1)
                    {
                        if (iIdGeneraFactura == 1)
                        {
                            Pedidos.frmCobros t = new Pedidos.frmCobros(iIdPedido.ToString());
                            t.ShowDialog();

                            if (t.DialogResult == DialogResult.OK)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }

                        else
                        {
                            Pedidos.frmCobrosEspeciales especial = new Pedidos.frmCobrosEspeciales(iIdPedido.ToString());
                            especial.ShowDialog();

                            if (especial.DialogResult == DialogResult.OK)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Su usuario no le permite realizar el cobro de la cuenta.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgvPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iFila = dgvPedido.CurrentCell.RowIndex;

            Pedidos.frmAumentaRemueveItems sumar = new Pedidos.frmAumentaRemueveItems(iFila);
            sumar.lblItem.Text = dgvPedido.CurrentRow.Cells["nombre_producto"].Value.ToString();
            sumar.lblCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.txtCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.ShowDialog();

            if (sumar.DialogResult == DialogResult.OK)
            {
                dgvPedido.Rows[iFila].Cells["cantidad"].Value = sumar.sValorRetorno;
                dbCantidadRecalcular = Convert.ToDouble(dgvPedido.Rows[iFila].Cells["cantidad"].Value.ToString());
                dbPrecioRecalcular = Convert.ToDouble(dgvPedido.Rows[iFila].Cells["valor_unitario"].Value.ToString());
                dbValorTotalRecalcular = dbCantidadRecalcular * dbPrecioRecalcular;
                dgvPedido.Rows[iFila].Cells["valor_total"].Value = dbValorTotalRecalcular.ToString("N2");
                recalcularValores();
                sumar.Close();
                dgvPedido.ClearSelection();
            }  
        }

        private void btnDescuentoItems_Click(object sender, EventArgs e)
        {
            invocarFormaDescuentos(1);
        }

        private void btnDatosClientes_Click(object sender, EventArgs e)
        {
            Facturador.frmControlDatosCliente controlDatosCliente = new Facturador.frmControlDatosCliente();
            controlDatosCliente.ShowDialog();

            if (controlDatosCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = controlDatosCliente.iCodigo;
                lblCliente.Text = (controlDatosCliente.sNombre + " " + controlDatosCliente.sApellido).Trim();                
                controlDatosCliente.Close();
            }
        }

        private void btnReorden_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No hay ítems ingresados.";
                    ok.ShowDialog();
                    return;
                }

                construirDataTable();

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    DataRow row = dtCortesiaDescuento.NewRow();
                    row["cantidad"] = dgvPedido.Rows[i].Cells["cantidad"].Value.ToString();
                    row["nombre_producto"] = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                    row["valor_unitario"] = dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString();
                    row["valor_total"] = dgvPedido.Rows[i].Cells["valor_total"].Value.ToString();
                    row["id_producto"] = dgvPedido.Rows[i].Cells["id_producto"].Value.ToString();
                    row["paga_iva"] = dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString();
                    row["codigo_producto"] = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                    row["secuencia_impresion"] = dgvPedido.Rows[i].Cells["secuencia_impresion"].Value.ToString();
                    row["bandera_cortesia"] = dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString();
                    row["motivo_cortesia"] = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    row["bandera_descuento"] = dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString();
                    row["motivo_descuento"] = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                    row["id_mascara"] = dgvPedido.Rows[i].Cells["id_mascara"].Value.ToString();
                    row["id_ordenamiento"] = dgvPedido.Rows[i].Cells["id_ordenamiento"].Value.ToString();
                    row["ordenamiento"] = dgvPedido.Rows[i].Cells["ordenamiento"].Value.ToString();
                    row["porcentaje_descuento"] = dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value.ToString();
                    row["bandera_comentario"] = dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString();
                    row["valor_descuento"] = dgvPedido.Rows[i].Cells["valor_descuento"].Value.ToString();

                    dtCortesiaDescuento.Rows.Add(row);
                }

                ComandaNueva.frmVistaPreviaItems vista = new frmVistaPreviaItems(dtCortesiaDescuento);
                vista.ShowDialog();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        private void btnVerReceta_Click(object sender, EventArgs e)
        {
            ok = new VentanasMensajes.frmMensajeNuevoOk();
            ok.lblMensaje.Text = "Módulo en desarrollo.";
            ok.ShowDialog();
        }

        private void frmComanda_KeyDown(object sender, KeyEventArgs e)
        {
            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }
    }
}
