using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Net;

namespace Palatium.Clases_Factura_Electronica
{
    class ClaseConsultarXML
    {
        public static string URL_Envio;
        public static string URL_Autorizacion;
        public static string RutaXML;
        public static string ClaveAcceso;


        private static string xmlEnvioRequestTemplate =
          String.Concat(
          "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
          " <SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"",
          " xmlns:ns1=\"http://ec.gob.sri.ws.recepcion\">",
          "  <SOAP-ENV:Body>",
          "    <ns1:validarComprobante>",
          "      <xml>{0}</xml>",
          "    </ns1:validarComprobante>",
          "  </SOAP-ENV:Body>",
          "</SOAP-ENV:Envelope>");

        private static string xmlAutorizacionRequestTemplate =
      String.Concat(
      "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
      " <SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"",
      " xmlns:ns1=\"http://ec.gob.sri.ws.autorizacion\">",
      " <SOAP-ENV:Header/>",
      "  <SOAP-ENV:Body>",
      "    <ns1:autorizacionComprobante>",
      "      <claveAccesoComprobante>{0}</claveAccesoComprobante>",
      "    </ns1:autorizacionComprobante>",
      "  </SOAP-ENV:Body>",
      "</SOAP-ENV:Envelope>");


        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sAyuda;
        string sSql;
        string sTipoComprobanteVenta;

        DataTable dtConsulta;
        bool bRespuesta;

