using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Palatium.Oficina
{
    public partial class frmNuevoMenuConfiguracion : Form
    {
        Clases.ClaseEtiquetaUsuario etiqueta = new Clases.ClaseEtiquetaUsuario();
        Clases.InformePreciosProductos informe;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        public frmNuevoMenuConfiguracion()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VERIFICAR LOS FORMULARIOS ABIERTOS
        private void verificarFormularios(Form frmHijo, Form frmPadre)
        {
            try
            {
                bool cargado = false;

                foreach (Form llamado in frmPadre.MdiChildren)
                {
                    if (llamado.Text == frmHijo.Text)
                    {
                        cargado = true;
                        break;
                    }
                }

                if (!cargado)
                {
                    frmHijo.MdiParent = frmPadre;
                    frmHijo.Show();
                    frmHijo.Focus();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void submenuJornadas_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionJornada jornada = new Formularios.FInformacionJornada();
            verificarFormularios(jornada, this);
        }

        private void submenuLocalidades_Click(object sender, EventArgs e)
        {
            Formularios.FLocalidades localidades = new Formularios.FLocalidades();
            verificarFormularios(localidades, this);
        }

        private void submenuTerminales_Click(object sender, EventArgs e)
        {
            Oficina.frmTerminales terminales = new Oficina.frmTerminales();
            verificarFormularios(terminales, this);
        }

        private void submenuLocalidadesImpresoras_Click(object sender, EventArgs e)
        {
            Formularios.FImpresorasLocalidad localidad = new Formularios.FImpresorasLocalidad();
            verificarFormularios(localidad, this);
        }

        private void submenuCajeros_Click(object sender, EventArgs e)
        {
            Personal.frmCajeros cajero = new Personal.frmCajeros();
            verificarFormularios(cajero, this);
            //Personal.frmPruebaFormulario prueba = new Personal.frmPruebaFormulario();
            //verificarFormularios(prueba, this);
        }

        private void submenuMeseros_Click(object sender, EventArgs e)
        {
            Personal.frmMeseros mesero = new Personal.frmMeseros();
            verificarFormularios(mesero, this);
        }

        private void submenuRepartidores_Click(object sender, EventArgs e)
        {
            Personal.frmRepartidores repartidor = new Personal.frmRepartidores();
            verificarFormularios(repartidor, this);
        }

        private void submenuPromotores_Click(object sender, EventArgs e)
        {
            Personal.frmPromotores promotores = new Personal.frmPromotores();
            verificarFormularios(promotores, this);
        }

        private void submenuMetodosPagos_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionMetodoPago metodos = new Formularios.FInformacionMetodoPago();
            verificarFormularios(metodos, this);
        }

        private void submenuFormasCobros_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPosTipForPag cobros = new Formularios.FInformacionPosTipForPag();
            verificarFormularios(cobros, this);
        }

        private void submenuCobros_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionCobro cobro = new Formularios.FInformacionCobro();
            verificarFormularios(cobro, this);
        }

        private void submenuImpresoras_Click(object sender, EventArgs e)
        {
            Impresoras.frmImpresoras impresoras = new Impresoras.frmImpresoras();
            verificarFormularios(impresoras, this);
        }

        private void submenuDestinoImpresion_Click(object sender, EventArgs e)
        {
            Impresoras.frmImpresionComanda impresion = new Impresoras.frmImpresionComanda();
            verificarFormularios(impresion, this);
        }

        private void submenuCategorias_Click(object sender, EventArgs e)
        {
            Productos.frmCategorias categorias = new Productos.frmCategorias();
            verificarFormularios(categorias, this);
        }

        private void submenuSubCategorias_Click(object sender, EventArgs e)
        {
            Productos.frmSubCategoria subCategorias = new Productos.frmSubCategoria();
            verificarFormularios(subCategorias, this);
        }

        private void submenuIngresoMateriaPrima_Click(object sender, EventArgs e)
        {
            Productos.frmMateriaPrima materia = new Productos.frmMateriaPrima();
            verificarFormularios(materia, this);
        }

        private void submenuIngresoProductos_Click(object sender, EventArgs e)
        {
            Productos.frmIngresoProductos productos = new Productos.frmIngresoProductos();
            verificarFormularios(productos, this);
        }

        private void submenuIngresoSubProductos_Click(object sender, EventArgs e)
        {
            Productos.frmIngresoSubProductos productos = new Productos.frmIngresoSubProductos();
            verificarFormularios(productos, this);
        }

        private void submenuIngresoModificadores_Click(object sender, EventArgs e)
        {
            Productos.FModificadores modificadores = new Productos.FModificadores();
            verificarFormularios(modificadores, this);
        }

        private void submenuItemsAdicionales_Click(object sender, EventArgs e)
        {
            Productos.frmModificadores modificadores = new Productos.frmModificadores();
            verificarFormularios(modificadores, this);
        }

        private void submenuReferenciaInsumos_Click(object sender, EventArgs e)
        {
            Productos.frmReferenciaInsumos insumos = new Productos.frmReferenciaInsumos();
            verificarFormularios(insumos, this);
        }

        private void submenuPreciosProductos_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPrecProduc precios = new Formularios.FInformacionPrecProduc();
            verificarFormularios(precios, this);
        }

        private void submenuClasificacionProductos_Click(object sender, EventArgs e)
        {
            Productos.frmAdministracionClaseProducto clase = new Productos.frmAdministracionClaseProducto();
            verificarFormularios(clase, this);
        }

        private void submenuTipoProductos_Click(object sender, EventArgs e)
        {
            Productos.frmTipoProducto producto = new Productos.frmTipoProducto();
            verificarFormularios(producto, this);
        }

        private void submenuEnmascararItems_Click(object sender, EventArgs e)
        {
            Oficina.frmMascaraItem mascara = new frmMascaraItem();
            verificarFormularios(mascara, this);
        }

        private void submenuTipoOrdenes_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPosOriOrd tipoOrden = new Formularios.FInformacionPosOriOrd();
            verificarFormularios(tipoOrden, this);
        }

        private void submenuSecciones_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPosSecMes secciones = new Formularios.FInformacionPosSecMes();
            verificarFormularios(secciones, this);
        }

        private void submenuMesas_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPosMesa mesas = new Formularios.FInformacionPosMesa();
            verificarFormularios(mesas, this);
        }

        private void submenuRegistroClientes_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionPersonas clientes = new Formularios.FInformacionPersonas();
            verificarFormularios(clientes, this);
        }

        private void submenuListaPrecios_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionLisPrecio listaPrecios = new Formularios.FInformacionLisPrecio();
            verificarFormularios(listaPrecios, this);
        }

        private void submenuProductosPorCategoria_Click(object sender, EventArgs e)
        {
            Formularios.FConsulProdPorCategoria consultaProductos = new Formularios.FConsulProdPorCategoria();
            verificarFormularios(consultaProductos, this);
        }

        private void submenuRecibosCobros_Click(object sender, EventArgs e)
        {
            Formularios.FReciboDeCobros reciboCobros = new Formularios.FReciboDeCobros();
            verificarFormularios(reciboCobros, this); 
        }

        private void submenuProductosUltimoNivel_Click(object sender, EventArgs e)
        {
            Formularios.FProductosUltimoNivel ultimoNivel = new Formularios.FProductosUltimoNivel();
            verificarFormularios(ultimoNivel, this);
        }

        private void submenuCrearReserva_Click(object sender, EventArgs e)
        {
            Formularios.FInformacionReserva reserva = new Formularios.FInformacionReserva();
            verificarFormularios(reserva, this);
        }

        private void submenuParametrosGenerales_Click(object sender, EventArgs e)
        {
            //Parametros.frmParametros parametros = new Parametros.frmParametros();
            Parametros.frmNuevoParametro parametros = new Parametros.frmNuevoParametro();
            verificarFormularios(parametros, this);
        }

        private void submenuParametrosPorLocalidad_Click(object sender, EventArgs e)
        {
            //Parametros.frmParametrosLocalidad localidad = new Parametros.frmParametrosLocalidad();
            Parametros.frmNuevoParametroLocalidad localidad = new Parametros.frmNuevoParametroLocalidad();
            verificarFormularios(localidad, this);
        }

        private void submenuClaveDeAdministracion_Click(object sender, EventArgs e)
        {
            Oficina.frmClaveAdministrador clave = new frmClaveAdministrador();
            verificarFormularios(clave, this);
        }

        private void submenuMailEmisor_Click(object sender, EventArgs e)
        {
            Oficina.frmCorreoEmisor correo = new Oficina.frmCorreoEmisor();
            verificarFormularios(correo, this);
        }

        private void submenuGenerarFacturaRepartidores_Click(object sender, EventArgs e)
        {
            RepartidorExterno.frmRepartidorExterno informe = new RepartidorExterno.frmRepartidorExterno();
            verificarFormularios(informe, this);
        }

        private void submenuFormatoFactura_Click(object sender, EventArgs e)
        {
            Oficina.frmFormatosFactura factura = new Oficina.frmFormatosFactura();
            verificarFormularios(factura, this);
        }

        private void submenuFormatoPrecuenta_Click(object sender, EventArgs e)
        {
            Oficina.frmFormatosPrecuenta precuenta = new Oficina.frmFormatosPrecuenta();
            verificarFormularios(precuenta, this);
        }

        private void submenuBackupBDD_Click(object sender, EventArgs e)
        {
            Oficina.frmBackUp respaldo = new Oficina.frmBackUp();
            verificarFormularios(respaldo, this);
        }

        private void submenuEjecutarSQL_Click(object sender, EventArgs e)
        {
            Oficina.frmEjecutarQuery sql = new frmEjecutarQuery();
            verificarFormularios(sql, this);
        }

        private void submenuReparacionMesas_Click(object sender, EventArgs e)
        {
            Soporte.frmReparacionMesas repararMesas = new Soporte.frmReparacionMesas();
            verificarFormularios(repararMesas, this);
        }

        private void submenuRecuperacionCartera_Click(object sender, EventArgs e)
        {
            Formularios.FRecuperacionCarteraPorFecha cartera = new Formularios.FRecuperacionCarteraPorFecha();
            verificarFormularios(cartera, this);
        }

        private void submenuInformeDiarioVentas_Click(object sender, EventArgs e)
        {
            Formularios.FInformeDiarioVentas informeDiario = new Formularios.FInformeDiarioVentas();
            verificarFormularios(informeDiario, this);
        }

        private void submenuControlFlujoClientes_Click(object sender, EventArgs e)
        {
            Informes.frmReporteControlFlujoDiario control = new Informes.frmReporteControlFlujoDiario();
            verificarFormularios(control, this);
        }

        private void submenuReporteTipoOrden_Click(object sender, EventArgs e)
        {
            Informes.informeDeVentas ventas = new Informes.informeDeVentas();
            verificarFormularios(ventas, this);
        }

        private void submenuReportePorProducto_Click(object sender, EventArgs e)
        {
            Informes.frmInformeProductos productos = new Informes.frmInformeProductos();
            verificarFormularios(productos, this);
        }

        private void submenuReportePorCategoria_Click(object sender, EventArgs e)
        {
            Informes.frmInformeVentasCategorias categoria = new Informes.frmInformeVentasCategorias();
            verificarFormularios(categoria, this);
        }

        private void submenuReportePorCliente_Click(object sender, EventArgs e)
        {
            Informes.frmReporteVentasPorClientes clientes = new Informes.frmReporteVentasPorClientes();
            verificarFormularios(clientes, this);
        }

        private void submenuReporteResumenVentas_Click(object sender, EventArgs e)
        {
            Informes.frmResumenReporteDeVentas ventas = new Informes.frmResumenReporteDeVentas();
            verificarFormularios(ventas, this);
        }

        private void submenuReporteTipoOrdenDetallado_Click(object sender, EventArgs e)
        {
            Informes.frmInformeVentasPorOrigen origen = new Informes.frmInformeVentasPorOrigen();
            verificarFormularios(origen, this);
        }

        private void submenuReporteFormasPago_Click(object sender, EventArgs e)
        {
            Informes.frmInformeDeOrdenesFormaPago pagos = new Informes.frmInformeDeOrdenesFormaPago();
            verificarFormularios(pagos, this);
        }

        private void submenuDashboard_Click(object sender, EventArgs e)
        {
            Informes.frmDashboard dash = new Informes.frmDashboard();
            verificarFormularios(dash, this);
        }

        private void submenuReportePreciosProductos_Click(object sender, EventArgs e)
        {
            Productos.frmListaProductos precios = new Productos.frmListaProductos();
            verificarFormularios(precios, this);
        }

        private void submenuReportePreciosProductosRollo_Click(object sender, EventArgs e)
        {
            informe = new Clases.InformePreciosProductos(1);
            informe.llenarInforme();
        }

        private void submenuReimpresionFacturas_Click(object sender, EventArgs e)
        {
            Formularios.FReimpresionFacturas reimpresionFactura = new Formularios.FReimpresionFacturas();
            verificarFormularios(reimpresionFactura, this);
        }

        private void submenuDefinicionCorta_Click(object sender, EventArgs e)
        {
            Bodega.frmBodegaDefinicionCorta bodega = new Bodega.frmBodegaDefinicionCorta();
            verificarFormularios(bodega, this);
        }

        private void submenuDefinicionDetallada_Click(object sender, EventArgs e)
        {
            Bodega.frmBodegaDefinicionDetallada bodega = new Bodega.frmBodegaDefinicionDetallada();
            verificarFormularios(bodega, this);
        }

        private void submenuIngresoBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmIngresoMateriaPrima ingresos = new Bodega.frmIngresoMateriaPrima();
            verificarFormularios(ingresos, this);
        }

        private void submenuEgresoBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmEgresoMateriaPrima egresos = new Bodega.frmEgresoMateriaPrima();
            verificarFormularios(egresos, this);
        }

        private void submenuDespachoEntreBodegas_Click(object sender, EventArgs e)
        {
            Bodega.frmTransferenciaDeProductos transferncia = new Bodega.frmTransferenciaDeProductos();
            verificarFormularios(transferncia, this);
        }

        private void submenuReporteMovimientosBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmReporteMovimientoBodega reporte = new Bodega.frmReporteMovimientoBodega();
            verificarFormularios(reporte, this);
        }

        private void submenuReporteMovimientosEmpresa_Click(object sender, EventArgs e)
        {
            Bodega.frmReporteMovimientoEmpresa empresa = new Bodega.frmReporteMovimientoEmpresa();
            verificarFormularios(empresa, this);
        }

        private void submenuExistenciaFecha_Click(object sender, EventArgs e)
        {
            Bodega.frmExistenciasAunaFecha fecha = new Bodega.frmExistenciasAunaFecha();
            verificarFormularios(fecha, this);
        }

        private void submenuReporteIngresosBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmReporteDeIngresosRangoDeFechas ingreso = new Bodega.frmReporteDeIngresosRangoDeFechas();
            verificarFormularios(ingreso, this);
        }

        private void submenuReporteEgresosBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmReporteEgresoEnRangoDeFechas egreso = new Bodega.frmReporteEgresoEnRangoDeFechas();
            verificarFormularios(egreso, this);
        }

        private void submenuKardexBodega_Click(object sender, EventArgs e)
        {
            Bodega.frmKardexPorBodega kardex = new Bodega.frmKardexPorBodega();
            verificarFormularios(kardex, this);
        }

        private void submenuKardexEmpresa_Click(object sender, EventArgs e)
        {
            Bodega.frmKardexPorEmpresa kardex = new Bodega.frmKardexPorEmpresa();
            verificarFormularios(kardex, this);
        }

        private void submenuIngresoRecetas_Click(object sender, EventArgs e)
        {
            Receta.frmNuevaAdministracionReceta receta = new Receta.frmNuevaAdministracionReceta();
            //Receta.frmAdministraciónDeReceta receta = new Receta.frmAdministraciónDeReceta();
            verificarFormularios(receta, this);
        }

        private void submenuListadoRecetas_Click(object sender, EventArgs e)
        {
            Receta.frmListarRecetas recetas = new Receta.frmListarRecetas();
            verificarFormularios(recetas, this);
        }

        private void submenuClasificacionReceta_Click(object sender, EventArgs e)
        {
            Receta.frmClasificacionReceta receta = new Receta.frmClasificacionReceta();
            verificarFormularios(receta, this);
        }

        private void submenuTipoReceta_Click(object sender, EventArgs e)
        {
            Receta.frmTipoReceta receta = new Receta.frmTipoReceta();
            verificarFormularios(receta, this);
        }

        private void submenuOrigenReceta_Click(object sender, EventArgs e)
        {
            Receta.frmOrigenReceta receta = new Receta.frmOrigenReceta();
            verificarFormularios(receta, this);
        }

        private void submenuTemperaturaServicio_Click(object sender, EventArgs e)
        {
            Receta.frmTemperatura receta = new Receta.frmTemperatura();
            verificarFormularios(receta, this);
        }

        private void submenuPorcionReceta_Click(object sender, EventArgs e)
        {
            Receta.frmPorcionReceta receta = new Receta.frmPorcionReceta();
            verificarFormularios(receta, this);
        }

        private void submenuTipoUnidades_Click(object sender, EventArgs e)
        {
            Receta.frmTipoUnidadMedida tipoUnidad = new Receta.frmTipoUnidadMedida();
            verificarFormularios(tipoUnidad, this);
        }

        private void submenuUnidadesReceta_Click(object sender, EventArgs e)
        {
            Receta.frmUnidadReceta receta = new Receta.frmUnidadReceta();
            verificarFormularios(receta, this);
        }

        private void submenuEquivalenciasUnidades_Click(object sender, EventArgs e)
        {
            Receta.frmEquivalenciaUnidad equivalencia = new Receta.frmEquivalenciaUnidad();
            verificarFormularios(equivalencia, this);
        }

        private void submenuGenerarXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmGenerarXML reimpresion = new Facturacion_Electronica.frmGenerarXML();
            verificarFormularios(reimpresion, this);
        }

        private void submenuFirmarXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmFirmarComprobanteElectronico firmar = new Facturacion_Electronica.frmFirmarComprobanteElectronico();
            verificarFormularios(firmar, this);
        }

        private void submenuEnviarXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmEnviarComprobanteElectronico enviar = new Facturacion_Electronica.frmEnviarComprobanteElectronico();
            verificarFormularios(enviar, this);
        }

        private void submenuConsultarXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmConsultaRespuestaComprobanteElectronico consultar = new Facturacion_Electronica.frmConsultaRespuestaComprobanteElectronico();
            verificarFormularios(consultar, this);
        }

        private void submenuSincronizacionIndividualXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmSincronizacionIndividual procesar = new Facturacion_Electronica.frmSincronizacionIndividual();
            verificarFormularios(procesar, this);
        }

        private void submenuGeneracionRIDE_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmGeneracionRide ride = new Facturacion_Electronica.frmGeneracionRide();
            verificarFormularios(ride, this);
        }

        private void submenuConsultarComprobantesElectronicos_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmConsultarEstadoComprobantes consulta = new Facturacion_Electronica.frmConsultarEstadoComprobantes();
            verificarFormularios(consulta, this);
        }

        private void submenuEditarClienteXML_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmEditarDatosSinAnularFactura editar = new Facturacion_Electronica.frmEditarDatosSinAnularFactura();
            verificarFormularios(editar, this);
        }

        private void submenuFEEmisor_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmRegistroEmpresa emisor = new Facturacion_Electronica.frmRegistroEmpresa();
            verificarFormularios(emisor, this);
        }

        private void submenuFEEstablecimientos_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmEstablecimientos establecimientos = new Facturacion_Electronica.frmEstablecimientos();
            verificarFormularios(establecimientos, this);
        }

        private void submenuFEDirectorios_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmDirectorios directorios = new Facturacion_Electronica.frmDirectorios();
            verificarFormularios(directorios, this);
        }

        private void submenuFEParametros_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmParametros parametros = new Facturacion_Electronica.frmParametros();
            verificarFormularios(parametros, this);
        }

        private void submenuFETipoEmision_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmTipoEmision emision = new Facturacion_Electronica.frmTipoEmision();
            verificarFormularios(emision, this);
        }

        private void submenuFETipoAmbiente_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmTipoAmbiente ambiente = new Facturacion_Electronica.frmTipoAmbiente();
            verificarFormularios(ambiente, this);
        }

        private void submenuFETipoCertificado_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmTipoCertificado certificado = new Facturacion_Electronica.frmTipoCertificado();
            verificarFormularios(certificado, this);
        }

        private void frmNuevoMenuConfiguracion_Load(object sender, EventArgs e)
        {
            if (Program.iFacturacionElectronica == 1)
            {
                ddlFacturacionElectronica.Visible = true;
            }

            else
            {
                ddlFacturacionElectronica.Visible = false;
            }

            etiqueta.crearEtiquetaAdministracion();
            this.Text = Program.sEtiquetaAdministrador;
        }

        private void frmNuevoMenuConfiguracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.ActiveMdiChild.Close();
            //for (int i = 0; i < this.MdiChildren.Length; i++)
            //{
            //    this.MdiChildren[i].Close();
            //}

            foreach(Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            Inicio.IForm frmInterface = this.Owner as Inicio.IForm;

            if (frmInterface != null)
            {
                frmInterface.mostrarOcultar(2);
            }
        }

        private void ddPalatiumContable_Click(object sender, EventArgs e)
        {
            if (Program.sUrlContabilidad == "")
            {
                ok.lblMensaje.Text = "No se ha definido la ruta del sistema PALATIUM";
                ok.ShowDialog();
            }

            else
            {
                if (File.Exists(Program.sUrlContabilidad))
                {
                    Process.Start(Program.sUrlContabilidad + @" d:\palatium\prd_ostras3.ini 3");
                }

                else
                {
                    ok.lblMensaje.Text = "No se encuentra instalado el sistema PALATIUM.";
                    ok.ShowDialog();
                }
            }
        }

        private void subMenuCargos_Click(object sender, EventArgs e)
        {
            Oficina.frmCargoMovimientos cargos = new frmCargoMovimientos();
            verificarFormularios(cargos, this);
        }

        private void submenuMovimientoCaja_Click(object sender, EventArgs e)
        {
            Oficina.frmRegistrarMovimientos movimiento = new frmRegistrarMovimientos(0);
            verificarFormularios(movimiento, this);
            //this.verificarFormularios((Form)new frmRegistrarMovimientos(0), (Form)this);
        }

        private void submenuCrearEmpresa_Click(object sender, EventArgs e)
        {
            Empresa.frmClienteEmpresarial clienteEmpresarial = new Empresa.frmClienteEmpresarial();
            verificarFormularios(clienteEmpresarial, this);
        }

        private void submenuCrearEmpleados_Click(object sender, EventArgs e)
        {
            Empresa.frmEmpleadosEmpresas empleadosEmpresas = new Empresa.frmEmpleadosEmpresas();
            verificarFormularios(empleadosEmpresas, this);
        }

        private void submenuCrearRegistrosRapidos_Click(object sender, EventArgs e)
        {
            Facturador.frmNuevoClienteRegistro personas = new Facturador.frmNuevoClienteRegistro(1);
            verificarFormularios(personas, this);
        }

        private void subMenuCrearRegistrosMasivos_Click(object sender, EventArgs e)
        {
            Oficina.frmIngresoMasivoPersonal ingreso = new frmIngresoMasivoPersonal();
            verificarFormularios(ingreso, this);
        }

        private void subMenuReportesCierreCaja_Click(object sender, EventArgs e)
        {
            Oficina.frmReportesCierre cierre = new Oficina.frmReportesCierre();
            verificarFormularios(cierre, this);
        }

        private void subMenuoperadorTarjetas_Click(object sender, EventArgs e)
        {
            Oficina.frmOperadorTarjetas operador = new Oficina.frmOperadorTarjetas();
            verificarFormularios(operador, this);
        }

        private void subMenuTiposTarjetas_Click(object sender, EventArgs e)
        {
            Oficina.frmTipoTarjetas tarjeta = new Oficina.frmTipoTarjetas();
            verificarFormularios(tarjeta, this);
        }

        private void submenuIngresoPT_Click(object sender, EventArgs e)
        {
            Bodega.frmIngresoProductoTerminado ingreso = new Bodega.frmIngresoProductoTerminado();
            verificarFormularios(ingreso, this);
        }

        private void submenuEgresoPT_Click(object sender, EventArgs e)
        {
            Bodega.frmEgresoProductoTerminado egreso = new Bodega.frmEgresoProductoTerminado();
            verificarFormularios(egreso, this);
        }

        private void subMenuReaperturarUnaCaja_Click(object sender, EventArgs e)
        {
            Cajero.frmReaperturaCaja reabrir = new Cajero.frmReaperturaCaja();
            verificarFormularios(reabrir, this);
        }

        private void subMenuReingresarEfectivo_Click(object sender, EventArgs e)
        {
            Cajero.frmReingresarMonedas monedas = new Cajero.frmReingresarMonedas();
            verificarFormularios(monedas, this);
        }

        private void subMenuReimprimirCaja_Click(object sender, EventArgs e)
        {
            Cajero.frmRevisarCierres cierre = new Cajero.frmRevisarCierres();
            verificarFormularios(cierre, this);
        }

        private void subMenuModificarComandasEmpresa_Click(object sender, EventArgs e)
        {
            Empresa.frmEditarComandaEmpresa editar = new Empresa.frmEditarComandaEmpresa();
            verificarFormularios(editar, this);
        }

        private void submenuActualizarLote_Click(object sender, EventArgs e)
        {
            Oficina.frmModificarLote lote = new Oficina.frmModificarLote();
            verificarFormularios(lote, this);
        }

        private void submenuManejoPropinas_Click(object sender, EventArgs e)
        {
            Oficina.frmPorcentajePropina propina = new Oficina.frmPorcentajePropina();
            verificarFormularios(propina, this);
        }

        private void submenuReporteGastos_Click(object sender, EventArgs e)
        {
            Oficina.frmReporteEgresosFechas reporte = new Oficina.frmReporteEgresosFechas();
            verificarFormularios(reporte, this);
        }

        private void submenuRegistroProveedor_Click(object sender, EventArgs e)
        {
            Proveedores.frmProveedores proveedor = new Proveedores.frmProveedores();
            verificarFormularios(proveedor, this);
        }

        private void submenuInventarioPT_Click(object sender, EventArgs e)
        {
            Inventario.frmInventarioProductoTerminado inv = new Inventario.frmInventarioProductoTerminado();
            verificarFormularios(inv, this);
        }

        private void subMenuClavesAcceso_Click(object sender, EventArgs e)
        {
            Claves_Administrar.frmMantenimientoClaves claves = new Claves_Administrar.frmMantenimientoClaves();
            verificarFormularios(claves, this);
        }

        private void submenuLogoFacturacion_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmLogoFacturacion logo = new Facturacion_Electronica.frmLogoFacturacion();
            verificarFormularios(logo, this);
        }

        private void subMenuEliminarCajasAbiertas_Click(object sender, EventArgs e)
        {
            Cajero.frmAnularCajaAbierta anular = new Cajero.frmAnularCajaAbierta();
            verificarFormularios(anular, this);
        }

        private void subMenuRegistroEmpleadosAreas_Click(object sender, EventArgs e)
        {
            ValesConsumos.frmAsignarConsumoEmpleados empleado = new ValesConsumos.frmAsignarConsumoEmpleados();
            verificarFormularios(empleado, this);
        }

        private void subMenuRegistroAreas_Click(object sender, EventArgs e)
        {
            ValesConsumos.frmAreasEmpleados area = new ValesConsumos.frmAreasEmpleados();
            verificarFormularios(area, this);
        }

        private void submenuConfigurarMailCierres_Click(object sender, EventArgs e)
        {
            Parametros.frmCorreosEnvioCierre cierre = new Parametros.frmCorreosEnvioCierre();
            verificarFormularios(cierre, this);
        }

        private void subMenuRegistroHuellasDactilares_Click(object sender, EventArgs e)
        {
            Empresa.frmRegistrarHuellaEmpleadoEmpresa huellas = new Empresa.frmRegistrarHuellaEmpleadoEmpresa();
            verificarFormularios(huellas, this);
        }

        private void submenuConfigurarTarjetaAlmuerzo_Click(object sender, EventArgs e)
        {
            Tarjeta_Almuerzo.frmMantenimientoTarjetaAlmuerzo tarjeta = new Tarjeta_Almuerzo.frmMantenimientoTarjetaAlmuerzo();
            verificarFormularios(tarjeta, this);
        }
    }
}
