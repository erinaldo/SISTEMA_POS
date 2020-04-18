using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

        ToolTip ttMensajeMesas = new ToolTip();

        SqlParameter[] parametro;

        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtConsulta;
        DataTable dtItems;
        DataTable dtDetalleItems;
        DataTable dtCortesiaDescuento;

        bool bRespuesta;

        Button[,] boton = new Button[2, 4];
        Button[,] botonModificador = new Button[2, 5];
        Button[,] botonProductos = new Button[5, 5];
        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;

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
        int iIdOrigenOrden;
        int iNumeroPersonas;
        int iIdPromotor;
        int iIdRepartidor;
        int iCategoriaDelivery;
        int iIdPersona;
        int iIdCajero;
        int iIdMesero;
        int iVersionImpresionComanda;
        int iIdPedido;
        int iConsumoAlimentos;
        int iIdMesa;
        int iPagaIva_P;
        int iBanderaAbrirPagos;
        int iIdGeneraFactura;
        int iBanderaCategorias;
        int iBanderaSubCategorias;
        int iBanderaModificadores;
        int iIdProductoPadreModificador;
        int iNivelGeneral;
        int iIdListaMinorista;

        string sDescripcionOrigen;
        string sNombreMesero;
        string sHistoricoOrden;
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
        
        double dbCantidadRecalcular;
        double dbPrecioRecalcular;
        double dbValorTotalRecalcular;

        Double dPorcentajeDescuento;

        Decimal dbCantidadProductoFactor = 1;
        Decimal dbTotalDebido_REC;

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

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER LOS DATOS DE LA LISTA BASE Y MINORISTA
        private void datosListas()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_minorista = @lista_minorista" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@lista_minorista";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 1;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                    iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"]);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

                if (reabrir == "COPIAR")
                {
                    if (cargarDetalleGridCopiar() == false)
                        return false;
                }

                else
                { 
                    if (cargarDetalleGrid() == false)
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                                       dtConsulta.Rows[i]["valor_dscto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["paga_servicio"].ToString().Trim()
                                       );

                    //LLENAR LA MATRIZ DE DETALLE ITEMS CON LOS DATOS INGRESADOS EN LOS DETALLES EN CASO DE QUE SI HAYA
                    sSql = "";
                    sSql += "select PD.detalle, P.id_producto" + Environment.NewLine;
                    sSql += "from pos_det_pedido_detalle PD, cv403_det_pedidos DP, cv401_productos P" + Environment.NewLine;
                    sSql += "where PD.id_det_pedido = DP.id_det_pedido " + Environment.NewLine;
                    sSql += "and DP.id_producto = P.id_producto " + Environment.NewLine;
                    sSql += "and PD.id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i]["id_det_pedido"].ToString()) + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and DP.estado = 'A'" + Environment.NewLine;
                    sSql += "and PD.estado = 'A'";

                    dtItems = new DataTable();
                    dtItems.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtItems, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //FUNCION PARA CARGAR EL DETALLE DE LA ORDEN EN EL DATAGRID
        private bool cargarDetalleGridCopiar()
        {
            try
            {
                sSql = "";
                sSql += "select PP.valor valor_nuevo, * " + Environment.NewLine;
                sSql += "from pos_vw_detalle_comanda DC INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON DC.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = @estado" + Environment.NewLine;
                sSql += "where DC.id_pedido = @id_pedido" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = @id_lista_precio" + Environment.NewLine;
                sSql += "order by id_det_pedido";

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pedido";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdPedido;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_lista_precio";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iIdListaMinorista;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvPedido.Rows.Add(Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString()).ToString(),
                                       dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                       dtConsulta.Rows[i]["valor_nuevo"].ToString().Trim(),
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
                                       dtConsulta.Rows[i]["valor_dscto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["paga_servicio"].ToString().Trim()
                                       );

                    //LLENAR LA MATRIZ DE DETALLE ITEMS CON LOS DATOS INGRESADOS EN LOS DETALLES EN CASO DE QUE SI HAYA
                    sSql = "";
                    sSql += "select PD.detalle, P.id_producto" + Environment.NewLine;
                    sSql += "from pos_det_pedido_detalle PD, cv403_det_pedidos DP, cv401_productos P" + Environment.NewLine;
                    sSql += "where PD.id_det_pedido = DP.id_det_pedido " + Environment.NewLine;
                    sSql += "and DP.id_producto = P.id_producto " + Environment.NewLine;
                    sSql += "and PD.id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i]["id_det_pedido"].ToString()) + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and DP.estado = 'A'" + Environment.NewLine;
                    sSql += "and PD.estado = 'A'";

                    dtItems = new DataTable();
                    dtItems.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtItems, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    row["paga_servicio"] = dgvPedido.Rows[i].Cells["paga_servicio"].Value.ToString();

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
                                           dtConsulta.Rows[i]["valor_descuento"].ToString(),
                                           dtConsulta.Rows[i]["paga_servicio"].ToString()

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
                dtCortesiaDescuento.Columns.Add("paga_servicio");
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

        //FUNCION PARA CARGAR LOS BOTONES DE CATEGORIA
        private void cargarCategorias(int iNivel_P, int iIdProductoPadre_P)
        {
            try
            {
                iBanderaCategorias = 1;
                iBanderaSubCategorias = 0;
                iBanderaModificadores = 0;

                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva," + Environment.NewLine;
                sSql += "P.subcategoria, isnull(P.categoria_delivery, 0) categoria_delivery," + Environment.NewLine;
                sSql += "isnull(P.imagen_categoria, '') imagen_categoria" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = " + iNivel_P + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "and P.modificador = 0" + Environment.NewLine;

                if (iNivel_P == 3)
                    sSql += "and P.id_producto_padre = " + iIdProductoPadre_P + Environment.NewLine;

                sSql += "order by P.secuencia";

                dtCategorias = new DataTable();
                dtCategorias.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCategorias, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    if (iNivel_P == 3)
                    {
                        iNivelGeneral = 4;
                        btnRegresar.Visible = true;
                    }

                    if (dtCategorias.Rows.Count > 8)
                    {
                        
                        btnSiguiente.Enabled = true;
                        btnAnterior.Visible = true;
                        btnSiguiente.Visible = true;
                    }

                    else
                    {
                        //btnRegresar.Visible = false;
                        btnSiguiente.Enabled = false;
                        btnAnterior.Visible = false;
                        btnSiguiente.Visible = false;
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
                        boton[i, j].Font = new Font("Maiandra GD", 8.25F, FontStyle.Bold);
                        boton[i, j].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                        boton[i, j].Text = dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString();
                        boton[i, j].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                        boton[i, j].FlatStyle = FlatStyle.Flat;
                        boton[i, j].FlatAppearance.BorderSize = 1;
                        boton[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128);
                        boton[i, j].FlatAppearance.MouseDownBackColor = Color.Fuchsia;

                        if (Convert.ToInt32(dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString()) == 1)
                        {
                            ttMensajeMesas.SetToolTip(boton[i, j], dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString().Trim().ToUpper() + " CONTIENE SUBCATEGORÍAS");
                            boton[i, j].BackColor = Color.LightSalmon;
                        }

                        else
                        {
                            ttMensajeMesas.SetToolTip(boton[i, j], "CATEGORÍA: " + dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString());
                            boton[i, j].BackColor = Color.White;
                        }

                        if (Program.iUsarIconosCategorias == 1)
                        {
                            if (dtCategorias.Rows[iCuentaCategorias]["imagen_categoria"].ToString().Trim() != "")
                            {
                                Image foto;
                                byte[] imageBytes;

                                imageBytes = Convert.FromBase64String(dtCategorias.Rows[iCuentaCategorias]["imagen_categoria"].ToString().Trim());

                                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                                {
                                    foto = Image.FromStream(ms, true);
                                }

                                boton[i, j].TextAlign = ContentAlignment.BottomCenter;
                                boton[i, j].Image = foto;
                                boton[i, j].ImageAlign = ContentAlignment.TopCenter;
                                boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                                                
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
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), botonSeleccionadoCategoria.Text.Trim().ToUpper());
                }
                else
                {
                    //cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 4, botonSeleccionadoCategoria.Text.Trim().ToUpper());
                    cargarCategorias(3, Convert.ToInt32(botonSeleccionadoCategoria.Tag));
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
        private void cargarProductos(int iIdProducto_P, string sNombreCategoria_P)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, PP.valor, CP.codigo, P.paga_servicio" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_clase_producto CP ON CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = " + iNivelGeneral + Environment.NewLine;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                        botonProductos[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128);
                        botonProductos[i, j].FlatAppearance.MouseDownBackColor = Color.Fuchsia;
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
                int iPagaServicio;
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

                //BUSCAR SI PAGA SERVICIO
                //-------------------------------------------------------------------------------------------------------------
                iIdProductoGrid = Convert.ToInt32(botonSeleccionadoProducto.Name.ToString());
                DataRow[] fila = dtProductos.Select("id_producto = " + iIdProductoGrid);

                if (fila.Length != 0)
                    iPagaServicio = Convert.ToInt32(fila[0][5].ToString());
                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Se encontró un error al buscar el parámetro de servicio en el producto.";
                    ok.ShowDialog();
                    return;
                }
                //-------------------------------------------------------------------------------------------------------------

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
                                        "0", "", "0", "", "0", "0", "0", Program.descuento_empleados * 100,
                                        "0", dbAuxiliarDesc, iPagaServicio);
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
                                        "0", "", "0", "", "0", "0", "0", "0", "0", "0", iPagaServicio);
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
                int iPagaServicio_REC;

                Decimal dbCantidad_REC;
                Decimal dbPrecioUnitario_REC;
                Decimal dbValorDescuento_REC;
                Decimal dbValorIva_REC;
                Decimal dbValorServicio_REC;

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
                    iPagaServicio_REC = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_servicio"].Value);

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

                    if (iPagaServicio_REC == 1)
                    {
                        dbValorServicio_REC = (dbPrecioUnitario_REC - dbValorDescuento_REC) * Convert.ToDecimal(Program.servicio);
                        dbSumaServicio_REC += dbCantidad_REC * dbValorServicio_REC;
                    }
                }

                dbSumaSubtotales_REC = dbSumaSubtotalConIva_REC + dbSumaSubtotalSinIva_REC;
                dbSumaDescuentos_REC = dbSumaDescuentoConIva_REC + dbSumaDescuentoSinIva_REC;

                dbSubtotalNeto_REC = dbSumaSubtotalConIva_REC + dbSumaSubtotalSinIva_REC - dbSumaDescuentoConIva_REC - dbSumaDescuentoSinIva_REC;
                dbTotalDebido_REC = dbSubtotalNeto_REC + dbSumaIva_REC + dbSumaServicio_REC;

                lblSubtotal.Text = "$ " + dbSumaSubtotales_REC.ToString("N2");
                lblDescuento.Text = "$ " + dbSumaDescuentos_REC.ToString("N2");
                lblImpuestos.Text = "$ " + (dbSumaIva_REC + dbSumaServicio_REC).ToString("N2");
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
        
        #endregion

        #region FUNCIONES PARA INSERTAR EN LA BASE DE DATOS

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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        #region FUNCIONES DE LOS MODIFICADORES

        //FUNCION PARA OBTENER EL IDENTIFICADOR DE MODIFICADORES
        private int obtenerIdModificadorPadre()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and modificador = 1" + Environment.NewLine;
                sSql += "and nivel = 2";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                    return 0;
                else
                {
                    iIdProductoPadreModificador = Convert.ToInt32(dtConsulta.Rows[0]["id_producto"].ToString());
                    return 1;
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

        //FUNCION PARA CONSULTAR LAS INICIALES DE LOS MODIFICADORES
        private void cargarModificadores()
        {
            try
            {
                int iRespuesta_P = obtenerIdModificadorPadre();

                if (iRespuesta_P == -1)
                    return;

                if (iRespuesta_P == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra configurador el módulo de modificadores.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "select * from art_vw_letrainicialModificador" + Environment.NewLine;
                sSql += "order by letra";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra ítems para el módulo de modificadores.";
                    ok.ShowDialog();
                    return;
                }

                iBanderaCategorias = 0;
                iBanderaSubCategorias = 0;
                iBanderaModificadores = 1;

                btnModificadores.BackColor = Color.FromArgb(255, 128, 128);
                lblProductos.Text = "MODIFICADORES";
                pnlProductos.Controls.Clear();

                dtCategorias = new DataTable();
                dtCategorias = dtConsulta.Clone();

                foreach (DataRow dr in dtConsulta.Rows)
                {
                    dtCategorias.ImportRow(dr);
                }

                iCuentaCategorias = 0;

                if (dtCategorias.Rows.Count > 0)
                {
                    if (dtCategorias.Rows.Count > 10)
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

                    if (crearBotonesModificadores() == false)
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

        //FUNCION PARA CREAR LOS BOTONES DE MODIFICADORES
        private bool crearBotonesModificadores()
        {
            try
            {
                pnlCategorias.Controls.Clear();
                iPosXCategorias = 0;
                iPosYCategorias = 0;
                iCuentaAyudaCategorias = 0;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        botonModificador[i, j] = new Button();
                        botonModificador[i, j].Cursor = Cursors.Hand;
                        botonModificador[i, j].Click += boton_clic_modificadores;
                        botonModificador[i, j].Size = new Size(92, 71);
                        botonModificador[i, j].Location = new Point(iPosXCategorias, iPosYCategorias);
                        botonModificador[i, j].BackColor = Color.Lime;
                        botonModificador[i, j].Font = new Font("Maiandra GD", 16, FontStyle.Bold);
                       //botonModificador[i, j].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                        botonModificador[i, j].Text = dtCategorias.Rows[iCuentaCategorias]["letra"].ToString();
                        //botonModificador[i, j].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                        botonModificador[i, j].FlatStyle = FlatStyle.Flat;
                        botonModificador[i, j].FlatAppearance.BorderSize = 1;
                        botonModificador[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);

                        pnlCategorias.Controls.Add(botonModificador[i, j]);
                        iCuentaCategorias++;
                        iCuentaAyudaCategorias++;

                        if (j + 1 == 5)
                        {
                            iPosXCategorias = 0;
                            iPosYCategorias += 71;
                        }

                        else
                        {
                            iPosXCategorias += 92;
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
        private void boton_clic_modificadores(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                botonSeleccionadoCategoria = sender as Button;

                cargarProductosModificadores(botonSeleccionadoCategoria.Text);
                
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA CARGAR LOS BOTONES DE PRODUCTOS
        private void cargarProductosModificadores(string sLetraInicial_P)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, PP.valor, CP.codigo, P.paga_servicio" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_clase_producto CP ON CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 3" + Environment.NewLine;
                sSql += "and P.is_active = 1" + Environment.NewLine;
                sSql += "and P.subcategoria = 0" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4" + Environment.NewLine;
                sSql += "and P.modificador = 1" + Environment.NewLine;
                sSql += "and P.codigo like '" + sLetraInicial_P + "%'" + Environment.NewLine;
                sSql += "order by P.secuencia";

                dtProductos = new DataTable();
                dtProductos.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtProductos, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaProductos = 0;

                if (dtProductos.Rows.Count > 0)
                {
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

        #endregion

        #region INTEGRACIÓN NUEVA PARA ENVIAR A LA CLASE DE COMANDAS

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarComanda(int iOp)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                iBanderaAbrirPagos = 0;

                if (iOp == 1)
                {
                    if (actualizarComanda_V2() == false)
                    {
                        goto reversa;
                    }

                    this.Cursor = Cursors.Default;
                }

                else
                {
                    if (insertarComanda_V2() == false)
                    {
                        goto reversa;
                    }

                    reabrir = "OK";

                    if (Program.iReimprimirCocina == 0)
                        chkImprimirCocina.Checked = false;
                    else
                        chkImprimirCocina.Checked = true;

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

        //FUNCION PARA CREAR LA TABLA DE ITEMS PARA ENVIAR POR PARAMETRO
        private bool crearTablaItems()
        {
            try
            {
                dtItems = new DataTable();
                dtItems.Clear();

                dtItems.Columns.Add("id_producto");
                dtItems.Columns.Add("valor_unitario");
                dtItems.Columns.Add("cantidad");
                dtItems.Columns.Add("valor_descuento");
                dtItems.Columns.Add("paga_iva");
                dtItems.Columns.Add("bandera_cortesia");
                dtItems.Columns.Add("bandera_descuento");
                dtItems.Columns.Add("bandera_comentario");
                dtItems.Columns.Add("id_mascara");
                dtItems.Columns.Add("id_ordenamiento");
                dtItems.Columns.Add("secuencia_impresion");
                dtItems.Columns.Add("motivo_cortesia");
                dtItems.Columns.Add("motivo_descuento");
                dtItems.Columns.Add("codigo_producto");
                dtItems.Columns.Add("nombre_producto");
                dtItems.Columns.Add("porcentaje_descuento");
                dtItems.Columns.Add("paga_servicio");

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dtItems.Rows.Add(dgvPedido.Rows[i].Cells["id_producto"].Value,
                                     dgvPedido.Rows[i].Cells["valor_unitario"].Value,
                                     dgvPedido.Rows[i].Cells["cantidad"].Value,
                                     dgvPedido.Rows[i].Cells["valor_descuento"].Value,
                                     dgvPedido.Rows[i].Cells["paga_iva"].Value,
                                     dgvPedido.Rows[i].Cells["bandera_cortesia"].Value,
                                     dgvPedido.Rows[i].Cells["bandera_descuento"].Value,
                                     dgvPedido.Rows[i].Cells["bandera_comentario"].Value,
                                     dgvPedido.Rows[i].Cells["id_mascara"].Value,
                                     dgvPedido.Rows[i].Cells["id_ordenamiento"].Value,
                                     dgvPedido.Rows[i].Cells["secuencia_impresion"].Value,
                                     dgvPedido.Rows[i].Cells["motivo_cortesia"].Value,
                                     dgvPedido.Rows[i].Cells["motivo_descuento"].Value,
                                     dgvPedido.Rows[i].Cells["codigo_producto"].Value,
                                     dgvPedido.Rows[i].Cells["nombre_producto"].Value,
                                     dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value,
                                     dgvPedido.Rows[i].Cells["paga_servicio"].Value
                                    );
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

        //FUNCION PARA CREAR LA TABLA DE DETALLE DE LOS ITEMS
        private bool crearTableDetalleItems()
        {
            try
            {
                int iIdDetalle_A = 0;
                string sDetalle_A;

                dtDetalleItems = new DataTable();
                dtDetalleItems.Clear();

                dtDetalleItems.Columns.Add("id_producto");
                dtDetalleItems.Columns.Add("detalle");

                for (int i = 0; i < Program.sDetallesItems.GetLength(0); i++)
                {
                    if (Program.sDetallesItems[i, 0] != null)
                    {
                        iIdDetalle_A = Convert.ToInt32(Program.sDetallesItems[i, 0]);
                        sDetalle_A = Program.sDetallesItems[i, 1];

                        dtDetalleItems.Rows.Add(iIdDetalle_A, sDetalle_A);
                    }

                    else
                        break;
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

        //FUNCION PARA ENVIAR LOS PARAMETROS A LA COMANDA - INSERTAR NUEVA COMANDA
        private bool insertarComanda_V2()
        {
            try
            {
                if (crearTablaItems() == false)
                    return false;

                if (crearTableDetalleItems() == false)
                    return false;

                Clases_Crear_Comandas.ClaseCrearComanda comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                bRespuesta = comanda.insertarComanda(0, iIdPersona, 0, iIdOrigenOrden, dbTotalDebido_REC, "Abierta",
                                                    Convert.ToDecimal(dPorcentajeDescuento), iIdMesa, iIdCajero, 
                                                    iNumeroPersonas, sNombreParaMesa, iIdMesero, Program.iIdPosTerminal, 
                                                    Convert.ToDecimal(Program.servicio * 100), iConsumoAlimentos, 
                                                    iIdPromotor, iIdRepartidor, Program.iIdPosCierreCajero,
                                                    dtItems, dtDetalleItems, 0, Program.iIdLocalidad, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdPedido = comanda.iIdPedido;
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                limpiar.limpiarArregloComentarios();

                if (Program.iHabilitarDestinosImpresion == 1)
                {
                    if (chkImprimirCocina.Checked == true)
                    {
                        if (Program.iEjecutarImpresion == 1)
                        {
                            Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iVersionImpresionComanda);
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
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS A LA COMANDA - INSERTAR NUEVA COMANDA
        private bool actualizarComanda_V2()
        {
            try
            {
                if (crearTablaItems() == false)
                    return false;

                if (crearTableDetalleItems() == false)
                    return false;

                Clases_Crear_Comandas.ClaseCrearComanda comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                bRespuesta = comanda.insertarComanda(iIdPedido, iIdPersona, 0, iIdOrigenOrden, dbTotalDebido_REC, "Abierta",
                                                    Convert.ToDecimal(dPorcentajeDescuento), iIdMesa, iIdCajero,
                                                    iNumeroPersonas, sNombreParaMesa, iIdMesero, Program.iIdPosTerminal,
                                                    Convert.ToDecimal(Program.servicio * 100), iConsumoAlimentos,
                                                    iIdPromotor, iIdRepartidor, Program.iIdPosCierreCajero,
                                                    dtItems, dtDetalleItems, 1, Program.iIdLocalidad, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
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
                            Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iVersionImpresionComanda);
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
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void frmComanda_Load(object sender, EventArgs e)
        {
            //this.Text = Program.sEtiqueta;
            datosListas();

            if (Program.iManejaMitad == 1)
            {
                btnMitad.Enabled = true;
            }

            else
            {
                btnMitad.Enabled = false;
            }

            if (reabrir == "OK" || reabrir == "DIVIDIDO" || reabrir == "COPIAR")
            {
                if (consultarDatosOrden() == false)
                {
                    this.Close();
                    return;
                }

                if (reabrir == "COPIAR")
                {
                    reabrir = "";
                    iVersionImpresionComanda = 1;
                    extraerNumeroOrden();
                }

                else
                {
                    consultarGeneraFactura();
                    versionImpresion();
                }
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

            iNivelGeneral = 3;
            cargarCategorias(2, 0);
            llenarDatosInformativosComanda();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (iBanderaCategorias == 1)
            {
                btnAnterior.Enabled = true;
                crearBotonesCategorias();
            }

            else if (iBanderaSubCategorias == 1)
            {

            }

            else if (iBanderaModificadores == 1)
            {
                btnAnterior.Enabled = true;
                crearBotonesModificadores();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (iBanderaCategorias == 1)
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

            else if (iBanderaSubCategorias == 1)
            {

            }

            else if (iBanderaModificadores == 1)
            {
                iCuentaCategorias -= iCuentaAyudaCategorias;

                if (iCuentaCategorias <= 10)
                {
                    btnAnterior.Enabled = false;
                }

                btnSiguiente.Enabled = true;
                iCuentaCategorias -= 10;

                crearBotonesModificadores();
            }
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
            if (iBanderaModificadores == 0)
            {
                cargarModificadores();

                if (iBanderaModificadores == 1)
                    btnRegresar.Visible = true;
            }

            else
            {
                btnRegresar.Visible = false;
                lblProductos.Text = "PRODUCTOS";
                btnModificadores.BackColor = Color.FromArgb(192, 255, 192);
                pnlProductos.Controls.Clear();
                iNivelGeneral = 3;
                cargarCategorias(2, 0);
            }
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
                    int iPagaServicio_A;
                    Decimal dbCantidad_I = item.dCantidad;
                    Decimal dbPrecioUnitario_I = item.dValorUnitario;
                    string sNombreItem_I = item.sNombreProducto;
                    item.Close();

                    if (Program.iManejaServicio == 1)
                        iPagaServicio_A = 1;
                    else
                        iPagaServicio_A = 0;

                    dgvPedido.Rows.Add(dbCantidad_I,
                                    sNombreItem_I,
                                    dbPrecioUnitario_I,
                                    (dbPrecioUnitario_I * dbCantidad_I).ToString("N2"),
                                    Program.iIdProductoNuevoItem,
                                    "1", "00",
                                    iVersionImpresionComanda.ToString(), 
                                    "0", "", "0", "", "0", "0", "0", "0", "1", "0", iPagaServicio_A);

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
                        row["paga_servicio"] = dgvPedido.Rows[i].Cells["paga_servicio"].Value.ToString();

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
                                               dtConsulta.Rows[i]["valor_descuento"].ToString(),
                                               dtConsulta.Rows[i]["paga_servicio"].ToString()

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
                            row["paga_servicio"] = dgvPedido.Rows[i].Cells["paga_servicio"].Value.ToString();

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

                    //sSql = "";
                    //sSql += "select count(*) cuenta" + Environment.NewLine;
                    //sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    //sSql += "where id_pedido = " + iIdPedido;

                    //dtConsulta = new DataTable();
                    //dtConsulta.Clear();

                    //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    //if (bRespuesta == false)
                    //{
                    //    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    //    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    //    catchMensaje.ShowDialog();
                    //    return;
                    //}

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
                            ComandaNueva.frmCobros t = new ComandaNueva.frmCobros(iIdPedido.ToString(), 0);
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

        private void btnRegresar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnRegresar.Visible = false;
            lblProductos.Text = "PRODUCTOS";
            btnModificadores.BackColor = Color.FromArgb(192, 255, 192);
            pnlProductos.Controls.Clear();
            iNivelGeneral = 3;
            cargarCategorias(2, 0);
        }
    }
}
