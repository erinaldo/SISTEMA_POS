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
    public partial class frmExistenciasAunaFecha : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sCodigoBodega;
        string sSentenciaDbAyuda;
        string sFecha;

        double dbCantidadMinima;
        double dbSaldoActual;

        public frmExistenciasAunaFecha()
        {
            InitializeComponent();
        }

        private void frmExistenciasAunaFecha_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaCorte.Text = sFecha;
            cargarComboEmpresa();

            cmbOficina.SelectedIndexChanged -= new EventHandler(cmbOficina_SelectedIndexChanged);            
            cargarComboOficina();
            cmbOficina.SelectedIndexChanged += new EventHandler(cmbOficina_SelectedIndexChanged);

            cargarComboBodega();

            if (Convert.ToInt32(cmbOficina.SelectedValue) != 0)
            {
                llenarSentencias();
                llenarSentenciasDbAyuda();
            }            
        }

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

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el combo oficina 1 
        private void cargarComboOficina()
        {
            try
            {
                sSql = "";
                sSql += "select LOC.id_localidad, BO.descripcion + case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion, BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine; 
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine; 
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "order by BO.descripcion ";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbOficina.llenar(dtConsulta, sSql);
                    
                    if (cmbOficina.Items.Count > 0)
                    {
                        cmbOficina.SelectedIndex = 1;
                    }
                }

                else
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al cargar los datos de oficina";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = "";
                sSql += "Select BO.id_bodega, BO.descripcion, BO.codigo, BO.categoria"  + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where BO.id_bodega = LOC.id_bodega" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.id_localidad = " + Convert.ToInt32(cmbOficina.SelectedValue);
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbBodega.llenar(dtConsulta, sSql);

                if (cmbBodega.Items.Count > 0)
                {
                    cmbBodega.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOficina.SelectedIndex > 0)
                {
                    cargarComboBodega();
                    llenarSentencias();
                    llenarSentenciasDbAyuda();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //función para buscar el código de bodega
        private void buscarCodigoBodega()
        {
            try
            {
                sSql = "";
                sSql += "Select codigo from cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCodigoBodega = dtConsulta.Rows[0][0].ToString();
                    }
                }

                else
                {
                    ok.lblMensaje.Text = "Error al buscar el código.";
                    ok.ShowDialog();
                }
            }

            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
            }
        }
        
        //Función para llenar las sentencias en el dbAyuda
        private void llenarSentencias()
        {
            try
            {
                string sCodigo = "";
                string sNombre = "";

                DataRow[] dFila = cmbBodega.dt.Select("id_bodega = " + cmbBodega.SelectedValue);

                sSentenciaDbAyuda = "";
                sSentenciaDbAyuda += "select substring(PRO.Codigo,1,15) codigo," + Environment.NewLine;
                sSentenciaDbAyuda += "isnull((Select Nombre From cv401_nombre_productos" + Environment.NewLine;
                sSentenciaDbAyuda += "where Id_Producto = PRO.Id_Producto" + Environment.NewLine;
                sSentenciaDbAyuda += "and Cg_Tipo_Nombre = 5076" + Environment.NewLine;
                sSentenciaDbAyuda += "and estado ='A'),'(Sin Nombre)')" + Environment.NewLine;
                sSentenciaDbAyuda += "descripcion,PRO.Id_Producto" + Environment.NewLine;
                sSentenciaDbAyuda += "from cv401_productos PRO, cv401_productos PROPADRE" + Environment.NewLine;
                sSentenciaDbAyuda += "where PRO.id_producto_Padre = PROPADRE.id_Producto" + Environment.NewLine;
                sSentenciaDbAyuda += "and PROPADRE.Codigo Like '" + Convert.ToInt32(dFila[0][3].ToString()) + "%'" + Environment.NewLine;
                sSentenciaDbAyuda += "and PRO.estado = 'A'" + Environment.NewLine;
                sSentenciaDbAyuda += "and PRO.nivel in (1,2,3)" + Environment.NewLine;
                sSentenciaDbAyuda += "order By PRO.Codigo,descripcion";

                dbAyudaFamiliaArticulo.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
                dbAyudaFamiliaArticulo.txtIdentificacion.Text = sCodigo;
                dbAyudaFamiliaArticulo.txtDatos.Text = sNombre;
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
            try
            {
                DataRow[] dFila = cmbBodega.dt.Select("id_bodega = " + cmbBodega.SelectedValue);

                sSentenciaDbAyuda = "";
                sSentenciaDbAyuda += "select Codigo, Descripcion, Id_Producto Correlativo" + Environment.NewLine;
                sSentenciaDbAyuda += "from cv401_vw_nombre_producto" + Environment.NewLine;
                sSentenciaDbAyuda += "where Codigo_Padre Like '" + Convert.ToInt32(dFila[0][3].ToString()) + "%'" + Environment.NewLine;
                sSentenciaDbAyuda += "order By Codigo, Descripcion";

                dbAyudaArticuloInicial.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
                dbAyudaArticuloFinal.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaCorte.Text).ToString("dd/MM/yyyy");
                llenarGrid();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();

                string sFecha = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaCorte.Text = sFecha;
                dgvDetalleVenta.Rows.Clear();
            }

        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dgvDetalleVenta.Rows.Clear();
                string sFecha = Convert.ToDateTime(txtFechaCorte.Text).ToString("yyyy-MM-dd");


                sSql = "";
                sSql += "SELECT PRD_PADRE.codigo, PRD.Codigo, PRD.Descripcion, PRD.Codigo_Unidad_Consumo Unidad," + Environment.NewLine;
                sSql += "PRD.correlativo, 0 id_bod_ubicacion, '' Ubicacion, PRD_PADRE.stock_min, PRD_PADRE.stock_max" + Environment.NewLine;
                sSql += "From cv402_movimientos_bodega MB, cv402_cabecera_movimientos CM," + Environment.NewLine;
                sSql += "cv402_bodegas BO, tp_codigos EMP, cv401_vw_productos_02 PRD, cv401_productos PRD_PADRE" + Environment.NewLine;
                sSql += "where CM.idempresa = " + Convert.ToInt32(cmbEmpresa.SelectedValue) + Environment.NewLine;
                sSql += "and CM.ID_BODEGA = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                sSql += "and CM.FECHA <= '" + sFecha + "'" + Environment.NewLine;
                sSql += "and BO.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                sSql += "and MB.ID_PRODUCTO = PRD.Correlativo" + Environment.NewLine;
                sSql += "and PRD.id_producto_padre = PRD_PADRE.id_producto" + Environment.NewLine;
                sSql += "and CM.ID_BODEGA = BO.id_bodega" + Environment.NewLine;
                sSql += "and CM.CG_EMPRESA = EMP.CORRELATIVO" + Environment.NewLine;
                sSql += "and CM.ID_MOVIMIENTO_BODEGA = MB.ID_MOVIMIENTO_BODEGA" + Environment.NewLine;

                if (dbAyudaFamiliaArticulo.txtIdentificacion.Text.Trim() != "")
                {
                    //sSql += "and PRD_PADRE.Codigo like '" + dbAyudaFamiliaArticulo.sCodigo + "%'" + Environment.NewLine;
                    sSql += "and PRD_PADRE.id_producto = " + dbAyudaFamiliaArticulo.iId + Environment.NewLine;
                }

                if (txtNombreProducto.Text.Trim() != "")
                {
                    sSql += "and PRD.descripcion like '" + txtNombreProducto.Text + "%'" + Environment.NewLine;
                }

                if (dbAyudaArticuloInicial.txtIdentificacion.Text.Trim() != "")
                {
                    sSql += "and PRD.Codigo >= '" + dbAyudaArticuloInicial.txtIdentificacion.Text + "'" + Environment.NewLine;
                }

                if (dbAyudaArticuloFinal.txtIdentificacion.Text.Trim() != "")
                {
                    sSql += "and PRD.Codigo <= '" + dbAyudaArticuloFinal.txtIdentificacion.Text + "'" + Environment.NewLine;
                }

                sSql += "and EMP.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and MB.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and CM.ESTADO = 'A'" + Environment.NewLine;
                sSql += "group by PRD_PADRE.codigo, PRD.Codigo, PRD.Descripcion," + Environment.NewLine;
                sSql += "PRD.Codigo_Unidad_Consumo, PRD.Correlativo," + Environment.NewLine;
                sSql += "PRD_PADRE.stock_min, PRD_PADRE.stock_max" + Environment.NewLine;
                sSql += "order By PRD_PADRE.codigo,PRD.Codigo Asc";
                   

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sCodigoProductoPadre = dtConsulta.Rows[i].ItemArray[0].ToString();
                            string sCodigoProducto = dtConsulta.Rows[i].ItemArray[1].ToString();
                            string sDescripcion = dtConsulta.Rows[i].ItemArray[2].ToString();
                            string sUnidad = dtConsulta.Rows[i].ItemArray[3].ToString();
                            int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[4].ToString());

                            string sQuery;
                            sQuery = "";
                            sQuery += "select isnull(Sum(M.CANTIDAD),0) Saldo" + Environment.NewLine;
                            sQuery += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                            sQuery += "where C.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                            sQuery += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                            sQuery += "and C.Fecha <= Convert(DateTime,'" + sFecha + "', 120)" + Environment.NewLine;
                            sQuery += "and C.estado = 'A'" + Environment.NewLine;
                            sQuery += "and M.estado = 'A'" + Environment.NewLine;
                            sQuery += "and M.correlativo+0 = M.correlativo+0" + Environment.NewLine;
                            sQuery += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega" + Environment.NewLine;
                            sQuery += "and C.idempresa = " + Convert.ToInt32(cmbEmpresa.SelectedValue);

                            DataTable dtAyuda = new DataTable();
                            dtAyuda.Clear();
                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                            if (bRespuesta == true)
                            {
                                double dbSaldo = Convert.ToDouble(dtAyuda.Rows[0].ItemArray[0].ToString());

                                if (chkIncluirSaldo.Checked == true)
                                {
                                    dgvDetalleVenta.Rows.Add(sCodigoProducto, sDescripcion, "", sUnidad, dbSaldo.ToString("N2"));
                                }

                                else
                                {
                                    if (dbSaldo != 0)
                                    {
                                        dgvDetalleVenta.Rows.Add(sCodigoProducto, sDescripcion, "", sUnidad, dbSaldo.ToString("N2"));
                                    }
                                }
                            }

                            else
                            {
                                catchMensaje.lblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                return;
                            }
                        }
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No existe información.";
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //FUNCION PARA PINTAR LAS CELDAS
                for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                {
                    dbCantidadMinima = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells["colMinimo"].Value);
                    dbSaldoActual = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells["saldoActual"].Value);

                    if (dbSaldoActual <= dbCantidadMinima)
                    {
                        //PINTAR CELDA
                        dgvDetalleVenta.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dgvDetalleVenta.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                }

                goto fin;

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin: { Cursor = Cursors.Default; }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                dgvDetalleVenta.Rows.Clear();
                dbAyudaArticuloInicial.limpiar();
                dbAyudaArticuloFinal.limpiar();
                dbAyudaFamiliaArticulo.limpiar();
                txtNombreProducto.Clear();
                chkIncluirSaldo.Checked = false;
            }                
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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
                excel.Cells[2, 2] = "CONSULTA DE EXISTENCIAS A UNA FECHA DE CORTE:";
                excel.Cells[3, 1] = "LOCALIDAD:";
                excel.Cells[3, 2] = cmbOficina.Text;
                excel.Cells[3, 3] = "BODEGA:";
                excel.Cells[3, 4] = cmbBodega.Text;
                excel.Cells[5, 1] = "FECHA CORTE";
                excel.Cells[5, 2] = txtFechaCorte.Text;
                excel.Cells[6, 1] = "FECHA INICIO";
                excel.Cells[6, 2] = "";
                excel.Cells[7, 1] = "FECHA FIN";
                excel.Cells[7, 2] = "";

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    if (iIndiceColumna == 2)
                        excel.Cells[9, iIndiceColumna].ColumnWidth = 20;

                    excel.Cells[9, iIndiceColumna] = col.HeaderText;
                    excel.Cells[9, iIndiceColumna].Interior.Color = Color.Gray;
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

                excel.get_Range("A9", "E9").BorderAround();

                excel.Visible = true;
                Cursor = Cursors.Arrow;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

    }
}
