using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Tarjeta_Almuerzo
{
    public partial class frmListarTarjetasVigentes : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public int iNumeroTarjeta_P;

        public frmListarTarjetasVigentes()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO PARA LISTAR LAS TARJETAS

        //FUNCION LLENAR GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int iOp_P = 0;

                sSql = "";
                sSql += "select * from pos_vw_tar_lista_tarjetas_almuerzo_emitidas" + Environment.NewLine;
                sSql += "where estado_tarjeta = 'Vigente'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iOp_P = 1;
                    sSql += "and (identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or numero_tarjeta like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or cliente like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                        dtConsulta.Rows[i]["identificacion"].ToString(),
                                        dtConsulta.Rows[i]["cliente"].ToString(),
                                        Convert.ToDateTime(dtConsulta.Rows[i]["fecha_emision"].ToString()).ToString("dd-MM-yyyy"),
                                        dtConsulta.Rows[i]["numero_tarjeta"].ToString(),
                                        dtConsulta.Rows[i]["estado_tarjeta"].ToString(),
                                        dtConsulta.Rows[i]["disponibles"].ToString()
                                        );
                }

                dgvDatos.ClearSelection();
                txtBuscar.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListarTarjetasVigentes_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iNumeroTarjeta_P = Convert.ToInt32(dgvDatos.CurrentRow.Cells["numero_tarjeta"].Value);
                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmListarTarjetasVigentes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
