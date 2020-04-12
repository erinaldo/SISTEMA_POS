using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Pedidos
{
    class ClaseIngresarCobros
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        Clases_Crear_Comandas.ClaseCrearComanda comanda;

        SqlParameter[] parametro;

        string sSql;
        string sFechaCorta;
        string sTabla;
        string sCampo;
        string sDescripcionPago;
        string sCorreoElectronico;
        string sDireccion;
        string sTelefono;
        string sSecuencial;
        string sNumeroOrden;
        public string sMensajeError;

        DataTable dtConsulta;
        DataTable dtMaximo;

        bool bRespuesta;

        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPedido;
        int iIdPago;
        int iIdDocumentoPagado;
        int iNumeroPago;
        int iNumeroNotaVenta;
        int iNumeroMovimientoCaja;
        int iCgTipoDocumento = 7456;
        int iIdDocumentoPago;
        int iIdPosTipoFormaCobro;
        int iIdTipoComprobante = Program.iComprobanteNotaEntrega;
        int iIdFactura;
        int iCgEstadoDctoPorCobrarPagado = 7461;
        int iCgEstadoDctoPorCobrarParcial = 7462;
        int iIdFacturaPedido;
        int iIdLocalidadImpresora;
        int iIdPosMovimientoCaja;
        int iIdCaja;
        int iEjecutarCierre;
        int iIdDetPedido;

        long iMaximo;

        double dbTotal;
        //double dbRecibido;
        double dbServicio;
        double dbTotalFacturado;

        public bool recibirParametros(int iIdPedido_P, int iIdDetPedido_P, double dbTotal_P)
        {
            try
            {
                this.iIdPedido = iIdPedido_P;
                this.iIdDetPedido = iIdDetPedido_P;
                this.dbTotal = dbTotal_P;

                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sFechaCorta = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return false;
                }

                if (actualizarEstadoDetPedido(iIdDetPedido) == false)
                {
                    return false;
                }

                if (insertarPago() == false)
                {
                    return false;
                }

                if (iEjecutarCierre == 1)
                {
                    if (insertarFactura() == false)
                    {
                        return false;
                    }
                }                

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ACTUALIZAR EL ESTADO DEL PEDIDO Y ELIMINAR EL IVA
        private bool actualizarEstadoDetPedido(int iIdDetPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "valor_iva = 0," + Environment.NewLine;
                sSql += "estado_pago = 'PAGADA'" + Environment.NewLine;
                sSql += "where id_det_pedido = " + iIdDetPedido_P + Environment.NewLine;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

        //INSERTAR LA SEGUNDA FASE DE PAGOS
        private bool insertarPago()
        {
            try
            {
                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
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
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    goto reversa;
                }

                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
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
                        iCuenta = 1;
                    }

                    else
                    {
                        iCuenta = 0;
                    }                    
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR O ACTUALIZAR LA TABLA CV403_PAGOS
                if (iCuenta == 0)
                {
                    sSql = "";
                    sSql += "select numero_pago, id_localidad_impresora" + Environment.NewLine;
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
                            iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                            iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        }

                        else
                        {
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "No se pudo obtener el número secuencial para pagos.";
                            ok.ShowDialog();
                            goto reversa;
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sSql = "";
                    sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                    sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                    sSql += "where id_localidad_impresora = " + iIdLocalidadImpresora;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAR EN LA TABLA CV403_PAGOS
                    sSql = "";
                    sSql += "insert into cv403_pagos (" + Environment.NewLine;
                    sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                    sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, cambio) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFechaCorta + "', " + Program.iMoneda + "," + Environment.NewLine;
                    sSql += dbTotal + ", 0, " + Program.iCgEmpresa + "," + Environment.NewLine;
                    sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //EXTRAER ID DEL REGISTRO CV403_PAGOS
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_pagos";
                    sCampo = "id_pago";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    else
                    {
                        iIdPago = Convert.ToInt32(iMaximo);
                    }

                    //INSERTAMOS EN LA TABLA CV403_NUMEROS_PAGOS
                    sSql = "";
                    sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                    sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "valor = valor + " + dbTotal + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }
                                              
                //EXTRAER EL REGISTRO DE EFECTIVO
                sSql = "";
                sSql += "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where cg_tipo_documento = " + iCgTipoDocumento + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPosTipoFormaCobro = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    sDescripcionPago = dtConsulta.Rows[0][1].ToString();
                }

                //INSERTAMOS EN LA TABLA CV403_DOCUMENTOS_PAGOS
                sSql = "";
                sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, valor_recibido) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFechaCorta + "', " + Environment.NewLine;
                sSql += Program.iMoneda + ", 1, " + dbTotal + ", " + iIdPosTipoFormaCobro + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, " + dbTotal + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PARA INSERTAR O ACTUALIZAR EL CV403_DOCUMENTOS_PAGADOS
                if (iCuenta == 0)
                {
                    //INSERTAMOS EL ÚNICO DOCUMENTO PAGADO
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                    sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                    sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dbTotal + ", 'A', 1, 0, " + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "valor = valor + " + dbTotal + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //CONSULTAR SI AUN EXISTEN LINEAS POR COBRAR
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado_pago is null" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    if (iCuenta == 0)
                    {
                        iEjecutarCierre = 1;
                    }

                    else
                    {
                        iEjecutarCierre = 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (iEjecutarCierre == 1)
                {            
                    //ACTUALIZAR LOS DATOS EN DCTOS_POR_COBRAR
                    sSql = "";
                    sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                    sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrarPagado + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    //ACTUALIZAR LOS DATOS EN DCTOS_POR_COBRAR
                    sSql = "";
                    sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                    sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrarParcial + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

        //INSERTAR LA TERCERA FASE - FACTURACION
        private bool insertarFactura()
        {
            try
            {
                if (consultarSecuenciasInformacion() == false)
                {
                    return false;
                }                

                //INSERTAR EN LA TABLA CV403_FACTURAS
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, fecha_factura, fecha_vcto," + Environment.NewLine;
                sSql += "cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdTipoComprobante + "," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", ";
                sSql += Program.iIdVendedor + ", 14, '" + sFechaCorta + "'," + Environment.NewLine;
                sSql += "'" + sFechaCorta + "', " + Program.iMoneda + ", " + dbTotalFacturado + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                sSql += "'" + sDireccion.ToUpper() + "', '" + sTelefono + "', '" + sDireccion + "'," + Environment.NewLine;
                sSql += "'" + sCorreoElectronico + "', " + dbServicio + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER ID DEL REGISTRO CV403_FACTURAS
                sTabla = "cv403_facturas";
                sCampo = "id_factura";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdFactura = Convert.ToInt32(iMaximo);
                }

                //INSERTAR EN LA TABLA CV403_NUMEROS_FACTURAS
                sSql = "";
                sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura, " + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, " + Environment.NewLine;
                sSql += "numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + iNumeroNotaVenta + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAMOS LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                sSql += "numero_documento = " + iNumeroNotaVenta + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
                sSql = "";
                sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdPedido + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER ID DEL REGISTRO CV403_FACTURAS_PEDIDOS
                sTabla = "cv403_facturas_pedidos";
                sCampo = "id_facturas_pedidos"; ;

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdFacturaPedido = Convert.ToInt32(iMaximo);
                }

                //RECUPERAMOS DATOS NECESARIOS DE LA TABLA CV403_DETALLE_PEDIDOS
                sSql = "";
                sSql += "select id_det_pedido, id_producto, cantidad " + Environment.NewLine;
                sSql += "from cv403_det_pedidos " + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL ESTADO A PAGADA Y AGREGAMOS LA FECHA DE CIERRE DE ORDENEN CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                sSql += "id_persona = " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += "fecha_cierre_orden = GETDATE()" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL TIPO DE COMPROBANTE EN CV403_NUMERO_CAB_PEDIDO
                sSql = "";
                sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                sSql += "idtipocomprobante = " + iIdTipoComprobante + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //RECUPERAR LOS PAGOS INGRESADOS
                sSql = "";
                sSql += "select descripcion, sum(valor), cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago " + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONAR EL ID DE CAJA
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select id_caja" + Environment.NewLine;
                sSql += "from cv405_cajas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and cg_tipo_caja = 8906";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_caja"].ToString());

                //INSERTAR LOS MOVIMIENTOS DE CAJA MOVIMIENTO
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {                    
                    //INSTRUCCION INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                    sSql += "tipo_movimiento, idempresa, id_localidad, id_persona, id_cliente," + Environment.NewLine;
                    sSql += "id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto," + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += ", " + Program.iIdPersonaMovimiento + ", " + Program.iIdPersona + ", " + iIdCaja + ", 1," + Environment.NewLine;
                    sSql += "'" + sFechaCorta + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString()) + "," + Environment.NewLine;
                    sSql += "'" + ("COBRO No. CUENTA " + sNumeroOrden + " (" + dtConsulta.Rows[i][0].ToString() + ")") + "'," + Environment.NewLine;
                    sSql += "'" + sSecuencial.Trim() + "', " + Convert.ToInt32(dtConsulta.Rows[i][5].ToString()) + ", " + Program.iJORNADA + "," + Environment.NewLine;
                    sSql += Program.iIdPosCierreCajero + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                    sTabla = "pos_movimiento_caja";
                    sCampo = "id_pos_movimiento_caja";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    else
                    {
                        iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);
                    }

                    //INSTRUCCION INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimientoCaja + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    iNumeroMovimientoCaja++;
                }

                Program.iSeleccionarNotaVenta = 0;

                //ACTUALIZAR EL NUMERO DE PAGOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeronotaentrega = numeronotaentrega + 1," + Environment.NewLine;
                sSql += "numeromovimientocaja = " + iNumeroMovimientoCaja + Environment.NewLine;
                sSql += "where id_localidad_impresora = " + iIdLocalidadImpresora;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

        //CONSULTAR LOS NUMEROS DE TP_LOCALIDADES IMPRESORAS
        private bool consultarSecuenciasInformacion()
        {
            try
            {
                //CONSULTAR LOS DATOS DEL CONSUMIDOR FINAL
                sSql = "";
                sSql += "select ltrim(TP.apellidos + ' ' + isnull(TP.nombres, '')) nombre," + Environment.NewLine;
                sSql += "isnull(TP.correo_electronico, '') correo_electronico, isnull(TD.direccion, 'ND') direccion, " + Environment.NewLine;
                sSql += "isnull(isnull(TT.domicilio, TT.oficina), 999999999) telefono_domicilio" + Environment.NewLine;
                sSql += "FROM tp_personas TP LEFT OUTER JOIN " + Environment.NewLine;
                sSql += "tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "where TP.id_persona = " + Program.iIdPersona;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCorreoElectronico = dtConsulta.Rows[0][1].ToString();
                        sDireccion = dtConsulta.Rows[0][2].ToString();
                        sTelefono = dtConsulta.Rows[0][3].ToString();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentra configurado el registro de Consumidor Final. Comuníquese con el administrador.";
                        ok.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    goto reversa;
                }

                //CONSULTAR EL NUMERO DE ORDEN
                sSql = "";
                sSql += "select CP.cuenta, NP.numero_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP" + Environment.NewLine;
                sSql += "where NP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sNumeroOrden = dtConsulta.Rows[0][1].ToString();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo consultar el número histórico de la comanda. Comuníquese con el administrador.";
                        ok.ShowDialog();
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER EL SERVICIO
                sSql = "";
                sSql = sSql + "select isnull(sum(cantidad * valor_otro), 0) suma" + Environment.NewLine;
                sSql = sSql + "from pos_vw_det_pedido" + Environment.NewLine;
                sSql = sSql + "where id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbServicio = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        dbServicio = 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAEMOS EL NUMERO_PAGO DE LA TABLA_TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "select P.numeronotaentrega, P.numeromovimientocaja," + Environment.NewLine;
                sSql += "L.establecimiento, L.punto_emision, P.id_localidad_impresora" + Environment.NewLine;
                sSql += "from tp_localidades L, tp_localidades_impresoras P" + Environment.NewLine;
                sSql += "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql += "and L.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and L.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iNumeroNotaVenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    sSecuencial = "N. VENTA. No. " + dtConsulta.Rows[0][2].ToString().Trim().PadLeft(3, '0') + "-" + dtConsulta.Rows[0][3].ToString().Trim().PadLeft(3, '0') + "-" + iNumeroNotaVenta.ToString().PadLeft(9, '0');
                    iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0][4].ToString());
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //CONSULTAR EL TOTAL DE LA FACTURA
                sSql = "";
                sSql += "select sum(cantidad * precio_unitario) suma" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dbTotalFacturado = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

    }
}
