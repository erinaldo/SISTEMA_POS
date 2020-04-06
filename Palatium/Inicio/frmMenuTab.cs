using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Inicio
{
    public partial class frmMenuTab : Form
    {
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoOk ok;

        Clases.ClaseEtiquetaUsuario etiqueta = new Clases.ClaseEtiquetaUsuario();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        private Form activeForm = null;

        int iVerFormulario = 0;
        public frmMenuTab()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA RELLENAR EL ARREGLO DE MAXIMOS
        private void llenarArregloMaximo()
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";
        }


        //FUNCION PARA ABRIR EL FORMULARIO HIJO
        private void abrirFormularioHijo(Form frmHijo, int iOp)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = frmHijo;
            frmHijo.TopLevel = false;
            frmHijo.FormBorderStyle = FormBorderStyle.None;
            frmHijo.Dock = DockStyle.Fill;

            if (iOp == 1)
            {
                frmHijo.MdiParent = this;
            }

            pnlContenedor.Controls.Add(frmHijo);
            pnlContenedor.Tag = frmHijo;
            frmHijo.BringToFront();
            
            if (iOp == 1)
            {
                frmHijo.Show(this);
            }

            else
            {
                frmHijo.Show();
            }
        }

        //FUNCION PARA MOSTRAR LOS BOTONES Y OCULTAR
        public void cambioEstado(bool ok)
        {
            btnRestaurante.Visible = ok;
            btnComedor.Visible = ok;
            btnTarjetasAlmuerzo.Visible = ok;
            btnUtilitarios.Visible = ok;
            //btnRestaurante.Visible = ok;
            //btnReportes.Visible = ok;
            btnCerrarSesion.Visible = ok;

            if (Program.iFacturacionElectronica == 1)
            {
                btnSincronizarSRI.Visible = true;
            }

            else
            {
                btnSincronizarSRI.Visible = false;
            }
        }

        #endregion

        private void btnRestaurante_Click(object sender, EventArgs e)
        {
            if (btnRestaurante.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Inicio.frmInicioRestaurante(), 0);
                btnRestaurante.BackColor = Color.FromArgb(0, 192, 0);
                btnComedor.BackColor = Color.Blue;
                btnTarjetasAlmuerzo.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.Blue;

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "1";
                btnComedor.AccessibleDescription = "0";
                btnTarjetasAlmuerzo.AccessibleDescription = "0";
                btnUtilitarios.AccessibleDescription = "0";
                btnSincronizarSRI.AccessibleDescription = "0";
                btnReportes.AccessibleDescription = "0";
            }
        }

        private void btnUtilitarios_Click(object sender, EventArgs e)
        {
            if (btnUtilitarios.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Inicio.frmUtilitarios(), 0);
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.Blue;
                btnTarjetasAlmuerzo.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.FromArgb(0, 192, 0);
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.Blue;

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "0";
                btnComedor.AccessibleDescription = "0";
                btnTarjetasAlmuerzo.AccessibleDescription = "0";
                btnUtilitarios.AccessibleDescription = "1";
                btnSincronizarSRI.AccessibleDescription = "0";
                btnReportes.AccessibleDescription = "0";
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (iVerFormulario == 1)
            {
                if (btnInicio.AccessibleDescription == "0")
                {
                    abrirFormularioHijo(new Inicio.frmInicioPrograma(), 0);
                    btnRestaurante.BackColor = Color.Blue;
                    btnComedor.BackColor = Color.Blue;
                    btnTarjetasAlmuerzo.BackColor = Color.Blue;
                    btnUtilitarios.BackColor = Color.Blue;
                    btnSincronizarSRI.BackColor = Color.Blue;
                    btnReportes.BackColor = Color.Blue;

                    btnInicio.AccessibleDescription = "1";
                    btnRestaurante.AccessibleDescription = "0";
                    btnComedor.AccessibleDescription = "0";
                    btnTarjetasAlmuerzo.AccessibleDescription = "0";
                    btnUtilitarios.AccessibleDescription = "0";
                    btnSincronizarSRI.AccessibleDescription = "0";
                    btnReportes.AccessibleDescription = "0";
                }
            }

            else
            {
                Inicio.frmIniciarSesion sesion = new frmIniciarSesion();
                sesion.ShowDialog();

                if (sesion.DialogResult == DialogResult.OK)
                {
                    sesion.Close();
                    etiqueta.crearEtiquetaUsuario();
                    lblEtiqueta.Text = Program.sEtiqueta;
                    iVerFormulario = 1;
                    abrirFormularioHijo(new Inicio.frmInicioPrograma(), 0);
                    btnRestaurante.BackColor = Color.Blue;
                    btnComedor.BackColor = Color.Blue;
                    btnUtilitarios.BackColor = Color.Blue;
                    btnSincronizarSRI.BackColor = Color.Blue;
                    btnReportes.BackColor = Color.Blue;

                    btnInicio.AccessibleDescription = "1";
                    btnRestaurante.AccessibleDescription = "0";
                    btnComedor.AccessibleDescription = "0";
                    btnUtilitarios.AccessibleDescription = "0";
                    btnSincronizarSRI.AccessibleDescription = "0";
                    btnReportes.AccessibleDescription = "0";
                    cambioEstado(true);
                }
            }
        }

        private void frmMenuTab_Load(object sender, EventArgs e)
        {
            abrirFormularioHijo(new Inicio.frmInicioPrograma(), 0);
            //Inicio.frmInicioPrograma ini = new Inicio.frmInicioPrograma();
            //pnlContenedor.Controls.Add(ini);
            //ini.Show(this);

            if (Program.iVersionDemo == 1)
                btnActivarProducto.Visible = true;
            else
                btnActivarProducto.Visible = false;
        }

        private void btnComedor_Click(object sender, EventArgs e)
        {
            if (btnComedor.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Inicio.frmInicioComedores(), 0);
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.FromArgb(0, 192, 0);
                btnTarjetasAlmuerzo.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.Blue;

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "0";
                btnComedor.AccessibleDescription = "1";
                btnTarjetasAlmuerzo.AccessibleDescription = "0";
                btnUtilitarios.AccessibleDescription = "0";
                btnSincronizarSRI.AccessibleDescription = "0";
                btnReportes.AccessibleDescription = "0";
            }
        }

        private void btnSincronizarSRI_Click(object sender, EventArgs e)
        {
            if (btnSincronizarSRI.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Inicio.frmInicioFacturacionElectronica(), 0);
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.Blue;
                btnTarjetasAlmuerzo.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.FromArgb(0, 192, 0);
                btnReportes.BackColor = Color.Blue;

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "0";
                btnComedor.AccessibleDescription = "0";
                btnTarjetasAlmuerzo.AccessibleDescription = "0";
                btnUtilitarios.AccessibleDescription = "0";
                btnSincronizarSRI.AccessibleDescription = "1";
                btnReportes.AccessibleDescription = "0";
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (btnReportes.AccessibleDescription == "0")
            {
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.Blue;
                btnTarjetasAlmuerzo.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.FromArgb(0, 192, 0);

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "0";
                btnComedor.AccessibleDescription = "0";
                btnTarjetasAlmuerzo.AccessibleDescription = "0";
                btnUtilitarios.AccessibleDescription = "0";
                btnSincronizarSRI.AccessibleDescription = "0";
                btnReportes.AccessibleDescription = "1";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea cerrar la aplicación?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }  
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea cerrar su sesión?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Program.iBanderaGrupoCierreCaja = 0;
                iVerFormulario = 0;
                abrirFormularioHijo(new Inicio.frmInicioPrograma(), 0);
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.Blue;
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.Blue;
                lblEtiqueta.Text = "DESCONECTADO";                
                cambioEstado(false);
                btnSincronizarSRI.Visible = false;
            }
        }

        private void frmMenuTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();

            Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
            acceso.ShowDialog();

            if (acceso.DialogResult == DialogResult.OK)
            {
                Oficina.frmNuevoMenuConfiguracion menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                menuOficina.ShowInTaskbar = true;
                menuOficina.Show();
            }
        }

        private void btnTarjetasAlmuerzo_Click(object sender, EventArgs e)
        {
            if (btnTarjetasAlmuerzo.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Inicio.frmInicioTarjetaAlmuerzos(), 0);
                btnRestaurante.BackColor = Color.Blue;
                btnComedor.BackColor = Color.Blue;
                btnTarjetasAlmuerzo.BackColor = Color.FromArgb(0, 192, 0);
                btnUtilitarios.BackColor = Color.Blue;
                btnSincronizarSRI.BackColor = Color.Blue;
                btnReportes.BackColor = Color.Blue;

                btnInicio.AccessibleDescription = "0";
                btnRestaurante.AccessibleDescription = "0";
                btnComedor.AccessibleDescription = "0";
                btnTarjetasAlmuerzo.AccessibleDescription = "1";
                btnUtilitarios.AccessibleDescription = "0";
                btnSincronizarSRI.AccessibleDescription = "0";
                btnReportes.AccessibleDescription = "0";
            }
        }

        private void btnActivarProducto_Click(object sender, EventArgs e)
        {
            Licencia.frmDialogoLicencia verificador = new Licencia.frmDialogoLicencia(Program.iCantidadPermitida - Program.iCantidadUsada, 0, 0, Program.sNombreEquipo);
            verificador.ShowInTaskbar = false;
            verificador.ShowDialog();

            if (verificador.DialogResult == DialogResult.OK)
            {
                Application.Restart();
                return;
            }
        }
    }
}
