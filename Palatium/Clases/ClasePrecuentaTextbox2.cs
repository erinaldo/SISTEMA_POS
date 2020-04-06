using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    public class ClasePrecuentaTextbox2
    {
        int iNumeroSecciones;
        
        DataTable dtPagosClase;
        DataTable dtSeccion;
        DataTable dtDomicilio;
        DataTable dtRecibir;
        DataTable dtPropinas;
        
        bool bRespuesta = false;
        
        string sOrigen;
        string sTexto;
        string sSql;
        string sDireccionPrecuenta;
        string sNombreCliente;
        string sPorcentajeDescuento;
        string sCorreoElectronico;

        double dbPorcentajeIva;
        double dbPorcentajeServicio;
        double dbPorcentajeDescuento;
        double dbCantidad;
        double dbPrecioUnitario;
        double dbSumaPrecio;
        double dbPrecioTotal;        
        double dbValorDescuento;
        public Double dbTotal;
        public Double dbTotalOrden;

        double dbSumaSubtotalConIva;
        double dbSumaSubtotalSinIva;
        double dbSumaDescuento;
        double dbSumaIVA;
        double dbSumaServicio;
        double dbSumaSubtotalNeto;
        double dbIva;
        double dbServicio;

        double dbPorcentajePropina;
        double dbTotalPropina;

        int iPagaIva;
        int iCuentaPropinas = 0;

        //VARIABLES PARA DESPLEGAR INFORMACION
        string sNombreProducto;
        string sCantidadProducto;
        

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases.ClaseManejoCaracteres caracteres = new Clases.ClaseManejoCaracteres();

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

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar los productos de la orden
        private void cargarProductos(int iIdPosOrden)
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
                sTexto += (dbPorcentajeServicio.ToString("N0") + "% SERVICIO:").PadRight(31, ' ') + dbSumaServicio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
            }

            sTexto += "----------------------------------------" + Environment.NewLine;
            dbTotal = dbSumaSubtotalNeto + dbSumaIVA + dbSumaServicio;
            dbTotalOrden = dbTotal;
        }

        private void completarPrecuenta(int op, int iOpDatos)
        {
            if (Program.iMostrarValoresPropina == 1)
            {
                iCuentaPropinas = valoresPropina();

                if (iCuentaPropinas == -1)
                {
                    return;
                }
            }            

            if ((op == 1) && (Program.iImprimirDatosFactura == 1))
            {
                sTexto += "         DATOS PARA LA FACTURA" + Environment.NewLine;
                sTexto += Environment.NewLine;
                sTexto += "Cliente:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "RUC/CI.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "Telf.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "Direcc.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                sTexto += "E-mail:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                if (Program.iMostrarValoresPropina == 1)
                {
                    if (iCuentaPropinas == 0)
                    {
                        sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += invocarPropinas();
                    }
                }

                else
                {
                    sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                }                

                sTexto += "SU OPINION ES IMPORTANTE:" + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
            }

            else if (op == 1)
            {
                sTexto += "         DATOS PARA LA FACTURA" + Environment.NewLine;
                sTexto += Environment.NewLine;
                sTexto += "Cliente:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "RUC/CI.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "Telf.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                sTexto += "Direcc.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                sTexto += "E-mail:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                if (Program.iMostrarValoresPropina == 1)
                {
                    if (iCuentaPropinas == 0)
                    {
                        sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += invocarPropinas();
                    }
                }

                else
                {
                    sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                }  

                sTexto += "SU OPINION ES IMPORTANTE:" + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
                sTexto += "".PadRight(40, '_') + Environment.NewLine;
            }

            else if (op == 2)
            {
                if (dtPagosClase.Rows.Count > 0)
                {
                    //sTexto += Environment.NewLine;
                    //sTexto += Environment.NewLine;
                    sTexto += "FORMAS DE PAGO:" + Environment.NewLine;
                    sTexto += Environment.NewLine;
                }

                Double dValor, dValorRecibido, dCambio, dSumaValores = 0, dCantidadDebida = 0;

                for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                {
                    dValor = Convert.ToDouble(dtPagosClase.Rows[i][1].ToString());
                    dValorRecibido = Convert.ToDouble(dtPagosClase.Rows[i][4].ToString());
                    dCantidadDebida = dCantidadDebida + dValor;

                    if (dValor != dValorRecibido)
                    {
                        dValor = dValorRecibido;
                    }

                    dSumaValores = dSumaValores + dValor;

                    sTexto += dtPagosClase.Rows[i][0].ToString().PadRight(20, ' ') + dValor.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                if (dtPagosClase.Rows.Count > 0)
                {

                    dCambio = Convert.ToDouble(dtPagosClase.Rows[0][2].ToString());
                    sTexto += "".PadRight(27, '-') + Environment.NewLine;
                    sTexto += "TOTAL RECIBIDO".PadRight(16, ' ') + dSumaValores.ToString("N2").PadLeft(11, ' ') + Environment.NewLine;
                    sTexto += "CANTIDAD DEBIDA".PadRight(16, ' ') + dCantidadDebida.ToString("N2").PadLeft(11, ' ') + Environment.NewLine;
                    sTexto += "CAMBIO".PadRight(16, ' ') + dCambio.ToString("N2").PadLeft(11, ' ') + Environment.NewLine;
                }

                if (iOpDatos == 1)
                {
                    sTexto += Environment.NewLine + Environment.NewLine;
                    sTexto += "         DATOS PARA LA FACTURA" + Environment.NewLine;
                    sTexto += Environment.NewLine;
                    sTexto += "Cliente:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                    sTexto += "RUC/CI.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                    sTexto += "Telf.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;
                    sTexto += "Direcc.:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                    sTexto += "E-mail:".PadRight(40, '_') + Environment.NewLine + Environment.NewLine;

                    if (Program.iMostrarValoresPropina == 1)
                    {
                        if (iCuentaPropinas == 0)
                        {
                            sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += invocarPropinas();
                        }
                    }

                    else
                    {
                        sTexto += "Propina:".PadRight(20, '_') + Environment.NewLine + Environment.NewLine;
                    }  

                    sTexto += "SU OPINION ES IMPORTANTE:" + Environment.NewLine;
                    sTexto += "".PadRight(40, '_') + Environment.NewLine;
                    sTexto += "".PadRight(40, '_') + Environment.NewLine;
                    sTexto += "".PadRight(40, '_') + Environment.NewLine;
                    sTexto += "".PadRight(40, '_') + Environment.NewLine;
                }

            }
        }

        public string llenarPrecuenta(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                dtPagosClase = new DataTable();
                this.dtRecibir = dtConsulta;
                this.dtPagosClase = dtPagos;

                sPorcentajeDescuento = Convert.ToDecimal(dtConsulta.Rows[0][6].ToString()).ToString("N2");
                dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0][58].ToString());
                dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0][9].ToString());

                dbTotal = 0;
                sTexto = "";
                
                string sNumeroOrden = dtConsulta.Rows[0][46].ToString();

                sOrigen = dtConsulta.Rows[0][40].ToString();
                sTexto += "----------------------------------------" + Environment.NewLine;
                sTexto += ("CUENTA No. " + dtConsulta.Rows[0][38].ToString()).PadLeft(26, ' ') + Environment.NewLine;
                sTexto += "----------------------------------------" + Environment.NewLine;
                sTexto += "ORDEN: " + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                sTexto += "No. Orden:".PadRight(11, ' ') + dtConsulta.Rows[0][46].ToString() + Environment.NewLine;
                sTexto += "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                sTexto += ("Fecha:".PadRight(8, ' ') + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("dd-MM-yyy")).PadRight(20, ' ') + " Hora: " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss") + Environment.NewLine;

                //if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                if (dtConsulta.Rows[0]["codigo_origen"].ToString() == "01")
                {
                    contarSecciones();

                    if (iNumeroSecciones > 1)
                    {
                        if (dtConsulta.Rows[0][57].ToString().Trim() == "")
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][57].ToString() + Environment.NewLine;
                            sTexto += dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
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

                else if (dtConsulta.Rows[0]["codigo_origen"].ToString() == "03")
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    sNombreCliente = dtConsulta.Rows[0]["cliente"].ToString().Trim();

                    sTexto += "CLIENTE:".PadRight(11, ' ');

                    if (sNombreCliente.Length > 29)
                    {
                        sTexto += caracteres.saltoLinea(sNombreCliente, 11);
                    }

                    else
                    {
                        sTexto += sNombreCliente + Environment.NewLine;
                    }

                    sTexto += "CI/RUC:".PadRight(11, ' ') + dtConsulta.Rows[0]["identificacion"].ToString() + Environment.NewLine;
                    sTexto += "TELEFONO:".PadRight(11, ' ') + dtConsulta.Rows[0]["telefono_domicilio"].ToString() + Environment.NewLine;
                    sTexto += "E-MAIL:".PadRight(11, ' ');

                    sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim().ToLower();

                    if (sCorreoElectronico.Length > 29)
                    {
                        sTexto += caracteres.saltoLinea(sCorreoElectronico, 11);
                    }

                    else
                    {
                        sTexto += sCorreoElectronico + Environment.NewLine;
                    }

                    sDireccionPrecuenta = "DIRECCION: ";

                    if (dtConsulta.Rows[0]["direccion"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["direccion"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["calle_principal"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["calle_principal"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["numero_vivienda"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["numero_vivienda"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["calle_interseccion"].ToString() != "")
                    {
                        sDireccionPrecuenta += "Y " + dtConsulta.Rows[0]["calle_interseccion"].ToString();
                    }

                    if (dtConsulta.Rows[0]["referencia"].ToString() != "")
                    {
                        sDireccionPrecuenta += ", " + dtConsulta.Rows[0]["referencia"].ToString();
                    }

                    sTexto += caracteres.saltoLinea(sDireccionPrecuenta, 0);
                }

                if ((dtConsulta.Rows[0][52].ToString().ToUpper() == "PAGADA") || (dtConsulta.Rows[0][52].ToString().ToUpper() == "CANCELADA"))
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "ESTADO DE LA ORDEN: " + dtConsulta.Rows[0][52].ToString().ToUpper() + Environment.NewLine;
                }

                ////AQUI CLASE PARA VALES FUNCIONARIOS, CONSUMO EMPLEADOS, MENU EXPRESS, CANJES

                //if ((Program.sIDPERSONA == null) || (Program.sIDPERSONA == "") || (Program.sIDPERSONA == Program.iIdPersona.ToString()))
                //{ }

                //else
                //{
                //    datosCliente();
                //}

                sTexto += "========================================" + Environment.NewLine;
                sTexto += "".PadRight(4, ' ') + "DESCRIPCION".PadRight(16, ' ') + "CANT".PadRight(7, ' ') + "PVP".PadRight(8, ' ') + "TOTAL".PadRight(5, ' ') + Environment.NewLine;
                sTexto += "========================================" + Environment.NewLine;

                cargarProductos(Convert.ToInt32(sIdOrden));
                sTexto += "TOTAL:".PadRight(31, ' ') + dbTotalOrden.ToString("N2").PadLeft(9, ' ') + Environment.NewLine + Environment.NewLine;

                if (dtConsulta.Rows[0]["codigo_origen"].ToString() == "03")
                {
                    completarPrecuenta(2, 0);
                }

                else
                {
                    if (sEstado == "Cancelada")
                    {
                        completarPrecuenta(0, 0);
                    }

                    else if (dtPagos.Rows.Count > 0)
                    {
                        if (sEstado == "Pagada")
                        {
                            completarPrecuenta(2, 0);
                        }

                        else
                        {
                            completarPrecuenta(2, 1);
                        }
                    }

                    else
                    {
                        completarPrecuenta(1, 0);
                    }
                }

                sTexto += Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";
                return sTexto;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return "";
            }

        //fin: { }
        }
    
        //PRIMERA SECCION DE LA PRECUENTA
        public string llenarPrecuentaDatos(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                dtPagosClase = new DataTable();
                this.dtPagosClase = dtPagos;
                dbTotalOrden = 0;
                sTexto = "";

                string sNumeroOrden = dtConsulta.Rows[0][46].ToString();
                dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0][58].ToString());
                dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0][9].ToString());

                sOrigen = dtConsulta.Rows[0][40].ToString();
                sTexto += "----------------------------------------" + Environment.NewLine;
                sTexto += ("CUENTA No. " + dtConsulta.Rows[0][38].ToString()).PadLeft(26, ' ') + Environment.NewLine;
                sTexto += "----------------------------------------" + Environment.NewLine;
                sTexto += "ORDEN: " + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                //sTexto += "Num. T.:".PadRight(9, ' ') + dtConsulta.Rows[0][46].ToString().PadRight(11, ' ') + "Msro: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                sTexto += "No. Orden:".PadRight(11, ' ') + dtConsulta.Rows[0][46].ToString() + Environment.NewLine;
                sTexto += "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                //sTexto += "Fecha:".PadRight(9, ' ') + dtConsulta.Rows[0][32].ToString().Substring(0, 10) + " Hora: " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss") + Environment.NewLine;
                sTexto += ("Fecha:".PadRight(8, ' ') + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("dd-MM-yyy")).PadRight(20, ' ') + " Hora: " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss") + Environment.NewLine;

                //if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                if (dtConsulta.Rows[0]["codigo_origen"].ToString() == "01")
                {
                    contarSecciones();

                    if (iNumeroSecciones > 1)
                    {
                        if (dtConsulta.Rows[0][57].ToString() == "")
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][57].ToString() + Environment.NewLine; sTexto += dtConsulta.Rows[0][50].ToString() + Environment.NewLine;
                        }
                    }

                    else
                    {
                        if (dtConsulta.Rows[0][57].ToString() == "")
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][57].ToString() + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += dtConsulta.Rows[0][49].ToString() + Environment.NewLine;
                        }
                    }

                    sTexto += ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine;
                }

                else if (dtConsulta.Rows[0]["codigo_origen"].ToString() == "03")
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                    sNombreCliente = dtConsulta.Rows[0]["cliente"].ToString().Trim();

                    sTexto += "CLIENTE:".PadRight(11, ' ');

                    if (sNombreCliente.Length > 29)
                    {
                        sTexto += caracteres.saltoLinea(sNombreCliente, 11);
                    }

                    else
                    {
                        sTexto += sNombreCliente + Environment.NewLine;
                    }

                    sTexto += "CI/RUC:".PadRight(11, ' ') + dtConsulta.Rows[0]["identificacion"].ToString() + Environment.NewLine;
                    sTexto += "TELEFONO:".PadRight(11, ' ') + dtConsulta.Rows[0]["telefono_domicilio"].ToString() + Environment.NewLine;
                    sTexto += "E-MAIL:".PadRight(11, ' ');

                    sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim().ToLower();

                    if (sCorreoElectronico.Length > 29)
                    {
                        sTexto += caracteres.saltoLinea(sCorreoElectronico, 11);
                    }

                    else
                    {
                        sTexto += sCorreoElectronico + Environment.NewLine;
                    }

                    sDireccionPrecuenta = "DIRECCION: ";

                    if (dtConsulta.Rows[0]["direccion"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["direccion"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["calle_principal"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["calle_principal"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["numero_vivienda"].ToString() != "")
                    {
                        sDireccionPrecuenta += dtConsulta.Rows[0]["numero_vivienda"].ToString() + " ";
                    }

                    if (dtConsulta.Rows[0]["calle_interseccion"].ToString() != "")
                    {
                        sDireccionPrecuenta += "Y " + dtConsulta.Rows[0]["calle_interseccion"].ToString();
                    }

                    if (dtConsulta.Rows[0]["referencia"].ToString() != "")
                    {
                        sDireccionPrecuenta += ", " + dtConsulta.Rows[0]["referencia"].ToString();
                    }

                    sTexto += caracteres.saltoLinea(sDireccionPrecuenta, 0);
                }

                if ((dtConsulta.Rows[0][52].ToString().ToUpper() == "PAGADA") || (dtConsulta.Rows[0][52].ToString().ToUpper() == "CANCELADA"))
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "ESTADO DE LA ORDEN: " + dtConsulta.Rows[0][52].ToString().ToUpper() + Environment.NewLine;
                }

                //AQUI CLASE PARA VALES FUNCIONARIOS, CONSUMO EMPLEADOS, MENU EXPRESS, CANJES

                //if ((Program.sIDPERSONA == null) || (Program.sIDPERSONA == "") || (Program.sIDPERSONA == Program.iIdPersona.ToString()))
                //{ }

                //else
                //{
                //    datosCliente();
                //}


                sTexto += "========================================" + Environment.NewLine;
                sTexto += "".PadRight(4, ' ') + "DESCRIPCION".PadRight(16, ' ') + "CANT".PadRight(7, ' ') + "PVP".PadRight(8, ' ') + "TOTAL".PadRight(5, ' ') + Environment.NewLine;
                sTexto += "========================================" + Environment.NewLine;

                cargarProductos(Convert.ToInt32(sIdOrden));                
                return sTexto;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return "";
            }
        }

        public string llenarDetallePrecuenta(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                sTexto = "";

                if (sEstado == "Cancelada")
                {
                    completarPrecuenta(0, 0);
                }

                else if (dtPagos.Rows.Count > 0)
                {
                    if (sEstado == "Pagada")
                    {
                        completarPrecuenta(2, 0);
                    }

                    else
                    {
                        completarPrecuenta(2, 1);
                    }
                }

                else
                {
                    completarPrecuenta(1, 0);
                }



                sTexto += Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";
                return sTexto;
                //goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return "";
            }

        //fin: { }
        }

        private void datosCliente()
        {
            try
            {
                //INSTRUCCION SQL PARA CONSULTAR LOS DATOS DEL CLIENTE
                sSql = "";
                sSql += "select identificacion, apellidos, nombres, correo_electronico," + Environment.NewLine;
                sSql += "codigo_alterno, id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where id_persona = " + Convert.ToInt32(Program.sIDPERSONA);

                dtDomicilio = new DataTable();
                dtDomicilio.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                if (bRespuesta == true)
                {
                    if (dtDomicilio.Rows.Count > 0)
                    {
                        sTexto += "----------------------------------------" + Environment.NewLine;
                        sNombreCliente = (dtDomicilio.Rows[0][2].ToString() + " " + dtDomicilio.Rows[0][1].ToString()).Trim();
                        sTexto += "CLIENTE:".PadRight(11, ' ');

                        if (sNombreCliente.Length > 29)
                        {
                            sTexto += caracteres.saltoLinea(sNombreCliente, 11);
                        }

                        else
                        {
                            sTexto += sNombreCliente + Environment.NewLine;
                        }

                        //sTexto += "CLIENTE:".PadRight(11, ' ') + (dtDomicilio.Rows[0][2].ToString() + " " + dtDomicilio.Rows[0][1].ToString()).Trim() + Environment.NewLine;
                        sTexto += "CI/RUC:".PadRight(11, ' ') + dtDomicilio.Rows[0][0].ToString() + Environment.NewLine;
                        sTexto += "TELEFONO:".PadRight(11, ' ') + dtDomicilio.Rows[0][4].ToString() + Environment.NewLine;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                //INSTRUCCION SQL PARA EXTRAER LA DIRECCIÓN DEL CLIENTE
                sDireccionPrecuenta = "";

                sSql = "";
                sSql += "select direccion, calle_principal, calle_interseccion," + Environment.NewLine;
                sSql += "numero_vivienda, referencia" + Environment.NewLine;
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
                        sDireccionPrecuenta = "DIRECCION: " + dtDomicilio.Rows[0][0].ToString().ToUpper() + " " + dtDomicilio.Rows[0][1].ToString().ToUpper() + " " + dtDomicilio.Rows[0][3].ToString().ToUpper();

                        if (dtDomicilio.Rows[0]["calle_interseccion"].ToString() != "")
                        {
                            sDireccionPrecuenta = sDireccionPrecuenta + " Y " + dtDomicilio.Rows[0][2].ToString().ToUpper();
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }


                //FUNCION PARA EXTRAER LA REFERENCIA DEL DOMICILIO

                sSql = "";
                sSql += "select top 1 " + conexion.GFun_St_esnulo() + "(referencia, '') referencia" + Environment.NewLine;
                sSql += "from tp_vw_direcciones" + Environment.NewLine;
                sSql += "where id_persona = " + Program.sIDPERSONA + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtDomicilio = new DataTable();
                dtDomicilio.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);


                if (bRespuesta == true)
                {
                    if (dtDomicilio.Rows.Count > 0)
                    {
                        if (dtDomicilio.Rows[0][0].ToString() != "")
                        {
                            sDireccionPrecuenta = sDireccionPrecuenta + ", " + dtDomicilio.Rows[0][0].ToString();
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                if (sDireccionPrecuenta.Length > 40)
                {
                    sTexto += caracteres.saltoLinea(sDireccionPrecuenta.Trim(), 0);
                }

                else
                {
                    sTexto += sDireccionPrecuenta.Trim() + Environment.NewLine;
                }

                goto fin;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            fin: { }
        }

        //FUNCION PARA CONSULTAR LOS PARAMETROS DE PROPINA
        private int valoresPropina()
        {
            try
            {
                sSql = "";
                sSql += "select valor from pos_porcentaje_propina" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_porcentaje_propina";

                dtPropinas = new DataTable();
                dtPropinas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPropinas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtPropinas.Rows.Count);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        private string invocarPropinas()
        {
            string sTextoPropina_P = "";
            sTextoPropina_P += "Propina:" + Environment.NewLine;

            for (int i = 0; i < dtPropinas.Rows.Count; i++)
            {
                dbPorcentajePropina = Convert.ToDouble(dtPropinas.Rows[i]["valor"].ToString()) / 100;
                dbTotalPropina = dbPorcentajePropina * dbTotalOrden;
                sTextoPropina_P += ((dbPorcentajePropina * 100).ToString() + "%:").PadRight(7, ' ') + "$ " + dbTotalPropina.ToString("N2").PadRight(8, ' ') + "_____" + Environment.NewLine;
            }

            sTextoPropina_P += "Otro: ".PadRight(7, ' ') + "".PadRight(15, '_') + Environment.NewLine + Environment.NewLine;
            return sTextoPropina_P;
        }
    }
}
