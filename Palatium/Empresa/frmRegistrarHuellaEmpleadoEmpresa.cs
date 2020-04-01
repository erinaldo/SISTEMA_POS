using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;

namespace Palatium.Empresa
{
    public partial class frmRegistrarHuellaEmpleadoEmpresa : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sEstado;
        string sMensajesVer = "";

        bool bRespuesta;

        DataTable dtConsulta;

        int iIdPersona;
        int iIdRegistro;

        Byte[] imagen;

        int iBanderaCajaTexto;
        int iVerificar;

        private AxZKFPEngX zk_dispositivo = new AxZKFPEngX();

        private bool Check;

        public frmRegistrarHuellaEmpleadoEmpresa()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE EMPRESAS
        private void llenarComboEmpresas()
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and CE.estado = 'A'" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "order by apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbEmpresas.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void limpiar()
        {
            cmbEmpresas.SelectedIndexChanged -= new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbEmpresas.SelectedIndexChanged += new EventHandler(cmbEmpresas_SelectedIndexChanged);
            txtBuscar.Clear();
            txtDescripcion.Clear();
            txtBase64_1.Clear();

            txtMensajes.Clear();
            btnVerificar.Visible = false;
            btnRemover.Visible = false;
            btnGuardar.Visible = false;
            btnVerificar.Enabled = false;
            btnGuardar.Enabled = false;
            grupoHuella.Enabled = false;
            imagenHuellas.Image = null;

            zk_dispositivo.OnImageReceived -= zkFprint_OnImageReceived;
            zk_dispositivo.OnFeatureInfo -= zkFprint_OnFeatureInfo;
            zk_dispositivo.OnEnroll -= zkFprint_OnEnroll;

            llenarGrid();
            txtBuscar.Focus();
        }

        private byte[] imagenABytes(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select E.id_pos_empleado_cliente, E.id_persona, E.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "TP.identificacion IDENTIFICACIÓN, " + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) 'NOMBRE EMPLEADO'," + Environment.NewLine;
                sSql += "case E.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO, E.aplica_almuerzo" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and E.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = " + Convert.ToInt32(cmbEmpresas.SelectedValue) + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and TP.identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[1].Visible = false;
                    dgvDatos.Columns[2].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[3].Width = 120;
                    dgvDatos.Columns[4].Width = 200;
                    dgvDatos.Columns[5].Width = 80;
                    dgvDatos.ClearSelection();
                    lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

                imagen = imagenABytes(imagenHuellas.Image);
                //string sBase64 = Convert.ToBase64String(imagen);
                //MemoryStream ms = new MemoryStream();
                //img.Image.Save(ms, ImageFormat.Jpeg);

                sSql = "";
                sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                sSql += "huella_dactilar = @huella" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @idRegistro";

                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter();
                Parametros[0].ParameterName = "@huella";
                Parametros[0].SqlDbType = SqlDbType.VarChar;
                Parametros[0].Value = txtBase64_1.Text.Trim();

