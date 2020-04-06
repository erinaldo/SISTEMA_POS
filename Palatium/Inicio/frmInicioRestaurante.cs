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
    public partial class frmInicioRestaurante : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseLimpiarArreglos limpiarArreglos = new Clases.ClaseLimpiarArreglos();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeSiNo SiNo;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdorigenOrden;
        int iIdPersona_Rec;

        public frmInicioRestaurante()
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
                    iIdorigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                    Program.iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                    Program.sDescripcionOrigenOrden = dtConsulta.Rows[0]["descripcion"].ToString();
                    Program.iGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0]["genera_factura"].ToString());
                    Program.iManejaServicioOrden = Convert.ToInt32(dtConsulta.Rows[0]["maneja_servicio"].ToString());

                    if ((dtConsulta.Rows[0]["id_persona"].ToString() == null) || (dtConsulta.Rows[0]["id_persona"].ToString() == ""))
                    {
                        Program.iIdPersonaOrigenOrden = 0;
                    }

                    else
                    {
                        Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                        Program.sIDPERSONA = dtConsulta.Rows[0]["id_persona"].ToString();

                    }

                    Program.iIdPosModoDelivery = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_modo_delivery"].ToString());
                    Program.iPresentaOpcionDelivery = Convert.ToInt32(dtConsulta.Rows[0]["presenta_opcion_delivery"].ToString());
                    Program.sCodigoAsignadoOrigenOrden = dtConsulta.Rows[0]["codigo"].ToString();

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
                                Program.sIdGrid = dtConsulta.Rows[0]["id_pos_tipo_forma_cobro"].ToString();
                                Program.sFormaPagoGrid = dtConsulta.Rows[0]["descripcion"].ToString();
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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //MESAS
                        if (dtConsulta.Rows[0][0].ToString() == "1")
                        {
                            btnMesas.Enabled = true;
                        }

                        else
                        {
                            btnMesas.Enabled = false;
                        }

                        //LLEVAR
                        if (dtConsulta.Rows[0][1].ToString() == "1")
                        {
                            btnLlevar.Enabled = true;
                        }

                        else
                        {
                            btnLlevar.Enabled = false;
                        }

                        //DOMICILIO
                        if (dtConsulta.Rows[0][2].ToString() == "1")
                        {
                            btnDomicilios.Enabled = true;
                            //btnDatosClientes.Enabled = true;
                        }

                        else
                        {
                            btnDomicilios.Enabled = false;
                        }

                        //CORTESIAS
                        if (dtConsulta.Rows[0][4].ToString() == "1")
                        {
                            btnCortesias.Enabled = true;
                        }

                        else
                        {
                            btnCortesias.Enabled = false;
                        }

                        //CANJES
                        if (dtConsulta.Rows[0][5].ToString() == "1")
                        {
                            btnCanjes.Enabled = true;
                        }

                        else
                        {
                            btnCanjes.Enabled = false;
                        }

                        //FUNCIONARIOS
                        if (dtConsulta.Rows[0][6].ToString() == "1")
                        {
                            btnFuncionarios.Enabled = true;
                        }

                        else
                        {
                            btnFuncionarios.Enabled = false;
                        }

                        //CONSUMO DE EMPLEADOS
                        if (dtConsulta.Rows[0][7].ToString() == "1")
                        {
                            btnConsumoEmpleados.Enabled = true;
                        }

                        else
                        {
                            btnConsumoEmpleados.Enabled = false;
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Ocurrió un problema en la consulta de los tipos tipos órdenes. Favor comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = sSql;
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

        #endregion

        private void btnRevisar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRevisar);
        }

        private void btnRevisar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRevisar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnMovimientoCaja_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnMovimientoCaja);
        }

        private void btnMovimientoCaja_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnMovimientoCaja);
        }

        private void btnSalidaCajero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalidaCajero);
        }

        private void btnSalidaCajero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalidaCajero);
        }

        private void btnMesas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnMesas);
        }

        private void btnMesas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnMesas);
        }

        private void btnLlevar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnLlevar);
        }

        private void btnLlevar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnLlevar);
        }

        private void btnDomicilios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDomicilios);
        }

        private void btnDomicilios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDomicilios);
        }

        private void btnCanjes_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCanjes);
        }

        private void btnCanjes_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCanjes);
        }

        private void btnCortesias_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCortesias);
        }

        private void btnCortesias_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCortesias);
        }

        private void btnFuncionarios_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnFuncionarios);
        }

        private void btnFuncionarios_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnFuncionarios);
        }

        private void btnConsumoEmpleados_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnConsumoEmpleados);
        }

        private void btnConsumoEmpleados_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnConsumoEmpleados);
        }

        private void btnDatosClientes_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDatosClientes);
        }

        private void btnDatosClientes_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDatosClientes);
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnMesas);

            Program.sIDPERSONA = null;
            consultarDatos("01", "");

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
                //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, Program.iIdPersona);
                or.ShowDialog();
            }
        }

        private void btnDomicilios_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnDomicilios);

            Program.sIDPERSONA = null;
            consultarDatos("03", "");
            Domicilios.frmNumeroTelefono numero = new Domicilios.frmNumeroTelefono(Program.iIdOrigenOrden);
            numero.ShowDialog();
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
                //ACTUALIZACION ELVIS COMANDA
                //=======================================================================================================================
                ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, Program.iIdPersona);
                or.ShowDialog();
                //=======================================================================================================================
                                
                //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                //or.ShowDialog();
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
                //ACTUALIZACION ELVIS COMANDA
                //=======================================================================================================================
                //ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, Program.iIdPersona);
                //or.ShowDialog();
                //=======================================================================================================================
                verificador.Close();
            }
        }

        private void btnConsumoEmpleados_Click(object sender, EventArgs e)
        {
            int iBandera_Rec = 0;

            llenarArregloMaximo();
            ingresaBoton(btnConsumoEmpleados);

            consultarDatos("06", "");
            Program.sIDPERSONA = null;
            
            //frmNombreEmpleado emp = new frmNombreEmpleado();
            //emp.ShowDialog();

            //if (emp.DialogResult == DialogResult.OK)
            //{
            //    emp.Close();
            //}

            ValesConsumos.frmSeleccionValesConsumo emp = new ValesConsumos.frmSeleccionValesConsumo(1);
            emp.ShowDialog();

            if (emp.DialogResult == DialogResult.OK)
            {
                iIdPersona_Rec = emp.iIdPersona;
                iBandera_Rec = 1;
                emp.Close();
            }

            if (iBandera_Rec == 1)
            {
                //ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, iIdPersona_Rec);
                //or.ShowDialog();

                frmVerificadorOrigen verificador = new frmVerificadorOrigen(Program.sDescripcionOrigenOrden);
                verificador.ShowDialog();
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
                //ACTUALIZACION ELVIS COMANDA
                //=======================================================================================================================
                ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, Program.iIdPersona);
                or.ShowDialog();
                //=======================================================================================================================

                //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, 0, 0);
                //or.ShowDialog();
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
                ok = new VentanasMensajes.frmMensajeOK();
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
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnSalidaCajero_Click(object sender, EventArgs e)
        {
            ingresaBoton(btnSalidaCajero);
            llenarArregloMaximo();

            if (Program.iPuedeCobrar == 1)
            {
                Claves_Administrar.frmClaveAccesoFormas clave = new Claves_Administrar.frmClaveAccesoFormas("01");
                clave.ShowDialog();

                if (clave.DialogResult == DialogResult.OK)
                {
                    clave.Close();

                    string sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    sSql += "where estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                    sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                        ok.ShowDialog();
                        return;
                    }

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

        private void frmInicioRestaurante_Load(object sender, EventArgs e)
        {
            habilitarBotonesMenu();

            if (Program.sLogo != "")
            {
                if (File.Exists(Program.sLogo))
                {
                    logo.Image = Image.FromFile(Program.sLogo);
                }
            }

            if (Program.iVersionDemo == 1)
                lblVersionDemo.Visible = true;
            else
                lblVersionDemo.Visible = false;

            lblNombreEquipo.Text = Program.sNombreEquipo;
        }
    }
}
