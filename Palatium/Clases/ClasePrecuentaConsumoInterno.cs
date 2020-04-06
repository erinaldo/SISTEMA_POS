using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClasePrecuentaConsumoInterno
    {
        private ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseManejoCaracteres caracteres = new ClaseManejoCaracteres();

        private int iIdPedido;
        private int iPagaIva;
        private int iPagaServicio;

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

        public string llenarPrecuenta(int iIdPedido_P, int iEspacioFirma_P, int iEtiqueta_P)
        {
            try
            {
                //  iEtiqueta_P
                //  1. CONSUMO INTERNO
                //  2. CONSUMO EMPLEADOS
                //  3. VALE FUNCIONARIOS
                //  4. CORTESIAS
                //  5. CANJES

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

                if (iEtiqueta_P == 1)
                    sTexto += "TICKET DE CONSUMO INTERNO".PadLeft(31, ' ') + Environment.NewLine;
                else if (iEtiqueta_P == 2)
                    sTexto += "TICKET DE CONSUMO EMPLEADO".PadLeft(33, ' ') + Environment.NewLine;
                else if (iEtiqueta_P == 3)
                    sTexto += "TICKET DE VALE FUNCIONARIO".PadLeft(33, ' ') + Environment.NewLine;
                else if (iEtiqueta_P == 4)
                    sTexto += "TICKET DE CORTESIA".PadLeft(29, ' ') + Environment.NewLine;
                else if (iEtiqueta_P == 5)
                    sTexto += "TICKET DE CANJES".PadLeft(28, ' ') + Environment.NewLine;

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                sNombreEmpleado = dtConsulta.Rows[0]["cliente"].ToString();

                if (sNombreEmpleado.Length > 30)
                {
                    sNombreEmpleado = sNombreEmpleado.Substring(0, 30);
                }                

                if ((iEtiqueta_P == 1) || (iEtiqueta_P == 2))
                {
                    sTexto += "EMPLEADO: ";

                    if (sNombreEmpleado.Length > 30)
                        sTexto += caracteres.saltoLinea(sNombreEmpleado, 10) + Environment.NewLine;
                    else
                        sTexto += sNombreEmpleado + Environment.NewLine;                    
                }

                else if ((iEtiqueta_P == 3) || (iEtiqueta_P == 4) || (iEtiqueta_P == 5))
                {
                    sTexto += "CLIENTE: ";

                    if (sNombreEmpleado.Length > 31)
                        sTexto += caracteres.saltoLinea(sNombreEmpleado, 9) + Environment.NewLine;
                    else
                        sTexto += sNombreEmpleado + Environment.NewLine;
                }

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

                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());
                    iPagaServicio = Convert.ToInt32(dtConsulta.Rows[i]["paga_servicio"].ToString());

                    if (iPagaServicio == 1)
                        dbPrecioUnitario += dbIva + dbServicio - dbValorDescuento;
                    else
                        dbPrecioUnitario += dbIva - dbValorDescuento;

                    if (dbValorDescuento != 0)
                        dbSumaDescuento += dbCantidad * dbValorDescuento;

                    if (iPagaIva == 0)
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
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

                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento + dbSumaServicio;
                dbTotal = dbSumaSubtotalNeto;

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "TOTAL DEBIDO:" + dbTotal.ToString("N2").PadLeft(27, ' ') + Environment.NewLine;

                string sMotivo = dtConsulta.Rows[0]["comentarios"].ToString().Trim().ToUpper();

                if (sMotivo != "")
                {
                    sTexto += Environment.NewLine + "MOTIVO:" + Environment.NewLine;

                    if (sMotivo.Length > 40)
                    {
                        int iSuma = 0;

                        for (int i = 0; i < sMotivo.Length; i = i + 40)
                        {
                            sTexto += sMotivo.Substring(i, 40) + Environment.NewLine;
                            iSuma += 40;

                            if (sMotivo.Length - iSuma <= 40)
                            {
                                sTexto += sMotivo.Substring(iSuma);
                                break;
                            }
                        }
                    }
                    
                    else
                    {
                        sTexto += sMotivo + Environment.NewLine;
                    }
                }

                if (iEspacioFirma_P == 1)
                {
                    sTexto += Environment.NewLine + Environment.NewLine;
                    sTexto += "".PadLeft(10, ' ') + "".PadRight(20, '_') + Environment.NewLine;
                    sTexto += "".PadLeft(10, ' ') + "  RECIBÍ  CONFORME" + Environment.NewLine;
                    sTexto += Environment.NewLine + Environment.NewLine + "." + Environment.NewLine;
                }

                else
                {
                    sTexto += Environment.NewLine + "." + Environment.NewLine;
                }


                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }
    }
}
