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
    public partial class informeDeVentas : Form
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
        

        public informeDeVentas()
        {
            InitializeComponent();
        }

        private void informeDeVentas_Load(object sender, EventArgs e)
        {
            llenarComboVentas();
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
        }

        //Función para llenar el combo de ventas
        private void llenarComboVentas()
        {
            try
            {
                sSql = "select id_pos_origen_orden, descripcion from pos_origen_orden";
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbTipoOrden.llenar(dtConsulta, sSql);
                cmbTipoOrden.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvVentas.Rows.Clear();
                double dbTotalMenuExpress = 0;

                sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido "+
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido "+
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido "+
                        "where id_pos_origen_orden = "+cmbTipoOrden.SelectedValue+" and CAB.fecha_orden between '"+sFechaInicio+"' and '"+sFechaFin+"'  "+
                        "and CAB.estado_orden  = 'Pagada' group by NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido"; 

                string sSql1 = "select descripcion from pos_origen_orden " +
                                      "where id_pos_origen_orden = " + cmbTipoOrden.SelectedValue;

                DataTable dtNombreOrigen = new DataTable();
                dtNombreOrigen.Clear();
                bool bRespuesta1 = conexion.GFun_Lo_Busca_Registro(dtNombreOrigen, sSql1);

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvVentas.Rows.Add();
                        dgvVentas.Rows.Add("", "ORDENES PARA " + cmbTipoOrden.Text, "");
                        dgvVentas.Rows.Add();
                        dgvVentas.Rows.Add("# DE ORDEN", "HORA", "TOTAL");
                        dgvVentas.Rows.Add();

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                            int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                            string sFechaIngreso = dtConsulta.Rows[i].ItemArray[1].ToString();

                            string sSqlAyuda = "select * from cv403_det_pedidos where id_pedido = "+iIdPedido+" and estado = 'a'";
                            DataTable dtEmergente = new DataTable();
                            dtEmergente.Clear();
                            bool bAyuda = conexion.GFun_Lo_Busca_Registro(dtEmergente, sSqlAyuda);
                            double iCantidad =0;
                            double dbValorIva =0;
                            double dbPrecioUnitario = 0;
                            double dbValorOtro=0;
                            double dbValorDescuento =0;
                            double dbTotal=0;
                            if (bAyuda == true)
                            {
                                for (int j = 0; j < dtEmergente.Rows.Count; j++)
                                {
                                    iCantidad = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[8].ToString());
                                    dbValorIva = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[11].ToString());
                                    dbPrecioUnitario = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[7].ToString());
                                    dbValorOtro = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[12].ToString());
                                    dbValorDescuento = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[9].ToString());
                                    dbTotal += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                                }
                                
                            }

                            
                            dgvVentas.Rows.Add(iNumeroPedido.ToString(), sFechaIngreso.PadLeft(12, ' '),
                                dbTotal.ToString("N2"));
                            //Variable para sumar el total de ordenes de menú express
                            dbTotalMenuExpress += dbTotal;

                        }

                        dgvVentas.Rows.Add();
                        dgvVentas.Rows.Add("TOTAL DE LAS ÓRDENES", "", "$"+dbTotalMenuExpress.ToString("N2"));
                        dgvVentas.SelectedRows[0].Selected = false;

                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccionado";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }



            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbTipoOrden.SelectedIndex == 0)
            {
                ok.LblMensaje.Text = "Por favor, seleccione un origen de orden";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                llenarGrid();
                if (dgvVentas.Rows.Count > 0)
                    llenarTxt();
            }

        }

        private void btnExportarTexto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVentas.Rows.Count > 0)
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
                        fileStream.Close();

                        ok.LblMensaje.Text = "Archivo Exportado Correctamente.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "No hay datos para ser exportados";
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

        //Funcion para llenar el txt
        private void llenarTxt()
        { 
        double dbTotalMenuExpress = 0;

            sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido " +
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido " +
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido " +
                        "where id_pos_origen_orden = " + cmbTipoOrden.SelectedValue + " and CAB.fecha_orden between '" + sFechaInicio + "' and '" + sFechaFin + "'  " +
                        "and CAB.estado_orden  = 'Pagada' group by NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido";

            string sSql1 = "select descripcion from pos_origen_orden " +
                                  "where id_pos_origen_orden = " + cmbTipoOrden.SelectedValue;


            DataTable dtNombreOrigen = new DataTable();
            dtNombreOrigen.Clear();
            bool bRespuesta1 = conexion.GFun_Lo_Busca_Registro(dtNombreOrigen, sSql1);

            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {

                    sTexto = " ";
                    sTexto += "\r\n";
                    sTexto += ("*".PadRight(40, '*')) + "\r\n";
                    sTexto += ("INFORME DE VENTAS ".PadLeft(25, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString()) + "\r\n";
                    sTexto += "\r\n";
                    sTexto += ("FECHA DESDE: ".PadRight(20, ' ') + sFechaInicio.PadLeft(20, ' ')) + "\r\n";
                    sTexto += ("FECHA HASTA: ".PadRight(20, ' ') + sFechaFin.PadLeft(20, ' ')) + "\r\n";
                    sTexto += "\r\n";
                    sTexto += ("*".PadRight(40, '*')) + "\r\n";
                    sTexto += ("NUMERO DE ORDENES: ".PadRight(31, ' ') + SacarTotalOrdenes().PadLeft(9, ' ')) + "\r\n";
                    sTexto += "\r\n";
                    sTexto += ("=".PadRight(40, '=')) + "\r\n";
                    sTexto += ("ORDENES ".PadLeft(20, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString()) + "\r\n";
                    sTexto += ("=".PadRight(40, '=')) + "\r\n";
                    sTexto += ("# DE ORDEN".PadRight(18, ' ') + "HORA".PadRight(17) + "TOTAL".PadLeft(5, ' ')) + "\r\n";

                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                        int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                        string sFechaIngreso = dtConsulta.Rows[i].ItemArray[1].ToString();

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
                        if (bAyuda == true)
                        {
                            for (int j = 0; j < dtEmergente.Rows.Count; j++)
                            {
                                iCantidad = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[8].ToString());
                                dbValorIva = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[11].ToString());
                                dbPrecioUnitario = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[7].ToString());
                                dbValorOtro = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[12].ToString());
                                dbValorDescuento = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[9].ToString());
                                dbTotal += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                            }

                        }
                        
                        sTexto += iNumeroPedido.ToString().PadLeft(8, ' ') + " ".PadRight(8, ' ') +
                                    sFechaIngreso.PadLeft(12, ' ').Substring(10) + dbTotal.ToString("N2").PadLeft(15, ' ') + "\r\n";
                        //Variable para sumar el total de ordenes de menú express
                        dbTotalMenuExpress += dbTotal;

                    }

                    sTexto +="\r\n";
                    sTexto += ("---------".PadLeft(40, ' ')) + "\r\n";
                    sTexto += ("TOTAL DE LAS ORDENES: ".PadRight(30, ' ') + "$" + dbTotalMenuExpress.ToString("N2").PadLeft(9, ' ')) + "\r\n";
                    txtInforme.Text = sTexto;

                }
                else
                {
                    
                }


            }
        }

        //Función para crear un archivo plano
        private void crearArchivoPlano(StreamWriter sw)
        {
            double dbTotalMenuExpress = 0;

            sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido " +
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido " +
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido " +
                        "where id_pos_origen_orden = " + cmbTipoOrden.SelectedValue + " and CAB.fecha_orden between '" + sFechaInicio + "' and '" + sFechaFin + "'  " +
                        "and CAB.estado_orden  = 'Pagada' group by NUM.numero_pedido, CAB.fecha_ingreso, CAb.id_pedido";

            string sSql1 = "select descripcion from pos_origen_orden " +
                                  "where id_pos_origen_orden = " + cmbTipoOrden.SelectedValue;


            DataTable dtNombreOrigen = new DataTable();
            dtNombreOrigen.Clear();
            bool bRespuesta1 = conexion.GFun_Lo_Busca_Registro(dtNombreOrigen, sSql1);

            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    
                    sw.WriteLine();
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.WriteLine("INFORME DE VENTAS ".PadLeft(25, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                    sw.WriteLine();
                    sw.WriteLine("FECHA DESDE: ".PadRight(20, ' ') + sFechaInicio.PadLeft(20, ' '));
                    sw.WriteLine("FECHA HASTA: ".PadRight(20, ' ') + sFechaFin.PadLeft(20, ' '));
                    sw.WriteLine();
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.WriteLine("NUMERO DE ORDENES: ".PadRight(31, ' ') + SacarTotalOrdenes().PadLeft(9, ' '));
                    sw.WriteLine();
                    sw.WriteLine("=".PadRight(40, '='));
                    sw.WriteLine("ORDENES ".PadLeft(20, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                    sw.WriteLine("=".PadRight(40, '='));
                    sw.WriteLine("# DE ORDEN".PadRight(18, ' ') + "HORA".PadRight(17) + "TOTAL".PadLeft(5, ' '));
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                        int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                        string sFechaIngreso = dtConsulta.Rows[i].ItemArray[1].ToString();

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
                        if (bAyuda == true)
                        {
                            for (int j = 0; j < dtEmergente.Rows.Count; j++)
                            {
                                iCantidad = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[8].ToString());
                                dbValorIva = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[11].ToString());
                                dbPrecioUnitario = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[7].ToString());
                                dbValorOtro = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[12].ToString());
                                dbValorDescuento = Convert.ToDouble(dtEmergente.Rows[j].ItemArray[9].ToString());
                                dbTotal += (iCantidad * ((dbPrecioUnitario + dbValorIva + dbValorOtro) - dbValorDescuento));
                            }

                        }
                        sw.WriteLine(iNumeroPedido.ToString().PadLeft(8, ' ') + " ".PadRight(8, ' ') +
                                    sFechaIngreso.PadLeft(12, ' ').Substring(10) + dbTotal.ToString("N2").PadLeft(15, ' '));
                        //Variable para sumar el total de ordenes de menú express
                        dbTotalMenuExpress += dbTotal;

                    }

                    sw.WriteLine();
                    sw.WriteLine("---------".PadLeft(40, ' '));
                    sw.WriteLine("TOTAL DE LAS ORDENES: ".PadRight(30, ' ') + "$" + dbTotalMenuExpress.ToString("N2").PadLeft(9, ' '));

                    sw.Close();

                }
                else
                {
                    sw.WriteLine();
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.WriteLine("INFORME DE VENTAS ".PadLeft(25, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                    sw.WriteLine();
                    sw.WriteLine("ENTRADA: ".PadRight(10, ' ') + sFechaInicio.PadLeft(30, ' '));
                    sw.WriteLine();
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.WriteLine("NUMERO DE ORDENES: ".PadRight(31, ' ') + SacarTotalOrdenes().PadLeft(9, ' '));
                    sw.WriteLine();
                    sw.WriteLine("=".PadRight(40, '='));
                    sw.WriteLine("ORDENES ".PadLeft(20, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                    sw.WriteLine("=".PadRight(40, '='));
                    sw.WriteLine();
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.WriteLine("NO SE HAN REGISTRADO ÓRDENES".PadLeft(30, ' '));
                    sw.WriteLine("*".PadRight(40, '*'));
                    sw.Close();
                }


            }
        }

        //Función para sacar el total de ordenes de menú express
        private string SacarTotalOrdenes()
        {
            try
            {
                
                sSql = "select COUNT(*) from cv403_cab_pedidos where fecha_orden between '" + sFechaInicio+ "' and '"+sFechaFin+"' " +
                        "AND ID_POS_ORIGEN_ORDEN = " + cmbTipoOrden.SelectedValue;
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) > 0)
                    {
                        return dtConsulta.Rows[0].ItemArray[0].ToString();
                    }
                    else
                        goto reversa;
                }
                else
                    goto reversa;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                return "0";
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dgvVentas.Rows.Count > 0)
                    exportarAExcel(dgvVentas);
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
            excel.Cells[1, 1] = "INFORME DE VENTAS POR ORIGEN DE ORDEN";
            excel.Cells[2, 1] = "DESDE " + sFechaInicio + " A " + sFechaFin;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;

                //excel.Cells[1, iIndiceColumna] = col.Name;
            }


            int iIndiceFila = 3;

            excel.Cells[6, 2].Interior.Color = Color.Aquamarine;
            excel.Cells[8, 1].Interior.Color = Color.Yellow;
            excel.Cells[8, 2].Interior.Color = Color.Yellow;
            excel.Cells[8, 3].Interior.Color = Color.Yellow;

            excel.get_Range("B6", "B6").BorderAround();
            excel.get_Range("A8", "C8").BorderAround();

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
                sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void grupoSeleccion_Enter(object sender, EventArgs e)
        {

        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(TxtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                TxtHasta.Text = calendario.txtFecha.Text;
                sFechaFin = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string sPath = @"C:\reportes\OrigenOrden.txt";

            if (!File.Exists(sPath))
            {
                StreamWriter sw = File.CreateText(sPath);
            }

            File.WriteAllText(sPath, txtInforme.Text);
        }

        //Fin de la clase
    }
}
