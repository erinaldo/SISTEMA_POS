using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    public class ClasePrecuentaTextBox
    {
        string sTexto = "";
        DataTable dtPagosClase;

        string sSql = "";
        DataTable dtDomicilio;
        bool bRespuesta = false;
        string sOrigen;

        double dbPorcentajeIva;
        double dbPorcentajeServicio;
        double dbCantidad;
        double dbPrecioUnitario;
        double dbSumaPrecio;
        double dbPrecioTotal;
        double dbDescuento;
        double dbIva;
        double dbServicio;
        public Double dbTotal;

        public Double dbTotalOrden;

        //VARIABLES PARA DESPLEGAR INFORMACION
        string sNombreProducto;
        string sCantidadProducto;

        string sNombreCliente;
        string sDireccionCliente;
        string sCorreoCliente;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        Clases.ClaseManejoCaracteres caracteres = new ClaseManejoCaracteres();

        //Función para cargar los productos de la orden
        private void cargarProductos(int iIdPosOrden)
        {
            Program.iCuenta = 0;
            bool bRespuesta = false;
            ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

            string sQuery = "";
            sQuery = sQuery + "SELECT CP.id_pedido, NP.nombre, DP.cantidad," + Environment.NewLine;
            sQuery = sQuery + "DP.precio_unitario, DP.valor_dscto, DP.comentario, DP.id_det_pedido," + Environment.NewLine;
            sQuery = sQuery + "CASE WHEN (C.id_pos_cortesia) IS NULL THEN 0 ELSE 1 END AS cortesia," + Environment.NewLine;
            sQuery = sQuery + "CASE WHEN (CAN.id_pos_cancelacion_productos) IS NULL THEN 0 ELSE 1 END AS cancelacion," + Environment.NewLine;
            sQuery = sQuery + "DP.valor_iva, DP.valor_otro" + Environment.NewLine;
            sQuery = sQuery + "FROM cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
            sQuery = sQuery + "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
            sQuery = sQuery + "and DP.estado = 'A'" + Environment.NewLine;
            sQuery = sQuery + "and CP.estado in ('A', 'N') INNER JOIN" + Environment.NewLine;
            sQuery = sQuery + "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
            sQuery = sQuery + "and NP.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
            sQuery = sQuery + "pos_cortesia C ON DP.id_det_pedido = C.id_det_pedido LEFT OUTER JOIN" + Environment.NewLine;
            sQuery = sQuery + "pos_cancelacion_productos CAN ON DP.id_det_pedido = CAN.id_det_pedido" + Environment.NewLine;
            sQuery = sQuery + "WHERE CP.id_pedido = " + iIdPosOrden + Environment.NewLine;
            sQuery = sQuery + "order by DP.id_det_pedido";

            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sQuery);

            if (bRespuesta == true)
            {
                dbDescuento = 0;
                dbPrecioTotal = 0;
                dbIva = 0;
                dbServicio = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (dtConsulta.Rows[i].ItemArray[5].ToString() == "")
                    {
                        sNombreProducto = dtConsulta.Rows[i].ItemArray[1].ToString();
                    }
                    else
                    {
                        sNombreProducto = dtConsulta.Rows[i].ItemArray[5].ToString();
                    }

                    dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                    dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());

                    if (Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString()) != 0)
                    {
                        dbDescuento = dbDescuento + (dbCantidad * Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString()));
                    }

                    dbIva = dbIva + (dbCantidad * Convert.ToDouble(dtConsulta.Rows[i].ItemArray[9].ToString()));
                    dbServicio = dbServicio + (dbCantidad * Convert.ToDouble(dtConsulta.Rows[i].ItemArray[10].ToString()));
                    dbSumaPrecio = dbCantidad * dbPrecioUnitario;
                    dbPrecioTotal = dbPrecioTotal + dbSumaPrecio;

                    if (dbCantidad < 1)
                    {
                        sCantidadProducto = "1/2";
                    }

                    else
                    {
                        sCantidadProducto = dbCantidad.ToString("N0");
                    }

                    if (sNombreProducto.Length > 20)
                    {
                        sTexto = sTexto + sNombreProducto.Substring(0, 20).PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        sTexto = sTexto + sNombreProducto.Substring(20).PadRight((sNombreProducto.Substring(21).Length + 5), ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto = sTexto + sNombreProducto.PadRight(20, ' ') + sCantidadProducto.PadLeft(3, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(8, ' ') + dbSumaPrecio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                    }
                }


                //SECCION PARA MOSTRAR LOS TOTALES
                sTexto = sTexto + "========================================" + Environment.NewLine;

                //sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + "SUBTOTAL NETO:".PadRight(31, ' ') + dbPrecioTotal.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                //sTexto = sTexto + ("DESCUENTO " + dtConsulta.Rows[0].ItemArray[6].ToString() + "%:").PadRight(31, ' ') + dbDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "DESCUENTO:".PadRight(31, ' ') + dbDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                sTexto = sTexto + ("SUBTOTAL " + dbPorcentajeIva.ToString("N0") + "%:").PadRight(31, ' ') + (dbPrecioTotal - dbDescuento).ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + ("IVA " + dbPorcentajeIva.ToString("N0") + "%:").PadRight(31, ' ') + dbIva.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                //sTexto = sTexto + "TOTAL DE LA ORDEN:".PadRight(31, ' ') + (subtotal + iva).ToString("N2").PadLeft(9, ' ') + Environment.NewLine;

                if (dbPorcentajeServicio != 0)
                {
                    sTexto = sTexto + (dbPorcentajeServicio.ToString("N0") + "% SERVICIO").PadRight(31, ' ') + dbServicio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                }

                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                sTexto = sTexto + "CANTIDAD DEBIDA:".PadRight(31, ' ') + (dbPrecioTotal - dbDescuento + dbIva + dbServicio).ToString("N2").PadLeft(9, ' ') + Environment.NewLine + Environment.NewLine;
                dbTotal = dbPrecioTotal - dbDescuento + dbIva + dbServicio;
                dbTotalOrden = dbTotal;
            }

            else
            {
                ok.LblMensaje.Text = sQuery;
                ok.ShowDialog();
            }
        }

        private void completarPrecuenta(int op)
        {
            if (op == 1)
            {
                sTexto = sTexto +  "         Gracias por su visita" + Environment.NewLine;
                sTexto = sTexto + "         DATOS PARA LA FACTURA" +Environment.NewLine;
                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + "NOMBRES:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "APELLIDOS:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "CEDULA O RUC:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "CORREO E.:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "DIRECCION:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "TELEFONO:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + "SU OPINION ES IMPORTANTE:" + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
                sTexto = sTexto + ".................................... " + Environment.NewLine;
            }

            else if (op == 2)
            {
                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + "FORMAS DE PAGO:" + Environment.NewLine;
                sTexto = sTexto + Environment.NewLine;

                Double dValor, dValorRecibido, dCambio, dSumaValores = 0, dCantidadDebida = 0;

                for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                {   
                    dValor = Convert.ToDouble(dtPagosClase.Rows[i].ItemArray[1].ToString());
                    dValorRecibido = Convert.ToDouble(dtPagosClase.Rows[i].ItemArray[4].ToString());
                    dCantidadDebida = dCantidadDebida + dValor;

                    if (dValor != dValorRecibido)
                    {
                        dValor = dValorRecibido;
                    }

                    dSumaValores = dSumaValores + dValor;

                    sTexto = sTexto + dtPagosClase.Rows[i].ItemArray[0].ToString().PadRight(20, ' ') + dValor.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                dCambio = Convert.ToDouble(dtPagosClase.Rows[0].ItemArray[2].ToString());
                sTexto = sTexto + "".PadRight(27, '-') + Environment.NewLine;
                sTexto = sTexto + "TOTAL RECIBIDO".PadRight(20, ' ') + dSumaValores.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                sTexto = sTexto + "CANTIDAD DEBIDA".PadRight(20, ' ') + dCantidadDebida.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                sTexto = sTexto + "CAMBIO".PadRight(20, ' ') + dCambio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
            }
        }

        public string llenarPrecuenta(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                dtPagosClase = new DataTable();
                this.dtPagosClase = dtPagos;

                    Double subtotal = 0;
                    Double iva = 0;
                    Double servicio = 0;
                    Double descuento = 0;
                    sTexto = "";

                    dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[58].ToString());
                    dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[9].ToString());

                    string sNumeroOrden = dtConsulta.Rows[0].ItemArray[46].ToString();

                    for (int j = 0; j < dtConsulta.Rows.Count; j++)
                    {
                        if ((dtConsulta.Rows[j].ItemArray[42].ToString() != "1") && (dtConsulta.Rows[j].ItemArray[43].ToString() != "1"))
                        {
                            //subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[5].ToString()) - Convert.ToDouble(dtConsulta.Rows[j].ItemArray[7].ToString())));
                            subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[5].ToString())));
                            iva = iva + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[10].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));
                            servicio = servicio + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[11].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));
                            descuento = descuento + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[7].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));
                        }
                    }

                    sOrigen = dtConsulta.Rows[0].ItemArray[40].ToString();

                    sTexto = sTexto + "Mesero: " + Program.nombreMesero + "         Estacion: 2" + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto = sTexto + "# de Cuenta: " + dtConsulta.Rows[0].ItemArray[38].ToString() + "          Orden:  " + dtConsulta.Rows[0].ItemArray[46].ToString() + Environment.NewLine;
                    sTexto = sTexto + "Tipo de Orden: " + sOrigen + Environment.NewLine;

                    //AQUI CLASE PARA VALES FUNCIONARIOS, CONSUMO EMPLEADOS, MENU EXPRESS, CANJES

                    if ((Program.sIDPERSONA == null) || (Program.sIDPERSONA == "") || (Program.sIDPERSONA == "64930"))
                    { }

                    else
                    {                        
                        //AQUI CREAMOS NUESTRA INSTRUCCION DE BUSQUEDA EN SQL
                        sSql = "";
                        sSql += "select identificacion, apellidos, nombres," + Environment.NewLine;
                        sSql += "correo_electronico, codigo_alterno, id_persona" + Environment.NewLine;
                        sSql += "from tp_personas where id_persona = '" + Program.sIDPERSONA + "'";

                        dtDomicilio = new DataTable();
                        dtDomicilio.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtDomicilio.Rows.Count > 0)
                            {
                                sNombreCliente = (dtDomicilio.Rows[0].ItemArray[2].ToString() + " " + dtDomicilio.Rows[0].ItemArray[1].ToString()).Trim();
                                sCorreoCliente = dtDomicilio.Rows[0].ItemArray[3].ToString();

                                sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;

                                sTexto = sTexto + "CLIENTE:".PadRight(11, ' ');

                                if (sNombreCliente.Length <= 29)
                                {
                                    sTexto = sTexto + sNombreCliente + Environment.NewLine;
                                }

                                else
                                {
                                    sTexto = sTexto + caracteres.saltoLinea(sNombreCliente, 11);
                                }

                                sTexto = sTexto + "CI/RUC:".PadRight(11, ' ') + dtDomicilio.Rows[0].ItemArray[0].ToString() + Environment.NewLine;
                                sTexto = sTexto + "TELEFONO:".PadRight(11, ' ') + dtDomicilio.Rows[0].ItemArray[4].ToString() + Environment.NewLine;

                                //iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString());

                                sSql = "";
                                sSql += "select isnull(direccion, '') direccion, isnull(calle_principal, '') calle_principal," + Environment.NewLine;
                                sSql += "isnull(calle_interseccion, '') calle_interseccion," + Environment.NewLine;
                                sSql += "isnull(numero_vivienda, '') numero_vivienda, isnull(referencia, '') referencia" + Environment.NewLine;
                                sSql += "from tp_direcciones" + Environment.NewLine;
                                sSql += "where id_persona = " + Program.sIDPERSONA + Environment.NewLine;
                                sSql += "and estado = 'A'";
                                
                                dtDomicilio = new DataTable();
                                dtDomicilio.Clear();
                                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                                if (bRespuesta == true)
                                {
                                    if (dtDomicilio.Rows.Count > 0)
                                    {
                                        sDireccionCliente = dtDomicilio.Rows[0].ItemArray[1].ToString() + " " + dtDomicilio.Rows[0].ItemArray[3].ToString() + " " + dtDomicilio.Rows[0].ItemArray[2].ToString();

                                        sTexto = sTexto + "DIRECCION:".PadRight(11, ' ');
                                        
                                        if (sDireccionCliente.Length <= 29)
                                        {
                                            sTexto = sTexto + sDireccionCliente.ToString() + Environment.NewLine;
                                        }

                                        else
                                        {
                                            sTexto = sTexto + caracteres.saltoLinea(sDireccionCliente, 11);
                                        }

                                        //FUNCION PARA INSERTAR LA REFERENCIA
                                        sDireccionCliente = "";
                                        sDireccionCliente = dtDomicilio.Rows[0].ItemArray[4].ToString();

                                        if (sDireccionCliente != "")
                                        {
                                            sTexto = sTexto + "REFERENCIA:";

                                            if (sDireccionCliente.Length <= 29)
                                            {
                                                sTexto = sTexto + sDireccionCliente.ToString() + Environment.NewLine;
                                            }

                                            else
                                            {
                                                sTexto = sTexto + caracteres.saltoLinea(sDireccionCliente, 11);
                                            }
                                        }
                                            
                                        //sSql = "select top 1 referencia from tp_vw_direcciones where id_persona = " + Program.sIDPERSONA + " and estado = 'A'";
                                        //dtDomicilio = new DataTable();

                                        //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                                        //if (bRespuesta == true)
                                        //{
                                        //    if (dtDomicilio.Rows.Count > 0)
                                        //    {
                                        //        if (dtDomicilio.Rows[0].ItemArray[0].ToString() != "")
                                        //        {
                                        //            int iLongitudCadena = dtDomicilio.Rows[0].ItemArray[0].ToString().Length;
                                        //            int iLongitudRecorte = iLongitudCadena;
                                        //            int iLineas = iLongitudCadena / 40;

                                        //            if (iLongitudCadena < 40)
                                        //            {
                                        //                //iLineas = 1;
                                        //                sTexto = sTexto + dtDomicilio.Rows[0].ItemArray[0].ToString().PadRight(40, ' ') + Environment.NewLine;
                                        //            }

                                        //            else if (iLongitudCadena % 40 != 0)
                                        //            {
                                        //                iLineas++;
                                        //                for (int i = 0; i < iLineas; i++)
                                        //                {
                                        //                    if (iLongitudRecorte >= 40)
                                        //                    {
                                        //                        sTexto = sTexto + dtDomicilio.Rows[0].ItemArray[0].ToString().Substring((i * 40), 40) + Environment.NewLine;
                                        //                        iLongitudRecorte = iLongitudRecorte - 40;
                                        //                    }

                                        //                    else
                                        //                    {
                                        //                        sTexto = sTexto + dtDomicilio.Rows[0].ItemArray[0].ToString().Substring((i * 40), iLongitudRecorte) + Environment.NewLine;
                                        //                    }


                                        //                }

                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            sTexto = sTexto + "SIN REFERENCIA" + Environment.NewLine;
                                        //        }
                                        //    }

                                        //}
                                        //else
                                        //{
                                        //    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                                        //    sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    sTexto = sTexto + "Error en la consulta de direcion" + Environment.NewLine;
                                        //    sTexto = sTexto + "Informe al administrador" + Environment.NewLine;
                                        //    sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    //sTexto = sTexto + "Error".PadLeft(15, ' ') + Environment.NewLine;
                                        //    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                                        //}


                                    }

                                }
                            }
                        }
                    }

                

                    //AQUI INCRUSTAR DATOS DEL CLIENTE


                    sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto = sTexto + "          >> ORDEN " + sEstado.ToUpper() + " <<" + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto = sTexto + "DESCRIPCION".PadRight(21, ' ') + "CANT.".PadRight(6, ' ') + "V.UNI.".PadRight(8, ' ') + "TOTAL" + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;

                    cargarProductos(Convert.ToInt32(sIdOrden));

                    sTexto = sTexto + Environment.NewLine;
                    sTexto = sTexto + "     Creado:   " + dtConsulta.Rows[0].ItemArray[32].ToString() + Environment.NewLine;


                    //if ((sEstado == "Abierta") || (sEstado == "Pre-Cuenta") && (sOrigen != "DOMICILIOS"))
                    if ((sEstado == "Pre-Cuenta") && (sOrigen != "DOMICILIOS"))
                    {
                        completarPrecuenta(1);
                    }

                    else if (sEstado == "Pagada")
                    {
                        sTexto = sTexto + "     Pagada:   " + dtConsulta.Rows[0].ItemArray[32].ToString() + Environment.NewLine;
                        completarPrecuenta(2);
                    }

                    else if (sEstado == "Cancelada")
                    {
                        completarPrecuenta(0);
                    }

                    sTexto = sTexto + Environment.NewLine + Environment.NewLine + ".";
                    return sTexto;

            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return "";
            }

        }
    }
}
