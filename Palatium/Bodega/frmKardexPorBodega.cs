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
    public partial class frmKardexPorBodega : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        string sFecha;
        bool bRespuesta;
        DataTable dtConsulta;
        
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sCodigoBodega, sSentenciaDbAyuda;

        public frmKardexPorBodega()
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
                sSql += "select idempresa, razonSocial, Codigo" + Environment.NewLine;
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

        //Función para cargar el combo oficina 1 
        private void cargarComboOficina()
        {
            try
            {
                //sSql = "";
                //sSql += "select LOC.id_localidad, BO.descripcion," + Environment.NewLine;
                //sSql += "case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion, BO.id_bodega" + Environment.NewLine; 
                //sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;  
                //sSql += "where LOC.id_bodega =  BO.id_bodega" + Environment.NewLine;
                //sSql += "and BO.estado = 'A'" + Environment.NewLine;
                //sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                //sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                //sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                //sSql += "order by BO.descripcion";

                sSql = "";
                sSql += "select LOC.id_localidad, BO.descripcion  + case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion, " + Environment.NewLine;
                sSql += "BO.id_bodega, BO.categoria" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado =  'A'" + Environment.NewLine;
                sSql += "order by BO.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbOficina.llenar(dtConsulta, sSql);
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

        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine; 
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.llenar(dtConsulta, sSql);
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

        //Función para llenar el combo de tipo movimiento
        private void llenarComboTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00648'" + Environment.NewLine;
                sSql += "and ESTADO = 'A'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbTipoMovimiento.llenar(dtConsulta, sSql);
                    //cmbTipoMovimiento.SelectedIndex = -1;
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
                excel.Cells[2, 2] = "KARDEX POR BODEGA:";
                excel.Cells[3, 1] = "LOCALIDAD:";
                excel.Cells[3, 2] = cmbOficina.Text;
                excel.Cells[4, 1] = "PROD. INICIAL:";
                excel.Cells[4, 2] = dbAyudaArticuloInicial.txtIdentificacion.Text;
                excel.Cells[5, 1] = "PROD. FINAL:";
                excel.Cells[5, 2] = dbAyudaArticuloFinal.txtIdentificacion.Text;
                excel.Cells[6, 1] = "FECHA INICIO";
                excel.Cells[6, 2] = txtFechaDesde.Text;
                excel.Cells[6, 4] = "FECHA FIN";
                excel.Cells[6, 5] = txtFechaHasta.Text;

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

                excel.get_Range("A8", "R8").BorderAround();

                excel.Visible = true;
                Cursor = Cursors.Arrow;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el grid
        private void llenarGrid()
        {
            try
            {
                dgvDetalleVenta.Rows.Clear();
                string sFechaInicio = Convert.ToDateTime(txtFechaDesde.Text).ToString("yyyy-MM-dd");
                string sFechaFin = Convert.ToDateTime(txtFechaHasta.Text).ToString("yyyy-MM-dd");
                int iTipoMovimiento = devuelveTipoMovimiento();

                if (iTipoMovimiento == -1)
                {
                    return;
                }

                if (iTipoMovimiento == 0)
                {
                    ok.lblMensaje.Text = "No existen registros en la consulta.";
                    ok.ShowDialog();
                }

                //INICIO LLENADO DE GRID
                double dbSaldoTotal = 0;
                double dbTotalFinal = 0;
                double dbTotalEgresoFinal = 0;

                sSql = "";
                sSql += "Select M.id_producto,P.Codigo, P.Descripcion,P.unidad_de_medida" + Environment.NewLine;
                sSql += "From cv402_movimientos_bodega M, cv402_cabecera_movimientos C," + Environment.NewLine;
                sSql += "cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                sSql += "Where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                sSql += "And C.Fecha >= Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                sSql += "And C.Fecha <= Convert(DateTime,'" + sFechaFin + "',120)" + Environment.NewLine;
                sSql += "And C.Id_Bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
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
                sSql += "and C.Estado = 'A' and M.Estado = 'A'" + Environment.NewLine;  // And C.Cg_Tipo_Movimiento  = "+iTipoMovimiento+" ";
                sSql += "group By P.codigo,P.descripcion, P.unidad_de_medida, M.id_producto" + Environment.NewLine;
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
                    string sunidad = dtConsulta.Rows[i][3].ToString();

                    sSql = "";
                    sSql += "select isnull(Sum(M.Cantidad),0) saldo," + Environment.NewLine;
                    sSql += "isnull(Sum(M.Cantidad*isnull(M.valor_unitario,0)),0) total" + Environment.NewLine;
                    sSql += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                    sSql += "where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                    sSql += "and C.Fecha < Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                    sSql += "and C.Id_Bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                    sSql += "and M.Id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and C.estado = 'A'" + Environment.NewLine;
                    sSql += "and M.estado = 'A' ";

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
                    double dbCostoUnitario = dbTotal / dbSaldo;

                    //Aquí añado la primera línea al datagridview
                    dgvDetalleVenta.Rows.Add("",
                                             sCodigo,
                                             "",
                                             sDescripcin,
                                             sCodigo,
                                             sunidad,
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

                    sSql = "";
                    sSql += "select C.Fecha, C.Numero_Movimiento, C.Id_Bodega, C.Id_Pedido, C.Referencia_Externa," + Environment.NewLine;
                    sSql += "P.Codigo Codigo_Producto, P.Descripcion Descripcion_Producto,P.unidad_de_medida, isnull(M.Cantidad,0)," + Environment.NewLine;
                    sSql += "isnull((M.Valor_Unitario-isnull(m.valor_dscto,0) ),0)valor_unitario, isnull(M.Precio_Promedio,0), null, null," + Environment.NewLine;
                    sSql += "C.Observacion, M.CORRELATIVO, AUX.codigo Codigo_Auxiliar, AUX.descripcion Descripcion_Auxiliar" + Environment.NewLine;
                    sSql += "from cv402_cabecera_movimientos C left outer join" + Environment.NewLine;
                    sSql += "cv404_auxiliares_contables AUX on C.Id_Auxiliar = AUX.Id_Auxiliar," + Environment.NewLine;
                    sSql += "cv402_movimientos_bodega M, cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                    sSql += "where C.idEMPRESA = " + cmbEmpresa.SelectedValue + Environment.NewLine;
                    sSql += "and C.Fecha >= Convert(DateTime,'" + sFechaInicio + "',120)" + Environment.NewLine;
                    sSql += "and C.Fecha <= Convert(DateTime,'" + sFechaFin + "',120)" + Environment.NewLine;
                    sSql += "and C.Id_Bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                    sSql += "and C.Id_Movimiento_Bodega = M.Id_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and M.Id_Producto = P.Id_Producto" + Environment.NewLine;
                    sSql += "and M.Id_Producto =  " + iIdProducto + Environment.NewLine;
                    sSql += "and C.Cg_Tipo_Movimiento = T.Cg_Tipo_Movimiento_Bodega" + Environment.NewLine;
                    sSql += "and C.estado = 'A'" + Environment.NewLine;
                    sSql += "and M.Estado = 'A'" + Environment.NewLine;     // And C.Cg_Tipo_Movimiento  = "+iTipoMovimiento+" "+
                    sSql += "order By C.CG_EMPRESA, C.Fecha, T.Tipo, M.Correlativo ";

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
                        string sFecha = Convert.ToDateTime(dtQuery.Rows[j][0].ToString().Substring(0, 10)).ToString("dd-MM-yyy");
                        string sNumeroMovimiento = dtQuery.Rows[j][1].ToString();
                        string sReferencia = dtQuery.Rows[j][4].ToString();
                        double dbCantidad = Convert.ToDouble(dtQuery.Rows[j][8].ToString());
                        double dbValorUnitario = Convert.ToDouble(dtQuery.Rows[j][9].ToString());
                        double dbPrecioPromedio = Convert.ToDouble(dtQuery.Rows[j][10].ToString());
                       
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

                        double dbTotalIngresos = (Math.Abs(dbCantidad) * dbValorUnitario);

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
                            dgvDetalleVenta.Rows.Add(sFecha,
                                                    sNumeroMovimiento,
                                                    cmbBodega.SelectedValue,
                                                    sReferencia,
                                                    sCodigo,
                                                    sunidad,
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
                                                    sObservacion
                                                    );

                            dbTotalEgresoFinal = (dbTotalFinal + dbTotalIngresos);
                        }
                        else
                        {
                            dbTotalEgresoFinal = (dbTotalFinal - dbTotalIngresos);
                            double dbTotalEgresos = 0;
                            dbTotalEgresos = (Math.Abs(dbCantidad) * dbPrecioPromedio);
                            dgvDetalleVenta.Rows.Add(sFecha,
                                                    sNumeroMovimiento,
                                                    cmbBodega.SelectedValue,
                                                    sReferencia,
                                                    sCodigo,
                                                    sunidad,
                                                    "",
                                                    "",
                                                    "",
                                                    Math.Abs(dbCantidad).ToString("N2"),
                                                    "",
                                                    dbTotalEgresos.ToString("N2"),
                                                    (dbSaldoTotal -= Math.Abs(dbCantidad)).ToString("N2"),
                                                    dbPrecioPromedio.ToString("N4"),
                                                    (dbTotalEgresoFinal - Math.Abs(dbTotalEgresos)).ToString("N2"),
                                                    sCodigoAuxiliar,
                                                    sDescripcionProveedor,
                                                    sObservacion
                                                    );
                        }

                        dbTotalEgresoFinal = (dbTotalFinal - dbTotalIngresos);

                    }
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

        //Función que devuelve el Cg_Tipo_Movimiento
        private int devuelveTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where codigo = 'EMP'" + Environment.NewLine;
                sSql += "and tabla = 'SYS$00648'";

                DataTable dtAydua = new DataTable();
                dtAydua.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAydua, sSql);
                
                if (bRespuesta == true)
                {
                    if (dtAydua.Rows.Count > 0)
                    {
                        int Correlativo = Convert.ToInt32(dtAydua.Rows[0][0].ToString());
                        return Correlativo;
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //función para buscar el código de bodega
        private void buscarCodigoBodega()
        {
            try
            {
                sSql = "";
                sSql += "select codigo" + Environment.NewLine;
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_bodega = " + cmbBodega.SelectedValue;

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

        //Función para llenar las sentencias en el dbAyuda
        private void llenarSentencias()
        {
            try
            {
                string sCodigo = "";
                string sNombre = "";
                sSentenciaDbAyuda = "";

                if (sCodigoBodega == "MP" || sCodigoBodega == "M2" || sCodigoBodega == "M3")
                {
                    sSentenciaDbAyuda += "select substring(PRO.Codigo,1,15) codigo, isnull((Select Nombre From cv401_nombre_productos" + Environment.NewLine;
                    sSentenciaDbAyuda += "where Id_Producto = PRO.Id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "and Cg_Tipo_Nombre = 5076" + Environment.NewLine;
                    sSentenciaDbAyuda += "and estado ='A'),'(Sin Nombre)')" + Environment.NewLine;
                    sSentenciaDbAyuda += "descripcion, PRO.Id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "from cv401_productos PRO, cv401_productos PROPADRE" + Environment.NewLine;
                    sSentenciaDbAyuda += "where PRO.id_producto_Padre = PROPADRE.id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "and PROPADRE.Codigo Like '%'  " + Environment.NewLine;
                    sSentenciaDbAyuda += "and PRO.estado = 'A'" + Environment.NewLine;
                    sSentenciaDbAyuda += "and PRO.nivel in (1,2,3)" + Environment.NewLine;
                    //sSentenciaDbAyuda += "order By PRO.Codigo, descripcion ";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSentenciaDbAyuda);
                    if (bRespuesta == true)
                    {
                        sCodigo = dtConsulta.Rows[0][0].ToString();
                        sNombre = dtConsulta.Rows[0][1].ToString();
                    }
                }
                else
                {
                    sSentenciaDbAyuda += "select substring(PRO.Codigo,1,15) codigo, isnull((Select Nombre From cv401_nombre_productos" + Environment.NewLine;
                    sSentenciaDbAyuda += "where Id_Producto = PRO.Id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "and Cg_Tipo_Nombre = 5076" + Environment.NewLine;
                    sSentenciaDbAyuda += "and estado ='A'),'(Sin Nombre)') " + Environment.NewLine;
                    sSentenciaDbAyuda += "descripcion,PRO.Id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "from cv401_productos PRO, cv401_productos PROPADRE" + Environment.NewLine;
                    sSentenciaDbAyuda += "where PRO.id_producto_Padre = PROPADRE.id_Producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "and PROPADRE.Codigo Like '2%' " + Environment.NewLine;
                    sSentenciaDbAyuda += "and PRO.estado = 'A'" + Environment.NewLine;
                    sSentenciaDbAyuda += "and PRO.nivel in (1,2,3)" + Environment.NewLine;
                    //sSentenciaDbAyuda += "order By PRO.Codigo,descripcion ";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSentenciaDbAyuda);
                    if (bRespuesta == true)
                    {
                        sCodigo = dtConsulta.Rows[0][0].ToString();
                        sNombre = dtConsulta.Rows[0][1].ToString();
                    }
                }

                dbAyudaFamiliaArticulo.Ver(sSentenciaDbAyuda, "PRO.Codigo", 2, 0, 1);
                //dbAyudaFamiliaArticulo.txtIdentificacion.Text = sCodigo;
                //dbAyudaFamiliaArticulo.txtDatos.Text = sNombre;
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
                sSentenciaDbAyuda = "";
                if (sCodigoBodega == "MP" || sCodigoBodega == "M2" || sCodigoBodega == "M3")
                {

                    sSentenciaDbAyuda += "select Codigo, Descripcion, Id_Producto Correlativo" + Environment.NewLine;
                    sSentenciaDbAyuda += "from cv401_vw_nombre_producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "where Codigo_Padre Like '1%'" + Environment.NewLine;
                    //sSentenciaDbAyuda += "order By Codigo, Descripcion ";
                }
                else
                {
                    sSentenciaDbAyuda += "select Codigo, Descripcion, Id_Producto Correlativo" + Environment.NewLine;
                    sSentenciaDbAyuda += "from cv401_vw_nombre_producto" + Environment.NewLine;
                    sSentenciaDbAyuda += "where Codigo_Padre Like '2%'" + Environment.NewLine;
                    //sSentenciaDbAyuda += "order By Codigo, Descripcion ";
                }

                dbAyudaArticuloInicial.Ver(sSentenciaDbAyuda, "Codigo", 2, 0, 1);
                dbAyudaArticuloFinal.Ver(sSentenciaDbAyuda, "Codigo", 2, 0, 1);
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

        private void frmKardexPorBodega_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaDesde.Text = sFecha;
            txtFechaHasta.Text = sFecha;
            cargarComboEmpresa();
            cargarComboBodega();
            cargarComboOficina();
            llenarComboTipoMovimiento();
        }

        
        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOficina.SelectedIndex > 0)
                {
                    sSql = "";
                    sSql += "select id_bodega" + Environment.NewLine;
                    sSql += "from tp_vw_localidades" + Environment.NewLine;
                    sSql += "where id_localidad = " + cmbOficina.SelectedValue;

                    DataTable dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        cmbBodega.SelectedValue = dtConsulta.Rows[0][0];
                        buscarCodigoBodega();
                        llenarSentencias();
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
                Convert.ToDateTime(txtFechaDesde.Text).ToString("dd/MM/yyyy");
                Convert.ToDateTime(txtFechaHasta.Text).ToString("dd/MM/yyyy");
                llenarGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese una fecha con el formato 'dd/mm/aaaa'", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
                txtFechaDesde.Text = sFecha;
                txtFechaHasta.Text = sFecha;
                dgvDetalleVenta.Rows.Clear();
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
