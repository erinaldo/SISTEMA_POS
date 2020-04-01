using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;

namespace Palatium.Empresa
{
    public partial class frmEspereHuellaDactilar : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sEstado;
        string sMensajesVer = "";

        bool Check;
        bool bRespuesta;

        DataTable dtConsulta;

        int iIdRegistro;
        private int FMatchType = 2, fpcHandle;
        private bool FAutoIdentify;
        int nameCount = 0;

        //VARIABLES PARA ABRIR LA COMANDA
        int iIdOrigenOrden;
        int iIdPersonaEmpresa;
        int iIdPersonaEmpleado;
        string sNombreEmpresa;
        string sNombreEmpleado;

        private AxZKFPEngX zk_dispositivo = new AxZKFPEngX();

        public frmEspereHuellaDactilar(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            lblEmpresa.Text = "EMPRESA";
            lblEmpleado.Text = "EMPLEADO";
            btnAceptar.Visible = false;
            llenarGrid();
            iniciarEspera();
        }

        //FUNCION PARA INICIAR LA ESPERA
        private void iniciarEspera()
        {
            zk_dispositivo.OnCapture += zkFprint_OnCapture;
            zk_dispositivo.BeginCapture();
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_busqueda_huellas_empleados_empresa";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                dgvDatos.DataSource = dtConsulta;
                string regTemplateString = "";
                int FpId = 0;

                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    try
                    {
                        regTemplateString = row.Cells[4].Value.ToString();

                        zk_dispositivo.AddRegTemplateStrToFPCacheDB(fpcHandle, FpId, regTemplateString);

                        FpId = FpId + 1;
                    }
                    catch { }

                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL LECTOR

        private void Inicial_Lector_ZK()
        {
            try
            {
                if (zk_dispositivo.InitEngine() == 0)
                {
                    zk_dispositivo.FPEngineVersion = "9";
                    zk_dispositivo.EnrollCount = 3;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zkFprint_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            string template = zk_dispositivo.EncodeTemplate1(e.aTemplate);
            string regTemplateString = "";
            int iBandera = 0;

            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                iIdPersonaEmpresa = Convert.ToInt32(row.Cells[0].Value.ToString());
                sNombreEmpresa = row.Cells[1].Value.ToString().Trim().ToUpper();
                iIdPersonaEmpleado = Convert.ToInt32(row.Cells[2].Value.ToString());
                sNombreEmpleado = row.Cells[3].Value.ToString().Trim().ToUpper();
                regTemplateString = row.Cells[4].Value.ToString();

                if (zk_dispositivo.VerFingerFromStr(ref template, regTemplateString, false, ref Check))
                {
                    iBandera = 1;
                    break;
                }
            }

            if (iBandera == 1)
            {
                lblEmpresa.Text = sNombreEmpresa;
                lblEmpleado.Text = sNombreEmpleado;
                btnAceptar.Visible = true;
                btnAceptar.Focus();
            }

            else
            {
                zk_dispositivo.CancelCapture();

                lblEmpresa.Text = "EMPRESA";
                lblEmpleado.Text = "EMPLEADO";
                btnAceptar.Visible = false;

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se encuentra el registro. Favor intente nuevamente.";
                ok.ShowDialog();

                zk_dispositivo.BeginCapture();
            }
        }

        #endregion

        private void frmEspereHuellaDactilar_Load(object sender, EventArgs e)
        {
            Controls.Add(zk_dispositivo);
            Inicial_Lector_ZK();
            fpcHandle = zk_dispositivo.CreateFPCacheDB();
            llenarGrid();
            iniciarEspera();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            zk_dispositivo.CancelEnroll();
            zk_dispositivo.OnCapture -= zkFprint_OnCapture;
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Empresa.frmComandaClienteEmpresarial comanda = new frmComandaClienteEmpresarial(iIdPersonaEmpleado, sNombreEmpresa, sNombreEmpleado, iIdPersonaEmpresa, iIdOrigenOrden);
            comanda.ShowDialog();

            if (comanda.DialogResult == DialogResult.OK)
            {
                limpiar();
            }
        }

        private void frmEspereHuellaDactilar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmEspereHuellaDactilar_FormClosing(object sender, FormClosingEventArgs e)
        {
            zk_dispositivo.CancelEnroll();
            zk_dispositivo.OnCapture -= zkFprint_OnCapture;
        }
    }
}
