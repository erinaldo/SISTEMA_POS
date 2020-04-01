using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.DashBoard
{
    public partial class frmDashBoard : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        string sSql;
        string sFecha;
        string sFechaInicio;
        string sFechaFinal;
        string sNombreDia;
        string sCantidadOrdenes;

        DataTable dtConsulta;

        DateTime fechaRecibida;

        ToolTip ttMensajeMesas = new ToolTip();

        bool bRespuesta;

        decimal dbVentasDia;
        decimal dbTransaccionesDia;
        decimal dbVentasMes;

        decimal dbVentasRango;
        decimal dbTransaccionesRango;

        int iDiaFinal;
        int iMes;
        int iAnio;
        int iDia;
        int iIntervalo;
        int iBandera;

        int[] iDiaVector;
        decimal[] dbValorVector;

        public frmDashBoard()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO - FECHA CORTE

        //FUNCION PARA CALCULAR LAS VENTAS DEL DÍA
        private void calcularVentasDiarias(string sFecha_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 10,2)), 0) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                        {
                            lblVentasDia.Text = "$ 0.00";
                        }

                        else
                        {
                            lblVentasDia.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                            dbVentasDia = Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                        }
                    }

                    else
                    {
                        lblVentasDia.Text = "$ 0.00";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION  PARA CONTAR LAS COMANDAS REALIZADAS
        private void calcularComandas(string sFecha_P)
        {
            try
            {            
                sSql = "";
                sSql += "select count (*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(dtConsulta.Rows[0]["cuenta"].ToString()) == 0)
                        {
                            lblTransaccionesDia.Text = "0";
                        }

                        else
                        {
                            lblTransaccionesDia.Text = dtConsulta.Rows[0]["cuenta"].ToString();
                            dbTransaccionesDia = Convert.ToDecimal(dtConsulta.Rows[0]["cuenta"].ToString());
                        }
                    }

                    else
                    {
                        lblTransaccionesDia.Text = "0";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CREAR LAS FECHAS
        private void crearFechasSemana(string sFecha_P)
        {
            try
            {
                sNombreDia = Convert.ToDateTime(sFecha_P).ToString("dddd").ToLower();
                //sNombreDia = DateTime.Now.ToString("dddd").ToLower();
                fechaRecibida = Convert.ToDateTime(sFecha_P);

                if (sNombreDia == "lunes")
                {
                    iDia = 1;
                }

                else if (sNombreDia == "martes")
                {
                    iDia = 2;
                }

                else if ((sNombreDia == "miercoles") || (sNombreDia == "miércoles"))
                {
                    iDia = 3;
                }

                else if (sNombreDia == "jueves")
                {
                    iDia = 4;
                }

                else if (sNombreDia == "viernes")
                {
                    iDia = 5;
                }

                else if ((sNombreDia == "sabado") || (sNombreDia == "sábado"))
                {
                    iDia = 6;
                }

                else if (sNombreDia == "domingo")
                {
                    iDia = 7;
                }

                //ALGORITMO
                if (iDia == 1)
                {
                    sFechaInicio = fechaRecibida.ToString("dd/MM/yyyy");
                    //sFechaFinal = fechaRecibida.AddDays(6).ToString("dd/MM/yyyy");
                }

                else if (iDia == 7)
                {
                    sFechaInicio = fechaRecibida.AddDays(-6).ToString("dd/MM/yyyy");
                    //sFechaFinal = fechaRecibida.ToString("dd/MM/yyyy");
                }

                else
                {
                    iIntervalo = iDia - 1;
                    sFechaInicio = fechaRecibida.AddDays(-iIntervalo).ToString("dd/MM/yyyy");
                    //iIntervalo = 7 - iDia;
                    //sFechaFinal = fechaRecibida.AddDays(iIntervalo).ToString("dd/MM/yyyy");
                }

                sFechaInicio = Convert.ToDateTime(sFechaInicio).ToString("yyyy/MM/dd");
                sFechaFinal = Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy/MM/dd");

                ttMensajeMesas.SetToolTip(pnlVentasSemana, "Fecha inicial: " + Convert.ToDateTime(sFechaInicio).ToString("dd-MM-yyyy") + Environment.NewLine + "Fecha final: " + Convert.ToDateTime(sFechaFinal).ToString("dd-MM-yyyy"));
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA SACAR LOS PROMEDIOS DE VENTAS
        private void promedioVentas()
        {
            try
            {
                if (dbVentasDia == 0)
                {
                    lblTicketPromedio.Text = "$ 0.00";
                }

                else
                {
                    lblTicketPromedio.Text = "$ " + (dbVentasDia / dbTransaccionesDia).ToString("N2");
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR LAS VENTAS DE LA SEMANA
        private void calcularVentasSemanaMes(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 10,2)), 0) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaInicio + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (iOp == 0)
                        {
                            if (Convert.ToDouble(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                            {
                                lblVentasSemana.Text = "$ 0.00";
                            }

                            else
                            {
                                lblVentasSemana.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                            }
                        }

                        else if (iOp == 1)
                        {
                            if (Convert.ToDouble(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                            {
                                lblVentasMes.Text = "$ 0.00";
                            }

                            else
                            {
                                lblVentasMes.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                            }
                        }
                    }

                    else
                    {
                        if (iOp == 0)
                        {
                            lblVentasSemana.Text = "$ 0.00";
                        }

                        if (iOp == 1)
                        {
                            lblVentasMes.Text = "$ 0.00";
                        }
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VALIDAR MES
        private void validarMesAnio(int iMes_P, int iAnio_P)
        {
            try
            {
                //if ((iMes_P == 1) || (iMes_P == 3) || (iMes_P == 5) || (iMes_P == 7) ||
                //    (iMes_P == 8) || (iMes_P == 10) || (iMes_P == 12))
                //{
                //    iDiaFinal = 31;
                //}

                //else if ((iMes_P == 4) || (iMes_P == 6) || (iMes_P == 9) || (iMes_P == 11))
                //{
                //    iDiaFinal = 30;
                //}

                //else
                //{
                //    if (iAnio_P % 4 == 0)
                //    {
                //        iDiaFinal = 29;
                //    }

                //    else
                //    {
                //        iDiaFinal = 28;
                //    }
                //}

                sFechaInicio = iAnio_P.ToString() + "-" + iMes_P.ToString().PadLeft(2, '0') + "-01";
                //sFechaFinal = iAnio_P + "-" + iMes_P.ToString().PadLeft(2, '0') + "-" + iDiaFinal.ToString().PadLeft(2, '0');
                sFechaFinal = Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy-MM.dd");

                ttMensajeMesas.SetToolTip(pnlVentasMes, "Fecha inicial: " + Convert.ToDateTime(sFechaInicio).ToString("dd-MM-yyyy") + Environment.NewLine + "Fecha final: " + Convert.ToDateTime(sFechaFinal).ToString("dd-MM-yyyy"));
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION  PARA CREAR LA LINEA DE TIEMPO
        private void crearLineaTiempo()
        {
            try
            {
                sSql = "";
                sSql += "select CP.fecha_pedido, datepart(dd, CP.fecha_pedido) as Dia," + Environment.NewLine;
                sSql += "isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 10,2)), 0) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaInicio + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1" + Environment.NewLine;
                sSql += "group by CP.fecha_pedido" + Environment.NewLine;
                sSql += "order by CP.fecha_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iDiaVector = new int[dtConsulta.Rows.Count];
                        dbValorVector = new decimal[dtConsulta.Rows.Count];

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            iDiaVector[i] = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                            dbValorVector[i] = Convert.ToDecimal(dtConsulta.Rows[i][2].ToString());
                        }

                        //chartVentasMes.Series["ventasMensuales"].Points.DataBindXY(iValorVector, iDiaVector);
                        chartVentasMes.Series["ventasMensuales"].Points.DataBindXY(iDiaVector, dbValorVector);
                    }

                    else
                    {
                        chartVentasMes.Series.Clear();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EJECUTAR EVENTOS
        private void cargarDashboard(string sFecha_P)
        {
            dbVentasDia = 0;
            dbTransaccionesDia = 0;
            calcularVentasDiarias(sFecha);
            calcularComandas(sFecha_P);
            promedioVentas();
            crearFechasSemana(sFecha_P);
            calcularVentasSemanaMes(0);
            fechaRecibida = Convert.ToDateTime(sFecha_P);
            validarMesAnio(fechaRecibida.Month, fechaRecibida.Year);
            calcularVentasSemanaMes(1);
            crearLineaTiempo();
        }

        #endregion

        #region FUNCIONES DEL USUARIO - RANGO DE FECHAS

        //FUNCION PARA CALCULAR LAS VENTAS EN UN RANGO  DE FECHAS
        private void calcularVentasRango(string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 10,2)), 0) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaDesde_P + "'" + Environment.NewLine;
                sSql += "and '" + sFechaHasta_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                        {
                            lblVentasRealizadasFechas.Text = "$ 0.00";
                        }

                        else
                        {
                            lblVentasRealizadasFechas.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                            dbVentasRango = Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                        }
                    }

                    else
                    {
                        lblVentasRealizadasFechas.Text = "$ 0.00";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION  PARA CONTAR LAS COMANDAS EN UN RANGO  DE FECHAS
        private void calcularComandasRango(string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                sSql = "";
                sSql += "select count (*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaDesde_P + "'" + Environment.NewLine;
                sSql += "and '" + sFechaHasta_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(dtConsulta.Rows[0]["cuenta"].ToString()) == 0)
                        {
                            lblTransaccionesFechas.Text = "0";
                        }

                        else
                        {
                            lblTransaccionesFechas.Text = dtConsulta.Rows[0]["cuenta"].ToString();
                            dbTransaccionesRango = Convert.ToDecimal(dtConsulta.Rows[0]["cuenta"].ToString());
                        }
                    }

                    else
                    {
                        lblTransaccionesFechas.Text = "0";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA SACAR LOS PROMEDIOS DE VENTAS EN UN RANGO DE FECHAS
        private void promedioVentasRango()
        {
            try
            {
                if (dbVentasRango == 0)
                {
                    lblTenedorPromedio.Text = "$ 0.00";
                }

                else
                {
                    lblTenedorPromedio.Text = "$ " + (dbVentasRango / dbTransaccionesRango).ToString("N2");
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA VER EL PLATILLO MAS VENDIDO
        private void platillosVendido(string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                sSql = "";
                sSql += "select NP.nombre, sum(DP.cantidad) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaDesde_P + "'" + Environment.NewLine;
                sSql += "and '" + sFechaHasta_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1" + Environment.NewLine;
                sSql += "group by NP.nombre" + Environment.NewLine;
                sSql += "order by cuenta desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        lblProductoMasVendido.Text = dtConsulta.Rows[0][0].ToString().Trim().ToUpper();
                        lblProductoMenosVendido.Text = dtConsulta.Rows[dtConsulta.Rows.Count - 1][0].ToString().Trim().ToUpper();
                    }

                    else
                    {
                        lblProductoMasVendido.Text = "NINGUNO";
                        lblProductoMenosVendido.Text = "NINGUNO";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA SACAR LOS TOTALES DE ORDENES
        private void consultarTotalComandas(string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where fecha_pedido between '" + sFechaDesde_P + "'" + Environment.NewLine;
                sSql += "and '" + sFechaHasta_P + "'" + Environment.NewLine;
                sSql += "and id_pos_origen_orden in (1, 2, 3)" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "group by id_pos_origen_orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //RECORRER MESAS
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][0].ToString() == "1")
                            {
                                iBandera = 1;
                                sCantidadOrdenes = dtConsulta.Rows[i][1].ToString();
                                break;
                            }
                        }

                        if (iBandera == 1)
                        {
                            lblMesasAtendidas.Text = sCantidadOrdenes;
                        }

                        else
                        {
                            lblMesasAtendidas.Text = "0";
                        }

                        iBandera = 0;
                        sCantidadOrdenes = "";

                        //RECORRER LLEVAR
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][0].ToString() == "2")
                            {
                                iBandera = 1;
                                sCantidadOrdenes = dtConsulta.Rows[i][1].ToString();
                                break;
                            }
                        }

                        if (iBandera == 1)
                        {
                            lblOrdenesLlevar.Text = sCantidadOrdenes;
                        }

                        else
                        {
                            lblOrdenesLlevar.Text = "0";
                        }

                        iBandera = 0;
                        sCantidadOrdenes = "";

                        //RECORRER DOMICILIO
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][0].ToString() == "3")
                            {
                                iBandera = 1;
                                sCantidadOrdenes = dtConsulta.Rows[i][1].ToString();
                                break;
                            }
                        }

                        if (iBandera == 1)
                        {
                            lblOrdenesDomicilio.Text = sCantidadOrdenes;
                        }

                        else
                        {
                            lblOrdenesDomicilio.Text = "0";
                        }
                    }

                    else
                    {
                        lblMesasAtendidas.Text = "0";
                        lblOrdenesLlevar.Text = "0";
                        lblOrdenesDomicilio.Text = "0";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EJECUTAR EVENTOS
        private void cargarDashboardRango(string sFechaDesde_P, string sFechaHasta_P)
        {
            iBandera = 0;
            sCantidadOrdenes = "";
            dbVentasRango = 0;
            dbTransaccionesRango = 0;
            calcularVentasRango(sFechaDesde_P, sFechaHasta_P);
            calcularComandasRango(sFechaDesde_P, sFechaHasta_P);
            promedioVentasRango();
            consultarTotalComandas(sFechaDesde_P, sFechaHasta_P);
            platillosVendido(sFechaDesde_P, sFechaHasta_P);
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE OFICINA LOCAL
        private void llenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) nombre_comercial, *" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbOficina.llenar(dtConsulta, sSql);
                cmbOficina2.llenar(dtConsulta, sSql);
                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion
        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();

            sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
            cargarDashboard(sFecha);

            sFechaInicio = Program.sFechaSistema.ToString("yyyy/MM/dd");
            sFechaFinal = Program.sFechaSistema.ToString("yyyy/MM/dd");
            cargarDashboardRango(sFechaInicio, sFechaFinal);

            tabDashboard.SelectedTab = tabFechaCorte;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            sFecha = Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy/MM/dd");
            cargarDashboard(sFecha);
            ttMensajeMesas.SetToolTip(pnlVentasDia, "FECHA: " + Convert.ToDateTime(sFecha).ToString("dd-MM-yyyy"));
        }

        private void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {
            sFechaInicio = Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy/MM/dd");
            sFechaFinal = Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy/MM/dd");

            if (Convert.ToDateTime(sFechaInicio) > Convert.ToDateTime(sFechaFinal))
            {
                ok.lblMensaje.Text = "La fecha inicial no puede ser superior a la fecha final.";
                ok.ShowDialog();
            }

            else
            {
                cargarDashboardRango(sFechaInicio, sFechaFinal);
            }
        }

        private void frmDashBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }        
    }
}
