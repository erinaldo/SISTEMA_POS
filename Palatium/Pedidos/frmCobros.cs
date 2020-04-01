using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmCobros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        ValidarCedula validarCedula = new ValidarCedula();
        
        Button[,] boton = new Button[5, 2];

        SqlParameter[] parametro;

        int iIdCaja;
        int iCgEstadoDctoPorCobrar = 7461;
        int iIdTipoEmision = 0;
        int iIdTipoAmbiente = 0;
        int iGeneraFactura;

        ComandaNueva.frmComanda ord;
        Button bpagar;

        string sSql;
        string sCiudad;
        string sNumeroFactura;
        string sIdOrden;
        //string sFechaCorta;
        string sTabla;
        string sCampo;
        string sFecha;
        string sFacturaRecuperada;
        string sMovimiento;
        string sSecuencial;
        string sNumeroOrden;
        string sEstablecimiento;
        string sPuntoEmision;
        string sClaveAcceso;
        string sCorreoElectronicoCF;
        string sCorreoAyuda;
        string sNumeroLote;
        string sLoteRecuperado;

        long iMaximo;

        DataTable dtConsulta;
        DataTable dtFormasPago;
        DataTable dtComanda;
        DataTable dtAuxiliar;
        DataTable dtTarjetasT;
        DataTable dtAgrupado;
        DataTable dtAlmacenar;
        DataTable dtOriginal;
        
        bool bRespuesta;
        int iCuentaFormasPagos;
        int iCuentaAyudaFormasPagos;
        int iPosXFormasPagos;
        int iPosYFormasPagos;
        int iIdLocalidadImpresora;
        int iNumeroMovimientoCaja;
        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iBanderaDomicilio;
        int iTercerDigito;
        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPago;
        int iIdDocumentoPagado;
        int iCgTipoDocumento;
        int iIdDocumentoPago;
        int iNumeroPago;
        int iEjecutarActualizacionIVA;
        int iEjecutarActualizacionTarjetas;
        int iIdPosMovimientoCaja;
        int iOpCambiarEstadoOrden;
        int iNumeroMovimiento;
        int iIdTipoComprobante;
        int iCerrarCuenta;
        int iNumeroPedido;
        int iIdFactura;
        int iManejaFE;
        int iBanderaGeneraFactura;
        int iIdListaMinorista_P;
        int iBanderaRemoverIvaBDD;
        int iBanderaRecargoBDD;
        int iBanderaRemoverIvaBoton;
        int iBanderaRecargoBoton;
        int iNumeroPedido_P;
        int iNumeroCuenta_P;
         int iIdFormaPago_1;
         int iIdFormaPago_2;
         int iIdFormaPago_3;
         int iConciliacion;
         int iBanderaInsertarLote;
         int iOperadorTarjeta;
         int iTipoTarjeta;
         int iBanderaComandaPendiente;

         Decimal dTotal;
         Decimal dSubtotal;
         Decimal dValor;
         Decimal dbSumaIva;
         Decimal dbRecalcularPrecioUnitario;
         Decimal dbRecalcularDescuento;
         Decimal dbRecalcularIva;
         Decimal dbValorGrid;
         Decimal dbValorRecuperado;
         Decimal dbPropina;
         Decimal dbCambio;
         Decimal dbIVAPorcentaje;
         Decimal dServicio;
         Decimal dbTotalAyuda;
         Decimal dbSubTotalRecargo;
         Decimal dbValorRecargo;
         Decimal dbPorcentajeRecargo;
         Decimal dbSubTotalNetoRecargo;
         Decimal dbIVARecargo;
         Decimal dbTotalRecargo;
         Decimal dbValorPropina;
         Decimal dbPropinaRecibidaFormaPago;

         Decimal dbSubtotalParaRetencion;
         Decimal dbSumaIvaParaRetencion;

         public frmCobros(string sIdOrden_P, int iBanderaComandaPendiente_P)
         {
            this.sIdOrden = sIdOrden_P;
            this.iBanderaComandaPendiente = iBanderaComandaPendiente_P;
            InitializeComponent();
         }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA EXTRAER LA FECHA DEL SISTEMA
        private bool extraerFecha()
        {
            try
            {
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

                 sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
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

         //FUNCION PARA EXTRAER LA LISTA MINORISTA
         private void extraerListaMinorista()
         {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_minorista = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"].ToString()) > 0)
                    {
                        iIdListaMinorista_P = Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"].ToString());
                    }

                    else
                    {
                        iIdListaMinorista_P = 4;
                    }

                    sSql = "";
                    sSql += "select DP.id_det_pedido, DP.id_producto, DP.precio_unitario, DP.valor_dscto," + Environment.NewLine;
                    sSql += "DP.valor_iva, DP.valor_otro, P.paga_iva, PP.valor, CP.porcentaje_iva" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado = 'A'" + Environment.NewLine;
                    sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                    sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                    sSql += "and PP.estado = 'A'" + Environment.NewLine;
                    sSql += "where CP.id_pedido = " + sIdOrden + Environment.NewLine;
                    sSql += "and PP.id_lista_precio = " + iIdListaMinorista_P;

                    dtOriginal = new DataTable();
                    dtOriginal.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtOriginal, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }
                 
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL IVA
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

                         if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                         {
                             catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
                 return false;
             }
         }

        //FUNCION PARA ACTUALIZAR EL IVA A CERO
         private bool actualizarIVACero()
         {
             try
             {
                 if (actualizarPreciosOriginales() == false)
                 {
                     return false;
                 }

                 sSql = "";
                 sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                 sSql += "valor_iva = 0" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                 sSql += "and estado = 'A'";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
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

        //FUNCION PARA ACTUALIZAR EL RECARGO DE TARJETAS
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
                         
                         if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                         {
                             catchMensaje = new VentanasMensajes.frmMensajeCatch();
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
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
                 return false;
             }
         }

        //FUNCION PARA REMOVER EL RECARGO TARJETAS
         private bool actualizarRemoverRecargoTarjetas()
         {
             try
             {
                 for (int i = 0; i < dtTarjetasT.Rows.Count; ++i)
                 {
                     sSql = "";
                     sSql += "select valor from cv403_precios_productos" + Environment.NewLine;
                     sSql += "where id_producto = " + dtTarjetasT.Rows[i]["id_producto"].ToString() + Environment.NewLine;
                     sSql += "and estado = 'A'" + Environment.NewLine;
                     sSql += "and id_lista_precio = " + iIdListaMinorista_P;

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

                     Decimal dbValor_R = Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString());
                     Decimal dbValorIVA_R;

                     if (Convert.ToInt32(dtTarjetasT.Rows[i]["paga_iva"].ToString()) == 0)
                     {
                         dbValorIVA_R = 0;
                     }

                     else
                     {
                         dbValorIVA_R = dbValor_R * Convert.ToDecimal(Program.iva);
                     }
	
                     sSql = "";
                     sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                     sSql += "precio_unitario = " + dbValor_R + "," + Environment.NewLine;
                     sSql += "valor_iva = " + dbValorIVA_R + Environment.NewLine;
                     sSql += "where id_det_pedido = " + Convert.ToInt32(dtTarjetasT.Rows[i]["id_det_pedido"].ToString());

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
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

        //FUNCION PARA APLICAR EL RECARGO DE TARJETA
         private bool aplicaRecargoTarjetas()
         {
             try
             {
                 dtTarjetasT = new DataTable();
                 dtTarjetasT.Clear();
                 dtTarjetasT = dtComanda.Copy();

                 for (int i = 0; i < dtTarjetasT.Rows.Count; i++)
                 {
                     Decimal dbPrecioUnitario_R = Convert.ToDecimal(dtTarjetasT.Rows[i]["precio_unitario"].ToString());
                     Decimal dbValorRecargo_R = dbPrecioUnitario_R * Convert.ToDecimal(Program.dbPorcentajeRecargoTarjeta);
                     Decimal dbTotal_R = dbPrecioUnitario_R + dbValorRecargo_R;
                     Decimal dbValorIVA_R;

                     if (Convert.ToInt32(dtTarjetasT.Rows[i]["paga_iva"].ToString()) == 0)
                     {
                         dbValorIVA_R = 0;
                     }

                     else
                     {
                         dbValorIVA_R = dbTotal_R * Convert.ToDecimal(Program.iva);
                     }

                     dtTarjetasT.Rows[i]["precio_unitario"] = dbTotal_R.ToString();
                     dtTarjetasT.Rows[i]["valor_iva"] = dbValorIVA_R.ToString();
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

        //FUNCION PARA OBTENER EL TOTAL
         private void obtenerTotal()
         {
             try
             {
                 sSql = "";
                 sSql += "select ltrim(str(sum(cantidad * (precio_unitario + valor_iva + valor_otro - valor_dscto)), 10, 2)) total," + Environment.NewLine;
                 sSql += "ltrim(str(sum(cantidad * (precio_unitario - valor_dscto)), 10, 2)) subtotal," + Environment.NewLine;
                 sSql += "ltrim(str(sum(cantidad * valor_iva), 10, 2)) iva" + Environment.NewLine;
                 sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden;

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == true)
                 {
                     dTotal = Convert.ToDecimal(dtConsulta.Rows[0]["total"].ToString());
                     dbSubtotalParaRetencion = Convert.ToDecimal(dtConsulta.Rows[0]["subtotal"].ToString());
                     dbSumaIvaParaRetencion = Convert.ToDecimal(dtConsulta.Rows[0]["iva"].ToString());
                     lblTotal.Text = "$ " + dTotal.ToString("N2");
                     dbTotalAyuda = Convert.ToDecimal(dtConsulta.Rows[0]["total"].ToString());
                 }

                 else
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CARGAR LA INFORMACION DEL CLIENTE
         private void cargarInformacionCliente()
         {
             try
             {
                 sSql = "";
                 sSql += "select * from pos_vw_cargar_informacion_cliente" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden;

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == true)
                 {
                     iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                     iNumeroCuenta_P = Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString());
                     iNumeroPedido_P = Convert.ToInt32(dtConsulta.Rows[0]["numero_pedido"].ToString());

                     if (iIdPersona == Program.iIdPersona)
                     {
                         txtIdentificacion.Text = "9999999999999";
                         txtApellidos.Text = "CONSUMIDOR FINAL";
                         txtNombres.Text = "CONSUMIDOR FINAL";
                         txtTelefono.Text = "9999999999";
                         txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString();
                         txtDireccion.Text = "QUITO";
                         iIdPersona = Program.iIdPersona;
                         idTipoIdentificacion = 180;
                         idTipoPersona = 2447;
                         btnEditar.Visible = false;
                     }

                     else
                     {
                         txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString();
                         txtNombres.Text = dtConsulta.Rows[0]["nombres"].ToString();
                         txtApellidos.Text = dtConsulta.Rows[0]["apellidos"].ToString();
                         txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString();
                         txtDireccion.Text = dtConsulta.Rows[0]["direccion_completa"].ToString();
                         sCiudad = dtConsulta.Rows[0]["direccion"].ToString();

                         if (dtConsulta.Rows[0]["telefono_domicilio"].ToString() != "")
                         {
                             txtTelefono.Text = dtConsulta.Rows[0]["telefono_domicilio"].ToString();
                         }

                         else if (dtConsulta.Rows[0]["celular"].ToString() != "")
                         {
                             txtTelefono.Text = dtConsulta.Rows[0]["celular"].ToString();
                         }

                         else
                         {
                             txtTelefono.Text = "";
                         }
                     }
                 }

                 else
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CARGAR TODAS LAS FORMAS DE PAGO
         private void cargarFormasPagos()
         {
             try
             {
                 sSql = "";
                 sSql += "select FC.id_pos_tipo_forma_cobro, MP.codigo, FC.descripcion," + Environment.NewLine;
                 sSql += "isnull(FC.imagen, '') imagen, MP.id_sri_forma_pago," + Environment.NewLine;
                 sSql += "isnull(FC.aplica_retencion, 0) aplica_retencion" + Environment.NewLine;
                 sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                 sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                 sSql += "and FC.estado = 'A'" + Environment.NewLine;
                 sSql += "and MP.estado = 'A'";

                 dtFormasPago = new DataTable();
                 dtFormasPago.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtFormasPago, sSql);

                 if (!bRespuesta)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }

                 else
                 {
                     iCuentaFormasPagos = 0;

                     if (dtFormasPago.Rows.Count > 0)
                     {
                         if (dtFormasPago.Rows.Count > 8)
                         {
                             btnSiguiente.Enabled = true;
                         }

                         else
                         {
                             btnSiguiente.Enabled = false;
                         }

                         if (crearBotonesFormasPagos() == true)
                         { }

                     }

                     else
                     {
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                         ok.ShowDialog();
                     }
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

         //FUNCION PARA CARGAR FORMAS DE PAGO CON RECARGO
         private void cargarFormasPagosRecargo()
         {
             try
             {
                 sSql = "";
                 sSql += "select FC.id_pos_tipo_forma_cobro, MP.codigo, FC.descripcion," + Environment.NewLine;
                 sSql += "isnull(FC.imagen, '') imagen, MP.id_sri_forma_pago," + Environment.NewLine;
                 sSql += "isnull(FC.aplica_retencion, 0) aplica_retencion" + Environment.NewLine;
                 sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                 sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                 sSql += "and FC.estado = 'A'" + Environment.NewLine;
                 sSql += "and MP.estado = 'A'" + Environment.NewLine;
                 sSql += "where MP.codigo in ('TC', 'TD')";

                 dtFormasPago = new DataTable();
                 dtFormasPago.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtFormasPago, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return;
                 }

                 iCuentaFormasPagos = 0;

                 if (dtFormasPago.Rows.Count > 0)
                 {
                     if (dtFormasPago.Rows.Count > 8)
                     {
                         btnSiguiente.Enabled = true;
                     }

                     else
                     {
                         btnSiguiente.Enabled = false;
                     }

                     if (crearBotonesFormasPagos() == true)
                     { }

                 }

                 else
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se encuentras ítems de categorías en el sistema.";
                     ok.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CREAR LOS BOTONS DE TODAS LAS FORMAS DE PAGO
         private bool crearBotonesFormasPagos()
         {
             try
             {
                 pnlFormasCobros.Controls.Clear();
                 iPosXFormasPagos = 0;
                 iPosYFormasPagos = 0;
                 iCuentaAyudaFormasPagos = 0;

                 for (int i = 0; i < 5; ++i)
                 {
                     for (int j = 0; j < 2; ++j)
                     {
                         boton[i, j] = new Button();
                         boton[i, j].Cursor = Cursors.Hand;
                         boton[i, j].Click += new EventHandler(boton_clic);
                         boton[i, j].Size = new Size(153, 71);
                         boton[i, j].Location = new Point(iPosXFormasPagos, iPosYFormasPagos);
                         boton[i, j].BackColor = Color.Lime;
                         boton[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Bold);
                         //boton[i, j].Tag = dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                         boton[i, j].Name = dtFormasPago.Rows[iCuentaFormasPagos]["id_pos_tipo_forma_cobro"].ToString();
                         boton[i, j].Tag = dtFormasPago.Rows[iCuentaFormasPagos]["aplica_retencion"].ToString();
                         boton[i, j].Text = dtFormasPago.Rows[iCuentaFormasPagos]["descripcion"].ToString();
                         boton[i, j].AccessibleDescription = dtFormasPago.Rows[iCuentaFormasPagos]["id_sri_forma_pago"].ToString();
                         boton[i, j].AccessibleName = dtFormasPago.Rows[iCuentaFormasPagos]["codigo"].ToString();
                         boton[i, j].TextAlign = ContentAlignment.MiddleCenter;
                         boton[i, j].FlatStyle = FlatStyle.Flat;
                         boton[i, j].FlatAppearance.BorderSize = 1;
                         boton[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);

                         if (dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim() != "" && File.Exists(dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim()))
                         {
                             boton[i, j].TextAlign = ContentAlignment.MiddleRight;
                             boton[i, j].Image = Image.FromFile(dtFormasPago.Rows[iCuentaFormasPagos]["imagen"].ToString().Trim());
                             boton[i, j].ImageAlign = ContentAlignment.MiddleLeft;
                             boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                         }

                         pnlFormasCobros.Controls.Add(boton[i, j]);
                         ++iCuentaFormasPagos;
                         ++iCuentaAyudaFormasPagos;

                         if (j + 1 == 2)
                         {
                             iPosXFormasPagos = 0;
                             iPosYFormasPagos += 71;
                         }

                         else
                         {
                             iPosXFormasPagos += 153;
                         }

                         if (dtFormasPago.Rows.Count == iCuentaFormasPagos)
                         {
                             btnSiguiente.Enabled = false;
                             break;
                         }
                     }

                     if (dtFormasPago.Rows.Count == iCuentaFormasPagos)
                     {
                         btnSiguiente.Enabled = false;
                         break;
                     }
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

        //EVENTO CLIC DE LAS FORMAS DE PAGO
         public void boton_clic(object sender, EventArgs e)
         {
             bpagar = sender as Button;

             if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0.0)
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "El saldo actual a pagar ya es de 0.00";
                 ok.ShowDialog();
             }

             else
             {
                 //if (Convert.ToInt32(bpagar.Tag) == 1)
                 //{

                 //}

                 //else
                 //{
                     //Efectivo efectivo = new Efectivo(bpagar.Tag.ToString(), dgvDetalleDeuda.Rows[1].Cells[1].Value.ToString(), "", bpagar.Text.ToString(), bpagar.AccessibleName.Trim());
                     Efectivo efectivo = new Efectivo(bpagar.Name.ToString(), dgvDetalleDeuda.Rows[1].Cells[1].Value.ToString(), "", bpagar.Text.ToString(), bpagar.AccessibleName.Trim());
                     efectivo.ShowDialog();

                     if (efectivo.DialogResult == DialogResult.OK)
                     {
                         dbValorGrid = efectivo.dbValorGrid;
                         dbValorRecuperado = efectivo.dbValorIngresado;
                         dbPropina = efectivo.dbValorPropina;
                         sNumeroLote = efectivo.sNumeroLote;
                         iConciliacion = efectivo.iConciliacion;
                         iOperadorTarjeta = efectivo.iOperadorTarjeta;
                         iTipoTarjeta = efectivo.iTipoTarjeta;
                         iBanderaInsertarLote = efectivo.iBanderaInsertarLote;

                         dgvPagos.Rows.Add(efectivo.sIdPago, efectivo.sNombrePago, dbValorGrid.ToString("N2"),
                                           bpagar.AccessibleDescription, iConciliacion.ToString(), iOperadorTarjeta.ToString(),
                                           iTipoTarjeta.ToString(), sNumeroLote, iBanderaInsertarLote.ToString(),
                                           dbPropina.ToString("N2"));

                         dgvDetalleDeuda.Rows[3].Cells[1].Value = dbPropina.ToString("N2");
                         dgvPagos.ClearSelection();
                         efectivo.Close();
                         recalcularValores();
                     }
                 //}                 
             }
         }

        //FUNCION PARA CONSULTAR LOS VALORES DE LA RETENCION
        private bool consultarValoresRetencion(int iIdPosTipoFormaCobro_P, string sDescripcionPago_P, int iIdSriFormaPago_P)
         {
            try
            {
                int iBandera_P = 0;

                //RECORRO PRIMERO EL GRID PARA VERIFICAR SI NO SE ENCUNETRA YA REGISTRADO LA RETENCION
                for (int i = 0; i < dgvPagos.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgvPagos.Rows[i].Cells["ID"].Value) == iIdPosTipoFormaCobro_P)
                    {
                        iBandera_P = 1;
                        break;
                    }
                }

                if (iBandera_P == 1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ya se encuentra ingresado un registro con el porcentaje de retención que ha seleccionado.";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "select codigo_retencion, porcentaje_retencion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_tipo_forma_cobro = " + iIdPosTipoFormaCobro_P + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }                

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentran registros de la retención. Favor comuníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return false;
                }

                Decimal dbPorcentajeRetencion_P;

                if (dtConsulta.Rows[0]["codigo_retencion"].ToString() == "RENTA")
                {
                    dbPorcentajeRetencion_P = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_fuente"].ToString());

                }

                else if (dtConsulta.Rows[0]["porcentaje_retencion"].ToString() == "IVA")
                {
                    dbPorcentajeRetencion_P = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_iva"].ToString());
                }

                dgvPagos.Rows.Add(iIdPosTipoFormaCobro_P, sDescripcionPago_P, dbValorGrid.ToString("N2"),
                                  iIdSriFormaPago_P.ToString(), 0, 0, 0, "", 0);

                dgvDetalleDeuda.Rows[3].Cells[1].Value = dbPropina.ToString("N2");
                dgvPagos.ClearSelection();
                //efectivo.Close();
                recalcularValores();

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

        //FUNCION PARA RECALCULAR LOS VALORES
         private void recalcularValores()
         {
             try
             {
                 dgvDetalleDeuda.Rows[0].Cells[1].Value = (Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value) + dbValorRecuperado).ToString("N2");
                 dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value)).ToString("N2");

                 if (Convert.ToDecimal(dgvDetalleDeuda.Rows[1].Cells[1].Value) < 0)
                 {
                     dgvDetalleDeuda.Rows[1].Cells[1].Value = "0.00";
                 }

                 dgvDetalleDeuda.Rows[2].Cells[1].Value = (Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value) - dTotal).ToString("N2");

                 if (Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) < 0)
                 {
                     dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                 }

                 if (Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) <= 0)
                 {
                     return;
                 }

                 Program.dCambioPantalla = Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value);
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA EXTRAER LA INFORMACION PARA FACTURAR
         private void datosFactura()
         {
             try
             {
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
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "No se encuentran registros en la consulta.";
                         ok.ShowDialog();
                     }

                     else
                     {
                         txtfacturacion.Text = dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString();

                         sEstablecimiento = dtConsulta.Rows[0]["establecimiento"].ToString();
                         sPuntoEmision = dtConsulta.Rows[0]["punto_emision"].ToString();

                         if (rdbFactura.Checked)
                         {
                             TxtNumeroFactura.Text = dtConsulta.Rows[0]["numero_factura"].ToString();
                         }

                         else if (rdbNotaVenta.Checked)
                         {
                             TxtNumeroFactura.Text = dtConsulta.Rows[0]["numeronotaentrega"].ToString();
                         }

                         iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0]["numeromovimientocaja"].ToString());
                         iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad_impresora"].ToString());
                     }
                 }

                 else
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE ISNTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CONSULTAR DATOS DEL CLIENTE
         private void consultarRegistro()
         {
             try
             {
                 sSql = "";
                 sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                 sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
                 sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                 sSql += "FROM tp_personas TP" + Environment.NewLine;
                 sSql += "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                 sSql += "and TP.estado = 'A'" + Environment.NewLine;
                 sSql += "and TD.estado = 'A'" + Environment.NewLine;
                 sSql += "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                 sSql += "and TT.estado = 'A'" + Environment.NewLine;
                 sSql += "WHERE TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta)
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

                         btnFacturar.Focus();
                     }

                     else
                     {
                         Facturador.frmNuevoCliente frmNuevoCliente = new Facturador.frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
                         frmNuevoCliente.ShowDialog();

                         if (frmNuevoCliente.DialogResult == DialogResult.OK)
                         {
                             iIdPersona = frmNuevoCliente.iCodigo;
                             txtIdentificacion.Text = frmNuevoCliente.sIdentificacion;
                             txtNombres.Text = frmNuevoCliente.sNombre;
                             txtApellidos.Text = frmNuevoCliente.sApellido;
                             txtTelefono.Text = frmNuevoCliente.sTelefono;
                             txtDireccion.Text = frmNuevoCliente.sDireccion;
                             txtMail.Text = frmNuevoCliente.sMail;
                             sCiudad = frmNuevoCliente.sCiudad;
                             frmNuevoCliente.Close();
                             btnFacturar.Focus();
                         }
                     }

                     btnEditar.Visible = true;
                     goto continua;
                 }
             }
             catch (Exception ex)
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                 ok.ShowDialog();
                 txtIdentificacion.Clear();
                 txtIdentificacion.Focus();
             }            

         continua:
             iBanderaDomicilio = 1;
         }

        //FUNCION MENSAJE DE VALIDACION DE CEDULA
         private void mensajeValidarCedula()
         {
             ok = new VentanasMensajes.frmMensajeOK();
             ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
             ok.ShowDialog();
             txtIdentificacion.Clear();
             txtApellidos.Clear();
             txtNombres.Clear();
             txtDireccion.Clear();
             txtTelefono.Clear();
             txtMail.Clear();
             txtIdentificacion.Focus();
         }

        //FUNCION PARA VALIDAR LA IDENTIFICACION
         private void validarIdentificacion()
         {
             try
             {
                 if (txtIdentificacion.Text.Length >= 10)
                 {
                     iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));

                     if (txtIdentificacion.Text.Length == 10)
                     {
                         if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                         {
                             consultarRegistro();
                             return;
                         }

                         else
                         {
                             mensajeValidarCedula();
                             return;
                         }
                     }

                     else if (txtIdentificacion.Text.Length == 13)
                     {
                         if (iTercerDigito == 9)
                         {
                             if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                             {
                                 consultarRegistro();
                                 return;
                             }

                             else
                             {
                                 mensajeValidarCedula();
                                 return;
                             }
                         }

                         else if (iTercerDigito == 6)
                         {
                             if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                             {
                                 consultarRegistro();
                                 return;
                             }

                             else
                             {
                                 mensajeValidarCedula();
                                 return;
                             }
                         }

                         else if (iTercerDigito <= 5 || iTercerDigito >= 0)
                         {
                             if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                             {
                                 consultarRegistro();
                                 return;
                             }

                             else
                             {
                                 mensajeValidarCedula();
                                 return;
                             }
                         }
                     }
                 }
             }

             catch (Exception ex)
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                 ok.ShowDialog();
                 txtIdentificacion.Clear();
                 txtIdentificacion.Focus();
             }             
         }

         //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
         public bool esNumero(object Expression)
         {

             bool isNum;

             double retNum;

             isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

             return isNum;
         }

        //CREAR DATATABLE ´PARA EL GRID DE DEUDA
         private void llenarDetalleDeuda()
         {
             try
             {
                 dgvDetalleDeuda.Rows.Add("ABONO", "0.00");
                 dgvDetalleDeuda.Rows.Add("SALDO", "0.00");
                 dgvDetalleDeuda.Rows.Add("CAMBIO", "0.00");
                 dgvDetalleDeuda.Rows.Add("PROPINA", "0.00");
                 dgvDetalleDeuda.ClearSelection();
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //LLENAR EL GRID DE PAGOS
         private void llenarGrid()
         {
             try
             {
                 sSql = "";
                 sSql += "select * from pos_vw_pedido_forma_pago" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if ((bRespuesta == false) || (dtConsulta.Rows.Count <= 0))
                 {
                     return;
                 }

                 Decimal num = new Decimal(0);

                 for (int i = 0; i < dtConsulta.Rows.Count; ++i)
                 {
                     string sCodigo_R = dtConsulta.Rows[i]["codigo"].ToString().Trim().ToUpper();
                     
                     if ((sCodigo_R == "TD") || (sCodigo_R == "TC"))
                     {
                         iConciliacion = 1;
                     }

                     else
                     {
                         iConciliacion = 0;
                     }

                     dgvPagos.Rows.Add();
                     dgvPagos.Rows[i].Cells["id"].Value = dtConsulta.Rows[i]["id_pos_tipo_forma_cobro"].ToString();
                     dgvPagos.Rows[i].Cells["fpago"].Value = dtConsulta.Rows[i]["descripcion"].ToString();
                     dValor = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                     dgvPagos.Rows[i].Cells["valor"].Value = dValor.ToString("N2");
                     dgvPagos.Rows[i].Cells["id_sri"].Value = dtConsulta.Rows[i]["id_sri_forma_pago"].ToString();
                     dgvPagos.Rows[i].Cells["conciliacion"].Value = iConciliacion.ToString();
                     dgvPagos.Rows[i].Cells["id_operador_tarjeta"].Value = dtConsulta.Rows[i]["id_pos_operador_tarjeta"].ToString();
                     dgvPagos.Rows[i].Cells["id_tipo_tarjeta"].Value = dtConsulta.Rows[i]["id_pos_tipo_tarjeta"].ToString();
                     dgvPagos.Rows[i].Cells["numero_lote"].Value = dtConsulta.Rows[i]["lote_tarjeta"].ToString();
                     dgvPagos.Rows[i].Cells["bandera_insertar_lote"].Value = "1";
                     
                     num += dValor;
                 }

                 dgvPagos.Columns[0].Visible = false;
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CARGAR LOS VALORES DE LA PRECUENTA
         private void valoresPrecuenta()
         {
             try
             {
                 dbSumaIva = 0;
                 sSql = "";
                 sSql += "select cantidad, precio_unitario, valor_dscto," + Environment.NewLine;
                 sSql += "valor_iva, valor_otro, nombre, paga_iva, id_det_pedido," + Environment.NewLine;
                 sSql += "id_producto, genera_factura" + Environment.NewLine;
                 sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                 dtComanda = new DataTable();
                 dtComanda.Clear();
                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtComanda, sSql);

                 if (bRespuesta == true)
                 {
                     iGeneraFactura = Convert.ToInt32(dtComanda.Rows[0]["genera_factura"].ToString());

                     for (int i = 0; i < dtComanda.Rows.Count; ++i)
                     {
                         if (Convert.ToDouble(dtComanda.Rows[i]["valor_iva"].ToString()) == 0.0 && Convert.ToInt32(dtComanda.Rows[i]["paga_iva"].ToString()) == 1)
                         {
                             dbRecalcularPrecioUnitario = Convert.ToDecimal(dtComanda.Rows[i]["precio_unitario"].ToString());
                             dbRecalcularDescuento = Convert.ToDecimal(dtComanda.Rows[i]["valor_dscto"].ToString());
                             dbRecalcularIva = (dbRecalcularPrecioUnitario - dbRecalcularDescuento) * Convert.ToDecimal(Program.iva);
                             dtComanda.Rows[i]["valor_iva"] = dbRecalcularIva.ToString();
                         }

                         dbSumaIva += Convert.ToDecimal(dtComanda.Rows[i]["cantidad"].ToString()) * Convert.ToDecimal(dtComanda.Rows[i]["valor_iva"].ToString());
                     }
                 }

                 else
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CAMBIAR LAS FORMAS DE PAGO
         private void cambiarFormasPagos(int iOp)
         {
             try
             {
                 if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) != 0.0 && iOp == 1)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No ha realizado el cobro completo de la comanda.";
                     ok.ShowDialog();
                     return;
                 }

                 if (extraerFecha() == false)
                 {
                     return;
                 }

                 if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Error al abrir transacción.";
                     ok.ShowDialog();
                     return;
                 }

                 sSql = "";
                 sSql += "select id_documento_pago" + Environment.NewLine;
                 sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 for (int i = 0; i < dtConsulta.Rows.Count; i++)
                 {
                     sSql = "";
                     sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }
                 }

                 if (iBanderaRecargoBoton == 1)
                 {
                     if (Program.iAplicaRecargoTarjeta == 1 && iBanderaRecargoBDD == 1)
                     {
                         if (actualizarPreciosRecargo() == false)
                         {
                             goto reversa;
                         }
                     }
                 }

                 else if (iBanderaRemoverIvaBoton == 1)
                 {
                     if (Program.iDescuentaIva == 1)
                     {
                         if (actualizarIVACero() == false)
                         {
                             goto reversa;
                         }
                     }
                 }

                 else if (Program.iDescuentaIva == 1 || Program.iAplicaRecargoTarjeta == 1)
                 {
                     if (actualizarPreciosOriginales() == false)
                     {
                         goto reversa;
                     }
                 }

                 sSql = "";
                 sSql += "select id_documento_cobrar" + Environment.NewLine;
                 sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                 sSql += "and estado = 'A'";

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                 iCuenta = 0;

                 sSql = "";
                 sSql += "select count(*) cuenta" + Environment.NewLine;
                 sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                 sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                 sSql += "and estado = 'A'";

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 if (iCuenta > 0)
                 {
                     sSql = "";
                     sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                     sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                     sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                     sSql += "and estado = 'A'";

                     dtConsulta = new DataTable();
                     dtConsulta.Clear();

                     bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                     if (bRespuesta == false)
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     if (dtConsulta.Rows.Count > 0)
                     {
                         iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                         iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                     }

                     sSql = "";
                     sSql += "update cv403_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     sSql = "";
                     sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }
                 }

                 sSql = "";
                 sSql += "insert into cv403_pagos (" + Environment.NewLine;
                 sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                 sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                 sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                 sSql += "numero_replica_trigger, numero_control_replica, cambio) " + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                 sSql += dTotal + ", " + Convert.ToDouble(dgvDetalleDeuda.Rows[3].Cells[1].Value) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                 sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + Convert.ToDouble(dgvDetalleDeuda.Rows[2].Cells[1].Value) + ")";
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

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

                 sSql = "";
                 sSql += "select numero_pago" + Environment.NewLine;
                 sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                 sSql += "and estado = 'A'";
                 
                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 sSql = "";
                 sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                 sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                 sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";
                             
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 for (int i = 0; i < dgvPagos.Rows.Count; i++)
                 {
                     sSql = "";
                     sSql += "select cg_tipo_documento" + Environment.NewLine;
                     sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                     sSql += "where id_pos_tipo_forma_cobro = " + dgvPagos.Rows[i].Cells[0].Value;
                     
                     dtConsulta = new DataTable();
                     dtConsulta.Clear();
                     bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                     if (bRespuesta == false)
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                     iConciliacion = Convert.ToInt32(dgvPagos.Rows[i].Cells[4].Value.ToString());
                     iOperadorTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[5].Value.ToString());
                     iTipoTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[6].Value.ToString());
                     iBanderaInsertarLote = Convert.ToInt32(dgvPagos.Rows[i].Cells[8].Value.ToString());
                     sNumeroLote = dgvPagos.Rows[i].Cells[7].Value.ToString();
                     dbPropinaRecibidaFormaPago = Convert.ToDecimal(dgvPagos.Rows[i].Cells["propina"].Value.ToString());

                     if (iConciliacion == 1)
                     {
                         int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                         if (iRespuestaNumeroLote == -1)
                         {
                             goto reversa;
                         }

                         if (iRespuestaNumeroLote == 0)
                         {
                             if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                             {
                                 goto reversa;
                             }
                         }
                     }

                     sSql = "";
                     sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                     sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                     sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                     sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                     sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                     sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                     sSql += "values(" + Environment.NewLine;
                     sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                     sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                     sSql += Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                     sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                     if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) == 1)
                     {
                         sSql += (Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + dbCambio) + ", ";
                     }

                     else
                     {
                         sSql += "null, ";
                     }

                     if (iConciliacion == 1)
                     {
                         sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                     }

                     else
                     {
                         sSql += "null, null, null, ";
                     }

                     sSql += dbPropinaRecibidaFormaPago + ")";
                     
                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }

                     if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) != 1 || Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) != 11)
                     {                         
                         sTabla = "cv403_documentos_pagos";
                         sCampo = "id_documento_pago";
                         
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
                             iIdDocumentoPago = Convert.ToInt32(iMaximo);
                         }
                     }
                 }

                 sSql = "";
                 sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                 sSql += "id_documento_cobrar, id_pago, valor, " + Environment.NewLine;
                 sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                 sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;
                 sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotal + ", 'A', 1, 0," + Environment.NewLine;
                 sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 sSql = "";
                 sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                 sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 if (iOpCambiarEstadoOrden == 1)
                 {
                     sSql = "";
                     sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                     if (iBanderaComandaPendiente == 1)
                     {
                         sSql += "id_pos_cierre_cajero_por_cobrar = " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                     }

                     sSql += "estado_orden = 'Pagada'" + Environment.NewLine;
                     sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                     sSql += "and estado = 'A'";
                                          
                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

                 sSql = "";
                 sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                 sSql += "recargo_tarjeta = " + iBanderaRecargoBoton + "," + Environment.NewLine;
                 sSql += "remover_iva = " + iBanderaRemoverIvaBoton + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden;

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     goto reversa;
                 }

                 conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "Las formas de pago se han actualizado éxitosamente";
                 ok.ShowDialog();
                 this.DialogResult = DialogResult.OK;
                 return;
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
         }

        //FUNCION PARA ACTUALIZAR EL MOVIMIENTO DE CAJA
         private bool actualizarMovimientosCaja()
         {
             try
             {
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

                 sSql = "";
                 sSql += "select numeromovimientocaja" + Environment.NewLine;
                 sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 if (dtConsulta.Rows.Count > 0)
                 {
                     iNumeroMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                 }

                 sSql = "";
                 sSql += "select id_factura" + Environment.NewLine;
                 sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 if (dtConsulta.Rows.Count > 0)
                 {
                     iIdFactura = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                 }

                 sSql = "";
                 sSql += "select id_persona, numero_pedido, establecimiento," + Environment.NewLine;
                 sSql += "punto_emision, numero_factura, idtipocomprobante" + Environment.NewLine;
                 sSql += "from pos_vw_factura" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                 sSql += "order by id_det_pedido";

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

                 iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                 iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                 sFacturaRecuperada = dtConsulta.Rows[0][2].ToString() + "-" + dtConsulta.Rows[0][3].ToString() + "-" + dtConsulta.Rows[0][4].ToString().PadLeft(9, '0');
                 iIdTipoComprobante = Convert.ToInt32(dtConsulta.Rows[0][5].ToString());

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

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 if (dtAuxiliar.Rows.Count == 0)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No existe formas de pagos realizados. Couníquese con el administrador del sistema.";
                     ok.ShowDialog();
                     return false;
                 }

                 if (iIdTipoComprobante == 1)
                 {
                     sFacturaRecuperada = "FACT. No. " + sFacturaRecuperada;
                 }

                 else
                 {
                     sFacturaRecuperada = "N. ENTREGA. No. " + sFacturaRecuperada;
                 }

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
                     sSql += Convert.ToDouble(dtAuxiliar.Rows[i][1].ToString()) + "," + Environment.NewLine;
                     sSql += "'COBRO No. CUENTA " + iNumeroPedido.ToString() + " (" + dtAuxiliar.Rows[i][0].ToString() + ")', '" + Environment.NewLine;
                     sSql += sFacturaRecuperada + "', " + Environment.NewLine;
                     sSql += Convert.ToInt32(dtAuxiliar.Rows[i][5].ToString()) + ", " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A', " + Environment.NewLine;
                     sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
                     
                     sTabla = "pos_movimiento_caja";
                     sCampo = "id_pos_movimiento_caja";

                     iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                     if (iMaximo == -1)
                     {
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                         ok.ShowDialog();
                         return false;
                     }

                     iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);

                     sSql = "";
                     sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                     sSql += "id_pos_movimiento_caja, numero_movimiento_caja," + Environment.NewLine;
                     sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                     sSql += "values (" + Environment.NewLine;
                     sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimiento + ", 'A', GETDATE()," + Environment.NewLine;
                     sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     iNumeroMovimiento++;
                 }

                 sSql = "";
                 sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                 sSql += "numeromovimientocaja = " + iNumeroMovimiento + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                 sSql += "and estado = 'A'";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
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

        //FUNCION PARA INSERTAR LOS PAGOS EN LA NUEVA COMANDA
         private bool insertarPagoNuevoPrecuenta()
         {
             try
             {
                 if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Error al abrir transacción.";
                     ok.ShowDialog();
                     return false;
                 }

                 if (Program.iDescuentaIva == 1)
                 {
                     if (iEjecutarActualizacionIVA == 1)
                     {
                         if (!actualizarIVACero())
                         {
                             return false;
                         }
                     }

                     else if (!actualizarIVA())
                     {
                         return false;
                     }
                 }

                 if (Program.iAplicaRecargoTarjeta == 1)
                 {
                     if (iEjecutarActualizacionTarjetas == 1)
                     {
                         if (!actualizarRecargoTarjetas())
                         {
                             return false;
                         }
                     }

                     else if (!actualizarRemoverRecargoTarjetas())
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "Ocurrió un problema en remover el recargo de tarjetas.";
                         catchMensaje.ShowDialog();
                         return false;
                     }
                 }

                 sSql = "";
                 sSql += "select id_documento_cobrar" + Environment.NewLine;
                 sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                 iCuenta = 0;

                 sSql = "";
                 sSql += "select count(*) cuenta" + Environment.NewLine;
                 sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                 sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 if (iCuenta > 0)
                 {
                     sSql = "";
                     sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                     sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                     sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                     sSql += "and estado = 'A'";

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

                     iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                     iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());

                     sSql = "";
                     sSql += "update cv403_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
                 }

                 dbCambio = Convert.ToDecimal(dgvDetalleDeuda.Rows[2].Cells[1].Value.ToString());
                 dbPropina = Convert.ToDecimal(dgvDetalleDeuda.Rows[3].Cells[1].Value.ToString());

                 sSql = "";
                 sSql += "insert into cv403_pagos (" + Environment.NewLine;
                 sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                 sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                 sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                 sSql += "numero_replica_trigger, numero_control_replica, cambio) " + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += Program.iIdEmpresa + ", " + iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                 sSql += dTotal + ", " + Convert.ToDouble(dgvDetalleDeuda.Rows[3].Cells[1].Value) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                 sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + dbCambio + ")";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sTabla = "cv403_pagos";
                 sCampo = "id_pago";
                 iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                 if (iMaximo == -1)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                     ok.ShowDialog();
                     return false;
                 }

                 iIdPago = Convert.ToInt32(iMaximo);

                 sSql = "";
                 sSql += "select numero_pago" + Environment.NewLine;
                 sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

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

                 iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 sSql = "";
                 sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                 sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                 sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                 sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 for (int i = 0; i < dgvPagos.Rows.Count; ++i)
                 {
                     sSql = "";
                     sSql += "select cg_tipo_documento" + Environment.NewLine;
                     sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                     sSql += "where id_pos_tipo_forma_cobro = " + dgvPagos.Rows[i].Cells[0].Value.ToString();

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

                     iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                     iConciliacion = Convert.ToInt32(dgvPagos.Rows[i].Cells[4].Value.ToString());
                     iOperadorTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[5].Value.ToString());
                     iTipoTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[6].Value.ToString());
                     iBanderaInsertarLote = Convert.ToInt32(dgvPagos.Rows[i].Cells[8].Value.ToString());
                     sNumeroLote = dgvPagos.Rows[i].Cells[7].Value.ToString();
                     dbPropinaRecibidaFormaPago = Convert.ToDecimal(dgvPagos.Rows[i].Cells["propina"].Value.ToString());

                     if (iConciliacion == 1)
                     {
                         int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                         if (iRespuestaNumeroLote == -1)
                         {
                             return false;
                         }

                         if (iRespuestaNumeroLote == 0)
                         {
                             if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                             {
                                 return false;
                             }
                         }
                     }

                     sSql = "";
                     sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                     sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                     sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                     sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                     sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                     sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                     sSql += "values(" + Environment.NewLine;
                     sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                     sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                     sSql += Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                     sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                     if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) == 1)
                     {
                         sSql += (Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + dbCambio) + ", ";
                     }

                     else
                     {
                         sSql += "null, ";
                     }

                     if (iConciliacion == 1)
                     {
                         sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                     }

                     else
                     {
                         sSql += "null, null, null, ";
                     }

                     sSql += dbPropinaRecibidaFormaPago + ")";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }                     
                 }

                 sSql = "";
                 sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                 sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                 sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                 sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;
                 sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotal + ", 'A', 1, 0, " + Environment.NewLine;
                 sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 //FIN FOR
                 sSql = "";
                 sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                 sSql += "estado_orden = 'Pre-Cuenta'" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                 sSql += "and estado = 'A'" + Environment.NewLine;

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 conexion.GFun_Lo_Maneja_Transaccion(2);
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

        //FUNCION PARA CAMBIAR LAS FORMAS DE PAGO EN LA PRECUENTA
         private bool cambiarFormasPagosPrecuenta(int iOp)
         {
             try
             {
                 if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Error al abrir transacción.";
                     ok.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "select id_documento_pago" + Environment.NewLine;
                 sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

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

                 for (int i = 0; i < dtConsulta.Rows.Count; i++)
                 {
                     sSql = "";
                     sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pago = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }
                 }

                 sSql = "";
                 sSql += "select id_documento_cobrar" + Environment.NewLine;
                 sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                 iCuenta = 0;

                 sSql = "";
                 sSql += "select count(*) cuenta" + Environment.NewLine;
                 sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                 sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 if (iCuenta > 0)
                 {
                     sSql = "";
                     sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                     sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                     sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                     sSql += "and estado = 'A'";

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

                     if (dtConsulta.Rows.Count > 0)
                     {
                         iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                         iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                     }

                     sSql = "";
                     sSql += "update cv403_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql +="terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql +="where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
                 }

                 dbCambio = Convert.ToDecimal(dgvDetalleDeuda.Rows[2].Cells[1].Value.ToString());
                 dbPropina = Convert.ToDecimal(dgvDetalleDeuda.Rows[3].Cells[1].Value.ToString());

                 sSql = "";
                 sSql += "insert into cv403_pagos (" + Environment.NewLine;
                 sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                 sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                 sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                 sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += Program.iIdEmpresa + ", " + iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                 sSql += dTotal + ", " + Convert.ToDouble(dgvDetalleDeuda.Rows[3].Cells[1].Value) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                 sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + dbCambio + ")";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sTabla = "cv403_pagos";
                 sCampo = "id_pago";

                 iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                 if (iMaximo == -1)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                     ok.ShowDialog();
                     return false;
                 }

                 iIdPago = Convert.ToInt32(iMaximo);

                 sSql = "";
                 sSql += "select numero_pago" + Environment.NewLine;
                 sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

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

                 iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 sSql = "";
                 sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                 sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                 sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                 sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 for (int i = 0; i < dgvPagos.Rows.Count; i++)
                 {          
                     sSql = "";
                     sSql += "select cg_tipo_documento" + Environment.NewLine;
                     sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                     sSql += "where id_pos_tipo_forma_cobro = " + dgvPagos.Rows[i].Cells[0].Value.ToString();

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

                     iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                     iConciliacion = Convert.ToInt32(dgvPagos.Rows[i].Cells[4].Value.ToString());
                     iOperadorTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[5].Value.ToString());
                     iTipoTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[6].Value.ToString());
                     iBanderaInsertarLote = Convert.ToInt32(dgvPagos.Rows[i].Cells[8].Value.ToString());
                     sNumeroLote = dgvPagos.Rows[i].Cells[7].Value.ToString();
                     dbPropinaRecibidaFormaPago = Convert.ToDecimal(dgvPagos.Rows[i].Cells["propina"].Value.ToString());

                     if (iConciliacion == 1)
                     {
                         int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                         if (iRespuestaNumeroLote == -1)
                         {
                             return false;
                         }

                         if (iRespuestaNumeroLote == 0)
                         {
                             if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                             {
                                 return false;
                             }
                         }
                     }

                     sSql = "";
                     sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                     sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                     sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                     sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                     sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                     sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                     sSql += "values(" + Environment.NewLine;
                     sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                     sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                     sSql += Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                     sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                     if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) == 1)
                     {
                         sSql += (Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + dbCambio) + ", ";
                     }

                     else
                     {
                         sSql += "null, ";
                     }

                     if (iConciliacion == 1)
                     {
                         sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                     }

                     else
                     {
                         sSql += "null, null, null, ";
                     }

                     sSql += dbPropinaRecibidaFormaPago + ")";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }                     
                 }

                 sSql = "";
                 sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                 sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                 sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                 sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;
                 sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotal + ", 'A', 1, 0, " + Environment.NewLine;
                 sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 if (iOpCambiarEstadoOrden == 1)
                 {
                     sSql = "";
                     sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                     if (iBanderaComandaPendiente == 1)
                     {
                         sSql += "id_pos_cierre_cajero_por_cobrar = " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                     }

                     sSql += "estado_orden = 'Pagada'" + Environment.NewLine;
                     sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                     sSql += "and estado = 'A'";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         goto reversa;
                     }
                 }

                 if (iOp == 1)
                 {
                     if (!actualizarMovimientosCaja())
                     {
                         goto reversa;
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

             reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
         }

        //FUNCION SOLO PARA ELIMINAR LAS FORMAS DE PAGO
        private bool eliminarFormasPagos()
         {
            try
            {
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

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

                iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                if (iCuenta > 0)
                {
                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

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

                    iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());

                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
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

        //FUNCION PARA CREAR LOS PAGOS FACTURA
         private void crearPagosFactura()
         {
             try
             {
                 Cursor = Cursors.WaitCursor;

                 if (extraerFecha() == false)
                 {
                     return;
                 }

                 if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Error al abrir transacción.";
                     ok.ShowDialog();
                     return;
                 }

                 if (btnCorreoElectronicoDefault.AccessibleName == "1")
                 {
                     sSql = "";
                     sSql += "update tp_personas set" + Environment.NewLine;
                     sSql += "correo_electronico = '" + txtMail.Text.Trim() + "'" + Environment.NewLine;
                     sSql += "where id_persona = " + iIdPersona + Environment.NewLine;
                     sSql += "and estado = 'A'";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         return;
                     }
                 }

                 if (iBanderaRecargoBoton == 1)
                 {
                     if ((Program.iAplicaRecargoTarjeta == 1) && (iBanderaRecargoBDD == 1) && (actualizarPreciosRecargo() == false))
                     {
                         conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         return;
                     }
                 }

                 else if (iBanderaRemoverIvaBoton == 1)
                 {
                     if (Program.iDescuentaIva == 1 && !actualizarIVACero())
                     {
                         conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         return;
                     }
                 }

                 else if (((Program.iDescuentaIva == 1) || (Program.iAplicaRecargoTarjeta == 1)) && (actualizarPreciosOriginales() == false))
                 {
                     conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                     return;
                 }
                 
                 if (insertarPagos() == false)
                 {
                     conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                     return;
                 }

                 else
                 {
                     if (iBanderaGeneraFactura == 0)
                     {
                         sSql = "";
                         sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                         if (iBanderaComandaPendiente == 1)
                         {
                             sSql += "id_pos_cierre_cajero_por_cobrar = " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                         }

                         sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                         sSql += "id_persona = " + iIdPersona + Environment.NewLine;
                         sSql += "where id_pedido = " + sIdOrden;

                         if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                         {
                             catchMensaje = new VentanasMensajes.frmMensajeCatch();
                             catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                             catchMensaje.ShowDialog();
                             conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         }
                     }
                     else
                     {
                         if (insertarFactura() == false)
                         {
                             conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                             return;
                         }

                         if (!insertarMovimientosCaja())
                         {
                             conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                             return;
                         }

                         sSql = "";
                         sSql += "update tp_localidades_impresoras set" + Environment.NewLine;

                         if (rdbFactura.Checked)
                         {
                             sSql += "numero_factura = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + Environment.NewLine;
                         }

                         else if (rdbNotaVenta.Checked)
                         {
                             sSql += "numeronotaentrega = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + Environment.NewLine;
                         }

                         sSql += "where id_localidad = " + Program.iIdLocalidad;
                         
                         if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                         {
                             catchMensaje = new VentanasMensajes.frmMensajeCatch();
                             catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                             catchMensaje.ShowDialog();
                             conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         }
                     }

                     conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                     if (iBanderaGeneraFactura == 1)
                     {
                         crearReporte();
                     }
                     else
                     {
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "Se ha procedido a ingresar los datos de forma éxitosa.";
                         ok.ShowDialog();

                         if (ok.DialogResult == DialogResult.OK)
                         {
                             this.DialogResult = DialogResult.OK;
                             Close();

                             //if (Program.iBanderaCerrarVentana == 0)
                             //{
                             //    ord = Owner as ComandaNueva.frmComanda;
                             //    ord.Close();
                             //}

                             //else
                             //{
                             //    Program.iBanderaCerrarVentana = 0;
                             //}
                         }
                     }

                     Program.iSeleccionarNotaVenta = 0;
                     Cursor = Cursors.Default;
                 }
             }

             catch (Exception ex)
             {
                 conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA INSERTAR LOS PAGOS
         private bool insertarPagos()
         {
             try
             {
                 sSql = "";
                 sSql += "select id_documento_cobrar" + Environment.NewLine;
                 sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                 iCuenta = 0;

                 sSql = "";
                 sSql += "select count(*) cuenta" + Environment.NewLine;
                 sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                 sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                 sSql += "and estado = 'A'";

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

                 iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 if (iCuenta > 0)
                 {
                     sSql = "";
                     sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                     sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                     sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                     sSql += "and estado = 'A'";

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

                     if (dtConsulta.Rows.Count > 0)
                     {
                         iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                         iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                     }

                     sSql = "";
                     sSql += "update cv403_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_pago = " + iIdPago;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                     sSql += "estado = 'E'," + Environment.NewLine;
                     sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                     sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                     sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                     sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
                 }

                 dbCambio = Convert.ToDecimal(dgvDetalleDeuda.Rows[2].Cells[1].Value.ToString());
                 dbPropina = Convert.ToDecimal(dgvDetalleDeuda.Rows[3].Cells[1].Value.ToString());

                 //INSERTAR EN LA TABLA CV403_PAGOS
                 sSql = "";
                 sSql += "insert into cv403_pagos (" + Environment.NewLine;
                 sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                 sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                 sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                 sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += Program.iIdEmpresa + ", " + iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                 sSql += dTotal + ", " + Convert.ToDouble(dgvDetalleDeuda.Rows[3].Cells[1].Value) + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                 sSql += Program.iIdLocalidad + ", 7799, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, " + dbCambio + ")";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sTabla = "cv403_pagos";
                 sCampo = "id_pago";

                 iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                 if (iMaximo == -1)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                     ok.ShowDialog();
                     return false;
                 }

                 iIdPago = Convert.ToInt32(iMaximo);

                 sSql = "";
                 sSql += "select numero_pago" + Environment.NewLine;
                 sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

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

                 iNumeroPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                 sSql = "";
                 sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                 sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                 sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += iIdPago + ", 'A', " + iNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                 sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                 sSql += "where id_localidad = " + Program.iIdLocalidad;

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 for (int i = 0; i < dgvPagos.Rows.Count; i++)
                 {           
                     sSql = "";
                     sSql += "select cg_tipo_documento" + Environment.NewLine;
                     sSql += "from pos_tipo_forma_cobro " + Environment.NewLine;
                     sSql += "where id_pos_tipo_forma_cobro = " + dgvPagos.Rows[i].Cells[0].Value.ToString();

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

                     iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                     iConciliacion = Convert.ToInt32(dgvPagos.Rows[i].Cells[4].Value.ToString());
                     iOperadorTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[5].Value.ToString());
                     iTipoTarjeta = Convert.ToInt32(dgvPagos.Rows[i].Cells[6].Value.ToString());
                     iBanderaInsertarLote = Convert.ToInt32(dgvPagos.Rows[i].Cells[8].Value.ToString());
                     sNumeroLote = dgvPagos.Rows[i].Cells[7].Value.ToString();
                     dbPropinaRecibidaFormaPago = Convert.ToDecimal(dgvPagos.Rows[i].Cells["propina"].Value.ToString());

                     if (iConciliacion == 1)
                     {
                         int iRespuestaNumeroLote = contarNumeroLote(iOperadorTarjeta);

                         if (iRespuestaNumeroLote == -1)
                         {
                             return false;
                         }

                         if (iRespuestaNumeroLote == 0)
                         {
                             if (insertarNumeroLote(sNumeroLote, iOperadorTarjeta) == false)
                             {
                                 return false;
                             }
                         }
                     }

                     sSql = "";
                     sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                     sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                     sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                     sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                     sSql += "numero_replica_trigger, numero_control_replica, valor_recibido," + Environment.NewLine;
                     sSql += "lote_tarjeta, id_pos_operador_tarjeta, id_pos_tipo_tarjeta, propina_recibida)" + Environment.NewLine;
                     sSql += "values(" + Environment.NewLine;
                     sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                     sSql += Program.iMoneda + ", 1, " + Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + "," + Environment.NewLine;
                     sSql += Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) + ", 'A', GETDATE()," + Environment.NewLine;
                     sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, 0,";

                     if (Convert.ToInt32(dgvPagos.Rows[i].Cells[0].Value) == 1)
                     {
                         sSql += (Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value) + dbCambio) + ", ";
                     }

                     else
                     {
                         sSql += "null, ";
                     }

                     if (iConciliacion == 1)
                     {      
                         sSql += "'" + sNumeroLote + "', " + iOperadorTarjeta + ", " + iTipoTarjeta + ", ";
                     }

                     else
                     {
                         sSql += "null, null, null, ";
                     }

                     sSql += dbPropinaRecibidaFormaPago + ")";

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }                     
                 }

                 sSql = "";
                 sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                 sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                 sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                 sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;
                 sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dTotal + ", 'A', 1, 0, " + Environment.NewLine;
                 sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
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

        //FUNCION PARA INSERTAR LA FACTURA
         private bool insertarFactura()
         {
             try
             {
                 int iFacturaElectronica_P;
                 iFacturaElectronica_P = 0;

                 if (rdbFactura.Checked)
                 {
                     iIdTipoComprobante = 1;
                 }

                 else if (rdbNotaVenta.Checked)
                 {
                     iIdTipoComprobante = Program.iComprobanteNotaEntrega;
                 }

                 if (iIdTipoComprobante == 1)
                 {
                     if (Program.iFacturacionElectronica == 1)
                     {
                         iFacturaElectronica_P = 1;
                         sClaveAcceso = sGenerarClaveAcceso();
                     }
                 }

                 llenarDataTable();

                 sSql = "";
                 sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                 sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, id_forma_pago2, id_forma_pago3," + Environment.NewLine;
                 sSql += "fecha_factura, fecha_vcto, cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, " + Environment.NewLine;
                 sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                 sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio," + Environment.NewLine;
                 sSql += "facturaelectronica, id_tipo_emision, id_tipo_ambiente, clave_acceso)" + Environment.NewLine;
                 sSql += "values(" + Environment.NewLine;
                 sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                 sSql += iIdTipoComprobante + "," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago_1 + ", " + Environment.NewLine;
                 
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

                 sSql += "'" + sFecha + "', '" + sFecha + "', " + Program.iMoneda + ", " + dTotal + ", 0, 0, GETDATE()," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                 sSql += "'" + txtDireccion.Text.Trim() + "', '" + txtTelefono.Text + "', '" + sCiudad + "'," + Environment.NewLine;
                 sSql += "'" + txtMail.Text.Trim() + "', " + dServicio + ", " + iFacturaElectronica_P + ", " + iIdTipoEmision + ", " + iIdTipoAmbiente + "," + Environment.NewLine;

                 if (iFacturaElectronica_P == 1)
                 {
                     sSql += "'" + sClaveAcceso + "')";
                 }

                 else
                 {
                     sSql += "null)";
                 }
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sTabla = "cv403_facturas";
                 sCampo = "id_factura";

                 iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                 if (iMaximo == -1)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                     ok.ShowDialog();
                     return false;
                 }

                 iIdFactura = Convert.ToInt32(iMaximo);

                 sSql = "";
                 sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura," + Environment.NewLine;
                 sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger," + Environment.NewLine;
                 sSql += "numero_control_replica) " + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;
                 sSql += iIdFactura + ", " + iIdTipoComprobante + ", " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + ", GETDATE()," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                 sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                 sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                 sSql += "values (" + Environment.NewLine;                 
                 sSql += iIdFactura + ", " + sIdOrden + ", GETDATE()," + Environment.NewLine;
                 sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0 )";

                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                 sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                 sSql += "cg_estado_dcto = " + iCgEstadoDctoPorCobrar + "," + Environment.NewLine;
                 sSql += "numero_documento = " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden;
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 sSql = "";
                 sSql += "update cv403_cab_pedidos set" + Environment.NewLine;

                 if (iBanderaComandaPendiente == 1)
                 {
                     sSql += "id_pos_cierre_cajero_por_cobrar = " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                 }

                 sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                 sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                 sSql += "fecha_cierre_orden = GETDATE()," + Environment.NewLine;
                 sSql += "recargo_tarjeta = " + iBanderaRecargoBoton + "," + Environment.NewLine;
                 sSql += "remover_iva = " + iBanderaRemoverIvaBoton + Environment.NewLine;
                 sSql += "where id_pedido = " + sIdOrden;
                 
                 if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 if (rdbNotaVenta.Checked)
                 {
                     sSql = "";
                     sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                     sSql += "idtipocomprobante = " + Program.iComprobanteNotaEntrega + Environment.NewLine;
                     sSql += "where id_pedido = " + sIdOrden;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
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

        //FUNCION PARA INSERTAR LOS MOVIMIENTOS DE CAJA
         private bool insertarMovimientosCaja()
         {
             try
             {
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
                 sSecuencial = TxtNumeroFactura.Text.Trim().PadLeft(9, '0');

                 sSql = "";
                 sSql += "select descripcion, sum(valor), cambio,  count(*) cuenta, " + Environment.NewLine;
                 sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, id_documento_pago" + Environment.NewLine;
                 sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                 sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                 sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                 sSql += "id_pago, id_documento_pago";

                 dtAuxiliar = new DataTable();
                 dtAuxiliar.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                 if (bRespuesta == false)
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                     return false;
                 }

                 if (dtAuxiliar.Rows.Count == 0)
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No existe formas de pagos realizados. Comuníquese con el administrador del sistema.";
                     ok.ShowDialog();
                     return false;
                 }

                 if (rdbFactura.Checked)
                 {
                     sMovimiento = ("FACT. No. " + txtfacturacion.Text.Trim() + "-" + sSecuencial).Trim();
                 }

                 else if (rdbNotaVenta.Checked)
                 {
                     sMovimiento = ("N. ENTREGA. No. " + txtfacturacion.Text.Trim() + "-" + sSecuencial).Trim();
                 }

                 for (int i = 0; i < dtAuxiliar.Rows.Count; ++i)
                 {
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
                     sSql += Convert.ToDecimal(dtAuxiliar.Rows[i][1].ToString()) + "," + Environment.NewLine;
                     sSql += "'COBRO No. CUENTA " + iNumeroPedido_P.ToString() + " (" + dtAuxiliar.Rows[i][0].ToString() + ")'," + Environment.NewLine;
                     sSql += "'" + sMovimiento.Trim() + "', " + Convert.ToInt32(dtAuxiliar.Rows[i][5].ToString()) + ", " + Program.iJORNADA + "," + Environment.NewLine;
                     sSql += Program.iIdPosCierreCajero + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                     
                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
                     
                     sTabla = "pos_movimiento_caja";
                     sCampo = "id_pos_movimiento_caja";

                     iMaximo= conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                     if (iMaximo == -1)
                     {
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                         ok.ShowDialog();
                         return false;
                     }

                     iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);

                     sSql = "";
                     sSql += "select numeromovimientocaja" + Environment.NewLine;
                     sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                     sSql += "where id_localidad = " + Program.iIdLocalidad;
                     
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

                     iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

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
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }

                     sSql = "";
                     sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                     sSql += "numeromovimientocaja = numeromovimientocaja + 1" + Environment.NewLine;
                     sSql += "where id_localidad = " + Program.iIdLocalidad;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
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

        //FUNCION PARA CREAR LA CLAVE DE ACCESO
        private string sGenerarClaveAcceso()
         {
             //GENERAR CLAVE DE ACCESO
             string sClaveAcceso_R = "";
             string sFecha_R = Convert.ToDateTime(sFecha).ToString("ddMMyyyy");
             string TipoComprobante = "01";
             string NumeroRuc = Program.sNumeroRucEmisor;
             string TipoAmbiente = Program.iTipoAmbiente.ToString();
             string TipoEmision = Program.iTipoEmision.ToString();
             string Serie = sEstablecimiento + sPuntoEmision;
             string NumeroComprobante = TxtNumeroFactura.Text.Trim().PadLeft(9, '0');
             string DigitoVerificador = "";
             string CodigoNumerico = "12345678";

             sClaveAcceso_R += sFecha_R + TipoComprobante + NumeroRuc + TipoAmbiente;
             sClaveAcceso_R += Serie + NumeroComprobante + CodigoNumerico + TipoEmision;

             DigitoVerificador = sDigitoVerificarModulo11(sClaveAcceso_R);
             sClaveAcceso_R += DigitoVerificador;
             return sClaveAcceso_R;
             //FIN CALVE ACCESO
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

        //FUNCION PARA CONTAR LOS NUMEROS DE LOTES
        private int contarNumeroLote(int iOperadorTarjeta_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_numero_lote" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado_lote = 'Abierta'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_pos_operador_tarjeta = " + iOperadorTarjeta_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

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
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

         //FUNCION PARA LLENAR EL DATATABLE
         private void llenarDataTable()
         {
             try
             {
                 crearDataTable();

                 for (int i = 0; i < dgvPagos.Rows.Count; i++)
                 {
                     DataRow row = dtAlmacenar.NewRow();
                     row["id_forma_pago"] = dgvPagos.Rows[i].Cells[3].Value.ToString();
                     row["valor"] = dgvPagos.Rows[i].Cells[2].Value.ToString();
                     dtAlmacenar.Rows.Add(row);
                 }

                 IEnumerable<IGrouping<string, DataRow>> query = from item in dtAlmacenar.AsEnumerable()
                                                                 group item by item["id_forma_pago"].ToString() into g
                                                                 select g;

                 dtAgrupado = Transformar(query);

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
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

                 if (dtAgrupado.Rows.Count > 2)
                 {
                     iIdFormaPago_3 = Convert.ToInt32(dtAgrupado.Rows[2]["id"].ToString());
                 }             
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA TRANSFORMAR EL DATATABLE
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
                     row["valor"] = item.Sum<DataRow>(x => Convert.ToDecimal(x["valor"]));
                     dt.Rows.Add(row);
                 }

                 return dt;
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
                 return null;
             }
         }

        //FUNCION PARA CREAR EL REPORTE
         private void crearReporte()
         {
             try
             {
                 Program.iCortar = 1;
                 dbCambio = Convert.ToDecimal(dgvDetalleDeuda.Rows[2].Cells[1].Value.ToString());

                 if (rdbFactura.Checked)
                 {
                     if (Program.iEjecutarImpresion == 1)
                     {
                         ReportesTextBox.frmVistaFactura frmVistaFactura = new ReportesTextBox.frmVistaFactura(iIdFactura, 1, 1);
                         frmVistaFactura.ShowDialog();

                         if (frmVistaFactura.DialogResult == DialogResult.OK)
                         {
                             this.DialogResult = DialogResult.OK;
                             Cambiocs cambiocs = new Cambiocs("$ " + dbCambio.ToString("N2"));
                             cambiocs.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                             cambiocs.ShowDialog();
                             Program.sIDPERSONA = null;
                             Program.dbValorPorcentaje = 0;
                             Program.dbDescuento = 0.0;
                             frmVistaFactura.Close();

                             this.DialogResult = DialogResult.OK;

                             //if (Program.iBanderaCerrarVentana == 0)
                             //{
                             //    ord = Owner as Orden;
                             //    ord.Close();
                             //}

                             //else
                             //{
                             //    Program.iBanderaCerrarVentana = 0;
                             //}
                         }
                     }

                     else
                     {
                         this.DialogResult = DialogResult.OK;
                         Cambiocs cambiocs = new Cambiocs("$ " + dbCambio.ToString("N2"));
                         cambiocs.lblVerMensaje.Text = "FACTURA GENERADA" + Environment.NewLine + "ÉXITOSAMENTE";
                         cambiocs.ShowDialog();

                         Program.sIDPERSONA = null;
                         Program.dbValorPorcentaje = 0;
                         Program.dbDescuento = 0;

                         this.DialogResult = DialogResult.OK;

                         //if (Program.iBanderaCerrarVentana == 0)
                         //{
                         //    ord = Owner as Orden;
                         //    ord.Close();
                         //}

                         //else
                         //{
                         //    Program.iBanderaCerrarVentana = 0;
                         //}
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

                            this.DialogResult = DialogResult.OK;

                            //if (Program.iBanderaCerrarVentana == 0)
                            //{
                            //    ord = Owner as Orden;
                            //    ord.Close();
                            //}

                            //else
                            //{
                            //    Program.iBanderaCerrarVentana = 0;
                            //}
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

                        this.DialogResult = DialogResult.OK;

                        //if (Program.iBanderaCerrarVentana == 0)
                        //{
                        //    ord = Owner as Orden;
                        //    ord.Close();
                        //}

                        //else
                        //{
                        //    Program.iBanderaCerrarVentana = 0;
                        //}
                    }
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();

                 if (ok.DialogResult == DialogResult.OK)
                 {
                     Program.sIDPERSONA = null;
                     //actualizarNumeroFactura();
                     Program.dbValorPorcentaje = 0;
                     Program.dbDescuento = 0;

                     this.DialogResult = DialogResult.OK;

                     //if (Program.iBanderaCerrarVentana == 0)
                     //{
                     //    ord = Owner as Orden;
                     //    ord.Close();
                     //}

                     //else
                     //{
                     //    Program.iBanderaCerrarVentana = 0;
                     //}
                 }
             }
         }

        //FUNCION PARA CARGAR LA CONFIGURACION DE LA FACTURACION ELECTRONICA
         private void configuracionFacturacion()
         {
             try
             {
                 sSql = "";
                 sSql += "select id_tipo_ambiente, id_tipo_emision" + Environment.NewLine;
                 sSql += "from sis_empresa" + Environment.NewLine;
                 sSql += "where idempresa = " + Program.iIdEmpresa;

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == true)
                 {
                     iIdTipoAmbiente = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_ambiente"].ToString());
                     iIdTipoEmision = Convert.ToInt32(dtConsulta.Rows[0]["id_tipo_emision"].ToString());
                 }

                 else
                 {
                     catchMensaje = new VentanasMensajes.frmMensajeCatch();
                     catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                     catchMensaje.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA CONSULTAR EL RECARGO DE TARJETAS
         private void consultarRecargoTarjeta()
         {
             try
             {
                 if (Program.iAplicaRecargoTarjeta == 1 || Program.iDescuentaIva == 1)
                 {
                     sSql = "";
                     sSql += "select recargo_tarjeta, remover_iva, porcentaje_iva" + Environment.NewLine;
                     sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                     sSql += "where estado = 'A'" + Environment.NewLine;
                     sSql += "and id_pedido = " + sIdOrden;

                     dtConsulta = new DataTable();
                     dtConsulta.Clear();

                     bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                     if (bRespuesta)
                     {
                         if (dtConsulta.Rows.Count > 0)
                         {
                             iBanderaRecargoBDD = Convert.ToInt32(dtConsulta.Rows[0]["recargo_tarjeta"].ToString());
                             iBanderaRemoverIvaBDD = Convert.ToInt32(dtConsulta.Rows[0]["remover_iva"].ToString());
                             iBanderaRecargoBoton = Convert.ToInt32(dtConsulta.Rows[0]["recargo_tarjeta"].ToString());
                             iBanderaRemoverIvaBoton = Convert.ToInt32(dtConsulta.Rows[0]["remover_iva"].ToString());
                             dbIVAPorcentaje = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_iva"].ToString());
                         }
                         else
                         {
                             iBanderaRecargoBDD = 0;
                             iBanderaRemoverIvaBDD = 0;
                             iBanderaRecargoBoton = 0;
                             iBanderaRemoverIvaBoton = 0;
                             dbIVAPorcentaje = Convert.ToDecimal(Program.iva * 100);
                         }

                         if (iBanderaRecargoBDD == 1)
                         {
                             btnRecargoTarjeta.AccessibleDescription = "REMOVER RECARGO";
                             btnRecargoTarjeta.Text = "REMOVER RECARGO";
                             btnRemoverIVA.Enabled = false;
                             btnPagoCompleto.Enabled = false;

                             string sValor_R = (Convert.ToDecimal((dTotal / (dbIVAPorcentaje / 100)).ToString("N2")) / (Program.dbPorcentajeRecargoTarjeta)).ToString("N2");
                             dbSumaIva = Convert.ToDecimal(sValor_R) * (dbIVAPorcentaje / 100);
                             dbSumaIva = Convert.ToDecimal(dbSumaIva.ToString("N2"));
                             dbTotalAyuda = Convert.ToDecimal(sValor_R) * (dbIVAPorcentaje / 100);
                             iEjecutarActualizacionTarjetas = 1;
                             cargarFormasPagosRecargo();
                             aplicaRecargoTarjetas();
                         }

                         else
                         {
                             if (iBanderaRemoverIvaBDD != 1)
                             {
                                 return;
                             }

                             btnRemoverIVA.BackColor = Color.Turquoise;
                             btnRemoverIVA.Text = "DEVOLVER IVA";
                             dbTotalAyuda += dbSumaIva;
                             iEjecutarActualizacionIVA = 1;
                             btnRecargoTarjeta.Enabled = false;
                             rdbNotaVenta.Checked = true;
                             rdbFactura.Enabled = false;
                             rdbNotaVenta.Enabled = false;
                         }
                     }

                     else
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                     }
                 }

                 else
                 {
                     iBanderaRecargoBDD = 0;
                     iBanderaRemoverIvaBDD = 0;
                     dbIVAPorcentaje = Convert.ToDecimal(Program.iva * 100);
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

        //FUNCION PARA ACTUALIZAR A PRECIOS ORIGINALES
         private bool actualizarPreciosOriginales()
         {
             try
             {
                 int iIdDetPedido_R;
                 Decimal dbValor_R;
                 Decimal dbValorDescuento_R;
                 Decimal dbValorIVA_R;

                 Decimal dbPorcentajeIva_R = Convert.ToDecimal(dtOriginal.Rows[0]["porcentaje_iva"].ToString()) / 100;

                 for (int i = 0; i < dtOriginal.Rows.Count; i++)
                 {
                     dbValor_R = Convert.ToDecimal(dtOriginal.Rows[i]["valor"].ToString());
                     dbValorDescuento_R = Convert.ToDecimal(dtOriginal.Rows[i]["valor_dscto"].ToString());
                     iIdDetPedido_R = Convert.ToInt32(dtOriginal.Rows[i]["id_det_pedido"].ToString());

                     if (Convert.ToInt32(dtOriginal.Rows[i]["paga_iva"].ToString()) == 1)
                     {
                         dbValorIVA_R = (dbValor_R - dbValorDescuento_R) * dbPorcentajeIva_R;
                     }

                     else
                     {
                         dbValorIVA_R = 0;
                     }
                     
                     //REVISION: 2019-12-05

                     sSql = "";
                     sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                     sSql += "precio_unitario = " + dbValor_R + "," + Environment.NewLine;
                     sSql += "valor_iva = " + dbValorIVA_R + Environment.NewLine;
                     sSql += "where id_det_pedido = " + iIdDetPedido_R;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
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

        //FUNCION PARA ACTUALIZAR LOS PRECIOS RECARGOS DE TARJETA
         private bool actualizarPreciosRecargo()
         {
             try
             {
                 int iIdDetPedido_R;
                 int iPagaIVA_R;

                 Decimal dbValor_R;
                 Decimal dbValorIVA_R;
                 Decimal dvValorRecargo_R;
                 Decimal dbValorSumaRecargo_P;
                 Decimal dbValorSumaRecargoDescuento_P;
                 Decimal dbValorDescuento_P;
                 Decimal dbValorRecargoDescuento_P;

                 Decimal dbPorcentajeIva_R = Convert.ToDecimal(dtOriginal.Rows[0]["porcentaje_iva"].ToString()) / 100;

                 for (int i = 0; i < dtOriginal.Rows.Count; i++)
                 {
                     dbValor_R = Convert.ToDecimal(dtOriginal.Rows[i]["valor"].ToString());
                     dbValorDescuento_P = Convert.ToDecimal(dtOriginal.Rows[i]["valor_dscto"].ToString());
                     iIdDetPedido_R = Convert.ToInt32(dtOriginal.Rows[i]["id_det_pedido"].ToString());
                     iPagaIVA_R = Convert.ToInt32(dtOriginal.Rows[i]["paga_iva"].ToString());

                     dvValorRecargo_R = dbValor_R * Program.dbPorcentajeRecargoTarjeta;
                     dbValorRecargoDescuento_P = dbValorDescuento_P * Program.dbPorcentajeRecargoTarjeta;
                     dbValorSumaRecargo_P = dbValor_R + dvValorRecargo_R;
                     dbValorSumaRecargoDescuento_P = dbValorRecargoDescuento_P + dbValorDescuento_P;

                     if (iPagaIVA_R == 1)
                     {
                         dbValorIVA_R = (dbValorSumaRecargo_P - dbValorSumaRecargoDescuento_P) * dbPorcentajeIva_R;
                     }

                     else
                     {
                         dbValorIVA_R = 0;
                     }

                     //REVISION: 2019-12-03
                     sSql = "";
                     sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                     sSql += "precio_unitario = " + dbValorSumaRecargo_P + "," + Environment.NewLine;
                     sSql += "valor_dscto = " + dbValorSumaRecargoDescuento_P + "," + Environment.NewLine;
                     sSql += "valor_iva = " + dbValorIVA_R + Environment.NewLine;
                     sSql += "where id_det_pedido = " + iIdDetPedido_R;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return false;
                     }
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

        //FUNCION PARA INSERTAR EL NUMERO DE LOTE EN LA TABLA POS_NUMERO_LOTE
        private bool insertarNumeroLote(string sNumeroLote_P, int iOperadorTarjeta_P)
         {
            try
            {
                sSql = "";
                sSql += "insert into pos_numero_lote (" + Environment.NewLine;
                sSql += "id_localidad, id_pos_jornada, id_pos_operador_tarjeta, lote," + Environment.NewLine;
                sSql += "fecha_apertura, estado_lote, id_pos_cierre_cajero, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", " + Program.iJORNADA + ", " + iOperadorTarjeta_P + ", ";
                sSql += "'" + sNumeroLote_P + "', '" + sFecha + "', 'Abierta', " + Program.iIdPosCierreCajero + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
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
        
        #endregion

        #region FUNCIONES DE LA CUENTAS POR COBRAR PENDIENTES

        //FUNCION PARA GENERAR LA CUENTA POR COBRAR
        private void generarCuentaPorCobrar()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "bandera_cuenta_por_cobrar = @bandera_cuenta_por_cobrar," + Environment.NewLine;
                sSql += "estado_orden = @estado_orden" + Environment.NewLine;
                sSql += "where id_pedido = @id_pedido" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@bandera_cuenta_por_cobrar";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 1;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_orden";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "Cerrada";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_pedido";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Convert.ToInt32(sIdOrden);

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

                //EJECUTAR LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "La cuenta por cobrar se registró con éxito";
                ok.ShowDialog();

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmCobros_Load(object sender, EventArgs e)
         {
             extraerListaMinorista();
             obtenerTotal();
             cargarInformacionCliente();
             cargarFormasPagos();
             datosFactura();
             llenarDetalleDeuda();
             valoresPrecuenta();

             dtTarjetasT = new DataTable();
             dtTarjetasT.Clear();
             dtTarjetasT = dtComanda.Copy();

             consultarRecargoTarjeta();
             //numeroLote();
             dSubtotal = 0;
             iBanderaGeneraFactura = 0;
             dbPorcentajeRecargo = Program.dbPorcentajeRecargoTarjeta;

             if (Program.iFacturacionElectronica == 1)
             {
                 configuracionFacturacion();
             }

             #region FUNCION PARA LAS COMANDAS SIN FACTURA
             //if (iGeneraFactura == 0)
             //{
             //    dgvPagos.Rows.Add();

             //    sSql = "";
             //    sSql += "select FC.id_pos_tipo_forma_cobro, FC.descripcion" + Environment.NewLine;
             //    sSql += "from cv403_cab_pedidos CP, pos_origen_orden OO," + Environment.NewLine;
             //    sSql += "pos_tipo_forma_cobro FC" + Environment.NewLine;
             //    sSql += "where CP.id_pos_origen_orden = OO.id_pos_origen_orden" + Environment.NewLine;
             //    sSql += "and OO.id_pos_tipo_forma_cobro = FC.id_pos_tipo_forma_cobro" + Environment.NewLine;
             //    sSql += "and CP.estado = 'A'" + Environment.NewLine;
             //    sSql += "and OO.estado = 'A'" + Environment.NewLine;
             //    sSql += "and FC.estado = 'A'" + Environment.NewLine;
             //    sSql += "and CP.id_pedido = " + sIdOrden;

             //    dtConsulta = new DataTable();
             //    dtConsulta.Clear();
             //    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

             //    if (bRespuesta == true)
             //    {
             //        if (dtConsulta.Rows.Count > 0)
             //        {
             //            dgvPagos.Rows[0].Cells["id"].Value = dtConsulta.Rows[0][0].ToString();
             //            dgvPagos.Rows[0].Cells["fpago"].Value = dtConsulta.Rows[0][1].ToString();
             //            dValor = dTotal;
             //            dgvPagos.Rows[0].Cells["valor"].Value = dValor.ToString("N2");
             //            dgvPagos.Rows[0].Cells["id_sri"].Value = "0";
             //            dgvPagos.Rows[0].Cells["conciliacion"].Value = "0";
             //            dgvPagos.Rows[0].Cells["id_operador_tarjeta"].Value = "0";
             //            dgvPagos.Rows[0].Cells["id_tipo_tarjeta"].Value = "0";
             //            dgvPagos.Rows[0].Cells["numero_lote"].Value = "";
             //            dgvPagos.Rows[0].Cells["bandera_insertar_lote"].Value = "0";

             //            //btnRemoverPago.Enabled = false;
             //        }
             //    }

             //    else
             //    {
             //        catchMensaje = new VentanasMensajes.frmMensajeCatch();
             //        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
             //        catchMensaje.ShowDialog();
             //        return;
             //    }

             //    grupoComprobantes.Visible = false;
             //    grupoEncabezadoFactura.Text = "Datos del beneficiario";
             //}

             //else
             //{
             //    llenarGrid();
             //    iBanderaGeneraFactura = 1;
             //}

             #endregion

             llenarGrid();
             iBanderaGeneraFactura = 1;

             for (int i = 0; i < dgvPagos.Rows.Count; i++)
             {
                 if (dgvPagos.Rows[i].Cells["fpago"].Value == null)
                 {
                     dgvPagos.Rows[i].Cells["fpago"].Value = 0;
                 }

                 dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value);
             }

             dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
             dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - dSubtotal).ToString("N2");
             dgvPagos.Columns[0].Visible = false;
             dgvPagos.ClearSelection();

             if (Program.iDescuentaIva == 1)
             {
                 btnRemoverIVA.Enabled = true;
                 btnPagoCompleto.Enabled = true;
             }

             else
             {
                 btnRemoverIVA.Enabled = false;
                 btnPagoCompleto.Enabled = false;
             }

             if (Program.iAplicaRecargoTarjeta == 1)
                 btnRecargoTarjeta.Enabled = true;
             else
                 btnRecargoTarjeta.Enabled = false;

             if (Program.iManejaPropinaSoloTarjetas == 1)
                 btnPropina.Enabled = false;
             else
                 btnPropina.Enabled = true;

             if (iBanderaComandaPendiente == 1)
                 btnCuentaPorCobrar.Enabled = false;
             else
                 btnCuentaPorCobrar.Enabled = true;
         }

         private void btnSiguiente_Click(object sender, EventArgs e)
         {
             btnAnterior.Enabled = true;
             crearBotonesFormasPagos();
         }

         private void btnAnterior_Click(object sender, EventArgs e)
         {
             iCuentaFormasPagos -= iCuentaAyudaFormasPagos;

             if (iCuentaFormasPagos <= 10)
             {
                 btnAnterior.Enabled = false;
             }

             btnSiguiente.Enabled = true;
             iCuentaFormasPagos -= 10;

             crearBotonesFormasPagos();
         }

         private void btnConsumidorFinal_Click(object sender, EventArgs e)
         {
             txtIdentificacion.Text = "9999999999999";
             txtApellidos.Text = "CONSUMIDOR FINAL";
             txtNombres.Text = "CONSUMIDOR FINAL";
             txtTelefono.Text = "9999999999";
             txtMail.Text = Program.sCorreoElectronicoDefault;
             txtDireccion.Text = "QUITO";
             iIdPersona = Program.iIdPersona;
             idTipoIdentificacion = 180;
             idTipoPersona = 2447;
             btnEditar.Visible = false;
         }

         private void btnBuscar_Click(object sender, EventArgs e)
         {
             Facturador.frmControlDatosCliente controlDatosCliente = new Facturador.frmControlDatosCliente();
             controlDatosCliente.ShowDialog();

             if (controlDatosCliente.DialogResult == DialogResult.OK)
             {
                 iIdPersona = controlDatosCliente.iCodigo;
                 txtIdentificacion.Text = controlDatosCliente.sIdentificacion;
                 consultarRegistro();
                 controlDatosCliente.Close();
             }
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

         private void btnEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             Facturador.frmNuevoCliente nuevoCliente = new Facturador.frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
             nuevoCliente.ShowDialog();

             if (nuevoCliente.DialogResult == DialogResult.OK)
             {
                 iIdPersona = nuevoCliente.iCodigo;
                 txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                 consultarRegistro();
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

         private void btnRemoverPago_Click(object sender, EventArgs e)
         {
             if (dgvPagos.Rows.Count == 0)
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "No hay formas de pago ingresados para remover del registro";
                 ok.ShowDialog();
             }

             else if (dgvPagos.SelectedRows.Count > 0)
             {
                 if (Program.iPuedeCobrar == 1)
                 {
                     dgvPagos.Rows.Remove(dgvPagos.CurrentRow);

                     dSubtotal = 0;

                     for (int i = 0; i < dgvPagos.Rows.Count; i++)
                     {
                         dSubtotal += Convert.ToDecimal(dgvPagos.Rows[i].Cells[2].Value.ToString());
                     }

                     dgvDetalleDeuda.Rows[0].Cells[1].Value = dSubtotal.ToString("N2");
                     dgvDetalleDeuda.Rows[1].Cells[1].Value = (dTotal - Convert.ToDecimal(dgvDetalleDeuda.Rows[0].Cells[1].Value)).ToString("N2");

                     if (dTotal > dSubtotal)        //AQUI REVISAR LA CONDICION
                     {
                         dgvDetalleDeuda.Rows[2].Cells[1].Value = "0.00";
                     }

                     else
                     {
                         dgvDetalleDeuda.Rows[2].Cells[1].Value = (dSubtotal - dTotal).ToString("N2");
                     }

                     if (dgvPagos.Rows.Count == 0)
                     {
                         dgvDetalleDeuda.Rows[3].Cells[1].Value = "0.00";
                     }

                     dgvPagos.ClearSelection();
                 }

                 else
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Su usuario no le permite remover el ítem. Póngase en contacto con el administrador.";
                     ok.ShowDialog();
                 }
             }

             else
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "No se ha seleccionado una línea para remover.";
                 ok.ShowDialog();
             }
         }

         private void btnSalir_Click(object sender, EventArgs e)
         {
             this.DialogResult = DialogResult.OK;

             //if (Program.iBanderaCerrarVentana == 0)
             //{
             //    ord = Owner as Orden;
             //    ord.Close();
             //}

             //else
             //{
             //    Program.iBanderaCerrarVentana = 0;
             //}
         }

         private void btnCrearCliente_Click(object sender, EventArgs e)
         {
             Facturador.frmNuevoCliente frmNuevoCliente = new Facturador.frmNuevoCliente("", false);
             frmNuevoCliente.ShowDialog();

             if (frmNuevoCliente.DialogResult == DialogResult.OK)
             {
                 iIdPersona = frmNuevoCliente.iCodigo;
                 txtIdentificacion.Text = frmNuevoCliente.sIdentificacion;
                 consultarRegistro();
             }
         }

         private void btnGrabarPagos_Click(object sender, EventArgs e)
         {
             try
             {
                 iOpCambiarEstadoOrden = 0;

                 if (extraerFecha() == false)
                 {
                     return;
                 }

                 iCerrarCuenta = 1;

                 if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
                 {
                     sSql = "";
                     sSql += "select count(*) cuenta" + Environment.NewLine;
                     sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                     sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                     sSql += "and estado = 'A'";

                     dtConsulta = new DataTable();
                     dtConsulta.Clear();

                     bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                     if (bRespuesta == true)
                     {
                         if (Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) == 0)
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
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                     }
                 }

                 else
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "No se ha realizado el cobro total de la orden.";
                     ok.ShowDialog();
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
             }
         }

         private void btnFacturar_Click(object sender, EventArgs e)
         {
             if (txtIdentificacion.Text == "" && txtApellidos.Text == "")
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "Favor ingrese los datos del cliente para la factura.";
                 ok.ShowDialog();
                 txtIdentificacion.Focus();
                 return;
             }

             if (Program.iFacturacionElectronica == 1)
             {
                 if (txtMail.Text.Trim() == "")
                 {
                     ok = new VentanasMensajes.frmMensajeOK();
                     ok.LblMensaje.Text = "Debe ingresar un correo electrónico para enviar el comprobante electrónico.";
                     ok.ShowDialog();
                     btnCorreoElectronicoDefault.Focus();
                     return;
                 }
             }

             if (Convert.ToDouble(dgvDetalleDeuda.Rows[1].Cells[1].Value) == 0)
             {
                 crearPagosFactura();
             }

             else
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "No se ha realizado el cobre total de la comanda.";
                 ok.ShowDialog();
             }
         }

         private void frmCobros_KeyDown(object sender, KeyEventArgs e)
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

         private void btnImprimir_Click(object sender, EventArgs e)
         {
             try
             {
                 if (extraerFecha() == false)
                 {
                     return;
                 }

                 if (dgvPagos.Rows.Count == 0)
                 {
                     //INICIAMOS UNA NUEVA TRANSACCION
                     if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                     {
                         ok = new VentanasMensajes.frmMensajeOK();
                         ok.LblMensaje.Text = "Error al abrir transacción";
                         ok.ShowDialog();
                         return;
                     }

                     if (eliminarFormasPagos() ==false)
                     {
                         conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                         return;
                     }

                     sSql = "";
                     sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                     sSql += "estado_orden = 'Pre-Cuenta' " + Environment.NewLine;
                     sSql += "where id_pedido = " + sIdOrden;

                     if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                     }

                     conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                     Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(sIdOrden, 1, "Pre-Cuenta");
                     precuenta.ShowDialog();
                 }

                 else
                 {
                     sSql = "";
                     sSql += "select count(*) cuenta" + Environment.NewLine;
                     sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                     sSql += "where id_pedido = " + sIdOrden + Environment.NewLine;
                     sSql += "and estado = 'A'";

                     dtConsulta = new DataTable();
                     dtConsulta.Clear();

                     bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                     if (bRespuesta == true)
                     {
                         if (Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) == 0)
                         {
                             if (insertarPagoNuevoPrecuenta() == false)
                             {
                                 conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                                 return;
                             }
                         }

                         else if (cambiarFormasPagosPrecuenta(1) == false)
                         {
                             conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                             return;
                         }

                         frmVerPrecuentaTextBox precuenta = new frmVerPrecuentaTextBox(sIdOrden, 1, "Pre-Cuenta");
                         precuenta.ShowDialog();
                         return;
                     }

                     else
                     {
                         catchMensaje = new VentanasMensajes.frmMensajeCatch();
                         catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                         catchMensaje.ShowDialog();
                         return;
                     }
                 }
             }

             catch (Exception ex)
             {
                 catchMensaje = new VentanasMensajes.frmMensajeCatch();
                 catchMensaje.LblMensaje.Text = ex.Message;
                 catchMensaje.ShowDialog();
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
                 rdbNotaVenta.Checked = true;
                 rdbFactura.Enabled = false;
                 rdbNotaVenta.Enabled = false;
                 Program.iSeleccionarNotaVenta = 1;
                 iBanderaRemoverIvaBoton = 1;
                 iBanderaRemoverIvaBDD = 1;
             }
             else
             {
                 btnRemoverIVA.BackColor = Color.SpringGreen;
                 btnRemoverIVA.Text = "REMOVER IVA";
                 dTotal = dbTotalAyuda;
                 iEjecutarActualizacionIVA = 0;
                 btnRecargoTarjeta.Enabled = true;
                 rdbFactura.Checked = true;
                 rdbFactura.Enabled = true;
                 rdbNotaVenta.Enabled = true;
                 Program.iSeleccionarNotaVenta = 0;
                 iBanderaRemoverIvaBoton = 0;
                 iBanderaRemoverIvaBDD = 0;
             }

             iBanderaRecargoBoton = 0;
             lblTotal.Text = "$ " + dTotal.ToString("N2");
             dgvPagos.Rows.Clear();

             dgvDetalleDeuda.Rows.Clear();
             dgvDetalleDeuda.Rows.Add("ABONO", "0.00");
             dgvDetalleDeuda.Rows.Add("SALDO", dTotal.ToString("N2"));
             dgvDetalleDeuda.Rows.Add("CAMBIO", "0.00");
             dgvDetalleDeuda.Rows.Add("PROPINA", "0.00");
             dgvDetalleDeuda.ClearSelection();
         }

         private void btnRecargoTarjeta_Click(object sender, EventArgs e)
         {
             if (btnRecargoTarjeta.AccessibleDescription == "RECARGO TARJETAS")
             {
                 btnRecargoTarjeta.AccessibleDescription = "REMOVER RECARGO";
                 btnRecargoTarjeta.Text = "REMOVER RECARGO";
                 dbSubTotalRecargo = dbTotalAyuda - dbSumaIva;
                 dbValorRecargo = dbSubTotalRecargo * dbPorcentajeRecargo;
                 dbSubTotalNetoRecargo = dbSubTotalRecargo + dbValorRecargo;
                 dbIVARecargo = dbSubTotalNetoRecargo * Convert.ToDecimal(Program.iva);
                 dbTotalRecargo = dbSubTotalNetoRecargo + dbIVARecargo;
                 dTotal = dbTotalRecargo;
                 iEjecutarActualizacionTarjetas = 1;
                 btnRemoverIVA.Enabled = false;
                 btnPagoCompleto.Enabled = false;
                 cargarFormasPagosRecargo();
                 aplicaRecargoTarjetas();
                 iBanderaRecargoBoton = 1;
                 iBanderaRecargoBDD = 1;
             }

             else
             {
                 btnRecargoTarjeta.AccessibleDescription = "RECARGO TARJETAS";
                 btnRecargoTarjeta.Text = "RECARGO TARJETAS";
                 dTotal = dbTotalAyuda;
                 btnRemoverIVA.Enabled = true;
                 btnPagoCompleto.Enabled = true;
                 iEjecutarActualizacionTarjetas = 0;
                 cargarFormasPagos();
                 iBanderaRecargoBoton = 0;
                 iBanderaRecargoBDD = 0;
             }

             iBanderaRemoverIvaBoton = 0;
             lblTotal.Text = "$ " + dTotal.ToString("N2");
             dgvPagos.Rows.Clear();
             
             dgvDetalleDeuda.Rows.Clear();
             dgvDetalleDeuda.Rows.Add("ABONO", "0.00");
             dgvDetalleDeuda.Rows.Add("SALDO", dTotal.ToString("N2"));
             dgvDetalleDeuda.Rows.Add("CAMBIO", "0.00");
             dgvDetalleDeuda.Rows.Add("PROPINA", "0.00");
             dgvDetalleDeuda.ClearSelection();
         }

         private void btnPagoCompleto_Click(object sender, EventArgs e)
         {
             frmEfectivoPagoCompleto efectivoPagoCompleto = new frmEfectivoPagoCompleto(sIdOrden, Convert.ToDouble(dTotal), iBanderaComandaPendiente);
             efectivoPagoCompleto.ShowDialog();

             if (efectivoPagoCompleto.DialogResult == DialogResult.OK)
             {
                 this.DialogResult = DialogResult.OK;
                 this.Close();
             }
         }

         private void btnCorreoElectronicoDefault_Click(object sender, EventArgs e)
         {
             //txtMail.Text = Program.sCorreoElectronicoDefault;
             if (btnCorreoElectronicoDefault.AccessibleName == "0")
             {
                 sCorreoAyuda = txtMail.Text.Trim();
                 btnCorreoElectronicoDefault.AccessibleName = "1";
                 txtMail.ReadOnly = false;
                 txtMail.Focus();
             }

             else
             {
                 txtMail.Text = sCorreoAyuda;
                 btnCorreoElectronicoDefault.AccessibleName = "0";
                 txtMail.ReadOnly = true;
                 btnCorreoElectronicoDefault.Focus();
             }
         }

         private void btnDividirPrecio_Click(object sender, EventArgs e)
         {
             tecladoNumericoDividirPrecio teclado = new tecladoNumericoDividirPrecio(dTotal.ToString());
             teclado.ShowDialog();
         }

         private void btnPropina_Click(object sender, EventArgs e)
         {
             Propina.frmPropina propina = new Propina.frmPropina();
             propina.ShowDialog();

             if (propina.DialogResult == DialogResult.OK)
             {
                 Decimal dbPropina_P = propina.dbPropina;
                 dgvDetalleDeuda.Rows[3].Cells[1].Value = dbPropina_P.ToString("N2");
                 propina.Close();
             }
         }

         private void btnCuentaPorCobrar_Click(object sender, EventArgs e)
         {
             if (iIdPersona == Program.iIdPersona)
             {
                 ok = new VentanasMensajes.frmMensajeOK();
                 ok.LblMensaje.Text = "La cuenta por cobrar debe ser ingresada con datos del cliente.";
                 ok.ShowDialog();
                 return;
             }

             NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
             NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea dejar la cuenta pendiente de cobro?";
             NuevoSiNo.ShowDialog();

             if (NuevoSiNo.DialogResult == DialogResult.OK)
             {
                 generarCuentaPorCobrar();
             }
         }
    }
}
