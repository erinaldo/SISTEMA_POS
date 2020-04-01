using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConexionBD;

namespace Palatium
{
    public partial class CodDomicilio : Form
    {
        ConexionBD.ConexionBD transacciones = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        bool bRespuesta = false;
        DataTable dtConsulta = new DataTable();

        public CodDomicilio()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtCodigoAlterno.Text = txtCodigoAlterno.Text + sValor;
                txtCodigoAlterno.Focus();
                txtCodigoAlterno.SelectionStart = txtCodigoAlterno.Text.Trim().Length;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR DATOS
        private void consultarRegistro()
        {
            try
            {
                //AQUI CREAMOS NUESTRA INSTRUCCION DE BUSQUEDA EN SQL
                sSql = "";
                sSql = sSql + "select identificacion, nombres, apellidos, correo_electronico," + Environment.NewLine;
                sSql = sSql + "codigo_alterno, id_persona" + Environment.NewLine;
                sSql = sSql + "from tp_personas" + Environment.NewLine;
                sSql = sSql + "where codigo_alterno = '" + txtCodigoAlterno.Text + "'" + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta.Clear();
                bRespuesta = transacciones.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        Domicilio frmDomicilio = new Domicilio();
                        frmDomicilio.dtConsulta = dtConsulta;
                        frmDomicilio.txtNumeroTelefono.Text = txtCodigoAlterno.Text;
                        frmDomicilio.ShowDialog();

                        if (frmDomicilio.DialogResult == DialogResult.OK)
                        {                            
                            this.DialogResult = DialogResult.OK;
                            frmDomicilio.Close();
                        }

                        goto fin;
                    }

                    else if (dtConsulta.Rows.Count == 1)
                    {
                        Program.sIDPERSONA = dtConsulta.Rows[0].ItemArray[5].ToString();
                        Domicilio frmDomicilio = new Domicilio();
                        frmDomicilio.dtConsulta = dtConsulta;
                        frmDomicilio.txtNumeroTelefono.Text = txtCodigoAlterno.Text;
                        frmDomicilio.ShowDialog();

                        if (frmDomicilio.DialogResult == DialogResult.OK)
                        {                            
                            this.DialogResult = DialogResult.OK;
                            frmDomicilio.Close();
                        }

                        goto fin;
                    }

                    else
                    {
                        sSql = "";
                        sSql = sSql + "select TP.id_persona as CÓDIGO, TP.identificacion as 'Cédula/RUC'," + Environment.NewLine;
                        sSql = sSql + "TP.nombres as 'NOMBRE DEL CLIENTE', TP.apellidos as 'APELLIDO DEL CLIENTE'," + Environment.NewLine;
                        sSql = sSql + "TP.correo_electronico as 'CORREO ELETRÓNICO', TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion as 'DIRECCIÓN', " + Environment.NewLine;
                        sSql = sSql + "TP.codigo_alterno as 'TELÉFONO'" + Environment.NewLine;
                        sSql = sSql + "from tp_personas TP, tp_direcciones TD " + Environment.NewLine;
                        sSql = sSql + "where TP.id_persona = TD.id_persona" + Environment.NewLine;
                        sSql = sSql + "and TP.estado = 'A'" + Environment.NewLine;
                        sSql = sSql + "and TD.estado = 'A' " + Environment.NewLine;
                        sSql = sSql + "and TP.codigo_alterno = '" + txtCodigoAlterno.Text.Trim() + "'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = transacciones.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                this.Close();
                                Domicilios.frmListaClientesDomicilio listado = new Domicilios.frmListaClientesDomicilio(dtConsulta);
                                listado.ShowDialog();

                                if (listado.DialogResult == DialogResult.OK)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    listado.Close();
                                }
                                goto fin;
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto fin;
                        }
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin:
            { }
        }

        #endregion

        private void btcancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void CodDomicilio_Load(object sender, EventArgs e)
        {
            Program.iIDMESA = 0;

            Program.sNombreMesa = null;
            Program.dbDescuento = 0;
            Program.dbValorPorcentaje = 0;

            this.ActiveControl = txtCodigoAlterno;
        }

        private void CodDomicilio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtCodigoAlterno.Text.Trim() == "")
            {
                Domicilio frmDomicilio = new Domicilio();
                frmDomicilio.dtConsulta = dtConsulta;
                frmDomicilio.txtNumeroTelefono.Text = txtCodigoAlterno.Text;
                frmDomicilio.ShowDialog();

                if (frmDomicilio.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    frmDomicilio.Close();
                }
            }

            else
            {
                consultarRegistro();
            }
        }

        private void txtCodigoAlterno_KeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                consultarRegistro();
            }
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

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtCodigoAlterno.Text.Length > 0)
            {

                str = txtCodigoAlterno.Text.Substring(txtCodigoAlterno.Text.Length - 1);
                loc = txtCodigoAlterno.Text.Length;
                txtCodigoAlterno.Text = txtCodigoAlterno.Text.Remove(loc - 1, 1);
            }

            txtCodigoAlterno.Focus();
            txtCodigoAlterno.SelectionStart = txtCodigoAlterno.Text.Trim().Length;
        }

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn0);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn1);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn2);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn3);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn4);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn5);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn6);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn7);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn8);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn9);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn9);
        }

        private void btnRetroceder_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRetroceder);
        }

        private void btnRetroceder_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRetroceder);
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }
    }
}
