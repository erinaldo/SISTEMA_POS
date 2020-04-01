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
    public partial class frmDivisionCuentas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        private int x = 50;
        private int h = 1;
        private DataGridView listaSeleccionada;
        private DataGridView listaSeleccionadaCopiar;
        private Label[] lblNumeroOrdenDividida = new Label[100];
        private Label[] lblTotalDividida = new Label[100];
        DataGridView[] dgvPedido2 = new DataGridView[100];

        //DataGridView dgvOrigen;
        //public DataGridView dgvCopia = new DataGridView();

        DataTable dtComanda;
        DataTable dtConsulta;
        DataTable dtSubReceta;
        DataTable dtReceta;
        DataTable dtAuxiliar;
        DataTable dtLocalidad;

        Double dbPorcentajePorLinea_P;

        bool bRespuesta;

        int posicion = 0;
        int contador = 0;
        int f = 0;
        int[] arregloGuardaPosicion = new int[200];
        int iMaximoGrid = 0;
        int iCoordenadaX = 160;
        int iAcumulador;
        int iIdProductoSiguiente;

        int iCoordenadaXGrid = 0;
        int iCoordenadaXCuenta = 51;
        int iCoordenadaXTotal = 185;

        int iCoordenadaYGrid = 0;
        int iCoordenadaYCuenta = 550;

        int iIdPedidoOrigen;
        int iCopiarPegar;
        int iCuenta;
        int iCuentaFilas;
        int iCuentaColumnas;
        int iBandera = 0;
        int iCabDespachos;
        int iIdPosOrigenOrden;
        int iIdOrden;
        int iCuentaDiaria;
        int iIdPedido;
        int iNumeroPedido;
        int iIdCabDespachos;
        int iIdDespachoPedido;
        int iIdEventoCobro;
        int iCgTipoDocumento = 2725;
        int icg_estado_dcto = 7460;
        int iVersionSecuencia = 1;
        int iIdProductoVerificador;
        int iCantidadNueva;
        int iNumeroOrden;
        int iIdDocumentoCobrar;
        int iIdPago;
        int iIdDocumentoPagado;
        int bandera = 0;
        int iIdOrden_P;
        int iIdProducto_P;
        int iSecuenciaImpresion;
        int iIdMascaraItem;
        int iSecuenciaEntrega;
        int iIdDetPedido;
        int iIdCajero;
        int iIdMesero;
        int iIdMesa;
        int iBanderaAcumulador;
        int iIdPosSubReceta;
        int iBanderaDescargaStock;
        int iIdMovimientoStock;
        int iCgClienteProveedor_Sub;
        int iCgTipoMovimiento_Sub;
        int iIdBodega;
        int iCgClienteProveedor;
        int iTipoMovimiento;
        int iIdPosReceta;
        int iPagaIva_P;
        int iBanderaCortesia_P;
        int iBanderaDescuento_P;
        int iBanderaComentario_P;
        int iIdOrdenamiento;
        int iSecuenciaImpresion_P;
        int iSecuenciaEntrega_P;
        int iIdRepartidor;
        int iIdPromotor;
        int iIdLocalidadBodega;
        int iIdBodegaInsumos;
        int iValorActualMovimiento;
        int iIdCabeceraMovimiento;

        int[] iRespuesta;

        long iMaximo;

        string[] copiar;
        string sCampo;
        string sTabla;
        string sSql;
        //string sfechaOrden;
        string sOrdenesGeneradas;
        //string sFechaConsulta;
        string sGuardarComentario;
        string sPorcentajeDescuento;
        string sAnio;
        string sMes;
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sNombreSubReceta;
        string sReferenciaExterna_Sub;
        string sFecha;
        string sNombreProducto_P;
        string sPagaIva_P;
        string sNombreProductoToolTip_P;
        string sMotivoCortesia_P = "";
        string sMotivoDescuento_P = "";
        string sCodigoProducto_P;
        string sNumeroMovimientoSecuencial;
        string sHistoricoOrden;

        double dbCantidad;
        double dbCantidadSiguiente;
        double valPorcentajeDescuento;
        double dDescuento_P;
        double dPrecioUnitario_P;
        double dCantidad_P;
        double dServicio;
        double dIVA_P;
        double dPorcentajeDescuento;
        double dbValorActual;

        int iIndex;
        int iBanderaControl;
        int iGrid;
        int iGridOK;

        int bandera_clear = 1;
        int bandera_mover = 1;
        int iBanderaAnteriorSiguiente = 1;
        int limite_dibujar;
        int inicio_dibujar;
        int iCuentaControlesGrid = 1;
        int iAcumulaControlesGrid = 1;
        int iBanderaControlesGrid = 1;

        public frmDivisionCuentas(DataTable dtComanda_P, int iIdPedido, string sPorcentajeDescuento, int iIdCajero, 
                                  int iIdMesero, int iIdMesa, int iIdPosOrigenOrden, int iNumeroPedido_P,
                                  int iIdPromotor_P, int iIdRepartidor_P)
        {
            this.dtComanda = dtComanda_P;
            this.iIdPedidoOrigen = iIdPedido;
            this.sPorcentajeDescuento = sPorcentajeDescuento;
            this.iIdCajero = iIdCajero;
            this.iIdMesero = iIdMesero;
            this.iIdMesa = iIdMesa;
            this.iIdPosOrigenOrden = iIdPosOrigenOrden;
            this.iNumeroPedido = iNumeroPedido_P;
            this.iIdPromotor = iIdPromotor_P;
            this.iIdRepartidor = iIdRepartidor_P;
            iMaximoGrid = this.dtComanda.Rows.Count;
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

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA AGREGAR UNA NUEVA CUENTA
        private void agregarCuenta()
        {
            try
            {
                if (iMaximoGrid != 0)
                {
                    dgvPedido2[posicion] = new DataGridView();

                    dgvPedido2[posicion].Location = new Point(iCoordenadaXGrid, iCoordenadaYGrid);
                    iCoordenadaXGrid += 330;
                    dgvPedido2[posicion].Tag = h;
                    dgvPedido2[posicion].Name = "Dividida_" + h;
                    h++;
                    dgvPedido2[posicion].MouseClick += dgv_MouseClick;
                    dgvPedido2[posicion].SelectionChanged -= dgv_SelectionChanged;
                    dgvPedido2[posicion].AllowUserToAddRows = false;
                    dgvPedido2[posicion].AllowUserToDeleteRows = false;
                    dgvPedido2[posicion].AllowUserToResizeColumns = false;
                    dgvPedido2[posicion].AllowUserToResizeRows = false;
                    dgvPedido2[posicion].MultiSelect = false;
                    dgvPedido2[posicion].Size = new Size(330, 532);

                    dgvPedido2[posicion].AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvPedido2[posicion].SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvPedido2[posicion].BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
                    dgvPedido2[posicion].CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                    dgvPedido2[posicion].ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
                    dgvPedido2[posicion].ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    dgvPedido2[posicion].ColumnHeadersVisible = false;
                    dgvPedido2[posicion].EnableHeadersVisualStyles = false;
                    dgvPedido2[posicion].ScrollBars = ScrollBars.Vertical;

                    DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();
                    cantidad.HeaderText = "CANT.";
                    cantidad.Name = "cantidad";
                    cantidad.Width = 65;
                    cantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cantidad.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    cantidad.Visible = true;

                    DataGridViewTextBoxColumn nombre_producto = new DataGridViewTextBoxColumn();
                    nombre_producto.HeaderText = "PRODUCTO";
                    nombre_producto.Name = "nombre_producto";
                    nombre_producto.Width = 193;
                    nombre_producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    nombre_producto.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    nombre_producto.Visible = true;

                    DataGridViewTextBoxColumn valor_unitario = new DataGridViewTextBoxColumn();
                    valor_unitario.HeaderText = "VALOR UNITARIO";
                    valor_unitario.Name = "valor_unitario";
                    valor_unitario.Visible = false;

                    DataGridViewTextBoxColumn valor_total = new DataGridViewTextBoxColumn();
                    valor_total.HeaderText = "VALOR TOTAL";
                    valor_total.Name = "valor_total";
                    valor_total.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    valor_total.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    valor_total.Visible = true;

                    DataGridViewTextBoxColumn id_producto = new DataGridViewTextBoxColumn();
                    id_producto.HeaderText = "ID PRODUCTO";
                    id_producto.Name = "id_producto";
                    id_producto.Visible = false;

                    DataGridViewTextBoxColumn paga_iva = new DataGridViewTextBoxColumn();
                    paga_iva.HeaderText = "PAGA IVA";
                    paga_iva.Name = "paga_iva";
                    paga_iva.Visible = false;

                    DataGridViewTextBoxColumn codigo_producto = new DataGridViewTextBoxColumn();
                    codigo_producto.HeaderText = "CODIGO PRODUCTO";
                    codigo_producto.Name = "codigo_producto";
                    codigo_producto.Visible = false;

                    DataGridViewTextBoxColumn secuencia_impresion = new DataGridViewTextBoxColumn();
                    secuencia_impresion.HeaderText = "SECUENCIA IMPRESION";
                    secuencia_impresion.Name = "secuencia_impresion";
                    secuencia_impresion.Visible = false;

                    DataGridViewTextBoxColumn bandera_cortesia = new DataGridViewTextBoxColumn();
                    bandera_cortesia.HeaderText = "BANDERA CORTESIA";
                    bandera_cortesia.Name = "bandera_cortesia";
                    bandera_cortesia.Visible = false;

                    DataGridViewTextBoxColumn motivo_cortesia = new DataGridViewTextBoxColumn();
                    motivo_cortesia.HeaderText = "MOTIVO CORTESÍA";
                    motivo_cortesia.Name = "motivo_cortesia";
                    motivo_cortesia.Visible = false;

                    DataGridViewTextBoxColumn bandera_descuento = new DataGridViewTextBoxColumn();
                    bandera_descuento.HeaderText = "BANDERA DESCUENTO";
                    bandera_descuento.Name = "bandera_descuento";
                    bandera_descuento.Visible = false;

                    DataGridViewTextBoxColumn motivo_descuento = new DataGridViewTextBoxColumn();
                    motivo_descuento.HeaderText = "MOTIVO DESCUENTO";
                    motivo_descuento.Name = "motivo_descuento";
                    motivo_descuento.Visible = false;

                    DataGridViewTextBoxColumn id_mascara = new DataGridViewTextBoxColumn();
                    id_mascara.HeaderText = "ID MÁSCARA";
                    id_mascara.Name = "id_mascara";
                    id_mascara.Visible = false;

                    DataGridViewTextBoxColumn id_ordenamiento = new DataGridViewTextBoxColumn();
                    id_ordenamiento.HeaderText = "ID ORDENAMIENTO";
                    id_ordenamiento.Name = "id_ordenamiento";
                    id_ordenamiento.Visible = false;

                    DataGridViewTextBoxColumn ordenamiento = new DataGridViewTextBoxColumn();
                    ordenamiento.HeaderText = "ORDENAMIENTO";
                    ordenamiento.Name = "ordenamiento";
                    ordenamiento.Visible = false;

                    DataGridViewTextBoxColumn porcentaje_descuento = new DataGridViewTextBoxColumn();
                    porcentaje_descuento.HeaderText = "PORCENTAJE DESCUENTO";
                    porcentaje_descuento.Name = "porcentaje_descuento";
                    porcentaje_descuento.Visible = false;

                    DataGridViewTextBoxColumn bandera_comentario = new DataGridViewTextBoxColumn();
                    bandera_comentario.HeaderText = "BANDERA COMENTARIO";
                    bandera_comentario.Name = "bandera_comentario";
                    bandera_comentario.Visible = false;

                    DataGridViewTextBoxColumn valor_descuento = new DataGridViewTextBoxColumn();
                    valor_descuento.HeaderText = "VALOR DESCUENTO";
                    valor_descuento.Name = "valor_descuento";
                    valor_descuento.Visible = false;
                   
                    dgvPedido2[posicion].Columns.Add(cantidad);
                    dgvPedido2[posicion].Columns.Add(nombre_producto);
                    dgvPedido2[posicion].Columns.Add(valor_unitario);
                    dgvPedido2[posicion].Columns.Add(valor_total);
                    dgvPedido2[posicion].Columns.Add(id_producto);
                    dgvPedido2[posicion].Columns.Add(paga_iva);
                    dgvPedido2[posicion].Columns.Add(codigo_producto);
                    dgvPedido2[posicion].Columns.Add(secuencia_impresion);
                    dgvPedido2[posicion].Columns.Add(bandera_cortesia);
                    dgvPedido2[posicion].Columns.Add(motivo_cortesia);
                    dgvPedido2[posicion].Columns.Add(bandera_descuento);
                    dgvPedido2[posicion].Columns.Add(motivo_descuento);
                    dgvPedido2[posicion].Columns.Add(id_mascara);
                    dgvPedido2[posicion].Columns.Add(id_ordenamiento);
                    dgvPedido2[posicion].Columns.Add(ordenamiento);
                    dgvPedido2[posicion].Columns.Add(porcentaje_descuento);
                    dgvPedido2[posicion].Columns.Add(bandera_comentario);
                    dgvPedido2[posicion].Columns.Add(valor_descuento);

                    dgvPedido2[posicion].Name = "dgvPedido " + h;
                    dgvPedido2[posicion].ReadOnly = true;
                    dgvPedido2[posicion].RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
                    dgvPedido2[posicion].RowHeadersVisible = false;

                    if (posicion % 2 == 0)
                    {
                        dgvPedido2[posicion].BackgroundColor = Color.FromArgb(225, 224, 192);
                    }

                    else
                    {
                        dgvPedido2[posicion].BackgroundColor = Color.FromArgb(192, 192, 255);
                    }

                    //ETIQUETA NUMERO DE CUENTA DIVIDIDA
                    lblNumeroOrdenDividida[posicion] = new Label();
                    lblNumeroOrdenDividida[posicion].Name = "Cuenta_" + h;
                    lblNumeroOrdenDividida[posicion].Text = "No. " + (posicion + 1);
                    lblNumeroOrdenDividida[posicion].Font = new Font("Maiandra GD", 15, FontStyle.Bold);
                    lblNumeroOrdenDividida[posicion].Location = new Point(iCoordenadaXCuenta, iCoordenadaYCuenta);
                    iCoordenadaXCuenta += 330;

                    //ETIQUETA TOTAL
                    lblTotalDividida[posicion] = new Label();
                    lblTotalDividida[posicion].Name = "Total_" + h;
                    lblTotalDividida[posicion].Text = "$ 0.00 ";
                    lblTotalDividida[posicion].Font = new Font("Maiandra GD", 15, FontStyle.Bold);
                    lblTotalDividida[posicion].Location = new Point(iCoordenadaXTotal, iCoordenadaYCuenta);
                    lblTotalDividida[posicion].TextAlign = ContentAlignment.MiddleRight;
                    iCoordenadaXTotal += 330;

                    pnlGrids.Width = iCoordenadaXGrid;
                    pnlGrids.Controls.Add(dgvPedido2[posicion]);
                    pnlGrids.Controls.Add(lblNumeroOrdenDividida[posicion]);
                    pnlGrids.Controls.Add(lblTotalDividida[posicion]);

                    if (iBandera == 0)
                    {
                        dgvPedido2[posicion].AccessibleName = iIdPedidoOrigen.ToString();
                        iCuentaColumnas = dtComanda.Columns.Count;

                        for (int j = 0; j < dtComanda.Rows.Count; j++)
                        {
                            dgvPedido2[posicion].Rows.Add();

                            for (int k = 0; k < dtComanda.Columns.Count; k++)
                            {
                                dgvPedido2[posicion].Rows[j].Cells[k].Value = dtComanda.Rows[j][k].ToString();
                            }
                        }

                        //FUNCION PARA SUMAR LOS VALORES DEL GRID
                        int iPagaIva_REC;

                        Decimal dbCantidad_REC;
                        Decimal dbPrecioUnitario_REC;
                        Decimal dbValorDescuento_REC;
                        Decimal dbValorIva_REC;
                        Decimal dbTotalDebido_REC;

                        Decimal dbSumaSubtotalConIva_REC = 0;
                        Decimal dbSumaSubtotalSinIva_REC = 0;
                        Decimal dbSumaDescuentoConIva_REC = 0;
                        Decimal dbSumaDescuentoSinIva_REC = 0;

                        Decimal dbSumaSubtotales_REC;
                        Decimal dbSumaDescuentos_REC;

                        Decimal dbSubtotalNeto_REC = 0;
                        Decimal dbSumaIva_REC = 0;
                        Decimal dbSumaServicio_REC = 0;

                        for (int i = 0; i < dgvPedido2[posicion].Rows.Count; i++)
                        {
                            iPagaIva_REC = Convert.ToInt32(dgvPedido2[posicion].Rows[i].Cells["paga_iva"].Value);

                            dbCantidad_REC = Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value);
                            dbPrecioUnitario_REC = Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["valor_unitario"].Value);
                            dbValorDescuento_REC = Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["valor_descuento"].Value);

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

                        lblTotalDividida[posicion].Text = "$ " + dbTotalDebido_REC.ToString("N2");
                        iMaximoGrid = dtComanda.Rows.Count;
                        iBandera = 1;
                    }

                    else
                    {
                        dgvPedido2[posicion].AccessibleName = "";
                    }

                    dgvPedido2[posicion].ClearSelection();
                    dgvPedido2[posicion].SelectionChanged += dgv_SelectionChanged;
                    contador++;

                    arregloGuardaPosicion[f] = Convert.ToInt32(dgvPedido2[posicion].Tag);
                    f++;
                    posicion++;
                    iMaximoGrid--;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay más items para dividir la cuenta";
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

        private void dgv_MouseClick(object sender, EventArgs e)
        {
            try
            {
                listaSeleccionada = sender as DataGridView;
                iGridOK = Convert.ToInt32(listaSeleccionada.Tag);

                if (iBanderaControl == 1)
                {
                    iBanderaControl = 2;
                }

                else if (iBanderaControl == 2)
                {
                    int x = 0;

                    x = listaSeleccionada.Rows.Add();

                    for (int i = 0; i < copiar.Length; i++)
                    {
                        listaSeleccionada.Rows[x].Cells[i].Value = copiar[i];
                    }

                    dgvPedido2[iGrid - 1].Rows.RemoveAt(iIndex);

                    calcularValores(iGrid - 1, iGridOK - 1);

                    dgvPedido2[iGrid - 1].ClearSelection();
                    dgvPedido2[iGridOK - 1].ClearSelection();
                    iBanderaControl = 0;
                    copiar = null;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                listaSeleccionada = sender as DataGridView;

                if (iBanderaControl == 0)
                {
                    iCuenta = listaSeleccionada.SelectedRows.Count;
                    iCuentaFilas = listaSeleccionada.Rows.Count;

                    if (iCuenta > 0)
                    {
                        if ((listaSeleccionada.AccessibleName != "") && (iCuentaFilas == 1))
                        {
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "No puede dejar sin ítems a la comanda origen.";
                            ok.ShowDialog();
                            listaSeleccionada.ClearSelection();
                        }

                        else
                        {
                            iIndex = listaSeleccionada.CurrentRow.Index;

                            copiar = new string[listaSeleccionada.Columns.Count];

                            for (int i = 0; i < listaSeleccionada.Columns.Count; i++)
                            {
                                copiar[i] = Convert.ToString(listaSeleccionada[i, listaSeleccionada.CurrentRow.Index].Value);
                            }

                            iBanderaControl = 1;
                            iGrid = Convert.ToInt32(listaSeleccionada.Tag);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULAR LOS DATAGRIDVIEW
        private void calcularValores(int iGridOrigen_P, int iGridDestino_P)
        {
            try
            {
                //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID ORIGEN
                //===================================================================================================================================================

                int iPagaIva_REC;

                Decimal dbCantidad_REC;
                Decimal dbPrecioUnitario_REC;
                Decimal dbValorDescuento_REC;
                Decimal dbValorIva_REC;
                Decimal dbTotalDebido_REC;

                Decimal dbSumaSubtotalConIva_REC = 0;
                Decimal dbSumaSubtotalSinIva_REC = 0;
                Decimal dbSumaDescuentoConIva_REC = 0;
                Decimal dbSumaDescuentoSinIva_REC = 0;

                Decimal dbSumaSubtotales_REC;
                Decimal dbSumaDescuentos_REC;

                Decimal dbSubtotalNeto_REC = 0;
                Decimal dbSumaIva_REC = 0;
                Decimal dbSumaServicio_REC = 0;

                for (int i = 0; i < dgvPedido2[iGridOrigen_P].Rows.Count; i++)
                {
                    iPagaIva_REC = Convert.ToInt32(dgvPedido2[iGridOrigen_P].Rows[i].Cells["paga_iva"].Value);

                    dbCantidad_REC = Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value);
                    dbPrecioUnitario_REC = Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valor_unitario"].Value);
                    dbValorDescuento_REC = Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valor_descuento"].Value);

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

                lblTotalDividida[iGridOrigen_P].Text = "$ " + dbTotalDebido_REC.ToString("N2");                

                //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID DESTINO
                //===================================================================================================================================================
                dbSumaSubtotalConIva_REC = 0;
                dbSumaSubtotalSinIva_REC = 0;
                dbSumaDescuentoConIva_REC = 0;
                dbSumaDescuentoSinIva_REC = 0;
                dbSubtotalNeto_REC = 0;
                dbSumaIva_REC = 0;
                dbSumaServicio_REC = 0;

                for (int i = 0; i < dgvPedido2[iGridDestino_P].Rows.Count; i++)
                {
                    iPagaIva_REC = Convert.ToInt32(dgvPedido2[iGridDestino_P].Rows[i].Cells["paga_iva"].Value);

                    dbCantidad_REC = Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value);
                    dbPrecioUnitario_REC = Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["valor_unitario"].Value);
                    dbValorDescuento_REC = Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["valor_descuento"].Value);

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

                lblTotalDividida[iGridDestino_P].Text = "$ " + dbTotalDebido_REC.ToString("N2"); 
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES PARA GUARDAR EN LA BASE DE DATOS

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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INICIAR LA TRANSACCION
        private bool iniciaTransaccion()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
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

        //FUNCION PARA ACTUALIZAR LA ORDEN ORIGINAL
        private bool actualizarOrdenOriginal()
        {
            try
            {
                iIdPedido = iIdPedidoOrigen;

                //ACTUALIZAR EN CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ELIMINAR EN CV403_DET_PEDIDOS
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //RECORRER EL DATAGRID EN CASO DE QUE EL SISTEMA ESTÉ HABILITADO PARA DESCARGAR EL INVENTARIO
                if (eliminarMovimientos(iIdPedidoOrigen) == false)
                {
                    return false;
                }
                //--------------------------------------------------------------------------------

                iAcumulador = 0;
                dtAuxiliar = new DataTable();
                dtAuxiliar.Columns.Add("producto");
                dtAuxiliar.Columns.Add("idProducto");
                dtAuxiliar.Columns.Add("cantidad");

                //ORDENAR EL DATAGRIDVIEW
                dgvPedido2[0].Sort(dgvPedido2[0].Columns["nombre_producto"], ListSortDirection.Ascending);

                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_DET_PEDIDOS
                //=======================================================================================================
                for (int i = 0; i < dgvPedido2[0].Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["id_producto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["valor_unitario"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["cantidad"].Value);
                    dDescuento_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["valor_descuento"].Value);
                    iPagaIva_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["paga_iva"].Value.ToString());
                    iBanderaCortesia_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["bandera_cortesia"].Value.ToString());
                    iBanderaDescuento_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["bandera_descuento"].Value.ToString());
                    iBanderaComentario_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["bandera_comentario"].Value.ToString());
                    iIdMascaraItem = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["id_mascara"].Value);
                    iSecuenciaEntrega_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["id_ordenamiento"].Value);
                    iSecuenciaImpresion_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["secuencia_impresion"].Value);
                    sMotivoCortesia_P = dgvPedido2[0].Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    sMotivoDescuento_P = dgvPedido2[0].Rows[i].Cells["motivo_descuento"].Value.ToString();
                    sCodigoProducto_P = dgvPedido2[0].Rows[i].Cells["codigo_producto"].Value.ToString();
                    sNombreProducto_P = dgvPedido2[0].Rows[i].Cells["nombre_producto"].Value.ToString();
                    dbPorcentajePorLinea_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["porcentaje_descuento"].Value);
                    dServicio = 0;
                    iAcumulador = i + 1;

                    //ACTUALIZACION DE CODIGO PARA RECALCULAR EL PORCENTAJE DE SERVICIO
                    if (Program.iManejaServicio == 1)
                    {
                        if (Convert.ToDouble(sPorcentajeDescuento) == 0)
                        {
                            dServicio = dPrecioUnitario_P * Program.servicio;
                        }

                        else
                        {
                            dServicio = dPrecioUnitario_P * (Convert.ToDouble(sPorcentajeDescuento) / 100);
                            dServicio = dPrecioUnitario_P - dServicio;
                            dServicio = dServicio * Program.servicio;
                        }
                    }

                    if (iBanderaComentario_P == 1)
                    {
                        sGuardarComentario = dgvPedido2[0].Rows[i].Cells["nombre_producto"].Value.ToString();
                    }

                    else
                    {
                        sGuardarComentario = "";
                    }

                    if (iPagaIva_P == 1)
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    //PROCESO PARA CONTAR
                    if (iAcumulador >= dgvPedido2[0].Rows.Count)
                    {
                        goto insertar_det_pedido;
                    }

                    iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[0].Rows[iAcumulador].Cells["id_producto"].Value);
                    iBanderaAcumulador = 0;
                    //  COMPARAR LOS ID_PRODUCTO ACTUAL Y SIGUIENTE
                    //  SI SON IGUALES INGRESA A UN FOR QUE ACUMULA LA CANTIDAD DE ITEMS PARALUEGO SER GUARDADOS
                    if (iIdProducto_P == iIdProductoSiguiente)
                    {
                        for (int j = iAcumulador; j < dgvPedido2[0].Rows.Count; j++)
                        {
                            iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[0].Rows[j].Cells["id_producto"].Value);
                            dbCantidadSiguiente = Convert.ToDouble(dgvPedido2[0].Rows[j].Cells["cantidad"].Value);

                            if (iIdProductoSiguiente == iIdProducto_P)
                            {
                                dCantidad_P += dbCantidadSiguiente;
                            }

                            else
                            {
                                iAcumulador = j - 1;
                                iBanderaAcumulador = 1;
                                break;
                            }
                        }

                        if (iBanderaAcumulador == 1)
                        {
                            i = iAcumulador;
                        }

                        else
                        {
                            i = dgvPedido2[0].Rows.Count;
                        }
                    }

                insertar_det_pedido: { }

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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    sHistoricoOrden = iNumeroPedido.ToString();

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

                    DataRow row = dtAuxiliar.NewRow();
                    row["producto"] = sNombreProducto_P;
                    row["idProducto"] = iIdProducto_P.ToString();
                    row["cantidad"] = dCantidad_P.ToString();
                    dtAuxiliar.Rows.Add(row);

                    int iBandera2 = 0;
                    int p, q, iCuenta = 0;

                    //INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    for (p = 0; p < Program.iContadorDetalle; p++)
                    {
                        if (Program.sDetallesItems[p, 0] == dgvPedido2[0].Rows[i].Cells["id_producto"].Value.ToString())
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

                        long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "No se pudo obtener el codigo del detalle pedido.";
                            ok.ShowDialog();
                            goto reversa;
                        }

                        iIdDetPedido = Convert.ToInt32(iMaximo);

                        for (q = 1; q <= iCuenta; q++)
                        {
                            sSql = "";
                            sSql += "insert into pos_det_pedido_detalle " + Environment.NewLine;
                            sSql += "(id_det_pedido, detalle, estado, fecha_ingreso," + Environment.NewLine;
                            sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                            sSql += "values(" + Environment.NewLine;
                            sSql += +iIdDetPedido + ", '" + Program.sDetallesItems[p, q] + "', " + Environment.NewLine;
                            sSql += "'A', getdate(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                            //EJECUCION DE INSTRUCCION SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                                catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
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

        reversa: { return false; }

        }

        //FUNCION PARA INSERTAR UNA NUEVA ORDEN POR CADA GRID CREADO
        private bool insertarNuevaOrden(int contador)
        {
            try
            {
                Orden o = Owner as Orden;

                int iNumeroPersonas = 0;

                if (verificarPagosExistente() == false)
                {
                    goto reversa;
                }

                extraerNumeroCuenta();

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
                sSql += "'" + sFecha + "', " + Program.iIdPersona + ", 8032," + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + ", " + Program.iIdVendedor + ",6967," + "0" + ", 7471," + Environment.NewLine; //AQUI OBTENER EL PORCENTAJE DE DESCUENTO
                sSql += iCuentaDiaria + ", " + iIdMesa + ", " + iIdCajero + "," + iIdPosOrigenOrden + ", 0, ";
                sSql += Program.iJORNADA + ", '" + sFecha + "', GETDATE(), null, 'Abierta'," + Environment.NewLine;
                sSql += iNumeroPersonas + ", 1, '', " + Program.iModoDelivery + ", ";
                sSql += iIdMesero + ", " + Program.iIdPosTerminal + ", " + (Program.servicio * 100) + ", 0, " + Environment.NewLine;
                sSql += iIdPromotor + ", " + iIdRepartidor + ", " + Program.iIdPosCierreCajero + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                
                //QUERY PARA INSERTAR EN CV403_CAB_DESPACHOS
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_cab_despachos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, cg_empresa, id_localidad, fecha_despacho, cg_motivo_despacho," + Environment.NewLine;
                sSql += "id_destinatario, punto_partida, cg_ciudad_entrega, direccion_entrega, id_transportador," + Environment.NewLine;
                sSql += "fecha_inicio_transporte, fecha_fin_transporte, cg_estado_despacho, punto_venta," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iCgMotivoDespacho + ", " + Program.iIdPersona + ", '" + Program.sPuntoPartida + "'," + Environment.NewLine;
                sSql += Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "', '" + Program.iIdPersona + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', '" + sFecha + "', " + Program.iCgEstadoDespacho + ",1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A',1,0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_PEDIDOS
                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdPedido = Convert.ToInt32(iMaximo);

                extraerNumeroOrden();

                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1," + iIdPedido + ", " + iNumeroOrden + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                sTabla = "cv403_cab_despachos";
                sCampo = "id_despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdCabDespachos = Convert.ToInt32(iMaximo);

                //PROCEDEMOS A INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //HACEMOS UN SELECT MAX A LA TABLA CV403_CAB_DESPACHOS                
                //=======================================================================================================

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS_PEDIDOS
                sTabla = "cv403_despachos_pedidos";
                sCampo = "id_despacho_pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdDespachoPedido = Convert.ToInt32(iMaximo);

                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7466, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A',1, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //OBTENEMOS EL MAX ID DE LA TABLA CV403_EVENTOS_COBROS
                //=======================================================================================================

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                sTabla = "cv403_eventos_cobros";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdEventoCobro = Convert.ToInt32(iMaximo);

                //INSERTAMOS EN LA TABLA CV403_DCTOS_POR_COBRAR
                //=======================================================================================================
                double dbValorSubtotal = 0;
                double dbValorTotal = 0;

                for (int i = 0; i < dgvPedido2[contador].Rows.Count; i++)
                {
                    dbValorSubtotal = Convert.ToDouble(dgvPedido2[contador].Rows[i].Cells["valor_total"].Value.ToString());
                    dbValorTotal = dbValorTotal + (dbValorSubtotal * (1 + (Program.iva + Program.servicio)));
                }

                string sValor = dbValorTotal.ToString("N2");

                sSql = "";
                sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda, valor," + Environment.NewLine;
                sSql += "cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + ", '" + sFecha + "'," + Environment.NewLine;
                sSql += Program.iMoneda + ", " + Convert.ToDouble(sValor) + "," + icg_estado_dcto + ", 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0)";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                string sGuardarComentario;
                int iIdMascaraItem;
                iAcumulador = 0;

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();
                dtAuxiliar.Columns.Add("producto");
                dtAuxiliar.Columns.Add("idProducto");
                dtAuxiliar.Columns.Add("cantidad");

                //ORDENAR EL DATAGRIDVIEW
                dgvPedido2[contador].Sort(dgvPedido2[contador].Columns["nombre_producto"], ListSortDirection.Ascending);

                dDescuento_P = 0;

                for (int i = 0; i < dgvPedido2[contador].Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["id_producto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido2[contador].Rows[i].Cells["valor_unitario"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido2[contador].Rows[i].Cells["cantidad"].Value);
                    dDescuento_P = Convert.ToDouble(dgvPedido2[contador].Rows[i].Cells["valor_descuento"].Value);
                    iPagaIva_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["paga_iva"].Value.ToString());
                    iBanderaCortesia_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["bandera_cortesia"].Value.ToString());
                    iBanderaDescuento_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["bandera_descuento"].Value.ToString());
                    iBanderaComentario_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["bandera_comentario"].Value.ToString());
                    iIdMascaraItem = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["id_mascara"].Value);
                    iSecuenciaEntrega_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["id_ordenamiento"].Value);
                    iSecuenciaImpresion_P = Convert.ToInt32(dgvPedido2[contador].Rows[i].Cells["secuencia_impresion"].Value);
                    sMotivoCortesia_P = dgvPedido2[contador].Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    sMotivoDescuento_P = dgvPedido2[contador].Rows[i].Cells["motivo_descuento"].Value.ToString();
                    sCodigoProducto_P = dgvPedido2[contador].Rows[i].Cells["codigo_producto"].Value.ToString();
                    sNombreProducto_P = dgvPedido2[contador].Rows[i].Cells["nombre_producto"].Value.ToString();
                    dbPorcentajePorLinea_P = Convert.ToDouble(dgvPedido2[contador].Rows[i].Cells["porcentaje_descuento"].Value);
                    dServicio = 0;
                    iAcumulador = i + 1;

                    //ACTUALIZACION DE CODIGO PARA RECALCULAR EL PORCENTAJE DE SERVICIO
                    if (Program.iManejaServicio == 1)
                    {
                        if (Convert.ToDouble(sPorcentajeDescuento) == 0)
                        {
                            dServicio = dPrecioUnitario_P * Program.servicio;
                        }

                        else
                        {
                            dServicio = dPrecioUnitario_P * (Convert.ToDouble(sPorcentajeDescuento) / 100);
                            dServicio = dPrecioUnitario_P - dServicio;
                            dServicio = dServicio * Program.servicio;
                        }
                    }

                    if (iBanderaComentario_P == 1)
                    {
                        sGuardarComentario = dgvPedido2[contador].Rows[i].Cells["nombre_producto"].Value.ToString();
                    }

                    else
                    {
                        sGuardarComentario = "";
                    }

                    if (iPagaIva_P == 1)
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }
                    
                    //PROCESO PARA CONTAR
                    if (iAcumulador >= dgvPedido2[contador].Rows.Count)
                    {
                        goto insertar_det_pedido_R;
                    }

                    iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[contador].Rows[iAcumulador].Cells["id_producto"].Value);
                    iBanderaAcumulador = 0;

                    //  COMPARAR LOS ID_PRODUCTO ACTUAL Y SIGUIENTE
                    //  SI SON IGUALES INGRESA A UN FOR QUE ACUMULA LA CANTIDAD DE ITEMS PARALUEGO SER GUARDADOS
                    if (iIdProducto_P == iIdProductoSiguiente)
                    {
                        for (int k = iAcumulador; k < dgvPedido2[contador].Rows.Count; k++)
                        {
                            iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[contador].Rows[k].Cells["id_producto"].Value);
                            dbCantidadSiguiente = Convert.ToDouble(dgvPedido2[contador].Rows[k].Cells["cantidad"].Value);

                            if (iIdProductoSiguiente == iIdProducto_P)
                            {
                                dCantidad_P += dbCantidadSiguiente;
                            }

                            else
                            {
                                iAcumulador = k - 1;
                                iBanderaAcumulador = 1;
                                break;
                            }
                        }

                        if (iBanderaAcumulador == 1)
                        {
                            i = iAcumulador;
                        }

                        else
                        {
                            i = dgvPedido2[contador].Rows.Count;
                        }
                    }

                insertar_det_pedido_R: { }

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
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    sHistoricoOrden = iNumeroOrden.ToString();

                    if (sCodigoProducto_P.Trim() == "02")
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

                    if (sCodigoProducto_P.Trim() == "03")
                    {
                        if (Program.iDescargarReceta == 1)
                        {
                            if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                            {
                                goto reversa;
                            }
                        }
                    }

                    DataRow row = dtAuxiliar.NewRow();
                    row["producto"] = sNombreProducto_P;
                    row["idProducto"] = iIdProducto_P.ToString();
                    row["cantidad"] = dCantidad_P.ToString();
                    dtAuxiliar.Rows.Add(row);

                    //PROCEDEMOS A INSERTAR EN LA TABLA CV403_CANTIDADES_DESPACHADAS
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDespachoPedido + ", " + iIdProducto_P + "," + Environment.NewLine;
                    sSql += dCantidad_P + ", 'A', 1, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                Program.iCuentaDiaria++;
                sOrdenesGeneradas = sOrdenesGeneradas + "Guardado en la orden: " + iNumeroOrden + Environment.NewLine;
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.Show();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }

        }

        private void extraerNumeroCuenta()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA VERIFICAR SI EXISTEN YA PAGOS INGRESADOS EN LA ORDEN PADRE
        private bool verificarPagosExistente()
        {
            try
            {
                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedidoOrigen + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    return false;
                }


                //VERIFICAR SI EXISTE UN DOCUMENTO PAGADO PARA DAR DE BAJA SUS DEPENDIENTES
                iCuenta = 0;

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    return false;
                }

                if (iCuenta > 0)
                {
                    /* SE PROCEDE A DAR DE BAJA LOS REGISTROS DE LAS TABLAS:
                     * CV403_PAGOS
                     * CV403_DOCUMENTOS_PAGOS
                     * CV403_NUMEROS_PAGOS
                     * CV403_DOCUMENTOS_PAGADOS
                    */

                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_PAGOS
                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_NUMEROS_PAGOS
                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        //Función para extraer el número de orden
        private void extraerNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                DataTable dtNumeroOrden = new DataTable();
                dtNumeroOrden.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtNumeroOrden, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    this.Close();
                    return;
                }

                if (dtNumeroOrden.Rows.Count > 0)
                {
                    iNumeroOrden = Convert.ToInt32(dtNumeroOrden.Rows[0].ItemArray[0].ToString());
                    iNumeroPedido = iNumeroOrden;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        #endregion

        private void frmDivisionCuentas_Load(object sender, EventArgs e)
        {
            agregarCuenta();
            pnlGrids.AutoScroll = true;
            pnlGrids.VerticalScroll.SmallChange = 100;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (iMaximoGrid != 0)
            {

                if (bandera_clear == iBanderaAnteriorSiguiente)
                {
                    if (iCuentaControlesGrid == 4)
                    {
                        pnlGrids.Controls.Clear();
                        pnlGrids.Width = 0;
                        iCoordenadaXGrid = 0;
                        iCoordenadaXCuenta = 51;
                        iCoordenadaXTotal = 185;
                        agregarCuenta();
                        iCuentaControlesGrid = 1;
                        bandera_clear++;
                        bandera_mover = bandera_clear;
                        iBanderaAnteriorSiguiente = bandera_clear;
                        btnAnterior.Enabled = true;
                    }

                    else
                    {
                        agregarCuenta();
                        iCuentaControlesGrid++;
                        iBanderaAnteriorSiguiente = bandera_clear;
                    }
                }

                else
                {
                    pnlGrids.Controls.Clear();
                    limite_dibujar = bandera_clear * 4;
                    inicio_dibujar = limite_dibujar - 4;
                    limite_dibujar = iAcumulaControlesGrid;
                    pnlGrids.Width = 0;
                    iCoordenadaXGrid = 0;
                    iCoordenadaXCuenta = 51;
                    iCoordenadaXTotal = 185;

                    if (iCuentaControlesGrid == 4)
                    {
                        agregarCuenta();
                        iCuentaControlesGrid = 1;
                        bandera_clear++;
                        bandera_mover = bandera_clear;
                        iBanderaAnteriorSiguiente = bandera_clear;
                        btnAnterior.Enabled = true;
                    }

                    else
                    {
                        for (int i = inicio_dibujar; i < limite_dibujar; i++)
                        {
                            dgvPedido2[i].Location = new Point(iCoordenadaXGrid, iCoordenadaYGrid);
                            lblNumeroOrdenDividida[i].Location = new Point(iCoordenadaXCuenta, iCoordenadaYCuenta);
                            lblTotalDividida[i].Location = new Point(iCoordenadaXTotal, iCoordenadaYCuenta);

                            pnlGrids.Controls.Add(dgvPedido2[i]);
                            pnlGrids.Controls.Add(lblNumeroOrdenDividida[i]);
                            pnlGrids.Controls.Add(lblTotalDividida[i]);

                            iCoordenadaXGrid += 330;
                            iCoordenadaXCuenta += 330;
                            iCoordenadaXTotal += 330;
                            pnlGrids.Width = iCoordenadaXGrid;
                        }

                        agregarCuenta();
                        iCuentaControlesGrid++;
                        iBanderaAnteriorSiguiente = bandera_clear;
                        btnAnterior.Enabled = true;
                    }
                }

                btnSiguiente.Enabled = false;
                iAcumulaControlesGrid++;
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay más items para dividir la cuenta";
                ok.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contador == 1)
                {
                    this.Close();
                }

                else
                {
                    //CREAR LAS ORDENES DIVIDIDAS
                    this.Cursor = Cursors.WaitCursor;

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
                        goto reversa;
                    }

                    for (int i = 0; i < posicion; i++)
                    {
                        dgvPedido2[i].SelectionChanged -= dgv_SelectionChanged;
                    }

                    iniciaTransaccion();

                    if (actualizarOrdenOriginal() == false)
                    {
                        goto reversa;
                    }

                    for (int i = 1; i < contador; i++)
                    {
                        iIdProductoVerificador = 0;
                        iCantidadNueva = 1;

                        if (dgvPedido2[i].Rows.Count > 0)
                        {
                            insertarNuevaOrden(i);
                        }
                    }

                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = sOrdenesGeneradas;
                    ok.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.Show();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                iBanderaAnteriorSiguiente--;
                bandera_mover = iBanderaAnteriorSiguiente;
                limite_dibujar = bandera_mover * 4;
                inicio_dibujar = limite_dibujar - 4;

                pnlGrids.Controls.Clear();
                pnlGrids.Width = 0;
                iCoordenadaXGrid = 0;
                iCoordenadaXCuenta = 51;
                iCoordenadaXTotal = 185;

                for (int i = inicio_dibujar; i < limite_dibujar; i++)
                {
                    dgvPedido2[i].Location = new Point(iCoordenadaXGrid, iCoordenadaYGrid);
                    lblNumeroOrdenDividida[i].Location = new Point(iCoordenadaXCuenta, iCoordenadaYCuenta);
                    lblTotalDividida[i].Location = new Point(iCoordenadaXTotal, iCoordenadaYCuenta);

                    pnlGrids.Controls.Add(dgvPedido2[i]);
                    pnlGrids.Controls.Add(lblNumeroOrdenDividida[i]);
                    pnlGrids.Controls.Add(lblTotalDividida[i]);

                    iCoordenadaXGrid += 330;
                    iCoordenadaXCuenta += 330;
                    iCoordenadaXTotal += 330;
                    pnlGrids.Width = iCoordenadaXGrid;
                }

                if (bandera_mover == 1)
                {
                    btnAnterior.Enabled = false;
                }

                btnSiguiente.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                iBanderaAnteriorSiguiente++;
                bandera_mover++;
                limite_dibujar = bandera_mover * 4;
                inicio_dibujar = limite_dibujar - 4;
                btnAnterior.Enabled = true;

                if (limite_dibujar >= posicion)
                {
                    limite_dibujar = posicion;
                    btnSiguiente.Enabled = false;
                }

                else
                {
                    btnSiguiente.Enabled = true;
                }

                pnlGrids.Controls.Clear();
                pnlGrids.Width = 0;
                iCoordenadaXGrid = 0;
                iCoordenadaXCuenta = 51;
                iCoordenadaXTotal = 185;

                for (int i = inicio_dibujar; i < limite_dibujar; i++)
                {
                    dgvPedido2[i].Location = new Point(iCoordenadaXGrid, iCoordenadaYGrid);
                    lblNumeroOrdenDividida[i].Location = new Point(iCoordenadaXCuenta, iCoordenadaYCuenta);
                    lblTotalDividida[i].Location = new Point(iCoordenadaXTotal, iCoordenadaYCuenta);

                    pnlGrids.Controls.Add(dgvPedido2[i]);
                    pnlGrids.Controls.Add(lblNumeroOrdenDividida[i]);
                    pnlGrids.Controls.Add(lblTotalDividida[i]);

                    iCoordenadaXGrid += 330;
                    iCoordenadaXCuenta += 330;
                    iCoordenadaXTotal += 330;
                    pnlGrids.Width = iCoordenadaXGrid;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
