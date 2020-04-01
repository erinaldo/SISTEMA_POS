using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Palatium.Informes
{
    public partial class frmPlantillaExcel : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sRutaPlantilla;

        DataTable dtConsulta;

        bool bRespuesta;

        int iFila;

        public frmPlantillaExcel()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO
        
        //FUNCION PARA LLENAR EL COMBO
        private void llenarComboPadre()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre " + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbProductoPadre.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error en la instruccion del combo");
            }
        }

        //LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, P.codigo, NP.nombre " + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 3" + Environment.NewLine;
                sSql += "and P.id_producto_padre = " + Convert.ToInt32(cmbProductoPadre.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                }

                else
                {
                    MessageBox.Show("Error en la instruccion del grid");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error en la instruccion del grid");
            }
        }

        
        //FUNCION PARA LLENAR EL EXCEL
        private void crearExcel()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook libro;
                Microsoft.Office.Interop.Excel.Worksheet hoja;

                libro = excel.Workbooks.Open(sRutaPlantilla, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                hoja = (Microsoft.Office.Interop.Excel.Worksheet)libro.ActiveSheet;

                hoja.Cells[1, 2] = cmbProductoPadre.SelectedValue;
                hoja.Cells[2, 2] = cmbProductoPadre.Text.Trim();

                iFila = 5;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    hoja.Cells[iFila, 1] = dgvDatos.Rows[i].Cells[0].Value.ToString();
                    hoja.Cells[iFila, 2] = dgvDatos.Rows[i].Cells[1].Value.ToString();
                    hoja.Cells[iFila, 3] = dgvDatos.Rows[i].Cells[2].Value.ToString();
                    iFila++;
                }

                excel.Visible = true;
                excel.UserControl = true;
                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                excel = null;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void frmPlantillaExcel_Load(object sender, EventArgs e)
        {
            llenarComboPadre();
            sRutaPlantilla = Program.sUrlReportes.Trim() + @"\plantilla_prueba.xlsx";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            

            if (Program.sUrlReportes == "")
            {
                MessageBox.Show("No se encuentra configurado la ubicación para los reportes");
                return;
            }

            if (!File.Exists(sRutaPlantilla))
            {
                MessageBox.Show("No se encuentra el archivo plantilla_prueba para generar el informe.");
                return;
            }

            if (dgvDatos.Rows.Count > 0)
            {
                crearExcel();
            }

            else
            {
                MessageBox.Show("No hay registros a exportar.");
            }
        }
    }
}
