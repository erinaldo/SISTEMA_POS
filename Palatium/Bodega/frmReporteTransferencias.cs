using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmReporteTransferencias : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool brespuesta;
        DataTable dtConsulta = new DataTable();
        int iIdMovimientoBodega;
        string sNumeroMovimiento;
        DataTable dt;
        dsReporte ds;
        DataTable dtDetalle;
        int iIdBodegaDestino;
        int iIdBodegaOrigen;

        public frmReporteTransferencias(int iIdMovimientoBodega, string sNumeroMovimiento, int iIdBodegaDestino, int iIdBodegaOrigen)
        {
            this.iIdBodegaDestino = iIdBodegaDestino;
            this.iIdBodegaOrigen = iIdBodegaOrigen;
            this.iIdMovimientoBodega = iIdMovimientoBodega;
            this.sNumeroMovimiento = sNumeroMovimiento;
            InitializeComponent();
        }

        private void frmReporteTransferencias_Load(object sender, EventArgs e)
        {
            crearReporteTransferencias();
        }

        //Función para crear el reporte de transferencias
        public void crearReporteTransferencias()
        {
            try
            {
                sSql = "Select m.Fecha,m.Cg_Empresa,m.ID_BODEGA,m.CG_TIPO_MOVIMIENTO,m.ID_LOCALIDAD, " +
                        "m.Cg_Moneda_Base,m.Referencia_Externa,m.Nota_pedido,m.FACTURA,m.Nota_entrega,m.Observacion, " +
                    "m.cg_motivo_movimiento_bodega,m.Id_Auxiliar, m.Id_Persona, m.Porcentaje_IVA,m.Porcentaje_descuento,  " +
                    "m.id_c_movimiento, m.estado_replica, m.estado from cv402_cabecera_movimientos m left outer join  " +
                    "cv404_auxiliares_contables a " +
                        "on m.id_auxiliar =a.id_auxiliar  Where m.Id_Movimiento_Bodega=" + iIdMovimientoBodega;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                brespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (brespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ds = new dsReporte(); //Creo una variable de tipo dsReporte
                        dt = ds.Tables["dtTrasnferencias"]; // intancio el dataset dtIngresos
                        dt.Clear(); // limpio el datatable
                        DataRow dr; //creo una varible de tipo datarow para aumentar lineas a mi reporte

                        for (int i = 0; i < dtConsulta.Rows.Count; i++) // recorro todo el datatable de la consulta sql
                        {
                            dr = dt.NewRow(); // instancio una nuevo fila a mi datatable
                            dr["nombreEmpresa"] = "RIVAS PAOLA "; // se llena el nombre de la empresa (por el momento se ingresa manualmente)
                            dr["numeroMovimiento"] = sNumeroMovimiento; // se llena con el número de movimiento
                            dr["fecha"] = dtConsulta.Rows[i].ItemArray[0].ToString(); // se llena con la fecha
                            dr["bodegaOrigen"] = buscarBodega(iIdBodegaOrigen); // me manda a una función que me retorna el nombre de la bodega
                            dr["bodegaDestino"] = buscarBodega(iIdBodegaDestino); // me manda a una función que me retorna el nombre de la bodega
                            dr["observacion"] = dtConsulta.Rows[i].ItemArray[10].ToString();
                            dr["motivo"] = buscarMotivo(Convert.ToInt32(dtConsulta.Rows[i].ItemArray[11].ToString())); //Función que me devuelve el nombre del motivo

                            dt.Rows.Add(dr);
                        }

                        Reportes.reporteTrasnferenciaBodega reporte = new Reportes.reporteTrasnferenciaBodega();

                        reporte.SetDataSource(dt);
                        //ESTA LINEA PERMITE VISUALIZAR EL REPORTE ANTES DE IMPRIMIR
                        this.crystalReportViewer1.ReportSource = reporte;
                        crystalReportViewer1.Refresh();

                    }
                }
                else
                    MessageBox.Show("Ocurrió un problema al crear el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        //Función que me retorna el nombre de la bodega
        private string buscarBodega(int iIdBodega)
        {
            string sSql ="select descripcion from cv402_bodegas where id_bodega = "+iIdBodega;
            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            brespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

            if(brespuesta == true)
            {
                if(dtAyuda.Rows.Count>0)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
            }
            return "Bodega no establecida";
           
        }

        //Función que me retorna el motivo
        private string buscarMotivo(int iIdMotivo)
        {
            string sSql = "select valor_texto from tp_codigos where correlativo = " + iIdMotivo;
            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            brespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

            if (brespuesta == true)
            {
                if (dtAyuda.Rows.Count > 0)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
            }
            return "Motivo no establecido";
        }

        
    }
}
