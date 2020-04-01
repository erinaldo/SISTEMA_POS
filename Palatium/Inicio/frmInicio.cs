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
    public partial class frmInicio : Form, IForm
    {
        Bar.frmComandaBar comanda;
        Oficina.frmNuevoMenuConfiguracion menuOficina;

        public frmInicio()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        public void mostrarOcultar(int iOp)
        {
            if (iOp == 1)
            {
                comanda.Close();
                this.Show();
            }

            else if (iOp == 2)
            {
                this.Show();
            }
        }

        #endregion

        private void btnPuntoVenta_MouseEnter(object sender, EventArgs e)
        {
            btnPuntoVenta.BackColor = Color.FromArgb(255, 128, 255);
        }

        private void btnPuntoVenta_MouseLeave(object sender, EventArgs e)
        {
            btnPuntoVenta.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void btnConfiguracion_MouseLeave(object sender, EventArgs e)
        {
            btnConfiguracion.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void btnConfiguracion_MouseEnter(object sender, EventArgs e)
        {
            btnConfiguracion.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            //this.ActiveControl = this;
        }

        private void btnPuntoVenta_Click(object sender, EventArgs e)
        {
            Bar.frmLoginBar login = new Bar.frmLoginBar();
            login.ShowDialog();

            if (login.DialogResult == DialogResult.OK)
            {
                login.Close();
                this.Hide();
            }

            else
            {
                return;
            }

            comanda = new Bar.frmComandaBar();
            comanda.Show(this);
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";

            Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
            acceso.ShowDialog();

            if (acceso.DialogResult == DialogResult.OK)
            {
                acceso.Close();
                this.Hide();
                //Oficina.frmNuevoMenuConfiguracion menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                //menuOficina.ShowDialog();
            }

            else
            {
                return;
            }

            menuOficina = new Oficina.frmNuevoMenuConfiguracion();
            menuOficina.ShowInTaskbar = true;
            menuOficina.Show(this);
        }
    }
}
