using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Áreas
{
    public partial class frmCambioMesa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        Clases.ClaseSeccionMesa mesas = new Clases.ClaseSeccionMesa();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        ToolTip ttMensajeMesas = new ToolTip();

        Button[,] botonMesas;
        Button botonSelecionado;
        Button bSeccionMesa;

        Label[,] lblTiempo;

        public int minutero = 0;
        int iVerificadorMesaUsada;
        int iVerificadorPrecuenta;
        int iInicio = 0;
        int iVerificador;
        int iIdSeccionMesa = 1;
        int iLabelX;
        int iLabelY;

        public int iIdMesa;
        public string sDescripcionMesa;

        bool usada;
        bool bRespuesta;

        DataTable dtMinutero;
        DateTime input;
        DateTime output;
        DateTime convertir;
        DataTable dtConsulta;
        DataTable dtVerificadorMesa;
        DataTable dtVerificadorPreCuenta;
        DataTable dtMesas;

        string horaRango;
        string horaInicio;
        string msg;
        //string sFecha;
        string sIdOrden;
        string sSqlMinutero;
        string sOrigen;
        string sSql;
        public string sNombreMesa;

        Clases.ClaseVectores vector = new Clases.ClaseVectores();

        public frmCambioMesa()
        {
            botonMesas = new Button[7, 9];
            InitializeComponent();
            mostrarSeccionesMesa();
            mostrarBotones();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LAS SECCIONES
        public void mostrarSeccionesMesa()
        {
            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";

            Button[,] boton = new Button[10, 10];
            int h = 0;

            //Program.saldo = double.Parse(txt_saldo.Text);
            if (mesas.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    boton[i, 0] = new Button();
                    boton[i, 0].Click += boton_clic;
                    boton[i, 0].Width = 200;
                    boton[i, 0].Height = 50;
                    boton[i, 0].Top = i * 50;
                    boton[i, 0].Left = 0;

                    if (i == 0)
                        boton[i, 0].BackColor = Color.Pink;
                    else if (i == 1)
                        boton[i, 0].BackColor = Color.LightGreen;
                    else if (i == 2)
                        boton[i, 0].BackColor = Color.Yellow;
                    else if (i == 3)
                        boton[i, 0].BackColor = Color.Turquoise;
                    else if (i == 4)
                        boton[i, 0].BackColor = Color.Snow;
                    else if (i == 4)
                        boton[i, 0].BackColor = Color.Pink;

                    if (h == mesas.iCuenta)
                    {
                        break;
                    }

                    boton[i, 0].Font = new Font("Consolas", 11);
                    //En el tag se guarda el código de la seccion de la mesa
                    boton[i, 0].Tag = mesas.seccionMesa[h].sIdSeccionMesa;
                    //En el text muestra la descripción
                    boton[i, 0].Text = mesas.seccionMesa[h].sDescripcion;
                    //cargar el color 
                    boton[i, 0].AccessibleDescription = mesas.seccionMesa[h].sColor;
                    boton[i, 0].Cursor = Cursors.Hand;

                    if (iInicio == 0)
                    {
                        iInicio = 1;
                    }

                    this.Controls.Add(boton[i, 0]);
                    pnlSeccionMesa.Controls.Add(boton[i, 0]);
                    h++;
                }

                lblPisos.Text = mesas.seccionMesa[0].sDescripcion.ToUpper();
            }
            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ninguna seccion de mesas registrada en el sistema.";
                ok.ShowDialog();
            }
        }

        public void boton_clic(object sender, EventArgs e)
        {
            bSeccionMesa = sender as Button;
            iIdSeccionMesa = Convert.ToInt32(bSeccionMesa.Tag);
            lblPisos.Text = bSeccionMesa.Text.ToUpper();
            mostrarBotones();
        }

        //FUNCION PARA LLENAR EL DATATABLE Y DISEÑAR LA INTERFAZ DE MESAS
        private void consultarDatos()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_mesa, numero_mesa, posicion_x, posicion_y, descripcion" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdSeccionMesa + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtMesas = new DataTable();
                dtMesas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtMesas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void mostrarBotones()
        {
            try
            {
                Program.iIdPersonaFacturador = 0;
                Program.iIdentificacionFacturador = "";

                consultarDatos();
                PanelMesas.Controls.Clear();
                verificarMesas();
                verificarMesasPrecuenta();

                botonMesas = new Button[7, 9];
                lblTiempo = new Label[7, 9];


                iLabelY = 54;

                //AQUI LLENAMOS EL PANEL
                for (int i = 0; i < 7; i++)
                {
                    iLabelX = 28;

                    for (int j = 0; j < 9; j++)
                    {
                        botonMesas[i, j] = new Button();
                        lblTiempo[i, j] = new Label();

                        botonMesas[i, j].Cursor = Cursors.Hand;
                        lblTiempo[i, j].Cursor = Cursors.Hand;
                        lblTiempo[i, j].Visible = false;

                        DataRow[] dFila = dtMesas.Select("posicion_x = " + i + " and posicion_y = " + j);

                        if (dFila.Length != 0)
                        {
                            botonMesas[i, j].Tag = dFila[0][0].ToString();
                            botonMesas[i, j].Text = dFila[0][1].ToString();
                            botonMesas[i, j].AccessibleName = dFila[0][4].ToString();
                            botonMesas[i, j].BackColor = Color.Lime;
                            botonMesas[i, j].Click += boton_clic_Mesa;
                            botonMesas[i, j].Width = 97;
                            botonMesas[i, j].Height = 87;
                            botonMesas[i, j].Top = i * 82;
                            botonMesas[i, j].Left = j * 92;
                            botonMesas[i, j].ForeColor = Color.Black;
                            ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA DISPONIBLE");
                            botonMesas[i, j].Font = new Font("Arial", 21, FontStyle.Bold);
                            botonMesas[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        }

                        else
                        {
                            botonMesas[i, j].Tag = 0;
                            botonMesas[i, j].Visible = false;
                            goto continuar;
                        }

                        //CONSULTAR SI LA MESA ESTÁ USADA
                        if (iVerificadorMesaUsada == 1)
                        {
                            for (int k = 0; k < dtVerificadorMesa.Rows.Count; k++)
                            {
                                if (Convert.ToInt32(botonMesas[i, j].Tag) == Convert.ToInt32(dtVerificadorMesa.Rows[k].ItemArray[0].ToString()))
                                {
                                    botonMesas[i, j].BackColor = Color.Red;
                                    ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA OCUPADA");
                                    lblTiempo[i, j].Visible = true;
                                    botonMesas[i, j].TextAlign = ContentAlignment.TopCenter;
                                    lblTiempo[i, j].AutoSize = false;
                                    lblTiempo[i, j].BackColor = Color.Red;
                                    lblTiempo[i, j].TextAlign = ContentAlignment.MiddleCenter;
                                    lblTiempo[i, j].Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                                    lblTiempo[i, j].Width = 40;
                                    lblTiempo[i, j].Height = 14;
                                    lblTiempo[i, j].Location = new Point(iLabelX, iLabelY);
                                    lblTiempo[i, j].ForeColor = Color.White;
                                    lblTiempo[i, j].Text = Convert.ToDateTime(dtVerificadorMesa.Rows[k][1].ToString()).ToString("HH:mm");
                                }
                            }
                        }

                        //CONSULTAR SI LA MESA ESTÁ EN PRECUENTA
                        if (iVerificadorPrecuenta == 1)
                        {
                            for (int k = 0; k < dtVerificadorPreCuenta.Rows.Count; k++)
                            {
                                if (Convert.ToInt32(botonMesas[i, j].Tag) == Convert.ToInt32(dtVerificadorPreCuenta.Rows[k].ItemArray[0].ToString()))
                                {
                                    botonMesas[i, j].BackColor = Color.Cyan;
                                    ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA EN PRECUENTA");
                                    lblTiempo[i, j].Visible = true;
                                    botonMesas[i, j].TextAlign = ContentAlignment.TopCenter;
                                    lblTiempo[i, j].AutoSize = false;
                                    lblTiempo[i, j].BackColor = Color.Cyan;
                                    lblTiempo[i, j].TextAlign = ContentAlignment.MiddleCenter;
                                    lblTiempo[i, j].Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                                    lblTiempo[i, j].Width = 40;
                                    lblTiempo[i, j].Height = 14;
                                    lblTiempo[i, j].Location = new Point(iLabelX, iLabelY);
                                    lblTiempo[i, j].ForeColor = Color.Black;
                                    lblTiempo[i, j].Text = Convert.ToDateTime(dtVerificadorPreCuenta.Rows[k][1].ToString()).ToString("HH:mm");
                                }
                            }
                        }

                    continuar: { }

                        iLabelX += 92;
                        PanelMesas.Controls.Add(lblTiempo[i, j]);
                        PanelMesas.Controls.Add(botonMesas[i, j]);

                    }

                    iLabelY += 82;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para dar clic en alguna mesa
        private void boton_clic_Mesa(Object sender, EventArgs e)
        {
            botonSelecionado = sender as Button;

            Program.iIDMESA = Convert.ToInt32(botonSelecionado.Tag);
            sNombreMesa = botonSelecionado.Text;

            iIdMesa = Convert.ToInt32(botonSelecionado.Tag);
            sDescripcionMesa = botonSelecionado.AccessibleName;

            comprobarMesa(botonSelecionado);
        }

        //Función para ver si la mesa está usada
        private void comprobarMesa(Button botonSeleccionado)
        {
            if (botonSeleccionado.BackColor == Color.Red || botonSeleccionado.BackColor == Color.Cyan)
            {
                usada = true;
            }

            else
            {
                usada = false;
            }


            if (usada == true)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "La mesa se encuentra ocupada";
                ok.ShowDialog();
            }

            else
            {

                this.DialogResult = DialogResult.OK;
            }
        }


        //Función para verificar si la mesa está con una orden
        private void verificarMesas()
        {
            sSql = "";
            sSql += "select id_pos_mesa, getdate() - fecha_apertura_orden tiempo" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
            sSql += "and estado_orden = 'Abierta'" + Environment.NewLine;
            sSql += "and id_pos_mesa > 0 ";

            dtVerificadorMesa = new DataTable();

            dtVerificadorMesa.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtVerificadorMesa, sSql);

            if (bRespuesta == true)
            {
                iVerificadorMesaUsada = 1;
            }
            else
            {
                iVerificadorMesaUsada = 0;
            }

        }

        //Función para verificar si la mesa está con una orden
        private void verificarMesasPrecuenta()
        {
            sSql = "";
            sSql += "select id_pos_mesa, getdate() - fecha_apertura_orden tiempo" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
            sSql += "and estado_orden = 'Pre-Cuenta'" + Environment.NewLine;
            sSql += "and id_pos_mesa > 0 ";

            dtVerificadorPreCuenta = new DataTable();

            dtVerificadorPreCuenta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtVerificadorPreCuenta, sSql);

            if (bRespuesta == true)
            {
                iVerificadorPrecuenta = 1;
            }
            else
            {
                iVerificadorPrecuenta = 0;
            }

        }

        #endregion

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            PanelMesas.Controls.Clear();
            mostrarBotones();
        }

        private void frmCambioMesa_KeyDown(object sender, KeyEventArgs e)
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

        private void frmCambioMesa_Load(object sender, EventArgs e)
        {
            Program.iIDMESA = 0;
            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";
            timerBlink.Start();        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PanelMesas.Controls.Clear();
            mostrarBotones();

            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";
        }

        
        private void timerBlink_Tick(object sender, EventArgs e)
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
    }
}
