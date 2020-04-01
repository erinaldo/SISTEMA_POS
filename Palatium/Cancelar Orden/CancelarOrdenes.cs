using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class CancelarOrdenes : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        //VentanasMensajes.frmMensajeEspere espere = new VentanasMensajes.frmMensajeEspere();

        string sSql;
        string sFechaActual;
        DataTable dtConsulta;
        bool bRespuesta;
        int iCuenta;
        int iCoordenadaY = 0;
        int iOp;

        //VARIABLES QUE SE USARAN EN EL DATATABLE
        //=======================================================================================================

        int iIdPedido;
        int iNumeroPedido;
        string iIdPosMesa;
        string sNombreCajero;
        string sTipoOrden;
        string sFechaIngresoOrden;
        string sEstadoOrden;
        string sNombreMesa;
        int iNumeroPersonas;
        int iNumeroCuentaDiaria;
        DataTable dtConsultaMesa;
        Double DSumaDetalleOrden;

        public CancelarOrdenes()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtBusqueda.Text = txtBusqueda.Text + sValor;
                txtBusqueda.Focus();
                txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowDialog();
            }
        }

        public void mostrarBotones()
        {  
            int h = 1; 
            int controlBoton1 = 1;
            int iCuenta = 0;
            pnlOrdenes.Controls.Clear();
            
            iCoordenadaY = 0;

            //INSTRUCCIONES SQL UTILIZADAS EN EL FORMULARIO
            //EXTRAMOS LOS REGISTROS DEL DIA CON LA JORNADA INGRESADA

            //EXTRAMOS LOS REGISTROS DEL DIA CON LA JORNADA INGRESADA
            sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");

            sSql = "";
            sSql += "select CP.id_pedido, NP.numero_pedido,isnull(CP.id_pos_mesa,0) id_pos_mesa," + Environment.NewLine;
            sSql += "isnull(M.descripcion,'NINGUNA') descripcion, C.descripcion, O.descripcion," + Environment.NewLine;
            sSql += "CP.fecha_apertura_orden,CP.estado_orden, CP.numero_personas, CP.cuenta" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos as CP inner join" + Environment.NewLine;
            sSql += "pos_origen_orden as O on O.id_pos_origen_orden = CP.id_pos_origen_orden inner join" + Environment.NewLine;
            sSql += "cv403_numero_cab_pedido as NP on NP.id_pedido = CP.id_pedido" + Environment.NewLine;
            sSql += "and NP.estado = 'A' left outer join" + Environment.NewLine;
            sSql += "pos_mesa as M on M.id_pos_mesa = CP.id_pos_mesa inner join" + Environment.NewLine;
            sSql += "pos_cajero as C on C.id_pos_cajero = CP.id_pos_cajero" + Environment.NewLine;
            sSql += "where CP.estado = 'A'" + Environment.NewLine;
            sSql += "and CP.fecha_orden = '" + sFechaActual + "'" + Environment.NewLine;
            sSql += "and CP.id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;
            sSql += "and CP.id_pos_cajero = " + Program.CAJERO_ID + Environment.NewLine;
            sSql += "and CP.estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;

            if (iOp == 1)
            {
                sSql += "and NP.numero_pedido = " + Convert.ToInt32(txtBusqueda.Text.Trim()) + Environment.NewLine;            
            }

            else if (iOp == 2)
            {
                sSql += "and CP.cuenta = " + Convert.ToInt32(txtBusqueda.Text.Trim()) + Environment.NewLine;
            }

            sSql += "order by CP.id_pedido desc";            

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    //iOrdenesJornada = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    //Button[] boton = new Button[iCuenta];
                    //Button[] boton1 = new Button[iCuenta];

                    iCuenta = dtConsulta.Rows.Count;
                    Button[] boton = new Button[iCuenta];
                    Button[] boton1 = new Button[iCuenta];
                    //btnTotalOrdenes.Text = "Total de Órdenes \n" + iCuenta;

                    for (int i = 0; i < iCuenta;  i++)
                    {
                        //DATOS CARGADOS EN EL DATATABLE RECUPERADO DE LA BASE DE DATOS
                        //USAREMOS VARIABLES PARA QUE EVITAR COLOCAR EL NOMBRE DE TODA LA INSTRUCCIÒN DEL DATATABLE
                        //==========================================================================================================

                        //POSICIÒN 0 = orden.id_pedido
                        iIdPedido = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                        //POSICION 1 = numero_pedido                        
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                        //POSICION 2 = id_pos_mesa
                        iIdPosMesa = dtConsulta.Rows[i][2].ToString();
                        //POSICION 3 = mesa.descripcion
                        sNombreMesa = dtConsulta.Rows[i][3].ToString();
                        //POSICION 4 = nombre del cajero
                        sNombreCajero = dtConsulta.Rows[i][4].ToString();
                        //POSICION 5 = tipo de orden
                        sTipoOrden = dtConsulta.Rows[i][5].ToString();
                        //POSICION 6 = fecha de apertura de la orden 
                        sFechaIngresoOrden = dtConsulta.Rows[i][6].ToString();
                        //POSICION 7 = orden.estado_orden
                        sEstadoOrden = dtConsulta.Rows[i][7].ToString();
                        //POSICION 8 = mesa.descripcion
                        iNumeroPersonas = Convert.ToInt32(dtConsulta.Rows[i][8].ToString());
                        //POSICION 9 = cuenta diarias
                        iNumeroCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[i][9].ToString());
                        //==========================================================================================================


                        boton[i] = new Button();
                        boton[i].Name = iIdPedido.ToString();
                        boton[i].Click += boton_clic;
                        boton[i].Width = 650;
                        boton[i].Height = 100;
                        boton[i].Top = i * 100;
                        boton[i].Left = 1 * 50;
                        boton[i].Text = "" + h;
                        boton[i].Font = new Font("Maiandra GD", 11);
                        boton[i].BackColor = Color.FromArgb(255, 224, 192);
                        boton[i].Image = Palatium.Properties.Resources.comanda_revisar;
                        boton[i].ImageAlign = ContentAlignment.MiddleLeft;

                        boton1[i] = new Button();
                        boton1[i].Name = iIdPedido.ToString();
                        boton1[i].Click += botonCancelar_clic;
                        boton1[i].Width = 95;
                        boton1[i].Height = 100;
                        boton1[i].Top = i * 100;
                        boton1[i].Left = 1 * 35;
                        boton1[i].Text = "" + h;
                        boton1[i].Location = new Point(710, iCoordenadaY);
                        iCoordenadaY += 100;
                        boton1[i].Font = new Font("Maiandra GD", 11);
                        boton1[i].BackColor = Color.FromArgb(192, 255, 255);
                        boton1[i].TextAlign = ContentAlignment.BottomCenter;
                        boton1[i].Image = Palatium.Properties.Resources.comanda_anular; ;
                        boton1[i].ImageAlign = ContentAlignment.TopCenter;

                        boton1[i].Text = "Cancelar\nOrden";
                        controlBoton1++;

                        //Función para verificar el número de mesas
                        //contarNumeroCuentas(i);

                        for (int j = 1; j <= iCuenta; j++)
                        {

                            if (boton[i].Text == j.ToString())
                            {
                                //domicilio /llevar
                                string t_st_linea1 = "";
                                string t_st_linea2 = "";
                                string t_st_linea3 = "";
                                string t_st_linea4 = "";
                                string t_st_linea5 = "";
                                string t_st_linea6 = "";
                                string t_st_linea7 = "";
                                string t_st_linea8 = "";
                                string t_st_linea9 = "";

                                int iBandera = 0;

                                if (dtConsulta.Rows[0][5].ToString() == "Pre-Cuenta")
                                {
                                    //Program.Orden[j][8] = "Abierta";
                                    iBandera = 1;
                                }

                                //INSTRUCCIONES PARA SUMAR EL VALOR DEL DETALLE DE LA ORDEN
                                //UTILIZAREMOS EL DATATABLE DE  LA INSTRUCCION DE LA MESA
                                dtConsultaMesa = new DataTable();
                                dtConsultaMesa.Clear();
                                

                                //sSql = "select sum((OD.PRECIO_UNIDAD * OD.CANTIDAD) * (" + Convert.ToDouble(Program.iva + Program.recargo + 1) +")) TOTAL from pos_orden O, pos_detalle_orden OD " +
                                //       "WHERE (O.ID_POS_ORDEN = OD.ID_POS_ORDEN) AND O.ID_POS_ORDEN = " + iIdPedido + " and OD.estado = 'A'";
                                sSql = "";
                                sSql += "select sum(DP.cantidad * DP.precio_unitario * (" + Convert.ToDouble(Program.iva + Program.servicio + 1) + ")) total" + Environment.NewLine;
                                sSql += "from cv403_det_pedidos as DP, cv403_cab_pedidos as CP " + Environment.NewLine;
                                sSql += "where (CP.id_pedido = DP.id_pedido)" + Environment.NewLine;
                                sSql += "and CP.id_pedido = " + iIdPedido + Environment.NewLine;
                                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                                sSql += "and DP.estado = 'A'";

                                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsultaMesa, sSql);

                                if (bRespuesta == true)
                                {
                                    DSumaDetalleOrden = Convert.ToDouble(dtConsultaMesa.Rows[0][0].ToString());
                                }


                                //Mesa
                                t_st_linea1 = "#de Cuenta: " + iNumeroCuentaDiaria.ToString() + "   # de Orden: " + (iNumeroPedido).ToString() + "  Mesero: " + sNombreCajero + "      Total: " + "$ " + DSumaDetalleOrden.ToString("N2"); 
                                //t_st_linea2 = "Hora de la orden: " + sFechaIngresoOrden + "  [TIME ELAPSED: " /*+ DateTime.Parse(DateTime.Now - DateTime.Parse(Program.Orden[j][Program.ORD_FECHA_ORDEN]) + "").Minute*/ + " Minutes]";
                                t_st_linea2 = "Fecha y Hora de la Orden: " + sFechaIngresoOrden;
                                t_st_linea3 = sTipoOrden + " # : " + sNombreMesa + " -  Nº de Personas: " + iNumeroPersonas.ToString() + "\n Orden " + sEstadoOrden;

                                //A domicilio
                                t_st_linea4 = "#de Cuenta: " + iNumeroCuentaDiaria.ToString() + "   # de Orden: " + (iNumeroPedido).ToString() + "      Total: " + "$ " + DSumaDetalleOrden.ToString("N2"); 
                                //t_st_linea5 = "Hora de la orden: " + sFechaIngresoOrden + "  [TIME ELAPSED: " /*+ DateTime.Parse(DateTime.Now - DateTime.Parse(Program.Orden[j][6]) + "").Minute */+ " Minutes]";
                                t_st_linea5 = "Fecha y Hora de la Orden: " + sFechaIngresoOrden;
                                t_st_linea6 = sTipoOrden + " - QUITO - ECUADOR" + "\n Orden " + sEstadoOrden;

                                //Para llevar

                                t_st_linea7 = "#de Cuenta: " + iNumeroCuentaDiaria.ToString() + "   # de Orden: " + (iNumeroPedido).ToString() + "      Total: " + "$ " + DSumaDetalleOrden.ToString("N2"); 
                                //t_st_linea8 = "Hora de la orden: " + sFechaIngresoOrden + "  [TIME ELAPSED: " /*+ DateTime.Parse(DateTime.Now - DateTime.Parse(Program.Orden[j][6]) + "").Minute */+ " Minutes]";
                                t_st_linea8 = "Fecha y Hora de la Orden: " + sFechaIngresoOrden;
                                t_st_linea9 = sTipoOrden + "\n Orden " + sEstadoOrden;

                                if (sTipoOrden == "MESAS")
                                {
                                    boton[i].Text = t_st_linea1 + "\n" + t_st_linea2 + "\n" + t_st_linea3;
                                }
                                else if (sTipoOrden == "DOMICILIOS")
                                {
                                    boton[i].Text = t_st_linea4 + "\n" + t_st_linea5 + "\n" + t_st_linea6;
                                }
                                else if (sTipoOrden == "PARA LLEVAR")
                                {
                                    boton[i].Text = t_st_linea7 + "\n" + t_st_linea8 + "\n" + t_st_linea9;
                                }
                                else
                                {
                                    boton[i].Text = t_st_linea7 + "\n" + t_st_linea8 + "\n" + t_st_linea9;
                                }

                                if (iBandera == 1)
                                {
                                    //Program.Orden[j][8] = "Pre-Cuenta";
                                }
                            }
                        }

                        pnlOrdenes.Controls.Add(boton1[i]);
                        pnlOrdenes.Controls.Add(boton[i]);
                        h++;
                    }                    
                }
                else
                {
                    //SE PUEDE OMITIR RELLENAR ESTA LINEA YA QUE NO DEVUELVE INFORMACION
                }

                iOp = 0;
            }

            else
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }
        }


        //FUNCION DE LOS BOTONES DE CANCELAR
        private void botonCancelar_clic(object sender, EventArgs e)
        {
            //MOSTRAR EL FORMULARIO DE MOTIVO DE CANCELACIÒN
            Button botonsel = sender as Button;
            MotivoCancelación cancelar = new MotivoCancelación(botonsel.Name);
            cancelar.ShowDialog();

            if (cancelar.DialogResult == DialogResult.OK)
            {
                contarOrdenesAbiertas();
            }
        }

        private void boton_clic(object sender, EventArgs e)
        {
            //MOSTRAR EL FORMULARIO DE MOTIVO DE CANCELACIÒN
            Button botonsel = sender as Button;
            MotivoCancelación cancelar = new MotivoCancelación(botonsel.Name);
            cancelar.ShowDialog();

            if (cancelar.DialogResult == DialogResult.OK)
            {
                contarOrdenesAbiertas();
            }
        }

        //FUNCION PARA CONTAR LAS ORDENES ABIERTAS Y MOSTRAR EN EL PANEL
        private void contarOrdenesAbiertas()
        {
            try
            {
                //NECESITAMOS EL NUMERO DE CUENTAS REALIZADAS EN LA JORNADA YA SEA DIURNA O NOCTURA
                //Y QUE SEAN DE LA FECHA ACTUAL
                sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;
                sSql += "and fecha_orden = ' " + sFechaActual + "'" + Environment.NewLine;
                sSql += "and estado_orden in('Abierta', 'Pre-Cuenta', 'Cerrada')";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (dtConsulta.Rows[0][0].ToString() == "0")
                        {
                            mostrarBotones();
                        }
                        else
                        {
                            mostrarBotones();
                        }
                    }
                    else
                    {
                        mostrarBotones();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        #endregion

        private void CancelarOrdenes_Load(object sender, EventArgs e)
        {
            contarOrdenesAbiertas();
        }

        private void CancelarOrdenes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtBusqueda.Text.Length > 0)
            {

                str = txtBusqueda.Text.Substring(txtBusqueda.Text.Length - 1);
                loc = txtBusqueda.Text.Length;
                txtBusqueda.Text = txtBusqueda.Text.Remove(loc - 1, 1);
            }

            txtBusqueda.Focus();
            txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBusquedaOrdenCuenta_Click(object sender, EventArgs e)
        {
            if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
            {
                btnBusquedaOrdenCuenta.Text = "Por número de cuenta";
            }

            else
            {
                btnBusquedaOrdenCuenta.Text = "Por número de orden";
            }

            txtBusqueda.Focus();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "")
            {
                ok.LblMensaje.Text = "Favor ingrese datos para la búsqueda.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");

                if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
                {
                    iOp = 1;
                }

                else
                {
                    iOp = 2;
                }

                mostrarBotones();
                txtBusqueda.Text = "";
                txtBusqueda.Focus();
            }
        }

        private void btnOrdenes_Click(object sender, EventArgs e)
        {
            mostrarBotones();
            txtBusqueda.Text = "";
            txtBusqueda.Focus();
        }
    }
}

