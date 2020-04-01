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
    public partial class frmAyudaBodega : Form
    {

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        int iIdBodega;
        public string sNumeroMovimiento { get; set;}
        public string sReferenciaExterna { get; set; }
        public string sObservacion { get; set; }
        public string sIdMovimientoBodega { get; set; }
        public string sFecha { get; set; }
        public string sIdMovimientoTesoreria { get; set; }
        public string sEstado { get; set; }

        public frmAyudaBodega(int iIdBodega)
        {
            this.iIdBodega = iIdBodega;
            InitializeComponent();
        }

        private void frmAyudaBodega_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        //Función para llenar el grid 
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgv_Datos.Rows.Clear();
                if (iBandera == 0)
                {
                    sSql = @"Select Numero_Movimiento, Referencia_Externa, Observacion,
                    Id_Movimiento_Bodega, fecha,id_c_movimiento Id_Mov_Tesoreria ,Estado 
                    From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega +
                    "And Cg_Tipo_Movimiento = 8000 And ID_ORDEN_COMPRA IS NULL  " +
                    "And Estado = 'A' order by fecha desc,numero_movimiento desc";
                }

                else
                {
                    sSql = @"Select Numero_Movimiento, Referencia_Externa, Observacion,
                    Id_Movimiento_Bodega, fecha,id_c_movimiento Id_Mov_Tesoreria ,Estado 
                    From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega +
                    "And Cg_Tipo_Movimiento = 8000 And ID_ORDEN_COMPRA IS NULL  " +
                    "And Estado = 'A' and Numero_Movimiento like '%" + TxtBusqueda.Text + "%' order by fecha desc,numero_movimiento desc ";
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
                                                dtConsulta.Rows[i].ItemArray[2].ToString(),
                                                dtConsulta.Rows[i].ItemArray[3].ToString(),
                                                dtConsulta.Rows[i].ItemArray[4].ToString(),
                                                dtConsulta.Rows[i].ItemArray[5].ToString(),
                                                dtConsulta.Rows[i].ItemArray[6].ToString()
                                                
                                                );
                        }
                           
                        
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message; ;
                catchMensaje.ShowDialog();
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
             llenarGrid(1);
        }

        //Función para llenar las variables
        private void llenarVariables()
        {
            sNumeroMovimiento = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
            sReferenciaExterna = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sObservacion = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            sIdMovimientoBodega = dgv_Datos.CurrentRow.Cells[3].Value.ToString();
            sFecha = dgv_Datos.CurrentRow.Cells[4].Value.ToString();
            sIdMovimientoTesoreria = dgv_Datos.CurrentRow.Cells[5].Value.ToString();
            sEstado = dgv_Datos.CurrentRow.Cells[6].Value.ToString();

            this.DialogResult = DialogResult.OK;
        }

        private void dgv_Datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            llenarVariables();
        }

        
        //Fin de la clase
    }
}
