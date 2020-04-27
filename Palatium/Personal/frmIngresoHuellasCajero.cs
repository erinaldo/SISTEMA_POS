using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;

namespace Palatium.Personal
{
    public partial class frmIngresoHuellasCajero : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sMensajesVer = "";

        bool bRespuesta;
        bool Check;

        int iIdRegistro;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        private AxZKFPEngX lectorHuellas = new AxZKFPEngX();

        public frmIngresoHuellasCajero()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvCajero.Rows.Clear();
                int iCantidad = 2;

                sSql = "";
                sSql += "select id_pos_cajero, is_active, codigo, descripcion," + Environment.NewLine;
                sSql += "case is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where estado in (@estado_1, @estado_2)" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "and descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by codigo";

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

                if (iCantidad == 3)
                {
                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@buscar";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = txtBuscar.Text.Trim();
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
                                       dtConsulta.Rows[i]["is_active"].ToString(),
                                       dtConsulta.Rows[i]["codigo"].ToString(),
                                       dtConsulta.Rows[i]["descripcion"].ToString(),
                                       dtConsulta.Rows[i]["estado"].ToString());
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

                //imagen = imagenABytes(imgHuellaCapturada.Image);

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "huella_dactilar = @huella_dactilar," + Environment.NewLine;
                sSql += "is_active_huella = @is_active_huella" + Environment.NewLine;
                sSql += "where id_pos_cajero = @id_pos_cajero" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@huella_dactilar";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtBase64_1.Text.Trim();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@is_active_huella";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 1;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_pos_cajero";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = iIdRegistro;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

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

        //FUNCION LIMPIAR
        private void limpiar()
        {
            txtBuscar.Clear();
            txtMensajes.Clear();
            txtNombreCajero.Clear();
            txtBase64_1.Clear();
            imgHuellaCapturada.Image = null;
            grupoDatos.Enabled = false;
            btnVerificar.Enabled = false;
            btnGuardar.Enabled = false;

            llenarGrid();
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

        private void frmIngresoHuellasCajero_Load(object sender, EventArgs e)
        {
            limpiar();
            this.ActiveControl = txtBuscar;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void dgvCajero_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvCajero.CurrentRow.Cells["id_pos_cajero"].Value.ToString());

                iniciarDispositivo();

                //if (recuperarHuella(iIdRegistro) == false)
                //    return;

                txtNombreCajero.Text = dgvCajero.CurrentRow.Cells["descripcion"].Value.ToString();
                grupoDatos.Enabled = true;

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

        private void btnRemoverRegistro_Click(object sender, EventArgs e)
        {
            imgHuellaCapturada.Image = null;

            lectorHuellas.CancelEnroll();
            txtBase64_1.Clear();
            lectorHuellas.EnrollCount = 3;
            lectorHuellas.BeginEnroll();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            finalizarDispositivo();
            limpiar();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            cancelarRegistroHuella();
            mostrarNotificacion("Proceso de registro cancelado." + Environment.NewLine + "Por favor dar muestra de huella digital.");
        }

        private void frmIngresoHuellasCajero_FormClosing(object sender, FormClosingEventArgs e)
        {
            finalizarDispositivo();
        }
    }
}
