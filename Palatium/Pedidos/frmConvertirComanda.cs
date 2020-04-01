using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmConvertirComanda : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;

        bool bRespuesta;

        DataTable dtConsulta;

        int iContador;
        int iPosicionX;
        int iPosicionY;
        int iBanderaColor;
        int iIdPedido;
        int iIdOrigenOrden;

        Button[,] botonComandas;
        Button[,] botonOrigenOrden;
        Button botonSeleccionado;
        Button botonOrigen;

        public frmConvertirComanda()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER LAS COMANDAS
        private void instruccionComandas()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_comandas_activas" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + Convert.ToDateTime(Program.sFechaSistema).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and estado_orden = 'Abierta'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "order by numero_pedido desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return;
                }
                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                int num = (int)catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES DE LAS COMANDAS
        private void crearBotonesComanda()
        {
            try
            {
                instruccionComandas();
                pnlOrdenes.Controls.Clear();
                iContador = 0;
                iPosicionX = 0;
                iPosicionY = 0;
                iBanderaColor = 0;

                if (dtConsulta.Rows.Count > 0)
                {
                    botonComandas = new Button[50, 3];

                    for (int i = 0; i < 50; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            botonComandas[i, j] = new Button();
                            botonComandas[i, j].Text = "ORDEN: " + dtConsulta.Rows[iContador]["numero_pedido"].ToString() + Environment.NewLine + "TIPO: " + dtConsulta.Rows[iContador]["tipo_orden"].ToString() + Environment.NewLine + "MESA: " + dtConsulta.Rows[iContador]["mesa"].ToString() + Environment.NewLine + "MESERO: " + dtConsulta.Rows[iContador]["mesero"].ToString();
                            botonComandas[i, j].Tag = (object)dtConsulta.Rows[iContador]["id_pedido"].ToString();
                            botonComandas[i, j].AccessibleDescription = dtConsulta.Rows[iContador]["numero_pedido"].ToString();
                            botonComandas[i, j].AccessibleName = dtConsulta.Rows[iContador]["tipo_orden"].ToString();
                            botonComandas[i, j].Font = new Font("Maiandra GD", 9.75f, FontStyle.Regular);
                            botonComandas[i, j].ForeColor = Color.Black;
                            botonComandas[i, j].Location = new Point(iPosicionX, iPosicionY);
                            botonComandas[i, j].Name = "btn_" + iContador.ToString();
                            botonComandas[i, j].Size = new Size(180, 105);
                            botonComandas[i, j].Click += boton_clic_comanda;
                            if (iBanderaColor == 0)
                            {
                                botonComandas[i, j].BackColor = Color.FromArgb(192, 255, 255);
                            }

                            else
                            {
                                botonComandas[i, j].BackColor = Color.FromArgb(255, 192, 128);
                            }

                            pnlOrdenes.Controls.Add(botonComandas[i, j]);
                            iPosicionX += 180;
                            ++iContador;

                            if (iContador == dtConsulta.Rows.Count)
                            {
                                break;
                            }
                        }

                        iBanderaColor = iBanderaColor != 0 ? 0 : 1;
                        iPosicionX = 0;
                        iPosicionY += 105;

                        if (iContador == dtConsulta.Rows.Count)
                        {
                            break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION CLIC DE LOS BOTONES COMENDAS
        public void boton_clic_comanda(object sender, EventArgs e)
        {
            botonSeleccionado = sender as Button;
            iIdPedido = Convert.ToInt32(botonSeleccionado.Tag);
            lblNumeroOrden.Text = botonSeleccionado.AccessibleDescription;
            lblTipoOrden.Text = botonSeleccionado.AccessibleName;
        }

        //FUNCION PARA OBTENER EL ORIGEN
        private void instruccionOrigenOrden()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where visualizar = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta)
                {
                    return;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void crearBotonesOrigenOrden()
        {
            try
            {
                instruccionOrigenOrden();
                pnlOrigenOrden.Controls.Clear();
                iContador = 0;
                iPosicionX = 0;
                iPosicionY = 0;
                iBanderaColor = 0;
                if (dtConsulta.Rows.Count > 0)
                {
                    botonOrigenOrden = new Button[10, 2];
                    for (int i = 0; i < 10; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            botonOrigenOrden[i, j] = new Button();
                            botonOrigenOrden[i, j].Text = dtConsulta.Rows[iContador]["descripcion"].ToString();
                            botonOrigenOrden[i, j].Tag = dtConsulta.Rows[iContador]["id_pos_origen_orden"].ToString();
                            botonOrigenOrden[i, j].Font = new Font("Maiandra GD", 14.25f, FontStyle.Bold);
                            botonOrigenOrden[i, j].ForeColor = Color.Black;
                            botonOrigenOrden[i, j].Location = new Point(iPosicionX, iPosicionY);
                            botonOrigenOrden[i, j].Name = "btnOrden_" + iContador.ToString();
                            botonOrigenOrden[i, j].Size = new Size(160, 90);
                            botonOrigenOrden[i, j].Click += boton_clic_origen;
                            botonOrigenOrden[i, j].BackColor = Color.Lime;
                            pnlOrigenOrden.Controls.Add(botonOrigenOrden[i, j]);

                            iPosicionX += 160;
                            ++iContador;

                            if (iContador == dtConsulta.Rows.Count)
                            {
                                break;
                            }
                        }

                        iPosicionX = 0;
                        iPosicionY += 90;

                        if (iContador == dtConsulta.Rows.Count)
                        {
                            break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void boton_clic_origen(object sender, EventArgs e)
        {
            botonOrigen = sender as Button;
            if (iIdPedido == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado ninguna comanda para realizar la transacción";
                ok.ShowDialog();
            }

            else
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea convertir la comanda No. " + lblNumeroOrden.Text.Trim() + " de Origen " + lblTipoOrden.Text.Trim() + " a " + botonOrigen.Text + "?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    iIdOrigenOrden = Convert.ToInt32(botonOrigen.Tag);
                    actualizarRegistro();
                }
            }
        }

        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "id_pos_origen_orden = " + iIdOrigenOrden + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    int num = (int)catchMensaje.ShowDialog();
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "La comanda ha sido actualizada éxitosamente.";
                ok.ShowDialog();
                crearBotonesComanda();
                iIdPedido = 0;
                lblTipoOrden.Text = "SIN SELECCIONAR";
                lblNumeroOrden.Text = "SIN SELECCIONAR";
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmConvertirComanda_Load(object sender, EventArgs e)
        {
            crearBotonesComanda();
            crearBotonesOrigenOrden();
        }

        private void frmConvertirComanda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
