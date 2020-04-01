using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmAyudaProveedores : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        public string sCodigo { get; set; }
        public string sNombreProveedor { get; set; }
        public string sCorrelativo { get; set; }

        public frmAyudaProveedores()
        {
            InitializeComponent();
        }

        private void frmAyudaProveedores_Load(object sender, EventArgs e)
        {
            cargarGrid(0);
        }

        // Función para cargar el grid
        private void cargarGrid(int iBandera)
        {
            try
            {
                dgvProveedor.Rows.Clear();
                string sAño = DateTime.Now.ToString("yyyy");

                sSql = "";
                sSql += "Select Codigo, Nombre, Correlativo" + Environment.NewLine;
                sSql += "From cv405_vw_proveedores_bodega_pt" + Environment.NewLine;
                sSql += "Where Empresa = " + Program.iCgEmpresa; //+" And Ano_Fiscal = " + sAño;

                if (iBandera == 1)
                {
                    sSql += "and Nombre like '%" + TxtBusqueda.Text + "%'";
                }
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for(int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvProveedor.Rows.Add( dtConsulta.Rows[i].ItemArray[0].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[1].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[2].ToString()
                                                    ); 
                        }
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            cargarVariables();
        }

        //función para cargar las variables 
        private void cargarVariables()
        {
            try
            {
                sCodigo = dgvProveedor.CurrentRow.Cells[0].Value.ToString();
                sNombreProveedor = dgvProveedor.CurrentRow.Cells[1].Value.ToString();
                sCorrelativo = dgvProveedor.CurrentRow.Cells[2].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void dgvProveedor_DoubleClick(object sender, EventArgs e)
        {
            cargarVariables();
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargarGrid(1);
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        //Fin de la clase
    }
}
