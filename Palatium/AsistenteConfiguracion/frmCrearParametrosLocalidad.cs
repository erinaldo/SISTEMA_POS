using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.AsistenteConfiguracion
{
    public partial class frmCrearParametrosLocalidad : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseCargarParametros parametros = new Clases.ClaseCargarParametros();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        DataTable dtConsulta;
        DataTable dtAyuda;

        bool bRespuesta;

        SqlParameter[] parametro;

        int iIdLocalidad;
        int iDestinosImpresion;
        int iManejaJornadas;
        int iManejaMesas;
        int iManejaLlevar;
        int iManejaDomicilio;
        int iManejaMenuExpress;
        int iManejaCortesia;
        int iManejaCanjes;
        int iManejaValeFuncionario;
        int iManejaConsumoEmpleados;
        int iImprimirDatosFactura;
        int iIdProductoAnulacion;
        int iEjecutarImpresiones;
        int iAbrirCajonDinero;
        int iDescargaReceta;
        int iDescargaProductoNoProcesado;
        int iManejaPromotor;
        int iManejaRepartidor;
        int iReimprimirCocina;
        int iMostrarValoresPropina;
        int iManejaMitad;
        int iManejaProductosComisionEmpleados;
        int iProductosComisionEmpleados;
        int iManejaDeliveryVariable;
        int iManejaVentaRapida;
        int iManejaClienteEmpresarial;
        int iManejaTarjetaAlmuerzo;
        int iManejaConsumoInterno;
        int iPropinaParaTarjetas;
        int iLeerMeseroMesas;
        int iImprimirPrecuentaGuardar;
        int iManejaFacturacionElectronica;
        int iConfiguracionDecimales;
        int iManejaNotaEntrega;
        int iSeleccionMeseroLlevar;
        int iVistaPreviaImpresiones;
        int iDescuentaIva;
        int iManejaAlmuerzos;
        int iAplicaRecargoTarjetas;

        public frmCrearParametrosLocalidad()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR DATOS DE LA LOCALIDAD
        private void llenarComboLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidad.DisplayMember = "nombre_localidad";
                    cmbLocalidad.ValueMember = "id_localidad";
                    cmbLocalidad.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION  PARA CARGAR LOS CONTROLES DB AYUDA
        private void cargarDbAyuda()
        {
            try
            {
                //DBAYUDA CIUDADES
                sSql = "";
                sSql += "select * from tp_vw_ciudad";
                dBAyudaCiudad.Ver(sSql, "valor_texto", 0, 2, 1);

                //DBAYUDA CAJEROS
                sSql = "";
                sSql += "select id_pos_cajero, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";
                dBAyudaCajero.Ver(sSql, "descripcion", 0, 2, 1);

                if (dBAyudaCajero.dtConsulta.Rows.Count == 0)
                    btnAgregarCajero.Visible = true;
                else
                    btnAgregarCajero.Visible = false;

                //DBAYUDA MESEROS
                sSql = "";
                sSql += "select id_pos_mesero, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_mesero" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";
                dBAyudaMesero.Ver(sSql, "descripcion", 0, 2, 1);

                if (dBAyudaMesero.dtConsulta.Rows.Count == 0)
                    btnAgregarMesero.Visible = true;
                else
                    btnAgregarMesero.Visible = false;

                //DBAYUDA PROMOTORES
                sSql = "";
                sSql += "select id_pos_promotor, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_promotor" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";
                dBAyudaPromotor.Ver(sSql, "descripcion", 0, 2, 1);

                if (dBAyudaPromotor.dtConsulta.Rows.Count == 0)
                    btnAgregarPromotor.Visible = true;
                else
                    btnAgregarPromotor.Visible = false;

                //DBAYUDA REPARTIDORES
                sSql = "";
                sSql += "select id_pos_repartidor, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_repartidor" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";
                dbAyudaRepartidor.Ver(sSql, "descripcion", 0, 2, 1);

                if (dbAyudaRepartidor.dtConsulta.Rows.Count == 0)
                    btnAgregarRepartidor.Visible = true;
                else
                    btnAgregarRepartidor.Visible = false;

                //DBAYUDA PERSONAS
                sSql = "";
                sSql += "select id_persona, apellidos + ' ' + isnull(nombres, '') nombre, identificacion" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";
                dBAyudaConsumidorFinal.Ver(sSql, "identificacion", 0, 2, 1);

                //DBAYUDA VENDEDORES
                sSql = "";
                sSql += "select id_vendedor, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from cv403_vendedores" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                dBAyudaVendedor.Ver(sSql, "descripcion", 0, 2, 1);

                //DBAYUDA PRODUCTO ANULADO
                sSql = "";
                sSql += "select P.id_producto, P.codigo, NP.nombre, PP.valor" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP, cv403_precios_productos PP" + Environment.NewLine;
                sSql += "where NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and PP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.nivel = 3" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4" + Environment.NewLine;
                sSql += "order by NP.nombre";
                dBAyudaProducto.Ver(sSql, "NP.nombre", 0, 1, 2);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CARGAR DATOS DE LA MONEDA
        private void llenarComboMoneda()
        {
            try
            {
                sSql = "select * from tp_vw_moneda";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["correlativo"] = "0";
                    row["valor_texto"] = "Seleccione la moneda...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbMoneda.DisplayMember = "valor_texto";
                    cmbMoneda.ValueMember = "correlativo";
                    cmbMoneda.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CARGAR DATOS DE PRECUENTAS
        private void llenarComboPrecuenta()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_formato_precuenta, descripcion" + Environment.NewLine;
                sSql += "from pos_formato_precuenta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                        btnAgregarPrecuentas.Visible = true;
                    else
                        btnAgregarPrecuentas.Visible = false;

                    DataRow row = dtConsulta.NewRow();
                    row["id_pos_formato_precuenta"] = "0";
                    row["descripcion"] = "Seleccione el formato de precuenta...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbPrecuenta.DisplayMember = "descripcion";
                    cmbPrecuenta.ValueMember = "id_pos_formato_precuenta";
                    cmbPrecuenta.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CARGAR DATOS DE FACTURAS
        private void llenarComboFactura()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_formato_factura, descripcion" + Environment.NewLine;
                sSql += "from pos_formato_factura" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                        btnAgregarFacturas.Visible = true;
                    else
                        btnAgregarFacturas.Visible = false;

                    DataRow row = dtConsulta.NewRow();
                    row["id_pos_formato_factura"] = "0";
                    row["descripcion"] = "Seleccione el formato de factura...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbFactura.DisplayMember = "descripcion";
                    cmbFactura.ValueMember = "id_pos_formato_factura";
                    cmbFactura.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE IMPRESORAS
        private void llenarComboImpresoras()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_impresora," + Environment.NewLine;
                sSql += "descripcion + ' - (' + path_url + ')' as impresora" + Environment.NewLine;
                sSql += "from pos_impresora" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["id_pos_impresora"] = "0";
                    row["impresora"] = "Seleccione impresora de reportes...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbImpresoras.DisplayMember = "impresora";
                    cmbImpresoras.ValueMember = "id_pos_impresora";
                    cmbImpresoras.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBOBOX DE TIPO DE COMPROBANTE
        private void llenarComboComprobantes()
        {
            try
            {
                sSql = "";
                sSql += "select idtipocomprobante, descripcion" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and codigo in ('Fac', 'Nen')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["idtipocomprobante"] = "0";
                    row["descripcion"] = "Seleccione tipo comprobante por defecto...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbTipoComprobantes.DisplayMember = "descripcion";
                    cmbTipoComprobantes.ValueMember = "idtipocomprobante";
                    cmbTipoComprobantes.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION CANCELAR
        private void cancelar()
        {
            iIdLocalidad = 0;

            chkDomicilio.Checked = false;
            chkCortesias.Checked = false;
            chkFuncionarios.Checked = false;
            chkConsumoEmpleados.Checked = false;
            chkMenuExpress.Checked = false;
            chkCanjes.Checked = false;
            chkVentaRapida.Checked = false;
            chkClienteEmpresarial.Checked = false;
            chkTarjetaAlmuerzo.Checked = false;
            chkConsumoInterno.Checked = false;

            dBAyudaCiudad.limpiar();
            dBAyudaCajero.limpiar();
            dBAyudaMesero.limpiar();
            dBAyudaPromotor.limpiar();
            dbAyudaRepartidor.limpiar();
            dBAyudaConsumidorFinal.limpiar();
            dBAyudaVendedor.limpiar();
            dBAyudaProducto.limpiar();

            chkDestinosImpresion.Checked = false;
            chkJornada.Checked = false;
            chkEjecutarImpresiones.Checked = false;
            chkAbrirCajon.Checked = false;
            chkNoProcesados.Checked = false;
            chkUsarRecetas.Checked = false;
            chkManejaPromotor.Checked = false;
            chkManejaRepartidor.Checked = false;
            chkReimprimirCocina.Checked = false;
            chkMostrarPropinas.Checked = false;
            chkMitad.Checked = false;
            chkProductosComision.Checked = false;
            chkDeliveryVariable.Checked = false;
            chkImprimeDatosFactura.Checked = false;
            chkPropinaTarjetas.Checked = false;

            cmbMoneda.SelectedValue = 0;
            cmbPrecuenta.SelectedValue = 0;
            cmbFactura.SelectedValue = 0;
            cmbImpresoras.SelectedValue = 0;
            cmbTipoComprobantes.SelectedValue = 0;
            txtValorComision.Text = "0.00";

            txtCantidadImpresiones.Text = "0";
            txtCantidadVentaExpress.Text = "0";
            txtCantidadCrearTarjetas.Text = "0";
            chkAplicaRecargoTarjetas.Checked = false;
            txtPorcentajeRecargoTarjetas.Text = "0";
            txtMontoMaximoRecargoTarjetas.Text = "0";
            txtPorcentajeRecargoTarjetas.ReadOnly = true;
            txtMontoMaximoRecargoTarjetas.ReadOnly = true;
            txtPorcentajeDescuentoEmpleados.Text = "0";
            txtPersonasDefault.Text = "0";
            txtRutaLogo.Clear();
            txtRutaReportes.Clear();
            txtCorreoPorDefecto.Clear();

            chkLeerMeseroMesas.Checked = false;
            chkImprimirPrecuentaGuardar.Checked = false;
            chkFacturacionElectronica.Checked = false;
            chkUsoNotasEntrega.Checked = false;
            chkSeleccionMeseroLlevar.Checked = false;
            chkVistaPreviaImpresiones.Checked = false;
            chkReimprimirCocina.Checked = false;
            chkVentaAlmuerzos.Checked = false;
            chkUsoNotasEntrega.Checked = false;
        }

        //FUNCION PARA VALIDAR LOS REGISTROS Y CREAR LA PARAMETRIZACIÓN POR LOCALIDAD
        private void validarRegistros()
        {
            try
            {
                if (dBAyudaCiudad.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de la ciudad por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaCajero.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del mesero por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaMesero.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del mesero por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaPromotor.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del promotor por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dbAyudaRepartidor.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del repartidor por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaConsumidorFinal.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de consumidor final por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaVendedor.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del vendedor por default.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaProducto.iId == 0)
                {
                    tbControl.SelectTab(1);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro del producto para anulación por default.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbMoneda.SelectedValue) == 0)
                {
                    tbControl.SelectTab(2);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de moneda por default.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbPrecuenta.SelectedValue) == 0)
                {
                    tbControl.SelectTab(2);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de formato de precuenta por default.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbFactura.SelectedValue) == 0)
                {
                    tbControl.SelectTab(2);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de formato de factura por default.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbTipoComprobantes.SelectedValue) == 0)
                {
                    tbControl.SelectTab(2);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el registro de comprobante por default.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(txtCantidadImpresiones.Text.Trim()) == 0)
                {
                    tbControl.SelectTab(3);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de impresiones del reporte de cliente empresarial no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadImpresiones.Text = "1";
                    txtCantidadImpresiones.Focus();
                    return;
                }

                if (Convert.ToInt32(txtCantidadVentaExpress.Text.Trim()) == 0)
                {
                    tbControl.SelectTab(3);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de impresiones del reporte de venta rápida no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadVentaExpress.Text = "1";
                    txtCantidadVentaExpress.Focus();
                    return;
                }

                if (Convert.ToInt32(txtCantidadCrearTarjetas.Text.Trim()) == 0)
                {
                    tbControl.SelectTab(3);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de reportes de la creación de tarjetas no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadCrearTarjetas.Text = "1";
                    txtCantidadCrearTarjetas.Focus();
                    return;
                }

                if (txtCorreoPorDefecto.Text.Trim() == "")
                {
                    tbControl.SelectTab(3);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese un correo electrónico default para el sistema.";
                    ok.ShowDialog();
                    txtCorreoPorDefecto.Focus();
                    return;
                }

                if (txtClaveAdmin.Text.Trim() == "")
                {
                    tbControl.SelectTab(3);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese una clave para acceder a la sección de administración.";
                    ok.ShowDialog();
                    txtClaveAdmin.Focus();
                    return;
                }

                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea crear el registro de localidad para el sistema?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    asignarValores();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ASIGNAR LOS DATOS A INSERTAR
        private void asignarValores()
        {
            try
            {
                if (chkMesas.Checked == true)
                    iManejaMesas = 1;
                else
                    iManejaMesas = 0;

                if (chkLlevar.Checked == true)
                    iManejaLlevar = 1;
                else
                    iManejaLlevar = 0;

                if (chkDomicilio.Checked == true)
                    iManejaDomicilio = 1;
                else
                    iManejaDomicilio = 0;

                if (chkCortesias.Checked == true)
                    iManejaCortesia = 1;
                else
                    iManejaCortesia = 0;

                if (chkFuncionarios.Checked == true)
                    iManejaValeFuncionario = 1;
                else
                    iManejaValeFuncionario = 0;

                if (chkConsumoEmpleados.Checked == true)
                    iManejaConsumoEmpleados = 1;
                else
                    iManejaConsumoEmpleados = 0;

                if (chkMenuExpress.Checked == true)
                    iManejaMenuExpress = 1;
                else
                    iManejaMenuExpress = 0;

                if (chkCanjes.Checked == true)
                    iManejaCanjes = 1;
                else
                    iManejaCanjes = 0;

                if (chkVentaRapida.Checked == true)
                    iManejaVentaRapida = 1;
                else
                    iManejaVentaRapida = 0;

                if (chkClienteEmpresarial.Checked == true)
                    iManejaClienteEmpresarial = 1;
                else
                    iManejaClienteEmpresarial = 0;

                if (chkTarjetaAlmuerzo.Checked == true)
                    iManejaTarjetaAlmuerzo = 1;
                else
                    iManejaTarjetaAlmuerzo = 0;

                if (chkConsumoInterno.Checked == true)
                    iManejaConsumoInterno = 1;
                else
                    iManejaConsumoInterno = 0;




                if (chkDestinosImpresion.Checked == true)
                    iDestinosImpresion = 1;
                else
                    iDestinosImpresion = 0;

                if (chkJornada.Checked == true)
                    iManejaJornadas = 1;
                else
                    iManejaJornadas = 0;

                if (chkEjecutarImpresiones.Checked == true)
                    iEjecutarImpresiones = 1;
                else
                    iEjecutarImpresiones = 0;

                if (chkAbrirCajon.Checked == true)
                    iAbrirCajonDinero = 1;
                else
                    iAbrirCajonDinero = 0;

                if (chkNoProcesados.Checked == true)
                    iDescargaProductoNoProcesado = 1;
                else
                    iDescargaProductoNoProcesado = 0;

                if (chkUsarRecetas.Checked == true)
                    iDescargaReceta = 1;
                else
                    iDescargaReceta = 0;

                if (chkManejaPromotor.Checked == true)
                    iManejaPromotor = 1;
                else
                    iManejaPromotor = 0;

                if (chkManejaRepartidor.Checked == true)
                    iManejaRepartidor = 1;
                else
                    iManejaRepartidor = 0;

                if (chkReimprimirCocina.Checked == true)
                    iReimprimirCocina = 1;
                else
                    iReimprimirCocina = 0;

                if (chkMostrarPropinas.Checked == true)
                    iMostrarValoresPropina = 1;
                else
                    iMostrarValoresPropina = 0;

                if (chkMitad.Checked == true)
                    iManejaMitad = 1;
                else
                    iManejaMitad = 0;

                if (chkProductosComision.Checked == true)
                    iManejaProductosComisionEmpleados = 1;
                else
                    iManejaProductosComisionEmpleados = 0;

                if (chkDeliveryVariable.Checked == true)
                    iManejaDeliveryVariable = 1;
                else
                    iManejaDeliveryVariable = 0;

                if (chkImprimeDatosFactura.Checked == true)
                    iImprimirDatosFactura = 1;
                else
                    iImprimirDatosFactura = 0;

                if (chkPropinaTarjetas.Checked == true)
                    iPropinaParaTarjetas = 1;
                else
                    iPropinaParaTarjetas = 0;



                if (chkAplicaRecargoTarjetas.Checked == true)
                    iAplicaRecargoTarjetas = 1;
                else
                    iAplicaRecargoTarjetas = 0;



                if (chkLeerMeseroMesas.Checked == true)
                    iLeerMeseroMesas = 1;
                else
                    iLeerMeseroMesas = 0;

                if (chkImprimirPrecuentaGuardar.Checked == true)
                    iImprimirPrecuentaGuardar = 1;
                else
                    iImprimirPrecuentaGuardar = 0;

                if (chkFacturacionElectronica.Checked == true)
                    iManejaFacturacionElectronica = 1;
                else
                    iManejaFacturacionElectronica = 0;

                if (chkUsoNotasEntrega.Checked == true)
                    iManejaNotaEntrega = 1;
                else
                    iManejaNotaEntrega = 0;

                if (chkSeleccionMeseroLlevar.Checked == true)
                    iSeleccionMeseroLlevar = 1;
                else
                    iSeleccionMeseroLlevar = 0;

                if (chkVistaPreviaImpresiones.Checked == true)
                    iVistaPreviaImpresiones = 1;
                else
                    iVistaPreviaImpresiones = 0;

                if (chkRemoverIva.Checked == true)
                    iDescuentaIva = 1;
                else
                    iDescuentaIva = 0;

                if (chkVentaAlmuerzos.Checked == true)
                    iManejaAlmuerzos = 1;
                else
                    iManejaAlmuerzos = 0;

                if (chkUsoDecimales.Checked == true)
                    iConfiguracionDecimales = 1;
                else
                    iConfiguracionDecimales = 0;

                insertarRegistro();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR EN LA TABLA
        private void insertarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_parametro_localidad (" + Environment.NewLine;
                sSql += "id_localidad, cg_ciudad, id_pos_mesero, id_pos_cajero, cg_moneda, id_pos_formato_factura," + Environment.NewLine;
                sSql += "id_pos_formato_precuenta, habilitar_destinos_impresion, consumidor_final, id_vendedor, maneja_jornada," + Environment.NewLine;
                sSql += "maneja_mesas, maneja_llevar, maneja_domicilio, maneja_menu_express, maneja_cortesia," + Environment.NewLine;
                sSql += "maneja_canjes, maneja_vale_funcionario, maneja_consumo_empleados, imprimir_datos_factura," + Environment.NewLine;
                sSql += "id_producto_anula, valor_precio_anula, clave_acceso_admin, id_pos_impresora, ejecutar_impresion," + Environment.NewLine;
                sSql += "permitir_abrir_cajon, valor_maximo_recargo, secuencia_codigo_empleado_cliente, descarga_receta," + Environment.NewLine;
                sSql += "descarga_no_procesados, maneja_promotor, id_pos_promotor, maneja_repartidor," + Environment.NewLine;
                sSql += "id_pos_repartidor, cantidad_reporte_empresa, cantidad_reporte_express," + Environment.NewLine;
                sSql += "id_tipo_comprobante_default, reimprimir_cocina," + Environment.NewLine;
                sSql += "mostrar_valores_propina, maneja_mitad, maneja_producto_comision_empleados," + Environment.NewLine;
                sSql += "valor_comision_producto_para_empleados, maneja_delivery_variable," + Environment.NewLine;
                sSql += "maneja_venta_rapida, maneja_cliente_empresarial, maneja_tarjeta_almuerzo," + Environment.NewLine;
                sSql += "maneja_consumo_interno, propina_para_tarjetas, cantidad_reporte_crear_tarjetas," + Environment.NewLine;
                sSql += "porcentaje_descuento_empleados, leer_mesero_mesas, imprimir_precuenta_guardar_comanda, maneja_facturacion_electronica," + Environment.NewLine;
                sSql += "configuracion_decimales, logo, maneja_nota_entrega, seleccion_mesero_para_llevar, vista_previa_impresion," + Environment.NewLine;
                sSql += "descuenta_iva, maneja_almuerzos, numero_personas_default, ruta_reportes," + Environment.NewLine;
                sSql += "aplica_recargo_tarjetas, porcentaje_recargo_tarjetas, correo_electronico_default," + Environment.NewLine;
                sSql += "is_active, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_localidad, @cg_ciudad, @id_pos_mesero, @id_pos_cajero, @cg_moneda, @id_pos_formato_factura," + Environment.NewLine;
                sSql += "@id_pos_formato_precuenta, @habilitar_destinos_impresion, @consumidor_final, @id_vendedor, @maneja_jornada," + Environment.NewLine;
                sSql += "@maneja_mesas, @maneja_llevar, @maneja_domicilio, @maneja_menu_express, @maneja_cortesia," + Environment.NewLine;
                sSql += "@maneja_canjes, @maneja_vale_funcionario, @maneja_consumo_empleados, @imprimir_datos_factura," + Environment.NewLine;
                sSql += "@id_producto_anula, @valor_precio_anula, @clave_acceso_admin, @id_pos_impresora, @ejecutar_impresion," + Environment.NewLine;
                sSql += "@permitir_abrir_cajon, @valor_maximo_recargo, @secuencia_codigo_empleado_cliente, @descarga_receta," + Environment.NewLine;
                sSql += "@descarga_no_procesados, @maneja_promotor, @id_pos_promotor, @maneja_repartidor," + Environment.NewLine;
                sSql += "@id_pos_repartidor, @cantidad_reporte_empresa, @cantidad_reporte_express," + Environment.NewLine;
                sSql += "@id_tipo_comprobante_default, @reimprimir_cocina," + Environment.NewLine;
                sSql += "@mostrar_valores_propina, @maneja_mitad, @maneja_producto_comision_empleados," + Environment.NewLine;
                sSql += "@valor_comision_producto_para_empleados, @maneja_delivery_variable," + Environment.NewLine;
                sSql += "@maneja_venta_rapida, @maneja_cliente_empresarial, @maneja_tarjeta_almuerzo," + Environment.NewLine;
                sSql += "@maneja_consumo_interno, @propina_para_tarjetas, @cantidad_reporte_crear_tarjetas," + Environment.NewLine;
                sSql += "@porcentaje_descuento_empleados, @leer_mesero_mesas, @imprimir_precuenta_guardar_comanda, @maneja_facturacion_electronica," + Environment.NewLine;
                sSql += "@configuracion_decimales, @logo, @maneja_nota_entrega, @seleccion_mesero_para_llevar, @vista_previa_impresion," + Environment.NewLine;
                sSql += "@descuenta_iva, @maneja_almuerzos, @numero_personas_default, @ruta_reportes," + Environment.NewLine;
                sSql += "@aplica_recargo_tarjetas, @porcentaje_recargo_tarjetas, @correo_electronico_default," + Environment.NewLine;
                sSql += "@is_active, @estado, getdate(), @usuario_ingreso, @terminal_ingreso)" + Environment.NewLine;

                int i = 0;
                parametro = new SqlParameter[69];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_localidad";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iIdLocalidad;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cg_ciudad";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaCiudad.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_mesero";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaMesero.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_cajero";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaCajero.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cg_moneda";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbMoneda.SelectedValue;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_formato_factura";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbPrecuenta.SelectedValue;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_formato_precuenta";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbFactura.SelectedValue;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@habilitar_destinos_impresion";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iDestinosImpresion;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@consumidor_final";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaConsumidorFinal.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_vendedor";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaVendedor.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_jornada";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaJornadas;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_mesas";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaMesas;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_llevar";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaLlevar;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_domicilio";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaDomicilio;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_menu_express";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaMenuExpress;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_cortesia";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaCortesia;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_canjes";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaCanjes;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_vale_funcionario";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaValeFuncionario;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_consumo_empleados";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaConsumoEmpleados;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@imprimir_datos_factura";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iImprimirDatosFactura;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_producto_anula";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaProducto.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@valor_precio_anula";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@clave_acceso_admin";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtClaveAdmin.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_impresora";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbImpresoras.SelectedValue;
                i++;
                                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@ejecutar_impresion";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iEjecutarImpresiones;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@permitir_abrir_cajon";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iAbrirCajonDinero;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@valor_maximo_recargo";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = Convert.ToDecimal(txtMontoMaximoRecargoTarjetas.Text);
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@secuencia_codigo_empleado_cliente";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = "1";
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descarga_receta";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iDescargaReceta;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descarga_no_procesados";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iDescargaProductoNoProcesado;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_promotor";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaPromotor;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_promotor";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dBAyudaPromotor.iId;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_repartidor";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaRepartidor;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_repartidor";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = dbAyudaRepartidor.iId;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_reporte_empresa";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = txtCantidadImpresiones.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_reporte_express";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = txtCantidadVentaExpress.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_tipo_comprobante_default";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = cmbTipoComprobantes.SelectedValue;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@reimprimir_cocina";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iReimprimirCocina;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@mostrar_valores_propina";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iMostrarValoresPropina;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_mitad";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaMitad;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_producto_comision_empleados";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaProductosComisionEmpleados;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@valor_comision_producto_para_empleados";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iProductosComisionEmpleados;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_delivery_variable";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaDeliveryVariable;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_venta_rapida";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaVentaRapida;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_cliente_empresarial";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaClienteEmpresarial;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_tarjeta_almuerzo";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaTarjetaAlmuerzo;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_consumo_interno";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaConsumoInterno;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@propina_para_tarjetas";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iPropinaParaTarjetas;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_reporte_crear_tarjetas";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = txtCantidadCrearTarjetas.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@porcentaje_descuento_empleados";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = txtPorcentajeDescuentoEmpleados.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@leer_mesero_mesas";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iLeerMeseroMesas;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@imprimir_precuenta_guardar_comanda";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iImprimirPrecuentaGuardar;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_facturacion_electronica";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaFacturacionElectronica;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@configuracion_decimales";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iConfiguracionDecimales;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@logo";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtRutaLogo.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_nota_entrega";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaNotaEntrega;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@seleccion_mesero_para_llevar";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iSeleccionMeseroLlevar;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@vista_previa_impresion";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iVistaPreviaImpresiones;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descuenta_iva";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iDescuentaIva;
                i++;
                
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@maneja_almuerzos";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iManejaAlmuerzos;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@numero_personas_default";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtPersonasDefault.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@ruta_reportes";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtRutaReportes.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@aplica_recargo_tarjetas";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iAplicaRecargoTarjetas;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@porcentaje_recargo_tarjetas";
                parametro[i].SqlDbType = SqlDbType.Decimal;
                parametro[i].Value = txtPorcentajeRecargoTarjetas.Text;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@correo_electronico_default";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = txtCorreoPorDefecto.Text.Trim();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@is_active";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 1;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "A";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "ADMINISTRADOR";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = Environment.MachineName.ToString();

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El registro de la localidad se ha creado con éxito. La aplicación se reiniciará para empezar su primero uso.";
                ok.ShowDialog();

                Application.Restart();
                
            }

            catch(Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR VALORES EN LA TABLA DE PRECUENTA Y FACTURA
        private void insertarFormatos(int iOp)
        {
            try
            {
                // iOp 1: Precuenta 2: Factura
                int i = 0;
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";

                if (iOp == 1)
                    sSql += "insert into pos_formato_precuenta (" + Environment.NewLine;
                else if (iOp == 2)
                    sSql += "insert into pos_formato_factura (" + Environment.NewLine;

                sSql += "codigo, descripcion, id_pos_impresora, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@codigo, @descripcion, @id_pos_impresora, @estado, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)";

                parametro = new SqlParameter[6];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@codigo";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "1";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descripcion";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "FORMATO 1";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_impresora";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "A";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "ADMINISTRADOR";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = Environment.MachineName.ToString();

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SEGUNDO REGISTRO
                parametro = new SqlParameter[6];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@codigo";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "2";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descripcion";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "FORMATO 2";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_impresora";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "A";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "ADMINISTRADOR";
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = Environment.MachineName.ToString();

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Formatos generados éxitosamente.";
                ok.ShowDialog();

                if (iOp == 1)
                    llenarComboPrecuenta();
                else
                    llenarComboFactura();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Aceptar")
            {
                iIdLocalidad = Convert.ToInt32(cmbLocalidad.SelectedValue);
                tbControl.SelectTab(0);
                btnGrabar.Visible = true;
                cmbLocalidad.Enabled = false;
                tbControl.Enabled = true;
                btnOK.Text = "Cancelar";
            }

            else
            {
                cancelar();
                tbControl.SelectTab(0);
                btnGrabar.Visible = false;
                cmbLocalidad.Enabled = true;
                tbControl.Enabled = false;
                btnOK.Text = "Aceptar";
            }
        }

        private void frmCrearParametrosLocalidad_Load(object sender, EventArgs e)
        {
            llenarComboLocalidad();
            cargarDbAyuda();
            llenarComboMoneda();
            llenarComboPrecuenta();
            llenarComboFactura();
            llenarComboImpresoras();
            llenarComboComprobantes();
        }

        private void txtValorComision_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtCantidadImpresiones_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtCantidadImpresiones_Leave(object sender, EventArgs e)
        {
            if (txtCantidadImpresiones.Text.Trim() == "")
            {
                txtCantidadImpresiones.Text = "1";
            }
        }

        private void txtCantidadVentaExpress_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCantidadVentaExpress_Leave(object sender, EventArgs e)
        {
            if (txtCantidadImpresiones.Text.Trim() == "")
            {
                txtCantidadImpresiones.Text = "1";
            }
        }

        private void txtCantidadCrearTarjetas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCantidadCrearTarjetas_Leave(object sender, EventArgs e)
        {
            if (txtCantidadCrearTarjetas.Text.Trim() == "")
            {
                txtCantidadCrearTarjetas.Text = "1";
            }
        }

        private void chkAplicarecargoTarjetas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAplicaRecargoTarjetas.Checked == true)
            {
                txtPorcentajeRecargoTarjetas.Text = "0";
                txtMontoMaximoRecargoTarjetas.Text = "0";
                txtPorcentajeRecargoTarjetas.ReadOnly = false;
                txtMontoMaximoRecargoTarjetas.ReadOnly = false;
            }

            else
            {
                txtPorcentajeRecargoTarjetas.Text = "0";
                txtMontoMaximoRecargoTarjetas.Text = "0";
                txtPorcentajeRecargoTarjetas.ReadOnly = true;
                txtMontoMaximoRecargoTarjetas.ReadOnly = true;
            }
        }

        private void txtPorcentajeRecargoTarjetas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPorcentajeRecargoTarjetas_Leave(object sender, EventArgs e)
        {
            if (txtPorcentajeRecargoTarjetas.Text.Trim() == "")
            {
                txtPorcentajeRecargoTarjetas.Text = "0";
            }
        }

        private void txtMontoMaximoRecargoTarjetas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtMontoMaximoRecargoTarjetas_Leave(object sender, EventArgs e)
        {
            if (txtMontoMaximoRecargoTarjetas.Text.Trim() == "")
            {
                txtMontoMaximoRecargoTarjetas.Text = "0";
            }
        }

        private void txtPorcentajeDescuentoEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPorcentajeDescuentoEmpleados_Leave(object sender, EventArgs e)
        {
            if (txtPorcentajeDescuentoEmpleados.Text.Trim() == "")
            {
                txtPorcentajeDescuentoEmpleados.Text = "0";
            }
        }

        private void txtPersonasDefault_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtPersonasDefault_Leave(object sender, EventArgs e)
        {
            if (txtPersonasDefault.Text.Trim() == "")
            {
                txtPersonasDefault.Text = "1";
            }
        }

        private void txtValorComision_Leave(object sender, EventArgs e)
        {
            if (txtValorComision.Text.Trim() == "")
            {
                txtValorComision.Text = "0.00";
            }
        }

        private void btnExaminarLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRutaLogo.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnExaminarReportes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog abrir = new FolderBrowserDialog();

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRutaReportes.Text = abrir.SelectedPath;
            }

            abrir.Dispose();
        }

        private void btnRemoverLogo_Click(object sender, EventArgs e)
        {
            txtRutaLogo.Clear();
            btnExaminarLogo.Focus();
        }

        private void btnRemoverReportes_Click(object sender, EventArgs e)
        {
            txtRutaReportes.Clear();
            btnExaminarReportes.Focus();
        }

        private void chkProductosComision_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProductosComision.Checked == true)
            {
                txtValorComision.ReadOnly = false;
                txtValorComision.Text = "0.00";
                txtValorComision.Focus();
            }

            else
            {
                txtValorComision.ReadOnly = true;
                txtValorComision.Text = "0.00";
                chkDeliveryVariable.Focus();
            }
        }

        private void txtClaveAdmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            validarRegistros();
        }

        private void btnAgregarPrecuentas_Click(object sender, EventArgs e)
        {
            insertarFormatos(1);
        }

        private void btnAgregarFacturas_Click(object sender, EventArgs e)
        {
            insertarFormatos(2);
        }

        private void btnAgregarCajero_Click(object sender, EventArgs e)
        {
            AsistenteConfiguracion.frmRegistrarPersonalAsistente registrar = new AsistenteConfiguracion.frmRegistrarPersonalAsistente(1);
            registrar.ShowDialog();

            if (registrar.DialogResult == DialogResult.OK)
            {
                int iIdRegistro_R = registrar.iIdRegistro;
                string sCodigo_R = registrar.sCodigo;
                string sDescripcion_R = registrar.sDescripcion;
                registrar.Close();
                cargarDbAyuda();

                dBAyudaCajero.iId = iIdRegistro_R;
                dBAyudaCajero.txtDatos.Text = sDescripcion_R;
                dBAyudaCajero.txtIdentificacion.Text = sCodigo_R;
                dBAyudaCajero.sNombre = sDescripcion_R;
                dBAyudaCajero.sCodigo = sCodigo_R;
            }
        }

        private void btnAgregarMesero_Click(object sender, EventArgs e)
        {
            AsistenteConfiguracion.frmRegistrarPersonalAsistente registrar = new AsistenteConfiguracion.frmRegistrarPersonalAsistente(2);
            registrar.ShowDialog();

            if (registrar.DialogResult == DialogResult.OK)
            {
                int iIdRegistro_R = registrar.iIdRegistro;
                string sCodigo_R = registrar.sCodigo;
                string sDescripcion_R = registrar.sDescripcion;
                registrar.Close();
                cargarDbAyuda();

                dBAyudaMesero.iId = iIdRegistro_R;
                dBAyudaMesero.txtDatos.Text = sDescripcion_R;
                dBAyudaMesero.txtIdentificacion.Text = sCodigo_R;
                dBAyudaMesero.sNombre = sDescripcion_R;
                dBAyudaMesero.sCodigo = sCodigo_R;
            }
        }

        private void btnAgregarPromotor_Click(object sender, EventArgs e)
        {
            AsistenteConfiguracion.frmRegistrarPersonalAsistente registrar = new AsistenteConfiguracion.frmRegistrarPersonalAsistente(3);
            registrar.ShowDialog();

            if (registrar.DialogResult == DialogResult.OK)
            {
                int iIdRegistro_R = registrar.iIdRegistro;
                string sCodigo_R = registrar.sCodigo;
                string sDescripcion_R = registrar.sDescripcion;
                registrar.Close();
                cargarDbAyuda();

                dBAyudaPromotor.iId = iIdRegistro_R;
                dBAyudaPromotor.txtDatos.Text = sDescripcion_R;
                dBAyudaPromotor.txtIdentificacion.Text = sCodigo_R;
                dBAyudaPromotor.sNombre = sDescripcion_R;
                dBAyudaPromotor.sCodigo = sCodigo_R;
            }
        }

        private void btnAgregarRepartidor_Click(object sender, EventArgs e)
        {
            AsistenteConfiguracion.frmRegistrarPersonalAsistente registrar = new AsistenteConfiguracion.frmRegistrarPersonalAsistente(4);
            registrar.ShowDialog();

            if (registrar.DialogResult == DialogResult.OK)
            {
                int iIdRegistro_R = registrar.iIdRegistro;
                string sCodigo_R = registrar.sCodigo;
                string sDescripcion_R = registrar.sDescripcion;
                registrar.Close();
                cargarDbAyuda();

                dbAyudaRepartidor.iId = iIdRegistro_R;
                dbAyudaRepartidor.txtDatos.Text = sDescripcion_R;
                dbAyudaRepartidor.txtIdentificacion.Text = sCodigo_R;
                dbAyudaRepartidor.sNombre = sDescripcion_R;
                dbAyudaRepartidor.sCodigo = sCodigo_R;
            }
        }
    }
}
