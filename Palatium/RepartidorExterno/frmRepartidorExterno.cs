using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.RepartidorExterno
{
    public partial class frmRepartidorExterno : Form
    {
        //#region FUNCIONES DE WINDOWS PARA MOVER EL FORMULARIO
        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private static extern void ReleaseCapture();

        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        //#endregion

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        Clases.ClaseManejoCaracteres caracteres = new Clases.ClaseManejoCaracteres();

        DataTable dtConsulta;
        DataTable dtDatos;
        DataTable dtCopiaConsulta;

        string sSql;
        string sFechaDesde;
        string sFechaHasta;
        string sSecuencial;
        string sNumeroFactura;
        string sTexto;
        string sIdentificacion;
        string sCliente;
        string sCiudad;
        string sDireccion;
        string sTelefono;
        string sCelular;
        string sMail;
        //string sFechaCompleta;
        string sFechaCorta;
        string sTabla;
        string sCampo;

        Double dSubtotal;
        Double dSubtotalSinImpuestos;
        Double dIva;
        Double dServicio;
        Double dDescuento;
        Double dValorItem;
        Double dTotal;
        Double dPorcentajeIva;
        Double dPorcentajeDescuento;

        bool bRespuesta;

        int iIdPersona;
        int iIdFactura;
        int iIdLocalidadImpresora;
        int iIdTipoComprobante = 1;

        long iMaximo;

        public frmRepartidorExterno()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR EL COMBO DE LAS CATEGORIAS
        private void llenarComboRepartidores()
        {
            try
            {                
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where repartidor_externo = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbRepartidoresExternos.llenar(dtConsulta, sSql);

                if (cmbRepartidoresExternos.Items.Count > 0)
                    cmbRepartidoresExternos.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboRepartidores();
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            txtDatos.Clear();
            btnFacturar.Enabled = false;
            cmbRepartidoresExternos.Focus();
        }

        //FUNCION PARA LLENAR EL GRID CON LOS DATOS DEL MENU EXPRESS
        private void listarRepartidor()
        {
            try
            {
                dSubtotal = 0;
                dIva = 0;
                dServicio = 0;
                dDescuento = 0;

                sSql = "";
                sSql += "select CP.id_pedido, CP.fecha_apertura_orden, NCP.numero_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NCP," + Environment.NewLine;
                sSql += "cv403_dctos_por_cobrar XC" + Environment.NewLine;
                sSql += "where NCP.id_pedido = CP.id_pedido " + Environment.NewLine;
                sSql += "and XC.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_origen_orden = " + Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) + Environment.NewLine;
                sSql += "and XC.id_factura is null" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NCP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.fecha_orden between '" + sFechaDesde + "'" + Environment.NewLine;
                sSql += "and '" + sFechaHasta + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existen órdenes para " + cmbRepartidoresExternos.Text + " en el rango de fechas seleccionado.";
                        ok.ShowDialog();
                        btnFacturar.Enabled = false;
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                dtCopiaConsulta = new DataTable();
                dtCopiaConsulta = dtConsulta;

                //LLENAR ENCABEZADO PARA LA FACTURA
                sTexto = "";
                sTexto = sTexto + Environment.NewLine + Environment.NewLine;
                //sTexto = sTexto + "".PadLeft(6, ' ') + "FACTURA N° " + sNumeroFactura + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "FECHA     : " + DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine;
                sTexto = sTexto + "CI/RUC    : " +sIdentificacion + Environment.NewLine;
                sTexto = sTexto + "CLIENTE   : " + sCliente + Environment.NewLine;

                //TELEFONO
                if (sTelefono != "")
                {
                    sTexto = sTexto + "TELEFONO  : " + sTelefono + Environment.NewLine;
                }

                else if (sCelular != "")
                {
                    sTexto = sTexto + "CELULAR   : " + sCelular + Environment.NewLine;
                }

                //DIRECCION
                sTexto = sTexto + "DIRECCIÓN : ";

                if (sDireccion.Length <= 28)
                {
                    sTexto = sTexto + sDireccion + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + caracteres.saltoLinea(sDireccion, 12) + Environment.NewLine;
                }


                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                sTexto = sTexto + "CANT.".PadRight(7, ' ') + "DESCRIPCIÓN".PadRight(16, ' ') + "V. UNIT.".PadRight(10, ' ') + "V. TOT." + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;

                ////LLENAR EL GRID PARA MOSTRAR LAS ORDENES DE MENU EXPRESS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    //sSql = "";
                    //sSql += "select ltrim(str(sum(DP.cantidad * DP.precio_unitario), 10, 2)) subtotal, " + Environment.NewLine;
                    //sSql += "ltrim(str(sum(DP.cantidad * DP.valor_iva), 10, 2)) iva," + Environment.NewLine;
                    //sSql += "ltrim(str(sum(DP.cantidad * DP.valor_otro), 10, 2)) servicio," + Environment.NewLine;
                    //sSql += "ltrim(str(sum(DP.cantidad * DP.valor_dscto), 10, 2)) descuento, CP.porcentaje_iva" + Environment.NewLine;
                    //sSql += "from cv403_det_pedidos DP, cv403_cab_pedidos CP" + Environment.NewLine;
                    //sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                    //sSql += "and DP.id_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString()) + Environment.NewLine;
                    //sSql += "and CP.estado = 'A'" + Environment.NewLine;
                    //sSql += "and DP.estado = 'A'" + Environment.NewLine;
                    //sSql += "group by CP.porcentaje_iva";

                    sSql = "";
                    sSql += "select sum(DP.cantidad * DP.precio_unitario) subtotal, " + Environment.NewLine;
                    sSql += "sum(DP.cantidad * DP.valor_iva) iva," + Environment.NewLine;
                    sSql += "sum(DP.cantidad * DP.valor_otro) servicio," + Environment.NewLine;
                    sSql += "sum(DP.cantidad * DP.valor_dscto) descuento, CP.porcentaje_iva" + Environment.NewLine;
                    sSql += "from cv403_det_pedidos DP, cv403_cab_pedidos CP" + Environment.NewLine;
                    sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                    sSql += "and DP.id_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString()) + Environment.NewLine;
                    sSql += "and CP.estado = 'A'" + Environment.NewLine;
                    sSql += "and DP.estado = 'A'" + Environment.NewLine;
                    sSql += "group by CP.porcentaje_iva";

                    dtDatos = new DataTable();
                    dtDatos.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDatos, sSql);

                    if (bRespuesta == true)
                    {
                        dValorItem = Convert.ToDouble(dtDatos.Rows[0].ItemArray[0].ToString()) + Convert.ToDouble(dtDatos.Rows[0].ItemArray[1].ToString()) + Convert.ToDouble(dtDatos.Rows[0].ItemArray[2].ToString()) - Convert.ToDouble(dtDatos.Rows[0].ItemArray[3].ToString());
                        sTexto = sTexto + "  1".PadRight(7, ' ') + ("ORDEN N° " + dtConsulta.Rows[i].ItemArray[2].ToString()).PadRight(16, ' ') + dValorItem.ToString("N2").PadLeft(8, ' ') + dValorItem.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(7, ' ') + dtConsulta.Rows[i].ItemArray[1].ToString().Substring(0, 10) + Environment.NewLine + Environment.NewLine;

                        dSubtotal = dSubtotal + dValorItem;
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                dPorcentajeIva = Convert.ToDouble(dtDatos.Rows[0].ItemArray[4].ToString());
                dDescuento = dSubtotal * (dPorcentajeDescuento / 100);
                dSubtotalSinImpuestos = dSubtotal - dDescuento;
                dIva = dSubtotalSinImpuestos * (dPorcentajeIva / 100);
                dTotal = dSubtotalSinImpuestos + dIva;

                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ("SUBTOTAL " + dPorcentajeIva.ToString() + "%").PadRight(25, ' ') + ":" + dSubtotal.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ("DESCUENTO " + dPorcentajeDescuento.ToString() + "%:").PadRight(25, ' ') + ":" + dDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + "SUBTOTAL SIN IMPUESTOS:".PadRight(25, ' ') + ":" + dSubtotalSinImpuestos.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ("IVA " + dPorcentajeIva.ToString() + "%").PadRight(25, ' ') + ":" + dIva.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                //sTexto = sTexto + "".PadLeft(5, ' ') + ((Program.recargo * 100).ToString() + "% SERVICIO").PadRight(25, ' ') + ":" + dServicio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + "TOTAL A PAGAR".PadRight(25, ' ') + ":" + dTotal.ToString("N2").PadLeft(9, ' ');
                sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                txtDatos.Text = sTexto;
                btnFacturar.Enabled = true;
                goto fin;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin:
            { }
        }


        //FUNCION PARA EXTRAER LOS DATOS DEL REPARTIDOR EXTERNO
        private void consultarRegistro()
        {
            try
            {
                txtDatos.Clear();

                if (Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) == 0)
                {
                    iIdPersona = 0;
                    sCliente = "";
                    sIdentificacion = "";
                    sMail = "";
                    sDireccion = "";
                    sTelefono = "";
                    sCelular = "";
                }

                else
                {
                    sSql = "";
                    sSql += "select O.id_persona, TP.apellidos + ' ' + isnull(TP.nombres, '') cliente," + Environment.NewLine;
                    sSql += "TP.identificacion, isnull(TP.correo_electronico, '') correo_electronico," + Environment.NewLine;
                    sSql += "TD.direccion + ' ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + calle_interseccion as direccion," + Environment.NewLine;
                    sSql += "isnull(TT.domicilio, TT.oficina) telefono," + Environment.NewLine;
                    sSql += "isnull(TT.celular, '') celular, O.porcentaje_descuento_externo" + Environment.NewLine;
                    sSql += "from pos_origen_orden O, tp_personas TP," + Environment.NewLine;
                    sSql += "tp_direcciones TD, tp_telefonos TT" + Environment.NewLine;
                    sSql += "where O.id_persona = TP.id_persona" + Environment.NewLine;
                    sSql += "and TD.id_persona = TP.id_persona" + Environment.NewLine;
                    sSql += "and TT.id_persona = TP.id_persona" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "and TP.estado = 'A'" + Environment.NewLine;
                    sSql += "and TD.estado = 'A'" + Environment.NewLine;
                    sSql += "and TT.estado = 'A'" + Environment.NewLine;
                    sSql += "and O.id_pos_origen_orden  = " + Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) + Environment.NewLine;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            sCliente = dtConsulta.Rows[0].ItemArray[1].ToString();
                            sIdentificacion = dtConsulta.Rows[0].ItemArray[2].ToString();
                            sMail = dtConsulta.Rows[0].ItemArray[3].ToString();
                            sDireccion = dtConsulta.Rows[0].ItemArray[4].ToString();
                            sTelefono = dtConsulta.Rows[0].ItemArray[5].ToString();
                            sCelular = dtConsulta.Rows[0].ItemArray[6].ToString();
                            dPorcentajeDescuento = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[7].ToString());
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Favor verifique los datos de " + cmbRepartidoresExternos.Text.ToUpper();
                            ok.ShowDialog();
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }
                }                
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            fin: { }
        }

        #endregion


        #region FUNCIONES PARA PROCEDER A FACTURAR

        private void insertarFactura()
        {
            try
            {
                //sFechaCompleta = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INICIAMOS UNA NUEVA TRANSACCION
                //-------------------------------------------------------------------------------------------------------------------------------------------------
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                //EXTRAER EL NUMERO DE FACTURA
                sSql = "";
                sSql += "select numero_factura, id_localidad_impresora" + Environment.NewLine;
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
                        sNumeroFactura = dtConsulta.Rows[0].ItemArray[0].ToString();
                        iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se puede extraer el número de factura";
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

                //INSERTAR EN LA TABLA CV403_FACTURAS
                //-------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, fecha_factura, fecha_vcto," + Environment.NewLine;
                sSql += "cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura, Ciudad_Factura, correo_electronico, servicio)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += iIdTipoComprobante + "," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", 14, '" + sFechaCorta + "'," + Environment.NewLine;
                sSql += "'" + sFechaCorta + "', " + Program.iMoneda + ", " + dTotal + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                sSql += "'" + sDireccion + "', '" + sTelefono + "', '" + sCiudad + "'," + Environment.NewLine;
                sSql += "'" + sMail + "', 0)";

                //EJECUTA LA INSTRUCCÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EXTRAER ID DEL REGISTRO CV403_FACTURAS
                //-------------------------------------------------------------------------------------------------------------------------------------------------
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
                //-------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura, " + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, " + Environment.NewLine;
                sSql += "numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + Convert.ToInt32(sNumeroFactura) + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                //EJECUTA LA INSTRUCCÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAMOS LA TABLA CV403_DCTOS_POR_COBRAR Y CV403_FACTURAS_PEDIDOS
                //-------------------------------------------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < dtCopiaConsulta.Rows.Count; i++)
                {
                    //ACTUALIZAMOS LA TABLA CV403_DCTOS_POR_COBRAR
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                    sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                    sSql += "numero_documento = " + Convert.ToInt32(sNumeroFactura) + Environment.NewLine;
                    sSql += " where id_pedido = " + Convert.ToInt32(dtCopiaConsulta.Rows[i].ItemArray[0].ToString());

                    //EJECUTA LA INSTRUCCÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }


                    //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                    sSql += "id_factura, id_pedido, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdFactura + ", " + Convert.ToInt32(dtCopiaConsulta.Rows[i].ItemArray[0].ToString()) + "," + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                    //EJECUTA LA INSTRUCCÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ACTUALIZAR EL ESTADO A PAGADA Y AGREGAMOS LA FECHA DE CIERRE DE ORDENEN CV403_CAB_PEDIDOS
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                    sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                    sSql += "fecha_cierre_orden = GETDATE()," + Environment.NewLine;
                    sSql += "consumo_alimentos = 1" + Environment.NewLine;
                    sSql += "where id_pedido = " + Convert.ToInt32(dtCopiaConsulta.Rows[i].ItemArray[0].ToString());

                    //EJECUCIÓN DE LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }
                //------------------- FIN DE CICLO FOR -------------------------------------------------------

                //ACTUALIZAMOS EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                //-------------------------------------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_factura = " + (Convert.ToInt32(sNumeroFactura) + 1) + Environment.NewLine;
                sSql += "where id_localidad_impresora = " + iIdLocalidadImpresora + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTA LA INSTRUCCÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //EJECUCIÓN DEL COMMIT PARA GUARDAR LA INFORMACION EN LA BASE DE DATOS
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.LblMensaje.Text = "Factura generada éxitosamente.";
                ok.ShowDialog();
                limpiar();
                goto fin;                
            }

            catch(Exception ex)
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

        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(TxtHasta.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                TxtHasta.Text = calendario.txtFecha.Text;
                sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }

        private void frmRepartidorExterno_Load(object sender, EventArgs e)
        {
            cmbRepartidoresExternos.SelectedIndexChanged -= new EventHandler(cmbRepartidoresExternos_SelectedIndexChanged);
            limpiar();
            cmbRepartidoresExternos.SelectedIndexChanged += new EventHandler(cmbRepartidoresExternos_SelectedIndexChanged);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtDesde.Text.Trim()) > Convert.ToDateTime(TxtHasta.Text.Trim()))
            {
                ok.LblMensaje.Text = "El rango de fechas no está definido correctamente.";
                ok.ShowDialog();
            }

            else if (Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione un registro para realizar la consulta.";
                ok.ShowDialog();
                cmbRepartidoresExternos.Focus();
            }

            else
            {
                listarRepartidor();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void cmbRepartidoresExternos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) == 0)
            {
                btnFacturar.Enabled = false;
            }

            else
            {
                consultarRegistro();
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            SiNo.LblMensaje.Text = "¿Está seguro que desea generar la factura para " + cmbRepartidoresExternos.Text.ToString() + "?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                insertarFactura();
            }
        }
    }
}
