using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmReenviarFacturaIndividual : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        Clases_Factura_Electronica.ClaseGenerarFacturaXml generar = new Clases_Factura_Electronica.ClaseGenerarFacturaXml();
        Clases_Factura_Electronica.ClaseFirmarXML firmar = new Clases_Factura_Electronica.ClaseFirmarXML();
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();
        Clases_Factura_Electronica.ClaseRIDE ride_2 = new Clases_Factura_Electronica.ClaseRIDE();
        Clases_Factura_Electronica.ClaseObtenerLogo logo;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sSecuenciaFactura;
        string sDirGenerados;
        string sDirFirmados;
        string sDirAutorizados;
        string sDirNoAutorizados;
        string sCorreoConsumidorFinal;
        string sCorreoAmbientePruebas;
        string sArchivoEnviar;
        string sClaveAcceso;
        string filename;
        string filenameRide;
        string miXMl;
        string sVersion = "1.0";
        string sUTF = "utf-8";
        string sStandAlone = "yes";
        string sRutaLogo;

        string sCodigoDocumento;

        string sIdTipoAmbiente;
        string sIdTipoEmision;

        string sEstadoAutorizacion;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdFactura;
        int iIdPersona;
        int iBanderPermitirFirmaElectronica;

        //VARIABLES PARA EL FIRMADO DEL XML
        string sArchivoFirmar;
        string sNumeroDocumento;

        string sJar;
        string sCertificado;
        string sPassCertificado;
        string sXmlPathOut;
        string sFileOut;
        string sCodigoError = "";
        string sDescripcionError = "";
        string[] sCertificado_digital = new string[5];

        //VARIABLES PARA CARGAR LOS PARAMETROS DE ENVIO DEL MAIL
        string P_St_correo_server_smtp;
        string P_St_from;
        string P_St_fromname;
        string P_St_correo_que_envia;
        string P_St_correo_con_copia;
        string P_St_correo_consumidor_final;
        string P_St_correo_ambiente_prueba;
        string P_St_correo_palabra_clave;
        long P_Ln_correo_puerto_smtp;
        int P_In_maneja_SSL;
        string P_St_telefono_empresa;
        string P_St_nombre_comercial;
        string sMensajeEnviar;
        string sTipoComprobante;
        string sMensajeRetorno;
        string sCorreoCliente;
        string sAsuntoMail;
        string srutaXML;
        string srutaRIDE;
        string sRutaAdjuntos;
        string sWSEnvioPruebas;
        string sWSConsultaPruebas;
        string sWSEnvioProduccion;
        string sWSConsultaProduccion;
        string sWebService;

        bool bRespuestaEnvioMail;

        Byte[] Logo_Factura { get; set; }

        public frmReenviarFacturaIndividual()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA ACTUALIZAR EL CORREO
        private bool actualizarCorreo()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "update tp_personas set" + Environment.NewLine;
                sSql += "correo_electronico = '" + txtMail.Text.Trim().ToLower() + "'" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersona;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();                    
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

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

        //FUNCION PARA BUSCAR LA FACTURA
        private void buscarFactura()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_factura" + Environment.NewLine;
                sSql += "where numero_factura = " + txtNumeroFactura.Text.Trim() + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "and idtipocomprobante = 1";

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
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros seleccionados";
                    ok.ShowDialog();
                    return;
                }

                int iFacturaElectronica = Convert.ToInt32(dtConsulta.Rows[0]["facturaelectronica"].ToString());

                txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString();
                txtApellidos.Text = dtConsulta.Rows[0]["apellidos"].ToString();
                txtNombres.Text = dtConsulta.Rows[0]["nombres"].ToString();
                txtDireccion.Text = dtConsulta.Rows[0]["direccion_factura"].ToString();
                txtTelefono.Text = dtConsulta.Rows[0]["telefono_factura"].ToString();
                txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString();
                txtFechaFactura.Text = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_factura"].ToString()).ToString("dd-MM-yyyy");
                txtSecuenciaFactura.Text = dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString() + "-" + dtConsulta.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');
                sSecuenciaFactura = dtConsulta.Rows[0]["establecimiento"].ToString() + dtConsulta.Rows[0]["punto_emision"].ToString() + dtConsulta.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');

                iIdFactura = Convert.ToInt32(dtConsulta.Rows[0]["id_factura"].ToString());
                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());

                Double dbCantidad;
                Double dbPrecioUnitario;
                Double dbTotal_1;
                Double dbSumaSubtotal_12 = 0;
                Double dbSumaSubtotal_0 = 0;
                Double dbSumaDescuento = 0;
                Double dbTotal_2;
                Double dbSumaIva = 0;
                Double dbTotal_3;
                int iPaga_Iva;
                string sProducto;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    iPaga_Iva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());

                    dbCantidad = Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    sProducto = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();
                    dbTotal_1 = dbCantidad * dbPrecioUnitario;

                    dgvDatos.Rows.Add(dbCantidad.ToString(), sProducto, dbPrecioUnitario, dbTotal_1.ToString("N2"));

                    if (iPaga_Iva == 1)
                        dbSumaSubtotal_12 += dbTotal_1;
                    else
                        dbSumaSubtotal_0 += dbTotal_1;

                    dbSumaDescuento += Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbSumaIva += Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString());
                }

                dbTotal_2 = dbSumaSubtotal_0 + dbSumaSubtotal_12 - dbSumaDescuento;
                dbTotal_3 = dbTotal_2 + dbSumaIva;

                txtSubtotal12.Text = dbSumaSubtotal_12.ToString("N2");
                txtSubtotal0.Text = dbSumaSubtotal_0.ToString("N2");
                txtDescuento.Text = dbSumaDescuento.ToString("N2");
                txtSubtotal.Text = dbTotal_2.ToString("N2");
                txtIVA.Text = dbSumaIva.ToString("N2");
                txtTotal.Text = dbTotal_3.ToString("N2");

                if (iFacturaElectronica == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La factura emitida no eselectrónica.";
                    ok.ShowDialog();
                    btnReenviarFactura.Enabled = false;
                    return;
                }

                dgvDatos.ClearSelection();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtNumeroFactura.Clear();
            txtSecuenciaFactura.Clear();
            txtFechaFactura.Clear();
            txtIdentificacion.Clear();
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            txtSubtotal12.Text = "0.00";
            txtSubtotal0.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtTotal.Text = "0.00";

            btnReenviarFactura.Visible = false;
            int iVer = datosCertificado();

            if ((iVer == -1) && (iBanderPermitirFirmaElectronica == 0))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error al consultar los parámetros de la firma electrónica.";
                ok.ShowDialog();
            }

            else if (iVer == 1)
                btnReenviarFactura.Visible = true;

            lblEstadoReenvio.Text = "Estado Reenvío";

            dgvDatos.Rows.Clear();
            txtNumeroFactura.Focus();
        }

        //FUNCION PARA CARGAR LOS PARAMETROS DE ENVIO DEL CORREO CON LOS ARCHIVOS ADJUNTOS
        public void traerparametrosMail()
        {
            try
            {
                P_St_correo_que_envia = "";
                P_Ln_correo_puerto_smtp = 0;

                sSql = "";
                sSql += "select correo_que_envia, correo_con_copia," + Environment.NewLine;
                sSql += "correo_consumidor_final,correo_ambiente_prueba,correo_palabra_clave," + Environment.NewLine;
                sSql += "correo_smtp,correo_puerto, maneja_SSL, wsdl_pruebas," + Environment.NewLine;
                sSql += "url_pruebas, wsdl_produccion, url_produccion" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        P_St_correo_que_envia = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][0].ToString(), "");
                        P_St_from = P_St_correo_que_envia;
                        P_St_correo_con_copia = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][1].ToString(), "");
                        P_St_correo_consumidor_final = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][2].ToString(), "");
                        P_St_correo_ambiente_prueba = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][3].ToString(), "");
                        P_St_correo_palabra_clave = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][4].ToString(), "");
                        P_St_correo_server_smtp = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][5].ToString(), "");
                        P_Ln_correo_puerto_smtp = Convert.ToInt64(conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][6].ToString(), "0"));
                        P_In_maneja_SSL = Convert.ToInt32(conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][7].ToString(), "0"));
                        sWSEnvioPruebas = dtConsulta.Rows[0]["wsdl_pruebas"].ToString();
                        sWSConsultaPruebas = dtConsulta.Rows[0]["url_pruebas"].ToString();
                        sWSEnvioProduccion = dtConsulta.Rows[0]["wsdl_produccion"].ToString();
                        sWSConsultaProduccion = dtConsulta.Rows[0]["url_produccion"].ToString();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //==================================================================================================
                sSql = "";
                sSql += "select telefono, nombrecomercial" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        P_St_telefono_empresa = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][0].ToString(), "");
                        P_St_nombre_comercial = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0][1].ToString(), "");
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
            }
        }

        //FUNCION PARA CONSULTAR EL TIPO DE AMBIENTE CONFIGURADO EN EL SISTEMA
        private void consultarTipoAmbiente()
        {
            try
            {
                sSql = "";
                sSql += "select TA.codigo" + Environment.NewLine;
                sSql += "from sis_empresa E,cel_tipo_ambiente TA" + Environment.NewLine;
                sSql += "where E.id_tipo_ambiente = TA.id_tipo_ambiente" + Environment.NewLine;
                sSql += "and E.estado = 'A'" + Environment.NewLine;
                sSql += "and TA.estado = 'A'" + Environment.NewLine;
                sSql += "order By TA.codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sIdTipoAmbiente = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se encuentra información de configuración del Tipo de Ambiente";
                        ok.ShowDialog();
                    }
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

        //FUNCION PARA CONSULTAR EL TIPO DE EMISION CONFIGURADO EN EL SISTEMA
        private void consultarTipoEmision()
        {
            try
            {
                sSql = "";
                sSql += "select TE.codigo" + Environment.NewLine;
                sSql += "from sis_empresa E,cel_tipo_emision TE" + Environment.NewLine;
                sSql += "where E.id_tipo_emision = TE.id_tipo_emision" + Environment.NewLine;
                sSql += "and E.estado = 'A'" + Environment.NewLine;
                sSql += "and TE.estado = 'A'" + Environment.NewLine;
                sSql += "order By TE.codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sIdTipoEmision = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se encuentra información de configuración del Tipo de Emisión";
                        ok.ShowDialog();
                    }
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

        //FUNCION PARA CARGAR LOS PARAMETROS PARA EL FIRMADO DE LOS XML
        private int datosCertificado()
        {
            try
            {
                firmar.Gsub_trae_parametros_certificado(sCertificado_digital);
                sCertificado = sCertificado_digital[0];
                sPassCertificado = sCertificado_digital[1];

                if (!File.Exists(sCertificado))
                {
                    iBanderPermitirFirmaElectronica = 0;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra el archivo de la firma electrónica." + Environment.NewLine + "RUTA: " + sCertificado;
                    ok.ShowDialog();
                    return 0;
                }

                iBanderPermitirFirmaElectronica = 1;
                return 1;
            }

            catch (Exception ex)
            {
                //catchMensaje = new VentanasMensajes.frmMensajeCatch();
                //catchMensaje.LblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                iBanderPermitirFirmaElectronica = 0;
                return -1;
            }
        }

        //CARGAR EL DIRECTORIO DONDE SE GUARDARAN LOS XML GENERADOS
        private bool buscarDirectorio()
        {
            try
            {
                //DIRECTORIOS DE LA FACTURACION ELECTRONICA
                sSql = "";
                sSql += "select id_directorio, id_tipo_comprobante, orden, codigo, nombres" + Environment.NewLine;
                sSql += "from cel_directorio" + Environment.NewLine;
                sSql += "where id_tipo_comprobante = 1" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count < 4)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Los directorios para los archivos de facturación no se encuentran configurados.";
                    ok.ShowDialog();
                    return false;
                }

                sDirGenerados = dtConsulta.Rows[0]["nombres"].ToString();
                sDirFirmados = dtConsulta.Rows[1]["nombres"].ToString();
                sDirAutorizados = dtConsulta.Rows[2]["nombres"].ToString();
                sDirNoAutorizados = dtConsulta.Rows[3]["nombres"].ToString();

                consultarDirectoriosExistentes();

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

        //FUNCION PARA VERIFICAR SI LOS DIRECTORIOS EXISTEN
        private int consultarDirectoriosExistentes()
        {
            try
            {
                if (!Directory.Exists(sDirGenerados))
                {
                    DirectoryInfo generado = Directory.CreateDirectory(sDirGenerados);
                }

                if (!Directory.Exists(sDirFirmados))
                {
                    DirectoryInfo firmado = Directory.CreateDirectory(sDirFirmados);
                }

                if (!Directory.Exists(sDirAutorizados))
                {
                    DirectoryInfo autorizado = Directory.CreateDirectory(sDirAutorizados);
                }

                if (!Directory.Exists(sDirNoAutorizados))
                {
                    DirectoryInfo no_autorizado = Directory.CreateDirectory(sDirNoAutorizados);
                }

                return 1;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS DE CONFIGURACION DE COMPROBANTES ELECTRONICOS
        private void parametrosFacturacion()
        {
            try
            {
                sSql = "";
                sSql += "select correo_consumidor_final, correo_ambiente_prueba" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCorreoConsumidorFinal = dtConsulta.Rows[0]["correo_consumidor_final"].ToString();
                        sCorreoAmbientePruebas = dtConsulta.Rows[0]["correo_ambiente_prueba"].ToString();
                    }

                    else
                    {
                        sCorreoConsumidorFinal = "";
                        sCorreoAmbientePruebas = "";
                    }
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

        #endregion

        #region FUNCIONES PARA EL PROCESO DE SINCRONIZACION DE FACTURAS ELECTRONICAS

        //FUNCION PARA GENERAR EL XML
        private bool generarXML(long iIdFactura_P)
        {
            try
            {
                generar.GSub_GenerarFacturaXML(iIdFactura_P, 1, sIdTipoEmision, sIdTipoAmbiente, sDirGenerados, "FACTURA", 2, sCorreoConsumidorFinal, sCorreoAmbientePruebas);
                return true;
            }

            catch (Exception ex)
            {
                //catchMensaje.lblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return false;
            }
        }

        //INSTRUCCIONES PARA FIRMAR EL DOCUMENTO XML
        private bool firmarArchivoXML(string sNumeroDocumento_P)
        {
            try
            {
                sArchivoFirmar = sDirGenerados + @"\" + sNumeroDocumento_P + ".xml";

                sJar = @"c:\SRI.jar";

                sXmlPathOut = sDirFirmados + @"\";

                sFileOut = sNumeroDocumento_P + ".xml";

                sCodigoError = firmar.GSub_FirmarXML(sJar, sCertificado, sPassCertificado, sArchivoFirmar, sXmlPathOut, sFileOut, sCodigoError, sDescripcionError);

                if (sCodigoError == "00")
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                //catchMensaje.lblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return false;
            }

        }

        //CREAR RIDE
        private void crearRide(long iIdFactura_P, string sNumeroDocumento)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Genera_Ride(dtConsulta, iIdFactura_P);

                if (bRespuesta == true)
                {
                    //bRespuesta = ride.generarRide(dtConsulta, filenameRide, iIdFactura_P);
                    bRespuesta = ride_2.generarRide(dtConsulta, filenameRide, iIdFactura_P, Logo_Factura);

                    if (bRespuesta == false)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Error al crear el reporte RIDE de la factura " + sNumeroDocumento;
                        ok.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ENVIAR AL CORREO ELECTRONICO REENVIO
        private bool enviarMailReenvio(long iIdFactura_P, string sNumeroDocumento_P)
        {
            try
            {
                filenameRide = sDirAutorizados + @"\" + sNumeroDocumento_P + ".pdf";

                crearRide(iIdFactura_P, sNumeroDocumento);

                sCorreoCliente = txtMail.Text.Trim().ToLower();
                sAsuntoMail = P_St_nombre_comercial + ", Envio de Comprobante electrónico " + sNumeroDocumento_P;
                srutaXML = sDirFirmados + @"\" + sNumeroDocumento_P + ".xml";
                srutaRIDE = sDirAutorizados + @"\" + sNumeroDocumento_P + ".pdf";
                sRutaAdjuntos = "";

                if (srutaRIDE != "")
                {
                    sRutaAdjuntos = sRutaAdjuntos + srutaXML + "|" + srutaRIDE;
                }
                else
                {
                    sRutaAdjuntos = sRutaAdjuntos + srutaXML;
                }

                sMensajeRetorno = crearMensajeEnvio(sNumeroDocumento_P);
                bRespuestaEnvioMail = correo.enviarCorreo(P_St_correo_server_smtp, Convert.ToInt32(P_Ln_correo_puerto_smtp), P_St_from,
                                    P_St_correo_palabra_clave, P_St_fromname, sCorreoCliente,
                                    P_St_correo_con_copia, P_St_correo_consumidor_final, sAsuntoMail,
                                    sRutaAdjuntos, sMensajeRetorno, P_In_maneja_SSL);

                if (bRespuestaEnvioMail == true)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CREAR EL CUERPO DEL MENSAJE
        private string crearMensajeEnvio(string sNumeroDocumento_P)
        {
            try
            {
                sMensajeEnviar = "";
                //sCodigoDocumento = dgvDatos.Rows[iFila_P].Cells["colTipo"].Value.ToString();

                if (sCodigoDocumento == "FAC")
                {
                    sTipoComprobante = "FACTURA";
                }

                sMensajeEnviar = sMensajeEnviar + "Estimado Cliente " + (txtNombres.Text.Trim() +  " " + txtApellidos.Text.Trim()).Trim() + ":" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + P_St_nombre_comercial + " informa que en cumplimiento con la Resolución No.NAC-DGERCGC17-00000430. ";
                sMensajeEnviar = sMensajeEnviar + "emitida por el SRI, adjunto a este correo se encuentra su " + sTipoComprobante + " electrónica No. " + sNumeroDocumento_P;
                sMensajeEnviar = sMensajeEnviar + " en formato XML, así como su interpretación en formato PDF. " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;

                sMensajeEnviar = sMensajeEnviar + "Favor no responder a este correo, cualquier consulta realice a nuestra oficina, teléfono " + P_St_telefono_empresa + "." + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Mayor información sobre facturación electrónica en: https://www.sri.gob.ec " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Atentamente, " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + P_St_nombre_comercial;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine + Environment.NewLine;

                return sMensajeEnviar;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }

        //FUNCION PARA SABER SI HAY CONEXION A INTERNET
        private bool conexionInternet()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("www.google.com");
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void btnCorreoElectronicoDefault_Click(object sender, EventArgs e)
        {
            txtMail.ReadOnly = false;
            txtMail.Focus();
        }

        private void frmReenviarFacturaIndividual_Load(object sender, EventArgs e)
        {
            llenarComboLocalidades();

            btnReenviarFactura.Visible = false;
            int iVer = datosCertificado();

            if ((iVer == -1) && (iBanderPermitirFirmaElectronica == 0))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error al consultar los parámetros de la firma electrónica.";
                ok.ShowDialog();
            }

            else if (iVer == 1)
                btnReenviarFactura.Visible = true;

            consultarTipoAmbiente();
            consultarTipoEmision();
            traerparametrosMail();
            parametrosFacturacion();
            this.ActiveControl = txtNumeroFactura;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNumeroFactura.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de factura a buscar.";
                ok.ShowDialog();
                txtNumeroFactura.Focus();
                return;
            }

            buscarFactura();
        }

        private void txtNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmReenviarFacturaIndividual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private async void btnReenviarFactura_Click(object sender, EventArgs e)
        {
            Task<bool> task;
            bool bConsulta;

            if (conexionInternet() == false)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay una conexión a internet. Favor verifique la conectividad.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Desea reenviar el comprobante seleccionado?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                buscarDirectorio();

                //AQUI TRAER EL LOGO
                //-----------------------------------------------------------------
                logo = new Clases_Factura_Electronica.ClaseObtenerLogo();

                if (logo.obtenerRutaLogo() == false)
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = logo.sRespuesta;
                    ok.ShowDialog();
                    return;
                }

                else
                {
                    sRutaLogo = logo.sRespuesta;
                }

                //SE INSTANCIA EL LOGO EN MEMORIA PARA ENVIAR
                if (sRutaLogo == "")
                {
                    Bitmap My_Logo = Properties.Resources.SIN_LOGO;
                    var ms = new MemoryStream();

                    My_Logo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Logo_Factura = ms.ToArray();
                }

                else
                {
                    if (File.Exists(sRutaLogo))
                        Logo_Factura = File.ReadAllBytes(sRutaLogo);
                    else
                    {
                        Bitmap My_Logo = Properties.Resources.SIN_LOGO;
                        var ms = new MemoryStream();

                        My_Logo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Logo_Factura = ms.ToArray();
                    }
                }
                //-----------------------------------------------------------------

                if (actualizarCorreo() == false)
                {
                    return;
                }

                string sNumeroDocumento = "F" + sSecuenciaFactura;

                //USANDO TASK ASYNC AWAIT

                // PASO 1.- INVOCAR A FUNCION PARA PRIMERO GENERAR EL XML
                task = new Task<bool>(() => generarXML(Convert.ToInt64(iIdFactura)));
                task.Start();
                lblEstadoReenvio.Text = "Generando XML.";
                bConsulta = await task;

                if (bConsulta == true)
                    lblEstadoReenvio.Text = "XML Generado.";
                else
                {
                    lblEstadoReenvio.Text = "Error generar XML.";
                    return;
                }

                // PASO 2.- INVOCAR A FUNCION PARA FIRMAR EL XML
                task = new Task<bool>(() => firmarArchivoXML(sNumeroDocumento));
                task.Start();
                lblEstadoReenvio.Text = "Firmando XML";
                bConsulta = await task;

                if (bConsulta == true)
                    lblEstadoReenvio.Text = "XML Firmado.";
                else
                {
                    lblEstadoReenvio.Text = "Error firmar XML.";
                    return;
                }

                Thread.Sleep(3000);

                //PASO 5.- ENVIAR AL CORREO ELECTRONICO DEL CLIENTE
                task = new Task<bool>(() => enviarMailReenvio(Convert.ToInt64(iIdFactura), sNumeroDocumento));
                task.Start();
                lblEstadoReenvio.Text = "Enviando al correo.";
                bConsulta = await task;

                if (bConsulta == true)
                {
                    lblEstadoReenvio.Text = "Correo enviado.";
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Se ha enviado la factura éxitosamente.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    limpiar();
                    return;
                }

                else
                {
                    lblEstadoReenvio.Text = "Error al enviar al correo.";
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al enviar la factura al correo.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    return;
                }

                this.Cursor = Cursors.Default;
            }
        }
    }
}
