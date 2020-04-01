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
    public partial class frmCambioSeccionMesa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseAbrirCajon abrir;

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeSiNo SiNo;

        ToolTip ttMensajeMesas = new ToolTip();

        string sSql;
        string sNombreMesa;
        public string sDescripcionMesa;

        DataTable dtConsulta;
        DataTable dtSecciones;
        DataTable dtMesas;
        DataTable dtVerificadorMesa;
        DataTable dtEjecutarCombinacion;

        Button[] botonSecciones = new Button[5];
        Button[,] botonMesas;
        Button bSeccionMesa;
        Button botonSeleccionado;

        bool bRespuesta;
        bool usada;

        int iCuentaSecciones;
        int iPosXSecciones;
        int iCuentaSeccionesAyuda;

        int iIdPosMesa;
        int iIdPosSeccionMesa;
        int iBanderaMesas;
        int iBanderaCombinar;
        int iEjecutarCombinacion;
        public int iIdMesa;

        public frmCambioSeccionMesa()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //INSTRUCCION PARA OBTENER EL ID DEL PRIMER REGISTRO DE SECCIONES DE MESA
        private int primerRegistroSecciones()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 id_pos_seccion_mesa, descripcion" + Environment.NewLine;
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_seccion_mesa asc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    lblPisos.Text = "PISO";
                    iIdPosSeccionMesa = 0;
                }

                else
                {
                    lblPisos.Text = dtConsulta.Rows[0]["descripcion"].ToString().ToUpper();
                    iIdPosSeccionMesa = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_seccion_mesa"].ToString());
                }

                return iIdPosSeccionMesa;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CARGAR LAS SECCIONES
        private void cargarAreas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_seccion_mesa, codigo, descripcion, color, fondo_pantalla" + Environment.NewLine;
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_seccion_mesa asc";

                dtSecciones = new DataTable();
                dtSecciones.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSecciones, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaSecciones = 0;

                if (dtSecciones.Rows.Count > 0)
                {
                    if (dtSecciones.Rows.Count > 8)
                    {
                        btnSiguiente.Enabled = true;
                    }

                    else
                    {
                        btnSiguiente.Enabled = false;
                    }

                    if (mostrarBotonesSecciones() == false)
                    {

                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentras secciones configuradas en el sistema.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LOS BOTONES DE LAS SECCIONES
        private bool mostrarBotonesSecciones()
        {
            try
            {
                pnlSecciones.Controls.Clear();
                iPosXSecciones = 0;
                iCuentaSeccionesAyuda = 0;

                for (int i = 0; i < 5; i++)
                {
                    botonSecciones[i] = new Button();
                    botonSecciones[i].Cursor = Cursors.Hand;
                    botonSecciones[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);
                    botonSecciones[i].FlatStyle = FlatStyle.Flat;
                    botonSecciones[i].Font = new Font("Maiandra GD", 12F, FontStyle.Bold);
                    botonSecciones[i].Image = Properties.Resources.icono_reporte_edificio;
                    botonSecciones[i].ImageAlign = ContentAlignment.TopLeft;
                    botonSecciones[i].Location = new Point(iPosXSecciones, 0);
                    botonSecciones[i].Size = new Size(140, 90);
                    botonSecciones[i].Text = "PISO 1";
                    botonSecciones[i].TextAlign = ContentAlignment.BottomRight;
                    botonSecciones[i].UseVisualStyleBackColor = false;
                    botonSecciones[i].FlatAppearance.BorderSize = 1;

                    botonSecciones[i].AccessibleDescription = dtSecciones.Rows[iCuentaSecciones]["color"].ToString();
                    botonSecciones[i].Name = dtSecciones.Rows[iCuentaSecciones]["id_pos_seccion_mesa"].ToString();
                    botonSecciones[i].Text = dtSecciones.Rows[iCuentaSecciones]["descripcion"].ToString();

                    if (i == 0)
                        botonSecciones[i].BackColor = Color.Pink;
                    else if (i == 1)
                        botonSecciones[i].BackColor = Color.LightGreen;
                    else if (i == 2)
                        botonSecciones[i].BackColor = Color.Yellow;
                    else if (i == 3)
                        botonSecciones[i].BackColor = Color.Turquoise;
                    else if (i == 4)
                        botonSecciones[i].BackColor = Color.Snow;
                    else if (i == 4)
                        botonSecciones[i].BackColor = Color.Orange;

                    botonSecciones[i].Click += boton_clic_secciones;

                    pnlSecciones.Controls.Add(botonSecciones[i]);

                    iCuentaSecciones++;
                    iCuentaSeccionesAyuda++;
                    iPosXSecciones += 140;

                    if (dtSecciones.Rows.Count == iCuentaSecciones)
                    {
                        btnSiguiente.Visible = false;
                        break;
                    }
                }

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

        //BOTON CLIC DE LAS SECCIONES
        public void boton_clic_secciones(object sender, EventArgs e)
        {
            bSeccionMesa = sender as Button;
            iIdPosSeccionMesa = Convert.ToInt32(bSeccionMesa.Name);
            lblPisos.Text = bSeccionMesa.Text.ToUpper();

            if (iBanderaCombinar == 0)
            {
                cargarMesas();
            }

            else
            {
                //mostrarBotonesCombinar();
            }

            //txtMesa.Clear();
            //txtMesa.Focus();
        }

        //FUNCION PARA VERIFICAR SI TIENE COMANDAS ACTIVAS
        private bool consultarComandasActivas()
        {
            try
            {
                sSql = "";
                sSql += "select CP.id_pos_mesa, getdate() - CP.fecha_apertura_orden tiempo," + Environment.NewLine;
                sSql += "CP.estado_orden, MESA.descripcion mesa" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_mesa MESA ON MESA.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and MESA.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and MESA.id_pos_seccion_mesa = " + iIdPosSeccionMesa + Environment.NewLine;
                sSql += "and CP.id_pos_mesa > 0";

                dtVerificadorMesa = new DataTable();
                dtVerificadorMesa.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtVerificadorMesa, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

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

        //FUNCION PARA CARGAR LAS MESAS
        private void cargarMesas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_mesa, numero_mesa, posicion_x, posicion_y, descripcion" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdPosSeccionMesa + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtMesas = new DataTable();
                dtMesas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtMesas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtMesas.Rows.Count > 0)
                {
                    if (mostrarBotonesMesas() == false)
                        return;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentras mesas configuradas en el área seleccinada.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LOS BOTONES DE LAS SECCIONES
        private bool mostrarBotonesMesas()
        {
            try
            {
                if (consultarComandasActivas() == false)
                {
                    return false;
                }

                pnlMesas.Controls.Clear();

                botonMesas = new Button[7, 10];

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        botonMesas[i, j] = new Button();
                        DataRow[] dFila = dtMesas.Select("posicion_x = " + i + " and posicion_y = " + j);

                        if (dFila.Length != 0)
                        {
                            botonMesas[i, j].Cursor = Cursors.Hand;
                            botonMesas[i, j].FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
                            botonMesas[i, j].FlatStyle = FlatStyle.Flat;
                            botonMesas[i, j].Font = new Font("Maiandra GD", 21.75F, FontStyle.Regular);
                            botonMesas[i, j].ForeColor = Color.Black;
                            botonMesas[i, j].Location = new Point(j * 85, i * 75);
                            botonMesas[i, j].Size = new Size(82, 72);
                            botonMesas[i, j].UseVisualStyleBackColor = false;
                            botonMesas[i, j].Name = dFila[0][0].ToString();
                            botonMesas[i, j].Text = dFila[0][1].ToString();
                            botonMesas[i, j].AccessibleName = dFila[0][4].ToString();
                            botonMesas[i, j].Click += boton_clic_Mesa;

                            iIdPosMesa = Convert.ToInt32(dFila[0][0].ToString());

                            for (int k = 0; k < dtVerificadorMesa.Rows.Count; k++)
                            {
                                if (iIdPosMesa == Convert.ToInt32(dtVerificadorMesa.Rows[k]["id_pos_mesa"].ToString()))
                                {
                                    if (dtVerificadorMesa.Rows[k]["estado_orden"].ToString().Trim().ToUpper() == "ABIERTA")
                                    {
                                        botonMesas[i, j].BackColor = Color.Red;
                                        botonMesas[i, j].Image = Properties.Resources.icono_cliente_seccion_mesas;
                                        ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA OCUPADA");
                                    }

                                    else
                                    {
                                        botonMesas[i, j].BackColor = Color.Cyan;
                                        botonMesas[i, j].Image = Properties.Resources.icono_reporte_precuenta;
                                        ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA EN PRECUENTA");
                                    }

                                    iBanderaMesas = 1;
                                    break;
                                }
                            }

                            if (iBanderaMesas == 1)
                            {
                                botonMesas[i, j].ImageAlign = ContentAlignment.TopLeft;
                                botonMesas[i, j].TextAlign = ContentAlignment.BottomRight;
                            }

                            else
                            {
                                botonMesas[i, j].BackColor = Color.Lime;
                                botonMesas[i, j].TextAlign = ContentAlignment.MiddleCenter;
                                ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA DISPONIBLE");
                            }
                        }

                        else
                        {
                            botonMesas[i, j].Visible = false;
                        }

                        pnlMesas.Controls.Add(botonMesas[i, j]);
                        iBanderaMesas = 0;
                    }
                }

                llenarGrid();

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

        //Función para dar clic en alguna mesa
        private void boton_clic_Mesa(Object sender, EventArgs e)
        {
            try
            {
                botonSeleccionado = sender as Button;

                if ((botonSeleccionado.BackColor == Color.Red) || (botonSeleccionado.BackColor == Color.Cyan))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "La mesa se encuentra ocupada";
                    ok.ShowDialog();
                    return;
                }

                Program.iIDMESA = Convert.ToInt32(botonSeleccionado.Name);
                sNombreMesa = botonSeleccionado.Text;

                iIdMesa = Convert.ToInt32(botonSeleccionado.Name);
                sDescripcionMesa = botonSeleccionado.AccessibleName;

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID DE TIEMPOS
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                for (int i = 0; i < dtVerificadorMesa.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                      dtVerificadorMesa.Rows[i]["mesa"].ToString().Trim().ToUpper(),
                                      dtVerificadorMesa.Rows[i]["estado_orden"].ToString().Trim().ToUpper(),
                                      Convert.ToDateTime(dtVerificadorMesa.Rows[i]["tiempo"].ToString()).ToString("HH:mm")
                        );

                    if (i % 2 == 0)
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }

                    else
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmCambioSeccionMesa_Load(object sender, EventArgs e)
        {
            int iCuenta = primerRegistroSecciones();

            if (iCuenta == -1)
                return;

            if (iCuenta == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No se encuentras secciones configuradas en el sistema.";
                ok.ShowDialog();
                return;
            }

            timerBlink.Start();
            cargarAreas();
            cargarMesas();
        }

        private void ttMensaje_Popup(object sender, PopupEventArgs e)
        {
            Random rand = new Random();
            int uno = rand.Next(0, 255);
            int dos = rand.Next(0, 255);
            int tres = rand.Next(0, 255);
            int cuatro = rand.Next(0, 255);

            lblPisos.ForeColor = Color.FromArgb(uno, dos, tres, cuatro);
        }

        private void btnSalirMesa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCambioSeccionMesa_KeyDown(object sender, KeyEventArgs e)
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
                        abrir = new Clases.ClaseAbrirCajon();
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }
    }
}
