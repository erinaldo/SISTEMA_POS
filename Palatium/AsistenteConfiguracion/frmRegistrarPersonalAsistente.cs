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
    public partial class frmRegistrarPersonalAsistente : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;
        string sTabla;
        string sCampo;
        public string sCodigo;
        public string sDescripcion;

        string[] sDatosMaximo = new string[5];

        bool bRespuesta;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        int iOp;
        public int iIdRegistro;

        long iMaximo;

        //iOp
        //1. Cajeros (pos_cajero)
        //2. Meseros (pos_mesero)
        //3. Promotores (pos_promotor)
        //4. Reprtidores (pos_repartidor)

        public frmRegistrarPersonalAsistente(int iOp_P)
        {
            this.iOp = iOp_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            dbAyudaPersonal.limpiar();
            txtClaveAcceso.Clear();
            chkContrasena.Checked = false;
            chkPermisos.Checked = false;
            txtCodigo.Focus();
        }

        //FUNCION PARA CONSULTAR EL CODIGO DE LOS REGISTROS PARA DUPLICADOS
        private int consultarCodigo()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;

                if (iOp == 1)
                    sSql += "from pos_cajero" + Environment.NewLine;
                else if (iOp == 2)
                    sSql += "from pos_mesero" + Environment.NewLine;
                else if (iOp == 3)
                    sSql += "from pos_promotor" + Environment.NewLine;
                else if (iOp == 4)
                    sSql += "from pos_repartidor" + Environment.NewLine;

                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@codigo";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtCodigo.Text.Trim();

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

        //FUNCION PARA CONSULTAR EL CODIGO DE LOS REGISTROS PARA DUPLICADOS
        private int consultarClaveAcceso(int iVer_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;

                if (iVer_P == 1)
                    sSql += "from pos_cajero" + Environment.NewLine;
                else if (iVer_P == 2)
                    sSql += "from pos_mesero" + Environment.NewLine;
                else if (iVer_P == 3)
                    sSql += "from pos_promotor" + Environment.NewLine;
                else if (iVer_P == 4)
                    sSql += "from pos_repartidor" + Environment.NewLine;

                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and claveacceso = @claveacceso";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@claveacceso";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtClaveAcceso.Text.Trim();

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

        //FUNCION PARA GUARDAR EL REGISTRO
        private void insertarRegistro()
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
                if (iOp == 1)
                    sSql += "insert into pos_cajero (" + Environment.NewLine;
                else if (iOp == 2)
                    sSql += "insert into pos_mesero (" + Environment.NewLine;
                else if (iOp == 3)
                    sSql += "insert into pos_promotor (" + Environment.NewLine;
                else if (iOp == 4)
                    sSql += "insert into pos_repartidor (" + Environment.NewLine;

                sSql += "id_persona, codigo, descripcion, claveacceso," + Environment.NewLine;  
                
                if (iOp == 1)
                    sSql += "administracion, "; 
                 
                sSql += "is_active, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_persona, @codigo, @descripcion, @claveacceso," + Environment.NewLine;

                if (iOp == 1)
                    sSql += "1, ";

                sSql += "@is_active, @estado, getdate(), @usuario_ingreso, @terminal_ingreso)";

                int i = 0;
                parametro = new SqlParameter[8];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_persona";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dbAyudaPersonal.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@codigo";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtCodigo.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descripcion";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtDescripcion.Text.Trim().ToUpper();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@claveacceso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtClaveAcceso.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@is_active";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 1;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[2];
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[0];
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[1];

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //OBTENER EL MAXIMO ID
                if (iOp == 1)
                {
                    sTabla = "pos_cajero";
                    sCampo = "id_pos_cajero";
                }

                else if (iOp == 2)
                {
                    sTabla = "pos_mesero";
                    sCampo = "id_pos_mesero";
                }

                else if (iOp == 3)
                {
                    sTabla = "pos_promotor";
                    sCampo = "id_pos_promotor";
                }

                else if (iOp == 4)
                {
                    sTabla = "pos_repartidor";
                    sCampo = "id_pos_repartidor";
                }

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", sDatosMaximo);

                if (iMaximo == -1)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return;
                }

                iIdRegistro = Convert.ToInt32(iMaximo);

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El registro se ha guardado con éxito.";
                ok.ShowDialog();

                this.DialogResult = DialogResult.OK;
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

        //LLENAR DBAYUDA
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion, apellidos + ' ' + isnull(nombres, '') nombres " + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dbAyudaPersonal.Ver(sSql, "identificacion", 0, 1, 2);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        #endregion

        private void frmRegistrarPersonalAsistente_Load(object sender, EventArgs e)
        {
            if (iOp == 1)
                chkPermisos.Visible = true;
            else
                chkPermisos.Visible = false;

            if (iOp == 1)
                this.Text = "Registro de Personal - Cajeros";
            else if (iOp == 2)
                this.Text = "Registro de Personal - Meseros";
            else if (iOp == 3)
                this.Text = "Registro de Personal - Promotores";
            else if (iOp == 4)
                this.Text = "Registro de Personal - Repartidores";

            llenarSentencia();

            sDatosMaximo[0] = "ADMINISTRADOR";
            sDatosMaximo[1] = Environment.MachineName.ToString();
            sDatosMaximo[2] = "A";

            this.ActiveControl = txtCodigo;
        }

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            int iCuenta_P;
            int iRespuesta;

            if (txtCodigo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el código para el registro.";
                ok.ShowDialog();
                txtCodigo.Focus();
                return;
            }

            if (txtDescripcion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la descripción para el registro.";
                ok.ShowDialog();
                txtDescripcion.Focus();
                return;
            }

            if (dbAyudaPersonal.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el registro de persona para asignar al registro.";
                ok.ShowDialog();
                return;
            }

            if (txtClaveAcceso.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la clave de acceso para el registro.";
                ok.ShowDialog();
                txtClaveAcceso.Focus();
                return;
            }

            iCuenta_P = consultarCodigo();

            if (iCuenta_P == -1)
                return;

            if (iCuenta_P > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El código ingresado ya pertenece a otro registro. Favor ingrese otro código.";
                ok.ShowDialog();
                txtCodigo.Clear();
                txtCodigo.Focus();
                return;
            }

            iCuenta_P = 0;

            for (int i = 1; i <= 4; i++)
            {
                iRespuesta = consultarClaveAcceso(i);

                if (iRespuesta == -1)
                    return;

                iCuenta_P += iRespuesta;

                if (iCuenta_P > 0)
                    break;
            }

            if (iCuenta_P > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La clave de acceso ingresada ya pertenece a otro registro. Favor ingrese otra clave.";
                ok.ShowDialog();
                txtClaveAcceso.Clear();
                txtClaveAcceso.Focus();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                sCodigo = txtCodigo.Text.Trim();
                sDescripcion = txtDescripcion.Text.Trim();
                insertarRegistro();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegistrarPersonalAsistente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
