using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Palatium.Informes
{
    public partial class frmInformeProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta = false;
        DataTable dtConsulta;
        string sfechaInicio, sfechaFin;
        string texto;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();


        //Constructor de la clase
        public frmInformeProductos()
        {
            InitializeComponent();
        }

        //Botón aceptar
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtDesde.Text == "")
            {
                ok.LblMensaje.Text = "Seleccione un fecha de inicio.";
                ok.ShowDialog();
            }
            else if (TxtHasta.Text == "")
            {
                ok.LblMensaje.Text = "Seleccione un fecha Fin.";
                ok.ShowDialog();
            }

            else
            {
                llenarGrid();

                if (dgvInforme.Rows.Count > 0)
                    cargarTxt();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvInforme.Rows.Clear();
                //string sFechaInicio = Convert.ToDateTime(txtDesde.Text).ToString("yyyy-MM-dd");
                //string sFechaFin = Convert.ToDateTime(TxtHasta.Text).ToString("yyyy-MM-dd");


                sSql = "select NOM.nombre, sum(DET.cantidad) CANTIDAD, sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL " +
                        "from  cv403_det_pedidos DET inner join cv401_nombre_productos NOM " +
                        "on DET.id_producto = NOM.id_producto and NOM.estado = 'A' and DET.estado = 'A' inner join cv403_cab_pedidos CAB  " +
                        "on CAB.id_pedido = DET.id_pedido and CAB.estado = 'A'" +
                        "where CAB.fecha_pedido between   '" + sfechaInicio + "' and '" + sfechaFin + "' " +
                        "and DET.estado = 'A' and CAB.id_localidad = " + Program.iIdLocalidad + 
                        "and CAB.estado_orden = 'Pagada' " +
                        "group by NOM.nombre order by sum(DET.cantidad)";


                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        
                        double dbCantidadTotal = 0;
                        double dbTotalFinal = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreProducto = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dbCantidadTotal += dbCantidad;
                            dbTotalFinal += dbTotal;

                            dgvInforme.Rows.Add(sNombreProducto, dbCantidad.ToString("N0"),dbTotal.ToString("N2"));
                        }

                        //Añade una fila en blanco
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("TOTALES", dbCantidadTotal.ToString("N0"), "$" + dbTotalFinal.ToString("N2"));
                        
                        dgvInforme.SelectedRows[0].Selected = false;
                        
                    }
                    else
                    {
                      
                        ok.LblMensaje.Text = "No hay productos registrados en el rango de fechas";
                        ok.ShowDialog();
                    }

                    goto fin;
                }
                else
                    goto reversa;

            }
            catch(Exception ex)
            {
                dgvInforme.Rows.Clear();
                goto reversa;
            }

            #region Funciones de Ayuda
        reversa:
            {
                VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Error al cargar el Grid. Por Favor Póngase en contacto con el Administrador";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
       fin:
            {

            }
            #endregion
        }

        private void btnExportarTexto_Load(object sender, EventArgs e)
        {
          
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sfechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sfechaFin = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
           // dgvInforme.SelectedRows[0].Selected = false;
            
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
                    ok.LblMensaje.Text = "No hay datos para mostrar";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowInTaskbar = false;
                ok.ShowDialog();

            }
        }

        //Función para cargar el txt
        private void cargarTxt()
        {
            texto = " ";
            texto += ("INFORME DE VENTAS POR PRODUCTO".PadLeft(60, ' '));
            texto += "\r\n";
            texto += ("*".PadRight(100, '*')) + "\r\n";
            texto += (" ".PadRight(10, ' ') + "PRODUCTOS".PadRight(50, ' ') + "CANTIDAD".PadLeft(20, ' ') + "TOTAL".PadLeft(20, ' ')) + "\r\n";
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
            
            sw.WriteLine("INFORME DE VENTAS POR PRODUCTO".PadLeft(60,' '));
            sw.WriteLine();
            sw.WriteLine("*".PadRight(100, '*'));
            sw.WriteLine();
            sw.WriteLine(" ".PadRight(10,' ')+ "PRODUCTOS".PadRight(50,' ')+"CANTIDAD".PadLeft(20,' ')+"TOTAL".PadLeft(20,' '));
            sw.WriteLine("=".PadRight(100, '='));
            
            for (int i = 0; i < dgvInforme.Rows.Count; i++)
            {
                string sNombreProducto="";
                string dbCantidad="";
                string dbTotal="";

                if (dgvInforme.Rows[i].Cells[0].Value !=null  || dgvInforme.Rows[i].Cells[0].Value !=null)
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
                    texto += ("----------".PadLeft(80, ' ') + "----------".PadLeft(20, ' ')) +"\r\n";
                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' ')) + "\r\n";
                }
                else
                {
                    if (i != 0)
                        sw.WriteLine("-".PadRight(100, '-'));
                    texto += ("-".PadRight(100, '-')) + "\r\n";
                    sw.WriteLine(sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' '));
                    texto += (sNombreProducto.PadRight(60, ' ') + dbCantidad.PadLeft(20, ' ') + dbTotal.PadLeft(20, ' ')) + "\r\n";
                }
                    
                  
            }

            

        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int iIndiceColumna = 0;

            excel.Columns.ColumnWidth = 20;
            excel.Cells[1, 1] = "INFORME DE VENTAS POR PRODUCTOS";
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

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInforme.Rows.Count > 0)
                    exportarAExcel(dgvInforme);
                else
                {
                    ok.LblMensaje.Text = "No hay datos para mostrar";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvInforme.Rows.Count > 0)
            {
                string sPath = @"C:\reportes\Productos.txt";

                if (!File.Exists(sPath))
                {
                    StreamWriter sw = File.CreateText(sPath);
                    crearArchivoPlano(sw);
                }

                File.WriteAllText(sPath, txtInforme.Text);
            }
            else
            {
                ok.LblMensaje.Text = "No hay datos para mostrar";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            
            
        }

       //Fin de la clase
    }
}
