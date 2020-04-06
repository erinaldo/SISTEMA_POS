using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Bar
{
    public partial class frmComandaBar : Form, Palatium.Inicio.IForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Oficina.frmNuevoMenuConfiguracion menuOficina;
        Bar.frmCobrarCuenta cobrar;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();

        string sSql;
        string sNombreProducto_P;
        string sCiudad;
        string sCorreoAyuda;
        string sFecha;
        string sMesCorto;
        string sCodigo;
        string sAnioCorto;
        string sNumeroMovimientoSecuencial;
        string sAnio;
        string sMes;
        string sTabla;
        string sCampo;
        string sNombreSubReceta;
        string sCodigoClaseProducto;
        string sDescripcionFormaPago;
        string sClaveAcceso;
        string sSecuencial;
        string sMovimiento;
        string sEstablecimiento;
        string sPuntoEmision;

        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtCategorias;
        DataTable dtProductos;
        DataTable dtLocalidad;
        DataTable dtReceta;
        DataTable dtSubReceta;

        Button[] botonFamilias = new Button[8];
        Button[,] botonProductos = new Button[6, 3];

        Button botonSeleccionadoCategoria;
        Button botonSeleccionadoProducto;
        Button botonMemoria;

        int iCuentaCategorias;
        int iPosYCategorias;
        int iCuentaAyudaCategorias;
        int iCuentaProductos;
        int iPosXProductos;
        int iPosYProductos;
        int iCuentaAyudaProductos;
        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iIdTipoAmbiente;
        int iIdTipoEmision;
        int iIdTipoComprobante;
        int iCuentaDiaria;
        int iIdLocalidadBodega;
        int iIdBodegaInsumos;
        int iValorActualMovimiento;
        int iNumeroPedidoOrden;
        int iIdPedido;
        int iIdCabeceraMovimiento;
        int iIdProducto_P;
        int iIdPosReceta;
        int iIdPosSubReceta;
        int iIdOrigenOrden;
        int iIdCabDespachos;
        int iIdDespachoPedido;
        int iIdEventoCobro;
        int iCgTipoDocumento = 2725;
        int iIdDocumentoCobrar;
        int iIdPago;
        int iNumeroPago;
        int iIdTipoFormaCobro;
        int iCgTipoDocumentoCobro;
        int iIdFormaPago_1;
        int iIdFactura;
        int iCgEstadoDctoPorCobrar = 7461;
        int iIdCaja;
        int iIdDocumentoPago;
        int iIdPosMovimientoCaja;
        int iNumeroMovimientoCaja;
        int iTercerDigito;
        int iPagaIva_P;
        int iPagaServicio_P;

        Decimal dTotalDebido;
        Decimal dbValorGrid;
        Decimal dbValorRecuperado;
        Decimal dbCambio;
        Decimal dIVA_P;
        Decimal dServicio_P;
        Decimal dPrecioUnitario_P;
        Decimal dCantidad_P;
        Decimal dbCantidadRecalcular;
        Decimal dbPrecioRecalcular;
        Decimal dbValorTotalRecalcular;
        Decimal dbSubtotalRecalcular;
        Decimal dbValorIVA;

        long iMaximo;

        public frmComandaBar()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        public void mostrarOcultar(int iOp)
        {
            if (iOp == 3)
            {
                cobrar .Close();
                this.Show();
            }
        }

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
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        //FUNCION MENSAJE DE VALIDACION DE CEDULA
        private void mensajeValidarCedula()
        {
            ok = new VentanasMensajes.frmMensajeNuevoOk();
            ok.lblMensaje.Text = "El número de identificación ingresado es incorrecto.";
            ok.ShowDialog();
            txtIdentificacion.Clear();
            txtRazonSocial.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            txtIdentificacion.Focus();
        }

        //FUNCION PARA CARGAR EL ID ORIGEN ORDEN
        private int cargarOrigenOrden()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and codigo = '10'";

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
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra configurado los parámetros de origen de la comanda.";
                    ok.ShowDialog();
                    return 0;
                }

                return Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
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
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
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

        //FUNCION PARA CREAR LOS BOTONES DE CATEGORIAS
        private bool crearBotonesCategorias()
        {
            try
            {
                pnlCategorias.Controls.Clear();

                iPosYCategorias = 0;
                iCuentaAyudaCategorias = 0;

                for (int i = 0; i < 8; i++)
                {
                    botonFamilias[i] = new Button();
                    botonFamilias[i].Name = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                    botonFamilias[i].Cursor = Cursors.Hand;
                    botonFamilias[i].Click += new EventHandler(boton_clic_categorias);
                    botonFamilias[i].Size = new Size(250, 60);
                    botonFamilias[i].FlatAppearance.BorderSize = 0;
                    botonFamilias[i].FlatAppearance.MouseOverBackColor = Color.Magenta; 
                    botonFamilias[i].FlatStyle = FlatStyle.Flat;
                    botonFamilias[i].Font = new Font("Century Gothic", 11.25F, FontStyle.Regular);
                    botonFamilias[i].BackColor = Color.FromArgb(45, 45, 48);
                    botonFamilias[i].ForeColor = Color.White;
                    botonFamilias[i].Image = Properties.Resources.producto;
                    botonFamilias[i].ImageAlign = ContentAlignment.MiddleLeft;
                    botonFamilias[i].Location = new Point(0, iPosYCategorias);
                    botonFamilias[i].Tag = dtCategorias.Rows[iCuentaCategorias]["id_producto"].ToString();
                    botonFamilias[i].Text = dtCategorias.Rows[iCuentaCategorias]["nombre"].ToString();
                    botonFamilias[i].AccessibleDescription = dtCategorias.Rows[iCuentaCategorias]["subcategoria"].ToString();
                    pnlCategorias.Controls.Add(botonFamilias[i]);

                    iCuentaCategorias++;
                    iCuentaAyudaCategorias++;
                    iPosYCategorias += 60;                    

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

        //EVENTO CLIC DEL BOTON CATEGORIAS
        private void boton_clic_categorias(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                botonSeleccionadoCategoria = sender as Button;

                if (botonMemoria != null)
                {
                    botonMemoria.BackColor = Color.FromArgb(45, 45, 48);
                }

                botonSeleccionadoCategoria.BackColor = Color.DodgerBlue;
                botonMemoria = botonSeleccionadoCategoria;

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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    if (dtProductos.Rows.Count > 18)
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
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
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

        //FUNCION PARA CREAR LOS BOTONES DE PRODUCTOS
        private bool crearBotonesProductos()
        {
            try
            {
                pnlProductos.Controls.Clear();

                iPosXProductos = 0;
                iPosYProductos = 0;
                iCuentaAyudaProductos = 0;

                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        botonProductos[i, j] = new Button();
                        botonProductos[i, j].Cursor = Cursors.Hand;
                        botonProductos[i, j].Click += new EventHandler(boton_clic_productos);
                        botonProductos[i, j].Size = new Size(190, 80);
                        botonProductos[i, j].FlatAppearance.BorderSize = 2;
                        botonProductos[i, j].FlatAppearance.MouseOverBackColor = Color.DodgerBlue; 
                        botonProductos[i, j].FlatStyle = FlatStyle.Flat;
                        botonProductos[i, j].Font = new Font("Century Gothic", 11.25F, FontStyle.Regular);
                        botonProductos[i, j].BackColor = Color.FromArgb(45, 45, 48);
                        botonProductos[i, j].ForeColor = Color.White;
                        botonProductos[i, j].Location = new Point(iPosXProductos, iPosYProductos);
                        botonProductos[i, j].Name = dtProductos.Rows[iCuentaProductos]["id_producto"].ToString();
                        botonProductos[i, j].Text = dtProductos.Rows[iCuentaProductos]["nombre"].ToString();
                        botonProductos[i, j].Tag = dtProductos.Rows[iCuentaProductos]["paga_iva"].ToString();
                        botonProductos[i, j].AccessibleDescription = dtProductos.Rows[iCuentaProductos]["codigo"].ToString();
                        botonProductos[i, j].AccessibleName = dtProductos.Rows[iCuentaProductos]["valor"].ToString();
                        pnlProductos.Controls.Add(botonProductos[i, j]);

                        iCuentaProductos++;
                        iCuentaAyudaProductos++;

                        if (j + 1 == 3)
                        {
                            iPosXProductos = 0;
                            iPosYProductos += 80;
                        }

                        else
                        {
                            iPosXProductos += 190;
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
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Se encontró un error al buscar el parámetro de servicio en el producto.";
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
                    dgvPedido.Rows[i].Cells["pagaIva"].Value = iPagaIva_R;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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

                if (iPagaIva == 1)
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

            //dTotalDebido = num1 + num2 - num3 - num4 + (num1 - num3) * Convert.ToDecimal(Program.iva) + num7;
            dTotalDebido = dSubtotalConIva + dSubtotalCero + dbSumaIva + dbSumaServicio;
            lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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

        //FUNCION PARA CARGAR LA CONFIGURACION DE LA FACTURACION ELECTRONICA
        private void configuracionFacturacion()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_ambiente, id_tipo_emision" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
                    iIdTipoEmision = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_emision"].ToString());
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        //FUNCION PARA CANCELAR TODO 
        private void cancelarTodo()
        {
            pnlProductos.Controls.Clear();
            dgvPedido.Rows.Clear();
            lblTotal.Text = "0.00";

            cargarDatosConsumidorFinal();
            cargarCategorias();

            consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);

            btnAnterior.Enabled = false;
            btnAnteriorProducto.Enabled = false;

            btnAnteriorProducto.Visible = false;
            btnSiguienteProducto.Visible = false;

            iCuentaCategorias = 0;

            cargarCategorias();
        }

        #endregion

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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

                    sSql = "";
                    sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_R + ", " + iIdCabeceraMovimientoSubReceta + ", 546," + (dbCantidadMateriaPrima_R * dbCantidadPedida_P * -1) + ", 'A')";

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

        #region FUNCIONES PARA INSERTAR EN LA BASE DE DATOS

        //FUNCION PARA EXTRAER EL NUMERO DE CUENTA
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //SECUENCIA DE INSERCION DE DATOS Y CERRAR 
        private void crearComanda()
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                extraerNumeroCuenta();

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    return;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                if (insertarPedido() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                if (insertarPagos() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                if (insertarFactura() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                crearReporte();

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //INSERTAR LA PRIMERA FASE - PEDIDO
        private bool insertarPedido()
        {
            try
            {
                string sEstadoOrden = "Pagada";

                //INSERTAR EN LA TABLA CV403_CAB_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                sSql += "'" + sFecha + "', " + iIdPersona + ",8032," + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + "," + Program.iIdVendedor + ",6967, 0, 7471," + Environment.NewLine;
                sSql += "GETDATE(),'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + iCuentaDiaria + ", 0, ";
                sSql += Program.iIdCajeroDefault + "," + iIdOrigenOrden + ", 0," + Program.iJORNADA + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', GETDATE(), GETDATE(), '" + sEstadoOrden + "'," + Environment.NewLine;
                sSql += "1, 1, 1, 0, 0, 'A', 1, null, null," + Environment.NewLine;
                sSql += Program.iIdMesero + ", " + Program.iIdPosTerminal + ", " + (Program.servicio * 100) + ", 0, " + Program.iIdPosCierreCajero + ")";

                Program.iBanderaCliente = 0;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_CAB_DESPACHOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_cab_despachos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, cg_empresa, id_localidad, fecha_despacho," + Environment.NewLine;
                sSql += "cg_motivo_despacho, id_destinatario, punto_partida, cg_ciudad_entrega," + Environment.NewLine;
                sSql += "direccion_entrega, id_transportador, fecha_inicio_transporte," + Environment.NewLine;
                sSql += "fecha_fin_transporte, cg_estado_despacho, punto_venta, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iCgMotivoDespacho + ", " + iIdPersona + "," + Environment.NewLine;
                sSql += "'" + Program.sPuntoPartida + "', " + Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "'," + Environment.NewLine;
                sSql += "'" + Program.iIdPersona + "', '" + sFecha + "', '" + sFecha + "', " + Program.iCgEstadoDespacho + "," + Environment.NewLine;
                sSql += "1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA CV403_CAB_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

                //CONSULTAR EL NUMERO DE PEDIDO PARA INSERTAR EN LA TABLA CV403_NUMERO_CAB_PEDIDO
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iNumeroPedidoOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                //ACTUALIZAR EL NUMERO DE PEDIDO EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_NUMERO_CAB_PEDIDO
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdTipoComprobante + ", " + iIdPedido + ", " + iNumeroPedidoOrden + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

                //INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA CV403_DESPACHOS_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

                //INSERTAR EN LA TABLA CV403_EVENTOS_COBROS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdPersona + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "7466, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

                //INSERTAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda," + Environment.NewLine;
                sSql += "valor, cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + dTotalDebido + "," + Environment.NewLine;
                sSql += "7460, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //CICLO PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    iPagaIva_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["pagaIva"].Value);
                    sCodigoClaseProducto = dgvPedido.Rows[i].Cells["tipoProducto"].Value.ToString();
                    sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    iPagaServicio_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["paga_servicio"].Value);

                    if (iPagaIva_P == 1)
                        dIVA_P = dPrecioUnitario_P * Convert.ToDecimal(Program.iva);
                    else
                        dIVA_P = 0;

                    if (iPagaServicio_P == 1)
                        dServicio_P = dPrecioUnitario_P * Convert.ToDecimal(Program.servicio);
                    else
                        dServicio_P = 0;

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
                    sSql += dCantidad_P + ", 0, 0, " + dIVA_P + ", " + dServicio_P + ", " + Environment.NewLine;
                    sSql += "null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 0, 1, null, 'A', 0, 0, " + iIdPersona + ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        return false;
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
                                return false;
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
                                return false;
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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //INSERTAR LA SEGUNDA FASE - PAGOS
        private bool insertarPagos()
        {
            try
            {
                //SELECCIONAR EL ID_DOCUMENTO_COBRAR
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
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
                    return false;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());

                //INSERTAR EN LA TABLA CV403_PAGOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += dTotalDebido + ", 0, " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 0, 0, " + dbCambio + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA CV403_PAGOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPago = Convert.ToInt32(iMaximo);

                //SELECCIONAR EL NUMERO PAGO
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                //INSERTAR EN LA TABLA CV403_NUMEROS_PAGOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR EL SECUENCIA NUMERO PAGO EN TP_LOCALIDADES_IMPRESORAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DEL EFECTIVO Y CG_TIPO_DOCUMENTO
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select FC.id_pos_tipo_forma_cobro, FC.cg_tipo_documento, FPA.id_forma_pago," + Environment.NewLine;
                sSql += "MP.descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                sSql += "and MP.estado = 'A'" + Environment.NewLine;
                sSql += "and FC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "sri_forma_pago FP ON FP.id_sri_forma_pago = MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and FP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_formas_pagos FPA ON FP.id_sri_forma_pago = FPA.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and FPA.estado = 'A'" + Environment.NewLine;
                sSql += "where FPA.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and MP.codigo = 'EF'";

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

                iIdTipoFormaCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_tipo_forma_cobro"].ToString());
                iCgTipoDocumentoCobro = Convert.ToInt32(dtConsulta.Rows[0]["cg_tipo_documento"].ToString());
                iIdFormaPago_1 = Convert.ToInt32(dtConsulta.Rows[0]["id_forma_pago"].ToString());
                sDescripcionFormaPago = dtConsulta.Rows[0]["descripcion"].ToString();

                //INSERTAR EN LA TABLA CV403_DOCUMENTOS_PAGOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", " + iCgTipoDocumentoCobro + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                sSql += Program.iMoneda + ", 1, " + dTotalDebido + ", " + iIdTipoFormaCobro + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, " + dbValorRecuperado + "," + Environment.NewLine;
                sSql += "null, null, null)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DEL DOCUMENTO PAGO
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "cv403_documentos_pagos";
                sCampo = "id_documento_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                else
                {
                    iIdDocumentoPago = Convert.ToInt32(iMaximo);
                }

                //INSERTAR EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotalDebido + ", 'A', 1, 0," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //INSERTAR LA TERCERA FASE - FACTURA
        private bool insertarFactura()
        {
            try
            {
                int iFacturaElectronica_P = 0;

                if (iIdTipoComprobante == 1)
                {
                    if (Program.iFacturacionElectronica == 1)
                    {
                        iFacturaElectronica_P = 1;
                        sClaveAcceso = sGenerarClaveAcceso();
                    }
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, id_forma_pago2, id_forma_pago3," + Environment.NewLine;
                sSql += "fecha_factura, fecha_vcto, cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, " + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio," + Environment.NewLine;
                sSql += "facturaelectronica, id_tipo_emision, id_tipo_ambiente, clave_acceso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdTipoComprobante + "," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago_1 + ", " + Environment.NewLine;
                sSql += "null, null, '" + sFecha + "', '" + sFecha + "', " + Program.iMoneda + ", " + dTotalDebido + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0," + Environment.NewLine;
                //sSql += "'QUITO', '2999999', 'QUITO', '" + Program.sCorreoElectronicoDefault + "', 0, 0, 0, 0)";
                sSql += "'" + txtDireccion.Text.Trim().ToUpper() + "', '" + txtTelefono.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'QUITO', '" + txtMail.Text.Trim().ToLower() + "', 0, " + iFacturaElectronica_P + ", " + iIdTipoEmision + ", " + iIdTipoAmbiente + ", " + Environment.NewLine;

                if (iFacturaElectronica_P == 1)
                {
                    sSql += "'" + sClaveAcceso + "')";
                }

                else
                {
                    sSql += "null)";
                }

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA FACTURAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "cv403_facturas";
                sCampo = "id_factura";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdFactura = Convert.ToInt32(iMaximo);

                //INSERTAR EN LA TABLA CV403_NUMEROS_FACTURAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR EL NUMERO DE NOTA DE ENTREGA EN TP_LOCALIDADES
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;

                if (iIdTipoComprobante == 1)
                {
                    sSql += "numero_factura = numero_factura + 1" + Environment.NewLine;
                }

                else
                {
                    sSql += "numeronotaentrega = numeronotaentrega + 1" + Environment.NewLine;
                }

                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdPedido + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR LA TABLA CV403_DCTOS_POR_COBRAR
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrar + "," + Environment.NewLine;
                sSql += "numero_documento = " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //SELECCIONAR EL ID DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select id_caja" + Environment.NewLine;
                sSql += "from cv405_cajas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and cg_tipo_caja = 8906";

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

                iIdCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_caja"].ToString());

                //VARIABLES PARA INSERTAR LOS MOVIMIENTOS DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSecuencial = TxtNumeroFactura.Text.Trim().PadLeft(9, '0');

                if (iIdTipoComprobante == 1)
                {
                    sMovimiento = ("FACT. No. " + txtEstablecimiento.Text.Trim() + "-" + sSecuencial).Trim();
                }

                else
                {
                    sMovimiento = ("N. ENTREGA. No. " + txtEstablecimiento.Text.Trim() + "-" + sSecuencial).Trim();
                }

                //INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                sSql += "tipo_movimiento, idempresa, id_localidad, id_persona, id_cliente," + Environment.NewLine;
                sSql += "id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto," + Environment.NewLine;
                sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + "," + Program.iIdPersonaMovimiento + ", ";
                sSql += +iIdPersona + ", " + iIdCaja + ", 1," + Environment.NewLine;
                sSql += "'" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + dTotalDebido + "," + Environment.NewLine;
                sSql += "'COBRO No. CUENTA " + iNumeroPedidoOrden.ToString() + " (" + sDescripcionFormaPago + ")'," + Environment.NewLine;
                sSql += "'" + sMovimiento.Trim() + "', " + iIdDocumentoPago + ", " + Program.iJORNADA + "," + Environment.NewLine;
                sSql += Program.iIdPosCierreCajero + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //EXTRAER EL ID DEL MOVIMIENTO DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "pos_movimiento_caja";
                sCampo = "id_pos_movimiento_caja";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);

                //CONSULTAR EL NUMERO DE MOVIMIENTO DE CAJA EN TP_LOCALIDADES_IMPRESORAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                //INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimientoCaja + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR EL NUMERO DE MOVIMIENTO DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = numeromovimientocaja + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //FUNCION PARA CREAR EL REPORTE
        private void crearReporte()
        {
            try
            {
                if (Program.iEjecutarImpresion == 1)
                {
                    if (iIdTipoComprobante == 1)
                    {
                        ReportesTextBox.frmVistaFactura frmVistaFactura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 1);
                        frmVistaFactura.ShowDialog();

                        if (frmVistaFactura.DialogResult == DialogResult.OK)
                        {
                            frmVistaFactura.Close();

                            ReportesTextBox.frmVerPedidoExpress precuenta = new ReportesTextBox.frmVerPedidoExpress(iIdPedido.ToString(), 1);
                            precuenta.ShowDialog();

                            Cambiocs cambiocs = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            cambiocs.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                            cambiocs.ShowDialog();
                            cancelarTodo();
                        }
                    }

                    else
                    {
                        ReportesTextBox.frmVerNotaVentaFactura notaVenta = new ReportesTextBox.frmVerNotaVentaFactura(iIdPedido.ToString(), 1);
                        notaVenta.ShowDialog();

                        if (notaVenta.DialogResult == DialogResult.OK)
                        {
                            Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                            ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                            ok.ShowDialog();
                            notaVenta.Close();
                            cancelarTodo();
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
                        cancelarTodo();
                    }

                    else
                    {
                        Cambiocs ok = new Cambiocs("$ " + dbCambio.ToString("N2"));
                        ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                        ok.ShowDialog();
                        cancelarTodo();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        //FUNCION PARA CREAR LA CLAVE DE ACCESO
        private string sGenerarClaveAcceso()
        {
            //GENERAR CLAVE DE ACCESO
            string sClaveAcceso_R = "";
            string sFecha_R = Convert.ToDateTime(sFecha).ToString("ddMMyyyy");
            string TipoComprobante = "01";
            string NumeroRuc = Program.sNumeroRucEmisor;
            //string TipoAmbiente = Program.iTipoAmbiente.ToString();
            //string TipoEmision = Program.iTipoEmision.ToString();
            string TipoAmbiente = iIdTipoAmbiente.ToString();
            string TipoEmision = iIdTipoEmision.ToString();
            string Serie = sEstablecimiento + sPuntoEmision;
            string NumeroComprobante = TxtNumeroFactura.Text.Trim().PadLeft(9, '0');
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
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se encuentran registros en la consulta.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        txtEstablecimiento.Text = dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString();

                        sEstablecimiento = dtConsulta.Rows[0]["establecimiento"].ToString();
                        sPuntoEmision = dtConsulta.Rows[0]["punto_emision"].ToString();

                        TxtNumeroFactura.Text = dtConsulta.Rows[0]["numeronotaentrega"].ToString();
                    }
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
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

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Application.Exit();

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea cerrar la aplicación?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void frmComandaBar_Load(object sender, EventArgs e)
        {
            iIdOrigenOrden = cargarOrigenOrden();

            if (iIdOrigenOrden == -1)
            {
                Application.Exit();
            }

            if (iIdOrigenOrden == 0)
            {
                Application.Exit();
            }

            cargarDatosConsumidorFinal();
            cargarCategorias();

            consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);

            if (Program.iFacturacionElectronica == 1)
            {
                configuracionFacturacion();
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

        private void btnSiguienteProducto_Click(object sender, EventArgs e)
        {
            btnAnteriorProducto.Enabled = true;
            crearBotonesProductos();
        }

        private void btnAnteriorProducto_Click(object sender, EventArgs e)
        {
            iCuentaProductos -= iCuentaAyudaProductos;

            if (iCuentaProductos <= 18)
            {
                btnAnteriorProducto.Enabled = false;
            }

            btnSiguienteProducto.Enabled = true;
            iCuentaProductos -= 18;
            crearBotonesProductos();
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Inicio.IForm frmInterface = this.Owner as Inicio.IForm;

            if (frmInterface != null)
            {
                frmInterface.mostrarOcultar(1);
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
                }

                else if (dgvPedido.SelectedRows.Count > 0)
                {
                    SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                    SiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        dgvPedido.Rows.Remove(dgvPedido.CurrentRow);
                        calcularTotales();
                        SiNo.Close();
                        dgvPedido.ClearSelection();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se ha seleccionado una línea para remover.";
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

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            cargarDatosConsumidorFinal();
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

        private void btnTarjetas_Click(object sender, EventArgs e)
        {
            cancelarTodo();
        }

        private void btnFacturaNotaEntrega_Click(object sender, EventArgs e)
        {
            if (btnFacturaNotaEntrega.AccessibleDescription == "0")
            {
                iIdTipoComprobante = 1;
                btnFacturaNotaEntrega.Text = "Factura";
                btnFacturaNotaEntrega.AccessibleDescription = "1";
                consultarFacturaNotaEntrega(1);
            }

            else if (btnFacturaNotaEntrega.AccessibleDescription == "1")
            {
                iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                btnFacturaNotaEntrega.Text = "Nota Entrega";
                btnFacturaNotaEntrega.AccessibleDescription = "0";
                consultarFacturaNotaEntrega(Program.iComprobanteNotaEntrega);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay ítems ingresados para crear la comanda";
                ok.ShowDialog();
                return;
            }

            cobrar = new frmCobrarCuenta(dTotalDebido);
            this.Hide();
            cobrar.Show(this);

            //Efectivo efectivo = new Efectivo("0", dTotalDebido.ToString("N2"), "", "EFECTIVO", "EF");
            //efectivo.ShowDialog();

            //if (efectivo.DialogResult == DialogResult.OK)
            //{
            //    dbValorGrid = efectivo.dbValorGrid;
            //    dbValorRecuperado = efectivo.dbValorIngresado;
            //    dbCambio = dbValorRecuperado - dbValorGrid;
            //    efectivo.Close();
            //    crearComanda();
            //}
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

        private void iconConfigurar_Click(object sender, EventArgs e)
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";

            Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
            acceso.ShowDialog();

            if (acceso.DialogResult == DialogResult.OK)
            {
                acceso.Close();
                menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                menuOficina.ShowInTaskbar = false;
                menuOficina.ShowDialog();
            }

            else
            {
                return;
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
    }
}
