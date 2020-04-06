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

namespace Palatium.Parametros
{
    public partial class frmNuevoParametro : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseCargarParametros parametros = new Clases.ClaseCargarParametros();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        SqlParameter[] parametro;

        string sSql;

        DataTable dtConsulta;
        DataTable dtAyuda;
        
        bool bRespuesta;

        int iIdParametro;
        int iBanderaTab;
        int iManejaServicio;
        int iEtiquetaMesa;
        int iOpcionLogin;
        int iManejaNomina;
        int iIncluirImpuesto;

        public frmNuevoParametro()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE TIPO DE COMPROBANTES
        private void llenarComboComprobantes()
        {
            try
            {
                dtAyuda = new DataTable();
                dtAyuda.Clear();

                sSql = "";
                sSql += "select idtipocomprobante, descripcion" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbTipoComprobante.llenar(dtAyuda, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LA TABLA DE LOCALIDADES
        private void cargarParametros()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_parametros";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        //FUNCION PARA ENVIAR EL PARAMETRO A LAS FUNCIONES
        private void enviarParametro()
        {
            if (iBanderaTab == 1)
                cargarTabPorcentajes();

            if (iBanderaTab == 2)
                cargarTabValoresDefault();

            if (iBanderaTab == 3)
                cargarTabParametros();
        }

        //FUNCION PARA VALIDAR LOS DATOS ANTES DE ENVIAR A LA BASE DE DATOS
        private void verificarDatos()
        {
            if (iBanderaTab == 1)
            {
                if (txtIva.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el porcentaje del IVA para trabajar en el sistema.";
                    ok.ShowDialog();
                    txtIva.Text = "0";
                    txtIva.Focus();
                    return;
                }

                if (txtIce.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el porcentaje del ICE para trabajar en el sistema.";
                    ok.ShowDialog();
                    txtIce.Text = "0";
                    txtIce.Focus();
                    return;
                }

                if (txtPorcentajeServicio.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el porcentaje del servicio para trabajar en el sistema.";
                    ok.ShowDialog();
                    txtPorcentajeServicio.Text = "0";
                    txtPorcentajeServicio.Focus();
                    return;
                }
            }

            if (iBanderaTab == 2)
            {
                if (dBAyudaModificadores.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el ítem del producto de modificadores.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaMovilizacion.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el ítem del producto de movilización.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaNuevoItem.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el ítem del producto de para crear nuevos ítems.";
                    ok.ShowDialog();
                    return;
                }
            }

            else if (iBanderaTab == 3)
            {
                if (txtTelefono.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el número de teléfono del fabricante del sistema.";
                    ok.ShowDialog();
                    txtTelefono.Focus();
                    return;
                }

                if (txtSitioWeb.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el sitio web del fabricante del sistema.";
                    ok.ShowDialog();
                    txtSitioWeb.Focus();
                    return;
                }

                if (Convert.ToInt32(this.cmbTipoComprobante.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de comprobante para Notas de Entrega.";
                    ok.ShowDialog();
                    cmbTipoComprobante.Focus();
                    return;
                }
            }

            enviarCambiosBDD();
        }

        private void enviarCambiosBDD()
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar los cambios?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (iBanderaTab == 1)
                {
                    if (chkManejaServicio.Checked == true)
                        iManejaServicio = 1;
                    else
                        iManejaServicio = 0;

                    if (chkMostrarNombreMesa.Checked == true)
                        iEtiquetaMesa = 1;
                    else
                        iEtiquetaMesa = 0;

                    if (chkUsuariosLogin.Checked == true)
                        iOpcionLogin = 1;
                    else
                        iOpcionLogin = 0;

                    if (chkNomina.Checked == true)
                        iManejaNomina = 1;
                    else
                        iManejaNomina = 0;

                    if (chkIncluirImpuestos.Checked == true)
                        iIncluirImpuesto = 1;
                    else
                        iIncluirImpuesto = 0;

                    actualizarTabPorcentajes();
                }

                else if (iBanderaTab == 2)
                {
                    actualizarValoresSistema();
                }

                else if (iBanderaTab == 3)
                {
                    actualizarTabParametros();
                }
            }
        }

        #endregion

        #region FUNCIONES DEL TAB DE PORCENTAJES

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabPorcentajes()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdParametro = 0;
                    txtIva.Text = "0";
                    txtIce.Text ="0";

                    chkManejaServicio.Checked = false;
                    chkUsuariosLogin.Checked = false;
                    chkMostrarNombreMesa.Checked = false;
                    chkNomina.Checked = false;
                    chkIncluirImpuestos.Checked = false;
                    txtPorcentajeServicio.ReadOnly = true;

                    txtPorcentajeServicio.Text = "0";
                }

                else
                {
                    iIdParametro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro"].ToString());

                    txtIva.Text = dtConsulta.Rows[0]["iva"].ToString();
                    txtIce.Text = dtConsulta.Rows[0]["ice"].ToString();
                    txtPorcentajeServicio.Text = dtConsulta.Rows[0]["servicio"].ToString();

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_servicio"].ToString()) == 1)
                    {
                        chkManejaServicio.Checked = true;
                        txtPorcentajeServicio.ReadOnly = false;
                    }
                    else
                    {
                        chkManejaServicio.Checked = false;
                        txtPorcentajeServicio.ReadOnly = true;
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0]["etiqueta_mesa"].ToString()) == 1)
                        chkMostrarNombreMesa.Checked = true;
                    else
                        chkMostrarNombreMesa.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["opcion_login"].ToString()) == 1)
                        chkUsuariosLogin.Checked = true;
                    else
                        chkUsuariosLogin.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_nomina"].ToString()) == 1)
                        chkNomina.Checked = true;
                    else
                        chkNomina.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["precio_incluye_impuesto"].ToString()) == 1)
                        chkIncluirImpuestos.Checked = true;
                    else
                        chkIncluirImpuestos.Checked = false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarTabPorcentajes()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro set" + Environment.NewLine;
                sSql += "iva = " + txtIva.Text.Trim() + "," + Environment.NewLine;
                sSql += "ice = " + txtIce.Text.Trim() + "," + Environment.NewLine;
                sSql += "maneja_servicio = " + iManejaServicio + "," + Environment.NewLine;
                sSql += "servicio = " + txtPorcentajeServicio.Text.Trim() + "," + Environment.NewLine;
                sSql += "etiqueta_mesa = " + iEtiquetaMesa + "," + Environment.NewLine;
                sSql += "opcion_login = " + iOpcionLogin + "," + Environment.NewLine;
                sSql += "maneja_nomina = " + iManejaNomina + "," + Environment.NewLine;
                sSql += "precio_incluye_impuesto = " + iIncluirImpuesto + Environment.NewLine;
                sSql += "where id_pos_parametro = " + iIdParametro + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametros();
                cargarParametros();
                enviarParametro();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL TAB DE VALORES DEFAULT

        //FUNCION  PARA CARGAR LOS CONTROLES DB AYUDA
        private void cargarDbAyuda()
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

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabValoresDefault()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdParametro = 0;
                    dBAyudaModificadores.limpiar();
                    dBAyudaMovilizacion.limpiar();
                    dBAyudaNuevoItem.limpiar();
                }

                else
                {
                    iIdParametro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro"].ToString());

                    dBAyudaModificadores.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_modificador"].ToString());
                    dBAyudaModificadores.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_modificador"].ToString();
                    dBAyudaModificadores.txtDatos.Text = dtConsulta.Rows[0]["nombre_modificador"].ToString();
                    dBAyudaModificadores.sCodigo = dtConsulta.Rows[0]["codigo_modificador"].ToString();
                    dBAyudaModificadores.sNombre = dtConsulta.Rows[0]["nombre_modificador"].ToString();

                    dBAyudaMovilizacion.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_domicilio"].ToString());
                    dBAyudaMovilizacion.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_domicilio"].ToString();
                    dBAyudaMovilizacion.txtDatos.Text = dtConsulta.Rows[0]["nombre_domicilio"].ToString();
                    dBAyudaMovilizacion.sCodigo = dtConsulta.Rows[0]["codigo_domicilio"].ToString();
                    dBAyudaMovilizacion.sNombre = dtConsulta.Rows[0]["nombre_domicilio"].ToString();

                    dBAyudaNuevoItem.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_item"].ToString());
                    dBAyudaNuevoItem.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_item"].ToString();
                    dBAyudaNuevoItem.txtDatos.Text = dtConsulta.Rows[0]["nombre_item"].ToString();
                    dBAyudaNuevoItem.sCodigo = dtConsulta.Rows[0]["codigo_item"].ToString();
                    dBAyudaNuevoItem.sNombre = dtConsulta.Rows[0]["nombre_item"].ToString();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarValoresSistema()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro set" + Environment.NewLine;
                sSql += "id_producto_modificador = " + dBAyudaModificadores.iId + "," + Environment.NewLine;
                sSql += "id_producto_domicilio = " + dBAyudaMovilizacion.iId + "," + Environment.NewLine;
                sSql += "id_producto_item = " + dBAyudaNuevoItem.iId + Environment.NewLine;
                sSql += "where id_pos_parametro = " + iIdParametro + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametros();
                cargarParametros();
                enviarParametro();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL TAB DE PARAMETROS

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabParametros()
        {
            try
            {
                llenarComboComprobantes();

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdParametro = 0;

                    txtTelefono.Clear();
                    txtSitioWeb.Clear();
                    txtUrlContable.Clear();

                    cmbTipoComprobante.SelectedValue = 0;
                }

                else
                {
                    iIdParametro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro"].ToString());

                    txtTelefono.Text = dtConsulta.Rows[0]["contacto_fabricante"].ToString();
                    txtSitioWeb.Text = dtConsulta.Rows[0]["sitio_web_fabricante"].ToString();
                    txtUrlContable.Text = dtConsulta.Rows[0]["url_contabilidad"].ToString();

                    cmbTipoComprobante.SelectedValue = dtConsulta.Rows[0]["idtipocomprobante"].ToString();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarTabParametros()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro set" + Environment.NewLine;
                sSql += "contacto_fabricante = @contacto_fabricante," + Environment.NewLine;
                sSql += "sitio_web_fabricante = @sitio_web_fabricante," + Environment.NewLine;
                sSql += "url_contabilidad = @url_contabilidad," + Environment.NewLine;
                sSql += "idtipocomprobante = @idtipocomprobante" + Environment.NewLine;
                sSql += "where id_pos_parametro = @id_pos_parametro" + Environment.NewLine;
                sSql += "and estado = 'A'";

                parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@contacto_fabricante";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtTelefono.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@sitio_web_fabricante";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = txtSitioWeb.Text.Trim();

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@url_contabilidad";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = txtUrlContable.Text.Trim();

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@idtipocomprobante";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = cmbTipoComprobante.SelectedValue;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@id_pos_parametro";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = iIdParametro;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametros();
                cargarParametros();
                enviarParametro();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmNuevoParametro_Load(object sender, EventArgs e)
        {
            cargarParametros();
            iBanderaTab = 1;
            enviarParametro();
        }

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedTab == tbControl.TabPages["tabPorcentajes"])
            {
                iBanderaTab = 1;
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabValores"])
            {
                iBanderaTab = 2;
                cargarDbAyuda();
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabParametros"])
            {
                iBanderaTab = 3;
                enviarParametro();
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            verificarDatos();
        }

        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtIce_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPorcentajeServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtIva_Leave(object sender, EventArgs e)
        {
            if (txtIva.Text.Trim() == "")
            {
                txtIva.Text = "0";
            }
        }

        private void txtIce_Leave(object sender, EventArgs e)
        {
            if (txtIce.Text.Trim() == "")
            {
                txtIce.Text = "0";
            }
        }

        private void txtPorcentajeServicio_Leave(object sender, EventArgs e)
        {
            if (txtPorcentajeServicio.Text.Trim() == "")
            {
                txtPorcentajeServicio.Text = "0";
            }
        }

        private void chkManejaServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManejaServicio.Checked == true)
            {
                txtPorcentajeServicio.Text = dtConsulta.Rows[0]["servicio"].ToString();
                txtPorcentajeServicio.ReadOnly = false;
                txtPorcentajeServicio.Focus();
            }

            else
            {
                txtPorcentajeServicio.Text = "0";
                txtPorcentajeServicio.ReadOnly = true;
                chkMostrarNombreMesa.Focus();
            }
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }        
    }
}