        public string GSub_ActualizaPantalla(string P_St_CodDoc, long P_Ln_Orden)
        {
            //      P_Ln_Orden
            //      1 Comprobantes generados
            //      2 Firmados
            //      3 Autorizados
            //      4 No autorizados


            //FACTURA
            if (P_St_CodDoc == "01")
            {
                sTipoComprobanteVenta = "Fac";

                sAyuda = "";
                sAyuda = sAyuda + "select" + Environment.NewLine;
                sAyuda = sAyuda + "NF.Numero_Factura," + Environment.NewLine;

                if (conexion.GFun_St_Conexion() == "MYSQL")
                {
                    sAyuda = sAyuda + "ltrim(concat(P.apellidos,' '," + conexion.GFun_St_esnulo() + "(P.nombres,''))) Cliente," + Environment.NewLine;
                }
                else
                {
                    sAyuda = sAyuda + "ltrim(P.apellidos + ' ' + " + conexion.GFun_St_esnulo() + "(P.nombres,'')) Cliente," + Environment.NewLine;
                }

                sAyuda = sAyuda + "SubString(LOCALIDAD.valor_texto, 1, 25) Localidad,F.fecha_factura," + Environment.NewLine;
                sAyuda = sAyuda + "F.id_factura," + Environment.NewLine;
                sAyuda = sAyuda + "F.clave_acceso," + Environment.NewLine;
                sAyuda = sAyuda + "L.establecimiento estab,isnull(L.punto_emision,'009') ptoEmi," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(autorizacion,'') autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(CONVERT (nvarchar(19), fecha_autorizacion, 120),'') fecha_autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + "id_tipo_emision,id_tipo_ambiente" + Environment.NewLine;
                sAyuda = sAyuda + "from" + Environment.NewLine;
                sAyuda = sAyuda + "cv403_facturas F," + Environment.NewLine;
                sAyuda = sAyuda + "cv403_numeros_facturas NF," + Environment.NewLine;
                sAyuda = sAyuda + "tp_personas P," + Environment.NewLine;
                sAyuda = sAyuda + "tp_localidades L," + Environment.NewLine;
                sAyuda = sAyuda + "tp_codigos LOCALIDAD," + Environment.NewLine;
                sAyuda = sAyuda + "vta_tipocomprobante TC" + Environment.NewLine;
                sAyuda = sAyuda + "where" + Environment.NewLine;
                sAyuda = sAyuda + "F.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sAyuda = sAyuda + "and F.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "and F.estado in ('A','E')" + Environment.NewLine;
                sAyuda = sAyuda + "and NF.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "and NF.id_factura = F.id_factura" + Environment.NewLine;
                sAyuda = sAyuda + "and F.id_persona = P.id_persona" + Environment.NewLine;
                sAyuda = sAyuda + "and F.id_localidad = L.id_localidad" + Environment.NewLine;

                //  Generadas
                if (P_Ln_Orden == 1)
                {
                    sAyuda = sAyuda + "and F.clave_acceso is not null" + Environment.NewLine;
                }

                //  Firmadas
                else if (P_Ln_Orden == 2)
                {
                    sAyuda = sAyuda + "and F.clave_acceso is not null" + Environment.NewLine;
                }

                //  Autorizadas
                else if (P_Ln_Orden == 3)
                {
                    sAyuda = sAyuda + "and F.autorizacion is not null" + Environment.NewLine;
                }

                sAyuda = sAyuda + "and L.cg_localidad = LOCALIDAD.correlativo" + Environment.NewLine;
                sAyuda = sAyuda + "and TC.idtipocomprobante=F.idtipocomprobante" + Environment.NewLine;
                sAyuda = sAyuda + "and TC.codigo='" + sTipoComprobanteVenta + "'" + Environment.NewLine;
                sAyuda = sAyuda + "order by F.id_factura desc";
            }


            //RETENCION
            if (P_St_CodDoc == "07")
            {
                sAyuda = "";
                sAyuda = sAyuda + "SELECT DISTINCT" + Environment.NewLine;

                if (conexion.GFun_St_Conexion() == "MYSQL")
                {
                    sAyuda = sAyuda + "convert(CABCR.NUMERO_PREIMPRESO,decimal) numero_secuencial," + Environment.NewLine;
                    sAyuda = sAyuda + "concat(PER.apellidos , ' ' , " + conexion.GFun_St_esnulo() + "(PER.nombres,'')) Razon_Social," + Environment.NewLine;
                }

                else
                {
                    sAyuda = sAyuda + "convert(numeric,CABCR.NUMERO_PREIMPRESO) numero_secuencial," + Environment.NewLine;
                    sAyuda = sAyuda + "PER.apellidos + ' ' + " + conexion.GFun_St_esnulo() + "(PER.nombres,'') Razon_Social," + Environment.NewLine;
                }

                sAyuda = sAyuda + "CABM.numero_movimiento,CABM.FECHA_MOVIMIENTO," + Environment.NewLine;
                sAyuda = sAyuda + "CABCR.ID_CAB_COMPROBANTE_RETENCION,CABCR.clave_acceso," + Environment.NewLine;
                sAyuda = sAyuda + "EstabRetencion1, ptoEmiRetencion1," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(autorizacion,'') autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(CONVERT (nvarchar(19), fecha_autorizacion, 120),'') fecha_autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + "id_tipo_emision,id_tipo_ambiente" + Environment.NewLine;
                sAyuda = sAyuda + "from" + Environment.NewLine;
                sAyuda = sAyuda + "cv405_comprobantes_retencion CR," + Environment.NewLine;
                sAyuda = sAyuda + "cv405_c_movimientos CABM," + Environment.NewLine;
                sAyuda = sAyuda + "cv404_auxiliares_contables AUX," + Environment.NewLine;
                sAyuda = sAyuda + "tp_personas PER," + Environment.NewLine;
                sAyuda = sAyuda + "cv405_cab_comprobantes_retencion CABCR" + Environment.NewLine;
                sAyuda = sAyuda + "where" + Environment.NewLine;
                sAyuda = sAyuda + "CABM.id_c_movimiento = CR.id_c_movimiento" + Environment.NewLine;
                sAyuda = sAyuda + "and AUX.id_auxiliar = CABM.id_beneficiario" + Environment.NewLine;
                sAyuda = sAyuda + "and PER.id_persona = CABM.id_persona" + Environment.NewLine;
                sAyuda = sAyuda + "and CR.ID_CAB_COMPROBANTE_RETENCION = CABCR.ID_CAB_COMPROBANTE_RETENCION" + Environment.NewLine;

                //  Generadas
                if (P_Ln_Orden == 1)
                {
                    sAyuda = sAyuda + "and CABCR.clave_acceso is not null" + Environment.NewLine;
                }

                //  Firmadas
                else if (P_Ln_Orden == 2)
                {
                    sAyuda = sAyuda + "and CABCR.clave_acceso is not null" + Environment.NewLine;
                }

                //  Autorizadas
                else if (P_Ln_Orden == 3)
                {
                    sAyuda = sAyuda + "and CABCR.autorizacion is not null" + Environment.NewLine;
                }

                sAyuda = sAyuda + "AND CABM.ESTADO = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "AND CR.ESTADO = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "AND CABCR.ESTADO = 'A'" + Environment.NewLine;

                if (conexion.GFun_St_Conexion() == "MYSQL")
                {
                    sAyuda = sAyuda + "Order by convert(CABCR.NUMERO_PREIMPRESO, decimal) desc, CABM.FECHA_MOVIMIENTO desc";
                }
                else
                {
                    sAyuda = sAyuda + "Order by convert(numeric,CABCR.NUMERO_PREIMPRESO) desc, CABM.FECHA_MOVIMIENTO desc";
                }
            }

            //NOTA DE CREDITO
            if (P_St_CodDoc == "04")
            {
                sAyuda = "";
                sAyuda = sAyuda + "select" + Environment.NewLine;
                sAyuda = sAyuda + "NNC.Numero_Nota," + Environment.NewLine;

                if (conexion.GFun_St_Conexion() == "MYSQL")
                {
                    sAyuda = sAyuda + "concat(P.apellidos,' '," + conexion.GFun_St_esnulo() + "(P.nombres,'')) Cliente," + Environment.NewLine;
                }

                else
                {
                    sAyuda = sAyuda + "P.apellidos + ' ' + " + conexion.GFun_St_esnulo() + "(P.nombres,'') Cliente," + Environment.NewLine;
                }

                sAyuda = sAyuda + "SubString(LOCALIDAD.valor_texto, 1, 25) Localidad," + Environment.NewLine;
                sAyuda = sAyuda + "N.fecha_vcto," + Environment.NewLine;
                sAyuda = sAyuda + "N.Id_Nota_Credito,N.clave_acceso," + Environment.NewLine;
                sAyuda = sAyuda + "L.establecimiento estab,isnull(L.punto_emision,'009') ptoEmi," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(autorizacion,'') autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(CONVERT (nvarchar(19), fecha_autorizacion, 120),'') fecha_autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + "id_tipo_emision,id_tipo_ambiente" + Environment.NewLine;
                sAyuda = sAyuda + "from" + Environment.NewLine;
                sAyuda = sAyuda + "cv403_notas_credito N, tp_localidades L," + Environment.NewLine;
                sAyuda = sAyuda + "tp_codigos LOCALIDAD," + Environment.NewLine;
                sAyuda = sAyuda + "tp_personas P," + Environment.NewLine;
                sAyuda = sAyuda + "cv403_numeros_notas_creditos NNC" + Environment.NewLine;
                sAyuda = sAyuda + "where" + Environment.NewLine;
                sAyuda = sAyuda + "N.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "and N.id_persona = P.id_persona" + Environment.NewLine;
                sAyuda = sAyuda + "and NNC.Id_Nota_Credito = N.Id_Nota_Credito" + Environment.NewLine;

                //If G_Ln_Id_Servidor > 1 Then
                //   T_St_Sql = T_St_Sql & "and l.id_servidor = " & G_Ln_Id_Servidor & " "
                //End If

                sAyuda = sAyuda + "and N.id_localidad = L.id_localidad" + Environment.NewLine;
                sAyuda = sAyuda + "and L.cg_localidad = LOCALIDAD.correlativo" + Environment.NewLine;

                //  Generadas
                if (P_Ln_Orden == 1)
                {
                    sAyuda = sAyuda + "and N.clave_acceso is not null" + Environment.NewLine;
                }

                //  Firmadas
                else if (P_Ln_Orden == 2)
                {
                    sAyuda = sAyuda + "and N.clave_acceso is not null" + Environment.NewLine;
                }

                //  Autorizadas
                else if (P_Ln_Orden == 3)
                {
                    sAyuda = sAyuda + "and N.autorizacion is not null" + Environment.NewLine;
                }

                sAyuda = sAyuda + "and NNC.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "Order by  N.Id_nota_credito desc";
            }

            //GUIA DE REMISION
            if (P_St_CodDoc == "06")
            {
                sAyuda = "";
                sAyuda = sAyuda + "select" + Environment.NewLine;
                sAyuda = sAyuda + "NGR.Numero_Guia_Remision," + Environment.NewLine;

                if (conexion.GFun_St_Conexion() == "MYSQL")
                {
                    sAyuda = sAyuda + "concat(P.apellidos,' '," + conexion.GFun_St_esnulo() + "(P.nombres,'')) Cliente," + Environment.NewLine;
                }

                else
                {
                    sAyuda = sAyuda + "P.apellidos + ' ' + " + conexion.GFun_St_esnulo() + "(P.nombres,'') Cliente," + Environment.NewLine;
                }

                sAyuda = sAyuda + "SubString(LOCALIDAD.valor_texto, 1, 25) Localidad," + Environment.NewLine;
                sAyuda = sAyuda + "G.fecha_emision," + Environment.NewLine;
                sAyuda = sAyuda + "G.Id_Guia_Remision,G.clave_acceso," + Environment.NewLine;
                sAyuda = sAyuda + "L.establecimiento estab,";
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(L.punto_emision,'009') ptoEmi," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(G.autorizacion,'') autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + conexion.GFun_St_esnulo() + "(CONVERT (nvarchar(19), G.fecha_autorizacion, 120),'') fecha_autorizacion," + Environment.NewLine;
                sAyuda = sAyuda + "G.id_tipo_emision,G.id_tipo_ambiente" + Environment.NewLine;
                sAyuda = sAyuda + "from" + Environment.NewLine;
                sAyuda = sAyuda + "cv403_guias_remision G, tp_localidades L," + Environment.NewLine;
                sAyuda = sAyuda + "tp_codigos LOCALIDAD," + Environment.NewLine;
                sAyuda = sAyuda + "tp_personas P," + Environment.NewLine;
                sAyuda = sAyuda + "cv403_numeros_guias_remision NGR" + Environment.NewLine;
                sAyuda = sAyuda + "where" + Environment.NewLine;
                sAyuda = sAyuda + "G.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "and G.id_destinatario = P.id_persona" + Environment.NewLine;
                sAyuda = sAyuda + "and NGR.Id_Guia_Remision = G.Id_Guia_Remision" + Environment.NewLine;

                //If G_Ln_Id_Servidor > 1 Then
                //    T_St_Sql = T_St_Sql & "and L.id_servidor = " & G_Ln_Id_Servidor & " "
                //End If

                sAyuda = sAyuda + "and G.id_localidad = L.id_localidad" + Environment.NewLine;
                sAyuda = sAyuda + "and L.cg_localidad = LOCALIDAD.correlativo" + Environment.NewLine;

                //  Generadas
                if (P_Ln_Orden == 1)
                {
                    sAyuda = sAyuda + "and G.clave_acceso is not null" + Environment.NewLine;
                }

                //  Firmadas
                else if (P_Ln_Orden == 2)
                {
                    sAyuda = sAyuda + "and G.clave_acceso is not null" + Environment.NewLine;
                }

                //  Autorizadas
                else if (P_Ln_Orden == 3)
                {
                    sAyuda = sAyuda + "and G.autorizacion is not null" + Environment.NewLine;
                }

                sAyuda = sAyuda + "and NGR.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "Order by  G.Id_Guia_Remision desc";
            }

            return sAyuda;
        }

