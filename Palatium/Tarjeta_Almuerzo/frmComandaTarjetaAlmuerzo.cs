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

namespace Palatium.Tarjeta_Almuerzo
{
    public partial class frmComandaTarjetaAlmuerzo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        Clases_Crear_Comandas.ClaseCrearComanda comanda;

        string sSql;
        string sFecha;
        string sEstablecimiento;
        string sPuntoEmision;
        string sDescripcionFormaPago;
        string sCiudad;
        string sTelefono;
        string sDireccion;
        string sMail;
        string sNumeroComprobante;
        string sNombreProducto;
        string sCodigoProducto;
        string sCodigoMetodoPago;

        ToolTip ttMensaje;

        DataTable dtConsulta;
        DataTable dtLocalidad;
        DataTable dtItems;
        DataTable dtDetalleItems;
        DataTable dtPagos;

        bool bRespuesta;

        int iIdPersona;
        int iPagaIva;
        int iPagaServicio;
        int iIdPosOrigenOrden;
        int iIdPosTarjeta;
        int iIdProductoTarjeta;
        int iIdProductoDescarga;
        int iIdTipoComprobante;
        int iIdListaMinorista;
        int iIdSriFormaPago_P;

        int iCantidadEmitir;
        int iCantidadTarjeta;
        int iCantidadVendidos;
        int iContador;
        int iContadorAyuda;
        int iPosXBoton;
        int iPosYBoton;
        int iIdPedido;
        int iNumeroPedidoOrden;
        int iIdFactura;
        int iIdTipoFormaCobro;
        int iNumeroNotaEntrega;
        int iNumeroTarjeta;
        int iIdDocumentoPorCobrar;

        Decimal dbPrecioUnitario;
        Decimal dbValorIva;
        Decimal dbValorServicio;
        Decimal dbTotalDebido;

        Button[,] botonDisponible;

        SqlParameter[] parametro;

