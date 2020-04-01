using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Palatium.Informes
{
    public partial class frmReporteVentasPorClientes : Form
    {
        string sFechaDesde;
        string sFechaHasta;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql, texto ="";
        bool bRespuesta;
        System.Data.DataTable dtConsulta;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public frmReporteVentasPorClientes()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInforme.Rows.Count > 0)
                    exportarAExcel(dgvInforme);
                else
                {
                    ok.LblMensaje.Text = "No hay datos para ser exportados en el rango de fechas selecionadas";
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtInforme.Text == "")
            {
                ok.LblMensaje.Text = "No hay datos para imprimir.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }

            else
            {
                string sPath = @"C:\reportes\Clientes.txt";

                if (!File.Exists(sPath))
                {
                    StreamWriter sw = File.CreateText(sPath);
                }

                File.WriteAllText(sPath, txtInforme.Text);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtDesde.Text != " " || TxtHasta.Text != " ")
                llenarTextbox(0);
        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int iIndiceColumna = 0;

            excel.DisplayFullScreen = true; 
            excel.Columns.ColumnWidth = 20;
            excel.Cells[1, 1] = "INFORME DE VENTAS POR CLIENTES";
            excel.Cells[2, 1] = "DESDE "+sFechaDesde+" A "+sFechaHasta;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;
                if (iIndiceColumna == 1)
                    excel.Cells[1, iIndiceColumna].ColumnWidth = 40;

                excel.Cells[5, iIndiceColumna] = col.HeaderText;
                excel.Cells[5, iIndiceColumna].Interior.Color = Color.Yellow;
                
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
                    excel.Cells[iIndiceFila + 1, iIndiceColumna].BorderAround();
                }

            }


            excel.get_Range("A5", "E5").BorderAround();
            excel.Visible = true;

        }

        //Función para llenar el textBox
        private void llenarTextbox(int iBandera)
        {
            try
            {
                texto = "";
                txtInforme.Text = texto;
                double dbTotal = 0;
                double dbTotalCantidad = 0;
                dgvInforme.Rows.Clear();

                if (iBandera == 0)
                {
                    sSql = "select rtrim(P.apellidos) + ' '+ rtrim(P.nombres) Nombres , F.id_persona, sum(F.valor) 'Total de Consumos', " +
                       "count(F.id_persona) 'Numero De Consumos' , isnull(F.telefono_factura,' ') Telefono  " +
                        "from  cv403_facturas F inner join tp_personas P " +
                        "on F.id_persona = P.id_persona " +
                        "where F.fecha_factura between '" + sFechaDesde + "' and '" + sFechaHasta + "' " +
                        "group by F.id_persona, F.telefono_factura, P.nombres, P.apellidos";
                }
                else
                {
                    sSql = "select rtrim(P.apellidos) + ' '+ rtrim(P.nombres) Nombres , F.id_persona, sum(F.valor) 'Total de Consumos', " +
                           "count(F.id_persona) 'Numero De Consumos' , isnull(F.telefono_factura,' ') Telefono  " +
                            "from  cv403_facturas F inner join tp_personas P " +
                            "on F.id_persona = P.id_persona " +
                            "where F.fecha_factura between '" + sFechaDesde + "' and '" + sFechaHasta + "' " +
                            " and P.apellidos like '%"+txtBusqueda.Text+"%'  "+
                            "group by F.id_persona, F.telefono_factura, P.nombres, P.apellidos";
                }


                dtConsulta = new System.Data.DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        texto += "\r\n";
                        texto += "REPORTE DE VENTAS POR CLIENTE ".PadLeft(78, ' ') + "\r\n";
                        texto += ("DESDE " + sFechaDesde + " A " + sFechaHasta).PadLeft(77, ' ') + "\r\n";
                        texto += "\r\n";
                        texto += "NOMBRES".PadRight(35, ' ') + "|# DE TELEFONO".PadLeft(19, ' ') +
                                    "   |TOTAL DE CONSUMO".PadLeft(17, ' ') + "  |CANT. CONSUMO".PadLeft(19, ' ') + "  |PROMEDIO DE CONSUMO|".PadLeft(20, ' ') + "\r\n";
                        texto += "-".PadRight(115,'-') +"\r\n";
                        dgvInforme.Rows.Add();

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombre = dtConsulta.Rows[i].ItemArray[0].ToString();
                            string sTelefono = dtConsulta.Rows[i].ItemArray[4].ToString();
                            double dbTotalConsumos = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            double dbCantidadConsumo = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString()); 
                            double dbPromedioConsumo = dbTotalConsumos/dbCantidadConsumo;
                            dbTotal += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dbTotalCantidad += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());

                            if (sNombre.Length > 35)
                            {
                                sNombre = sNombre.Substring(0, 35);
                            }
                            texto += sNombre.PadRight(35, ' ') + sTelefono.PadLeft(19, ' ') + dbTotalConsumos.ToString("N2").PadLeft(17, ' ') +
                                dbCantidadConsumo.ToString("N2").PadLeft(19, ' ') + dbPromedioConsumo.ToString("N2").PadLeft(20) + "\r\n";
                            dgvInforme.Rows.Add(sNombre, sTelefono, dbTotalConsumos.ToString("N2"), dbCantidadConsumo.ToString("N2"), dbPromedioConsumo.ToString("N2"));
                        }

                        texto += "\r\n";
                        texto += "\r\n";
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add();
                        texto += "-".PadRight(115, '-') + "\r\n";
                        texto += "TOTALES".PadRight(54, ' ') +( "$"+dbTotal.ToString("N2")).PadLeft(17,' ') + dbTotalCantidad.ToString("N2").PadLeft(19,' ');
                        dgvInforme.Rows.Add("TOTALES","","$"+dbTotal,"$"+dbTotalCantidad);
                        txtInforme.Text = texto;

                    }
                    else
                    {
                        if (iBandera == 0)
                        {
                            ok.LblMensaje.Text = "No hay datos para ser mostrados en el rango de fechas selecionadas";
                            ok.ShowInTaskbar = false;
                            ok.ShowDialog();
                        }
                        
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un porblema al conectarse a la base de datos";
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

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
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
                sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }

        private void frmReporteVentasPorClientes_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDesde.Text != " " || TxtHasta.Text != " ")
            {
                if (txtBusqueda.Text != "")
                    llenarTextbox(1);
                else
                    llenarTextbox(0);
            }   
        }


    }
}
