using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmClaveAdministrador : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;
        bool bRespuesta;
        bool bActualizar;
        DataTable dtConsulta;
        int iIdParametro;

        public frmClaveAdministrador()
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
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where cg_localidad = " + Program.iCgLocalidadRecuperado;

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

        //CONSULTAR EL REGISTRO 
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_parametro_localidad, clave_acceso_admin" + Environment.NewLine;
                sSql += "from pos_parametro_localidad" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdParametro = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        txtClave.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                        bActualizar = true;
                        txtClave.Focus();
                    }

                    else
                    {
                        bActualizar = false;
                        txtClave.Focus();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        fin: { }
        }

        //FUNCION PARA ACTUALIZAR LA CLAVE DE ADMINISTRACION
        private void actualizarClave()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    //limpiar();
                    return;
                }

                //INSTRUCCIÓN SQL PARA ACTUALIZAR
                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "clave_acceso_admin = '" + txtClave.Text.Trim() + "'" + Environment.NewLine;
                sSql += "where id_pos_parametro_localidad = " + iIdParametro;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                Program.sPasswordAdmin = txtClave.Text.Trim();

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Clave de administrador modificada éxitosamente.";
                ok.ShowDialog();
                consultarRegistro();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClaveAdministrador_Load(object sender, EventArgs e)
        {
            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            consultarRegistro();
        }

        private void chkVerClave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerClave.Checked == true)
            {
                txtClave.PasswordChar = '\0';
                txtClave.Focus();
            }

            else
            {
                txtClave.PasswordChar = '*';
                txtClave.Focus();
            }

            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese una clave para la administración.";
                ok.ShowDialog();
            }

            else
            {
                if (bActualizar == true)
                {
                    //ENVIAR A ACTUALIZAR
                    actualizarClave();
                }

                else
                {
                    //ENVIAR A INSERTAR
                }
            }
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarRegistro();
        }
    }
}
