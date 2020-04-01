using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturador
{
    public partial class frmFacturador : Form
    {
        //VARIABLES PARA REALZAR LA FACTURA
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta;
        DataTable dtDireccion;
        DataTable dtAuxiliar;
        DataTable dtRecuperado;
        DataTable dtAlmacenar;
        DataTable dtAgrupado;

        bool bRespuesta = false;
        string sSql;
        string sNumeroIdentificacion;

        string sTabla;
        string sCampo;
        string sMovimiento;
        long iMaximo;

        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iTercerDigito;
        int iIdTipoComprobante;
        int iIdLocalidadImpresora;
        int iNumeroMovimientoCaja;

        int iIdPersonaDomicilio;
        int iBanderaDomicilio;

        int iCgEstadoDctoPorCobrar = 7461;

        //VARIABLES PARA GUARDAR LA FACTURA Y DATOS DE LA ORDEB
        Orden ord;
        string sIdOrden;
        Double dTotal;
        Double dCambio;
        Double dServicio;
        int iIdFactura;
        int iIdFacturaPedido;
        string sCiudad = Program.sCiudadDefault;
        string sNumeroFactura;
        string sSecuencial;
        string sFecha;

        int iIdPosMovimientoCaja;
        int iIdCaja = 30;
        int iNumeroMovimiento;
        int iLongi;
        int iIdPersonaFactura;
        int iIdFormaPago_1;
        int iIdFormaPago_2;
        int iIdFormaPago_3;

        string sValor;

        public frmFacturador(string sIdOrden, Orden ord, Double total, int iIdPersonaFactura_P, string sNumeroIdentificacion_P)
        //public frmFacturador()
        {
            this.ord = ord;
            this.sIdOrden = sIdOrden;
            this.dTotal = total;
            iIdPersonaFactura = iIdPersonaFactura_P;
            sNumeroIdentificacion = sNumeroIdentificacion_P;
            InitializeComponent();
        }

        #region FUNCIONES DE CONTROL DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        #region FUNCIONES DE INTEGRACION

        //FUNCION PARA CREAR UN DATATABLE
        private void crearDataTable()
        {
            try
            {
                dtAlmacenar = new DataTable();
                dtAlmacenar.Columns.Add("id_forma_pago");
                dtAlmacenar.Columns.Add("valor");
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                int num = (int)catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATATABLE
        private void llenarDataTable()
        {
            try
            {
                crearDataTable();

                for (int i = 0; i < dgvFormasPago.Rows.Count; i++)
                {
                    DataRow row = dtAlmacenar.NewRow();
                    row["id_forma_pago"] = dgvFormasPago.Rows[i].Cells[2].Value.ToString();
                    row["valor"] = dgvFormasPago.Rows[i].Cells[1].Value.ToString();
                    dtAlmacenar.Rows.Add(row);
                }

                IEnumerable<IGrouping<string, DataRow>> query = from item in dtAlmacenar.AsEnumerable()
                                                                group item by item["id_forma_pago"].ToString() into g
                                                                select g;

                dtAgrupado = Transformar(query);

                //dtAgrupado = Transformar(dtAlmacenar.AsEnumerable().GroupBy<DataRow, string>((Func<DataRow, string>)(item => item["id_forma_pago"].ToString())).Select<IGrouping<string, DataRow>, IGrouping<string, DataRow>>((Func<IGrouping<string, DataRow>, IGrouping<string, DataRow>>)(g => g)));

                DataColumn id = new DataColumn("id");
                id.DataType = System.Type.GetType("System.String");
                dtAgrupado.Columns.Add(id);

                for (int i = 0; i < dtAgrupado.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "select id_forma_pago" + Environment.NewLine;
                    sSql += "from cv403_formas_pagos" + Environment.NewLine;
                    sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += "and id_sri_forma_pago = " + Convert.ToInt32(dtAgrupado.Rows[i]["id_forma_pago"].ToString()) + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtAgrupado.Rows.Count > 0)
                        {
                            dtAgrupado.Rows[i]["id"] = dtConsulta.Rows[0][0].ToString();
                        }

                        else
                        {
                            dtAgrupado.Rows[i]["id"] = 0;
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                iIdFormaPago_1 = 0;
                iIdFormaPago_2 = 0;
                iIdFormaPago_3 = 0;

                iIdFormaPago_1 = Convert.ToInt32(dtAgrupado.Rows[0]["id"].ToString());

                if (dtAgrupado.Rows.Count > 1)
                {
                    iIdFormaPago_2 = Convert.ToInt32(dtAgrupado.Rows[1]["id"].ToString());
                }

                if (dtAgrupado.Rows.Count >= 2)
                {
                    iIdFormaPago_3 = Convert.ToInt32(dtAgrupado.Rows[2]["id"].ToString());
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA TRANSFORMAR
        private DataTable Transformar(IEnumerable<IGrouping<string, DataRow>> datos)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id_forma_pago");
                dt.Columns.Add("valor");

                foreach (IGrouping<string, DataRow> item in datos)
                {
                    DataRow row = dt.NewRow();
                    row["id_forma_pago"] = item.Key;
                    row["valor"] = item.Sum<DataRow>(x => Convert.ToDecimal(x["cantidad"]));
                    dt.Rows.Add(row);
                }

                return dt;
            }

            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region FUNCIONES PARA CREAR LA FACTURA

        //FUNCION PARA INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
        private bool insertarMovimiento()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sSecuencial = TxtNumeroFactura.Text.Trim().PadLeft(9, '0');
                //=======================================================================================

                //INSTRUCCIÓN PARA EXTRAER LAS FORMAS DE PAGO
                sSql = "";
                //sSql += "select descripcion, valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                //sSql += "isnull(valor_recibido, valor) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "select descripcion, sum(valor), cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, id_documento_pago" + Environment.NewLine;
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
                        ok.LblMensaje.Text = "No existe formas de pagos realizados. Comuníquese con el administrador del sistema.";
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

                if (rdbFactura.Checked == true)
                {
                    sMovimiento = ("FACT. No. " + txtfacturacion.Text.Trim() + "-" + sSecuencial).Trim();
                }

                else if (rdbNotaVenta.Checked == true)
                {
                    sMovimiento = ("N. VENTA. No. " + txtfacturacion.Text.Trim() + "-" + sSecuencial).Trim();
                }

                for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
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
                    sSql += ", " + Program.iIdPersonaMovimiento + ", " + iIdPersona + ", " + iIdCaja + ", 1," + Environment.NewLine;
                    sSql += "'" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtAuxiliar.Rows[i][1].ToString()) + "," + Environment.NewLine;
                    sSql += "'" + ("COBRO No. CUENTA " + LblOrden.Text.Trim() + " (" + dtAuxiliar.Rows[i][0].ToString() + ")") + "'," + Environment.NewLine;
                    sSql += "'" + sMovimiento.Trim() + "', " + Convert.ToInt32(dtAuxiliar.Rows[i][5].ToString()) + ", " + Program.iJORNADA + "," + Environment.NewLine;
                    sSql += Program.iIdPosCierreCajero + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
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
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    iNumeroMovimientoCaja++;
                }

                //INSTRUCCION ACTUALIZAR EL NUMERO DE MOVIMIENTO EN TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;

                if (rdbFactura.Checked == true)
                {
                    sSql += "numero_factura = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + "," + Environment.NewLine;
                }

                else if (rdbNotaVenta.Checked == true)
                {
                    sSql += "numeronotaventa = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + "," + Environment.NewLine;
                }

                sSql += "numeromovimientocaja = " + iNumeroMovimientoCaja + Environment.NewLine;
                sSql += "where id_localidad_impresora = " + iIdLocalidadImpresora;
                //sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //return false;
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                return false;
            }
        }

        //PROCESO PARA CREAR LA FACTURA
        
        private void insertarFactura()
        {
            try
            {
                string sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");
                int iManejaFE;

                if (rdbFactura.Checked == true)
                {
                    iIdTipoComprobante = 1;
                }

                else if (rdbNotaVenta.Checked == true)
                {
                    iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                }

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS

                if (Program.iFacturacionElectronica == 0)
                {
                    iManejaFE = 0;
                }

                else
                {
                    iManejaFE = 1;
                }

                llenarDataTable();

                //FUNCION PARA INSERTAR EN LA TABLA CV403_FACTURAS
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, id_forma_pago2, id_forma_pago3," + Environment.NewLine;
                sSql += "fecha_factura, fecha_vcto, cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio, facturaelectronica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdTipoComprobante + ", " + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago_1 + "," + Environment.NewLine;

                if (iIdFormaPago_2 == 0)
                {
                    sSql += "null, ";
                }

                else
                {
                    sSql += iIdFormaPago_2 + ", ";
                }

                if (iIdFormaPago_3 == 0)
                {
                    sSql += "null, ";
                }

                else
                {
                    sSql += iIdFormaPago_3 + ", ";
                }

                sSql += "'" + sFechaCorta + "', '" + sFechaCorta + "', " + Program.iMoneda + ", " + dTotal + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                sSql += "'" + txtDireccion.Text.Trim() + "', '" + txtTelefono.Text + "', '" + sCiudad + "'," + Environment.NewLine;
                sSql += "'" + txtMail.Text.Trim() + "', " + dServicio + ", " + iManejaFE + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER ID DEL REGISTRO CV403_FACTURAS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_facturas";
                sCampo = "id_factura";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
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
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAMOS LA TABLA CV403_DCTOS_POR_COBRAR

                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrar + "," + Environment.NewLine;
                sSql += "numero_documento = " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS

                sSql = "";
                sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + Convert.ToInt32(sIdOrden) + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER ID DEL REGISTRO CV403_FACTURAS_PEDIDOS

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_facturas_pedidos";
                sCampo = "id_facturas_pedidos"; ;

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowInTaskbar = false;
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
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    //ENVIAR A HACER UN ROLLBACK
                    goto reversa;
                }
                //=========================================================================================================

                //ACTUALIZAR EL ESTADO A PAGADA Y AGREGAMOS LA FECHA DE CIERRE DE ORDENEN CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "fecha_cierre_orden = GETDATE()" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EJECUTAR ACTUALIZACION 
                if (rdbNotaVenta.Checked == true)
                {
                    sSql = "";
                    sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                    sSql += "idtipocomprobante = 2" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                    //EJECUCIÓN DE LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowInTaskbar = false;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }


                //INSERTAR UN MOVIMIENTO
                if (insertarMovimiento() == true)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    crearReporte();
                    Program.iSeleccionarNotaVenta = 0;
                    goto fin;
                }
                else
                {
                    goto reversa;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            //ACCEDER A HACER EL ROLLBACK
            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

            fin: { }
        }

        //FUNCION PARA CREAR EL REPORTE O IMPRIMIR
        private void crearReporte()
        {
            try
            {                
                Program.iCortar = 1;

                if (rdbFactura.Checked == true)
                {
                    if (Program.iEjecutarImpresion == 1)
                    {
                        ReportesTextBox.frmVistaFactura factura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 1);
                        factura.ShowDialog();

                        if (factura.DialogResult == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;

                            Cambiocs ok = new Cambiocs("$ " + Program.dCambioPantalla.ToString("N2"));
                            ok.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                            ok.ShowDialog();

                            Program.sIDPERSONA = null;
                            Program.dbValorPorcentaje = 0;
                            Program.dbDescuento = 0;

                            factura.Close();
                            this.Close();

                            if (Program.iBanderaCerrarVentana == 0)
                            {
                                ord.Close();
                            }

                            else
                            {
                                Program.iBanderaCerrarVentana = 0;
                            }
                        }
                    }

                    else
                    {
                        this.DialogResult = DialogResult.OK;

                        Cambiocs ok = new Cambiocs("$ " + Program.dCambioPantalla.ToString("N2"));
                        ok.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                        ok.ShowDialog();

                        Program.sIDPERSONA = null;
                        Program.dbValorPorcentaje = 0;
                        Program.dbDescuento = 0;
                        this.Close();

                        if (Program.iBanderaCerrarVentana == 0)
                        {
                            ord.Close();
                        }

                        else
                        {
                            Program.iBanderaCerrarVentana = 0;
                        }
                    }
                }

                else if (rdbNotaVenta.Checked == true)
                {
                    if (Program.iEjecutarImpresion == 1)
                    {
                        //ReportesTextBox.frmVerNotaVenta notaVenta = new ReportesTextBox.frmVerNotaVenta(sIdOrden, 1);
                        ReportesTextBox.frmVerNotaVentaFactura notaVenta = new ReportesTextBox.frmVerNotaVentaFactura(sIdOrden, 1);
                        notaVenta.ShowDialog();

                        if (notaVenta.DialogResult == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;

                            Cambiocs ok = new Cambiocs("$ " + Program.dCambioPantalla.ToString("N2"));
                            ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                            ok.ShowDialog();

                            Program.sIDPERSONA = null;
                            Program.dbValorPorcentaje = 0;
                            Program.dbDescuento = 0;
                            notaVenta.Close();
                            this.Close();

                            if (Program.iBanderaCerrarVentana == 0)
                            {
                                ord.Close();
                            }

                            else
                            {
                                Program.iBanderaCerrarVentana = 0;
                            }
                        }
                    }

                    else
                    {
                        this.DialogResult = DialogResult.OK;

                        Cambiocs ok = new Cambiocs("$ " + Program.dCambioPantalla.ToString("N2"));
                        ok.lblVerMensaje.Text = "NOTA DE ENTREGA GENERADA";
                        ok.ShowDialog();

                        Program.sIDPERSONA = null;
                        Program.dbValorPorcentaje = 0;
                        Program.dbDescuento = 0;
                        this.Close();

                        if (Program.iBanderaCerrarVentana == 0)
                        {
                            ord.Close();
                        }

                        else
                        {
                            Program.iBanderaCerrarVentana = 0;
                        }
                    }
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    Program.sIDPERSONA = null;
                    //actualizarNumeroFactura();
                    Program.dbValorPorcentaje = 0;
                    Program.dbDescuento = 0;
                    this.Close();
                    if (Program.iBanderaCerrarVentana == 0)
                        ord.Close();
                    else
                        Program.iBanderaCerrarVentana = 0;
                }

            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO PARA DATOS DEL CLIENTE        

        //FUNCION PARA CARGAR LAS FORMAS DE PAGO
        private void cargarFormasPago()
        {
            try
            {
                sSql = "";
                sSql += "select descripcion 'FORMA DE PAGO'," + Environment.NewLine;
                sSql += "ltrim(str(isnull(valor, 0), 8, 2)) 'VALOR'," + Environment.NewLine;
                sSql += "id_sri_forma_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "order by valor desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvFormasPago.DataSource = dtConsulta;
                    dgvFormasPago.Columns[0].Width = 250;
                    dgvFormasPago.Columns[1].Width = 120;
                    dgvFormasPago.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    dgvFormasPago.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
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

        //FUNCION PARA EXTRAER EL NUMERO DE ORDEN
        private void consultarNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select CP.cuenta, NP.numero_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP" + Environment.NewLine;
                sSql += "where NP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        LblOrden.Text = dtConsulta.Rows[0][1].ToString();
                        goto fin;
                    }
                }

                else
                {
                    goto reversa;
                }
            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }

        fin: { }
        }

        //FUNCION PARA LEER LOS VALORES PARA REGISTRAR EN LA FACTURA
        private void datosFactura()
        {
            try
            {
                cargarFormasPago();

                sSql = "";
                sSql += "select L.id_localidad, L.establecimiento, L.punto_emision, " + Environment.NewLine;
                sSql += "P.numero_factura, P.numeronotaentrega, P.numeromovimientocaja, P.id_localidad_impresora" + Environment.NewLine;
                sSql += "from tp_localidades L, tp_localidades_impresoras P " + Environment.NewLine;
                sSql += "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql += "and L.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and L.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'";
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No se encuentran registros en la consulta.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                    else
                    {
                        txtfacturacion.Text = dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString();

                        if (rdbFactura.Checked == true)
                        {
                            TxtNumeroFactura.Text = dtConsulta.Rows[0]["numero_factura"].ToString();
                        }

                        else if (rdbNotaVenta.Checked == true)
                        {
                            TxtNumeroFactura.Text = dtConsulta.Rows[0]["numeronotaentrega"].ToString();
                        }

                        iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0]["numeromovimientocaja"].ToString());
                        iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad_impresora"].ToString());
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

        //FUNCION PARA VALIDAR LA CEDULA O RUC
        private void validarIdentificacion()
        {
            try
            {
                if (txtIdentificacion.Text.Length >= 10)
                {
                    iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));
                }
                else
                {
                    goto mensaje;
                }

                if (txtIdentificacion.Text.Length == 10)
                {
                    if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                        goto fin;
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else if (txtIdentificacion.Text.Length == 13)
                {
                    if (iTercerDigito == 9)
                    {
                        if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }

                    }

                    else if (iTercerDigito == 6)
                    {
                        if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else if ((iTercerDigito <= 5) || (iTercerDigito >= 0))
                    {
                        if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {
                
            }

        mensaje:
            {
                ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        //FUNCION EXTRA PARA EXTRACCION DE DATOS EN CASO DE DOMICILIO
        private void consultarRegistroDomicilio(int iIdPersonaDomicilio_P)
        {
            try
            {
                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
                sSql += "TT.domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM dbo.tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE TP.id_persona = " + iIdPersonaDomicilio_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtNombres.Text = dtConsulta.Rows[0][2].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0][3].ToString();
                        txtMail.Text = dtConsulta.Rows[0][4].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0][5].ToString();
                        sCiudad = dtConsulta.Rows[0][8].ToString();

                        if (dtConsulta.Rows[0][6].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][6].ToString();
                        }

                        else if (dtConsulta.Rows[0][7].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][7].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "";
                        }

                        btnGuardar.Enabled = true;
                        btnGuardar.Focus();

                    }

                    btnEditar.Visible = true;
                    goto fin;
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {
                goto mensaje;
            }

        mensaje:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            {
                iBanderaDomicilio = 1;
            }
        }


        //CONSULTAR DATOS EN LA BASE
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM dbo.tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE  TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtNombres.Text = dtConsulta.Rows[0][2].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0][3].ToString();
                        txtMail.Text = dtConsulta.Rows[0][4].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0][5].ToString();
                        sCiudad = dtConsulta.Rows[0][8].ToString();

                        if (dtConsulta.Rows[0][6].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][6].ToString();
                        }

                        else if (dtConsulta.Rows[0][7].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][7].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "";
                        }

                        btnGuardar.Enabled = true;
                        btnGuardar.Focus();

                    }

                    else
                    {
                        //ok.LblMensaje.Text = "No existe ningún registro con la identificación ingresada.";
                        //ok.ShowDialog();

                        frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
                        nuevoCliente.ShowDialog();

                        if (nuevoCliente.DialogResult == DialogResult.OK)
                        {
                            iIdPersona = nuevoCliente.iCodigo;
                            txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                            txtNombres.Text = nuevoCliente.sNombre;
                            txtApellidos.Text = nuevoCliente.sApellido;
                            txtTelefono.Text = nuevoCliente.sTelefono;
                            txtDireccion.Text = nuevoCliente.sDireccion;
                            txtMail.Text = nuevoCliente.sMail;
                            sCiudad = nuevoCliente.sCiudad;
                            nuevoCliente.Close();
                            btnGuardar.Enabled = true;
                            btnGuardar.Focus();
                        }
                    }

                    btnEditar.Visible = true;
                    goto fin;
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {
                goto mensaje;
            }

            mensaje:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
            fin:
            {
                iBanderaDomicilio = 1;
            }
        }

        //FUNCION PARA CALCULAR EL VALOR DE SERVICIO
        private void obtenerServicio()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(cantidad * valor_otro), 0) suma" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dServicio = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        dServicio = 0;
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

        #endregion

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Text = "9999999999999";
            txtApellidos.Text = "CONSUMIDOR FINAL";
            txtNombres.Text = "CONSUMIDOR FINAL";
            txtTelefono.Text = "9999999999";
            txtMail.Text = "dominio@dominio.com";
            txtDireccion.Text = "QUITO";
            iIdPersona = Program.iIdPersona;
            idTipoIdentificacion = 180;
            idTipoPersona = 2447;
            btnGuardar.Enabled = true;
            btnEditar.Visible = false;
            btnGuardar.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtMail.Clear();
            txtIdentificacion.Focus();
        }

        private void frmFacturador_Load(object sender, EventArgs e)
        {
            if (Program.iManejaNotaVenta == 1)
            {
                grupoComprobantes.Visible = true;
            }

            else
            {
                grupoComprobantes.Visible = false;
            }

            if (Program.iDescuentaIva == 1)
            {
                if (Program.iSeleccionarNotaVenta == 1)
                {
                    rdbFactura.Checked = false;
                    rdbNotaVenta.Checked = true;
                    rdbFactura.Enabled = false;
                }

                else
                {
                    rdbFactura.Checked = true;
                    rdbNotaVenta.Checked = false;
                    rdbFactura.Enabled = true;
                }
            }

            if (Program.sCodigoAsignadoOrigenOrden == "03")
            {
                sSql = "";
                sSql += "select TP.identificacion, TP.id_persona" + Environment.NewLine;
                sSql += "from tp_personas TP, cv403_cab_pedidos CP" + Environment.NewLine;
                sSql += "where CP.id_persona = TP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + Convert.ToInt32(sIdOrden);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtIdentificacion.Text = dtConsulta.Rows[0][0].ToString();
                        iIdPersonaDomicilio = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        consultarRegistro();
                        datosFactura();
                    }

                    else
                    {
                        btnConsumidorFinal_Click(sender, e);
                        datosFactura();
                    }
                }
            }

            else if (Program.iIdPersonaFacturador != 0)
            {
                txtIdentificacion.Text = Program.iIdentificacionFacturador;
                consultarRegistro();
                datosFactura();
            }

            else
            {
                btnConsumidorFinal_Click(sender, e);
                datosFactura();
            }

            consultarNumeroOrden();
            obtenerServicio();

            //LblOrden.Text = sIdOrden;
            LblPagar.Text = "$ " + dTotal.ToString("N2");
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                    {
                        //INSTRUCCIONES PARA VALIDAR
                        validarIdentificacion();
                    }
                    else
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmControlDatosCliente controlClientes = new frmControlDatosCliente();
            controlClientes.ShowInTaskbar = false;
            controlClientes.ShowDialog();

            if (controlClientes.DialogResult == DialogResult.OK)
            {
                iIdPersona = controlClientes.iCodigo;
                txtIdentificacion.Text = controlClientes.sIdentificacion;
                consultarRegistro();
                btnGuardar.Focus();
                controlClientes.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtIdentificacion.Text == "") && (txtApellidos.Text == ""))
            {
                ok.LblMensaje.Text = "Favor ingrese los datos del cliente para la factura.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                insertarFactura();
            }
        }

        private void btnEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
            nuevoCliente.ShowDialog();

            if (nuevoCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = nuevoCliente.iCodigo;
                txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                consultarRegistro();
            }
        }

        private void btnEditarFactura_Click(object sender, EventArgs e)
        {
            if (TxtNumeroFactura.ReadOnly == true)
            {
                sNumeroFactura = TxtNumeroFactura.Text.Trim();
                TxtNumeroFactura.ReadOnly = false;
                TxtNumeroFactura.Focus();
            }

            else
            {
                TxtNumeroFactura.Text = sNumeroFactura;
                TxtNumeroFactura.ReadOnly = true;
                txtIdentificacion.Focus();
            }
        }

        private void TxtNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (e.KeyChar == ((char)Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }

            else
            {
                e.Handled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

            if (Program.iBanderaCerrarVentana == 0)
            {
                ord.Close();
            }

            else
            {
                Program.iBanderaCerrarVentana = 0;
            }
        }

        private void frmFacturador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void rdbFactura_CheckedChanged(object sender, EventArgs e)
        {
            datosFactura();
        }

        private void rdbNotaVenta_CheckedChanged(object sender, EventArgs e)
        {
            datosFactura();
        }

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int uno = rand.Next(0, 255);
            int dos = rand.Next(0, 255);
            int tres = rand.Next(0, 255);
            int cuatro = rand.Next(0, 255);

            lblEtiquetaFacturacion.ForeColor = Color.FromArgb(uno, dos, tres, cuatro);
        }

        private void chkPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            txtIdentificacion.Focus();
        }

        private void btnGuardar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnGuardar);
        }

        private void btnGuardar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnGuardar);
        }

        private void btnLimpiar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnLimpiar);
        }

        private void btnLimpiar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnLimpiar);
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalir);
        }

        private void btnEditarFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEditarFactura);
        }

        private void btnEditarFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEditarFactura);
        }

    }
}
