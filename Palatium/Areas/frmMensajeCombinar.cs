using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Areas
{
    public partial class frmMensajeCombinar : Form
    {
        DataTable dtInformacion;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sNumeroMesa;
        public string sNumeroPedido;
        public int iIdPedido;
        
        public frmMensajeCombinar(DataTable dtInformacion_P)
        {
            this.dtInformacion = dtInformacion_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL DATATABLE
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                for (int i = 0; i < dtInformacion.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                    dtInformacion.Rows[i]["id_pedido"].ToString(),
                                    dtInformacion.Rows[i]["numero_mesa"].ToString(),
                                    dtInformacion.Rows[i]["numero_pedido"].ToString()                        
                        );
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmMensajeCombinar_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado el registro contenedor de las comandas a combinar.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea combinar las comandas en el pedido No. " + sNumeroPedido + " de la mesa " + sNumeroMesa  +"?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmMensajeCombinar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sNumeroMesa = dgvDatos.CurrentRow.Cells["numero_mesa"].Value.ToString().Trim().ToUpper();
            sNumeroPedido = dgvDatos.CurrentRow.Cells["numero_pedido"].Value.ToString().Trim();

            iIdPedido = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pedido"].Value);
        }
    }
}
