using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmCombinarOrdenes : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;
        
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public int iIdPedido { get; set; }
        int iIdPedidoAyuda;
        int iIdOrden;
        string sMesa;

        public frmCombinarOrdenes(int iIdOrden, string sMesa)
        {
            this.iIdOrden = iIdOrden;
            this.sMesa = sMesa;
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region FUNCIONES DEL USUARIO

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        //Método para saber el número de órden que se han hecho para la mesa
        private int verificarNumeroOrdenes()
        {
            try
            {
                int iNumeroOrdenes = 0;
                int iIdPosOrigenOrden = buscarIdPosOrigenOrden();

                if (iIdPosOrigenOrden != 0)
                {
                    string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                    sSql += "where id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;
                    sSql += "and fecha_orden = '" + sFecha + "'" + Environment.NewLine;
                    sSql += "and id_localidad = " + Program.iIdLocalidad;

                    dtConsulta = new DataTable();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iNumeroOrdenes = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                            return iNumeroOrdenes;
                        }
                        else
                            return 0;
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUnción que me retorna el id pos origen orden
        private int buscarIdPosOrigenOrden()
        {
            sSql = "";
            sSql += "select id_pos_origen_orden" + Environment.NewLine;
            sSql += "from pos_origen_orden" + Environment.NewLine;
            sSql += "where codigo = '01'";

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
            if (bRespuesta == true)
            { 
                if(dtConsulta.Rows.Count>0)
                    return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                else
                    return 0;
            }
            else
                return 0;


        }

        //Función para mostrar los botones
        private void mostrarBotones()
        {
            try
            {
                int iNumeroOrdenes = verificarNumeroOrdenes();
                if (iNumeroOrdenes > 0)
                {
                    string sFecha = DateTime.Now.ToString("yyyy/MM/dd");
                    int iIdPosOrigenOrden = buscarIdPosOrigenOrden();

                    sSql = "select CP.id_pedido, NP.numero_pedido, isnull(CP.id_pos_mesa,0) id_pos_mesa, "+
                            "isnull(M.descripcion,'NINGUNA') descripcion, C.descripcion, O.descripcion, "+
                            "CP.fecha_apertura_orden, CP.estado_orden, CP.numero_personas, CP.cuenta, Mes.descripcion   " +
                            "from cv403_cab_pedidos as CP inner join pos_origen_orden as O  "+
                            "on (O.id_pos_origen_orden = CP.id_pos_origen_orden) "+
                            "inner join cv403_numero_cab_pedido as NP on (NP.id_pedido = CP.id_pedido and NP.estado = 'A') "+
                            "left outer join pos_mesa as M on (M.id_pos_mesa = CP.id_pos_mesa)  "+
                            "inner join pos_cajero as C on (C.id_pos_cajero = CP.id_pos_cajero) "+ 
                            "inner join pos_mesero as Mes on Cp.id_pos_mesero = Mes.id_pos_mesero "+
                            "where CP.estado in ( 'A', 'N')  "+
                            "and CP.fecha_orden = '"+sFecha+"' "+
                            "and CP.id_pos_origen_orden = "+iIdPosOrigenOrden+" "+
                            "and Cp.id_pedido <> "+iIdOrden+" "+
                            "and CP.estado_orden in ('Abierta', 'Pre-Cuenta') " +
                            "order by CP.id_pedido desc";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            int iCuenta = dtConsulta.Rows.Count;
                            Button[,] boton = new Button[iCuenta,3];
                            int iContador = 0;
                            for (int i = 0; i < 100; i++)
                            {
                                if (iContador == iCuenta)
                                    break;

                                for (int j = 0; j < 3; j++)
                                {
                                    int iIdPedido = Convert.ToInt32(dtConsulta.Rows[iContador].ItemArray[0].ToString());
                                    int iNumeroDeOrden = Convert.ToInt32(dtConsulta.Rows[iContador].ItemArray[1].ToString());
                                    string sDescripcion = dtConsulta.Rows[iContador].ItemArray[3].ToString();
                                    int iNumeroDePersonas = Convert.ToInt32(dtConsulta.Rows[iContador].ItemArray[8].ToString());
                                    int iNumeroDeCuenta = Convert.ToInt32(dtConsulta.Rows[iContador].ItemArray[9].ToString());
                                    string sTexto = "";
                                    double sTotalOrden = sacarTotalOrden(iIdPedido);
                                    string sNombreMesero = dtConsulta.Rows[0].ItemArray[10].ToString();

                                    //if (iIdPedido != -1)
                                    //{
                                        //Creo los botones de todas las órdenes
                                        boton[i,j] = new Button();
                                        boton[i,j].Name = iIdPedido.ToString(); //Se guarda el id del pedido
                                        boton[i, j].Width = 200;
                                        boton[i, j].Height = 100;
                                        boton[i, j].Top = i * 110;
                                        boton[i, j].Left = j * 210;
                                        boton[i,j].Font = new Font("Arial", 12);
                                        boton[i, j].Click += botonClicCombinar;
                                        boton[i, j].Tag = sTotalOrden; //Se guarda el total de la orden
                                        boton[i, j].AccessibleName = sDescripcion; //Se guarda la descripción de la mesa
                                        boton[i, j].AccessibleDescription = sNombreMesero;//Se guardo el nombre del mesero
                                        boton[i, j].AccessibleDefaultActionDescription = iNumeroDeOrden.ToString();//Se guarda el número de orden

                                        sTexto += "# De Orden: " + iNumeroDeOrden + Environment.NewLine;
                                        sTexto += "# De Cuenta: " + iNumeroDeCuenta + Environment.NewLine;
                                        sTexto += "# Total de la Orden: " + sTotalOrden.ToString("N2") + Environment.NewLine;
                                        sTexto += sDescripcion;

                                        boton[i,j].Text = sTexto;

                                        this.Controls.Add(boton[i,j]);
                                        pnlCombinar.Controls.Add(boton[i,j]);
                                        iContador++;
                                        if (iContador == iCuenta)
                                            break;
                                        
                                    //} 
                                }
                                  
                            }
                        }
                    }
                    else
                        MessageBox.Show("Ocurrió un problema al cargar los botones:\n","Combinar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    ok.LblMensaje.Text = "No hay órdenes para ser mostradas";
                    ok.ShowDialog();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocurrió un problema al cargar los botones:\n"
                    +exc.Message,"Combinar",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        //Función que me retorna el total de la orden
        private double sacarTotalOrden(int iIdPedido)
        {
            string sQuery = "select sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto + DP.valor_iva + Dp.valor_otro)) total_incluido_impuestos " +
                                       "from cv403_det_pedidos as DP, cv403_cab_pedidos as CP " +
                                       "where (CP.id_pedido = DP.id_pedido) and CP.id_pedido = " + iIdPedido + " and CP.estado in ('A', 'N') and DP.estado = 'A'";
            DataTable dtConsultaMesa = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsultaMesa, sQuery);

            if (bRespuesta == true)
            {
                return Convert.ToDouble(dtConsultaMesa.Rows[0].ItemArray[0].ToString());
            }
            else
                return -1;
        }

        private void frmCombinarOrdenes_Load(object sender, EventArgs e)
        {
            Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);
            mostrarBotones();
        }

        //Función cuando se da clic en algún boton
        private void botonClicCombinar(object sender, EventArgs e)
        {
            Button btBotonSel = sender as Button;
            int iIdPedido = Convert.ToInt32(btBotonSel.Name);

            sSql = "";
            sSql += "select * from pos_vw_det_pedido" + Environment.NewLine;
            sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
            sSql += "and estado = 'A'" + Environment.NewLine;
            sSql += "order by id_det_pedido";

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dgvPedido.Rows.Clear();
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        string sCantidad =dtConsulta.Rows[i].ItemArray[4].ToString();
                        double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[5].ToString());
                        double dbPrecioTotal = (Convert.ToDouble(sCantidad) * dbPrecioUnitario);
                        string sNombre;
                        if(dtConsulta.Rows[i].ItemArray[27].ToString() != null)
                            sNombre = dtConsulta.Rows[i].ItemArray[2].ToString();
                        else
                            sNombre = dtConsulta.Rows[i].ItemArray[27].ToString();

                        dgvPedido.Rows.Add( sCantidad,
                                            sNombre,
                                            dbPrecioUnitario,
                                            dbPrecioTotal.ToString("N2")
                                            );

                        txt_numeromesa.Text = btBotonSel.AccessibleName;
                        txtMesero.Text = btBotonSel.AccessibleDescription;
                        txtOrden.Text = btBotonSel.AccessibleDefaultActionDescription;
                        double dbTotalDebido = Convert.ToDouble(btBotonSel.Tag.ToString());
                        lblCantidadDebida.Text = dbTotalDebido.ToString("N2") ;
                        iIdPedidoAyuda = Convert.ToInt32(btBotonSel.Name);
                    }
                }
                else
                    MessageBox.Show("No hay pedidos en la orden","Combinar",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }


        }

        private void btnCombinar_Click(object sender, EventArgs e)
        {
            if (dgvPedido.Rows.Count > 0)
            {
                SiNo.LblMensaje.Text = "¿Está seguro que desea combinar la orden de la " + sMesa + " con la " + txt_numeromesa.Text + "?";
                SiNo.ShowInTaskbar = false;
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    iIdPedido = iIdPedidoAyuda;
                    DialogResult = DialogResult.OK;
                }
            }

            else
            {
                ok.LblMensaje.Text = "Seleccione una orden para ser combinada.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }                
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSalir);
        }

        private void btnCombinar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCombinar);
        }

        private void btnCombinar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCombinar);
        }
    }
}
