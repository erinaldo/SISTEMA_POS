using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class frmFormasPagos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;

        int iLeePropina;
        int iIdFormaCobro;
        int iHabilitado;
        int iMostrarSeccionCobros;

        SqlParameter[] parametro;

        public frmFormasPagos()
        {
            InitializeComponent();
        }

        private void FInformacionPosTipForPag_Load(object sender, EventArgs e)
        {
            llenarGrid();
            llenarComboTipoDocumento();
            llenarComboMetodosPago();
            llenarComboTipoVenta();
        }

        #region FUNCIONES DEL USUARIO

        private string ConvertirImagenToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

       //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            cmbTipoDocumento.SelectedValue = 0;
            cmbMetodoPago.SelectedValue = 0;
            cmbTipoVenta.SelectedValue = 0;

            txtBuscar.Clear();
            txtBase64.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtVisualizarBoton.Clear();
            iHabilitado = 0;
            iIdFormaCobro = 0;
            iLeePropina = 0;
            txtRuta.Clear();

            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            chkPropina.Checked = false;
            chkMostrarSeccionCobros.Checked = false;

            if (imgLogo.Image != null)
            {
                imgLogo.Image.Dispose();
                imgLogo.Image = null;
            }

            llenarGrid();
        }

        //LLENAR COMBO DE TIPO DE IDENTIFICACICON
        private void llenarComboTipoDocumento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, codigo + ' - ' + valor_texto descripcion" + Environment.NewLine;
                sSql += "from tp_vw_tipo_documento_cobro";

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

                DataRow row = dtConsulta.NewRow();
                row["correlativo"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoDocumento.DisplayMember = "descripcion";
                cmbTipoDocumento.ValueMember = "correlativo";
                cmbTipoDocumento.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE METODOS DE PAGO
        private void llenarComboMetodosPago()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_pos_metodo_pago, codigo + ' - ' + descripcion descripcion" + Environment.NewLine;
                sSql += "from pos_metodo_pago" + Environment.NewLine;
                sSql += "where estado = @estado";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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

                DataRow row = dtConsulta.NewRow();
                row["id_pos_metodo_pago"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbMetodoPago.DisplayMember = "descripcion";
                cmbMetodoPago.ValueMember = "id_pos_metodo_pago";
                cmbMetodoPago.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE TIPOS DE VENTA
        private void llenarComboTipoVenta()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_venta, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_venta" + Environment.NewLine;
                sSql += "where estado = @estado";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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

                DataRow row = dtConsulta.NewRow();
                row["id_pos_tipo_venta"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoVenta.DisplayMember = "descripcion";
                cmbTipoVenta.ValueMember = "id_pos_tipo_venta";
                cmbTipoVenta.DataSource = dtConsulta;
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int a = 1;

                sSql = "";
                sSql += "select FC.id_pos_tipo_forma_cobro, isnull(FC.cg_tipo_documento, 0) cg_tipo_documento," + Environment.NewLine;
                sSql += "FC.id_pos_metodo_pago, id_pos_tipo_venta, isnull(FC.is_active, 0) is_active, FC.lee_propina," + Environment.NewLine;
                sSql += "isnull(FC.mostrar_seccion_cobros, 0) mostrar_seccion_cobros, FC.codigo, FC.descripcion," + Environment.NewLine;
                sSql += "isnull(DC.codigo + ' - ' + DC.valor_texto, 'NINGUN REGISTRO') tipo_documento," + Environment.NewLine;
                sSql += "case FC.lee_propina when 1 then 'SI' else 'NO' end lee_propina," + Environment.NewLine;
                sSql += "case FC.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado," + Environment.NewLine;
                sSql += "isnull(texto_visualizar_boton, '') texto_visualizar_boton" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro AS FC LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "tp_vw_tipo_documento_cobro AS DC ON FC.cg_tipo_documento = DC.correlativo" + Environment.NewLine;
                sSql += "and FC.estado = @estado" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    a++;
                    sSql += "where FC.codigo like '%@buscar%'" + Environment.NewLine;
                    sSql += "OR FC.descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by FC.codigo" + Environment.NewLine;

                parametro = new SqlParameter[a];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                if (a == 2)
                {
                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@buscar";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = txtBuscar.Text.Trim();
                }

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

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_tipo_forma_cobro"].ToString(),
                                      dtConsulta.Rows[i]["cg_tipo_documento"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_metodo_pago"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_tipo_venta"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["lee_propina"].ToString(),
                                      dtConsulta.Rows[i]["mostrar_seccion_cobros"].ToString(),
                                      dtConsulta.Rows[i]["texto_visualizar_boton"].ToString(),
                                      dtConsulta.Rows[i]["codigo"].ToString(),
                                      dtConsulta.Rows[i]["descripcion"].ToString(),
                                      dtConsulta.Rows[i]["tipo_documento"].ToString(),
                                      dtConsulta.Rows[i]["lee_propina"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString());
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONTAR LOS REGISTROS
        private int contarRegistros()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = txtCodigo.Text.Trim();

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA EXTRAER LA IMAGEN DE LA BASE DE DATOS
        private bool extraerImagenBDD(int iIdRegistro_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(imagen_base_64, '') imagen_base_64" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and id_pos_tipo_forma_cobro = @id_pos_tipo_forma_cobro";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_tipo_forma_cobro";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdRegistro_P;

                #endregion

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
                    txtBase64.Text = "";
                    imgLogo.Image = null;
                }

                else
                {
                    txtBase64.Text = dtConsulta.Rows[0]["imagen_base_64"].ToString();

                    if (txtBase64.Text.Trim() == "")
                    {
                        imgLogo.Image = null;
                    }

                    else
                    {
                        byte[] imageBytes;
                        Image foto = null;

                        imageBytes = Convert.FromBase64String(txtBase64.Text.Trim());

                        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                        {
                            foto = Image.FromStream(ms, true);
                        }

                        imgLogo.Image = foto;
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

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_tipo_forma_cobro (" + Environment.NewLine;
                sSql += "codigo, descripcion, lee_propina, id_pos_metodo_pago," + Environment.NewLine;
                sSql += "cg_tipo_documento, imagen_base_64, id_pos_tipo_venta, is_active, " + Environment.NewLine;
                sSql += "mostrar_seccion_cobros, texto_visualizar_boton, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@codigo, @descripcion, @lee_propina, @id_pos_metodo_pago," + Environment.NewLine;
                sSql += "@cg_tipo_documento, @imagen_base_64, @id_pos_tipo_venta, @is_active, " + Environment.NewLine;
                sSql += "@mostrar_seccion_cobros, @texto_visualizar_boton, @estado," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS 

                int a = 0;
                parametro = new SqlParameter[13];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtCodigo.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@descripcion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@lee_propina";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iLeePropina;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_metodo_pago";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbMetodoPago.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_documento";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Convert.ToInt32(cmbTipoDocumento.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@imagen_base_64";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBase64.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_venta";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbTipoVenta.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@mostrar_seccion_cobros";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iMostrarSeccionCobros;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@texto_visualizar_boton";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtVisualizarBoton.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }
                
                sSql = "";
                sSql += "update pos_tipo_forma_cobro set" + Environment.NewLine;
                sSql += "descripcion = @descripcion," + Environment.NewLine;
                sSql += "lee_propina = @lee_propina," + Environment.NewLine;
                sSql += "imagen_base_64 = @imagen_base_64," + Environment.NewLine;
                sSql += "id_pos_metodo_pago = @id_pos_metodo_pago," + Environment.NewLine;
                sSql += "cg_tipo_documento = @cg_tipo_documento," + Environment.NewLine;
                sSql += "id_pos_tipo_venta = @id_pos_tipo_venta," + Environment.NewLine;
                sSql += "is_active = @is_active," + Environment.NewLine;
                sSql += "mostrar_seccion_cobros = @mostrar_seccion_cobros," + Environment.NewLine;
                sSql += "texto_visualizar_boton = @texto_visualizar_boton" + Environment.NewLine;
                sSql += "where id_pos_tipo_forma_cobro = @id_pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@descripcion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@lee_propina";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iLeePropina;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "imagen_base_64";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBase64.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_metodo_pago";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbMetodoPago.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_documento";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbTipoDocumento.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_venta";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbTipoVenta.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@mostrar_seccion_cobros";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iMostrarSeccionCobros;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@texto_visualizar_boton";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtVisualizarBoton.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_forma_cobro";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdFormaCobro;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "update pos_tipo_forma_cobro set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_tipo_forma_cobro = @id_pos_tipo_forma_cobro";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 0;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_tipo_forma_cobro";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdFormaCobro;

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        #endregion

        private void Btn_CerrarPosTipForPag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarPosTipForPag_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void Btn_BuscarPosTipForPag_Click(object sender, EventArgs e)
        {
            try
            {
                llenarGrid();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();

                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
            }
        }

        private void BtnNuevoPosTipForPag_Click(object sender, EventArgs e)
        {
            
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
                return;
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR O ACTUALIZAR
            if ((txtCodigo.Text.Trim() == "") && (txtDescripcion.Text.Trim() == ""))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Debe rellenar todos los campos obligatorios.";
                ok.ShowDialog();
                txtCodigo.Focus();
                return;
            }

            if (txtCodigo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el código del tipo forma de pago.";
                ok.ShowDialog();
                txtCodigo.Focus();
                return;
            }

            if (txtDescripcion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la descripción del tipo forma de pago.";
                ok.ShowDialog();
                txtDescripcion.Focus();
                return;
            }

            if (txtVisualizarBoton.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la descripción a visualizar en el botón.";
                ok.ShowDialog();
                txtVisualizarBoton.Focus();
                return;
            }

            if (Convert.ToInt32(cmbTipoDocumento.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el tipo de documento para el registro.";
                ok.ShowDialog();
                return;
            }

            if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el método de pago para el registro.";
                ok.ShowDialog();
                return;
            }

            if (txtBase64.Text.Trim().Length > 8000)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El ícono para el botón supera el tamaño permitido. Favor seleccione un nuevo ícono.";
                ok.ShowDialog();
                imgLogo.Image = null;
                txtBase64.Clear();
                txtRuta.Clear();
                return;
            }

            if (chkPropina.Checked == true)
                iLeePropina = 1;
            else
                iLeePropina = 0;

            if (chkHabilitado.Checked == true)
                iHabilitado = 1;
            else
                iHabilitado = 0;

            if (chkMostrarSeccionCobros.Checked == true)
                iMostrarSeccionCobros = 1;
            else
                iMostrarSeccionCobros = 0;

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

            if (btnNuevo.Text == "Guardar")
            {
                int iCuenta = contarRegistros();

                if (iCuenta == -1)
                    return;

                if (iCuenta > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El código ingresado ya se encuentra registrado. Favor ingrese un nuevo código.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    insertarRegistro();
                }
            }

            else if (btnNuevo.Text == "Actualizar")
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    actualizarRegistro();
                }
            }
        }

        private void Btn_AnularPosTipForPag_Click(object sender, EventArgs e)
        {
            if (iIdFormaCobro == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningún registro para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
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
                imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                imgLogo.SizeMode = PictureBoxSizeMode.Zoom;
                txtBase64.Text = ConvertirImagenToBase64(imgLogo.Image);
            }

            abrir.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
            txtBase64.Clear();
            imgLogo.Image.Dispose();
            imgLogo.Image = null;
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdFormaCobro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_tipo_forma_cobro"].Value.ToString());

                if (extraerImagenBDD(iIdFormaCobro) == false)
                    return;

                txtCodigo.Text = dgvDatos.CurrentRow.Cells["codigo"].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["descripcion"].Value.ToString();
                txtVisualizarBoton.Text = dgvDatos.CurrentRow.Cells["texto_visualizar_boton"].Value.ToString();
                cmbMetodoPago.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_metodo_pago"].Value.ToString());
                cmbTipoVenta.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_tipo_venta"].Value.ToString());
                cmbTipoDocumento.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells["cg_tipo_documento"].Value.ToString());

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active"].Value.ToString()) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["lee_propina"].Value.ToString()) == 1)
                    chkPropina.Checked = true;
                else
                    chkPropina.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["mostrar_seccion_cobros"].Value.ToString()) == 1)
                    chkMostrarSeccionCobros.Checked = true;
                else
                    chkMostrarSeccionCobros.Checked = false;

                grupoDatos.Enabled = true;
                btnNuevo.Text = "Actualizar";
                txtCodigo.Enabled = false;
                chkHabilitado.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
