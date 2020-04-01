using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmReaperturaCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sEstadoCierre;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdPosCierreCajero;

        public frmReaperturaCaja()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
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

        //FUNCION PARA CONSULTAR EL REGISTRO DE LA ULTIMA CAJA
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 * from pos_vw_reabrir_caja" + Environment.NewLine;
                sSql += "where id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "order by id_pos_cierre_cajero desc";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros en el sistema.";
                    ok.ShowDialog();
                    return;
                }

                sEstadoCierre = dtConsulta.Rows[0]["estado_cierre_cajero"].ToString().Trim().ToUpper();
                txtFechaApertura.Text = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("dd-MM-yyyy");
                txtHoraApertura.Text = dtConsulta.Rows[0]["hora_apertura"].ToString();

                if (sEstadoCierre == "ABIERTA")
                {
                    txtFechaCierre.Clear();
                    txtHoraCierre.Clear();
                    btnReabrir.Visible = false;
                }

                else
                {
                    txtFechaCierre.Text = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre"].ToString()).ToString("dd-MM-yyyy");
                    txtHoraCierre.Text = dtConsulta.Rows[0]["hora_cierre"].ToString().Trim();
                    btnReabrir.Visible = true;
                }

                txtUsuarioApertura.Text = dtConsulta.Rows[0]["cajero"].ToString().Trim();
                txtEstadoCaja.Text = sEstadoCierre;
                txtCajaInicial.Text = dtConsulta.Rows[0]["caja_inicial"].ToString().Trim();
                txtCajaFinal.Text = dtConsulta.Rows[0]["caja_final"].ToString().Trim();
                txtJornada.Text = dtConsulta.Rows[0]["jornada"].ToString().Trim().ToUpper();
                txtAhorroEmergencia.Text = dtConsulta.Rows[0]["ahorro_emergencia"].ToString().Trim();

                iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cierre_cajero"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message; 
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EJECUTAR LA APERTURA
        private void ejecutarReapertura()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir una transacción.";
                    ok.ShowDialog();
                    return;
                }

                //INSTRUCCION PARA REAPERTURAR LA CAJA
                //----------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                sSql += "fecha_cierre = null," + Environment.NewLine;
                sSql += "hora_cierre = null," + Environment.NewLine;
                sSql += "estado_cierre_cajero = 'Abierta'," + Environment.NewLine;
                sSql += "caja_final = 0" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //FUNCION PARA ELIMINAR EL REGISTRO DE MONEDAS GUARDADAS
                //----------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update pos_monedas set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1]+ "'" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and tipo_ingreso = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into log_apertura_caja (" + Environment.NewLine;
                sSql += "id_pos_cierre_cajero, motivo, fecha_modifica, usuario_modifica," + Environment.NewLine;
                sSql += "terminal_modifica, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdPosCierreCajero + ", '" + txtMotivo.Text.Trim().ToUpper() + "', GETDATE(), 'ADMINISTRADOR', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Caja reaperturada éxitosamente.";
                ok.ShowDialog();
                consultarRegistro();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReaperturaCaja_Load(object sender, EventArgs e)
        {
            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            consultarRegistro();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarRegistro();
        }

        private void btnReabrir_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el motivo de la reapertura de caja.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea reaperturar la caja seleccionada?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                ejecutarReapertura();
            }
        }
    }
}
