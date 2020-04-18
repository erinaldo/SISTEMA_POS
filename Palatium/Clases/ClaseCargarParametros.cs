using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseCargarParametros
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        
        string sSql;
        string sRetorno;

        DataTable dtConsulta;
        bool bRespuesta = false;

        public int iRespuesta;

        //CARGAR PARAMETROS EN EL SISTEMA
        public string cargarParametros()
        {
            try
            {
                iRespuesta = 0;

                sSql = "";
                sSql += "select * from pos_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    iRespuesta = -1;
                    return conexion.sMensajeError;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    Program.iIdProductoModificador = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_modificador"].ToString());
                    Program.iIdProductoDomicilio = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_domicilio"].ToString());
                    Program.iIdProductoNuevoItem = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_item"].ToString());
                    Program.iva = Convert.ToDouble(dtConsulta.Rows[0]["iva"].ToString()) / 100;
                    Program.ice = Convert.ToDouble(dtConsulta.Rows[0]["ice"].ToString()) / 100;
                    Program.servicio = Convert.ToDouble(dtConsulta.Rows[0]["servicio"].ToString()) / 100;
                    Program.iManejaServicio = Convert.ToInt32(dtConsulta.Rows[0]["maneja_servicio"].ToString());
                    Program.iBanderaNumeroMesa = Convert.ToInt32(dtConsulta.Rows[0]["etiqueta_mesa"].ToString());
                    Program.iUsuarioLogin = Convert.ToInt32(dtConsulta.Rows[0]["opcion_login"].ToString());
                    Program.sContactoFabricante = dtConsulta.Rows[0]["contacto_fabricante"].ToString();
                    Program.sSitioWebFabricante = dtConsulta.Rows[0]["sitio_web_fabricante"].ToString();
                    Program.sUrlContabilidad = dtConsulta.Rows[0]["url_contabilidad"].ToString();
                    Program.iCobrarConSinProductos = Convert.ToInt32(dtConsulta.Rows[0]["precio_incluye_impuesto"].ToString());
                    Program.iManejaNomina = Convert.ToInt32(dtConsulta.Rows[0]["maneja_nomina"].ToString());
                    Program.iComprobanteNotaEntrega = Convert.ToInt32(dtConsulta.Rows[0]["idtipocomprobante"].ToString());
                    Program.iUsarIconosCategorias = Convert.ToInt32(dtConsulta.Rows[0]["usar_iconos_categorias"].ToString());

                    fechaSistema();
                    return "";
                }

                else
                {
                    iRespuesta = 1;
                    return "No se ha configurado los parámetros para el sistema.";
                }
            }

            catch (Exception ex)
            {
                iRespuesta = -1;
                return ex.Message;
            }
        }

        public string cargarParametrosPredeterminados()
        {
            try
            {
                iRespuesta = 0;

                sSql = "";
                sSql += "select * from pos_vw_cargar_parametros_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.iCgLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["cg_ciudad"].ToString());
                        Program.iIdCajeroDefault = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cajero"].ToString());
                        Program.iIdMesero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_mesero"].ToString());
                        Program.nombreMesero = dtConsulta.Rows[0]["nombre_mesero"].ToString();
                        Program.iMoneda = Convert.ToInt32(dtConsulta.Rows[0]["cg_moneda"].ToString());
                        Program.iFormatoPrecuenta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_formato_precuenta"].ToString());
                        Program.iFormatoFactura = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_formato_factura"].ToString());
                        Program.iHabilitarDestinosImpresion = Convert.ToInt32(dtConsulta.Rows[0]["habilitar_destinos_impresion"].ToString());
                        Program.iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["consumidor_final"].ToString());
                        Program.iIdVendedor = Convert.ToInt32(dtConsulta.Rows[0]["id_vendedor"].ToString());
                        Program.iManejaJornada = Convert.ToInt32(dtConsulta.Rows[0]["maneja_jornada"].ToString());
                        Program.sNombreCajeroDefault = dtConsulta.Rows[0]["nombre_cajero"].ToString();
                        Program.iImprimirDatosFactura = Convert.ToInt32(dtConsulta.Rows[0]["imprimir_datos_factura"].ToString());
                        Program.sCiudadDefault = dtConsulta.Rows[0]["valor_texto"].ToString().ToUpper();
                        Program.iIdProductoAnular = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_anula"].ToString());
                        Program.dValorProductoAnular = Convert.ToDouble(dtConsulta.Rows[0]["valor_precio_anula"].ToString());
                        Program.dValorProductoAnular = Program.dValorProductoAnular + (Program.dValorProductoAnular * Program.iva);
                        Program.sPasswordAdmin = dtConsulta.Rows[0]["clave_acceso_admin"].ToString();
                        Program.iIdImpresoraReportes = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_impresora"].ToString());
                        Program.iEjecutarImpresion = Convert.ToInt32(dtConsulta.Rows[0]["ejecutar_impresion"].ToString());
                        Program.iPermitirAbrirCajon = Convert.ToInt32(dtConsulta.Rows[0]["permitir_abrir_cajon"].ToString());
                        Program.dbValorMaximoRecargoTarjetas = Convert.ToDecimal(dtConsulta.Rows[0]["valor_maximo_recargo"].ToString());
                        Program.iDescargarReceta = Convert.ToInt32(dtConsulta.Rows[0]["descarga_receta"].ToString());
                        Program.iDescargarProductosNoProcesados = Convert.ToInt32(dtConsulta.Rows[0]["descarga_no_procesados"].ToString());
                        Program.iManejaPromotor = Convert.ToInt32(dtConsulta.Rows[0]["maneja_promotor"].ToString());
                        Program.iIdPosPromotor = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_promotor"].ToString());
                        Program.iManejaRepartidor = Convert.ToInt32(dtConsulta.Rows[0]["maneja_repartidor"].ToString());
                        Program.iIdPosRepartidor = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_repartidor"].ToString());
                        Program.sNombreRepartidor = dtConsulta.Rows[0]["nombre_repartidor"].ToString();
                        Program.iCantidadImpresionesEmpresa = Convert.ToInt32(dtConsulta.Rows[0]["cantidad_reporte_empresa"].ToString());
                        Program.iCantidadImpresionesExpress = Convert.ToInt32(dtConsulta.Rows[0]["cantidad_reporte_express"].ToString());
                        Program.iReimprimirCocina = Convert.ToInt32(dtConsulta.Rows[0]["reimprimir_cocina"].ToString());
                        Program.iTipoComprobantePorDefaultComanda = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_comprobante_default"].ToString());
                        Program.iMostrarValoresPropina = Convert.ToInt32(dtConsulta.Rows[0]["mostrar_valores_propina"].ToString());
                        Program.iManejaMitad = Convert.ToInt32(dtConsulta.Rows[0]["maneja_mitad"].ToString());
                        Program.iManejaPropinaSoloTarjetas = Convert.ToInt32(dtConsulta.Rows[0]["propina_para_tarjetas"].ToString());
                        Program.iManejaProductoComisionEmpleados = Convert.ToInt32(dtConsulta.Rows[0]["maneja_producto_comision_empleados"].ToString());
                        Program.dbValorComisionEmpleados = Convert.ToDecimal(dtConsulta.Rows[0]["valor_comision_producto_para_empleados"].ToString());
                        Program.iManejaDeliveryVariable = Convert.ToInt32(dtConsulta.Rows[0]["maneja_delivery_variable"].ToString());
                        Program.iCantidadImpresionesTarjetas = Convert.ToInt32(dtConsulta.Rows[0]["cantidad_reporte_crear_tarjetas"].ToString());

                        Program.descuento_empleados = Convert.ToDouble(dtConsulta.Rows[0]["porcentaje_descuento_empleados"].ToString()) / 100;
                        Program.iLeerMesero = Convert.ToInt32(dtConsulta.Rows[0]["leer_mesero_mesas"].ToString());
                        Program.iImprimeOrden = Convert.ToInt32(dtConsulta.Rows[0]["imprimir_precuenta_guardar_comanda"].ToString());
                        Program.iFacturacionElectronica = Convert.ToInt32(dtConsulta.Rows[0]["maneja_facturacion_electronica"].ToString());
                        Program.iHabilitarDecimal = Convert.ToInt32(dtConsulta.Rows[0]["configuracion_decimales"].ToString());
                        Program.sLogo = dtConsulta.Rows[0]["logo"].ToString();
                        Program.iManejaNotaVenta = Convert.ToInt32(dtConsulta.Rows[0]["maneja_nota_entrega"].ToString());
                        Program.iSeleccionMesero = Convert.ToInt32(dtConsulta.Rows[0]["seleccion_mesero_para_llevar"].ToString());
                        Program.iVistaPreviaImpresiones = Convert.ToInt32(dtConsulta.Rows[0]["vista_previa_impresion"].ToString());
                        Program.iDescuentaIva = Convert.ToInt32(dtConsulta.Rows[0]["descuenta_iva"].ToString());
                        Program.iManejaAlmuerzos = Convert.ToInt32(dtConsulta.Rows[0]["maneja_almuerzos"].ToString());
                        Program.iNumeroPersonasDefault = Convert.ToInt32(dtConsulta.Rows[0]["numero_personas_default"].ToString());
                        Program.sUrlReportes = dtConsulta.Rows[0]["ruta_reportes"].ToString();
                        Program.iAplicaRecargoTarjeta = Convert.ToInt32(dtConsulta.Rows[0]["aplica_recargo_tarjetas"].ToString());
                        Program.dbPorcentajeRecargoTarjeta = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_recargo_tarjetas"].ToString()) / 100;
                        Program.sCorreoElectronicoDefault = dtConsulta.Rows[0]["correo_electronico_default"].ToString();

                        Program.iUsarLectorHuellas = Convert.ToInt32(dtConsulta.Rows[0]["usar_lector_huellas_dactilares"].ToString());
                        Program.iUsarLectorPantallaEspere = Convert.ToInt32(dtConsulta.Rows[0]["usar_pantalla_espera_almuerzos"].ToString());
                        Program.iIdProductoAlmuerzoDefault = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_almuerzo_default"].ToString());

                        Program.iComandaVentaRapida = Convert.ToInt32(dtConsulta.Rows[0]["maneja_venta_rapida"].ToString());
                        Program.iComandaClienteEmpresarial = Convert.ToInt32(dtConsulta.Rows[0]["maneja_cliente_empresarial"].ToString());
                        Program.iComandaTarjetaAlmuerzos = Convert.ToInt32(dtConsulta.Rows[0]["maneja_tarjeta_almuerzo"].ToString());
                        Program.iComandaConsumoInterno = Convert.ToInt32(dtConsulta.Rows[0]["maneja_consumo_interno"].ToString());

                        return "";
                    }

                    else
                    {
                        iRespuesta = 1;
                        return "No se ha configurado los parámetros predeterminados para el sistema.";
                    }
                }

                else
                {
                    iRespuesta = -1;
                    return conexion.sMensajeError;
                }
            }

            catch (Exception ex)
            {
                iRespuesta = -1;
                return ex.Message;
            }
        }

        public string cargarFormatosImpresiones()
        {
            try
            {
                sSql = "select * from pos_formato_precuenta where estado = 'A'";
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.iFormatoPrecuenta =  Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        sSql = "";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                Program.iFormatoPrecuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                                return "";
                            }
                            else
                            {
                                return "Ocurrió un problema al realizar la consulta. Comuníquese con el administrador.";
                            }
                        }

                        else
                        {
                            return "Ocurrió un problema al realizar la consulta. Comuníquese con el administrador.";
                        }

                    }

                    else
                    {
                        return "Ocurrió un problema al realizar la consulta. Comuníquese con el administrador.";
                    }
                }

                else
                {
                    return "Ocurrió un problema al realizar la consulta. Comuníquese con el administrador.";
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //public string cargarDatosTerminal()
        //{
        //    try
        //    {
        //        sSql = "";
        //        sSql += "select id_pos_terminal from pos_terminal" + Environment.NewLine;
        //        sSql += "where nombre_maquina = '" + Environment.MachineName.ToString() + "'" + Environment.NewLine;
        //        sSql += "and estado = 'A'";

        //        dtConsulta = new DataTable();
        //        dtConsulta.Clear();

        //        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //        if (bRespuesta == true)
        //        {
        //            if (dtConsulta.Rows.Count > 0)
        //            {
        //                Program.iIdTerminal = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
        //                return "";
        //            }
        //            else
        //            {
        //                return "Ocurrió un problema al obtener el ID del terminal. Comuníquese con el administrador.";
        //            }
        //        }

        //        else
        //        {
        //            return "Ocurrió un problema al obtener el ID del terminal. Comuníquese con el administrador.";
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public void cargarDatosImpresion()
        {
            try
            {
                sSql = "";
                sSql += "select direccion, telefono1, telefono2, establecimiento," + Environment.NewLine;
                sSql += "punto_emision nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.direccion = dtConsulta.Rows[0][0].ToString();
                        Program.telefono1 = dtConsulta.Rows[0][1].ToString();
                        Program.telefono2 = dtConsulta.Rows[0][2].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se encontraron registros.";
                        ok.ShowInTaskbar = false;
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

        public void cargarDatosEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select razonsocial, direccionmatriz" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.local = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se encontraron registros.";
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
        
        //CARGAR FECHA DE LA BASE DE DATOS
        private void fechaSistema()
        {
            try
            {
                sSql = "select getdate()";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == true)
                {
                    Program.sFechaSistema = Convert.ToDateTime(dtConsulta.Rows[0][0].ToString());
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

        public void cargarParametrosFacturacionElectronica()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_ambiente, id_tipo_emision," + Environment.NewLine;
                sSql += "id_tipo_certificado_digital, numeroruc" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.iTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
                        Program.iTipoEmision = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_emision"].ToString());
                        Program.iTipoCertificado = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_certificado_digital"].ToString());
                        Program.sNumeroRucEmisor = dtConsulta.Rows[0]["numeroruc"].ToString().Trim();

                        if (Program.iTipoAmbiente == 0)
                        {
                            ok.LblMensaje.Text = "No se encuentra configurado el tipo de ambiente para facturación electrónica. Comuníquese con el administrador.";
                            ok.ShowDialog();
                            return;
                        }

                        else if (Program.iTipoEmision == 0)
                        {
                            ok.LblMensaje.Text = "No se encuentra configurado el tipo de emisión para facturación electrónica. Comuníquese con el administrador.";
                            ok.ShowDialog();
                            return;
                        }

                        else if (Program.iTipoCertificado == 0)
                        {
                            ok.LblMensaje.Text = "No se encuentra configurado el tipo de certificado digital para facturación electrónica. Comuníquese con el administrador.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se encuentra la configuración para la facturación electrónica.";
                        ok.ShowDialog();
                        return;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "select certificado_ruta, certificado_palabra_clave, correo_smtp, correo_puerto," + Environment.NewLine;
                sSql += "correo_que_envia, correo_palabra_clave, correo_con_copia, correo_consumidor_final," + Environment.NewLine;
                sSql += "correo_ambiente_prueba, wsdl_pruebas, url_pruebas, wsdl_produccion, url_produccion," + Environment.NewLine;
                sSql += "maneja_SSL" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.sWebServiceEnvioPruebas = dtConsulta.Rows[0]["wsdl_pruebas"].ToString();
                        Program.sWebServiceConsultaPruebas = dtConsulta.Rows[0]["url_pruebas"].ToString();
                        Program.sWebServiceEnvioProduccion = dtConsulta.Rows[0]["wsdl_produccion"].ToString();
                        Program.sWebServiceConsultaProduccion = dtConsulta.Rows[0]["url_produccion"].ToString();
                        Program.sRutaCertificado = dtConsulta.Rows[0]["certificado_ruta"].ToString();
                        Program.sClaveCertificado = dtConsulta.Rows[0]["certificado_palabra_clave"].ToString();
                        Program.sCorreoSmtp = dtConsulta.Rows[0]["correo_smtp"].ToString();
                        Program.sCorreoEmisor = dtConsulta.Rows[0]["correo_que_envia"].ToString();
                        Program.sClaveCorreoEmisor = dtConsulta.Rows[0]["correo_palabra_clave"].ToString();
                        Program.sCorreoCopia = dtConsulta.Rows[0]["correo_con_copia"].ToString();
                        Program.sCorreoConsumidorFinal = dtConsulta.Rows[0]["correo_consumidor_final"].ToString();
                        Program.sCorreoAmbientePruebas = dtConsulta.Rows[0]["correo_ambiente_prueba"].ToString();
                        Program.iCorreoPuerto = Convert.ToInt32(dtConsulta.Rows[0]["correo_puerto"].ToString());
                        Program.iManejaSSL = Convert.ToInt32(dtConsulta.Rows[0]["maneja_SSL"].ToString());
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se encuentra la configuración de parámetros para la facturación electrónica.";
                        ok.ShowDialog();
                        return;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //OBTENER EL IDENTIFICADOR CG_LOCALIDAD
        public void obtenerCgLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select cg_localidad" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    Program.iCgLocalidadRecuperado = Convert.ToInt32(dtConsulta.Rows[0]["cg_localidad"].ToString());
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
    }
}
