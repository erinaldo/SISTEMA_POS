using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmParametros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseCargarParametros parametros = new Clases.ClaseCargarParametros();

        DataTable dtConsulta;

        bool bRespuesta;
        bool bActualizar;

        string sSql;
        string sEstado;

        int iIdParametro;
        int iManejaSSL;
        
        public frmParametros()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR INFORMACION
        private void cargarInformacion()
        {
            try
            {
                sSql = "";
                sSql += "select correo_que_envia, correo_palabra_clave, correo_smtp," + Environment.NewLine;
                sSql += "correo_puerto, correo_con_copia, correo_consumidor_final," + Environment.NewLine;
                sSql += "correo_ambiente_prueba, wsdl_pruebas, url_pruebas," + Environment.NewLine;
                sSql += "wsdl_produccion, url_produccion, certificado_ruta," + Environment.NewLine;
                sSql += "certificado_palabra_clave, Estado, id_cel_parametro, maneja_ssl" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtCuenta.Text = dtConsulta.Rows[0]["correo_que_envia"].ToString();
                        txtPasswordCuenta.Text = dtConsulta.Rows[0]["correo_palabra_clave"].ToString();
                        txtSmtp.Text = dtConsulta.Rows[0]["correo_smtp"].ToString();
                        txtPuerto.Text = dtConsulta.Rows[0]["correo_puerto"].ToString();
                        txtCorreoCopia.Text = dtConsulta.Rows[0]["correo_con_copia"].ToString();
                        txtCorreoConsumidorFinal.Text = dtConsulta.Rows[0]["correo_consumidor_final"].ToString();
                        txtCorreoAmbientePruebas.Text = dtConsulta.Rows[0]["correo_ambiente_prueba"].ToString();
                        txtEnvioPruebas.Text = dtConsulta.Rows[0]["wsdl_pruebas"].ToString();
                        txtConsultaPruebas.Text = dtConsulta.Rows[0]["url_pruebas"].ToString();
                        txtEnvioProduccion.Text = dtConsulta.Rows[0]["wsdl_produccion"].ToString();
                        txtConsultaProduccion.Text = dtConsulta.Rows[0]["url_produccion"].ToString();
                        txtRuta.Text = dtConsulta.Rows[0]["certificado_ruta"].ToString();
                        txtPasswordCertificado.Text = dtConsulta.Rows[0]["certificado_palabra_clave"].ToString();

                        if (dtConsulta.Rows[0][13].ToString() == "A")
                        {
                            cmbEstado.SelectedIndex = 0;
                        }

                        else
                        {
                            cmbEstado.SelectedIndex = 1;
                        }

                        iIdParametro = Convert.ToInt32(dtConsulta.Rows[0][14].ToString());

                        if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_ssl"].ToString()) == 1)
                        {
                            chkSSL.Checked = true;
                        }

                        else
                        {
                            chkSSL.Checked = false;
                        }

                        bActualizar = true;

                        chkMostrarPasswordCuenta.Checked = false;
                        chkPasswordCertificado.Checked = false;

                        txtCuenta.Focus();
                        txtCuenta.SelectionStart = txtCuenta.Text.Trim().Length;
                    }

                    else
                    {
                        bActualizar = false;
                        cmbEstado.SelectedIndex = 0;
                        limpiar();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtCuenta.Clear();
            txtPasswordCuenta.Clear();
            txtSmtp.Clear();
            txtPuerto.Clear();
            txtCorreoCopia.Clear();
            txtCorreoConsumidorFinal.Clear();
            txtCorreoAmbientePruebas.Clear();
            txtEnvioPruebas.Clear();
            txtConsultaPruebas.Clear();
            txtEnvioProduccion.Clear();
            txtConsultaProduccion.Clear();
            txtRuta.Clear();
            txtPasswordCertificado.Clear();
            iIdParametro = 0;
            chkMostrarPasswordCuenta.Checked = false;
            chkPasswordCertificado.Checked = false;
            chkSSL.Checked = false;
            txtCuenta.Focus();
        }

        //FUNCION PARA INSERTAR UN REGISTRO NUEVO
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into cel_parametro(" + Environment.NewLine;
                sSql += "correo_que_envia, correo_palabra_clave, correo_smtp," + Environment.NewLine;
                sSql += "correo_puerto, correo_con_copia, correo_consumidor_final," + Environment.NewLine;
                sSql += "correo_ambiente_prueba, wsdl_pruebas, url_pruebas," + Environment.NewLine;
                sSql += "wsdl_produccion, url_produccion, certificado_ruta," + Environment.NewLine;
                sSql += "certificado_palabra_clave, maneja_ssl, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCuenta.Text.Trim() + "', '" + txtPasswordCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtSmtp.Text.Trim() + "', " + Convert.ToInt32(txtPuerto.Text.Trim()) + "," + Environment.NewLine;
                sSql += "'" + txtCorreoCopia.Text.Trim() + "', '" + txtCorreoConsumidorFinal.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtCorreoAmbientePruebas.Text.Trim() + "', '" + txtEnvioPruebas.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtConsultaPruebas.Text.Trim() + "', '" + txtEnvioProduccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtConsultaProduccion.Text.Trim() + "', '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtPasswordCertificado.Text.Trim() + "', " + iManejaSSL + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTA EL QUERY DE INSERCIÓN
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro insertado éxitosamente.";
                ok.ShowDialog();
                cargarInformacion();
                parametros.cargarParametrosFacturacionElectronica();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
         private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cel_parametro set" + Environment.NewLine;
                sSql += "correo_que_envia = '" + txtCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_palabra_clave = '" + txtPasswordCuenta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_smtp = '" + txtSmtp.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_puerto = " + Convert.ToInt32(txtPuerto.Text.Trim()) + "," + Environment.NewLine;
                sSql += "correo_con_copia = '" + txtCorreoCopia.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_consumidor_final = '" + txtCorreoConsumidorFinal.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_ambiente_prueba = '" + txtCorreoAmbientePruebas.Text.Trim() + "'," + Environment.NewLine;
                sSql += "wsdl_pruebas = '" + txtEnvioPruebas.Text.Trim() + "'," + Environment.NewLine;
                sSql += "url_pruebas = '" + txtConsultaPruebas.Text.Trim() + "'," + Environment.NewLine;
                sSql += "wsdl_produccion = '" + txtEnvioProduccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "url_produccion = '" + txtConsultaProduccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "certificado_ruta = '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "certificado_palabra_clave = '" + txtPasswordCertificado.Text.Trim() + "'," + Environment.NewLine;
                sSql += "maneja_ssl = " + iManejaSSL + "," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_cel_parametro = " + iIdParametro;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                cargarInformacion();
                parametros.cargarParametrosFacturacionElectronica();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void frmParametros_Load(object sender, EventArgs e)
        {
            cargarInformacion();
        }

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

        private void chkPasswordCertificado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasswordCertificado.Checked == true)
            {
                txtPasswordCertificado.PasswordChar = '\0';
                txtPasswordCertificado.Focus();
            }
            else
            {
                txtPasswordCertificado.PasswordChar = '*';
                txtPasswordCertificado.Focus();
            }

            txtPasswordCertificado.SelectionStart = txtPasswordCertificado.Text.Trim().Length;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cargarInformacion();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Certificados digitales |*.p12";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el correo electrónico del emisor.";
                ok.ShowDialog();
                txtCuenta.Focus();
            }

            else if (txtPasswordCuenta.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la contraseña del correo electrónico del emisor.";
                ok.ShowDialog();
                txtPasswordCuenta.Focus();
            }

            else if (txtSmtp.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el Protocolo para Transferencia Simple de Correo (SMTP) del correo electrónico del emisor.";
                ok.ShowDialog();
                txtSmtp.Focus();
            }

            else if (txtPuerto.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el puerto del correo electrónico del emisor.";
                ok.ShowDialog();
                txtPuerto.Focus();
            }

            else if (txtCorreoCopia.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un correo electrónico para enviar copias.";
                ok.ShowDialog();
                txtCorreoCopia.Focus();
            }

            else if (txtCorreoConsumidorFinal.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese un correo electrónico para consumidor final.";
                ok.ShowDialog();
                txtCorreoConsumidorFinal.Focus();
            }

            else if (txtEnvioPruebas.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la URL de envio de comprobantes electrónicos en ambiente de pruebas.";
                ok.ShowDialog();
                txtEnvioPruebas.Focus();
            }

            else if (txtConsultaPruebas.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la URL de consulta de comprobantes electrónicos en ambiente de pruebas.";
                ok.ShowDialog();
                txtConsultaPruebas.Focus();
            }

            else if (txtEnvioProduccion.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la URL de envio de comprobantes electrónicos en ambiente de producción.";
                ok.ShowDialog();
                txtEnvioProduccion.Focus();
            }

            else if (txtConsultaProduccion.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la URL de consulta de comprobantes electrónicos en ambiente de producción.";
                ok.ShowDialog();
                txtConsultaProduccion.Focus();
            }

            else if (txtRuta.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor seleccione un certificado digital para firmar los comprobantes electrónicos";
                ok.ShowDialog();
                txtRuta.Focus();
            }

            else if (txtPasswordCertificado.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la contraseña del certificado digital del emisor.";
                ok.ShowDialog();
            }

            else
            {
                if (chkSSL.Checked == true)
                {
                    iManejaSSL = 1;
                }

                else
                {
                    iManejaSSL = 0;
                }

                if (bActualizar == true)
                {
                    if (cmbEstado.Text == "ACTIVO")
                    {
                        sEstado = "A";
                    }

                    else
                    {
                        sEstado = "N";
                    }

                    //ENVIAR A FUNCION PARA ACTUALIZAR EL REGISTRO
                    NuevoSiNo.lblMensaje.Text = "¿Desea actualizar el registro...?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        actualizarRegistro();
                    }
                }

                else
                {
                    //ENVIAR A FUNCION PARA INSERTAR EL REGISTRO
                    NuevoSiNo.lblMensaje.Text = "¿Desea guardar el registro...?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        insertarRegistro();
                    }
                }
            }
        }
    }
}
