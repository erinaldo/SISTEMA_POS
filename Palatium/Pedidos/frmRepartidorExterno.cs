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
    public partial class frmRepartidorExterno : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases.ClaseRepartidorExterno repartidor = new Clases.ClaseRepartidorExterno();
        Clases.ClaseLimpiarArreglos limpiarArreglos = new Clases.ClaseLimpiarArreglos();
        Clases.ClaseEtiquetaUsuario etiqueta = new Clases.ClaseEtiquetaUsuario();

        DataTable dtConsulta;

        Button bRepartidor;

        int iIdRepartidor;

        string sSql;

        bool bRespuesta;

        public frmRepartidorExterno()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CONSULTA ÀRA HABILITAR LAS OPCIONES
        private void consultarDatos(string sOpcion)
        {
            try
            {
                Program.sIDPERSONA = null;
                Program.iIdPersonaFacturador = 0;
                Program.iIdentificacionFacturador = "";

                Program.iDomicilioEspeciales = 0;
                Program.iModoDelivery = 0;
                Program.iIDMESA = 0;

                Program.dbValorPorcentaje = 25;
                Program.dbDescuento = Program.dbValorPorcentaje / 100;

                limpiarArreglos.limpiarArregloComentarios();

                sSql = "";
                sSql = sSql + "select id_pos_origen_orden, descripcion, genera_factura," + Environment.NewLine;
                sSql = sSql + "id_persona, id_pos_modo_delivery, presenta_opcion_delivery," + Environment.NewLine;
                sSql = sSql + "codigo, id_pos_tipo_forma_cobro, maneja_servicio" + Environment.NewLine;
                sSql = sSql + "from pos_origen_orden where codigo = '" + sOpcion + "'" + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    Program.iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    Program.sDescripcionOrigenOrden = dtConsulta.Rows[0].ItemArray[1].ToString();
                    Program.iGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());
                    Program.iManejaServicioOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[8].ToString());

                    if ((dtConsulta.Rows[0].ItemArray[3].ToString() == null) || (dtConsulta.Rows[0].ItemArray[3].ToString() == ""))
                    {
                        Program.iIdPersonaOrigenOrden = 0;
                    }

                    else
                    {
                        Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[3].ToString());
                        Program.sIDPERSONA = dtConsulta.Rows[0].ItemArray[3].ToString();

                    }
                    Program.iIdPosModoDelivery = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString());
                    Program.iPresentaOpcionDelivery = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString());
                    Program.sCodigoAsignadoOrigenOrden = dtConsulta.Rows[0].ItemArray[6].ToString();

                    if (Program.iGeneraFactura == 0)
                    {
                        sSql = "";
                        sSql = sSql + "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                        sSql = sSql + "from pos_tipo_forma_cobro" + Environment.NewLine;
                        sSql = sSql + "where id_pos_tipo_forma_cobro = " + Convert.ToInt32(dtConsulta.Rows[0].ItemArray[7].ToString()) + Environment.NewLine;
                        sSql = sSql + "and estado = 'A'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                Program.sIdGrid = dtConsulta.Rows[0].ItemArray[0].ToString();
                                Program.sFormaPagoGrid = dtConsulta.Rows[0].ItemArray[1].ToString();
                            }
                        }

                        else
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                        }
                    }

                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LAS SECCIONES
        public void mostrarRepartidores()
        {
            Program.iIdPersonaFacturador = 0;
            Program.iIdentificacionFacturador = "";

            Button[,] boton = new Button[10, 10];
            int h = 0;

            //Program.saldo = double.Parse(txt_saldo.Text);
            if (repartidor.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].BackColor = Color.FromArgb(255, 224, 192);
                        boton[i, j].Click += boton_clic;
                        boton[i, j].Width = 165;
                        boton[i, j].Height = 120;
                        boton[i, j].Top = i * 120;
                        boton[i, j].Left = j * 165;
                        boton[i, j].Font = new Font("Consolas", 11, FontStyle.Bold);

                        if (h == repartidor.iCuenta)
                        {
                            break;
                        }

                        if (repartidor.repartidor[h].sImagen == "")
                        {
                            boton[i, j].Tag = repartidor.repartidor[h].sIdRepartidor;
                            boton[i, j].Text = repartidor.repartidor[h].sDescripcion;
                            boton[i, j].AccessibleName = repartidor.repartidor[h].sCodigo;
                        }

                        else
                        {
                            boton[i, j].Tag = repartidor.repartidor[h].sIdRepartidor;
                            boton[i, j].AccessibleName = repartidor.repartidor[h].sCodigo;
                            boton[i, j].Text = repartidor.repartidor[h].sDescripcion;
                            boton[i, j].TextAlign = ContentAlignment.BottomCenter;
                            boton[i, j].Image = Image.FromFile(repartidor.repartidor[h].sImagen);
                            boton[i, j].ImageAlign = ContentAlignment.TopCenter;
                            boton[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        this.Controls.Add(boton[i, j]);
                        pnlBotones.Controls.Add(boton[i, j]);
                        h++;
                    }

                }
            }
            else
            {
                ok.LblMensaje.Text = "No hay repartidores externos registrados en el sistema..";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                this.Close();
            }
        }

        //EVENTO CLIC
        public void boton_clic(object sender, EventArgs e)
        {
            bRepartidor = sender as Button;
            iIdRepartidor = Convert.ToInt32(bRepartidor.Tag);
            consultarDatos(bRepartidor.AccessibleName);

            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void frmRepartidorExterno_Load(object sender, EventArgs e)
        {
            mostrarRepartidores();
        }

        private void frmRepartidorExterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
