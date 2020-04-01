using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Palatium;
using System.Diagnostics;

namespace Palatium
{
    public partial class PagoTarjetas : Form
    {
        Clases.ClaseFormasPago formasPago = new Clases.ClaseFormasPago();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases.ClaseFormasPagosRecargo formasPagoRecargo = new Clases.ClaseFormasPagosRecargo();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
                
        DataTable dtComanda;
        DataTable dtTarjetasT;
        
        Button boton;
        float subtotal;
        float lbTotal;
        float lbAbono;        
        
        Button bpagar;
        Button botonDinamico;

        double subtotal1 = 0;
        double subtotal2 = 0;
        double iva = 0;
        double recargo = 0;
        double total2 = 0;
        double dbSumaIva;
        double dbTotalAyuda;
        double dbAbono;
        double dbRecalcularIva;
        double dbRecalcularPrecioUnitario;
        double dbRecalcularDescuento;
        double dbPorcentajeRecargo = 0.08;
        double dbSubTotalRecargo;
        double dbSubTotalIncluyeRecargo;
        double dbSubTotalNetoRecargo;
        double dbIVARecargo;
        double dbTotalRecargo;
        double dbValorRecargo;
        
        int iBuscarIDOrden;
        int iBuscarPosicion = 0;
        int iCerrarCuenta;
        int iCuenta;
        int iOpCambiarEstadoOrden;
        int F_int_id_pago;
        int iEjecutarActualizacionIVA;
        int iEjecutarActualizacionTarjetas;

        public string nombre, cantidad, n1 = "", total, referencia;
        string ayudante;
        string sIdOrden;
        string sFecha;
        string sFechaCorta;
        string sTabla;
        string sCampo;
        long iMaximo;

        //VARIABLES NECESARIAS PARA REMOVER E INGRESAR EN POS_MOVIMIENTO_CAJA
        int iNumeroMovimiento;
        int iIdPosMovimientoCaja;
        DataTable dtAuxiliar;
        int iIdPantalla;

        int iIdPersona;
        int iNumeroPedido;
        string sFacturaRecuperada;

        int iIdCaja = 30;

        //VARIABLES NECESARIAS PARA INSERTAR EN LA BASE DE DATOS
        //=======================================================================================================================
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql = "";        
        DataTable dtConsulta;
        bool bRespuesta = false;
        //int iIdConsumidorFinal = 64930;
        int iIdPago;
        int iNumeroPago;
        int iIdEventoCobro;
        int iNumeroFactura;
        int iCgTipoDocumento;
        int iIdDocumentoPago;
        int iIdDocumentoCobrar;
        int iIdDocumentoPagado;
        int iIdTipoComprobante;

        int iIdFactura;

        Double dTotal;
        Double dValor;

        public PagoTarjetas(string sIdOrden, Double dTotal)
        {
            try
            {
                InitializeComponent();
                this.sIdOrden = sIdOrden;
                this.dTotal = dTotal;
                this.dbTotalAyuda = dTotal;
                lbl_total.Text = dTotal.ToString("N2"); 
                llenarInfo();
            }
            catch(Exception)
            {
                ok.LblMensaje.Text = "Error al cargar los datos para el cobro.";
                ok.ShowDialog();
                return;
            }
           
        }

        public PagoTarjetas()
        {
            InitializeComponent();
            //llenarInfo();           
        }

        #region FUNCIONES PARA AGREGAR LAS FORMAS DE PAGO EN LA PRECUENTA

        private bool insertarPagoNuevoPrecuenta()
        {
            try
            {
                //string sFechaCompleta = Program.sFechaSistema.ToString("yyyy/MM/dd HH:mm:ss");
                sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return false;
                }
                //=======================================================================================================

                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    goto reversa;
                }

