using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Domicilios
{
    public partial class frmNumeroTelefono : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sIdentificacion;
        string sApellidos;
        string sNombres;
        string sCodigoAlterno;
        string sNumeroDomicilio;
        string sNumeroCelular;

        DataTable dtConsulta;

        bool bRespuesta;

        int iBanderaBusqueda;
        int iIdPersona;
        int iIdOrigenOrden;
        int iIdDireccion;
        int iIdTelefono;

        ToolTip ttMensajeBotones = new ToolTip();

        public frmNumeroTelefono(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LOS REGISTROS
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_buscar_cliente_domicilio" + Environment.NewLine;

                if (iBanderaBusqueda == 1)
                {
                    sSql += "where (codigo_alterno = '" + txtBusqueda.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "or domicilio = '" + txtBusqueda.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "or celular = '" + txtBusqueda.Text.Trim() + "')";
                }

                else if (iBanderaBusqueda == 2)
                {
                    sSql += "where identificacion = '" + txtBusqueda.Text.Trim() + "'";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    limpiarCajas();
                }

                else if (dtConsulta.Rows.Count == 1)
                {
                    iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                    sApellidos = dtConsulta.Rows[0]["apellidos"].ToString().Trim().ToUpper();
                    sNombres = dtConsulta.Rows[0]["nombres"].ToString().Trim().ToUpper();
                    txtRazonSocial.Text = (sNombres + " " + sApellidos).Trim();
                    txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString().Trim();
                    txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim();

                    sCodigoAlterno = dtConsulta.Rows[0]["codigo_alterno"].ToString().Trim();
                    sNumeroDomicilio = dtConsulta.Rows[0]["domicilio"].ToString().Trim();
                    sNumeroCelular = dtConsulta.Rows[0]["celular"].ToString().Trim();

                    if (sCodigoAlterno != "")
                    {
                        txtNumeroTelefono.Text = sCodigoAlterno;
                    }

                    else if (sNumeroDomicilio != "")
                    {
                        txtNumeroTelefono.Text = sNumeroDomicilio;
                    }

                    else if (sNumeroCelular != "")
                    {
                        txtNumeroTelefono.Text = sNumeroCelular;
                    }

                    txtSector.Text = dtConsulta.Rows[0]["direccion"].ToString().Trim().ToUpper();
                    txtCallePrincipal.Text = dtConsulta.Rows[0]["calle_principal"].ToString().Trim().ToUpper();
                    txtNumeracionVivienda.Text = dtConsulta.Rows[0]["numero_vivienda"].ToString().Trim().ToUpper();
                    txtCalleSecundaria.Text = dtConsulta.Rows[0]["calle_interseccion"].ToString().Trim().ToUpper();
                    txtReferencia.Text = dtConsulta.Rows[0]["referencia"].ToString().Trim().ToUpper();

                    iIdDireccion = Convert.ToInt32(dtConsulta.Rows[0]["id_direccion"].ToString());
                    iIdTelefono = Convert.ToInt32(dtConsulta.Rows[0]["id_telefono"].ToString());

                    llenarDatosAdicionales();
                }

                else if (dtConsulta.Rows.Count > 1)
                {
                    Domicilios.frmListaClientesDomicilio lista = new Domicilios.frmListaClientesDomicilio(dtConsulta);
                    lista.ShowDialog();

                    if (lista.DialogResult == DialogResult.OK)
                    {
                        iIdPersona = lista.iIdPersona;
                        iIdDireccion = lista.iIdDireccion;
                        iIdTelefono = lista.iIdTelefono;

                        txtIdentificacion.Text = lista.sIdentificacion;
                        sApellidos = lista.sApellidos;
                        sNombres = lista.sNombres;
                        txtRazonSocial.Text = (sNombres + " " + sApellidos).Trim();

                        sCodigoAlterno = lista.sCodigoAlterno;
                        sNumeroDomicilio = lista.sTelefonoConvencional;
                        sNumeroCelular = lista.sTelefonoCelular;

                        if (sCodigoAlterno != "")
                        {
                            txtNumeroTelefono.Text = sCodigoAlterno;
                        }

                        else if (sNumeroDomicilio != "")
                        {
                            txtNumeroTelefono.Text = sNumeroDomicilio;
                        }

                        else if (sNumeroCelular != "")
                        {
                            txtNumeroTelefono.Text = sNumeroCelular;
                        }

                        txtMail.Text = lista.sCorreoElectronico;
                        txtSector.Text = lista.sSector;
                        txtCallePrincipal.Text = lista.sCallePrincipal;
                        txtNumeracionVivienda.Text = lista.sNumeracion;
                        txtCalleSecundaria.Text = lista.sCalleSecundaria;
                        txtReferencia.Text = lista.sReferencia;

                        llenarDatosAdicionales();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VALIDAR LA BUSQUEDA
        private void validarBusqueda()
        {
            try
            {
                if (txtBusqueda.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese información para buscar.";
                    ok.ShowDialog();
                    return;
                }

                consultarRegistro();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR LOS CAMPOS EXTRAS EN EL FORMULARIO
        private void llenarDatosAdicionales()
        {
            try
            {
                //LLENAR TEXTBOX DE CLIENTE DESDE
                sSql = "";
                sSql += "select top 1 isnull(fecha_pedido, 'NINGUNO') fecha_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    txtClienteDesde.Text = dtConsulta.Rows[0]["fecha_pedido"].ToString().Substring(0, 10);
                }

                else
                {
                    txtClienteDesde.Text = "NINGUNO";
                }

                //LLENAR TEXTBOX DE CANTIDAD DE ORDENES DEL CLIENTE
                sSql = "";
                sSql += "select count(*) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    txtNumeroOrdenes.Text = dtConsulta.Rows[0]["suma"].ToString();
                }

                else
                {
                    txtNumeroOrdenes.Text = "0";
                }

                //LLENAR TEXTBOX DE ULTIMO REGISTRO DE ORDEN
                sSql = "";
                sSql += "select top 1 isnull(fecha_pedido, 'NINGUNO') fecha_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pedido desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    txtFechaUltimaOrden.Text = dtConsulta.Rows[0]["fecha_pedido"].ToString().Substring(0, 10);
                }

                else
                {
                    txtFechaUltimaOrden.Text = "NINGUNO";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LIMPIAR SOLO LAS CAJAS DE TEXTO RECUPERADOS
        private void limpiarCajas()
        {
            sNombres = "";
            sApellidos = "";
            iIdPersona = 0;
            iIdDireccion = 0;
            iIdTelefono = 0;
            txtRazonSocial.Clear();
            txtIdentificacion.Clear();
            txtNumeroTelefono.Clear();
            txtMail.Clear();
            txtSector.Clear();
            txtCallePrincipal.Clear();
            txtNumeracionVivienda.Clear();
            txtCalleSecundaria.Clear();
            txtReferencia.Clear();
            txtClienteDesde.Clear();
            txtNumeroOrdenes.Clear();
            txtFechaUltimaOrden.Clear();
        }

        #endregion

        private void btnPorTelefono_Click(object sender, EventArgs e)
        {
            btnPorTelefono.BackColor = Color.FromArgb(255, 192, 128);
            btnPorIdentificacion.BackColor = Color.FromArgb(192, 255, 192);
            iBanderaBusqueda = 1;
            ttMensaje.SetToolTip(btnPorTelefono, "Búsqueda por Número Telefónico");
            txtBusqueda.Clear();
            txtBusqueda.Focus();
        }

        private void btnPorIdentificacion_Click(object sender, EventArgs e)
        {
            btnPorIdentificacion.BackColor = Color.FromArgb(255, 192, 128);
            btnPorTelefono.BackColor = Color.FromArgb(192, 255, 192);
            iBanderaBusqueda = 2;
            ttMensaje.SetToolTip(btnPorIdentificacion, "Búsqueda por Número de Identificación");
            txtBusqueda.Clear();
            txtBusqueda.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            validarBusqueda();
        }

        private void frmNumeroTelefono_Load(object sender, EventArgs e)
        {
            iBanderaBusqueda = 1;
            this.ActiveControl = txtBusqueda;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNumeroTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
                validarBusqueda();
            }
        }

        private void btnEditarDatos_Click(object sender, EventArgs e)
        {
            Domicilios.Direccion direccion = new Direccion(iIdPersona, iIdDireccion, iIdTelefono);

            if (iIdPersona != 0)
            {
                direccion.txtIdentificacion.Text = this.txtIdentificacion.Text.Trim();
                direccion.txtApellidos.Text = this.sApellidos;
                direccion.txtNombres.Text = this.sNombres;
                direccion.txtTelefono.Text = this.txtNumeroTelefono.Text.Trim();
                direccion.txtMail.Text = this.txtMail.Text.Trim();
                direccion.txtSector.Text = this.txtSector.Text.Trim();
                direccion.txtPrincipal.Text = this.txtCallePrincipal.Text.Trim();
                direccion.txtNumeracion.Text = this.txtNumeracionVivienda.Text.Trim();
                direccion.txtSecundaria.Text = this.txtCalleSecundaria.Text.Trim();
                direccion.txtReferencia.Text = this.txtReferencia.Text.Trim();
            }

            direccion.ShowDialog();

            if (direccion.DialogResult == DialogResult.OK)
            {
                iIdDireccion = direccion.iIdDireccion;
                iIdTelefono = direccion.iIdTelefono;
                txtIdentificacion.Text = direccion.txtIdentificacion.Text.Trim();
                sApellidos = direccion.txtApellidos.Text.Trim().ToUpper();
                sNombres = direccion.txtNombres.Text.Trim().ToUpper();
                txtRazonSocial.Text = (sNombres + " " + sApellidos).Trim();
                txtNumeroTelefono.Text = direccion.txtTelefono.Text.Trim();
                txtMail.Text = direccion.txtMail.Text.Trim().ToLower();
                txtSector.Text = direccion.txtSector.Text.Trim();
                txtCallePrincipal.Text = direccion.txtPrincipal.Text.Trim();
                txtNumeracionVivienda.Text = direccion.txtNumeracion.Text.Trim();
                txtCalleSecundaria.Text = direccion.txtSecundaria.Text.Trim();
                txtReferencia.Text = direccion.txtReferencia.Text.Trim();

                direccion.Close();
            }
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            if ((txtIdentificacion.Text == "") || (txtSector.Text == ""))
            {
                ok.lblMensaje.Text = "Favor rellene los campos para el domicilio";
                ok.ShowDialog();
            }

            else
            {
                //Program.sIDPERSONA = iIdDireccion.ToString();
                //Program.sIdentificacion = txtIdentificacion.Text.Trim();

                if (Program.iManejaRepartidor == 1)
                {
                    Domicilios.frmSeleccionarRepartidor rep = new frmSeleccionarRepartidor();
                    rep.ShowDialog();

                    if (rep.DialogResult == DialogResult.OK)
                    {
                        int iIdRepartidor_Recuperado = rep.iIdRepartidor;
                        rep.Close();

                        //ACTUALIZACION ELVIS COMANDA
                        //=======================================================================================================================
                        ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, "NINGUNA", iIdRepartidor_Recuperado, 0, iIdPersona);
                        or.ShowDialog();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        //=======================================================================================================================

                        //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", iIdPersona, Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, 0, iIdRepartidor_Recuperado);
                        //or.ShowDialog();
                        //this.DialogResult = DialogResult.OK;
                        //this.Close();
                    }
                }

                else
                {
                    //ACTUALIZACION ELVIS COMANDA
                    //=======================================================================================================================
                    ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, "NINGUNA", Program.iIdPosRepartidor, 0, iIdPersona);
                    or.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    //=======================================================================================================================

                    //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", iIdPersona, Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, 0, Program.iIdPosRepartidor);
                    //or.ShowDialog();
                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
            }
        }
    }
}
