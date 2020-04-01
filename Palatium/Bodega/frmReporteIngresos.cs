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
    public partial class frmReporteIngresos : Form
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
        int iIdBodegaOrigen;
        int iIdBodegaDestino;


        public frmReporteIngresos(int iIdMovimientoBodega, string sNumeroMovimiento)
        {
            //this.iIdBodegaDestino = iIdBodegaDestino;
            //this.iIdBodegaOrigen = iIdBodegaOrigen;
            this.iIdMovimientoBodega = iIdMovimientoBodega;
            this.sNumeroMovimiento = sNumeroMovimiento;
            InitializeComponent();
        }

        public void frmReporteIngresos_Load(object sender, EventArgs e)
        {
            //crearReporteTransferencias();
            crearReporte();
        }

        //Función para crear el reporte
        public void crearReporte()
        {
            try
            {
                sSql = "";
                sSql += "Select m.Fecha, m.Cg_Empresa, m.ID_BODEGA, m.CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "m.ID_LOCALIDAD, m.Cg_Moneda_Base, m.Referencia_Externa, m.Nota_pedido," + Environment.NewLine;
                sSql += "m.FACTURA, m.Nota_entrega, m.Observacion, m.cg_motivo_movimiento_bodega," + Environment.NewLine;
                sSql += "m.Id_Auxiliar, m.Id_Persona, m.Porcentaje_IVA,m.Porcentaje_descuento," + Environment.NewLine;
                sSql += "m.id_c_movimiento, m.estado_replica, m.estado" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos m left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables a on m.id_auxiliar = a.id_auxiliar" + Environment.NewLine;
                sSql += "where m.Id_Movimiento_Bodega=" + iIdMovimientoBodega;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                brespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (brespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ds = new dsReporte();           //Creo una variable de tipo dsReporte
                        dt = ds.Tables["dtIngresos"];   // intancio el dataset dtIngresos
                        dt.Clear();                     // limpio el datatable
                        DataRow dr;                     //creo una varible de tipo datarow para aumentar lineas a mi reporte

                        for (int i = 0; i < dtConsulta.Rows.Count; i++) // recorro todo el datatable de la consulta sql
                        {
                            dr = dt.NewRow(); // instancio una nuevo fila a mi datatable
                            dr["nombreEmpresa"] = "RIVAS PAOLA"; // se llena el nombre de la empresa (por el momento se ingresa manualmente)
                            dr["numeroIngreso"] = sNumeroMovimiento; // se llena con el numero de movimiento
                            dr["fechaIngreso"] = dtConsulta.Rows[i].ItemArray[0].ToString(); // se llena con la fecha
                            int iIdBodega = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dr["nombreBodega"] = buscarBodega(iIdBodega); // me manda a una función que me retorna el nombre de la bodega
                            dr["referencia"] = dtConsulta.Rows[i].ItemArray[6].ToString(); // me devuelve el número de referencia
                            dr["proveedor"] =  buscarNombreProveedor(Convert.ToInt32(dtConsulta.Rows[i].ItemArray[12].ToString())); // función que me retorna el nombre del proovedor
                            dr["Iva"] = dtConsulta.Rows[i].ItemArray[14].ToString(); // Me devuelve el porcentaje del iva
                            dr["numeroFactura"] =  dtConsulta.Rows[i].ItemArray[8].ToString(); //Me devuelve el número de factura que está almacenado en la base de datos
                            dr["horaIngreso"] = "10:49";//la hora está puesta manualmente hasta el momento
                            dr["motivo"] = buscarMotivo(Convert.ToInt32(dtConsulta.Rows[i].ItemArray[11].ToString())); //Función que me devuelve el nombre del motivo
                            dr["descuento"] = dtConsulta.Rows[i].ItemArray[15].ToString(); // me devuelve el porcentaje de descuento
                            dr["observacion"] = dtConsulta.Rows[i].ItemArray[10].ToString(); // me devuelve la observación
                            dr["notaEntrega"] = dtConsulta.Rows[i].ItemArray[9].ToString();
                           
                            dt.Rows.Add(dr);
                        }

                        sSql = "";
                        sSql += "SELECT MB.CORRELATIVO, P.codigo codigo_producto, N.nombre producto, MB.Id_Producto," + Environment.NewLine;
                        sSql += "MB.especificacion, U.codigo unidad, MB.cg_unidad_compra cg_unidad_compra, MB.CANTIDAD," + Environment.NewLine;
                        sSql += conexion.GFun_St_esnulo() + "(MB.Valor_Unitario, 0) Valor_unitario," + Environment.NewLine;
                        sSql += conexion.GFun_St_esnulo() + "(round(100*MB.Valor_Dscto/MB.valor_Unitario,2), 0) Pct_Dscto," + Environment.NewLine;
                        sSql += conexion.GFun_St_esnulo() + "(MB.Valor_Iva, 0) Valor_Iva, " + conexion.GFun_St_esnulo() + "(MB.VALOR_DSCTO, 0) valor_dscto," + Environment.NewLine;
                        sSql += "Case when P.Paga_Iva = 1 Then 1 Else 0 End Paga_Iva" + Environment.NewLine;
                        sSql += "from cv402_movimientos_bodega MB, cv401_productos P," + Environment.NewLine;
                        sSql += "tp_codigos U, cv401_nombre_productos N" + Environment.NewLine;
                        sSql += "where MB.Id_Producto = P.Id_Producto" + Environment.NewLine;
                        sSql += "and P.Id_Producto = N.Id_Producto" + Environment.NewLine;
                        sSql += "and N.Nombre_Interno = 1" + Environment.NewLine;
                        sSql += "and N.Estado = 'A'" + Environment.NewLine;
                        sSql += "and MB.CG_UNIDAD_COMPRA = U.Correlativo " + Environment.NewLine;
                        sSql += "and MB.Id_Movimiento_Bodega = " + iIdMovimientoBodega + Environment.NewLine;
                        sSql += "and MB.Estado = 'A'" + Environment.NewLine;
                        sSql += "order By MB.Correlativo";

                        DataTable dtConsulta1 = new DataTable();
                        dtDetalle = ds.Tables["dtDetalleIngresos"];
                        dtConsulta1.Clear();
                        DataRow drDetalle;
                        brespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta1, sSql);

                        if (brespuesta == true)
                        {
                            if (dtConsulta1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtConsulta1.Rows.Count; i++)
                                {
                                    drDetalle = dtDetalle.NewRow();
                                    drDetalle["item"] = i+1;
                                    drDetalle["codigo"] = dtConsulta1.Rows[i].ItemArray[1].ToString();
                                    drDetalle["producto"] = dtConsulta1.Rows[i].ItemArray[2].ToString();
                                    drDetalle["cantidad"] = dtConsulta1.Rows[i].ItemArray[7].ToString();
                                    drDetalle["precio"] = dtConsulta1.Rows[i].ItemArray[8].ToString();
                                    drDetalle["descuentoProducto"] = dtConsulta1.Rows[i].ItemArray[11].ToString();
                                    double dbCantidad = Convert.ToDouble(dtConsulta1.Rows[i].ItemArray[7].ToString());
                                    double dbDescuento = Convert.ToDouble(dtConsulta1.Rows[i].ItemArray[11].ToString());
                                    double dbPrecio = Convert.ToDouble(dtConsulta1.Rows[i].ItemArray[8].ToString());
                                    double dbIva = Convert.ToDouble(dtConsulta1.Rows[i].ItemArray[10].ToString());
                                    double dbTotal = (dbCantidad * (dbPrecio + dbIva - dbDescuento));
                                    drDetalle["total"] = dbTotal;

                                    dtDetalle.Rows.Add(drDetalle);
                                }

                            }
                        }


                       // llenarDetalle();

                        Reportes.reporteIngresoBodega reporte = new Reportes.reporteIngresoBodega();
                        
                        reporte.SetDataSource(dt);
                        reporte.Subreports[0].SetDataSource(dtDetalle);
                        //ESTA LINEA PERMITE VISUALIZAR EL REPORTE ANTES DE IMPRIMIR
                        this.crystalReportViewer1.ReportSource = reporte;
                        crystalReportViewer1.Refresh();
                        
                    }
                }
                else
                    MessageBox.Show("Ocurrió un problema al crear el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
    
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al crear el reporte","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //Función que me retorne el nombre del proveedor
        private string buscarNombreProveedor(int iIdProveedor)
        {
            string sSql ="select descripcion from cv404_auxiliares_contables where id_auxiliar = "+iIdProveedor;
            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            brespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

            if(brespuesta == true)
            {
                if(dtAyuda.Rows.Count>0)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
            }
            return "Proveedor no establecido";
        }

        //Función que me retorna el motivo
        private string buscarMotivo(int iIdMotivo)
        {
            string sSql ="select valor_texto from tp_codigos where correlativo = "+iIdMotivo;
            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            brespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

            if(brespuesta == true)
            {
                if(dtAyuda.Rows.Count>0)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
            }
            return "Motivo no establecido";
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
                            dr["nombreEmpresa"] = "RIVAS PAOLA 5"; // se llena el nombre de la empresa (por el momento se ingresa manualmente)
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
                MessageBox.Show(exc.Message,"Reportes",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

       


        //Fin de la clase
    }
}
