using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cancelar_Orden
{
    public partial class frmEliminarComanda : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeEspere espere = new VentanasMensajes.frmMensajeEspere();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Cancelar_Orden.ClaseEliminacionComanda eliminacion = new ClaseEliminacionComanda();

        delegate void mostrarBotonesDelegado();

        string sSql;
        string sFechaActual;
        string sNumeroCuenta;
        string sNumeroPedido;
        string sNombreMesero;
        string sTotalDebido;
        string sFechaApertura;
        string sTipoOrden;
        string sEstadoOrden;
        string sCodigo_P;
        string sTexto_P;
        string sMotivo;

        DataTable dtComandas;
        DataTable dtConsulta;

        Button[] botonComandas = new Button[4];
        Button[] botonPrecuenta = new Button[4];

        bool bRespuesta;

        int iIdPedido;
        int iCuentaComandas;
        int iPosXComanda;
        int iPosYComanda;
        int iPosXPrecuenta;
        int iCuentaAyudaComanda;

        int iOp;

        public frmEliminarComanda()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LAS CUENTAS
        private void cargarComandas()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    mostrarBotonesDelegado delegado = new mostrarBotonesDelegado(cargarComandas);
                    this.Invoke(delegado);
                }

                else
                {
                    sSql = "";
                    sSql += "select * from pos_vw_comandas_creadas" + Environment.NewLine;
                    sSql += "where fecha_pedido = '" + sFechaActual + "'" + Environment.NewLine;
                    sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;

                    if (iOp == 1)
                    {
                        sSql += "and codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                    }

                    else if (iOp == 2)
                    {
                        sSql += "and numero_pedido = '" + txtBusqueda.Text.Trim() + "'" + Environment.NewLine;
                    }

                    else if (iOp == 3)
                    {
                        sSql += "and cuenta = '" + txtBusqueda.Text.Trim() + "'" + Environment.NewLine;
                    }

                    sSql += "order by numero_pedido desc, cuenta" + Environment.NewLine;

                    dtComandas = new DataTable();
                    dtComandas.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtComandas, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    iCuentaComandas = 0;

                    if (dtComandas.Rows.Count == 0)
                    {
                        btnSubirComandas.Visible = false;
                        btnBajarComandas.Visible = false;
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
                        ok.LblMensaje.Text = "No se pudo crear los botones de las comandas.";
                        ok.ShowDialog();
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES EN EL PANEL
        private bool crearBotones()
        {
            try
            {
                pnlOrdenes.Controls.Clear();
                iPosXComanda = 0;
                iPosYComanda = 0;
                iPosXPrecuenta = 701;
                iCuentaAyudaComanda = 0;

                for (int i = 0; i < 4; i++)
                {
                    sNumeroCuenta = dtComandas.Rows[iCuentaComandas]["cuenta"].ToString();
                    sNumeroPedido = dtComandas.Rows[iCuentaComandas]["numero_pedido"].ToString();
                    sNombreMesero = dtComandas.Rows[iCuentaComandas]["mesero"].ToString();
                    sTotalDebido = dtComandas.Rows[iCuentaComandas]["total"].ToString();
                    sFechaApertura = Convert.ToDateTime(dtComandas.Rows[iCuentaComandas]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss");
                    sTipoOrden = dtComandas.Rows[iCuentaComandas]["descripcion"].ToString().ToUpper();
                    sEstadoOrden = dtComandas.Rows[iCuentaComandas]["estado_orden"].ToString();
                    iIdPedido = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["id_pedido"].ToString());

                    sTexto_P = "";
                    sTexto_P += ("Número de Cuenta: " + sNumeroCuenta).PadRight(22, ' ') + ("Número de Orden: " + sNumeroPedido).PadRight(22, ' ') + ("Mesero: " + sNombreMesero).PadRight(28, ' ') + "Total: $ " + sTotalDebido + Environment.NewLine;
                    sTexto_P += "Fecha y Hora de la Comanda: " + sFechaApertura + Environment.NewLine;
                    sTexto_P += sTipoOrden + Environment.NewLine;
                    sTexto_P += "Orden " + sEstadoOrden;

                    botonComandas[i] = new Button();
                    botonComandas[i].Name = iIdPedido.ToString();
                    botonComandas[i].Click += boton_clic;
                    botonComandas[i].Size = new Size(700, 100);
                    botonComandas[i].Location = new Point(iPosXComanda, iPosYComanda);
                    botonComandas[i].Text = sTexto_P;
                    botonComandas[i].Font = new Font("Maiandra GD", 11);
                    botonComandas[i].BackColor = Color.FromArgb(255, 224, 192);
                    botonComandas[i].Image = Palatium.Properties.Resources.comanda_revisar;
                    botonComandas[i].ImageAlign = ContentAlignment.MiddleLeft;
                    pnlOrdenes.Controls.Add(botonComandas[i]);

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
                    botonPrecuenta[i].AccessibleDescription = sEstadoOrden;
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
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        private void boton_clic(object sender, EventArgs e)
        {
            //PARA ABRIR EL FORMULARIO ORIGINAL
            Button botonsel = sender as Button;

            Menú.frmCodigoAdministrador codigo = new Menú.frmCodigoAdministrador();
            codigo.ShowDialog();

            if (codigo.DialogResult == DialogResult.OK)
            {
                codigo.Close();

                Cancelar_Orden.frmMotivoEliminacionComanda motivo = new frmMotivoEliminacionComanda();
                motivo.ShowDialog();

                if (motivo.DialogResult == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    sMotivo = motivo.txtMotivo.Text.Trim().ToUpper();
                    motivo.Close();

                    if (eliminacion.procesoEliminacion(Convert.ToInt32(botonsel.Name), sMotivo) == false)
                    {
                        ok.LblMensaje.Text = "No se pudo eliminar la comanda ingresada. Favor comuníquese con el administrador del sistema.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Comanda eliminada éxitosamente.";
                        ok.ShowDialog();

                        txtBusqueda.Text = "";

                        iOp = 0;
                        sCodigo_P = "";

                        espere.AccionEjecutar = cargarComandas;
                        espere.ShowDialog();
                        btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();

                    }
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void botonImprimir_clic(object sender, EventArgs e)
        {
            try
            {
                //PARA ABRIR EL FORMULARIO ORIGINAL
                Button botonsel = sender as Button;

                Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(botonsel.Name, 1, botonsel.AccessibleDescription);
                precuenta.ShowDialog();

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtBusqueda.Text = txtBusqueda.Text + sValor;
                txtBusqueda.Focus();
                txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowDialog();
            }
        }

        #endregion

        private void frmEliminarComanda_Load(object sender, EventArgs e)
        {
            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();

            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void frmEliminarComanda_KeyDown(object sender, KeyEventArgs e)
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

        private void btnMesa_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 1;
            sCodigo_P = "01";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnLlevar_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 1;
            sCodigo_P = "02";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnDomicilio_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 1;
            sCodigo_P = "10";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnCanceladas_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 1;
            sCodigo_P = "12";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnTotalOrdenes_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrdenes_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            pnlOrdenes.Controls.Clear();

            pnlOrdenes.Controls.Clear();
            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(-1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            //btnFecha.Text = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            pnlOrdenes.Controls.Clear();

            pnlOrdenes.Controls.Clear();
            iOp = 0;
            sCodigo_P = "";

            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();
            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(btnFecha.Text);
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                btnFecha.Text = calendario.txtFecha.Text;
                sFechaActual = btnFecha.Text.Substring(6, 4) + "/" + btnFecha.Text.Substring(3, 2) + "/" + btnFecha.Text.Substring(0, 2);
                pnlOrdenes.Controls.Clear();

                pnlOrdenes.Controls.Clear();
                iOp = 0;
                sCodigo_P = "";

                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();
                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "")
            {
                ok.LblMensaje.Text = "Favor ingrese datos para la búsqueda.";
                ok.ShowDialog();
            }
            else
            {
                sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");

                if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
                {
                    iOp = 2;
                }

                else
                {
                    iOp = 3;
                }

                pnlOrdenes.Controls.Clear();
                //iOp = 0;
                sCodigo_P = "";

                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();
                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();

                txtBusqueda.Text = "";
            }
        }

        private void btnBusquedaOrdenCuenta_Click(object sender, EventArgs e)
        {
            if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
            {
                btnBusquedaOrdenCuenta.Text = "Por número de cuenta";
            }

            else
            {
                btnBusquedaOrdenCuenta.Text = "Por número de orden";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
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

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtBusqueda.Text.Length > 0)
            {

                str = txtBusqueda.Text.Substring(txtBusqueda.Text.Length - 1);
                loc = txtBusqueda.Text.Length;
                txtBusqueda.Text = txtBusqueda.Text.Remove(loc - 1, 1);
            }

            txtBusqueda.Focus();
            txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBusqueda_Click(sender, e);
            }
        }

        private void btnBajarComandas_Click(object sender, EventArgs e)
        {
            btnSubirComandas.Enabled = true;
            crearBotones();
        }

        private void btnSubirComandas_Click(object sender, EventArgs e)
        {
            iCuentaComandas -= iCuentaAyudaComanda;

            if (iCuentaComandas <= 4)
            {
                btnSubirComandas.Enabled = false;
            }

            btnBajarComandas.Enabled = true;
            iCuentaComandas -= 4;

            crearBotones();
        }
    }
}
