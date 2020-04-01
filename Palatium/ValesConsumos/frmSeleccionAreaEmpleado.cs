using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ValesConsumos
{
    public partial class frmSeleccionAreaEmpleado : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseAbrirCajon abrir;

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;

        ToolTip ttMensajeMesas = new ToolTip();

        Button[] botonSecciones = new Button[3];
        Button[,] botonEmpleados;
        Button bSeccion;

        DataTable dtConsulta;
        DataTable dtSecciones;
        DataTable dtEmpleados;

        bool bRespuesta;

        int iCuentaSecciones;
        int iPosXSecciones;
        int iCuentaSeccionesAyuda;
        int iIdArea;
        int iPosXEmpleados;
        int iPosYEmpleados;
        int iCuentaAyudaEmpleados;
        int iCuentaEmpleados;
        int iIdOrigenOrden;

        public frmSeleccionAreaEmpleado(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //INSTRUCCION PARA OBTENER EL ID DEL PRIMER REGISTRO DE SECCIONES DE MESA
        private int primerRegistroSecciones()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 id_pos_area_consumo_empleados, descripcion" + Environment.NewLine;
                sSql += "from pos_area_consumo_empleados" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1" + Environment.NewLine;
                sSql += "order by id_pos_area_consumo_empleados asc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    lblPisos.Text = "SELECCIONE";
                    iIdArea = 0;
                }

                else
                {
                    lblPisos.Text = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
                    iIdArea = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_area_consumo_empleados"].ToString());
                }

                return iIdArea;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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
                sSql += "select id_pos_area_consumo_empleados, descripcion" + Environment.NewLine;
                sSql += "from pos_area_consumo_empleados" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_area_consumo_empleados asc";

                dtSecciones = new DataTable();
                dtSecciones.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSecciones, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                iCuentaSecciones = 0;

                if (dtSecciones.Rows.Count > 0)
                {
                    if (dtSecciones.Rows.Count > 3)
                    {
                        btnSiguiente.Enabled = true;
                        btnAnterior.Visible = true;
                        btnSiguiente.Visible = true;
                    }

                    else
                    {
                        btnSiguiente.Enabled = false;
                        btnAnterior.Visible = false;
                        btnSiguiente.Visible = false;
                    }

                    if (mostrarBotonesSecciones() == false)
                    {

                    }
                }

                else
                {
                    btnAnterior.Visible = false;
                    btnSiguiente.Visible = false;
                    btnSiguiente.Enabled = false;
                    btnAnterior.Enabled = false;

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras secciones configuradas en el sistema.";
                    ok.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
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

                for (int i = 0; i < 3; i++)
                {
                    botonSecciones[i] = new Button();
                    botonSecciones[i].Cursor = Cursors.Hand;
                    botonSecciones[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 255);
                    botonSecciones[i].FlatStyle = FlatStyle.Flat;
                    botonSecciones[i].Font = new Font("Maiandra GD", 10F, FontStyle.Bold);
                    botonSecciones[i].Image = Properties.Resources.icono_reporte_edificio;
                    botonSecciones[i].ImageAlign = ContentAlignment.TopLeft;
                    botonSecciones[i].Location = new Point(iPosXSecciones, 0);
                    botonSecciones[i].Size = new Size(140, 90);
                    botonSecciones[i].TextAlign = ContentAlignment.BottomRight;
                    botonSecciones[i].UseVisualStyleBackColor = false;
                    botonSecciones[i].FlatAppearance.BorderSize = 1;

                    botonSecciones[i].Name = dtSecciones.Rows[iCuentaSecciones]["id_pos_area_consumo_empleados"].ToString();
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
                    else if (i == 5)
                        botonSecciones[i].BackColor = Color.Orange;

                    botonSecciones[i].Click += boton_clic_secciones;

                    pnlSecciones.Controls.Add(botonSecciones[i]);

                    iCuentaSecciones++;
                    iCuentaSeccionesAyuda++;
                    iPosXSecciones += 140;

                    if (dtSecciones.Rows.Count == iCuentaSecciones)
                    {
                        btnSiguiente.Enabled = false;
                        break;
                    }
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

        //BOTON CLIC DE LAS SECCIONES
        public void boton_clic_secciones(object sender, EventArgs e)
        {
            bSeccion = sender as Button;
            iIdArea = Convert.ToInt32(bSeccion.Name);
            lblPisos.Text = bSeccion.Text.Trim().ToUpper();
            cargarEmpleados();
        }

        //FUNCION PARA CARGAR LAS MESAS
        private void cargarEmpleados()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, empleado" + Environment.NewLine;
                sSql += "from pos_vw_consumo_empleados" + Environment.NewLine;
                sSql += "where id_pos_area_consumo_empleados = " + iIdArea + Environment.NewLine;
                sSql += "and is_active = 1";

                dtEmpleados = new DataTable();
                dtEmpleados.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtEmpleados, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                pnlEmpleados.Controls.Clear();
                iCuentaEmpleados = 0;

                if (dtEmpleados.Rows.Count > 0)
                {
                    if (dtEmpleados.Rows.Count > 9)
                    {
                        btnSiguienteEmpleado.Enabled = true;
                        btnAnteriorEmpleado.Visible = true;
                        btnSiguienteEmpleado.Visible = true;
                    }

                    else
                    {
                        btnSiguienteEmpleado.Enabled = false;
                        btnAnteriorEmpleado.Visible = false;
                        btnSiguienteEmpleado.Visible = false;
                    }

                    if (mostrarBotonesEmpleados())
                            ;
                }
                
                else
                {
                    btnAnteriorEmpleado.Visible = false;
                    btnSiguienteEmpleado.Visible = false;
                    btnSiguienteEmpleado.Enabled = false;
                    btnAnteriorEmpleado.Enabled = false;

                    pnlEmpleados.Controls.Clear();

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentras registros de empleados en la empresa seleccionada.";
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

        //FUNCION PARA MOSTRAR LOS BOTONES DE LAS SECCIONES
        private bool mostrarBotonesEmpleados()
        {
            try
            {
                pnlEmpleados.Controls.Clear();
                iPosXEmpleados = 0;
                iPosYEmpleados = 0;
                iCuentaAyudaEmpleados = 0;

                botonEmpleados = new Button[3, 3];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        botonEmpleados[i, j] = new Button();

                        botonEmpleados[i, j].Cursor = Cursors.Hand;
                        botonEmpleados[i, j].FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
                        botonEmpleados[i, j].Image = Properties.Resources.icono_cliente_seccion_mesas;
                        botonEmpleados[i, j].FlatStyle = FlatStyle.Flat;
                        botonEmpleados[i, j].Font = new Font("Maiandra GD", 12F, FontStyle.Regular);
                        botonEmpleados[i, j].ForeColor = Color.Black;

                        int iValue = i + j;

                        if (iValue % 2 == 0)
                            botonEmpleados[i, j].BackColor = Color.Lime;
                        else
                            botonEmpleados[i, j].BackColor = Color.Cyan;
                        
                        botonEmpleados[i, j].Location = new Point(iPosXEmpleados, iPosYEmpleados);
                        botonEmpleados[i, j].Size = new Size(190, 95);
                        botonEmpleados[i, j].UseVisualStyleBackColor = false;
                        botonEmpleados[i, j].ImageAlign = ContentAlignment.TopLeft;
                        botonEmpleados[i, j].TextAlign = ContentAlignment.BottomRight;
                        botonEmpleados[i, j].Name = dtEmpleados.Rows[iCuentaEmpleados]["id_persona"].ToString();
                        botonEmpleados[i, j].Text = dtEmpleados.Rows[iCuentaEmpleados]["empleado"].ToString();
                        botonEmpleados[i, j].Click += boton_clic_empleado;
                        pnlEmpleados.Controls.Add(botonEmpleados[i, j]);

                        iCuentaEmpleados++;
                        iCuentaAyudaEmpleados++;

                        if (j + 1 == 3)
                        {
                            iPosXEmpleados = 0;
                            iPosYEmpleados += 95;
                        }

                        else
                        {
                            iPosXEmpleados += 190;
                        }

                        if (dtEmpleados.Rows.Count == iCuentaEmpleados)
                        {
                            btnSiguienteEmpleado.Enabled = false;
                            break;
                        }
                    }

                    if (dtEmpleados.Rows.Count == iCuentaEmpleados)
                    {
                        btnSiguienteEmpleado.Enabled = false;
                        break;
                    }
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

        //BOTON CLIC DE LAS SECCIONES
        public void boton_clic_empleado(object sender, EventArgs e)
        {
            Button btnEmpleado = sender as Button;
            int iIdEmpleado_P = Convert.ToInt32(btnEmpleado.Name);
            string sNombreEmpleado_P = btnEmpleado.Text.Trim().ToUpper();

            ValesConsumos.frmComandaConsumoInterno modal = new frmComandaConsumoInterno(iIdEmpleado_P, sNombreEmpleado_P, iIdOrigenOrden);
            modal.ShowDialog();
        }

        #endregion

        private void frmSeleccionAreaEmpleado_Load(object sender, EventArgs e)
        {
            int iCuenta = primerRegistroSecciones();

            if (iCuenta == -1)
                return;

            if (iCuenta == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se encuentras secciones configuradas en el sistema.";
                ok.ShowDialog();
                return;
            }

            timerBlink.Start();
            cargarAreas();
            cargarEmpleados();
        }

        private void frmSeleccionAreaEmpleado_KeyDown(object sender, KeyEventArgs e)
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

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int uno = rand.Next(0, 255);
            int dos = rand.Next(0, 255);
            int tres = rand.Next(0, 255);
            int cuatro = rand.Next(0, 255);

            lblPisos.ForeColor = Color.FromArgb(uno, dos, tres, cuatro);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            mostrarBotonesSecciones();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaSecciones -= iCuentaSeccionesAyuda;

            if (iCuentaSecciones <= 3)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaSecciones -= 3;

            mostrarBotonesSecciones();
        }

        private void btnSiguienteEmpleado_Click(object sender, EventArgs e)
        {
            btnAnteriorEmpleado.Enabled = true;
            mostrarBotonesEmpleados();
        }

        private void btnAnteriorEmpleado_Click(object sender, EventArgs e)
        {
            iCuentaEmpleados -= iCuentaAyudaEmpleados;

            if (iCuentaEmpleados <= 9)
            {
                btnAnteriorEmpleado.Enabled = false;
            }

            btnSiguienteEmpleado.Enabled = true;
            iCuentaEmpleados -= 9;

            mostrarBotonesEmpleados();
        }
    }
}
