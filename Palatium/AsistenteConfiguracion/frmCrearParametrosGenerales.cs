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

namespace Palatium.AsistenteConfiguracion
{
    public partial class frmCrearParametrosGenerales : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

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
        int iManejaServicio;
        int iEtiquetaMesa;
        int iOpcionLogin;
        int iIncluyeImpuesto;
        int iManejaNomina;
        int iUsarIconosCategorias;

        public frmCrearParametrosGenerales()
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    btnGuardar.Visible = true;

                    iIdParametro = 0;
                    txtIva.Text = "0";
                    txtIce.Text = "0";

                    chkManejaServicio.Checked = false;
                    chkUsuariosLogin.Checked = false;
                    chkMostrarNombreMesa.Checked = false;
                    chkNomina.Checked = false;
                    chkIncluirImpuestos.Checked = false;
                    chkUsarIconosCategorias.Checked = false;

                    txtPorcentajeServicio.ReadOnly = true;
                    txtPorcentajeServicio.Text = "0";

                    dBAyudaModificadores.limpiar();
                    dBAyudaMovilizacion.limpiar();
                    dBAyudaNuevoItem.limpiar();

                    txtTelefono.Clear();
                    txtSitioWeb.Clear();
                    txtUrlContable.Clear();

                    cmbTipoComprobante.SelectedValue = 0;
                }

                else
                {
                    btnGuardar.Visible = false;

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

                    if (Convert.ToInt32(dtConsulta.Rows[0]["usar_iconos_categorias"].ToString()) == 1)
                        chkUsarIconosCategorias.Checked = true;
                    else
                        chkUsarIconosCategorias.Checked = false;

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

        //FUNCION PARA INSERTAR EN LA TABLA POS_PARAMETRO
        private void insertarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_parametro (" + Environment.NewLine;
                sSql += "id_producto_modificador, id_producto_domicilio, id_producto_item, iva," + Environment.NewLine;
                sSql += "ice, servicio, maneja_servicio, etiqueta_mesa, opcion_login, contacto_fabricante," + Environment.NewLine;
                sSql += "sitio_web_fabricante, url_contabilidad, precio_incluye_impuesto, maneja_nomina," + Environment.NewLine;
                sSql += "idtipocomprobante, usar_iconos_categorias, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto_modificador, @id_producto_domicilio, @id_producto_item, @iva," + Environment.NewLine;
                sSql += "@ice, @servicio, @maneja_servicio, @etiqueta_mesa, @opcion_login, @contacto_fabricante," + Environment.NewLine;
                sSql += "@sitio_web_fabricante, @url_contabilidad, @precio_incluye_impuesto, @maneja_nomina," + Environment.NewLine;
                sSql += "@idtipocomprobante, @usar_iconos_categorias, @estado, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)";

                int i = 0;

                parametro = new SqlParameter[19];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_producto_modificador";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaModificadores.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_producto_domicilio";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaMovilizacion.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_producto_item";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaNuevoItem.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@iva";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = Convert.ToDecimal(txtIva.Text.Trim());
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@ice";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = Convert.ToDecimal(txtIce.Text.Trim());
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@servicio";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = Convert.ToDecimal(txtPorcentajeServicio.Text.Trim());
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_servicio";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaServicio;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@etiqueta_mesa";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iEtiquetaMesa;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@opcion_login";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iOpcionLogin;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@contacto_fabricante";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtTelefono.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@sitio_web_fabricante";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtSitioWeb.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@url_contabilidad";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtUrlContable.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@precio_incluye_impuesto";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iIncluyeImpuesto;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_nomina";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaNomina;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@idtipocomprobante";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbTipoComprobante.SelectedValue;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usar_iconos_categorias";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iUsarIconosCategorias;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "A";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "ADMINISTRADOR";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = Environment.MachineName.ToString();

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. la aplicación se reiniciará para verificar las siguientes configuraciones.";
                ok.ShowDialog();
                cargarParametros();
                Application.Restart();
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

        private void frmCrearParametrosGenerales_Load(object sender, EventArgs e)
        {
            llenarComboComprobantes();
            cargarDbAyuda();
            cargarParametros();
        }

        private void chkManejaServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManejaServicio.Checked == true)
            {
                if (iIdParametro == 0)
                    txtPorcentajeServicio.Text = "0";
                else
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

        private void btnGuardar_Click(object sender, EventArgs e)
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

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
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
                    iIncluyeImpuesto = 1;
                else
                    iIncluyeImpuesto = 0;

                if (chkUsarIconosCategorias.Checked == true)
                    iUsarIconosCategorias = 1;
                else
                    iUsarIconosCategorias = 0;

                insertarRegistro();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }
    }
}
