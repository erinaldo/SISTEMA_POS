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
    public partial class frmAyudaTransferenciaBodega : Form
    {

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        int iCgTipoMovimiento;
        int iIdBodega;
        public string sNumeroMovimiento { get; set; }
        public string sReferencia { get; set; }
        public string sObservacion { get; set; }
        public int iIdMovimientoBodega { get; set; }
        public string sFecha { get; set; }

        public frmAyudaTransferenciaBodega(int iIdBodega)
        {
            this.iIdBodega = iIdBodega;
            InitializeComponent();
        }

        private void frmAyudaTransferenciaBodega_Load(object sender, EventArgs e)
        {
            obtenerCgTipoMovimiento();
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
                    sSql = "Select Numero_Movimiento, Referencia_Externa, Observacion, Id_Movimiento_Bodega, fecha  " +
                        "From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega + " And  " +
                        "Cg_Tipo_Movimiento = " + iCgTipoMovimiento + " And Estado = 'A' order by fecha desc,numero_movimiento desc  ";
                }
                else
                {
                    sSql = "Select Numero_Movimiento, Referencia_Externa, Observacion, Id_Movimiento_Bodega, fecha  " +
                            "From cv402_cabecera_movimientos Where Cg_Empresa = " + Program.iCgEmpresa + " And ID_Bodega = " + iIdBodega + " And  " +
                            "Cg_Tipo_Movimiento = " + iCgTipoMovimiento + " And Estado = 'A' and Numero_movimiento like '%"+TxtBusqueda.Text
                             +"%' order by fecha desc,numero_movimiento desc  ";
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
                                                   dtConsulta.Rows[i].ItemArray[4].ToString()
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

        //Función para obtener el correlativo
        private void obtenerCgTipoMovimiento()
        {
            try
            {
                sSql = "select correlativo from tp_codigos where codigo = 'TEM' and tabla = 'SYS$00648'";
                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCgTipoMovimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
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
                sNumeroMovimiento = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
                sReferencia = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
                sObservacion = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
                iIdMovimientoBodega = Convert.ToInt32(dgv_Datos.CurrentRow.Cells[3].Value.ToString());
                sFecha = dgv_Datos.CurrentRow.Cells[4].Value.ToString();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }            
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            llenarGrid(1);
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            llenarVariables();
        }
    }
}
