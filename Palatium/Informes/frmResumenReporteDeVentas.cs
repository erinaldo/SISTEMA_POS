using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace Palatium.Informes
{
    public partial class frmResumenReporteDeVentas : Form
    {
        string sFechaInicio, sFechaFin;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        System.Data.DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        double dbSubtotal = 0;
        double dbIva = 0;
        double dbOtros = 0;
        double dbTotalPagos = 0;
        int iNumeroOrdenes = 0;
        double dbTotalPersona = 0;
        string sTexto = "";
        double dbTotalTabla = 0;

        public frmResumenReporteDeVentas()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dbSubtotal = 0;
            dbIva = 0;
            dbOtros = 0;
            dbTotalPagos = 0;
            iNumeroOrdenes = 0;
            dbTotalPersona = 0;
            sTexto = "";
            llenarGrid();
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {

                dgvInforme.Rows.Clear();
                sSql = @"select CAB.id_pedido, sum(DET.Cantidad *(DET.precio_unitario + valor_iva + valor_otro - valor_dscto)) 
                        Total, CAB.numero_personas,sum((DET.precio_unitario - DET.valor_dscto) * Det.cantidad), sum(DET.valor_iva ) IVA, sum (DET.valor_otro) Impuestos 
                        from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET
                        on CAB.id_pedido = DET.id_pedido
                        where CAB.fecha_pedido between '" + sFechaInicio + "' and '" + sFechaFin + "' " +
                        "and DET.estado ='A' and CAB.estado_orden= 'Pagada'  and CAB.id_pos_origen_orden in (1,2,3) " +
                        "group by CAB.id_pedido,CAB.numero_personas";

                dtConsulta = new System.Data.DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iNumeroOrdenes = dtConsulta.Rows.Count;
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //En la variable se almacena el subtotal de las ventas en el rango de fechas
                            dbSubtotal += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                            dbIva += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString());
                            dbOtros += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[5].ToString());
                            dbTotalPersona += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dbTotalTabla += dbSubtotal;
                        }
                        //Se agregar una línea en blanco
                        cargarTxt();

                        dgvInforme.Rows.Add();
                        //se agregar una línea con *
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Recopilación de Ventas");
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("", "Cantidad", "Conteo");
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Ventas Totales", "$" + dbSubtotal.ToString("N2"), dtConsulta.Rows.Count.ToString());
                        dgvInforme.Rows.Add("Iva", "$" + dbIva.ToString("N2"));
                        dgvInforme.Rows.Add("Otros Impuestos", "$" + dbOtros.ToString("N2"));
                        dgvInforme.Rows.Add();
                        double DBfinal = (dbSubtotal + dbOtros + dbIva);
                        dgvInforme.Rows.Add("Ventas Brutas", DBfinal.ToString("N2"));
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Recibos netos esperados", DBfinal.ToString("N2"));
                        dgvInforme.Rows.Add();
                        llenarFormasPago();
                        dgvInforme.Rows.Add();
                        llenarEstadisticaVentas();
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para ser mostrados en el rango de fechas seleccionado";
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

        //Función para llenar las formas de Pago
        private void llenarFormasPago()
        {
            try
            {

                sSql = "select FP.descripcion,sum(FP.valor) valor, count (CP.id_pedido) Numero " +
                         " from cv403_cab_pedidos CP, " +
                            "cv403_numero_cab_pedido NP,  pos_vw_pedido_forma_pago FP   " +
                            "where CP.fecha_pedido between '"+sFechaInicio+"' and '"+sFechaFin+"' " +
                        "and CP.id_pedido = NP.id_pedido and CP.id_pedido = FP.id_pedido and CP.id_pos_origen_orden in (1,2,3)  "+
                        "and CP.estado_orden= 'Pagada' "+
                        "group by FP.descripcion";

                dtConsulta = new System.Data.DataTable();
                dtConsulta.Rows.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Desglose de Pagos");
                        dgvInforme.Rows.Add();

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sDescripcion = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbValor = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dbTotalPagos += dbValor;
                            dgvInforme.Rows.Add(sDescripcion, dbValor.ToString("N2"), dbCantidad.ToString("N0"));
                            sTexto += sDescripcion.PadRight(38, ' ') + ("$" + dbValor.ToString("N2")).PadLeft(24, ' ') +
                                dbCantidad.ToString("N0").PadLeft(18, ' ') + "\r\n";
                            
                        }

                        sTexto += " ".PadRight(38, ' ') + "===============".PadLeft(24, ' ') + "======".PadLeft(18, ' ') + "\r\n";
                        sTexto += "PAGOS TOTALES >>>".PadRight(38, ' ') + ("$" + dbTotalPagos.ToString("N2")).PadLeft(24, ' ') + "\r\n\r\n\r\n";
                        sTexto += "*".PadRight(80, '*') + "\r\n";


                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Pagos Totales >>>>", "$" + dbTotalPagos);
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

        //Función para llenar las estadísticas de ventas
        private void llenarEstadisticaVentas()
        {
            try
            {
                double dbPromedioPersonas = (dbSubtotal + dbOtros + dbIva) / dbTotalPersona;

                dgvInforme.Rows.Add("Estadísticas de Ventas");
                dgvInforme.Rows.Add();
                dgvInforme.Rows.Add("Promedio de Orden:", ((dbSubtotal + dbOtros + dbIva) / iNumeroOrdenes).ToString("N2"));
                dgvInforme.Rows.Add();
                dgvInforme.Rows.Add("Total de Personas:", dbTotalPersona);
                if (dbTotalPersona <= 0)
                    dbPromedioPersonas = 0;
                dgvInforme.Rows.Add("Promedio por Persona", dbPromedioPersonas.ToString("N2"));

                sTexto += "ESTADÍSTICAS DE VENTAS \r\n";
                sTexto += "*".PadRight(80, '*') + "\r\n";
                sTexto += "PROMEDIO DE ORDEN: ".PadRight(38, ' ') + ((dbSubtotal + dbOtros + dbIva) / iNumeroOrdenes).ToString("N2").PadLeft(24, ' ') + "\r\n";
                sTexto += "TOTAL DE PERSONAS: ".PadRight(38, ' ') + dbTotalPersona.ToString().PadLeft(24, ' ') + "\r\n";
                sTexto += "PROMEDIO DE ORDEN: ".PadRight(38, ' ') + dbPromedioPersonas.ToString("N2").PadLeft(24, ' ') + "\r\n";
                sTexto += "\r\n\r\n\r\n\r\n\r\n\r\n\r\n";

                txtInforme.Text = sTexto;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //FUnción para cargar el Txt
        private void cargarTxt()
        {
            sTexto += "*** RESUMEN DEL REPORTE DE VENTAS *** ".PadLeft(61, ' ') + "\r\n";
            sTexto += ("DE " + sFechaInicio + " A " + sFechaFin).PadLeft(55, ' ') + "\r\n\r\n";
            sTexto += "RECOPILACION DE LAS VENTAS".PadRight(38, ' ') + "\r\n";
            sTexto += "CANTIDAD".PadLeft(62, ' ') + "CONTEO".PadLeft(18, ' ') + "\r\n";
            sTexto += " ".PadRight(38, ' ') + "--------------".PadLeft(24, ' ') + "----------".PadLeft(18, ' ') + "\r\n";
            sTexto += "VENTAS TOTALES: ".PadRight(38, ' ') + ("$" + dbSubtotal.ToString("N2")).PadLeft(24, ' ') + (iNumeroOrdenes.ToString()).PadLeft(18, ' ') + "\r\n";
            sTexto += "I.V.A: ".PadRight(38, ' ') + ("$" + dbIva.ToString("N2")).PadLeft(24, ' ') + "\r\n";
            sTexto += "OTROS IMPUESTOS".PadRight(38, ' ') + ("$" + dbOtros.ToString("N2")).PadLeft(24, ' ') + "\r\n";
            sTexto += " ".PadRight(38, ' ') + "===============".PadLeft(24, ' ') + "\r\n";
            sTexto += "VENTAS BRUTAS:".PadRight(38, ' ') + ("$" + (dbSubtotal + dbIva + dbOtros).ToString("N2")).PadLeft(24, ' ') + "\r\n\r\n";
            sTexto += "RECIBOS NETOS ESPERADOS>>>>".PadRight(38, ' ') + ("$" + (dbSubtotal + dbIva + dbOtros).ToString("N2")).PadLeft(24, ' ') + "\r\n\r\n";
            sTexto += "*".PadRight(80, '*') + "\r\n";
            sTexto += "DESGLOCE DE PAGOS:\r\n\r\n";

        }

        private void btnExportarTexto_Click(object sender, EventArgs e)
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

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;
                if (iIndiceColumna == 1)
                    excel.Cells[1, iIndiceColumna].ColumnWidth = 40;

               // excel.Cells[5, iIndiceColumna] = col.HeaderText;
               // excel.Cells[5, iIndiceColumna].Interior.Color = Color.Yellow;

            }

            excel.Cells[4, 1].Interior.Color = Color.Yellow;
            excel.Cells[17, 1].Interior.Color = Color.Yellow;
            excel.Cells[6, 2].Interior.Color = Color.BurlyWood;
            excel.Cells[6, 3].Interior.Color = Color.BurlyWood;
            excel.get_Range("B6", "C6").BorderAround();
            excel.get_Range("A4").BorderAround();
            excel.get_Range("A17").BorderAround();

            int iIndiceFila = 0;

            foreach (DataGridViewRow row in dgvCierre.Rows)
            {
                iIndiceFila++;

                iIndiceColumna = 0;

                foreach (DataGridViewColumn col in dgvCierre.Columns)
                {
                    iIndiceColumna++;
                    excel.Cells[iIndiceFila + 1, iIndiceColumna] = row.Cells[col.Name].Value;
                    //excel.Cells[iIndiceFila + 1, iIndiceColumna].BorderAround();
                }

            }

           // excel.get_Range("A5", "E5").BorderAround();

            excel.Visible = true;

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

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtHasta.Text = calendario.txtFecha.Text;
                sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
            }
        }

        private void frmResumenReporteDeVentas_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
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
                string sPath = @"C:\reportes\Ventas.txt";

                if (!File.Exists(sPath))
                {
                    StreamWriter sw = File.CreateText(sPath);
                }

                File.WriteAllText(sPath, txtInforme.Text);
            }
        }



        //Fin de la Clase
    }
}
