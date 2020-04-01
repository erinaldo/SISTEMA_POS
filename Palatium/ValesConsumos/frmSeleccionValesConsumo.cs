using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ValesConsumos
{
    public partial class frmSeleccionValesConsumo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public int iIdPersona;
        int iBandera;

        public frmSeleccionValesConsumo(int iBandera_P)
        {
            this.iBandera = iBandera_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";

                // iBandera = 1: Consumo Empleados
                // iBandera = 2: Vale Funcionarios

                if (iBandera == 1)
                {
                    sSql += "select id_persona, identificacion, persona" + Environment.NewLine;
                    sSql += "from pos_vw_listar_consumo_empleados" + Environment.NewLine;

                    if (txtBuscar.Text.Trim() != "")
                    {
                        sSql += "where identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                        sSql += "or apellidos like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                        sSql += "or nombres like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    }

                    sSql += "order by persona";
                }

                else if (iBandera == 2)
                {

                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(  dtConsulta.Rows[i]["persona"].ToString(),
                                        dtConsulta.Rows[i]["identificacion"].ToString(),
                                        dtConsulta.Rows[i]["id_persona"].ToString()
                                     );
                }

                dgvDatos.ClearSelection();

                lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";

                if (dtConsulta.Rows.Count == 0)
                    btnContinuar.Enabled = false;
                else
                    btnContinuar.Enabled = true;

                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void dgvAyudaConsumoEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells["identificacion"].Value.ToString();
                txtNombre.Text = dgvDatos.CurrentRow.Cells["nombres"].Value.ToString().Trim().ToUpper();
                iIdPersona = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_persona"].Value);

                btnContinuar.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmSeleccionValesConsumo_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSeleccionValesConsumo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            Program.iIdPersonaConsumoVale = iIdPersona;
            Program.iBanderaConsumoVale = 1;
            this.DialogResult = DialogResult.OK;
        }
    }
}
