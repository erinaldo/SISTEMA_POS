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
    public partial class frmInformeVentasPorOrigen : Form
    {
        //Variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta = false;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        DataTable dtConsulta;
        string sFechaInicio, sFechaFin;
        string sTexto;

        //Constructores
        public frmInformeVentasPorOrigen()
        {
            InitializeComponent();
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

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvInforme.Rows.Clear();
                double dbTotalMenuExpress = 0;
                double dbDineroEsperado = 0;
                double dbDineroCancelado = 0;

                sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido,  OO.descripcion ,CAB.estado_orden "+
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido "+ 
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido "+
                        "inner join pos_origen_orden OO on CAB.id_pos_origen_orden = OO.id_pos_origen_orden "+
                        "where  CAB.fecha_orden between '"+sFechaInicio+"'  "+
                        "and '"+sFechaFin+"'  and CAB.estado_orden   in('Pagada', 'Cancelada') "+
                        "group by NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido, OO.descripcion, CAB.estado_orden ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0]);
                            string sFechaPedido = dtConsulta.Rows[i].ItemArray[1].ToString();
                            int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2]);
                            string sTipoOrden = dtConsulta.Rows[i].ItemArray[3].ToString();
                            string sEstadoOrden = dtConsulta.Rows[i].ItemArray[4].ToString();

                            string sSqlAyuda = "select * from cv403_det_pedidos where id_pedido = " + iIdPedido + " and estado = 'a'";
                            DataTable dtEmergente = new DataTable();
                            dtEmergente.Clear();
                            bool bAyuda = conexion.GFun_Lo_Busca_Registro(dtEmergente, sSqlAyuda);
                            double iCantidad = 0;
                            double dbValorIva = 0;
                            double dbPrecioUnitario = 0;
                            double dbValorOtro = 0;
                            double dbValorDescuento = 0;
                            double dbTotal = 0;
                            double dbTotalNeto = 0;
                            double dbTotalCancelado = 0;
                            if (bAyuda == true)
                            {
                                for (int j = 0; j < dtEmergente.Rows.Count; j++)
                                {
                                    iCantidad = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[8].ToString());
                                    dbValorIva = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[11].ToString());
                                    dbPrecioUnitario = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[7].ToString());
                                    dbValorOtro = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[12].ToString());
                                    dbValorDescuento = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[9].ToString());
                                    if(sEstadoOrden == "Cancelada")
                                        dbTotalNeto +=0;
                                    else
                                        dbTotalNeto +=(iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                                    

                                    if (sEstadoOrden == "Pagada")
                                        dbTotalCancelado += 0;
                                    else
                                        dbTotalCancelado += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));

                                    dbTotal += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                                }

                            }

                            dgvInforme.Rows.Add(sFechaPedido,iNumeroPedido+" /"+ sEstadoOrden,
                                                dbTotal.ToString("N2"), sTipoOrden);
                            //Variable para sumar el total de ordenes de menú express
                            dbTotalMenuExpress += dbTotal;
                            dbDineroEsperado += dbTotalNeto;
                            dbDineroCancelado += dbTotalCancelado;
 
                        }

                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("TOTAL DE LAS ÓRDENES", "", "$" + dbTotalMenuExpress.ToString("N2"));
                        dgvInforme.Rows.Add("TOTAL DE ORDENES CANCELADAS", "", "$" + dbDineroCancelado.ToString("N2"));
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("TOTAL DEL DINERO ESPERADO", "", "$" + dbDineroEsperado.ToString("N2"));
                        
                        dgvInforme.SelectedRows[0].Selected = false;
 
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccinadas";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
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

        //Función pra llenar el txt
        private void llenarTxt()
        {
            try
            {
                sTexto = " ";
                double dbTotalMenuExpress = 0;
                double dbDineroEsperado = 0;
                double dbDineroCancelado = 0;

                sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido,  OO.descripcion ,CAB.estado_orden " +
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido " +
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido " +
                        "inner join pos_origen_orden OO on CAB.id_pos_origen_orden = OO.id_pos_origen_orden " +
                        "where  CAB.fecha_orden between '" + sFechaInicio + "'  " +
                        "and '" + sFechaFin + "'  and CAB.estado_orden   in('Pagada', 'Cancelada') " +
                        "group by NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido, OO.descripcion, CAB.estado_orden ";

               

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto += ("REPORTE DE PEDIDOS ENTRE " + sFechaInicio + " HASTA " + sFechaFin +"").PadLeft(75,' ')+"\r\n";
                        sTexto += "\r\n\r\n";
                        sTexto += "FECHA DE LA ORDEN".PadRight(24,' ') +"NUMERO DE ORDEN".PadRight(31,' ') +"VALOR".PadLeft(13,' ')+
                                "TIPO DE ORDEN".PadLeft(21,' ')+"\r\n";
                        sTexto += "=".PadRight(90,'=')+"\r\n";

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0]);
                            string sFechaPedido = dtConsulta.Rows[i].ItemArray[1].ToString();
                            int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2]);
                            string sTipoOrden = dtConsulta.Rows[i].ItemArray[3].ToString();
                            string sEstadoOrden = dtConsulta.Rows[i].ItemArray[4].ToString();

                            string sSqlAyuda = "select * from cv403_det_pedidos where id_pedido = " + iIdPedido + " and estado = 'a'";
                            DataTable dtEmergente = new DataTable();
                            dtEmergente.Clear();
                            bool bAyuda = conexion.GFun_Lo_Busca_Registro(dtEmergente, sSqlAyuda);
                            double iCantidad = 0;
                            double dbValorIva = 0;
                            double dbPrecioUnitario = 0;
                            double dbValorOtro = 0;
                            double dbValorDescuento = 0;
                            double dbTotal = 0;
                            double dbTotalNeto = 0;
                            double dbTotalCancelado = 0;
                            if (bAyuda == true)
                            {
                                for (int j = 0; j < dtEmergente.Rows.Count; j++)
                                {
                                    iCantidad = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[8].ToString());
                                    dbValorIva = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[11].ToString());
                                    dbPrecioUnitario = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[7].ToString());
                                    dbValorOtro = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[12].ToString());
                                    dbValorDescuento = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[9].ToString());
                                    if (sEstadoOrden == "Cancelada")
                                        dbTotalNeto += 0;
                                    else
                                        dbTotalNeto += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));

                                    if (sEstadoOrden == "Pagada")
                                        dbTotalCancelado += 0;
                                    else
                                        dbTotalCancelado += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));


                                    dbTotal += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                                }

                            }

                            sTexto += sFechaPedido.PadRight(24, ' ') + (iNumeroPedido + " /" + sEstadoOrden).ToString().PadRight(31, ' ') +
                                dbTotal.ToString("N2").PadLeft(13, ' ') + " ".PadRight(8,' ')+ sTipoOrden.PadRight(21, ' ') + "\r\n";
 
                            //Variable para sumar el total de ordenes de menú express
                            dbTotalMenuExpress += dbTotal;
                            dbDineroEsperado += dbTotalNeto;
                            dbDineroCancelado += dbTotalCancelado;
                        }

                        sTexto += "\r\n";
                        sTexto += "*".PadRight(90, '*') + "\r\n";
                        sTexto += "\r\n";
                        sTexto += "TOTAL DE LAS ORDENES: ".PadRight(55,' ')+( "$"+ dbTotalMenuExpress.ToString("N2")).PadLeft(13,' ');
                        sTexto += "\r\n";                        
                        sTexto += "TOTAL DE CUENTAS CANCELADAS: ".PadRight(55, ' ') + ("$" + dbDineroCancelado.ToString("N2")).PadLeft(13, ' ');
                        sTexto += "\r\n";
                        sTexto += "----------".PadLeft(69,' ')+"\r\n";
                        sTexto += "TOTAL DEL DINERO ESPERADO: ".PadRight(55, ' ') + ("$" + dbDineroEsperado.ToString("N2")).PadLeft(13, ' ');
                        
                       // dgvInforme.SelectedRows[0].Selected = false;

                        txtInforme.Text = sTexto;

                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccinadas";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el reporte";
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

        private void frmInformeVentasPorOrigen_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
        }

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //Función para exportar a excel
        private void exportarAExcel(DataGridView dgvCierre)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int iIndiceColumna = 0;

            excel.Columns.ColumnWidth = 30;
            excel.Cells[1, 1] = "INFORME DE VENTAS ";
            excel.Cells[2, 1] = "DESDE " + sFechaInicio + " A " + sFechaFin;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;

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

            excel.get_Range("A4", "D4").BorderAround();

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
            catch(Exception ex)
            {
                catchMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }
        //Fin de la clase
    }
}
