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
    public partial class frmCreacionTarjetaAlmuerzo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        ValidarCedula validarCedula = new ValidarCedula();

        string sSql;
        string sCiudad;
        string sCorreoAyuda;
        string sNumeroLote;
        string sFecha;
        string sTabla;
        string sCampo;
        string sDescripcionFormaPago;
        string sClaveAcceso;
        string sSecuencial;
        string sEstablecimiento;
        string sPuntoEmision;
        string sMovimiento;
        string sNombreTarjeta;
        string sNombreItem;

        DataTable dtConsulta;
        DataTable dtTarjetas;
        DataTable dtRecargos;
        DataTable dtLocalidad;

        bool bRespuesta;

        int iIdCantidadAlmuerzos;
        int iIdListaMinorista;
        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iTercerDigito;
        int iBanderaEfectivoTarjeta;
        int iPagaIva;
        int iIdProductoTarjeta;
        int iIdProductoDescarga;
        int iBanderaAplicaRecargo;
        int iIdTipoFormaCobro;
        int iConciliacion;
        int iOperadorTarjeta;
        int iTipoTarjeta;
        int iBanderaInsertarLote;
        int iCuentaDiaria;
        int iIdBodegaInsumos;
        int iIdLocalidadBodega;
        int iIdOrigenOrden;
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
        int iIdCaja;
        int iNumeroFactura;
        int iIdDocumentoPago;
        int iIdPosMovimientoCaja;
        int iNumeroMovimientoCaja;
        int iIdTipoAmbiente;
        int iIdTipoEmision;
        int iIdPosTarjeta;
        int iIdPosTarCabecera;
        int iNumeroTarjeta;
        int iCantidadNominal;
        int iCantidadReal;
        
        long iMaximo;

        Decimal dbPropina;
        Decimal dTotalDebido;
        Decimal dbValorGrid;
        Decimal dbValorRecuperado;
        Decimal dbCambio;
        Decimal dbPrecioUnitario_P;
        Decimal dbValorIva_P;
        Decimal dbValorTotal_P;

        public frmCreacionTarjetaAlmuerzo(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        private bool esNumero(object Expression)
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
            txtApellidos.Clear();
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
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
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

                if (bRespuesta)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtApellidos.Text = (dtConsulta.Rows[0][2].ToString() + " " + dtConsulta.Rows[0][3].ToString()).Trim().ToUpper();
                        txtMail.Text = dtConsulta.Rows[0][4].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0][5].ToString();
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
                            txtApellidos.Text = (frmNuevoCliente.sNombre + " " + frmNuevoCliente.sApellido).Trim().ToUpper();
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
            }
            catch (Exception ex)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO TAB CREAR

        //FUNCION PARA LIMPIAR 
        private void limpiarCrear()
        {
            obtenerIdListaMinorista();
            cmbListarTarjetas.SelectedValueChanged -= new EventHandler(cmbListarTarjetas_SelectedIndexChanged);
            llenarComboTarjetas();
            cmbListarTarjetas.SelectedValueChanged += new EventHandler(cmbListarTarjetas_SelectedIndexChanged);

            chkPasaporte.Checked = false;

            txtApellidos.Clear();
            txtIdentificacion.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            btnCorreoElectronicoDefault.AccessibleName = "0";
            txtMail.ReadOnly = true;
            txtCantidadNominal.Text = "0";
            txtCantidadReal.Text = "0";
            txtPrecioFinal.Text = "0.00";
            txtIdentificacion.Focus();
            Cursor = Cursors.Default;
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

        //FUNCION PARA LLENAR EL COMBOBOX
        private void llenarComboTarjetas()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_tar_seleccion_tarjeta_venta" + Environment.NewLine;
                sSql += "where id_lista_precio = @id_lista_precio";

                SqlParameter[] parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_lista_precio";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdListaMinorista;

                dtTarjetas = new DataTable();
                dtTarjetas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtTarjetas, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtTarjetas.NewRow();
                row["id_pos_tar_cantidad_tipo_almuerzo"] = "0";
                row["descripcion"] = "Seleccione Tarjeta";
                dtTarjetas.Rows.InsertAt(row, 0);

                cmbListarTarjetas.DisplayMember = "descripcion";
                cmbListarTarjetas.ValueMember = "id_pos_tar_cantidad_tipo_almuerzo";
                cmbListarTarjetas.DataSource = dtTarjetas;
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
                crearReportetarjeta();
                limpiarCrear();
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
                sSql += Program.iIdCajeroDefault + "," + iIdOrigenOrden + ", 0," + Program.iJORNADA + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', GETDATE(), GETDATE(), 'Pagada'," + Environment.NewLine;
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
                sSql += "1, " + iIdPedido + ", " + iNumeroPedidoOrden + ", GETDATE()," + Environment.NewLine;
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

                if (iBanderaAplicaRecargo == 0)
                {
                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedido + ", " + iIdProductoTarjeta + ", 546, " + dbPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += "1, 0, 0, " + dbValorIva_P + ", 0, " + Environment.NewLine;
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
                }

                else
                {
                    Decimal dPrecioUnitario_Recargo = Convert.ToDecimal(dtRecargos.Rows[0]["valor_recargo"].ToString());
                    Decimal dbValorIva_Recargo = 0;

                    if (iPagaIva== 1)
                        dbValorIva_Recargo = Convert.ToDecimal(dtRecargos.Rows[0]["valor_iva"].ToString());

                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPedido + ", " + iIdProductoTarjeta + ", 546, " + dPrecioUnitario_Recargo + ", " + Environment.NewLine;
                    sSql += "1, 0, 0, " + dbValorIva_Recargo + ", 0, " + Environment.NewLine;
                    sSql += "null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 0, 1, null, 'A', 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        return false;
                    }

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

        private bool insertarTarjeta()
        {
            try
            {
                SqlParameter[] parametro;

                sSql = "";
                sSql += "insert into pos_tar_tarjeta (" + Environment.NewLine;
                sSql += "id_pos_tar_cantidad_tipo_almuerzo, id_pos_tar_cantidad_almuerzo, id_persona," + Environment.NewLine;
                sSql += "id_localidad, id_producto_tarjeta, id_producto_descarga, observacion," + Environment.NewLine;
                sSql += "estado_tarjeta, fecha_emision, is_active, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_pos_tar_cantidad_tipo_almuerzo, @id_pos_tar_cantidad_almuerzo, @id_persona," + Environment.NewLine;
                sSql += "@id_localidad, @id_producto_tarjeta, @id_producto_descarga, @observacion," + Environment.NewLine;
                sSql += "@estado_tarjeta, @fecha_emision, @is_active, @estado, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)" + Environment.NewLine;

                parametro = new SqlParameter[13];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_cantidad_tipo_almuerzo";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = cmbListarTarjetas.SelectedValue;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_tar_cantidad_almuerzo";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdCantidadAlmuerzos;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_persona";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iIdPersona;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@id_localidad";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = Program.iIdLocalidad;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@id_producto_tarjeta";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = iIdProductoTarjeta;

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@id_producto_descarga";
                parametro[5].SqlDbType = SqlDbType.Int;
                parametro[5].Value = iIdProductoDescarga;

                parametro[6] = new SqlParameter();
                parametro[6].ParameterName = "@observacion";
                parametro[6].SqlDbType = SqlDbType.VarChar;
                parametro[6].Value = "";

                parametro[7] = new SqlParameter();
                parametro[7].ParameterName = "@estado_tarjeta";
                parametro[7].SqlDbType = SqlDbType.VarChar;
                parametro[7].Value = "Vigente";

                parametro[8] = new SqlParameter();
                parametro[8].ParameterName = "@fecha_emision";
                parametro[8].SqlDbType = SqlDbType.VarChar;
                parametro[8].Value = sFecha;
                
                parametro[9] = new SqlParameter();
                parametro[9].ParameterName = "@is_active";
                parametro[9].SqlDbType = SqlDbType.Int;
                parametro[9].Value = 1;
                
                parametro[10] = new SqlParameter();
                parametro[10].ParameterName = "@estado";
                parametro[10].SqlDbType = SqlDbType.VarChar;
                parametro[10].Value = "A";

                parametro[11] = new SqlParameter();
                parametro[11].ParameterName = "@usuario_ingreso";
                parametro[11].SqlDbType = SqlDbType.VarChar;
                parametro[11].Value = Program.sDatosMaximo[0];

                parametro[12] = new SqlParameter();
                parametro[12].ParameterName = "@terminal_ingreso";
                parametro[12].SqlDbType = SqlDbType.VarChar;
                parametro[12].Value = Program.sDatosMaximo[1];

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID DE LA TABLA POS_TAR_TARJETA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sTabla = "pos_tar_tarjeta";
                sCampo = "id_pos_tar_tarjeta";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPosTarjeta = Convert.ToInt32(iMaximo);

                //EXTRAER EL NUMERO DE TARJETA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select numerotarjetaalmuerzo" + Environment.NewLine;
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

                iNumeroTarjeta = Convert.ToInt32(dtConsulta.Rows[0]["numerotarjetaalmuerzo"].ToString());

                sSql = "";
                sSql += "insert into pos_tar_numero_tarjeta (" + Environment.NewLine;
                sSql += "id_pos_tar_tarjeta, numero_tarjeta, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_pos_tar_tarjeta, @numero_tarjeta, @estado, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)";

                parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_tarjeta";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPosTarjeta;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@numero_tarjeta";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iNumeroTarjeta;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@usuario_ingreso";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = Program.sDatosMaximo[0];

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@terminal_ingreso";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = Program.sDatosMaximo[1];

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR EL NUMERO DE TARJETA EN TP_LOCALIDADES_IMPRESORAS
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numerotarjetaalmuerzo = numerotarjetaalmuerzo + 1" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad"+ Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

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
                parametro[4].Value = "Venta";

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@tipo_movimiento";
                parametro[5].SqlDbType = SqlDbType.Int;
                parametro[5].Value = 1;
                
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
                parametro[2].Value = txtCantidadReal.Text;

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
                sSql += dTotalDebido + ", " + dbPropina + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
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

                if (iBanderaEfectivoTarjeta == 0)
                {
                    sSql += "and MP.codigo = 'EF'";
                }

                else
                {
                    sSql += "and FC.id_pos_tipo_forma_cobro = " + iIdTipoFormaCobro;
                }

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

                if (iConciliacion == 1)
                {
                    int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                    if (iRespuestaNumeroLote == -1)
                    {
                        return false;
                    }

                    if (iRespuestaNumeroLote == 0)
                    {
                        if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                        {
                            return false;
                        }
                    }
                }

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
                sSql += Program.iMoneda + ", 1, " + dTotalDebido + ", " + iIdTipoFormaCobro + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, " + dbValorRecuperado + "," + Environment.NewLine;

                if (iConciliacion == 1)
                {
                    sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                }

                else
                {
                    sSql += "null, null, null, ";
                }

                sSql += dbPropina + ")";

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
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotalDebido + ", 'A', 0, 0," + Environment.NewLine;
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
                sSql += "select numero_factura" + Environment.NewLine;
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

                iNumeroFactura = Convert.ToInt32(dtConsulta.Rows[0]["numero_factura"].ToString());

                if (Program.iFacturacionElectronica == 1)
                {
                    iFacturaElectronica_P = 1;
                    sClaveAcceso = sGenerarClaveAcceso();
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
                sSql += "1," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago_1 + ", " + Environment.NewLine;
                sSql += "null, null, '" + sFecha + "', '" + sFecha + "', " + Program.iMoneda + ", " + dTotalDebido + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0," + Environment.NewLine;
                //sSql += "'QUITO', '2999999', 'QUITO', '" + Program.sCorreoElectronicoDefault + "', 0, 0, 0, 0)";
                sSql += "'" + txtDireccion.Text.Trim().ToUpper() + "', '" + txtTelefono.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + sCiudad + "', '" + txtMail.Text.Trim().ToLower() + "', 0, " + iFacturaElectronica_P + ", " + iIdTipoEmision + ", " + iIdTipoAmbiente + ", " + Environment.NewLine;

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
                sSql += iIdFactura + ", 1, " + iNumeroFactura + ", GETDATE()," + Environment.NewLine;
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
                sSql += "numero_factura = numero_factura + 1" + Environment.NewLine;
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
                sSql += "numero_documento = " + iNumeroFactura + Environment.NewLine;
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
                sSecuencial = iNumeroFactura.ToString().PadLeft(9, '0');
                sMovimiento = ("FACT. No. " + sEstablecimiento + "-" + sPuntoEmision + "-" + sSecuencial).Trim();

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
                    ReportesTextBox.frmVistaFactura frmVistaFactura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 1);
                    frmVistaFactura.ShowDialog();

                    if (frmVistaFactura.DialogResult == DialogResult.OK)
                    {
                        frmVistaFactura.Close();
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

        //FUNCION PARA CREAR UN REPORTE DE LA TARJETA
        private void crearReportetarjeta()
        {
            try
            {
                string sTexto = "";
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "     REPORTE DE COMPRA DE TARJETA" + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "IDENTIFICACIÓN: " + txtIdentificacion.Text.Trim() + Environment.NewLine;

                string sNombreCliente = txtApellidos.Text.Trim().ToUpper();

                if (sNombreCliente.Length > 40)
                    sTexto += sNombreCliente.Substring(0, 40) + Environment.NewLine;
                else
                    sTexto += sNombreCliente + Environment.NewLine;

                sTexto += Environment.NewLine;
                sTexto += "NÚMERO DE FACTURA: " + sEstablecimiento + "-" + sPuntoEmision + "-" + sSecuencial + Environment.NewLine;
                sTexto += "NÚMERO DE TARJETA: " + iNumeroTarjeta.ToString() + Environment.NewLine;
                sTexto += "FECHA DE COMPRA  : " + Convert.ToDateTime(sFecha).ToString("dd-MM-yyyy") + Environment.NewLine + Environment.NewLine;

                sTexto += "ÍTEM: 1 " + sNombreTarjeta + Environment.NewLine;
                sTexto += "DETALLE: " + sNombreItem + Environment.NewLine + Environment.NewLine;

                sTexto += "EQUIVALENCIA:" + Environment.NewLine;
                sTexto += "CANTIDAD NOMINAL: " + iCantidadNominal.ToString() + Environment.NewLine;
                sTexto += "CORTESÍA        : " + (iCantidadReal - iCantidadNominal).ToString() + Environment.NewLine;
                sTexto += "TOTAL TARJETA   : " + iCantidadReal.ToString() + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine + Environment.NewLine  + Environment.NewLine + Environment.NewLine;
                sTexto += "_______________          _______________" + Environment.NewLine;
                sTexto += "  AUTORIZADO             RECIBÍ CONFORME" + Environment.NewLine;
                sTexto += "".PadLeft(40, '=');

                Utilitarios.frmReporteGenerico reporte = new Utilitarios.frmReporteGenerico(sTexto, 1, 1, 1, Program.iCantidadImpresionesTarjetas);
                reporte.ShowDialog();

                Cambiocs cambiocs = new Cambiocs("$ " + dbCambio.ToString("N2"));
                cambiocs.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                cambiocs.ShowDialog();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
            string NumeroComprobante = iNumeroFactura.ToString().PadLeft(9, '0');
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

        //FUNCION PARA INSERTAR EL NUMERO DE LOTE EN LA TABLA POS_NUMERO_LOTE
        private bool insertarNumeroLote(string sNumeroLote_P, int iOperadorTarjeta_P)
        {
            try
            {
                //string sFecha_P = Program.sFechaSistema.ToString("yyyy-MM-dd");

                sSql = "";
                sSql += "insert into pos_numero_lote (" + Environment.NewLine;
                sSql += "id_localidad, id_pos_jornada, id_pos_operador_tarjeta, lote," + Environment.NewLine;
                sSql += "fecha_apertura, estado_lote, id_pos_cierre_cajero, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", " + Program.iJORNADA + ", " + iOperadorTarjeta_P + ", ";
                sSql += "'" + sNumeroLote_P + "', '" + sFecha + "', 'Abierta', " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

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

        //FUNCION PARA CONTAR LOS NUMEROS DE LOTES
        private int contarNumeroLote(int iOperadorTarjeta_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_numero_lote" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado_lote = 'Abierta'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_pos_operador_tarjeta = " + iOperadorTarjeta_P + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;

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

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO PARA LISTAR LAS TARJETAS

        //FUNCION LLENAR GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int iOp_P = 0;

                sSql = "";
                sSql += "select * from pos_vw_tar_lista_tarjetas_almuerzo_emitidas" + Environment.NewLine;
                
                if (txtBuscar.Text.Trim() != "")
                {
                    iOp_P = 1;
                    sSql += "where (identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or numero_tarjeta like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or cliente like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                if (rdbVigentes.Checked == true)
                {
                    if (iOp_P == 0)
                    {
                        sSql += "where ";
                    }

                    else
                    {
                        sSql += "and ";
                    }
                
                    sSql += "estado_tarjeta = 'Vigente'" + Environment.NewLine;
                }

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

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                        dtConsulta.Rows[i]["identificacion"].ToString(),
                                        dtConsulta.Rows[i]["cliente"].ToString(),
                                        Convert.ToDateTime(dtConsulta.Rows[i]["fecha_emision"].ToString()).ToString("dd-MM-yyyy"),
                                        dtConsulta.Rows[i]["numero_tarjeta"].ToString(),
                                        dtConsulta.Rows[i]["estado_tarjeta"].ToString(),
                                        dtConsulta.Rows[i]["disponibles"].ToString()
                                        );
                }

                dgvDatos.ClearSelection();
                txtBuscar.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmCreacionTarjetaAlmuerzo_Load(object sender, EventArgs e)
        {
            configuracionFacturacion();
            limpiarCrear();
            this.ActiveControl = txtIdentificacion;
        }

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedTab == tbControl.TabPages["tabCrear"])
            {
                limpiarCrear();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabLista"])
            {
                rdbVigentes.Checked = true;
                llenarGrid();
                txtBuscar.Clear();
                txtBuscar.Focus();
                return;
            }
        }

        private void cmbListarTarjetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            iIdCantidadAlmuerzos = 0;
            iIdProductoTarjeta = 0;
            iIdProductoDescarga = 0;
            iPagaIva = 0;
            dbPrecioUnitario_P = 0;
            dbValorIva_P = 0;
            dbValorTotal_P = 0;
            txtCantidadNominal.Text = "0";
            txtCantidadReal.Text = "0";
            txtPrecioFinal.Text = "0.00";

            if (Convert.ToInt32(cmbListarTarjetas.SelectedValue) == 0)
            {                
                return;
            }

            DataRow[] fila = dtTarjetas.Select("id_pos_tar_cantidad_tipo_almuerzo = " + cmbListarTarjetas.SelectedValue);

            if (fila.Length != 0)
            {
                iIdProductoTarjeta = Convert.ToInt32(fila[0][5].ToString());
                iIdProductoDescarga = Convert.ToInt32(fila[0][6].ToString());
                txtCantidadNominal.Text = fila[0][7].ToString();
                txtCantidadReal.Text = fila[0][8].ToString();

                iCantidadNominal = Convert.ToInt32(fila[0][7].ToString());
                iCantidadReal = Convert.ToInt32(fila[0][8].ToString());

                sNombreTarjeta = fila[0][9].ToString().Trim().ToUpper();
                sNombreItem = fila[0][10].ToString().Trim().ToUpper();

                iIdCantidadAlmuerzos = Convert.ToInt32(fila[0][11].ToString());

                iPagaIva = Convert.ToInt32(fila[0][3].ToString());
                dbPrecioUnitario_P = Convert.ToDecimal(fila[0][2].ToString());

                if (iPagaIva == 1)
                {
                    dbValorIva_P = dbPrecioUnitario_P * Convert.ToDecimal(Program.iva);
                    dbValorTotal_P = dbPrecioUnitario_P + dbValorIva_P;

                    txtPrecioFinal.Text = dbValorTotal_P.ToString("N2");
                }

                else
                {
                    dbValorIva_P = 0;
                    dbValorTotal_P = dbPrecioUnitario_P;
                    txtPrecioFinal.Text = dbPrecioUnitario_P.ToString("N2");
                }
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

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Text = "9999999999999";
            txtApellidos.Text = "CONSUMIDOR FINAL";
            txtTelefono.Text = "9999999999";
            txtMail.Text = Program.sCorreoElectronicoDefault;
            txtDireccion.Text = "QUITO";
            iIdPersona = Program.iIdPersona;
            idTipoIdentificacion = 180;
            idTipoPersona = 2447;
            btnEditar.Visible = false;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCrear();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (iIdPersona == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione los datos de la persona a facturar la tarjeta.";
                ok.ShowDialog();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (iIdPersona == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el cliente a facturar la tarjeta de almuerzo.";
                ok.ShowDialog();
                txtIdentificacion.Focus();
                return;
            }

            if (iIdProductoTarjeta == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la tarjeta que se va a facturar.";
                ok.ShowDialog();
                cmbListarTarjetas.Focus();
                return;
            }

            dTotalDebido = Convert.ToDecimal(txtPrecioFinal.Text);

            Efectivo efectivo = new Efectivo("0", dTotalDebido.ToString("N2"), "", "EFECTIVO", "EF");
            efectivo.ShowDialog();

            if (efectivo.DialogResult == DialogResult.OK)
            {
                dbValorGrid = efectivo.dbValorGrid;
                dbValorRecuperado = efectivo.dbValorIngresado;
                dbCambio = dbValorRecuperado - dbValorGrid;
                efectivo.Close();

                iBanderaEfectivoTarjeta = 0;
                crearComanda();
            }
        }

        private void btnTarjetas_Click(object sender, EventArgs e)
        {
            if (iIdPersona == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el cliente a facturar la tarjeta de almuerzo.";
                ok.ShowDialog();
                txtIdentificacion.Focus();
                return;
            }

            if (iIdProductoTarjeta == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la tarjeta que se va a facturar.";
                ok.ShowDialog();
                cmbListarTarjetas.Focus();
                return;
            }

            dTotalDebido = Convert.ToDecimal(txtPrecioFinal.Text);

            DataTable dtItems = new DataTable();
            dtItems.Columns.Add("cantidad");
            dtItems.Columns.Add("valor_item");
            dtItems.Columns.Add("valor_recargo");
            dtItems.Columns.Add("valor_iva");
            dtItems.Columns.Add("total");
            dtItems.Columns.Add("id_producto");
            dtItems.Columns.Add("paga_iva");

            DataRow row = dtItems.NewRow();
            row["cantidad"] = "1";
            row["valor_item"] = dbPrecioUnitario_P;
            row["valor_recargo"] = "0";
            row["valor_iva"] = "0";
            row["total"] = "0";
            row["id_producto"] = iIdProductoTarjeta;
            row["paga_iva"] = iPagaIva;
            dtItems.Rows.Add(row);

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
                    //lblTotal.Text = "$ " + dTotalDebido.ToString("N2");
                }

                dbValorRecuperado = dTotalDebido;
                dbCambio = 0;
                cobro.Close();
                crearComanda();
            }
        }

        private void rdbVigentes_CheckedChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
            }
        }

        private void frmCreacionTarjetaAlmuerzo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
