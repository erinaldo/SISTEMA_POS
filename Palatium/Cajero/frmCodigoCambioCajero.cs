using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmCodigoCambioCajero : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        int iIdCajero;
        int iIdCierreCajero;
        bool bRespuesta;
        DataTable dtConsulta;


        public frmCodigoCambioCajero(int iIdCierreCajero, int iIdCajero)
        {
            this.iIdCierreCajero = iIdCierreCajero;
            this.iIdCajero = iIdCajero;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private bool actualizarRegistro(int iId)
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql = sSql + "update pos_cierre_cajero set" + Environment.NewLine;
                sSql = sSql + "id_cajero = " + iId + Environment.NewLine;
                sSql = sSql + "where id_pos_cierre_cajero = " + iIdCierreCajero;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                return false;
            }
        }

        //FUNCION PARA CONSULTAR 
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_cajero, descripcion, claveacceso, id_persona, estado" + Environment.NewLine;
                sSql = sSql + "from pos_cajero" + Environment.NewLine;
                sSql = sSql + "where claveacceso = '" + txtClave.Text.Trim() + "'" + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) == iIdCajero)
                        {
                            ok.LblMensaje.Text = "Los datos ingresados corresponden al cajero en línea.";
                            ok.ShowDialog();

                            txtClave.Clear();
                            txtClave.Focus();
                        }

                        else
                        {
                            if (actualizarRegistro(Convert.ToInt32(dtConsulta.Rows[0][0].ToString())) == true)
                            {
                                Program.CAJERO_ID = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                                Program.sNombreCajero = dtConsulta.Rows[0][1].ToString();
                                Program.sNombreUsuario = dtConsulta.Rows[0][1].ToString();
                                Program.sEstadoUsuario = dtConsulta.Rows[0][4].ToString();
                                Program.iIdPersonaMovimiento = Convert.ToInt32(dtConsulta.Rows[0][3].ToString());

                                Program.sDatosMaximo[0] = dtConsulta.Rows[0][1].ToString();
                                Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                                Program.sDatosMaximo[2] = dtConsulta.Rows[0][4].ToString();


                                ok.LblMensaje.Text = "Bienvenido (a)\n\n" + Program.sNombreUsuario;
                                ok.ShowDialog();
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe un cajero registrado con los datos ingresados.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btn_1_Click(object sender, EventArgs e)
        {
            txtClave.Text += "1";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            txtClave.Text += "2";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            txtClave.Text += "3";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            txtClave.Text += "4";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            txtClave.Text += "5";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            txtClave.Text += "6";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            txtClave.Text += "7";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            txtClave.Text += "8";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            txtClave.Text += "9";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            txtClave.Text += "0";
            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtClave.Text.Length > 0)
            {

                str = txtClave.Text.Substring(txtClave.Text.Length - 1);
                loc = txtClave.Text.Length;
                txtClave.Text = txtClave.Text.Remove(loc - 1, 1);
            }

            txtClave.Focus();
            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }

        private void frmCodigoCambioCajero_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtClave;
        }

        private void frmCodigoCambioCajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == "")
            {
                ok.LblMensaje.Text = "Favor ingrese la clave para proceder con la consulta.";
                ok.ShowDialog();
            }

            else
            {
                consultarRegistro();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtClave.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la clave para proceder con la consulta.";
                    ok.ShowDialog();
                }

                else
                {
                    consultarRegistro();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
