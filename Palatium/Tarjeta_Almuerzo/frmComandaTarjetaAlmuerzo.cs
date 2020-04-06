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

        string sSql;
        string sFecha;
        string sEstablecimiento;
        string sPuntoEmision;
        string sTabla;
        string sCampo;
        string sDescripcionFormaPago;
        string sCiudad;
        string sTelefono;
        string sDireccion;
        string sMail;

        long iMaximo;

        ToolTip ttMensaje;

        DataTable dtConsulta;
        DataTable dtLocalidad;

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

        int iCantidadEmitir;
        int iCantidadTarjeta;
        int iCantidadVendidos;
        int iContador;
        int iContadorAyuda;
        int iPosXBoton;
        int iPosYBoton;
        int iCuentaDiaria;
        int iIdLocalidadBodega;
        int iIdBodegaInsumos;
        int iIdPedido;
        int iNumeroPedidoOrden;
        int iIdCabDespachos;
        int iIdDespachoPedido;
        int iIdEventoCobro;
        int iCgTipoDocumento = 2725;
        int iIdDocumentoCobrar;
        int iIdPago;
        int iNumeroPago;
        int iCgTipoDocumentoCobro;
        int iIdFormaPago_1;
        int iIdFactura;
        int iCgEstadoDctoPorCobrar = 7461;
        int iIdPosTarCabecera;
        int iIdTipoFormaCobro;
        int iNumeroNotaEntrega;

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
                sSql += "select P.paga_iva, PP.id_lista_precio, PP.valor, P.paga_servicio" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = @P_Estado" + Environment.NewLine;
                sSql += "and PP.estado = @PP_Estado" + Environment.NewLine;
                sSql += "where P.id_producto = @id_producto" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = @id_lista_precio";

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@P_Estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@PP_Estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_producto";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iIdProductoDescarga;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@id_lista_precio";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = iIdListaMinorista;

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

        #region FUNCIONES PARA INSERTAR EN LA BASE DE DATOS

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

                if (extraerNumeroCuenta() == false)
                    return;

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    return;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                iCantidadEmitir = Convert.ToInt32(txtCantidadSolicitada.Text);
                dbTotalDebido = iCantidadEmitir * (dbPrecioUnitario + dbValorIva + dbValorServicio);

                if (insertarPedido() == false)
                {
                    Cursor = Cursors.Default;
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                if (insertarTarjeta() == false)
                {
                    Cursor = Cursors.Default;
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                if (insertarPagos() == false)
                {
                    Cursor = Cursors.Default;
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                if (insertarFactura() == false)
                {
                    Cursor = Cursors.Default;
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                crearReporte();

                limpiar();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE CUENTA
        private bool extraerNumeroCuenta()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = @id_pos_cierre_cajero";

                SqlParameter[] parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_cierre_cajero";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdPosCierreCajero;

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

                iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
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

        //FUNCION PARA RECUPERAR LOS DATOS DE LA LOCALIDAD
        private bool recuperarDatosLocalidad()
        {
            try
            {
                SqlParameter[] parametro = new SqlParameter[1];

                sSql = "";
                sSql += "select * from tp_localidades" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = @id_localidad";

                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                dtLocalidad = new DataTable();
                dtLocalidad.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtLocalidad, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sEstablecimiento = dtLocalidad.Rows[0]["establecimiento"].ToString();
                sPuntoEmision = dtLocalidad.Rows[0]["punto_emision"].ToString();

                //AQUI SE RECUPERA LA LOCALIDAD INSUMO
                sSql = "";
                sSql += "select id_localidad_insumo" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and estado = 'A'";

                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

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
                sSql += "where id_localidad = @iIdLocalidadBodega" + Environment.NewLine;
                sSql += "and estado = 'A'";

                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@iIdLocalidadBodega";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdLocalidadBodega;

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

        //INSERTAR LA PRIMERA FASE - PEDIDO
        private bool insertarPedido()
        {
            try
            {
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
                sSql += Program.iIdCajeroDefault + "," + iIdPosOrigenOrden + ", 0," + Program.iJORNADA + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', GETDATE(), GETDATE(), 'Cerrada'," + Environment.NewLine;
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
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + dbTotalDebido + "," + Environment.NewLine;
                sSql += "7460, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------                
                sSql = "";
                sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                sSql += "id_pos_secuencia_entrega, Estado, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPedido + ", " + iIdProductoDescarga + ", 546, " + dbPrecioUnitario + ", " + Environment.NewLine;
                sSql += iCantidadEmitir + ", 0, 0, " + dbValorIva + ", " + dbValorServicio + ", " + Environment.NewLine;
                sSql += "null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 0, 1, null, 'A', 0, 0)";

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

                //if (sCodigoClaseProducto.Trim() == "02")
                //{
                //    if (Program.iDescargarProductosNoProcesados == 1)
                //    {
                //        if (insertarMovimientoProductoNoProcesado(Convert.ToDecimal(dCantidad_P)) == false)
                //        {
                //            return false;
                //        }
                //    }
                //}

                //ACTUALIZACION
                //FECHA: 2019-10-05
                //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                //if (sCodigoClaseProducto.Trim() == "03")
                //{
                //    if (Program.iDescargarReceta == 1)
                //    {
                //        if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                //        {
                //            return false;
                //        }
                //    }
                //}

                sSql = "";
                sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDespachoPedido + ", " + iIdProductoTarjeta + ", 1, 'A', 0, 0)";

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

        //INSERTAR LA SEGUNDA FASE - TARJETA DE ALMUERZO
        private bool insertarTarjeta()
        {
            try
            {
                SqlParameter[] parametro;
                
                sSql = "";
                sSql += "insert into pos_tar_cab_movimiento(" + Environment.NewLine;
                sSql += "id_pos_tar_tarjeta, id_pedido, id_localidad, fecha_pedido_tarjeta," + Environment.NewLine;
                sSql += "fecha_hora_pedido_tarjeta, estado_pedido_tarjeta, tipo_movimiento," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_pos_tar_tarjeta, @id_pedido, @id_localidad, @fecha_pedido_tarjeta," + Environment.NewLine;
                sSql += "getdate(), @estado_pedido_tarjeta, @tipo_movimiento," + Environment.NewLine;
                sSql += "@estado, getdate(), @usuario_ingreso, @terminal_ingreso)";

                parametro = new SqlParameter[9];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_tarjeta";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPosTarjeta;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pedido";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdPedido;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_localidad";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Program.iIdLocalidad;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@fecha_pedido_tarjeta";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = sFecha;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@estado_pedido_tarjeta";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = "Despacho";

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@tipo_movimiento";
                parametro[5].SqlDbType = SqlDbType.Int;
                parametro[5].Value = 0;

                parametro[6] = new SqlParameter();
                parametro[6].ParameterName = "@estado";
                parametro[6].SqlDbType = SqlDbType.VarChar;
                parametro[6].Value = "A";

                parametro[7] = new SqlParameter();
                parametro[7].ParameterName = "@usuario_ingreso";
                parametro[7].SqlDbType = SqlDbType.VarChar;
                parametro[7].Value = Program.sDatosMaximo[0];

                parametro[8] = new SqlParameter();
                parametro[8].ParameterName = "@terminal_ingreso";
                parametro[8].SqlDbType = SqlDbType.VarChar;
                parametro[8].Value = Program.sDatosMaximo[1];

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA pos_tar_cab_movimiento
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "pos_tar_cab_movimiento";
                sCampo = "id_pos_tar_cab_movimiento";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPosTarCabecera = Convert.ToInt32(iMaximo);

                sSql = "";
                sSql += "insert into pos_tar_det_movimiento (" + Environment.NewLine;
                sSql += "id_pos_tar_cab_movimiento, id_producto, cantidad, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_pos_tar_cab_movimiento, @id_producto, @cantidad, @estado," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                parametro = new SqlParameter[6];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_cab_movimiento";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPosTarCabecera;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdProductoDescarga;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@cantidad";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iCantidadEmitir * -1;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@usuario_ingreso";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = Program.sDatosMaximo[0];

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@terminal_ingreso";
                parametro[5].SqlDbType = SqlDbType.VarChar;
                parametro[5].Value = Program.sDatosMaximo[1];

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (iCantidadTarjeta == iCantidadEmitir)
                {
                    sSql = "";
                    sSql += "update pos_tar_tarjeta set" + Environment.NewLine;
                    sSql += "estado_tarjeta = @estado_tarjeta," + Environment.NewLine;
                    sSql += "is_active = @is_active" + Environment.NewLine;
                    sSql += "where id_pos_tar_tarjeta = @id_pos_tar_tarjeta" + Environment.NewLine;
                    sSql += "and estado = @estado";

                    parametro = new SqlParameter[4];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@estado_tarjeta";
                    parametro[0].SqlDbType = SqlDbType.VarChar;
                    parametro[0].Value = "Cerrada";

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@is_active";
                    parametro[1].SqlDbType = SqlDbType.Int;
                    parametro[1].Value = 0;

                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@id_pos_tar_tarjeta";
                    parametro[2].SqlDbType = SqlDbType.Int;
                    parametro[2].Value = iIdPosTarjeta;

                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@estado";
                    parametro[3].SqlDbType = SqlDbType.VarChar;
                    parametro[3].Value = "A";

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
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

        //INSERTAR LA TERCERA FASE - PAGOS
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
                sSql += dbTotalDebido + ", 0, " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 0, 0, 0)";

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
                sSql += "FC.descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                sSql += "and MP.estado = 'A'" + Environment.NewLine;
                sSql += "and FC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "sri_forma_pago FP ON FP.id_sri_forma_pago = MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and FP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_formas_pagos FPA ON FP.id_sri_forma_pago = FPA.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and FPA.estado = 'A'" + Environment.NewLine;
                sSql += "where FPA.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and MP.codigo = 'TA'";

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
                sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", " + iCgTipoDocumentoCobro + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                sSql += Program.iMoneda + ", 1, " + dbTotalDebido + ", " + iIdTipoFormaCobro + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, " + dbTotalDebido + "," + Environment.NewLine;
                sSql += "null, null, null, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }


                //INSERTAR EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dbTotalDebido + ", 'A', 0, 0," + Environment.NewLine;
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

        //INSERTAR LA CUARTA FASE - FACTURA
        private bool insertarFactura()
        {
            try
            {
                int iFacturaElectronica_P = 0;

                //EXTRAER EL NUMERO DE FACTURA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select numeronotaentrega" + Environment.NewLine;
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

                iNumeroNotaEntrega = Convert.ToInt32(dtConsulta.Rows[0]["numeronotaentrega"].ToString());


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
                sSql += "null, null, '" + sFecha + "', '" + sFecha + "', " + Program.iMoneda + ", " + dbTotalDebido + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0," + Environment.NewLine;
                //sSql += "'QUITO', '2999999', 'QUITO', '" + Program.sCorreoElectronicoDefault + "', 0, 0, 0, 0)";
                sSql += "'" + sDireccion.ToUpper() + "', '" + sTelefono.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + sCiudad + "', '" + sMail.Trim().ToLower() + "', 0, " + iFacturaElectronica_P + ", 0, 0, null)";

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
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + iNumeroNotaEntrega + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR EL NUMERO DE FACTURA EN TP_LOCALIDADES_IMPRESORAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeronotaentrega = numeronotaentrega + 1" + Environment.NewLine;
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
                sSql += "numero_documento = " + iNumeroNotaEntrega + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

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
                crearComanda();
            }
        }
    }
}
