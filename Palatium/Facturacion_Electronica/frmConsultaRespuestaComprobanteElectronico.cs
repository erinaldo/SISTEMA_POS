using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmConsultaRespuestaComprobanteElectronico : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases_Factura_Electronica.ClaseConsultarXML consultarXML = new Clases_Factura_Electronica.ClaseConsultarXML();
        Clases_Factura_Electronica.ClaseXMLAyuda xmlH = new Clases_Factura_Electronica.ClaseXMLAyuda();


        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;

        string sAyuda;
        string sCodigoDocumento;
        string sNumeroDocumento;
        string sRutaArchivo;
        string sRutaXmlFirmado;
        string miXMl;
        string sFecha;
        string sWSEnvioPruebas;
        string sWSConsultaPruebas;
        string sWSEnvioProduccion;
        string sWSConsultaProduccion;
        string sWebService;

        string sVersion = "1.0";
        string sUTF = "utf-8";
        string sStandAlone = "yes";

        XDocument xml;
        XElement autorizacion;
        XElement estado;
        XElement numeroAutorizacion;
        XElement fechaAutorizacion;
        XElement ambiente;
        XElement comprobante;

        int iCol_Correlativo;
        int iCol_Codigo;
        int iCol_Descripcion;
        int iIdFactura;
        int iIdTipoAmbiente;

        private XmlDocument xmlDoc;

        public frmConsultaRespuestaComprobanteElectronico()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        //FUNCION PARA RELLENAR LAS INSTRUCCIONES SQL
        private void llenarInstruccionesSQL()
        {
            sAyuda = consultarXML.GSub_ActualizaPantalla(sCodigoDocumento, 2);
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
            txtAutorizacionEstado.Clear();
            txtNumeroAutorizacion.Clear();
            txtFechaAutorizacion.Clear();
            txtDetalles_1.Clear();
            txtDetalles_2.Clear();
            txtRutaAutorizados.Clear();
            txtArchivoAutorizado.Clear();

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
                        sCodigoDocumento = dtConsulta.Rows[0][6].ToString();
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

                        sNumeroDocumento = sNumeroDocumento + dtConsulta.Rows[0][7].ToString() +
                                           dtConsulta.Rows[0][8].ToString() +
                                           dtConsulta.Rows[0][9].ToString().PadLeft(9, '0');

                        txtNumeroDocumento.Text = sNumeroDocumento;

                        //CONSULTAR SI EXISTE EL ARCHIVO EN LA RUTA CONFIGURADA
                        sRutaXmlFirmado = consultarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 2) + @"\" + sNumeroDocumento + ".xml";
                        sRutaArchivo = consultarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 3) + @"\" + sNumeroDocumento + ".xml";

                        txtArchivoAutorizado.Text = sNumeroDocumento + ".xml";
                        txtRutaAutorizados.Text = consultarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 3);

                        //if (File.Exists(sRutaArchivo))
                        //{
                        //    txtArchivoAutorizado.Text = sNumeroDocumento + ".xml";
                        //    txtRutaAutorizados.Text = consultarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 3);
                        //}

                        //else
                        //{
                        //    ok.lblMensaje.Text = "No existe el archivo en la ruta:" + Environment.NewLine + sRutaArchivo;
                        //    ok.ShowDialog();
                        //}
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

        //NUEVO PROCESO PARA CONVERTIR EN BASE 64 EL ARCHIVO
        public string ConvertirString()
        {
            byte[] xmlLeido = File.ReadAllBytes(sRutaXmlFirmado);
            return Convert.ToString(xmlLeido);
        }

        //CONSTRUIR XML AUTORIZADO
        private void xmlAutorizado(RespuestaSRI sri, string filename)
        {
            try
            {
                miXMl = File.ReadAllText(sRutaXmlFirmado);
                //Declaramos el documento y su definición
                xml = new XDocument(
                    new XDeclaration(sVersion, sUTF, sStandAlone));

                autorizacion = new XElement("autorizacion");
                autorizacion.Add(new XElement("estado", sri.Estado));
                autorizacion.Add(new XElement("numeroAutorizacion", sri.NumeroAutorizacion));
                autorizacion.Add(new XElement("fechaAutorizacion", sri.FechaAutorizacion));
                autorizacion.Add(new XElement("ambiente", sri.Ambiente));
                autorizacion.Add(new XElement("comprobante", new XCData(sri.Comprobante)));
                autorizacion.Add(new XElement("mensajes", sri.ErrorMensaje));
                xml.Add(autorizacion);

                //PROBAR COMO GUARDA
                xml.Save(filename);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //ACTUALIZAR EN LA BASE DE DATOS
        private void actualizarDatos()
        {
            try
            {
                sFecha = txtFechaAutorizacion.Text.Substring(0, 10) + " " + txtFechaAutorizacion.Text.Substring(11, 8);

                if (txtNumeroAutorizacion.Text.Trim() != "")
                {
                    //SE INICIA UNA TRANSACCION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.lblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                        limpiar();
                        return;
                    }

                    //UPDATE PARA FACTURAS
                    if (sCodigoDocumento == "01")
                    {
                        sSql = "";
                        sSql += "update cv403_facturas set" + Environment.NewLine;
                        sSql += "autorizacion = '" + txtNumeroAutorizacion.Text.Trim() + "'," + Environment.NewLine;
                        sSql += "fecha_autorizacion = '" + sFecha + "'" + Environment.NewLine;
                        sSql += "where id_factura = " + dB_Ayuda_Facturas.iId;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = "No se pudo grabar la Autorización en la factura " + dB_Ayuda_Facturas.sCodigo;
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
                        sSql += "where id_cab_comprobante_retencion = " + dB_Ayuda_Facturas.iId;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = "No se pudo grabar la Autorización en el comprobante de retención " + dB_Ayuda_Facturas.sCodigo;
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
                        sSql += "where id_nota_credito = " + dB_Ayuda_Facturas.iId;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = "No se pudo grabar la Autorización en la nota de crédito " + dB_Ayuda_Facturas.sCodigo;
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
                        sSql += "where id_guia_remision = " + dB_Ayuda_Facturas.iId;

                        //EJECUTAR LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = "No se pudo grabar la Autorización en la guía de remisión " + dB_Ayuda_Facturas.sCodigo;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    //ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                    //ok.ShowDialog();
                    //limpiar();
                    return;
                }

                else
                {
                    ok.lblMensaje.Text = "No ha ingresado el nombre del archivo autorizado";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        #endregion

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
                            if (dtConsulta.Rows[0][0].ToString() == "NINGUNA")
                            {
                                ok.lblMensaje.Text = "No ha generado el archivo xml. Favor vuelva a generar.";
                                ok.ShowDialog();
                            }

                            else
                            {
                                txtClaveAcceso.Text = dtConsulta.Rows[0]["clave_acceso"].ToString();
                                iIdTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmConsultaRespuestaComprobanteElectronico_Load(object sender, EventArgs e)
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

        private void btnValidar_Click(object sender, EventArgs e)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(txtClaveAcceso.Text))
            {
                //wsH.URL_Autorizacion = LblAutorizacion.Text;
                //wsH.ClaveAcceso = TxtClave.Text;

                //Cursor.Current = Cursors.WaitCursor;

                XmlDocument xmlAut;                

                try
                {
                    if (iIdTipoAmbiente == 1)
                    {
                        sWebService = sWSConsultaPruebas;
                    }

                    else if (iIdTipoAmbiente == 2)
                    {
                        sWebService = sWSConsultaProduccion;
                    }

                    RespuestaSRI respuesta = consultarXML.AutorizacionComprobante(out xmlAut, txtClaveAcceso.Text, sWebService);
                    //string respuestaStr;

                    txtEstadoEnvio.Text = respuesta.Estado;
                    txtNumeroAutorizacion.Text = respuesta.NumeroAutorizacion;
                    txtFechaAutorizacion.Text = respuesta.FechaAutorizacion;
                    txtDetalles_1.Text = respuesta.ErrorIdentificador + Environment.NewLine + respuesta.ErrorMensaje + Environment.NewLine + respuesta.ErrorTipo;;
                    txtDetalles_2.Text = respuesta.ErrorInfoAdicional;

                    if ((txtEstadoEnvio.Text == "AUTORIZADO") || (txtEstadoEnvio.Text == "NO AUTORIZADO"))
                    {
                        //Genera y guarda el XML autorizado
                        string filename = Path.GetFileNameWithoutExtension(txtArchivoAutorizado.Text.Trim()) + ".xml";
                        string path = txtRutaAutorizados.Text;

                        filename = Path.Combine(path, filename);

                        xmlAutorizado(respuesta, filename);

                        actualizarDatos();
                    }
                }
                catch (Exception ex)
                {
                    catchMensaje.lblMensaje.Text = ex.Message;
                    catchMensaje.ShowDialog();
                    //ClientScript.RegisterClientScriptBlock(GetType(), "mensaje", "alert('Error al usar los web services del SRI 2');", true);
                }
                finally
                {
                    //Cursor.Current = Cursors.Default;
                }
            }

            //return result;
        }
    }
}
