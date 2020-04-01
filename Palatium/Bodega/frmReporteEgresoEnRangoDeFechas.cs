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
    public partial class frmReporteEgresoEnRangoDeFechas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;
        string sFechaInicio, sFechaFin;

        public frmReporteEgresoEnRangoDeFechas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dgvInforme.Rows.Clear();
                int iCgTipoMovimiento = obtenerTipoMovimiento();

                if (iCgTipoMovimiento == -1)
                {
                    return;
                }

                sFechaInicio = Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy/MM/dd");
                sFechaFin = Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select CM.id_movimiento_bodega, CM.id_movimiento_bodega," + Environment.NewLine;
                sSql += "CM.id_localidad, CM.fecha, isnull(CM.factura, 0)" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos CM" + Environment.NewLine;
                sSql += "where CM.cg_tipo_movimiento = " + iCgTipoMovimiento + Environment.NewLine;
                sSql += "and CM.numero_movimiento like 'EG%'" + Environment.NewLine;
                sSql += "and CM.estado = 'A'" + Environment.NewLine;
                sSql += "and CM.fecha between '" + sFechaInicio + "' and '" + sFechaFin + "' ";

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
                    int iIdMovimientoBodega = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                    string sNombreProveedor = dtConsulta.Rows[i][1].ToString();
                    int iIdLocalida = Convert.ToInt32(dtConsulta.Rows[i][2].ToString());
                    string sFecha = dtConsulta.Rows[i][3].ToString();
                    int iNumeroFactura = obteneriIdPedido(iIdMovimientoBodega);

                    sSql = "";
                    sSql += "select MB.cantidad, isNull(PP.valor,0),isnull(MB.valor_dscto,0)," + Environment.NewLine;
                    sSql += "NP.nombre, P.codigo, P.paga_iva" + Environment.NewLine;
                    sSql += "from cv402_movimientos_bodega MB inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on MB.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "and MB.estado = 'A' inner join" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NP on P.id_producto = NP.id_producto inner join" + Environment.NewLine;
                    sSql += "cv403_precios_productos PP on P.id_producto = PP.id_producto" + Environment.NewLine;
                    sSql += "where MB.id_movimiento_bodega = " + iIdMovimientoBodega + Environment.NewLine;
                    sSql += "and PP.id_lista_precio = 4";

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
                        double iCantida = Convert.ToDouble(dtQuery.Rows[j][0].ToString());
                        double dbValorUnitario = Convert.ToDouble(dtQuery.Rows[j][1].ToString());
                        double dbValorDescuento = Convert.ToDouble(dtQuery.Rows[j][2].ToString());
                        string sNombreProducto = dtQuery.Rows[j][3].ToString();
                        string sCodigoProducto = dtQuery.Rows[j][4].ToString();
                        int iBanderaIva = Convert.ToInt32(dtQuery.Rows[j][5].ToString());
                        string sPagaIva;

                        if (iBanderaIva == 1)
                        {
                            sPagaIva = "SI";
                        }

                        else
                        {
                            sPagaIva = "NO";
                        }

                        dgvInforme.Rows.Add("",
                                            iIdLocalida,
                                            sFecha.Substring(0, 10),
                                            sNombreProveedor,
                                            iNumeroFactura,
                                            sCodigoProducto,
                                            "",
                                            sNombreProducto,
                                            Math.Abs(iCantida).ToString("N2"),
                                            dbValorUnitario.ToString("N2"),
                                            Math.Abs((iCantida * dbValorUnitario)).ToString("N2"),
                                            sPagaIva,
                                            dbValorDescuento.ToString("N2"),
                                            ""
                                            );
                    }

                }
            }

            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
                Cursor = Cursors.Arrow;
            }
        }

        //Función para obtener cgTipoMovimiento
        private int obtenerTipoMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where  codigo = 'EMP'" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and tabla = 'SYS$00648'";

                DataTable dtAyuda = new DataTable();
                dtAyuda.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtAyuda.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtAyuda.Rows[0][0].ToString());
                }

                else
                {
                    return 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
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

                excel.Columns.ColumnWidth = 40;
                excel.Cells[1, 4] = "INFORME DE INGRESOS ";
                excel.Cells[2, 4] = "DESDE " + sFechaInicio + " A " + sFechaFin;

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;

                    if (iIndiceColumna == 1)
                        excel.Cells[1, iIndiceColumna].ColumnWidth = 0;

                    if (iIndiceColumna == 2 || iIndiceColumna == 6 || iIndiceColumna == 7 || iIndiceColumna == 12 || iIndiceColumna == 13)
                        excel.Cells[1, iIndiceColumna].ColumnWidth = 8;

                    if (iIndiceColumna == 3 || iIndiceColumna == 5 || iIndiceColumna == 9 || iIndiceColumna == 10 || iIndiceColumna == 11)
                        excel.Cells[1, iIndiceColumna].ColumnWidth = 10;


                    if (iIndiceColumna != 1)
                    {
                        excel.Cells[4, iIndiceColumna] = col.HeaderText;
                        excel.Cells[4, iIndiceColumna].Interior.Color = Color.Yellow;
                        excel.Cells[4, iIndiceColumna].BorderAround();
                    }
                }


                int iIndiceFila = 4;

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

                excel.get_Range("A4", "M4").BorderAround();
                excel.Visible = true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para obtener el id del pedido
        private int obteneriIdPedido(int iIdMovimientoBodega)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(id_pedido, 0) id_pedido" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_movimiento_bodega = " + iIdMovimientoBodega + Environment.NewLine;
                sSql += "and estado = 'A'";
                
                DataTable dtAyuda = new DataTable();
                dtAyuda.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return 0;
                }

                int iIdPedido = Convert.ToInt32(dtAyuda.Rows[0][0].ToString());

                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from cv403_numero_cab_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                DataTable dtQuery = new DataTable();
                dtQuery.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtQuery, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtQuery.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtQuery.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            dgvInforme.Rows.Clear();
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy/MM/dd");
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy/MM/dd");
        }

        #endregion
                
        private void frmReporteEgresoEnRangoDeFechas_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            llenarGrid();
            Cursor = Cursors.Default;
        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtHasta.Text = calendario.txtFecha.Text;
                sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiar();
            }
        }

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (dgvInforme.Rows.Count > 0)
                    exportarAExcel(dgvInforme);
                else
                {
                    ok.lblMensaje.Text = "No hay datos para mostrar.";
                    ok.ShowInTaskbar = false;
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
    }
}
