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
    public partial class frmAyudaBodegas_Proveedores_ : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        public string sCodigo { get; set; }
        public string sNombre { get; set; }
        public string sCorrelativo { get; set; }

        public frmAyudaBodegas_Proveedores_()
        {
            InitializeComponent();
        }

        private void frmAyudaBodegas_Proveedores__Load(object sender, EventArgs e)
        {
            llenarGrid(1);
        }

        //Función para llenar el grid
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgv_Datos.Rows.Clear();

                if (iBandera == 1)
                {
                    sSql = "Select Codigo, Nombre, Correlativo " +
                        "From cv404_vw_clientes_Bodega " +
                        "Where Empresa = "+Program.iCgEmpresa+" And Ano_Fiscal = 2018 " +
                        "order by Codigo ";
                }
                else
                {
                    sSql = "Select Codigo, Nombre, Correlativo " +
                            "From cv404_vw_clientes_Bodega " +
                            "Where Empresa = 69 And Ano_Fiscal = 2018 and  Nombre like '%" + TxtBusqueda.Text + "%' " +
                            "order by Codigo  ";
                }
                

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgv_Datos.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
                                                dtConsulta.Rows[i].ItemArray[1].ToString(),
                                                dtConsulta.Rows[i].ItemArray[2].ToString());
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

        //Función para llenar varibales
        private void llenarVariables()
        {
            try
            {
                sCodigo = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
                sNombre = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
                sCorrelativo = dgv_Datos.CurrentRow.Cells[2].Value.ToString();

                this.DialogResult = DialogResult.OK;
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
            llenarVariables();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            llenarGrid(0);
        }


        //Fin de la clase
    }
}