                Parametros[1] = new SqlParameter();
                Parametros[1].ParameterName = "@idRegistro";
                Parametros[1].SqlDbType = SqlDbType.Int;
                Parametros[1].Value = iIdRegistro;

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, Parametros))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();

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

        //ACTUALIZAR LA HUELLA EN EL SISTEMA
        private void eliminarHuella()
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

                sSql = "";
                sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                sSql += "huella_dactilar = null" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = " + iIdRegistro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();

                limpiar();
                return;
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECUPERAR LA HUELLA
        private bool recuperarHuella(int iIdRegistro_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(huella_dactilar, '') huella_dactilar" + Environment.NewLine;
                sSql += "from pos_empleado_cliente" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_empleado_cliente = @id_pos_empleado_cliente";

                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter();
                parametros[0].ParameterName = "@id_pos_empleado_cliente";
                parametros[0].SqlDbType = SqlDbType.VarChar;
                parametros[0].Value = iIdRegistro_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametros);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    txtBase64_1.Text = "";
                    imagenHuellas.Image = null;
                }

                else
                {
                    string sBase64_P = dtConsulta.Rows[0]["huella_dactilar"].ToString().Trim();
                    txtBase64_1.Text = sBase64_P;

                    Byte[] imageBytes = Convert.FromBase64String(sBase64_P);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    Image img = Image.FromStream(ms, true);

                    imagenHuellas.Image = img;
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

        #region FUNCIONES DEL BIOMETRICO

        private void Inicial_Lector_ZK()
        {
            try
            {
                zk_dispositivo.OnImageReceived -= zkFprint_OnImageReceived;
                zk_dispositivo.OnFeatureInfo -= zkFprint_OnFeatureInfo;
                zk_dispositivo.OnEnroll -= zkFprint_OnEnroll;

                if (zk_dispositivo.InitEngine() == 0)
                {
                    zk_dispositivo.FPEngineVersion = "9";
                    zk_dispositivo.EnrollCount = 3;
                    deviceSerial.Text += " " + zk_dispositivo.SensorSN + " Count: " + zk_dispositivo.SensorCount.ToString() + " Index: " + zk_dispositivo.SensorIndex.ToString();
                    mostrarNotificacion("Dispositivo conectado éxitosamente.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zkFprint_OnImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = imagenHuellas.CreateGraphics();
            Bitmap bmp = new Bitmap(imagenHuellas.Width, imagenHuellas.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            zk_dispositivo.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            imagenHuellas.Image = bmp;
        }

        private void zkFprint_OnFeatureInfo(object sender, IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            String strTemp = string.Empty;

            if (zk_dispositivo.EnrollIndex != 1)
            {
                if (zk_dispositivo.IsRegister)
                {
                    if (zk_dispositivo.EnrollIndex - 1 > 0)
                    {
                        int eindex = zk_dispositivo.EnrollIndex - 1;
                        strTemp = "Por favor, coloque su huella nuevamente... " + eindex;
                    }
                }
            }

            mostrarNotificacion(strTemp);
        }

        private void zkFprint_OnEnroll(object sender, IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                string template = zk_dispositivo.EncodeTemplate1(e.aTemplate);
                txtBase64_1.Text = template;
                mostrarNotificacion("Registro satisfactorio, puede verificar su huella.");
                btnVerificar.Enabled = true;
            }

            else
            {
                zk_dispositivo.EnrollIndex += 1;
                mostrarNotificacion("Error, por favor, registre la huella nuevaente.");
                zk_dispositivo.CancelEnroll();
                zk_dispositivo.EnrollCount = 3;
                zk_dispositivo.BeginEnroll();
            }
        }

        private void zkFprint_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            string template = zk_dispositivo.EncodeTemplate1(e.aTemplate);

            if (zk_dispositivo.VerFingerFromStr(ref template, txtBase64_1.Text, false, ref Check))
            {
                mostrarNotificacion("Verificado...!!!");
                btnGuardar.Enabled = true;
            }
            else
            {
                mostrarNotificacion("No verificado...!!!");
                btnGuardar.Enabled = false;
            }
        }

        private void mostrarNotificacion(String s)
        {
            sMensajesVer = txtMensajes.Text.Trim();

            if (sMensajesVer.Trim() != "")
                sMensajesVer += Environment.NewLine;

            sMensajesVer += s;
            txtMensajes.Text = sMensajesVer;
        }

        #endregion

        private void frmRegistrarHuellaEmpleadoEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                Controls.Add(zk_dispositivo);
                Inicial_Lector_ZK();                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            llenarGrid();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());

                //if (recuperarHuella(iIdRegistro) == false)
                //{
                //    return;
                //}

                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                btnVerificar.Visible = true;
                btnRemover.Visible = true;
                btnGuardar.Visible = true;
                grupoHuella.Enabled = true;

                iBanderaCajaTexto = 1;

                zk_dispositivo.OnImageReceived += zkFprint_OnImageReceived;
                zk_dispositivo.OnFeatureInfo += zkFprint_OnFeatureInfo;
                zk_dispositivo.OnEnroll += zkFprint_OnEnroll;

                zk_dispositivo.CancelEnroll();
                zk_dispositivo.EnrollCount = 3;
                zk_dispositivo.BeginEnroll();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            imagenHuellas.Image = null;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                string sRutaImagen = abrir.FileName;
                imagenHuellas.Image = Image.FromFile(sRutaImagen);
            }

            abrir.Dispose();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (zk_dispositivo.IsRegister)
            {
                zk_dispositivo.CancelEnroll();
            }

            btnVerificar.Enabled = false;
            zk_dispositivo.OnCapture += zkFprint_OnCapture;
            zk_dispositivo.BeginCapture();
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

        private void btnRemover_Click(object sender, EventArgs e)
        {
            imagenHuellas.Image = null;
            zk_dispositivo.CancelEnroll();
            zk_dispositivo.EnrollCount = 3;
            zk_dispositivo.BeginEnroll();
        }
    }
}
