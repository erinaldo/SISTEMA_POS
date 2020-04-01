using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Palatium.Oficina
{
    public partial class frmReporteEgresosFechas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        Decimal dbSuma;

        public frmReporteEgresosFechas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE OFICINA LOCAL
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select  LOC.id_localidad,  BO.descripcion + ' ' + loc.establecimiento +" + Environment.NewLine;
                sSql += "case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.tipo = '2'" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "order by BO.descripcion + ' ' + loc.establecimiento + case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbLocalidad.llenar(dtConsulta, sSql);

                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {
            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        } 

        //FUNCION DE EXPORTACION DE EXCEL
        public bool exportarExcel(DataGridView dgView)
        {

            try
            {
                string sValorGrid;
                string sFont = "Arial";
                int iSize = 11;
                //CREACIÓN DE LOS OBJETOS DE EXCEL
                Excel.Application xlsApp = new Excel.Application();
                Excel.Worksheet xlsSheet;
                Excel.Workbook xlsBook;
                //AGREGAMOS EL LIBRO Y HOJA DE EXCEL
                xlsBook = xlsApp.Workbooks.Add(true);
                xlsSheet = (Excel.Worksheet)xlsBook.ActiveSheet;
                //ESPECIFICAMOS EL TIPO DE LETRA Y TAMAÑO DE LA LETRA DEL LIBRO
                xlsSheet.Rows.Cells.Font.Size = iSize;
                xlsSheet.Rows.Cells.Font.Name = sFont;
                //AGREGAMOS LOS ENCABEZADOS
                int iFil = 0, iCol = 0;
                foreach (DataGridViewColumn column in dgView.Columns)
                    if (column.Visible)
                        xlsSheet.Cells[1, ++iCol] = column.HeaderText;
                //MARCAMOS LAS CELDAS DEL ENCABEZADO EN NEGRITA Y EN COLOR DE RELLENO GRIS
                xlsSheet.get_Range((object)xlsSheet.Cells[1, 1], (object)xlsSheet.Cells[1, dgView.ColumnCount]).Font.Bold = true;
                xlsSheet.get_Range((object)xlsSheet.Cells[1, 1], (object)xlsSheet.Cells[1, dgView.ColumnCount]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                Excel.Range r;
                Color c;
                for (iFil = 0; iFil < dgView.RowCount; iFil++)
                {
                    for (iCol = 0; iCol < dgView.ColumnCount; iCol++)
                    {
                        sValorGrid = dgView.Rows[iFil].Cells[iCol].Value.ToString();

                        if ((esNumero(sValorGrid) == true) && (sValorGrid.Length >= 9))
                        {
                            xlsSheet.Cells[iFil + 2, iCol + 1] = "'" + sValorGrid;
                        }

                        else
                        {
                            xlsSheet.Cells[iFil + 2, iCol + 1] = sValorGrid;
                        }

                        c = dgView.Rows[iFil].Cells[iCol].Style.BackColor;
                        if (!c.IsEmpty)
                        {// COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                            r = (Excel.Range)(object)xlsSheet.Cells[iFil + 2, iCol + 1];
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[iFil].Cells[iCol].Style.BackColor);
                        }
                    }
                }
                xlsSheet.Columns.AutoFit();
                xlsSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                xlsSheet.PageSetup.Zoom = 80;

                Excel.Range rango = xlsSheet.get_Range((object)xlsSheet.Cells[1, 1], (object)xlsSheet.Cells[dgView.RowCount + 1, dgView.ColumnCount]);
                rango.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                rango.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                xlsApp.Visible = true;

                return true;
            }

            catch (Exception ex)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "ERROR:" + Environment.NewLine + "No se pudo exportar los registros a Excel. Comuníquese con el administrador.";
                ok.ShowDialog();
                return false;
            }
        }

        private void llenarGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_reporte_egresos_contabilidad" + Environment.NewLine;
                sSql += "where fecha between '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and concepto like '%" + txtBuscar.Text.Trim() + "&'" + Environment.NewLine;
                }

                if (Convert.ToInt32(cmbLocalidad.SelectedValue) != 0)
                {
                    sSql += "and id_localidad = " + cmbLocalidad.SelectedValue + Environment.NewLine;
                }

                sSql += "order by numero_movimiento_caja";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    txtTotal.Text = "0.00";
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con las parámetros ingresados.";
                    ok.ShowDialog();
                    txtTotal.Text = "0.00";
                    this.Cursor = Cursors.Default;
                    return;
                }

                dbSuma = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(Convert.ToDateTime(dtConsulta.Rows[i]["fecha"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["numero_movimiento_caja"].ToString(),
                                      dtConsulta.Rows[i]["concepto"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["valor"].ToString());

                    dbSuma += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtTotal.Text = dbSuma.ToString("N2");
                dgvDatos.ClearSelection();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboLocalidades();
            txtDesde.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtHasta.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtBuscar.Clear();
            txtTotal.Text = "0.00";
            dgvDatos.Rows.Clear();
        }

        #endregion

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No existen registros para realizar la exportación de datos.";
                ok.ShowDialog();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (exportarExcel(dgvDatos) == false)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            this.Cursor = Cursors.Default;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtDesde.Text.Trim()) > Convert.ToDateTime(txtHasta.Text.Trim()))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha inicial no puede ser superior a la fecha final de búsqueda.";
                ok.ShowDialog();
                return;
            }

            llenarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmReporteEgresosFechas_Load(object sender, EventArgs e)
        {
            llenarComboLocalidades();
        }
    }
}
