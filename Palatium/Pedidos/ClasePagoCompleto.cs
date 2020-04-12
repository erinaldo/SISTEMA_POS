using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Pedidos
{
    class ClasePagoCompleto
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases_Crear_Comandas.ClaseCrearComanda comanda;

        SqlParameter[] parametro;

        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        string sSql;
        string sFecha;
        string sDescripcionPago;
        string sSecuencial;
        string sNumeroOrden;
        string sEstablecimiento;
        string sPuntoEmision;
        string sNumeroComprobante;
        string sDescripcionFormaPago;
        public string sMensajeError;

        DataTable dtConsulta;
        DataTable dtPagos;

        bool bRespuesta;

        int iIdPedido;
        int iIdPosTipoFormaCobro;
        int iIdTipoComprobante = Program.iComprobanteNotaEntrega;
        public int iIdFactura;
        int iIdCaja;
        int iBanderaComandaPendiente;
        int iIdPersona;
        int iNumeroPedidoOrden;
        int iIdTipoFormaCobro;
        int iIdSriFormaPago_P;

        long iMaximo;

        double dbTotal;
        double dbRecibido;
        double dbCambio;
        double dbServicio;

        public bool insertarPagoCompleto(int iIdOrden_P, double dbTotal_P, double dbRecibido_P, 
                                         double dbCambio_P, int iBanderaComandaPendiente_P,
                                          int iIdPersona_P, int iNumeroPedidoOrden_P)
        {
            try
            {
                this.iIdPedido = iIdOrden_P;
                this.dbTotal = dbTotal_P;
                this.dbRecibido = dbRecibido_P;
                this.dbCambio = dbCambio_P;
                this.iBanderaComandaPendiente = iBanderaComandaPendiente_P;
                this.iIdPersona = iIdPersona_P;
                this.iNumeroPedidoOrden = iNumeroPedidoOrden_P;

                if (obtenerDatosFormaPagoRealizada("EF") == false)
                    return false;
                                
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new  VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                if (crearPagosFactura_V2() == false)
                {
                    return false;
                }

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

        //FUNCION PARA CONTROLAR LA GENERACION DE COMANDAS
        private bool crearPagosFactura_V2()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                if (Program.iDescuentaIva == 1)
                {
                    sSql = "";
                    sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                    sSql += "valor_iva = 0" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        sMensajeError = conexion.sMensajeError;
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        return false;
                    }
                }

                if (insertarPagos_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                if (insertarFactura_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                if (insertarMovimientosCaja_V2() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR NUEVOS PAGOS
        private bool insertarPagos_V2()
        {
            try
            {
                if (crearTablaPagos() == false)
                    return false;

                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarPagos(iIdPedido, dtPagos, Convert.ToDecimal(dbTotal), Convert.ToDecimal(dbCambio), 0,
                                                   iIdPersona, sFecha, Program.iIdLocalidad, 0, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR FACTRUA
        private bool insertarFactura_V2()
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                bRespuesta = comanda.insertarFactura(iIdPedido, iIdTipoComprobante, 0,
                                                     iIdPersona, Program.iIdLocalidad, dtPagos, Convert.ToDecimal(dbTotal), 0,
                                                     0, 0, sFecha, conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sEstablecimiento = comanda.sEstablecimiento;
                sPuntoEmision = comanda.sPuntoEmision;
                sNumeroComprobante = comanda.sNumeroComprobante;
                iIdFactura = comanda.iIdFactura;

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ENVIAR LOS PARAMETROS- INSERTAR FACTRUA
        private bool insertarMovimientosCaja_V2()
        {
            try
            {
                comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                string sNumeroComprobante_P = sEstablecimiento + "-" + sPuntoEmision + "-" + sNumeroComprobante.Trim().PadLeft(9, '0');

                bRespuesta = comanda.insertarMovimientosCaja(sNumeroComprobante_P, iIdPedido, iIdTipoComprobante,
                                                             iIdPersona, iNumeroPedidoOrden, Program.iIdLocalidad,
                                                             conexion);

                if (bRespuesta == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CREAR LA TABLA DE PAGOS PARA ENVIAR POR PARAMETRO
        private bool crearTablaPagos()
        {
            try
            {
                dtPagos = new DataTable();
                dtPagos.Clear();

                dtPagos.Columns.Add("id_pos_tipo_forma_cobro");
                dtPagos.Columns.Add("forma_pago");
                dtPagos.Columns.Add("valor");
                dtPagos.Columns.Add("id_sri_forma_pago");
                dtPagos.Columns.Add("conciliacion");
                dtPagos.Columns.Add("id_operador_tarjeta");
                dtPagos.Columns.Add("id_tipo_tarjeta");
                dtPagos.Columns.Add("numero_lote");
                dtPagos.Columns.Add("bandera_insertar_lote");
                dtPagos.Columns.Add("propina");

                dtPagos.Rows.Add(iIdTipoFormaCobro, sDescripcionFormaPago, dbTotal, iIdSriFormaPago_P, 0,
                                 0, 0, "", 0, 0);

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

        //FUNCION PARA OBTENER LOS VALORES PARA INSERTAR EN LA SECCION DE PAGOS
        private bool obtenerDatosFormaPagoRealizada(string sCodigoFormaPago_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_obtener_datos_formas_pagos" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_localidad";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdLocalidad;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = sCodigoFormaPago_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentran configurados los registros de cobros. Favor comuníquese con el administrador.";
                    ok.ShowDialog();
                    return false;
                }

                iIdTipoFormaCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_tipo_forma_cobro"].ToString());
                sDescripcionFormaPago = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
                iIdSriFormaPago_P = Convert.ToInt32(dtConsulta.Rows[0]["id_sri_forma_pago"].ToString());


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
    }
}