        //GFun_St_Ruta_Archivo
        public string GFun_St_Ruta_Archivo(string P_St_Codigo_Documento, long P_Ln_Orden)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select D.nombres" + Environment.NewLine;
                sSql = sSql + "from  cel_directorio D, cel_tipo_comprobante C" + Environment.NewLine;
                sSql = sSql + "where D.id_tipo_comprobante = C.id_tipo_comprobante and" + Environment.NewLine;
                sSql = sSql + "C.codigo = '" + P_St_Codigo_Documento + "' and" + Environment.NewLine;
                sSql = sSql + "D.orden = " + P_Ln_Orden + Environment.NewLine;
                sSql = sSql + "and D.estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sSql = conexion.GFun_Va_Valor_Defecto(dtConsulta.Rows[0].ItemArray[0].ToString(), "");
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                }

                return sSql;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.Show();
                return "";
            }
        }

        public static RespuestaSRI GetRespuestaRecepcion(XmlDocument xml_doc)
        {
            RespuestaSRI result = new RespuestaSRI();
            result.Estado = GetNodeValue("RespuestaRecepcionComprobante", "estado", xml_doc);

            if (result.Estado != "RECIBIDA")
            {
                result.ClaveAcceso = GetNodeValue("comprobante", "claveAcceso", xml_doc);
                result.ErrorIdentificador = GetNodeValue("mensaje", "identificador", xml_doc);
                result.ErrorMensaje = GetNodeValue("mensaje", "mensaje", xml_doc);
                result.ErrorInfoAdicional = GetNodeValue("mensaje", "informacionAdicional", xml_doc);
                result.ErrorTipo = GetNodeValue("mensaje", "tipo", xml_doc);
            }

            return result;
        }

        /// <summary>
        /// Devuelve la respuesta de la solicitud de recepción del comprobante en una estructura detallada.
        /// </summary>
        /// <param name="xml_doc">Documento XML de respuesta</param>
        public static RespuestaSRI GetRespuestaAutorizacion(XmlDocument xml_doc)
        {
            RespuestaSRI result = new RespuestaSRI();
            string pathLevelAutorizacion = "RespuestaAutorizacionComprobante/autorizaciones/autorizacion[last()]";
            string pathLevelMensajes = "RespuestaAutorizacionComprobante/autorizaciones/autorizacion/mensajes[last()]/mensaje";

            if (result == null)
            {
                return result;
            }

            result.Estado = GetNodeValue(pathLevelAutorizacion, "estado", xml_doc);

            if (result.Estado == "AUTORIZADO")
            {
                result.Estado = GetNodeValue(pathLevelAutorizacion, "estado", xml_doc);
                result.NumeroAutorizacion = GetNodeValue(pathLevelAutorizacion, "numeroAutorizacion", xml_doc);
                result.FechaAutorizacion = GetNodeValue(pathLevelAutorizacion, "fechaAutorizacion", xml_doc);
                result.Ambiente = GetNodeValue(pathLevelAutorizacion, "ambiente", xml_doc);
                result.Comprobante = GetNodeValue(pathLevelAutorizacion, "comprobante", xml_doc);
            }
            else if (result.Estado == "NO AUTORIZADO")
            {
                result.Estado = GetNodeValue(pathLevelAutorizacion, "estado", xml_doc);
                result.FechaAutorizacion = GetNodeValue(pathLevelAutorizacion, "fechaAutorizacion", xml_doc);
                result.Ambiente = GetNodeValue(pathLevelAutorizacion, "ambiente", xml_doc);
                result.Comprobante = GetNodeValue(pathLevelAutorizacion, "comprobante", xml_doc);
                result.ErrorIdentificador = GetNodeValue(pathLevelMensajes, "identificador", xml_doc);
                result.ErrorMensaje = GetNodeValue(pathLevelMensajes, "mensaje", xml_doc);
                result.ErrorTipo = GetNodeValue(pathLevelMensajes, "tipo", xml_doc);
            }

            return result;
        }

        private static string GetNodeValue(string rootNodo, string infoNodo, XmlDocument doc)
        {
            string result = null;
            string node_path = "//" + rootNodo + "//" + infoNodo;

            XmlNode node = doc.SelectSingleNode(node_path);

            if (node != null)
            {
                result = node.InnerText;
            }

            return result;
        }

        public static XmlDocument ConvertStringToDocument(string xml_string)
        {
            XmlDocument result = new XmlDocument();
            result.LoadXml(xml_string);

            return result;
        }

        public static XmlDocument ConvertFileToDocument(string file_path)
        {
            XmlDocument result = new XmlDocument();
            result.Load(file_path);

            return result;
        }


        /// <summary>
        /// Convierte el documento en string Base64
        /// </summary>
        /// <param name="file_path">Ruta del archivo a aconvertir</param>
        public static string ConvertToBase64String(string file_path)
        {
            byte[] binarydata = File.ReadAllBytes(file_path);
            return Convert.ToBase64String(binarydata, 0, binarydata.Length);
        }

        /// <summary>
        /// Envía la clave de acceso a los webs services del SRI para consultar ele estado de autorización.
        /// </summary>
        public RespuestaSRI AutorizacionComprobante(out XmlDocument xml_doc, string sClaveAcceso, string sWebService_P)
        {
            RespuestaSRI result = null;
            //string ws_url = "https://celcer.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantes?wsdl";
            string ws_url = sWebService_P;

            //Crea el request del web service
            HttpWebRequest request = CreateWebRequest(ws_url, "POST");

            //Arma la cadena xml para el envío al web service
            string stringRequest = string.Format(xmlAutorizacionRequestTemplate, sClaveAcceso);

            //Convierte la cadena en un documeto xml
            XmlDocument xmlRequest = ConvertStringToDocument(stringRequest);
            xml_doc = xmlRequest;

            //Crea un flujo de datos (stream) y escribe el xml en la solicitud de respuesta del web service
            using (Stream stream = request.GetRequestStream())
            {
                xmlRequest.Save(stream);
            }

            //Obtiene la respuesta del web service
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    //Lee el flujo de datos (stream) de respuesta del web service
                    string soapResultStr = rd.ReadToEnd();

                    //Convierte la respuesta de string a xml para extraer el detalle de la respuesta del web service
                    XmlDocument soapResultXML = ConvertStringToDocument(soapResultStr);


                    ////PROBAR COMO GUARDA
                    //soapResultXML.Save(@"D:\\FACT.XML");

                    //Obtiene la respuesta detallada
                    result = GetRespuestaAutorizacion(soapResultXML);
                }
            }

            return result;
        }

        /// <summary>
        /// Crea y devuelve una instancia de objeto para la solicitud de respuesta desde una URI.
        /// </summary>
        /// <param name="uri">URI del recurso de internet</param>
        /// <param name="method">Método de solicitud</param>
        private static HttpWebRequest CreateWebRequest(string uri, string method)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Headers.Add("SOAP:Action");
            webRequest.ContentType = "application/soap+xml;charset=utf-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = method;

            return webRequest;
        }

        public string GetInfoTributaria(string info, XmlDocument xml_doc)
        {
            return GetNodeValue("infoTributaria", info, xml_doc);
        }

        public string GetInfoFactura(string info, XmlDocument xml_doc)
        {
            return GetNodeValue("infoFactura", info, xml_doc);
        }
    }
}
