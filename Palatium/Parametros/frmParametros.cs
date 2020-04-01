using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Parametros
{
    public partial class frmParametros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseCargarParametros parametros = new Clases.ClaseCargarParametros();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;

        int iIdParametro = 0;

        int iLeerMesero = 0;
        int iImprimeOrden = 0;
        int iManejaServicio = 0;
        int iFacturacionElectronica = 0;
        int iDescripcionMesa = 0;
        int iHabilitarDecimales = 0;
        int iSeleccionMesero = 0;
        int iVistaPreviaImpresiones = 0;
        int iUsuarioLogin = 0;
        int iTecladoTouch = 0;
        int iVesionDemo = 0;
        int iUsarReceta = 0;
        int iAnimacionMesas = 0;
        int iRISE = 0;

        //int iProductoModificador;
        //int iProductoDomicilio;
        //int iProductoNuevoItem;

        public frmParametros()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE TIPO DE COMPROBANTES
        private void llenarComboPComprobantes()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                sSql = "";
                sSql += "select idtipocomprobante, descripcion" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbTipoComprobante.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private bool verificarCampos()
        {
            if (txtIva.Text == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un valor de IVA.";
                ok.ShowDialog();
                txtIva.Focus();
                return false;
            }

            else if (txtIce.Text == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un valor de ICE.";
                ok.ShowDialog();
                txtIce.Focus();
                return false;
            }

            else if (txtEmpleados.Text == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un valor para el porcentaje de descuento para empleados.";
                ok.ShowDialog();
                txtEmpleados.Focus();
                return false;
            }

            else if (txtTamanoLetraMesa.Text == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el tamaño de letra para la sección de mesas. Puede ingresar un valor entre 12 y 20.";
                ok.ShowDialog();
                txtTamanoLetraMesa.Focus();
                return false;
            }

            else if (txtServicio.Text == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el porcentaje de servicio.";
                ok.ShowDialog();
                txtServicio.Focus();
                return false;
            }

            else if ((Convert.ToDouble(txtTamanoLetraMesa.Text.Trim()) < 12) && (Convert.ToDouble(txtTamanoLetraMesa.Text.Trim()) < 40))
            {
                ok.lblMensaje.Text = "El número de fuente no puede ser menor a 12 y mayor a 40.";
                ok.ShowDialog();
                txtTamanoLetraMesa.Text = "12";
                txtTamanoLetraMesa.Focus();
                return false;
            }

            else if (txtTelefono.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el número de teléfono del fabricante del sistema.";
                ok.ShowDialog();
                txtTelefono.Focus();
                return false;
            }

            else if (txtSitioWeb.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el sitio web del fabricante del sistema.";
                ok.ShowDialog();
                txtSitioWeb.Focus();
                return false;
            }

            else if (txtNumeroPersonas.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el número de personas para mesas.";
                ok.ShowDialog();
                txtNumeroPersonas.Focus();
                return false;
            }

            else if (txtCorreoElectronicoDefault.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un correo electrónico default para el sistema.";
                ok.ShowDialog();
                txtCorreoElectronicoDefault.Focus();
                return false;
            }


            else if (Convert.ToInt32(this.cmbTipoComprobante.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione el tipo de comprobante para Notas de Entrega.";
                ok.ShowDialog();
                cmbTipoComprobante.Focus();
                return false;
            }

            else
            {
                return true;
            }
        }

        //FUNCION PARA INSERTAR
        private void insertarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_parametro (" + Environment.NewLine;
                sSql += "id_producto_modificador, id_producto_domicilio, id_producto_item," + Environment.NewLine;
                sSql += "iva, ice, servicio, descuento_empleados, leer_mesero," + Environment.NewLine;
                sSql += "imprimir_orden, maneja_servicio, maneja_facturacion_electronica," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "etiqueta_mesa,tamano_letra_mesa, configuracion_decimales," + Environment.NewLine;
                sSql += "codigo_modificador, logo, seleccion_mesero, vista_previa_impresion," + Environment.NewLine;
                sSql += "opcion_login, habilitar_teclado_touch, demo, descarga_receta,)" + Environment.NewLine;
                sSql += "contacto_fabricante, sitio_web_fabricante, animacion_mesas," + Environment.NewLine;
                sSql += "url_contabilidad, rise, numero_personas_default, ruta_reportes," + Environment.NewLine;
                sSql += "idtipocomprobante, correo_electronico_default)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += dBAyudaModificadores.iId + ", " + dBAyudaMovilizacion.iId + ", " + dBAyudaNuevoItem.iId + "," + Environment.NewLine;
                sSql += Convert.ToDouble(txtIva.Text) + ", " + Convert.ToDouble(txtIce.Text) + "," + Environment.NewLine;
                sSql += Convert.ToDouble(txtServicio.Text) + ", " + Convert.ToDouble(txtEmpleados.Text) + "," + Environment.NewLine;
                sSql += iLeerMesero + ", " + iImprimeOrden + ", " + iManejaServicio + "," + Environment.NewLine;
                sSql += iFacturacionElectronica + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += iDescripcionMesa + ", " + txtTamanoLetraMesa.Text + ", " + iHabilitarDecimales + "," + Environment.NewLine;
                sSql += "'" + dBAyudaModificadores.txtIdentificacion.Text.Trim() + "', '" + txtRuta.Text.Trim() + "', " + iSeleccionMesero + "," + Environment.NewLine;
                sSql += iVistaPreviaImpresiones + ", " + iUsuarioLogin + ", " + iTecladoTouch + ", " + iVesionDemo + ", " + iUsarReceta + Environment.NewLine;
                sSql += "'" + txtTelefono.Text.Trim() + "', '" + txtSitioWeb.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += iAnimacionMesas + ", '" + txtUrlContable.Text.Trim() + "', " + iRISE + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtNumeroPersonas.Text.Trim()) + ", '" + txtUrlContable.Text.Trim() + "'," + Environment.NewLine;
                sSql += cmbTipoComprobante.SelectedValue + ", '" + txtCorreoElectronicoDefault.Text.Trim().ToLower() + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado éxitosamente." + Environment.NewLine + "Se han aplicado las nuevas configuraciones al sistema.";
                ok.ShowDialog();
                //Application.Restart();

                string sMensaje = parametros.cargarParametros();

                if (sMensaje != "")
                {
                    catchMensaje.lblMensaje.Text = sMensaje;
                    catchMensaje.ShowDialog();
                }

                return;
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro set" + Environment.NewLine;
                sSql += "id_producto_modificador = " + dBAyudaModificadores.iId + "," + Environment.NewLine;
                sSql += "id_producto_domicilio = " + dBAyudaMovilizacion.iId + "," + Environment.NewLine;
                sSql += "id_producto_item = " + dBAyudaNuevoItem.iId + "," + Environment.NewLine;
                sSql += "iva = " + Convert.ToDouble(txtIva.Text) + ", " + Environment.NewLine;
                sSql += "ice = " + Convert.ToDouble(txtIce.Text) + "," + Environment.NewLine;
                sSql += "servicio = " + Convert.ToDouble(txtServicio.Text) + "," + Environment.NewLine;
                sSql += "descuento_empleados = " + Convert.ToDouble(txtEmpleados.Text) + "," + Environment.NewLine;
                sSql += "leer_mesero = " + iLeerMesero + "," + Environment.NewLine;
                sSql += "imprimir_orden = " + iImprimeOrden + "," + Environment.NewLine;
                sSql += "maneja_servicio = " + iManejaServicio + "," + Environment.NewLine;
                sSql += "maneja_facturacion_electronica = " + iFacturacionElectronica + "," + Environment.NewLine;
                sSql += "etiqueta_mesa = " + iDescripcionMesa + "," + Environment.NewLine;
                sSql += "tamano_letra_mesa = " + txtTamanoLetraMesa.Text + "," + Environment.NewLine;
                sSql += "configuracion_decimales = " + iHabilitarDecimales + "," + Environment.NewLine;
                sSql += "codigo_modificador = '" + dBAyudaModificadores.txtIdentificacion.Text + "'," + Environment.NewLine;
                sSql += "logo = '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "seleccion_mesero = " + iSeleccionMesero + "," + Environment.NewLine;
                sSql += "vista_previa_impresion = " + iVistaPreviaImpresiones + "," + Environment.NewLine;
                sSql += "opcion_login = " + iUsuarioLogin + "," + Environment.NewLine;
                sSql += "habilitar_teclado_touch = " + iTecladoTouch + "," + Environment.NewLine;
                sSql += "demo = " + iVesionDemo + "," + Environment.NewLine;
                sSql += "descarga_receta = " + iUsarReceta + "," + Environment.NewLine;
                sSql += "contacto_fabricante = '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql += "sitio_web_fabricante = '" + txtSitioWeb.Text.Trim() + "'," + Environment.NewLine;
                sSql += "animacion_mesas = " + iAnimacionMesas + "," + Environment.NewLine;
                sSql += "url_contabilidad = '" + txtUrlContable.Text.Trim() + "'," + Environment.NewLine;
                sSql += "rise = " + iRISE + "," + Environment.NewLine;
                sSql += "numero_personas_default = " + Convert.ToInt32(txtNumeroPersonas.Text.Trim()) + "," + Environment.NewLine;
                sSql += "ruta_reportes = '" + txtUrlReportes.Text.Trim() + "'," + Environment.NewLine;
                sSql += "idtipocomprobante = " + cmbTipoComprobante.SelectedValue + "," + Environment.NewLine;
                sSql += "correo_electronico_default = '" + txtCorreoElectronicoDefault.Text.Trim().ToLower() + "'" + Environment.NewLine;
                sSql += "where id_pos_parametro = " + iIdParametro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                //ok.lblMensaje.Text = "Registro actualizado éxitosamente." + Environment.NewLine + "La aplicación se reiniciará para que se guarden los cambios.";
                ok.lblMensaje.Text = "Registro actualizado éxitosamente." + Environment.NewLine + "Se han aplicado las nuevas configuraciones al sistema.";
                ok.ShowDialog();
                //Application.Restart();

                string sMensaje = parametros.cargarParametros();

                if (Program.iFacturacionElectronica == 1)
                {
                    parametros.cargarParametrosFacturacionElectronica();
                }

                if (sMensaje != "")
                {
                    catchMensaje.lblMensaje.Text = sMensaje;
                    catchMensaje.ShowDialog();
                }

                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboPComprobantes();
            iIdParametro = 0;
            txtIva.Text = "0";
            txtIce.Text = "0";
            txtServicio.Text = "0";
            txtEmpleados.Text = "0";
            txtTelefono.Clear();
            txtSitioWeb.Clear();
            txtIva.Focus();
            txtNumeroPersonas.Text = "1";
            txtUrlReportes.Clear();
            txtUrlContable.Clear();
        }

        //FUNCION PARA CARGAR LOS PARÁMETROS
        private void cargarParametros()
        {
            try
            {
                sSql = "select * from pos_vw_parametros";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdParametro = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        if (Convert.ToInt32(dtConsulta.Rows[0][1].ToString()) != 0)
                        {
                            dBAyudaModificadores.iId = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                            dBAyudaModificadores.txtIdentificacion.Text = dtConsulta.Rows[0][2].ToString();
                            dBAyudaModificadores.txtDatos.Text = dtConsulta.Rows[0][3].ToString();
                            dBAyudaModificadores.sCodigo = dtConsulta.Rows[0][2].ToString();
                            dBAyudaModificadores.sNombre = dtConsulta.Rows[0][3].ToString();
                        }

                        if (Convert.ToInt32(dtConsulta.Rows[0][4].ToString()) != 0)
                        {
                            dBAyudaMovilizacion.iId = Convert.ToInt32(dtConsulta.Rows[0][4].ToString());
                            dBAyudaMovilizacion.txtIdentificacion.Text = dtConsulta.Rows[0][5].ToString();
                            dBAyudaMovilizacion.txtDatos.Text = dtConsulta.Rows[0][6].ToString();
                            dBAyudaMovilizacion.sCodigo = dtConsulta.Rows[0][5].ToString();
                            dBAyudaMovilizacion.sNombre = dtConsulta.Rows[0][6].ToString();
                        }

                        if (Convert.ToInt32(dtConsulta.Rows[0][7].ToString()) != 0)
                        {
                            dBAyudaNuevoItem.iId = Convert.ToInt32(dtConsulta.Rows[0][7].ToString());
                            dBAyudaNuevoItem.txtIdentificacion.Text = dtConsulta.Rows[0][8].ToString();
                            dBAyudaNuevoItem.txtDatos.Text = dtConsulta.Rows[0][9].ToString();
                            dBAyudaNuevoItem.sCodigo = dtConsulta.Rows[0][8].ToString();
                            dBAyudaNuevoItem.sNombre = dtConsulta.Rows[0][9].ToString();
                        }

                        txtIva.Text = dtConsulta.Rows[0][10].ToString();
                        txtIce.Text = dtConsulta.Rows[0][11].ToString();
                        txtServicio.Text = dtConsulta.Rows[0][12].ToString();
                        txtEmpleados.Text = dtConsulta.Rows[0][13].ToString();

                        iLeerMesero = Convert.ToInt32(dtConsulta.Rows[0][14].ToString());
                        iImprimeOrden = Convert.ToInt32(dtConsulta.Rows[0][15].ToString());
                        iManejaServicio = Convert.ToInt32(dtConsulta.Rows[0][16].ToString());
                        iFacturacionElectronica = Convert.ToInt32(dtConsulta.Rows[0][17].ToString());                        
                        iDescripcionMesa = Convert.ToInt32(dtConsulta.Rows[0][18].ToString());
                        txtTamanoLetraMesa.Text = dtConsulta.Rows[0][19].ToString();
                        iHabilitarDecimales = Convert.ToInt32(dtConsulta.Rows[0][20].ToString());
                        //txtCodigoModificador.Text = dtConsulta.Rows[0][12].ToString();
                        txtRuta.Text = dtConsulta.Rows[0][21].ToString();
                        iSeleccionMesero = Convert.ToInt32(dtConsulta.Rows[0][22].ToString());
                        iVistaPreviaImpresiones = Convert.ToInt32(dtConsulta.Rows[0][23].ToString());
                        iUsuarioLogin = Convert.ToInt32(dtConsulta.Rows[0][24].ToString());
                        iTecladoTouch = Convert.ToInt32(dtConsulta.Rows[0][25].ToString());
                        iVesionDemo = Convert.ToInt32(dtConsulta.Rows[0][26].ToString());
                        iUsarReceta = Convert.ToInt32(dtConsulta.Rows[0][27].ToString());
                        txtTelefono.Text = dtConsulta.Rows[0][28].ToString();
                        txtSitioWeb.Text = dtConsulta.Rows[0][29].ToString();
                        txtUrlContable.Text = dtConsulta.Rows[0][31].ToString();
                        iAnimacionMesas = Convert.ToInt32(dtConsulta.Rows[0][30].ToString());
                        iRISE = Convert.ToInt32(dtConsulta.Rows[0][32].ToString());
                        txtNumeroPersonas.Text = dtConsulta.Rows[0][33].ToString();
                        txtUrlReportes.Text = dtConsulta.Rows[0][34].ToString();
                        cmbTipoComprobante.SelectedValue = dtConsulta.Rows[0]["idtipocomprobante"].ToString();
                        txtCorreoElectronicoDefault.Text = dtConsulta.Rows[0]["correo_electronico_default"].ToString();

                        if (iLeerMesero == 1)
                        {
                            chkLeerMesero.Checked = true;
                        }

                        else
                        {
                            chkLeerMesero.Checked = false;
                        }

                        if (iImprimeOrden == 1)
                        {
                            chkImprimirOrden.Checked = true;
                        }

                        else
                        {
                            chkImprimirOrden.Checked = false;
                        }

                        if (iManejaServicio == 1)
                        {
                            chkManejaServicio.Checked = true;
                            txtServicio.Enabled = true;
                        }

                        else
                        {
                            chkManejaServicio.Checked = false;
                            txtServicio.Text = "0";
                            txtServicio.Enabled = false;
                        }

                        if (iFacturacionElectronica == 1)
                        {
                            chkFacturacionElectronica.Checked = true;
                        }

                        else
                        {
                            chkFacturacionElectronica.Checked = false;
                        }

                        if (iDescripcionMesa == 1)
                        {
                            chkMostrarNombreMesa.Checked = true;
                        }

                        else
                        {
                            chkMostrarNombreMesa.Checked = false;
                        }

                        if (iHabilitarDecimales == 1)
                        {
                            chkHabilitarDecimal.Checked = true;
                        }

                        else
                        {
                            chkHabilitarDecimal.Checked = false;
                        }

                        if (iSeleccionMesero == 1)
                        {
                            chkSeleccionMesero.Checked = true;
                        }

                        else
                        {
                            chkSeleccionMesero.Checked = false;
                        }

                        if (iVistaPreviaImpresiones == 1)
                        {
                            chkVistaPrevia.Checked = true;
                        }

                        else
                        {
                            chkVistaPrevia.Checked = false;
                        }

                        if (iUsuarioLogin == 1)
                        {
                            chkUsuariosLogin.Checked = true;
                        }

                        else
                        {
                            chkUsuariosLogin.Checked = false;
                        }

                        if (iTecladoTouch == 1)
                        {
                            chkTeclado.Checked = true;
                        }

                        else
                        {
                            chkTeclado.Checked = false;
                        }

                        if (iVesionDemo == 1)
                        {
                            chkDemo.Checked = true;
                        }

                        else
                        {
                            chkDemo.Checked = false;
                        }

                        if (iUsarReceta == 1)
                        {
                            chkRecetas.Checked = true;
                        }

                        else
                        {
                            chkRecetas.Checked = false;
                        }

                        if (iAnimacionMesas == 1)
                        {
                            chkDisenioMesas.Checked = true;
                        }

                        else
                        {
                            chkDisenioMesas.Checked = false;
                        }

                        if (iRISE == 1)
                        {
                            chkRise.Checked = true;
                        }

                        else
                        {
                            chkRise.Checked = false;
                        }

                        txtIva.Focus();
                    }

                    else
                    {
                        limpiar();
                    }
                }

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION  PARA LLENAR LOS DBAYUDA
        private void llenarDbAyuda()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre = (" + Environment.NewLine;
                sSql += "select id_producto " + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '2'" + Environment.NewLine;
                sSql += "and estado = 'A')" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and P.modificador = 1" + Environment.NewLine;
                sSql += "and P.nivel = 2";

                dBAyudaModificadores.Ver(sSql, "P.nombre", 0, 1, 2);

                sSql = "";
                sSql += "select P.id_producto as ID_PRODUCTO, P.codigo as CODIGO, NP.nombre as NOMBRE" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where nivel = 2" + Environment.NewLine;
                sSql += "and otros = 1" + Environment.NewLine;
                sSql += "and estado = 'A')" + Environment.NewLine;
                sSql += "and P.nivel = 3" + Environment.NewLine;
                sSql += "order by P.codigo";

                dBAyudaMovilizacion.Ver(sSql, "P.nombre", 0, 1, 2);
                dBAyudaNuevoItem.Ver(sSql, "P.nombre", 0, 1, 2);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void txtIce_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cargarParametros();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (verificarCampos() == true)
                {
                    NuevoSiNo.lblMensaje.Text = "¿Está seguro de guardar la información?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        if (chkLeerMesero.Checked == true)
                        {
                            iLeerMesero = 1;
                        }

                        else
                        {
                            iLeerMesero = 0;
                        }

                        if (chkImprimirOrden.Checked == true)
                        {
                            iImprimeOrden = 1;
                        }

                        else
                        {
                            iImprimeOrden = 0;
                        }

                        if (chkManejaServicio.Checked == true)
                        {
                            iManejaServicio = 1;
                        }

                        else
                        {
                            iManejaServicio = 0;
                        }

                        if (chkFacturacionElectronica.Checked == true)
                        {
                            iFacturacionElectronica = 1;
                        }

                        else
                        {
                            iFacturacionElectronica = 0;
                        }

                        if (chkMostrarNombreMesa.Checked == true)
                        {
                            iDescripcionMesa = 1;
                        }

                        else
                        {
                            iDescripcionMesa = 0;
                        }

                        if (chkHabilitarDecimal.Checked == true)
                        {
                            iHabilitarDecimales = 1;
                        }

                        else
                        {
                            iHabilitarDecimales = 0;
                        }

                        if (chkSeleccionMesero.Checked == true)
                        {
                            iSeleccionMesero = 1;
                        }

                        else
                        {
                            iSeleccionMesero = 0;
                        }

                        if (chkVistaPrevia.Checked == true)
                        {
                            iVistaPreviaImpresiones = 1;
                        }

                        else
                        {
                            iVistaPreviaImpresiones = 0;
                        }

                        if (chkUsuariosLogin.Checked == true)
                        {
                            iUsuarioLogin = 1;
                        }

                        else
                        {
                            iUsuarioLogin = 0;
                        }

                        if (chkTeclado.Checked == true)
                        {
                            iTecladoTouch = 1;
                        }

                        else
                        {
                            iTecladoTouch = 0;
                        }

                        if (chkDemo.Checked == true)
                        {
                            iVesionDemo = 1;
                        }

                        else
                        {
                            iVesionDemo = 0;
                        }

                        if (chkRecetas.Checked == true)
                        {
                            iUsarReceta = 1;
                        }

                        else
                        {
                            iUsarReceta = 0;
                        }

                        if (chkDisenioMesas.Checked == true)
                        {
                            iAnimacionMesas = 1;
                        }

                        else
                        {
                            iAnimacionMesas = 0;
                        }

                        if (chkRise.Checked == true)
                        {
                            iRISE = 1;
                        }

                        else
                        {
                            iRISE = 0;
                        }

                        //INSERTAR O ACTUALIZAR
                        if (iIdParametro == 0)
                        {
                            insertarRegistro();
                        }

                        else
                        {
                            actualizarRegistro();
                        }
                    }
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmParametros_Load(object sender, EventArgs e)
        {
            llenarDbAyuda();
            llenarComboPComprobantes();
            cargarParametros();
        }

        private void chkManejaServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManejaServicio.Checked == true)
            {
                txtServicio.Enabled = true;
            }

            else
            {
                txtServicio.Text = "0";
                txtServicio.Enabled = false;
            }
        }

        private void chkMostrarNombreMesa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarNombreMesa.Checked == true)
            {
                txtTamanoLetraMesa.Enabled = false;
                txtTamanoLetraMesa.Text = "12";
            }

            else
            {
                txtTamanoLetraMesa.Enabled = true;
                txtTamanoLetraMesa.Clear();
                txtTamanoLetraMesa.Focus();
            }

        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
        }

        private void txtTamanoLetraMesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnExaminarContable_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos exe (*.exe;)|*.exe;";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtUrlContable.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnRemoverContable_Click(object sender, EventArgs e)
        {
            txtUrlContable.Clear();
        }

        private void txtNumeroPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnExaminarReportes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog abrir = new FolderBrowserDialog();
            
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtUrlReportes.Text = abrir.SelectedPath;
            }

        }

        private void btnRemoverReportes_Click(object sender, EventArgs e)
        {
            txtUrlReportes.Clear();
        }
    }
}
