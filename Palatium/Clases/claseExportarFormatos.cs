using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace Palatium.Clases
{
    class claseExportarFormatos
    {
        //FUNCION DE EXPORTACION DE EXCEL
        public bool exportarExcel(DataGridView dgView)
        {

            try
            {
                //if (pBar != null)
                //{
                //    pBar.Maximum = dgView.RowCount;
                //    pBar.Value = 0;
                //    if (!pBar.Visible) pBar.Visible = true;
                //}
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

                        //else if ((esNumero(sValorGrid) == true) && (sValorGrid.Length <= 3) && (sValorGrid.Length >= 1))
                        //{
                        //    xlsSheet.Cells[iFil + 2, iCol + 1] = "'" + sValorGrid;
                        //}

                        else
                        {
                            xlsSheet.Cells[iFil + 2, iCol + 1] = sValorGrid;
                        }

                        //xlsSheet.Cells[iFil + 2, iCol + 1] = dgView.Rows[iFil].Cells[iCol].Value.ToString();
                        c = dgView.Rows[iFil].Cells[iCol].Style.BackColor;
                        if (!c.IsEmpty)
                        {// COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                            r = (Excel.Range)(object)xlsSheet.Cells[iFil + 2, iCol + 1];
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[iFil].Cells[iCol].Style.BackColor);
                        }
                    }
                    //pBar.Value += 1;
                }
                xlsSheet.Columns.AutoFit();
                xlsSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                //xlsSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;
                xlsSheet.PageSetup.Zoom = 80;

                Excel.Range rango = xlsSheet.get_Range((object)xlsSheet.Cells[1, 1], (object)xlsSheet.Cells[dgView.RowCount + 1, dgView.ColumnCount]);
                rango.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                rango.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                //rango.Cells.AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatList1, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                xlsApp.Visible = true;

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        private bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }  
    }
}
