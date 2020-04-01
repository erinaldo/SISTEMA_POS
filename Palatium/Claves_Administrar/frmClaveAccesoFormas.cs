using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Claves_Administrar
{
    public partial class frmClaveAccesoFormas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sCodigoClave;
        string sDescripcionClave;
        string sClaveIngresada;
        string sClaveConsultada;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmClaveAccesoFormas(string sCodigoClave_P)
        {
            this.sCodigoClave = sCodigoClave_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtCodigo.Text = txtCodigo.Text + sValor;
                txtCodigo.Focus();
                txtCodigo.SelectionStart = txtCodigo.Text.Trim().Length;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LA CLAVE DE ACCESO
        private bool consultarClaveAcceso()
        {
            try
            {
                sSql = "";
                sSql += "select descripcion, clave_acceso" + Environment.NewLine;
                sSql += "from pos_administracion_claves" + Environment.NewLine;
                sSql += "where codigo = '" + sCodigoClave + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sDescripcionClave = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
                sClaveConsultada = dtConsulta.Rows[0]["clave_acceso"].ToString().Trim();

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

        private void frmClaveAccesoFormas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmClaveAccesoFormas_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtCodigo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnIngresar_Click(sender, e);
            }
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtCodigo.Text.Length > 0)
            {

                str = txtCodigo.Text.Substring(txtCodigo.Text.Length - 1);
                loc = txtCodigo.Text.Length;
                txtCodigo.Text = txtCodigo.Text.Remove(loc - 1, 1);
            }

            txtCodigo.Focus();
            txtCodigo.SelectionStart = txtCodigo.Text.Trim().Length;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la clave de acceso.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                sClaveIngresada = txtCodigo.Text.Trim();

                if (consultarClaveAcceso() == false)
                {
                    return;
                }

                if (sClaveConsultada == sClaveIngresada)
                {
                    this.DialogResult = DialogResult.OK;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La clave ingresada es incorrecta. Favor verifique.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
