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
    public partial class frmKardexPorEmpresa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        string sFecha;
        string sSentenciaDbAyuda;

        bool bRespuesta;
        DataTable dtConsulta;

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        
        public frmKardexPorEmpresa()
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias en el dbAyuda
        private void llenarSentencias()
        {
            string sCodigo = "";
            string sNombre = "";

            sSentenciaDbAyuda = "";
            sSentenciaDbAyuda += "Select substring(PRO.Codigo, 1, 15) codigo, isnull((" + Environment.NewLine;
            sSentenciaDbAyuda += "Select Nombre From cv401_nombre_productos" + Environment.NewLine;
            sSentenciaDbAyuda += "Where Id_Producto = PRO.Id_Producto" + Environment.NewLine;
            sSentenciaDbAyuda += "And Cg_Tipo_Nombre = 5076" + Environment.NewLine;
            sSentenciaDbAyuda += "and estado ='A')," + Environment.NewLine;
            sSentenciaDbAyuda += "'(Sin Nombre)') descripcion, PRO.Id_Producto" + Environment.NewLine;
            sSentenciaDbAyuda += "From cv401_productos PRO, cv401_productos PROPADRE" + Environment.NewLine;
            sSentenciaDbAyuda += "Where PRO.id_producto_Padre = PROPADRE.id_Producto" + Environment.NewLine;
            sSentenciaDbAyuda += "and PROPADRE.Codigo Like '1%'" + Environment.NewLine;            
            sSentenciaDbAyuda += "and PRO.estado = 'A'" + Environment.NewLine;
            sSentenciaDbAyuda += "and PRO.nivel in (1,2,3)" + Environment.NewLine;
            sSentenciaDbAyuda += "Order By PRO.Codigo,descripcion";


            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSentenciaDbAyuda);
            if (bRespuesta == true)
            {
                sCodigo = dtConsulta.Rows[0][0].ToString();
                sNombre = dtConsulta.Rows[0][1].ToString();
            }

            dbAyudaFamiliaArticulo.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);

        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentenciasDbAyuda()
        {
            sSentenciaDbAyuda = "";
            sSentenciaDbAyuda += "Select Codigo, Descripcion, Id_Producto Correlativo" + Environment.NewLine; 
            sSentenciaDbAyuda += "From cv401_vw_nombre_producto" + Environment.NewLine; 
            sSentenciaDbAyuda += "Where Codigo_Padre Like '2%'" + Environment.NewLine;
            sSentenciaDbAyuda += "Order By Codigo , Descripcion ";

            dbAyudaArticuloInicial.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
            dbAyudaArticuloFinal.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);

        }

        //Función para llenar el combo de tipo movimiento
        private void llenarComboTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "Select correlativo, valor_texto" + Environment.NewLine;
                sSql += "From tp_codigos" + Environment.NewLine;
                sSql += "Where tabla = 'SYS$00648'" + Environment.NewLine;
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvDetalleVenta.Rows.Clear();
                string sFechaInicio = Convert.ToDateTime(txtFechaDesde.Text).ToString("yyyy-MM-dd");
                string sFechaFin = Convert.ToDateTime(txtFechaHasta.Text).ToString("yyyy-MM-dd");

                double dbSaldoTotal = 0;
                double dbTotalFinal = 0;
                double dbTotalEgresoFinal = 0;

                sSql = "";
                sSql += "Select M.id_producto,P.Codigo, P.Descripcion" + Environment.NewLine;
                sSql += "From cv402_movimientos_bodega M, cv402_cabecera_movimientos C," + Environment.NewLine;
                sSql += "cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                sSql += "Where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                sSql += "And C.Fecha >= Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                sSql += "And C.Fecha <= Convert(DateTime,'" + sFechaFin + "',120)" + Environment.NewLine;
                sSql += "And C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                sSql += "And M.Id_Producto = P.Id_Producto" + Environment.NewLine;
                sSql += "And P.Codigo_Padre like '" + dbAyudaFamiliaArticulo.txtIdentificacion.Text + "%'" + Environment.NewLine;

                if (dbAyudaArticuloInicial.txtIdentificacion.Text.Trim() != "")
                {
                    sSql += "And P.Codigo >= '" + dbAyudaArticuloInicial.txtIdentificacion.Text + "'" + Environment.NewLine;
                }

                if (dbAyudaArticuloFinal.txtIdentificacion.Text.Trim() != "")
                {
                    sSql += "And P.Codigo <= '" + dbAyudaArticuloFinal.txtIdentificacion.Text + "'" + Environment.NewLine;
                }

                sSql += "And C.Cg_Tipo_Movimiento = T.Cg_Tipo_Movimiento_Bodega" + Environment.NewLine;
                sSql += "and C.Estado = 'A' and M.Estado = 'A'" + Environment.NewLine;
                sSql += "group By P.codigo, P.descripcion, P.unidad_de_medida, M.id_producto" + Environment.NewLine;
                sSql += "order By P.codigo ";

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
                    ok.lblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccinadas";
                    ok.ShowDialog();
                    return;
                }

                //RECORRIDO DE REGISTROS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                    string sCodigo = dtConsulta.Rows[i][1].ToString();
                    string sDescripcin = dtConsulta.Rows[i][2].ToString();
                    double dbSumaingresos = 0;
                    double dbSumaSoloIngresos = 0;
                    double dbSumaEgresos = 0;
                    double dbSumaTotalEgresos = 0;
                    double dbSaldoFinal = 0;
                    double dbPrecioPromedio = 0;

                    sSql = "";
                    sSql += "Select isnull(Sum(M.Cantidad),0) saldo, isnull(Sum(M.Cantidad * isnull(M.valor_unitario, 0)), 0) total" + Environment.NewLine;
                    sSql += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                    sSql += "Where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                    sSql += "And C.Fecha < Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                    sSql += "And C.externo = 1" + Environment.NewLine;
                    sSql += "And M.Id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "And C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "And C.Estado = 'A'" + Environment.NewLine;
                    sSql += "and M.Estado = 'A' ";

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
                    double dbSaldo = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());
                    double dbTotal = Convert.ToDouble(dtAyuda.Rows[0][1].ToString());
                    double dbCostoUnitario;

                    if (dbSaldo <= 0)
                    {
                        dbCostoUnitario = 0;
                    }

                    else
                    {
                        dbCostoUnitario = dbTotal / dbSaldo;
                    }

                    //Aquí añado la primera línea al datagridview
                    dgvDetalleVenta.Rows.Add("",
                                             sCodigo,
                                             "",
                                             "*Saldo Anterior*",
                                             sCodigo,
                                             sDescripcin,
                                             dbSaldo.ToString("N2"),
                                             dbCostoUnitario.ToString("N4"),
                                             dbTotal.ToString("N2"),
                                             "",
                                             "",
                                             "",
                                             dbSaldo.ToString("N2"),
                                             "",
                                             dbTotal.ToString("N2")

                                             );
                    dbSaldoTotal = 0;
                    dbSaldoTotal += dbSaldo;
                    dbTotalFinal = 0;
                    dbTotalFinal += dbTotal;
                    dbSumaingresos = dbSaldo;

                    sSql = "";
                    sSql += "Select C.Fecha, C.Numero_Movimiento, C.Id_Bodega, C.Id_Pedido, C.Referencia_Externa," + Environment.NewLine;
                    sSql += "P.Codigo Codigo_Producto, P.Descripcion Descripcion_Producto, isnull(M.Cantidad,0)," + Environment.NewLine;
                    sSql += "isnull((M.Valor_Unitario-isnull(m.valor_dscto,0) ),0) valor_unitario, isnull(M.Precio_Promedio, 0) precio_promedio, NULL, NULL," + Environment.NewLine;
                    sSql += "isnull(numero_TRANSFERENCIA_cruzado,'') numero_TRANSFERENCIA_cruzado, C.Observacion," + Environment.NewLine;
                    sSql += "M.CORRELATIVO, AUX.codigo Codigo_Auxiliar, AUX.descripcion Descripcion_Auxiliar" + Environment.NewLine;
                    sSql += "From cv402_cabecera_movimientos C left outer join" + Environment.NewLine;
                    sSql += "cv404_auxiliares_contables AUX on C.Id_Auxiliar = AUX.Id_Auxiliar," + Environment.NewLine;
                    sSql += "cv402_movimientos_bodega M, cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                    sSql += "Where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                    sSql += "And C.Fecha >= Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                    sSql += "And C.Fecha <= Convert(DateTime,'" + sFechaFin + "', 120)" + Environment.NewLine;
                    sSql += "And C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "And M.Id_Producto = P.Id_Producto" + Environment.NewLine;
                    sSql += "And M.Id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "And C.Cg_Tipo_Movimiento = T.Cg_Tipo_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "And C.Estado = 'A'" + Environment.NewLine;
                    sSql += "and M.Estado = 'A'" + Environment.NewLine;
                    sSql += " Order By C.CG_EMPRESA, C.Fecha, T.Tipo, M.Correlativo";

                    DataTable dtQuery = new DataTable();
                    dtQuery.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtQuery, sSql);

                    //RESPUESTA A LA CONSULTA A LA BASE DE DATOS
                    if (bRespuesta == false)
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    //VERIFICAR SI EXISTEN REGISTROS
                    if (dtQuery.Rows.Count == 0)
                    {
                        break;
                    }

                    for (int j = 0; j < dtQuery.Rows.Count; j++)
                    {
                        // dbSaldoFinal = 0;
                        dbPrecioPromedio = 0;
                        string sFecha = dtQuery.Rows[j][0].ToString();
                        string sNumeroMovimiento = dtQuery.Rows[j][1].ToString();
                        int iIdBodega = Convert.ToInt32(dtQuery.Rows[j][2].ToString());
                        string sReferencia = dtQuery.Rows[j][4].ToString();
                        double dbCantidad = Convert.ToDouble(dtQuery.Rows[j][7].ToString());
                        double dbValorUnitario = Convert.ToDouble(dtQuery.Rows[j][8].ToString());
                        dbPrecioPromedio = Convert.ToDouble(dtQuery.Rows[j][9].ToString());
                        
                        string sObservacion;
                        if (dtQuery.Rows[j][13].ToString() != null)
                        {
                            sObservacion = dtQuery.Rows[j][13].ToString();
                        }

                        else
                        {
                            sObservacion = "";
                        }

                        int iCorrelativo = Convert.ToInt32(dtQuery.Rows[j][14].ToString());
                        
                        string sDescripcionProveedor;
                        if (dtQuery.Rows[j][16].ToString() != null)
                        {
                            sDescripcionProveedor = dtQuery.Rows[j][16].ToString();
                        }

                        else
                        {
                            sDescripcionProveedor = "";
                        }

                        double dbTotalIngresos = (dbCantidad * dbValorUnitario);
                        
                        string sCodigoAuxiliar;
                        if (dtQuery.Rows[j][15].ToString() != null)
                        {
                            sCodigoAuxiliar = dtQuery.Rows[j][15].ToString();
                        }

                        else
                        {
                            sCodigoAuxiliar = "";
                        }

                        if (dbCantidad > 0)
                        {
                            dgvDetalleVenta.Rows.Add(sFecha.Substring(0, 10),
                                                    sNumeroMovimiento,
                                                    iIdBodega,
                                                    sReferencia,
                                                    sCodigo,
                                                    sDescripcin,
                                                    Math.Abs(dbCantidad).ToString("N2"),
                                                    dbValorUnitario.ToString("N2"),
                                                    dbTotalIngresos.ToString("N2"),
                                                    "",
                                                    "",
                                                    "",
                                                    (dbSaldoTotal += dbCantidad).ToString("N2"),
                                                    dbPrecioPromedio.ToString("N4"),
                                                    (dbTotalFinal + dbTotalIngresos).ToString("N2"),
                                                    sCodigoAuxiliar,
                                                    sDescripcionProveedor,
                                                    sObservacion,
                                                    "",
                                                    iCorrelativo
                                                    );

                            dbSumaingresos += Math.Abs(dbCantidad);
                            dbSumaSoloIngresos += dbTotalIngresos;
                            dbTotalEgresoFinal = (dbTotalFinal + dbTotalIngresos);
                            dbSaldoFinal = dbSaldoTotal;
                        }
                        else
                        {
                            dbTotalEgresoFinal = (dbTotalFinal - dbTotalIngresos);
                            double dbTotalEgresos = 0;
                            dbTotalEgresos = (Math.Abs(dbCantidad) * dbPrecioPromedio);
                            dgvDetalleVenta.Rows.Add(sFecha.Substring(0, 10),
                                                    sNumeroMovimiento,
                                                    iIdBodega,
                                                    sReferencia,
                                                    sCodigo,
                                                    sDescripcin,
                                                    "",
                                                    "",
                                                    "",
                                                    Math.Abs(dbCantidad).ToString("N2"),
                                                    dbPrecioPromedio.ToString("N4"),
                                                    dbTotalEgresos.ToString("N2"),
                                                    (dbSaldoTotal -= Math.Abs(dbCantidad)).ToString("N2"),
                                                    dbPrecioPromedio.ToString("N4"),
                                                    (dbTotalEgresoFinal - dbTotalEgresos).ToString("N2"),
                                                    sCodigoAuxiliar,
                                                    sDescripcionProveedor,
                                                    sObservacion,
                                                    "",
                                                    iCorrelativo
                                                    );

                            dbSumaEgresos += Math.Abs(dbCantidad);
                            dbSumaTotalEgresos += dbTotalEgresos;

                            dbSaldoFinal = dbSaldoTotal;

                        }
                    }
                    

                    dgvDetalleVenta.Rows.Add("",
                                                                "**Saldo Final**",
                                                                "",
                                                                "TOTALES:",
                                                                sCodigo,
                                                                "",
                                                                dbSumaingresos.ToString("N2"),
                                                                "",
                                                                dbSumaSoloIngresos.ToString("N2"),
                                                                dbSumaEgresos.ToString("N2"),
                                                                "",
                                                                dbSumaTotalEgresos.ToString("N2"),
                                                                dbSaldoFinal.ToString("N2"),
                                                                dbPrecioPromedio.ToString("N4")
                                                                );

                    dgvDetalleVenta.Rows.Add();
                    dgvDetalleVenta.Rows.Add();


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
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Application.Workbooks.Add(true);

                int iIndiceColumna = 0;
                excel.Columns.ColumnWidth = 10;
                excel.Cells[1, 1] = "EMPRESA: ";
                excel.Cells[1, 2] = "COSA NOSTRA";
                excel.Cells[2, 1] = "REPORTE:";
                excel.Cells[2, 2] = "KARDEX POR EMPRESA";
                excel.Cells[3, 1] = "PROD. INICIAL:";
                excel.Cells[3, 2] = dbAyudaArticuloInicial.txtIdentificacion.Text;
                excel.Cells[4, 1] = "PROD. FINAL:";
                excel.Cells[4, 2] = dbAyudaArticuloFinal.txtIdentificacion.Text;
                excel.Cells[5, 1] = "FECHA INICIO";
                excel.Cells[5, 2] = txtFechaDesde.Text;
                excel.Cells[5, 4] = "FECHA FIN";
                excel.Cells[5, 5] = txtFechaHasta.Text;

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    if (iIndiceColumna == 2 || iIndiceColumna == 3)
                        excel.Cells[8, iIndiceColumna].ColumnWidth = 15;

                    excel.Cells[8, iIndiceColumna] = col.HeaderText;
                    excel.Cells[8, iIndiceColumna].Interior.Color = Color.Yellow;
                    excel.Cells[8, iIndiceColumna].BorderAround();
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

                excel.get_Range("A8", "T8").BorderAround();

                excel.Visible = true;
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
            dgvDetalleVenta.Rows.Clear();
            rbFechasProductos.Checked = false;
            rbRangoFechas.Checked = true;
            dbAyudaArticuloInicial.limpiar();
            dbAyudaArticuloFinal.limpiar();
            dbAyudaFamiliaArticulo.limpiar();
        }

        #endregion

        private void frmKardexPorEmpresa_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaDesde.Text = sFecha;
            txtFechaHasta.Text = sFecha;
            cargarComboEmpresa();
            llenarSentencias();
            llenarSentenciasDbAyuda();
            llenarComboTipoMovimiento();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Convert.ToDateTime(txtFechaDesde.Text).ToString("dd/MM/yyyy");
                Convert.ToDateTime(txtFechaHasta.Text).ToString("dd/MM/yyyy");
                llenarGrid();
                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                Cursor = Cursors.Default;
            }
        }

        private void btnEnviarAExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (dgvDetalleVenta.Rows.Count > 0)
                {
                    exportarAExcel(dgvDetalleVenta);
                }

                else
                {
                    ok.lblMensaje.Text = "No hay datos para mostrar.";
                    ok.ShowDialog();
                }

                Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                Cursor = Cursors.Default;
            }
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
    }
}
