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
    public partial class FReimpresionFacturas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases_Factura_Electronica.ClaseGenerarFacturaXml xml = new Clases_Factura_Electronica.ClaseGenerarFacturaXml();
        DataTable dtTipoEmpresa = new DataTable();
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dt2;
        DataTable dtlocalidad;
        DataTable dtVendedor;
        DataTable dtMoneda;

        string estado = "";
        string T_st_sql = "";
        string sIdentificac;
        bool x = false;
        bool z = false;
        string sNomCliente;
        int iIdFactur = 0;
        int INumFactura;
        int iNumeroDecimales;

        string sSql;
        string sDirectorio;
        DataTable dtConsulta;
        bool bRespuesta = false;

        public FReimpresionFacturas()
        {
            InitializeComponent();
        }

        private void FReimpresionFacturas_Load(object sender, EventArgs e)
        {
            //txtNSerie1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //txtFechaFinal.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //string[] t_st_datos = { "1", "adsdasdasd" };
            //llenarGrid(t_st_datos);
            llenarCombLocalidad();
            llenarVendedor();
            cmbMoned();
        }

        //CARGAR EL DIRECTORIO DONDE SE GUARDARAN LOS XML GENERADOS
        private bool buscarDirectorio()
        {
            try
            {
                sSql = "select codigo, nombres from cel_directorio where id_directorio = 1 and estado = 'A'";
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sDirectorio = dtConsulta.Rows[0].ItemArray[1].ToString();
                        return true;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe una configuracion de directorio para guardar los xml genereados.";
                        ok.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
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
                cmbMonedaFactura.llenar(dtMoneda, sql);
                cmbMonedaFactura.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaFacturaReimpFacturas ayuda1 = new Formularios.FAyudaFacturaReimpFacturas(Convert.ToInt32(cmbLocalidad.SelectedValue.ToString()));
            ayuda1.ShowDialog();

            INumFactura = ayuda1.NumeroFactura;
            txtNoFactura.Text = INumFactura.ToString();
            sIdentificac = ayuda1.Identificacion;
            sNomCliente = ayuda1.Nombrecliente;
            txtNombFactura.Text = sNomCliente;
            iIdFactur = ayuda1.IdFacturaCliente;
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtFecha.Text = "";
            txtNSerie1.Text = "";
            txtNSerie2.Text = "";
            txtCiudad.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtPorcientoDescuento.Text = "";
            txtFechaVcto.Text = "";
            txtNAut.Text = "";
            iIdFactur = 0;
            dgvReimpresionFactura.Rows.Clear();
        }

        private void btnCerrarCateraCobrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Grb_DatoInformeVentas.Enabled = false;
            //btnNuevoLocalidades.Text = "Nuevo";
            limpiarTodo();
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

            t_st_query = "Select F.id_persona,F.Direccion_Factura,F.Telefono_Factura,F.Ciudad_Factura,F.Fabricante,F.Referencia,F.Comentarios,CO.valor_texto as Moneda,FORPAGO.descripcion as Foma_Pago,F.fecha_vcto,F.Comentarios,F.Peso_Neto,F.Peso_Bruto,F.numero_exportacion,F.partida_arancelaria,F.idformulariossri,TIPOCO.descripcion as Formato,isnull(F.autorizacion,'') autorizacion,VENDE.codigo as Vendedor,CODI.valor_texto as Tipo_cliente,CP.Porcentaje_Dscto,CP.Porcentaje_IVA,PR.codigo,NP.nombre,UNIDAD.codigo Unidad,isnull(DP.comentario,'') Comentario,DP.precio_unitario,DP.Cantidad,Case when DP.precio_unitario=0 then 0 else round(100*DP.valor_Dscto/DP.precio_unitario,2) end Pct_Dscto,DP.valor_Dscto,DP.valor_ICE,DP.valor_IVA,DP.Comentario,DP.Id_Det_Pedido,F.fecha_factura,Case when PR.Expira = 1 Then 1 Else 0 End Expira From cv403_facturas F,cv403_facturas_pedidos FP,cv403_cab_pedidos CP,cv403_det_pedidos DP,cv401_productos PR,cv401_nombre_productos NP,tp_codigos UNIDAD, tp_codigos CO, tp_codigos CODI, cv403_vendedores VENDE,cv403_formas_pagos FORPAGO,vta_tipocomprobante TIPOCO Where F.id_factura = "+iIdFactur+" And F.id_factura = FP.id_factura And F.cg_moneda = CO.correlativo And CP.cg_tipo_cliente = CODI.correlativo And F.idtipocomprobante = TIPOCO.idtipocomprobante And F.id_forma_pago = FORPAGO.id_forma_pago And F.id_vendedor = VENDE.id_vendedor And FP.estado = 'A' And FP.Id_Pedido = CP.Id_Pedido And CP.Id_Pedido = DP.Id_Pedido And DP.estado = 'A' And DP.Cg_Unidad_Medida = UNIDAD.correlativo And DP.id_producto = PR.id_producto And PR.id_producto = NP.id_producto And NP.nombre_Interno = 1 And NP.estado = 'A' order by DP.Id_Det_Pedido ";

            x = conexion.GFun_Lo_Busca_Registro(dt, t_st_query);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
                foreach (DataRow row in dt.Rows)
                {//contar cuantos registros me devuelve el datatable
                    if (dt.Rows.Count > 0)
                    {
                        txtDireccion.Text = dt.Rows[0].ItemArray[1].ToString();
                        txtTelefono.Text = dt.Rows[0].ItemArray[2].ToString();
                        txtCiudad.Text = dt.Rows[0].ItemArray[3].ToString();
                        txtFabricante.Text = dt.Rows[0].ItemArray[4].ToString();
                        txtRefOt.Text = dt.Rows[0].ItemArray[5].ToString();
                        txtObser.Text = dt.Rows[0].ItemArray[6].ToString();
                        cmbMonedaFactura.Text = dt.Rows[0].ItemArray[7].ToString();
                        cmbTipoPago.Text = dt.Rows[0].ItemArray[8].ToString();
                        txtFechaVcto.Text = dt.Rows[0].ItemArray[9].ToString();
                        txtPesoNeto.Text = dt.Rows[0].ItemArray[11].ToString();
                        txtPesoBruto.Text = dt.Rows[0].ItemArray[12].ToString();
                        txtNExportacion.Text = dt.Rows[0].ItemArray[13].ToString();
                        txtPartidaArancelaria.Text = dt.Rows[0].ItemArray[14].ToString();
                        cmbAutSri.Text = dt.Rows[0].ItemArray[15].ToString();
                        cmbFormato.Text = dt.Rows[0].ItemArray[16].ToString();
                        //txtNAut.Text = dt.Rows[0].ItemArray[17].ToString();
                        cmbVendedor.Text = dt.Rows[0].ItemArray[18].ToString();
                        cmbTipoCliente.Text = dt.Rows[0].ItemArray[19].ToString();
                        txtPorcientoDescuento.Text = dt.Rows[0].ItemArray[20].ToString();
                        txtFecha.Text = dt.Rows[0].ItemArray[34].ToString();

                        string sCodigo = dt.Rows[iContador].ItemArray[22].ToString();
                        string sNombre = dt.Rows[iContador].ItemArray[23].ToString();
                        string sUnidad = dt.Rows[iContador].ItemArray[24].ToString();
                        double dbVUnidad = Convert.ToDouble(dt.Rows[iContador].ItemArray[26].ToString());
                        double dbPocenDescuento = Convert.ToDouble(dt.Rows[iContador].ItemArray[28].ToString());
                        double dbValorDescuento = Convert.ToDouble(dt.Rows[iContador].ItemArray[29].ToString());
                        double dbCantidad = Convert.ToDouble(dt.Rows[iContador].ItemArray[27].ToString());
                        //agrego una fila vacia para comenzar a guardar los registros 
                        dgvReimpresionFactura.Rows.Add("");
                        //agregamos los valores al GRID
                        dgvReimpresionFactura.Rows[iContador].Cells[0].Value = sCodigo;
                        dgvReimpresionFactura.Rows[iContador].Cells[1].Value = sNombre;
                        dgvReimpresionFactura.Rows[iContador].Cells[2].Value = sUnidad;
                        dgvReimpresionFactura.Rows[iContador].Cells[3].Value = dbVUnidad;
                        dgvReimpresionFactura.Rows[iContador].Cells[4].Value = dbPocenDescuento;
                        dgvReimpresionFactura.Rows[iContador].Cells[5].Value = dbValorDescuento;
                        dgvReimpresionFactura.Rows[iContador].Cells[6].Value = dbCantidad;

                        //para poner el formato de la celda
                        dgvReimpresionFactura.Columns[3].DefaultCellStyle.Format = "#,##0.00";
                        dgvReimpresionFactura.Columns[4].DefaultCellStyle.Format = "#,##0.00";
                        dgvReimpresionFactura.Columns[5].DefaultCellStyle.Format = "#,##0.00";
                        dgvReimpresionFactura.Columns[6].DefaultCellStyle.Format = "#,##0.00";

                        //para alinear los valores
                        dgvReimpresionFactura.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvReimpresionFactura.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvReimpresionFactura.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvReimpresionFactura.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvReimpresionFactura.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dt2 = new DataTable();
                        dt2.Clear();
                        t_st_query2 = "Select idformulariossri,estabRetencion1,ptoEmiRetencion1,autRetencion1 From vta_formulariossri Where idformulariossri = 18 ";

                        z = conexion.GFun_Lo_Busca_Registro(dt2, t_st_query2);
                        if (z == false)
                            MessageBox.Show("Error en la consulta");
                        else
                        {
                            foreach (DataRow row2 in dt2.Rows)
                            {
                                if (dt2.Rows.Count > 0)
                                {
                                    txtNSerie1.Text =dt2.Rows[0].ItemArray[1].ToString();
                                    txtNSerie2.Text = dt2.Rows[0].ItemArray[2].ToString();
                                    txtNAut.Text = dt2.Rows[0].ItemArray[3].ToString();
                                }
                            }
                        }

                    }
                    iContador++;
                }



        }

        public void totales()
        {
            //totakes dentro del GRID
            double dbValorUnidad = 0;
            double dbDescuento = 0;
            int iCantidad = 0;
            double dbVTotal = 0;
            int iContador = 0;

            foreach (DataGridViewRow row in dgvReimpresionFactura.Rows) 
            {
                dbValorUnidad = Convert.ToDouble(row.Cells[3].Value);
                dbDescuento = Convert.ToDouble(row.Cells[5].Value);
                iCantidad = Convert.ToInt32(row.Cells[6].Value);

                dbVTotal = (dbValorUnidad * iCantidad) - dbDescuento;

                dgvReimpresionFactura.Rows[iContador].Cells[7].Value = dbVTotal.ToString();
                iContador++;
            }

            //totales fuera del GRID
            double dbTotalValor = 0;
            double dbTotalDescuento = 0;
            double dbSubTotal = 0;
            double dbIva = 0;
            double dbTotalPagar = 0;
         

            foreach (DataGridViewRow row in dgvReimpresionFactura.Rows)   //recorrera toda la tabla donde se encuentren valores 
            {
                dbTotalValor += Convert.ToDouble(row.Cells[7].Value); //lo que tenga la columna 7 ira sumando 
                dbTotalDescuento += Convert.ToDouble(row.Cells[5].Value);
                //dbTotalValorNeto += Convert.ToDouble(row.Cells[7].Value);
                //dbTotalValorIva += Convert.ToDouble(row.Cells[8].Value);
                //dbTotalValorTotal += Convert.ToDouble(row.Cells[9].Value);

            }

            txtValor.Text = dbTotalValor.ToString("N2");
            txtDescuento.Text = dbTotalDescuento.ToString("N2");
            dbSubTotal = dbTotalValor - dbTotalDescuento;
            txtSubTotal.Text = dbSubTotal.ToString("N2");

            dbIva = dbSubTotal * Program.iva;
            txtIva.Text = dbIva.ToString("N2");

            dbTotalPagar = dbSubTotal + dbIva;
            txtTotalPagar.Text = dbTotalPagar.ToString("N2");
        }

        private void btnOKFactura_Click(object sender, EventArgs e)
        {
            dgvReimpresionFactura.Rows.Clear();
            if (txtNoFactura.Text == "" && txtNombFactura.Text == "")
            {
                MessageBox.Show("Favor elegir una Factura");
                btnFactura.Focus();
            }
            else
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridSinCliente(t_st_datos);
                txtIdentiCliente.Text = sIdentificac;
                txtNomCliente.Text = sNomCliente;
                cmbLocalidad2.Text= cmbLocalidad.Text;
                totales();
            }
        }

        private void btnGenerarXML_Click(object sender, EventArgs e)
        {
            if (iIdFactur == 0)
            {
                ok.LblMensaje.Text = "No ha seleccionado ninguna factura.";
                ok.ShowDialog();
            }

            else if (dgvReimpresionFactura.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No ha cargado los datos de la factura.";
                ok.ShowDialog();
            }

            else
            {
                if (rad2Decimales.Checked == true)
                {
                    iNumeroDecimales = 2;
                }

                else if (rad4Decimales.Checked == true)
                {
                    iNumeroDecimales = 4;
                }

                if (buscarDirectorio() == true)
                {
                    //xml.GSub_GenerarFacturaXML(iIdFactur, 0, "1", "1", @"C:", "FACTURA", 2, "elvis.geovanni@hotmail.com", "elvis.geovanni@hotmail.com");
                    xml.GSub_GenerarFacturaXML(iIdFactur, 0, "1", "1", sDirectorio, "FACTURA", iNumeroDecimales, "elvis.geovanni@hotmail.com", "elvis.geovanni@hotmail.com");
                }
            }
        }

        private void btnFormatoRide_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opcion en mantenimiento");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opcion en mantenimiento");
        }

    }
}
