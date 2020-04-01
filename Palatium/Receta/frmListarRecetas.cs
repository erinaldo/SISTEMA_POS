using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmListarRecetas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtConsulta;
        DataTable dt;

        string sSql;

        bool bRespuesta;

        public frmListarRecetas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR DATOS EN EL DATATABLE DEL REPORTE
        private void construirReporte(int iIdReceta)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta + Environment.NewLine;
                sSql += "order by id_pos_detalle_receta";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        llenarDataTable();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "La receta no contiene ingredientes ingresados.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATATABLE
        private void llenarDataTable()
        {
            try
            {
                dsReporte ds = new dsReporte();
                dt = ds.Tables["dtReceta"];
                dt.Clear();

                DataRow dr;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["descripcion_receta"] = dtConsulta.Rows[i]["descripcion_receta"];
                    dr["descripcion_clasificacion_receta"] = dtConsulta.Rows[i]["descripcion_clasificacion_receta"];
                    dr["descripcion_tipo_receta"] = dtConsulta.Rows[i]["descripcion_tipo_receta"];
                    dr["descripcion_origen_receta"] = dtConsulta.Rows[i]["descripcion_origen_receta"];
                    dr["rendimiento_receta"] = dtConsulta.Rows[i]["rendimiento_receta"];
                    dr["num_porciones"] = dtConsulta.Rows[i]["num_porciones"];
                    dr["descripcion_temperatura_de_servicio"] = dtConsulta.Rows[i]["descripcion_temperatura_de_servicio"];
                    dr["precio_de_venta"] = dtConsulta.Rows[i]["precio_de_venta"];
                    dr["costo_unitario_receta"] = dtConsulta.Rows[i]["costo_unitario_receta"];
                    dr["porcentaje_costo"] = dtConsulta.Rows[i]["porcentaje_costo"];
                    dr["porcentaje_utilidad"] = dtConsulta.Rows[i]["porcentaje_utilidad"];
                    dr["utilidad_de_servicios"] = dtConsulta.Rows[i]["utilidad_de_servicios"];
                    dr["utilidad_de_ganancias"] = dtConsulta.Rows[i]["utilidad_de_ganancias"];
                    dr["costo_total"] = dtConsulta.Rows[i]["costo_total"];
                    dr["nombre_producto"] = dtConsulta.Rows[i]["nombre_producto"];
                    dr["cantidad_bruta"] = dtConsulta.Rows[i]["cantidad_bruta"];
                    dr["cantidad_neta"] = dtConsulta.Rows[i]["cantidad_neta"];
                    dr["descripcion_unidad"] = dtConsulta.Rows[i]["descripcion_unidad"];
                    dr["descripcion_porcion"] = dtConsulta.Rows[i]["descripcion_porcion"];
                    dr["costo_unitario"] = dtConsulta.Rows[i]["costo_unitario"];
                    dr["rendimiento"] = dtConsulta.Rows[i]["rendimiento"];
                    dr["importe"] = dtConsulta.Rows[i]["importe"];
                    dr["id_pos_receta"] = dtConsulta.Rows[i]["id_pos_receta"];
                    dr["id_pos_detalle_receta"] = dtConsulta.Rows[i]["id_pos_detalle_receta"];

                    dt.Rows.Add(dr);
                }

                Receta.frmReporteListadoReceta reporte = new frmReporteListadoReceta(dt);
                reporte.ShowDialog();

            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select R.id_pos_receta, R.codigo, R.descripcion, TR.descripcion descripcion_receta," + Environment.NewLine;
                sSql += "CR.descripcion clasificacion, ORI.descripcion origen_descripcion," + Environment.NewLine;
                sSql += "case when R.estado = 'A' then 'ACTIVO' else 'INACTIVO' end estado_receta" + Environment.NewLine;
                sSql += "from pos_receta R, pos_clasificacion_receta CR," + Environment.NewLine;
                sSql += "pos_origen_receta ORI, pos_tipo_receta TR" + Environment.NewLine;
                sSql += "where CR.id_pos_clasificacion_receta = R.id_pos_clasificacion_receta" + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_receta = R.id_pos_origen_receta" + Environment.NewLine;
                sSql += "and TR.id_pos_tipo_receta = R.id_pos_tipo_receta" + Environment.NewLine;
                sSql += "and R.estado = 'A'" + Environment.NewLine;
                sSql += "and CR.estado = 'A'" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "and TR.estado = 'A'" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and R.codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or R.descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by R.id_pos_receta";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        dgvDatos.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
                                            dtConsulta.Rows[i].ItemArray[1].ToString(),
                                            dtConsulta.Rows[i].ItemArray[2].ToString(),
                                            dtConsulta.Rows[i].ItemArray[3].ToString(),
                                            dtConsulta.Rows[i].ItemArray[4].ToString(),
                                            dtConsulta.Rows[i].ItemArray[5].ToString(),
                                            dtConsulta.Rows[i].ItemArray[6].ToString()
                                              );
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmListarRecetas_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void btnBuscarjornada_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == senderGrid.Columns[7].Index && e.RowIndex >= 0)
            {
                construirReporte(Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value));
            }
        }
    }
}
