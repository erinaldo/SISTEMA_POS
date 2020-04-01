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
    public partial class frmMantenimientoTarjetaAlmuerzo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        bool bRespuesta;
        bool bActualizar;
        bool bActualizarParametro;

        int iIdRegistro;
        int iIdRegistroParametro;
        int iActive;

        DataTable dtConsulta;

        public frmMantenimientoTarjetaAlmuerzo()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO TAB CANTIDADES

        //FUNCION PARA LIMPIAR
        private void limpiarCantidades()
        {
            obtenerCodigoCantidades();
            txtCantidadNominal.Text = "1";
            txtCantidadReal.Text = "1";
            chkEstado.Checked = true;
            chkEstado.Enabled = false;
            btnGrabarCantidades.Text = "Nuevo";
            bActualizar = false;
            iActive = 0;
            llenarGridCantidades();
        }

        //FUNCION PARA LLENAR GRID
        private void llenarGridCantidades()
        {
            try
            {
                dgvDatosCantidades.Rows.Clear();

                sSql = "";
                sSql += "select id_pos_tar_cantidad_almuerzo, is_active, codigo, cantidad_nominal, cantidad_real," + Environment.NewLine;
                sSql += "case is_active when 1 then 'HABILITADO' else 'INHABILITADO' end estado" + Environment.NewLine;
                sSql += "from pos_tar_cantidad_almuerzo" + Environment.NewLine;
                sSql += "where estado = 'A'";

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
                    dgvDatosCantidades.Rows.Add(
                                                dtConsulta.Rows[i]["id_pos_tar_cantidad_almuerzo"].ToString(),
                                                dtConsulta.Rows[i]["is_active"].ToString(),
                                                dtConsulta.Rows[i]["codigo"].ToString(),
                                                dtConsulta.Rows[i]["cantidad_nominal"].ToString(),
                                                dtConsulta.Rows[i]["cantidad_real"].ToString(),
                                                dtConsulta.Rows[i]["estado"].ToString()
                        );
                }

                dgvDatosCantidades.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER EL CODIGO AUTOMATICO
        private bool obtenerCodigoCantidades()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 isnull(codigo, 0) codigo" + Environment.NewLine;
                sSql += "from pos_tar_cantidad_almuerzo" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_tar_cantidad_almuerzo desc";

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
                    txtCodigoCantidad.Text = "1";
                else
                {
                    int iValor = Convert.ToInt32(dtConsulta.Rows[0]["codigo"].ToString());
                    txtCodigoCantidad.Text = (iValor + 1).ToString();
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

        //FUNCION PARA INSERTAR UN REGISTRO 
        private void insertarRegistroCantidades()
        {
            try
            {
                if (obtenerCodigoCantidades() == false)
                {
                    return;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_tar_cantidad_almuerzo (" + Environment.NewLine;
                sSql += "codigo, cantidad_nominal, cantidad_real, is_active, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@codigo, @cantidad_nominal, @cantidad_real, @is_active, @estado," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                SqlParameter[] parametro = new SqlParameter[7];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@codigo";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtCodigoCantidad.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@cantidad_nominal";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Convert.ToInt32(txtCantidadNominal.Text);

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@cantidad_real";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Convert.ToInt32(txtCantidadReal.Text);

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@is_active";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = 1;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@estado";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = "A";

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@usuario_ingreso";
                parametro[5].SqlDbType = SqlDbType.VarChar;
                parametro[5].Value = Program.sDatosMaximo[0];

                parametro[6] = new SqlParameter();
                parametro[6].ParameterName = "@terminal_ingreso";
                parametro[6].SqlDbType = SqlDbType.VarChar;
                parametro[6].Value = Program.sDatosMaximo[0];

                //EJECUTAR LA INSTRUCCIÓN SQL
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
                ok.lblMensaje.Text = "Registro agregado éxitosamente.";
                ok.ShowDialog();
                limpiarCantidades();
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

        //FUNCION PARA ACTUALIZAR UN REGISTRO 
        private void actualizarRegistroCantidades()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_tar_cantidad_almuerzo set" + Environment.NewLine;
                sSql += "cantidad_nominal = @cantidad_nominal," + Environment.NewLine;
                sSql += "cantidad_real = @cantidad_real," + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_tar_cantidad_almuerzo = @id_pos_tar_cantidad_almuerzo" + Environment.NewLine;
                sSql += "and estado = @estado";

                SqlParameter[] parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@cantidad_nominal";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Convert.ToInt32(txtCantidadNominal.Text);

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@cantidad_real";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Convert.ToInt32(txtCantidadReal.Text);

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@is_active";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iActive;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@id_pos_tar_cantidad_almuerzo";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = iIdRegistro;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@estado";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = "A";

                //EJECUTAR LA INSTRUCCIÓN SQL
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarCantidades();
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


        #region FUNCIONES DEL USUARIO TAB PARAMETROS

        //FUNCION PARA LIMPIAR EL TAB DE PARAMETROS
        private void limpiarParametros()
        {
            llenarComboCantidades();
            llenarComboTarjetas();
            llenarComboItemTarjeta();
            llenarGridParametros();

            cmbTipoTarjetaAlmuerzo.Enabled = false;
            cmbItemTarjetaAlmuerzo.Enabled = false;
            cmbRegistroCantidades.Enabled = false;

            iActive = 0;

            btnGrabarParametros.Text = "Nuevo";
            chkHabilitadoParametros.Enabled = false;
            chkHabilitadoParametros.Checked = true;
            bActualizarParametro = false;
        }

        //LLENAR EL COMBOBOX DE CANTIDADES
        private void llenarComboCantidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tar_cantidad_almuerzo, 'CANTIDAD NOMINAL: ' + convert(varchar(10), cantidad_nominal)" + Environment.NewLine;
                sSql += "+ ' - CANTIDAD REAL: ' + convert(varchar(10), cantidad_real) valor" + Environment.NewLine;
                sSql += "from pos_tar_cantidad_almuerzo" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";

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
                row["id_pos_tar_cantidad_almuerzo"] = "0";
                row["valor"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbRegistroCantidades.DisplayMember = "valor";
                cmbRegistroCantidades.ValueMember = "id_pos_tar_cantidad_almuerzo";
                cmbRegistroCantidades.DataSource = dtConsulta;

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE PRODUCTOS TARJETAS
        private void llenarComboTarjetas()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto, nombre_producto" + Environment.NewLine;
                sSql += "from pos_vw_tar_producto_tarjetas_regalo" + Environment.NewLine;
                sSql += "where maneja_tarjeta_almuerzo = 1";

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
                row["id_producto"] = "0";
                row["nombre_producto"] = "Seleccione Tarjeta";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoTarjetaAlmuerzo.DisplayMember = "nombre_producto";
                cmbTipoTarjetaAlmuerzo.ValueMember = "id_producto";
                cmbTipoTarjetaAlmuerzo.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE CANTIDADES
        private void llenarComboItemTarjeta()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto, nombre_producto" + Environment.NewLine;
                sSql += "from pos_vw_tar_producto_tarjetas_regalo" + Environment.NewLine;
                sSql += "where maneja_item_tarjeta_almuerzo = 1";

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
                row["id_producto"] = "0";
                row["nombre_producto"] = "Seleccione ítem de Tarjeta";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbItemTarjetaAlmuerzo.DisplayMember = "nombre_producto";
                cmbItemTarjetaAlmuerzo.ValueMember = "id_producto";
                cmbItemTarjetaAlmuerzo.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR GRID DE PARAMETROS
        private void llenarGridParametros()
        {
            try
            {
                dgvDatosParametros.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_ta_cantidad_tipo_almuerzo";

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
                    dgvDatosParametros.Rows.Add(
                                                dtConsulta.Rows[i]["id_pos_tar_cantidad_tipo_almuerzo"].ToString(),
                                                dtConsulta.Rows[i]["id_pos_tar_cantidad_almuerzo"].ToString(),
                                                dtConsulta.Rows[i]["id_producto_tarjeta"].ToString(),
                                                dtConsulta.Rows[i]["id_producto_descarga"].ToString(),
                                                dtConsulta.Rows[i]["is_active"].ToString(),
                                                dtConsulta.Rows[i]["cantidad_nominal"].ToString(),
                                                dtConsulta.Rows[i]["cantidad_real"].ToString(),
                                                dtConsulta.Rows[i]["item_tarjeta"].ToString(),
                                                dtConsulta.Rows[i]["item_producto"].ToString(),
                                                dtConsulta.Rows[i]["estado"].ToString()
                        );
                }

                dgvDatosParametros.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR UN REGISTRO 
        private void insertarRegistroParametros()
        {
            try
            {
                if (obtenerCodigoCantidades() == false)
                {
                    return;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_tar_cantidad_tipo_almuerzo (" + Environment.NewLine;
                sSql += "id_pos_tar_cantidad_almuerzo, id_producto_tarjeta, id_producto_descarga, is_active," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_pos_tar_cantidad_almuerzo, @id_producto_tarjeta, @id_producto_descarga, @is_active," + Environment.NewLine;
                sSql += "@estado, getdate(), @usuario_ingreso, @terminal_ingreso)";

                SqlParameter[] parametro = new SqlParameter[7];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_cantidad_almuerzo";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Convert.ToInt32(cmbRegistroCantidades.SelectedValue);

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto_tarjeta";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Convert.ToInt32(cmbTipoTarjetaAlmuerzo.SelectedValue);

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_producto_descarga";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Convert.ToInt32(cmbItemTarjetaAlmuerzo.SelectedValue);

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@is_active";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = 1;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@estado";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = "A";

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@usuario_ingreso";
                parametro[5].SqlDbType = SqlDbType.VarChar;
                parametro[5].Value = Program.sDatosMaximo[0];

                parametro[6] = new SqlParameter();
                parametro[6].ParameterName = "@terminal_ingreso";
                parametro[6].SqlDbType = SqlDbType.VarChar;
                parametro[6].Value = Program.sDatosMaximo[0];

                //EJECUTAR LA INSTRUCCIÓN SQL
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
                ok.lblMensaje.Text = "Registro agregado éxitosamente.";
                ok.ShowDialog();
                limpiarParametros();
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

        //FUNCION PARA ACTUALIZAR UN REGISTRO 
        private void actualizarRegistroParametros()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_tar_cantidad_tipo_almuerzo set" + Environment.NewLine;
                sSql += "id_pos_tar_cantidad_almuerzo = @id_pos_tar_cantidad_almuerzo," + Environment.NewLine;
                sSql += "id_producto_tarjeta = @id_producto_tarjeta," + Environment.NewLine;
                sSql += "id_producto_descarga = @id_producto_descarga," + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_tar_cantidad_tipo_almuerzo = @id_pos_tar_cantidad_tipo_almuerzo" + Environment.NewLine;
                sSql += "and estado = @estado";

                SqlParameter[] parametro = new SqlParameter[6];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_tar_cantidad_almuerzo";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Convert.ToInt32(cmbRegistroCantidades.SelectedValue);

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto_tarjeta";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Convert.ToInt32(cmbTipoTarjetaAlmuerzo.SelectedValue);

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_producto_descarga";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Convert.ToInt32(cmbItemTarjetaAlmuerzo.SelectedValue);

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@is_active";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = iActive;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@id_pos_tar_cantidad_tipo_almuerzo";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = iIdRegistroParametro;

                parametro[5] = new SqlParameter();
                parametro[5].ParameterName = "@estado";
                parametro[5].SqlDbType = SqlDbType.VarChar;
                parametro[5].Value = "A";

                //EJECUTAR LA INSTRUCCIÓN SQL
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarParametros();
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

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedTab == tbControl.TabPages["tabCantidades"])
            {
                limpiarCantidades();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabProductos"])
            {
                limpiarParametros();
                return;
            }
        }

        private void frmMantenimientoTarjetaAlmuerzo_Load(object sender, EventArgs e)
        {
            limpiarCantidades();
        }

        private void btnLimpiarCantidades_Click(object sender, EventArgs e)
        {
            limpiarCantidades();
        }

        private void btnGrabarCantidades_Click(object sender, EventArgs e)
        {
            if (btnGrabarCantidades.Text == "Nuevo")
            {
                txtCantidadNominal.ReadOnly = false;
                txtCantidadReal.ReadOnly = false;
                chkEstado.Enabled = false;
                chkEstado.Checked = true;
                btnGrabarCantidades.Text = "Guardar";
                txtCantidadNominal.Focus();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (bActualizar == false)
                    insertarRegistroCantidades();

                else
                {
                    if (chkEstado.Checked == true)
                        iActive = 1;
                    else
                        iActive = 0;

                    actualizarRegistroCantidades();
                }                
            }
        }

        private void txtCantidadNominal_Leave(object sender, EventArgs e)
        {
            if (txtCantidadNominal.Text.Trim() == "")
                txtCantidadNominal.Text = "1";

            if (Convert.ToInt32(txtCantidadNominal.Text) <= 0)
                txtCantidadNominal.Text = "1";
        }

        private void txtCantidadReal_Leave(object sender, EventArgs e)
        {
            if (txtCantidadNominal.Text.Trim() == "")
                txtCantidadNominal.Text = "1";

            if (Convert.ToInt32(txtCantidadNominal.Text) <= 0)
                txtCantidadNominal.Text = "1";
        }

        private void txtCantidadNominal_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCantidadReal_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void dgvDatosCantidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatosCantidades.CurrentRow.Cells["id_pos_tar_cantidad_almuerzo"].Value);
                txtCodigoCantidad.Text = dgvDatosCantidades.CurrentRow.Cells["codigo"].Value.ToString().Trim();
                txtCantidadNominal.Text = dgvDatosCantidades.CurrentRow.Cells["cantidad_nominal"].Value.ToString().Trim();
                txtCantidadReal.Text = dgvDatosCantidades.CurrentRow.Cells["cantidad_real"].Value.ToString().Trim();

                if (Convert.ToInt32(dgvDatosCantidades.CurrentRow.Cells["is_active"].Value) == 1)
                    chkEstado.Checked = true;
                else
                    chkEstado.Checked = false;

                chkEstado.Enabled = true;
                bActualizar = true;
                btnGrabarCantidades.Text = "Guardar";
                txtCantidadNominal.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnGrabarParametros_Click(object sender, EventArgs e)
        {
            if (btnGrabarParametros.Text == "Nuevo")
            {
                chkHabilitadoParametros.Enabled = false;
                chkHabilitadoParametros.Checked = true;
                btnGrabarParametros.Text = "Guardar";
                cmbTipoTarjetaAlmuerzo.Enabled = true;
                cmbItemTarjetaAlmuerzo.Enabled = true;
                cmbRegistroCantidades.Enabled = true;
                cmbTipoTarjetaAlmuerzo.Focus();
                return;
            }

            if (Convert.ToInt32(cmbTipoTarjetaAlmuerzo.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el tipo de tarjeta a registrar.";
                ok.ShowDialog();
                cmbTipoTarjetaAlmuerzo.Focus();
                return;
            }

            if (Convert.ToInt32(cmbItemTarjetaAlmuerzo.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el item del producto a descargar de la tarjeta.";
                ok.ShowDialog();
                cmbItemTarjetaAlmuerzo.Focus();
                return;
            }

            if (Convert.ToInt32(cmbRegistroCantidades.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la cantidad de ítems que contendrá la tarjeta.";
                ok.ShowDialog();
                cmbRegistroCantidades.Focus();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (bActualizarParametro == false)
                    insertarRegistroParametros();

                else
                {
                    if (chkHabilitadoParametros.Checked == true)
                        iActive = 1;
                    else
                        iActive = 0;

                    actualizarRegistroParametros();
                }
            }
        }

        private void dgvDatosParametros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistroParametro = Convert.ToInt32(dgvDatosParametros.CurrentRow.Cells["id_pos_tar_cantidad_tipo_almuerzo"].Value);
                cmbTipoTarjetaAlmuerzo.SelectedValue = dgvDatosParametros.CurrentRow.Cells["id_producto_tarjeta"].Value;
                cmbItemTarjetaAlmuerzo.SelectedValue = dgvDatosParametros.CurrentRow.Cells["id_producto_descarga"].Value;
                cmbRegistroCantidades.SelectedValue = dgvDatosParametros.CurrentRow.Cells["id_pos_tar_cantidad_almuerzo_P"].Value;

                if (Convert.ToInt32(dgvDatosParametros.CurrentRow.Cells["is_active_P"].Value) == 1)
                    chkHabilitadoParametros.Checked = true;
                else
                    chkHabilitadoParametros.Checked = false;

                chkHabilitadoParametros.Enabled = true;
                bActualizarParametro = true;

                cmbTipoTarjetaAlmuerzo.Enabled = true;
                cmbItemTarjetaAlmuerzo.Enabled = true;
                cmbRegistroCantidades.Enabled = true;

                btnGrabarParametros.Text = "Guardar";
                cmbTipoTarjetaAlmuerzo.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiarParametros_Click(object sender, EventArgs e)
        {
            limpiarParametros();
        }
    }
}
