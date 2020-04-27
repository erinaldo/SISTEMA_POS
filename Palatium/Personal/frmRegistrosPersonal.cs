using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Personal
{
    public partial class frmRegistrosPersonal : Form
    {
        private Form activeForm = null;

        public frmRegistrosPersonal()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private void MoverPanelLateral(Control c)
        {
            SidePanel.Height = c.Height;
            SidePanel.Top = c.Top;
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

        #endregion

        private void btnRegistroCajeros_Click(object sender, EventArgs e)
        {
            if (btnRegistroCajeros.AccessibleDescription == "0")
            {
                MoverPanelLateral(btnRegistroCajeros);
                abrirFormularioHijo(new Personal.frmCajeros(), 0);
                btnRegistroCajeros.AccessibleDescription = "1";
                btnHuellasCajeros.AccessibleDescription = "0";
                btnInactivarHuellas.AccessibleDescription = "0";
            }            
        }

        private void frmRegistrosPersonal_Load(object sender, EventArgs e)
        {
            MoverPanelLateral(btnRegistroCajeros);
            abrirFormularioHijo(new Personal.frmCajeros(), 0);
            btnRegistroCajeros.AccessibleDescription = "1";
            btnHuellasCajeros.AccessibleDescription = "0";
            btnInactivarHuellas.AccessibleDescription = "0";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuellasCajeros_Click(object sender, EventArgs e)
        {
            if (btnHuellasCajeros.AccessibleDescription == "0")
            {
                MoverPanelLateral(btnRegistroCajeros);
                abrirFormularioHijo(new Personal.frmIngresoHuellasCajero(), 0);
                btnRegistroCajeros.AccessibleDescription = "0";
                btnHuellasCajeros.AccessibleDescription = "1";
                btnInactivarHuellas.AccessibleDescription = "0";
            }    
        }

        private void btnInactivarHuellas_Click(object sender, EventArgs e)
        {
            if (btnInactivarHuellas.AccessibleDescription == "0")
            {
                MoverPanelLateral(btnRegistroCajeros);
                abrirFormularioHijo(new Personal.frmInactivarHuellasCajeros(), 0);
                btnRegistroCajeros.AccessibleDescription = "0";
                btnHuellasCajeros.AccessibleDescription = "0";
                btnInactivarHuellas.AccessibleDescription = "1";
            }   
        }
    }
}
