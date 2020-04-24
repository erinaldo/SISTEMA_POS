using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;

namespace Palatium.Registros_Dactilares
{
    public partial class frmPantallaRegistroEmpleadosEmpresas : Form
    {
        private Form activeForm = null;

        private AxZKFPEngX lectorHuellas = new AxZKFPEngX();

        public frmPantallaRegistroEmpleadosEmpresas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private void MoverPanelLateral(Control c)
        {
            pnlLateral.Height = c.Height;
            pnlLateral.Top = c.Top;
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

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            MoverPanelLateral(btnRegistroEmpresas);            
        }

        private void btnIngresarHuellas_Click(object sender, EventArgs e)
        {
            MoverPanelLateral(btnIngresarHuellas);
            abrirFormularioHijo(new Registros_Dactilares.frmLeerHuellas_V2(), 0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContrlHuellas_Click(object sender, EventArgs e)
        {
            MoverPanelLateral(btnControlHuellas);
            abrirFormularioHijo(new Registros_Dactilares.frmInactivarHuellas(0), 0);
        }

        private void btnRegistroEmpresas_Click(object sender, EventArgs e)
        {
            MoverPanelLateral(btnRegistroEmpresas);
            abrirFormularioHijo(new Registros_Dactilares.frmClienteEmpresarial(), 0);
        }

        private void frmPantallaRegistroEmpleadosEmpresas_Load(object sender, EventArgs e)
        {
            if (Program.iUsarLectorHuellas == 1)
            {
                btnIngresarHuellas.Visible = true;
                btnControlHuellas.Visible = true;
            }

            else
            {
                btnIngresarHuellas.Visible = false;
                btnControlHuellas.Visible = false;
            }

            MoverPanelLateral(btnRegistroEmpresas);
            abrirFormularioHijo(new Registros_Dactilares.frmClienteEmpresarial(), 0);
        }

        private void btnRegistroEmpleados_Click(object sender, EventArgs e)
        {
            MoverPanelLateral(btnRegistroEmpleados);
            abrirFormularioHijo(new Registros_Dactilares.frmEmpleadosEmpresas(), 0);
        }

        private void frmPantallaRegistroEmpleadosEmpresas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.iUsarLectorHuellas == 1)
            {
                Controls.Add(lectorHuellas);

                if (lectorHuellas.InitEngine() == 0)
                {
                    lectorHuellas.FPEngineVersion = "9";
                    lectorHuellas.EnrollCount = 3;
                }

                lectorHuellas.CancelEnroll();
                lectorHuellas.EndEngine();
            }
        }
    }
}
