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

namespace Palatium.Comida_Rapida
{
    public partial class frmComandaComidaRapida : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        Clases_Crear_Comandas.ClaseCrearComanda comanda;

        ValidarCedula validarCedula = new ValidarCedula();

        ToolTip ttMensajeMesas = new ToolTip();

        string sSql;
        string sNombreProducto_P;
        string sFecha;
        string sDescripcionFormaPago;
        string sEstablecimiento;
        string sPuntoEmision;
        string sNumeroLote;
        string sNumeroComprobante;
        string sCiudad;
        string sCorreoAyuda;
        string sCodigoMetodoPago;

        long iMaximo;

        DataTable dtConsulta;
        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtRecargos;
        DataTable dtItems;
        DataTable dtDetalleItems;
        DataTable dtPagos;

        bool bRespuesta;

        Button[,] botonFamilias = new Button[2, 4];
        Button[,] botonProductos = new Button[5, 5];

        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;

        int iCuentaCategorias;
        int iPosXCategorias;
        int iPosYCategorias;
        int iCuentaAyudaCategorias;
        int iCuentaProductos;
        int iPosXProductos;
        int iPosYProductos;
        int iCuentaAyudaProductos;
        int iIdListaMinorista;

        int iIdPersona;
        int iIdOrigenOrden;
        int iIdPedido;
        int iNumeroPedidoOrden;
        int iIdTipoFormaCobro;
        int iIdTipoComprobante;
        int iIdFactura;
        int iBanderaEfectivoTarjeta;
        int iBanderaAplicaRecargo;
        int iBanderaExpressTarjeta;
        int iConciliacion;
        int iOperadorTarjeta;
        int iTipoTarjeta;
        int iBanderaInsertarLote;
        int iPagaIva_P;
        int iIdSriFormaPago_P;
        int iNivelGeneral;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iTercerDigito;
        int iIdDocumentoPorCobrar;
        int iBanderaReabrir;

        Decimal dTotalDebido;
        Decimal dbCantidadRecalcular;
        Decimal dbPrecioRecalcular;
        Decimal dbValorTotalRecalcular;
        Decimal dbSubtotalRecalcular;
        Decimal dbValorIVA;
        Decimal dbValorGrid;
        Decimal dbValorRecuperado;
        Decimal dbCambio;
        Decimal dbPropina;

        SqlParameter[] parametro;

        public frmComandaComidaRapida(int iIdPosOrigenOrden_P, int iBanderaExpressTarjeta_P)
        {
            this.iIdOrigenOrden = iIdPosOrigenOrden_P;
            this.iBanderaExpressTarjeta = iBanderaExpressTarjeta_P;
            InitializeComponent();
        }

