using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class frmOpcionesReabrir : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();

        DataTable dtConsulta;
        DataTable dtAuxiliar;

        bool bRespuesta;
        bool bRespuestaEnvioMail;
        
        string sIdOrden;
        string sSql;
        string sTabla;
        string sCampo;
        string sMotivoAnulacion;
        string sDescripcionOrigen;
        string sNombreMesero;

        int iNumeroPersonas;
        int iIdOrigenOrden;
        int iIdMesa;
        int iIdCabPedido_F;
        int iIdCabDespacho_F;
        int iIdFacturasPedidos_F;
        int iIdDespachoPedido_F;
        int iIdEventoCobro_F;
        int iIdTipoComprobante;
        int iIdFactura_F;
        int iNumeroFactura_F;
        int iCuenta;
        int iIdDocumentoPagado;
        int iIdPago;
        int iNumeroPedido;
        int iOp;
        int iIdCajero;
        int iIdMesero;
       
        long iMaximo;

        string sFecha;
        string sCorreoEmisor;
        string sCorreoCopia1;
        string sCorreoCopia2;
        string sPalabraClave;
        string sSmtp;
        string sPuerto;
        string sManejaSSL;
        string sNombreComercial;
        string sRazonSocial;
        string sMensajeEnviar;
        string sFacturaActual;
        string sAsuntoMail;
        string sMensajeRetorno;
        string sCorreoOrigenEnvio;
        
        Double dbTotal;
        double dValor;

        int iIdDocumentoCobrar;

        public frmOpcionesReabrir(string sIdOrden, Double dbTotal)
        {
            this.sIdOrden = sIdOrden;
            this.dbTotal = dbTotal;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LOS DATOS DE LA ORDEN
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from  pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0][35].ToString());
                        sDescripcionOrigen = dtConsulta.Rows[0][40].ToString();
                        iNumeroPersonas = Convert.ToInt32(dtConsulta.Rows[0][34].ToString());
                        iIdCajero = Convert.ToInt32(dtConsulta.Rows[0][30].ToString());
                        iIdMesero = Convert.ToInt32(dtConsulta.Rows[0][31].ToString());
                        sNombreMesero = dtConsulta.Rows[0][48].ToString();


                        if (dtConsulta.Rows[0][29].ToString() == null)
                        {
                            iIdMesa = 0;
                        }

                        else
                        {
                            iIdMesa = Convert.ToInt32(dtConsulta.Rows[0][29].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No hay productos registrados en la comanda. Favor comunicarse con el administrador del sistema.";
                        ok.ShowDialog();
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

        //FUNCION PARA CONSULTAR LOS DATOS DEL CORREO DEL EMISOR
        private void consultarDatosMail()
        {
            try
            {
                sSql = "";
                sSql += "select correo_que_envia, correo_con_copia_1," + Environment.NewLine;
                sSql += "correo_con_copia_2, correo_palabra_clave," + Environment.NewLine;
                sSql += "correo_smtp, correo_puerto, maneja_SSL" + Environment.NewLine;
                sSql += "from pos_correo_emisor" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCorreoEmisor = dtConsulta.Rows[0][0].ToString();
                        sCorreoCopia1 = dtConsulta.Rows[0][1].ToString();
                        sCorreoCopia2 = dtConsulta.Rows[0][2].ToString();
                        sPalabraClave = dtConsulta.Rows[0][3].ToString();
                        sSmtp = dtConsulta.Rows[0][4].ToString();
                        sPuerto = dtConsulta.Rows[0][5].ToString();
                        sManejaSSL = dtConsulta.Rows[0][6].ToString();

                        consultarNombreComercial();
                    }

                    else
                    {
                        iOp = 0;
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


        //FUNCION PARA EXTRAER EL NOMBRE COMERCIAL
        private void consultarNombreComercial()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(nombrecomercial, '') nombrecomercial, razonsocial" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    sNombreComercial = dtConsulta.Rows[0][0].ToString();
                    sRazonSocial = dtConsulta.Rows[0][1].ToString();
                    iOp = 1;
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


        //FUNCION PARA ENVIAR EL CORREO ELECTRONICO
        private bool enviarMail()
        {
            try
            {
                sAsuntoMail = "IMPORTANTE: ANULACIÓN DE FACTURA";

                sMensajeRetorno = crearMensajeEnvio();

                bRespuestaEnvioMail = correo.enviarCorreo(sSmtp, Convert.ToInt32(sPuerto), sCorreoEmisor,
                                      sPalabraClave, sCorreoEmisor, sCorreoEmisor,
                                      sCorreoCopia1, sCorreoCopia2, sAsuntoMail,
                                      "", sMensajeRetorno, Convert.ToInt32(sManejaSSL));

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

                sMensajeEnviar = sMensajeEnviar + "Estimado(a) " + sRazonSocial + ":" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Se informa que se ha procedido a anular la factura No. " + sFacturaActual + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "MOTIVO DE ANULACIÓN:" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + sMotivoAnulacion.ToUpper() + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Enviado por: " + Program.sDatosMaximo[0].ToUpper() + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Fecha y Hora: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                return sMensajeEnviar;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }

        //=================================================================================================
        private void insertarPedido()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //EXTRAER EL NUMERO DE PEDIDO DE LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                
                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_cab_pedidos (idEmpresa, cg_empresa, Id_Localidad," + Environment.NewLine;
                sSql += "Fecha_Pedido, id_persona, Cg_Tipo_Cliente, Cg_Moneda, Porcentaje_Iva," + Environment.NewLine;
                sSql += "id_vendedor, Fabricante, referencia, Comentarios, cg_estado_Pedido," + Environment.NewLine;
                sSql += "Porcentaje_Dscto, Cg_Facturado, Fecha_Ingreso, Usuario_Ingreso," + Environment.NewLine;
                sSql += "Terminal_Ingreso, Estado, numero_replica_trigger, numero_control_replica, porcentaje_servicio, id_pos_cierre_cajero) " + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iIdPersona + ", 8032, " + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + ", " + Program.iIdVendedor + ", 78, '', '', 6967, 0, 7469," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'A', 0, 0, " + (Program.servicio * 100) + ", " + Program.iIdPosCierreCajero + ")";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCabPedido_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "Insert Into cv403_cab_despachos (idEmpresa, id_persona, cg_empresa, Id_Localidad," + Environment.NewLine;
                sSql += "Fecha_Despacho, Cg_Motivo_Despacho, Id_Destinatario, Punto_Partida, Cg_Ciudad_Entrega," + Environment.NewLine;
                sSql += "Direccion_Entrega, Id_Transportador, Fecha_Inicio_Transporte, Fecha_Fin_Transporte," + Environment.NewLine;
                sSql += "cg_estado_Despacho, Punto_Venta, Comentarios, Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', 6972, " + Program.iIdPersona + ", '" + Program.sCiudadDefault + "'," + Environment.NewLine;
                sSql += "0, '" + Program.sCiudadDefault + "', " + Program.iIdPersona + ", '" + sFecha + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', 6970, 1, '', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_despachos";
                sCampo = "Id_Despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCabDespacho_F = Convert.ToInt32(iMaximo);
                }
                
                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_despachos_pedidos (Id_Despacho, Id_Pedido, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdCabDespacho_F + ", " + iIdCabPedido_F + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_DESPACHOS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_despachos_pedidos";
                sCampo = "Id_Despacho_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdDespachoPedido_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN  LA TABLA CV403_NUMERO_CAB_PEDIDO
                sSql = "";
                sSql = "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                sSql += "numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1," + iIdCabPedido_F + ", " + iNumeroPedido + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL NUMERO DE PEDIDO EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_det_pedidos(Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva, comentario, Id_Definicion_Combo," + Environment.NewLine;
                sSql += "fecha_ingreso, Usuario_Ingreso, Terminal_ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdCabPedido_F + ", " + Program.iIdProductoAnular + ", 546, " + Program.dValorProductoAnular + "," + Environment.NewLine;
                sSql += "1, 0, 0, " + (Program.iva * 100) + ", '', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CANTIDADES_DESPACHADAS
                sSql = "";
                sSql += "Insert Into cv403_cantidades_despachadas (Id_Despacho_Pedido, id_producto," + Environment.NewLine;
                sSql += "Cantidad, Estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdDespachoPedido_F + ", " + Program.iIdProductoAnular + ", 1, 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                
                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "Insert Into cv403_eventos_cobros (idEmpresa, cg_empresa, id_persona, Id_Localidad," + Environment.NewLine;
                sSql += "Cg_Evento_Cobro, Cg_Moneda, Valor,Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7466, " + Program.iMoneda + ", " + Program.dValorProductoAnular + "," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_eventos_cobros ";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdEventoCobro_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCIONES PARA OBTENER DATOS DE LAS TABLAS CV403_FACTURAS_PEDIDOS Y CV403_NUMEROS_FACTURAS
                sSql = "";
                sSql += "select FP.id_facturas_pedidos, FP.id_factura, NF.numero_factura" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos FP, cv403_numeros_facturas NF" + Environment.NewLine;
                sSql += "where FP.id_factura = NF.id_factura" + Environment.NewLine;
                sSql += "and FP.id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;
                sSql += "and NF.estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdFacturasPedidos_F = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iIdFactura_F = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        iNumeroFactura_F = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existen datos en las tablas cv403_facturas_pedidos" + Environment.NewLine + "y" + Environment.NewLine + "cv403_numeros_facturas";
                        ok.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EL MOTIVO DE ANULACIÓN DE LA FACTURA
                sSql = "";
                sSql += "insert into pos_anulacion_factura (" + Environment.NewLine;
                sSql += "id_factura, motivo_anulacion, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura_F + ", '" + sMotivoAnulacion + "', 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "Insert Into cv403_dctos_por_cobrar (id_evento_cobro, cg_tipo_documento," + Environment.NewLine;
                sSql += "id_factura, Numero_Documento, Fecha_Vcto, Cg_moneda, Valor, cg_estado_dcto," + Environment.NewLine;
                sSql += "Estado, Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdEventoCobro_F + ", 2725, " + iIdFactura_F + ", " + iNumeroFactura_F + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + Program.dValorProductoAnular + ", 7460," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA ACTUALIZAR EL ID DEL PEDIDO EN LA TABLA CV403_FACTURAS_PEDIDOS
                sSql = "";
                sSql += "update cv403_facturas_pedidos set" + Environment.NewLine;
                sSql += "id_pedido = " + iIdCabPedido_F + Environment.NewLine;
                sSql += "where id_facturas_pedidos = " + iIdFacturasPedidos_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA DEJAR LOS CAMPOS NULOS EN CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "id_factura = null," + Environment.NewLine;
                sSql += "numero_documento = null" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (eliminarPagos() == false)
                {
                    goto reversa;
                }

                //INSTRUCCION PARA CAMBIAR EL ESTADO A ABIERTA DE LA ORDEN
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Abierta'," + Environment.NewLine;
                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0" + Environment.NewLine; 
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                consultarRegistro();

                //ABRIR EL FORMULARIO DE ORDEN
                //ACTUALIZACION ELVIS COMANDA
                //=======================================================================================================================
                ComandaNueva.frmComanda o = new ComandaNueva.frmComanda(Convert.ToInt32(sIdOrden), "OK");
                o.ShowDialog();
                this.DialogResult = DialogResult.OK;
                Program.iBanderaReabrir = 1;
                //=======================================================================================================================

                //Orden o = new Orden(iIdOrigenOrden, sDescripcionOrigen, iNumeroPersonas, iIdMesa, Convert.ToInt32(sIdOrden), "OK", Program.iIdPersona, iIdCajero, iIdMesero, sNombreMesero, 0, 0);
                //o.ShowDialog();
                //this.DialogResult = DialogResult.OK;
                //Program.iBanderaReabrir = 1;

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        private bool eliminarPagos()
        {
            try
            {
                //SE PROCEDE A ACTUALIZAR A ESTADO "E" LOS MMOVIMIENTOS EN CAJA
                sSql = "";
                sSql += "select id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        //EXTRAER EL ID DE LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                        sSql = "";
                        sSql += "select id_pos_movimiento_caja" + Environment.NewLine;
                        sSql += "from pos_movimiento_caja" + Environment.NewLine;
                        sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        dtAuxiliar = new DataTable();
                        dtAuxiliar.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                        if (bRespuesta == true)
                        {
                            sSql = "";
                            sSql += "update pos_numero_movimiento_caja set" + Environment.NewLine;
                            sSql += "estado = 'E'" + Environment.NewLine;
                            sSql += "where id_pos_movimiento_caja = " + Convert.ToInt32(dtAuxiliar.Rows[0][0].ToString());

                            //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }

                        sSql = "";
                        sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;

                        //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }


                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    return false;
                }


                //VERIFICAR SI EXISTE UN DOCUMENTO PAGADO PARA DAR DE BAJA SUS DEPENDIENTES
                iCuenta = 0;
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    return false;
                }

                if (iCuenta > 0)
                {
                    /* SE PROCEDE A DAR DE BAJA LOS REGISTROS DE LAS TABLAS:
                     * CV403_PAGOS
                     * CV403_DOCUMENTOS_PAGOS
                     * CV403_NUMEROS_PAGOS
                     * CV403_DOCUMENTOS_PAGADOS
                    */

                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_PAGOS
                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_NUMEROS_PAGOS
                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            //PagoTarjetas pagos = new PagoTarjetas(sIdOrden, dbTotal);
            Pedidos.frmCambiarFormasCobros pagos = new Pedidos.frmCambiarFormasCobros(sIdOrden);
            pagos.ShowDialog();

            if (pagos.DialogResult == DialogResult.OK)
            {
                pagos.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmOpcionesReabrir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnReversar_Click(object sender, EventArgs e)
        {
            SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar los cobros con la anulación de factura de la comanda?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                MotivosCancelacion.frmMotivoAnulacionFactura anulacion = new MotivosCancelacion.frmMotivoAnulacionFactura();
                anulacion.lblEtiqueta.Text = "Ingrese el motivo de anulación de la factura.";
                anulacion.ShowDialog();

                if (anulacion.DialogResult == DialogResult.OK)
                {
                    sMotivoAnulacion = anulacion.sMotivoAnulacion;
                    anulacion.Close();
                    insertarPedido();
                }
            }
        }

        private void frmOpcionesReabrir_Load(object sender, EventArgs e)
        {
            //consultarDatosMail();
        }
    }
}
