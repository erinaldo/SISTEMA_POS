using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class frmVerificadorOrigen : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sNombreOrigen;
        string sSql = "";
        DataTable dtConsulta;
        bool bRespuesta = false;

        public frmVerificadorOrigen(string sNombreOrigen)
        {
            this.sNombreOrigen = sNombreOrigen;
            InitializeComponent();
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {
            //Mesas1 mesas = new Mesas1(3,sNombreOrigen);
            //Áreas.frmAreasMesas mesas = new Áreas.frmAreasMesas(3, sNombreOrigen);
            //AddOwnedForm(mesas);
            //mesas.ShowDialog();
            //this.Close();

            Areas.frmSeccionMesas mesas = new Areas.frmSeccionMesas(1);
            mesas.ShowDialog();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnLlevar_Click(object sender, EventArgs e)
        {
            ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, Program.iIdPersonaConsumoVale);
            or.ShowDialog();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDomicilio_Click(object sender, EventArgs e)
        {
            sSql = "select id_pos_modo_delivery from pos_modo_delivery where codigo = '03'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                Program.iModoDelivery = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());

                Program.iDomicilioEspeciales = 1;
                CodDomicilio domicilio = new CodDomicilio();
                domicilio.ShowInTaskbar = false;
                domicilio.ShowDialog();

                if (domicilio.DialogResult == DialogResult.OK)
                {
                    this.Close();
                }
            }

            
        }

        private void frmVerificadorOrigen_Load(object sender, EventArgs e)
        {
            //Program.dbDescuento = 0;
            //Program.dbValorPorcentaje = 0;
        }

        private void frmVerificadorOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

       
    }
}
