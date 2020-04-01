using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Informes
{
    public partial class frmReporteControlFlujoDiario : Form
    {
        //Variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta = false;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        DataTable dtConsulta;
        string sFechaInicio, sFechaFin;

        public frmReporteControlFlujoDiario()
        {
            InitializeComponent();
        }

        private void frmReporteControlFlujoDiario_Load(object sender, EventArgs e)
        {
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaInicio = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaFin = txtHasta.Text.Substring(6, 4) + "/" + txtHasta.Text.Substring(3, 2) + "/" + txtHasta.Text.Substring(0, 2);
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvInforme.Rows.Clear();
                sSql = "select CAB.fecha_ingreso, CAb.id_pedido,  OO.descripcion , MES.Descripcion 'Mesero', CAB.Numero_personas '#PAX', "+
                        "sum((DET.precio_unitario + DET.valor_iva + DET.valor_otro - det.valor_dscto )* DET.cantidad) 'Total', "+
                        "MESA.numero_mesa '# Mesa', SEC.descripcion 'Sección Mesa', "+
                        "sum(((DET.precio_unitario + DET.valor_iva + DET.valor_otro - det.valor_dscto )* DET.cantidad)/Cab.numero_personas)  "+
                        "'Consumo Promedio' "+
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET on CAB.id_pedido = DET.id_pedido  "+
                        "inner join cv403_numero_cab_pedido NUM on CAB.id_pedido = NUM.id_pedido  "+
                        "inner join pos_origen_orden OO on CAB.id_pos_origen_orden = OO.id_pos_origen_orden "+
                        "inner join pos_mesero MES on CAB.id_pos_mesero = MES.id_pos_mesero  "+
                        "inner join pos_mesa MESA on MESA.id_pos_mesa = CAB.id_pos_mesa  "+
                        "inner join pos_seccion_mesa SEC on SEC.id_pos_seccion_mesa = MESA.id_pos_seccion_mesa  "+
                        "where  CAB.fecha_orden between '"+sFechaInicio+"'  and '"+sFechaFin+"'  and CAB.estado_orden   "+
                        "in('Pagada', 'Cancelada') and CAB.Numero_personas >0 and Det.estado = 'A' and Cab.estado = 'A' " +
                        "group by CAB.fecha_ingreso, CAb.id_pedido,  "+
                        "OO.descripcion, MES.Descripcion, CAB.Numero_personas,MESA.numero_mesa, SEC.descripcion  "+ 
                        "order by CAB.fecha_ingreso";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        double dbTotalNumeroPax = 0;
                        double dbTotalCuenta = 0;
                        string sFechaComprobacion = dtConsulta.Rows[0].ItemArray[0].ToString();
                        
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {

                            string sFecha = dtConsulta.Rows[i].ItemArray[0].ToString().Substring(0,10);

                            if (sFechaComprobacion != sFecha)
                            {
                                if (dgvInforme.Rows.Count > 0)
                                {
                                    dgvInforme.Rows.Add();
                                    dgvInforme.Rows.Add();
                                }
                                sFechaComprobacion = sFecha;
                                dgvInforme.Rows.Add(sFecha);
                            }

                            string sNombreMesero = dtConsulta.Rows[i].ItemArray[3].ToString();
                            string sNumeroPax = dtConsulta.Rows[i].ItemArray[4].ToString();
                            double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[5].ToString());
                            string sNumeroMesa = dtConsulta.Rows[i].ItemArray[6].ToString();
                            string sSeccionMesa = dtConsulta.Rows[i].ItemArray[7].ToString();
                            double dbConsumoPromedio = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[8].ToString());
                            string sHora = dtConsulta.Rows[i].ItemArray[0].ToString().Substring(11,5);
                            dbTotalNumeroPax += Convert.ToDouble(sNumeroPax);
                            dbTotalCuenta += dbTotal;

                            dgvInforme.Rows.Add(sNombreMesero,
                                                sNumeroMesa,
                                                sSeccionMesa,
                                                sHora,
                                                sNumeroPax,
                                                dbTotal.ToString("N2"),
                                                dbConsumoPromedio.ToString("N2")
                                                );

                            
           

                        }

                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add();
                        dgvInforme.Rows.Add("Totales","","","",dbTotalNumeroPax.ToString("N2"),"$" +dbTotalCuenta.ToString("N2"));
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para ser mostrados en el rango de fechas seleccionados";
                        ok.ShowDialog();
                    }
                }
                else 
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception exc)
            {
                catchMensaje.LblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
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
            excel.Cells[1, 1] = "INFORME FLUJO DIARIO DE CLIENTES";
            excel.Cells[2, 1] = "DESDE " + sFechaInicio + " A " + sFechaFin;

            foreach (DataGridViewColumn col in dgvCierre.Columns)
            {
                iIndiceColumna++;

                if (iIndiceColumna != 0)
                    excel.Columns.ColumnWidth = 15;
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
                    excel.Cells[iIndiceFila + 1, iIndiceColumna].BorderAround();
                }

            }

            excel.get_Range("A4", "G4").BorderAround();

            excel.Visible = true;

        }



        //Fin de la clase
    }
}