        public frmComandaTarjetaAlmuerzo(int iIdPosOrigenOrden_P)
        {
            this.iIdPosOrigenOrden = iIdPosOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR DATOS DE LA TARJETA
        private void consultarTarjeta(int iNumeroTarjeta_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_tar_lista_tarjetas_almuerzo_emitidas" + Environment.NewLine;
                sSql += "where estado_tarjeta = @estado_tarjeta" + Environment.NewLine;
                sSql += "and numero_tarjeta = @numero_tarjeta";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_tarjeta";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "Vigente";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@numero_tarjeta";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iNumeroTarjeta_P;

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La tarjeta no se encuentra registrada o ya no está vigente.";
                    ok.ShowDialog();
                    txtNumeroTarjeta.Focus();
                    return;
                }

                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                iIdPosTarjeta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_tar_tarjeta"].ToString());
                iIdProductoTarjeta = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_tarjeta"].ToString());
                iIdProductoDescarga = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_descarga"].ToString());

                txtNombreCliente.Text = dtConsulta.Rows[0]["cliente"].ToString();
                txtCantidadDisponible.Text = dtConsulta.Rows[0]["disponibles"].ToString();
                iCantidadTarjeta = Convert.ToInt32(dtConsulta.Rows[0]["disponibles"].ToString());

                iContador = 0;
             
                if (iCantidadTarjeta > 30)
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

                crearBotones();
                obtenerValoresProducto();
                consultarRegistro();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL ARREGLO DE BOTONES
        private void crearBotones()
        {
            try
            {
                pnlBotones.Controls.Clear();
                iContadorAyuda = 0;
                iPosXBoton = 0;
                iPosYBoton = 0;

                botonDisponible = new Button[6, 5];

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        botonDisponible[i, j] = new Button();
                        botonDisponible[i, j].AccessibleDescription = "0";
                        botonDisponible[i, j].BackColor = Color.FromArgb(128, 128, 255);
                        botonDisponible[i, j].Click += boton_clic;
                        botonDisponible[i, j].Cursor = Cursors.Hand;
                        botonDisponible[i, j].FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 192);
                        botonDisponible[i, j].FlatStyle = FlatStyle.Flat;
                        botonDisponible[i, j].Font = new Font("Maiandra GD", 12F, FontStyle.Regular);
                        botonDisponible[i, j].Location = new Point(iPosXBoton, iPosYBoton);
                        botonDisponible[i, j].Size = new Size(45, 40);
                        botonDisponible[i, j].Text = "1";
                        botonDisponible[i, j].UseVisualStyleBackColor = false;

                        ttMensaje = new ToolTip();
                        ttMensaje.SetToolTip(botonDisponible[i, j], "OPCIÓN DISPONIBLE");

                        pnlBotones.Controls.Add(botonDisponible[i, j]);
                        iContador++;
                        iContadorAyuda++;

                        if (j + 1 == 5)
                        {
                            iPosXBoton = 0;
                            iPosYBoton += 46;
                        }

                        else
                        {
                            iPosXBoton += 51;
                        }

                        if (iCantidadTarjeta == iContador)
                        {
                            btnSiguiente.Enabled = false;
                            break;
                        }
                    }

                    if (iCantidadTarjeta == iContador)
                    {
                        btnSiguiente.Enabled = false;
                        break;
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

        //BOTON CLIC DE LAS SECCIONES
        public void boton_clic(object sender, EventArgs e)
        {
            Button btnSeleccion = sender as Button;

            ttMensaje = new ToolTip();
            int iSeleccion = Convert.ToInt32(btnSeleccion.AccessibleDescription);
            int iValor = Convert.ToInt32(txtCantidadSolicitada.Text);

            if (iSeleccion == 0)
            {
                iValor++;
                txtCantidadSolicitada.Text = iValor.ToString();
                btnSeleccion.AccessibleDescription = "1";
                btnSeleccion.BackColor = Color.Red;
                ttMensaje.SetToolTip(btnSeleccion, "OPCIÓN EN PROCESO");
            }

            else
            {
                iValor--;
                txtCantidadSolicitada.Text = iValor.ToString();
                btnSeleccion.AccessibleDescription = "0";
                btnSeleccion.BackColor = Color.FromArgb(128, 128, 255);
                ttMensaje.SetToolTip(btnSeleccion, "OPCIÓN DISPONIBLE");
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            this.Cursor = Cursors.Default;
            iIdPersona = 0;
            iIdPosTarjeta = 0;
            iIdProductoDescarga = 0;
            iIdProductoTarjeta = 0;
            pnlBotones.Controls.Clear();
            txtNumeroTarjeta.Clear();
            txtNombreCliente.Clear();
            txtCantidadDisponible.Text = "0";
            txtCantidadSolicitada.Text = "0";
            txtNumeroTarjeta.Focus();
        }

        //FUNCION PARA OBTENER LA LISTA MINORISTA
        private void obtenerIdListaMinorista()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and lista_minorista = @lista_minorista";

                SqlParameter[] parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@lista_minorista";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 1;

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

                if (dtConsulta.Rows.Count == 0)
                    iIdListaMinorista = 0;
                else
                    iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //OBTENER EL VALOR DEL PRODUCTO
        private void obtenerValoresProducto()
        {
            try
            {
                sSql = "";
                sSql += "select P.paga_iva, PP.id_lista_precio, PP.valor, P.paga_servicio," + Environment.NewLine;
                sSql += "NP.nombre nombre_producto, CLASE.codigo codigo_clase_producto" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = @P_Estado" + Environment.NewLine;
                sSql += "and PP.estado = @PP_Estado INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = @NP_Estado INNER JOIN" + Environment.NewLine;
                sSql += "pos_clase_producto CLASE ON CLASE.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSql += "and CLASE.estado = @C_Estado" + Environment.NewLine;
                sSql += "where P.id_producto = @id_producto" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = @id_lista_precio";

                parametro = new SqlParameter[6];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@P_Estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@PP_Estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@NP_Estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@C_Estado";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@id_producto";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = iIdProductoDescarga;

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@id_lista_precio";
                parametro[5].SqlDbType = SqlDbType.Int;
                parametro[5].Value = iIdListaMinorista;

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El producto no se encuentra configurado. Favor revise el registro.";
                    ok.ShowDialog();
                    btnGenerar.Enabled = false;
                    return;
                }

                btnGenerar.Enabled = true;
                iPagaIva = Convert.ToInt32(dtConsulta.Rows[0]["paga_iva"].ToString());
                iPagaServicio = Convert.ToInt32(dtConsulta.Rows[0]["paga_servicio"].ToString());
                dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString());
                sNombreProducto = dtConsulta.Rows[0]["nombre_producto"].ToString().Trim().ToUpper();
                sCodigoProducto = dtConsulta.Rows[0]["codigo_clase_producto"].ToString().Trim();

                if (iPagaIva == 1)
                    dbValorIva = dbPrecioUnitario * Convert.ToDecimal(Program.iva);
                else
                    dbValorIva = 0;

                if (iPagaServicio == 1)
                    dbValorServicio = dbPrecioUnitario * Convert.ToDecimal(Program.servicio);
                else
                    dbValorServicio = 0;
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
                sSql += "SELECT TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion direccion_cliente," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE TP.id_persona = @id_persona";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_persona";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPersona;

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
                {
                    sMail = dtConsulta.Rows[0]["correo_electronico"].ToString();
                    sDireccion = dtConsulta.Rows[0]["direccion_cliente"].ToString();
                    sCiudad = dtConsulta.Rows[0]["direccion"].ToString();

                    if (dtConsulta.Rows[0]["telefono_domicilio"].ToString() != "")
                    {
                        sTelefono = dtConsulta.Rows[0]["telefono_domicilio"].ToString();
                    }

                    else if (dtConsulta.Rows[0]["celular"].ToString() != "")
                    {
                        sTelefono = dtConsulta.Rows[0]["celular"].ToString();
                    }

                    else
                    {
                        sTelefono = "";
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra el registro del cliente. Comuníquese con el administrador.";
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

                iCantidadEmitir = Convert.ToInt32(txtCantidadSolicitada.Text);
                dbTotalDebido = iCantidadEmitir * (dbPrecioUnitario + dbValorIva + dbValorServicio);

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                if (insertarComanda_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                if (insertarTarjetaAlmuerzo_V2() == false)
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

                if (insertarFactura_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    Cursor = Cursors.Default;
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                crearReporte();

                limpiar();

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
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

                dtItems.Rows.Add(iIdProductoDescarga, dbPrecioUnitario, iCantidadEmitir, "0", iPagaIva,
                                     "0", "0", "0", "0", "0", "1", "", "", sNombreProducto, sCodigoProducto, "0", "0");

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

                dtDetalleItems = new DataTable();
                dtDetalleItems.Clear();

                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarComanda(0, iIdPersona, 0, iIdPosOrigenOrden, dbTotalDebido, "Cerrada",
                                                    0, 0, Program.CAJERO_ID,
                                                    0, "", Program.iIdMesero, Program.iIdPosTerminal,
                                                    Convert.ToDecimal(Program.servicio * 100), 0,
                                                    0, 0, Program.iIdPosCierreCajero,
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
                iNumeroPedidoOrden = comanda.iNumeroPedidoOrden;

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

        //FUNCION PARA ENVIAR LOS PARAMETROS A LA FUNCION DE CREAR UNA TARJETA DE ALMUERZO
        private bool insertarTarjetaAlmuerzo_V2()
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarTarjetaALmuerzo(0, 0, iIdPersona, Program.iIdLocalidad, 0, iIdProductoDescarga,
                                                             "", "Vigente", sFecha, Program.sDatosMaximo[0], Program.sDatosMaximo[1],
                                                             iCantidadEmitir, iIdPedido, 0, "Despacho", iIdPosTarjeta, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iNumeroTarjeta = comanda.iNumeroTarjeta;

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

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR NUEVOS PAGOS
        private bool insertarPagos_V2()
        {
            try
            {
                if (crearTablaPagos() == false)
                    return false;

                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarPagos(iIdPedido, dtPagos, dbTotalDebido, 0, 0,
                                                   iIdPersona, sFecha, Program.iIdLocalidad, 0, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdDocumentoPorCobrar = comanda.iIdDocumentoCobrar;

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

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR FACTURA
        private bool insertarFactura_V2()
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarFactura(iIdPedido, iIdTipoComprobante, 0,
                                                     iIdPersona, Program.iIdLocalidad, dtPagos, dbTotalDebido, 0,
                                                     0, 0, sFecha, iIdDocumentoPorCobrar, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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

                dtPagos.Rows.Add(iIdTipoFormaCobro, sDescripcionFormaPago, dbTotalDebido, iIdSriFormaPago_P, 0, 0, 0, "", 0, 0,
                                 sCodigoMetodoPago, "", "", "0", "", "");

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

        //FUNCION PARA OBTENER LOS VALORES PARA INSERTAR EN LA SECCION DE PAGOS
        private bool obtenerDatosFormaPagoRealizada(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_obtener_datos_formas_pagos" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = sCodigo_P;

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran configurados los registros de cobros. Favor comuníquese con el administrador.";
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
                ReportesTextBox.frmVerPedidoTarjetaAlmuerzo precuenta = new ReportesTextBox.frmVerPedidoTarjetaAlmuerzo(iIdPedido.ToString(), 1);
                precuenta.ShowDialog();

                Cambiocs ok = new Cambiocs("$ 0.00");
                ok.lblVerMensaje.Text = "TICKET DE TARJETA GENERADA";
                ok.ShowDialog();
                precuenta.Close();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        #endregion

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iContador -= iContadorAyuda;

            if (iContadorAyuda <= 30)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iContador -= 30;

            crearBotones();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearBotones();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNumeroTarjeta.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de tarjeta a buscar.";
                ok.ShowDialog();
                return;
            }

            consultarTarjeta(Convert.ToInt32(txtNumeroTarjeta.Text));
        }

        private void txtNumeroTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtNumeroTarjeta.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el número de tarjeta a buscar.";
                    ok.ShowDialog();
                    return;
                }

                consultarTarjeta(Convert.ToInt32(txtNumeroTarjeta.Text));
            }
        }

        private void frmComandaTarjetaAlmuerzo_Load(object sender, EventArgs e)
        {
            iIdTipoComprobante = Program.iComprobanteNotaEntrega;
            obtenerIdListaMinorista();
            this.ActiveControl = txtNumeroTarjeta;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Tarjeta_Almuerzo.frmListarTarjetasVigentes listar = new Tarjeta_Almuerzo.frmListarTarjetasVigentes();
            listar.ShowDialog();

            if (listar.DialogResult == DialogResult.OK)
            {
                int iNumeroTarjeta = listar.iNumeroTarjeta_P;
                listar.Close();
                txtNumeroTarjeta.Text = iNumeroTarjeta.ToString();
                consultarTarjeta(iNumeroTarjeta);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (iIdPosTarjeta == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione una tarjeta.";
                ok.ShowDialog();

                txtNumeroTarjeta.Focus();
                return;
            }

            if (Convert.ToInt32(txtCantidadSolicitada.Text) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la cantidad de ítems a solicitar.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea genera un ticket de almuerzo?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (obtenerDatosFormaPagoRealizada("TA") == false)
                    return;

                crearComandaNueva_V2();
            }
        }
    }
}
