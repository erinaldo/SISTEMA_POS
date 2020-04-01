using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Parametros
{
    public partial class frmCorreosEnvioCierre : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdRegistro;
        int iIdSSL;

        public frmCorreosEnvioCierre()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR EL REGISTRO
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_correo_cierre where estado = 'A'";

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
                    limpiar();
                    return;
                }

                iIdRegistro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_correo_cierre"].ToString());
                txtCuenta.Text = dtConsulta.Rows[0]["cuenta"].ToString().Trim().ToLower();
                txtPassword.Text = dtConsulta.Rows[0]["password"].ToString().Trim();
                txtSmtp.Text = dtConsulta.Rows[0]["smtp"].ToString().Trim();
                txtPuerto.Text = dtConsulta.Rows[0]["puerto"].ToString().Trim();
                txtCorreo_1.Text = dtConsulta.Rows[0]["correo_1"].ToString().Trim().ToLower();
                txtCorreo_2.Text = dtConsulta.Rows[0]["correo_2"].ToString().Trim().ToLower();
                txtCorreo_3.Text = dtConsulta.Rows[0]["correo_3"].ToString().Trim().ToLower();

                if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_SSL"].ToString()) == 1)
                    chkSSL.Checked = true;
                else
                    chkSSL.Checked = false;
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
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                //INSTRUCCIÓN SQL PARA ACTUALIZAR
                sSql = "";
                sSql += "insert into pos_correo_cierre (" + Environment.NewLine;
                sSql += "cuenta, password, smtp, puerto, maneja_SSL, correo_1, correo_2," + Environment.NewLine;
                sSql += "correo_3, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "'" + txtCuenta.Text.Trim().ToLower() + "', '" + txtPassword.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtSmtp.Text.Trim().ToLower() + "', " + txtPuerto.Text.Trim() + ", " + iIdSSL + "," + Environment.NewLine;
                sSql += "'" + txtCorreo_1.Text.Trim().ToLower() + "', '" + txtCorreo_2.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "'" + txtCorreo_3.Text.Trim().ToLower() + "', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro insertado éxitosamente.";
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

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
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

                //INSTRUCCIÓN SQL PARA ACTUALIZAR
                sSql = "";
                sSql += "update pos_correo_cierre set" + Environment.NewLine;
                sSql += "cuenta = '" + txtCuenta.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "password = '" + txtPassword.Text.Trim() + "'," + Environment.NewLine;
                sSql += "smtp = '" + txtSmtp.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "puerto = " + txtPuerto.Text.Trim() + "," + Environment.NewLine;
                sSql += "maneja_SSL = " + iIdSSL + "," + Environment.NewLine;
                sSql += "correo_1 = '" + txtCorreo_1.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "correo_2 = '" + txtCorreo_2.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "correo_3 = '" + txtCorreo_3.Text.Trim().ToLower() + "'" + Environment.NewLine;
                sSql += "where id_pos_correo_cierre = " + iIdRegistro;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            iIdRegistro = 0;

            txtCuenta.Clear();
            txtPassword.Clear();
            txtSmtp.Clear();
            txtPuerto.Clear();
            txtCorreo_1.Clear();
            txtCorreo_2.Clear();
            txtCorreo_3.Clear();

            chkMostrarPassword.Checked = false;
            chkSSL.Checked = false;

            txtCuenta.Focus();
        }

        #endregion

        private void chkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarPassword.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
                txtPassword.Focus();
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtPassword.Focus();
            }

            txtPassword.SelectionStart = txtPassword.Text.Trim().Length;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el correo emisor.";
                ok.ShowDialog();
                txtCuenta.Focus();
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la contraseña del correo emisor.";
                ok.ShowDialog();
                txtPassword.Focus();
                return;
            }

            if (txtSmtp.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el SMTP del correo emisor.";
                ok.ShowDialog();
                txtSmtp.Focus();
                return;
            }

            if (txtPuerto.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el puerto del correo emisor.";
                ok.ShowDialog();
                txtPuerto.Focus();
                return;
            }

            if (txtCorreo_1.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la dirección a cual recibirá el cierre de caja.";
                ok.ShowDialog();
                txtCorreo_1.Focus();
                return;
            }

            if (chkSSL.Checked == true)
                iIdSSL = 1;
            else
                iIdSSL = 0;

            if (iIdRegistro == 0)
                insertarRegistro();
            else
                actualizarRegistro();
        }

        private void frmCorreosEnvioCierre_Load(object sender, EventArgs e)
        {
            consultarRegistro();
        }
    }
}
