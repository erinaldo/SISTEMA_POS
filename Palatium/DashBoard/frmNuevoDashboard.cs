using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Palatium.DashBoard
{
    public partial class frmNuevoDashboard : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sSql;
        string sFecha;
        string sFechaInicio;
        string sFechaFinal;

        DataTable dtConsulta;

        bool bRespuesta;

        decimal dFacturado;
        decimal dConsumo;
        decimal dFacturadoMes;

        int iDiaFinal;
        int iMes;
        int iAnio;

        public frmNuevoDashboard()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CREAR LA LISTA DE MESES
        private void listaMeses()
        {
            try
            {
                DataTable dtMeses = new DataTable();
                dtMeses.Columns.Add("id_mes");
                dtMeses.Columns.Add("descripcion");

                DataRow row = dtMeses.NewRow();
                row["id_mes"] = "1";
                row["descripcion"] = "ENERO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "2";
                row["descripcion"] = "FEBRERO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "3";
                row["descripcion"] = "MARZO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "4";
                row["descripcion"] = "ABRIL";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "5";
                row["descripcion"] = "MAYO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "6";
                row["descripcion"] = "JUNIO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "7";
                row["descripcion"] = "JULIO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "8";
                row["descripcion"] = "AGOSTO";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "9";
                row["descripcion"] = "SEPTIEMBRE";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "10";
                row["descripcion"] = "OCTUBRE";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "11";
                row["descripcion"] = "NOVIEMBRE";
                dtMeses.Rows.Add(row);

                row = dtMeses.NewRow();
                row["id_mes"] = "12";
                row["descripcion"] = "DICIEMBRE";
                dtMeses.Rows.Add(row);

                cmbMes.DisplayMember = "descripcion";
                cmbMes.ValueMember = "id_mes";
                cmbMes.DataSource = dtMeses;

                cmbMes.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR LAS VENTAS DEL DÍA
        private void calcularVentasDiarias(string sFecha_P, int iOp)
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

                if (iOp == 1)
                {
                    sSql += "and O.genera_factura = 1";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                        {
                            if (iOp == 1)
                            {
                                lblTotalFacturado.Text = "$ 0.00";
                            }

                            else
                            {
                                lblTotalConsumido.Text = "$ 0.00";
                            }
                        }

                        else
                        {
                            if (iOp == 1)
                            {
                                lblTotalFacturado.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                                dFacturado = Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                            }

                            else
                            {
                                lblTotalConsumido.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                                dConsumo = Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                            }
                        }
                    }

                    else
                    {
                        if (iOp == 1)
                        {
                            lblTotalFacturado.Text = "$ 0.00";
                        }

                        else
                        {
                            lblTotalConsumido.Text = "$ 0.00";
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

        //FUNCION  PARA CONTAR LAS COMANDAS REALIZADAS
        private void calcularComandas(string sFecha_P, int iOp)
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

                if (iOp == 1)
                {
                    sSql += "and O.genera_factura = 1";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dtConsulta.Rows[0]["cuenta"].ToString()) == 0)
                        {
                            if (iOp == 1)
                            {
                                lblCantidadComandasFacturadas.Text = "0";
                            }

                            else
                            {
                                lblCantidadComandasConsumidas.Text = "0";
                            }
                        }

                        else
                        {
                            if (iOp == 1)
                            {
                                lblCantidadComandasFacturadas.Text = dtConsulta.Rows[0]["cuenta"].ToString();
                            }

                            else
                            {
                                lblCantidadComandasConsumidas.Text = dtConsulta.Rows[0]["cuenta"].ToString();
                            }
                        }
                    }

                    else
                    {
                        if (iOp == 1)
                        {
                            lblCantidadComandasFacturadas.Text = "0";
                        }

                        else
                        {
                            lblCantidadComandasConsumidas.Text = "0";
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
        
        //FUNCION PARA SACAR LOS PROMEDIOS DE VENTAS
        private void promedioVentas()
        {
            try
            {
                if (dFacturado == 0)
                {
                    lblPromedioFacturado.Text = "$ 0.00";
                }

                else
                {
                    lblPromedioFacturado.Text = "$ " + (dFacturado / Convert.ToDecimal(lblCantidadComandasFacturadas.Text.Trim())).ToString("N2");
                }

                if (dConsumo == 0)
                {
                    lblPromedioConsumo.Text = "$ 0.00";
                }

                else
                {
                    lblPromedioConsumo.Text = "$ " + (dConsumo / Convert.ToDecimal(lblCantidadComandasConsumidas.Text.Trim())).ToString("N2");
                }

                if (dFacturadoMes == 0)
                {
                    lblPromedioFacturadoMes.Text = "$ 0.00";
                }

                else
                {
                    lblPromedioFacturadoMes.Text = "$ " + (dFacturadoMes / Convert.ToDecimal(lblCantidadComandasFacturadasMes.Text.Trim())).ToString("N2");
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA SACAR LOS PROMEDIOS DE VENTAS AL MES
        private void promedioVentasMes()
        {
            try
            {      
                if (dFacturadoMes == 0)
                {
                    lblPromedioFacturadoMes.Text = "$ 0.00";
                }

                else
                {
                    lblPromedioFacturadoMes.Text = "$ " + (dFacturadoMes / Convert.ToDecimal(lblCantidadComandasFacturadasMes.Text.Trim())).ToString("N2");
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }        

        //FUNCION PARA VER EL NUMERO DE PERSONAS FACTURADAS
        private void numeroPersonas(string sFecha_P)
        {
            try
            {
                sSql = "";
                sSql += "select sum(CP.numero_personas) numero_personas" + Environment.NewLine;
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
                    lblPersonasAtendidas.Text = dtConsulta.Rows[0]["numero_personas"].ToString();
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

        //FUNCION PARA CONTAR LAS MESAS ATENDIDAS FACTURADAS
        private void mesasAtendidas(string sFecha_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFecha_P + "'" + Environment.NewLine;
                sSql += "and O.genera_factura = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_mesa is not null";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    lblMesasAtendidas.Text = dtConsulta.Rows[0]["cuenta"].ToString();
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

        //FUNCION PARA CONTAR LOS PLATILLOS VENDIDOS FACTURADOS
        private void platillosVendidos(string sFecha_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
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
                    lblPlatillosVendidos.Text = dtConsulta.Rows[0]["cuenta"].ToString();
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

        //FUNCION PARA VER EL PLATILLO MAS VENDIDO
        private void platilloMasVendido(string sFecha_P)
        {
            try
            {
                sSql = "";
                //sSql += "select NP.nombre, count (*) cuenta" + Environment.NewLine;
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
                sSql += "and CP.fecha_pedido = '" + sFecha_P + "'" + Environment.NewLine;
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
                        lblPlatilloMasVendido.Text = dtConsulta.Rows[0][0].ToString().Trim().ToUpper();
                    }

                    else
                    {
                        lblPlatilloMasVendido.Text = "NINGUNO";
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
            dFacturado = 0;
            dConsumo = 0;

            calcularVentasDiarias(sFecha_P, 1);
            calcularComandas(sFecha_P, 1);
            calcularVentasDiarias(sFecha_P, 0);
            calcularComandas(sFecha_P, 0);
            promedioVentas();
            numeroPersonas(sFecha_P);
            mesasAtendidas(sFecha_P);
            platillosVendidos(sFecha_P);
            platilloMasVendido(sFecha_P);
        }

        //FUNCION PARA CALCULAR TOTALES POR MES
        private void calcularVentasMensuales()
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
                        if (Convert.ToDouble(dtConsulta.Rows[0]["suma"].ToString()) == 0)
                        {
                            lblTotalFacturadoMes.Text = "$ 0.00";
                        }

                        else
                        {
                            dFacturadoMes = Convert.ToDecimal(dtConsulta.Rows[0]["suma"].ToString());
                            lblTotalFacturadoMes.Text = "$ " + dtConsulta.Rows[0]["suma"].ToString();
                        }
                    }

                    else
                    {
                        lblTotalFacturadoMes.Text = "$ 0.00";
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

        //FUNCION  PARA CONTAR LAS COMANDAS REALIZADAS POR MES
        private void calcularComandasMensuales()
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
                        if (Convert.ToDouble(dtConsulta.Rows[0]["cuenta"].ToString()) == 0)
                        {
                            lblCantidadComandasFacturadasMes.Text = "0";
                        }

                        else
                        {
                            lblCantidadComandasFacturadasMes.Text = dtConsulta.Rows[0]["cuenta"].ToString();
                        }
                    }

                    else
                    {
                        lblCantidadComandasFacturadasMes.Text = "0";
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
        private void validarMes(int iMes_P)
        {
            try
            {
                if ((iMes_P == 1) || (iMes_P == 3) || (iMes_P == 5) || (iMes_P == 7) || 
                    (iMes_P == 8) || (iMes_P == 10) || (iMes_P == 12))
                {
                    iDiaFinal = 31;
                }

                else if ((iMes_P == 4) || (iMes_P == 6) || (iMes_P == 9) || (iMes_P == 11))
                {
                    iDiaFinal = 30;
                }

                else
                {
                    if (Convert.ToInt32(cmbAnio.Text) % 4 == 0)
                    {
                        iDiaFinal = 29;
                    }

                    else
                    {
                        iDiaFinal = 28;
                    }
                }

                sFechaInicio = cmbAnio.Text.Trim() + "-" + iMes_P.ToString().PadLeft(2, '0') + "-01";
                sFechaFinal = cmbAnio.Text.Trim() + "-" + iMes_P.ToString().PadLeft(2, '0') + "-" + iDiaFinal.ToString().PadLeft(2, '0');
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLEVAR EL CHART
        private void llenarChart()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta, O.descripcion" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.fecha_pedido between '" + sFechaInicio + "' " + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "group by O.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        chartOrdenes.Series.Clear();
                        chartOrdenes.Palette = ChartColorPalette.BrightPastel;
                        chartOrdenes.Titles.Add("ORDENES ATENDIDAS");

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            Series series = chartOrdenes.Series.Add(dtConsulta.Rows[i][1].ToString());
                            series.Points.Add(Convert.ToInt32(dtConsulta.Rows[i][0].ToString()));
                        }
                    }

                    else
                    {
                        chartOrdenes.Series.Clear();
                        chartOrdenes.Palette = ChartColorPalette.BrightPastel;
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

        #endregion

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFecha.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                //sFecha = Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy/MM/dd");
                sFecha = calendario.txtFecha.Text.Trim();
                txtFecha.Text = sFecha;
                cargarDashboard(Convert.ToDateTime(sFecha).ToString("yyyy/MM/dd"));
            }            
        }

        private void frmNuevoDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNuevoDashboard_Load(object sender, EventArgs e)
        {
            dFacturadoMes = 0;
            cmbAnio.SelectedIndex = 0;
            listaMeses();
            txtFecha.Text = Program.sFechaSistema.ToString("dd/MM/yyyy");
            sFecha = Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy/MM/dd");
            cargarDashboard(sFecha);

            int iAux = Convert.ToInt32(Convert.ToDateTime(txtFecha.Text.Trim()).ToString("MM"));
            cmbMes.SelectedValue = iAux;
            iMes = Convert.ToInt32(cmbMes.SelectedValue);
            validarMes(iMes);
            calcularVentasMensuales();
            calcularComandasMensuales();
            promedioVentasMes();
            llenarChart();
        }

        private void btnOkMes_Click(object sender, EventArgs e)
        {
            dFacturadoMes = 0;
            iMes = Convert.ToInt32(cmbMes.SelectedValue);
            validarMes(iMes);
            calcularVentasMensuales();
            calcularComandasMensuales();
            promedioVentasMes();
            llenarChart();
        }

        private void lblPorSemana_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DashBoard.frmDashBoardSemana semana = new DashBoard.frmDashBoardSemana();
            semana.ShowDialog();
        }
    }
}
