using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmComandasCuentasPorCobrar : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sFecha;
        string sTipoComanda;
        string sFechaPedido;
        string sFechaHoraPedido;
        string sIdentificacion;
        string sCliente;
        string sValor;

        DataTable dtConsulta;
        DataTable dtComandas;

        bool bRespuesta;

        SqlParameter[] parametro;

        int iCuentaComandas;
        int iCuentaAyudaComanda;
        int iPosXComanda;
        int iPosYComanda;
        int iPosXPrecuenta;
        int iIdPedido;
        int iIdPersona;
        int iNumeroPedido;

        Button[] botonComandas = new Button[4];
        Button[] botonPrecuenta = new Button[4];

        public frmComandasCuentasPorCobrar()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER LA FECHA DEL SERVIDOR
        private void obtenerFecha()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("dd/MM/yyyy");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LAS COMANDAS
        private void cargarComandas()
        {
            try
            {
                sFecha = Convert.ToDateTime(dtFecha.Text).ToString("yyyy-MM-dd");
                int iCantidadParametros = 2;
                string sFiltro_P = "";

                sSql = "";
                sSql += "select * from pos_vw_revisar_comandas_por_cobrar" + Environment.NewLine;
                sSql += "where fecha_pedido = @fecha_pedido" + Environment.NewLine;
                sSql += "and id_localidad = @id_localidad" + Environment.NewLine;

                if (txtBusqueda.Text.Trim() != "")
                {
                    sFiltro_P = txtBusqueda.Text.Trim();

                    sSql += "and (identificacion like '%@identificacion%'" + Environment.NewLine;
                    sSql += "or cliente like '%@cliente%'" + Environment.NewLine;
                    sSql += "or numero_pedido like '%@numero_pedido%')" + Environment.NewLine;

                    iCantidadParametros = 5;
                }

                sSql += "order by fecha_pedido, numero_pedido";

                parametro = new SqlParameter[iCantidadParametros];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@fecha_pedido";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = sFecha;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_localidad";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Program.iIdLocalidad;

                if (iCantidadParametros == 5)
                {
                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@identificacion";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = sFiltro_P;

                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@cliente";
                    parametro[3].SqlDbType = SqlDbType.VarChar;
                    parametro[3].Value = sFiltro_P;

                    parametro[4] = new SqlParameter();
                    parametro[4].ParameterName = "@numero_pedido";
                    parametro[4].SqlDbType = SqlDbType.VarChar;
                    parametro[4].Value = sFiltro_P;
                }

                dtComandas = new DataTable();
                dtComandas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtComandas, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaComandas = 0;

                if (dtComandas.Rows.Count == 0)
                {
                    pnlOrdenes.Controls.Clear();
                    btnSubirComandas.Visible = false;
                    btnBajarComandas.Visible = false;

                    //ok = new VentanasMensajes.frmMensajeNuevoOk();
                    //ok.lblMensaje.Text = "No se encuentran registro con los parámetros ingresados.";
                    //ok.ShowDialog();

                    return;
                }

                if (dtComandas.Rows.Count > 4)
                {
                    btnSubirComandas.Visible = true;
                    btnBajarComandas.Visible = true;
                    btnBajarComandas.Enabled = true;
                    btnSubirComandas.Enabled = false;
                }

                else
                {
                    btnSubirComandas.Visible = false;
                    btnBajarComandas.Visible = false;
                    btnBajarComandas.Enabled = false;
                }

                if (crearBotones() == false)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo crear los botones de las comandas.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES
        private bool crearBotones()
        {
            try
            {
                pnlOrdenes.Controls.Clear();
                iPosXComanda = 0;
                iPosYComanda = 0;
                iPosXPrecuenta = 551;
                iCuentaAyudaComanda = 0;

                for (int i = 0; i < 4; i++)
                {
                    iIdPedido = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["id_pedido"].ToString());
                    iIdPersona = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["id_persona"].ToString());
                    sTipoComanda = dtComandas.Rows[iCuentaComandas]["tipo_comanda"].ToString();
                    iNumeroPedido = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["numero_pedido"].ToString());
                    sFechaPedido = Convert.ToDateTime(dtComandas.Rows[iCuentaComandas]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy");
                    sFechaHoraPedido = Convert.ToDateTime(dtComandas.Rows[iCuentaComandas]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
                    sIdentificacion = dtComandas.Rows[iCuentaComandas]["identificacion"].ToString();
                    sCliente = dtComandas.Rows[iCuentaComandas]["cliente"].ToString().Trim().ToUpper();
                    sValor = dtComandas.Rows[iCuentaComandas]["valor"].ToString();

                    string sTextoBoton = "";
                    //sTextoBoton += "Identificación: " + sIdentificacion + "   Cliente: " + sCliente + Environment.NewLine;
                    sTextoBoton += "Cliente: " + sCliente + Environment.NewLine;
                    sTextoBoton += "Tipo de Comanda: " + sTipoComanda + "   Número de Pedido: " + iNumeroPedido.ToString() + Environment.NewLine;
                    sTextoBoton += "Fecha y Hora de la Orden: " + sFechaHoraPedido + Environment.NewLine;

                    sTextoBoton += "CUENTA POR COBRAR";

                    botonComandas[i] = new Button();
                    botonComandas[i].Name = iIdPedido.ToString();
                    botonComandas[i].Click += boton_clic;
                    botonComandas[i].Size = new Size(555, 100);
                    botonComandas[i].Location = new Point(iPosXComanda, iPosYComanda);
                    botonComandas[i].Font = new Font("Maiandra GD", 11);
                    botonComandas[i].BackColor = Color.FromArgb(255, 224, 192);
                    botonComandas[i].Image = Palatium.Properties.Resources.comanda_revisar;
                    botonComandas[i].ImageAlign = ContentAlignment.MiddleLeft;
                    botonComandas[i].Text = sTextoBoton;

                    botonPrecuenta[i] = new Button();
                    botonPrecuenta[i].Name = iIdPedido.ToString();
                    botonPrecuenta[i].Click += botonImprimir_clic;
                    botonPrecuenta[i].Image = Palatium.Properties.Resources.impresora_icono;
                    botonPrecuenta[i].ImageAlign = ContentAlignment.TopCenter;
                    botonPrecuenta[i].Size = new Size(95, 100);
                    botonPrecuenta[i].Location = new Point(iPosXPrecuenta, iPosYComanda);
                    botonPrecuenta[i].Font = new Font("Maiandra GD", 11);
                    botonPrecuenta[i].Text = "Imprimir" + Environment.NewLine + "Precuenta";
                    botonPrecuenta[i].TextAlign = ContentAlignment.BottomCenter;
                    botonPrecuenta[i].BackColor = Color.FromArgb(192, 255, 255);                    

                    pnlOrdenes.Controls.Add(botonComandas[i]);
                    pnlOrdenes.Controls.Add(botonPrecuenta[i]);

                    iCuentaComandas++;
                    iCuentaAyudaComanda++;

                    if (dtComandas.Rows.Count == iCuentaComandas)
                    {
                        btnBajarComandas.Enabled = false;
                        break;
                    }

                    iPosYComanda += 100;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ABRIR EL MÓDULO DE COBROS
        private void boton_clic(object sender, EventArgs e)
        {
            Button botonsel = sender as Button;

            Pedidos.frmCobros cobros = new Pedidos.frmCobros(botonsel.Name, 1);
            cobros.ShowDialog();

            if (cobros.DialogResult == DialogResult.OK)
            {
                cargarComandas();
            }
        }

        //FUNCIÓN PARA IMPRIMIR LA PRECUENTA
        private void botonImprimir_clic(object sender, EventArgs e)
        {
            Button botonsel = sender as Button;

            Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(botonsel.Name, 1, "Pre-Cuenta");
            precuenta.ShowDialog();    
        }

        #endregion

        private void frmComandasCuentasPorCobrar_Load(object sender, EventArgs e)
        {
            obtenerFecha();
            dtFecha.Text = sFecha;
            cargarComandas();
            this.ActiveControl = txtBusqueda;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarComandas();
            txtBusqueda.Focus();
        }
    }
}
