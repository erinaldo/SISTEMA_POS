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
    public partial class frmExtraerInformacion : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        string sCampo;
        string sOrdenamiento;
        string sSqlAyuda;
        bool bRespuesta;
        DataTable dtConsulta;

        public int iIdCodigo;
        public string sCodigo;
        public string sDescripcion;

        public frmExtraerInformacion(string sSql, string sCampo, string sOrdenamiento)
        {
            this.sSql = sSql;
            this.sCampo = sCampo;
            this.sSqlAyuda = sSql;
            this.sOrdenamiento = sOrdenamiento;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO
        
        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int op)
        {
            try
            {
                if (op == 1)
                {
                    sSql = sSqlAyuda + " where '" + sCampo + "' like '%' + '" + TxtBusqueda.Text.Trim() + "' + '%'";
                }

                else if (op == 0)
                {
                    sSql= sSqlAyuda;
                }
                
                sSql = sSql + " order by " + sOrdenamiento;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgv_Datos.DataSource = dtConsulta;
                        dgv_Datos.Columns[0].Width = 100;
                        dgv_Datos.Columns[1].Width = 300;
                        dgv_Datos.Columns[2].Visible = false;
                        goto fin;
                    }
                }

                else
                {
                    goto reversa;
                }
            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }

        fin: { }
        }

        #endregion

        private void frmExtraerInformacion_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_Datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Datos.Columns[2].Visible = true;
            iIdCodigo = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[0].Value.ToString());
            sDescripcion = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sCodigo = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;  
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dgv_Datos.Columns[2].Visible = true;
            iIdCodigo = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[0].Value.ToString());
            sDescripcion = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sCodigo = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;  
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
    }
}
