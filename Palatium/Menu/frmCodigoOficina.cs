using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Menú
{
    public partial class frmCodigoOficina : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        string sSql;
        string sClaveAdmin;

        DataTable dtConsulta;

        bool bRespuesta;

        int iCuenta;

        public frmCodigoOficina()
        {
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

            catch (Exception)
            {
                ok.lblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LA CLAVE DEL ADMINISTRADOR
        private int consultarClaveAdministrador()
        {
            try
            {
                sSql = "";
                sSql += "select clave_acceso_admin" + Environment.NewLine;
                sSql += "from pos_parametro_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sClaveAdmin = dtConsulta.Rows[0][0].ToString().Trim();
                        return 1;
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No se encuentra configurado los parámetros de localidad.";
                        ok.ShowDialog();
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        #region FUNCIONES DE CONTROL DE BOTONES

        //private void consultarPermisos()
        //{
        //    try
        //    {
        //        sSql = "";
        //        sSql += "select administracion" + Environment.NewLine;
        //        sSql += "from pos_cajero" + Environment.NewLine;
        //        sSql += "where id_cajero = " + Program.CAJERO_ID + Environment.NewLine;
        //        sSql += "and estado = 'A'" + Environment.NewLine;

        //        dtConsulta = new DataTable();
        //        dtConsulta.Clear();

        //        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //        if (bRespuesta == true)
        //        {
        //            if (dtConsulta.Rows.Count > 0)
        //            {
        //                if (dtConsulta.Rows[0].ItemArray[0].ToString() == "1")
        //                {
        //                    Program.sNombreUsuarioAdministracion = Program.sNombreUsuario;
        //                    this.DialogResult = DialogResult.OK;
        //                    this.Close();
        //                }

        //                else
        //                {
        //                    ok.lblMensaje.Text = "El usuario no tiene permisos para ingresar a la configuración del sistema.";
        //                    ok.ShowDialog();
        //                }
        //            }

        //            else
        //            {
        //                ok.lblMensaje.Text = "No hay registros en el sistema.";
        //                ok.ShowDialog();
        //            }
        //        }

        //        else
        //        {
        //            catchMensaje.lblMensaje.Text = sSql;
        //            catchMensaje.ShowDialog();
        //        }
        //    }

        //    catch(Exception ex)
        //    {
        //        catchMensaje.lblMensaje.Text = ex.Message;
        //        catchMensaje.ShowDialog();
        //    }
        //}

        //INGRESAR EL CURSOR AL BOTON
        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        private void btcancelar_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void frmCodigoOficina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmCodigoOficina_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtCodigo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código de usuario";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }

                else
                {
                    iCuenta = consultarClaveAdministrador();

                    if ((iCuenta == -1) || (iCuenta == 0))
                    {
                        return;
                    }
                    
                    if (txtCodigo.Text.Trim() == sClaveAdmin)
                    {
                        if ((Program.sNombreUsuario == "") || (Program.sNombreUsuario == null))
                        {
                            Program.sDatosMaximo[0] = "ADMINISTRADOR";
                            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                            Program.sDatosMaximo[2] = "A";
                        }


                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "La clave ingresada es incorrecta. Favor verifique.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
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

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn9);
        }

        private void btnRetroceder_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRetroceder);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn0);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn1);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn2);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn3);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn4);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn5);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn6);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn7);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn8);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn9);
        }

        private void btnRetroceder_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRetroceder);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
        }
    }
}
