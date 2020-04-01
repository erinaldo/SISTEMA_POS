using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Palatium.Facturador
{
    public partial class frmNuevoCliente : Form
    {
        //VARIABLES PARA REALZAR LA FACTURA
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        DataTable dtConsulta;
        bool bRespuesta = false;
        string sSql;

        int iIdPersona;
        int iIdTipoIdentificacion;
        int iIdTipoPersona;
        int iTercerDigito;

        public int iCodigo;
        public string sIdentificacion;
        public string sNombre;
        public string sApellido;
        public string sTelefono;
        public string sMail;
        public string sDireccion;
        public string sCiudad;
        string sIdentificacionRespaldo;

        int iIdDireccion;
        int iIdTelefono;

        int iBandera = 0;

        bool bPasaporte;

        public frmNuevoCliente(string sIdentificacion, bool check)
        {
            this.sIdentificacion = sIdentificacion;
            bPasaporte = check;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        //FUNCION PARA VALIDAR EL CORREO ELECTRONICO
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //FUNCION PARA VALIDAR LA CEDULA O RUC
        private void validarIdentificacion(int iOp)
        {
            try
            {
                iBandera = 0;

                iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));

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

            catch (Exception)
            {
                goto mensaje;
            }

        mensaje:
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                //txtIdentificacion.Clear();
                txtIdentificacion.Text = sIdentificacionRespaldo;
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }

        //CONSULTAR DATOS EN LA BASE
        private void consultarRegistro()
        {
            try
            {
                //sSql = "select TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico, TD.direccion, " +
                //       "TD.calle_principal, TD.numero_vivienda, TD.calle_interseccion, TD.referencia, TT.oficina, " +
                //       "TT.celular from tp_personas TP, tp_direcciones TD, tp_telefonos TT " +
                //       "where TP.id_persona = TD.id_persona and TP.id_persona = TT.id_persona and " +
                //       "TP.estado = 'A' and TD.estado = 'A' and TT.estado = 'A' and " +
                //       "TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                sIdentificacionRespaldo = txtIdentificacion.Text.Trim();

                sSql = "";
                sSql = sSql + "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql = sSql + "TD.direccion, TD.calle_principal, TD.numero_vivienda, TD.calle_interseccion, TD.referencia," + Environment.NewLine;
                sSql = sSql + "TT.domicilio, TT.celular" + Environment.NewLine;
                sSql = sSql + "FROM tp_personas TP" + Environment.NewLine;
                sSql = sSql + "LEFT OUTER JOIN tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql = sSql + "and TP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and TD.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "LEFT OUTER JOIN tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql = sSql + "and TT.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "WHERE  TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {   
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtNombres.Text = dtConsulta.Rows[0][2].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0][3].ToString();
                        txtMail.Text = dtConsulta.Rows[0][4].ToString();
                        txtSector.Text = dtConsulta.Rows[0][5].ToString();
                        txtCallePrincipal.Text = dtConsulta.Rows[0][6].ToString();
                        txtNumeracion.Text = dtConsulta.Rows[0][7].ToString();
                        txtCalleSecundaria.Text = dtConsulta.Rows[0][8].ToString();
                        txtReferencia.Text = dtConsulta.Rows[0][9].ToString();

                        if (dtConsulta.Rows[0][10].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][10].ToString();
                        }

                        else if (dtConsulta.Rows[0][11].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][11].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "299999999";
                        }

                        btnGuardar.Text = "Actualizar";
                    }

                    else
                    {
                        btnGuardar.Text = "Guardar";
                        txtApellidos.Focus();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    txtIdentificacion.Clear();
                    txtIdentificacion.Focus();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        //FUNCION PARA

        //INSERTAR UN NUEVO CLIENTE
        private void insertarCliente()
        {
            try
            {
                //INICIAR LA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                if (txtIdentificacion.Text.Trim().Length == 13)
                {
                    int iTercero = Convert.ToInt32(txtIdentificacion.Text.Trim().Substring(2, 1));

                    iIdTipoIdentificacion = 179;

                    if (iTercero == 9)
                    {
                        iIdTipoPersona = 2448;
                    }

                    else if (iTercero == 6)
                    {
                        iIdTipoPersona = 2448;
                    }

                    else if (iTercero < 6)
                    {
                        iIdTipoPersona = 2447;
                    }
                }

                else if (txtIdentificacion.Text.Trim().Length == 10)
                {
                    iIdTipoIdentificacion = 178;
                    iIdTipoPersona = 2447;
                }

                else
                {
                    iIdTipoIdentificacion = 180;
                    iIdTipoPersona = 2447;
                }

                //INSTRUCCION PARA INSERTAR UN NUEVO CLIENTE EN  LA TABLA TP_PERSONAS
                sSql = "";
                sSql = sSql + "Insert Into tp_personas (" + Environment.NewLine;
                sSql = sSql + "idempresa, Cg_Tipo_Persona, Cg_Pais_Residencia," + Environment.NewLine;
                sSql = sSql + "Cg_Tipo_Identificacion, Identificacion, Nombres, Apellidos, Cliente," + Environment.NewLine;
                sSql = sSql + "Correo_Electronico, codigo_alterno, Estado, Usuario_Ingreso, terminal_ingreso,contador," + Environment.NewLine;
                sSql = sSql + "porcentaje_descuento, hacerlaretencionfuenteir, numero_replica_trigger," + Environment.NewLine;
                sSql = sSql + "numero_control_replica ) " + Environment.NewLine;
                sSql = sSql + "Values (" + Environment.NewLine;
                sSql = sSql + Program.iIdEmpresa + ", " + iIdTipoPersona + ", 2843, " + iIdTipoIdentificacion + "," + Environment.NewLine;
                sSql = sSql + "'" + txtIdentificacion.Text.Trim() + "', '" + txtNombres.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'" + txtApellidos.Text.Trim() + "', 1, '" + txtMail.Text.Trim() + "', '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'A','" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "',0,0,0,0,0 )";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                string sTabla = "tp_personas";
                string sCampo = "id_persona"; 

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo del cliente.";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPersona = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA TP_DIRECCIONES
                sSql = "";
                sSql = sSql + "Insert Into  tp_direcciones (" + Environment.NewLine;
                sSql = sSql + "id_persona,IdTipoEstablecimiento, " + Environment.NewLine;
                sSql = sSql + "Direccion,calle_principal,numero_vivienda,calle_interseccion, referencia," + Environment.NewLine;
                sSql = sSql + "Cg_Localidad,Estado,usuario_ingreso,terminal_ingreso, " + Environment.NewLine;
                sSql = sSql + "fecha_ingreso,numero_replica_trigger,numero_control_replica) " + Environment.NewLine;
                sSql = sSql + "Values (" + Environment.NewLine;
                sSql = sSql + iIdPersona + ", 1, '" + txtSector.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'" + txtCallePrincipal.Text.Trim() + "','" + txtNumeracion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'" + txtCalleSecundaria.Text.Trim() + "', '" + txtReferencia.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + Program.iCgLocalidad + ", 'A','" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GetDate(),0,0 )";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PARA INSERTAR EL TELEFONO EN LA TABLA TP_TELEFONOS
                sSql = "";
                sSql = sSql + "Insert Into tp_telefonos (" + Environment.NewLine;
                sSql = sSql + "id_persona,idTipoEstablecimiento, " + Environment.NewLine;
                sSql = sSql + "CODIGO_AREA,domicilio,celular,Estado,fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                sSql = sSql + "terminal_ingreso,numero_replica_trigger,numero_control_replica ) " + Environment.NewLine;
                sSql = sSql + "Values (" + iIdPersona + ", 1, '02'," + Environment.NewLine;

                if (txtTelefono.Text.Substring(0, 2) == "09")
                {
                    sSql = sSql + " '', '" + txtTelefono.Text.Trim() + "'" + Environment.NewLine;
                }

                else
                {
                    sSql = sSql + " '" + txtTelefono.Text.Trim() + "', ''" + Environment.NewLine;
                }

                sSql = sSql + ",'A', GetDate(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[1] + "', 0,0 )";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Cliente registrado éxitosamente";
                ok.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    iCodigo = iIdPersona;
                    sIdentificacion = txtIdentificacion.Text.Trim();
                    sNombre = txtNombres.Text.Trim();
                    sApellido = txtApellidos.Text.Trim();
                    sTelefono = txtTelefono.Text.Trim();
                    sMail = txtMail.Text.Trim();
                    sDireccion = txtSector.Text.Trim() + ", " + txtCallePrincipal.Text.Trim() + " " + 
                                 txtNumeracion.Text.Trim() + " " + txtCalleSecundaria.Text.Trim();
                    sCiudad = txtSector.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //FUNCION PARA ACTUALIZAR LOS DATOS DEL CLIENTE
        private void actualizarCliente()
        {
            try
            {
                //INICIAR LA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción,";
                    ok.ShowDialog();
                    return;
                }

                if (txtIdentificacion.Text.Trim().Length == 13)
                {
                    int iTercero = Convert.ToInt32(txtIdentificacion.Text.Trim().Substring(2, 1));

                    iIdTipoIdentificacion = 179;

                    if (iTercero == 9)
                    {
                        iIdTipoPersona = 2448;
                    }

                    else if (iTercero == 6)
                    {
                        iIdTipoPersona = 2448;
                    }

                    else if (iTercero < 6)
                    {
                        iIdTipoPersona = 2447;
                    }
                }

                else if (txtIdentificacion.Text.Trim().Length == 10)
                {
                    iIdTipoIdentificacion = 178;
                    iIdTipoPersona = 2447;
                }

                else
                {
                    iIdTipoIdentificacion = 180;
                    iIdTipoPersona = 2447;
                }

                //INSTRUCCION PARA ACTUALIZAR LA TABLA TP_PERSONAS
                sSql = "";
                sSql = sSql + "update tp_personas set" + Environment.NewLine;
                sSql = sSql + "identificacion = '" + txtIdentificacion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "apellidos = '" + txtApellidos.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "nombres = '" + txtNombres.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "codigo_alterno = '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "correo_electronico = '" + txtMail.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "cg_tipo_persona = " + iIdTipoPersona + "," + Environment.NewLine;
                sSql = sSql + "cg_tipo_identificacion = " + iIdTipoIdentificacion + Environment.NewLine;
                sSql = sSql + "where id_persona = " + iIdPersona;

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRCCION PARA EXTRAER EL PRIMER REGISTRO DE DIRECCION DEL CLIENTE CON ESTADO A
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql = sSql + "select top 1 correlativo" + Environment.NewLine;
                sSql = sSql + "from tp_direcciones" + Environment.NewLine;
                sSql = sSql + "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdDireccion = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        //ACTUALIZAMOS LA TABLA DE DIRECCIONES
                        sSql = "";
                        sSql = sSql + "update tp_direcciones set" + Environment.NewLine;
                        sSql = sSql + "direccion = '" + txtSector.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "calle_principal = '" + txtCallePrincipal.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "calle_interseccion = '" + txtCalleSecundaria.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "numero_vivienda = '" + txtNumeracion.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "referencia = '" + txtReferencia.Text.Trim() + "'" + Environment.NewLine;
                        sSql = sSql + "where correlativo = " + iIdDireccion;
                    }

                    else
                    {
                        //INSTRUCCION PARA INSERTAR EN LA TABLA TP_DIRECCIONES
                        sSql = "";
                        sSql = sSql + "Insert Into  tp_direcciones (" + Environment.NewLine;
                        sSql = sSql + "id_persona,IdTipoEstablecimiento, " + Environment.NewLine;
                        sSql = sSql + "Direccion,calle_principal,numero_vivienda,calle_interseccion, referencia," + Environment.NewLine;
                        sSql = sSql + "Cg_Localidad,Estado,usuario_ingreso,terminal_ingreso, " + Environment.NewLine;
                        sSql = sSql + "fecha_ingreso,numero_replica_trigger,numero_control_replica) " + Environment.NewLine;
                        sSql = sSql + "Values (" + Environment.NewLine;
                        sSql = sSql + iIdPersona + ", 1, '" + txtSector.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "'" + txtCallePrincipal.Text.Trim() + "','" + txtNumeracion.Text.Trim() + "'," + Environment.NewLine;
                        sSql = sSql + "'" + txtCalleSecundaria.Text.Trim() + "', '" + txtReferencia.Text.Trim() + "', " + Program.iCgLocalidad + ", 'A','" + Environment.NewLine;
                        sSql = sSql + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GetDate(),0,0 )";
                    }
                }

                else
                {
                    goto reversa;
                }

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRCCION PARA EXTRAER EL PRIMER REGISTRO DE TELEFONO DEL CLIENTE CON ESTADO A
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql = sSql + "select top 1 correlativo" + Environment.NewLine;
                sSql = sSql + "from tp_telefonos" + Environment.NewLine;
                sSql = sSql + "where id_persona = " + iIdPersona + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdTelefono = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        sSql = "";
                        sSql = sSql + "update tp_telefonos set" + Environment.NewLine;
                        sSql = sSql + "domicilio = '" + txtTelefono.Text.Trim() + "'" + Environment.NewLine;
                        sSql = sSql + "where correlativo = " + iIdTelefono;
                    }

                    else
                    {
                        //PARA INSERTAR EL TELEFONO EN LA TABLA TP_TELEFONOS
                        sSql = "";
                        sSql = sSql + "Insert Into tp_telefonos (" + Environment.NewLine;
                        sSql = sSql + "id_persona, idTipoEstablecimiento, CODIGO_AREA, domicilio," + Environment.NewLine;
                        sSql = sSql + "celular,Estado,fecha_ingreso, usuario_ingreso, " + Environment.NewLine;
                        sSql = sSql + "terminal_ingreso,numero_replica_trigger,numero_control_replica)" + Environment.NewLine;
                        sSql = sSql + "Values (" + iIdPersona + ", 1, '02'," + Environment.NewLine;

                        if (txtTelefono.Text.Substring(0, 2) == "09")
                        {
                            sSql = sSql + " '', '" + txtTelefono.Text.Trim() + "'";
                        }

                        else
                        {
                            sSql = sSql + " '" + txtTelefono.Text.Trim() + "', ''";
                        }

                        sSql = sSql + ",'A', GetDate(), '" + Program.sDatosMaximo[0] + "', '" +
                               Program.sDatosMaximo[1] + "', 0,0 )";
                    }
                }

                else
                {
                    goto reversa;
                }

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI NO EXISTIO ERRORES SE REALIZA EL COMMIT A LA BASE DE DATOS
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Cliente actualizado éxitosamente";
                ok.ShowDialog();

                if (ok.DialogResult == DialogResult.OK)
                {
                    iCodigo = iIdPersona;
                    sIdentificacion = txtIdentificacion.Text.Trim();
                    sNombre = txtNombres.Text.Trim();
                    sApellido = txtApellidos.Text.Trim();
                    sTelefono = txtTelefono.Text.Trim();
                    sMail = txtMail.Text.Trim();
                    sDireccion = txtSector.Text.Trim() + ", " + txtCallePrincipal.Text.Trim() + " " +
                                 txtNumeracion.Text.Trim() + " " + txtCalleSecundaria.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                ok.ShowDialog();
            }
        }

        //FUNCION LIMPIAR
        private void limpiar()
        {
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            txtSector.Clear();
            txtCallePrincipal.Clear();
            txtNumeracion.Clear();
            txtCalleSecundaria.Clear();
            txtReferencia.Clear();
            txtIdentificacion.Focus();
        }

        //FUNCION PARA CONTAR EL REGISTRO
        private int contarRegistros()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where identificacion = '" + txtIdentificacion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                    {
                        validarIdentificacion(1);
                    }

                    else
                    {
                        iIdTipoPersona = 2447;
                        iIdTipoIdentificacion = 180;
                        consultarRegistro();
                    }
                }
            }
        }

        private void frmNuevoCliente_Load(object sender, EventArgs e)
        {
            if (sIdentificacion != "")
            {
                txtIdentificacion.Text = sIdentificacion;
                chkPasaporte.Checked = bPasaporte;
                consultarRegistro();
                //txtNombres.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el número de identificación";
                ok.ShowDialog();
                txtIdentificacion.Clear();
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

                //validarIdentificacion();

                //if (iBandera == 0)
                //{
                //    goto fin;
                //}
            }
            
            if (txtApellidos.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el apellido del cliente.";
                ok.ShowDialog();
                txtApellidos.Clear();
                txtApellidos.Focus();
            }

            else if (txtTelefono.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el número de teléfono del cliente.";
                ok.ShowDialog();
                txtTelefono.Clear();
                txtTelefono.Focus();
            }

            else if (txtMail.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el correo electrónico del cliente.";
                ok.ShowDialog();
                txtMail.Clear();
                txtMail.Focus();
            }

            else if (txtSector.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el sector donde reside el cliente.";
                ok.ShowDialog();
                txtSector.Clear();
                txtSector.Focus();
            }

            else
            {
                if (btnGuardar.Text == "Guardar")
                {
                    //ENVIAR A FUNCION DE GUARDAR

                    int iContador = contarRegistros();

                    if (iContador == -1)
                    {
                        return;
                    }

                    if (iContador > 0)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "El número de identificación ya se encuentra registrado.";
                        ok.ShowDialog();

                        consultarRegistro();
                        return;
                    }

                    insertarCliente();
                }

                else if (btnGuardar.Text == "Actualizar")
                {
                    //ENVIAR A LA FUNCION DE ACTUALIZACION
                    actualizarCliente();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void frmNuevoCliente_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCorreoElectronicoDefault_Click(object sender, EventArgs e)
        {
            txtMail.Text = Program.sCorreoElectronicoDefault;
        }
    }
}
