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
using AxZKFPEngXControl;
using System.IO;

namespace Palatium.Registros_Dactilares
{
    public partial class frmLeerHuellas_V2 : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;        
        string sMensajesVer = "";

        DataTable dtConsulta;

        bool bRespuesta;
        bool Check;

        int iIdRegistro;
        int fpcHandle;

        SqlParameter[] parametro;

        Byte[] imagen;

        private AxZKFPEngX lectorHuellas = new AxZKFPEngX();

        public frmLeerHuellas_V2()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private byte[] imagenABytes(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE EMPRESAS
        private void llenarComboEmpresas()
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and CE.estado = @estado_1" + Environment.NewLine;
                sSql += "and TP.estado = @estado_2" + Environment.NewLine;
                sSql += "order by apellidos";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

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

                DataRow row = dtConsulta.NewRow();
                row["id_pos_cliente_empresarial"] = "0";
                row["cliente"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbEmpresas.DisplayMember = "cliente";
                cmbEmpresas.ValueMember = "id_pos_cliente_empresarial";
                cmbEmpresas.DataSource = dtConsulta;
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

                sSql = "";
                sSql += "select E.id_pos_empleado_cliente, E.id_persona, E.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "E.aplica_almuerzo, E.is_active, TP.identificacion, " + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) nombre_empleado," + Environment.NewLine;
                sSql += "case E.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = @estado_1" + Environment.NewLine;
                sSql += "and E.estado in (@estado_2, @estado_3)" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = @id_pos_cliente_empresarial" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and (TP.identificacion like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%@buscar%')" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[5];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_1";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_2";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_3";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cliente_empresarial";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresas.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@buscar";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBuscar.Text.Trim();

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
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_empleado_cliente"].ToString(),
                                      dtConsulta.Rows[i]["id_persona"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_cliente_empresarial"].ToString(),
                                      dtConsulta.Rows[i]["aplica_almuerzo"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["identificacion"].ToString(),
                                      dtConsulta.Rows[i]["nombre_empleado"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString()
                                      );
                }

                lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";
                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbEmpresas.SelectedIndexChanged -= new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbEmpresas.SelectedIndexChanged += new EventHandler(cmbEmpresas_SelectedIndexChanged);
            dgvDatos.Rows.Clear();
            txtBuscar.Clear();
            txtDescripcion.Clear();
            txtBase64_1.Clear();
            imgHuellaCapturada.Image = null;

            btnVerificar.Enabled = false;
            btnRemover.Enabled = false;
            btnGuardar.Enabled = false;
            btnVerificar.Enabled = false;

            //finalizarDispositivo();

            llenarGrid();
            txtBuscar.Focus();
        }

        //ACTUALIZAR LA HUELLA EN EL SISTEMA
        private void registrarHuella()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    return;
                }

                imagen = imagenABytes(imgHuellaCapturada.Image);

                sSql = "";
                sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                sSql += "huella_dactilar = @huella" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @idRegistro" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@huella";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtBase64_1.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@idRegistro";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdRegistro;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                #endregion

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

                finalizarDispositivo();
                limpiar();
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

        //FUNCION PARA RECUPERAR LA HUELLA DACTILAR
        private bool recuperarHuella(int iIdRegistro_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(huella_dactilar, '') huella_dactilar" + Environment.NewLine;
                sSql += "from pos_empleado_cliente" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @id_pos_empleado_cliente" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pos_empleado_cliente";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdRegistro_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

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
                    imgHuellaCapturada.Image = null;
                    txtBase64_1.Text = "";
                }

                else
                {
                    object regTemplateString = dtConsulta.Rows[0]["huella_dactilar"].ToString();
                    int FpId = 1;

                    txtBase64_1.Text = regTemplateString.ToString();
                    fpcHandle = lectorHuellas.CreateFPCacheDB();
                    lectorHuellas.AddRegTemplateStrToFPCacheDB(fpcHandle, FpId, regTemplateString.ToString());

                    Graphics g = imgHuellaCapturada.CreateGraphics();
                    Bitmap bmp = new Bitmap(imgHuellaCapturada.Width, imgHuellaCapturada.Height);
                    g = Graphics.FromImage(bmp);
                    int dc = g.GetHdc().ToInt32();
                    lectorHuellas.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
                    g.Dispose();
                    //imgHuellaCapturada.Image = lectorHuellas.GetFingerImage(regTemplateString);
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

        #endregion

        #region FUNCIONES DEL LECTOR DE HUELLAS

        //FUNCION PARA INICIALIZAR EL DISPOSITIVO
        private void iniciarDispositivo()
        {
            try
            {
                Controls.Add(lectorHuellas);
                imgHuellaCapturada.Image = null;

                lectorHuellas.OnImageReceived += lectorHuellas_OnImageReceived;
                lectorHuellas.OnFeatureInfo += lectorHuellas_OnFeatureInfo;
                lectorHuellas.OnEnroll += lectorHuellas_OnEnroll;

                //int iver = lectorHuellas.InitEngine();

                if (lectorHuellas.InitEngine() == 0)
                {
                    lectorHuellas.FPEngineVersion = "9";
                    lectorHuellas.EnrollCount = 3;
                    mostrarNotificacion("Se ha iniciado las funciones del lector de huellas dactilares.");
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void lectorHuellas_OnImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = imgHuellaCapturada.CreateGraphics();
            Bitmap bmp = new Bitmap(imgHuellaCapturada.Width, imgHuellaCapturada.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            lectorHuellas.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            imgHuellaCapturada.Image = bmp;
        }

        private void lectorHuellas_OnFeatureInfo(object sender, IZKFPEngXEvents_OnFeatureInfoEvent e)
        {

            String strTemp = string.Empty;
            if (lectorHuellas.EnrollIndex != 1)
            {
                if (lectorHuellas.IsRegister)
                {
                    if (lectorHuellas.EnrollIndex - 1 > 0)
                    {
                        int eindex = lectorHuellas.EnrollIndex - 1;
                        strTemp = "Por favor escanee nuevamente ..." + eindex;
                    }
                }
            }
            mostrarNotificacion(strTemp);
        }

        private void lectorHuellas_OnEnroll(object sender, IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                string template = lectorHuellas.EncodeTemplate1(e.aTemplate);
                txtBase64_1.Text = template;
                mostrarNotificacion("Registro exitoso. Puede proceder a verificar");
                btnVerificar.Enabled = true;
            }

            else
            {
                mostrarNotificacion("Error, por favor registra nuevamente.");
                btnVerificar.Enabled = false;
            }
        }

        private void lectorHuellas_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            string template = lectorHuellas.EncodeTemplate1(e.aTemplate);

            if (lectorHuellas.VerFingerFromStr(ref template, txtBase64_1.Text.Trim(), false, ref Check))
            {
                mostrarNotificacion("Verificado");
                btnGuardar.Enabled = true;
            }

            else
            {
                mostrarNotificacion("No verificado");
                btnGuardar.Enabled = false;
            }

        }

        private void mostrarNotificacion(String s)
        {
            if (s != "")
            {
                sMensajesVer = s + Environment.NewLine + txtMensajes.Text.Trim();
                txtMensajes.Text = sMensajesVer.Trim();
            }
        }

        //FUNCION PARA CANCELAR EL REGISTRO
        private void cancelarRegistroHuella()
        {
            lectorHuellas.CancelEnroll();
            txtBase64_1.Clear();
            imgHuellaCapturada.Image = null;
            lectorHuellas.OnCapture -= lectorHuellas_OnCapture;
        }

        //FUNCION PARA DETENER EL DISPOSITIVO
        private void finalizarDispositivo()
        {
            lectorHuellas.OnCapture -= lectorHuellas_OnCapture;
            lectorHuellas.CancelEnroll();
            lectorHuellas.EndEngine();
            txtMensajes.Clear();
            mostrarNotificacion("Se ha finalizado las funciones del lector de huellas dactilares.");
            imgHuellaCapturada.Image = null;
        }

        #endregion

        private void frmLeerHuellas_V2_Load(object sender, EventArgs e)
        {
            limpiar();
            this.ActiveControl = cmbEmpresas;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbEmpresas.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la empresa.";
                ok.ShowDialog();
                return;
            }

            llenarGrid();
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_empleado_cliente"].Value.ToString());

                iniciarDispositivo();

                //if (recuperarHuella(iIdRegistro) == false)
                //    return;

                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["nombre_empleado"].Value.ToString();
                btnVerificar.Visible = true;
                btnRemover.Visible = true;
                btnGuardar.Visible = true;

                lectorHuellas.CancelEnroll();
                lectorHuellas.EnrollCount = 3;
                lectorHuellas.BeginEnroll();
                mostrarNotificacion("Por favor dar muestra de huella digital.");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            cancelarRegistroHuella();
            mostrarNotificacion("Proceso de registro cancelado." + Environment.NewLine + "Por favor dar muestra de huella digital.");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            finalizarDispositivo();
            limpiar();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if (lectorHuellas.IsRegister)
            {
                lectorHuellas.CancelEnroll();
            }

            btnVerificar.Enabled = false;
            lectorHuellas.OnCapture += lectorHuellas_OnCapture;
            lectorHuellas.BeginCapture();
            mostrarNotificacion("Por favor verifique la huella.");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea registrar la huella al registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                registrarHuella();
            }
        }

        private void btnRemoverRegistro_Click(object sender, EventArgs e)
        {
            imgHuellaCapturada.Image = null;

            lectorHuellas.CancelEnroll();
            txtBase64_1.Clear();
            lectorHuellas.EnrollCount = 3;
            lectorHuellas.BeginEnroll();
        }
    }
}
