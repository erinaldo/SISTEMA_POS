using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmEditarDatosSinAnularFactura : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmEditarDatosSinAnularFactura()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR EL COMBO DE EMPRESA
        private void LLenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql = sSql + "select idempresa,isnull(nombrecomercial, razonsocial) nombre_comercial, *" + Environment.NewLine;
                sSql = sSql + "from sis_empresa" + Environment.NewLine;
                sSql = sSql + "where idempresa = " + Program.iIdEmpresa;

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count >= 1)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBO DE LOCALIDADES
        private void LLenarComboLocalidad()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql = sSql + "select id_localidad,nombre_localidad" + Environment.NewLine;
                sSql = sSql + "from tp_vw_localidades" + Environment.NewLine;
                sSql = sSql + "where id_localidad = " + Program.iIdLocalidad;

                cmbLocalidad.llenar(dtConsulta, sSql);

                if (cmbLocalidad.Items.Count >= 1)
                {
                    cmbLocalidad.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmEditarDatosSinAnularFactura_Load(object sender, EventArgs e)
        {
            LLenarComboEmpresa();
            LLenarComboLocalidad();
        }
    }
}
