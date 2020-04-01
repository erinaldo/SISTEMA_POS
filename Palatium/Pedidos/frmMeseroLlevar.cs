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
    public partial class frmMeseroLlevar : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseMeseros meseros = new Clases.ClaseMeseros();

        Button botonMesero;

        string sSql;
        string sDescripcionOrigen;
        string sNombreMesero;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdMesero;
        int iIdOrigenOrden;

        public frmMeseroLlevar(int iIdOrigenOrden, string sDescripcionOrigen)
        {
            this.iIdOrigenOrden = iIdOrigenOrden;
            this.sDescripcionOrigen = sDescripcionOrigen;
            InitializeComponent();
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
        
        //FUNCION PARA CONSULTAR Y PERMITIR EL ACCESO A LA COMANDA
        private void abrirMenuComanda()
        {
            try
            {
                if ((lblMesero.Text == "MESERO") && (Program.iLeerMesero == 1))
                {
                    ok.LblMensaje.Text = "Favor seleccione un mesero para continuar.";
                    ok.ShowDialog();
                }

                else
                {
                    Program.iIdMesero = iIdMesero;
                    Program.nombreMesero = lblMesero.Text;
                    this.DialogResult = DialogResult.OK;

                    //ACTUALIZACION ELVIS COMANDA
                    //=======================================================================================================================
                    ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(iIdOrigenOrden, sDescripcionOrigen, 0, 0, 0, "", Program.CAJERO_ID, iIdMesero, sNombreMesero, "NINGUNA", 0, 0, Program.iIdPersona);
                    or.ShowDialog();
                    //=======================================================================================================================
                    
                    //Orden or = new Orden(iIdOrigenOrden, sDescripcionOrigen, 0, 0, 0, "", Program.iIdPersona, Program.CAJERO_ID, iIdMesero, sNombreMesero, 0, 0);
                    //or.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LAS SECCIONES
        public void mostrarMeseros()
        {
            Button[,] boton = new Button[10, 10];
            int h = 0;

            //Program.saldo = double.Parse(txt_saldo.Text);
            if (meseros.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Click += boton_clic_mesero;
                        boton[i, j].MouseEnter += boton_mouse_enter;
                        boton[i, j].MouseLeave += boton_mouse_leave;
                        boton[i, j].BackColor = Color.Transparent;
                        boton[i, j].ForeColor = Color.White;
                        boton[i, j].FlatAppearance.BorderSize = 2;
                        boton[i, j].FlatStyle = FlatStyle.Flat;
                        boton[i, j].Cursor = Cursors.Hand;
                        boton[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                        boton[i, j].ForeColor = Color.White;
                        boton[i, j].BackColor = Color.Navy;
                        boton[i, j].Width = 125;
                        boton[i, j].Height = 75;
                        boton[i, j].Top = i * 75;
                        boton[i, j].Left = j * 125;
                        //boton[i, j].BackColor = Color.Cyan;

                        if (h == meseros.iCuenta)
                        {
                            break;
                        }

                        //boton[i, j].Font = new Font("Consolas", 11);
                        //En el tag se guarda el código de la seccion de la mesa
                        boton[i, j].Tag = meseros.Meseros[h].sIdMesero;
                        //En el text muestra la descripción
                        boton[i, j].Text = meseros.Meseros[h].sDescripcion;

                        this.Controls.Add(boton[i, j]);
                        pnlMeseros.Controls.Add(boton[i, j]);
                        h++;
                    }
                }
            }
            else
            {
                ok.LblMensaje.Text = "No hay ninguna seccion de mesas registrada en el sistema.";
                ok.ShowDialog();
            }
        }

        //BOTON CLIC 
        public void boton_clic_mesero(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            iIdMesero = Convert.ToInt32(botonMesero.Tag);
            lblMesero.Text = botonMesero.Text;
            sNombreMesero = botonMesero.Text.ToUpper();
        }

        //FUNCION MOUSE_ENTER
        public void boton_mouse_enter(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            ingresaBoton(botonMesero);
        }


        //FUNCION MOUSE_LEAVE
        private void boton_mouse_leave(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            salidaBoton(botonMesero);
        }

        #endregion


        private void btcancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btingresar_Click(object sender, EventArgs e)
        {
            abrirMenuComanda();
        }

        private void frmMeseroLlevar_Load(object sender, EventArgs e)
        {
            mostrarMeseros();
        }

        private void frmMeseroLlevar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btingresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btingresar);
        }

        private void btingresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btingresar);
        }

        private void btcancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btcancelar);
        }

        private void btcancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btcancelar);
        }
    }
}
