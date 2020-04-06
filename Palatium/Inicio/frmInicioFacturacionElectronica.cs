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
    public partial class frmInicioFacturacionElectronica : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmInicioFacturacionElectronica()
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
            Program.iBanderaConsumoVale = 0;
            Program.iIdPersonaConsumoVale = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";
        }

        #endregion

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

        private void btnFacturasSri_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnFacturasSri);
        }

        private void btnFacturasSri_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnFacturasSri);
        }

        private void btnReenviarFacturas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReenviarFacturas);
        }

        private void btnReenviarFacturas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReenviarFacturas);
        }

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

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
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

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void btnReenviarFacturas_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();
            ingresaBoton(btnReenviarFacturas);

            if (Program.iPuedeCobrar == 1)
            {
                Facturacion_Electronica.frmGenerarEnviarFactura facturas = new Facturacion_Electronica.frmGenerarEnviarFactura("01");
                facturas.ShowDialog();
            }

            else
            {
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
            }
        }

        private void frmInicioFacturacionElectronica_Load(object sender, EventArgs e)
        {
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

        private void btnReenviarFacturaIndividual_Click(object sender, EventArgs e)
        {
            Facturacion_Electronica.frmReenviarFacturaIndividual enviar = new Facturacion_Electronica.frmReenviarFacturaIndividual();
            enviar.ShowDialog();
        }
    }
}
