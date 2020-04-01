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
    public partial class frmInformeDeOrdenesFormaPago : Form
    {
        //Variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta = false;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        DataTable dtConsulta;
        string sFechaInicio, sFechaFin;
        string sTexto;

        public frmInformeDeOrdenesFormaPago()
        {
            InitializeComponent();
        }

        private void frmInformeDeOrdenesFormaPago_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
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
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtHasta.Text = calendario.txtFecha.Text;
                sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            llenarGrid();
            if (dgvInforme.Rows.Count > 0)
                llenarTxt();
        }

        //Función para llenar el txt
        private void llenarTxt()
        {
            try
            {
                double dbTotalOrdenes = 0;
                sTexto = "";

                sSql = "select POS.fecha_ingreso, POS.numero_pedido, POS.apellidos + ' '+ POS.nombres , " +
                        "sum(DET.cantidad * (DET.Precio_Unitario + DET.Valor_iva + DET.valor_otro - DET.Valor_Dscto)) Total, " +
                        "CAB.estado_orden, Pos.id_pedido, Pos.descripcion_origen_orden " +
                        "from pos_vw_factura POS inner join cv403_det_pedidos DET on " +
                        "POS.id_det_pedido = DET.id_det_pedido inner join cv403_cab_pedidos CAB on " +
                        "Pos.id_pedido = CAb.id_pedido " +
                        "where POS.fecha_factura between '" + sFechaInicio + "' and '" + sFechaFin + "' and Cab.estado = 'A' " +
                        "group by POS.fecha_ingreso, POS.numero_pedido, POS.apellidos + ' '+ POS.nombres, CAB.estado_orden, " +
                        "Pos.id_pedido, Pos.descripcion_origen_orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += ("REPORTE DE PEDIDOS ENTRE " + sFechaInicio + " HASTA " + sFechaFin + "").PadLeft(85, ' ') + "\r\n";
                        sTexto += "\r\n\r\n";
                        sTexto += "NOMBRE DEL CLIENTE".PadRight(40, ' ') + "NUMERO DE ORDEN".PadRight(16, ' ') + "FECHA DE ORDEN".PadLeft(16, ' ') +
                                    "TOTAL".PadLeft(20, ' ') + "TIPO DE ORDEN".PadLeft(16, ' ') +"\r\n";
                        sTexto += "=".PadRight(108, '=') + "\r\n";

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreCliente = dtConsulta.Rows[i].ItemArray[2].ToString();
                            string sNumeroPedido = dtConsulta.Rows[i].ItemArray[1].ToString();
                            string sFechaPedido = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbTotalPedido = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                            int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[5].ToString());
                            string sTipoOrden = dtConsulta.Rows[i].ItemArray[6].ToString();
                            dbTotalOrdenes += dbTotalPedido;

                            //Agrego los datos al reporte
                            sTexto += sNombreCliente.PadRight(40, ' ') + sNumeroPedido.PadLeft(16, ' ') + sFechaPedido.PadLeft(25, ' ') +
                                      dbTotalPedido.ToString("N2").PadLeft(10, ' ') + " ".PadRight(5,' ')+sTipoOrden.PadRight(11, ' ') + "\r\n";


                            string sQuery;
                            sQuery = "";
                            sQuery += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta," + Environment.NewLine;
                            sQuery += "codigo from pos_vw_pedido_forma_pago" + Environment.NewLine;
                            sQuery += "where id_pedido = '" + iIdPedido + "'" + Environment.NewLine;
                            sQuery += "group by descripcion, valor, codigo, cambio" + Environment.NewLine;
                            sQuery += "having count(*) >= 1";


                            DataTable dtPagos = new DataTable();
                            dtPagos.Clear();
                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPagos, sQuery);
                            if (bRespuesta == true)
                            {
                                if (dtPagos.Rows.Count > 0)
                                {
                                    sTexto += "\r\n";
                                    sTexto += "FORMAS DE PAGO".PadLeft(72, ' ')+"\r\n";

                                    for (int j = 0; j < dtPagos.Rows.Count; j++)
                                    {
                                        string sDescripcion = dtPagos.Rows[j].ItemArray[0].ToString();
                                        double dbTotalPago = Convert.ToDouble(dtPagos.Rows[j].ItemArray[1].ToString());
                                        double dbCambio = Convert.ToDouble(dtPagos.Rows[j].ItemArray[2].ToString());
                                        string sCodigo = dtPagos.Rows[j].ItemArray[4].ToString();

                                        sTexto += "".PadRight(10, ' ') + sDescripcion.PadRight(30, ' ') + ("$" + (dbTotalPago+dbCambio).ToString("N2")).PadRight(16,' ')
                                                    + "".PadRight(25, ' ') + "CAMBIO".PadLeft(10, ' ') + ("$" + dbCambio.ToString("N2")).PadLeft(11,' ') +"\r\n";

                                    }
                                    //Incremento un salto de línea
                                    sTexto += "-".PadRight(108, '-')+"\r\n";
                                }
                            }
                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema al cargar las formas de pago";
                                ok.ShowDialog();
                            }

                        }

                        sTexto += "\r\n\r\n\r\n";
                        sTexto += "TOTAL DE PAGO DE ORDENES".PadRight(36,' ')+("$"+dbTotalOrdenes.ToString("N2")).PadLeft(10,' ');
                        sTexto += "\r\n\r\n\r\n";
                        txtInforme.Text = sTexto;

                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccionadas";
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el Reporte";
                    ok.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                double dbTotalOrdenes = 0;

                dgvInforme.Rows.Clear();
                sSql = "";
                sSql += "select POS.fecha_ingreso, POS.numero_pedido, POS.apellidos + ' '+ POS.nombres , " + Environment.NewLine;
                sSql += "sum(DET.cantidad * (DET.Precio_Unitario + DET.Valor_iva + DET.valor_otro - DET.Valor_Dscto)) Total," + Environment.NewLine;
                sSql += "CAB.estado_orden, Pos.id_pedido, Pos.descripcion_origen_orden " + Environment.NewLine;
                sSql += "from pos_vw_factura POS inner join" + Environment.NewLine;
                sSql += "cv403_det_pedidos DET on POS.id_det_pedido = DET.id_det_pedido inner join" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CAB on Pos.id_pedido = CAb.id_pedido " + Environment.NewLine;
                sSql += "where POS.fecha_factura between '" + sFechaInicio + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFin + "'" + Environment.NewLine;
                sSql += "and Cab.estado = 'A'" + Environment.NewLine;
                sSql += "group by POS.fecha_ingreso, POS.numero_pedido, POS.apellidos + ' '+ POS.nombres, CAB.estado_orden," + Environment.NewLine;
                sSql += "Pos.id_pedido, Pos.descripcion_origen_orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreCliente = dtConsulta.Rows[i].ItemArray[2].ToString();
                            string sNumeroPedido = dtConsulta.Rows[i].ItemArray[1].ToString();
                            string sFechaPedido = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbTotalPedido = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                            int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[5].ToString());
                            string sTipoOrden = dtConsulta.Rows[i].ItemArray[6].ToString();
                            dbTotalOrdenes += dbTotalPedido;
                            //Agrego los datos al reporte
                            dgvInforme.Rows.Add( sNombreCliente,
                                                 sNumeroPedido,
                                                 sFechaPedido,
                                                 "$"+dbTotalPedido.ToString("N2"),
                                                 sTipoOrden
                                                );

                            string sQuery;
                            sQuery = ""; 
                            sQuery += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, codigo" + Environment.NewLine;
                            sQuery += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                            sQuery += "where id_pedido = '" + iIdPedido + "'" + Environment.NewLine;
                            sQuery += "group by descripcion, valor, codigo, cambio having count(*) >= 1";

                            
                            DataTable dtPagos = new DataTable();
                            dtPagos.Clear();
                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPagos, sQuery);
                            if (bRespuesta == true)
                            {
                                if (dtPagos.Rows.Count > 0)
                                {
                                    dgvInforme.Rows.Add("","","".PadRight(8,' ')+"FORMAS DE PAGO","","");

                                    for (int j = 0; j < dtPagos.Rows.Count; j++)
                                    {
                                        string sDescripcion = dtPagos.Rows[j].ItemArray[0].ToString();
                                        double dbTotalPago = Convert.ToDouble(dtPagos.Rows[j].ItemArray[1].ToString());
                                        double dbCambio = Convert.ToDouble(dtPagos.Rows[j].ItemArray[2].ToString());
                                        string sCodigo = dtPagos.Rows[j].ItemArray[4].ToString();

                                        dgvInforme.Rows.Add("".PadRight(10,' ')+ sDescripcion,
                                                            "$"+(dbTotalPago+dbCambio).ToString("N2"),
                                                            "",
                                                            "CAMBIO",
                                                            "$"+dbCambio.ToString("N2")
                                                            );
                                        
                                    }
                                    //Incremento un salto de línea
                                    dgvInforme.Rows.Add();
                                    
                                }
                            }
                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema al cargar las formas de pago";
                                ok.ShowDialog();
                            }

                        }
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("TOTAL DE PAGO DE ORDENES", "$"+dbTotalOrdenes.ToString("N2"));
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccionadas";
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
                    ok.ShowDialog();
                }

    
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
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
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
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
            excel.Cells[1, 1] = "INFORME DE VENTAS ";
            excel.Cells[2, 1] = "DESDE " + sFechaInicio + " A " + sFechaFin;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;
                if (iIndiceColumna == 1)
                    excel.Cells[4, iIndiceColumna].ColumnWidth = 40;

                excel.Cells[4, iIndiceColumna] = col.HeaderText;
                excel.Cells[4, iIndiceColumna].Interior.Color = Color.Yellow;
                excel.Cells[4, iIndiceColumna].BorderAround();
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

            excel.get_Range("A4", "E4").BorderAround();

            excel.Visible = true;

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInforme.Rows.Count > 0)
                {
                    string sPath = @"C:\reportes\OrigenOrden1.txt";

                    if (!File.Exists(sPath))
                    {
                        StreamWriter sw = File.CreateText(sPath);
                    }

                    File.WriteAllText(sPath, txtInforme.Text);
                }
                else
                {
                    ok.LblMensaje.Text = "No hay datos para ser impresos";
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



        //Fin de la clase
    }
}
