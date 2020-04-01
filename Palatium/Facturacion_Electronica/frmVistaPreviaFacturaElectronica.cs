using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmVistaPreviaFacturaElectronica : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        Facturacion_Electronica.dsFacturaElectronica ds = new Facturacion_Electronica.dsFacturaElectronica();

        string sSql;
        string filename;
        string sCodigo;
        string sNumeroRUC;
        string sRazonSocial;
        string sNombreComercial;
        string sDireccionMatriz;
        string sDireccionSucursal;
        string sNumeroResolucion;
        string sObligadoContabilidad;
        string sClaveAcceso;
        string sFechaAutorizacion;
        string sHoraAutorizacion;
        string sAmbiente;
        string sEmision;
        string sFechaFactura;
        string sIdentificacion;
        string sDireccion;
        string sTelefono;
        string sCorreoElectronico;
        string sTipoOrden;
        string sNumeroOrden;
        string sNumeroCuenta;
        string sCajero;
        string sFormaPagoSRI;
        string sNumeroFactura;
        string sNombreCliente;
        string sNombreProducto;

        DataTable dtConsulta;
        DataTable dtDatos;

        Decimal dbPorcentajeDescuento;

        Decimal dbCantidad;
        Decimal dbPrecioUnitario;
        Decimal dbValorDescuento;
        Decimal dbPorcentajeIVA;
        Decimal dbValorIVA;
        Decimal dbValorICE;
        Decimal dbPropina;
        Decimal dbPrecioItemTotal;
        Decimal dbSumaIVA;
        Decimal dbSumaICE;
        Decimal dbSumaDescuento;
        Decimal dbTotal;
        Decimal dbSubtotalSinIva;
        Decimal dbSubtotalConIva;
        Decimal dbSubtotal;

        int iPagaIVA;
        int iPagaICE;
        int iIdFactura;

        bool bRespuesta;

        Byte[] Logo { get; set; }
        Byte[] Logo_Factura { get; set; }

        public frmVistaPreviaFacturaElectronica(DataTable dtDatos_R, string filename_R, long iIdFactura_P, Byte[] Logo_Factura_P)
        {
            this.dtDatos = dtDatos_R;
            this.filename = filename_R;
            this.iIdFactura = Convert.ToInt32(iIdFactura_P);
            this.Logo_Factura = Logo_Factura_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CREAR EL REPORTE
        private void crearReporte()
        {
            try
            {
                sSql = "";
                sSql += "select consumo_alimentos" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                if (dtConsulta.Rows[0]["consumo_alimentos"].ToString() == "1")
                {
                    if (llenarFacturaConsumoAlimentos() == false)
                    {
                        ok.LblMensaje.Text = "Error el generar el reporte electrónico.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    dsReporte ds = new dsReporte();
                    DataTable dt = ds.Tables["dtRIDE"];
                    dt.Clear();

                    sNumeroRUC = dtDatos.Rows[0]["numeroruc"].ToString();
                    sRazonSocial = dtDatos.Rows[0]["razonsocial"].ToString();
                    sNombreComercial = dtDatos.Rows[0]["nombrecomercial"].ToString();
                    sDireccionMatriz = dtDatos.Rows[0]["direccionmatriz"].ToString();
                    sDireccionSucursal = dtDatos.Rows[0]["direccionsucursal"].ToString();
                    sNumeroResolucion = dtDatos.Rows[0]["numeroresolucioncontribuyenteespecial"].ToString();
                    sObligadoContabilidad = dtDatos.Rows[0]["obligadollevarcontabilidad"].ToString();
                    sClaveAcceso = dtDatos.Rows[0]["clave_acceso"].ToString();
                    sFechaAutorizacion = dtDatos.Rows[0]["fecha_autorizacion"].ToString();
                    sHoraAutorizacion = dtDatos.Rows[0]["hora_autorizacion"].ToString();

                    if (sHoraAutorizacion.Trim() == "")
                    {
                        sHoraAutorizacion = DateTime.Now.ToString();
                    }

                    sAmbiente = dtDatos.Rows[0]["ambiente"].ToString();
                    sEmision = dtDatos.Rows[0]["emision"].ToString();
                    sFechaFactura = dtDatos.Rows[0]["fecha_factura"].ToString();
                    sIdentificacion = dtDatos.Rows[0]["identificacion"].ToString();
                    sDireccion = dtDatos.Rows[0]["direccion_factura"].ToString();
                    sTelefono = dtDatos.Rows[0]["telefono_factura"].ToString();
                    sCorreoElectronico = dtDatos.Rows[0]["email_factura"].ToString();
                    sTipoOrden = dtDatos.Rows[0]["tipo_orden"].ToString();
                    sNumeroOrden = dtDatos.Rows[0]["numero_orden"].ToString();
                    sNumeroCuenta = dtDatos.Rows[0]["numero_cuenta"].ToString();
                    sCajero = dtDatos.Rows[0]["cajero"].ToString();
                    sFormaPagoSRI = dtDatos.Rows[0]["descripcion_sri_forma_pago"].ToString();
                    sNumeroFactura = dtDatos.Rows[0]["estab"].ToString() + "-" + dtDatos.Rows[0]["ptoemi"].ToString() + "-" + dtDatos.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');
                    sNombreCliente = (dtDatos.Rows[0]["nombres"].ToString() + " " + dtDatos.Rows[0]["apellidos"].ToString()).Trim();

                    dbPorcentajeIVA = Convert.ToDecimal(dtDatos.Rows[0]["porcentaje_iva"].ToString());
                    dbPropina = Convert.ToDecimal(dtDatos.Rows[0]["propina"].ToString());

                    Logo = barcode(sClaveAcceso);

                    dbSumaIVA = 0;
                    dbSumaICE = 0;
                    dbSumaDescuento = 0;
                    dbTotal = 0;
                    dbSubtotalSinIva = 0;
                    dbSubtotalConIva = 0;
                    dbSubtotal = 0;

                    for (int i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        sCodigo = dtDatos.Rows[i]["codigo"].ToString();
                        sNombreProducto = dtDatos.Rows[i]["nombre"].ToString();

                        iPagaIVA = Convert.ToInt32(dtDatos.Rows[i]["paga_iva"].ToString());
                        iPagaICE = Convert.ToInt32(dtDatos.Rows[i]["paga_ice"].ToString());

                        dbCantidad = Convert.ToDecimal(dtDatos.Rows[i]["cantidad"].ToString());
                        dbPrecioUnitario = Convert.ToDecimal(dtDatos.Rows[i]["precio_unitario"].ToString());
                        dbValorDescuento = Convert.ToDecimal(dtDatos.Rows[i]["valor_dscto"].ToString());
                        dbValorIVA = Convert.ToDecimal(dtDatos.Rows[i]["valor_iva"].ToString());
                        dbValorICE = Convert.ToDecimal(dtDatos.Rows[i]["valor_ice"].ToString());

                        dbSumaDescuento += dbCantidad * dbValorDescuento;
                        dbPrecioItemTotal = dbCantidad * (dbPrecioUnitario - dbValorDescuento);

                        if (iPagaIVA == 1)
                        {
                            dbSumaIVA += dbCantidad * dbValorIVA;
                            dbSubtotalConIva += dbPrecioItemTotal;
                        }

                        else
                        {
                            dbSubtotalSinIva += dbPrecioItemTotal;
                        }

                        if (iPagaICE == 1)
                        {
                            dbSumaICE += dbCantidad * dbValorICE;
                        }

                        dt.Rows.Add();
                        dt.Rows[i]["cantidad"] = dbCantidad;
                        dt.Rows[i]["precio_unitario"] = dbPrecioUnitario;
                        dt.Rows[i]["valor_dscto"] = dbValorDescuento;
                        dt.Rows[i]["porcentaje_IVA"] = dbPorcentajeIVA;
                        dt.Rows[i]["valor_IVA"] = dbValorIVA;
                        dt.Rows[i]["Paga_IVA"] = iPagaIVA;
                        dt.Rows[i]["valor_ICE"] = dbValorICE;
                        dt.Rows[i]["Paga_ICE"] = iPagaICE;
                        dt.Rows[i]["propina"] = dbPropina;
                        dt.Rows[i]["codigo"] = sCodigo;
                        dt.Rows[i]["numeroruc"] = sNumeroRUC;
                        dt.Rows[i]["razonsocial"] = sRazonSocial;
                        dt.Rows[i]["nombrecomercial"] = sNombreComercial;
                        dt.Rows[i]["direccionmatriz"] = sDireccionMatriz;
                        dt.Rows[i]["direccionsucursal"] = sDireccionSucursal;
                        dt.Rows[i]["numeroresolucioncontribuyenteespecial"] = sNumeroResolucion;
                        dt.Rows[i]["obligadollevarcontabilidad"] = sObligadoContabilidad;
                        dt.Rows[i]["clave_acceso"] = sClaveAcceso;
                        dt.Rows[i]["fecha_autorizacion"] = sFechaAutorizacion;
                        dt.Rows[i]["hora_autorizacion"] = sHoraAutorizacion;
                        dt.Rows[i]["ambiente"] = sAmbiente;
                        dt.Rows[i]["emision"] = sEmision;
                        dt.Rows[i]["fecha_factura"] = sFechaFactura;
                        dt.Rows[i]["identificacion"] = sIdentificacion;
                        dt.Rows[i]["direccion_factura"] = sDireccion;
                        dt.Rows[i]["telefono_factura"] = sTelefono;
                        dt.Rows[i]["email_factura"] = sCorreoElectronico;
                        dt.Rows[i]["tipo_orden"] = sTipoOrden;
                        dt.Rows[i]["numero_orden"] = sNumeroOrden;
                        dt.Rows[i]["numero_cuenta"] = sNumeroCuenta;
                        dt.Rows[i]["cajero"] = sCajero;
                        dt.Rows[i]["descripcion_sri_forma_pago"] = sFormaPagoSRI;
                        dt.Rows[i]["numero_factura"] = sNumeroFactura;
                        dt.Rows[i]["nombre_cliente"] = sNombreCliente;
                        dt.Rows[i]["precio_total"] = dbPrecioItemTotal;
                        dt.Rows[i]["suma_iva"] = 0;
                        dt.Rows[i]["suma_ice"] = 0;
                        dt.Rows[i]["suma_descuento"] = 0;
                        dt.Rows[i]["total"] = 0;
                        dt.Rows[i]["nombre"] = sNombreProducto;
                        dt.Rows[i]["subtotal_con_iva"] = 0;
                        dt.Rows[i]["subtotal_sin_iva"] = 0;
                        dt.Rows[i]["suma_sin_impuestos"] = 0;
                        dt.Rows[i]["clave_acceso_barra"] = Logo;
                        dt.Rows[i]["logo_factura"] = Logo_Factura;
                    }

                    dbTotal = dbSubtotalConIva + dbSubtotalSinIva + dbSumaICE + dbSumaIVA;

                    dt.Rows[0]["suma_iva"] = dbSumaIVA;
                    dt.Rows[0]["suma_ice"] = dbSumaICE;
                    dt.Rows[0]["suma_descuento"] = dbSumaDescuento;
                    dt.Rows[0]["total"] = dbTotal;
                    dt.Rows[0]["subtotal_con_iva"] = dbSubtotalConIva;
                    dt.Rows[0]["subtotal_sin_iva"] = dbSubtotalSinIva;
                    dt.Rows[0]["suma_sin_impuestos"] = dbSubtotalConIva + dbSubtotalSinIva;

                    //rptFacturaEletronica reporte = ProcessingMode.Local;
                    rptVisor.LocalReport.ReportEmbeddedResource = "Palatium.Facturacion_Electronica.rptRIDE.rdlc";
                    //rptVisor.LocalReport.ReportPath = 
                    ReportDataSource reporte = new ReportDataSource("dsRide", dt);
                    rptVisor.LocalReport.DataSources.Clear();
                    rptVisor.LocalReport.DataSources.Add(reporte);
                    rptVisor.LocalReport.Refresh();
                    rptVisor.RefreshReport();
                }
            }

            catch (Exception)
            {
               
            }
        }

        //FUNCION PARA CREAR EL RIDE DE CONSUMO DE ALIMENTOA
        private bool llenarFacturaConsumoAlimentos()
        {
            try
            {
                dsReporte ds = new dsReporte();
                DataTable dt = ds.Tables["dtRIDE"];
                dt.Clear();

                sNumeroRUC = dtDatos.Rows[0]["numeroruc"].ToString();
                sRazonSocial = dtDatos.Rows[0]["razonsocial"].ToString();
                sNombreComercial = dtDatos.Rows[0]["nombrecomercial"].ToString();
                sDireccionMatriz = dtDatos.Rows[0]["direccionmatriz"].ToString();
                sDireccionSucursal = dtDatos.Rows[0]["direccionsucursal"].ToString();
                sNumeroResolucion = dtDatos.Rows[0]["numeroresolucioncontribuyenteespecial"].ToString();
                sObligadoContabilidad = dtDatos.Rows[0]["obligadollevarcontabilidad"].ToString();
                sClaveAcceso = dtDatos.Rows[0]["clave_acceso"].ToString();
                sFechaAutorizacion = dtDatos.Rows[0]["fecha_autorizacion"].ToString();
                sHoraAutorizacion = dtDatos.Rows[0]["hora_autorizacion"].ToString();

                if (sHoraAutorizacion.Trim() == "")
                {
                    sHoraAutorizacion = DateTime.Now.ToString();
                }

                sAmbiente = dtDatos.Rows[0]["ambiente"].ToString();
                sEmision = dtDatos.Rows[0]["emision"].ToString();
                sFechaFactura = dtDatos.Rows[0]["fecha_factura"].ToString();
                sIdentificacion = dtDatos.Rows[0]["identificacion"].ToString();
                sDireccion = dtDatos.Rows[0]["direccion_factura"].ToString();
                sTelefono = dtDatos.Rows[0]["telefono_factura"].ToString();
                sCorreoElectronico = dtDatos.Rows[0]["email_factura"].ToString();
                sTipoOrden = dtDatos.Rows[0]["tipo_orden"].ToString();
                sNumeroOrden = dtDatos.Rows[0]["numero_orden"].ToString();
                sNumeroCuenta = dtDatos.Rows[0]["numero_cuenta"].ToString();
                sCajero = dtDatos.Rows[0]["cajero"].ToString();
                sFormaPagoSRI = dtDatos.Rows[0]["descripcion_sri_forma_pago"].ToString();
                sNumeroFactura = dtDatos.Rows[0]["estab"].ToString() + "-" + dtDatos.Rows[0]["ptoemi"].ToString() + "-" + dtDatos.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');
                sNombreCliente = (dtDatos.Rows[0]["nombres"].ToString() + " " + dtDatos.Rows[0]["apellidos"].ToString()).Trim();

                dbPorcentajeIVA = Convert.ToDecimal(dtDatos.Rows[0]["porcentaje_iva"].ToString());
                dbPropina = Convert.ToDecimal(dtDatos.Rows[0]["propina"].ToString());

                Logo = barcode(sClaveAcceso);

                sCodigo = "CONSALI";
                sNombreProducto = "CONSUMO ALIMENTOS";

                dbSumaIVA = 0;
                dbSumaICE = 0;
                dbSumaDescuento = 0;
                dbTotal = 0;
                dbSubtotalSinIva = 0;
                dbSubtotalConIva = 0;
                dbSubtotal = 0;

                for (int i = 0; i < dtDatos.Rows.Count; i++)
                {
                    iPagaIVA = Convert.ToInt32(dtDatos.Rows[i]["paga_iva"].ToString());
                    iPagaICE = Convert.ToInt32(dtDatos.Rows[i]["paga_ice"].ToString());

                    dbCantidad = Convert.ToDecimal(dtDatos.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDecimal(dtDatos.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDecimal(dtDatos.Rows[i]["valor_dscto"].ToString());
                    dbValorIVA = Convert.ToDecimal(dtDatos.Rows[i]["valor_iva"].ToString());
                    dbValorICE = Convert.ToDecimal(dtDatos.Rows[i]["valor_ice"].ToString());

                    dbSumaDescuento += dbCantidad * dbValorDescuento;
                    dbPrecioItemTotal = dbCantidad * (dbPrecioUnitario - dbValorDescuento);

                    if (iPagaIVA == 1)
                    {
                        dbSumaIVA += dbCantidad * dbValorIVA;
                        dbSubtotalConIva += dbPrecioItemTotal;
                    }

                    else
                    {
                        dbSubtotalSinIva += dbPrecioItemTotal;
                    }

                    if (iPagaICE == 1)
                    {
                        dbSumaICE += dbCantidad * dbValorICE;
                    }
                }

                dt.Rows.Add();
                dt.Rows[0]["cantidad"] = "1";
                dt.Rows[0]["precio_unitario"] = dbSubtotalConIva + dbSubtotalSinIva;
                dt.Rows[0]["valor_dscto"] = dbValorDescuento;
                dt.Rows[0]["porcentaje_IVA"] = dbPorcentajeIVA;
                dt.Rows[0]["valor_IVA"] = dbValorIVA;
                dt.Rows[0]["Paga_IVA"] = iPagaIVA;
                dt.Rows[0]["valor_ICE"] = dbValorICE;
                dt.Rows[0]["Paga_ICE"] = iPagaICE;
                dt.Rows[0]["propina"] = dbPropina;
                dt.Rows[0]["codigo"] = sCodigo;
                dt.Rows[0]["numeroruc"] = sNumeroRUC;
                dt.Rows[0]["razonsocial"] = sRazonSocial;
                dt.Rows[0]["nombrecomercial"] = sNombreComercial;
                dt.Rows[0]["direccionmatriz"] = sDireccionMatriz;
                dt.Rows[0]["direccionsucursal"] = sDireccionSucursal;
                dt.Rows[0]["numeroresolucioncontribuyenteespecial"] = sNumeroResolucion;
                dt.Rows[0]["obligadollevarcontabilidad"] = sObligadoContabilidad;
                dt.Rows[0]["clave_acceso"] = sClaveAcceso;
                dt.Rows[0]["fecha_autorizacion"] = sFechaAutorizacion;
                dt.Rows[0]["hora_autorizacion"] = sHoraAutorizacion;
                dt.Rows[0]["ambiente"] = sAmbiente;
                dt.Rows[0]["emision"] = sEmision;
                dt.Rows[0]["fecha_factura"] = sFechaFactura;
                dt.Rows[0]["identificacion"] = sIdentificacion;
                dt.Rows[0]["direccion_factura"] = sDireccion;
                dt.Rows[0]["telefono_factura"] = sTelefono;
                dt.Rows[0]["email_factura"] = sCorreoElectronico;
                dt.Rows[0]["tipo_orden"] = sTipoOrden;
                dt.Rows[0]["numero_orden"] = sNumeroOrden;
                dt.Rows[0]["numero_cuenta"] = sNumeroCuenta;
                dt.Rows[0]["cajero"] = sCajero;
                dt.Rows[0]["descripcion_sri_forma_pago"] = sFormaPagoSRI;
                dt.Rows[0]["numero_factura"] = sNumeroFactura;
                dt.Rows[0]["nombre_cliente"] = sNombreCliente;
                dt.Rows[0]["precio_total"] = dbSubtotalConIva + dbSubtotalSinIva;
                dt.Rows[0]["suma_iva"] = 0;
                dt.Rows[0]["suma_ice"] = 0;
                dt.Rows[0]["suma_descuento"] = 0;
                dt.Rows[0]["total"] = 0;
                dt.Rows[0]["nombre"] = sNombreProducto;
                dt.Rows[0]["subtotal_con_iva"] = 0;
                dt.Rows[0]["subtotal_sin_iva"] = 0;
                dt.Rows[0]["suma_sin_impuestos"] = 0;
                dt.Rows[0]["clave_acceso_barra"] = Logo;
                dt.Rows[0]["logo_factura"] = Logo_Factura;

                dbTotal = dbSubtotalConIva + dbSubtotalSinIva + dbSumaICE + dbSumaIVA;

                dt.Rows[0]["suma_iva"] = dbSumaIVA;
                dt.Rows[0]["suma_ice"] = dbSumaICE;
                dt.Rows[0]["suma_descuento"] = dbSumaDescuento;
                dt.Rows[0]["total"] = dbTotal;
                dt.Rows[0]["subtotal_con_iva"] = dbSubtotalConIva;
                dt.Rows[0]["subtotal_sin_iva"] = dbSubtotalSinIva;
                dt.Rows[0]["suma_sin_impuestos"] = dbSubtotalConIva + dbSubtotalSinIva;

                //rptFacturaEletronica reporte = ProcessingMode.Local;
                rptVisor.LocalReport.ReportEmbeddedResource = "Palatium.Facturacion_Electronica.rptRIDE.rdlc";
                //rptVisor.LocalReport.ReportPath = 
                ReportDataSource reporte = new ReportDataSource("dsRide", dt);
                rptVisor.LocalReport.DataSources.Clear();
                rptVisor.LocalReport.DataSources.Add(reporte);
                rptVisor.LocalReport.Refresh();
                rptVisor.RefreshReport();


                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void frmVistaPreviaFacturaElectronica_Load(object sender, EventArgs e)
        {
            crearReporte();
        }

        //FUNCION DE CODIGO DE BARRAS
        private byte[] barcode(string sClaveAcceso_R)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = false;

            var ms = new MemoryStream();

            Bitmap imgOK = new Bitmap(codigo.Encode(BarcodeLib.TYPE.CODE128, sClaveAcceso_R.ToString(), Color.Black, Color.White, 2300, 350));

            imgOK.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }
    }
}
