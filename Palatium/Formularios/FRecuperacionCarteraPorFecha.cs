using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FRecuperacionCarteraPorFecha : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        DataTable dtTipoEmpresa = new DataTable();
        DataTable dtBodega, dtListPrec, dtServidor, dtlocalidad;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dt2;
        DataTable dtVendedor;
        DataTable dtMoneda;
        string estado = "";
        string T_st_sql = "";
        bool x = false;
        bool y = false;
        bool z = false;//creamos la variable
        int iIdpersona;
        int iIdEmpresa;
        int iIdLocalidad;
        int iIdVendedor;
        int iIdMoneda;

        public FRecuperacionCarteraPorFecha()
        {
            InitializeComponent();
        }

        private void FRecuperacionCarteraPorFecha_Load(object sender, EventArgs e)
        {
            txtFechaInicio.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtFechaFinal.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //string[] t_st_datos = { "1", "adsdasdasd" };
            //llenarGrid(t_st_datos);
            llenarComboEmpresa();
            llenarCombLocalidad();
            llenarVendedor();
            cmbMoned();
        }

        //Llenar combo tipo Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                dtTipoEmpresa = new DataTable();
                string sql = "select idempresa,nombrecomercial from sis_empresa";
                cmbEmpresa.llenar(dtTipoEmpresa, sql);
                cmbEmpresa.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta");
            }

        }

        //LLENAR COMBO localidad
        private void llenarCombLocalidad()
        {
            try
            {
                dtlocalidad = new DataTable();
                string sql = "select id_localidad,nombre_localidad from tp_vw_localidades";
                cmbLocalidad.llenar(dtlocalidad, sql);
                cmbLocalidad.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //LLENAR COMBO Vendedor
        private void llenarVendedor()
        {
            try
            {
                dtVendedor = new DataTable();
                string sql = "select id_vendedor,codigo from cv403_vendedores";
                cmbVendedor.llenar(dtVendedor, sql);
                cmbVendedor.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //LLENAR COMBO moneda
        private void cmbMoned()
        {
            try
            {
                dtMoneda = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00021' and estado='A'";
                cmbMoneda.llenar(dtMoneda, sql);
                cmbMoneda.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        private void btnPregunta_Click_1(object sender, EventArgs e)
        {
            Formularios.FAyudaClienteDarioVentas ayuda1 = new Formularios.FAyudaClienteDarioVentas();
            ayuda1.ShowDialog();

            iIdpersona = Convert.ToInt32(ayuda1.Id);
            txtIdentificacionCliente.Text = ayuda1.Identificaci;
            txtNombreCliente.Text = ayuda1.Cliente;
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtIdentificacionCliente.Text = "";
            txtNombreCliente.Text = "";
            txtFechaInicio.Focus();
            Grb_DatoCateraCobrar.Enabled = true;
        }

        private void btnCerrarCateraCobrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCateraCobrar_Click(object sender, EventArgs e)
        {
            Grb_DatoCateraCobrar.Enabled = false;
            //btnNuevoLocalidades.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnOKCateraCobrar_Click(object sender, EventArgs e)
        {
            ok.lblMensaje.Text = "Módulo en mantenimiento.";
            //ok.ShowDialog();

            dgvCateraCobrar.Rows.Clear();
            iIdEmpresa = Convert.ToInt32(cmbEmpresa.SelectedValue.ToString());
            iIdLocalidad = Convert.ToInt32(cmbLocalidad.SelectedValue.ToString());
            iIdVendedor = Convert.ToInt32(cmbVendedor.SelectedValue.ToString());
            iIdMoneda = Convert.ToInt32(cmbMoneda.SelectedValue.ToString());

            //verificara si se ha selecionado un cliente o solo la localidad
            if (txtIdentificacionCliente.Text == "" && txtNombreCliente.Text == "")
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridSinCliente(t_st_datos);
                totales();
            }
            else
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridConCliente(t_st_datos);
                totales();
            }
        }

        //FUNCION PARA LLENAR EL GRID cuando no se haya seleccionado ningun cliente 
        private void llenarGridSinCliente(string[] t_st_datos)
        {
            //int iIdTipoComprovante = 1;
            int iContador = 0;
            //try
            //{
            string t_st_query = "";
            string t_st_query2 = "";
            dt = new DataTable();
            //t_st_query = "SELECT TIPO.valor_texto,FAC.id_factura,DPC.id_documento_cobrar,DPC.cg_estado_dcto,FAC.fecha_factura,FAC.fecha_vcto, FAC.valor VALOR_FACTURA, FAC.direccion_factura,FAC.telefono_factura, FAC.ciudad_factura ciudad_factura, DPC.numero_documento numero_factura, VEN.apellidos + ' ' + isnull(VEN.nombreS,'') Vendedor, CL.apellidos + ' ' + isnull(CL.nombreS,'') CLIENTE,   (select valor_texto FROM tp_localidades LOC, tp_codigos COD Where LOC.id_localidad = FAC.id_localidad and LOC.cg_localidad = COD.correlativo) Localidad,  (Select Sum(PAG.valor)   FROM cv403_documentos_pagados PAG  Where PAG.id_documento_cobrar = DPC.id_documento_cobrar And PAG.estado = 'A') Total_Pagado ,DPC.valor - (SELECT isnull(SUM(DPAG.valor),0)     FROM cv403_documentos_pagados DPAG where     DPAG.id_documento_cobrar = DPC.id_documento_cobrar     AND DPAG.estado = 'A') SALDO FROM    cv403_dctos_por_cobrar DPC,    cv403_facturas FAC,    cv403_vendedores CVV,tp_personas VEN,    tp_personas CL, tp_codigos TIPO Where DPC.cg_tipo_documento = TIPO.correlativo and FAC.id_factura = DPC.id_factura and DPC.estado= 'A' and FAC.estado='A' and FAC.fecha_factura between Convert(DateTime,'" + txtFechaInicio.Text.ToString().Trim() + "',120) AND Convert(DateTime,'" + txtFechaFinal.Text.ToString().Trim() + "',120) AND FAC.id_vendedor = CVV.id_vendedor and CVV.id_persona = VEN.id_persona and VEN.estado='A' and VEN.estado='A'  AND FAC.id_persona = CL.id_persona and  DPC.cg_estado_dcto in (7462,7461)    AND FAC.id_persona = " + iIdpersona + "    AND FAC.id_localidad = " + cmbLocalidad.SelectedValue.ToString() + "  order by VENDEDOR,CLIENTE,numero_factura,fecha_factura ";

            t_st_query = "SELECT TIPO.valor_texto,FAC.id_factura,DPC.id_documento_cobrar,DPC.cg_estado_dcto,FAC.fecha_factura,FAC.fecha_vcto, FAC.valor VALOR_FACTURA, FAC.direccion_factura,FAC.telefono_factura, FAC.ciudad_factura ciudad_factura, DPC.numero_documento numero_factura, VEN.apellidos + ' ' + isnull(VEN.nombreS,'') Vendedor,  CL.apellidos + ' ' + isnull(CL.nombreS,'') CLIENTE,   (select valor_texto FROM tp_localidades LOC, tp_codigos COD Where LOC.id_localidad = FAC.id_localidad and LOC.cg_localidad = COD.correlativo) Localidad,  (Select Sum(PAG.valor)   FROM cv403_documentos_pagados PAG Where PAG.id_documento_cobrar = DPC.id_documento_cobrar And PAG.estado = 'A') Total_Pagado ,DPC.valor - (SELECT isnull(SUM(DPAG.valor),0)     FROM cv403_documentos_pagados DPAG where     DPAG.id_documento_cobrar = DPC.id_documento_cobrar     AND DPAG.estado = 'A') SALDO FROM    cv403_dctos_por_cobrar DPC,    cv403_facturas FAC,    cv403_vendedores CVV,tp_personas VEN,    tp_personas CL, tp_codigos TIPO Where DPC.cg_tipo_documento = TIPO.correlativo and FAC.id_factura = DPC.id_factura and DPC.estado= 'A' and FAC.estado='A' and FAC.fecha_factura between Convert(DateTime,'" + txtFechaInicio.Text.ToString().Trim() + "',120) AND Convert(DateTime,'" + txtFechaFinal.Text.ToString().Trim() + "',120) AND FAC.id_vendedor = CVV.id_vendedor and CVV.id_persona = VEN.id_persona and VEN.estado='A' and VEN.estado='A'  AND FAC.id_persona = CL.id_persona and  DPC.cg_estado_dcto in (7462,7461)    AND FAC.id_vendedor= "+cmbVendedor.SelectedValue.ToString()+"     AND FAC.id_localidad = " + cmbLocalidad.SelectedValue.ToString() + "  order by VENDEDOR,CLIENTE,numero_factura,fecha_factura";

            x = conexion.GFun_Lo_Busca_Registro(dt, t_st_query);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
                foreach (DataRow row in dt.Rows)
                {//contar cuantos registros me devuelve el datatable
                    if (dt.Rows.Count > 0)
                    {
                        string sValorTexto = dt.Rows[iContador].ItemArray[0].ToString();
                        string sFechaFactura = dt.Rows[iContador].ItemArray[4].ToString();
                        string sFechaVcto = dt.Rows[iContador].ItemArray[5].ToString();
                        int iIdDocumentoCobrar = Convert.ToInt32(dt.Rows[iContador].ItemArray[2].ToString());
                        //agrego una fila vacia para comenzar a guardar los registros 
                        dgvCateraCobrar.Rows.Add("");
                        dgvCateraCobrar.Rows[iContador].Cells[1].Value = sFechaFactura;
                        dgvCateraCobrar.Rows[iContador].Cells[2].Value = sFechaVcto;

                        dt2 = new DataTable();
                        dt2.Clear();
                        t_st_query2 = "select DPA.id_pago,case when DPA.cg_tipo_documento = 9449 then 1 else 0 end  cuota,  sum(DPA.valor) valor_cuota ,TIPO.valor_Texto, DPA.cg_tipo_documento,DPA.numero_documento,DPA.fecha_vcto,PAG.fecha_pago,TIPO.codigo, DPAG.valor valor_pagado, case when  DPA.cg_tipo_documento = 9449 then (select DCTO.id_documento_cobrar FROM cv403_dctos_por_cobrar DCTO  where DPA.id_documento_pago = DCTO.id_documento_pago) end id_documento_cobrar FROM cv403_documentos_pagos DPA,cv403_documentos_pagados DPAG, cv403_pagos PAG,tp_codigos TIPO Where TIPO.correlativo = DPA.cg_tipo_documento and PAG.id_pago = DPAG.id_pago and DPAG.id_documento_cobrar = " + iIdDocumentoCobrar + " and PAG.id_pago = DPA.id_pago and  PAG.estado = 'A' and DPA.estado ='A' group by DPA.id_pago,DPA.cg_tipo_documento,DPA.numero_documento,TIPO.codigo, DPAG.valor, DPA.fecha_vcto,PAG.fecha_pago,DPA.id_documento_pago,TIPO.valor_Texto order by DPA.id_pago,PAG.fecha_pago ";

                        y = conexion.GFun_Lo_Busca_Registro(dt2, t_st_query2);
                        if (x == false)
                            MessageBox.Show("Error en la consulta");
                        else
                        {
                            foreach (DataRow row2 in dt2.Rows)
                            {
                                if (dt2.Rows.Count > 0)
                                {
                                    int iNumeroDocumento = Convert.ToInt32(dt2.Rows[0].ItemArray[5].ToString());
                                    string sFechaPago = dt2.Rows[0].ItemArray[7].ToString();
                                    string sCodigo = dt2.Rows[0].ItemArray[8].ToString();
                                    double dbValorPagado = Convert.ToDouble(dt2.Rows[0].ItemArray[9].ToString());

                                    dgvCateraCobrar.Rows[iContador].Cells[0].Value = iNumeroDocumento;
                                    dgvCateraCobrar.Rows[iContador].Cells[3].Value = sValorTexto + " " + Convert.ToString(iNumeroDocumento);
                                    dgvCateraCobrar.Rows[iContador].Cells[4].Value = sFechaPago;
                                    dgvCateraCobrar.Rows[iContador].Cells[5].Value = sCodigo;
                                    dgvCateraCobrar.Rows[iContador].Cells[6].Value = Convert.ToString(iNumeroDocumento);
                                    dgvCateraCobrar.Rows[iContador].Cells[7].Value = Convert.ToString(dbValorPagado);
                                    dgvCateraCobrar.Rows[iContador].Cells[14].Value = Convert.ToString(dbValorPagado);
                                    
                                }
                            }
                        }

                    }
                    iContador++;
                }

        }

        //FUNCION PARA LLENAR EL GRID cuando se haya seleccionado un cliente 
        private void llenarGridConCliente(string[] t_st_datos)
        {
            int iContador=0;
            //try
            //{
                string t_st_query = "";
                string t_st_query2 = "";
                dt = new DataTable();
                t_st_query = "SELECT TIPO.valor_texto,FAC.id_factura,DPC.id_documento_cobrar,DPC.cg_estado_dcto, "+
                    "FAC.fecha_factura,FAC.fecha_vcto, FAC.valor VALOR_FACTURA, FAC.direccion_factura,FAC.telefono_factura, "+
                    "FAC.ciudad_factura ciudad_factura, DPC.numero_documento numero_factura, VEN.apellidos + ' ' + "+
                    "isnull(VEN.nombreS,'') Vendedor, CL.apellidos + ' ' + isnull(CL.nombreS,'') CLIENTE,  "+
                    "(select valor_texto FROM tp_localidades LOC, tp_codigos COD Where LOC.id_localidad = FAC.id_localidad "+
                    "and LOC.cg_localidad = COD.correlativo) Localidad,  (Select Sum(PAG.valor)   FROM cv403_documentos_pagados PAG "+
                    "Where PAG.id_documento_cobrar = DPC.id_documento_cobrar And PAG.estado = 'A')  "+
                    "Total_Pagado ,DPC.valor - (SELECT isnull(SUM(DPAG.valor),0)     "+
                    "FROM cv403_documentos_pagados DPAG where     DPAG.id_documento_cobrar = DPC.id_documento_cobrar   "+
                    "AND DPAG.estado = 'A') SALDO FROM    cv403_dctos_por_cobrar DPC,    cv403_facturas FAC,  "+
                    "cv403_vendedores CVV,tp_personas VEN,    tp_personas CL, tp_codigos TIPO Where DPC.cg_tipo_documento = TIPO.correlativo  "+
                    "and FAC.id_factura = DPC.id_factura and DPC.estado= 'A' and FAC.estado='A' and FAC.fecha_factura between "+
                    "Convert(DateTime,'" + txtFechaInicio.Text.ToString().Trim() + "',120) AND Convert(DateTime,'" + txtFechaFinal.Text.ToString().Trim() + "',120)  "+
                    "AND FAC.id_vendedor = CVV.id_vendedor and CVV.id_persona = VEN.id_persona and VEN.estado='A'  "+
                    "and VEN.estado='A'  AND FAC.id_persona = CL.id_persona and  DPC.cg_estado_dcto in (7462,7461)  "+
                    "AND FAC.id_persona = " + iIdpersona + "    AND FAC.id_localidad = " + cmbLocalidad.SelectedValue.ToString() + "  "+
                    "order by VENDEDOR,CLIENTE,numero_factura,fecha_factura ";
                

                    x = conexion.GFun_Lo_Busca_Registro(dt, t_st_query);
                    if (x == false)
                        MessageBox.Show("Error en la consulta");
                    else
                        foreach (DataRow row in dt.Rows)
                        {//contar cuantos registros me devuelve el datatable
                            if (dt.Rows.Count > 0)
                            {
                                string sValorTexto = dt.Rows[iContador].ItemArray[0].ToString();
                                string sFechaFactura = dt.Rows[iContador].ItemArray[4].ToString();
                                string sFechaVcto = dt.Rows[iContador].ItemArray[5].ToString();
                                int iIdDocumentoCobrar = Convert.ToInt32(dt.Rows[iContador].ItemArray[2].ToString());
                                //agrego una fila vacia para comenzar a guardar los registros 
                                dgvCateraCobrar.Rows.Add("");
                                dgvCateraCobrar.Rows[iContador].Cells[1].Value = sFechaFactura;
                                dgvCateraCobrar.Rows[iContador].Cells[2].Value = sFechaVcto;

                                dt2= new DataTable();
                                dt2.Clear();
                                t_st_query2 = "select DPA.id_pago,case when DPA.cg_tipo_documento = 9449 then 1 else 0 end  cuota,  sum(DPA.valor) valor_cuota ,TIPO.valor_Texto, DPA.cg_tipo_documento,DPA.numero_documento,DPA.fecha_vcto,PAG.fecha_pago,TIPO.codigo, DPAG.valor valor_pagado, case when  DPA.cg_tipo_documento = 9449 then (select DCTO.id_documento_cobrar FROM cv403_dctos_por_cobrar DCTO  where DPA.id_documento_pago = DCTO.id_documento_pago) end id_documento_cobrar FROM cv403_documentos_pagos DPA,cv403_documentos_pagados DPAG, cv403_pagos PAG,tp_codigos TIPO Where TIPO.correlativo = DPA.cg_tipo_documento and PAG.id_pago = DPAG.id_pago and DPAG.id_documento_cobrar = "+iIdDocumentoCobrar+" and PAG.id_pago = DPA.id_pago and  PAG.estado = 'A' and DPA.estado ='A' group by DPA.id_pago,DPA.cg_tipo_documento,DPA.numero_documento,TIPO.codigo, DPAG.valor, DPA.fecha_vcto,PAG.fecha_pago,DPA.id_documento_pago,TIPO.valor_Texto order by DPA.id_pago,PAG.fecha_pago ";
                                
                                y = conexion.GFun_Lo_Busca_Registro(dt2, t_st_query2);
                                if (x == false)
                                    MessageBox.Show("Error en la consulta");
                                else
                                {
                                    foreach (DataRow row2 in dt2.Rows)
                                    {
                                        if (dt2.Rows.Count > 0)
                                        {
                                            int iNumeroDocumento = Convert.ToInt32(dt2.Rows[0].ItemArray[5].ToString());
                                            string sFechaPago = dt2.Rows[0].ItemArray[7].ToString();
                                            string sCodigo = dt2.Rows[0].ItemArray[8].ToString();
                                            double dbValorPagado = Convert.ToDouble(dt2.Rows[0].ItemArray[9].ToString());

                                            dgvCateraCobrar.Rows[iContador].Cells[0].Value = iNumeroDocumento;
                                            dgvCateraCobrar.Rows[iContador].Cells[3].Value = sValorTexto + " " + Convert.ToString(iNumeroDocumento);
                                            dgvCateraCobrar.Rows[iContador].Cells[4].Value = sFechaPago;
                                            dgvCateraCobrar.Rows[iContador].Cells[5].Value = sCodigo;
                                            dgvCateraCobrar.Rows[iContador].Cells[6].Value = Convert.ToString(iNumeroDocumento);
                                            dgvCateraCobrar.Rows[iContador].Cells[7].Value = Convert.ToString(dbValorPagado);
                                            dgvCateraCobrar.Rows[iContador].Cells[14].Value = Convert.ToString(dbValorPagado);
                                        }
                                    }
                                }

                            }
                            iContador++;
                        }

        }

        public void totales()
        {
            double dbTotalValorPagado = 0;
            double dbTotalRFuente = 0;
            double dbTotalRIva = 0;
            double dbTotalSobrante = 0;
            double dbTotalNCredito = 0;

            foreach (DataGridViewRow row in dgvCateraCobrar.Rows)   //recorrera toda la tabla donde se encuentren valores 
            {
                dbTotalValorPagado += Convert.ToDouble(row.Cells[7].Value); //lo que tenga la columna 5 ira sumando 
                dbTotalRFuente += Convert.ToDouble(row.Cells[9].Value);
                dbTotalRIva += Convert.ToDouble(row.Cells[10].Value);
                dbTotalSobrante += Convert.ToDouble(row.Cells[12].Value);
                dbTotalNCredito += Convert.ToDouble(row.Cells[13].Value);
            }

            txtVPagado.Text = dbTotalValorPagado.ToString();
            txtRFuente.Text = dbTotalRFuente.ToString();
            txtBaseComision.Text = Convert.ToString(dbTotalValorPagado + dbTotalRFuente);
            txtRIva.Text = dbTotalRIva.ToString();
            txtSobrante.Text = dbTotalSobrante.ToString();
            txtNCredito.Text = dbTotalNCredito.ToString();
        }

        public void exportarExcel(DataGridView tabla)
        {
            Microsoft.Office.Interop.Excel.Application archiExcel = new Microsoft.Office.Interop.Excel.Application(); //aqui creo un archivo en excel desde cero
            archiExcel.Application.Workbooks.Add(true);   //aqui agrego una hoja o una pagina 

            int indiceColumna = 0;


            foreach (DataGridViewColumn col in tabla.Columns)   // foreach...es el que recorre las columnas del dataGrid, tabla es el parametro antes creado
            {
                indiceColumna++;
                archiExcel.Cells[1, indiceColumna] = col.Name;   // en las celdas de excel. En si va recorriendo por las celdas
            }

            int indiceFila = 0;
            foreach (DataGridViewRow row in tabla.Rows)   // foreach...es el que recorre las filas del dataGrid, tabla es el parametro antes creado
            {
                indiceFila++;
                indiceColumna = 0;

                foreach (DataGridViewColumn col in tabla.Columns)   // foreach...es el que recorre las columnas del dataGrid, tabla es el parametro antes creado
                {
                    indiceColumna++;
                    archiExcel.Cells[indiceFila + 1, indiceColumna] = row.Cells[col.Name].Value;   // en las celdas de excel. En si va recorriendo por las celdas
                }

                archiExcel.Visible = true;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            exportarExcel(dgvCateraCobrar);
        }

       

    }
}
