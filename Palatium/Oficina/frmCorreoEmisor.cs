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

namespace Palatium.Oficina
{
    public partial class frmCorreoEmisor : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdCorreoEmisor;
        int iContar;
        int iSSL;

        public frmCorreoEmisor()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtCuenta.Clear();
            txtPasswordCuenta.Clear();
            txtSmtp.Clear();
            txtPuerto.Clear();
            txtCorreoCopia_1.Clear();
            txtCorreoCopia_2.Clear();
            chkMostrarPasswordCuenta.Checked = false;
            chkSSL.Checked = false;
            iIdCorreoEmisor = 0;
            cmbEstado.SelectedIndex = 0;
            txtCuenta.Focus();
        }

        //FUNCION PARA CONSULTAR INFORMACION
        private void consultarRegistro()
        {
            try
            {
                //sSql = "";
                //sSql += "select count(*) cuenta" + Environment.NewLine;
                //sSql += "from pos_correo_emisor" + Environment.NewLine;
                //sSql += "where estado = 'A'" + Environment.NewLine;

                //iContar = conexion.GFun_Ln_Contar_Registros(sSql);

                //if (iContar == 0)
                //{
                //    limpiar();
                //    goto fin;
                //}

                //else if (iContar == -1)
                //{
                //    ok.LblMensaje.Text = "No se pudo extraer el número de registros.";
                //    ok.ShowDialog();
                //    goto fin;
                //}
                    

                sSql = "";
                sSql += "select id_pos_correo_emisor, correo_que_envia," + Environment.NewLine;
                sSql += "correo_con_copia_1, correo_con_copia_2," + Environment.NewLine;
                sSql += "correo_palabra_clave, correo_smtp," + Environment.NewLine;
                sSql += "correo_puerto, maneja_SSL, estado" + Environment.NewLine;
                sSql += "from pos_correo_emisor" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdCorreoEmisor = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtCuenta.Text = dtConsulta.Rows[0][1].ToString();
                        txtCorreoCopia_1.Text = dtConsulta.Rows[0][2].ToString();
                        txtCorreoCopia_2.Text = dtConsulta.Rows[0][3].ToString();
                        txtPasswordCuenta.Text = dtConsulta.Rows[0][4].ToString();
                        txtSmtp.Text = dtConsulta.Rows[0][5].ToString();
                        txtPuerto.Text = dtConsulta.Rows[0][6].ToString();

                        if (Convert.ToInt32(dtConsulta.Rows[0][7].ToString()) == 1)
                        {
                            chkSSL.Checked = true;
                        }

                        else
                        {
                            chkSSL.Checked = false;
                        }

                        if (dtConsulta.Rows[0][8].ToString() == "A")
                        {
                            cmbEstado.SelectedIndex = 0;
                        }

                        else
                        {
                            cmbEstado.SelectedIndex = 1;
                        }

                        txtCuenta.Focus();
                    }

                    else
                    {
                        limpiar();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            //fin: { };
        }


        //FUNCION PARA INSERTAR EL REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "insert into pos_correo_emisor (" + Environment.NewLine;
                sSql += "correo_que_envia, correo_con_copia_1, correo_con_copia_2," + Environment.NewLine;
                sSql += "correo_palabra_clave, correo_smtp, correo_puerto, maneja_SSL," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "'" + txtCuenta.Text.Trim() + "', '" + txtCorreoCopia_1.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtCorreoCopia_2.Text.Trim() + "', '" + txtPasswordCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtSmtp.Text.Trim() + "', " + Convert.ToInt32(txtPuerto.Text.Trim()) + "," + Environment.NewLine;
                sSql += iSSL + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTA EL QUERY DE INSERCIÓN
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro insertado éxitosamente.";
                ok.ShowDialog();
                consultarRegistro();
                goto fin;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

            fin: { }
        }


        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "update pos_correo_emisor set" + Environment.NewLine;
                sSql += "correo_que_envia = '" + txtCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_con_copia_1 = '" + txtCorreoCopia_1.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_con_copia_2 = '" + txtCorreoCopia_2.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_palabra_clave = '" + txtPasswordCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_smtp = '" + txtSmtp.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_puerto = " + Convert.ToInt32(txtPuerto.Text.Trim()) + "," + Environment.NewLine;
                sSql += "maneja_SSL = " + iSSL + Environment.NewLine;
                sSql += "where id_pos_correo_emisor = " + iIdCorreoEmisor + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTA EL QUERY DE INSERCIÓN
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                consultarRegistro();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        #endregion

        private void chkMostrarPasswordCuenta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarPasswordCuenta.Checked == true)
            {
                txtPasswordCuenta.PasswordChar = '\0';
                txtPasswordCuenta.Focus();
            }
            else
            {
                txtPasswordCuenta.PasswordChar = '*';
                txtPasswordCuenta.Focus();
            }

            txtPasswordCuenta.SelectionStart = txtPasswordCuenta.Text.Trim().Length;
        }

        private void frmCorreoEmisor_Load(object sender, EventArgs e)
        {
            consultarRegistro();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            consultarRegistro();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el correo electrónico del emisor.";
                ok.ShowDialog();
                txtCuenta.Focus();
            }

            else if (txtPasswordCuenta.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese la contraseña del correo electrónico del emisor.";
                ok.ShowDialog();
                txtPasswordCuenta.Focus();
            }

            else if (txtSmtp.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el dominio del correo electrónico.";
                ok.ShowDialog();
                txtSmtp.Focus();
            }

            else if (txtPuerto.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el puerto del correo electrónico del emisor.";
                ok.ShowDialog();
                txtPuerto.Focus();
            }

            else
            {
                if (chkSSL.Checked == true)
                {
                    iSSL = 1;
                }

                else
                {
                    iSSL = 0;
                }

                if (iIdCorreoEmisor == 0)
                {
                    SiNo.LblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        insertarRegistro();
                    }
                }

                else
                {
                    actualizarRegistro();
                }
            }
        }
    }
}
