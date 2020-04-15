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

namespace Palatium.Inicio
{
    public partial class frmInicioPrograma : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdorigenOrden;

        Clases.ClaseLimpiarArreglos limpiarArreglos = new Clases.ClaseLimpiarArreglos();

        public frmInicioPrograma()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        //FUNCION PARA RELLENAR EL ARREGLO DE MAXIMOS
        private void llenarArregloMaximo()
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";
        }

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

        //FUNCION PARA CONSULTAR LOS DATOS DEL CIERRE DE CAJA
        private void consultarCajaVigente()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_reabrir_caja" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    lblFechaApertura.Text = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("dd-MM-yyyy");
                    lblHoraApertura.Text = dtConsulta.Rows[0]["hora_apertura"].ToString().Trim();
                    lblJornada.Text = dtConsulta.Rows[0]["jornada"].ToString().Trim().ToUpper();
                    lblCajeroApertura.Text = dtConsulta.Rows[0]["cajero"].ToString().Trim().ToUpper();
                    lblEstadoCaja.Text = dtConsulta.Rows[0]["estado_cierre_cajero"].ToString().Trim().ToUpper();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnOficina_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnOficina);
        }

        private void btnOficina_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnOficina);
        }


        private void frmInicioPrograma_Load(object sender, EventArgs e)
        {
            if (Program.sLogo != "")
            {
                if (File.Exists(Program.sLogo))
                {
                    logo.Image = Image.FromFile(Program.sLogo);
                }
            }

            if (Program.iBanderaGrupoCierreCaja == 1)
            {
                grupoDatos.Visible = true;
                consultarCajaVigente();
            }

            else
            {
                grupoDatos.Visible = false;
            }

            if (Program.iVersionDemo == 1)
                lblVersionDemo.Visible = true;
            else
                lblVersionDemo.Visible = false;

            lblNombreEquipo.Text = Program.sNombreEquipo;
        }

        private void lblSitioWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblSitioWeb.Text.Trim());
        }

        private void btnAcerca_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Oficina.frmSoporteTecnico soporte = new Oficina.frmSoporteTecnico();
            soporte.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Reportes_Formas.frmHistorialClientePorProducto historial = new Reportes_Formas.frmHistorialClientePorProducto(0);
            historial.ShowDialog();
        }
    }
}