        public frmComandaComidaRapida(int iIdPedido_P)
        {
            this.iIdPedido = iIdPedido_P;
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                    iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"]);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR UNA PRECUENTA RAPIDA
        private bool crearPrecuentaRapida()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                Decimal dbTotal_R = 0;
                Decimal dbSubtotalConIva_R = 0;
                Decimal dbSubtotalSinIva_R = 0;
                Decimal dbSubtotalNeto_R;
                Decimal dbSumaIVA_R = 0;
                Decimal dbCantidad_R;
                Decimal dbPrecioUnitario_R;
                Decimal dbValorIva_R;
                int iPagaIva_R;
                string sNombre_R;


                string sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "IMPRESIÓN DE LA PRECUENTA".PadLeft(34, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "ORDEN: VENTA RÁPIDA" + Environment.NewLine;
                sTexto += "Fecha: " + Convert.ToDateTime(sFecha).ToString("dd-MM-yyyy") + "   Hora: " + Convert.ToDateTime(sFecha).ToString("HH:mm:ss") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "CI. RUC: " + txtIdentificacion.Text.Trim() + Environment.NewLine;
                sTexto += "CLIENTE: " + txtRazonSocial.Text.Trim().ToUpper() + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "    DESCRIPCION     CANT   PVP     TOTAL" + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                //RECORREL EL DATAGRID
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    sNombre_R = dgvPedido.Rows[i].Cells["producto"].Value.ToString().Trim().ToUpper();
                    dbPrecioUnitario_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value);
                    dbCantidad_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    iPagaIva_R = Convert.ToInt32(dgvPedido.Rows[i].Cells["pagaIva"].Value);

                    if (iPagaIva_R == 1)
                    {
                        dbSubtotalConIva_R += dbCantidad_R * dbPrecioUnitario_R;
                        dbValorIva_R = dbCantidad_R * dbPrecioUnitario_R * Convert.ToDecimal(Program.iva);
                        dbSumaIVA_R += dbValorIva_R;
                    }

                    else
                    {
                        dbSubtotalSinIva_R += dbCantidad_R * dbPrecioUnitario_R;
                    }

                    if (sNombre_R.Length > 20)
                    {
                        sTexto += sNombre_R.Substring(0, 20) + dbCantidad_R.ToString().PadLeft(3, ' ') + dbPrecioUnitario_R.ToString("N2").PadLeft(8, ' ') + (dbCantidad_R * dbPrecioUnitario_R).ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        
                        if (sNombre_R.Length > 40)
                            sTexto += sNombre_R.Substring(20, 20) + Environment.NewLine;
                        else
                            sTexto += sNombre_R.Substring(20) + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += sNombre_R.PadRight(20, ' ') + dbCantidad_R.ToString().PadLeft(3, ' ') + dbPrecioUnitario_R.ToString("N2").PadLeft(8, ' ') + (dbCantidad_R * dbPrecioUnitario_R).ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += ("SUBTOTAL " + (Program.iva * 100).ToString("N0") + "%").PadRight(20, ' ') + dbSubtotalConIva_R.ToString("N2").PadLeft(20, ' ') + Environment.NewLine;
                sTexto += ("SUBTOTAL 0%").PadRight(20, ' ') + dbSubtotalSinIva_R.ToString("N2").PadLeft(20, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                dbSubtotalNeto_R = dbSubtotalConIva_R + dbSubtotalSinIva_R;
                sTexto += "SUBTOTAL NETO:".PadRight(20, ' ') + dbSubtotalNeto_R.ToString("N2").PadLeft(20, ' ') + Environment.NewLine;
                sTexto += ("IVA " + (Program.iva * 100).ToString("N0") + "%:").PadRight(20, ' ') + dbSumaIVA_R.ToString("N2").PadLeft(20, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                dbTotal_R = dbSubtotalNeto_R + dbSumaIVA_R;
                sTexto += "TOTAL:".PadRight(20, ' ') + dbTotal_R.ToString("N2").PadLeft(20, ' ');

                Utilitarios.frmReporteGenerico reporte = new Utilitarios.frmReporteGenerico(sTexto, 1, 0, 0, 0);
                reporte.ShowDialog();

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

        //FUNCION PARA CARGAR LAS CATEGORIAS
        private void cargarCategorias(int iNivel_P, int iIdProductoPadre_P)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva," + Environment.NewLine;
                sSql += "P.subcategoria, isnull(P.categoria_delivery, 0) categoria_delivery," + Environment.NewLine;
                sSql += "isnull(P.imagen_categoria, '') imagen_categoria" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_Producto = NP.id_Producto" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = " + iNivel_P + Environment.NewLine;
                //sSql += "and id_producto_padre in" + Environment.NewLine;
                //sSql += "(select id_producto from cv401_productos where codigo ='2')" + Environment.NewLine;
                sSql += "and modificador = 0" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;

                if (iNivel_P == 3)
                    sSql += "and P.id_producto_padre = " + iIdProductoPadre_P + Environment.NewLine;

                if (iBanderaExpressTarjeta == 1)
                {
                    sSql += "and P.maneja_almuerzos = 1" + Environment.NewLine;
                }
                sSql += "order by P.secuencia";

                dtCategorias = new DataTable();
                dtCategorias.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCategorias, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = dtCategorias.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dtCategorias.Rows[i]["categoria_delivery"].ToString()) == 1)
                    {
                        dtCategorias.Rows.RemoveAt(i);
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
                        btnSiguiente.Enabled = false;
                        btnAnterior.Visible = false;
                        btnSiguiente.Visible = false;
                    }

                    if (crearBotonesCategorias() == false)
                    { }

                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
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
                        botonFamilias[i, j] = new Button();
                        botonFamilias[i, j].Cursor = Cursors.Hand;
                        botonFamilias[i, j].Click += new EventHandler(boton_clic_categorias);
                        botonFamilias[i, j].Size = new Size(130, 71);
                        botonFamilias[i, j].Location = new Point(iPosXCategorias, iPosYCategorias);
                        botonFamilias[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Bold);
                        botonFamilias[i, j].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                        botonFamilias[i, j].Text = dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString();
                        botonFamilias[i, j].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                        botonFamilias[i, j].FlatStyle = FlatStyle.Flat;
                        botonFamilias[i, j].FlatAppearance.BorderSize = 1;
                        botonFamilias[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128);
                        botonFamilias[i, j].FlatAppearance.MouseDownBackColor = Color.Fuchsia;

                        if (Convert.ToInt32(dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString()) == 1)
                        {
                            ttMensajeMesas.SetToolTip(botonFamilias[i, j], dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString().Trim().ToUpper() + " CONTIENE SUBCATEGORÍAS");
                            botonFamilias[i, j].BackColor = Color.LightSalmon;
                        }

                        else
                        {
                            ttMensajeMesas.SetToolTip(botonFamilias[i, j], "CATEGORÍA: " + dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString());
                            botonFamilias[i, j].BackColor = Color.White;
                        }

                        if (dtCategorias.Rows[iCuentaCategorias]["imagen_categoria"].ToString().Trim() != "")
                        {
                            Image foto;
                            byte[] imageBytes;

                            imageBytes = Convert.FromBase64String(dtCategorias.Rows[iCuentaCategorias]["imagen_categoria"].ToString().Trim());

                            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                            {
                                foto = Image.FromStream(ms, true);
                            }

                            botonFamilias[i, j].TextAlign = ContentAlignment.BottomCenter;
                            botonFamilias[i, j].Image = foto;
                            botonFamilias[i, j].ImageAlign = ContentAlignment.TopCenter;
                            botonFamilias[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        pnlCategorias.Controls.Add(botonFamilias[i, j]);

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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                    cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag));
                }

                else
                {
                    //cargarProductos(Convert.ToInt32(botonSeleccionadoCategoria.Tag), 4);
                    cargarCategorias(3, Convert.ToInt32(botonSeleccionadoCategoria.Tag));
                }

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PRODUCTOS
        private void cargarProductos(int iIdProducto_P)
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaProductos = 0;

                if (dtProductos.Rows.Count > 0)
                {
                    if (dtProductos.Rows.Count > 25)
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
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

        //FUNCION PARA CREAR LOS BOTONES DE PRODUCTOS
        private bool crearBotonesProductos()
        {
            try
            {
                pnlProductos.Controls.Clear();

                iPosXProductos = 0;
                iPosYProductos = 0;
                iCuentaAyudaProductos = 0;

                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < 5; ++j)
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                int iPagaIva_R;
                int iPagaServicio_R;
                int iIdProductoGrid;

                Decimal dbCantidad_R;
                Decimal dbValorUnitario_R;
                Decimal dbSubtotal_R;
                Decimal dbValorIVA_R;
                Decimal dbValorServicio_R;
                Decimal dbTotal_R;

                for (int i = 0; i < dgvPedido.Rows.Count; ++i)
                {
                    if (dgvPedido.Rows[i].Cells["idProducto"].Value.ToString() == botonSeleccionadoProducto.Name.ToString())
                    {
                        iPagaIva_R = Convert.ToInt32(dgvPedido.Rows[i].Cells["pagaIva"].Value);
                        iPagaServicio_R = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_servicio"].Value);

                        dbCantidad_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                        dbCantidad_R += 1;
                        dgvPedido.Rows[i].Cells["cantidad"].Value = dbCantidad_R;
                        dbValorUnitario_R = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value);
                        dbSubtotal_R = dbCantidad_R * dbValorUnitario_R;
                        dgvPedido.Rows[i].Cells["subtotal"].Value = dbSubtotal_R.ToString("N2");
                        
                        if (iPagaIva_R == 1)
                            dbValorIVA_R = dbSubtotal_R * Convert.ToDecimal(Program.iva);
                        else
                            dbValorIVA_R = 0;

                        if (iPagaServicio_R == 1)
                            dbValorServicio_R = dbSubtotal_R * Convert.ToDecimal(Program.servicio);
                        else
                            dbValorServicio_R = 0;

                        dbTotal_R = dbSubtotal_R + dbValorIVA_R + dbValorServicio_R;
                        dgvPedido.Rows[i].Cells["valor"].Value = dbTotal_R.ToString("N2");
                        iExiste_R = 1;
                    }
                }

                if (iExiste_R == 0)
                {
                    //BUSCAR SI PAGA SERVICIO
                    //-------------------------------------------------------------------------------------------------------------
                    iIdProductoGrid = Convert.ToInt32(botonSeleccionadoProducto.Name.ToString());
                    DataRow[] fila = dtProductos.Select("id_producto = " + iIdProductoGrid);

                    if (fila.Length != 0)
                        iPagaServicio_R = Convert.ToInt32(fila[0][5].ToString());
                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Se encontró un error al buscar el parámetro de servicio en el producto.";
                        ok.ShowDialog();
                        return;
                    }
                    //-------------------------------------------------------------------------------------------------------------

                    iPagaIva_R = Convert.ToInt32(botonSeleccionadoProducto.Tag);

                    int i = dgvPedido.Rows.Add();

                    dgvPedido.Rows[i].Cells["cantidad"].Value = "1";
                    dgvPedido.Rows[i].Cells["producto"].Value = botonSeleccionadoProducto.Text.ToString().Trim();
                    sNombreProducto_P = botonSeleccionadoProducto.Text.ToString().Trim();
                    dgvPedido.Rows[i].Cells["idProducto"].Value = botonSeleccionadoProducto.Name;
                    iPagaIva_P = Convert.ToInt32(botonSeleccionadoProducto.Tag.ToString());
                    dgvPedido.Rows[i].Cells["pagaIva"].Value = iPagaIva_P;
                    dgvPedido.Rows[i].Cells["tipoProducto"].Value = botonSeleccionadoProducto.AccessibleDescription;
                    dgvPedido.Rows[i].Cells["paga_servicio"].Value = iPagaServicio_R;

                    if (iPagaIva_P == 1)
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

                    if (iPagaIva_R == 1)
                        dbValorIVA_R = dbSubtotal_R * Convert.ToDecimal(Program.iva);
                    else
                        dbValorIVA_R = 0;

                    if (iPagaServicio_R == 1)
                        dbValorServicio_R = dbSubtotal_R * Convert.ToDecimal(Program.servicio);
                    else
                        dbValorServicio_R = 0;

                    dbTotal_R = dbSubtotal_R + dbValorIVA_R + dbValorServicio_R;
                    dgvPedido.Rows[i].Cells["valor"].Value = dbTotal_R.ToString("N2");
                }

                calcularTotales();
                dgvPedido.ClearSelection();

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR TOTALES
        public void calcularTotales()
        {
            int iPagaIva;
            int iPagaServicio;

            Decimal dSubtotalConIva = 0;
            Decimal dSubtotalCero = 0;
            Decimal dbValorIva = 0;
            Decimal dbValorServicio = 0;
            Decimal dbSumaIva = 0;
            Decimal dbSumaServicio = 0;
            dTotalDebido = 0;

            for (int i = 0; i < dgvPedido.Rows.Count; ++i)
            {
                iPagaIva = Convert.ToInt32(dgvPedido.Rows[i].Cells["pagaIva"].Value);
                iPagaServicio = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_servicio"].Value);

                if (iPagaIva == 0)
                    dSubtotalCero += Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString());

                else
                {
                    dSubtotalConIva += Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString());
                    dbValorIva = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString()) * Convert.ToDecimal(Program.iva);
                    dbSumaIva += dbValorIva;
                }

