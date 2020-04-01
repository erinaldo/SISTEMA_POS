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
    public partial class ConsumoAlimentos : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        DataTable dtConsulta;

        string sSql;

        public ConsumoAlimentos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL DBCOMBO DE OPCIONES
        private void llenarCombo()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_mascara_item, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_mascara_item" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbOpcionesMascara.llenar(dtConsulta, sSql);

                if (cmbOpcionesMascara.Items.Count > 0)
                {
                    cmbOpcionesMascara.SelectedIndex = 1;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConsumoAlimentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkTodos.Checked == true)
            //{
            //    dgvPedido.SelectAll();
            //}

            //else if (chkTodos.Checked == false)
            //{
            //    dgvPedido.ClearSelection();
            //    dgvPedido.Rows[0].Selected = true;
            //    dgvPedido.CurrentCell = dgvPedido.Rows[0].Cells[1];
            //}
        }

        private void ConsumoAlimentos_Load(object sender, EventArgs e)
        {
            llenarCombo();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dgvPedido.Rows[i].Cells["colMascara"].Value = cmbOpcionesMascara.Text.ToUpper();
                    dgvPedido.Rows[i].Cells["colIdMascara"].Value = cmbOpcionesMascara.SelectedValue.ToString();
                    //ord.dgvPedido.Rows[i].Cells["producto"].Value = "CONSUMO ALIMENTOS";
                }
            }

            else
            {
                int f = Convert.ToInt32(dgvPedido.CurrentRow.Index);
                dgvPedido.Rows[f].Cells["colMascara"].Value = cmbOpcionesMascara.Text.ToUpper();
                dgvPedido.Rows[f].Cells["colIdMascara"].Value = cmbOpcionesMascara.SelectedValue.ToString();
                //ord.dgvPedido.Rows[f].Cells["producto"].Value = "CONSUMO ALIMENTOS";
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    //dgvPedido.Rows[i].Cells["producto"].Value = "CONSUMO ALIMENTOS";
                    dgvPedido.Rows[i].Cells["colMascara"].Value = "";
                    dgvPedido.Rows[i].Cells["colIdMascara"].Value = "";
                    //ord.dgvPedido.Rows[i].Cells["producto"].Value = "CONSUMO ALIMENTOS";
                }
            }

            else
            {
                int f = Convert.ToInt32(dgvPedido.CurrentRow.Index);
                dgvPedido.Rows[f].Cells["colMascara"].Value = "";
                dgvPedido.Rows[f].Cells["colIdMascara"].Value = "";
                //ord.dgvPedido.Rows[f].Cells["producto"].Value = "CONSUMO ALIMENTOS";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Orden ord = Owner as Orden;

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    if ((dgvPedido.Rows[i].Cells["colIdMascara"].Value != null) && (dgvPedido.Rows[i].Cells["colIdMascara"].Value != ""))
                    {
                        ord.dgvPedido.Rows[i].Cells["producto"].Value = dgvPedido.Rows[i].Cells["colMascara"].Value.ToString();
                        ord.dgvPedido.Rows[i].Cells["colIdMascara"].Value = dgvPedido.Rows[i].Cells["colIdMascara"].Value.ToString();
                    }
                }

                this.Close();
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
