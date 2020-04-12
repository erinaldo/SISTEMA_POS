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
        Clases_Crear_Comandas.ClaseCrearComanda comanda;
        ValidarCedula validarCedula = new ValidarCedula();

        SqlParameter[] parametro;

        string sSql;
        string sCiudad;
        string sCorreoAyuda;
        string sNumeroLote;
        string sFecha;
        string sDescripcionFormaPago;
        string sEstablecimiento;
        string sPuntoEmision;
        string sNombreTarjeta;
        string sNombreItem;
        string sNumeroComprobante;
        string sCodigoProducto;

        DataTable dtConsulta;
        DataTable dtTarjetas;
        DataTable dtRecargos;
        DataTable dtItems;
        DataTable dtDetalleItems;
        DataTable dtPagos;

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
        int iIdOrigenOrden;
        int iIdPedido;
        int iNumeroPedidoOrden;
        int iIdFactura;
        int iNumeroFactura;
        int iNumeroTarjeta;
        int iCantidadNominal;
        int iCantidadReal;
        int iIdSriFormaPago_P;
        int iIdTipoComprobante;

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
                sTexto += "NÚMERO DE FACTURA: " + sEstablecimiento + "-" + sPuntoEmision + "-" + sNumeroComprobante.PadLeft(9, '0') + Environment.NewLine;
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
                crearReportetarjeta();
                limpiarCrear();

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

                if (iBanderaEfectivoTarjeta == 0)
                {
                    dtItems.Rows.Add(iIdProductoTarjeta, dbPrecioUnitario_P, "1", "0", iPagaIva,
                                     "0", "0", "0", "0", "0", "1", "", "", sCodigoProducto,
                                     sNombreTarjeta, "0", "0");
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

                bRespuesta = comanda.insertarComanda(0, iIdPersona, 0, iIdOrigenOrden, dTotalDebido, "Pagada",
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

                bRespuesta = comanda.insertarTarjetaALmuerzo(Convert.ToInt32(cmbListarTarjetas.SelectedValue), iIdCantidadAlmuerzos,
                                                             iIdPersona, Program.iIdLocalidad, iIdProductoTarjeta, iIdProductoDescarga,
                                                             "", "Vigente", sFecha, Program.sDatosMaximo[0], Program.sDatosMaximo[1],
                                                             Convert.ToInt32(txtCantidadReal.Text), iIdPedido, 1, "Venta", 0, conexion);

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

                bRespuesta = comanda.insertarPagos(iIdPedido, dtPagos, dTotalDebido, dbCambio, dbPropina,
                                                   iIdPersona, sFecha, Program.iIdLocalidad, 0, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

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
        private bool insertarFactura_V2(int iFacturaElectronica_P)
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarFactura(iIdPedido, iIdTipoComprobante, iFacturaElectronica_P,
                                                     iIdPersona, Program.iIdLocalidad, dtPagos, dTotalDebido, 0,
                                                     0, 0, sFecha, conexion);

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
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

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

                dtPagos.Rows.Add(iIdTipoFormaCobro, sDescripcionFormaPago, dTotalDebido, iIdSriFormaPago_P, iConciliacion,
                                 iOperadorTarjeta, iTipoTarjeta, sNumeroLote, iBanderaInsertarLote, dbPropina);

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

        private void frmCreacionTarjetaAlmuerzo_Load(object sender, EventArgs e)
        {
            iIdTipoComprobante = 1;
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
                sCodigoProducto = fila[0][12].ToString().Trim();

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

            DataTable dtItems_P = new DataTable();
            dtItems_P.Columns.Add("cantidad");
            dtItems_P.Columns.Add("valor_item");
            dtItems_P.Columns.Add("valor_recargo");
            dtItems_P.Columns.Add("valor_iva");
            dtItems_P.Columns.Add("total");
            dtItems_P.Columns.Add("id_producto");
            dtItems_P.Columns.Add("paga_iva");
            dtItems_P.Columns.Add("paga_servicio");
            dtItems_P.Columns.Add("valor_servicio");
            dtItems_P.Columns.Add("nombre_producto");
            dtItems_P.Columns.Add("codigo_producto");

            DataRow row = dtItems_P.NewRow();
            row["cantidad"] = "1";
            row["valor_item"] = dbPrecioUnitario_P;
            row["valor_recargo"] = "0";
            row["valor_iva"] = "0";
            row["total"] = "0";
            row["id_producto"] = iIdProductoTarjeta;
            row["paga_iva"] = iPagaIva;
            row["paga_servicio"] = "0";
            row["valor_servicio"] = "0";
            row["nombre_producto"] = sNombreTarjeta;
            row["codigo_producto"] = sCodigoProducto;
            dtItems_P.Rows.Add(row);

            Comida_Rapida.frmCobroRapidoTarjetas cobro = new Comida_Rapida.frmCobroRapidoTarjetas(dTotalDebido, dtItems_P);
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

                if (obtenerDatosFormaPagoRealizada(iIdTipoFormaCobro, "") == false)
                    return;

                crearComandaNueva_V2();
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