                if (iPagaServicio == 1)
                {
                    dbValorServicio = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value) * Convert.ToDecimal(Program.servicio);
                    dbSumaServicio += dbValorServicio;
                }

                if (iIdPedido != 0)
                {
                    Decimal dbTotalLinea_P;

                    dbTotalLinea_P = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString());
                    dbTotalLinea_P += dbValorIva + dbValorServicio;

                    dgvPedido.Rows[i].Cells["valor"].Value = dbTotalLinea_P.ToString("N2");
                }

            }

            //dTotalDebido = num1 + num2 - num3 - num4 + (num1 - num3) * Convert.ToDecimal(Program.iva) + num7;
            dTotalDebido = dSubtotalConIva + dSubtotalCero + dbSumaIva + dbSumaServicio;
            lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
        }
        
        #endregion

        #region FUNCIONES PARA GUARDAR LA COMANDA

        //FUNCION PARA OBTENER LA ID LOCALIDAD
        private void datosFactura()
        {
            try
            {
                sSql = "";
                sSql += "select L.id_localidad, L.establecimiento, L.punto_emision" + Environment.NewLine;
                sSql += "from tp_localidades L, tp_localidades_impresoras P " + Environment.NewLine;
                sSql += "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql += "and L.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and L.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentran registros en la consulta.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        txtEstablecimiento.Text = dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString();

                        sEstablecimiento = dtConsulta.Rows[0]["establecimiento"].ToString();
                        sPuntoEmision = dtConsulta.Rows[0]["punto_emision"].ToString();

                        //TxtNumeroFactura.Text = dtConsulta.Rows[0]["numeronotaentrega"].ToString();
                    }
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
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
        
        //FUNCION PARA CREAR EL REPORTE
        private void crearReporte()
        {
            try
            {
                if (Program.iHabilitarDestinosImpresion == 1)
                {
                    Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), 1);
                    cocina.ShowDialog();
                }

                if (Program.iEjecutarImpresion == 1)
                {
                    if (iIdTipoComprobante == 1)
                    {
                        ReportesTextBox.frmVistaFactura frmVistaFactura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 1);
                        frmVistaFactura.ShowDialog();

                        if (frmVistaFactura.DialogResult == DialogResult.OK)
                        {
                            frmVistaFactura.Close();

                            if (iBanderaExpressTarjeta == 0)
                            {
                                ReportesTextBox.frmVerPedidoExpress precuenta = new ReportesTextBox.frmVerPedidoExpress(iIdPedido.ToString(), 1);
                                precuenta.ShowDialog();
                            }

                            else
                            {
                                ReportesTextBox.frmVerPedidoTarjetaAlmuerzo precuenta = new ReportesTextBox.frmVerPedidoTarjetaAlmuerzo(iIdPedido.ToString(), 1);
                                precuenta.ShowDialog();
                            }

                            Cambiocs cambiocs = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            cambiocs.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                            cambiocs.ShowDialog();                            
                            this.Close();
                        }
                    }

                    else
                    {
                        if (iBanderaExpressTarjeta == 0)
                        {
                            ReportesTextBox.frmVerNotaVentaFactura notaVenta = new ReportesTextBox.frmVerNotaVentaFactura(iIdPedido.ToString(), 1);
                            notaVenta.ShowDialog();

                            if (notaVenta.DialogResult == DialogResult.OK)
                            {
                                Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                                ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                                ok.ShowDialog();
                                notaVenta.Close();
                                this.Close();
                            }
                        }

                        else
                        {
                            ReportesTextBox.frmVerPedidoTarjetaAlmuerzo precuenta = new ReportesTextBox.frmVerPedidoTarjetaAlmuerzo(iIdPedido.ToString(), 1);
                            precuenta.ShowDialog();

                            Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            ok.lblVerMensaje.Text = "TICKET DE TARJETA GENERADA";
                            ok.ShowDialog();
                            precuenta.Close();
                            this.Close();
                        }
                    }
                }

                else
                {
                    if (iIdTipoComprobante == 1)
                    {
                        Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                        ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                        ok.ShowDialog();
                        this.Close();
                    }

                    else
                    {
                        if (iBanderaExpressTarjeta == 0)
                        {
                            Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                            ok.ShowDialog();
                            this.Close();
                        }

                        else
                        {
                            Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            ok.lblVerMensaje.Text = "TICKET DE TARJETA GENERADA";
                            ok.ShowDialog();
                            this.Close();
                        }
                    }
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

        #region FUNCIONES NUEVAS INTEGRADAS PARA FACTURACION

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {
            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        //FUNCION PARA VALIDAR LA IDENTIFICACION
        private void validarIdentificacion()
        {
            try
            {
                if (txtIdentificacion.Text.Length >= 10)
                {
                    iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));

                    if (txtIdentificacion.Text.Length == 10)
                    {
                        if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                        {
                            consultarRegistro();
                            return;
                        }

                        else
                        {
                            mensajeValidarCedula();
                            return;
                        }
                    }

                    else if (txtIdentificacion.Text.Length == 13)
                    {
                        if (iTercerDigito == 9)
                        {
                            if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                            {
                                consultarRegistro();
                                return;
                            }

                            else
                            {
                                mensajeValidarCedula();
                                return;
                            }
                        }

                        else if (iTercerDigito == 6)
                        {
                            if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                            {
                                consultarRegistro();
                                return;
                            }

                            else
                            {
                                mensajeValidarCedula();
                                return;
                            }
                        }

                        else if (iTercerDigito <= 5 || iTercerDigito >= 0)
                        {
                            if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                            {
                                consultarRegistro();
                                return;
                            }

                            else
                            {
                                mensajeValidarCedula();
                                return;
                            }
                        }
                    }
                }

                else
                {
                    mensajeValidarCedula();
                    return;
                }
            }

            catch (Exception ex)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        //FUNCION MENSAJE DE VALIDACION DE CEDULA
        private void mensajeValidarCedula()
        {
            ok = new VentanasMensajes.frmMensajeOK();
            ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
            ok.ShowDialog();
            txtIdentificacion.Clear();
            txtRazonSocial.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            txtIdentificacion.Focus();
        }

        //FUNCION PARA CONSULTAR DATOS DEL CLIENTE
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion direccion_cliente," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                        txtRazonSocial.Text = (dtConsulta.Rows[0]["nombres"].ToString().Trim().ToUpper() + " " + dtConsulta.Rows[0]["apellidos"].ToString().Trim().ToUpper()).Trim();
                        txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0]["direccion_cliente"].ToString();
                        sCiudad = dtConsulta.Rows[0][8].ToString();

                        if (dtConsulta.Rows[0][6].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][6].ToString();
                        }

                        else if (dtConsulta.Rows[0][7].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][7].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "";
                        }
                    }

                    else
                    {
                        Facturador.frmNuevoCliente frmNuevoCliente = new Facturador.frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
                        frmNuevoCliente.ShowDialog();

                        if (frmNuevoCliente.DialogResult == DialogResult.OK)
                        {
                            iIdPersona = frmNuevoCliente.iCodigo;
                            txtIdentificacion.Text = frmNuevoCliente.sIdentificacion;
                            txtRazonSocial.Text = (frmNuevoCliente.sNombre + " " + frmNuevoCliente.sApellido).Trim().ToUpper();
                            txtTelefono.Text = frmNuevoCliente.sTelefono;
                            txtDireccion.Text = frmNuevoCliente.sDireccion;
                            txtMail.Text = frmNuevoCliente.sMail;
                            sCiudad = frmNuevoCliente.sCiudad;
                            frmNuevoCliente.Close();
                        }
                    }

                    btnEditar.Visible = true;
                    return;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch(); ;
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        //FUNCION PARA CONSULTAR EL SECUENCIAL DE FACTURA O NOTA DE ENTREGA
        private void consultarFacturaNotaEntrega(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select LOC.establecimiento, LOC.punto_emision, LI.numero_factura, LI.numeronotaentrega" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras LI INNER JOIN" + Environment.NewLine;
                sSql += "tp_localidades LOC ON LOC.id_localidad = LI.id_localidad" + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "and LI.estado = 'A'" + Environment.NewLine;
                sSql += "where LI.id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                txtEstablecimiento.Text = dtConsulta.Rows[0]["establecimiento"].ToString().Trim() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString().Trim();

                if (iOp == 1)
                {
                    TxtNumeroFactura.Text = dtConsulta.Rows[0]["numero_factura"].ToString().Trim().PadLeft(9, '0');
                }

                else
                {
                    TxtNumeroFactura.Text = dtConsulta.Rows[0]["numeronotaentrega"].ToString().Trim().PadLeft(9, '0');
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO DE CONSUMIDOR FINAL
        private void cargarDatosConsumidorFinal()
        {
            txtIdentificacion.Text = "9999999999999";
            txtRazonSocial.Text = "CONSUMIDOR FINAL";
            txtTelefono.Text = "9999999999";
            txtMail.Text = Program.sCorreoElectronicoDefault;
            txtDireccion.Text = "QUITO";
            iIdPersona = Program.iIdPersona;
            idTipoIdentificacion = 180;
            idTipoPersona = 2447;
            btnEditar.Visible = false;
        }

        #endregion

        #region FUNCIONES PARA INTEGRAR LA SEGUNDA VERSION DE GUARDAR LA COMANDA

        //FUNCION PARA CONTROLAR LA GENERACION DE COMANDAS
        private bool crearComandaNueva_V2()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (extraerFecha() == false)
                {
                    Cursor = Cursors.Default;
                    return false;
                }

                int iFacturaElectronica_A = 0;

                if (Program.iFacturacionElectronica == 1)
                    iFacturaElectronica_A = 1;
                
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                if (insertarComanda_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                if (insertarPagos_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                if (insertarFactura_V2(iFacturaElectronica_A) == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                if (insertarMovimientosCaja_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                crearReporte();

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA EXTRAER LA FECHA DEL SISTEMA
        private bool extraerFecha()
        {
            try
            {
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
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

                if (iBanderaEfectivoTarjeta == 0)
                {
                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        dtItems.Rows.Add(dgvPedido.Rows[i].Cells["idProducto"].Value,
                                         dgvPedido.Rows[i].Cells["valuni"].Value,
                                         dgvPedido.Rows[i].Cells["cantidad"].Value,
                                         "0",
                                         dgvPedido.Rows[i].Cells["pagaIva"].Value,
                                         "0", "0", "0", "0", "0", "1", "", "",
                                         dgvPedido.Rows[i].Cells["tipoProducto"].Value,
                                         dgvPedido.Rows[i].Cells["producto"].Value,
                                         "0",
                                         dgvPedido.Rows[i].Cells["paga_servicio"].Value
                                        );
                    }
                }

                else
                {
                    Decimal dbPrecioUni_P;

                    for (int i = 0; i < dtRecargos.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dtRecargos.Rows[i]["valor_recargo"].ToString()) == 0)
                            dbPrecioUni_P = Convert.ToDecimal(dtRecargos.Rows[i]["valor_item"].ToString());
                        else
                            dbPrecioUni_P = Convert.ToDecimal(dtRecargos.Rows[i]["valor_recargo"].ToString());

                        dtItems.Rows.Add(dtRecargos.Rows[i]["id_producto"].ToString(),
                                             dbPrecioUni_P,
                                             dtRecargos.Rows[i]["cantidad"].ToString(),
                                             "0",
                                             dtRecargos.Rows[i]["paga_iva"].ToString(),
                                             "0", "0", "0", "0", "0", "1", "", "",
                                             dtRecargos.Rows[i]["codigo_producto"].ToString(),
                                             dtRecargos.Rows[i]["nombre_producto"].ToString(),
                                             "0",
                                             dtRecargos.Rows[i]["paga_servicio"].ToString()
                                            );
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

        //FUNCION PARA ENVIAR LOS PARAMETROS A LA COMANDA - INSERTAR NUEVA COMANDA
        private bool insertarComanda_V2()
        {
            try
            {
                if (crearTablaItems() == false)
                    return false;

                dtDetalleItems = new DataTable();
                dtDetalleItems.Clear();

                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarComanda(0, iIdPersona, 0, iIdOrigenOrden, dTotalDebido, "Pagada",
                                                    0, 0, Program.iIdCajeroDefault,
                                                    0, "", Program.iIdMesero, Program.iIdPosTerminal,
                                                    Convert.ToDecimal(Program.servicio * 100), 0,
                                                    0, 0, Program.iIdPosCierreCajero,
                                                    dtItems, dtDetalleItems, 0, Program.iIdLocalidad, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdPedido = comanda.iIdPedido;
                iNumeroPedidoOrden = comanda.iNumeroPedidoOrden;
                
                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR NUEVOS PAGOS
        private bool insertarPagos_V2()
        {
            try
            {
                if (crearTablaPagos() == false)
                    return false;

                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarPagos(iIdPedido, dtPagos, dTotalDebido, dbCambio, dbPropina,
                                                   iIdPersona, sFecha, Program.iIdLocalidad, 0, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdDocumentoPorCobrar = comanda.iIdDocumentoCobrar;

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR FACTRUA
        private bool insertarFactura_V2(int iFacturaElectronica_P)
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarFactura(iIdPedido, iIdTipoComprobante, iFacturaElectronica_P,
                                                     iIdPersona, Program.iIdLocalidad, dtPagos, dTotalDebido, 0,
                                                     0, 0, sFecha, iIdDocumentoPorCobrar, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sEstablecimiento = comanda.sEstablecimiento;
                sPuntoEmision = comanda.sPuntoEmision;
                sNumeroComprobante = comanda.sNumeroComprobante;
                iIdFactura = comanda.iIdFactura;

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR FACTRUA
        private bool insertarMovimientosCaja_V2()
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                string sNumeroComprobante_P = sEstablecimiento + "-" + sPuntoEmision + "-" + sNumeroComprobante.Trim().PadLeft(9, '0');

                bRespuesta = comanda.insertarMovimientosCaja(sNumeroComprobante_P, iIdPedido, iIdTipoComprobante,
                                                             iIdPersona, iNumeroPedidoOrden, Program.iIdLocalidad,
                                                             conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CREAR LA TABLA DE PAGOS PARA ENVIAR POR PARAMETRO
        private bool crearTablaPagos()
        {
            try
            {
                dtPagos = new DataTable();
                dtPagos.Clear();

                dtPagos.Columns.Add("id_pos_tipo_forma_cobro");
                dtPagos.Columns.Add("forma_pago");
                dtPagos.Columns.Add("valor");
                dtPagos.Columns.Add("id_sri_forma_pago");
                dtPagos.Columns.Add("conciliacion");
                dtPagos.Columns.Add("id_operador_tarjeta");
                dtPagos.Columns.Add("id_tipo_tarjeta");
                dtPagos.Columns.Add("numero_lote");
                dtPagos.Columns.Add("bandera_insertar_lote");
                dtPagos.Columns.Add("propina");
                dtPagos.Columns.Add("codigo_metodo_pago");
                dtPagos.Columns.Add("numero_documento");
                dtPagos.Columns.Add("fecha_vcto");
                dtPagos.Columns.Add("cg_banco");
                dtPagos.Columns.Add("numero_cuenta");
                dtPagos.Columns.Add("titular");

                dtPagos.Rows.Add(iIdTipoFormaCobro, sDescripcionFormaPago, dTotalDebido, iIdSriFormaPago_P, iConciliacion,
                                 iOperadorTarjeta, iTipoTarjeta, sNumeroLote, iBanderaInsertarLote, dbPropina,
                                 sCodigoMetodoPago, "", "", "0", "", "");

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

        //FUNCION PARA OBTENER LOS VALORES PARA INSERTAR EN LA SECCION DE PAGOS
        private bool obtenerDatosFormaPagoRealizada(int iIdFormaPago_P, string sCodigoFormaPago_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_obtener_datos_formas_pagos" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;

                if (iIdFormaPago_P == 0)
                    sSql += "and codigo = @codigo";
                else
                    sSql += "and id_pos_tipo_forma_cobro = @id_pos_tipo_forma_cobro";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                if (iIdFormaPago_P == 0)
                {
                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@codigo";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = sCodigoFormaPago_P;
                }

                else
                {
                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@id_pos_tipo_forma_cobro";
                    parametro[1].SqlDbType = SqlDbType.Int;
                    parametro[1].Value = iIdFormaPago_P;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentran configurados los registros de cobros. Favor comuníquese con el administrador.";
                    ok.ShowDialog();
                    return false;
                }

                iIdTipoFormaCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_tipo_forma_cobro"].ToString());
                sDescripcionFormaPago = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
                iIdSriFormaPago_P = Convert.ToInt32(dtConsulta.Rows[0]["id_sri_forma_pago"].ToString());
                sCodigoMetodoPago = dtConsulta.Rows[0]["codigo"].ToString().Trim().ToUpper();

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString();
                //txtRazonSocial.Text = dtConsulta.Rows[0]["cliente"].ToString();

                if (dtConsulta.Rows[0]["identificacion"].ToString().Trim() == "9999999999999")
                    cargarDatosConsumidorFinal();
                else
                    consultarRegistro();

                if (iIdPedido != 0)
                {
                    if (cargarDetalleGridCopiar() == false)
                        return false;
                }

                else
                {
                    if (cargarDetalleGrid() == false)
                        return false;
                }

                calcularTotales();
                dgvPedido.ClearSelection();

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvPedido.Rows.Add(Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString()).ToString(),
                                       dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                       dtConsulta.Rows[i]["precio_unitario"].ToString().Trim(),
                                       dtConsulta.Rows[i]["precio_total"].ToString().Trim(),
                                       Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString()) * Convert.ToDecimal(dtConsulta.Rows[i]["precio_unitario"].ToString().Trim()),
                                       dtConsulta.Rows[i]["paga_iva"].ToString().Trim(),
                                       dtConsulta.Rows[i]["id_producto"].ToString().Trim(),                                       
                                       dtConsulta.Rows[i]["codigo_producto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["paga_servicio"].ToString().Trim()
                                       );
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvPedido.Rows.Add(Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString()).ToString(),
                                       dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                       dtConsulta.Rows[i]["valor_nuevo"].ToString().Trim(),
                                       dtConsulta.Rows[i]["precio_total"].ToString().Trim(),
                                       Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString()) * Convert.ToDecimal(dtConsulta.Rows[i]["precio_unitario"].ToString().Trim()),
                                       dtConsulta.Rows[i]["paga_iva"].ToString().Trim(),
                                       dtConsulta.Rows[i]["id_producto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["codigo_producto"].ToString().Trim(),
                                       dtConsulta.Rows[i]["paga_servicio"].ToString().Trim()
                                       );
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

        private void frmComandaComidaRapida_Load(object sender, EventArgs e)
        {
            datosListas();
            datosFactura();
            iNivelGeneral = 3;

            if (iIdPedido == 0)
            {
                cargarDatosConsumidorFinal();

                if (iBanderaExpressTarjeta == 1)
                {
                    iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                    consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);
                    iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                    btnSeleccionFactura.BackColor = Color.FromArgb(255, 255, 192);
                    btnSeleccionFactura.ForeColor = Color.Black;
                    btnSeleccionNotaEntrega.BackColor = Color.Red;
                    btnSeleccionNotaEntrega.ForeColor = Color.White;

                    btnSeleccionFactura.Visible = false;
                    txtIdentificacion.ReadOnly = true;
                    btnEditar.Visible = false;
                    btnBuscar.Visible = false;
                    btnConsumidorFinal.Visible = false;
                    chkPasaporte.Visible = false;
                }

                else
                {
                    //iIdTipoComprobante = 1;
                    //consultarFacturaNotaEntrega(1);
                    //iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                    //consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);

                    if (Program.iTipoComprobantePorDefaultComanda == 1)
                    {
                        iIdTipoComprobante = 1;
                        consultarFacturaNotaEntrega(1);
                        btnSeleccionFactura.BackColor = Color.Red;
                        btnSeleccionFactura.ForeColor = Color.White;
                        btnSeleccionNotaEntrega.BackColor = Color.FromArgb(255, 255, 192);
                        btnSeleccionNotaEntrega.ForeColor = Color.Black;
                    }

                    else if (Program.iTipoComprobantePorDefaultComanda == Program.iComprobanteNotaEntrega)
                    {
                        consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);
                        iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                        btnSeleccionFactura.BackColor = Color.FromArgb(255, 255, 192);
                        btnSeleccionFactura.ForeColor = Color.Black;
                        btnSeleccionNotaEntrega.BackColor = Color.Red;
                        btnSeleccionNotaEntrega.ForeColor = Color.White;
                    }
                }
            }

            else
            {
                consultarDatosOrden();
            }

            cargarCategorias(2, 0);

            if (iBanderaExpressTarjeta == 1)
            {
                btnAceptar.Visible = false;
                btnTarjetas.Visible = false;
                btnCobroTarjetaAlmuerzo.Visible = true;
                this.Text = "COMANDA PARA TARJETA DE ALMUERZOS";
            }

            else
            {
                btnAceptar.Visible = true;
                btnTarjetas.Visible = true;
                btnCobroTarjetaAlmuerzo.Visible = false;
                this.Text = "COMANDA PARA VENTA EXPRESS";
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
                dbSubtotalRecalcular = dbCantidadRecalcular * dbPrecioRecalcular;
                dgvPedido.Rows[iFila].Cells["subtotal"].Value = dbSubtotalRecalcular.ToString("N2");
                dbValorIVA = dbSubtotalRecalcular * Convert.ToDecimal(Program.iva);
                dbValorTotalRecalcular = dbSubtotalRecalcular + dbValorIVA;
                dgvPedido.Rows[iFila].Cells["valor"].Value = dbValorTotalRecalcular.ToString("N2");
                calcularTotales();
                sumar.Close();
                dgvPedido.ClearSelection();
            }
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

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotonesCategorias();
        }

        private void btnAnteriorProducto_Click(object sender, EventArgs e)
        {
            iCuentaProductos -= iCuentaAyudaProductos;

            if (iCuentaProductos <= 25)
            {
                btnAnteriorProducto.Enabled = false;
            }

            btnSiguienteProducto.Enabled = true;
            iCuentaProductos -= 25;
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems ingresados para crear la comanda";
                ok.ShowDialog();
            }

            else
            {
                Efectivo efectivo = new Efectivo("0", dTotalDebido.ToString("N2"), "", "EFECTIVO", "EF");
                efectivo.ShowDialog();

                if (efectivo.DialogResult == DialogResult.OK)
                {
                    dbValorGrid = efectivo.dbValorGrid;
                    dbValorRecuperado = efectivo.dbValorIngresado;
                    dbCambio = dbValorRecuperado - dbValorGrid;
                    efectivo.Close();

                    if (obtenerDatosFormaPagoRealizada(0, "EF") == false)
                        return;

                    iConciliacion = 0;
                    iOperadorTarjeta = 0;
                    iTipoTarjeta = 0;
                    sNumeroLote = "";
                    iBanderaInsertarLote = 0;
                    iBanderaEfectivoTarjeta = 0;
                    crearComandaNueva_V2();
                }
            }
        }

        private void frmComandaComidaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

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

        private void btnTarjetas_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems ingresados para crear la comanda";
                ok.ShowDialog();
            }

            else
            {
                DataTable dtItems = new DataTable();
                dtItems.Columns.Add("cantidad");
                dtItems.Columns.Add("valor_item");
                dtItems.Columns.Add("valor_recargo");
                dtItems.Columns.Add("valor_iva");
                dtItems.Columns.Add("total");
                dtItems.Columns.Add("id_producto");
                dtItems.Columns.Add("paga_iva");
                dtItems.Columns.Add("paga_servicio");
                dtItems.Columns.Add("valor_servicio");
                dtItems.Columns.Add("nombre_producto");
                dtItems.Columns.Add("codigo_producto");

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    DataRow row = dtItems.NewRow();
                    row["cantidad"] = dgvPedido.Rows[i].Cells[0].Value.ToString();
                    row["valor_item"] = dgvPedido.Rows[i].Cells[2].Value.ToString();
                    row["valor_recargo"] = "0";
                    row["valor_iva"] = "0";
                    row["total"] = "0";
                    row["id_producto"] = dgvPedido.Rows[i].Cells[6].Value.ToString();
                    row["paga_iva"] = dgvPedido.Rows[i].Cells[5].Value.ToString();
                    row["paga_servicio"] = dgvPedido.Rows[i].Cells["paga_servicio"].Value.ToString();
                    row["valor_servicio"] = "0";
                    row["nombre_producto"] = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    row["codigo_producto"] = dgvPedido.Rows[i].Cells["tipoProducto"].Value.ToString();
                    
                    dtItems.Rows.Add(row);
                }


                Comida_Rapida.frmCobroRapidoTarjetas cobro = new Comida_Rapida.frmCobroRapidoTarjetas(dTotalDebido, dtItems);
                cobro.ShowDialog();

                if (cobro.DialogResult == DialogResult.OK)
                {
                    iBanderaEfectivoTarjeta = 1;
                    iBanderaAplicaRecargo = cobro.iBanderaRecargo;
                    dtRecargos = new DataTable();
                    dtRecargos = cobro.dtValores;
                    iIdTipoFormaCobro = cobro.iIdFormaPago;

                    dbPropina = cobro.dbValorPropina;
                    sNumeroLote = cobro.sNumeroLote;
                    iConciliacion = cobro.iConciliacion;
                    iOperadorTarjeta = cobro.iOperadorTarjeta;
                    iTipoTarjeta = cobro.iTipoTarjeta;
                    iBanderaInsertarLote = cobro.iBanderaInsertarLote;
                    iConciliacion = 1;
                    
                    if (iBanderaAplicaRecargo == 1)
                    {
                        dTotalDebido = cobro.dbPagar;
                        lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
                    }

                    dbValorRecuperado = dTotalDebido;
                    dbCambio = 0;
                    cobro.Close();

                    if (obtenerDatosFormaPagoRealizada(iIdTipoFormaCobro, "") == false)
                        return;

                    crearComandaNueva_V2();
                }
            }
        }

        private void btnCobroTarjetaAlmuerzo_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems ingresados para crear la comanda";
                ok.ShowDialog();
            }

            else
            {
                //crearComanda();
            }
        }

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            cargarDatosConsumidorFinal();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                    {
                        //INSTRUCCIONES PARA VALIDAR
                        validarIdentificacion();
                    }
                    else
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                    }
                }
            }
        }

        private void btnSeleccionFactura_Click(object sender, EventArgs e)
        {
            consultarFacturaNotaEntrega(1);
            iIdTipoComprobante = 1;
            btnSeleccionFactura.BackColor = Color.Red;
            btnSeleccionFactura.ForeColor = Color.White;
            btnSeleccionNotaEntrega.BackColor = Color.FromArgb(255, 255, 192);
            btnSeleccionNotaEntrega.ForeColor = Color.Black;
        }

        private void btnSeleccionNotaEntrega_Click(object sender, EventArgs e)
        {
            consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);
            iIdTipoComprobante = Program.iComprobanteNotaEntrega;
            btnSeleccionFactura.BackColor = Color.FromArgb(255, 255, 192);
            btnSeleccionFactura.ForeColor = Color.Black;
            btnSeleccionNotaEntrega.BackColor = Color.Red;
            btnSeleccionNotaEntrega.ForeColor = Color.White;
        }

        private void btnEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Facturador.frmNuevoCliente nuevoCliente = new Facturador.frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
            nuevoCliente.ShowDialog();

            if (nuevoCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = nuevoCliente.iCodigo;
                txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                consultarRegistro();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Facturador.frmControlDatosCliente controlDatosCliente = new Facturador.frmControlDatosCliente();
            controlDatosCliente.ShowDialog();

            if (controlDatosCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = controlDatosCliente.iCodigo;
                txtIdentificacion.Text = controlDatosCliente.sIdentificacion;
                consultarRegistro();
                controlDatosCliente.Close();
            }
        }

        private void btnCorreoElectronicoDefault_Click(object sender, EventArgs e)
        {
            if (btnCorreoElectronicoDefault.AccessibleName == "0")
            {
                sCorreoAyuda = txtMail.Text.Trim();
                btnCorreoElectronicoDefault.AccessibleName = "1";
                txtMail.ReadOnly = false;
                txtMail.Focus();
            }

            else
            {
                txtMail.Text = sCorreoAyuda;
                btnCorreoElectronicoDefault.AccessibleName = "0";
                txtMail.ReadOnly = true;
                btnCorreoElectronicoDefault.Focus();
            }
        }

        private void btnImprimirPrecuenta_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems ingresados para imprimir la precuenta";
                ok.ShowDialog();
                return;
            }

            crearPrecuentaRapida();        
        }

        private void btnRegresar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnRegresar.Visible = false;
            lblProductos.Text = "PRODUCTOS";
            pnlProductos.Controls.Clear();
            iNivelGeneral = 3;
            cargarCategorias(2, 0);
        }
    }
}
