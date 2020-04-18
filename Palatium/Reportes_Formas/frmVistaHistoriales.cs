using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Reportes_Formas
{
    public partial class frmVistaHistoriales : Form
    {
        private Form activeForm = null;

        public frmVistaHistoriales()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

        #endregion

        private void frmVistaHistoriales_Load(object sender, EventArgs e)
        {
            abrirFormularioHijo(new Reportes_Formas.frmHistorialClientePorOrigen(1), 0);
            btnHistorialComanda.BackColor = Color.FromArgb(0, 192, 0);
            btnHistorialProducto.BackColor = Color.Blue;
            btnDetallarProductos.BackColor = Color.Blue;

            btnHistorialComanda.AccessibleDescription = "1";
            btnHistorialProducto.AccessibleDescription = "0";
            btnDetallarProductos.AccessibleDescription = "0";
        }

        private void btnHistorialComanda_Click(object sender, EventArgs e)
        {
            if (btnHistorialComanda.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Reportes_Formas.frmHistorialClientePorOrigen(1), 0);
                btnHistorialComanda.BackColor = Color.FromArgb(0, 192, 0);
                btnHistorialProducto.BackColor = Color.Blue;
                btnDetallarProductos.BackColor = Color.Blue;

                btnHistorialComanda.AccessibleDescription = "1";
                btnHistorialProducto.AccessibleDescription = "0";
                btnDetallarProductos.AccessibleDescription = "0";
            }
        }

        private void btnHistorialProducto_Click(object sender, EventArgs e)
        {
            if (btnHistorialProducto.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Reportes_Formas.frmHistorialClientePorProducto(1), 0);
                btnHistorialProducto.BackColor = Color.FromArgb(0, 192, 0);
                btnHistorialComanda.BackColor = Color.Blue;
                btnDetallarProductos.BackColor = Color.Blue;

                btnHistorialProducto.AccessibleDescription = "1";
                btnHistorialComanda.AccessibleDescription = "0";
                btnDetallarProductos.AccessibleDescription = "0";
            }
        }

        private void frmVistaHistoriales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDetallarProductos_Click(object sender, EventArgs e)
        {
            if (btnDetallarProductos.AccessibleDescription == "0")
            {
                abrirFormularioHijo(new Reportes_Formas.frmListadoResumidoConsumo(1), 0);
                btnDetallarProductos.BackColor = Color.FromArgb(0, 192, 0);
                btnHistorialComanda.BackColor = Color.Blue;
                btnHistorialProducto.BackColor = Color.Blue;

                btnHistorialProducto.AccessibleDescription = "0";
                btnHistorialComanda.AccessibleDescription = "0";
                btnDetallarProductos.AccessibleDescription = "1";
            }
        }
    }
}
