using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Menú
{
    public partial class frmMenuPos : Form
    {
        //Principal principal = new Principal();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases.ControlResolucion resolucion = new Clases.ControlResolucion();

        ConexionBD.ConexionBD conectar = new ConexionBD.ConexionBD();
        Clases.ClaseLimpiarArreglos limpiarArreglos = new Clases.ClaseLimpiarArreglos();
        Clases.ClaseEtiquetaUsuario etiqueta = new Clases.ClaseEtiquetaUsuario();

        DataTable dtCampo = new DataTable();
        bool bRespuesta = false;
        string sSql;
        DataTable dtConsulta;
        DataTable dtImprimir;

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmVerDatosConfig config = new VentanasMensajes.frmVerDatosConfig();

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        int iCortarPapel;
        int iAbrirCajon;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        string sEstado;

        public frmMenuPos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        //FUNCION PARA RELLENAR EL ARREGLO DE MAXIMOS
        private void llenarArregloMaximo()
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";
        }

        //FUNCION PARA EXTRAER LA PÁGINA WEB DEL FABRICANTE
        private void extraerContactos()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(contacto_fabricante, '0995610690') contacto_fabricante," + Environment.NewLine;
                sSql += "isnull(sitio_web_fabricante, 'www.aplicsis.nets') sitio_web_fabricante" + Environment.NewLine;
                sSql += "from pos_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count> 0)
                    {
                        lblContacto.Text = "CONTACTO: " + dtConsulta.Rows[0]["contacto_fabricante"].ToString();
                        lblSitioWeb.Text = dtConsulta.Rows[0]["sitio_web_fabricante"].ToString();
                    }

                    else
                    {
                        lblContacto.Text = "CONTACTO: 0995610690";
                        lblSitioWeb.Text = "www.aplicsis.net";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
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

        #region INSTRUCCIONES PARA ABRIR EL CAJÓN DE DINERO

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden()
        {
            try
            {
                sSql = "";
                sSql += "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                sSql += "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                sSql += "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                sSql += "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql += "and FF.estado = 'A'" + Environment.NewLine;
                sSql += "and I.estado = 'A'" + Environment.NewLine;
                sSql += "and FF.id_pos_formato_factura = " + Program.iFormatoFactura;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt16(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt16(dtImprimir.Rows[0][6].ToString());

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        imprimir.AbreCajon();
                        imprimir.imprimirReporte(sNombreImpresora);
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO PARA CONSULTAR

        //CONSULTA ÀRA HABILITAR LAS OPCIONES
        private void consultarDatos(string sOpcion, string sAuxiliar)
        {
            try
            {
                Program.sIDPERSONA = null;
                Program.iIdPersonaFacturador = 0;
                Program.iIdentificacionFacturador = "";

                Program.iDomicilioEspeciales = 0;
                Program.iModoDelivery = 0;
                Program.iIDMESA = 0;

                Program.dbValorPorcentaje = 25;
                Program.dbDescuento = Program.dbValorPorcentaje / 100;

                limpiarArreglos.limpiarArregloComentarios();

                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion, genera_factura," + Environment.NewLine;
                sSql += "id_persona, id_pos_modo_delivery, presenta_opcion_delivery," + Environment.NewLine;
                sSql += "codigo, maneja_servicio" + Environment.NewLine;
                sSql += "from pos_origen_orden where codigo = '" + sOpcion + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    Program.iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    Program.sDescripcionOrigenOrden = dtConsulta.Rows[0][1].ToString();
                    Program.iGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                    Program.iManejaServicioOrden = Convert.ToInt32(dtConsulta.Rows[0][7].ToString());

                    if ((dtConsulta.Rows[0][3].ToString() == null) || (dtConsulta.Rows[0][3].ToString() == ""))
                    {
                        Program.iIdPersonaOrigenOrden = 0;
                    }

                    else
                    {
                        Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0][3].ToString());
                        Program.sIDPERSONA = dtConsulta.Rows[0][3].ToString();

                    }
                    Program.iIdPosModoDelivery = Convert.ToInt32(dtConsulta.Rows[0][4].ToString());
                    Program.iPresentaOpcionDelivery = Convert.ToInt32(dtConsulta.Rows[0][5].ToString());
                    Program.sCodigoAsignadoOrigenOrden = dtConsulta.Rows[0][6].ToString();

                    if (Program.iGeneraFactura == 0)
                    {
                        sSql = "";
                        sSql += "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                        sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                        sSql += "where codigo = '" + sAuxiliar + "'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                Program.sIdGrid = dtConsulta.Rows[0][0].ToString();
                                Program.sFormaPagoGrid = dtConsulta.Rows[0][1].ToString();
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                            ok.ShowDialog();
                        }
                    }

                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void activarBotones()
        {
            CodigoCajero c1 = new CodigoCajero();
            c1.ShowInTaskbar = false;
            c1.Owner = this;
            btnCancelar.Enabled = true;
            btnSalidaCajero.Enabled = true;
            btnClienteEmpresarial.Enabled = true;
            btnRevisar.Enabled = true;
            btnEntradaCajero.Enabled = false;
            btnMovimientoCaja.Enabled = true;
            btnCerrarSesion.Enabled = true;
            grupoAccesos.Enabled = true;
            btnCobroAlmuerzos.Enabled = true;
            btnCobroAlmuerzos.Enabled = true;
            btnVentaExpress.Enabled = true;
            btnTarjetaAlmuerzo.Enabled = true;

            if (Program.iFacturacionElectronica == 1)
            {
                //btnNotasCredito.Enabled = true;
                btnFacturasSri.Enabled = true;
                btnEditarFactura.Enabled = true;
            }

            else
            {
                btnFacturasSri.Enabled = false;
                btnEditarFactura.Enabled = false;
            }

            habilitarBotonesMenu();
        }

        private void habilitarBotonesMenu()
        {
            try
            {
                sSql = "";
                sSql += "select maneja_mesas, maneja_llevar, maneja_domicilio, maneja_menu_express, maneja_cortesia, " + Environment.NewLine;
                sSql += "maneja_canjes, maneja_vale_funcionario, maneja_consumo_empleados" + Environment.NewLine;
                sSql += "from pos_parametro_localidad" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                //sSql += "and id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //MESAS
                        if (dtConsulta.Rows[0][0].ToString() == "1")
                        {
                            btnMesas.Enabled = true;
                        }

                        //LLEVAR
                        if (dtConsulta.Rows[0][1].ToString() == "1")
                        {
                            btnLlevar.Enabled = true;
                        }

                        //DOMICILIO
                        if (dtConsulta.Rows[0][2].ToString() == "1")
                        {
                            btnDomicilios.Enabled = true;
                            btnDatosClientes.Enabled = true;
                        }

                        //MENU EXPRESS
                        if (dtConsulta.Rows[0][3].ToString() == "1")
                        {
                            btnRepartidorExterno.Enabled = true;
                        }

                        //CORTESIAS
                        if (dtConsulta.Rows[0][4].ToString() == "1")
                        {
                            btnCortesias.Enabled = true;
                        }

                        //CANJES
                        if (dtConsulta.Rows[0][5].ToString() == "1")
                        {
                            btnCanjes.Enabled = true;
                        }

                        //FUNCIONARIOS
                        if (dtConsulta.Rows[0][6].ToString() == "1")
                        {
                            btnFuncionarios.Enabled = true;
                        }

                        //CONSUMO DE EMPLEADOS
                        if (dtConsulta.Rows[0][7].ToString() == "1")
                        {
                            btnConsumoEmpleados.Enabled = true;
                        }

                        btnEstadisticas.Enabled = true;

                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema en la consulta de los tipos tipos órdenes. Favor comuníquese con el administrador.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void desactivarBotones()
        {
            CodigoCajero c1 = new CodigoCajero();
            c1.ShowInTaskbar = false;
            c1.Owner = this;
            btnMesas.Enabled = false;
            btnLlevar.Enabled = false;
            btnDomicilios.Enabled = false;
            btnEstadisticas.Enabled = false;
            btnRepartidorExterno.Enabled = false;
            btnCancelar.Enabled = false;
            //btnNotasCredito.Enabled = false;
            btnFacturasSri.Enabled = false;
            btnCanjes.Enabled = false;
            btnCortesias.Enabled = false;
            btnFuncionarios.Enabled = false;
            btnConsumoEmpleados.Enabled = false;
            btnSalidaCajero.Enabled = false;
            btnCerrarSesion.Enabled = false;
            btnClienteEmpresarial.Enabled = false;
            //BtnOficina.Enabled = false;
            btnRevisar.Enabled = false;
            btnMovimientoCaja.Enabled = false;
            btnEntradaCajero.Enabled = true;
            grupoAccesos.Enabled = false;
            btnCobroAlmuerzos.Enabled = false;
            btnVentaExpress.Enabled = false;
            btnTarjetaAlmuerzo.Enabled = false;
            //btnSalir.Enabled = true;
        }

        #endregion

        private void btnMesas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnMesas);
        }

        private void btnLlevar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnLlevar);
        }

        private void btnDomicilios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDomicilios);
        }

        private void btnEstadisticas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEstadisticas);
        }

        private void btnRepartidorExterno_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRepartidorExterno);
        }

        private void btnFuncionarios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnFuncionarios);
        }

        private void btnConsumoEmpleados_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnConsumoEmpleados);
        }

        private void btnCanjes_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCanjes);
        }

        private void btnCortesias_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCortesias);
        }

        private void btnRevisar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRevisar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnDatosClientes_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDatosClientes);
        }

        private void btnFacturasSri_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnFacturasSri);
        }

        private void btnReabrirCaja_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReabrirCaja);
        }

        private void btnEntradaCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEntradaCajero);
        }

        private void btnSalidaCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalidaCajero);
        }

        private void btnMovimientoCaja_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnMovimientoCaja);
        }

        private void btnOficina_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnOficina);
        }

        private void btnCerrarSesion_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCerrarSesion);
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnMesas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnMesas);
        }

        private void btnLlevar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnLlevar);
        }

        private void btnDomicilios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDomicilios);
        }

        private void btnEstadisticas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEstadisticas);
        }

        private void btnRepartidorExterno_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRepartidorExterno);
        }

        private void btnFuncionarios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnFuncionarios);
        }

        private void btnConsumoEmpleados_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnConsumoEmpleados);
        }

        private void btnCanjes_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCanjes);
        }

        private void btnCortesias_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCortesias);
        }

        private void btnRevisar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRevisar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnDatosClientes_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDatosClientes);
        }

        private void btnFacturasSri_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnFacturasSri);
        }

        private void btnReabrirCaja_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReabrirCaja);
        }

        private void btnEntradaCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEntradaCajero);
        }

        private void btnSalidaCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalidaCajero);
        }

        private void btnMovimientoCaja_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnMovimientoCaja);
        }

        private void btnOficina_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnOficina);
        }

        private void btnCerrarSesion_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCerrarSesion);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalir);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);

            SiNo.LblMensaje.Text = "¿Está seguro que desea cerrar la aplicación?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }    
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            ingresaBoton(btnCerrarSesion);

            SiNo.LblMensaje.Text = "¿Está seguro que desea cerrar su sesión?";
            SiNo.Owner = this;
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Program.iHabilitaOpciones = 0;
                Program.iPuedeCobrar = 1;
                //Program.CAJERO_ID = 0;
                //Program.iIdMesero = 0;

                btnMesas.Enabled = false;
                btnLlevar.Enabled = false;
                btnDomicilios.Enabled = false;
                btnEstadisticas.Enabled = false;
                btnRepartidorExterno.Enabled = false;
                btnCancelar.Enabled = false;
                //btnReabrirCaja.Enabled = false;
                btnFacturasSri.Enabled = false;
                btnCanjes.Enabled = false;
                btnCortesias.Enabled = false;
                btnFuncionarios.Enabled = false;
                btnConsumoEmpleados.Enabled = false;
                btnSalidaCajero.Enabled = false;
                btnCerrarSesion.Enabled = false;
                btnClienteEmpresarial.Enabled = false;
                //BtnOficina.Enabled = false;
                btnRevisar.Enabled = false;
                btnMovimientoCaja.Enabled = false;
                btnTarjetaAlmuerzo.Enabled = false;
                btnVentaExpress.Enabled = false;

                btnEntradaCajero.Enabled = true;
                grupoAccesos.Enabled = false;

                //etiqueta.crearEtiquetaVacia();
                this.Text = Program.sEtiqueta;
                Program.sNombreUsuario = "";
                Program.sDatosMaximo[0] = "";
                Program.sDatosMaximo[1] = "";
                Program.sDatosMaximo[2] = "";

                frmVerMenu principal = (frmVerMenu)this.MdiParent;
                principal.Text = "DESCONECTADO";

            }
        }

        private void btnAbrirCajonDinero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAbrirCajonDinero);
        }

        private void btnReimprimirFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReimprimirFactura);
        }

        private void btnCambioCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCambioCajero);
        }

        private void btnConsultarPrecios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnConsultarPrecios);
        }

        private void btnRepartidor_MouseEnter(object sender, EventArgs e)
        {
            //ingresaBoton(btnRepartidor);
        }

        private void btnAbrirCajonDinero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAbrirCajonDinero);
        }

        private void btnReimprimirFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReimprimirFactura);
        }

        private void btnCambioCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCambioCajero);
        }

        private void btnConsultarPrecios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnConsultarPrecios);
        }

        private void btnRepartidor_MouseLeave(object sender, EventArgs e)
        {
            //salidaBoton(btnRepartidor);
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnMesas);

            Program.sIDPERSONA = null;
            consultarDatos("01", "");
            
            //Mesas1 mesa = new Mesas1();
            //Áreas.frmAreasMesas mesa = new Áreas.frmAreasMesas();
            //mesa.ShowDialog();

            Areas.frmSeccionMesas mesas = new Areas.frmSeccionMesas(0);
            mesas.ShowDialog();
        }

        private void btnLlevar_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnLlevar);

            Program.sIDPERSONA = null;
            consultarDatos("02", "");

            if (Program.iSeleccionMesero == 1)
            {
                Pedidos.frmMeseroLlevar meseros = new Pedidos.frmMeseroLlevar(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden);
                meseros.ShowDialog();

                if (meseros.DialogResult == DialogResult.OK)
                {
                    meseros.Close();
                }
            }

            else
            {
                Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                or.ShowDialog();
            }
        }

        private void btnDomicilios_Click(object sender, EventArgs e)
        {       
            if (Program.iIdProductoDomicilio == 0)
            {
                ok.LblMensaje.Text = "No se encuentra configurado el ítem de movilización. Favor comúniquese con el administrador.";
                ok.ShowDialog();
            }

            else
            {
                llenarArregloMaximo();
                ingresaBoton(btnDomicilios);

                Program.sIDPERSONA = null;
                consultarDatos("03", "");
                CodDomicilio cd = new CodDomicilio();
                cd.ShowDialog();

                if (cd.DialogResult == DialogResult.OK)
                {
                    cd.Close();
                }
            }            
        }

        private void btnRepartidorExterno_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnRepartidorExterno);

            Pedidos.frmRepartidorExterno repartidor = new Pedidos.frmRepartidorExterno();
            repartidor.ShowDialog();

            if (repartidor.DialogResult == DialogResult.OK)
            {
                repartidor.Close();
                //Orden or = new Orden(Program.sDescripcionOrigenOrden, "0", "0");
                Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                or.ShowInTaskbar = false;
                or.ShowDialog();
            }
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnFuncionarios);

            Program.sIDPERSONA = null;
            consultarDatos("05", "13");

            frmVerificadorOrigen verificador = new frmVerificadorOrigen(Program.sDescripcionOrigenOrden);
            verificador.ShowDialog();

            if (verificador.DialogResult == DialogResult.OK)
            {
                //Orden or = new Orden(Program.sDescripcionOrigenOrden, "0", "0");
                Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                or.ShowDialog();
            }
        }

        private void btnConsumoEmpleados_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnConsumoEmpleados);

            Program.sIDPERSONA = null;
            frmNombreEmpleado emp = new frmNombreEmpleado();
            emp.ShowDialog();  

            if (emp.DialogResult == DialogResult.OK)
            {
                emp.Close();
            }
        }

        private void btnCanjes_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnCanjes);

            Program.sIDPERSONA = null;
            consultarDatos("08", "16");

            frmVerificadorOrigen verificador = new frmVerificadorOrigen(Program.sDescripcionOrigenOrden);
            verificador.ShowDialog();

            if (verificador.DialogResult == DialogResult.OK)
            {
                //Orden or = new Orden(Program.sDescripcionOrigenOrden, "0", "0");
                Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                or.ShowDialog();
            }
        }

        private void btnCortesias_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnCortesias);

            Program.sIDPERSONA = null;
            consultarDatos("04", "12");

            frmVerificadorOrigen verificador = new frmVerificadorOrigen(Program.sDescripcionOrigenOrden);
            verificador.ShowDialog();

            if (verificador.DialogResult == DialogResult.OK)
            {
                //Orden or = new Orden(Program.sDescripcionOrigenOrden, "0", "0");
                Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                or.ShowDialog();
            }  
        }

        private void btnRevisar_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnRevisar);

            Program.iModoDelivery = 0;
            Revisar.Revisar r = new Revisar.Revisar();
            r.ShowDialog();

            if (r.DialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnCancelar);

            if (Program.iPuedeCobrar == 1)
            {
                Program.iModoDelivery = 0;
                CancelarOrdenes c = new CancelarOrdenes();
                c.ShowDialog();
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnDatosClientes_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnDatosClientes);

            //Facturador.frmNuevoCliente personas = new Facturador.frmNuevoCliente("", false);
            Facturador.frmNuevoClienteRegistro personas = new Facturador.frmNuevoClienteRegistro(0);
            personas.ShowDialog();
        }

        private void btnFacturasSri_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnFacturasSri);

            if (Program.iPuedeCobrar == 1)
            {
                Facturacion_Electronica.frmSincronizarFacturas facturas = new Facturacion_Electronica.frmSincronizarFacturas("01");
                facturas.ShowDialog();
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnReabrirCaja_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnReabrirCaja);

            if (Program.iPuedeCobrar == 1)
            {
                Menú.frmCodigoOficina oficina = new Menú.frmCodigoOficina();
                oficina.ShowDialog();

                if (oficina.DialogResult == DialogResult.OK)
                {
                    Pedidos.frmReabrirCaja caja = new Pedidos.frmReabrirCaja();
                    caja.ShowDialog();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnEntradaCajero_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnEntradaCajero);

            if (Program.iUsuarioLogin == 0)
            {
                CodigoCajero c1 = new CodigoCajero();
                c1.Owner = this;
                c1.ShowDialog();

                if (c1.DialogResult == DialogResult.OK)
                {
                    etiqueta.crearEtiquetaUsuario();

                    if (Program.iVerCaja == 1)
                    {
                        activarBotones();

                        frmVerMenu principal = (frmVerMenu)this.MdiParent;
                        principal.Text = Program.sEtiqueta;
                    }

                    else
                    {
                        btnCerrarSesion.Enabled = true;
                        btnSalidaCajero.Enabled = true;
                        btnRevisar.Enabled = true;
                    }


                    this.Text = Program.sEtiqueta;
                    c1.Close();
                }
            }

            else
            {
                Cajero.frmIngresoUsuario c2 = new Cajero.frmIngresoUsuario();
                c2.Owner = this;
                c2.ShowDialog();

                if (c2.DialogResult == DialogResult.OK)
                {
                    etiqueta.crearEtiquetaUsuario();

                    if (Program.iVerCaja == 1)
                    {
                        activarBotones();

                        frmVerMenu principal = (frmVerMenu)this.MdiParent;
                        principal.Text = Program.sEtiqueta;
                    }

                    else
                    {
                        btnCerrarSesion.Enabled = true;
                        btnSalidaCajero.Enabled = true;
                        btnRevisar.Enabled = true;
                    }


                    this.Text = Program.sEtiqueta;
                    c2.Close();
                }
            }
        }

        private void btnSalidaCajero_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnSalidaCajero);

            if (Program.iPuedeCobrar == 1)
            {
                string sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                sSql += "and fecha_orden = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and id_pos_jornada = " + Program.iJORNADA;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) > 0)
                    {
                        Cajero.frmResumenCaja caja = new Cajero.frmResumenCaja(0);
                        caja.ShowDialog();
                    }

                    else
                    {
                        Cajero.frmResumenCaja caja = new Cajero.frmResumenCaja(1);
                        caja.ShowDialog();

                        if (caja.DialogResult == DialogResult.OK)
                        {
                            caja.Close();
                            btnMesas.Enabled = false;
                            btnLlevar.Enabled = false;
                            btnDomicilios.Enabled = false;
                            btnEstadisticas.Enabled = false;
                            btnRepartidorExterno.Enabled = false;
                            btnCancelar.Enabled = false;
                            //btnReabrirCaja.Enabled = false;
                            btnFacturasSri.Enabled = false;
                            btnCanjes.Enabled = false;
                            btnCortesias.Enabled = false;
                            btnFuncionarios.Enabled = false;
                            btnConsumoEmpleados.Enabled = false;
                            btnSalidaCajero.Enabled = false;
                            btnCerrarSesion.Enabled = false;
                            btnClienteEmpresarial.Enabled = false;
                            //BtnOficina.Enabled = false;
                            btnRevisar.Enabled = false;
                            btnMovimientoCaja.Enabled = false;
                            btnEntradaCajero.Enabled = true;

                            etiqueta.crearEtiquetaVacia();
                            this.Text = Program.sEtiqueta;
                        }
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnMovimientoCaja_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnMovimientoCaja);

            if (Program.iPuedeCobrar == 1)
            {
                Oficina.frmMovimientosCaja movimiento = new Oficina.frmMovimientosCaja(0);
                movimiento.Owner = this;
                movimiento.ShowDialog();
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnOficina_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnOficina);

            Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
            acceso.ShowDialog();

            if (acceso.DialogResult == DialogResult.OK)
            {
                //AQUI ABRIMOS EL FORMULARIO DE OFICINA                
                //Oficina.frmMenuOficina menuOficina = new Oficina.frmMenuOficina();
                //Oficina.frmMenuConfiguracion menuOficina = new Oficina.frmMenuConfiguracion();
                Oficina.frmNuevoMenuConfiguracion menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                menuOficina.ShowDialog();
            }
        }

        private void btnAbrirCajonDinero_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnAbrirCajonDinero);

            if (Program.iPuedeCobrar == 1)
            {
                Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
                acceso.ShowInTaskbar = false;
                acceso.ShowDialog();

                if (acceso.DialogResult == DialogResult.OK)
                {
                    //AQUI ABRIMOS EL FORMULARIO DE OFICINA           
                    consultarImpresoraTipoOrden();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para utilizar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnReimprimirFactura_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnReimprimirFactura);

            if (Program.iPuedeCobrar == 1)
            {
                Facturador.frmReimprimirFactura factura = new Facturador.frmReimprimirFactura();
                factura.ShowDialog();

                if (factura.DialogResult == DialogResult.OK)
                {
                    factura.Close();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnCambioCajero_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnCambioCajero);

            if (Program.iPuedeCobrar == 1)
            {
                Cajero.frmCambiarCajero cambiar = new Cajero.frmCambiarCajero();
                cambiar.ShowDialog();

                if (cambiar.DialogResult == DialogResult.OK)
                {
                    etiqueta.crearEtiquetaUsuario();
                    frmVerMenu principal = (frmVerMenu)this.MdiParent;
                    principal.Text = Program.sEtiqueta;

                    cambiar.Close();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }  
        }

        private void btnConsultarPrecios_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnConsultarPrecios);

            Menú.frmConsultarPreciosProductos precios = new Menú.frmConsultarPreciosProductos();
            precios.ShowDialog();
        }

        private void btnInformación_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            config.ShowDialog();
        }

        private void btnRepartidor_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            //ingresaBoton(btnRepartidor);
        }

        private void btnSoporte_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            Oficina.frmSoporteTecnico soporte = new Oficina.frmSoporteTecnico();
            soporte.ShowDialog();
        }

        private void frmMenuPos_Load(object sender, EventArgs e)
        {
            //Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            //redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);

            extraerContactos();           

            if (btnEntradaCajero.Enabled == true)
            {
                this.Text = "";
                desactivarBotones();
            }

            if (Program.sLogo != "")
            {
                if (File.Exists(Program.sLogo))
                {
                    logo.Image = Image.FromFile(Program.sLogo);
                }
            }

            if (Program.iManejaAlmuerzos == 1)
            {
                btnCobroAlmuerzos.Visible = true;
            }

            else
            {
                btnCobroAlmuerzos.Visible = false;
            }
        }

        private void btnAnularFactura_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnAnularFactura);

            if (Program.iPuedeCobrar == 1)
            {
                Facturador.frmBuscarTicket buscarOrden = new Facturador.frmBuscarTicket();
                buscarOrden.ShowDialog();

                if (buscarOrden.DialogResult == DialogResult.OK)
                {
                    buscarOrden.Close();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnEditarFactura_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnEditarFactura);

            if (Program.iPuedeCobrar == 1)
            {
                Facturador.frmEditarDatosClienteFactura editar = new Facturador.frmEditarDatosClienteFactura();
                editar.ShowDialog();
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnEditarFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEditarFactura);
        }

        private void btnEditarFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEditarFactura);
        }

        private void btnAnularFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAnularFactura);
        }

        private void btnAnularFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAnularFactura);
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            //DashBoard.frmNuevoDashboard dashboard = new DashBoard.frmNuevoDashboard();
            DashBoard.frmDashBoard dashboard = new DashBoard.frmDashBoard();
            dashboard.ShowDialog();
        }

        private void lblSitioWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblSitioWeb.Text.Trim());
        }

        private void btnCobroAlmuerzos_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCobroAlmuerzos);
        }

        private void btnCobroAlmuerzos_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCobroAlmuerzos);
        }

        private void btnCobroAlmuerzos_Click(object sender, EventArgs e)
        {
            Pedidos.frmCobrarAlmuerzos almuerzo = new Pedidos.frmCobrarAlmuerzos();
            almuerzo.ShowDialog();
        }

        private void frmMenuPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.M)
            {
                btnMesas_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.L)
            {
                btnLlevar_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                btnEstadisticas_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                btnRevisar_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.E)
            {
                btnCancelar_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.K)
            {
                btnSalidaCajero_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.F)
            {
                btnMovimientoCaja_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.O)
            {
                btnOficina_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Q)
            {
                btnCobroAlmuerzos_Click(sender, e);
            }
        }


        private void btnAcerca_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llenarArregloMaximo();
            //config.ShowDialog();
            Oficina.frmSoporteTecnico soporte = new Oficina.frmSoporteTecnico();
            soporte.ShowDialog();
        }

        private void btnCambioOrigen_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();

            if (Program.iPuedeCobrar == 1)
            {
                Pedidos.frmConvertirComanda convertir = new Pedidos.frmConvertirComanda();
                convertir.ShowDialog();
            }
            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }            
        }

        private void btnCambioOrigen_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCambioOrigen);
        }

        private void btnCambioOrigen_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCambioOrigen);
        }

        private void btnClienteEmpresarial_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnClienteEmpresarial);
            Program.sIDPERSONA = (string)null;
            consultarDatos("12", "");

            Empresa.frmSeleccionEmpresaEmpleado seleccion = new Empresa.frmSeleccionEmpresaEmpleado(Program.iIdOrigenOrden);
            seleccion.ShowDialog();
        }

        private void btnClienteEmpresarial_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnClienteEmpresarial);
        }

        private void btnClienteEmpresarial_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnClienteEmpresarial);
        }

        private void btnVentaExpress_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnVentaExpress);

            consultarDatos("10", "");

            Comida_Rapida.frmComandaComidaRapida comanda = new Comida_Rapida.frmComandaComidaRapida(Program.iIdOrigenOrden, 0);
            comanda.ShowDialog();
        }

        private void btnVentaExpress_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnVentaExpress);
        }

        private void btnVentaExpress_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnVentaExpress);
        }

        private void btnTarjetaAlmuerzo_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnTarjetaAlmuerzo);

            consultarDatos("13", "");

            Comida_Rapida.frmComandaComidaRapida comanda = new Comida_Rapida.frmComandaComidaRapida(Program.iIdOrigenOrden, 1);
            comanda.ShowDialog();
        }

        private void btnTarjetaAlmuerzo_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnTarjetaAlmuerzo);
        }

        private void btnTarjetaAlmuerzo_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnTarjetaAlmuerzo);
        }
    }
}
