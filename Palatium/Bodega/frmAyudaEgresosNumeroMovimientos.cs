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
    public partial class frmAyudaEgresosNumeroMovimientos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        int iIdBodega;
        int cgTipoMovimiento = 7999;
        public string sNumeroMovimiento{get; set;}
        public string sReferenciaExterna { get; set; }
        public string sObservacion { get; set; }
        public string sIdMovimientoBodega { get; set; }
        public string sFecha { get; set; }
        public string sidMovimientoTesoreria { get; set; }

        public frmAyudaEgresosNumeroMovimientos(int iIdBodega)
        {
            this.iIdBodega = iIdBodega;
            InitializeComponent();
            obtenerCgTipoMovimiento();
        }

        private void frmAyudaEgresosNumeroMovimientos_Load(object sender, EventArgs e)
        {
            llenarGrid(1);
        }

        //Función para obtener el correlativo
        private void obtenerCgTipoMovimiento()
        {
            try
            {
                sSql = "select correlativo from tp_codigos where codigo = 'EMP' and tabla = 'SYS$00648'";
                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    cgTipoMovimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
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

        //Función para llenar el grid
        private void llenarGrid(int iBandera)
        {
            try
            {
                if (iBandera == 1)
                {
                    sSql = "Select Numero_Movimiento, Referencia_Externa, Observacion, Id_Movimiento_Bodega, " +
                        "fecha,id_c_movimiento Id_Mov_Tesoreria  " +
                        "From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega + " " +
                        "And Cg_Tipo_Movimiento = " + cgTipoMovimiento + " " +
                        "And Estado = 'A' order by fecha desc,numero_movimiento desc ";
                }
                else
                {
                    sSql = "Select Numero_Movimiento, Referencia_Externa, Observacion, Id_Movimiento_Bodega, " +
                        "fecha,id_c_movimiento Id_Mov_Tesoreria  " +
                        "From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega + " " +
                        "And Cg_Tipo_Movimiento = " + cgTipoMovimiento + " " +
                        "And Estado = 'A' order by fecha desc,numero_movimiento desc "+
                        "And Numero_Movimiento like '%"+TxtBusqueda.Text+"%' ";
                }
                    
                

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvMovimientos.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[1].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[2].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[3].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[4].ToString(),
                                                    dtConsulta.Rows[i].ItemArray[5].ToString()
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

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        //Función para llenar variables
        private void llenarVariables()
        {
            sNumeroMovimiento = dgvMovimientos.CurrentRow.Cells[0].Value.ToString();
            sReferenciaExterna = dgvMovimientos.CurrentRow.Cells[1].Value.ToString();
            sObservacion = dgvMovimientos.CurrentRow.Cells[2].Value.ToString();
            sIdMovimientoBodega = dgvMovimientos.CurrentRow.Cells[3].Value.ToString();
            sFecha = dgvMovimientos.CurrentRow.Cells[4].Value.ToString();
            if (dgvMovimientos.CurrentRow.Cells[5].Value != null)
                sidMovimientoTesoreria = dgvMovimientos.CurrentRow.Cells[5].Value.ToString();
            else
                sidMovimientoTesoreria = " ";

            
            
            this.DialogResult = DialogResult.OK;

        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            llenarVariables();
        }

        private void dgvMovimientos_DoubleClick(object sender, EventArgs e)
        {
            llenarVariables();
        }

    }
}
