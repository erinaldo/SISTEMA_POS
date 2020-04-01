using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Palatium.Bodega
{
    public partial class frmExistenciasRangodeFechas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sSql;
        bool bRespuesta;
        DataTable dtConsulta;
        
        string sCodigoBodega;
        string sSentenciaDbAyuda;
        string sFecha;

        public frmExistenciasRangodeFechas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para cargar el combo empresa
        private void cargarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "Select idempresa, razonSocial, Codigo" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo oficina 1 
        private void cargarComboOficina()
        {
            try
            {
                sSql = "";
                sSql += "select  LOC.id_localidad,  BO.descripcion +" + Environment.NewLine;
                sSql += "case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion," + Environment.NewLine;
                sSql += "BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;  
                sSql += "where LOC.id_bodega =  BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.estado =  'A'" + Environment.NewLine; 
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "order by BO.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                cmbOficina.llenar(dtConsulta, sSql);
                if (cmbOficina.Items.Count > 4)
                {
                    cmbOficina.SelectedIndex = 4;
                }
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = "";
                sSql = "select id_bodega, descripcion" + Environment.NewLine; 
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                cmbBodega.llenar(dtConsulta, sSql);
                if (cmbBodega.Items.Count > 4)
                {
                    cmbBodega.SelectedItem = 4;
                }
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //función para buscar el código de bodega
        private void buscarCodigoBodega()
        {
            try
            {
                sSql = "";
                sSql += "select codigo From cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCodigoBodega = dtConsulta.Rows[0][0].ToString();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentenciasDbAyuda()
        {
            sSql = "";
            sSql += "select codigo, descripcion, Id_Producto Correlativo" + Environment.NewLine;
            sSql += "from cv401_vw_nombre_producto" + Environment.NewLine;

            if (sCodigoBodega == "MP" || sCodigoBodega == "M2" || sCodigoBodega == "M3")
            {                
                sSql += "where Codigo_Padre Like '1%'" + Environment.NewLine;                
            }

            else
            {
                sSql += "where Codigo_Padre Like '2%'" + Environment.NewLine;
            }

            sSql += "Order By Codigo , Descripcion ";

            dbAyudaArticulo.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvDetalleVenta.Rows.Clear();
                Cursor = Cursors.WaitCursor;
                ejecutar();
                Cursor = Cursors.Arrow;
            }
            
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                Cursor = Cursors.Arrow;
            }
        }

        //FUNCION PARA AGREGAR LOS REGISTROS AL GRID
        private void llenarRegistros(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double dbSaldoAnterior = Convert.ToDouble(dt.Rows[i][5].ToString());
                    double dbIngresos = Convert.ToDouble(dt.Rows[i][6].ToString());
                    double dbEgresos = Convert.ToDouble(dt.Rows[i][7].ToString());
                    double dbSaldoActual = Convert.ToDouble(dt.Rows[i][8].ToString());

                    dgvDetalleVenta.Rows.Add(dt.Rows[i][1].ToString(),
                                                dt.Rows[i][2].ToString(),
                                                dt.Rows[i][3].ToString(),
                                                dbSaldoAnterior.ToString("N2"),
                                                dbIngresos.ToString("N2"),
                                                dbEgresos.ToString("N2"),
                                                dbSaldoActual.ToString("N2")
                                            );
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            try
            {
                Cursor = Cursors.AppStarting;
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Application.Workbooks.Add(true);

                int iIndiceColumna = 0;
                excel.Columns.ColumnWidth = 13;
                excel.Cells[1, 1] = "EMPRESA: ";
                excel.Cells[1, 2] = cmbEmpresa.Text;
                excel.Cells[2, 1] = "REPORTE:";
                excel.Cells[2, 2] = "CONSULTA DE EXISTENCIAS EN UN RANGO DE FECHAS:";
                excel.Cells[3, 1] = "LOCALIDAD:";
                excel.Cells[3, 2] = cmbOficina.Text;
                excel.Cells[3, 3] = "BODEGA:";
                excel.Cells[3, 4] = cmbBodega.Text;
                excel.Cells[5, 1] = "FECHA INICIO";
                excel.Cells[5, 2] = txtFechaDesde.Text;
                excel.Cells[6, 1] = "FECHA FIN";
                excel.Cells[6, 2] = txtFechaHasta.Text;
                excel.Cells[7, 1] = "ARTÍCULO:";

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    if (iIndiceColumna == 2)
                        excel.Cells[9, iIndiceColumna].ColumnWidth = 25;

                    excel.Cells[9, iIndiceColumna] = col.HeaderText;
                    excel.Cells[9, iIndiceColumna].Interior.Color = Color.Yellow;
                    excel.Cells[9, iIndiceColumna].BorderAround();
                }

                int iIndiceFila = 9;

                foreach (DataGridViewRow row in dgvCierre.Rows)
                {
                    iIndiceFila++;

                    iIndiceColumna = 0;

                    foreach (DataGridViewColumn col in dgvCierre.Columns)
                    {
                        iIndiceColumna++;
                        excel.Cells[iIndiceFila + 1, iIndiceColumna] = row.Cells[col.Name].Value;
                    }
                }

                excel.get_Range("A9", "G9").BorderAround();

                excel.Visible = true;
                Cursor = Cursors.Arrow;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                Cursor = Cursors.Arrow;
            }
        }

        //Función para ejecutar
        private void ejecutar()
        {
            try
            {
                string sFechaInicio = Convert.ToDateTime(txtFechaDesde.Text).ToString("yyyy-MM-dd");
                string sFechaFin = Convert.ToDateTime(txtFechaHasta.Text).ToString("yyyy-MM-dd");
                string ruta = ConexionBD.ConexionBD.path;

                DataTable dt = new DataTable();
                dt.Clear();

                SqlConnection cnn = new SqlConnection(ruta);
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter("Sp_Cv402_Inventario_02", cnn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@P_Ln_Empresa", Program.iCgEmpresa);
                da.SelectCommand.Parameters.Add("@P_Ln_Moneda", 0);
                da.SelectCommand.Parameters.Add("@P_Dt_Fecha_Inicio", "" + sFechaInicio + "");
                da.SelectCommand.Parameters.Add("@P_Dt_Fecha_Fin", "" + sFechaFin + "");

                if (dbAyudaArticulo.txtIdentificacion.Text.Trim() == "")
                {
                    da.SelectCommand.Parameters.Add("@P_Ln_Articulo", 0);
                }

                else
                {
                    da.SelectCommand.Parameters.Add("@P_Ln_Articulo", dbAyudaArticulo.iId);
                }

                da.SelectCommand.Parameters.Add("@P_Ln_Bodega", cmbBodega.SelectedValue);
                da.SelectCommand.Parameters.Add("@P_In_Valorizar", 0);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    llenarRegistros(dt);
                }

                else
                {
                    ok.lblMensaje.Text = "No existe información.";
                    ok.ShowDialog();
                }

                cnn.Close();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmExistenciasRangodeFechas_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaDesde.Text = sFecha;
            txtFechaHasta.Text = sFecha;
            cargarComboEmpresa();
            cargarComboBodega();
            cargarComboOficina();
        }

        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOficina.SelectedIndex > 0)
                {
                    sSql = "";
                    sSql += "select id_bodega from tp_vw_localidades" + Environment.NewLine;
                    sSql += "where id_localidad = " + Convert.ToInt32(cmbOficina.SelectedValue);

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        cmbBodega.SelectedValue = dtConsulta.Rows[0][0];
                        buscarCodigoBodega();
                        llenarSentenciasDbAyuda();
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaHasta.Text).ToString("dd/MM/yyyy");
                Convert.ToDateTime(txtFechaDesde.Text).ToString("dd/MM/yyyy");
                llenarGrid();
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ingrese una fecha con el formato 'dd/mm/aaaa'", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
                //txtFechaDesde.Text = sFecha;
                //txtFechaHasta.Text = sFecha;
                //dgvDetalleVenta.Rows.Clear();
            }
        }
        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                dgvDetalleVenta.Rows.Clear();
            }
        }

        private void btnEnviarAExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalleVenta.Rows.Count > 0)
                {
                    exportarAExcel(dgvDetalleVenta);
                }

                else
                {
                    ok.lblMensaje.Text = "No hay datos para mostrar.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }        
    }
}