                //VERIFICAR SI EXISTE UN DOCUMENTO PAGADO PARA DAR DE BAJA SUS DEPENDIENTES
                iCuenta = 0;
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    goto reversa;
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
                    sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
                    }
                }

                //INSERTAR EN LA TABLA CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFechaCorta + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += Convert.ToDouble(lbl_total.Text) + ", " + Convert.ToDouble(lblPropina.Text) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(lblCambio.Text) + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                //=========================================================================================================

                //EXTRAER ID DEL REGISTRO CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPago = Convert.ToInt32(iMaximo);
                }

                //=========================================================================================================
                //EXTRAEMOS EL NUMERO_PAGO DE LA TABLA_TP_LOCALIDADES_IMPRESORAS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    //ENVIAR A HACER UN ROLLBACK
                    goto reversa;
                }
                //=========================================================================================================


                //INSERTAMOS EN LA TABLA CV403_NUMEROS_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //CICLO FOR PARA INSERTAR REGISTROS EN LA TABLA CV403_DOCUMENTOS_PAGOS
                for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
                {
                    //HACEMOS UN SELECT A LA TABLA POS_TIPOS_FORMAS_COBROS PARA EXTRAER LOS CORRELATIVOS
                    sSql = "";
                    sSql += "select cg_tipo_documento" + Environment.NewLine;
                    sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                    sSql += "where id_pos_tipo_forma_cobro = " + dgv_DetallePago.Rows[i].Cells[0].Value;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAMOS EN LA TABLA CV403_DOCUMENTOS_PAGOS 
                    //=======================================================================================================
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                    sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                    sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, valor_recibido) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFechaCorta + "', " + Environment.NewLine;
                    sSql += Program.iMoneda + ", 1, " + Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                    if (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) == 1)
                    {
                        sSql += (Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + Convert.ToDouble(lblCambio.Text));
                    }

                    else
                    {
                        sSql += "null";
                    }

                    sSql += ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                    //=======================================================================================================

                    //VERIFICAMOS SI ES EFECTIVO O NO
                    //=======================================================================================================
                    //=======================================================================================================
                    if ((Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 1) || (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 11))
                    {
                        //=======================================================================================================

                        //OBTENEMOS EL MAX ID DE LA TABLA CV403_DOCUMENTOS_PAGOS
                        //=======================================================================================================
                        //=======================================================================================================

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_documentos_pagos";
                        sCampo = "id_documento_pago";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdDocumentoPago = Convert.ToInt32(iMaximo);
                        }
                    }
                }

                //INSERTAMOS EL ÚNICO DOCUMENTO PAGADO
                //=======================================================================================================
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + Convert.ToDouble(lbl_total.Text) + ", 'A', 1, 0, " + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL NUMERO DE PAGOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR LA ORDEN A ESTADO PRECUENTA
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Pre-Cuenta'" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

         //ACCEDER A HACER EL ROLLBACK
        //=======================================================================================================
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                return false;
            }
        }

        /// <summary>
        /// FUNCION cambiar Formas de Pago
        /// </summary>
        /// <param name="op"></param>
        /// 
        private bool cambiarFormasPagosPrecuenta(int iOp)
        {
            try
            {
                //string sFechaCompleta = Program.sFechaSistema.ToString("yyyy/MM/dd HH:mm:ss");
                string sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return false;
                }
                //=======================================================================================================

                //SE PROCEDE A ACTUALIZAR A ESTADO "E" LOS MOVIMIENTOS EN CAJA
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
                        sSql = "";
                        sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString()) + Environment.NewLine;

                        //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    goto reversa;
                }


                //VERIFICAR SI EXISTE UN DOCUMENTO PAGADO PARA DAR DE BAJA SUS DEPENDIENTES
                iCuenta = 0;
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    goto reversa;
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
                    sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
                    }

                }

                //INSERTAR EN LA TABLA CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFechaCorta + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += Convert.ToDouble(lbl_total.Text) + ", " + Convert.ToDouble(lblPropina.Text) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(lblCambio.Text) + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                //=========================================================================================================

                //EXTRAER ID DEL REGISTRO CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPago = Convert.ToInt32(iMaximo);
                }

                //=========================================================================================================


                //EXTRAEMOS EL NUMERO_PAGO DE LA TABLA_TP_LOCALIDADES_IMPRESORAS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    //ENVIAR A HACER UN ROLLBACK
                    goto reversa;
                }
                //=========================================================================================================


                //INSERTAMOS EN LA TABLA CV403_NUMEROS_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //CICLO FOR PARA INSERTAR REGISTROS EN LA TABLA CV403_DOCUMENTOS_PAGOS
                for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
                {
                    //HACEMOS UN SELECT A LA TABLA POS_TIPOS_FORMAS_COBROS PARA EXTRAER LOS CORRELATIVOS
                    sSql = "";
                    sSql += "select cg_tipo_documento" + Environment.NewLine;
                    sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                    sSql += "where id_pos_tipo_forma_cobro = " + dgv_DetallePago.Rows[i].Cells[0].Value;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAMOS EN LA TABLA CV403_DOCUMENTOS_PAGOS 
                    //=======================================================================================================
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                    sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                    sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, valor_recibido) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFechaCorta + "', " + Environment.NewLine;
                    sSql += Program.iMoneda + ", 1, " + Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                    if (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) == 1)
                    {
                        sSql += (Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + Convert.ToDouble(lblCambio.Text));
                    }

                    else
                    {
                        sSql += "null";
                    }

                    sSql += ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                    //=======================================================================================================

                    //VERIFICAMOS SI ES EFECTIVO O NO
                    //=======================================================================================================
                    //=======================================================================================================
                    if ((Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 1) || (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 11))
                    {
                        //=======================================================================================================

                        //OBTENEMOS EL MAX ID DE LA TABLA CV403_DOCUMENTOS_PAGOS
                        //=======================================================================================================
                        //=======================================================================================================

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_documentos_pagos";
                        sCampo = "id_documento_pago";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdDocumentoPago = Convert.ToInt32(iMaximo);
                        }
                    }
                }

                //INSERTAMOS EL ÚNICO DOCUMENTO PAGADO
                //=======================================================================================================
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + Convert.ToDouble(lbl_total.Text) + ", 'A', 1, 0," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL NUMERO DE PAGOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //ACTUALIZAR EL ESTADO DE LA ORDEN

                if (iOpCambiarEstadoOrden == 1)
                {
                    sSql = "";
                    sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                    sSql += "estado_orden = 'Pagada'" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                if (iOp == 1)
                {
                    if (actualizarMovimientosCaja() == false)
                    {
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

             //ACCEDER A HACER EL ROLLBACK
        //=======================================================================================================
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                return false;
            }
        }

        #endregion

        #region FUNCIONES DE CONTROL DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.BackgroundImage = Properties.Resources.boton_cambio;
            btnProceso.BackgroundImageLayout = ImageLayout.Stretch;
            btnProceso.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProceso.FlatStyle = FlatStyle.Flat;
            btnProceso.BackColor = Color.Transparent;
            btnProceso.ForeColor = Color.Black;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.BackgroundImage = Properties.Resources.boton;
            btnProceso.BackgroundImageLayout = ImageLayout.Stretch;
            btnProceso.ForeColor = Color.White;
        }

        //INGRESAR EL CURSOR AL BOTON DINAMICO
        private void ingresaBotonDinamico(Button btnProceso)
        {
            btnProceso.BackColor = Color.MediumBlue;
            btnProceso.ForeColor = Color.White;
        }

        //SALIR EL CURSOR DEL BOTON DINAMICO
        private void salidaBotonDinamico(Button btnProceso)
        {
            btnProceso.BackColor = Color.FromArgb(255, 224, 192);
            btnProceso.ForeColor = Color.Black;
        }

        #endregion

        #region FUNCIONES PARA INSERTAR EL COBRO AL DAR CLIC EN CONFIRMAR PAGO
        /// <summary>
        /// SE PROCEDE A SEPARAR POR TIPOS DE FORMAS DE COBROS
        /// FUNCION insertarPagoNuevo.- Se la utilizará cuando no se haya ingresado ninguna forma de pago. Pago por primera vez.
        /// </summary
        /// 
        private void insertarPagoNuevo()
        {
            try
            {
                //string sFechaCompleta = Program.sFechaSistema.ToString("yyyy/MM/dd HH:mm:ss");
                sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    goto fin;
                }
                //=======================================================================================================

                if (Program.iDescuentaIva == 1)
                {
                    if (iEjecutarActualizacionIVA == 1)
                    {
                        if (actualizarIVACero() == false)
                        {
                            goto reversa;
                        }
                    }

                    else
                    {
                        if (actualizarIVA() == false)
                        {
                            goto reversa;
                        }
                    }
                }

                if (Program.iAplicaRecargoTarjeta == 1)
                {
                    if (iEjecutarActualizacionTarjetas == 1)
                    {
                        if (actualizarRecargoTarjetas() == false)
                        {
                            goto reversa;
                        }
                    }
                }

                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    goto reversa;
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
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    goto reversa;
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
                    sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
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
                        goto reversa;
                    }
                }

                //INSERTAR EN LA TABLA CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFechaCorta + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += Convert.ToDouble(lbl_total.Text) + ", " + Convert.ToDouble(lblPropina.Text) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(lblCambio.Text) + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                //=========================================================================================================

                //EXTRAER ID DEL REGISTRO CV403_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPago = Convert.ToInt32(iMaximo);
                }

                //=========================================================================================================
                //EXTRAEMOS EL NUMERO_PAGO DE LA TABLA_TP_LOCALIDADES_IMPRESORAS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "select numero_pago" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    //ENVIAR A HACER UN ROLLBACK
                    goto reversa;
                }
                //=========================================================================================================


                //INSERTAMOS EN LA TABLA CV403_NUMEROS_PAGOS
                //=========================================================================================================
                //=========================================================================================================
                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //CICLO FOR PARA INSERTAR REGISTROS EN LA TABLA CV403_DOCUMENTOS_PAGOS
                for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
                {
                    //HACEMOS UN SELECT A LA TABLA POS_TIPOS_FORMAS_COBROS PARA EXTRAER LOS CORRELATIVOS
                    sSql = "";
                    sSql += "select cg_tipo_documento" + Environment.NewLine;
                    sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                    sSql += "where id_pos_tipo_forma_cobro = " + dgv_DetallePago.Rows[i].Cells[0].Value;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAMOS EN LA TABLA CV403_DOCUMENTOS_PAGOS 
                    //=======================================================================================================
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                    sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                    sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, valor_recibido) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFechaCorta + "', " + Environment.NewLine;
                    sSql += Program.iMoneda + ", 1, " + Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                    if (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) == 1)
                    {
                        sSql += (Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + Convert.ToDouble(lblCambio.Text));
                    }

                    else
                    {
                        sSql += "null";
                    }

                    sSql += ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                    //=======================================================================================================

                    //VERIFICAMOS SI ES EFECTIVO O NO
                    //=======================================================================================================
                    //=======================================================================================================
                    if ((Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 1) || (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 11))
                    {
                        //=======================================================================================================

                        //OBTENEMOS EL MAX ID DE LA TABLA CV403_DOCUMENTOS_PAGOS
                        //=======================================================================================================
                        //=======================================================================================================

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_documentos_pagos";
                        sCampo = "id_documento_pago";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdDocumentoPago = Convert.ToInt32(iMaximo);
                        }
                    }
                }

                //INSERTAMOS EL ÚNICO DOCUMENTO PAGADO
                //=======================================================================================================
                //=======================================================================================================
                sSql = "";
                sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + Convert.ToDouble(lbl_total.Text) + ", 'A', 1, 0, " + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL NUMERO DE PAGOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }                

                //AQUI ABRIMOS LA VENTA DE FACTURACION
                Program.dPropinas = 0;
                Orden ord = Owner as Orden;

                //SI NO ES CONSUMIDOR FINAL SE ABRIRA LA VENTANA DE FACTURACION
                if ((Program.sIDPERSONA != null) && (Program.sIDPERSONA != Program.iIdPersona.ToString()) && (Program.sCodigoAsignadoOrigenOrden != "03"))
                {
                    sSql = "";
                    sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                    sSql += "estado_orden = 'Pagada'" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    this.DialogResult = DialogResult.OK;

                    ok.LblMensaje.Text = "La orden se ha almacenado con éxito. Se ha generado un documento por cobrar.";
                    ok.ShowDialog();
                                        
                    this.Close();
                    goto fin;
                }

                else
                {
                    if (Program.iGeneraFactura == 0)
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        Facturador.frmDatosSinFactura datos = new Facturador.frmDatosSinFactura(sIdOrden, ord, Convert.ToDouble(lbl_total.Text));
                        datos.ShowDialog();

                        if (datos.DialogResult == DialogResult.OK)
                        {
                            datos.Close();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }

                    else
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        //Facturador.frmFacturador facturador = new Facturador.frmFacturador(sIdOrden, ord, Convert.ToDouble(lbl_total.Text));
                        //facturador.ShowDialog();

                        //if (facturador.DialogResult == DialogResult.OK)
                        //{
                        //    this.DialogResult = DialogResult.OK;
                        //    this.Close();
                        //}
                    }

                    goto fin;
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

         //ACCEDER A HACER EL ROLLBACK
        //=======================================================================================================
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                //ok.ShowDialog();
            }

        //=======================================================================================================
        fin:
            { }
        }

        /// <summary>
        /// FUNCION cambiar Formas de Pago
        /// </summary>
        /// <param name="op"></param>
        /// 
        private void cambiarFormasPagos(int iOp)
        {
            try
            {
                if ((Convert.ToDouble(lblSaldo.Text) != 0) && (iOp == 1))
                {
                    ok.LblMensaje.Text = "No ha realizado el cobro completo de la comanda.";
                    ok.ShowDialog();
                }

                else
                {
                    //string sFechaCompleta = Program.sFechaSistema.ToString("yyyy/MM/dd HH:mm:ss");
                    string sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                    //INICIAMOS UNA NUEVA TRANSACCION
                    //=======================================================================================================
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.LblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                        goto fin;
                    }
                    //=======================================================================================================

                    //SE PROCEDE A ACTUALIZAR A ESTADO "E" LOS MOVIMIENTOS EN CAJA
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
                            sSql = "";
                            sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                            sSql += "estado = 'E'," + Environment.NewLine;
                            sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                            sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                            sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                            sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString()) + Environment.NewLine;

                            //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //EN CASO DE MANEJAR IVA
                    if (Program.iDescuentaIva == 1)
                    {
                        if (iEjecutarActualizacionIVA == 1)
                        {
                            if (actualizarIVACero() == false)
                            {
                                goto reversa;
                            }
                        }

                        else
                        {
                            if (actualizarIVA() == false)
                            {
                                goto reversa;
                            }
                        }
                    }

                    //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                    sSql = "";
                    sSql += "select id_documento_cobrar" + Environment.NewLine;
                    sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                    sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                        ok.ShowDialog();
                        goto reversa;
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
                        iCuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        goto reversa;
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
                        sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                        sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                iIdPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                                iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                            ok.ShowDialog();
                            goto reversa;
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
                            goto reversa;
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
                            goto reversa;
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
                            goto reversa;
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
                            goto reversa;
                        }

                    }

                    //INSERTAR EN LA TABLA CV403_PAGOS
                    //=========================================================================================================
                    //=========================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_pagos (" + Environment.NewLine;
                    sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                    sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFechaCorta + "', " + Program.iMoneda + "," + Environment.NewLine;
                    sSql += Convert.ToDouble(lbl_total.Text) + ", " + Convert.ToDouble(lblPropina.Text) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                    sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(lblCambio.Text) + ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                    //=========================================================================================================

                    //EXTRAER ID DEL REGISTRO CV403_PAGOS
                    //=========================================================================================================
                    //=========================================================================================================
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv403_pagos";
                    sCampo = "id_pago";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    else
                    {
                        iIdPago = Convert.ToInt32(iMaximo);
                    }

                    //=========================================================================================================


                    //EXTRAEMOS EL NUMERO_PAGO DE LA TABLA_TP_LOCALIDADES_IMPRESORAS
                    //=========================================================================================================
                    //=========================================================================================================
                    sSql = "";
                    sSql += "select numero_pago" + Environment.NewLine;
                    sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                    sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                    else
                    {
                        //ENVIAR A HACER UN ROLLBACK
                        goto reversa;
                    }
                    //=========================================================================================================


                    //INSERTAMOS EN LA TABLA CV403_NUMEROS_PAGOS
                    //=========================================================================================================
                    //=========================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                    sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }


                    //CICLO FOR PARA INSERTAR REGISTROS EN LA TABLA CV403_DOCUMENTOS_PAGOS
                    for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
                    {
                        //HACEMOS UN SELECT A LA TABLA POS_TIPOS_FORMAS_COBROS PARA EXTRAER LOS CORRELATIVOS
                        sSql = "";
                        sSql += "select cg_tipo_documento" + Environment.NewLine;
                        sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                        sSql += "where id_pos_tipo_forma_cobro = " + dgv_DetallePago.Rows[i].Cells[0].Value;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                        //INSERTAMOS EN LA TABLA CV403_DOCUMENTOS_PAGOS 
                        //=======================================================================================================
                        //=======================================================================================================
                        sSql = "";
                        sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                        sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                        sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                        sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                        sSql += "numero_replica_trigger, numero_control_replica, valor_recibido) " + Environment.NewLine;
                        sSql += "values(" + Environment.NewLine;
                        sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFechaCorta + "', " + Environment.NewLine;
                        sSql += Program.iMoneda + ", 1, " + Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                        sSql += Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                        sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                        if (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) == 1)
                        {
                            sSql += (Convert.ToDouble(dgv_DetallePago.Rows[i].Cells[2].Value) + Convert.ToDouble(lblCambio.Text));
                        }

                        else
                        {
                            sSql += "null";
                        }

                        sSql += ")";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                        //=======================================================================================================

                        //VERIFICAMOS SI ES EFECTIVO O NO
                        //=======================================================================================================
                        //=======================================================================================================
                        if ((Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 1) || (Convert.ToInt32(dgv_DetallePago.Rows[i].Cells[0].Value) != 11))
                        {
                            //=======================================================================================================

                            //OBTENEMOS EL MAX ID DE LA TABLA CV403_DOCUMENTOS_PAGOS
                            //=======================================================================================================
                            //=======================================================================================================

                            dtConsulta = new DataTable();
                            dtConsulta.Clear();

                            sTabla = "cv403_documentos_pagos";
                            sCampo = "id_documento_pago";

                            iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                            if (iMaximo == -1)
                            {
                                ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                                ok.ShowDialog();
                                goto reversa;
                            }

                            else
                            {
                                iIdDocumentoPago = Convert.ToInt32(iMaximo);
                            }
                        }
                    }

                    //INSERTAMOS EL ÚNICO DOCUMENTO PAGADO
                    //=======================================================================================================
                    //=======================================================================================================
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                    sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                    sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + Convert.ToDouble(lbl_total.Text) + ", 'A', 1, 0," + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ACTUALIZAR EL NUMERO DE PAGOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                    sSql = "";
                    sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                    sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                    sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }


                    //ACTUALIZAR EL ESTADO DE LA ORDEN

                    if (iOpCambiarEstadoOrden == 1)
                    {
                        sSql = "";
                        sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                        sSql += "estado_orden = 'Pagada'" + Environment.NewLine;
                        sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    if (iOp == 1)
                    {
                        if (actualizarMovimientosCaja() == false)
                        {
                            goto reversa;
                        }
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    
                    ok.LblMensaje.Text = "Las formas de pago se han actualizado éxitosamente";
                    ok.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

             //ACCEDER A HACER EL ROLLBACK
        //=======================================================================================================
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                //ok.ShowDialog();
            }

        //=======================================================================================================
        fin:
            { }
        }

        //FUNCION PARA ACTUALIZAR LOS MOVIMIENTOS EN LAS TABLAS POS_MOVIMIENTO_CAJA Y POS_NUMERO_MOVIMIENTO_CAJA
        private bool actualizarMovimientosCaja()
        {
            try
            {
                //INSTRUCCION SQL PARA EXTRAER EL NUMERO DE MOVIMIENTO A INSERTAR
                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
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
                        iNumeroMovimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENER EL ID_FACTURA
                sSql = "";
                sSql += "select id_factura" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdFactura = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el id de la factura de la tabla cv403_facturas_pedidos";
                        ok.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSTRUCCION PARA EXTRAER DATOS PARA INSERTAR EN EL MOVIMIENTO.
                sSql = "";
                sSql += "select id_persona, numero_pedido, establecimiento," + Environment.NewLine;
                sSql += "punto_emision, numero_factura, idtipocomprobante" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        sFacturaRecuperada = dtConsulta.Rows[0].ItemArray[2].ToString() + "-" + dtConsulta.Rows[0].ItemArray[3].ToString() + "-" + dtConsulta.Rows[0].ItemArray[4].ToString().PadLeft(9, '0');
                        iIdTipoComprobante = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString());
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                /* INSTRUCCIÓN PARA EXTRAER LAS FORMAS DE PAGO                 
                 * PPARA PROCEDER A INSERTAR POR LINEA EN LA TABLA POS_MOVIMIENTO_CAJA
                 */
                sSql = "";
                sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "isnull(valor_recibido, valor) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago " + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                if (bRespuesta == true)
                {
                    if (dtAuxiliar.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existe formas de pagos realizados. Couníquese con el administrador del sistema.";
                        ok.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (iIdTipoComprobante == 1)
                {
                    sFacturaRecuperada = "FACT. No. " + sFacturaRecuperada;
                }

                else if (iIdTipoComprobante == 2)
                {
                    sFacturaRecuperada = "N. VENTA. No. " + sFacturaRecuperada;
                }

                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //FUNCION PARA INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
                {                    
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                    sSql += "tipo_movimiento, idempresa, id_localidad, " + Environment.NewLine;
                    sSql += "id_persona, id_cliente, id_caja, id_pos_cargo," + Environment.NewLine;
                    sSql += "fecha, hora, cg_moneda, valor, concepto, " + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                    sSql += Program.iIdPersonaMovimiento + ", " + iIdPersona + ", " + iIdCaja + ", 1, " + Environment.NewLine;
                    sSql += "'" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtAuxiliar.Rows[i].ItemArray[1].ToString()) + "," + Environment.NewLine;
                    sSql += "'" + ("COBRO No. CUENTA " + iNumeroPedido.ToString() + " (" + dtAuxiliar.Rows[i].ItemArray[0].ToString() + ")") + "', '" + Environment.NewLine;
                    sSql += sFacturaRecuperada + "', " + Environment.NewLine;
                    sSql += Convert.ToInt32(dtAuxiliar.Rows[i].ItemArray[5].ToString()) + ", " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A', " + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    string sTabla = "pos_movimiento_caja";
                    string sCampo = "id_pos_movimiento_caja";

                    long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        return false;
                    }

                    else
                    {
                        iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);
                    }

                    //INSTRUCCION INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimiento + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iNumeroMovimiento++;
                }

                //INSTRUCCION ACTUALIZAR EL NUMERO DE MOVIMIENTO EN TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = " + iNumeroMovimiento + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
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
        
        //FUNCION PARA ANULAR LOS COBROS EN LA TABLA POS_MOVIMIENTO_CAJA
        private bool actualizarMovimiento()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INSTRUCCION SQL PARA EXTRAER EL NUMERO DE MOVIMIENTO A INSERTAR
                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
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
                        iNumeroMovimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSTRUCCION PARA EXTRAER DATOS PARA INSERTAR EN EL MOVIMIENTO.
                sSql = "";
                sSql += "select id_persona, numero_pedido, establecimiento," + Environment.NewLine;
                sSql += "punto_emision, numero_factura" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        sFacturaRecuperada = dtConsulta.Rows[0].ItemArray[2].ToString() + "-" + dtConsulta.Rows[0].ItemArray[3].ToString() + "-" + dtConsulta.Rows[0].ItemArray[4].ToString();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSTRUCCIÓN PARA EXTRAER LAS FORMAS DE PAGO
                sSql = "";
                sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "isnull(valor_recibido, valor) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago " + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                if (bRespuesta == true)
                {
                    if (dtAuxiliar.Rows.Count == 0)
                    {
                        //ok.LblMensaje.Text = "No existe formas de pagos realizados. Couníquese con el administrador del sistema.";
                        //ok.ShowDialog();
                        //return false;
                        goto continuar;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
                {
                    //INSTRUCCION INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (tipo_movimiento, idempresa, id_localidad, " + Environment.NewLine;
                    sSql += "id_persona, id_cliente, id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto, " + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += ", " + Program.iIdPersonaMovimiento + ", " + iIdPersona + ", " + iIdCaja + ", 1 " + Environment.NewLine;
                    sSql += ", '" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtAuxiliar.Rows[i].ItemArray[1].ToString()) + ", '" + ("COBRO No. CUENTA " + iNumeroPedido.ToString() + " (" + dtAuxiliar.Rows[i].ItemArray[0].ToString() + ")") + "', '" + Environment.NewLine;
                    sSql += ("FACT. No. " + sFacturaRecuperada) + "', " + Environment.NewLine;
                    sSql += Convert.ToInt32(dtAuxiliar.Rows[i].ItemArray[5].ToString()) + ", " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A', " + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    string sTabla = "pos_movimiento_caja";
                    string sCampo = "id_pos_movimiento_caja";

                    long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        return false;
                    }

                    else
                    {
                        iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);
                    }

                    //INSTRUCCION INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimiento + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iNumeroMovimiento++;
                }

                //INSTRUCCION ACTUALIZAR EL NUMERO DE MOVIMIENTO EN TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = " + iNumeroMovimiento + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                continuar: {};

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

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CREAR EL ARREGLO DE BOTONES TODAS FORMAS
        public void llenarInfo()
        {
            try
            {
                pnlFormasPagos.Controls.Clear();

                Button[,] boton = new Button[10, 10];
                int h = 0;

                //Program.saldo = double.Parse(txt_saldo.Text);
                if (formasPago.llenarDatos() == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            boton[i, j] = new Button();
                            boton[i, j].Cursor = Cursors.Hand;
                            boton[i, j].BackColor = Color.FromArgb(255, 224, 192);
                            boton[i, j].Click += boton_clic;
                            boton[i, j].MouseEnter += pagos_mouse_enter;
                            boton[i, j].MouseLeave += pagos_mouse_leave;
                            boton[i, j].Width = 150;
                            boton[i, j].Height = 100;
                            boton[i, j].Top = i * 100;
                            boton[i, j].Left = j * 150;
                            boton[i, j].Font = new Font("Consolas", 11, FontStyle.Bold);

                            if (h == formasPago.iCuenta)
                            {
                                break;
                            }

                            if (formasPago.formasPago[h].sImagen == "")
                            {
                                //En el tag se guarda el código de la forma de pago
                                boton[i, j].Tag = formasPago.formasPago[h].sIdFormaPago;
                                //En el text muestra la descripción
                                boton[i, j].Text = formasPago.formasPago[h].sDescripcion;
                                //En el name se guarda el id de la forma de pago
                                //boton[i, j].Name = formasPago.formasPago[h].sIdFormaPago;
                            }

                            else
                            {
                                boton[i, j].Tag = formasPago.formasPago[h].sIdFormaPago;
                                boton[i, j].Text = formasPago.formasPago[h].sDescripcion;

                                if (File.Exists(formasPago.formasPago[h].sImagen))
                                {
                                    boton[i, j].TextAlign = ContentAlignment.BottomCenter;
                                    boton[i, j].Image = Image.FromFile(formasPago.formasPago[h].sImagen);
                                    boton[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                                    boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                                }
                                //boton[i, j].TextImageRelation = TextImageRelation.ImageBeforeText;
                            }

                            //this.Controls.Add(boton[i, j]);
                            pnlFormasPagos.Controls.Add(boton[i, j]);
                            h++;
                        }
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "No hay ninguna forma de pago registrada en el sistema.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL ARREGLO DE BOTONES TARJETAS
        public void llenarInfoTarjetas()
        {
            try
            {
                pnlFormasPagos.Controls.Clear();

                Button[,] boton = new Button[10, 10];
                int h = 0;

                //Program.saldo = double.Parse(txt_saldo.Text);
                if (formasPagoRecargo.llenarDatos() == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            boton[i, j] = new Button();
                            boton[i, j].Cursor = Cursors.Hand;
                            boton[i, j].BackColor = Color.FromArgb(255, 224, 192);
                            boton[i, j].Click += boton_clic;
                            boton[i, j].MouseEnter += pagos_mouse_enter;
                            boton[i, j].MouseLeave += pagos_mouse_leave;
                            boton[i, j].Width = 150;
                            boton[i, j].Height = 100;
                            boton[i, j].Top = i * 100;
                            boton[i, j].Left = j * 150;
                            boton[i, j].Font = new Font("Consolas", 11, FontStyle.Bold);

                            if (h == formasPagoRecargo.iCuenta)
                            {
                                break;
                            }

                            if (formasPagoRecargo.formasPago[h].sImagen == "")
                            {
                                //En el tag se guarda el código de la forma de pago
                                boton[i, j].Tag = formasPagoRecargo.formasPago[h].sIdFormaPago;
                                //En el text muestra la descripción
                                boton[i, j].Text = formasPagoRecargo.formasPago[h].sDescripcion;
                                //En el name se guarda el id de la forma de pago
                                //boton[i, j].Name = formasPago.formasPago[h].sIdFormaPago;
                            }

                            else
                            {
                                boton[i, j].Tag = formasPagoRecargo.formasPago[h].sIdFormaPago;
                                boton[i, j].Text = formasPagoRecargo.formasPago[h].sDescripcion;

                                if (File.Exists(formasPagoRecargo.formasPago[h].sImagen))
                                {
                                    boton[i, j].TextAlign = ContentAlignment.BottomCenter;
                                    boton[i, j].Image = Image.FromFile(formasPagoRecargo.formasPago[h].sImagen);
                                    boton[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                                    boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                                }
                                //boton[i, j].TextImageRelation = TextImageRelation.ImageBeforeText;
                            }

                            //this.Controls.Add(boton[i, j]);
                            pnlFormasPagos.Controls.Add(boton[i, j]);
                            h++;
                        }
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "No hay ninguna forma de pago registrada en el sistema.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            //ESTA FUNCION SOLO SE VERA CARGADA SI EL PAGO YA SE GENERÒ Y SE DESEA REABRIR LA ORDEN 
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        int x = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgv_DetallePago.Rows.Add();
                            dgv_DetallePago.Rows[i].Cells["id"].Value = dtConsulta.Rows[i].ItemArray[1].ToString();
                            dgv_DetallePago.Rows[i].Cells["fpago"].Value = dtConsulta.Rows[i].ItemArray[2].ToString();
                            dValor = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                            dgv_DetallePago.Rows[i].Cells["valor"].Value = dValor.ToString("N2");
                            dTotal = dTotal + dValor;
                        }

                        lblAbono.Text = (Convert.ToDouble(lbl_total.Text) - dTotal).ToString("N2");
                        lblSaldo.Text = (Convert.ToDouble(lbl_total.Text) - Convert.ToDouble(lblAbono.Text)).ToString("N2");

                        dgv_DetallePago.Columns[0].Visible = false;
                        btnConfirmarPago.Enabled = true;
                        //dgv_DetallePago.ClearSelection();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO CLIC
        public void boton_clic(object sender, EventArgs e)
        {
            bpagar = sender as Button;

            if (Convert.ToDouble(lblSaldo.Text) == 0)
            {
                ok.LblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                ok.ShowDialog();
            }

            else
            {
                Efectivo formEfectivo = new Efectivo(bpagar.Tag.ToString(), lblSaldo.Text, total, bpagar.Text.ToString(), "EF");    //PARAMETROS ENVIADOS SOLO PARA QUITAR EL ERROR DE COMPILACION
                AddOwnedForm(formEfectivo);
                formEfectivo.ShowDialog();

                if (formEfectivo.DialogResult == DialogResult.OK)
                {
                    dgv_DetallePago.Rows[0].Selected = true;
                    dgv_DetallePago.CurrentCell = dgv_DetallePago.Rows[0].Cells[1];
                }
            }
        }

        //EVENTOS DE ANIMACION
        private void pagos_mouse_enter(object sender, EventArgs e)
        {
            botonDinamico = sender as Button;
            ingresaBotonDinamico(botonDinamico);
        }

        private void pagos_mouse_leave(object sender, EventArgs e)
        {
            botonDinamico = sender as Button;
            salidaBotonDinamico(botonDinamico);
        }

        //FUNCION PARA CONSULTAR EL IVA
        private void valoresPrecuenta()
        {
            try
            {
                dbSumaIva = 0;

                sSql = "";
                //sSql += "select ltrim(str(sum(cantidad * valor_iva), 10, 2)) suma_iva" + Environment.NewLine;
                //sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                //sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                sSql += "select cantidad, precio_unitario, valor_dscto," + Environment.NewLine;
                sSql += "valor_iva, valor_otro, nombre, paga_iva, id_det_pedido" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtComanda = new DataTable();
                dtComanda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtComanda, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtComanda.Rows.Count; i++)
                    {
                        if ((Convert.ToDouble(dtComanda.Rows[i]["valor_iva"].ToString()) == 0) && (Convert.ToInt32(dtComanda.Rows[i]["paga_iva"].ToString()) == 1))
                        {
                            dbRecalcularPrecioUnitario = Convert.ToDouble(dtComanda.Rows[i]["precio_unitario"].ToString());
                            dbRecalcularDescuento = Convert.ToDouble(dtComanda.Rows[i]["valor_dscto"].ToString());

                            dbRecalcularIva = (dbRecalcularPrecioUnitario - dbRecalcularDescuento) * Program.iva;

                            dtComanda.Rows[i]["valor_iva"] = dbRecalcularIva.ToString();
                        }

                        dbSumaIva += Convert.ToDouble(dtComanda.Rows[i]["cantidad"].ToString()) * Convert.ToDouble(dtComanda.Rows[i]["valor_iva"].ToString());
                    }

                    //dbSumaIva = Convert.ToDouble(dtComanda.Rows[0][0].ToString());
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

        //FUNCION PARA ACTUALIZAR EL IVA EN DET PEDIDOS
        private bool actualizarIVA()
        {
            try
            {
                for (int i = 0; i < dtComanda.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtComanda.Rows[i]["paga_iva"].ToString()) == 1)
                    {
                        sSql = "";
                        sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                        sSql += "valor_iva = " + Convert.ToDouble(dtComanda.Rows[i]["valor_iva"].ToString()) + Environment.NewLine;
                        sSql += "where id_det_pedido = " + Convert.ToInt32(dtComanda.Rows[i]["id_det_pedido"].ToString());

                        //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
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

        //FUNCION PARA ACTUALIZAR EL IVA EN DET PEDIDOS
        private bool actualizarIVACero()
        {
            try
            {
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "valor_iva = 0" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
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

        //FUNCION PARA ACTUALIZAR EL IVA EN DET PEDIDOS CON RECARGO DE TARJETAS
        private bool actualizarRecargoTarjetas()
        {
            try
            {
                for (int i = 0; i < dtTarjetasT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtTarjetasT.Rows[i]["paga_iva"].ToString()) == 1)
                    {
                        sSql = "";
                        sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                        sSql += "precio_unitario = " + Convert.ToDouble(dtTarjetasT.Rows[i]["precio_unitario"].ToString()) + "," + Environment.NewLine;
                        sSql += "valor_iva = " + Convert.ToDouble(dtTarjetasT.Rows[i]["valor_iva"].ToString()) + Environment.NewLine;
                        sSql += "where id_det_pedido = " + Convert.ToInt32(dtTarjetasT.Rows[i]["id_det_pedido"].ToString());

                        //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
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

        //FUNCION PARA ACTUALIZAR EL RECARGO DE TARJETAS DE CREDITO
        private bool aplicaRecargoTarjetas()
        {
            try
            {
                dtTarjetasT = new DataTable();
                dtTarjetasT.Clear();

                double dbPU_P;
                double dbRecargo_P;
                double dbPrecioConRecargo_P;
                double dbIvaRecargo_P;

                dbPU_P = 0;
                dbRecargo_P = 0;
                dbPrecioConRecargo_P = 0;
                dbIvaRecargo_P = 0;                

                dtTarjetasT = dtComanda.Copy();

                for (int i = 0; i < dtTarjetasT.Rows.Count; i++)
                {
                    dbPU_P = Convert.ToDouble(dtTarjetasT.Rows[i]["precio_unitario"].ToString());
                    dbRecargo_P = dbPU_P * Convert.ToDouble(Program.dbPorcentajeRecargoTarjeta);
                    dbPrecioConRecargo_P = dbPU_P + dbRecargo_P;

                    if (Convert.ToInt32(dtTarjetasT.Rows[i]["paga_iva"].ToString()) == 1)
                    {
                        dbIvaRecargo_P = dbPrecioConRecargo_P * Program.iva;
                    }

                    else
                    {
                        dbIvaRecargo_P = 0;
                    }

                    dtTarjetasT.Rows[i]["precio_unitario"] = dbPrecioConRecargo_P.ToString();
                    dtTarjetasT.Rows[i]["valor_iva"] = dbIvaRecargo_P.ToString();
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

        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                //INGRESA O ACTUALIZA LOS PAGOS REALIZADOS
                if (Convert.ToDouble(lblSaldo.Text.Trim()) == 0)
                {
                    //PREGUNTAR SI YA EXISTE UNA FACTURA EMITIDA
                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) == 0)
                        {
                            if (insertarPagoNuevoPrecuenta() == false)
                            {
                                goto fin;
                            }
                        }

                        else
                        {
                            if (cambiarFormasPagosPrecuenta(1) == false)
                            {
                                goto fin;
                            }
                        }

                        Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(sIdOrden, 1, "Pre-Cuenta");
                        precuenta.ShowDialog();
                        goto fin;
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto fin;
                    }
                }

                //ABRE LA PRECUENTA SIN LA INFORMACION DE PAGOS
                else
                {
                    //INICIAMOS UNA NUEVA TRANSACCION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.LblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                        goto fin;
                    }

                    else
                    {
                        sSql = "";
                        sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                        sSql += "estado_orden = 'Pre-Cuenta' " + Environment.NewLine;
                        sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                        //================================================================================
                        //VERIFICAR SI EXISTE O NO UNA PRE-CUENTA CREADA


                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(sIdOrden, 1, "Pre-Cuenta");
                        precuenta.ShowDialog();
                        goto fin;

                    }
                }                    
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa:
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                }
            fin: { }

        }

        private void btnRemoverPropina_Click(object sender, EventArgs e)
        {
            lblPropina.Text = "0.00";
        }

        private void BtnListo_Click(object sender, EventArgs e)
        {
            try
            {
                iOpCambiarEstadoOrden = 0;

                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                iCerrarCuenta = 1;
                if (Convert.ToDouble(lblSaldo.Text) == 0)
                {
                    //PREGUNTAR SI YA EXISTE UNA FACTURA EMITIDA
                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) == 0)
                        {
                            cambiarFormasPagos(0);
                        }

                        else
                        {
                            cambiarFormasPagos(1);
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }

                }

                else
                {
                    ok.LblMensaje.Text = "No se ha realizado el cobro total de la orden.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            tecladoNumericoDividirPrecio teclado = new tecladoNumericoDividirPrecio(lblSaldo.Text);
            teclado.ShowDialog();
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(sIdOrden, 0, "Pre-Cuenta");
            precuenta.ShowDialog();
        }

        private void PagoTarjetas_Load(object sender, EventArgs e)
        {
            Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);

            this.Width = 940;

            subtotal = 0;
            if (Program.iGeneraFactura == 0)
            {
                //CARGAR LOS DATOS EN EL GRID DIRECTAMENTE
                dgv_DetallePago.Rows.Add();

                //dgv_DetallePago.Rows[0].Cells["id"].Value = Program.sIdGrid;
                //dgv_DetallePago.Rows[0].Cells["fpago"].Value = Program.sFormaPagoGrid;

                sSql = "";
                sSql += "select FC.id_pos_tipo_forma_cobro, FC.descripcion" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, pos_origen_orden OO," + Environment.NewLine;
                sSql += "pos_tipo_forma_cobro FC" + Environment.NewLine;
                sSql += "where CP.id_pos_origen_orden = OO.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and OO.id_pos_tipo_forma_cobro = FC.id_pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "and FC.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgv_DetallePago.Rows[0].Cells["id"].Value = dtConsulta.Rows[0].ItemArray[0].ToString();
                        dgv_DetallePago.Rows[0].Cells["fpago"].Value = dtConsulta.Rows[0].ItemArray[1].ToString();
                        dValor = Convert.ToDouble(lbl_total.Text);
                        dgv_DetallePago.Rows[0].Cells["valor"].Value = dValor.ToString("N2");
                        dTotal = dTotal + dValor;

                        btnRemoverPago.Enabled = false;
                    }
                }                
            }

            else
            {
                llenarGrid();
            }

            //dgv_DetallePago.Columns[0].Visible = true;

            for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
            {
                if (dgv_DetallePago.Rows[i].Cells["fpago"].Value == null)
                {
                    dgv_DetallePago.Rows[i].Cells["fpago"].Value = 0;
                }

                subtotal = subtotal + float.Parse(dgv_DetallePago.Rows[i].Cells[2].Value.ToString());
            }


            lblAbono.Text = subtotal.ToString("N2");

            lblSaldo.Text = string.Format("{0:0.00}", (float.Parse(lbl_total.Text) - float.Parse(lblAbono.Text))) + "";


            dgv_DetallePago.Columns[0].Visible = false;

            if (dgv_DetallePago.Rows.Count > 0)
            {
                dgv_DetallePago.Rows[0].Selected = true;
                dgv_DetallePago.CurrentCell = dgv_DetallePago.Rows[0].Cells[1];
            }

            valoresPrecuenta();

            if (Program.iDescuentaIva == 1)
            {
                btnRemoverIVA.Visible = true;
                btnRecargoTarjeta.Visible = true;
                btnPagoCompleto.Visible = true;
                btnDividir.Visible = false;
            }

            else
            {
                btnRemoverIVA.Visible = false;
                btnRecargoTarjeta.Visible = false;
                btnPagoCompleto.Visible = false;
                btnDividir.Visible = true;
            }
        }

        private void PagoTarjetas_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCrearCliente_Click(object sender, EventArgs e)
        {
            Facturador.frmNuevoCliente cliente = new Facturador.frmNuevoCliente("", false);
            cliente.ShowDialog();

            if (cliente.DialogResult == DialogResult.OK)
            {
                Program.iIdPersonaFacturador = cliente.iCodigo;
                Program.iIdentificacionFacturador = cliente.txtIdentificacion.Text.Trim();
                cliente.Close();
            }
        }

        private void btnDividir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDividir);
        }

        private void btnDividir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDividir);
        }

        private void btnRemoverPago_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRemoverPago);
        }

        private void btnRemoverPago_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRemoverPago);
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalir);
        }

        private void BtnListo_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(BtnListo);
        }

        private void BtnListo_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(BtnListo);
        }

        private void btnCrearCliente_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCrearCliente);
        }

        private void btnCrearCliente_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCrearCliente);
        }

        private void btnConfirmarPago_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnConfirmarPago);
        }

        private void btnConfirmarPago_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnConfirmarPago);
        }

        private void btnImprimir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnImprimir);
        }

        private void btnImprimir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnImprimir);
        }

        private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            try
            {
                iOpCambiarEstadoOrden = 1;

                if (Program.iDescuentaIva == 1)
                {
                    Program.dCambioPantalla = Convert.ToDouble(lblCambio.Text.Trim());
                }

                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                iCerrarCuenta = 1;

                if (Convert.ToDouble(lblSaldo.Text) == 0)
                {
                    //PREGUNTAR SI YA EXISTE UNA FACTURA EMITIDA
                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                    sSql += " where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) == 0)
                        {
                            insertarPagoNuevo();
                        }

                        else
                        {
                            cambiarFormasPagos(1);
                        }

                        goto fin;
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "No se ha realizado el cobro total de la orden.";
                    ok.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            //=======================================================================================================
        fin:
            { }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (Program.iBanderaCerrarVentana == 1)
            {
                this.Close();
                Program.iBanderaCerrarVentana = 0;
            }

            else
            {
                Orden ord = Owner as Orden;
                ord.Hide();
                this.Hide();
            }
        }

        private void btnRemoverPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DetallePago.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No hay formas de pago ingresados para remover del registro";
                    ok.ShowDialog();
                }

                else
                {
                    dgv_DetallePago.Rows.Remove(dgv_DetallePago.CurrentRow);

                    subtotal = 0;

                    for (int i = 0; i < dgv_DetallePago.Rows.Count; i++)
                    {
                        subtotal = subtotal + float.Parse(dgv_DetallePago.Rows[i].Cells[2].Value.ToString());
                    }

                    lblAbono.Text = subtotal.ToString("N2");
                    lblSaldo.Text = (Convert.ToDouble(lbl_total.Text) - Convert.ToDouble(lblAbono.Text)).ToString("N2");

                    if (Convert.ToDouble(lbl_total.Text) < subtotal)
                    {
                        lblCambio.Text = (subtotal - Convert.ToDouble(lbl_total.Text)).ToString("N2");
                    }
                    else
                    {
                        lblCambio.Text = "0.00";
                    }

                    //dgv_DetallePago.Columns[0].Visible = false;
                }
            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "No hay ningún producto para eliminar";
                ok.ShowDialog();
            }
        }
        
        private void btnRemoverIVA_Click(object sender, EventArgs e)
        {
            iEjecutarActualizacionTarjetas = 0;

            if (btnRemoverIVA.Text == "REMOVER IVA")
            {
                btnRemoverIVA.BackColor = Color.Turquoise;
                btnRemoverIVA.Text = "DEVOLVER IVA";
                dTotal = dbTotalAyuda - dbSumaIva;

                iEjecutarActualizacionIVA = 1;

                btnRecargoTarjeta.Enabled = false;

                Program.iSeleccionarNotaVenta = 1;
            }

            else
            {
                btnRemoverIVA.BackColor = Color.SpringGreen;
                btnRemoverIVA.Text = "REMOVER IVA";
                dTotal = dbTotalAyuda;

                iEjecutarActualizacionIVA = 0;

                btnRecargoTarjeta.Enabled = true;

                Program.iSeleccionarNotaVenta = 0;
            }

            lbl_total.Text = dTotal.ToString("N2");

            dgv_DetallePago.Rows.Clear();

            lblAbono.Text = "0.00";
            lblSaldo.Text = dTotal.ToString("N2");
            lblCambio.Text = "0.00";
            lblPropina.Text = "0.00";
        }

        private void btnPagoCompleto_Click(object sender, EventArgs e)
        {
            Pedidos.frmEfectivoPagoCompleto efectivo_1 = new Pedidos.frmEfectivoPagoCompleto(sIdOrden, dTotal);
            efectivo_1.ShowDialog();

            if (efectivo_1.DialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnPagoCompleto_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnPagoCompleto);
        }

        private void btnPagoCompleto_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnPagoCompleto);
        }

        private void btnRecargoTarjeta_Click(object sender, EventArgs e)
        {
            iEjecutarActualizacionIVA = 0;

            if (btnRecargoTarjeta.AccessibleDescription == "RECARGO TARJETAS")
            {
                btnRecargoTarjeta.AccessibleDescription = "REMOVER RECARGO";
                btnRecargoTarjeta.Text = "REMOVER" + Environment.NewLine + "RECARGO";

                //dTotal = dbTotalAyuda - dbSumaIva;

                dbSubTotalRecargo = dbTotalAyuda - dbSumaIva;
                dbValorRecargo = dbSubTotalRecargo * dbPorcentajeRecargo;
                dbSubTotalNetoRecargo = dbSubTotalRecargo + dbValorRecargo;
                dbIVARecargo = dbSubTotalNetoRecargo * Program.iva;
                dbTotalRecargo = dbSubTotalNetoRecargo + dbIVARecargo;

                dTotal = dbTotalRecargo;

                iEjecutarActualizacionTarjetas = 1;
                btnRemoverIVA.Enabled = false;
                btnPagoCompleto.Enabled = false;

                llenarInfoTarjetas();

                aplicaRecargoTarjetas();
            }

            else
            {
                btnRecargoTarjeta.AccessibleDescription = "RECARGO TARJETAS";
                btnRecargoTarjeta.Text = "RECARGO" + Environment.NewLine + "TARJETAS";
                dTotal = dbTotalAyuda;

                btnRemoverIVA.Enabled = true;
                btnPagoCompleto.Enabled = true;

                iEjecutarActualizacionTarjetas = 0;
                llenarInfo();

                
            }

            lbl_total.Text = dTotal.ToString("N2");

            dgv_DetallePago.Rows.Clear();

            lblAbono.Text = "0.00";
            lblSaldo.Text = dTotal.ToString("N2");
            lblCambio.Text = "0.00";
            lblPropina.Text = "0.00";
        }
    }
}
