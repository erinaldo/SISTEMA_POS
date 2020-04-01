using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmSincronizacionIndividual : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases_Factura_Electronica.ClaseConsultarXML consultarXML = new Clases_Factura_Electronica.ClaseConsultarXML();
        Clases_Factura_Electronica.ClaseXMLAyuda xmlH = new Clases_Factura_Electronica.ClaseXMLAyuda();

        Clases_Factura_Electronica.ClaseGenerarFacturaXml generar = new Clases_Factura_Electronica.ClaseGenerarFacturaXml();
        Clases_Factura_Electronica.ClaseFirmarXML firmar = new Clases_Factura_Electronica.ClaseFirmarXML();
        Clases_Factura_Electronica.ClaseEnviarXML enviar = new Clases_Factura_Electronica.ClaseEnviarXML();
        //Clases_Factura_Electronica.ClaseConsultarXML consultar = new Clases_Factura_Electronica.ClaseConsultarXML();
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();
        Clases_Factura_Electronica.ClaseGenerarRIDE ride = new Clases_Factura_Electronica.ClaseGenerarRIDE();


        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;

        string sAyuda;
        string sCodigoDocumento;
        string sCodigoDocumentoLetras;
        string sCaracterDocumento;
        string sNumeroDocumento;
        string sRutaArchivo;
        string sRutaXmlFirmado;
        string miXMl;
        string sFecha;

        string sDirGenerados;
        string sDirFirmados;
        string sDirAutorizados;
        string sDirNoAutorizados;
        string sCorreoConsumidorFinal;
        string sCorreoAmbientePruebas;

        string sIdTipoAmbiente;
        string sIdTipoEmision;
        string sArchivoEnviar;
        string sClaveAcceso;
        string filename;
        string sEstadoEnvio;
        string sEstadoProceso;
        string sDetalleError;
        string sNumeroAutorizacion;
        string sFechaAutorizacion;

        string sWSEnvioPruebas;
        string sWSConsultaPruebas;
        string sWSEnvioProduccion;
        string sWSConsultaProduccion;
        string sWebService;

        string sVersion = "1.0";
        string sUTF = "utf-8";
        string sStandAlone = "yes";

        XmlDocument xmlAut;
        XDocument xml;
        XElement autorizacion;

        int iCol_Correlativo;
        int iCol_Codigo;
        int iCol_Descripcion;
        int iIdFactura;
        int iIdCambioCombo;
        int iIdTipoAmbiente;

        //VARIABLES PARA EL FIRMADO DEL XML
        string sArchivoFirmar;

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

        bool bRespuestaEnvioMail;

        private XmlDocument xmlDoc;


        //VARIABLES QUE SE EXTRAE EN LA CONSULTA DE LA FACTURA
        string sCliente;

        public frmSincronizacionIndividual()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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
                        ok.LblMensaje.Text = "No se encuentra información de configuración del Tipo de Ambiente";
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
                        ok.LblMensaje.Text = "No se encuentra información de configuración del Tipo de Emisión";
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

        //FUNCION PARA CARGAR LOS PARAMETROS PARA EL FIRMADO DE LOS XML
        private void datosCertificado()
        {
            try
            {
                firmar.Gsub_trae_parametros_certificado(sCertificado_digital);
                sCertificado = sCertificado_digital[0];
                sPassCertificado = sCertificado_digital[1];
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CARGAR EL DIRECTORIO DONDE SE GUARDARAN LOS XML GENERADOS
        private void buscarDirectorio()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_tipo_comprobante, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_comprobante";

                cmbTipoComprobante.llenar(dtConsulta, sSql);

                if (cmbTipoComprobante.Items.Count > 0)
                {
                    cmbTipoComprobante.SelectedIndex = 1;                    
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RELLENAR LAS INSTRUCCIONES SQL
        private void llenarInstruccionesSQL()
        {
            sAyuda = consultarXML.GSub_ActualizaPantalla(sCodigoDocumento, 0);
            iCol_Correlativo = 4;
            iCol_Codigo = 0;
            iCol_Descripcion = 1;
            dB_Ayuda_Facturas.Ver(sAyuda, "", iCol_Correlativo, iCol_Codigo, iCol_Descripcion);
        }

        //FUNCION PARA LIMPIAR EL FORMULARIO
        private void limpiar()
        {
            iIdFactura = 0;
            txtClaveAcceso.Clear();
            txtEstadoEnvio.Clear();
            txtNumeroAutorizacion.Clear();
            txtFechaAutorizacion.Clear();
            txtDetalles_1.Clear();
            txtDetalles_2.Clear();
            txtCorreoCliente.Clear();
            txtEstadoProceso.Clear();

            dB_Ayuda_Facturas.limpiar();
            llenarInstruccionesSQL();

            //llenarComboTipoComprobante();
            //llenarComboTipoAmbiente();
            //llenarComboTipoEmision();
        }

        //LLENAR EL COMBOBOX DE TIPO DE COMPROBANTE
        private void llenarComboTipoComprobante()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_tipo_comprobante, nombres " + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_comprobante";

                cmbTipoComprobante.llenar(dtConsulta, sSql);

                if (cmbTipoComprobante.Items.Count > 0)
                {
                    cmbTipoComprobante.SelectedIndex = 1;
                    iIdCambioCombo = 1;
                    llenarInstruccionesSQL();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE TIPOS DE AMBIENTES
        private void llenarComboTipoAmbiente()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "Select id_tipo_ambiente, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_ambiente" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "Order By codigo";

                cmbTipoAmbiente.llenar(dtConsulta, sSql);
                cmbTipoAmbiente_1.llenar(dtConsulta, sSql);

                if (cmbTipoAmbiente.Items.Count > 0)
                {
                    cmbTipoAmbiente.SelectedIndex = 1;
                }

                if (cmbTipoAmbiente_1.Items.Count > 0)
                {
                    cmbTipoAmbiente_1.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE TIPOS DE EMISION
        private void llenarComboTipoEmision()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "Select id_tipo_emision, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_emision" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "Order By codigo";

                cmbTipoEmision.llenar(dtConsulta, sSql);

                if (cmbTipoEmision.Items.Count > 0)
                {
                    cmbTipoEmision.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA CARGAR LOS PARAMETROS DE ENVIO DEL CORREO CON LOS ARCHIVOS ADJUNTOS
        public void traerparametrosMail()
        {
            try
            {
                P_St_correo_que_envia = "";
                P_Ln_correo_puerto_smtp = 0;

                sSql = "";
                sSql += "select correo_que_envia,correo_con_copia," + Environment.NewLine;
                sSql += "correo_consumidor_final,correo_ambiente_prueba,correo_palabra_clave," + Environment.NewLine;
                sSql += "correo_smtp,correo_puerto, maneja_SSL" + Environment.NewLine;
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
                        //P_St_fromname = G_St_Nombre_Empresa;

                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
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

        fin: { }

        }


        //FUNCION PARA EXTRAER LOS DATOS PARA EL PROCESO DE FACTURACION
        private int extraerInformacion(long iIdFactura)
        {
            try
            {
                sSql = "";
                sSql += "select F.id_factura, VL.nombre_localidad, F.fecha_factura, VL.establecimiento, VL.punto_emision," + Environment.NewLine;
                sSql += "NF.numero_factura, ltrim((isnull(P.nombres, '') + ' ' + P.apellidos)) cliente," + Environment.NewLine;
                sSql += "isnull(P.correo_electronico, '') correo_electronico, F.id_tipo_ambiente" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_numeros_facturas NF, tp_personas P, tp_vw_localidades VL" + Environment.NewLine;
                sSql += "where NF.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and F.id_localidad = VL.id_localidad" + Environment.NewLine;
                sSql += "and F.id_persona = P.id_persona" + Environment.NewLine;
                sSql += "and VL.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and NF.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and F.id_factura = " + iIdFactura;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //VARIABLES QUE SE EXTRAE EN LA CONSULTA DE LA FACTURA
                        sFecha = dtConsulta.Rows[0][2].ToString().Substring(0, 10);
                        txtNumeroDocumento.Text = sCaracterDocumento + dtConsulta.Rows[0][3].ToString() + dtConsulta.Rows[0][4].ToString() + dtConsulta.Rows[0][5].ToString().PadLeft(9, '0');
                        sCliente = dtConsulta.Rows[0][6].ToString();
                        sCorreoCliente = dtConsulta.Rows[0][7].ToString();
                        txtCorreoCliente.Text = sCorreoCliente;
                        iIdTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());

                        return 1;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }


        //FUNCION PARA CONSULTAR LOS DIRECTORIOS
        private void consultarDirectorios()
        {
            try
            {
                sSql = "";
                sSql += "select nombres" + Environment.NewLine;
                sSql += "from cel_directorio" + Environment.NewLine;
                sSql += "where id_tipo_comprobante = " + Convert.ToInt32(cmbTipoComprobante.SelectedValue) + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sDirGenerados = dtConsulta.Rows[0][0].ToString();
                        sDirFirmados = dtConsulta.Rows[1][0].ToString();
                        sDirAutorizados = dtConsulta.Rows[2][0].ToString();
                        sDirNoAutorizados = dtConsulta.Rows[3][0].ToString();

                        if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 1)
                        {
                            sCodigoDocumento = "01";
                            sCaracterDocumento = "F";
                            sCodigoDocumentoLetras = "FAC";
                        }

                        else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 2)
                        {
                            sCodigoDocumento = "04";
                            sCaracterDocumento = "NC";
                            sCodigoDocumentoLetras = "NOTACREDITO";
                        }

                        else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 3)
                        {
                            sCodigoDocumento = "05";
                            sCaracterDocumento = "ND";
                            sCodigoDocumentoLetras = "NOTADEBITO";
                        }

                        else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 4)
                        {
                            sCodigoDocumento = "06";
                            sCaracterDocumento = "G";
                            sCodigoDocumentoLetras = "GUIA";
                        }

                        else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 5)
                        {
                            sCodigoDocumento = "07";
                            sCaracterDocumento = "R";
                            sCodigoDocumentoLetras = "RETENCION";
                        }

                        llenarInstruccionesSQL();
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
        }

        //CONSULTAR LOS WEB SERVICE
        private void consultarWebService()
        {
            try
            {
                sSql = "";
                sSql += "select wsdl_pruebas, url_pruebas, wsdl_produccion, url_produccion" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sWSEnvioPruebas = dtConsulta.Rows[0]["wsdl_pruebas"].ToString();
                        sWSConsultaPruebas = dtConsulta.Rows[0]["url_pruebas"].ToString();
                        sWSEnvioProduccion = dtConsulta.Rows[0]["wsdl_produccion"].ToString();
                        sWSConsultaProduccion = dtConsulta.Rows[0]["url_produccion"].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se ha configurado los parámetros de emisión de comprobantes electrónicos";
                        ok.ShowDialog();
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        #region FUNCIONES DE SINCRONIZACION 

        //FUNCION PARA GENERAR EL XML
        private bool generarXML(long iIdFactura_P)
        {
            try
            {
                generar.GSub_GenerarFacturaXML(iIdFactura_P, 1, sIdTipoAmbiente, sIdTipoEmision, sDirGenerados, "FACTURA", 2, sCorreoConsumidorFinal, sCorreoAmbientePruebas);
                return true;
            }

            catch (Exception ex)
            {
                //catchMensaje.LblMensaje.Text = ex.Message;
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
                //catchMensaje.LblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return false;
            }

        } 


        //INSTRUCCIONES PARA FIRMAR EL DOCUMENTO XML
        private bool enviarArchivoXML()
        {
            try
            {
                sArchivoEnviar = sDirFirmados + @"\" + txtNumeroDocumento.Text.Trim() + ".xml";

                if (iIdTipoAmbiente == 1)
                {
                    sWebService = sWSConsultaPruebas;
                }

                else if (iIdTipoAmbiente == 2)
                {
                    sWebService = sWSConsultaProduccion;
                }

                RespuestaSRI respuesta = enviar.EnvioComprobante(sArchivoEnviar, sWebService);

                sEstadoEnvio = respuesta.Estado;
               
                return true;
            }

            catch (Exception ex)
            {
                //catchMensaje.LblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return false;
            }
        }


        //INSTRUCCIONES PARA CONSULTAR EL ESTADO DEL XML DEL SRI
        private bool consultarArchivoXML()
        {
            try
            {
                sClaveAcceso = consultarClaveAcceso(iIdFactura);
                //txtClaveAcceso.Text = sClaveAcceso;

                if (sClaveAcceso != "")
                {
                    if (iIdTipoAmbiente == 1)
                    {
                        sWebService = sWSConsultaPruebas;
                    }

                    else if (iIdTipoAmbiente == 2)
                    {
                        sWebService = sWSConsultaProduccion;
                    }

                    RespuestaSRI respuesta = consultarXML.AutorizacionComprobante(out xmlAut, sClaveAcceso, sWebService);

                    //dgvDatos.Rows[iFila_P].Cells["colEstado"].Value = respuesta.Estado;
                    sEstadoEnvio = respuesta.Estado;
                    sNumeroAutorizacion = respuesta.NumeroAutorizacion;
                    sFechaAutorizacion = respuesta.FechaAutorizacion;

                    //Genera y guarda el XML autorizado
                    filename = Path.GetFileNameWithoutExtension(txtNumeroDocumento.Text.Trim()) + ".xml";

                    if (respuesta.Estado == "AUTORIZADO")
                    {
                        filename = Path.Combine(sDirAutorizados, filename);
                    }

                    else
                    {
                        sDetalleError= respuesta.ErrorMensaje;
//                        dgvDatos.Rows[iFila_P].Cells["colEstado"].Style.BackColor = Color.Red;
                        filename = Path.Combine(sDirNoAutorizados, filename);
                    }
                    

                    xmlAutorizado(respuesta, filename);
                    actualizarDatos();

                    //ENVIAR A FUNCION PARA CREAR EL PDF
                    filename = Path.GetFileNameWithoutExtension(txtNumeroDocumento.Text.Trim()) + ".pdf";
                    filename = Path.Combine(sDirAutorizados, filename);
                    crearRide(filename);

                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                //catchMensaje.LblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return false;
            }
        }

        //CREAR RIDE
        private void crearRide(string filename)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Genera_Ride(dtConsulta, dB_Ayuda_Facturas.iId);

                if (bRespuesta == true)
                {
                    bRespuesta = ride.generarRide(dtConsulta, filename, dB_Ayuda_Facturas.iId);

                    if (bRespuesta == false)
                    {
                        ok.LblMensaje.Text = "Error al crear el reporte RIDE de la factura " + sNumeroDocumento;
                        ok.ShowDialog();
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //CONSULTAR LA CLAVE DE ACCESO DE CADA FACTURA DEL GRID
        private string consultarClaveAcceso(long iIdFactura_P)
        {
            try
            {
                sSql = "";
                sSql += "select " + conexion.GFun_St_esnulo() + "(clave_acceso, 'NINGUNA') clave_acceso " + Environment.NewLine;
                sSql += "from cv403_facturas " + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        return "";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return "";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }


        //CONSTRUIR XML AUTORIZADO
        private void xmlAutorizado(RespuestaSRI sri, string filename)
        {
            try
            {
                sArchivoFirmar = sDirFirmados + @"\" + txtNumeroDocumento.Text.Trim() + ".xml";

                miXMl = File.ReadAllText(sArchivoFirmar);
                //Declaramos el documento y su definición
                xml = new XDocument(
                    new XDeclaration(sVersion, sUTF, sStandAlone));

                autorizacion = new XElement("autorizacion");
                autorizacion.Add(new XElement("estado", sri.Estado));
                autorizacion.Add(new XElement("numeroAutorizacion", sri.NumeroAutorizacion));
                autorizacion.Add(new XElement("fechaAutorizacion", sri.FechaAutorizacion));
                autorizacion.Add(new XElement("ambiente", sri.Ambiente));
                autorizacion.Add(new XElement("comprobante", new XCData(miXMl)));
                autorizacion.Add(new XElement("mensajes", sri.ErrorMensaje));
                xml.Add(autorizacion);

                //PROBAR COMO GUARDA
                xml.Save(filename);
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ENVIAR AL CORREO ELECTRONICO
        private bool enviarMail()
        {
            try
            {
                sAsuntoMail = P_St_nombre_comercial + ", Envio de Comprobante electrónico " + txtNumeroDocumento.Text.Trim();
                srutaXML = sDirAutorizados + @"\" + txtNumeroDocumento.Text.Trim() + ".xml";
                srutaRIDE = sDirAutorizados + @"\" + txtNumeroDocumento.Text.Trim() + ".pdf";
                sRutaAdjuntos = "";

                if (srutaRIDE != "")
                {
                    sRutaAdjuntos = sRutaAdjuntos + srutaXML + "|" + srutaRIDE;
                }
                else
                {
                    sRutaAdjuntos = sRutaAdjuntos + srutaXML;
                }

                sMensajeRetorno = crearMensajeEnvio();
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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CREAR EL CUERPO DEL MENSAJE
        private string crearMensajeEnvio()
        {
            try
            {
                sMensajeEnviar = "";

                if (sCodigoDocumento == "FAC")
                {
                    sTipoComprobante = "FACTURA";
                }

                else if (sCodigoDocumento == "NOTADEBITO")
                {
                    sTipoComprobante = "NOTA DE DÉBITO";
                }

                else if (sCodigoDocumento == "NOTACREDITO")
                {
                    sTipoComprobante = "NOTA DE CRÉDITO";
                }

                else if (sCodigoDocumento == "GUIA")
                {
                    sTipoComprobante = "GUÍA DE REMISIÓN";
                }

                else if (sCodigoDocumento == "RETENCION")
                {
                    sTipoComprobante = "COMPROBANTE DE RETENCIÓN";
                }

                sMensajeEnviar = sMensajeEnviar + "Estimado Cliente " + sCliente + ":" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + P_St_nombre_comercial + " informa que en cumplimiento con la Resolución No.NAC-DGERCGC17-00000430. ";
                sMensajeEnviar = sMensajeEnviar + "emitida por el SRI, adjunto a este correo se encuentra su " + sTipoComprobante + " electrónica No. " + txtNumeroDocumento.Text.Trim();
                sMensajeEnviar = sMensajeEnviar + " en formato XML, así como su interpretación en formato PDF. " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;

                sMensajeEnviar = sMensajeEnviar + "Favor no responder a este correo, cualquier consulta realice a nuestra oficina, teléfono " + P_St_telefono_empresa + "." + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Mayor información sobre facturación electrónica en: http://www.sri.gob.ec/web/guest/comprobantes-electronicos1 " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Atentamente, " + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + P_St_nombre_comercial;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine + Environment.NewLine;

                return sMensajeEnviar;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }

        //ACTUALIZAR EN LA BASE DE DATOS
        private bool actualizarDatos()
        {
            try
            {
                if (sFecha != "")
                {
                    //SE INICIA UNA TRANSACCION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.LblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                    }

                    //UPDATE PARA FACTURAS
                    if (sCodigoDocumento == "01")
                    {
                        sSql = "";
                        sSql += "update cv403_facturas set" + Environment.NewLine;
                        sSql += "autorizacion = '" + sNumeroAutorizacion + "'," + Environment.NewLine;
                        sSql += "fecha_autorizacion = '" + Convert.ToDateTime(sFecha).ToString("yyyy/MM/dd") + "'" + Environment.NewLine;
                        sSql += "where id_factura = " + iIdFactura;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "No se pudo grabar la autorización de la factura.";
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    //UPDATE PARA COMPROBANTES DE RETENCION
                    else if (sCodigoDocumento == "07")
                    {
                        sSql = "";
                        sSql += "update cv405_cab_comprobantes_retencion set" + Environment.NewLine;
                        sSql += "autorizacion = '" + txtNumeroAutorizacion.Text.Trim() + "'," + Environment.NewLine;
                        sSql += "fecha_autorizacion = '" + sFecha + "'" + Environment.NewLine;
                        sSql += "where id_cab_comprobante_retencion = " + iIdFactura;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "No se pudo grabar la autorización del comprobante de retención";
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    //UPDATE PARA NOTAS DE CREDITO
                    else if (sCodigoDocumento == "04")
                    {
                        sSql = "";
                        sSql += "update cv403_notas_credito set" + Environment.NewLine;
                        sSql += "autorizacion = '" + txtNumeroAutorizacion.Text.Trim() + "'," + Environment.NewLine;
                        sSql += "fecha_autorizacion = '" + sFecha + "'" + Environment.NewLine;
                        sSql += "where id_nota_credito = " + iIdFactura;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "No se pudo grabar la autorización de la nota de crédito " + iIdFactura;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    //UPDATE PARA GUIAS DE REMISION
                    else if (sCodigoDocumento == "06")
                    {
                        sSql = "";
                        sSql += "update cv403_guias_remision set" + Environment.NewLine;
                        sSql += "autorizacion = '" + txtNumeroAutorizacion.Text.Trim() + "'," + Environment.NewLine;
                        sSql += "fecha_autorizacion = '" + sFecha + "'" + Environment.NewLine;
                        sSql += "where id_guia_remision = " + iIdFactura;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "No se pudo grabar la Autorización de la guía de remisión.";
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    return true;
                }

                else
                {
                    ok.LblMensaje.Text = "No ha ingresado el nombre del archivo autorizado";
                    ok.ShowDialog();
                    return false;
                }
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

        #endregion

        private void frmSincronizacionIndividual_Load(object sender, EventArgs e)
        {
            cmbTipoComprobante.SelectedIndexChanged -= new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            buscarDirectorio();
            cmbTipoComprobante.SelectedIndexChanged += new EventHandler(cmbTipoComprobante_SelectedIndexChanged);

            consultarDirectorios();

            datosCertificado();
            
            llenarComboTipoAmbiente();
            llenarComboTipoEmision();
            consultarTipoAmbiente();
            consultarTipoEmision();
            consultarWebService();
            parametrosFacturacion();
            traerparametrosMail();            

            limpiar();

            lblTipoDocumento.Text = cmbTipoComprobante.Text;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private async void btnValidar_Click(object sender, EventArgs e)
        {
            Task<bool> task;
            bool bConsulta;

            if (dB_Ayuda_Facturas.iId == 0)
            {
                ok.LblMensaje.Text = "No ha seleccionando ninguna factura.";
                ok.ShowDialog();
            }

            else
            {
                this.Cursor = Cursors.WaitCursor;

                if (extraerInformacion(dB_Ayuda_Facturas.iId) == 1)
                {
                    iIdFactura = dB_Ayuda_Facturas.iId;

                    //CONSULTAR DIRECTORIOS
                    consultarDirectorios();

                    //USANDO TASK ASYNC AWAIT

                    // PASO 1.- INVOCAR A FUNCION PARA PRIMERO GENERAR EL XML
                    task = new Task<bool>(() => generarXML(dB_Ayuda_Facturas.iId));
                    task.Start();
                    txtEstadoProceso.Text = "Generando XML...";
                    bConsulta = await task;

                    if (bConsulta == true)
                    {
                        txtEstadoProceso.Text = "XML Generado.";
                        txtEstadoProceso.BackColor= Color.Yellow;
                    }

                    else
                    {
                        txtEstadoProceso.Text = "Error generar XML.";
                        txtEstadoProceso.BackColor = Color.Red;
                        goto salir;
                    }

                    // PASO 2.- INVOCAR A FUNCION PARA FIRMAR EL XML
                    task = new Task<bool>(() => firmarArchivoXML(txtNumeroDocumento.Text.Trim()));
                    task.Start();
                    txtEstadoProceso.Text = "Firmando XML...";
                    bConsulta = await task;

                    if (bConsulta == true)
                    {
                        txtEstadoProceso.Text = "XML Firmado.";
                        txtEstadoProceso.BackColor = Color.Yellow;
                    }

                    else
                    {
                        txtEstadoProceso.Text = "Error firmar XML.";
                        txtEstadoProceso.BackColor = Color.Red;
                        goto salir;
                    }

                    Thread.Sleep(2000);

                    // PASO 3.- INVOCAR A FUNCION PARA ENVIAR EL XML AL SRI
                    task = new Task<bool>(() => enviarArchivoXML());
                    task.Start();
                    txtEstadoProceso.Text = "Enviando al SRI...";
                    bConsulta = await task;

                    if (bConsulta == true)
                    {                        
                        txtEstadoProceso.Text = "XML Enviado.";
                        txtEstadoProceso.BackColor = Color.Yellow;
                    }

                    else
                    {
                        txtEstadoProceso.Text = "Error al enviar XML.";
                        txtEstadoProceso.BackColor = Color.Red;
                        goto salir;
                    }

                    // PASO 4.- INVOCAR A FUNCION PARA CONSULTAR LA AUTORIZACION DEL SRI
                    task = new Task<bool>(() => consultarArchivoXML());
                    task.Start();
                    txtEstadoProceso.Text = "Consultando autorización SRI...";
                    bConsulta = await task;

                    if (bConsulta == true)
                    {
                        txtClaveAcceso.Text = sClaveAcceso;
                        txtNumeroAutorizacion.Text = sNumeroAutorizacion;
                        txtFechaAutorizacion.Text = sFechaAutorizacion;
                        txtEstadoEnvio.Text = sEstadoEnvio;                        
                        txtEstadoProceso.Text = "Consulta SRI éxitosa.";
                        txtEstadoProceso.BackColor = Color.Yellow;

                        if (sEstadoEnvio != "AUTORIZADO")
                        {
                            txtDetalles_1.Text = sDetalleError;
                            goto salir;
                        }                        
                    }

                    else
                    {
                        txtEstadoProceso.Text = "Error consultando SRI.";
                        txtEstadoProceso.BackColor = Color.Red;
                        goto salir;
                    }


                    //PASO 5.- ENVIAR AL CORREO ELECTRONICO DEL CLIENTE
                    task = new Task<bool>(() => enviarMail());
                    task.Start();
                    txtEstadoProceso.Text = "Enviando al correo...";
                    bConsulta = await task;

                    if (bConsulta == true)
                    {
                        txtEstadoProceso.Text = "Correo enviado.";
                        txtEstadoProceso.BackColor = Color.Yellow;
                    }

                    else
                    {
                        txtEstadoProceso.Text = "Error al enviar al correo.";
                        txtEstadoProceso.BackColor = Color.Red;
                        goto salir;
                    }                                
                }

                else
                {
                    ok.LblMensaje.Text = "No se encontró información del registro seleccionado.";
                    ok.ShowDialog();
                }

                
            }

            salir: {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarDirectorios();
        }
    }
}
