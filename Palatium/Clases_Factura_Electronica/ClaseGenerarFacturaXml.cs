﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Palatium.Clases_Factura_Electronica
{
    class ClaseGenerarFacturaXml
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        #region DECLARACION DE VARIABLES PARA CREAR EL XML
        
        XDocument xml;
        XElement factura;
        XElement infoTributaria;
        XElement infoFactura;
        XElement totalConImpuestos;
        XElement totalImpuesto;
        XElement detalles;
        XElement detalle;
        XElement pagos;
        XElement pago;
        XElement detallesAdicionales;
        XElement detAdicional;
        XElement impuestos;
        XElement impuesto;
        XElement infoAdicional;
        XElement campoAdicional;

        #endregion

        #region DECLARACION DE VARIABLES PARA EL ENCABEZADO DEL XML

        string sVersion = "1.0";
        string sUTF = "utf-8";
        string sStandAlone = "yes";
        string sDocumentoVersion;
        string sComentario;
        string sXsi;
        string noNamespaceSchemaLocation;
        string sComprobante;

        #endregion

        #region DECLARACION DE VARIABLES NECESARIAS PARA LA FACTURACION ELECTRONICA
        string sSql;
        string sDirMatriz;
        string sDirEstablecimiento;
        string sRuc;
        string sNombreComercial;
        string sCodigoNumerico;
        string sClaveAcceso;
        string sClaveContingencia;
        long lIdContingencia;
        string sDigitoVerificador;
        string sDireccionCliente;
        string sTelefonoCliente;
        string sSecuencial;
        string sCorreoElectronicoCliente;
        string sAyuda;
        string sTipoComprobanteVenta;

        string sClaveToken;

        string sNumeroAutorizacion;
        string sEstablecimiento;
        string sPuntoEmision;
        string sFechaAutorizacion;
        string sCaducidad;
        string sFechaEmision;
        string sFechaddmmaaaa;
        string sRazonSocial;
        string sCodigoDocumento;
        string sTipoIdentificacionComprador;
        string sRazonSocialComprador;
        string sIdentifacionComprador;
        string sDireccionComprador;
        string sMoneda;
        double dBaseIVA;

        double dImporteTotal;
        double dTotalSinImpuestos;

        string sBaseIVA;

        double dValorIVA;
        string sValorIVA;

        string sImporteTotal;
        string sTotalSinImpuestos;

        double dTotalDescuento;
        string sTotalDescuento;

        string sCodigo;
        string sCodigoPorcentaje;
        string sCodigoPorcentajeCero;

        double dValor;
        string sValor;

        double dPropina;
        string sPropina;

        string sContribuyenteEspecial;
        string sObligadoContabilidad;

        double dBaseNoObjetoIVA;
        double dBaseIVA0;

        string sBaseNoObjetoIVA;
        string sBaseIVA0;

        string sMensajeError;

        string sReferencia;
        string sFabricante;
        string sComentarios;

        string sFormaPago;
        long lPlazo;
        string sPlazo;
        string sUnidadTiempo;

        DataTable dtConsulta;
        DataTable dtDetalle;
        bool bRespuesta;
        long lIdFactura;
        long lNumeroDecimales;

        #endregion


        public string GSub_ActualizaPantalla(string P_St_CodDoc, int iIdLocalidad_P)
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

                ////  Generadas
                //if (P_Ln_Orden == 1)
                //{
                //    sAyuda = sAyuda + "and F.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Firmadas
                //else if (P_Ln_Orden == 2)
                //{
                //    sAyuda = sAyuda + "and F.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Autorizadas
                //else if (P_Ln_Orden == 3)
                //{
                //    sAyuda = sAyuda + "and F.autorizacion is not null" + Environment.NewLine;
                //}

                sAyuda = sAyuda + "and L.cg_localidad = LOCALIDAD.correlativo" + Environment.NewLine;
                sAyuda = sAyuda + "and TC.idtipocomprobante=F.idtipocomprobante" + Environment.NewLine;
                sAyuda = sAyuda + "and TC.codigo='" + sTipoComprobanteVenta + "'" + Environment.NewLine;
                sAyuda = sAyuda + "and F.id_localidad = " + iIdLocalidad_P + "" + Environment.NewLine;
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

                ////  Generadas
                //if (P_Ln_Orden == 1)
                //{
                //    sAyuda = sAyuda + "and CABCR.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Firmadas
                //else if (P_Ln_Orden == 2)
                //{
                //    sAyuda = sAyuda + "and CABCR.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Autorizadas
                //else if (P_Ln_Orden == 3)
                //{
                //    sAyuda = sAyuda + "and CABCR.autorizacion is not null" + Environment.NewLine;
                //}

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

                ////  Generadas
                //if (P_Ln_Orden == 1)
                //{
                //    sAyuda = sAyuda + "and N.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Firmadas
                //else if (P_Ln_Orden == 2)
                //{
                //    sAyuda = sAyuda + "and N.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Autorizadas
                //else if (P_Ln_Orden == 3)
                //{
                //    sAyuda = sAyuda + "and N.autorizacion is not null" + Environment.NewLine;
                //}

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

                ////  Generadas
                //if (P_Ln_Orden == 1)
                //{
                //    sAyuda = sAyuda + "and G.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Firmadas
                //else if (P_Ln_Orden == 2)
                //{
                //    sAyuda = sAyuda + "and G.clave_acceso is not null" + Environment.NewLine;
                //}

                ////  Autorizadas
                //else if (P_Ln_Orden == 3)
                //{
                //    sAyuda = sAyuda + "and G.autorizacion is not null" + Environment.NewLine;
                //}

                sAyuda = sAyuda + "and NGR.estado = 'A'" + Environment.NewLine;
                sAyuda = sAyuda + "Order by  G.Id_Guia_Remision desc";
            }

            return sAyuda;
        }


        public void GSub_GenerarFacturaXML(long P_Ln_Id_Factura, long P_Ln_Silencio, 
                                           string P_St_Tipo_Emision, string P_St_Tipo_Ambiente, 
                                           string P_St_Ruta_Archivo, string P_St_Nombre_Archivo, 
                                           long P_Ln_Numero_de_Decimales, string P_St_correo_consumidor_final, 
                                           string P_St_correo_ambiente_prueba)
        {
            try
            {
                //sCodigpDocumento = "01";
                //sMoneda = "DOLAR";
                lIdFactura = P_Ln_Id_Factura;
                lNumeroDecimales = P_Ln_Numero_de_Decimales;

                dBaseNoObjetoIVA = 0;
                sBaseNoObjetoIVA = dBaseNoObjetoIVA.ToString("N2");

                //RECUPERAMOS EL NUMERO DE RUC Y LA DIRECCION DE LA MATRIZ
                sSql = "";
                sSql = sSql + "select" + Environment.NewLine;
                sSql = sSql + "F.razonSocial, F.nombrecomercial, F.ruc, F.codDoc, F.estab,F.ptoEmi," + Environment.NewLine;
                sSql = sSql + "F.secuencial, F.dirMatriz, F.fechaEmision, F.dirEstablecimiento," + Environment.NewLine;
                sSql = sSql + "F.contribuyenteEspecial, F.obligadoContabilidad, F.tipoIdentificacionComprador," + Environment.NewLine;
                sSql = sSql + "F.razonSocialComprador, F.identificacionComprador, F.moneda, F.clavetoken," + Environment.NewLine;
                sSql = sSql + "F.Direccion, F.telefono, F.email, F.referencia, F.fabricante, F.comentarios" + Environment.NewLine;
                sSql = sSql + "from cel_vw_infofactura F" + Environment.NewLine;
                sSql = sSql + "where F.idEmpresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql = sSql + "and F.id_factura = " + P_Ln_Id_Factura + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sRazonSocial = dtConsulta.Rows[0][0].ToString();

                        sNombreComercial = dtConsulta.Rows[0][1].ToString();

                        sRuc = dtConsulta.Rows[0][2].ToString();

                        sCodigoDocumento = dtConsulta.Rows[0][3].ToString();
                        
                        sEstablecimiento = dtConsulta.Rows[0][4].ToString();
                        sEstablecimiento = sEstablecimiento.PadLeft(3, '0');

                        sPuntoEmision = dtConsulta.Rows[0][5].ToString();
                        sPuntoEmision = sPuntoEmision.PadLeft(3, '0');

                        sSecuencial = dtConsulta.Rows[0][6].ToString();
                        sSecuencial = sSecuencial.PadLeft(9, '0');                        

                        sDirMatriz = dtConsulta.Rows[0][7].ToString();

                        sFechaEmision = Convert.ToDateTime(dtConsulta.Rows[0][8].ToString()).ToString("dd/MM/yyyy");

                        sDirEstablecimiento = dtConsulta.Rows[0][9].ToString();
                        sContribuyenteEspecial = dtConsulta.Rows[0][10].ToString();
                        sContribuyenteEspecial = sContribuyenteEspecial.PadLeft(5, '0');

                        if (sContribuyenteEspecial == "44444")
                        {
                            sContribuyenteEspecial = "00000";
                        }

                        sObligadoContabilidad = dtConsulta.Rows[0][11].ToString();

                        if (P_St_Tipo_Ambiente == "2")
                        {
                            sCorreoElectronicoCliente = dtConsulta.Rows[0][19].ToString();
                        }
                        else
                        {
                            if (sRuc == "9999999999999")
                            {
                                sCorreoElectronicoCliente = P_St_correo_consumidor_final;
                            }

                            else
                            {
                                sCorreoElectronicoCliente = P_St_correo_ambiente_prueba;
                            }
                        }

                        sTipoIdentificacionComprador = dtConsulta.Rows[0][12].ToString();
                        sRazonSocialComprador = dtConsulta.Rows[0][13].ToString();
                        sIdentifacionComprador = dtConsulta.Rows[0][14].ToString();
                        sMoneda = dtConsulta.Rows[0][15].ToString();
                        sClaveToken = dtConsulta.Rows[0][16].ToString();
                        
                        sDireccionCliente = dtConsulta.Rows[0][17].ToString();
                        sDireccionCliente = "";
                        sDireccionComprador = sDireccionCliente;

                        sTelefonoCliente = dtConsulta.Rows[0][18].ToString();

                        sReferencia = dtConsulta.Rows[0][20].ToString();
                        sFabricante = dtConsulta.Rows[0][21].ToString();
                        sComentarios = dtConsulta.Rows[0][22].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Esta factura no es electrónica.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    ok.LblMensaje.Text = sSql;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                //SEGUNDO CURSOR
                sSql = "";
                sSql = sSql + "select sum(V.valor_Neto) Valor_Neto," + Environment.NewLine;
                sSql = sSql + "sum(V.valor_Neto - V.base_cero) Base_doce," + Environment.NewLine;
                sSql = sSql + "sum(V.Base_Cero) Base_cero," + Environment.NewLine;
                sSql = sSql + "sum(V.valor_descuento) valor_descuento," + Environment.NewLine;
                sSql = sSql + "sum(V.valor_iva) Valor_Iva," + Environment.NewLine;
                sSql = sSql + "max(V.servicio) propina," + Environment.NewLine;
                sSql = sSql + "'2' codigo," + Environment.NewLine;
                sSql = sSql + "case max(V.porcentaje_iva) when 12 then '2'" + Environment.NewLine;
                sSql = sSql + "when 14 then '3' else '2' end codigoPorcentaje," + Environment.NewLine;
                sSql = sSql + "sum(V.valor_total) + max(V.servicio) Valor_Total" + Environment.NewLine;
                sSql = sSql + "from cv403_vw_facturas_det_pedidos V" + Environment.NewLine;
                sSql = sSql + "where V.id_factura = " + P_Ln_Id_Factura + Environment.NewLine;
                sSql = sSql + "and V.estado_det_pedido = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dBaseIVA0 = Convert.ToDouble(dtConsulta.Rows[0][2].ToString());
                        sBaseIVA0 = dBaseIVA0.ToString("N2");

                        sCodigo = dtConsulta.Rows[0][6].ToString();
                        sCodigoPorcentaje = dtConsulta.Rows[0][7].ToString();
                        
                        dBaseIVA = Convert.ToDouble(dtConsulta.Rows[0][1].ToString());
                        sBaseIVA = dBaseIVA.ToString("N2");

                        dValorIVA = Convert.ToDouble(dtConsulta.Rows[0][4].ToString());
                        sValorIVA = dValorIVA.ToString("N2");

                        dTotalDescuento = Convert.ToDouble(dtConsulta.Rows[0][3].ToString());
                        sTotalDescuento = dTotalDescuento.ToString("N2");

                        dImporteTotal = Convert.ToDouble(dtConsulta.Rows[0][8].ToString());
                        sImporteTotal = dImporteTotal.ToString("N2");

                        dTotalSinImpuestos = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                        sTotalSinImpuestos = dTotalSinImpuestos.ToString("N2");

                        //dBaseIVA0 = Convert.ToDouble(dtConsulta.Rows[0][2].ToString());

                        dPropina = Convert.ToDouble(dtConsulta.Rows[0][5].ToString());
                        sPropina = dPropina.ToString("N2");
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Error: Recuperando los datos de la tabla det_pedidos.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    ok.LblMensaje.Text = sSql;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                //TERCER CURSOR
                sSql = "";
                sSql = sSql + "select F.formapago, F.plazo, F.unidadtiempo" + Environment.NewLine;
                sSql = sSql + "from cel_vw_infofacturaformapago F" + Environment.NewLine;
                sSql = sSql + "where F.id_factura = " + P_Ln_Id_Factura + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sFormaPago = dtConsulta.Rows[0][0].ToString();
                        lPlazo = Convert.ToInt64(dtConsulta.Rows[0][1].ToString());
                        sPlazo = lPlazo.ToString("N2");
                        sUnidadTiempo = dtConsulta.Rows[0][2].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Error: Esta factura no tiene una forma de pago.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    ok.LblMensaje.Text = sSql;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                //LLAMAR A FUNCION SUB_GSUB_INICIAR PARA EL ENCABEZADO DEL XML Y NOMBRE DE NODO 
                GSub_Iniciar(sCodigoDocumento);

                sFechaddmmaaaa = Convert.ToDateTime(sFechaEmision).ToString("ddMMyyyy");
                
                sCodigoNumerico = "12345678";  //OJO: ESTO HAY QUE PARAMETRIZAR

                //GENERAR LA CLAVE DE ACCESO
                sClaveAcceso = "";

                //EMISION NORMAL- SISTEMA DEL SRI DISPONIBLE
                if (P_St_Tipo_Emision == "1")
                {
                    sClaveAcceso = sClaveAcceso + sFechaddmmaaaa + sCodigoDocumento + sRuc + P_St_Tipo_Ambiente;
                    sClaveAcceso = sClaveAcceso + sEstablecimiento + sPuntoEmision + sSecuencial + sCodigoNumerico + P_St_Tipo_Emision;
                }

                //EMISION POR INDISPONIBILIDAD DEL SISTEMA - SISTEMA DEL SRI DISPONIBLE
                else
                {
                    //sClaveAcceso = sClaveAcceso + sFechaddmmaaaa + sCodigoDocumento;
                    //FALTA FUNCION PARA CONSULTAR CLAVES DE CONTINGENCIA
                }

                sDigitoVerificador = sDigitoVerificarModulo11(sClaveAcceso);
                sClaveAcceso = sClaveAcceso + sDigitoVerificador;

                //LLAMAR A FUNCION GSUB_AGREGARINFORTRIBUTARIA PARA AGREGAR EL NODO INFOTRIBUTARIA
                GSub_Agregar_Info_Tributaria(P_St_Tipo_Ambiente, P_St_Tipo_Emision);

                //LLAMAR A FUNCION GSUB_AGREGAR_INFO_FACTURA PARA AGREGAR NODOS DE INFORMACION
                GSub_Agregar_Info_Factura();

                //LLAMAR A FUNCION GSUB_AGREGAR_DETALLES_FACTURA
                GSub_Agregar_Detalles_Factura();

                //LLAMAR A FUNCION GSUB_AGREGARINFORADICIONAL
                GSub_AgregarInfoAdicional(sCodigoDocumento, sDirMatriz, sNombreComercial, sRuc, sDireccionCliente, sTelefonoCliente, sCorreoElectronicoCliente, sReferencia, sFabricante, sComentario);

                P_St_Nombre_Archivo = "F" + sEstablecimiento + sPuntoEmision + sSecuencial + ".xml";

                //xml.Save(@"D:\\" + P_St_Nombre_Archivo);

                if (P_St_Ruta_Archivo.Length == 0)
                {
                    P_St_Nombre_Archivo = @"C:\" + P_St_Nombre_Archivo;
                }

                else
                {
                    P_St_Nombre_Archivo = P_St_Ruta_Archivo + @"\" + P_St_Nombre_Archivo;
                }

                //GUARDAR EL ARCHIVO XML
                xml.Save(P_St_Nombre_Archivo);

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "update cv403_facturas set" + Environment.NewLine;
                sSql = sSql + "clave_acceso = '" + sClaveAcceso + "'," + Environment.NewLine;
                sSql = sSql + "id_tipo_ambiente = " + Convert.ToInt32(P_St_Tipo_Ambiente) + "," + Environment.NewLine;
                sSql = sSql + "id_tipo_emision = " + Convert.ToInt32(P_St_Tipo_Emision) + Environment.NewLine;
                sSql = sSql + "where id_factura = " + P_Ln_Id_Factura;

                //EJECUTA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //if (P_St_Tipo_Ambiente == "2")
                //{
                //    sSql = "";
                //    sSql = sSql + "update cel_contingencia set" + Environment.NewLine;
                //    sSql = sSql + "usado = 1" + Environment.NewLine;
                //    sSql = sSql + "where id_contingencia = " + lIdContingencia;

                //    //EJECUTA INSTRUCCIÓN SQL
                //    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //    {
                //        ok.LblMensaje.Text = "No se pudo grabar el registro de contingencia como ocupado.";
                //        ok.ShowInTaskbar = false;
                //        ok.ShowDialog();
                //        goto reversa;
                //    }
                //}

                //EJECUTA EL COMMIT SI NO HUBO INCONVENIENTES AL INSERTAR EN LA BASE DE DATOS
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                if (P_Ln_Silencio == 0)
                {
                    ok.LblMensaje.Text = "Archivo: " + P_St_Nombre_Archivo +  " generado éxitosamente.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

                goto fin;

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        //FUNCION GSUB_INICIAR
        private void GSub_Iniciar(string P_St_Tipo_Comprobante)
        {
            try
            {
                //string sVersion = "version=\"1.0\" encoding= \"UTF-8\" standalone=\"yes\"";
                

                if (P_St_Tipo_Comprobante == "01")
                {
                    sComprobante = "factura";
                    sDocumentoVersion = "1.1.0";
                }

                else if (P_St_Tipo_Comprobante == "07")
                {
                    sComprobante = "comprobanteRetencion";
                    sDocumentoVersion = "1.0.0";
                }

                else if (P_St_Tipo_Comprobante == "04")
                {
                    sComprobante = "notaCredito";
                    sDocumentoVersion = "1.1.0";
                }

                else if (P_St_Tipo_Comprobante == "06")
                {
                    sComprobante = "guiaRemision";
                    sDocumentoVersion = "1.1.0";
                }

                else if (P_St_Tipo_Comprobante == "05")
                {
                    sComprobante = "notaDebito";
                    sDocumentoVersion = "1.1.0";
                }

                //Declaramos el documento y su definición
                xml = new XDocument(
                    new XDeclaration(sVersion, sUTF, sStandAlone));

                ////Creamos el nodo factura y lo añadimos al documento
                factura = new XElement(sComprobante, new XAttribute("version", sDocumentoVersion),
                    new XAttribute("id", "comprobante"));
                xml.Add(factura);   
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }

        reversa:
            {

            }

        fin: { }
        }
    
        //FUNCION PARA EL DIGITO VERIFICADOR MODULO 11
        private string sDigitoVerificarModulo11(string sClaveAcceso)
        {
            Int32 suma = 0;
            int inicio = 7;

            for (int i = 0; i < sClaveAcceso.Length; i++)
            {
                suma = suma + Convert.ToInt32(sClaveAcceso.Substring(i, 1)) * inicio;
                inicio--;
                if (inicio == 1)
                    inicio = 7;
            }

            Decimal modulo = suma % 11;
            suma = 11 - Convert.ToInt32(modulo);

            if (suma == 11)
            {
                suma = 0;
            }
            else if (suma == 10)
            {
                suma = 1;
            }
            //sClaveAcceso = sClaveAcceso + Convert.ToString(suma);

            return suma.ToString();            
        }
    
        //FUNCION GSUB_VA_VALOR_DEFECTO
        private string GFun_Va_Valor_Defecto(string P_Va_Dato, string P_Va_Defecto)
        {
            string sRetorno;

            if (P_Va_Dato == null)
            {
                sRetorno = P_Va_Defecto;
            }

            else if (P_Va_Dato == "")
            {
                sRetorno = P_Va_Defecto;
            }

            else
            {
                sRetorno = P_Va_Dato;
            }

            return sRetorno;
        }

        //FUNCION GSUB_AGREGARINFORTRIBUTARIA
        private void GSub_Agregar_Info_Tributaria(string P_St_Ambiente, string P_St_tipoEmision)
        {
            //creamos el nodo infoTributaria y agregamos items, posteriormente al documento
            infoTributaria = new XElement("infoTributaria");
            infoTributaria.Add(new XElement("ambiente", P_St_Ambiente));
            infoTributaria.Add(new XElement("tipoEmision", P_St_tipoEmision));
            infoTributaria.Add(new XElement("razonSocial", sRazonSocial));

            if (sNombreComercial.Length > 0)
            {
                infoTributaria.Add(new XElement("nombreComercial", sNombreComercial));
            }

            infoTributaria.Add(new XElement("ruc", sRuc));
            infoTributaria.Add(new XElement("claveAcceso", sClaveAcceso));
            infoTributaria.Add(new XElement("codDoc", sCodigoDocumento));
            infoTributaria.Add(new XElement("estab", sEstablecimiento));
            infoTributaria.Add(new XElement("ptoEmi", sPuntoEmision));
            infoTributaria.Add(new XElement("secuencial", sSecuencial));
            infoTributaria.Add(new XElement("dirMatriz", sDirMatriz));
            factura.Add(infoTributaria);
        }
    
        //FUNCION GSUB_AGREGAR_INFOFACTURA
        private void GSub_Agregar_Info_Factura()
        {
            string sCodigoPorcentajeCero;

            //creamos el nodo infoFactura y agregamos items, posteriormente al documento
            infoFactura = new XElement("infoFactura");
            infoFactura.Add(new XElement("fechaEmision", sFechaEmision));

            if (sDirEstablecimiento != "")
            {
                infoFactura.Add(new XElement("dirEstablecimiento", sDirEstablecimiento));
            }

            else
            {
                infoFactura.Add(new XElement("dirEstablecimiento", sDirMatriz));
            }

            if ((sContribuyenteEspecial != "") && (sContribuyenteEspecial != "00000"))
            {
                infoFactura.Add(new XElement("contribuyenteEspecial", sContribuyenteEspecial));
            }
                        
            infoFactura.Add(new XElement("obligadoContabilidad", sObligadoContabilidad));
            infoFactura.Add(new XElement("tipoIdentificacionComprador", sTipoIdentificacionComprador));
            infoFactura.Add(new XElement("razonSocialComprador", sRazonSocialComprador));
            infoFactura.Add(new XElement("identificacionComprador", sIdentifacionComprador));
            infoFactura.Add(new XElement("totalSinImpuestos", sTotalSinImpuestos));
            infoFactura.Add(new XElement("totalDescuento", sTotalDescuento));
            factura.Add(infoFactura);

            //creamos el nodo totalConImpuestos y agregamos items, posteriormente al nodo infoFactura
            totalConImpuestos = new XElement("totalConImpuestos");
            totalImpuesto = new XElement("totalImpuesto");

            //creamos el nodo totalImpuesto y agregamos items, posteriormente al nodo totalConImpuestos
            //PRIMERO SE INGRESA EL IVA 0
            if (Convert.ToDouble(sBaseIVA0) > 0)
            {
                totalImpuesto.Add(new XElement("codigo", sCodigo));
                sCodigoPorcentajeCero = "0";
                totalImpuesto.Add(new XElement("codigoPorcentaje", sCodigoPorcentajeCero));
                totalImpuesto.Add(new XElement("baseImponible", sBaseIVA0));
                string sValorCero = "0";
                totalImpuesto.Add(new XElement("valor", sValorCero));

                totalConImpuestos.Add(totalImpuesto);
            }

            //Luego ingresamos el iva diferente de cero (base diferente de cero)
            if (Convert.ToDouble(sBaseIVA) > 0)
            {
                //if (Convert.ToDouble(sBaseIVA0) > 0)
                //{
                    totalImpuesto = new XElement("totalImpuesto");

                    totalImpuesto.Add(new XElement("codigo", sCodigo));
                    sCodigoPorcentaje = "2";
                    totalImpuesto.Add(new XElement("codigoPorcentaje", sCodigoPorcentaje));
                    totalImpuesto.Add(new XElement("baseImponible", sBaseIVA));
                    totalImpuesto.Add(new XElement("valor", sValorIVA));
                    totalConImpuestos.Add(totalImpuesto);
                //}
            }            
           
            infoFactura.Add(totalConImpuestos);

            infoFactura.Add(new XElement("propina", sPropina));
            infoFactura.Add(new XElement("importeTotal", sImporteTotal));
            infoFactura.Add(new XElement("moneda", sMoneda));

            //Aqui comienza el detalle de pagos
            pagos = new XElement("pagos");
            pago = new XElement("pago");
            pago.Add(new XElement("formaPago", sFormaPago));
            pago.Add(new XElement("total", sImporteTotal));

            //verificamos si el plazo es mayor que cero
            if (Convert.ToDouble(sPlazo) == 0)
            {
                sPlazo = "1";
            }

            pago.Add(new XElement("plazo", sPlazo));
            pago.Add(new XElement("unidadTiempo", sUnidadTiempo));
            pagos.Add(pago);
            infoFactura.Add(pagos);
        }
    
        //FUNCION GSUB_REGARDETALLES_FACTURA
        private void GSub_Agregar_Detalles_Factura()
        {
            try
            {
                //DECLARACION DE VARIABLES PARA EL CICLO DE LA FUNCION
                long lIdDetPedido;
                long lI;
                string T_sIvaCompras;
                string T_sFacturaDetallesDetalle;
                double T_dValorNeto;
                double T_dValorUnidad;
                double T_dDescuento;
                double T_dCantidad;

                string T_sPrecioUnitario;
                string T_sDescuento;
                string T_sPrecioTotalSinImpuesto;
                string T_sNumeroDocumento;
                string T_sFacturaDetallesDetalle_i_detallesAdicionales;
                string T_sCodigoPrincipal;
                string T_sCantidad;
                string T_sDescripcion;
                string T_sCodigo;
                string T_sCodigoPorcentaje;

                double T_dTarifa;
                string T_sTarifa;

                double T_dBaseImponible;
                string T_sBaseImponible;
                double T_dValor;
                string T_sValor;
                string T_sFacturaDetallesDetalle_i_impuesto;
                string T_sFacturaDetallesDetalle_i_impuestos_impuesto;
                long lLinea;
                string T_sEspecificacion;
                long lJ;
                string T_sNombreDetalle;

                sSql = "";
                sSql = sSql + "Select  PR.codigo codigoPrincipal, NP.nombre descripcion," + Environment.NewLine;
                sSql = sSql + "DP.Cantidad, DP.precio_unitario precioUnitario, isnull(numero_documento,'') numero_documento," + Environment.NewLine;
                //sSql = sSql + "DP.Cantidad * DP.valor_Dscto descuento," + Environment.NewLine;
                sSql = sSql + "DP.valor_Dscto descuento," + Environment.NewLine;
                sSql = sSql + "DP.Cantidad * (DP.precio_unitario-DP.valor_Dscto) precioTotalSinmpuesto," + Environment.NewLine;
                sSql = sSql + "'2' codigo, case DP.valor_IVA when  0 then '0' else  " + Environment.NewLine;
                sSql = sSql + "case CP.porcentaje_IVA when 12 then '2'  else '3' end end as codigoPorcentaje," + Environment.NewLine;
                sSql = sSql + "case DP.valor_IVA when  0 then 0 else CP.Porcentaje_IVA  end as tarifa, " + Environment.NewLine;
                sSql = sSql + "DP.Cantidad * (DP.precio_unitario-DP.valor_Dscto) baseImponible,DP.Cantidad * DP.valor_IVA valor,DP.id_det_pedido," + Environment.NewLine;
                sSql = sSql + "CP.consumo_alimentos" + Environment.NewLine;
                sSql = sSql + "From cv403_facturas F, cv403_facturas_pedidos FP, cv403_cab_pedidos CP," + Environment.NewLine;
                sSql = sSql + "cv403_det_pedidos DP, cv401_productos PR," + Environment.NewLine;
                sSql = sSql + "cv401_nombre_productos NP, tp_codigos UNIDAD" + Environment.NewLine;
                sSql = sSql + "Where F.id_factura = " + lIdFactura + Environment.NewLine;
                sSql = sSql + "and F.id_factura = FP.id_factura And FP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FP.Id_Pedido = CP.Id_Pedido" + Environment.NewLine;
                sSql = sSql + "and CP.Id_Pedido = DP.Id_Pedido And DP.estado = 'A' " + Environment.NewLine;
                sSql = sSql + "and DP.Cg_Unidad_Medida = UNIDAD.correlativo " + Environment.NewLine;
                sSql = sSql + "and DP.id_producto = PR.id_producto And PR.id_producto = NP.id_producto " + Environment.NewLine;
                sSql = sSql + "and NP.nombre_Interno = 1 " + Environment.NewLine;
                sSql = sSql + "and NP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "order by DP.Id_Det_Pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {         
                        //PARA CREAR NODO DE CONSUMO DE ALIMENTOS
                        if (dtConsulta.Rows[0][13].ToString() == "1")
                        {
                            T_sCodigo = GFun_Va_Valor_Defecto(dtConsulta.Rows[0][7].ToString(), "2");
                            T_sCodigoPorcentaje = GFun_Va_Valor_Defecto(dtConsulta.Rows[0][8].ToString(), "2");

                            T_sTarifa = GFun_Va_Valor_Defecto(dtConsulta.Rows[0][9].ToString(), "");

                            GSub_ConsumoAlimentos(dtConsulta, T_sCodigo, T_sCodigoPorcentaje, T_sTarifa);
                        }

                        else
                        {
                            XElement detalles = new XElement("detalles");

                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {
                                //Creamos el nodo detalle y el contenido con sus nodos hijos
                                detalle = new XElement("detalle");

                                T_sCodigoPrincipal = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][0].ToString(), "");
                                detalle.Add(new XElement("codigoPrincipal", T_sCodigoPrincipal));

                                T_sDescripcion = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][1].ToString(), "");
                                detalle.Add(new XElement("descripcion", T_sDescripcion));

                                T_sCantidad = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][2].ToString(), "");
                                T_dCantidad = Convert.ToDouble(T_sCantidad);
                                detalle.Add(new XElement("cantidad", T_dCantidad.ToString("N2")));

                                T_sPrecioUnitario = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][3].ToString(), "");
                                T_dValorUnidad = Convert.ToDouble(T_sPrecioUnitario);

                                if (lNumeroDecimales == 2)
                                {
                                    T_sPrecioUnitario = T_dValorUnidad.ToString("N2");
                                }

                                else
                                {
                                    T_sPrecioUnitario = T_dValorUnidad.ToString("N4");
                                }
                                detalle.Add(new XElement("precioUnitario", T_sPrecioUnitario));

                                //T_dDescuento = T_dCantidad * Convert.ToDouble(GFun_Va_Valor_Defecto(dtConsulta.Rows[i][5].ToString(), ""));
                                T_dDescuento = Convert.ToDouble(GFun_Va_Valor_Defecto(dtConsulta.Rows[i][5].ToString(), ""));

                                //Codigos y Valores para impuesto IVA
                                T_sCodigo = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][7].ToString(), "2");
                                T_sCodigoPorcentaje = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][8].ToString(), "2");

                                T_sTarifa = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][9].ToString(), "");
                                T_dTarifa = Convert.ToDouble(T_sTarifa);

                                T_sBaseImponible = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][10].ToString(), "");
                                T_dBaseImponible = Convert.ToDouble(T_sBaseImponible);

                                T_sValor = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][11].ToString(), "");
                                T_dValor = Convert.ToDouble(T_sValor);

                                detalle.Add(new XElement("descuento", T_dDescuento.ToString("N2")));

                                //T_dValorNeto = (T_dValorUnidad - T_dDescuento) * T_dCantidad;
                                T_dValorNeto = (T_dValorUnidad - T_dDescuento);
                                //T_sPrecioTotalSinImpuesto = T_dValorNeto.ToString("N2");
                                T_sPrecioTotalSinImpuesto = (T_dCantidad * T_dValorNeto).ToString("N2");

                                detalle.Add(new XElement("precioTotalSinImpuesto", T_sPrecioTotalSinImpuesto));

                                T_sNumeroDocumento = GFun_Va_Valor_Defecto(dtConsulta.Rows[i][4].ToString(), "");


                                //ESTA PARTE CONSULTAR CON FAUSTO
                                //===================================================================================================================================================
                                ////Aumentamos detalles adicionales si es que existen en cada linea de la factura

                                //if (T_sNumeroDocumento.Length > 0)
                                //{
                                //    lJ = 1;

                                //    //El numero de documento es una guia aera y procedemos a darle el formato respectivo                                
                                //    T_sNumeroDocumento = T_sNumeroDocumento.Substring(0, 3) + "-" + T_sNumeroDocumento.Substring(3, 4) + "-" + T_sNumeroDocumento.Substring(7, 4);

                                //    //Aqui comienzan los detallesAdicionales si es que existen, para cada linea del detalle de la factura
                                //    detallesAdicionales = new XElement("detallesAdicionales");
                                //}

                                lIdDetPedido = Convert.ToInt64(dtConsulta.Rows[i][12].ToString());

                                //===================================================================================================================================================


                                //Aqui comienza el detalle de impuestos
                                impuestos = new XElement("impuestos");

                                impuesto = new XElement("impuesto");
                                impuesto.Add(new XElement("codigo", T_sCodigo));
                                impuesto.Add(new XElement("codigoPorcentaje", T_sCodigoPorcentaje));
                                impuesto.Add(new XElement("tarifa", T_sTarifa));
                                impuesto.Add(new XElement("baseImponible", T_dBaseImponible.ToString("N2")));
                                impuesto.Add(new XElement("valor", T_dValor.ToString("N2")));

                                impuestos.Add(impuesto);
                                detalle.Add(impuestos);
                                detalles.Add(detalle);
                            }
                            //FIN FOR
                            factura.Add(detalles);
                        }                      
                    }
                    //FIN IF
                }

                else
                {
                    ok.LblMensaje.Text = sSql;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
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

        //FUNCION PARA CREAR EL NODO DE CONSUMO ALIMENTOS
        private void GSub_ConsumoAlimentos(DataTable dtConsulta, string T_sCodigo, string T_sCodigoPorcentaje, string T_sTarifa)
        {
            try
            {
                XElement detalles = new XElement("detalles");

                detalle = new XElement("detalle");

                detalle.Add(new XElement("codigoPrincipal", "CONSALI"));

                detalle.Add(new XElement("descripcion", "CONSUMO ALIMENTOS"));

                detalle.Add(new XElement("cantidad", "1"));
                                
                detalle.Add(new XElement("precioUnitario", sBaseIVA));

                detalle.Add(new XElement("descuento", dTotalDescuento.ToString("N2")));

                detalle.Add(new XElement("precioTotalSinImpuesto", sBaseIVA));

                //Aqui comienza el detalle de impuestos
                impuestos = new XElement("impuestos");

                impuesto = new XElement("impuesto");
                impuesto.Add(new XElement("codigo", T_sCodigo));
                impuesto.Add(new XElement("codigoPorcentaje", T_sCodigoPorcentaje));
                impuesto.Add(new XElement("tarifa", T_sTarifa));
                impuesto.Add(new XElement("baseImponible", sBaseIVA));
                impuesto.Add(new XElement("valor", sValorIVA));

                impuestos.Add(impuesto);
                detalle.Add(impuestos);
                detalles.Add(detalle);
                factura.Add(detalles);
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    
        //FUNCION GSUB_AGREGARINFOADICIONAL
        private void GSub_AgregarInfoAdicional(string P_St_Tipo_Comprobante, string P_St_DirMatriz, 
                                               string P_St_NombreComercial, string P_St_Ruc, string P_St_DireccionCliente,
                                               string P_St_TelefonoCliente, string P_St_CorreoElectronicoCliente,
                                               string P_St_Referencia, string P_St_Fabricante, string P_St_Comentarios)
        {
            try
            {
                long lI;
                string T_sFacturaInfoAdicional_campoAdicional_i;
                string T_sComprobante;

                infoAdicional = new XElement("infoAdicional");

                if (P_St_Tipo_Comprobante == "01")
                {
                    T_sComprobante = "factura";
                }

                else if (P_St_Tipo_Comprobante == "07")
                {
                    T_sComprobante = "comprobanteRetencion";
                }

                else if (P_St_Tipo_Comprobante == "04")
                {
                    T_sComprobante = "notaCredito";
                }

                else if (P_St_Tipo_Comprobante == "06")
                {
                    T_sComprobante = "guiaRemision";
                }

                else if (P_St_Tipo_Comprobante == "05")
                {
                    T_sComprobante = "notaDebito";
                }

                if (P_St_DireccionCliente == "")
                {
                    P_St_DireccionCliente = "ND";
                }


                ////Creamos el nodo factura y lo añadimos al documento
                //XElement factura = new XElement("factura", new XAttribute("version", "1.0.0"),
                //    new XAttribute("id", "comprobante"));
                //xml.Add(factura);

                campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "direccion"), P_St_DireccionCliente);
                infoAdicional.Add(campoAdicional);

                if ((P_St_TelefonoCliente != "") && (P_St_TelefonoCliente != null))
                {
                    campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "telefono"), P_St_TelefonoCliente);
                    infoAdicional.Add(campoAdicional);
                }

                campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "email"), P_St_CorreoElectronicoCliente);
                infoAdicional.Add(campoAdicional);

                if ((P_St_Referencia != "") && (P_St_Referencia != null))
                {
                    campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "referencia"), P_St_Referencia);
                    infoAdicional.Add(campoAdicional);
                }

                if ((P_St_Fabricante != "") && (P_St_Fabricante != null))
                {
                    campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "fabricante"), P_St_Fabricante);
                    infoAdicional.Add(campoAdicional);
                }

                if ((P_St_Comentarios != "") && (P_St_Comentarios != null))
                {
                    campoAdicional = new XElement("campoAdicional", new XAttribute("nombre", "comentarios"), P_St_Comentarios);
                    infoAdicional.Add(campoAdicional);
                }

                factura.Add(infoAdicional);
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }
    }
}
