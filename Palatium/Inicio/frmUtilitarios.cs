using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Inicio
{
    public partial class frmUtilitarios : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseLimpiarArreglos limpiarArreglos = new Clases.ClaseLimpiarArreglos();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeSiNo SiNo;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmUtilitarios()
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

        //CONSULTA ÀRA HABILITAR LAS OPCIONES
        private void consultarDatos(string sOpcion, string sAuxiliar)
        {
            try
            {
                Program.iBanderaConsumoVale = 0;
                Program.iIdPersonaConsumoVale = 0;
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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

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

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

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
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                            ok.ShowDialog();
                        }
                    }

                }
                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
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

        #endregion

        private void btnAbrirCajonDinero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAbrirCajonDinero);
        }

        private void btnAbrirCajonDinero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAbrirCajonDinero);
        }

        private void btnReimprimirFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReimprimirFactura);
        }

        private void btnReimprimirFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReimprimirFactura);
        }

        private void btnAnularFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnAnularFactura);
        }

        private void btnAnularFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnAnularFactura);
        }

        private void btnEditarFactura_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEditarFactura);
        }

        private void btnEditarFactura_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEditarFactura);
        }

        private void btnCambioCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCambioCajero);
        }

        private void btnCambioCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCambioCajero);
        }

        private void btnConsultarPrecios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnConsultarPrecios);
        }

        private void btnConsultarPrecios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnConsultarPrecios);
        }

        private void btnReabrirCaja_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDetalleConsumo);
        }

        private void btnReabrirCaja_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDetalleConsumo);
        }

        private void btnCambioOrigen_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCambioOrigen);
        }

        private void btnCambioOrigen_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCambioOrigen);
        }

        private void btnOficina_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnOficina);
        }

        private void btnOficina_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnOficina);
        }

        private void btnAbrirCajonDinero_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnAbrirCajonDinero);

            if (Program.iPuedeCobrar == 1)
            {
                Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
                acceso.ShowDialog();

                if (acceso.DialogResult == DialogResult.OK)
                {
                    abrir.consultarImpresoraAbrirCajon();
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
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
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
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
                ok = new VentanasMensajes.frmMensajeOK();
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
                ok = new VentanasMensajes.frmMensajeOK();
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
                    //etiqueta.crearEtiquetaUsuario();
                    frmVerMenu principal = (frmVerMenu)this.MdiParent;
                    principal.Text = Program.sEtiqueta;

                    cambiar.Close();
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
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

        private void btnReabrirCaja_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnDetalleConsumo);

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
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
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
                ok = new VentanasMensajes.frmMensajeOK();
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
                Oficina.frmNuevoMenuConfiguracion menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                menuOficina.ShowInTaskbar = true;
                menuOficina.Show();
            }
        }

        private void frmUtilitarios_Load(object sender, EventArgs e)
        {
            if (Program.sLogo != "")
            {
                if (File.Exists(Program.sLogo))
                {
                    logo.Image = Image.FromFile(Program.sLogo);
                }
            }

            if (Program.iFacturacionElectronica == 1)
            {
                btnEditarFactura.Enabled = true;
            }

            else
            {
                btnEditarFactura.Enabled = false;
            }

            if (Program.iVersionDemo == 1)
                lblVersionDemo.Visible = true;
            else
                lblVersionDemo.Visible = false;

            lblNombreEquipo.Text = Program.sNombreEquipo;
        }

        private void btnSalidaCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnListarVentas);
        }

        private void btnSalidaCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnListarVentas);
        }

        private void btnEliminarPedido_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnEliminarPedido);

            if (Program.iPuedeCobrar == 1)
            {
                Cancelar_Orden.frmEliminarComanda comanda = new Cancelar_Orden.frmEliminarComanda();
                comanda.ShowDialog();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para utilizar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnEliminarPedido_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEliminarPedido);
        }

        private void btnEliminarPedido_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEliminarPedido);
        }

        private void btnDashboard_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDashboard);
        }

        private void btnDashboard_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDashboard);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashBoard.frmDashBoard dashboard = new DashBoard.frmDashBoard();
            dashboard.ShowDialog();
        }

        private void btnListarVentas_Click(object sender, EventArgs e)
        {
            ingresaBoton(btnListarVentas);
            llenarArregloMaximo();

            if (Program.iPuedeCobrar == 1)
            {
                Claves_Administrar.frmClaveAccesoFormas clave = new Claves_Administrar.frmClaveAccesoFormas("01");
                clave.ShowDialog();

                if (clave.DialogResult == DialogResult.OK)
                {
                    clave.Close();

                    Utilitarios.frmListarCajasVentas listar = new Utilitarios.frmListarCajasVentas();
                    listar.ShowDialog();
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnDetalleConsumo_Click(object sender, EventArgs e)
        {
            ingresaBoton(btnDetalleConsumo);
            llenarArregloMaximo();

            if (Program.iPuedeCobrar == 1)
            {
                Claves_Administrar.frmClaveAccesoFormas clave = new Claves_Administrar.frmClaveAccesoFormas("01");
                clave.ShowDialog();

                if (clave.DialogResult == DialogResult.OK)
                {
                    clave.Close();
                    string sCodigo_P;

                    Utilitarios.frmSeleccionConsumoInternoEmpleados seleccion = new Utilitarios.frmSeleccionConsumoInternoEmpleados();
                    seleccion.ShowDialog();

                    if (seleccion.DialogResult == DialogResult.OK)
                    {
                        sCodigo_P = seleccion.sCodigo;
                        seleccion.Close();

                        if (sCodigo_P == "06")
                        {
                            Utilitarios.frmDetalleConsumoEmpleados detalle = new Utilitarios.frmDetalleConsumoEmpleados();
                            detalle.ShowDialog();
                        }

                        else if (sCodigo_P == "14")
                        {
                            Utilitarios.frmDetallePorOrigenSinFactura detalle = new Utilitarios.frmDetallePorOrigenSinFactura();
                            detalle.ShowDialog();
                        }
                    }
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }            
        }

        private void btnListarComandas_Click(object sender, EventArgs e)
        {
            Utilitarios.frmListarComandasRangoFechaHora comandas = new Utilitarios.frmListarComandasRangoFechaHora();
            comandas.ShowDialog();
        }

        private void btnCobrarComandasPendientes_Click(object sender, EventArgs e)
        {
            ComandaNueva.frmComandasCuentasPorCobrar cobrar = new ComandaNueva.frmComandasCuentasPorCobrar();
            cobrar.ShowDialog();
        }
    }
}
