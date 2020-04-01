using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmFacturarClienteEmpresarial : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sIdentificacion;
        string sCliente;
        string sDireccion;
        string sTelefono;
        string sCorreoElectronico;
        string sCiudadFactura;
        string sFecha;
        string sClaveAcceso;
        string sEstablecimiento;
        string sPuntoEmision;
        string sNumeroFactura;
        string sNumeroPago;
        string sCampo;
        string sTabla;

        DataTable dtConsulta;
        DataTable dtPedidos;

        DateTime dFechaInicio;
        DateTime dFechaFinal;

        bool bRespuesta;

        int iIdPersona;
        int iCantidad;
        int iIdTipoFormaCobro;
        int iIdFormaPago;
        int iIdLocalidadImpresora;
        int iFacturaElectronica_P;
        int iIdTipoEmision;
        int iIdTipoAmbiente;
        int iIdFactura;
        int iIdPedido;
        int iIdDocumentoCobrar;
        int iIdPago;
        int iCgTipoDocumento;

        Decimal dbTotal;
        Decimal dbTotalPorCuenta;

        long iMaximo;

        public frmFacturarClienteEmpresarial()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR DATOS DE LA MONEDA
        private void llenarComboEmpresas()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select CE.id_persona, ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) cliente" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CE.estado = 'A'" + Environment.NewLine;
                sSql += "order by TP.apellidos";

                cmbClienteEmpresarial.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();
                txtTotal.Text = "0.00";

                sSql = "";
                sSql += "select nombre, sum(cantidad) cantidad," + Environment.NewLine;
                sSql += "ltrim(str(isnull(precio_unitario + valor_iva - valor_dscto + valor_otro, 0), 10, 2)) valor_unitario," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(cantidad * (precio_unitario + valor_iva - valor_dscto + valor_otro)), 0), 10, 2)) valor_total" + Environment.NewLine;
                sSql += "from pos_vw_items_cliente_empresarial" + Environment.NewLine;
                sSql += "where fecha_pedido between '" + dFechaInicio.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + dFechaFinal.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "group by nombre, precio_unitario, valor_iva, valor_dscto, valor_otro";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros seleccionados.";
                    ok.ShowDialog();
                    return;
                }

                dbTotal = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    iCantidad = Convert.ToInt32(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbTotal += Convert.ToDecimal(dtConsulta.Rows[i]["valor_total"].ToString());

                    dgvDatos.Rows.Add(iCantidad.ToString(), dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["valor_unitario"].ToString().Trim(),
                                      dtConsulta.Rows[i]["valor_total"].ToString().Trim());
                }

                txtTotal.Text = dbTotal.ToString("N2");

                dgvDatos.ClearSelection();

                sSql = "";
                sSql += "select * from pos_vw_pedidos_cliente_empresarial" + Environment.NewLine;
                sSql += "where fecha_pedido between '" + dFechaInicio.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + dFechaFinal.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona;

                dtPedidos = new DataTable();
                dtPedidos.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPedidos, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LA CLAVE DE ACCESO
        private string sGenerarClaveAcceso()
        {
            //GENERAR CLAVE DE ACCESO
            string sClaveAcceso_R = "";
            string sFecha_R = Program.sFechaSistema.ToString("ddMMyyyy");
            string TipoComprobante = "01";
            string NumeroRuc = Program.sNumeroRucEmisor;
            string TipoAmbiente = Program.iTipoAmbiente.ToString();
            string TipoEmision = Program.iTipoEmision.ToString();
            string Serie = sEstablecimiento + sPuntoEmision;
            string NumeroComprobante = sNumeroFactura.PadLeft(9, '0');
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

        //FUNCION PARA CARGAR LA CONFIGURACION DE LA FACTURACION ELECTRONICA
        private bool configuracionFacturacion()
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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR LA FACTURA
        private bool insertarFactura()
        {
            try
            {
                //SELECCIONAR LOS DATOS DE LA LOCALIDAD
                //----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select L.establecimiento, L.punto_emision, " + Environment.NewLine;
                sSql += "P.numero_factura, P.numero_pago, P.id_localidad_impresora" + Environment.NewLine;
                sSql += "from tp_localidades L, tp_localidades_impresoras P " + Environment.NewLine;
                sSql += "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql += "and L.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and L.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sEstablecimiento = dtConsulta.Rows[0]["establecimiento"].ToString();
                sPuntoEmision = dtConsulta.Rows[0]["punto_emision"].ToString();
                sNumeroFactura = dtConsulta.Rows[0]["numero_factura"].ToString();
                sNumeroPago = dtConsulta.Rows[0]["numero_pago"].ToString();
                iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad_impresora"].ToString());

                //SELECCIONAR LOS DATOS DEL CLIENTE
                //----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "SELECT TP.identificacion, isnull(TP.nombres, '') nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion direccion_cliente," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(TT.domicilio, TT.oficina) telefono_domicilio, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE TP.id_persona = " + iIdPersona;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sIdentificacion = dtConsulta.Rows[0]["identificacion"].ToString();
                sCliente = (dtConsulta.Rows[0]["nombres"].ToString().Trim().ToUpper() + " " + dtConsulta.Rows[0]["apellidos"].ToString().Trim().ToUpper()).Trim();
                sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim().ToLower();
                sDireccion = dtConsulta.Rows[0]["direccion_cliente"].ToString().Trim().ToUpper();
                sCiudadFactura = dtConsulta.Rows[0]["direccion"].ToString().Trim().ToUpper();

                if (dtConsulta.Rows[0]["telefono_domicilio"].ToString().Trim() != "")
                {
                    sTelefono = dtConsulta.Rows[0]["telefono_domicilio"].ToString().Trim();
                }

                else
                {
                    dtConsulta.Rows[0]["celular"].ToString().Trim();
                }

                //SELECCIONAR LOS DATOS DEL METODO PAGO
                //----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "select FC.id_pos_metodo_pago, FPA.id_forma_pago, FC.cg_tipo_documento" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                sSql += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                sSql += "and MP.estado = 'A'" + Environment.NewLine;
                sSql += "and FC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "sri_forma_pago SFP ON SFP.id_sri_forma_pago = MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and SFP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_formas_pagos FPA ON SFP.id_sri_forma_pago = FPA.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and FPA.estado = 'A'" + Environment.NewLine;
                sSql += "where MP.codigo = 'CR'" + Environment.NewLine;
                sSql += "and FPA.id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.lblMensaje.Text = "No se encuentra configurado el registro para cobros. Comuníquese con el administrador.";
                    ok.ShowDialog();
                    return false;
                }

                iIdTipoFormaCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_metodo_pago"].ToString());
                iIdFormaPago = Convert.ToInt32(dtConsulta.Rows[0]["id_forma_pago"].ToString());
                iCgTipoDocumento = Convert.ToInt32(dtConsulta.Rows[0]["cg_tipo_documento"].ToString());

                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                iFacturaElectronica_P = 0;

                if (Program.iFacturacionElectronica == 1)
                {
                    iFacturaElectronica_P = 1;

                    if (configuracionFacturacion() == false)
                    {
                        return false;
                    }

                    sClaveAcceso = sGenerarClaveAcceso();
                }

                //INICIAMOS UNA NUEVA TRANSACCION
                //------------------------------------------------------------------------------------------------------------------
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_FACTURAS
                //------------------------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "id_localidad, idformulariossri, id_vendedor, id_forma_pago, id_forma_pago2, id_forma_pago3," + Environment.NewLine;
                sSql += "fecha_factura, fecha_vcto, cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, " + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " + Environment.NewLine;
                sSql += "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico, servicio," + Environment.NewLine;
                sSql += "facturaelectronica, id_tipo_emision, id_tipo_ambiente, clave_acceso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + "," + Environment.NewLine;
                sSql += "1," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", " + Program.iIdVendedor + ", " + iIdFormaPago + ", " + Environment.NewLine;
                sSql += "null, null, '" + sFecha + "', '" + sFecha + "', " + Program.iMoneda + ", " + dbTotal + ", 0, 0, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0," + Environment.NewLine;
                sSql += "'" + sDireccion + "', '" + sTelefono + "', '" + sCiudadFactura + "'," + Environment.NewLine;
                sSql += "'" + sCorreoElectronico + "', 0, " + iFacturaElectronica_P + ", " + iIdTipoEmision + ", " + iIdTipoAmbiente + "," + Environment.NewLine;

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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sTabla = "cv403_facturas";
                sCampo = "id_factura";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdFactura = Convert.ToInt32(iMaximo);

                //INSERTAR EN LA TABLA CV403_NUMEROS_FACTURAS
                //-----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", 1, " + sNumeroFactura + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0 )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //ACTUALIZAR LOS NUMEROS EN TP_LOCALIDADES_IMPRESORAS
                //-----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_factura = numero_factura + 1,"+ Environment.NewLine;
                sSql += "numero_pago = numero_pago + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_PAGOS
                //------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_pagos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, fecha_pago, cg_moneda, valor," + Environment.NewLine;
                sSql += "propina, cg_empresa, id_localidad, cg_cajero, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, " + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica,cambio) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", '" + sFecha + "', " + Program.iMoneda + "," + Environment.NewLine;
                sSql += dbTotal + ", 0, " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", 7799, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A' , 1, 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sTabla = "cv403_pagos";
                sCampo = "id_pago";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdPago = Convert.ToInt32(iMaximo);

                //INSERTAR EN LA TABLA CV403_NUMEROS_PAGOS
                //-----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_numeros_pagos (" + Environment.NewLine;
                sSql += "id_pago, serie, numero_pago, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", 'A', " + sNumeroPago + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR EN LA TABLA CV403_DOCUMENTOS_PAGOS
                //-----------------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "insert into cv403_documentos_pagos (" + Environment.NewLine;
                sSql += "id_pago, cg_tipo_documento, numero_documento, fecha_vcto, " + Environment.NewLine;
                sSql += "cg_moneda, cotizacion, valor, id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, valor_recibido)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPago + ", " + iCgTipoDocumento + ", 9999, '" + sFecha + "', " + Environment.NewLine;
                sSql += Program.iMoneda + ", 1, " + dbTotal + ", " + iIdTipoFormaCobro + ", " + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0, " + dbTotal + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtPedidos.Rows.Count; i++)
                {
                    iIdPedido = Convert.ToInt32(dtPedidos.Rows[i]["id_pedido"].ToString());
                    iIdDocumentoCobrar = Convert.ToInt32(dtPedidos.Rows[i]["id_documento_cobrar"].ToString());
                    dbTotalPorCuenta = Convert.ToDecimal(dtPedidos.Rows[i]["valor"].ToString());

                    //INSERTAR EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                    //-----------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "insert into cv403_documentos_pagados (" + Environment.NewLine;
                    sSql += "id_documento_cobrar, id_pago, valor," + Environment.NewLine;
                    sSql += "estado, numero_replica_trigger,numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDocumentoCobrar + ", " + iIdPago + ", " + dbTotalPorCuenta + ", 'A', 1, 0, " + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
                    //-----------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "insert into cv403_facturas_pedidos (" + Environment.NewLine;
                    sSql += "id_factura, id_pedido, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdFactura + ", " + iIdPedido + ", GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                    //-----------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                    sSql += "id_factura = " + iIdFactura + "," + Environment.NewLine;
                    sSql += "cg_estado_dcto = 7461," + Environment.NewLine;
                    sSql += "numero_documento = " + sNumeroFactura + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR EN LA TABLA CV403_CAB_PEDIDOS
                    //-----------------------------------------------------------------------------------------------------
                    sSql = "";
                    sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                    sSql += "estado_orden = 'Pagada'," + Environment.NewLine;
                    sSql += "fecha_cierre_orden = GETDATE()" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void frmFacturarClienteEmpresarial_Load(object sender, EventArgs e)
        {
            llenarComboEmpresas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbClienteEmpresarial.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione la empresa.";
                ok.ShowDialog();
                cmbClienteEmpresarial.Focus();
                return;
            }

            dFechaInicio = Convert.ToDateTime(txtDesde.Text.Trim());
            dFechaFinal = Convert.ToDateTime(txtHasta.Text.Trim());

            if (dFechaInicio > dFechaFinal)
            {
                ok.lblMensaje.Text = "El rango de fecha no se encuentra definido correctamente.";
                ok.ShowDialog();
                txtDesde.Focus();
                return;
            }

            iIdPersona = Convert.ToInt32(cmbClienteEmpresarial.SelectedValue);
            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            txtTotal.Text = "0.00";
            cmbClienteEmpresarial.SelectedIndex = 0;
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.lblMensaje.Text = "No se encuentran registros para generar una factura.";
                ok.ShowDialog();
                return;
            }

            SiNo.lblMensaje.Text = "¿Está seguro que desea generar la factura?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (insertarFactura() == true)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "Factura generada éxitosamente.";
                    ok.ShowDialog();
                    return;
                }

                else
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFacturarClienteEmpresarial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
