using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmLlenarGridInformacion : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        string sSqlAuxiliar;
        string sCampoBusqueda;
        string sCampoOrdenar;
        DataTable dtConsulta;
        bool bRespuesta = false;

        public int iIdCodigo;
        public string sCodigo;
        public string sDescripcion;

        public frmLlenarGridInformacion(string sQuery, string sCampoBusqueda, string sCampoOrdenar)
        {
            this.sSqlAuxiliar = sQuery;
            this.sCampoBusqueda = sCampoBusqueda;
            this.sCampoOrdenar = sCampoOrdenar;
            InitializeComponent();
        }

        #region FUNCIONES PARA EL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int op)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (op == 0)
                {
                    sSql = sSqlAuxiliar +  " order by " + sCampoOrdenar;
                }

                else
                {                    
                    sSql = sSqlAuxiliar + " and " + sCampoBusqueda + " LIKE '%' + '" + TxtBusqueda.Text.Trim() + "' + '%' order by" + sCampoOrdenar;                    
                }

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgv_Datos.DataSource = dtConsulta;
                    dgv_Datos.Columns[1].Width = 100;
                    dgv_Datos.Columns[2].Width = 300;

                    //NICOLE

                    if (dgv_Datos.Rows.Count > 0)
                    {
                        dgv_Datos.Rows[0].Selected = true;
                        dgv_Datos.CurrentCell = dgv_Datos.Rows[0].Cells[1];
                    }

                    dgv_Datos.Columns[0].Visible = false;
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta. Comuníquese con el administrador";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        #endregion

        private void frmLlenarGridInformacion_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (TxtBusqueda.Text == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dgv_Datos.Columns[0].Visible = true;
            iIdCodigo = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[0].Value.ToString());
            sCodigo = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sDescripcion = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;            
        }

        private void dgv_Datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Datos.Columns[0].Visible = true;
            iIdCodigo = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[0].Value.ToString());
            sCodigo = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sDescripcion = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;  
        }

        private void frmLlenarGridInformacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
