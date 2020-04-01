using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClasePrecuentaMesas
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtRecibir;

        //VARIABLES PARA DESPLEGAR INFORMACION
        string sSql;
        string sNombreProducto;
        string sCantidadProducto;
        string sTexto;
        string sPorcentajeDescuento;

        double dbPorcentajeIva;
        double dbPorcentajeServicio;
        double dbPorcentajeDescuento;
        double dbCantidad;
        double dbPrecioUnitario;
        double dbSumaPrecio;
        double dbPrecioTotal;
        double dbDescuento;
        double dbIva;
        double dbServicio;
        public Double dbTotal;
        public Double dbTotalOrden;

        double dbSumaSubtotalConIva;
        double dbSumaSubtotalSinIva;
        double dbSumaDescuento;
        double dbSumaIVA;
        double dbSumaServicio;
        double dbSumaSubtotalNeto;
        double dbValorDescuento;

        int iNumeroSecciones;
        int iPagaIva;

        DataTable dtConsulta;
        DataTable dtSeccion;

        bool bRespuesta;

        //FUNCION PARA CONTAR EL NUMERO DE SECCIONES 
        private void contarSecciones()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtSeccion = new DataTable();
                dtSeccion.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSeccion, sSql);

                if (bRespuesta == true)
                {
                    iNumeroSecciones = Convert.ToInt32(dtSeccion.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA MOSTRAR LOS ITEMS DE LA ORDEN
        private void llenarItemsOrden()
        {
            try
            {
                Program.iCuenta = 0;
                dbSumaSubtotalConIva = 0;
                dbSumaSubtotalSinIva = 0;
                dbSumaDescuento = 0;
                dbSumaIVA = 0;
                dbSumaServicio = 0;
                dbPorcentajeDescuento = Convert.ToDouble(dtRecibir.Rows[0]["porcentaje_dscto"].ToString());

                for (int i = 0; i < dtRecibir.Rows.Count; i++)
                {
                    if (dtRecibir.Rows[i]["comentario"].ToString() == "")
                    {
                        sNombreProducto = dtRecibir.Rows[i]["nombre"].ToString();
                    }
                    else
                    {
                        sNombreProducto = dtRecibir.Rows[i]["comentario"].ToString();
                    }

                    dbCantidad = Convert.ToDouble(dtRecibir.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDouble(dtRecibir.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDouble(dtRecibir.Rows[i]["valor_dscto"].ToString());
                    dbIva = Convert.ToDouble(dtRecibir.Rows[i]["valor_iva"].ToString());
                    dbServicio = Convert.ToDouble(dtRecibir.Rows[i]["valor_otro"].ToString());

                    iPagaIva = Convert.ToInt32(dtRecibir.Rows[i]["paga_iva"].ToString());

                    if (dbValorDescuento != 0)
                    {
                        dbSumaDescuento += (dbCantidad * dbValorDescuento);
                    }

                    if (iPagaIva == 0)
                    {
                        //dbSumaSubtotalSinIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
                    }

                    else
                    {
                        //dbSumaSubtotalConIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        dbSumaSubtotalConIva += dbCantidad * dbPrecioUnitario;
                        dbSumaIVA += dbCantidad * dbIva;
                    }

                    if (dbPorcentajeServicio != 0)
                    {
                        if (dbCantidad < 1)
                        {
                            dbSumaServicio += dbServicio;
                        }

                        else
                        {
                            dbSumaServicio += dbCantidad * dbServicio;
                        }
                    }

                    dbSumaPrecio = dbCantidad * dbPrecioUnitario;
                    //dbPrecioTotal = dbPrecioTotal + dbSumaPrecio;

                    if (dbCantidad < 1)
                    {
                        sCantidadProducto = "1/2";
                    }

                    else
                    {
                        sCantidadProducto = dbCantidad.ToString();
                    }

                    if (sNombreProducto.Length > 20)
                    {
                        sTexto += sNombreProducto.Substring(0, 20).PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        sTexto += sNombreProducto.Substring(20).PadRight((sNombreProducto.Substring(21).Length + 5), ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += sNombreProducto.PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }

                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento;

                //SECCION PARA MOSTRAR LOS TOTALES
                sTexto += "========================================" + Environment.NewLine;
                sTexto += ("SUBTOTAL " + dbPorcentajeIva.ToString("N0") + "%:").PadRight(31, ' ') + dbSumaSubtotalConIva.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto += "SUBTOTAL 0%:".PadRight(31, ' ') + dbSumaSubtotalSinIva.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;

                if (dbPorcentajeDescuento == 0)
                {
                    if (dbSumaDescuento > 0)
                    {
                        sTexto += ("DESCUENTO: ").PadRight(31, ' ') + dbSumaDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }

                else
                {
                    if (dbSumaDescuento > 0)
                    {
                        sTexto += ("DESCUENTO " + sPorcentajeDescuento + "%:").PadRight(31, ' ') + dbSumaDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }

                sTexto += "----------------------------------------" + Environment.NewLine;
                sTexto += "SUBTOTAL NETO:".PadRight(31, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto += ("IVA " + dbPorcentajeIva.ToString("N0") + "%:").PadRight(31, ' ') + dbSumaIVA.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;

                if (dbPorcentajeServicio != 0)
                {
                    sTexto += (dbPorcentajeServicio.ToString("N0") + "% SERVICIO").PadRight(31, ' ') + dbSumaServicio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                }

                sTexto += "----------------------------------------" + Environment.NewLine;
                dbTotal = dbSumaSubtotalNeto + dbSumaIVA + dbSumaServicio;
                dbTotalOrden = dbTotal;
                sTexto += "TOTAL A PAGAR:".PadRight(31, ' ') + dbTotalOrden.ToString("N2").PadLeft(9, ' ');
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        public string llenarPrecuenta(DataTable dtConsulta, int iIdOrden)
        {
            try
            {
                this.dtConsulta = new DataTable();
                this.dtConsulta = dtConsulta;
                this.dtRecibir = dtConsulta.Copy();

                sPorcentajeDescuento = Convert.ToDouble(dtConsulta.Rows[0][6].ToString()).ToString("N2");
                dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0][58].ToString());
                dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0][9].ToString());

                //sOrigen = dtConsulta.Rows[0][40].ToString();
                sTexto = "";
                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                sTexto = sTexto + ("CUENTA No. " + dtConsulta.Rows[0][38].ToString()).PadLeft(26, ' ') + Environment.NewLine;
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                //sTexto = sTexto + "ORDEN: " + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                //sTexto = sTexto + "Num. T.:".PadRight(9, ' ') + dtConsulta.Rows[0][46].ToString().PadRight(11, ' ') + "Msro: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                sTexto = sTexto + "No. Orden:".PadRight(11, ' ') + dtConsulta.Rows[0][46].ToString() + Environment.NewLine;
                sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                sTexto = sTexto + ("Fecha:".PadRight(8, ' ') + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("dd-MM-yyy")).PadRight(20, ' ') + " Hora: " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss") + Environment.NewLine;

                if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                {
                    contarSecciones();

                    //sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + Environment.NewLine;

                    if (iNumeroSecciones > 1)
                    {
                        if (dtConsulta.Rows[0][57].ToString().Trim() == "")
                        {
                            sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                        }

                        else
                        {
                            sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][57].ToString() + Environment.NewLine;
                            sTexto = sTexto + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                        }

                        sTexto += ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine;
                    }

                    else
                    {
                        if (dtConsulta.Rows[0][57].ToString().Trim() == "")
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString().PadRight(21, ' ') + "Num. Personas: " + dtConsulta.Rows[0][34].ToString() + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                            sTexto += ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine;
                        }        
                    }
                }

                
                sTexto = sTexto + "========================================" + Environment.NewLine;
                sTexto = sTexto + "".PadRight(4, ' ') + "DESCRIPCION".PadRight(16, ' ') + "CANT".PadRight(7, ' ') + "PVP".PadRight(8, ' ') + "TOTAL".PadRight(5, ' ') + Environment.NewLine;
                sTexto = sTexto + "========================================" + Environment.NewLine;

                llenarItemsOrden();
                return sTexto;

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }
    }
}
