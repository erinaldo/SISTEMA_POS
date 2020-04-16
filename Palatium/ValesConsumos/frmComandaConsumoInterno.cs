using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ValesConsumos
{
    public partial class frmComandaConsumoInterno : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases_Crear_Comandas.ClaseCrearComanda comanda;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Button[,] boton = new Button[2, 4];
        Button[,] botonProductos = new Button[5, 5];

        SqlParameter[] parametro;
        
        string sSql;
        string sNombreProducto_P;
        string sNombreEmpleado;
        string sFecha;
        string sFechaConsulta;
        string sNombreSubReceta;
        string sDescripcionFormaPago;
        string sDireccionEmpleado;
        string sTelefonoEmpleado;
        string sMailEmpleado;
        string sCiudadEmpleado;
        string sEstablecimiento;
        string sPuntoEmision;
        string sNumeroComprobante;
        string sCodigoMetodoPago;

        bool bRespuesta;

        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;

        DataTable dtConsulta;
        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtItems;
        DataTable dtDetalleItems;
        DataTable dtPagos;

        int iPosXProductos;
        int iPosYProductos;
        int iCuentaAyudaProductos;
        int iCuentaCategorias;
        int iPosXCategorias;
        int iPosYCategorias;
        int iCuentaAyudaCategorias;
        int iCuentaProductos;
        int iIdPersona;
        int iIdOrigenOrden;
        int iIdPedido;
        int iNumeroPedidoOrden;
        int iIdProducto_P;
        int iIdTipoFormaCobro;
        int iIdFactura;
        int iPagaIva_P;
        int iPagaServicio_P;
        int iIdTipoComprobante;
        int iIdSriFormaPago_P;
        int iIdDocumentoPorCobrar;

        Decimal dTotalDebido;
        Decimal dbCantidadRecalcular;
        Decimal dbPrecioRecalcular;
        Decimal dbValorTotalRecalcular;

        public frmComandaConsumoInterno(int iIdPersona_P, string sNombreEmpleado_P, int iIdOrigenOrden_P)
        {
            this.iIdPersona = iIdPersona_P;
            this.sNombreEmpleado = sNombreEmpleado_P;
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES EL USUARIO

        //FUNCION PARA CONSULTAR DATOS DEL CLIENTE
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "SELECT isnull(TP.correo_electronico, '') correo_electronico," + Environment.NewLine;
                sSql += "isnull(TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion, '') direccion_cliente," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, isnull(TD.direccion, '') direccion" + Environment.NewLine;
                sSql += "FROM tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE TP.id_persona = " + iIdPersona;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch(); ;
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sDireccionEmpleado = dtConsulta.Rows[0]["direccion_cliente"].ToString().Trim().ToUpper();
                sTelefonoEmpleado = dtConsulta.Rows[0]["telefono_domicilio"].ToString().Trim().ToUpper();
                sMailEmpleado = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim().ToUpper();
                sCiudadEmpleado = dtConsulta.Rows[0]["direccion"].ToString().Trim().ToUpper();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra ítems de categorías en el sistema.";
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                sSql += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, PP.valor, CP.codigo, P.paga_servicio" + Environment.NewLine;
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra ítems de categorías en el sistema.";
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
            Decimal dbValorIva;
            Decimal dbValorServicio;
            Decimal dbSumaIva = 0;
            Decimal dbSumaServicio = 0;
            dTotalDebido = 0;

            for (int i = 0; i < dgvPedido.Rows.Count; ++i)
            {
                iPagaIva = Convert.ToInt32(dgvPedido.Rows[i].Cells["pagaIva"].Value);
                iPagaServicio = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_servicio"].Value);

                if (dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString() == "0")
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
            }

            dTotalDebido = dSubtotalConIva + dSubtotalCero + dbSumaIva + dbSumaServicio;
            lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
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

                if (insertarFactura_V2(0) == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ReportesTextBox.frmVerPrecuentaEmpresaTextBox precuenta = new ReportesTextBox.frmVerPrecuentaEmpresaTextBox(iIdPedido.ToString(), 1, 2, 0, 1);
                precuenta.ShowDialog();

                Cambiocs ok_1 = new Cambiocs("$ 0.00");
                ok_1.lblVerMensaje.Text = "CONSUMO INTERNO GENERADO";
                ok_1.ShowDialog();
                this.Close();

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

                bRespuesta = comanda.insertarPagos(iIdPedido, dtPagos, dTotalDebido, 0, 0,
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

                dtPagos.Rows.Add(iIdTipoFormaCobro, sDescripcionFormaPago, dTotalDebido, iIdSriFormaPago_P, 0,
                                 0, 0, "", 0, 0, sCodigoMetodoPago, "", "", "0", "" , "");

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
        private bool obtenerDatosFormaPagoRealizada(string sCodigoFormaPago_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_obtener_datos_formas_pagos" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and codigo_forma_cobro = @codigo_forma_cobro";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo_forma_cobro";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = sCodigoFormaPago_P;

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

        #endregion

        private void frmComandaConsumoInterno_Load(object sender, EventArgs e)
        {
            iIdTipoComprobante = Program.iComprobanteNotaEntrega;
            lblEmpleado.Text = sNombreEmpleado;
            consultarRegistro();
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
                    ok.LblMensaje.Text = "No se ha seleccionado una línea para remover.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
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
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems para generar la comanda.";
                ok.ShowDialog();
            }

            else
            {
                if (obtenerDatosFormaPagoRealizada("22") == false)
                    return;

                crearComandaNueva_V2();
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

        private void frmComandaConsumoInterno_KeyDown(object sender, KeyEventArgs e)
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
