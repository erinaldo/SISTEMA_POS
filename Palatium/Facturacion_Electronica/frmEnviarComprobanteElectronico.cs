using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmEnviarComprobanteElectronico : Form
    {       
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases_Factura_Electronica.ClaseEnviarXML enviarXML = new Clases_Factura_Electronica.ClaseEnviarXML();
        Clases_Factura_Electronica.ClaseXMLAyuda xmlH = new Clases_Factura_Electronica.ClaseXMLAyuda();
        

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;
        string sSql;
        string sWSEnvioPruebas;
        string sWSConsultaPruebas;
        string sWSEnvioProduccion;
        string sWSConsultaProduccion;
        string sWebService;

        bool bRespuesta = false;
        int iIdFactura = 0;
        int iIdTipoAmbiente;
        int iCol_Correlativo;
        int iCol_Codigo;
        int iCol_Descripcion;

        string sNumeroDocumento;
        string sCodigoDocumento;
        string sRutaArchivo;

        string sTipoComprobanteVenta;
        string sAyuda;

        string sCertificado_Ruta;
        string sCertificado_Palabra_Clave;

        #region VARIABLES PARA EL CONSUMO DEL WEB SERVICE

        long T_Ln_I;
        string T_St_ArchivoEnviar;

        long T_Ln_id_factura;
        string T_St_secuencial;

        string T_St_codigoError;
        string T_St_DescripcionError;
        string T_Ln_Numero_de_Decimales;

        string T_St_Archivobase64;
        string T_St_TxtEncode64;

        string strSoap;
        string strSOAPAction;
        string strWsdl;
        string strUrl;
        string StrHost;

        string T_st_Estado;
        string T_st_fechaAutorizacion;

        long T_ln_codigoError;
        string T_St_parteInicialSoap;
        string T_St_parteCentralSoap;
        string T_St_parteFinalSoap;

        string T_St_Archivo_In;
        string T_St_Archivo_Out;

        //HttpWebRequest xmlResponse = (HttpWebRequest)WebRequest.Create("");

        #endregion

        public frmEnviarComprobanteElectronico()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA RELLENAR LAS INSTRUCCIONES SQL
        private void llenarInstruccionesSQL()
        {
            sAyuda = enviarXML.GSub_ActualizaPantalla(sCodigoDocumento, 2);
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
            txtNumeroDocumento.Clear();
            txtRutaArchivosFirmados.Clear();

            dB_Ayuda_Facturas.limpiar();

            llenarComboTipoComprobante();
            llenarComboTipoAmbiente();
            llenarComboTipoEmision();
            cargarConfiguracion();
        }

        //FUNCION PARA CARGAR LOS PARAMETROS CONFIGURADOS EN EL SISTEMA
        private void cargarConfiguracion()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_emision, id_tipo_ambiente" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTipoEmision.SelectedValue = dtConsulta.Rows[0][0].ToString();
                        cmbTipoAmbiente.SelectedValue = dtConsulta.Rows[0][1].ToString();
                        cmbTipoAmbiente_1.SelectedValue = dtConsulta.Rows[0][1].ToString();
                    }

                    else
                    {
                        cmbTipoEmision.SelectedIndex = 0;
                        cmbTipoAmbiente.SelectedIndex = 0;

                        ok.lblMensaje.Text = "No se encuentra configurado los parámetros de facturación electrónica.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE TIPO DE COMPROBANTE
        private void llenarComboTipoComprobante()
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
                catchMensaje.lblMensaje.Text = ex.Message;
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
                catchMensaje.lblMensaje.Text = ex.Message;
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA COMPLETAR LA INFORMACION DEL FORMULARIO 
        private void llenarInformacionFactura(int iId)
        {
            try
            {
                sSql = "";
                sSql += "select * from cel_vw_infofactura" + Environment.NewLine;
                sSql += "where id_factura = " + iId;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCodigoDocumento = dtConsulta.Rows[0].ItemArray[6].ToString();
                        sNumeroDocumento = "";

                        if (sCodigoDocumento == "01")
                        {
                            sNumeroDocumento = sNumeroDocumento + "F";
                        }

                        else if (sCodigoDocumento == "04")
                        {
                            sNumeroDocumento = sNumeroDocumento + "NC";
                        }

                        else if (sCodigoDocumento == "07")
                        {
                            sNumeroDocumento = sNumeroDocumento + "R";
                        }

                        else if (sCodigoDocumento == "06")
                        {
                            sNumeroDocumento = sNumeroDocumento + "G";
                        }

                        else if (sCodigoDocumento == "05")
                        {
                            sNumeroDocumento = sNumeroDocumento + "ND";
                        }

                        sNumeroDocumento = sNumeroDocumento + dtConsulta.Rows[0].ItemArray[7].ToString() +
                                           dtConsulta.Rows[0].ItemArray[8].ToString() +
                                           dtConsulta.Rows[0].ItemArray[9].ToString().PadLeft(9, '0');

                        txtNumeroDocumento.Text = sNumeroDocumento;

                        //CONSULTAR SI EXISTE EL ARCHIVO EN LA RUTA CONFIGURADA
                        sRutaArchivo = enviarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 2) + @"\" + sNumeroDocumento + ".xml";

                        if (File.Exists(sRutaArchivo))
                        {
                            txtArchivoEnviar.Text = sNumeroDocumento + ".xml";
                            txtRutaArchivosFirmados.Text = enviarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 2);
                        }

                        else
                        {
                            ok.lblMensaje.Text = "No existe el archivo en la ruta:" + Environment.NewLine + sRutaArchivo;
                            ok.ShowDialog();
                        }
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
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
                        ok.lblMensaje.Text = "No se ha configurado los parámetros de emisión de comprobantes electrónicos";
                        ok.ShowDialog();
                    }
                }
                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmEnviarComprobanteElectronico_Load(object sender, EventArgs e)
        {
            cmbTipoComprobante.SelectedIndexChanged -= new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            limpiar();
            cmbTipoComprobante.SelectedIndexChanged += new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            sCodigoDocumento = "01";
            lblTipoDocumento.Text = cmbTipoComprobante.Text;
            consultarWebService();
            llenarInstruccionesSQL();
        }

        private void cmbTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "No ha seleccionado el tipo de comprobante para recuperar los directorios.";
                    ok.ShowDialog();
                }

                else
                {
                    if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 1)
                    {
                        sCodigoDocumento = "01";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 2)
                    {
                        sCodigoDocumento = "04";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 3)
                    {
                        sCodigoDocumento = "05";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 4)
                    {
                        sCodigoDocumento = "06";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 5)
                    {
                        sCodigoDocumento = "07";
                    }

                    lblTipoDocumento.Text = cmbTipoComprobante.Text;
                    llenarInstruccionesSQL();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                iIdFactura = dB_Ayuda_Facturas.iId;

                if (iIdFactura == 0)
                {
                    ok.lblMensaje.Text = "No ha seleccionado ninguna factura.";
                    ok.ShowDialog();
                }

                else
                {
                    sSql = "";
                    sSql += "select " + conexion.GFun_St_esnulo() + "(clave_acceso, 'NINGUNA') clave_acceso," + Environment.NewLine;
                    sSql += "id_tipo_ambiente" + Environment.NewLine;
                    sSql += "from cv403_facturas " + Environment.NewLine;
                    sSql += "where id_factura = " + iIdFactura;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            if (dtConsulta.Rows[0].ItemArray[0].ToString() == "NINGUNA")
                            {
                                ok.lblMensaje.Text = "No ha generado el archivo xml. Favor vuelva a generar.";
                                ok.ShowDialog();
                            }

                            else
                            {
                                txtClaveAcceso.Text = dtConsulta.Rows[0]["clave_acceso"].ToString();
                                iIdTipoAmbiente = Convert.ToInt32(this.dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
                                llenarInformacionFactura(iIdFactura);
                            }
                        }

                        else
                        {
                            ok.lblMensaje.Text = "No existe un registro de factura. Comuníquese con el administrador.";
                            ok.ShowDialog();
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }

                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                //enviarComprobanteSRI();
                bool result = false;

                T_St_ArchivoEnviar = txtRutaArchivosFirmados.Text + @"\" + txtArchivoEnviar.Text;

                NuevoSiNo.lblMensaje.Text = "¿Está seguro de enviar el archivo " + T_St_ArchivoEnviar + "?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult != DialogResult.OK)
                {
                    goto fin;
                }

                if ((txtRutaArchivosFirmados.Text.Trim() != "") && (txtNumeroDocumento.Text.Trim() != ""))
                {
                    T_St_Archivo_In = txtRutaArchivosFirmados.Text + @"\" + txtNumeroDocumento.Text + ".xml";
                    T_St_Archivo_Out = txtRutaArchivosFirmados.Text + @"\" + txtNumeroDocumento.Text + ".txt";
                }

                T_St_Archivobase64 = txtRutaArchivosFirmados.Text + @"\" + txtNumeroDocumento.Text + ".txt";

                if (iIdTipoAmbiente == 1)
                {
                    sWebService = sWSEnvioPruebas;
                }

                else if (iIdTipoAmbiente == 2)
                {
                    sWebService = sWSEnvioProduccion;
                }

                RespuestaSRI respuesta = enviarXML.EnvioComprobante(T_St_Archivo_In, sWebService);

                if (respuesta.Estado == "RECIBIDA")
                {
                    result = true;
                }

                txtEstadoEnvio.Text = respuesta.Estado;
                txtMensajeErrorEnvio.Text = respuesta.ErrorMensaje;
                txtCodigoEnvio.Text = respuesta.ErrorIdentificador;
                txtInformacionAdicionalEnvio.Text = respuesta.ErrorInfoAdicional;
                txtTipoEnvio.Text = respuesta.ErrorTipo;

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin: { }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
