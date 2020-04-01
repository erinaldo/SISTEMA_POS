using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmDividirCuenta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        private int x = 50;
        private int h = 1;
        private DataGridView listaSeleccionada;
        private DataGridView listaSeleccionadaCopiar;
        private Label[] lblNumeroOrdenDividida = new Label[100];
        private Label[] lblTotalDividida = new Label[100];
        DataGridView[] dgvPedido2 = new DataGridView[100];

        DataGridView dgvOrigen;
        public DataGridView dgvCopia = new DataGridView();

        DataTable dtConsulta;
        DataTable dtSubReceta;
        DataTable dtReceta;
        DataTable dtAuxiliar;

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

        int[] iRespuesta;

        long iMaximo;

        string[] copiar;
        string sIdOrden;
        string sCampo;
        string sTabla;
        string sSql;
        string sfechaOrden;
        string sOrdenesGeneradas;
        string sFechaConsulta;
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

        public frmDividirCuenta(DataGridView dgvOrigen, string sIdOrden, string sPorcentajeDescuento, int iIdCajero, int iIdMesero, int iIdMesa, int iIdPosOrigenOrden, int iNumeroPedido_P)
        {
            this.dgvOrigen = dgvOrigen;
            this.dgvCopia = dgvOrigen; 
            this.sIdOrden = sIdOrden;
            this.iIdOrden = Convert.ToInt32(sIdOrden);
            this.sPorcentajeDescuento = sPorcentajeDescuento;
            this.iIdCajero = iIdCajero;
            this.iIdMesero = iIdMesero;
            this.iIdMesa = iIdMesa;
            this.iIdPosOrigenOrden = iIdPosOrigenOrden;
            this.iNumeroPedido = iNumeroPedido_P;
            iMaximoGrid = this.dgvOrigen.Rows.Count;
            InitializeComponent();
        }

        #region FUNCIONES PARA ANIMACION DE BOTONES EN LA COMANDA

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.MediumBlue;
            btnProceso.ForeColor = Color.White;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.DeepSkyBlue;
            btnProceso.ForeColor = Color.Black;
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
                    sReferenciaExterna_P = "ITEMS - ORDEN " + iNumeroPedido.ToString();
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
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iRespuesta[2] + ", '', '', '', '', " + iRespuesta[1] + ", " + Environment.NewLine;
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
                //sSql = "";
                //sSql += "select id_producto, cantidad_bruta" + Environment.NewLine;
                //sSql += "from pos_detalle_receta" + Environment.NewLine;
                //sSql += "where id_pos_receta = " + iIdPosReceta_P + Environment.NewLine;
                //sSql += "and estado = 'A'";

                sSql = "";
                sSql += "select DR.id_producto, DR.cantidad_bruta, U.cg_unidad" + Environment.NewLine;
                sSql += "from pos_detalle_receta DR INNER JOIN" + Environment.NewLine;
                sSql += "pos_unidad U ON U.id_pos_unidad = DR.id_pos_unidad" + Environment.NewLine;
                sSql += "and DR.estado = 'A'" + Environment.NewLine;
                sSql += "and U.estado = 'A'" + Environment.NewLine;
                sSql += "where DR.id_pos_receta = " + iIdPosReceta_P;

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
                    int iIdProducto_R = Convert.ToInt32(dtReceta.Rows[i]["id_producto"].ToString());
                    double dbCantidad_R = Convert.ToDouble(dtReceta.Rows[i]["cantidad_bruta"].ToString());
                    int iCgUnidad = Convert.ToInt32(dtReceta.Rows[i]["cg_unidad"].ToString());

                    iIdPosSubReceta = 0;

                    //VARIABLE PARA COCNSULTAR SI TIENE SUBRECETA
                    int iSubReceta_R = consultarSubReceta(iIdProducto_R);

                    if (iSubReceta_R == 0)
                    {
                        sSql = "";
                        sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                        sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                        sSql += "Values (" + Environment.NewLine;
                        sSql += iIdProducto_R + ", " + iIdMovimientoBodega_P + ", " + iCgUnidad + "," + (dbCantidad_R * dbCantidadPedida_P * -1) + ", 'A')";

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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtSubReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtSubReceta.Rows[i]["id_producto"].ToString());
                    double dbCantidad_R = Convert.ToDouble(dtSubReceta.Rows[i]["cantidad_bruta"].ToString());
                    int iCgUnidad = Convert.ToInt32(dtSubReceta.Rows[i]["cg_unidad"].ToString());
                    iIdPosSubReceta = 0;

                    if (crearMovimientosBodega(iIdProducto_R, iIdMaximoCabMov, dbCantidad_R, dbCantidadPedida_P, iCgUnidad) == false)
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
        private bool crearMovimientosBodega(int iIdProducto_R, int iIdMovimientoBodega_P, double dbCantidad_R, double dbCantidadPedida_P, int iCgUnidad_P)
        {
            try
            {
                sSql = "";
                sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdProducto_R + ", " + iIdMovimientoBodega_P + ", " + iCgUnidad_P + "," + (dbCantidad_R * dbCantidadPedida_P * -1) + ", 'A')";

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

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA AGREGAR UNA NUEVA CUENTA
        private void agregarCuenta()
        {
            try
            {
                //if (iMaximoGrid != 1)
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

                    DataGridViewTextBoxColumn guardada = new DataGridViewTextBoxColumn();
                    guardada.HeaderText = "guardada";
                    guardada.Name = "guardada";
                    guardada.Visible = false;
                    DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();
                    cantidad.HeaderText = "CANT.";
                    cantidad.Name = "cantidad";
                    cantidad.Width = 65;
                    cantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cantidad.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    cantidad.Visible = true;
                    DataGridViewTextBoxColumn producto = new DataGridViewTextBoxColumn();
                    producto.HeaderText = "Producto";
                    producto.Name = "producto";
                    producto.Width = 193;
                    producto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    producto.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    producto.Visible = true;
                    DataGridViewTextBoxColumn valuni = new DataGridViewTextBoxColumn();
                    valuni.HeaderText = "valuni";
                    valuni.Name = "valuni";
                    valuni.Visible = false;
                    DataGridViewTextBoxColumn valor = new DataGridViewTextBoxColumn();
                    valor.HeaderText = "valor";
                    valor.Name = "valor";
                    valor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    valor.DefaultCellStyle.Font = new Font("Maiandra GD", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    valor.Visible = true;
                    DataGridViewTextBoxColumn cod = new DataGridViewTextBoxColumn();
                    cod.HeaderText = "cod";
                    cod.Name = "cod";
                    cod.Visible = false;
                    DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
                    ID.HeaderText = "ID";
                    ID.Name = "ID";
                    ID.Visible = false;
                    DataGridViewTextBoxColumn idProducto = new DataGridViewTextBoxColumn();
                    idProducto.HeaderText = "idProducto";
                    idProducto.Name = "idProducto";
                    idProducto.Visible = false;
                    DataGridViewTextBoxColumn cortesia = new DataGridViewTextBoxColumn();
                    cortesia.HeaderText = "cortesia";
                    cortesia.Name = "cortesia";
                    cortesia.Visible = false;
                    DataGridViewTextBoxColumn motivoCortesia = new DataGridViewTextBoxColumn();
                    motivoCortesia.HeaderText = "motivoCortesia";
                    motivoCortesia.Name = "motivoCortesia";
                    motivoCortesia.Visible = false;
                    DataGridViewTextBoxColumn cancelar = new DataGridViewTextBoxColumn();
                    cancelar.HeaderText = "cancelar";
                    cancelar.Name = "cancelar";
                    cancelar.Visible = false;
                    DataGridViewTextBoxColumn motivoCancelacion = new DataGridViewTextBoxColumn();
                    motivoCancelacion.HeaderText = "motivoCancelacion";
                    motivoCancelacion.Name = "motivoCancelacion";
                    motivoCancelacion.Visible = false;
                    DataGridViewTextBoxColumn colIdMascara = new DataGridViewTextBoxColumn();
                    colIdMascara.HeaderText = "colIdMascara";
                    colIdMascara.Name = "colIdMascara";
                    colIdMascara.Visible = false;
                    DataGridViewTextBoxColumn colSecuenciaImpresion = new DataGridViewTextBoxColumn();
                    colSecuenciaImpresion.HeaderText = "Secuencia";
                    colSecuenciaImpresion.Name = "colSecuenciaImpresion";
                    colSecuenciaImpresion.Visible = false;
                    DataGridViewTextBoxColumn colOrdenamiento = new DataGridViewTextBoxColumn();
                    colOrdenamiento.HeaderText = "Ordenamiento";
                    colOrdenamiento.Name = "colOrdenamiento";
                    colOrdenamiento.Visible = false;
                    DataGridViewTextBoxColumn colIdOrden = new DataGridViewTextBoxColumn();
                    colIdOrden.HeaderText = "IdOrden";
                    colIdOrden.Name = "colIdOrden";
                    colIdOrden.Visible = false;
                    DataGridViewTextBoxColumn colPagaIva = new DataGridViewTextBoxColumn();
                    colPagaIva.HeaderText = "PAGA IVA";
                    colPagaIva.Name = "pagaIva";
                    colPagaIva.Visible = false;


                    //lblNumeroOrdenDividida[posicion] = new Label();
                    //lblNumeroOrdenDividida[posicion].Width = 300;
                    //lblNumeroOrdenDividida[posicion].Height = 50;
                    //lblNumeroOrdenDividida[posicion].Text = "N° de Cuenta: " + (posicion + 1);
                    //lblNumeroOrdenDividida[posicion].Font = new Font("Arial", 15);
                    //lblNumeroOrdenDividida[posicion].ForeColor = System.Drawing.SystemColors.ControlLightLight;
                    //lblNumeroOrdenDividida[posicion].Location = new Point(iCoordenadaX - 50, 490);
                    //iCoordenadaX += 300;

                    dgvPedido2[posicion].Columns.Add(guardada);
                    dgvPedido2[posicion].Columns.Add(cantidad);
                    dgvPedido2[posicion].Columns.Add(producto);
                    dgvPedido2[posicion].Columns.Add(valuni);
                    dgvPedido2[posicion].Columns.Add(valor);
                    dgvPedido2[posicion].Columns.Add(cod);
                    dgvPedido2[posicion].Columns.Add(ID);
                    dgvPedido2[posicion].Columns.Add(idProducto);
                    dgvPedido2[posicion].Columns.Add(cortesia);
                    dgvPedido2[posicion].Columns.Add(motivoCortesia);
                    dgvPedido2[posicion].Columns.Add(cancelar);
                    dgvPedido2[posicion].Columns.Add(motivoCancelacion);
                    dgvPedido2[posicion].Columns.Add(colIdMascara);
                    dgvPedido2[posicion].Columns.Add(colSecuenciaImpresion);
                    dgvPedido2[posicion].Columns.Add(colOrdenamiento);
                    dgvPedido2[posicion].Columns.Add(colIdOrden);
                    dgvPedido2[posicion].Columns.Add(colPagaIva);

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
                        dgvPedido2[posicion].AccessibleName = sIdOrden;
                        iCuentaColumnas = dgvOrigen.Columns.Count;

                        for (int j = 0; j < dgvOrigen.Rows.Count; j++)
                        {
                            dgvPedido2[posicion].Rows.Add();

                            for (int k = 0; k < dgvOrigen.Columns.Count; k++)
                            {
                                dgvPedido2[posicion].Rows[j].Cells[k].Value = dgvOrigen.Rows[j].Cells[k].Value;
                            }
                        }

                        //FUNCION PARA SUMAR LOS VALORES DEL GRID

                        Decimal dSubtotalConIva = 0;
                        Decimal dSubtotalCero = 0;
                        Decimal dDescuentoConIva = 0;
                        Decimal dDescuentoCero = 0;
                        Decimal dSubtotalNeto = 0;
                        Decimal dIva = 0;
                        Decimal dServicio = 0;
                        Decimal dTotalDebido = 0;

                        //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID
                        //===================================================================================================================================================
                        for (int i = 0; i < dgvPedido2[posicion].Rows.Count; i++)
                        {
                            if ((dgvPedido2[posicion].Rows[i].Cells["cortesia"].Value.ToString() == "0") && (dgvPedido2[posicion].Rows[i].Cells["cancelar"].Value.ToString() == "0"))
                            {
                                if (Program.sCodigoAsignadoOrigenOrden == "06")
                                {
                                    dgvPedido2[posicion][0, i].Value = (Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["valuni"].Value) * (Program.dbValorPorcentaje / 100)).ToString();
                                }

                                else
                                {
                                    dgvPedido2[posicion][0, i].Value = (Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["valuni"].Value) * (Convert.ToDouble(sPorcentajeDescuento) / 100)).ToString();
                                }
                            }

                            else
                            {
                                dgvPedido2[posicion][0, i].Value = (Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[posicion].Rows[i].Cells["valuni"].Value)).ToString();
                            }

                            if (dgvPedido2[posicion].Rows[i].Cells["pagaIva"].Value.ToString() == "0")
                            {
                                dSubtotalCero += (Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["valuni"].Value.ToString()));
                                dDescuentoCero += Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["guardada"].Value.ToString());
                            }

                            else
                            {
                                dSubtotalConIva += (Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["valuni"].Value.ToString()));
                                dDescuentoConIva += Convert.ToDecimal(dgvPedido2[posicion].Rows[i].Cells["guardada"].Value.ToString());
                            }


                        }
                        //=======================================================================================================                        
                        //INSTRUCCION PARA LLENAR EL SUBTOTAL NETO
                        dSubtotalNeto = dSubtotalConIva + dSubtotalCero - dDescuentoConIva - dDescuentoCero;
                        dIva = (dSubtotalConIva - dDescuentoConIva) * Convert.ToDecimal(Program.iva);
                        dServicio = (dSubtotalNeto) * Convert.ToDecimal(Program.servicio);
                        dTotalDebido = dSubtotalNeto + dIva + dServicio;

                        lblTotalDividida[posicion].Text = "$ " + dTotalDebido.ToString("N2");
                        iMaximoGrid = dgvOrigen.Rows.Count;
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
                    ok.LblMensaje.Text = "No hay más items para dividir la cuenta";
                    ok.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgv_MouseClick(object sender, EventArgs e)
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

        private void dgv_SelectionChanged(object sender, EventArgs e)
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

        //FUNCION PARA RECALCULAR LOS DATAGRIDVIEW
        private void calcularValores(int iGridOrigen_P, int iGridDestino_P)
        {
            try
            {
                Decimal total_Origen = 0;
                Decimal total_Destino = 0;

                //FUNCION PARA SUMAR LOS VALORES DEL GRID

                Decimal dSubtotalConIva = 0;
                Decimal dSubtotalCero = 0;
                Decimal dDescuentoConIva = 0;
                Decimal dDescuentoCero = 0;
                Decimal dSubtotalNeto = 0;
                Decimal dIva = 0;
                Decimal dServicio = 0;
                //Decimal dTotalDebido = 0;

                //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID ORIGEN
                //===================================================================================================================================================
                for (int i = 0; i < dgvPedido2[iGridOrigen_P].Rows.Count; i++)
                {
                    if ((dgvPedido2[iGridOrigen_P].Rows[i].Cells["cortesia"].Value.ToString() == "0") && (dgvPedido2[iGridOrigen_P].Rows[i].Cells["cancelar"].Value.ToString() == "0"))
                    {
                        if (Program.sCodigoAsignadoOrigenOrden == "06")
                        {
                            dgvPedido2[iGridOrigen_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valuni"].Value) * (Program.dbValorPorcentaje / 100)).ToString();
                        }

                        else
                        {
                            dgvPedido2[iGridOrigen_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valuni"].Value) * (Convert.ToDouble(sPorcentajeDescuento) / 100)).ToString();
                        }
                    }

                    else
                    {
                        dgvPedido2[iGridOrigen_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valuni"].Value)).ToString();
                    }

                    if (dgvPedido2[iGridOrigen_P].Rows[i].Cells["pagaIva"].Value.ToString() == "0")
                    {
                        dSubtotalCero += (Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valuni"].Value.ToString()));
                        dDescuentoCero += Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["guardada"].Value.ToString());
                    }

                    else
                    {
                        dSubtotalConIva += (Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["valuni"].Value.ToString()));
                        dDescuentoConIva += Convert.ToDecimal(dgvPedido2[iGridOrigen_P].Rows[i].Cells["guardada"].Value.ToString());
                    }
                }
                //=======================================================================================================                        
                //INSTRUCCION PARA LLENAR EL SUBTOTAL NETO
                dSubtotalNeto = dSubtotalConIva + dSubtotalCero - dDescuentoConIva - dDescuentoCero;
                dIva = (dSubtotalConIva - dDescuentoConIva) * Convert.ToDecimal(Program.iva);
                dServicio = (dSubtotalNeto) * Convert.ToDecimal(Program.servicio);
                total_Origen = dSubtotalNeto + dIva + dServicio;

                lblTotalDividida[iGridOrigen_P].Text = "$ " + total_Origen.ToString("N2");

                //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID DESTINO
                //===================================================================================================================================================
                dSubtotalConIva = 0;
                dSubtotalCero = 0;
                dDescuentoConIva = 0;
                dDescuentoCero = 0;
                dSubtotalNeto = 0;
                dIva = 0;
                dServicio = 0;

                for (int i = 0; i < dgvPedido2[iGridDestino_P].Rows.Count; i++)
                {
                    if ((dgvPedido2[iGridDestino_P].Rows[i].Cells["cortesia"].Value.ToString() == "0") && (dgvPedido2[iGridDestino_P].Rows[i].Cells["cancelar"].Value.ToString() == "0"))
                    {
                        if (Program.sCodigoAsignadoOrigenOrden == "06")
                        {
                            dgvPedido2[iGridDestino_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["valuni"].Value) * (Program.dbValorPorcentaje / 100)).ToString();
                        }

                        else
                        {
                            dgvPedido2[iGridDestino_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["valuni"].Value) * (Convert.ToDouble(sPorcentajeDescuento) / 100)).ToString();
                        }
                    }

                    else
                    {
                        dgvPedido2[iGridDestino_P][0, i].Value = (Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido2[iGridDestino_P].Rows[i].Cells["valuni"].Value)).ToString();
                    }

                    if (dgvPedido2[iGridDestino_P].Rows[i].Cells["pagaIva"].Value.ToString() == "0")
                    {
                        dSubtotalCero += (Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["valuni"].Value.ToString()));
                        dDescuentoCero += Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["guardada"].Value.ToString());
                    }

                    else
                    {
                        dSubtotalConIva += (Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["valuni"].Value.ToString()));
                        dDescuentoConIva += Convert.ToDecimal(dgvPedido2[iGridDestino_P].Rows[i].Cells["guardada"].Value.ToString());
                    }
                }

                //=======================================================================================================                        
                //INSTRUCCION PARA LLENAR EL SUBTOTAL NETO
                dSubtotalNeto = dSubtotalConIva + dSubtotalCero - dDescuentoConIva - dDescuentoCero;
                dIva = (dSubtotalConIva - dDescuentoConIva) * Convert.ToDecimal(Program.iva);
                dServicio = (dSubtotalNeto) * Convert.ToDecimal(Program.servicio);
                total_Destino = dSubtotalNeto + dIva + dServicio;

                lblTotalDividida[iGridDestino_P].Text = "$ " + total_Destino.ToString("N2");
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //private void datagridviewClic(object sender, EventArgs e)
        //{
        //    //  OPCIONES DE LA VARIABLE iCopiarPegar
        //    //  0 --> COPIA LA CELDA
        //    //  1 --> PEGA LA CELDA Y ELIMINA DEL ORIGEN

        //    listaSeleccionada = sender as DataGridView;

        //    iCuenta = listaSeleccionada.SelectedRows.Count;
        //    iCuentaFilas = listaSeleccionada.Rows.Count;

        //    if (iCopiarPegar == 0)
        //    {
        //        if (iCuenta > 0)
        //        {
        //            if ((listaSeleccionada.AccessibleName != "") && (iCuentaFilas == 1))
        //            {
        //                MessageBox.Show("No puede dejar sin ítems a la comanda origen.");
        //                listaSeleccionada.ClearSelection();
        //            }

        //            else
        //            {

        //                listaSeleccionadaCopiar = sender as DataGridView;

        //                copiar = new string[listaSeleccionada.Columns.Count];

        //                for (int i = 0; i < listaSeleccionada.Columns.Count; i++)
        //                {
        //                    copiar[i] = Convert.ToString(listaSeleccionada[i, listaSeleccionada.CurrentRow.Index].Value);
        //                }

        //                iCopiarPegar = 1;
        //            }
        //        }
        //    }


        //    else if (iCopiarPegar == 1)
        //    {
        //        int x = 0;

        //        x = listaSeleccionada.Rows.Add();

        //        for (int i = 0; i < copiar.Length; i++)
        //        {
        //            listaSeleccionada.Rows[x].Cells[i].Value = copiar[i];
        //        }

        //        listaSeleccionadaCopiar.Rows.Remove(listaSeleccionadaCopiar.CurrentRow);
        //        listaSeleccionadaCopiar.ClearSelection();
        //        listaSeleccionada.ClearSelection();
        //        iCopiarPegar = 0;
        //    }
        //}

        //INGRESAR EL CURSOR AL BOTON
        //private void ingresaBoton(Button btnProceso)
        //{
        //    btnProceso.BackgroundImage = Properties.Resources.boton_cambio;
        //    btnProceso.BackgroundImageLayout = ImageLayout.Stretch;
        //    btnProceso.FlatAppearance.MouseOverBackColor = Color.Transparent;
        //    btnProceso.FlatStyle = FlatStyle.Flat;
        //    btnProceso.ForeColor = Color.Black;
        //}

        ////SALIR EL CURSOR DEL BOTON
        //private void salidaBoton(Button btnProceso)
        //{
        //    btnProceso.BackgroundImage = Properties.Resources.boton;
        //    btnProceso.BackgroundImageLayout = ImageLayout.Stretch;
        //    btnProceso.ForeColor = Color.White;
        //}

        #endregion

        #region FUNCIONES PARA GUARDAR EN LA BASE DE DATOS

        //FUNCION PARA INICIAR LA TRANSACCION
        private void iniciaTransaccion()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR LA ORDEN ORIGINAL
        private bool actualizarOrdenOriginal()
        {
            try
            {
                iIdPedido = Convert.ToInt32(sIdOrden);

                //ACTUALIZAR EN CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //FUNCIONES PARA LA BODEGA
                //--------------------------------------------------------------------------------
                if (eliminarMovimientos(Convert.ToInt32(sIdOrden)) == false)
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
                dgvPedido2[0].Sort(dgvPedido2[0].Columns["producto"], ListSortDirection.Ascending);

                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_DET_PEDIDOS
                //=======================================================================================================
                //=======================================================================================================
                for (int i = 0; i < dgvPedido2[0].Rows.Count; i++)
                {
                    
                    iIdProducto_P = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido2[0].Rows[i].Cells["cantidad"].Value);
                    iSecuenciaImpresion = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["colSecuenciaImpresion"].Value);
                    sPagaIva_P = dgvPedido2[0].Rows[i].Cells["pagaIva"].Value.ToString();
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
                    
                    if (dgvPedido2[0].Rows[i].Cells["colIdOrden"].Value.ToString() == "")
                    {
                        iSecuenciaEntrega = 0;
                    }

                    else
                    {
                        iSecuenciaEntrega = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["colIdOrden"].Value);
                    }

                    iIdMascaraItem = 0;

                    //if (valPorcentajeDescuento == 0)
                    if (Convert.ToDouble(sPorcentajeDescuento) == 0)
                    {
                        if (dgvPedido2[0].Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido2[0].Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            dDescuento_P = 0;
                        }
                    }

                    else
                    {
                        if (dgvPedido2[0].Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido2[0].Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            //dDescuento_P = dPrecioUnitario_P * dPorcentajeDescuento;
                            dDescuento_P = dPrecioUnitario_P * (Convert.ToDouble(sPorcentajeDescuento) / 100);
                        }
                    }

                    if (sPagaIva_P == "1")
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    //CONTROL DE CONSUMO ALIMENTOS,CORTESIAS Y CANCELACION ITEM
                    if ((dgvPedido2[0].Rows[i].Cells["colIdMascara"].Value.ToString() != "0") && (dgvPedido2[0].Rows[i].Cells["colIdMascara"].Value.ToString() != ""))
                    {
                        sGuardarComentario = dgvPedido2[0].Rows[i].Cells["producto"].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                        iIdMascaraItem = Convert.ToInt32(dgvPedido2[0].Rows[i].Cells["colIdMascara"].Value);
                    }
                    else if (dgvPedido2[0].Rows[i].Cells["cortesia"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido2[0].Rows[i].Cells["producto"].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }

                    else if (dgvPedido2[0].Rows[i].Cells["cancelar"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido2[0].Rows[i].Cells["producto"].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }
                    else if (dgvPedido2[0].Rows[i].Cells["idProducto"].Value.ToString() == Program.iIdProduto.ToString())
                    {
                        sGuardarComentario = dgvPedido2[0].Rows[i].Cells["producto"].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }
                    else
                    {
                        sNombreProducto_P = dgvPedido2[0].Rows[i].Cells["producto"].Value.ToString();
                        sGuardarComentario = null;
                    }

                    //PROCESO PARA CONTAR
                    if (iAcumulador >= dgvPedido2[0].Rows.Count)
                    {
                        goto insertar_det_pedido;
                    }

                    iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[0].Rows[iAcumulador].Cells["idProducto"].Value);
                    iBanderaAcumulador = 0;
                    //  COMPARAR LOS ID_PRODUCTO ACTUAL Y SIGUIENTE
                    //  SI SON IGUALES INGRESA A UN FOR QUE ACUMULA LA CANTIDAD DE ITEMS PARALUEGO SER GUARDADOS
                    if (iIdProducto_P == iIdProductoSiguiente)
                    {
                        for (int j = iAcumulador; j < dgvPedido2[0].Rows.Count; j++)
                        {
                            iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[0].Rows[j].Cells["idProducto"].Value);
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
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia, " + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado,numero_replica_trigger,numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedido + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdMascaraItem + "," + Environment.NewLine;
                    sSql += iSecuenciaImpresion + ", " + Environment.NewLine;

                    if (iSecuenciaEntrega == 0)
                    {
                        sSql += "null, ";
                    }

                    else
                    {
                        sSql += iSecuenciaEntrega + ", ";
                    }

                    sSql += "'A', 0, 0)";

                    //FUNCION PARA EJECUTAR LA INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
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
                        if (Program.sDetallesItems[p, 0] == dgvPedido2[0].Rows[i].Cells["idProducto"].Value.ToString())
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
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_det_pedidos";
                        sCampo = "id_det_pedido";

                        long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo del detalle pedido.";
                            ok.ShowInTaskbar = false;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdDetPedido = Convert.ToInt32(iMaximo);
                        }

                        for (q = 1; q <= iCuenta; q++)
                        {
                            sSql = "";
                            sSql += "insert into pos_det_pedido_detalle " + Environment.NewLine;
                            sSql += "(id_det_pedido, detalle, estado, fecha_ingreso," + Environment.NewLine;
                            sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                            sSql += "values(" + Environment.NewLine;
                            sSql += +iIdDetPedido + ", '" + Program.sDetallesItems[p, q] + "', " + Environment.NewLine;
                            sSql += "'A', getdate(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                            //EJECUTA SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
                        }
                    }
                }

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

                    for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
                    {
                        string sNombreProducto_R = dtAuxiliar.Rows[i]["producto"].ToString();
                        iIdProducto_P = Convert.ToInt32(dtAuxiliar.Rows[i]["idProducto"].ToString());
                        dCantidad_P = Convert.ToDouble(dtAuxiliar.Rows[i]["cantidad"].ToString());
                        iIdPosReceta = obteneridReceta(iIdProducto_P);

                        if (iIdPosReceta == -1)
                        {
                            return false;
                        }

                        else
                        {
                            if (crearEgreso(sNombreProducto_R + " - ORDEN " + iNumeroPedido.ToString(), iCgClienteProveedor,
                                        iTipoMovimiento, iIdPosReceta, iIdProducto_P, dCantidad_P) == false)
                            {
                                return false;
                            }

                            iIdMovimientoStock = 0;
                            iBanderaDescargaStock = 0;
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

        reversa: { return false; }

        }
        
        //FUNCION PARA INSERTAR UNA NUEVA ORDEN POR CADA GRID CREADO
        private void insertarNuevaOrden(int contador)
        {
            try
            {
                Orden o = Owner as Orden;
                //string sfecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sfechaOrden = Program.sFechaSistema.ToString("yyyy/MM/dd");

                int iNUmeroPersonas = 0;
                
                if (verificarPagosExistente() == false)
                {
                    goto reversa;
                }

                extraerNumeroCuenta();

                sSql = "";
                sSql += "insert into cv403_cab_pedidos(" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, fecha_pedido, id_persona, cg_tipo_cliente," + Environment.NewLine;
                sSql += "cg_moneda, porcentaje_iva, id_vendedor, cg_estado_pedido, porcentaje_dscto," + Environment.NewLine;
                sSql += "cg_facturado, fecha_ingreso, usuario_ingreso, terminal_ingreso,cuenta,id_pos_mesa,id_pos_cajero," + Environment.NewLine;
                sSql += "id_pos_origen_orden, id_pos_orden_dividida, id_pos_jornada, fecha_orden, fecha_apertura_orden," + Environment.NewLine;
                sSql += "fecha_cierre_orden, estado_orden,numero_personas,origen_dato, numero_replica_trigger," + Environment.NewLine;
                sSql += "estado_replica, numero_control_replica, estado, idtipoestablecimiento, comentarios," + Environment.NewLine;
                sSql += "id_pos_modo_delivery, id_pos_mesero, id_pos_terminal, porcentaje_servicio, id_pos_cierre_cajero)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", '" + sfechaOrden + "'," + Environment.NewLine;
                sSql += Program.iIdPersona + ", 8032," + Program.iMoneda + ", " + (Program.iva * 100) + ", " + Program.iIdVendedor + "," + Environment.NewLine;
                sSql += "6967, 0, 7471, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', " + iCuentaDiaria + "," + Environment.NewLine;

                if (iIdMesa == 0)
                {
                    sSql += "null, ";
                }

                else
                {
                    sSql += iIdMesa + ", ";
                }

                sSql += iIdCajero + ", " + iIdPosOrigenOrden + ", " + iIdOrden + ", " + Program.iJORNADA + "," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', GETDATE(), null, 'Abierta', " + iNUmeroPersonas + ", 1, 1, 0, 0, 'A', 1, '" + Program.sNOMBREMESA + "'," + Environment.NewLine;

                if (Program.iModoDelivery == 0)
                {
                    sSql += "null, ";
                }

                else
                {
                    sSql += Program.iModoDelivery + ", ";
                }

                sSql += iIdMesero + ", " + Program.iIdPosTerminal + ", " + (Program.servicio * 100) + ", " + Program.iIdPosCierreCajero + ")";


                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
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
                sSql += "'" + sfechaOrden + "', " + Program.iCgMotivoDespacho + ", " + Program.iIdPersona + ", '" + Program.sPuntoPartida + "'," + Environment.NewLine;
                sSql += Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "', '" + Program.iIdPersona + "'," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', '" + sfechaOrden + "', " + Program.iCgEstadoDespacho + ",1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A',1,0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPedido = Convert.ToInt32(iMaximo);
                }

                extraerNumeroOrden();

                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1," + iIdPedido + ", " + (iNumeroOrden) + ", getdate(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_despachos";
                sCampo = "id_despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCabDespachos = Convert.ToInt32(iMaximo);
                }

                //PROCEDEMOS A INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                //=======================================================================================================
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //HACEMOS UN SELECT MAX A LA TABLA CV403_CAB_DESPACHOS                
                //=======================================================================================================
                //=======================================================================================================

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_despachos_pedidos";
                sCampo = "id_despacho_pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdDespachoPedido = Convert.ToInt32(iMaximo);
                }
                
                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7466, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A',1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //OBTENEMOS EL MAX ID DE LA TABLA CV403_EVENTOS_COBROS
                //=======================================================================================================
                //=======================================================================================================

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_eventos_cobros";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdEventoCobro = Convert.ToInt32(iMaximo);
                }

                //INSERTAMOS EN LA TABLA CV403_DCTOS_POR_COBRAR
                //=======================================================================================================
                //=======================================================================================================
                double dbValorSubtotal = 0;
                double dbValorTotal = 0;

                for (int i = 0; i < dgvPedido2[contador].Rows.Count; i++)
                {
                    dbValorSubtotal = Convert.ToDouble(dgvPedido2[contador][4, i].Value.ToString());
                    dbValorTotal = dbValorTotal + (dbValorSubtotal * (1 + (Program.iva + Program.servicio)));
                }

                string sValor = dbValorTotal.ToString("N2");

                sSql = "";
                sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda, valor," + Environment.NewLine;
                sSql += "cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + ", '" + sfechaOrden + "'," + Environment.NewLine;
                sSql += Program.iMoneda + ", " + Convert.ToDouble(sValor) + "," + icg_estado_dcto + ", 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
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
                dgvPedido2[contador].Sort(dgvPedido2[contador].Columns["producto"], ListSortDirection.Ascending);

                dDescuento_P = 0;

                for (int j = 0; j < dgvPedido2[contador].Rows.Count; j++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido2[contador].Rows[j].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido2[contador].Rows[j].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido2[contador].Rows[j].Cells["cantidad"].Value);
                    iSecuenciaImpresion = Convert.ToInt32(dgvPedido2[contador].Rows[j].Cells["colSecuenciaImpresion"].Value);
                    sPagaIva_P = dgvPedido2[contador].Rows[j].Cells["pagaIva"].Value.ToString();
                    dServicio = 0;
                    iAcumulador = j + 1;

                    if (sPagaIva_P == "1")
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    //CONTROL DE CONSUMO ALIMENTOS,CORTESIAS Y CANCELACION ITEM
                    iIdMascaraItem = 0;

                    if ((dgvPedido2[contador][12, j].Value.ToString() != "0") && (dgvPedido2[contador][12, j].Value.ToString() != ""))
                    {
                        sGuardarComentario = dgvPedido2[contador][2, j].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                        iIdMascaraItem = Convert.ToInt32(dgvPedido2[contador][12, j].Value.ToString());
                    }

                    else if ((dgvPedido2[contador][8, j].Value.ToString() == "1"))
                    {
                        sGuardarComentario = dgvPedido2[contador][2, j].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }

                    else if ((dgvPedido2[contador][10, j].Value.ToString() == "1"))
                    {
                        sGuardarComentario = dgvPedido2[contador][2, j].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }

                    else if (dgvPedido2[contador][7, j].Value.ToString() == Program.iIdProduto.ToString())
                    {
                        sGuardarComentario = dgvPedido2[contador][2, j].Value.ToString();
                        sNombreProducto_P = sGuardarComentario;
                    }
                    else
                    {
                        sNombreProducto_P = dgvPedido2[contador].Rows[j].Cells["producto"].Value.ToString();
                        sGuardarComentario = null;
                    }

                    //PROCESO PARA CONTAR
                    if (iAcumulador >= dgvPedido2[contador].Rows.Count)
                    {
                        goto insertar_det_pedido_R;
                    }

                    iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[contador].Rows[iAcumulador].Cells["idProducto"].Value);
                    iBanderaAcumulador = 0;

                    //  COMPARAR LOS ID_PRODUCTO ACTUAL Y SIGUIENTE
                    //  SI SON IGUALES INGRESA A UN FOR QUE ACUMULA LA CANTIDAD DE ITEMS PARALUEGO SER GUARDADOS
                    if (iIdProducto_P == iIdProductoSiguiente)
                    {
                        for (int k = iAcumulador; k < dgvPedido2[contador].Rows.Count; k++)
                        {
                            iIdProductoSiguiente = Convert.ToInt32(dgvPedido2[contador].Rows[k].Cells["idProducto"].Value);
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
                            j = iAcumulador;
                        }

                        else
                        {
                            j = dgvPedido2[contador].Rows.Count;
                        }
                    }

                    insertar_det_pedido_R: { }

                    //INSTRUCCION SQL PARA GUARDAR EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva, Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia, " + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedido + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdMascaraItem + "," + Environment.NewLine;
                    sSql += iSecuenciaImpresion + ", " + Environment.NewLine;

                    if (iSecuenciaEntrega == 0)
                    {
                        sSql += "null, ";
                    }

                    else
                    {
                        sSql += iSecuenciaEntrega + ", ";
                    }

                    sSql += "'A', 0, 0)";
                    

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    DataRow row = dtAuxiliar.NewRow();
                    row["producto"] = sNombreProducto_P;
                    row["idProducto"] = iIdProducto_P.ToString();
                    row["cantidad"] = dCantidad_P.ToString();
                    dtAuxiliar.Rows.Add(row);

                    //PROCEDEMOS A INSERTAR EN LA TABLA CV403_CANTIDADES_DESPACHADAS
                    //=======================================================================================================
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDespachoPedido + ", " + iIdProducto_P + "," + Environment.NewLine;
                    sSql += dCantidad_P + ", 'A', 1, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

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

                    for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
                    {
                        string sNombreProducto_R = dtAuxiliar.Rows[i]["producto"].ToString();
                        iIdProducto_P = Convert.ToInt32(dtAuxiliar.Rows[i]["idProducto"].ToString());
                        dCantidad_P = Convert.ToDouble(dtAuxiliar.Rows[i]["cantidad"].ToString());
                        iIdPosReceta = obteneridReceta(iIdProducto_P);

                        if (iIdPosReceta == -1)
                        {
                            goto reversa;
                        }

                        else
                        {
                            if (crearEgreso(sNombreProducto_R + " - ORDEN " + iNumeroPedido.ToString(), iCgClienteProveedor,
                                        iTipoMovimiento, iIdPosReceta, iIdProducto_P, dCantidad_P) == false)
                            {
                                goto reversa;
                            }

                            iIdMovimientoStock = 0;
                            iBanderaDescargaStock = 0;
                        }
                    }
                //}

                continuar_proceso: { }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                Program.iCuentaDiaria++;
                sOrdenesGeneradas = sOrdenesGeneradas + "Guardado en la orden: " + iNumeroOrden + Environment.NewLine;
                goto fin;


            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.Show();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

            fin: { }
        }

        private void extraerNumeroCuenta()
        {
            try
            {
                sFechaConsulta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + sFechaConsulta + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) + 1;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
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
                sSql += "where id_pedido = " + iIdOrden + Environment.NewLine;
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
                        catchMensaje.LblMensaje.Text = sSql;
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
                        catchMensaje.LblMensaje.Text = sSql;
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
                        catchMensaje.LblMensaje.Text = sSql;
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
                        catchMensaje.LblMensaje.Text = sSql;
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
        
        //Función para extraer el número de orden
        private void extraerNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select numero_pedido from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                DataTable dtNumeroOrden = new DataTable();
                dtNumeroOrden.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtNumeroOrden, sSql);

                if (bRespuesta == true)
                {
                    if (dtNumeroOrden.Rows.Count > 0)
                    {
                        iNumeroOrden = Convert.ToInt32(dtNumeroOrden.Rows[0].ItemArray[0].ToString());
                        iNumeroPedido = iNumeroOrden;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                        ok.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        #endregion

        private void frmDividirCuenta_Load(object sender, EventArgs e)
        {
            //iMaximoGrid = dgvOrigen.Rows.Count;
            agregarCuenta();
            //extraerIdPosOrigenOrden();            

            // Configurar opciones del panel
            pnlGrids.AutoScroll = true;
            pnlGrids.VerticalScroll.SmallChange = 100;

            //// Vincular los eventos Click en un sólo controlador de eventos
            //btnIzquierda.Click += Desplazar_Contenido;
            //btnDerecha.Click += Desplazar_Contenido;
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
                //if (contador == 0)
                if (contador == 1)
                {
                    this.Close();
                }

                else
                {
                    //CREAR LAS ORDENES DIVIDIDAS

                    this.Cursor = Cursors.WaitCursor;

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
                    ok.LblMensaje.Text = sOrdenesGeneradas;
                    ok.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    goto fin;
                }                
            }

            catch(Exception ex)
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

            fin: { }
        }


        private void btnAnterior_Click(object sender, EventArgs e)
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

        private void btnSiguiente_Click(object sender, EventArgs e)
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

        private void btnAgregar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAgregar);
        }

        private void btnAceptar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAceptar);
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnAnterior_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAnterior);
        }

        private void btnSiguiente_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSiguiente);
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAgregar);
        }

        private void btnAceptar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAceptar);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalir);
        }

        private void btnAnterior_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAnterior);
        }

        private void btnSiguiente_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSiguiente);
        }
    }
}
