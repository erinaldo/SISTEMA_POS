using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ConexionBD;
using Palatium;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;

namespace Palatium
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        //static void Main()
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //LLAMO A LA CLASE ENVIANDO LOS PARAMETROS DE CONEXION
            ConexionBD.ConexionBD conectar = new ConexionBD.ConexionBD();
            
            Clases.ClaseCargarParametros parametros = new Clases.ClaseCargarParametros();
            Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            Clases.ClaseLlenarMonedas monedas = new Clases.ClaseLlenarMonedas();

            VentanasMensajes.frmMensajeNuevoOk ok;
            VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
            VentanasMensajes.frmMensajeNuevoSiNo SiNo;
            string sMensaje;
            int iRespuesta_P;            
            
            //string path = "C:\\palatium\\config.ini";
            string path = args[0];

            if (File.Exists(path))
            {
                conectar = new ConexionBD.ConexionBD();
                if (conectar.lecturaConfiguracion(path) == true)
                {
                    iIdEmpresa = Convert.ToInt32(conectar.id_Empresa);
                    iCgEmpresa = Convert.ToInt32(conectar.Cg_Empresa);
                    iIdLocalidad = Convert.ToInt32(conectar.id_Localidad);
                    iCgMotivoDespacho = Convert.ToInt32(conectar.Motivo_Despacho);

                    SQLBDATOS = conectar.SQLBDATOS;
                    SQLCONEXION = conectar.SQLCONEXION;
                    SQLSERVIDOR = conectar.SQLSERVIDOR;
                    SQLDNS = conectar.SQLDSN_ODBC;

                    //VERIFICAR LOS PARAMETROS GENERALES
                    //-----------------------------------------------------------------------------------------------------------
                    sMensaje = parametros.cargarParametros();
                    iRespuesta_P = parametros.iRespuesta;

                    if (iRespuesta_P == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Error el iniciar el programa. Favor comuníquese con el administrador";
                        ok.ShowDialog();
                        Application.Exit();
                        return;
                    }

                    if (iRespuesta_P == 1)
                    {
                        SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        SiNo.lblMensaje.Text = sMensaje + Environment.NewLine + "¿Desea configurar el sistema?";
                        SiNo.ShowInTaskbar = true;
                        SiNo.ShowDialog();

                        if (SiNo.DialogResult == DialogResult.OK)
                        {
                            //AsistenteConfiguracion.frmCrearParametrosGenerales crear = new AsistenteConfiguracion.frmCrearParametrosGenerales();
                            //crear.ShowInTaskbar = true;
                            //crear.Show();

                            Application.Run(new AsistenteConfiguracion.frmCrearParametrosGenerales());
                            return;
                        }

                        else
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    //-----------------------------------------------------------------------------------------------------------

                    //VERIFICAR LOS PARAMETROS POR LOCALIDAD
                    //-----------------------------------------------------------------------------------------------------------
                    sMensaje = parametros.cargarParametrosPredeterminados();
                    iRespuesta_P = parametros.iRespuesta;

                    if (iRespuesta_P == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Error el iniciar el programa. Favor comuníquese con el administrador";
                        ok.ShowDialog();
                        Application.Exit();
                        return;
                    }

                    if (iRespuesta_P == 1)
                    {
                        SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        SiNo.lblMensaje.Text = sMensaje + Environment.NewLine + "¿Desea configurar el sistema?";
                        SiNo.ShowDialog();

                        if (SiNo.DialogResult == DialogResult.OK)
                        {
                            //AsistenteConfiguracion.frmCrearParametrosGenerales crear = new AsistenteConfiguracion.frmCrearParametrosGenerales();
                            //crear.ShowInTaskbar = true;
                            //crear.Show();

                            Application.Run(new AsistenteConfiguracion.frmCrearParametrosLocalidad());
                            return;
                        }

                        else
                        {
                            Application.Exit();
                            return;
                        }
                    }

                    //OBTENER EL ID Y EL SERIAL
                    //----------------------------------------------------------------------------------------

                    Licencia.ClaseFuncionesLicencia lic = new Licencia.ClaseFuncionesLicencia();
                    Program.sValor_P = lic.GetSystemInfo("PALATIUM");
                    Program.sIdEquipo = lic.sId;
                    Program.sSerialEquipo = lic.sPass;

                    Licencia.ClaseConsultaEquipo consultarEquipo = new Licencia.ClaseConsultaEquipo();

                    if (consultarEquipo.consultarEquipoLicencia() == false)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = consultarEquipo.sMensajeError;
                        ok.ShowDialog();
                        Application.Exit();
                        return;
                    }
                    
                    string sIdEquipo_REC = consultarEquipo.sIdEquipo_REC;
                    string sSerialEquipo_REC = consultarEquipo.sSerialEquipo_REC;
                    int iVersionDemo_REC = consultarEquipo.iVersionDemo_REC;
                    int iCantidadPermitida_REC = consultarEquipo.iCantidadPermitida_REC;
                    int iCantidadDisponible_REC = consultarEquipo.iCantidadDisponible_REC;
                    int iInsertar_REC = consultarEquipo.iInsertar;

                    Program.iIdPosTerminal = consultarEquipo.iIdPosTerminal_REC;
                    Program.iCantidadPermitida = iCantidadPermitida_REC;
                    Program.iCantidadUsada = iCantidadDisponible_REC;
                    Program.sNombreEquipo = consultarEquipo.sNombreEquipo_REC;
                    Program.iVistaAplicacion = consultarEquipo.iVistaPrevia_REC;

                    if (sIdEquipo_REC != Program.sIdEquipo)
                    {
                        if (sSerialEquipo_REC != Program.sSerialEquipo)
                        {
                            Program.iVersionDemo = 1;
                            Licencia.frmDialogoLicencia verificador = new Licencia.frmDialogoLicencia(iCantidadPermitida_REC - iCantidadDisponible_REC, iInsertar_REC, 1, Program.sNombreEquipo);
                            verificador.ShowDialog();

                            if (verificador.DialogResult != DialogResult.OK)
                            {
                                Application.Exit();
                                return;
                            }
                        }
                    }

                    else
                    {
                        if (iVersionDemo_REC == 1)
                        {
                            Program.iVersionDemo = 1;
                            Licencia.frmDialogoLicencia verificador = new Licencia.frmDialogoLicencia(iCantidadPermitida_REC - iCantidadDisponible_REC, iInsertar_REC, 1, Program.sNombreEquipo);
                            verificador.ShowDialog();

                            if (verificador.DialogResult != DialogResult.OK)
                            {
                                Application.Exit();
                                return;
                            }
                        }
                    }

                    //----------------------------------------------------------------------------------------

                    //AQUI PARA LLENAR LA CONFIGURACION DE FACTURACION ELECTRONICA
                    if (Program.iFacturacionElectronica == 1)
                    {
                        parametros.cargarParametrosFacturacionElectronica();
                    }

                    //sMensaje = parametros.cargarDatosTerminal();
                    
                    if (sMensaje != "")
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = sMensaje;
                        catchMensaje.ShowInTaskbar = false;
                        catchMensaje.ShowDialog();
                    }

                    parametros.cargarDatosImpresion();
                    parametros.cargarDatosEmpresa();
                    parametros.obtenerCgLocalidad();
                    redimension.extraerPixelado();

                    if (Program.iHabilitarDecimal == 1)
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

                        Program.iArregloMenuFila = 12;
                        Program.iArregloMenuColumna = 4;
                    }

                    else
                    {
                        Program.iArregloMenuFila = 10;
                        Program.iArregloMenuColumna = 5;
                    }

                    if (iVistaAplicacion == 1)
                    {
                        Application.Run(new Inicio.frmMenuTab());
                    }

                    else
                    {
                        Inicio.frmIniciarSesion sesion = new Inicio.frmIniciarSesion();
                        sesion.ShowInTaskbar = true;
                        sesion.ShowDialog();

                        if (sesion.DialogResult == DialogResult.OK)
                        {
                            sesion.Close();

                            Clases_Crear_Comandas.ClaseObtenerIdOrigenOrden idOrigen = new Clases_Crear_Comandas.ClaseObtenerIdOrigenOrden();

                            if (idOrigen.consultarDatos("12", "") == false)
                            {
                                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                                catchMensaje.lblMensaje.Text = idOrigen.sMensajeError;
                                catchMensaje.Show();
                                Application.Exit();
                                return;
                            }

                            int iIdOrigen_P = idOrigen.iIdOrigenOrden;

                            Application.Run(new Empresa.frmPantallaEspereAlmuerzos(iIdOrigen_P));
                        }
                    }
                }

                else
                {
                    VentanasMensajes.frmVerDatosConfig config = new VentanasMensajes.frmVerDatosConfig();
                    config.ShowDialog();
                    MessageBox.Show("No se pudo establecer la conexiòn.");
                    Application.Exit();
                }
            }

            else
            {
                MessageBox.Show("No existe el archivo de configuraciòn en la ruta " + path + "\nConsulte con el administrador.");
                Application.Exit();
            }                
        }

        //VARIABLES PARA OBTENER EL ID Y EL SERIAL
        public static string sIdEquipo;
        public static string sSerialEquipo;
        public static string sValor_P;
        public static int iVersionDemo;
        public static int iCantidadPermitida;
        public static int iCantidadUsada;
        public static int iIdPosTerminal;
        public static string sNombreEquipo;
        public static int iVistaAplicacion;

        //VARIABLES PARA FACTURACION ELECTRONICA
        public static int iTipoAmbiente;
        public static int iTipoEmision;
        public static int iTipoCertificado;

        public static string sNumeroRucEmisor;
        public static string sWebServiceEnvioPruebas;
        public static string sWebServiceConsultaPruebas;
        public static string sWebServiceEnvioProduccion;
        public static string sWebServiceConsultaProduccion;
        public static string sRutaCertificado;
        public static string sClaveCertificado;
        public static string sCorreoSmtp;
        public static string sCorreoEmisor;
        public static string sClaveCorreoEmisor;
        public static string sCorreoCopia;
        public static string sCorreoConsumidorFinal;
        public static string sCorreoAmbientePruebas;
        public static int iCorreoPuerto;
        public static int iManejaSSL;

        //VARIABLES DE LA PARAMETRIZACION GENERAL
        //-----------------------------------------------------------------------------------------------------------------------
        public static double iva;
        public static double ice;
        public static double servicio;
        public static double descuento_empleados;
        public static int iLeerMesero;
        public static int iImprimeOrden;
        public static int iManejaServicio;
        public static int iFacturacionElectronica;
        public static int iBanderaNumeroMesa;
        //public static float iTamañoLetraMesa;
        public static int iHabilitarDecimal;
        //public static string sCodigoModificador;
        public static string sLogo;
        public static int iManejaNotaVenta;
        public static int iSeleccionMesero;
        public static int iVistaPreviaImpresiones;
        public static int iUsuarioLogin;
        //public static int iActivaTeclado;
        //public static int iVersionDemo;
        //public static int iUsarReceta;
        public static int iIdProductoModificador;
        public static int iIdProductoDomicilio;
        public static int iIdProductoNuevoItem;
        //public static int iDisenioMesas;
        //public static int iManejaRise;
        public static string sContactoFabricante;
        public static string sSitioWebFabricante;
        public static string sUrlContabilidad;
        public static int iCobrarConSinProductos;
        public static int iDescuentaIva;
        public static int iManejaNomina;
        public static int iManejaAlmuerzos;
        public static int iNumeroPersonasDefault;
        public static string sUrlReportes;
        public static int iAplicaRecargoTarjeta;
        public static decimal dbPorcentajeRecargoTarjeta;
        public static int iComprobanteNotaEntrega;
        public static string sCorreoElectronicoDefault;
        public static int iUsarLectorHuellas;
        public static int iUsarLectorPantallaEspere;
        public static int iIdProductoAlmuerzoDefault;
        //-----------------------------------------------------------------------------------------------------------------------


        //VARIABLES DE LA PARAMETRIZACION POR LOCALIDADES
        //-----------------------------------------------------------------------------------------------------------------------
        public static int iCgLocalidad;
        public static int iIdCajeroDefault;
        public static int iIdMesero;
        public static string nombreMesero;
        public static int iMoneda;
        public static int iFormatoPrecuenta;
        public static int iFormatoFactura;
        public static int iHabilitarDestinosImpresion;
        public static int iIdPersona;
        public static int iIdVendedor;
        public static int iManejaJornada;
        public static string sNombreCajeroDefault;
        public static int iImprimirDatosFactura;
        public static string sCiudadDefault;
        public static int iIdProductoAnular;
        public static double dValorProductoAnular;
        public static string sPasswordAdmin;
        public static int iIdImpresoraReportes;
        public static int iEjecutarImpresion;
        public static int iPermitirAbrirCajon;
        public static Decimal dbValorMaximoRecargoTarjetas;
        public static int iDescargarReceta;
        public static int iDescargarProductosNoProcesados;
        public static int iManejaPromotor;
        public static int iIdPosPromotor;
        public static int iManejaRepartidor;
        public static int iIdPosRepartidor;
        public static string sNombreRepartidor;
        public static int iCantidadImpresionesEmpresa;
        public static int iCantidadImpresionesExpress;
        public static int iReimprimirCocina;
        public static int iTipoComprobantePorDefaultComanda;
        public static int iMostrarValoresPropina;
        public static int iManejaMitad;
        public static int iManejaProductoComisionEmpleados;
        public static int iManejaPropinaSoloTarjetas;
        public static Decimal dbValorComisionEmpleados;
        public static int iManejaDeliveryVariable;
        public static int iCantidadImpresionesTarjetas;
        public static int iComandaVentaRapida;
        public static int iComandaClienteEmpresarial;
        public static int iComandaTarjetaAlmuerzos;
        public static int iComandaConsumoInterno;
        //-----------------------------------------------------------------------------------------------------------------------

        //VERSION DEL PRODUCTO
        public static string sVersionProducto = "3.4";

        //VARIABLE PARA HABILITAR NOTA DE VENTA
        public static int iSeleccionarNotaVenta;        

        //ID PERSONA PARA CONSUMO EMPLEADOS
        public static int iIdPersonaConsumoVale;
        public static int iBanderaConsumoVale;

        public static int iBanderaGrupoCierreCaja = 0;

        public static string sValorCierreCaja = "";     
        
        public static int iHabilitaOpciones;
                

        public static int iCortar = 0;
        public static int iIdPersonaFacturador;
        public static string iIdentificacionFacturador;        

        public static int iArregloMenuFila;
        public static int iArregloMenuColumna;

        public static int iBanderaCerrarVentana = 0;

        public static int iAnchoPantalla;
        public static int iLargoPantalla;

        public static double dCambioPantalla= 0;
        public static double dValorFacturado;

        //VARIABLE PARA GUARDAR LA ETIQUETA DE USUARIO EN EL SISTEMA
        public static string sEtiqueta;
        public static string sEtiquetaAdministrador;

        //SECCION DE DATATABLES
        public static string[,] sDetallesItems = new string[100, 100];
        //public static string[,] sContarDinero = new string[12, 3];
        public static string[,] sContarDinero = { { "1", "0", "0.00" }, { "2", "0", "0.00" }, { "5", "0", "0.00" }, { "10", "0", "0.00" }, { "20", "0", "0.00" }, { "50", "0", "0.00" }, { "100", "0", "0.00" }, { "1 Ctvo.", "0", "0.00" }, { "5 Ctvos.", "0", "0.00" }, { "10 Ctvos.", "0", "0.00" }, { "25 Ctvos.", "0", "0.00" }, { "50 Ctvos.", "0", "0.00" } };
        public static DataTable dtMonedasCierre;

        public static int iContadorDetalle = 0;
        public static int iContadorDetalleMximoX = 100;
        public static int iContadorDetalleMximoY = 100;

        public static int iVerCaja;
        public static int iCgTipoUnidad = 6142;

        public static string sNombreUsuario = "";
        //public static string sNombreUsuarioAdministracion;
        public static string sNombreTerminal;
        public static string sEstadoUsuario;
        
        public static string sCierreCajero;
        public static DateTime sFechaSistema;

        public static int iPuedeCobrar;
        public static int iManejaServicioOrden;
        public static int iMostrarJornada = 0;
        public static int iJornadaRecuperada;
        public static int iUnidadCompraConsumo = 6142;
        public static int iIdPersonaMovimiento;        

        public static string[] sDatosMaximo = new string[5];
        //public static string[] sDatosMaximoAdministracion = new string[5];

        public static int iModoDebug = 0;
        //VARIABLES PARA LA CONSULTA EN LA TABLA POS_ORIGEN_ORDEN
        public static int iIdOrigenOrden;
        public static string sDescripcionOrigenOrden;
        public static int iGeneraFactura;
        public static int iIdPersonaOrigenOrden;
        public static int iIdPosModoDelivery;
        public static int iPresentaOpcionDelivery;
        public static string sCodigoAsignadoOrigenOrden;
        public static string sIdentificacion;

        public static string sIdGrid;
        public static string sFormaPagoGrid;

        //VARIABLES GLOABLES PARA EVITAR REPETIR 

        public static int iBanderaReabrir = 0;

        public static string SQLBDATOS;
        public static string SQLCONEXION;
        public static string SQLSERVIDOR;
        public static string SQLDNS;

        public static int iIdEmpresa;
        public static int iCgEmpresa;
        
        public static int iIdLocalidad;
        public static int iCgMotivoDespacho;
        public static int iCgLocalidadRecuperado;
        public static string sPuntoPartida = "Matriz Quito";
        public static int iCgCiudadEntrega = 0;
        public static string sDireccionEntrega = "Matriz Quito";
        public static int iCgEstadoDespacho = 6970;
        
        public static int iIdFormularioSri = 19;
        public static double valorDescuento = 0;
        public static string sMotivoDescuento = "";
        public static int iBanderaCliente = 0;
        public static int iDomicilioEspeciales = 0;
        public static int iModoDelivery = 0;
        public static double dDescuentoEmpleados = 25;
        public static double dPorcentajeEmpleados = 0;
        public static int iNuevoNumeroPersonas = 0;
        public static string iCodigoAreaTelefono = "02";

        public static int iIdPosCierreCajero;
        public static string sFechaAperturaCajero;
        public static string sEstadoCajero;
        public static int iJornadaCajero;

        public static int iIdProduto = 0;

        public static int iCuentaDiaria=0;

        public static string sMotivoProductoCancelado;
        public static int iBanderaCortesia = 0;

        //VARIABLES SOLO PARA USAR CON EFECTIVO o DINERO ELECTRONICO
        public static int iEfectivo = 1;
        public static int iDineroElectronico = 11;

        public static Double dPropinas = 0;

        public static string sMotivoCortesia;
        public static int iOrigenOrden;

        //Arreglos para guardar nombre de productos
        public static int iCuenta = 0;
        public static string[] sNombreProductos;
        public static string[] sCantidadProductos;
        public static double[] dPreciosProductos;

        //Variables para recuperar datos de la orden
        public static int iNumeroDeOrden;
        public static int iIdCabPedido;
        public static int iIdPosMesa;
        public static string sNombreMesa;
        public static string sNombreCajero;
        public static string sCorreoElectronico;
        public static string sNOmbreOrigenOrden;
        public static string sFechaOrden;
        public static int iNumeroPersonas;

        //VARIABLE PARA GUARDAR EL NUMERO DE ORDEN PADRE
        public static string sREFERENCIA_ORDEN = null;
        //VARIABLE PARA GUARDAR EL NOMBRE DE LA MESA
        public static string sNOMBREMESA = null;
        //VARIABLE PARA GUARDAR LA IDENTIFICACION DEL CLIENTE
        public static string sIDENTIFICACION = null;
        //VARIABLE PARA GUARDAR EL TIPO DE ORDEN
        public static string sIDTIPOSORDEN = null;
        public static string sTIPOSORDEN = null;
        //VARIABLE PARA GUARDAR EL ID DEL PAGO
        public static int iIdPago = 0;
        //VARIABLE PARA GUARDAR LA POSICION DEL VECTOR ORDEN PARA GUARDAR LA POSICION
        public static int iPosicionOrden = 0;
        //FUNCION PÀRA GUARDAR EL ID PERSONA
        public static string sIDPERSONA = null;
        //VARIABLE PARA ABRIR EL FACTURADOR
        public static int iVERIFICADOR=0;
        //VARIABLE PARA ALMACENAR LA JORNADA
        public static int iJORNADA = 0;
        //VARIABLE PARA GUARDAR TEMPORALMENTE EL ID DE LA MESA
        public static int iIDMESA = 0;
               
        //Arreglo para guardar el cambio
        public static double[,] dbCambio = new double[100, 100];

        //Bandera para controlar el estado de la mesa
        public static int iBaderaColorMesa = 0;

        //
        public static double[,] dbTotalDescuento = new double[100, 2];
        //Controlador para Descuento
        public static int iControlaorDescuento = 0;

        //nuevo insertar
        //VARIABLES PARA MANEJO DE TRANSACCION
        public const int G_INICIA_TRANSACCION = 1;
        public const int G_TERMINA_TRANSACCION = 2;
        public const int G_REVERSA_TRANSACCION = 3;

        public static string G_st_query = "";
        public static string GFun_St_Saca_Campo = "";
        public static string GFun_In_Mensaje = "";
        public static string G_st_fecha = "";
        public static string G_st_mensaje = "";

        //===================================================================================
        //===================================================================================
        //Arreglo para guardar datos del cliente
        public static string[,] sClientes = new string[100, 10];
        public static int CLI_CODIGO = 0;
        public static int CLI_CEDULA = 1;
        public static int CLI_NOMBRE_CLIENTE = 2;
        public static int CLI_TELEFONO = 3;
        public static int CLI_CIUDAD = 4;
        public static int CLI_SECTOR = 5;
        public static int CLI_CALLE_PRINCIPAL = 6;
        public static int CLI_CALLE_SECUNDARIA = 7;
        public static int CLI_NUMERACION = 8;
        public static int CLI_REFERENCIA = 9;


        //===================================================================================
        //===================================================================================
        //CODIGO NATIVO DE ESTA SECCION PARA ABAJO
        //===================================================================================
        //===================================================================================

        //Variables para controlar el acceso del cajero
        public static int CAJERO_ID;
        public static int CAJERO_NOMBRE = 1;

        //Variables para controlar los descuentos
        public static double dbValorPorcentaje = 0;
        public static double dbDescuento = 0;

        //Controlador para motivo cortesía
        public static int iControladroMotivoCortesia = 0;

        //Arreglo para guardar el motivo de la cancelación de un producto
        public static string[,] sMotivoCancelacion = new string[100, 2];
        //Controlador para motivo de cancelación
        public static int iControladorMotivoCancelacion = 0;

        //Arreglo para guardar las propinas
        public static double[,] dTotalDePropinas = new double[1000, 2];
        //Controlador para propinas
        public static int iControladorPropinas = 0;

        //Arreglo para guardar el valor de órdenes canceladas
        public static string[,] sTotalOrdenesCanceladas = new string[100, 2];
        //Controlador para órdenes canceladas
        public static int iControladorOrdenesCanceladas = 0;


        //Arreglo para guardar el valor del pago parcial
        public static string[,] dPagoParcial = new string[100, 2];
        //Controlador para Pago Parcial
        public static int icontroladorPagoParcial = 0;


        //ARREGLO PARA GUARDAR EL NÚMERO DE ORDEN DE ÓRDENES HIJAS
        public static int[] numeroOrdenHija = new int[100];

        //Tabla para ver si es hija
        public static string[,] tablaHijas = new string[100, 2];

        //Arreglo para guardar los items de cortesía
        public static string[,] sProductosCortesias = new string[100, 4];
        //Controlador para el número de productos de cortesía
        public static int iControladorCortesias = 0;

        //Arreglos para guardar Produtos Cancelados
        public static string[,] sProductosCancelados = new string[100, 100];
        //Controlador para el numero de productos cancelados
        public static int iControladorProductos = 0;

        //Definición de Constantes\\
        //Constantes Mesas
        public static int NUMERO_MESAS_LARGO = 6;
        public static int NUMERO_MESAS_ANCHO;
        //Constantes Orden

        

        //Variable para controlar el codigo de reabrir Mesas
        public static int contadorDeLasMesas = 1;
        //Variables para los totales de cuentas canceladas
        public static int TotalCuentasCanceladas = 0;
        public static int iTotalCuentasMesa = 0;
        public static float fTotalValorMesa = 0;
        public static int iTotalCuentasDomicilio = 0;
        public static int iTotalCuentasLlevar = 0;
        public static int iTotalCuentasCanjes = 0;
        public static int iTotalCuentasConsumoEmpleados = 0;
        public static int iTotalCuentasCortesias = 0;
        public static int iTotalCuentasFuncionarios = 0;
        public static int iContadorPersonas = 0;
        public static int iTotalCuentasMenuExpress = 0;


        //Tabla Mesas
        public static string[,] tablaMesas = new string[30, 2];

        //Boton Global
        public static Button botonGlobal1;
        public static Button botonGlobal2;

        //Validar Mesa
        public static bool mesausada;

        //Control De Mesas
        public static Button controlMesa;

        //Dirección de Clientes
        public static string cedula;
        public static string cliente;
        public static string telefonoCliente;
        public static string sectorCliente;
        public static string callePrincipal;
        public static string calleSecundaria;
        public static string numeroCliente;
        public static string referenciaCliente;


        //Estado
        public static int ayudaOrden = 0;
        public static string EstadodeOrden;

        //entrada y cierre de caja
        public static string[] entradaSalida;

        public static string entrada = "";
        public static string salida = "";
        public static string horaEntrada = "";
        public static string horaSalida = "";
        public static string fecha = "";

        //Resumen de cierre de caja
        public static string direccion2 = "República del Salvador";
        public static string jornada = "DIURNA";

        public static double totalEfectivo = 0;

        //Iva y Recargo
        

        public static double factorPrecio = 1;

        //Nuevo número de Personas
        public static string nuevoPersonas = "";
        public static string nPersonas = "";
        //Nueva Orden
        public static string nuevaOrden = "";

        //Variables Local Impresion Datos
        #region Variables Local Impresion Datos
        public static string local;
        public static string direccion;
        public static string telefono1;
        public static string telefono2;
        
        #endregion

        //Control indice detalle pedido
        public static int maximodetallePedido = 0;
        //Control indice detallePagos
        public static int maximodetallePagos = 0;
        //pagos

    }
}
