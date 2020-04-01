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
    public partial class frmAyudaProductosMovimientoBodega : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        bool bRespuesta;
        DataTable dtConsulta;
        public string sCodigo { get; set; }
        public string sDescripcion { get; set; }
        public int iIdProducto { get; set; }

        public frmAyudaProductosMovimientoBodega()
        {
            InitializeComponent();
        }

        private void frmAyudaProductosMovimientoBodega_Load(object sender, EventArgs e)
        {
            cargarGrid(0);
        }

        //Función para cargar el grid
        private void cargarGrid(int iBandera)
        {
            try
            {
                dgv_Datos.Rows.Clear();
                if (iBandera == 0)
                {
                    sSql = @"Select Codigo, Descripcion, id_producto 
                        From cv401_vw_nombre_producto
                         Where Codigo_Padre like '1%'  ";
                }
                else
                {
                    sSql = @"Select Codigo, Descripcion, id_producto 
                        From cv401_vw_nombre_producto
                         Where Codigo_Padre like '1%' and Descripcion like '%"+TxtBusqueda.Text+"%' ";
                }                

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgv_Datos.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
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

        private void dgv_Datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            llenarVariables();
        }

        //Función para llenar las variables
        private void llenarVariables()
        {
            try
            {
                sCodigo = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
                sDescripcion = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
                iIdProducto = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[2].Value.ToString());
                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            llenarVariables();
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargarGrid(1);
        }




        //Fin de la clase
    }
}
