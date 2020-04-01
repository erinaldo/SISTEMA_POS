using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace Palatium.Informes
{
    public partial class frmInformeVentasCategorias : Form
    {
        //Variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        string texto;
        string sfechaInicio, sfechaFin;

        public frmInformeVentasCategorias()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtDesde.Text == "")
            {
                ok.LblMensaje.Text = "Seleccione una fecha de inicio.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else if (TxtHasta.Text == "")
            {
                ok.LblMensaje.Text = "Seleccione una fecha Fin.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }

            else
            {
                llenarGrid();
                if (dgvInforme.Rows.Count > 0)
                    llenartxt();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvInforme.Rows.Clear();
                double dbTotalVentas = 0;
                double dbTotalCantidad = 0;

                sSql = "select sum(DET.cantidad) CANTIDAD, sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) "+ 
                    "TOTAL, PRO.id_producto_padre "+
                    "from  cv403_det_pedidos DET inner join cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto "+
                    "inner join cv403_cab_pedidos CAB  on CAB.id_pedido = DET.id_pedido and CAB.estado = 'A' and DET.estado = 'A' inner join cv401_productos PRO "+
                    "on NOM.id_producto = PRO.id_producto  and PRO.estado = 'A' and NOM.estado = 'A' "+
                    "where CAB.fecha_pedido between   "+
                    " '" + sfechaInicio + "' and '" + sfechaFin + "' and DET.estado = 'A' and CAB.id_localidad = " + Program.iIdLocalidad + " " +
                    "and CAB.estado_orden = 'Pagada' "+ 
                    "group by  PRO.id_producto_padre "+
                      "order by sum(DET.cantidad)";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[0].ToString());
                            double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            int idProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                            string sNombreCategoria ="";
                            dbTotalCantidad += dbCantidad;
                            dbTotalVentas += dbTotal;
                            string sQuery = "select nombre from cv401_nombre_productos where id_producto = "+idProducto + " and estado = 'A'";
                            DataTable dtAyuda = new DataTable();
                            dtAyuda.Clear();
                            bool bEmergente = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                            if(bEmergente == true)
                                sNombreCategoria = dtAyuda.Rows[0].ItemArray[0].ToString();
                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema a cargar el nombre de la categoría";
                                ok.ShowInTaskbar = false;
                                ok.ShowDialog();
                            }

                            dgvInforme.Rows.Add(sNombreCategoria, dbCantidad.ToString("N0"), dbTotal.ToString("N2"));
                        }

                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("TOTALES",dbTotalCantidad.ToString("N0"), "$"+dbTotalVentas.ToString("N2"));
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccionado";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }

                    dgvInforme.SelectedRows[0].Selected = false;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInforme.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = "*.txt";
                    sfd.DefaultExt = "txt";
                    sfd.Filter = "txt files (*.txt)| *.txt";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Stream fileStream = sfd.OpenFile();
                        StreamWriter sw = new StreamWriter(fileStream);
                        crearArchivoPlano(sw);
                        sw.Close();
                        fileStream.Close();

                        ok.LblMensaje.Text = "Archivo Exportado Correctamente.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "No hay datos para mostrar.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el txt
        private void llenartxt()
        {
            texto = " ";
            texto += ("INFORME DE VENTAS POR CATEGORIA".PadLeft(60, ' ')) + "\r\n";
            texto += ("*".PadRight(100, '*')) + "\r\n";
            texto += "\r\n";
            texto += (" ".PadRight(10, ' ') + "CATEGORIAS".PadRight(50, ' ') + "CANTIDAD".PadLeft(20, ' ') + "TOTAL".PadLeft(20, ' ')) + "\r\n";
            texto += ("=".PadRight(100, '=')) + "\r\n";
            for (int i = 0; i < dgvInforme.Rows.Count; i++)
            {
                string sNombreProducto = "";
                string dbCantidad = "";
                string dbTotal = "";

                if (dgvInforme.Rows[i].Cells[0].Value != null || dgvInforme.Rows[i].Cells[0].Value != null)
                {
                    sNombreProducto = dgvInforme.Rows[i].Cells[0].Value.ToString();
                }

                if (dgvInforme.Rows[i].Cells[1].Value != null || dgvInforme.Rows[i].Cells[1].Value != null)
                {
                    dbCantidad = dgvInforme.Rows[i].Cells[1].Value.ToString();
                }

                if (dgvInforme.Rows[i].Cells[2].Value != null || dgvInforme.Rows[i].Cells[2].Value != null)
                {
                    dbTotal = dgvInforme.Rows[i].Cells[2].Value.ToString();
                }

                if (i == (dgvInforme.Rows.Count - 1))
                {
                    texto += ("----------".PadLeft(80, ' ') + "----------".PadLeft(20, ' ')) + "\r\n";
                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' ')) + "\r\n";
                }
                else
                {
                    if (i != 0)
                    texto += ("-".PadRight(100, '-')) + "\r\n";

                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' ')) + "\r\n";
                }


            }
            txtInforme.Text = texto;

        }

        //Función para crear un archivo plano
        private void crearArchivoPlano(StreamWriter sw)
        {
            sw.WriteLine("INFORME DE VENTAS POR CATEGORIA".PadLeft(60, ' '));
            sw.WriteLine();
            sw.WriteLine("*".PadRight(100, '*'));
            sw.WriteLine();
            sw.WriteLine(" ".PadRight(10, ' ') + "CATEGORIAS".PadRight(50, ' ') + "CANTIDAD".PadLeft(20, ' ') + "TOTAL".PadLeft(20, ' '));
            sw.WriteLine("=".PadRight(100, '='));
            

            for (int i = 0; i < dgvInforme.Rows.Count; i++)
            {
                string sNombreProducto = "";
                string dbCantidad = "";
                string dbTotal = "";

                if (dgvInforme.Rows[i].Cells[0].Value != null || dgvInforme.Rows[i].Cells[0].Value != null)
                {
                    sNombreProducto = dgvInforme.Rows[i].Cells[0].Value.ToString();
                }

                if (dgvInforme.Rows[i].Cells[1].Value != null || dgvInforme.Rows[i].Cells[1].Value != null)
                {
                    dbCantidad = dgvInforme.Rows[i].Cells[1].Value.ToString();
                }

                if (dgvInforme.Rows[i].Cells[2].Value != null || dgvInforme.Rows[i].Cells[2].Value != null)
                {
                    dbTotal = dgvInforme.Rows[i].Cells[2].Value.ToString();
                }

                if (i == (dgvInforme.Rows.Count - 1))
                {
                    sw.WriteLine("----------".PadLeft(80, ' ') + "----------".PadLeft(20, ' '));
                    sw.WriteLine(sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' '));

                    texto += ("----------".PadLeft(80, ' ') + "----------".PadLeft(20, ' ')) + "\r\n";
                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' '))+ "\r\n";
                }
                else
                {
                    if (i != 0)
                        sw.WriteLine("-".PadRight(100, '-'));
                    texto += ("-".PadRight(100, '-')) + "\r\n";
                    
                    sw.WriteLine(sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' '));
                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' ')) +"\r\n";
                }


            }

        }

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            if (dgvInforme.Rows.Count > 0)
                exportarAExcel(dgvInforme);
            else
            {
                ok.LblMensaje.Text = "No hay datos para mostrar.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int iIndiceColumna = 0;

            excel.Columns.ColumnWidth = 20;
            excel.Cells[1, 1] = "INFORME DE VENTAS POR CATEGORIAS";
            excel.Cells[2, 1] = "DESDE " + sfechaInicio + " A " + sfechaFin;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;
                if (iIndiceColumna == 1)
                    excel.Cells[1, iIndiceColumna].ColumnWidth = 40;

                excel.Cells[5, iIndiceColumna] = col.HeaderText;
                excel.Cells[5, iIndiceColumna].Interior.Color = Color.Yellow;
            }

            int iIndiceFila = 5;

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

            excel.get_Range("A5", "C5").BorderAround();
            excel.Visible = true;

        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sfechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(TxtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                TxtHasta.Text = calendario.txtFecha.Text;
                sfechaFin = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }

        private void frmInformeVentasCategorias_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sfechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sfechaFin = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvInforme.Rows.Count > 0)
            {
                string sPath = @"C:\reportes\Categorias.txt";

                if (!File.Exists(sPath))
                {
                    StreamWriter sw = File.CreateText(sPath);
                    crearArchivoPlano(sw);
                }

                File.WriteAllText(sPath, txtInforme.Text);
            }
            else
            {
                ok.LblMensaje.Text = "No hay datos para mostrar.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }            
        }


        //Fin  de la clase
    }
}
