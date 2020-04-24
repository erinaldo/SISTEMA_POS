using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmOrigenOrden : Form
    {
        //VARIABLES, INSTANCIAS
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoOk ok;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter;

        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;

        int iDelivery;
        int iRepartidor;
        int iGeneraFactura;
        int iIdOrigenOrden;
        int iIdManejaServicio;
        int iServicioConsulta;
        int iCuentaPorCobrar;
        int iPagoAnticipado;
        int iHabilitado;

        SqlParameter[] parametro;

        public frmOrigenOrden()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR EL CODIGO
        private int contarRegistro(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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

        //FUNCION PARA CONSULTAR SI ESTÁ ACTIVA LA OPCION  DE SERVICIO
        private void consultarServicio()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(maneja_servicio, 0) maneja_servicio" + Environment.NewLine;
                sSql += "from pos_parametro" + Environment.NewLine;
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

                if (dtConsulta.Rows.Count > 0)
                {
                    iServicioConsulta = Convert.ToInt32(dtConsulta.Rows[0]["maneja_servicio"].ToString());

                    if (iServicioConsulta == 1)
                        chkManejaServicio.Visible = true;
                    else
                        chkManejaServicio.Visible = false;
                }

                else
                    iServicioConsulta = 0;
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DBAYUDA
        private bool llenarDbAyuda(int iIdPersona)
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, ltrim(apellidos + ' ' + isnull(nombres, '')) as apellidos, identificacion" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and id_persona = @id_persona";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_persona";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdPersona;

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

                if (dtConsulta.Rows.Count > 0)
                {
                    dbAyudaPersona.iId = iIdPersona;
                    dbAyudaPersona.txtDatos.Text = dtConsulta.Rows[0]["apellidos"].ToString();
                    dbAyudaPersona.txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString();
                }

                else
                {
                    dbAyudaPersona.iId = 0;
                    dbAyudaPersona.txtDatos.Clear();
                    dbAyudaPersona.txtIdentificacion.Clear();
                }

                return true;
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //Función para llenar LA SENTENDIA DEL DBAYUDA
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, ltrim(apellidos + ' ' + isnull(nombres,'')) as apellidos, identificacion" + Environment.NewLine;
                sSql += "from tp_personas where estado = 'A'";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                dbAyudaPersona.Ver(sSql, "identificacion", 0, 2, 1);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        }

        //LLENAR EL COMBO DELIVERY
        private void llenarComboDelivery()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_modo_delivery, descripcion" + Environment.NewLine;
                sSql += "from pos_modo_delivery" + Environment.NewLine;
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
                row["id_pos_modo_delivery"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbModoDelivery.DisplayMember = "descripcion";
                cmbModoDelivery.ValueMember = "id_pos_modo_delivery";
                cmbModoDelivery.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBO DELIVERY
        private void llenarComboPagos()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where estado = 'A'";

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
                row["id_pos_tipo_forma_cobro"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbFormasCobros.DisplayMember = "descripcion";
                cmbFormasCobros.ValueMember = "id_pos_tipo_forma_cobro";
                cmbFormasCobros.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            dbAyudaPersona.limpiar();
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPorcentajeRecargo.Text = "0";
            llenarComboDelivery();
            llenarComboPagos();

            chkManejaServicio.Checked = false;
            chkDelivery.Checked = false;
            chkGeneraFactura.Checked = false;
            chkRepartidorExterno.Checked = false;
            chkPagoAnticipado.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

            llenarGrid();
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int iCantidad = 1;

                sSql = "";
                sSql += "select id_pos_origen_orden, genera_factura, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery, repartidor_externo," + Environment.NewLine;
                sSql += "isnull(id_pos_tipo_forma_cobro, 0) id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "isnull(id_persona, 0) id_persona, maneja_servicio," + Environment.NewLine;
                sSql += "cuenta_por_cobrar, pago_anticipado, is_active," + Environment.NewLine;
                sSql += "isnull(porcentaje_incremento_delivery, 0) porcentaje_incremento_delivery," + Environment.NewLine;
                sSql += "codigo, descripcion, case is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "and codigo like '%@buscar%'" + Environment.NewLine;
                    sSql += "or descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by codigo";

                parametro = new SqlParameter[iCantidad];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                if (iCantidad == 2)
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
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_origen_orden"].ToString(),
                                      dtConsulta.Rows[i]["genera_factura"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_modo_delivery"].ToString(),
                                      dtConsulta.Rows[i]["presenta_opcion_delivery"].ToString(),
                                      dtConsulta.Rows[i]["repartidor_externo"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_tipo_forma_cobro"].ToString(),
                                      dtConsulta.Rows[i]["id_persona"].ToString(),
                                      dtConsulta.Rows[i]["maneja_servicio"].ToString(),
                                      dtConsulta.Rows[i]["cuenta_por_cobrar"].ToString(),
                                      dtConsulta.Rows[i]["pago_anticipado"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["porcentaje_incremento_delivery"].ToString(),
                                      dtConsulta.Rows[i]["codigo"].ToString(),
                                      dtConsulta.Rows[i]["descripcion"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString()
                        );
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

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }
                
                sSql = "";
                sSql += "insert into pos_origen_orden (" + Environment.NewLine;
                sSql += "codigo, descripcion, genera_factura, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery, repartidor_externo, id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "id_persona, maneja_servicio, cuenta_por_cobrar, pago_anticipado," + Environment.NewLine;
                sSql += "porcentaje_incremento_delivery, is_active, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@codigo, @descripcion, @genera_factura, @id_pos_modo_delivery," + Environment.NewLine;
                sSql += "@presenta_opcion_delivery, @repartidor_externo, @id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "@id_persona, @maneja_servicio, @cuenta_por_cobrar, @pago_anticipado," + Environment.NewLine;
                sSql += "@porcentaje_incremento_delivery, @is_active, @estado," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[16];
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
                parametro[a].ParameterName = "@genera_factura";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iGeneraFactura;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_modo_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbModoDelivery.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@presenta_opcion_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDelivery;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@repartidor_externo";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iRepartidor;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_forma_cobro";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbFormasCobros.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_persona";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = dbAyudaPersona.iId;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@maneja_servicio";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdManejaServicio;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cuenta_por_cobrar";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iCuentaPorCobrar;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@pago_anticipado";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagoAnticipado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@porcentaje_incremento_delivery";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = Convert.ToDecimal(txtPorcentajeRecargo.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
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

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
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

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_origen_orden set" + Environment.NewLine;
                sSql += "descripcion = @descripcion," + Environment.NewLine;
                sSql += "genera_factura = @genera_factura," + Environment.NewLine;
                sSql += "id_persona = @id_persona," + Environment.NewLine;
                sSql += "id_pos_modo_delivery = @id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery = @presenta_opcion_delivery," + Environment.NewLine;
                sSql += "repartidor_externo = @repartidor_externo," + Environment.NewLine;
                sSql += "id_pos_tipo_forma_cobro = @id_pos_tipo_forma_cobro," + Environment.NewLine;                
                sSql += "maneja_servicio = @maneja_servicio," + Environment.NewLine;
                sSql += "cuenta_por_cobrar = @cuenta_por_cobrar," + Environment.NewLine;
                sSql += "pago_anticipado = @pago_anticipado," + Environment.NewLine;               
                sSql += "porcentaje_incremento_delivery = @porcentaje_incremento_delivery," + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = @id_pos_origen_orden" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[14];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@descripcion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@genera_factura";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iGeneraFactura;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_persona";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = dbAyudaPersona.iId;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_modo_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbModoDelivery.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@presenta_opcion_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDelivery;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@repartidor_externo";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iRepartidor;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_forma_cobro";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbFormasCobros.SelectedValue);
                a++;                

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@maneja_servicio";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdManejaServicio;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cuenta_por_cobrar";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iCuentaPorCobrar;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@pago_anticipado";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagoAnticipado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@porcentaje_incremento_delivery";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = Convert.ToDecimal(txtPorcentajeRecargo.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_origen_orden";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdOrigenOrden;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";

                #endregion

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
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

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_origen_orden set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = @id_pos_origen_orden" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[14];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_origen_orden";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdOrigenOrden;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";

                #endregion

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro eliminado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
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

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["repartidor_externo"].Value) == 0)
                    dbAyudaPersona.limpiar();
                else
                {
                    if (llenarDbAyuda(Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_persona"].Value)) == false)
                        return;
                }

                iIdOrigenOrden = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_origen_orden"].Value);

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["genera_factura"].Value) == 1)
                {
                    chkGeneraFactura.Checked = true;
                    grupoPago.Enabled = false;
                }

                else
                {
                    chkGeneraFactura.Checked = false;
                    grupoPago.Enabled = true;
                }

                cmbModoDelivery.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_modo_delivery"].Value);

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["presenta_opcion_delivery"].Value) == 1)
                    chkDelivery.Checked = true;
                else
                    chkDelivery.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["repartidor_externo"].Value) == 1)
                    chkRepartidorExterno.Checked = true;
                else
                    chkRepartidorExterno.Checked = false;

                cmbFormasCobros.SelectedValue = dgvDatos.CurrentRow.Cells["id_pos_tipo_forma_cobro"].Value;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["maneja_servicio"].Value.ToString()) == 1)
                    chkManejaServicio.Checked = true;
                else
                    chkManejaServicio.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["cuenta_por_cobrar"].Value.ToString()) == 1)
                    chkCuentaPorCobrar.Checked = true;
                else
                    chkCuentaPorCobrar.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["pago_anticipado"].Value.ToString()) == 1)
                    chkPagoAnticipado.Checked = true;
                else
                    chkPagoAnticipado.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active"].Value.ToString()) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                txtPorcentajeRecargo.Text = dgvDatos.CurrentRow.Cells["porcentaje_incremento_delivery"].Value.ToString();
                txtCodigo.Text = dgvDatos.CurrentRow.Cells["codigo"].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["descripcion"].Value.ToString();

                grupoDatos.Enabled = true;
                chkHabilitado.Enabled = true;
                btnNuevo.Text = "Actualizar";
                btnAnular.Enabled = true;
                txtCodigo.Enabled = false;

                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void chkGerneraFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeneraFactura.Checked == true)
            {
                dbAyudaPersona.limpiar();
                grupoPago.Enabled = false;
            }

            else
            {
                dbAyudaPersona.limpiar();
                grupoPago.Enabled = true;
            }
        }

        private void chkCuentaPorCobrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCuentaPorCobrar.Checked == true)
                chkPagoAnticipado.Checked = false;
        }

        private void chkPagoAnticpado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagoAnticipado.Checked == true)
                chkCuentaPorCobrar.Checked = false;
        }

        private void chkRepartidorExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRepartidorExterno.Checked == true)
            {
                grupoDelivery.Enabled = true;
            }

            else
            {
                grupoDelivery.Enabled = false;
                dbAyudaPersona.limpiar();
                txtPorcentajeRecargo.Text = "0";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnAnular.Enabled = false;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
                return;
            }

            if (chkGeneraFactura.Checked == true)
                iGeneraFactura = 1;
            else
                iGeneraFactura = 0;

            if (chkDelivery.Checked == true)
                iDelivery = 1;
            else
                iDelivery = 0;

            if (chkRepartidorExterno.Checked == true)
                iRepartidor = 1;
            else
                iRepartidor = 0;

            if (chkManejaServicio.Checked == true)
                iIdManejaServicio = 1;
            else
                iIdManejaServicio = 0;

            if (chkCuentaPorCobrar.Checked == true)
                iCuentaPorCobrar = 1;
            else
                iCuentaPorCobrar = 0;

            if (chkPagoAnticipado.Checked == true)
                iPagoAnticipado = 1;
            else
                iPagoAnticipado = 0;

            if (chkHabilitado.Checked == true)
                iHabilitado = 1;
            else
                iHabilitado = 0;

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

            if (btnNuevo.Text == "Guardar")
            {
                int iCuenta = contarRegistro(txtCodigo.Text.Trim());

                if (iCuenta == -1)
                    return;

                if (iCuenta > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El código ingresado ya se encuentra registrado.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                    insertarRegistro();
            }

            else if (btnNuevo.Text == "Actualizar")
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                SiNo.ShowDialog();
                
                if (SiNo.DialogResult == DialogResult.OK)
                    actualizarRegistro();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Esta seguro que desea inhabilitar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void frmOrigenOrden_Load(object sender, EventArgs e)
        {
            llenarSentencias();
            llenarComboDelivery();
            llenarComboPagos();
            llenarGrid();
            consultarServicio();
        }

        private void txtPorcentajeRecargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter = new Clases.ClaseValidarCaracteres();
            caracter.soloDecimales(sender, e, 2);
        }
    }
}
