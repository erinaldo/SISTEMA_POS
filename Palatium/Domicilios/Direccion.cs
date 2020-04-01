using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Domicilios
{
    public partial class Direccion : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        ValidarCedula validarCedula = new ValidarCedula();
        
        bool bRespuesta = false;

        DataTable dtConsulta;

        string sSql;
        string sTabla;
        string sCampo;
        string sIdentificacionRespaldo;

        public int iIdPersona;
        public int iIdTelefono;
        public int iIdDireccion;
        int iTercerDigito;
        int iIdTipoPersona;
        int iIdTipoIdentificacion;
        int iBandera;
        
        long iMaximo;

        public Direccion(int iIdPersona_P, int iIdDireccion_P, int iIdTelefono_P)
        {
            this.iIdPersona = iIdPersona_P;
            this.iIdDireccion = iIdDireccion_P;
            this.iIdTelefono = iIdTelefono_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CONSULTAR DATOS EN LA BASE
        private void consultarRegistro()
        {
            try
            {
                sIdentificacionRespaldo = txtIdentificacion.Text.Trim();

                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion, TD.calle_principal, TD.numero_vivienda, TD.calle_interseccion, TD.referencia," + Environment.NewLine;
                sSql += "isnull(TT.domicilio, TT.oficina) domicilio, TT.celular" + Environment.NewLine;
                sSql += "FROM dbo.tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE  TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

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
                        txtSector.Text = dtConsulta.Rows[0].ItemArray[5].ToString();
                        txtPrincipal.Text = dtConsulta.Rows[0].ItemArray[6].ToString();
                        txtNumeracion.Text = dtConsulta.Rows[0].ItemArray[7].ToString();
                        txtSecundaria.Text = dtConsulta.Rows[0].ItemArray[8].ToString();
                        txtReferencia.Text = dtConsulta.Rows[0].ItemArray[9].ToString();
                        //sCiudad = dtConsulta.Rows[0].ItemArray[8].ToString();

                        if (dtConsulta.Rows[0].ItemArray[10].ToString() == "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0].ItemArray[11].ToString();
                        }

                        else if (dtConsulta.Rows[0].ItemArray[11].ToString() == "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0].ItemArray[10].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = dtConsulta.Rows[0].ItemArray[10].ToString();
                        }

                        txtApellidos.Focus();

                        //btnGuardar.Enabled = true;
                        //btnGuardar.Focus();
                    }

                    else
                    {
                        iIdPersona = 0;
                        txtIdentificacion.Clear();
                        txtApellidos.Clear();
                        txtNombres.Clear();
                        txtTelefono.Clear();
                        txtSector.Clear();
                        txtPrincipal.Clear();
                        txtNumeracion.Clear();
                        txtSecundaria.Clear();
                        txtReferencia.Clear();
                        txtIdentificacion.Focus();
                    }

                    //btnEditar.Visible = true;
                    goto fin;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin:
            { }
        }


        //FUNCION PARA VALIDAR LA CEDULA O RUC
        private void validarIdentificacion(int iOp)
        {
            try
            {
                iBandera = 0;

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
                        iIdTipoPersona = 2447;
                        iIdTipoIdentificacion = 178;

                        if (iOp == 1)
                        {
                            consultarRegistro();
                        }

                        iBandera = 1;
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
                            iIdTipoPersona = 2448;
                            iIdTipoIdentificacion = 179;

                            if (iOp == 1)
                            {
                                consultarRegistro();
                            }

                            iBandera = 1;
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
                            iIdTipoPersona = 2448;
                            iIdTipoIdentificacion = 179;

                            if (iOp == 1)
                            {
                                consultarRegistro();
                            }

                            iBandera = 1;
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
                            iIdTipoPersona = 2447;
                            iIdTipoIdentificacion = 179;

                            if (iOp == 1)
                            {
                                consultarRegistro();
                            }

                            iBandera = 1;
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

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        mensaje:
            {
                ok.lblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                //txtIdentificacion.Clear();
                txtIdentificacion.Text = sIdentificacionRespaldo;
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

        //FUNCION PARA ACTUALIZAR EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //SE INICIA LA TRANSACCIÓN
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al realizar la transacción. Por favor comuníquese con el administrador en caso de continuar con el inconveniente.";
                    ok.ShowDialog();
                    return;
                }

                //ACTUALIZAR EN LA TABLA TP_PERSONAS
                sSql = "";
                sSql += "update tp_personas set" + Environment.NewLine;
                sSql += "apellidos = '" + txtApellidos.Text.Trim() + "'," + Environment.NewLine;
                sSql += "nombres = '" + txtNombres.Text.Trim() + "'," + Environment.NewLine;
                sSql += "codigo_alterno = '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_electronico = '" + txtMail.Text.Trim() + "'," + Environment.NewLine;
                sSql += "cg_tipo_persona = " + iIdTipoPersona + "," + Environment.NewLine;
                sSql += "cg_tipo_identificacion = " + iIdTipoIdentificacion + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (iIdDireccion == 0)
                {
                    //PARA INSERTAR LA DIRECCION
                    sSql = "";
                    sSql += "Insert Into  tp_direcciones (" + Environment.NewLine;
                    sSql += "id_persona, IdTipoEstablecimiento, Direccion, calle_principal," + Environment.NewLine;
                    sSql += "numero_vivienda, calle_interseccion, referencia," + Environment.NewLine;
                    sSql += "Cg_Localidad, Estado, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "fecha_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdPersona + ", 1, '" + txtSector.Text.Trim().ToUpper() + "', '" + txtPrincipal.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "'" + txtNumeracion.Text.Trim().ToUpper() + "','" + txtSecundaria.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "'" + txtReferencia.Text.Trim().ToUpper() + "', " + Program.iCgLocalidad + "," + Environment.NewLine;
                    sSql += "'A','" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                    sSql += "GetDate(), 0, 0)";

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sTabla = "tp_direcciones";
                    sCampo = "correlativo";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    iIdDireccion = Convert.ToInt32(iMaximo);
                }

                else
                {
                    //ACTUALIZAMOS LA TABLA DE DIRECCIONES
                    //=================================================================================================================
                    sSql = "";
                    sSql += "update tp_direcciones set" + Environment.NewLine;
                    sSql += "direccion = '" + txtSector.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "calle_principal = '" + txtPrincipal.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "calle_interseccion = '" + txtSecundaria.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "numero_vivienda = '" + txtNumeracion.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "referencia = '" + txtReferencia.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "where correlativo = " + iIdDireccion;

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }                    
                }

                if (iIdTelefono == 0)
                {
                    //PARA INSERTAR EL TELEFONO
                    sSql = "";
                    sSql += "Insert Into tp_telefonos (" + Environment.NewLine;
                    sSql += "id_persona, idTipoEstablecimiento, CODIGO_AREA," + Environment.NewLine;
                    sSql += "domicilio, Estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdPersona + ", 1, '02','" + txtTelefono.Text.Trim() + "','A', GetDate()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sTabla = "tp_telefonos";
                    sCampo = "correlativo";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    iIdTelefono = Convert.ToInt32(iMaximo);
                }

                else
                {
                    //ACTUALIZAMOS LA TABLA DE TELEFONOS
                    //=================================================================================================================
                    sSql = "";
                    sSql += "update tp_telefonos set" + Environment.NewLine;
                    sSql += "domicilio = '" + txtTelefono.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "where correlativo = " + iIdTelefono;

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Su registro se ha actualizado con éxito";
                ok.ShowDialog();
                this.DialogResult = DialogResult.OK;
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //SE INICIA LA TRANSACCIÓN
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al realizar la transacción. Por favor comuníquese con el administrador en caso de continuar con el inconveniente.";
                    ok.ShowDialog();
                    return;
                }

                //INSERTAR EN LA TABLA TP_PERSONAS
                //-----------------------------------------------------------------------------------------------
                sSql = "";
                sSql += "Insert Into tp_personas (" + Environment.NewLine;
                sSql += "idempresa, Cg_Tipo_Persona, Cg_Pais_Residencia," + Environment.NewLine;
                sSql += "Cg_Tipo_Identificacion, Identificacion, Nombres, Apellidos," + Environment.NewLine;
                sSql += "Cliente, codigo_alterno, correo_electronico, estado, fecha_ingreso, Usuario_Ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso,contador, porcentaje_descuento," + Environment.NewLine;
                sSql += "hacerlaretencionfuenteir, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdTipoPersona + ", 2843, " + iIdTipoIdentificacion + ", '" + txtIdentificacion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtNombres.Text.Trim().ToUpper() + "', '" + txtApellidos.Text.Trim().ToUpper() + "', 1," + Environment.NewLine;
                sSql += "'" + txtTelefono.Text.Trim() + "', " + txtMail.Text.Trim().ToLower() + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "',0,0,0,0,0 )";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sTabla = "tp_personas";
                sCampo = "id_persona";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdPersona = Convert.ToInt32(iMaximo);

                //PARA INSERTAR LA DIRECCION
                sSql = "";
                sSql += "Insert Into  tp_direcciones (" + Environment.NewLine;
                sSql += "id_persona, IdTipoEstablecimiento, Direccion, calle_principal," + Environment.NewLine;
                sSql += "numero_vivienda, calle_interseccion, referencia," + Environment.NewLine;
                sSql += "Cg_Localidad, Estado, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "fecha_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdPersona + ", 1, '" + txtSector.Text.Trim().ToUpper() + "', '" + txtPrincipal.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + txtNumeracion.Text.Trim().ToUpper() + "','" + txtSecundaria.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + txtReferencia.Text.Trim().ToUpper() + "', " + Program.iCgLocalidad + "," + Environment.NewLine;
                sSql += "'A','" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GetDate(), 0, 0)";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PARA INSERTAR EL TELEFONO
                sSql = "";
                sSql += "Insert Into tp_telefonos (" + Environment.NewLine;
                sSql += "id_persona, idTipoEstablecimiento, CODIGO_AREA," + Environment.NewLine;
                sSql += "domicilio, Estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdPersona + ", 1, '02','" + txtTelefono.Text.Trim() + "','A', GetDate()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "El registro se ha guardado con éxito.";
                ok.ShowDialog();

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        #endregion

        private void Direccion_Load(object sender, EventArgs e)
        {
            if (iIdPersona != 0)
            {
                txtIdentificacion.ReadOnly = true;
                txtApellidos.Focus();
                this.ActiveControl = txtApellidos;
            }

            else
            {
                txtIdentificacion.ReadOnly = false;
                txtIdentificacion.Focus();
                this.ActiveControl = txtIdentificacion;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void Direccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el número de identificación";
                ok.ShowDialog();
                txtIdentificacion.Focus();
                return;
            }

            else
            {
                if (chkPasaporte.Checked == false)
                {
                    validarIdentificacion(0);

                    if (iBandera == 0)
                    {
                        return;
                    }
                }

                else
                {
                    iIdTipoPersona = 2447;
                    iIdTipoIdentificacion = 180;
                }
            }

            if (txtApellidos.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el apellido para el registro";
                ok.ShowDialog();
                txtApellidos.Focus();
                return;
            }
                
            if (txtTelefono.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el número telefónico para el registro";
                ok.ShowDialog();
                txtTelefono.Focus();
                return;
            }

            if (txtMail.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el correo electrónico para el registro";
                ok.ShowDialog();
                txtMail.Focus();
                return;
            }

            if (txtSector.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el sector para el registro del domicilio.";
                ok.ShowDialog();
                txtSector.Focus();
                return;
            }

            if (txtPrincipal.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la calle principal para el registro del domicilio.";
                ok.ShowDialog();
                txtPrincipal.Focus();
                return;
            }

            if (txtNumeracion.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la numeración para el registro del domicilio.";
                ok.ShowDialog();
                txtNumeracion.Focus();
                return;
            }

            if (txtSecundaria.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la calle secundaria para el registro del domicilio.";
                ok.ShowDialog();
                txtSecundaria.Focus();
                return;
            }

            if (txtReferencia.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la referencia para el registro del domicilio.";
                ok.ShowDialog();
                txtReferencia.Focus();
                return;
            }

            //INSERTAR NUEVO REGISTRO
            if (iIdPersona == 0)
            {
                insertarRegistro();
            }

            //ACTUALIZAR EL REGISTRO
            else
            {
                actualizarRegistro();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                    {
                        //INSTRUCCIONES PARA VALIDAR
                        validarIdentificacion(1);
                    }
                    else
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        iIdTipoPersona = 2447;
                        iIdTipoIdentificacion = 180;
                        consultarRegistro();
                    }
                }
            }
        }
    }
}
