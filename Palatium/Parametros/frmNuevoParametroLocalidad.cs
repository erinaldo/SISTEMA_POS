using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Parametros
{
    public partial class frmNuevoParametroLocalidad : Form
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

        int iBanderaTab;
        int iIdLocalidad;
        int iIdPosParametroLocalidad;

        int iManejaMesas;
        int iManejaLlevar;
        int iManejaDomicilio;
        int iManejaCortesia;
        int iManejaValeFuncionario;
        int iManejaConsumoEmpleado;
        int iManejaMenuExpress;
        int iManejaCanje;
        int iManejaVentaExpress;
        int iManejaClienteEmpresarial;
        int iManejaTarjetaAlmuerzo;
        int iManejaConsumoInterno;

        int iDestinosImpresion;
        int iManejaJornadas;
        int iEjecutarImpresiones;
        int iAbrirCajon;
        int iDescargarNoProcesados;
        int iDescargarMateriaPrima;
        int iManejaPromotor;
        int iManejaRepartidor;
        int iReimprimirCocina;
        int iMostrarValoresPropina;
        int iManejaMitad;
        int iManejaProductosComision;
        int iManejaDeliveryVariable;
        int iImprimirDatosFactura;
        int iPropinaTarjetas;
        int iManejaRecargoTarjetas;

        int iLeerMeseroMesas;
        int iImprimirPrecuentaGuardar;
        int iHabilitarOpcionesFacturacionElectronica;
        int iUsoNotasEntrega;
        int iSeleccionMeseroLlevar;
        int iVistaPreviaImpresiones;
        int iRemoverIva;
        int iManejaAlmuerzos;
        int iUsoDecimales;

        Decimal dValor;

        public frmNuevoParametroLocalidad()
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //FUNCION PARA CARGAR LA TABLA DE LOCALIDADES
        private void cargarParametros()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_parametros_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + cmbLocalidad.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //FUNCION PARA ENVIAR EL PARAMETRO A LAS FUNCIONES
        private void enviarParametro()
        {
            if (iBanderaTab == 1)
                cargarTabComandas();

            if (iBanderaTab == 2)
                cargarTabValoresDefault();

            if (iBanderaTab == 3)
                cargarTabOpciones();

            if (iBanderaTab == 4)
                cargarTabAsignacion();

            if (iBanderaTab == 5)
                cargarTabParametrosAdicionales();
        }

        //FUNCION PARA VALIDAR LOS DATOS ANTES DE ENVIAR A LA BASE DE DATOS
        private void verificarDatos()
        {
            if (iBanderaTab == 2)
            {
                if (dBAyudaCiudad.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la ciudad predeterminada para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaCajero.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el cajero predeterminado para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaMesero.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el mesero predeterminado para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaPromotor.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el promotor predeterminado para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dbAyudaRepartidor.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el repartidor predeterminado para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaConsumidorFinal.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el valor predeterminado para consumidor final de la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaVendedor.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el vendedor para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (dBAyudaProducto.iId == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione un producto para ingresar como anulación.";
                    ok.ShowDialog();
                    return;
                }
            }

            else if (iBanderaTab == 3)
            {
                if (Convert.ToInt32(cmbMoneda.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de moneda para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbPrecuenta.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de formato de precuenta para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbFactura.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de formato de factura para la localidad.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbImpresoras.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione una impresora para la impresión de reportes.";
                    ok.ShowDialog();
                    return;
                }

                if (Convert.ToInt32(cmbTipoComprobantes.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de comprobante a emitir por default.";
                    ok.ShowDialog();
                    return;
                }
            }

            else if (iBanderaTab == 4)
            {
                if (Convert.ToInt32(txtCantidadImpresiones.Text.Trim()) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de impresiones del reporte de cliente empresarial no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadImpresiones.Text = "1";
                    txtCantidadImpresiones.Focus();
                    return;
                }

                if (txtCantidadVentaExpress.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de impresiones del reporte de venta rápida no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadVentaExpress.Text = "1";
                    txtCantidadVentaExpress.Focus();
                    return;
                }

                if (txtCantidadCrearTarjetas.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad de reportes de la creación de tarjetas no puede ser cero.";
                    ok.ShowDialog();
                    txtCantidadCrearTarjetas.Text = "1";
                    txtCantidadCrearTarjetas.Focus();
                    return;
                }

                if (txtCorreoPorDefecto.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese un correo electrónico default para el sistema.";
                    ok.ShowDialog();
                    txtCorreoPorDefecto.Focus();
                    return;
                }
            }

            enviarCambiosBDD();
        }

        private void enviarCambiosBDD()
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar los cambios?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (iBanderaTab == 1)
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
                        iManejaConsumoEmpleado = 1;
                    else
                        iManejaConsumoEmpleado = 0;

                    if (chkMenuExpress.Checked == true)
                        iManejaMenuExpress = 1;
                    else
                        iManejaMenuExpress = 0;

                    if (chkCanjes.Checked == true)
                        iManejaCanje = 1;
                    else
                        iManejaCanje = 0;

                    if (chkVentaRapida.Checked == true)
                        iManejaVentaExpress = 1;
                    else
                        iManejaVentaExpress = 0;

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


                    actualizarComandasSistema();
                }

                else if (iBanderaTab == 2)
                {
                    actualizarValoresSistema();
                }

                else if (iBanderaTab == 3)
                {
                    if (chkCocina.Checked == true)
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
                        iAbrirCajon = 1;
                    else
                        iAbrirCajon = 0;

                    if (chkNoProcesados.Checked == true)
                        iDescargarNoProcesados = 1;
                    else
                        iDescargarNoProcesados = 0;

                    if (chkUsarRecetas.Checked == true)
                        iDescargarMateriaPrima = 1;
                    else
                        iDescargarMateriaPrima = 0;

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
                        iManejaProductosComision = 1;
                    else
                        iManejaProductosComision = 0;

                    if (chkDeliveryVariable.Checked == true)
                        iManejaDeliveryVariable = 1;
                    else
                        iManejaDeliveryVariable = 0;

                    if (chkImprimeDatosFactura.Checked == true)
                        iManejaLlevar = 1;
                    else
                        iImprimirDatosFactura = 0;

                    if (chkPropinaTarjetas.Checked == true)
                        iPropinaTarjetas = 1;
                    else
                        iPropinaTarjetas = 0;

                    if (txtValorComision.Text.Trim() == "")
                        txtValorComision.Text = "0.00";

                    actualizarComandasOpciones();
                }

                else if (iBanderaTab == 4)
                {
                    if (chkAplicarecargoTarjetas.Checked == true)
                        iManejaRecargoTarjetas = 1;
                    else
                        iManejaRecargoTarjetas = 0;

                    actualizarComandasAsignacion();
                }

                else if (iBanderaTab == 5)
                {
                    if (chkLeerMeseroMesas.Checked == true)
                        iLeerMeseroMesas = 1;
                    else
                        iLeerMeseroMesas = 0;

                    if (chkImprimirPrecuentaGuardar.Checked == true)
                        iImprimirPrecuentaGuardar = 1;
                    else
                        iImprimirPrecuentaGuardar = 0;

                    if (chkFacturacionElectronica.Checked == true)
                        iHabilitarOpcionesFacturacionElectronica = 1;
                    else
                        iHabilitarOpcionesFacturacionElectronica = 0;

                    if (chkUsoNotasEntrega.Checked == true)
                        iUsoNotasEntrega = 1;
                    else
                        iUsoNotasEntrega = 0;

                    if (chkSeleccionMeseroLlevar.Checked == true)
                        iSeleccionMeseroLlevar = 1;
                    else
                        iSeleccionMeseroLlevar = 0;

                    if (chkVistaPreviaImpresiones.Checked == true)
                        iVistaPreviaImpresiones = 1;
                    else
                        iVistaPreviaImpresiones = 0;

                    if (chkRemoverIva.Checked == true)
                        iRemoverIva = 1;
                    else
                        iRemoverIva = 0;

                    if (chkVentaAlmuerzos.Checked == true)
                        iManejaAlmuerzos = 1;
                    else
                        iManejaAlmuerzos = 0;

                    if (chkUsoDecimales.Checked == true)
                        iUsoDecimales = 1;
                    else
                        iUsoDecimales = 0;

                    actualizarParametrosAdicionales();
                }
            }
        }

        #endregion

        #region FUNCIONES DEL TAB DE COMANDAS

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabComandas()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidad = 0;
                    iIdPosParametroLocalidad = 0;
                    chkMesas.Checked = false;
                    chkLlevar.Checked = false;
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
                }

                else
                {
                    iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                    iIdPosParametroLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro_localidad"].ToString());

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_mesas"].ToString()) == 1)
                        chkMesas.Checked = true;
                    else
                        chkMesas.Checked = false;
    
                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_llevar"].ToString()) == 1)
                        chkLlevar.Checked = true;
                    else
                        chkLlevar.Checked = false;                        

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_domicilio"].ToString()) == 1)
                        chkDomicilio.Checked = true;
                    else
                        chkDomicilio.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_cortesia"].ToString()) == 1)
                        chkCortesias.Checked = true;
                    else
                        chkCortesias.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_vale_funcionario"].ToString()) == 1)
                        chkFuncionarios.Checked = true;
                    else
                        chkFuncionarios.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_consumo_empleados"].ToString()) == 1)
                        chkConsumoEmpleados.Checked = true;
                    else
                        chkConsumoEmpleados.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_menu_express"].ToString()) == 1)
                        chkMenuExpress.Checked = true;
                    else
                        chkMenuExpress.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_canjes"].ToString()) == 1)
                        chkCanjes.Checked = true;
                    else
                        chkCanjes.Checked = false;                        

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_venta_rapida"].ToString()) == 1)
                        chkVentaRapida.Checked = true;
                    else
                        chkVentaRapida.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_cliente_empresarial"].ToString()) == 1)
                        chkClienteEmpresarial.Checked = true;
                    else
                        chkClienteEmpresarial.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_tarjeta_almuerzo"].ToString()) == 1)
                        chkTarjetaAlmuerzo.Checked = true;
                    else
                        chkTarjetaAlmuerzo.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_consumo_interno"].ToString()) == 1)
                        chkConsumoInterno.Checked = true;
                    else
                        chkConsumoInterno.Checked = false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarComandasSistema()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "maneja_mesas = " + iManejaMesas + "," + Environment.NewLine;
                sSql += "maneja_llevar = " + iManejaLlevar + "," + Environment.NewLine;
                sSql += "maneja_domicilio = " + iManejaDomicilio + "," + Environment.NewLine;
                sSql += "maneja_cortesia = " + +iManejaCortesia + "," + Environment.NewLine;
                sSql += "maneja_vale_funcionario = " + iManejaValeFuncionario + "," + Environment.NewLine;
                sSql += "maneja_consumo_empleados = " + iManejaConsumoEmpleado + "," + Environment.NewLine;
                sSql += "maneja_menu_express = " + iManejaMenuExpress + "," + Environment.NewLine;
                sSql += "maneja_canjes = " + iManejaCanje + "," + Environment.NewLine;
                sSql += "maneja_venta_rapida = " + iManejaVentaExpress +"," + Environment.NewLine;
                sSql += "maneja_cliente_empresarial = " + iManejaClienteEmpresarial + "," + Environment.NewLine;
                sSql += "maneja_tarjeta_almuerzo = " + iManejaTarjetaAlmuerzo +"," + Environment.NewLine;
                sSql += "maneja_consumo_interno = " + iManejaConsumoInterno + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametrosPredeterminados();
                cargarParametros();
                enviarParametro();
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

        #region FUNCIONES DEL TAB DE VALORES DEFAULT

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
                sSql += "where estado = 'A'";
                dBAyudaCajero.Ver(sSql, "descripcion", 0, 2, 1);

                //DBAYUDA MESEROS
                sSql = "";
                sSql += "select id_pos_mesero, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_mesero" + Environment.NewLine;
                sSql += "where estado = 'A'";
                dBAyudaMesero.Ver(sSql, "descripcion", 0, 2, 1);

                //DBAYUDA PROMOTORES
                sSql = "";
                sSql += "select id_pos_promotor, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_promotor" + Environment.NewLine;
                sSql += "where estado = 'A'";
                dBAyudaPromotor.Ver(sSql, "descripcion", 0, 2, 1);

                //DBAYUDA REPARTIDORES
                sSql = "";
                sSql += "select id_pos_repartidor, descripcion as 'DESCRIPCION', codigo as 'CODIGO'" + Environment.NewLine;
                sSql += "from pos_repartidor" + Environment.NewLine;
                sSql += "where estado = 'A'";
                dbAyudaRepartidor.Ver(sSql, "descripcion", 0, 2, 1);

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

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabValoresDefault()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidad = 0;
                    iIdPosParametroLocalidad = 0;
                    dBAyudaCiudad.limpiar();
                    dBAyudaCajero.limpiar();
                    dBAyudaMesero.limpiar();
                    dBAyudaPromotor.limpiar();
                    dbAyudaRepartidor.limpiar();
                    dBAyudaConsumidorFinal.limpiar();
                    dBAyudaVendedor.limpiar();
                    dBAyudaProducto.limpiar();
                }

                else
                {
                    iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                    iIdPosParametroLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro_localidad"].ToString());

                    dBAyudaCiudad.iId = Convert.ToInt32(dtConsulta.Rows[0]["cg_ciudad"].ToString());
                    dBAyudaCiudad.txtDatos.Text = dtConsulta.Rows[0]["valor_texto"].ToString().Trim().ToUpper();
                    dBAyudaCiudad.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo"].ToString().Trim().ToUpper();
                    dBAyudaCiudad.sNombre = dtConsulta.Rows[0]["valor_texto"].ToString();
                    dBAyudaCiudad.sCodigo = dtConsulta.Rows[0]["codigo"].ToString();

                    dBAyudaCajero.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cajero"].ToString());
                    dBAyudaCajero.txtDatos.Text = dtConsulta.Rows[0]["cajero"].ToString().Trim().ToUpper();
                    dBAyudaCajero.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_cajero"].ToString().Trim().ToUpper();
                    dBAyudaCajero.sNombre = dtConsulta.Rows[0]["cajero"].ToString();
                    dBAyudaCajero.sCodigo = dtConsulta.Rows[0]["codigo_cajero"].ToString();

                    dBAyudaMesero.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_mesero"].ToString());
                    dBAyudaMesero.txtDatos.Text = dtConsulta.Rows[0]["mesero"].ToString().Trim().ToUpper();
                    dBAyudaMesero.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_mesero"].ToString().Trim().ToUpper();
                    dBAyudaMesero.sNombre = dtConsulta.Rows[0]["mesero"].ToString();
                    dBAyudaMesero.sCodigo = dtConsulta.Rows[0]["codigo_mesero"].ToString();

                    dBAyudaPromotor.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_promotor"].ToString());
                    dBAyudaPromotor.txtDatos.Text = dtConsulta.Rows[0]["promotor"].ToString().Trim().ToUpper();
                    dBAyudaPromotor.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_promotor"].ToString().Trim().ToUpper();
                    dBAyudaPromotor.sNombre = dtConsulta.Rows[0]["promotor"].ToString();
                    dBAyudaPromotor.sCodigo = dtConsulta.Rows[0]["codigo_promotor"].ToString();

                    dbAyudaRepartidor.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_repartidor"].ToString());
                    dbAyudaRepartidor.txtDatos.Text = dtConsulta.Rows[0]["repartidor"].ToString().Trim().ToUpper();
                    dbAyudaRepartidor.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_repartidor"].ToString().Trim().ToUpper();
                    dbAyudaRepartidor.sNombre = dtConsulta.Rows[0]["repartidor"].ToString();
                    dbAyudaRepartidor.sCodigo = dtConsulta.Rows[0]["codigo_repartidor"].ToString();

                    dBAyudaConsumidorFinal.iId = Convert.ToInt32(dtConsulta.Rows[0]["consumidor_final"].ToString());
                    dBAyudaConsumidorFinal.txtDatos.Text = dtConsulta.Rows[0]["nombre"].ToString().Trim().ToUpper();
                    dBAyudaConsumidorFinal.txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString().Trim().ToUpper();
                    dBAyudaConsumidorFinal.sNombre = dtConsulta.Rows[0]["nombre"].ToString();
                    dBAyudaConsumidorFinal.sCodigo = dtConsulta.Rows[0]["identificacion"].ToString();

                    dBAyudaVendedor.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_vendedor"].ToString());
                    dBAyudaVendedor.txtDatos.Text = dtConsulta.Rows[0]["vendedor"].ToString().Trim().ToUpper();
                    dBAyudaVendedor.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_vendedor"].ToString().Trim().ToUpper();
                    dBAyudaVendedor.sNombre = dtConsulta.Rows[0]["vendedor"].ToString();
                    dBAyudaVendedor.sCodigo = dtConsulta.Rows[0]["codigo_vendedor"].ToString();

                    dBAyudaProducto.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_anula"].ToString());
                    dBAyudaProducto.sNombre = dtConsulta.Rows[0]["nombre_producto"].ToString();
                    dBAyudaProducto.sCodigo = dtConsulta.Rows[0]["codigo_producto"].ToString();
                    dBAyudaProducto.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo_producto"].ToString();
                    dBAyudaProducto.txtDatos.Text = dtConsulta.Rows[0]["nombre_producto"].ToString();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //INSTRUCCION PARA OBTENER EL PRECIO DEL PRODUCTO
        private bool obtenerValorProducto()
        {
            try
            {
                sSql = "";
                sSql += "select valor from cv403_precios_productos" + Environment.NewLine;
                sSql += "where id_producto = " + dBAyudaProducto.iId + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and id_lista_precio = 4";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                if (dtAyuda.Rows.Count > 0)
                    dValor = Convert.ToDecimal(dtAyuda.Rows[0]["valor"].ToString());
                else
                    dValor = 0;

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarValoresSistema()
        {
            try
            {
                if (obtenerValorProducto() == false)
                {
                    return;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "cg_ciudad = " + dBAyudaCiudad.iId + "," + Environment.NewLine;
                sSql += "id_pos_cajero = " + dBAyudaCajero.iId + "," + Environment.NewLine;
                sSql += "id_pos_mesero = " + dBAyudaMesero.iId + "," + Environment.NewLine;
                sSql += "id_pos_promotor = " + dBAyudaPromotor.iId + "," + Environment.NewLine;
                sSql += "id_pos_repartidor = " + dbAyudaRepartidor.iId + "," + Environment.NewLine;
                sSql += "consumidor_final = " + dBAyudaConsumidorFinal.iId + "," + Environment.NewLine;
                sSql += "id_vendedor = " + dBAyudaVendedor.iId + "," + Environment.NewLine;
                sSql += "id_producto_anula = " + dBAyudaProducto.iId + "," + Environment.NewLine;
                sSql += "valor_precio_anula = " + dValor + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametrosPredeterminados();
                cargarParametros();
                enviarParametro();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL TAB DE OPCIONES

        //CARGAR DATOS DE LA MONEDA
        private void llenarComboMoneda()
        {
            try
            {
                dtAyuda = new DataTable();
                dtAyuda.Clear();

                sSql = "select * from tp_vw_moneda";

                cmbMoneda.llenar(dtAyuda, sSql);
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
                dtAyuda = new DataTable();
                dtAyuda.Clear();

                sSql = "";
                sSql += "select id_pos_formato_precuenta, descripcion" + Environment.NewLine;
                sSql += "from pos_formato_precuenta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbPrecuenta.llenar(dtAyuda, sSql);
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
                dtAyuda = new DataTable();
                dtAyuda.Clear();

                sSql = "";
                sSql += "select id_pos_formato_factura, descripcion" + Environment.NewLine;
                sSql += "from pos_formato_factura" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbFactura.llenar(dtAyuda, sSql);
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

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                cmbImpresoras.llenar(dtAyuda, sSql);
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
                dtAyuda = new DataTable();
                dtAyuda.Clear();

                sSql = "";
                sSql += "select idtipocomprobante, descripcion" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and codigo in ('Fac', 'Nen')";

                cmbTipoComprobantes.llenar(dtAyuda, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabOpciones()
        {
            try
            {
                llenarComboMoneda();
                llenarComboPrecuenta();
                llenarComboFactura();
                llenarComboImpresoras();
                llenarComboComprobantes();

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidad = 0;
                    iIdPosParametroLocalidad = 0;
                    chkCocina.Checked = false;
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
                }

                else
                {
                    iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                    iIdPosParametroLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro_localidad"].ToString());

                    if (Convert.ToInt32(dtConsulta.Rows[0]["habilitar_destinos_impresion"].ToString()) == 1)
                        chkCocina.Checked = true;
                    else
                        chkCocina.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_jornada"].ToString()) == 1)
                        chkJornada.Checked = true;
                    else
                        chkJornada.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["ejecutar_impresion"].ToString()) == 1)
                        chkEjecutarImpresiones.Checked = true;
                    else
                        chkEjecutarImpresiones.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["permitir_abrir_cajon"].ToString()) == 1)
                        chkAbrirCajon.Checked = true;
                    else
                        chkAbrirCajon.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["descarga_no_procesados"].ToString()) == 1)
                        chkNoProcesados.Checked = true;
                    else
                        chkNoProcesados.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["descarga_receta"].ToString()) == 1)
                        chkUsarRecetas.Checked = true;
                    else
                        chkUsarRecetas.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_promotor"].ToString()) == 1)
                        chkManejaPromotor.Checked = true;
                    else
                        chkManejaPromotor.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_repartidor"].ToString()) == 1)
                        chkManejaRepartidor.Checked = true;
                    else
                        chkManejaRepartidor.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["reimprimir_cocina"].ToString()) == 1)
                        chkReimprimirCocina.Checked = true;
                    else
                        chkReimprimirCocina.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["mostrar_valores_propina"].ToString()) == 1)
                        chkMostrarPropinas.Checked = true;
                    else
                        chkMostrarPropinas.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_mitad"].ToString()) == 1)
                        chkMitad.Checked = true;
                    else
                        chkMitad.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_producto_comision_empleados"].ToString()) == 1)
                    {
                        chkProductosComision.Checked = true;
                        txtValorComision.Text = dtConsulta.Rows[0]["valor_comision_producto_para_empleados"].ToString();
                    }
                    else
                    {
                        chkProductosComision.Checked = false;
                        txtValorComision.Text = "0.00";
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_delivery_variable"].ToString()) == 1)
                        chkDeliveryVariable.Checked = true;
                    else
                        chkDeliveryVariable.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["imprimir_datos_factura"].ToString()) == 1)
                        chkImprimeDatosFactura.Checked = true;
                    else
                        chkImprimeDatosFactura.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["propina_para_tarjetas"].ToString()) == 1)
                        chkImprimeDatosFactura.Checked = true;
                    else
                        chkImprimeDatosFactura.Checked = false;

                    cmbMoneda.SelectedValue = dtConsulta.Rows[0]["cg_moneda"].ToString();
                    cmbPrecuenta.SelectedValue = dtConsulta.Rows[0]["id_pos_formato_precuenta"].ToString();
                    cmbFactura.SelectedValue = dtConsulta.Rows[0]["id_pos_formato_factura"].ToString();
                    cmbImpresoras.SelectedValue = dtConsulta.Rows[0]["id_pos_impresora"].ToString();
                    cmbTipoComprobantes.SelectedValue = dtConsulta.Rows[0]["id_tipo_comprobante_default"].ToString();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarComandasOpciones()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "habilitar_destinos_impresion = " + iDestinosImpresion + "," + Environment.NewLine;
                sSql += "maneja_jornada = " + iManejaJornadas + "," + Environment.NewLine;
                sSql += "ejecutar_impresion = " + iEjecutarImpresiones + "," + Environment.NewLine;
                sSql += "permitir_abrir_cajon = " + +iAbrirCajon + "," + Environment.NewLine;
                sSql += "descarga_no_procesados = " + iDescargarNoProcesados + "," + Environment.NewLine;
                sSql += "descarga_receta = " + iDescargarMateriaPrima + "," + Environment.NewLine;
                sSql += "maneja_promotor = " + iManejaPromotor + "," + Environment.NewLine;
                sSql += "maneja_repartidor = " + iManejaRepartidor + "," + Environment.NewLine;
                sSql += "reimprimir_cocina = " + iReimprimirCocina + "," + Environment.NewLine;
                sSql += "mostrar_valores_propina = " + iMostrarValoresPropina + "," + Environment.NewLine;
                sSql += "maneja_mitad = " + iManejaMitad + "," + Environment.NewLine;
                sSql += "maneja_producto_comision_empleados = " + iManejaProductosComision + "," + Environment.NewLine;
                sSql += "maneja_delivery_variable = " + iManejaDeliveryVariable + "," + Environment.NewLine;
                sSql += "imprimir_datos_factura = " + iImprimirDatosFactura + "," + Environment.NewLine;
                sSql += "propina_para_tarjetas = " + iPropinaTarjetas + "," + Environment.NewLine;
                sSql += "cg_moneda = " + cmbMoneda.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_formato_precuenta = " + cmbPrecuenta.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_formato_factura = " + cmbFactura.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_impresora = " + cmbImpresoras.SelectedValue + "," + Environment.NewLine;
                sSql += "id_tipo_comprobante_default = " + cmbTipoComprobantes.SelectedValue + "," + Environment.NewLine;
                sSql += "valor_comision_producto_para_empleados = " + txtValorComision.Text.Trim() + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametrosPredeterminados();
                cargarParametros();
                enviarParametro();
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

        #region FUNCIONES DEL TAB DE VALORES DE ASIGNACION

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabAsignacion()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidad = 0;
                    iIdPosParametroLocalidad = 0;
                    chkAplicarecargoTarjetas.Checked = false;
                    txtPorcentajeRecargoTarjetas.Text = "0";
                    txtMontoMaximoRecargoTarjetas.Text = "0";
                    txtCantidadImpresiones.Text = "1";
                    txtCantidadVentaExpress.Text = "1";
                    txtCantidadCrearTarjetas.Text = "1";
                    txtPorcentajeDescuentoEmpleados.Text = "0";
                    txtPersonasDefault.Text = "0";
                    txtRutaLogo.Text = "";
                    txtRutaReportes.Text = "";
                    txtCorreoPorDefecto.Text = "";
                }

                else
                {
                    iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                    iIdPosParametroLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro_localidad"].ToString());
                    txtCantidadImpresiones.Text = dtConsulta.Rows[0]["cantidad_reporte_empresa"].ToString().Trim();
                    txtCantidadVentaExpress.Text = dtConsulta.Rows[0]["cantidad_reporte_express"].ToString().Trim();
                    txtCantidadCrearTarjetas.Text = dtConsulta.Rows[0]["cantidad_reporte_crear_tarjetas"].ToString().Trim();
                    txtPorcentajeDescuentoEmpleados.Text = dtConsulta.Rows[0]["porcentaje_descuento_empleados"].ToString().Trim();
                    txtPersonasDefault.Text = dtConsulta.Rows[0]["numero_personas_default"].ToString().Trim();
                    txtRutaLogo.Text = dtConsulta.Rows[0]["logo"].ToString().Trim();
                    txtRutaReportes.Text = dtConsulta.Rows[0]["ruta_reportes"].ToString().Trim();
                    txtCorreoPorDefecto.Text = dtConsulta.Rows[0]["correo_electronico_default"].ToString().Trim();

                    if (Convert.ToInt32(dtConsulta.Rows[0]["aplica_recargo_tarjetas"].ToString()) == 1)
                    {
                        chkAplicarecargoTarjetas.Checked = true;
                        txtPorcentajeRecargoTarjetas.Text = dtConsulta.Rows[0]["porcentaje_recargo_tarjetas"].ToString().Trim();
                        txtMontoMaximoRecargoTarjetas.Text = dtConsulta.Rows[0]["valor_maximo_recargo"].ToString().Trim();
                        txtPorcentajeRecargoTarjetas.ReadOnly = false;
                        txtMontoMaximoRecargoTarjetas.ReadOnly = false;
                    }

                    else
                    {
                        chkAplicarecargoTarjetas.Checked = false;
                        txtPorcentajeRecargoTarjetas.Text = "0";
                        txtMontoMaximoRecargoTarjetas.Text = "0";
                        txtPorcentajeRecargoTarjetas.ReadOnly = true;
                        txtMontoMaximoRecargoTarjetas.ReadOnly = true;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarComandasAsignacion()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "aplica_recargo_tarjetas = " + iManejaRecargoTarjetas + "," + Environment.NewLine;
                sSql += "porcentaje_recargo_tarjetas = " + txtPorcentajeRecargoTarjetas.Text + "," + Environment.NewLine;
                sSql += "valor_maximo_recargo = " + txtMontoMaximoRecargoTarjetas.Text.Trim() + "," + Environment.NewLine;
                sSql += "cantidad_reporte_empresa = " + txtCantidadImpresiones.Text.Trim() + "," + Environment.NewLine;
                sSql += "cantidad_reporte_express = " + txtCantidadVentaExpress.Text.Trim() + "," + Environment.NewLine;
                sSql += "cantidad_reporte_crear_tarjetas = " + txtCantidadCrearTarjetas.Text.Trim() + "," + Environment.NewLine;
                sSql += "porcentaje_descuento_empleados = " + txtPorcentajeDescuentoEmpleados.Text.Trim() + "," + Environment.NewLine;
                sSql += "numero_personas_default = " + txtPersonasDefault.Text.Trim() + "," + Environment.NewLine;
                sSql += "logo = '" + txtRutaLogo.Text.Trim() + "'," + Environment.NewLine;
                sSql += "ruta_reportes = '" + txtRutaReportes.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_electronico_default = '" + txtCorreoPorDefecto.Text.Trim() + "'" + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametrosPredeterminados();
                cargarParametros();
                enviarParametro();
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

        #region FUNCIONES DEL TAB DE PARAMETROS ADICIONALES

        //FUNCION PARA CARGAR LOS PARAMETROS DEL TAB
        private void cargarTabParametrosAdicionales()
        {
            try
            {
                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidad = 0;
                    iIdPosParametroLocalidad = 0;
                    chkLeerMeseroMesas.Checked = false;
                    chkImprimirPrecuentaGuardar.Checked = false;
                    chkFacturacionElectronica.Checked = false;
                    chkUsoNotasEntrega.Checked = false;
                    chkSeleccionMeseroLlevar.Checked = false;
                    chkVistaPreviaImpresiones.Checked = false;
                    chkRemoverIva.Checked = false;
                    chkVentaAlmuerzos.Checked = false;
                    chkUsoDecimales.Checked = false;
                }

                else
                {
                    iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_localidad"].ToString());
                    iIdPosParametroLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_parametro_localidad"].ToString());

                    if (Convert.ToInt32(dtConsulta.Rows[0]["leer_mesero_mesas"].ToString()) == 1)
                        chkLeerMeseroMesas.Checked = true;
                    else
                        chkLeerMeseroMesas.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["imprimir_precuenta_guardar_comanda"].ToString()) == 1)
                        chkImprimirPrecuentaGuardar.Checked = true;
                    else
                        chkImprimirPrecuentaGuardar.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_facturacion_electronica"].ToString()) == 1)
                        chkFacturacionElectronica.Checked = true;
                    else
                        chkFacturacionElectronica.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_nota_entrega"].ToString()) == 1)
                        chkUsoNotasEntrega.Checked = true;
                    else
                        chkUsoNotasEntrega.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["seleccion_mesero_para_llevar"].ToString()) == 1)
                        chkSeleccionMeseroLlevar.Checked = true;
                    else
                        chkSeleccionMeseroLlevar.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["vista_previa_impresion"].ToString()) == 1)
                        chkVistaPreviaImpresiones.Checked = true;
                    else
                        chkVistaPreviaImpresiones.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["descuenta_iva"].ToString()) == 1)
                        chkRemoverIva.Checked = true;
                    else
                        chkRemoverIva.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["maneja_almuerzos"].ToString()) == 1)
                        chkVentaAlmuerzos.Checked = true;
                    else
                        chkVentaAlmuerzos.Checked = false;

                    if (Convert.ToInt32(dtConsulta.Rows[0]["configuracion_decimales"].ToString()) == 1)
                        chkUsoDecimales.Checked = true;
                    else
                        chkUsoDecimales.Checked = false;                    
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarParametrosAdicionales()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    enviarParametro();
                    return;
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "leer_mesero_mesas = " + iLeerMeseroMesas + "," + Environment.NewLine;
                sSql += "imprimir_precuenta_guardar_comanda = " + iImprimirPrecuentaGuardar + "," + Environment.NewLine;
                sSql += "maneja_facturacion_electronica = " + iHabilitarOpcionesFacturacionElectronica + "," + Environment.NewLine;
                sSql += "maneja_nota_entrega = " + +iUsoNotasEntrega + "," + Environment.NewLine;
                sSql += "seleccion_mesero_para_llevar = " + iSeleccionMeseroLlevar + "," + Environment.NewLine;
                sSql += "vista_previa_impresion = " + iVistaPreviaImpresiones + "," + Environment.NewLine;
                sSql += "descuenta_iva = " + iRemoverIva + "," + Environment.NewLine;
                sSql += "maneja_almuerzos = " + iManejaAlmuerzos + "," + Environment.NewLine;
                sSql += "configuracion_decimales = " + iUsoDecimales + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente. Los cambios se aplicarán al reiniciar el programa.";
                ok.ShowDialog();
                parametros.cargarParametrosPredeterminados();
                cargarParametros();
                enviarParametro();
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

        private void frmNuevoParametroLocalidad_Load(object sender, EventArgs e)
        {
            cmbLocalidad.SelectedIndexChanged -= new EventHandler(cmbLocalidad_SelectedIndexChanged);
            llenarComboLocalidad();
            cmbLocalidad.SelectedIndexChanged += new EventHandler(cmbLocalidad_SelectedIndexChanged);
            cargarParametros();
            iBanderaTab = 1;
            enviarParametro();
        }

        private void tbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbControl.SelectedTab == tbControl.TabPages["tabComandas"])
            {
                iBanderaTab = 1;
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabValores"])
            {
                iBanderaTab = 2;
                cargarDbAyuda();
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabOpciones"])
            {
                iBanderaTab = 3;
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabAsignacion"])
            {
                iBanderaTab = 4;
                enviarParametro();
                return;
            }

            if (tbControl.SelectedTab == tbControl.TabPages["tabParametrosAdicionales"])
            {
                iBanderaTab = 5;
                enviarParametro();
                return;
            }
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarParametros();
            enviarParametro();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            verificarDatos();
        }

        private void BtnLimpiarCiudad_Click(object sender, EventArgs e)
        {
            dBAyudaCiudad.limpiar();
        }

        private void BtnLimpiarCajero_Click(object sender, EventArgs e)
        {
            dBAyudaCajero.limpiar();
        }

        private void BtnLimpiarMesero_Click(object sender, EventArgs e)
        {
            dBAyudaMesero.limpiar();
        }

        private void btnLimpiarPromotor_Click(object sender, EventArgs e)
        {
            dBAyudaPromotor.limpiar();
        }

        private void btnLimpiarRepartidor_Click(object sender, EventArgs e)
        {
            dbAyudaRepartidor.limpiar();
        }

        private void btnEliminarCF_Click(object sender, EventArgs e)
        {
            dBAyudaConsumidorFinal.limpiar();
        }

        private void btnLimpiarVendedor_Click(object sender, EventArgs e)
        {
            dBAyudaVendedor.limpiar();
        }

        private void dbAyudaLimpiarProducto_Click(object sender, EventArgs e)
        {
            dBAyudaProducto.limpiar();
        }

        private void chkProductosComision_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProductosComision.Checked == true)
            {
                txtValorComision.ReadOnly = false;
                txtValorComision.Text = dtConsulta.Rows[0]["valor_comision_producto_para_empleados"].ToString().Trim();
                txtValorComision.Focus();
            }

            else
            {
                txtValorComision.ReadOnly = true;
                txtValorComision.Text = "0.00";
                chkDeliveryVariable.Focus();
            }
        }

        private void txtValorComision_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtMontoMaximo_Leave(object sender, EventArgs e)
        {
            if (txtMontoMaximoRecargoTarjetas.Text.Trim() == "")
            {
                txtMontoMaximoRecargoTarjetas.Text = "0";
            }
        }

        private void txtCantidadImpresiones_Leave(object sender, EventArgs e)
        {
            if (txtCantidadImpresiones.Text.Trim() == "")
            {
                txtCantidadImpresiones.Text = "1";
            }
        }

        private void txtCantidadVentaExpress_Leave(object sender, EventArgs e)
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

        private void txtCantidadImpresiones_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtMontoMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtCantidadCrearTarjetas_Leave(object sender, EventArgs e)
        {
            if (txtCantidadCrearTarjetas.Text.Trim() == "")
            {
                txtCantidadCrearTarjetas.Text = "1";
            }
        }

        private void txtCantidadCrearTarjetas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void chkAplicarecargoTarjetas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAplicarecargoTarjetas.Checked == true)
            {
                txtPorcentajeRecargoTarjetas.Text = dtConsulta.Rows[0]["porcentaje_recargo_tarjetas"].ToString();
                txtMontoMaximoRecargoTarjetas.Text = dtConsulta.Rows[0]["valor_maximo_recargo"].ToString().Trim();
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

        private void txtPorcentajeRecargoTarjetas_Leave(object sender, EventArgs e)
        {
            if (txtPorcentajeRecargoTarjetas.Text.Trim() == "")
            {
                txtPorcentajeRecargoTarjetas.Text = "0";
            }
        }

        private void txtPorcentajeRecargoTarjetas_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPersonasDefault_Leave(object sender, EventArgs e)
        {
            if (txtPersonasDefault.Text.Trim() == "")
            {
                txtPersonasDefault.Text = "1";
            }
        }

        private void txtPersonasDefault_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtPorcentajeDescuentoEmpleados_Leave(object sender, EventArgs e)
        {
            if (txtPorcentajeDescuentoEmpleados.Text.Trim() == "")
            {
                txtPorcentajeDescuentoEmpleados.Text = "0";
            }
        }

        private void txtPorcentajeDescuentoEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
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

        private void btnRemoverLogo_Click(object sender, EventArgs e)
        {
            txtRutaLogo.Clear();
            btnExaminarLogo.Focus();
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

        private void btnRemoverReportes_Click(object sender, EventArgs e)
        {
            txtRutaReportes.Clear();
            btnExaminarReportes.Focus();
        }
    }
}
