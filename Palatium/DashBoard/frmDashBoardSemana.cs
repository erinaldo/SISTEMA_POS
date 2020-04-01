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
    public partial class frmDashBoardSemana : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sSql;
        string sFecha;
        string sFechaInicio;
        string sFechaFinal;
        string sNombreDia;

        DataTable dtConsulta;

        DateTime fechaRecibida;

        bool bRespuesta;

        int iIntervalo;
        int iDia;

        decimal dFacturado;
        decimal dConsumo;

        public frmDashBoardSemana()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CREAR LAS FECHAS
        private void crearFechas(string sFecha_P)
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

                else if ((sNombreDia == "miercoles") ||(sNombreDia == "miércoles"))
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
                    sFechaFinal = fechaRecibida.AddDays(7).ToString("dd/MM/yyyy");
                }

                else if (iDia == 7)
                {
                    sFechaInicio = fechaRecibida.AddDays(-7).ToString("dd/MM/yyyy");
                    sFechaFinal = fechaRecibida.ToString("dd/MM/yyyy");
                }

                else
                {
                    iIntervalo = iDia - 1;
                    sFechaInicio = fechaRecibida.AddDays(-iIntervalo).ToString("dd/MM/yyyy");
                    iIntervalo = 7 - iDia;
                    sFechaFinal = fechaRecibida.AddDays(iIntervalo).ToString("dd/MM/yyyy");
                }

                lblFechaInicio.Text = sFechaInicio;
                lblFechaFinal.Text = sFechaFinal;
                sFechaInicio = Convert.ToDateTime(sFechaInicio).ToString("yyyy/MM/dd");
                sFechaFinal = Convert.ToDateTime(sFechaFinal).ToString("yyyy/MM/dd");
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR LAS VENTAS DEL DÍA
        private void calcularVentasDiarias(int iOp)
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
        private void calcularComandas(int iOp)
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
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL DASHBOARD
        private void cargarDashboard(string sFecha_P)
        {
            try
            {
                crearFechas(sFecha_P);
                calcularVentasDiarias(1);
                calcularComandas(1);
                calcularVentasDiarias(0);
                calcularComandas(0);
                promedioVentas();
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmDashBoardSemana_Load(object sender, EventArgs e)
        {
            txtFecha.Text = Program.sFechaSistema.ToString("dd/MM/yyyy");
            sFecha = txtFecha.Text.Trim();
            cargarDashboard(sFecha);            
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFecha.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtFecha.Text = calendario.txtFecha.Text.Trim();
                sFecha = txtFecha.Text.Trim();
                cargarDashboard(sFecha);
            }    
        }

        private void frmDashBoardSemana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
