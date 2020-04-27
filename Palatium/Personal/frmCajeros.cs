using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Personal
{
    public partial class frmCajeros : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        bool bRespuesta = false;
        
        DataTable dtConsulta;
        
        string sSql;

        int iIdPersona;
        int iIdCajero;
        int iPermisos;
        int iCuenta;
        int iCuentaMeseros;
        int iCuentaCajeros;
        int iHabilitado;

        SqlParameter[] parametro;

        public frmCajeros()
        {
            InitializeComponent();
        }

        private void FInformacionCajero_Load(object sender, EventArgs e)
        {
            llenarGrid();
            llenarSentencia();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion, apellidos + ' ' + isnull(nombres, '') nombres " + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (Program.iManejaNomina == 1)
                {
                    sSql += "and estaenroldepagos = 1" + Environment.NewLine;
                }

                dbAyudaPersonal.Ver(sSql, "identificacion", 0, 1, 2);
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            dbAyudaPersonal.limpiar();
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtClaveAcceso.Clear();
            txtCodigo.Enabled = true;
            chkContrasena.Checked = false;
            chkPermisos.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            iIdCajero = 0;
            iIdPersona = 0;
            iCuenta = 0;
            iHabilitado = 0;
            llenarGrid();
        }

        //Función para comprobar si hay un código repetido
        private int comprobarCodigo()
        {
            int iBandera = 0;
            for (int i = 0; i < dgvCajero.Rows.Count; i++)
            {
                if (txtCodigo.Text == dgvCajero.Rows[i].Cells[2].Value.ToString())
                {
                    iBandera = 1;
                    break;
                }
            }

            return iBandera;
        }

        //FUNCION PARA COMPROBAR LA CLAVE INGRESADA PARA EVITAR DUPLICADOS
        private int devolverConsultaPasswordCajero()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where claveacceso = @claveacceso" + Environment.NewLine;
                sSql += "and estado in (@estado_1, @estado_2)" + Environment.NewLine;
                sSql += "and id_pos_cajero <> @id_pos_cajero";

                #region PARAMETROS

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@claveacceso";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtClaveAcceso.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_1";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_2";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "N";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@id_pos_cajero";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = iIdCajero;

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

        //FUNCION PARA COMPROBAR LA CLAVE INGRESADA PARA EVITAR DUPLICADOS
        private int devolverConsultaPasswordMesero()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_mesero" + Environment.NewLine;
                sSql += "where claveacceso = @claveacceso" + Environment.NewLine;
                sSql += "and estado in (@estado_1, @estado_2)";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@claveacceso";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtClaveAcceso.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_1";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_2";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "N";

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

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvCajero.Rows.Clear();
                int iCantidad = 3;

                sSql = "";
                sSql += "select CAJ.id_pos_cajero, isnull(PER.id_persona,0) id_persona, CAJ.codigo," + Environment.NewLine;
                sSql += "CAJ.descripcion, isnull(CAJ.claveacceso, 0) claveacceso," + Environment.NewLine;
                sSql += "case CAJ.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado," + Environment.NewLine;
                sSql += "isnull(PER.identificacion,' ') identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(PER.nombres, '') + ' ' + PER.apellidos) cajero," + Environment.NewLine;
                sSql += "CAJ.administracion, isnull(CAJ.is_active, 0) is_active" + Environment.NewLine;
                sSql += "from tp_personas PER inner join" + Environment.NewLine;
                sSql += "pos_cajero CAJ on CAJ.id_persona = PER.id_persona" + Environment.NewLine;
                sSql += "and CAJ.estado in (@estado_1, @estado_2)" + Environment.NewLine;
                sSql += "and PER.estado = @estado_3" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "and CAJ.descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by CAJ.codigo";

                #region PARAMETROS

                parametro = new SqlParameter[iCantidad];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "N";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_3";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                if (iCantidad == 4)
                {
                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@buscar";
                    parametro[3].SqlDbType = SqlDbType.VarChar;
                    parametro[3].Value = txtBuscar.Text.Trim();
                }

                #endregion

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
                    dgvCajero.Rows.Add(dtConsulta.Rows[i]["id_pos_cajero"].ToString(),
                                       dtConsulta.Rows[i]["id_persona"].ToString(),
                                       dtConsulta.Rows[i]["codigo"].ToString(),
                                       dtConsulta.Rows[i]["descripcion"].ToString(),
                                       dtConsulta.Rows[i]["claveacceso"].ToString(),
                                       dtConsulta.Rows[i]["estado"].ToString(),
                                       dtConsulta.Rows[i]["identificacion"].ToString(),
                                       dtConsulta.Rows[i]["cajero"].ToString(),
                                       dtConsulta.Rows[i]["administracion"].ToString(),
                                       dtConsulta.Rows[i]["is_active"].ToString());
                }

                dgvCajero.ClearSelection();

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR EL REGISTRO
        private void insertarRegistro()
        {
            try
            {
                iCuentaCajeros = devolverConsultaPasswordCajero();

                if (iCuentaCajeros == -1)
                {
                    txtClaveAcceso.Focus();
                    return;
                }

                if (iCuentaCajeros > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    txtClaveAcceso.Focus();
                    return;
                }

                iCuentaMeseros = devolverConsultaPasswordMesero();

                if (iCuentaMeseros == -1)
                {
                    return;
                }

                if (iCuentaMeseros > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    return;
                }

                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_cajero (" + Environment.NewLine;
                sSql += "id_persona, codigo, descripcion, claveacceso, administracion," + Environment.NewLine;
                sSql += "is_active, huella_dactilar, is_active_huella, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@id_persona, @codigo, @descripcion, @claveacceso, @administracion," + Environment.NewLine;
                sSql += "@is_active, @huella_dactilar, @is_active_huella, @estado," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_persona";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdPersona;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtCodigo.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@descripcion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@claveacceso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtClaveAcceso.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@administracion";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPermisos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@huella_dactilar";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active_huella";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
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

                //EJECUTA LA INSTRUCCION SQL
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
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                iCuentaCajeros = devolverConsultaPasswordCajero();

                if (iCuentaCajeros == -1)
                {
                    txtClaveAcceso.Focus();
                    return;
                }

                if (iCuentaCajeros > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    txtClaveAcceso.Focus();
                    return;
                }

                iCuentaMeseros = devolverConsultaPasswordMesero();

                if (iCuentaMeseros == -1)
                {
                    return;
                }

                if (iCuentaMeseros > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    return;
                }

                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "id_persona = @id_persona," + Environment.NewLine;
                sSql += "codigo = @codigo," + Environment.NewLine;
                sSql += "descripcion = @descripcion," + Environment.NewLine;
                sSql += "claveacceso = @claveacceso," + Environment.NewLine;
                sSql += "administracion = @administracion," + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_cajero = @id_pos_cajero" + Environment.NewLine;
                sSql += "and estado in (@estado_1, @estado_2)";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[9];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_persona";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdPersona;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtCodigo.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@descripcion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@claveacceso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtClaveAcceso.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@administracion";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPermisos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cajero";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdCajero;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_1";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_2";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "N";
                
                #endregion

                //EJECUTA LA INSTRUCCION SQL
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
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
                return;

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_cajero = @id_pos_cajero" + Environment.NewLine;
                sSql += "and estado = @estado";


                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[9];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cajero";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdCajero;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";

                #endregion

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
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

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void chkContrasena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContrasena.Checked == true)
            {
                txtClaveAcceso.PasswordChar = '\0';
                txtClaveAcceso.Focus();
            }
            else
            {
                txtClaveAcceso.PasswordChar = '*';
                txtClaveAcceso.Focus();
            }

            txtClaveAcceso.SelectionStart = txtClaveAcceso.Text.Trim().Length;
        }

        private void dgvCajero_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Grb_DatoCajero.Enabled = true;
                txtCodigo.Enabled = false;
                btnNuevo.Text = "Actualizar";

                iIdCajero = Convert.ToInt32(dgvCajero.CurrentRow.Cells[0].Value.ToString());
                iIdPersona = Convert.ToInt32(dgvCajero.CurrentRow.Cells[1].Value.ToString());
                dbAyudaPersonal.iId = Convert.ToInt32(dgvCajero.CurrentRow.Cells[1].Value.ToString());
                txtCodigo.Text = dgvCajero.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvCajero.CurrentRow.Cells[3].Value.ToString();
                txtClaveAcceso.Text = dgvCajero.CurrentRow.Cells[4].Value.ToString();
                dbAyudaPersonal.txtIdentificacion.Text = dgvCajero.CurrentRow.Cells[6].Value.ToString();
                dbAyudaPersonal.txtDatos.Text = dgvCajero.CurrentRow.Cells[7].Value.ToString();

                iPermisos = Convert.ToInt32(dgvCajero.CurrentRow.Cells[8].Value.ToString());

                if (iPermisos == 0)
                    chkPermisos.Checked = false;
                else
                    chkPermisos.Checked = true;

                iHabilitado = Convert.ToInt32(dgvCajero.CurrentRow.Cells[9].Value.ToString());

                if (iHabilitado == 0)
                    chkHabilitado.Checked = false;
                else
                    chkHabilitado.Checked = true;

                chkHabilitado.Enabled = true;
                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Grb_DatoCajero.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            llenarGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                Grb_DatoCajero.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
                return;
            }

            if (txtCodigo.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el código del cajero.";
                ok.ShowDialog();
                txtCodigo.Focus();
                return;
            }

            if (txtDescripcion.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la descripción del cajero.";
                ok.ShowDialog();
                txtDescripcion.Focus();
                return;
            }

            if (dbAyudaPersonal.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione los datos de la persona.";
                ok.ShowDialog();
                dbAyudaPersonal.Focus();
            }

            if (txtClaveAcceso.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la la clave de acceso para el cajero.";
                ok.ShowDialog();
                txtClaveAcceso.Focus();
                return;
            }

            if (chkPermisos.Checked == true)
                iPermisos = 1;
            else
                iPermisos = 0;

            if (chkHabilitado.Checked == true)
                iHabilitado = 1;
            else
                iHabilitado = 0;

            iIdPersona = dbAyudaPersonal.iId;

            if (btnNuevo.Text == "Guardar")
            {
                iCuenta = comprobarCodigo();

                if (iCuenta == 0)
                {
                    insertarRegistro();
                }

                else if (iCuenta > 1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ya existe un registro con el código ingresado.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }
            }

            else if (btnNuevo.Text == "Actualizar")
            {
                actualizarRegistro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdCajero == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado un registro para verificar la eliminación.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "Esta seguro que desea dar de baja el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
