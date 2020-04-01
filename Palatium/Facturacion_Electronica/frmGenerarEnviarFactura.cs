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
using System.Xml;
using System.Xml.Linq;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmGenerarEnviarFactura : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeSiNo SiNo;

        Clases_Factura_Electronica.ClaseGenerarFacturaXml generar = new Clases_Factura_Electronica.ClaseGenerarFacturaXml();
        Clases_Factura_Electronica.ClaseFirmarXML firmar = new Clases_Factura_Electronica.ClaseFirmarXML();
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();
        Clases_Factura_Electronica.ClaseRIDE ride_2 = new Clases_Factura_Electronica.ClaseRIDE();
        Clases_Factura_Electronica.ClaseObtenerLogo logo;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        DataTable dtConsulta;

        XmlDocument xmlAut;
        XDocument xml;
        XElement autorizacion;

        int iNumeroRegistros;
        int iErrorGenerados;
        int iErrorFirmados;
        int iErrorEnviarSRI;
        int iErrorConsultarXMLSRI;
        int iErrorEnviarMail;
        int iEnviadosExitosamente;
        int iBanderPermitirFirmaElectronica;

        string sSql;
        string sFechaInicial;
        string sFechaFinal;
        string sDirGenerados;
        string sDirFirmados;
        string sDirAutorizados;
        string sDirNoAutorizados;
        string sCorreoConsumidorFinal;
        string sCorreoAmbientePruebas;
        string sRutaLogo;

        string sArchivoEnviar;
        string sClaveAcceso;
        string filename;
        string filenameRide;
        string miXMl;
        string sVersion = "1.0";
        string sUTF = "utf-8";
        string sStandAlone = "yes";

        string sCodigoDocumento;

        string sIdTipoAmbiente;
        string sIdTipoEmision;

        string sEstadoAutorizacion;

        bool bRespuesta;

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

        public frmGenerarEnviarFactura(string sCodigoDocumento)
        {
            this.sCodigoDocumento = sCodigoDocumento;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
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
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentra información de configuración del Tipo de Ambiente";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentra información de configuración del Tipo de Emisión";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra el archivo de la firma electrónica." + Environment.NewLine + "RUTA: " + sCertificado;
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
        private void buscarDirectorio()
        {
            try
            {
                //sSql = "";
                //sSql += "select codigo, nombres" + Environment.NewLine;
                //sSql += "from cel_directorio" + Environment.NewLine;
                //sSql += "where id_tipo_comprobante = 1" + Environment.NewLine;
                //sSql += "and estado = 'A'";

                //dtConsulta = new DataTable();
                //dtConsulta.Clear();

                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                //if (bRespuesta == true)
                //{
                //    if (dtConsulta.Rows.Count > 0)
                //    {
                //        sDirGenerados = dtConsulta.Rows[0][1].ToString();
                //        sDirFirmados = dtConsulta.Rows[1][1].ToString();
                //        sDirAutorizados = dtConsulta.Rows[2][1].ToString();
                //        sDirNoAutorizados = dtConsulta.Rows[3][1].ToString();
                //        return true;
                //    }

                //    else
                //    {
                //        ok = new VentanasMensajes.frmMensajeOK();
                //        ok.LblMensaje.Text = "No existe una configuracion de directorio para guardar los xml genereados.";
                //        ok.ShowDialog();
                //        return false;
                //    }
                //}

                //else
                //{
                //    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                //    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //    catchMensaje.ShowDialog();
                //    return false;
                //}

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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count < 4)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Los directorios para los archivos de facturación no se encuentran configurados.";
                    ok.ShowDialog();
                    return;
                }

                sDirGenerados = dtConsulta.Rows[0]["nombres"].ToString();
                sDirFirmados = dtConsulta.Rows[1]["nombres"].ToString();
                sDirAutorizados = dtConsulta.Rows[2]["nombres"].ToString();
                sDirNoAutorizados = dtConsulta.Rows[3]["nombres"].ToString();

                consultarDirectoriosExistentes();

                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        ///FUNCION PARA LIMPIAR
        private void limpiar()
        {
            LLenarComboEmpresa();
            LLenarComboLocalidad();

            btnReenviar.Visible = false;
            int iVer = datosCertificado();

            if ((iVer == -1) && (iBanderPermitirFirmaElectronica == 0))
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Error al consultar los parámetros de la firma electrónica.";
                ok.ShowDialog();
            }

            else if (iVer == 1)
                btnReenviar.Visible = true;

            chkSeleccionar.Checked = false;
            chkSeleccionar.Text = "Seleccionar todos los registros";

            txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //LLENAR EL COMBO DE EMPRESA
        private void LLenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select idempresa, case when nombrecomercial in ('', null) then" + Environment.NewLine;
                sSql += "razonsocial else nombrecomercial end nombre_comercial, *" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count >= 1)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBO DE LOCALIDADES
        private void LLenarComboLocalidad()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_localidad,nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                cmbLocalidad.llenar(dtConsulta, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sFechaInicial = Convert.ToDateTime(txtFechaInicial.Text.Trim()).ToString("yyyy/MM/dd");
                sFechaFinal = Convert.ToDateTime(txtFechaFinal.Text.Trim()).ToString("yyyy/MM/dd");

                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select F.id_factura, VL.nombre_localidad, F.fecha_factura, VL.establecimiento, VL.punto_emision," + Environment.NewLine;
                sSql += "NF.numero_factura, ltrim((isnull(P.nombres, '') + ' ' + P.apellidos)) cliente," + Environment.NewLine;
                sSql += "isnull(P.correo_electronico, '" + Program.sCorreoElectronicoDefault.Trim().ToLower() + "') correo_electronico" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_numeros_facturas NF, tp_personas P, tp_vw_localidades VL," + Environment.NewLine;
                sSql += "cv403_facturas_pedidos FP, pos_origen_orden O, cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where NF.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and F.id_localidad = VL.id_localidad" + Environment.NewLine;
                sSql += "and F.id_persona = P.id_persona" + Environment.NewLine;
                sSql += "and FP.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and VL.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and O.repartidor_externo = 0" + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and NF.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "and F.fecha_factura between '" + sFechaInicial + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "and F.facturaelectronica = 1" + Environment.NewLine;
                sSql += "and isnull(F.autorizacion, '') <> ''" + Environment.NewLine;
                sSql += "and F.id_tipo_emision = " + sIdTipoEmision + Environment.NewLine;
                sSql += "and F.id_tipo_ambiente = " + sIdTipoAmbiente + Environment.NewLine;
                sSql += "order by F.fecha_factura, NF.numero_factura";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvDatos.Rows.Add(
                                                false,
                                                dtConsulta.Rows[i][0].ToString(),
                                                dtConsulta.Rows[i][1].ToString(),
                                                "FAC",
                                                Convert.ToDateTime(dtConsulta.Rows[i][2].ToString()).ToString("dd/MM/yyyy"),
                                                dtConsulta.Rows[i][3].ToString(),
                                                dtConsulta.Rows[i][4].ToString(),
                                                dtConsulta.Rows[i][5].ToString().PadLeft(9, '0'),
                                                dtConsulta.Rows[i][6].ToString().Trim(),
                                                dtConsulta.Rows[i][7].ToString(),
                                                ""
                                );
                        }

                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                    }

                    else
                    {
                        dgvDatos.Rows.Clear();
                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                    }

                    dgvDatos.ClearSelection();
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                }

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
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
                    //AQUI INSTRUCCION DE SINCRONIZACION CON LOGO
                    //----------------------------------------------------------------------------------------------------


                    //----------------------------------------------------------------------------------------------------

                    bRespuesta = ride_2.generarRide(dtConsulta, filenameRide, iIdFactura_P, Logo_Factura);

                    if (bRespuesta == false)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Error al crear el reporte RIDE de la factura " + sNumeroDocumento;
                        ok.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ENVIAR AL CORREO ELECTRONICO REENVIO
        private bool enviarMailReenvio(int iFila_P, long iIdFactura_P, string sNumeroDocumento_P)
        {
            try
            {
                filenameRide = sDirAutorizados + @"\" + sNumeroDocumento_P + ".pdf";

                crearRide(iIdFactura_P, sNumeroDocumento);

                sCorreoCliente = dgvDatos.Rows[iFila_P].Cells["colMail"].Value.ToString();
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

                sMensajeRetorno = crearMensajeEnvio(iFila_P, sNumeroDocumento_P);
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CREAR EL CUERPO DEL MENSAJE
        private string crearMensajeEnvio(int iFila_P, string sNumeroDocumento_P)
        {
            try
            {
                sMensajeEnviar = "";
                //sCodigoDocumento = dgvDatos.Rows[iFila_P].Cells["colTipo"].Value.ToString();

                if (sCodigoDocumento == "FAC")
                {
                    sTipoComprobante = "FACTURA";
                }

                sMensajeEnviar = sMensajeEnviar + "Estimado Cliente " + dgvDatos.Rows[iFila_P].Cells["colCliente"].Value.ToString() + ":" + Environment.NewLine;
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
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }

        #endregion

        private void frmGenerarEnviarFactura_Load(object sender, EventArgs e)
        {
            buscarDirectorio();

            //btnReenviar.Visible = false;
            //int iVer = datosCertificado();

            //if ((iVer == -1) && (iBanderPermitirFirmaElectronica == 0))
            //{
            //    ok = new VentanasMensajes.frmMensajeOK();
            //    ok.LblMensaje.Text = "Error al consultar los parámetros de la firma electrónica.";
            //    ok.ShowDialog();
            //}

            //else if (iVer == 1)
            //    btnReenviar.Visible = true;

            consultarTipoAmbiente();
            consultarTipoEmision();
            traerparametrosMail();
            parametrosFacturacion();
            limpiar();
        }

        private void frmGenerarEnviarFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void btnInicial_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFechaInicial.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtFechaInicial.Text = calendario.txtFecha.Text;

                if (Convert.ToDateTime(txtFechaInicial.Text) > Convert.ToDateTime(txtFechaFinal.Text))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "La fecha inicial no puede ser superior a la ficha final del rango.";
                    ok.ShowDialog();
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

            }
        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFechaFinal.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtFechaFinal.Text = calendario.txtFecha.Text;

                if (Convert.ToDateTime(txtFechaInicial.Text) > Convert.ToDateTime(txtFechaFinal.Text))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "La fecha inicial no puede ser superior a la ficha final del rango.";
                    ok.ShowDialog();
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
            limpiar();
        }

        private async void btnReenviar_Click(object sender, EventArgs e)
        {
            iNumeroRegistros = 0;
            iErrorGenerados = 0;
            iErrorFirmados = 0;
            iErrorEnviarSRI = 0;
            iErrorConsultarXMLSRI = 0;
            iErrorEnviarMail = 0;
            iEnviadosExitosamente = 0;

            Task<bool> task;
            bool bConsulta;

            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["colMarca"].Value) == true)
                {
                    iNumeroRegistros++;
                }
            }

            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No existen comprobantes electrónicos para procesar.";
                ok.ShowDialog();
                return;
            }

            else if (iNumeroRegistros == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay registros seleccionados para procesar la información.";
                ok.ShowDialog();
                return;
            }

            else
            {
                if (conexionInternet() == false)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay una conexión a internet. Favor verifique la conectividad.";
                    ok.ShowDialog();
                    return;
                }

                else
                {
                    SiNo = new VentanasMensajes.frmMensajeSiNo();
                    SiNo.LblMensaje.Text = "¿Desea procesar los comprobantes electrónicos emitidos?";
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
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = logo.sRespuesta;
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

                        //AQUI SE INICIA EL PROCESO DE SINCRONIZACION CON EL SRI
                        for (int i = 0; i < dgvDatos.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["colMarca"].Value) == true)
                            {
                                sNumeroDocumento = "F" + dgvDatos.Rows[i].Cells["colEstablecimiento"].Value.ToString() +
                                               dgvDatos.Rows[i].Cells["colPtoEmision"].Value.ToString() +
                                               dgvDatos.Rows[i].Cells["colNumeroComprobante"].Value.ToString().PadLeft(9, '0');



                                //USANDO TASK ASYNC AWAIT

                                // PASO 1.- INVOCAR A FUNCION PARA PRIMERO GENERAR EL XML
                                task = new Task<bool>(() => generarXML(Convert.ToInt64(dgvDatos.Rows[i].Cells["colIdFactura"].Value)));
                                task.Start();
                                dgvDatos.Rows[i].Cells["colEstado"].Value = "Generando XML...";
                                bConsulta = await task;

                                if (bConsulta == true)
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "XML Generado.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Lime;
                                }

                                else
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "Error generar XML.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Red;
                                    goto retorna;
                                }

                                // PASO 2.- INVOCAR A FUNCION PARA FIRMAR EL XML
                                task = new Task<bool>(() => firmarArchivoXML(sNumeroDocumento));
                                task.Start();
                                dgvDatos.Rows[i].Cells["colEstado"].Value = "Firmando XML...";
                                bConsulta = await task;

                                if (bConsulta == true)
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "XML Firmado.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Lime;

                                }

                                else
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "Error firmar XML.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Red;
                                    goto retorna;
                                }

                                Thread.Sleep(3000);

                                //PASO 5.- ENVIAR AL CORREO ELECTRONICO DEL CLIENTE
                                task = new Task<bool>(() => enviarMailReenvio(i, Convert.ToInt64(dgvDatos.Rows[i].Cells["colIdFactura"].Value), sNumeroDocumento));
                                task.Start();
                                dgvDatos.Rows[i].Cells["colEstado"].Value = "Enviando al correo...";
                                bConsulta = await task;

                                if (bConsulta == true)
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "Correo enviado.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Yellow;
                                }

                                else
                                {
                                    dgvDatos.Rows[i].Cells["colEstado"].Value = "Error al enviar al correo.";
                                    dgvDatos.Rows[i].Cells["colEstado"].Style.BackColor = Color.Red;
                                    goto retorna;
                                }

                                dgvDatos.Rows[i].Cells["colMarca"].Value = false;
                            }

                        retorna: { }
                        }

                        this.Cursor = Cursors.Default;
                        chkSeleccionar.Checked = false;
                        chkSeleccionar.Text = "Seleccionar todos los registros";
                    }
                }
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked == true)
            {
                chkSeleccionar.Text = "Quitar selección de todos los registros";
                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    dgvDatos.Rows[i].Cells["colMarca"].Value = true;
                }

            }

            else
            {
                chkSeleccionar.Text = "Seleccionar todos los registros";
                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    dgvDatos.Rows[i].Cells["colMarca"].Value = false;
                }
            }
        }
    }
}
