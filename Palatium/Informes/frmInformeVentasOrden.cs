using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Informes
{
    public partial class frmInformeVentasOrden : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtConsulta;

        bool bRespuesta;

        string sSql;
        string sFechaDesde;
        string sFechaHasta;

        public frmInformeVentasOrden()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE TIPO DE ORIGEN
        private void llenarComboOrden()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion, codigo," + Environment.NewLine;
                sSql += "genera_factura, isnull(id_persona, 0), id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery, repartidor_externo," + Environment.NewLine;
                sSql += "porcentaje_descuento_externo" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbTipoOrden.llenar(dtConsulta, sSql);

                if (cmbTipoOrden.Items.Count > 0)
                {
                    cmbTipoOrden.SelectedIndex = 0;
                }
               
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }



        #endregion

        private void frmInformeVentasOrden_Load(object sender, EventArgs e)
        {
            llenarComboOrden();
        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(TxtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                TxtHasta.Text = calendario.txtFecha.Text;
                sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }
    }
}
