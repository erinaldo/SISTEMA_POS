using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FProductosUltimoNivel : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        
        string T_st_sql3 = "";
        string T_st_sql4 = "";
        bool x = false; //creamos la variable

        public FProductosUltimoNivel()
        {
            InitializeComponent();
        }

        private void limpiar()
        {
            txtCodigoProducto.Text = "";
        }

        //FUNCION PARA LLENAR EL GRID 
        private void llenarGrid(string[] t_st_datos)
        {
            T_st_sql3 = "";

            try
            {
                string t_st_query = "";

                if (t_st_datos[0] == "1")
                {
                    t_st_query = "SELECT PRD.CODIGO,NOM.NOMBRE,TPC.Valor_Texto,(SELECT CODIGO FROM cv401_productos WHERE ID_PRODUCTO = PRD.ID_PRODUCTO_PADRE) Prod_Padre From cv401_productos PRD,cv401_unidades_productos UND,cv401_nombre_productos NOM,tp_codigos TPC Where PRD.ID_PRODUCTO_PADRE = UND.ID_PRODUCTO AND PRD.ID_PRODUCTO = NOM.ID_PRODUCTO AND UND.CG_UNIDAD = TPC.CORRELATIVO  and UND.UNIDAD_COMPRA = 1 AND NOM.ESTADO ='A'  AND PRD.ESTADO='A'  AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL = 1 AND PRD.CODIGO LIKE '%' order by NOM.NOMBRE ";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProducUltimoNivel.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProducUltimoNivel.Refresh();
                    dgvProducUltimoNivel.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //FUNCION PARA LLENAR EL GRID cuando se haya escrito un codigo
        private void llenarGridConCodigo(string[] t_st_datos2)
        {
            try
            {
                string t_st_query2 = "";

                if (t_st_datos2[0] == "1")
                {
                    t_st_query2 = "SELECT PRD.CODIGO Codigo,NOM.NOMBRE,TPC.Valor_Texto,(SELECT CODIGO FROM cv401_productos WHERE ID_PRODUCTO = PRD.ID_PRODUCTO_PADRE) Prod_Padre From cv401_productos PRD,cv401_unidades_productos UND,cv401_nombre_productos NOM,tp_codigos TPC Where PRD.ID_PRODUCTO_PADRE = UND.ID_PRODUCTO AND PRD.ID_PRODUCTO = NOM.ID_PRODUCTO AND UND.CG_UNIDAD = TPC.CORRELATIVO  and UND.UNIDAD_COMPRA = 1 AND NOM.ESTADO ='A'  AND PRD.ESTADO='A'  AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL = 1 AND PRD.CODIGO LIKE '" + txtCodigoProducto.Text.ToString().Trim() + "%' order by NOM.NOMBRE";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query2, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProducUltimoNivel.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProducUltimoNivel.Refresh();
                    dgvProducUltimoNivel.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnOkProducUltimoNivel_Click(object sender, EventArgs e)
        {
            if (txtCodigoProducto.Text == "")
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
            else
            {
                string[] t_st_datos2 = { "1", "adsdasdasd" };
                llenarGridConCodigo(t_st_datos2);
            }
        }

        private void btnLimpiarProducUltimoNivel_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnCerrarProducUltimoNivel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcelProducUltimoNivel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProducUltimoNivel.Rows.Count > 0)
                    exportarAExcel(dgvProducUltimoNivel);
                else
                {
                    MessageBox.Show("No hay datos para ser exportados ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al exportar los datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int iIndiceColumna = 0;

            excel.Columns.ColumnWidth = 20;
            excel.Cells[1, 1] = "PRODUCTOS";

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;
                
                excel.Cells[3, iIndiceColumna] = col.HeaderText;
                excel.Cells[3, iIndiceColumna].Interior.Color = Color.Yellow;

            }

            int iIndiceFila = 3;

            foreach (DataGridViewRow row in dgvCierre.Rows)
            {
                iIndiceFila++;

                iIndiceColumna = 0;

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    excel.Cells[iIndiceFila + 1, iIndiceColumna] = row.Cells[col.Name].Value;
                    excel.Cells[iIndiceFila + 1, iIndiceColumna].BorderAround();
                }

            }

            excel.get_Range("A3", "E3").BorderAround();
            excel.Visible = true;

        }

        private void FProductosUltimoNivel_Load(object sender, EventArgs e)
        {

        }


    }
}
