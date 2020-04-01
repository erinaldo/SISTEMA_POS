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
    public partial class frmAreasMesas : Form
    {
        //Constante para el número de mesas
        //Inicializar los botones de las mesas
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseSeccionMesa mesas = new Clases.ClaseSeccionMesa();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        Areas.ClaseCombinarComandas comandas = new Areas.ClaseCombinarComandas();

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
        int iIdMesa;
        int iBanderaCombinar;
        int iIdMesaCombinar;
        int iIdPedidoCombinar;
        int iBanderaEncuentraMesa;
        int iEjecutarCombinacion;
        int iCuentaRegistrosDevuelta;
        int iIdPedidoDevuelta;
        int iLabelX;
        int iLabelY;

        int iMesaComandaPrincipal;

        bool usada;
        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtVerificadorMesa;
        DataTable dtVerificadorPreCuenta;
        DataTable dtMesas;
        DataTable dtEjecutarCombinacion;
        

        //string sFecha;
        string sOrigen;
        string sSql;
        string sNombreMesaCombinar;
        string sOrdenCombinar;

        //VARIABLES DE LA RECETA
        //--------------------------------------------------------------------------
        double dbValorActual;

        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sAnio;
        string sMes;        
        string sReferenciaExterna_Sub;
        string sNombreSubReceta;

        int iBanderaDescargaStock;
        int iIdMovimientoStock;
        int iIdBodega;
        int iIdPedido;
        int iCgClienteProveedor_Sub;
        int iCgTipoMovimiento_Sub;
        int iIdPosSubReceta;

        int[] iRespuesta;

        DataTable dtReceta;
        DataTable dtSubReceta;

        //--------------------------------------------------------------------------

        Clases.ClaseVectores vector = new Clases.ClaseVectores();

        public frmAreasMesas()
        {
            botonMesas = new Button[7, 9];
            InitializeComponent();
            mostrarSeccionesMesa();
            mostrarBotones();
        }

        public frmAreasMesas(int iBandera, string Origen)
        {
            this.iVerificador = iBandera;
            this.sOrigen = Origen;
            botonMesas = new Button[7,9];
            InitializeComponent();
            mostrarSeccionesMesa();
            mostrarBotones();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            iBanderaCombinar = 0;
            iEjecutarCombinacion = 0;
            btnCombinar.Text = "COMBINAR" + Environment.NewLine + "MESAS";
            PanelMesas.Controls.Clear();
            mostrarBotones();

            txtMesa.Clear();
            txtMesa.Focus();

            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";

            btnCombinar.ForeColor = Color.White;
            btnCombinar.FlatAppearance.BorderColor = Color.White;
            btnCombinar.FlatAppearance.BorderSize = 2;

            btnCombinar.MouseEnter += new EventHandler(btnCombinar_MouseEnter);
            btnCombinar.MouseLeave += new EventHandler(btnCombinar_MouseLeave);
            btnCombinar.BackColor = Color.Transparent;
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

        //BOTON CLIC DE LAS SECCIONES
        public void boton_clic(object sender, EventArgs e)
        {
            bSeccionMesa = sender as Button;
            iIdSeccionMesa = Convert.ToInt32(bSeccionMesa.Tag);
            lblPisos.Text = bSeccionMesa.Text.ToUpper();

            if (iBanderaCombinar == 0)
            {
                mostrarBotones();
            }

            else
            {
                mostrarBotonesCombinar();
            }

            txtMesa.Clear();
            txtMesa.Focus();
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

        //Función para crear los botones de las mesas
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
            try
            {
                botonSelecionado = sender as Button;

                if (iBanderaCombinar == 0)
                {                    
                    Program.iIDMESA = Convert.ToInt32(botonSelecionado.Tag);
                    Program.sNombreMesa = botonSelecionado.AccessibleName.ToUpper();

                    if (iVerificador == 1)
                    {
                        int longitud = botonSelecionado.Text.Length - 12;

                        comprobarMesa(botonSelecionado);
                    }

                    else if (iVerificador == 2)
                    {
                        comprobarMesa(botonSelecionado);
                    }

                    else
                    {
                        comprobarMesa(botonSelecionado);
                    }
                }

                else if (iBanderaCombinar == 1)
                {
                    if (iMesaComandaPrincipal == 1)
                    {                        
                        consultarPedidoMesaContenedora(botonSelecionado);
                    }

                    else
                    {
                        if (botonSelecionado.BackColor == Color.Yellow)
                        {
                            botonSelecionado.BackColor = Color.Red;
                            ttMensaje.SetToolTip(botonSelecionado, "MESA OCUPADA");

                            if (dtEjecutarCombinacion.Rows.Count == 1)
                            {
                                crearDataTable();
                            }

                            else
                            {
                                //ELIMINAR LA MESA A A COMBINAR
                                for (int i = dtEjecutarCombinacion.Rows.Count -1; i >= 0; i--)
                                {
                                    if (Convert.ToInt32(dtEjecutarCombinacion.Rows[i][0].ToString()) == Convert.ToInt32(botonSelecionado.Tag))
                                    {
                                        dtEjecutarCombinacion.Rows.RemoveAt(i);
                                    }
                                }
                            }
                        }

                        else
                        {
                            //CONTAR CUANTAS ORDENES TIENE LA MESA OCUPADA
                            iCuentaRegistrosDevuelta = consultarPedidosMesa(Convert.ToInt32(botonSelecionado.Tag), "MESA " + botonSelecionado.Text);

                            if (iCuentaRegistrosDevuelta == 1)
                            {
                                botonSelecionado.BackColor = Color.Yellow;
                                ttMensaje.SetToolTip(botonSelecionado, "COMBINAR A " + sNombreMesaCombinar);

                                DataRow row = dtEjecutarCombinacion.NewRow();
                                row["idMesa"] = botonSelecionado.Tag.ToString();
                                row["idPedido"] = iIdPedidoDevuelta.ToString();
                                dtEjecutarCombinacion.Rows.Add(row);
                            }

                            else if (iCuentaRegistrosDevuelta == 0)
                            {
                                botonSelecionado.BackColor = Color.Yellow;
                                ttMensaje.SetToolTip(botonSelecionado, "COMBINAR A " + sNombreMesaCombinar);
                                return;
                            }
                        }
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
                if ((iVerificador == 2) || (iVerificador == 3))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "La mesa se encuentra ocupada";
                    ok.ShowDialog();
                }
                else
                {
                    VerificarCuentaMesa v = new VerificarCuentaMesa(botonSeleccionado.Tag.ToString(), botonSeleccionado);
                    v.ShowDialog();

                    if (v.DialogResult == DialogResult.OK)
                    {
                        //this.Close();
                        PanelMesas.Controls.Clear();
                        mostrarBotones();
                    }
                }
            }

            else
            {
                if (iVerificador != 2)
                {
                    NMesas m = new NMesas(botonSeleccionado, iVerificador, Convert.ToInt32(botonSelecionado.Tag));

                    if (Program.iBanderaNumeroMesa == 1)
                    {
                        m.Text = "INGRESE EL NÚMERO DE PERSONAS - " + botonSeleccionado.Text.ToUpper();
                    }

                    else
                    {
                        //m.Text = "INGRESE EL NUMERO DE PERSONAS - MESA " + botonSeleccionado.Text.ToUpper();
                        m.Text = "NUMERO DE PERSONAS";
                    }

                    m.ShowDialog();

                    if (m.DialogResult == DialogResult.OK)
                    {
                        m.Close();
                        this.Close();
                    }
                }
                else
                {
                    Orden orden = Owner as Orden;
                    //Orden orden = new Orden();
                    //int longitud = botonSelecionado.Text.Length - 12;
                    NMesas numeroPersona = new NMesas(1, orden.txt_numeropersonas.Text);
                    numeroPersona.ShowDialog();

                    if (numeroPersona.DialogResult == DialogResult.OK)
                    {
                        if (Program.iBanderaNumeroMesa == 0)
                        {
                            orden.txt_numeromesa.Text = "MESA " + botonSeleccionado.Text;
                        }

                        else
                        {
                            orden.txt_numeromesa.Text = botonSeleccionado.Text.Substring(0, 7);
                        }


                        //orden.txt_numeromesa.Text = botonSelecionado.Text.Substring(0, longitud);
                        orden.txt_numeropersonas.Text = Program.iNuevoNumeroPersonas.ToString();
                        numeroPersona.Close();
                        this.Close();
                    }

                }
            }
        }


        //Función para verificar si la mesa está con una orden
        private void verificarMesas()
        {
            sSql = "";
            sSql += "select id_pos_mesa, getdate() - fecha_apertura_orden tiempo" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where estado_orden = 'Abierta'" + Environment.NewLine;
            sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
            sSql += "and id_pos_mesa > 0 " + Environment.NewLine;
            sSql += "and estado = 'A'";

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
            sSql += "where estado = 'A'" + Environment.NewLine;
            sSql += "and id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
            sSql += "and estado_orden = 'Pre-Cuenta'" + Environment.NewLine;
            sSql += "and id_pos_mesa > 0 " + Environment.NewLine;
            sSql += "and estado = 'A'";

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

        //FUNCION PARA BUSCAR DATOS CON LA CAJA DE TEXTO
        private void verMesaTextBox()
        {
            try
            {
                Button btnSelecion = new Button();
                string sMesa_Descripcion, sMesa_Numero;

                sSql = "";
                sSql += "select id_pos_mesa, descripcion, numero_mesa" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where numero_mesa = " + Convert.ToInt32(txtMesa.Text.Trim()) + Environment.NewLine;
                sSql += "and id_pos_seccion_mesa = " + iIdSeccionMesa + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //Program.iIDMESA = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());

                        iIdMesa = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        sMesa_Descripcion = dtConsulta.Rows[0].ItemArray[1].ToString();
                        sMesa_Numero = dtConsulta.Rows[0].ItemArray[2].ToString();

                        btnSelecion.Tag = Program.iIDMESA.ToString();

                        if (Program.iBanderaNumeroMesa == 1)
                        {
                            btnSelecion.Text = sMesa_Descripcion;
                        }

                        else
                        {
                            btnSelecion.Text = sMesa_Numero;
                        }

                        sSql = "";
                        sSql += "select count(*) cuenta, estado_orden" + Environment.NewLine;
                        sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                        sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                        sSql += "and estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                        sSql += "and id_pos_mesa = " + Program.iIDMESA + Environment.NewLine;
                        sSql += "and estado = 'A'" + Environment.NewLine;
                        sSql += "group by estado_orden";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count == 0)
                            {
                                NMesas m = new NMesas(btnSelecion, iVerificador, iIdMesa);

                                if (Program.iBanderaNumeroMesa == 1)
                                {
                                    m.Text = "INGRESE EL NÚMERO DE PERSONAS - " + btnSelecion.Text.ToUpper();
                                }

                                else
                                {
                                    m.Text = "INGRESE EL NUMERO DE PERSONAS - MESA " + btnSelecion.Text.ToUpper();
                                }

                                txtMesa.Clear();
                                m.ShowDialog();

                                if (m.DialogResult == DialogResult.OK)
                                {
                                    m.Close();
                                    this.Close();
                                }
                            }

                            else
                            {
                                VerificarCuentaMesa v = new VerificarCuentaMesa(btnSelecion.Tag.ToString(), btnSelecion);
                                v.ShowDialog();

                                if (v.DialogResult == DialogResult.OK)
                                {
                                    //this.Close();
                                    PanelMesas.Controls.Clear();
                                    mostrarBotones();
                                }
                            }
                        }

                        else
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No existe el número de mesa ingresado.";
                        ok.ShowDialog();

                        txtMesa.Clear();
                        txtMesa.Focus();
                    }
                }

                else
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

        #endregion

        #region FUNCIONES DE COMBINACION DE COMANDAS

        //Función para crear los botones de las mesas en combinación
        public void mostrarBotonesCombinar()
        {
            try
            {
                Program.iIdPersonaFacturador = 0;
                Program.iIdentificacionFacturador = "";

                consultarDatos();                
                verificarMesas();               //BOTON COLOR ROJO
                verificarMesasPrecuenta();      //BOTON COLOR CELESTE

                if ((dtVerificadorMesa.Rows.Count == 0) && (dtVerificadorPreCuenta.Rows.Count == 0))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay mesas con comandas abiertas para combinar.";
                    ok.ShowDialog();
                }

                else if (dtVerificadorMesa.Rows.Count + dtVerificadorPreCuenta.Rows.Count == 1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Existe únicamente una mesa ocupada.";
                    ok.ShowDialog();
                }

                else
                {
                    crearDataTable();
                    PanelMesas.Controls.Clear();
                    iBanderaCombinar = 1;
                    iEjecutarCombinacion = 1;
                    Program.iIdPersonaFacturador = 0;
                    Program.iIdentificacionFacturador = "";

                    btnCombinar.Text = "COMBINAR" + Environment.NewLine + "MESAS OK";

                    btnCombinar.ForeColor = Color.Black;
                    btnCombinar.FlatAppearance.BorderColor = Color.White;
                    btnCombinar.FlatAppearance.BorderSize = 3;

                    btnCombinar.MouseEnter -= new EventHandler(btnCombinar_MouseEnter);
                    btnCombinar.MouseLeave -= new EventHandler(btnCombinar_MouseLeave);
                    btnCombinar.BackColor = Color.White;

                    botonMesas = new Button[7, 9];
                    //AQUI LLENAMOS EL PANEL
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            botonMesas[i, j] = new Button();
                            botonMesas[i, j].Cursor = Cursors.Hand;

                            DataRow[] dFila = dtMesas.Select("posicion_x = " + i + " and posicion_y = " + j);

                            if (dFila.Length != 0)
                            {
                                iIdMesaCombinar = Convert.ToInt32(dFila[0][0].ToString());
                                iBanderaEncuentraMesa = 0;

                                for (int k = 0; k < dtVerificadorMesa.Rows.Count; k++)
                                {
                                    if (iIdMesaCombinar == Convert.ToInt32(dtVerificadorMesa.Rows[k]["id_pos_mesa"].ToString()))
                                    {
                                        iBanderaEncuentraMesa = 1;
                                        break;
                                    }
                                }

                                if (iBanderaEncuentraMesa == 1)
                                {
                                    botonMesas[i, j].BackColor = Color.Red;
                                    ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA OCUPADA");
                                    goto continua;
                                }

                                //VERIFICAR EN PRECUENTA
                                for (int k = 0; k < dtVerificadorPreCuenta.Rows.Count; k++)
                                {
                                    if (iIdMesaCombinar == Convert.ToInt32(dtVerificadorPreCuenta.Rows[k]["id_pos_mesa"].ToString()))
                                    {
                                        iBanderaEncuentraMesa = 1;
                                        break;
                                    }
                                }

                                if (iBanderaEncuentraMesa == 1)
                                {
                                    botonMesas[i, j].BackColor = Color.Cyan;
                                    ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA EN PRECUENTA");
                                    goto continua;
                                }

                                else
                                {
                                    botonMesas[i, j].Tag = 0;
                                    botonMesas[i, j].Visible = false;
                                    goto continuar;
                                }

                            continua: { }
                                botonMesas[i, j].Font = new Font("Arial", 25, FontStyle.Bold);
                                botonMesas[i, j].ForeColor = Color.Black;
                                botonMesas[i, j].Tag = dFila[0][0].ToString();
                                botonMesas[i, j].Text = dFila[0][1].ToString();
                                botonMesas[i, j].AccessibleName = dFila[0][4].ToString();

                                botonMesas[i, j].TextAlign = ContentAlignment.MiddleCenter;
                                botonMesas[i, j].Click += boton_clic_Mesa;
                                botonMesas[i, j].Width = 97;//90
                                botonMesas[i, j].Height = 87;//80
                                botonMesas[i, j].Top = i * 82;
                                botonMesas[i, j].Left = j * 92;
                                ttMensajeMesas.SetToolTip(botonMesas[i, j], "MESA DISPONIBLE");
                            }

                            else
                            {
                                botonMesas[i, j].Tag = 0;
                                botonMesas[i, j].Visible = false;
                                goto continuar;
                            }

                        continuar: { }
                            this.Controls.Add(botonMesas[i, j]);
                            PanelMesas.Controls.Add(botonMesas[i, j]);

                        }
                    }

                    //AQUI PREGUNTAR MESA ORIGEN
                    iMesaComandaPrincipal = 1;
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Seleccione mesa contenedora de comandas a combinar.";
                    ok.ShowDialog();

                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL DATATABLE
        private void crearDataTable()
        {
            try
            {
                dtEjecutarCombinacion = new DataTable();
                dtEjecutarCombinacion.Clear();
                dtEjecutarCombinacion.Columns.Add("idMesa");
                dtEjecutarCombinacion.Columns.Add("idPedido");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LOS PEDIDOS DE LAS MESAS
        private int consultarPedidosMesa(int iIdMesaClic, string sMesaClic)
        {
            try
            {
                sSql = "";
                sSql += "select CP.id_pedido, isnull(CP.comentarios, '') comentarios, NCP.numero_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NCP" + Environment.NewLine;
                sSql += "where CP.id_pedido = NCP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NCP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_Orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and CP.id_pos_mesa = " + iIdMesaClic;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 1)
                    {
                        iIdPedidoDevuelta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        return 1;
                    }

                    else if (dtConsulta.Rows.Count > 1)
                    {
                        Areas.frmOrdenesMesas ordenes = new Areas.frmOrdenesMesas(iIdMesaClic, dtConsulta, sMesaClic);
                        ordenes.ShowDialog();

                        if (ordenes.DialogResult == DialogResult.OK)
                        {
                            for (int i = 0; i < ordenes.dtCombinar.Rows.Count; i++)
                            {
                                DataRow row = dtEjecutarCombinacion.NewRow();
                                row["idMesa"] = iIdMesaClic;
                                row["idPedido"] = ordenes.dtCombinar.Rows[i][1].ToString();
                                dtEjecutarCombinacion.Rows.Add(row);
                            }

                            ordenes.Close();
                        }

                        return 0;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CONSULTAR EL ID PEDIDO DE LA MESA CONTENEDORA
        private void consultarPedidoMesaContenedora(Button btnClic)
        {
            try
            {
                sSql = "";
                sSql += "select CP.id_pedido, NCP.numero_pedido, CP.estado_orden" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NCP" + Environment.NewLine;
                sSql += "where CP.id_pedido = NCP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NCP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_Orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and CP.id_pos_mesa = " + btnClic.Tag;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    sNombreMesaCombinar = "MESA " + btnClic.Text;

                    if (dtConsulta.Rows.Count == 1)
                    {
                        iIdPedidoCombinar = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        sOrdenCombinar = dtConsulta.Rows[0][1].ToString();

                        DataRow row = dtEjecutarCombinacion.NewRow();
                        row["idMesa"] = btnClic.Tag;
                        row["idPedido"] = dtConsulta.Rows[0][0].ToString();
                        dtEjecutarCombinacion.Rows.Add(row);
                        goto eventos_boton;
                        
                    }

                    else if (dtConsulta.Rows.Count > 1)
                    {
                        Areas.frmOrdenesMesaContenedora ordenes = new Areas.frmOrdenesMesaContenedora(Convert.ToInt32(btnClic.Tag), dtConsulta, sNombreMesaCombinar);
                        ordenes.ShowDialog();

                        if (ordenes.DialogResult == DialogResult.OK)
                        {
                            for (int i = 0; i < ordenes.dtCombinar.Rows.Count; i++)
                            {
                                iIdPedidoCombinar = Convert.ToInt32(ordenes.dtCombinar.Rows[i][1].ToString());

                                DataRow row = dtEjecutarCombinacion.NewRow();
                                row["idMesa"] = btnClic.Tag;
                                row["idPedido"] = ordenes.dtCombinar.Rows[i][1].ToString();
                                dtEjecutarCombinacion.Rows.Add(row);
                            }

                            ordenes.Close();
                            goto eventos_boton;
                        }
                    }

                    eventos_boton: {
                        iMesaComandaPrincipal = 0;
                        botonSelecionado.BackColor = Color.Magenta;
                        ttMensaje.SetToolTip(botonSelecionado, "MESA CONTENEDORA A COMBINAR");
                        botonSelecionado.Enabled = false;                        
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LOS DATOS DE LAS ORDENES EN MESAS A COMBINAR
        private void consultarOrdenesCombinar()
        {
            try
            {
                sSql = "";
                sSql += "select sum(isnull(cantidad, 0)) cantidad, id_producto, nombre, precio_unitario," + Environment.NewLine;
                sSql += "sum(valor_dscto) valor_dscto, sum(valor_otro) valor_otro, id_pos_cortesia, motivo_cortesia, id_pos_cancelacion_productos," + Environment.NewLine;
                sSql += "motivo_cancelacion, comentario, cortesia, cancelacion, id_pos_mascara_item, descripcion" + Environment.NewLine;
                sSql += "from pos_vw_detalle_productos_comanda" + Environment.NewLine;
                sSql += "where id_pedido in (";

                for (int i = 0; i < dtEjecutarCombinacion.Rows.Count; i++)
                {
                    sSql += dtEjecutarCombinacion.Rows[i][1].ToString();

                    if (i + 1 != dtEjecutarCombinacion.Rows.Count)
                    {                        
                        sSql += ", ";
                    }
                }

                sSql += ")" + Environment.NewLine;
                sSql += "group by id_producto, nombre, precio_unitario, id_pos_cortesia," + Environment.NewLine;
                sSql += "motivo_cortesia, id_pos_cancelacion_productos, motivo_cancelacion, comentario," + Environment.NewLine;
                sSql += "cortesia, cancelacion, id_pos_mascara_item, descripcion" + Environment.NewLine;
                sSql += "order by id_producto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (comandas.recibirParametrosCombinacion(iIdMesaCombinar, iIdPedidoCombinar, sOrdenCombinar, dtEjecutarCombinacion, dtConsulta) == false)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Ocurrió un problema al combinar las comandas de las mesas";
                        ok.ShowDialog();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "Comandas combinadas éxitosamante.";
                        ok.ShowDialog();
                    }

                    iBanderaCombinar = 0;
                    iEjecutarCombinacion = 0;
                    btnCombinar.Text = "COMBINAR" + Environment.NewLine + "MESAS";
                    PanelMesas.Controls.Clear();
                    mostrarBotones();

                    txtMesa.Clear();
                    txtMesa.Focus();

                    Program.iIdPersonaFacturador = 0;
                    Program.iIdentificacionFacturador = "";

                    btnCombinar.ForeColor = Color.White;
                    btnCombinar.FlatAppearance.BorderColor = Color.White;
                    btnCombinar.FlatAppearance.BorderSize = 2;

                    btnCombinar.MouseEnter += new EventHandler(btnCombinar_MouseEnter);
                    btnCombinar.MouseLeave += new EventHandler(btnCombinar_MouseLeave);
                    btnCombinar.BackColor = Color.Transparent;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                
        #endregion
        
        private void btnSalirMesa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtMesa.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el número de mesa.";
                ok.ShowDialog();
                txtMesa.Focus();
            }

            else
            {
                verMesaTextBox();
            }
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

        private void txtMesa_KeyPress(object sender, KeyPressEventArgs e)
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
                if (txtMesa.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese el número de mesa.";
                    ok.ShowDialog();
                    txtMesa.Focus();
                }

                else
                {
                    verMesaTextBox();
                }
            }
        }

        private void frmAreasMesas_KeyDown(object sender, KeyEventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            PanelMesas.Controls.Clear();
            mostrarBotones();

            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";
        }

        private void frmAreasMesas_Load(object sender, EventArgs e)
        {
            //Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            //redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);

            Program.iIDMESA = 0;
            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";
            timerBlink.Start();

            this.ActiveControl = lblPisos;
        }

        private void btnActualizar_MouseEnter(object sender, EventArgs e)
        {
            btnActualizar.ForeColor = Color.Black;
            btnActualizar.FlatAppearance.BorderColor = Color.White;
            btnActualizar.FlatAppearance.BorderSize = 3;
        }

        private void btnSalirMesa_MouseEnter(object sender, EventArgs e)
        {
            btnSalirMesa.ForeColor = Color.Black;
            btnSalirMesa.FlatAppearance.BorderColor = Color.White;
            btnSalirMesa.FlatAppearance.BorderSize = 3;
        }

        private void btnSalirMesa_MouseLeave(object sender, EventArgs e)
        {
            btnSalirMesa.ForeColor = Color.White;
            btnSalirMesa.FlatAppearance.BorderColor = Color.White;
            btnSalirMesa.FlatAppearance.BorderSize = 2;
        }

        private void btnActualizar_MouseLeave(object sender, EventArgs e)
        {
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatAppearance.BorderColor = Color.White;
            btnActualizar.FlatAppearance.BorderSize = 2;
        }

        private void btnCombinar_Click(object sender, EventArgs e)
        {            
            if (iEjecutarCombinacion == 0)
            {
                txtMesa.Clear();
                txtMesa.Focus();
                mostrarBotonesCombinar();                
            }

            else if (iEjecutarCombinacion == 1)
            {
                int iContarEjecutar = dtEjecutarCombinacion.Rows.Count;

                if (iContarEjecutar == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay mesas para combinar las comandas.";
                    ok.ShowDialog();
                }

                else if (iContarEjecutar == 1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay seleccionado las mesas a combinar.";
                    ok.ShowDialog();
                }

                else
                {
                    consultarOrdenesCombinar();
                }
            }
        }

        private void btnCombinar_MouseEnter(object sender, EventArgs e)
        {
            btnCombinar.ForeColor = Color.Black;
            btnCombinar.FlatAppearance.BorderColor = Color.White;
            btnCombinar.FlatAppearance.BorderSize = 3;
        }

        private void btnCombinar_MouseLeave(object sender, EventArgs e)
        {
            btnCombinar.ForeColor = Color.White;
            btnCombinar.FlatAppearance.BorderColor = Color.White;
            btnCombinar.FlatAppearance.BorderSize = 2;
        }
    }
}
