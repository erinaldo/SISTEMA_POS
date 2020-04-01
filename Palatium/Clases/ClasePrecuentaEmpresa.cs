using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    public class ClasePrecuentaEmpresa
    {
        private ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        private int iIdPedido;
        private int iPagaIva;

        private string sTexto;
        private string sSql;
        private string sNombreEmpresa;
        private string sNombreEmpleado;
        private string sNombreProducto;
        private string sCantidadProducto;

        private bool bRespuesta;

        private DataTable dtConsulta;

        private Decimal dbSumaSubtotalConIva;
        private Decimal dbSumaSubtotalSinIva;
        private Decimal dbSumaDescuento;
        private Decimal dbSumaIVA;
        private Decimal dbSumaServicio;
        private Decimal dbCantidad;
        private Decimal dbValorDescuento;
        private Decimal dbPrecioUnitario;
        private Decimal dbIva;
        private Decimal dbServicio;
        private Decimal dbSumaPrecio;
        private Decimal dbSumaSubtotalNeto;
        private Decimal dbTotal;

        public string llenarPrecuenta(int iIdPedido_P)
        {
            try
            {
                iIdPedido = iIdPedido_P;

                sTexto = "";
                sSql = "";

                sSql += "select * from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "ERROR";
                }
                                
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += ("CUENTA No. " + dtConsulta.Rows[0]["cuenta"].ToString()).PadLeft(26, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                sTexto += "TICKET CLIENTE EMPRESARIAL".PadLeft(33, ' ') + Environment.NewLine;

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sNombreEmpresa = dtConsulta.Rows[0]["cliente"].ToString();

                if (sNombreEmpresa.Length > 30)
                {
                    sNombreEmpresa = sNombreEmpresa.Substring(0, 30);
                }

                sNombreEmpleado = dtConsulta.Rows[0]["empleado"].ToString();

                if (sNombreEmpleado.Length > 30)
                {
                    sNombreEmpleado = sNombreEmpleado.Substring(0, 30);
                }

                sTexto += "EMPRESA:  " + sNombreEmpresa + Environment.NewLine;
                sTexto += "EMPLEADO: " + sNombreEmpleado + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "No. ORDEN: " + dtConsulta.Rows[0]["numero_pedido"].ToString() + Environment.NewLine;
                sTexto += ("Fecha:".PadRight(8, ' ') + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyy")).PadRight(20, ' ') + " Hora: " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("HH:mm:ss") + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "".PadRight(4, ' ') + "DESCRIPCION".PadRight(16, ' ') + "CANT".PadRight(7, ' ') + "PVP".PadRight(8, ' ') + "TOTAL".PadRight(5, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                dbSumaSubtotalConIva = 0;
                dbSumaSubtotalSinIva = 0;
                dbSumaDescuento = 0;
                dbSumaIVA = 0;
                dbSumaServicio = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dbCantidad = Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDecimal(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbIva = Convert.ToDecimal(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbServicio = Convert.ToDecimal(dtConsulta.Rows[i]["valor_otro"].ToString());
                    sNombreProducto = dtConsulta.Rows[i]["nombre"].ToString();
                    dbPrecioUnitario += dbIva;
                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());

                    if (dbValorDescuento != 0)
                    {
                        dbSumaDescuento += dbCantidad * dbValorDescuento;
                    }

                    if (iPagaIva == 0)
                    {
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
                    }

                    else
                    {
                        dbSumaSubtotalConIva += dbCantidad * dbPrecioUnitario;
                        dbSumaIVA += dbCantidad * dbIva;
                    }

                    dbSumaPrecio = dbCantidad * dbPrecioUnitario;
                    sCantidadProducto = dbCantidad.ToString("N0");

                    if (sNombreProducto.Length > 20)
                    {
                        sTexto += sNombreProducto.Substring(0, 20).PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        sTexto += sNombreProducto.Substring(20).PadRight(sNombreProducto.Substring(21).Length + 5, ' ') + Environment.NewLine;
                    }
                    else
                    {
                        sTexto += sNombreProducto.PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }

                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento;
                dbTotal = dbSumaSubtotalNeto;

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "TOTAL DEBIDO:" + dbTotal.ToString("N2").PadLeft(27, ' ') + Environment.NewLine + Environment.NewLine + "." + Environment.NewLine;
                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

    }
}
