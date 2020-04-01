using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Areas
{
    public partial class frmComandasCombinar : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeSiNo SiNo;

        string sSql;
        string sTextoComanda;
        string sNumeroMesa;
        string sSeccionMesa;
        string sNumeroPedido;
        string sNumeroCuenta;
        string sFechaComanda;
        string sNombreMesero;
        string sCantidadProducto;
        string sNombreProducto;

        Decimal dbCantidad;
        Decimal dbPrecioUnitario;
        Decimal dbValorDescuento;
        Decimal dbValorIva;
        Decimal dbValorServicio;
        Decimal dbTotal;

        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtComandas;
        DataTable dtComandasSeleccionadas;

        int iCuentaComandas;
        int iCuentaComandasAyuda;
        int iPosXTexto;
        int iPosYTexto;
        int iPosXBoton;
        int iPosYBoton;
        int iBanderaBoton;

        TextBox[,] lista;
        Button[,] botonSeleccionar;

        public frmComandasCombinar()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS PEDIDOS VIGENTES
        private int consultarPedidosVigentes()
        {
            try
            {
                sSql = "";
                sSql += "select id_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_pos_mesa > 0" + Environment.NewLine;
                sSql += "order by id_pos_mesa";

                dtComandas = new DataTable();
                dtComandas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtComandas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return dtComandas.Rows.Count;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CREAR LOS CONTROLES
        private void crearControles()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                pnlComandas.Controls.Clear();
                iPosXTexto = 0;
                iPosYTexto = 0;
                iPosXBoton = 247;
                iPosYBoton = 0;
                iCuentaComandasAyuda = 0;

                lista = new TextBox[2, 3];
                botonSeleccionar = new Button[2, 3];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int iIdPedido_REC = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["id_pedido"].ToString());
                        iBanderaBoton = 0;

                        if (crearTexto(iIdPedido_REC) == true)
                        {
                            lista[i, j] = new TextBox();
                            lista[i, j].Text = sTextoComanda;
                            lista[i, j].Location = new Point(iPosYTexto, iPosXTexto);
                            lista[i, j].Size = new Size(255, 245);
                            lista[i, j].BackColor = Color.White;
                            lista[i, j].Multiline = true;
                            lista[i, j].ReadOnly = true;

                            botonSeleccionar[i, j] = new Button();
                            botonSeleccionar[i, j].Click += boton_clic_seleccion;                            
                            botonSeleccionar[i, j].FlatAppearance.BorderColor = Color.White;
                            botonSeleccionar[i, j].FlatAppearance.BorderSize = 2;
                            botonSeleccionar[i, j].FlatAppearance.MouseOverBackColor = Color.Blue;
                            botonSeleccionar[i, j].FlatStyle = FlatStyle.Flat;
                            botonSeleccionar[i, j].Font = new Font("Maiandra GD", 14.25F, FontStyle.Regular);
                            botonSeleccionar[i, j].Location = new Point(iPosYBoton, iPosXBoton);
                            botonSeleccionar[i, j].Name = iIdPedido_REC.ToString();
                            botonSeleccionar[i, j].Size = new Size(255, 56);                            
                            botonSeleccionar[i, j].UseVisualStyleBackColor = false;
                            botonSeleccionar[i, j].AccessibleDescription = sNumeroPedido;
                            botonSeleccionar[i, j].AccessibleName = sNumeroMesa + " - " + sSeccionMesa;

                            for (int k = 0; k < dtComandasSeleccionadas.Rows.Count; k++)
                            {
                                if (iIdPedido_REC == Convert.ToInt32(dtComandasSeleccionadas.Rows[k]["id_pedido"].ToString()))
                                {
                                    iBanderaBoton = 1;
                                    break;
                                }
                            }

                            if (iBanderaBoton == 1)
                            {
                                botonSeleccionar[i, j].BackColor = Color.Orange;
                                botonSeleccionar[i, j].Text = "REMOVER";
                            }

                            else
                            {
                                botonSeleccionar[i, j].BackColor = Color.SpringGreen;
                                botonSeleccionar[i, j].Text = "SELECCIONE";
                            }

                            pnlComandas.Controls.Add(lista[i, j]);
                            pnlComandas.Controls.Add(botonSeleccionar[i, j]);
                        }

                        iCuentaComandas++;
                        iCuentaComandasAyuda++;
                        iPosYTexto += 260;
                        iPosYBoton += 260;

                        if (dtComandas.Rows.Count == iCuentaComandas)
                        {
                            btnSiguiente.Visible = false;
                            break;
                        }
                    }

                    iPosYTexto = 0;
                    iPosXTexto += 320;

                    iPosYBoton = 0;
                    iPosXBoton += 320;
                    

                    if (dtComandas.Rows.Count == iCuentaComandas)
                    {
                        btnSiguiente.Visible = false;
                        break;
                    }
                }

                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }

        //FUNCION PARA CONSULTAR EL PEDIDO
        private bool crearTexto(int iIdPedido_P)
        {
            try
            {
                sTextoComanda = "";

                sSql = "";
                sSql += "select * from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                dbTotal = 0;
                sNumeroMesa = dtConsulta.Rows[0]["descripcion_mesa"].ToString().Trim().ToUpper();
                sSeccionMesa = dtConsulta.Rows[0]["seccion_mesa"].ToString().Trim().ToUpper();
                sNumeroPedido = dtConsulta.Rows[0]["numero_pedido"].ToString().Trim();
                sNumeroCuenta = dtConsulta.Rows[0]["numero_pedido"].ToString().Trim();
                sFechaComanda = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
                sNombreMesero = dtConsulta.Rows[0]["nombre_mesero"].ToString().Trim().ToUpper();

                sTextoComanda += "MESA: " + sNumeroMesa + " - " + sSeccionMesa + Environment.NewLine + Environment.NewLine;
                sTextoComanda += "No. PEDIDO: " + sNumeroPedido + Environment.NewLine;
                sTextoComanda += "CUENTA: " + sNumeroCuenta + Environment.NewLine;
                sTextoComanda += "FECHA COMANDA: " + sFechaComanda + Environment.NewLine;
                sTextoComanda += "MESERO: " + sNombreMesero + Environment.NewLine + Environment.NewLine;
                sTextoComanda += "DETALLE DE LA COMANDA:" + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sCantidadProducto = dtConsulta.Rows[0]["cantidad"].ToString().Trim();
                    sNombreProducto = dtConsulta.Rows[0]["nombre"].ToString().Trim().ToUpper();
                    dbCantidad = Convert.ToDecimal(sCantidadProducto);
                    dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[0]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDecimal(dtConsulta.Rows[0]["valor_dscto"].ToString());
                    dbValorIva = Convert.ToDecimal(dtConsulta.Rows[0]["valor_iva"].ToString());
                    dbValorServicio = Convert.ToDecimal(dtConsulta.Rows[0]["valor_otro"].ToString());

                    dbTotal += dbCantidad * (dbPrecioUnitario - dbValorDescuento + dbValorIva + dbValorServicio);

                    sTextoComanda += sCantidadProducto.PadRight(5, ' ') + sNombreProducto + Environment.NewLine;
                }

                sTextoComanda += Environment.NewLine;
                sTextoComanda += "TOTAL: " + dbTotal.ToString("N2");

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION DEL BOTON
        private void boton_clic_seleccion(object sender, EventArgs e)
        {
            Button btnClic = sender as Button;

            int iIdPedido_P = Convert.ToInt32(btnClic.Name);

            if (btnClic.Text == "SELECCIONE")
            {
                dgvDatos.Rows.Add(iIdPedido_P, btnClic.AccessibleName, btnClic.AccessibleDescription);
                dtComandasSeleccionadas.Rows.Add(btnClic.Name, btnClic.AccessibleName, btnClic.AccessibleDescription);
                btnClic.Text = "REMOVER";
                btnClic.BackColor = Color.Orange;
                dgvDatos.ClearSelection();
            }

            else
            {
                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (iIdPedido_P == Convert.ToInt32(dgvDatos.Rows[i].Cells["id_pedido"].Value))
                    {
                        dgvDatos.Rows.RemoveAt(i);
                        break;
                    }
                }

                for (int j = dtComandasSeleccionadas.Rows.Count - 1; j >= 0; j--)
                {
                    if (Convert.ToInt32(dtComandasSeleccionadas.Rows[j]["id_pedido"].ToString()) == iIdPedido_P)
                    {
                        dtComandasSeleccionadas.Rows.RemoveAt(j);
                    }
                }

                btnClic.Text = "SELECCIONE";
                btnClic.BackColor = Color.SpringGreen;
                dgvDatos.ClearSelection();
            }
        }

        //FUNCION PARA CREAR EL DATATABLE
        private void crearDataTable()
        {
            try
            {
                dtComandasSeleccionadas = new DataTable();
                dtComandasSeleccionadas.Clear();
                dtComandasSeleccionadas.Columns.Add("id_pedido");
                dtComandasSeleccionadas.Columns.Add("numero_mesa");
                dtComandasSeleccionadas.Columns.Add("numero_pedido");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmComandasCombinar_Load(object sender, EventArgs e)
        {
            int iCuenta = consultarPedidosVigentes();
            crearDataTable();

            if (iCuenta == -1)
            {
                this.Close();
                return;
            }

            if (iCuenta == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No existen comandas de mesa activas.";
                ok.ShowDialog();
                this.Close();
                return;
            }

            iCuentaComandas = 0;
            crearControles();
            
            this.ActiveControl = label2;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Visible = true;
            crearControles();
            this.ActiveControl = label2;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaComandas -= iCuentaComandasAyuda;

            if (iCuentaComandas <= 6)
            {
                btnAnterior.Visible = false;
            }

            btnSiguiente.Visible = true;
            iCuentaComandas -= 6;

            crearControles();
        }

        private void btnCombinar_Click(object sender, EventArgs e)
        {
            Areas.frmMensajeCombinar mensaje = new frmMensajeCombinar(dtComandasSeleccionadas);
            mensaje.ShowDialog();

            if (mensaje.DialogResult == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                string sNumeroPedido_Combinar = mensaje.sNumeroPedido;
                int iIdPedido_Combinar = mensaje.iIdPedido;
                mensaje.Close();

                Areas.ClaseCombinarComandas_V2 combinar = new ClaseCombinarComandas_V2();

                if (combinar.recibirInformacion(dtComandasSeleccionadas, iIdPedido_Combinar, sNumeroPedido_Combinar) == false)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = combinar.sRespuesta;
                    ok.ShowDialog(); this.Cursor = Cursors.Default;
                    return;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Combinación de comandas se realizó éxitosamente.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void frmComandasCombinar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
