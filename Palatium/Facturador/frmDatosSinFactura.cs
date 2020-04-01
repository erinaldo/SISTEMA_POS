using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturador
{
    public partial class frmDatosSinFactura : Form
    {
        //VARIABLES PARA REALZAR LA FACTURA
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        int iIdPersona;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iTercerDigito;

        double dTotal;

        bool bRespuesta = false;
        string sSql;
        string sIdOrden;
        string sCiudad = Program.sCiudadDefault;
        DataTable dtConsulta;
        DataTable dtDireccion;

        Orden ord;

        public frmDatosSinFactura(string sIdOrden, Orden ord, Double total)
        {
            this.ord = ord;
            this.sIdOrden = sIdOrden;
            this.dTotal = total;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA ACTUALIZAR EL ID_PERSONAS EN CV403_CAB_PEDIDOS
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = sSql + "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql = sSql + "estado_orden = 'Pagada'," + Environment.NewLine;
                sSql = sSql + "id_persona = " + iIdPersona + Environment.NewLine;
                sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden);

                //EJECUCIÓN DE LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.LblMensaje.Text = "Se ha procedido a ingresar los datos de forma éxitosa.";
                ok.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;

                    this.Close();

                    if (Program.iBanderaCerrarVentana == 0)
                    {
                        ord.Close();
                    }
                    else
                    {
                        Program.iBanderaCerrarVentana = 0;
                    }
                }

                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE ORDEN
        private void consultarNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select CP.cuenta, NP.numero_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP" + Environment.NewLine;
                sSql += "where NP.id_pedido = CP.id_pedido " + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + sIdOrden;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        LblOrden.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                        return;
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

        //FUNCION PARA VALIDAR LA CEDULA O RUC
        private void validarIdentificacion()
        {
            try
            {
                if (txtIdentificacion.Text.Length >= 10)
                {
                    iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));
                }
                else
                {
                    goto mensaje;
                }

                if (txtIdentificacion.Text.Length == 10)
                {
                    if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                        goto fin;
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else if (txtIdentificacion.Text.Length == 13)
                {
                    if (iTercerDigito == 9)
                    {
                        if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }

                    }

                    else if (iTercerDigito == 6)
                    {
                        if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else if ((iTercerDigito <= 5) || (iTercerDigito >= 0))
                    {
                        if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {

            }

        mensaje:
            {
                ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        //CONSULTAR DATOS EN LA BASE
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql = sSql + "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql = sSql + "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
                sSql = sSql + "TT.oficina, TT.celular, TD.direccion" + Environment.NewLine;
                sSql = sSql + "FROM dbo.tp_personas TP" + Environment.NewLine;
                sSql = sSql + "LEFT OUTER JOIN dbo.tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql = sSql + "and TP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and TD.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "LEFT OUTER JOIN dbo.tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql = sSql + "and TT.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "WHERE  TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        txtNombres.Text = dtConsulta.Rows[0].ItemArray[2].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0].ItemArray[3].ToString();
                        txtMail.Text = dtConsulta.Rows[0].ItemArray[4].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0].ItemArray[5].ToString();
                        sCiudad = dtConsulta.Rows[0].ItemArray[8].ToString();

                        if (dtConsulta.Rows[0].ItemArray[6].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0].ItemArray[6].ToString();
                        }

                        else if (dtConsulta.Rows[0].ItemArray[7].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0].ItemArray[7].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "";
                        }

                        btnGuardar.Enabled = true;
                        btnGuardar.Focus();
                    }

                    else
                    {
                        //ok.LblMensaje.Text = "No existe ningún registro con la identificación ingresada.";
                        //ok.ShowDialog();

                        frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
                        nuevoCliente.ShowDialog();

                        if (nuevoCliente.DialogResult == DialogResult.OK)
                        {
                            iIdPersona = nuevoCliente.iCodigo;
                            txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                            txtNombres.Text = nuevoCliente.sNombre;
                            txtApellidos.Text = nuevoCliente.sApellido;
                            txtTelefono.Text = nuevoCliente.sTelefono;
                            txtDireccion.Text = nuevoCliente.sDireccion;
                            txtMail.Text = nuevoCliente.sMail;
                            sCiudad = nuevoCliente.sCiudad;
                            nuevoCliente.Close();
                            btnGuardar.Enabled = true;
                            btnGuardar.Focus();
                        }
                    }

                    btnEditar.Visible = true;
                    goto fin;
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {
                goto mensaje;
            }

        mensaje:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }


        #endregion

        private void frmDatosSinFactura_Load(object sender, EventArgs e)
        {
            btnConsumidorFinal_Click(sender, e);

            consultarNumeroOrden();

            //LblOrden.Text = sIdOrden;
            LblPagar.Text = "$ " + dTotal.ToString("N2");
        }

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Text = "9999999999999";
            txtApellidos.Text = "CONSUMIDOR FINAL";
            txtNombres.Text = "CONSUMIDOR FINAL";
            txtTelefono.Text = "9999999999";
            txtMail.Text = "dominio@dominio.com";
            txtDireccion.Text = "QUITO";
            iIdPersona = Program.iIdPersona;
            idTipoIdentificacion = 180;
            idTipoPersona = 2447;
            btnGuardar.Enabled = true;
            btnEditar.Visible = false;
            btnGuardar.Focus();
        }

        private void btnEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
            nuevoCliente.ShowDialog();

            if (nuevoCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = nuevoCliente.iCodigo;
                txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                consultarRegistro();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmControlDatosCliente controlClientes = new frmControlDatosCliente();
            controlClientes.ShowDialog();

            if (controlClientes.DialogResult == DialogResult.OK)
            {
                iIdPersona = controlClientes.iCodigo;
                txtIdentificacion.Text = controlClientes.sIdentificacion;
                consultarRegistro();
                btnGuardar.Focus();
                controlClientes.Close();
            }
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if (esNumero(txtIdentificacion.Text.Trim()) == true)
                    {
                        //INSTRUCCIONES PARA VALIDAR
                        validarIdentificacion();
                    }
                    else
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtMail.Clear();
            txtIdentificacion.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            ord.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtIdentificacion.Text == "") && (txtApellidos.Text == ""))
            {
                ok.LblMensaje.Text = "Favor ingrese los datos del cliente para la factura.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                actualizarRegistro();
            }
        }

        private void btnVisualizarComanda_Click(object sender, EventArgs e)
        {
            Pedidos.frmVerPrecuentaTextBox pedido = new Pedidos.frmVerPrecuentaTextBox(sIdOrden, 0, "Pre-Cuenta");
            pedido.ShowDialog();
        }

        private void frmDatosSinFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void chkPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            txtIdentificacion.Focus();
        }
    }
}
