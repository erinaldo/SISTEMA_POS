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
    public partial class frmReporteMovimientoEmpresa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sFecha;
        string sSentenciaDbAyuda;

        bool bRespuesta;

        DataTable dtConsulta;

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sCodigoProducto, sCodigoProductoFina, sDescripcionProducto, sDescripcionProductoFinal;

        int iIdProducto, iIdProductoFinal;

        public frmReporteMovimientoEmpresa()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentenciasDbAyuda()
        {
            try
            {
                sSentenciaDbAyuda = "";
                sSentenciaDbAyuda += "Select Codigo, Descripcion, id_producto" + Environment.NewLine;
                sSentenciaDbAyuda += "from cv401_vw_nombre_producto" + Environment.NewLine;
                sSentenciaDbAyuda += "order By Codigo, Descripcion";

                dbAyudaArticuloInicial.Ver(sSentenciaDbAyuda, "Codigo", 2, 0, 1);
                dbAyudaArticuloFinal.Ver(sSentenciaDbAyuda, "Codigo", 2, 0, 1);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE OFICINA LOCAL
        private void llenarComboOficina()
        {
            try
            {
                sSql = "";
                sSql = "Select LOC.id_localidad, BO.descripcion, BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.tipo = '1'" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado = 'A'";

                dtConsulta = new DataTable();
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

        //Función para llenar el combo de tipo movimiento
        private void llenarComboTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00648'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbTipoMovimiento.llenar(dtConsulta, sSql);
                }

                else
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = "Ocurrió un problema al cargar el combo de oficina";
                catchMensaje.ShowDialog();
            }
        }

        //Función para verificar los campos
        private bool comprobarCampos()
        {
            try
            {
                int iBandera = 0;

                if (dbAyudaArticuloInicial.txtIdentificacion.Text.Trim() == "")
                {
                    iBandera = 1;
                    ok.lblMensaje.Text = "Advertencia: Debe selecionar el código del producto inicial.";
                    ok.ShowDialog();
                }

                if (iBandera == 1)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
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
                excel.Columns.ColumnWidth = 10;
                excel.Cells[1, 1] = "EMPRESA: ";
                excel.Cells[1, 2] = "COSA NOSTRA";
                excel.Cells[2, 1] = "REPORTE:";
                excel.Cells[2, 2] = "MOVIMIENTO POR EMPRESA:";
                excel.Cells[3, 1] = "PROD. INICIAL:";
                excel.Cells[3, 2] = dbAyudaArticuloInicial.txtIdentificacion.Text.Trim() + "  " + dbAyudaArticuloInicial.txtDatos.Text.Trim();
                excel.Cells[4, 1] = "PROD. FINAL:";
                excel.Cells[4, 2] = dbAyudaArticuloFinal.txtIdentificacion.Text.Trim() + "  " + dbAyudaArticuloFinal.txtDatos.Text.Trim();
                excel.Cells[5, 1] = "FECHA INICIO";
                excel.Cells[5, 2] = txtFechaDesde.Text;
                excel.Cells[6, 1] = "FECHA FIN";
                excel.Cells[6, 2] = txtFechaHasta.Text;

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    if (iIndiceColumna == 2 || iIndiceColumna == 3 || iIndiceColumna == 4 || iIndiceColumna == 8 || iIndiceColumna == 9 || iIndiceColumna == 12)
                        excel.Cells[8, iIndiceColumna].ColumnWidth = 15;

                    excel.Cells[8, iIndiceColumna] = col.HeaderText;
                    excel.Cells[8, iIndiceColumna].Interior.Color = Color.Yellow;
                    excel.Cells[8, iIndiceColumna].BorderAround();
                }


                int iIndiceFila = 8;

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

                excel.get_Range("A8", "L8").BorderAround();

                excel.Visible = true;
                Cursor = Cursors.Arrow;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbTipoMovimiento.SelectedValue = 0;
            dbAyudaArticuloInicial.limpiar();
            dbAyudaArticuloFinal.limpiar();
            dgvDetalleVenta.Rows.Clear();
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string sFechaDesde = Convert.ToDateTime(txtFechaDesde.Text).ToString("yyyy-MM-dd");
                string sFechaHasta = Convert.ToDateTime(txtFechaHasta.Text).ToString("yyyy-MM-dd");
                double dbTotalIngresos = 0;
                double dbtotalEgresos = 0;
                double dbTotalValorIngresos = 0;
                double dbTotalValorEgresos = 0;
                double dbSaldo;
                double dbPrecioEgresos = 0;
                double dbPrecioEgresos1 = 0;

                dgvDetalleVenta.Rows.Clear();

                sSql = "";
                sSql += "select id_Producto, codigo, descripcion" + Environment.NewLine;
                sSql += "from cv401_vw_nombre_producto" + Environment.NewLine;
                sSql += "where codigo >=  '" + dbAyudaArticuloInicial.txtIdentificacion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and codigo <=  '" + dbAyudaArticuloFinal.txtIdentificacion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "order by codigo ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                //RESPUESTA A LA CONSULTA A LA BASE DE DATOS
                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //VERIFICAR SI EXISTEN REGISTROS
                if (dtConsulta.Rows.Count == 0)
                {
                    ok.lblMensaje.Text = "No hay registros en la consulta.";
                    ok.ShowDialog();
                    return;
                }

                //CONSULTAR DATOS CON INFORMACION DEL DTCONSULTA
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    //Almaceno los datos de la base de datos en variables 
                    int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                    string sCodigoProducto = dtConsulta.Rows[i].ItemArray[1].ToString();
                    string sDescripcionProducto = dtConsulta.Rows[i].ItemArray[2].ToString();


                    //Agrego la primera línea al datagrid
                    sSql = "";
                    sSql += "select isnull(Sum(M.Cantidad),0) saldo, isnull(Sum(M.Cantidad * isnull(M.valor_unitario, 0)), 0) total" + Environment.NewLine;
                    sSql += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                    sSql += "where C.CG_EMPRESA = " + Convert.ToInt32(cmbOficina.SelectedValue) + Environment.NewLine;
                    sSql += "and C.Fecha < Convert(DateTime,'" + sFechaDesde + "', 120)" + Environment.NewLine;
                    sSql += "and C.externo = 1" + Environment.NewLine;
                    sSql += "and M.Id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and M.estado = 'A'" + Environment.NewLine;
                    sSql += "and C.Estado = 'A'";
                    dbTotalIngresos = 0;

                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                    //RESPUESTA A LA CONSULTA A LA BASE DE DATOS
                    if (bRespuesta == false)
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    //VERIFICAR SI EXISTEN REGISTROS
                    if (dtAyuda.Rows.Count == 0)
                    {
                        ok.lblMensaje.Text = "No hay registros en la consulta.";
                        ok.ShowDialog();
                        return;
                    }

                    //EMPIEZA A CREAR EL DATAGRID
                    double dbIngresos = Convert.ToDouble(dtAyuda.Rows[0].ItemArray[0].ToString());
                    double dbTotal = Convert.ToDouble(dtAyuda.Rows[0].ItemArray[1].ToString());
                    double dbPrecioIngreso;

                    if (dbIngresos != 0)
                    {
                        dbPrecioIngreso = (Convert.ToDouble(dbTotal.ToString("N2")) / Convert.ToDouble(dbIngresos.ToString("N2")));
                    }

                    else
                    {
                        dbPrecioIngreso = 0;
                    }

                    dgvDetalleVenta.Rows.Add("",
                                            sCodigoProducto,
                                            "**Saldo Anterior**",
                                            dbTotal.ToString("N4"),
                                            "",
                                            "",
                                            "",
                                            dbIngresos.ToString("N2"),
                                            "",
                                            dbTotal.ToString("N2"),
                                            "",
                                            "");

                    dbPrecioEgresos = dbIngresos;
                    dbSaldo = dbIngresos;

                    sSql = "";
                    sSql += "select C.Fecha, C.Numero_Movimiento, C.Id_Bodega, C.Referencia_Externa," + Environment.NewLine;
                    sSql += "P.Codigo Codigo_Producto, P.Descripcion Descripcion_Producto," + Environment.NewLine;
                    sSql += "isnull(M.Cantidad, 0), isnull(M.Valor_Unitario, 0), isnull(M.Precio_Promedio, 0)," + Environment.NewLine;
                    sSql += "null, null, C.Observacion, M.CORRELATIVO, AUX.codigo Codigo_Auxiliar, AUX.descripcion Descripcion_Auxiliar" + Environment.NewLine;
                    sSql += "from cv402_cabecera_movimientos C LEFT OUTER JOIN" + Environment.NewLine;
                    sSql += "cv404_auxiliares_contables AUX on C.Id_Auxiliar = AUX.Id_Auxiliar," + Environment.NewLine;
                    sSql += "cv402_movimientos_bodega M, cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                    sSql += "where C.CG_EMPRESA = " + Program.iCgEmpresa + Environment.NewLine;
                    sSql += "and C.Fecha >= Convert(DateTime,'" + sFechaDesde + "', 120)" + Environment.NewLine;
                    sSql += "and C.Fecha <= Convert(DateTime,'" + sFechaHasta + "', 120)" + Environment.NewLine;
                    sSql += "and C.externo = 1" + Environment.NewLine;
                    sSql += "and C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and M.Id_Producto = P.Id_Producto" + Environment.NewLine;
                    sSql += "and M.Id_Producto =  " + iIdProducto + Environment.NewLine;
                    sSql += "and C.Cg_Tipo_Movimiento = T.Cg_Tipo_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and M.Estado = 'A'" + Environment.NewLine;
                    sSql += "and C.Estado = 'A'" + Environment.NewLine;
                    sSql += "order by C.CG_EMPRESA, C.Fecha, T.Tipo,  M.Correlativo";

                    DataTable dtSentencia = new DataTable();
                    dtSentencia.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSentencia, sSql);

                    //RESPUESTA A LA CONSULTA A LA BASE DE DATOS
                    if (bRespuesta == false)
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    //VERIFICAR SI EXISTEN REGISTROS
                    if (dtSentencia.Rows.Count == 0)
                    {
                        //ok.lblMensaje.Text = "No hay registros en la consulta.";
                        //ok.ShowDialog();
                        break;
                        //return;
                    }

                    //CONTINUACION DE CONSTRUCCION DEL DATAGRID
                    double dbPrecioPromedio = 0;
                    dbtotalEgresos = 0;
                    dbTotalValorEgresos = 0;
                    dbTotalValorIngresos = 0;
                    double dbTotalPrecioIngreso = 0;

                    for (int j = 0; j < dtSentencia.Rows.Count; j++)
                    {
                        string sFecha = dtSentencia.Rows[j].ItemArray[0].ToString();
                        string sNumeroMovimientos = dtSentencia.Rows[j].ItemArray[1].ToString();
                        string sReferencia;

                        if (dtSentencia.Rows[j].ItemArray[3].ToString() != null)
                        {
                            sReferencia = dtSentencia.Rows[j].ItemArray[3].ToString();
                        }

                        else
                        {
                            sReferencia = "";
                        }

                        double dbCantidad = Convert.ToDouble(dtSentencia.Rows[j].ItemArray[6].ToString());
                        double dbValorUnitario;

                        if (dtSentencia.Rows[j].ItemArray[7].ToString() != null)
                        {
                            dbValorUnitario = Convert.ToDouble(dtSentencia.Rows[j].ItemArray[7].ToString());
                        }

                        else
                        {
                            dbValorUnitario = 0;
                        }


                        if (dtSentencia.Rows[j].ItemArray[8].ToString() != null)
                        {
                            dbPrecioPromedio = Convert.ToDouble(dtSentencia.Rows[j].ItemArray[8].ToString());
                        }

                        else
                        {
                            dbPrecioPromedio = 0;
                        }

                        string sObservacion = dtSentencia.Rows[j].ItemArray[11].ToString();
                        int iCorrelativo = Convert.ToInt32(dtSentencia.Rows[j].ItemArray[12].ToString());
                        double dbValorIngreso = (Convert.ToDouble(dbCantidad.ToString("N2")) * Convert.ToDouble(dbPrecioPromedio.ToString("N2")));
                        dbSaldo += dbCantidad;

                        if (dbCantidad > 0)
                        {
                            dbPrecioEgresos += (Convert.ToDouble(dbCantidad.ToString("N2")));

                            dgvDetalleVenta.Rows.Add(sFecha,
                                                   sNumeroMovimientos,
                                                   sReferencia,
                                                   dbValorIngreso.ToString("N4"),
                                                   "",
                                                   "",
                                                   "",
                                                   dbPrecioEgresos.ToString("N2"),
                                                   dbPrecioPromedio.ToString("N2"),
                                                   "",
                                                    "",
                                                   sObservacion
                                                 );

                            dbTotalIngresos += Convert.ToDouble(dbValorIngreso.ToString("N4"));
                            dbTotalValorIngresos += Convert.ToDouble(dbValorIngreso.ToString("N2"));

                        }
                        else
                        {
                            dbPrecioEgresos -= Convert.ToDouble(Math.Abs(Convert.ToDouble(dbCantidad.ToString("N4"))).ToString("N2"));
                            dbTotalPrecioIngreso += Convert.ToDouble(Math.Abs(Convert.ToDouble(dbCantidad.ToString("N4"))).ToString("N2"));

                            dgvDetalleVenta.Rows.Add(sFecha,
                                                   sNumeroMovimientos,
                                                   sReferencia,
                                                   "",
                                                   Math.Abs(Convert.ToDouble(dbCantidad.ToString("N4"))).ToString("N2"),
                                                   dbPrecioPromedio.ToString("N2"),
                                                   Math.Abs(dbValorIngreso).ToString("N2"),
                                                   dbPrecioEgresos.ToString("N2"),
                                                   dbPrecioPromedio.ToString("N2"),
                                                   "",
                                                   "",
                                                   sObservacion
                                                 );

                            dbtotalEgresos += Convert.ToDouble(Math.Abs(Convert.ToDouble(dbCantidad.ToString("N4"))).ToString("N2"));
                            dbTotalValorEgresos += Convert.ToDouble(Math.Abs(Convert.ToDouble(dbValorIngreso.ToString("N2"))).ToString("N2"));

                        }

                    }

                    dgvDetalleVenta.Rows.Add("",
                            "**Saldo Final**",
                            "TOTALES: ",
                            dbTotalIngresos.ToString("N4"),
                            dbTotalPrecioIngreso.ToString("N2"),
                            "",
                            dbTotalValorEgresos.ToString("N2"),
                            dbPrecioEgresos.ToString("N2"),
                            dbPrecioPromedio.ToString("N2"),
                            "",
                            "",
                            ""
                            );

                    dgvDetalleVenta.Rows.Add();
                    dgvDetalleVenta.Rows.Add();
                }

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmReporteMovimientoEmpresa_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaDesde.Text = sFecha;
            txtFechaHasta.Text = sFecha;

            //cmbOficina.SelectedIndexChanged -= new EventHandler(cmbOficina_SelectedIndexChanged);
            llenarComboOficina();
            //cmbOficina.SelectedIndexChanged += new EventHandler(cmbOficina_SelectedIndexChanged);

            llenarComboTipoMovimiento();
            llenarSentenciasDbAyuda();
        }        

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comprobarCampos() == true)
            {
                if (dbAyudaArticuloFinal.txtIdentificacion.Text.Trim() == "")
                {
                    dbAyudaArticuloFinal.txtIdentificacion.Text = dbAyudaArticuloInicial.txtIdentificacion.Text.Trim();
                    dbAyudaArticuloFinal.txtDatos.Text = dbAyudaArticuloInicial.txtDatos.Text.Trim();
                    dbAyudaArticuloFinal.iId = dbAyudaArticuloInicial.iId;
                    sCodigoProductoFina = sCodigoProducto;
                    sDescripcionProductoFinal = sDescripcionProducto;
                    iIdProductoFinal = iIdProducto;
                    llenarGrid();
                }
                else
                {
                    llenarGrid();
                }
            }

            Cursor = Cursors.Default;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea Limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiar();
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
                    ok.lblMensaje.Text = "No hay datos para mostrar";
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
