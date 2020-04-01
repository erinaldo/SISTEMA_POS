using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using ArregloDeBotones;


namespace Palatium
{
    public partial class NMesas : Form
    {
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseMeseros meseros = new Clases.ClaseMeseros();

        DataTable dtConsulta;
        
        //bool bRespuesta;

        string sSql;                
        string sNombreMesero;
        
        Button botonMesero;
        public Button boton;

        int iIdMesero;
        int iBanderaPersonas = 0;
        int iNumeroPersonas;
        int iIdMesa;
        public int iVerificador;

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtValor.Text = txtValor.Text + sValor;
                txtValor.Focus();
                txtValor.SelectionStart = txtValor.Text.Trim().Length;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR Y PERMITIR EL ACCESO A LA COMANDA
        private void abrirMenuComanda()
        {
            try
            {
                if (iBanderaPersonas == 1)
                {
                    Program.iNuevoNumeroPersonas = Convert.ToInt32(txtValor.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //int numeroMaximo = 99;

                    if (txtValor.Text.Trim() == "")
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Favor ingrese la cantidad de personas para la mesa.";
                        ok.ShowDialog();

                        txtValor.Focus();
                        txtValor.SelectionStart = txtValor.Text.Trim().Length;
                    }

                    else if (Convert.ToInt32(txtValor.Text.Trim()) > 99)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "El número de Personas ha excedido el límite.";
                        ok.ShowDialog();
                        txtValor.Text = "";

                        txtValor.Focus();
                        txtValor.SelectionStart = txtValor.Text.Trim().Length;
                    }

                    else if (Convert.ToInt32(txtValor.Text.Trim()) == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "El número de Personas debe ser diferente de 0.";
                        ok.ShowDialog();
                        txtValor.Text = "";

                        txtValor.Focus();
                        txtValor.SelectionStart = txtValor.Text.Trim().Length;
                    }

                    else if ((lblMesero.Text == "MESERO") && (Program.iLeerMesero == 1))
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Favor seleccione un mesero para continuar.";
                        ok.ShowDialog();

                        txtValor.Focus();
                        txtValor.SelectionStart = txtValor.Text.Trim().Length;
                    }

                    else
                    {
                        //Program.iIdMesero = Convert.ToInt32(cmbMesero.SelectedValue);
                        //Program.nombreMesero = cmbMesero.Text;

                        if (Program.iLeerMesero == 1)
                        {
                            Program.iIdMesero = iIdMesero;
                            Program.nombreMesero = lblMesero.Text;
                        }

                        else
                        {
                            iIdMesero = Program.iIdMesero;
                        }

                        //ACTUALIZACION ELVIS COMANDA

                        int iIdPersona_Rec;

                        if (Program.iBanderaConsumoVale == 1)
                            iIdPersona_Rec = Program.iIdPersonaConsumoVale;
                        else
                            iIdPersona_Rec = Program.iIdPersona;

                        //=======================================================================================================================
                        ComandaNueva.frmComanda o = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, Convert.ToInt32(txtValor.Text.Trim()), iIdMesa, 0, "", Program.CAJERO_ID, iIdMesero, sNombreMesero, Program.sNombreMesa, 0, 0, iIdPersona_Rec);
                        this.DialogResult = DialogResult.OK;
                        o.ShowDialog();
                        //=======================================================================================================================

                        //Orden o = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, Convert.ToInt32(txtValor.Text.Trim()), iIdMesa, 0, "", Program.iIdPersona, Program.CAJERO_ID, iIdMesero, sNombreMesero, 0, 0);
                        //this.DialogResult = DialogResult.OK;
                        //o.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA POSICIONAR LOS CONTROLES
        private void posicionarControles()
        {
            try
            {
                if (Program.iLeerMesero == 1)
                {
                    this.Width = 757;
                    mostrarMeseros();
                }

                else
                {
                    this.Width = 270;

                    pnlCombo.Location = new Point(1, 336);
                }

                centrarFormulario();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CAMBIAR TAMAÑO DE FORMULARIO Y CENTRAR
        private void centrarFormulario()
        {
            try
            {
                int boundWidth = Screen.PrimaryScreen.Bounds.Width;
                int boundHeight = Screen.PrimaryScreen.Bounds.Height;
                int x = boundWidth - this.Width;
                int y = boundHeight - this.Height;
                this.Location = new Point(x / 2, y / 2);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS MESEROS
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
                        boton[i, j].Cursor = Cursors.Hand;
                        boton[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                        boton[i, j].ForeColor = Color.White;
                        boton[i, j].BackColor = Color.Navy;
                        boton[i, j].Width = 125;
                        boton[i, j].Height = 75;
                        boton[i, j].Top = i * 75;
                        boton[i, j].Left = j * 125;

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
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ninguna seccion de mesas registrada en el sistema.";
                ok.ShowDialog();
            }
        }

        //FUNCION CLIC
        public void boton_clic_mesero(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            iIdMesero = Convert.ToInt32(botonMesero.Tag);
            lblMesero.Text = botonMesero.Text;
            sNombreMesero = botonMesero.Text;

            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        //FUNCION MOUSE_ENTER
        public void boton_mouse_enter(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            ingresaBoton(botonMesero);
            //salidaBoton(botonMesero);
        }

        //FUNCION MOUSE_LEAVE
        private void boton_mouse_leave(object sender, EventArgs e)
        {
            botonMesero = sender as Button;
            salidaBoton(botonMesero);
            //ingresaBoton(botonMesero);
        }

        //FUNCION PARA CARGAR LOS MESEROS
        private void cargarMeseros()
        {
            try
            {
                sSql = "select id_pos_mesero, descripcion from pos_mesero where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbMesero.llenar(dtConsulta, sSql);
                cmbMesero.SelectedValue = Program.iIdMesero.ToString();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DE CONTROL DE BOTONES

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
        
        public NMesas(Button boton,int iVerificador, int iIdMesa)
        {
            //this.sNombreOrigen = Origen;
            this.iIdMesa = iIdMesa;
            this.iVerificador = iVerificador;
            Program.controlMesa = boton;
            InitializeComponent();
            this.boton = boton;
        }

        public NMesas(int iBandera, string iNumeroPersonas)
        {
            this.iNumeroPersonas = Convert.ToInt32(iNumeroPersonas);
            this.iBanderaPersonas = iBandera;
            InitializeComponent();
            txtValor.Text = iNumeroPersonas.ToString();
        }

        private void NMesas_Load(object sender, EventArgs e)
        {
            posicionarControles();
            cargarMeseros();

            if (Program.iNumeroPersonasDefault == 0)
            {
                txtValor.Text = "";
            }

            else
            {
                txtValor.Text = Program.iNumeroPersonasDefault.ToString();
            }

            sNombreMesero = Program.nombreMesero;
            iIdMesero = Program.iIdMesero;
            lblMesero.Text = Program.nombreMesero.ToUpper();
            this.ActiveControl = txtValor;
        }

        private void btcancelar_Click(object sender, EventArgs e)
        {            
            try
            {
                if (boton.BackColor == Color.Red)
                {
                    boton.BackColor = Control.DefaultBackColor;
                }
                this.Hide();    
            }

            catch (Exception)
            {
                this.Hide();
            }
            
        }

        private void textBoxcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                abrirMenuComanda();
            }
        }

        private void NMesas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtValor.Text.Length > 0)
            {
                str = txtValor.Text.Substring(txtValor.Text.Length - 1);
                loc = txtValor.Text.Length;
                txtValor.Text = txtValor.Text.Remove(loc - 1, 1);
            }

            txtValor.Focus();
            txtValor.SelectionStart = txtValor.Text.Trim().Length;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {            
            try
            {
                if (boton.BackColor == Color.Red)
                {
                    boton.BackColor = Control.DefaultBackColor;
                }

                this.Hide();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Hide();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            abrirMenuComanda();
        }

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn0);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn1);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn2);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn3);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn4);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn5);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn6);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn7);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn8);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn9);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn9);
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnRetroceder_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRetroceder);
        }

        private void btnRetroceder_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRetroceder);
        }
    }
}
