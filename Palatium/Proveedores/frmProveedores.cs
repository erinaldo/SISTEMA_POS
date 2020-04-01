using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Proveedores
{
    public partial class frmProveedores : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        string sSql;
        string sAuxiliar = "PVL";
        string sAuxiliarConcatenar;
        string sTabla;
        string sCampo;
        string sDescripcionAuxiliar;

        DataTable dtConsulta;

        bool bRespuesta;

        Int32 iSecuenciaCodigo;

        int iIdPersona;
        int iIdDireccion;
        int iIdTelefono;
        int iIdTipoEstablecimiento;
        int iCgLocalidad;
        int iIdAuxiliar;
        int iValorNumero;
        int iObligadoContabilidad;
        int iContribuyenteEspecial;

        long iMaximo;

        public frmProveedores()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE TIPO DE IDENTIFICACION
        private void llenarComboTipoIdentificacion()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, codigo, valor_texto, valor_fecha," + Environment.NewLine;
                sSql += "valor_numero, tabla,  valor_texto  descripcion" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00030'" + Environment.NewLine;
                sSql += "and estado = 'A' " + Environment.NewLine;
                sSql += "order by valor_texto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbTipoIdentificacion.DisplayMember = "valor_texto";
                cmbTipoIdentificacion.ValueMember = "correlativo";
                cmbTipoIdentificacion.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE TIPO DE PERSONA
        private void llenarComboTipoPersona()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, codigo, valor_texto, valor_fecha," + Environment.NewLine;
                sSql += "valor_numero, tabla,  valor_texto  descripcion" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00056'" + Environment.NewLine;
                sSql += "and estado = 'A' " + Environment.NewLine;
                sSql += "order by correlativo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbTipoPersona.DisplayMember = "valor_texto";
                cmbTipoPersona.ValueMember = "correlativo";
                cmbTipoPersona.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE PAIS DE RESIDENCIA
        private void llenarComboPaisResidencia()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, codigo, valor_texto, valor_fecha," + Environment.NewLine;
                sSql += "valor_numero, tabla,  valor_texto  descripcion" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00815'" + Environment.NewLine;
                sSql += "and estado = 'A' " + Environment.NewLine;
                sSql += "order by valor_texto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbPaisResidencia.DisplayMember = "valor_texto";
                cmbPaisResidencia.ValueMember = "correlativo";
                cmbPaisResidencia.DataSource = dtConsulta;

                cmbPaisResidencia.SelectedValue = "2843";
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA LLENAR EL COMBOBOX DE CIUDADES
        private void llenarComboCiudades()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, codigo, valor_texto, valor_fecha," + Environment.NewLine;
                sSql += "valor_numero, tabla,  valor_texto  descripcion" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00005'" + Environment.NewLine;
                sSql += "and estado = 'A' " + Environment.NewLine;
                sSql += "order by valor_texto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbCiudad.DisplayMember = "valor_texto";
                cmbCiudad.ValueMember = "correlativo";
                cmbCiudad.DataSource = dtConsulta;

                cmbCiudad.SelectedValue = "2935";
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE ESTABLECIMIENTOS
        private void llenarComboEstablecimiento()
        {
            try
            {
                sSql = "";
                sSql += "select idTipoEstablecimiento, codigo + ' ' + substring(descripcion,1,8) descripcion" + Environment.NewLine;
                sSql += "from sistipoestablecimiento" + Environment.NewLine;
                sSql += "where estado = 'A' ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbEstablecimiento.DisplayMember = "descripcion";
                cmbEstablecimiento.ValueMember = "idTipoEstablecimiento";
                cmbEstablecimiento.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE AÑOS FISCALES
        private void llenarComboAnioFiscal()
        {
            try
            {
                sSql = "";
                sSql += "Select distinct A.Descripcion , A.Ano_Fiscal" + Environment.NewLine;
                sSql += "From cv404_anos_fiscales A, cv404_planes_empresa E" + Environment.NewLine;
                sSql += "Where A.Ano_Fiscal = E.Ano_Fiscal" + Environment.NewLine;
                sSql += "And  E.IdEmpresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "And A.IdEmpresa = E.IdEmpresa" + Environment.NewLine;
                sSql += "order by A.Ano_Fiscal desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbAnioFiscal.DisplayMember = "Descripcion";
                cmbAnioFiscal.ValueMember = "Ano_Fiscal";
                cmbAnioFiscal.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE TIPOS DE AUXILIARES
        private void llenarComboTipoAuxiliares()
        {
            try
            {
                sSql = "";
                sSql += "select '['+codigo+']'+valor_texto descripcion, correlativo " + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where TABLA = 'SYS$00037'" + Environment.NewLine;
                sSql += "and estado ='A'" + Environment.NewLine;
                sSql += "and valor_numero = 2";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                cmbTipoAuxiliares.DisplayMember = "descripcion";
                cmbTipoAuxiliares.ValueMember = "correlativo";
                cmbTipoAuxiliares.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboTipoIdentificacion();
            llenarComboPaisResidencia();
            llenarComboTipoPersona();
            llenarComboEstablecimiento();
            llenarComboCiudades();
            llenarComboAnioFiscal();
            llenarComboTipoAuxiliares();

            txtIdentificacion.Clear();
            txtApellidos.Clear();
            txtNombres.Clear();
            txtNombreComercial.Clear();
            txtContactoEmpresa.Clear();
            txtSector.Clear();
            txtCallePrincipal.Clear();
            txtNumeracion.Clear();
            txtCalleSecundaria.Clear();
            txtCodigoProvincia.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtMail.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            chkObligadoContabilidad.Checked = false;
            chkContribuyenteEspecial.Checked = false;

            iIdPersona = 0;
            iIdDireccion = 0;
            iIdTelefono = 0;
            iIdTipoEstablecimiento = 0;
            iCgLocalidad = 0;
            iIdAuxiliar = 0;

            txtIdentificacion.Enabled = true;
            txtIdentificacion.Focus();
        }

        //FUNCION PARA LIMPIAR
        private void limpiarConsulta()
        {
            txtApellidos.Clear();
            txtNombres.Clear();
            txtNombreComercial.Clear();
            txtContactoEmpresa.Clear();
            txtSector.Clear();
            txtCallePrincipal.Clear();
            txtNumeracion.Clear();
            txtCalleSecundaria.Clear();
            txtCodigoProvincia.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtMail.Clear();
            txtDescripcion.Clear();
            chkObligadoContabilidad.Checked = false;
            chkContribuyenteEspecial.Checked = false;

            iIdPersona = 0;
            iIdDireccion = 0;
            iIdTelefono = 0;
            iIdTipoEstablecimiento = 0;
            iCgLocalidad = 0;
            iIdAuxiliar = 0;
            txtIdentificacion.Enabled = true;
            txtApellidos.Focus();
        }

        //FUNCION PARA CONSULTAR REGISTROS
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, apellidos, isnull(nombres,'') nombres, identificacion, codigo_alterno," + Environment.NewLine;
                sSql += "isnull(nombre_comercial, '') nombre_comercial, isnull(contacto_empresa, '') contacto_empresa," + Environment.NewLine;
                sSql += "isnull(correo_electronico, '') correo_electronico, cg_tipo_identificacion," + Environment.NewLine;
                sSql += "cg_tipo_persona, personanaturalobligadacontabilidad, contribuyenteespecial" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where identificacion = '" + txtIdentificacion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    if (generarCodigo() == false)
                    {
                        return;
                    }

                    limpiarConsulta();
                }

                else
                {
                    iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                    txtApellidos.Text = dtConsulta.Rows[0]["apellidos"].ToString().Trim().ToUpper();
                    txtNombres.Text = dtConsulta.Rows[0]["nombres"].ToString().Trim().ToUpper();
                    txtNombreComercial.Text = dtConsulta.Rows[0]["nombre_comercial"].ToString().Trim().ToUpper();
                    txtContactoEmpresa.Text = dtConsulta.Rows[0]["contacto_empresa"].ToString().Trim().ToUpper();
                    txtMail.Text = dtConsulta.Rows[0]["correo_electronico"].ToString().Trim().ToUpper();

                    cmbTipoIdentificacion.SelectedValue = dtConsulta.Rows[0]["cg_tipo_identificacion"].ToString();
                    cmbTipoPersona.SelectedValue = dtConsulta.Rows[0]["cg_tipo_persona"].ToString();

                    if (Convert.ToInt32(dtConsulta.Rows[0]["personanaturalobligadacontabilidad"].ToString()) == 0)
                    {
                        chkObligadoContabilidad.Checked = false;
                    }

                    else
                    {
                        chkObligadoContabilidad.Checked = true;
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0]["contribuyenteespecial"].ToString()) == 0)
                    {
                        chkContribuyenteEspecial.Checked = false;
                    }

                    else
                    {
                        chkContribuyenteEspecial.Checked = true;
                    }

                    txtIdentificacion.Enabled = false;

                    if (recuperarInformacion() == false)
                    {
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA GENERAR UN CODIGO DE PROVEEDOR
        private bool generarCodigo()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(max(codigo),'') ultimocodigo" + Environment.NewLine;
                sSql += "From cv404_auxiliares_contables" + Environment.NewLine;
                sSql += "Where Idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and cg_clasificacion = " + cmbTipoAuxiliares.SelectedValue + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iSecuenciaCodigo = 1;
                    sAuxiliarConcatenar = sAuxiliar + iSecuenciaCodigo.ToString().PadLeft(5, '0');
                }

                else
                {
                    sAuxiliarConcatenar = dtConsulta.Rows[0][0].ToString().Trim();
                    iSecuenciaCodigo = Convert.ToInt32(sAuxiliarConcatenar.Substring(3, sAuxiliarConcatenar.Length - 3));
                    iSecuenciaCodigo++;
                    sAuxiliarConcatenar = sAuxiliar + iSecuenciaCodigo.ToString().PadLeft(5, '0');
                }

                txtCodigo.Text = sAuxiliarConcatenar;

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

        //FUNCION PARA RECUPERAR DATOS
        private bool recuperarInformacion()
        {
            try
            {
                sSql = "";
                sSql += "Select TIPO.idTipoEstablecimiento, TIPO.codigo+' '+substring(descripcion,1,8) descripcion " + Environment.NewLine;
                sSql += "From sistipoestablecimiento TIPO, tp_vw_direcciones D" + Environment.NewLine;
                sSql += "Where TIPO.estado = 'A'" + Environment.NewLine;
                sSql += "and D.id_persona = " +iIdPersona + Environment.NewLine;
                sSql += "And D.idTipoEstablecimiento = TIPO.idTipoEstablecimiento ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                cmbEstablecimiento.SelectedValue = dtConsulta.Rows[0]["idTipoEstablecimiento"].ToString();
                iIdTipoEstablecimiento = Convert.ToInt32(dtConsulta.Rows[0]["idTipoEstablecimiento"].ToString());

                sSql = "";
                sSql += "select top 1 Correlativo, Direccion, calle_principal," + Environment.NewLine;
                sSql += "numero_vivienda, calle_interseccion, Cg_Localidad" + Environment.NewLine;
                sSql += "From tp_direcciones" + Environment.NewLine;
                sSql += "Where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "And IdTipoEstablecimiento = " + iIdTipoEstablecimiento + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    txtSector.Clear();
                    txtCallePrincipal.Clear();
                    txtNumeracion.Clear();
                    txtCalleSecundaria.Clear();
                    iCgLocalidad = 0;
                }

                else
                {
                    txtSector.Text = dtConsulta.Rows[0]["Direccion"].ToString().Trim().ToUpper();
                    txtCallePrincipal.Text = dtConsulta.Rows[0]["calle_principal"].ToString().Trim().ToUpper();
                    txtNumeracion.Text = dtConsulta.Rows[0]["numero_vivienda"].ToString().Trim().ToUpper();
                    txtCalleSecundaria.Text = dtConsulta.Rows[0]["calle_interseccion"].ToString().Trim().ToUpper();
                    iCgLocalidad = Convert.ToInt32(dtConsulta.Rows[0]["Cg_Localidad"].ToString());
                    iIdDireccion = Convert.ToInt32(dtConsulta.Rows[0]["correlativo"].ToString().Trim());
                }

                sSql = "";
                sSql += "Select Correlativo, codigo_area, isnull(oficina,domicilio) oficina, celular" + Environment.NewLine;
                sSql += "From tp_telefonos" + Environment.NewLine;
                sSql += "Where id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "And idTipoEstablecimiento = " + iIdTipoEstablecimiento + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    txtCodigoProvincia.Clear();
                    txtTelefono.Clear();
                    txtCelular.Clear();
                    iIdTelefono = 0;
                }

                else
                {
                    txtCodigoProvincia.Text = dtConsulta.Rows[0]["codigo_area"].ToString().Trim();
                    txtTelefono.Text = dtConsulta.Rows[0]["oficina"].ToString().Trim();
                    txtCelular.Text = dtConsulta.Rows[0]["celular"].ToString().Trim();
                    iIdTelefono = Convert.ToInt32(dtConsulta.Rows[0]["correlativo"].ToString().Trim());
                }

                sSql = "";
                sSql += "Select id_auxiliar, codigo, descripcion" + Environment.NewLine;
                sSql += "From cv404_auxiliares_contables" + Environment.NewLine;
                sSql += "Where Idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "And cg_clasificacion = " + cmbTipoAuxiliares.SelectedValue + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    if (generarCodigo() == false)
                    {
                        return false;
                    }
                }

                else
                {
                    iIdAuxiliar = Convert.ToInt32(dtConsulta.Rows[0]["id_auxiliar"].ToString());
                    txtCodigo.Text = dtConsulta.Rows[0]["codigo"].ToString().Trim().ToUpper();
                    txtDescripcion.Text = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
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

        //FUNCION PARA INSERTAR
        private void insertarRegistro()
        {
            try
            {
                //sSql = "";
                //sSql += "select valor_numero" + Environment.NewLine;
                //sSql += "from tp_codigos" + Environment.NewLine;
                //sSql += "where tabla = 'SYS$00023'" + Environment.NewLine;
                //sSql += "and estado = 'A'" + Environment.NewLine;
                //sSql += "and codigo = 'NUMERO_PROVINCIA'";

                //dtConsulta = new DataTable();
                //dtConsulta.Clear();

                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                //if (bRespuesta == false)
                //{
                //    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                //    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //    catchMensaje.ShowDialog();
                //    return;
                //}

                //iValorNumero = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                if (conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION) == false)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo iniciar la transacción para guardar la información.";
                    ok.ShowDialog();
                    return;
                }

                if (registroTpPersonas() == false)
                {
                    goto reversa;
                }
                
                if (registroTpDirecciones()== false)
                {
                    goto reversa;
                }

                if (registroTpTelefonos() == false)
                {
                    goto reversa;
                }

                if (registroAuxiliares() == false)
                {
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro guardado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR O INSERTAR EN LA TABLA TP_PERSONAS
        private bool registroTpPersonas()
        {
            try
            {
                sSql = "";

                if (iIdPersona == 0)
                {
                    sSql += "insert into tp_personas (" + Environment.NewLine;
                    sSql += "identificacion, apellidos, nombres, nombre_comercial, contacto_empresa," + Environment.NewLine;
                    sSql += "cg_tipo_persona, cg_tipo_identificacion, cg_pais_residencia," + Environment.NewLine;
                    sSql += "personanaturalobligadacontabilidad, contribuyenteespecial, Correo_Electronico," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "'" + txtIdentificacion.Text.Trim() + "', '" + txtApellidos.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "'" + txtNombres.Text.Trim().ToUpper() + "', '" + txtNombreComercial.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "'" + txtContactoEmpresa.Text.Trim().ToUpper() + "', " + cmbTipoPersona.SelectedValue + ", ";
                    sSql += cmbTipoIdentificacion.SelectedValue + ", " + cmbPaisResidencia.SelectedValue + "," + Environment.NewLine;
                    sSql += iObligadoContabilidad + ", " + iContribuyenteEspecial + ", '" + txtMail.Text.Trim().ToLower() + "'," + Environment.NewLine;
                    sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";
                }

                else
                {
                    sSql += "Update tp_personas Set" + Environment.NewLine;
                    sSql += "Cg_Tipo_Persona = " + cmbTipoPersona.SelectedValue + "," + Environment.NewLine;
                    sSql += "Cg_Pais_Residencia = " + cmbPaisResidencia.SelectedValue + "," + Environment.NewLine;
                    sSql += "Cg_Tipo_Identificacion = " + cmbTipoIdentificacion.SelectedValue + "," + Environment.NewLine;
                    sSql += "Identificacion = '" + txtIdentificacion.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "Nombres = '" + txtNombres.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "Apellidos = '" + txtApellidos.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "contacto_empresa = '" + txtContactoEmpresa.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "nombre_comercial = '" + txtNombreComercial.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "personanaturalobligadacontabilidad = " + iObligadoContabilidad + "," + Environment.NewLine;
                    sSql += "contribuyenteespecial = " + iContribuyenteEspecial + "," + Environment.NewLine;
                    sSql += "Correo_Electronico = '" + txtMail.Text.Trim().ToLower() + "'" + Environment.NewLine;
                    sSql += "Where id_persona = " + iIdPersona;
                }

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (iIdPersona == 0)
                {
                    sTabla = "tp_personas";
                    sCampo = "id_persona";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se pudo obtener el campo identificador de la tabla " + sTabla + ".";
                        ok.ShowDialog();
                        return false;
                    }

                    iIdPersona = Convert.ToInt32(iMaximo);
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

        //FUNCION PARA ACTUALIZAR O INSERTAR EN LA TABLA TP_DIRECCIONES
        private bool registroTpDirecciones()
        {
            try
            {
                sSql = "";

                if (iIdDireccion == 0)
                {
                    sSql += "insert into tp_direcciones (" + Environment.NewLine;
                    sSql += "id_persona, idtipoestablecimiento, cg_localidad, direccion," + Environment.NewLine;
                    sSql += "calle_principal, calle_interseccion, numero_vivienda, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPersona + ", " + cmbEstablecimiento.SelectedValue + ", " + cmbCiudad.SelectedValue + ", ";
                    sSql += "'" + txtSector.Text.Trim().ToUpper() + "', '" + txtCallePrincipal.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "'" + txtCalleSecundaria.Text.Trim().ToUpper() + "', '" + txtNumeracion.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";
                }

                else
                {
                    sSql += "Update tp_direcciones Set" + Environment.NewLine;
                    sSql += "Direccion = '" + txtSector.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "calle_principal = '" + txtCallePrincipal.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "numero_vivienda = '" + txtNumeracion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "calle_interseccion = '" + txtCalleSecundaria.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "Cg_Localidad = " + cmbCiudad.SelectedValue + "," + Environment.NewLine;
                    sSql += "IdTipoEstablecimiento = " + cmbEstablecimiento.SelectedValue + Environment.NewLine;
                    sSql += "Where Correlativo = " + iIdDireccion;
                }

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
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

        //FUNCION PARA ACTUALIZAR O INSERTAR EN LA TABLA TP_TELEFONOS
        private bool registroTpTelefonos()
        {
            try
            {
                sSql = "";

                if (iIdTelefono == 0)
                {
                    sSql += "insert into tp_telefonos (" + Environment.NewLine;
                    sSql += "id_persona, idtipoestablecimiento, codigo_area, oficina," + Environment.NewLine;
                    sSql += "celular, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPersona + ", " + cmbEstablecimiento.SelectedValue + ", '" + txtCodigoProvincia.Text.Trim() + "', ";
                    sSql += "'" + txtTelefono.Text.Trim() + "', '" + txtCelular.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";
                }

                else
                {
                    sSql += "Update tp_telefonos Set" + Environment.NewLine;
                    sSql += "IdTipoEstablecimiento = " + cmbEstablecimiento.SelectedValue + "," + Environment.NewLine;
                    sSql += "Codigo_Area = '" + txtCodigoProvincia.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "Oficina = '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "Celular = '" + txtCelular.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "Where Correlativo = " + iIdTelefono;
                }

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
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

        //FUNCION PARA ACTUALIZAR O INSERTAR EN LAS TABLAS CV404_AUXILIARES_CONTABLES Y CV404_AUXILIARES_EMPRESA
        private bool registroAuxiliares()
        {
            try
            {
                sDescripcionAuxiliar = (txtApellidos.Text.Trim().ToUpper() + " " + txtNombres.Text.Trim().ToUpper()).Trim();

                sSql = "";

                if (iIdAuxiliar == 0)
                {
                    sSql += "insert into cv404_auxiliares_contables (" + Environment.NewLine;
                    sSql += "idempresa, cg_clasificacion, codigo, descripcion, direccion, representante, ruc," + Environment.NewLine;
                    sSql += "telefono, casilla, porcentaje, codigo_auxiliar_base, estado, id_persona," + Environment.NewLine;
                    sSql += "cg_comprobante_retencion, cg_cs_presupuesta, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                    sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + cmbTipoAuxiliares.SelectedValue + ", '" + txtCodigo.Text.Trim().ToUpper() + "', ";
                    sSql += "'" + sDescripcionAuxiliar + "', '', '', '', '', '', 0, '', 'A'," + Environment.NewLine;
                    sSql += iIdPersona + ", null, null, GETDATE(), '" + Program.sDatosMaximo[0] + "', ";
                    sSql += "'" + Program.sDatosMaximo[1] + "', 0, 0)";
                }

                else
                {
                    sSql += "update cv404_auxiliares_contables set" + Environment.NewLine;
                    sSql += "descripcion = '" + sDescripcionAuxiliar + "'," + Environment.NewLine;
                    sSql += "estado = 'A'" + Environment.NewLine;
                    sSql += "where id_auxiliar = " + iIdAuxiliar;
                }

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (iIdAuxiliar == 0)
                {
                    sTabla = "cv404_auxiliares_contables";
                    sCampo = "id_auxiliar";

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se pudo obtener el campo identificador de la tabla " + sTabla + ".";
                        ok.ShowDialog();
                        return false;
                    }

                    iIdAuxiliar = Convert.ToInt32(iMaximo);

                    sSql = "";
                    sSql += "insert into cv404_auxiliares_empresa (" + Environment.NewLine;
                    sSql += "idempresa, cg_empresa, ano_fiscal, id_auxiliar, estado, cg_moneda_restriccion," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + cmbAnioFiscal.SelectedValue + ", ";
                    sSql += iIdAuxiliar + ", 'A', " + Program.iMoneda + ", 0, 0)";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
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

        //FUNCION MENSAJE DE VALIDACION DE CEDULA
        private void mensajeValidarCedula()
        {
            ok = new VentanasMensajes.frmMensajeNuevoOk();
            ok.lblMensaje.Text = "El número de identificación ingresado es incorrecto.";
            ok.ShowDialog();
            txtIdentificacion.Clear();
            txtIdentificacion.Focus();
        }

        #endregion

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {          
            if (txtIdentificacion.Text.Trim() != "")
            {
                int iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Trim().Substring(2, 1));

                if (Convert.ToInt32(cmbTipoIdentificacion.SelectedValue) == 178)
                {
                    if (txtIdentificacion.Text.Trim().Length < 10)
                    {
                        mensajeValidarCedula();
                        return;
                    }

                    if (iTercerDigito > 6)
                    {
                        mensajeValidarCedula();
                        return;
                    }

                    if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                    {
                        cmbTipoPersona.SelectedValue = 2447;
                        consultarRegistro();
                        return;
                    }

                    else
                    {
                        mensajeValidarCedula();
                        return;
                    }
                }

                else if (Convert.ToInt32(cmbTipoIdentificacion.SelectedValue) == 179)
                {
                    if (txtIdentificacion.Text.Trim().Length < 13)
                    {
                        mensajeValidarCedula();
                        return;
                    }

                    if (iTercerDigito == 9)
                    {
                        if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                        {
                            cmbTipoPersona.SelectedValue = 2448;
                            consultarRegistro();
                            return;
                        }

                        else
                        {
                            mensajeValidarCedula();
                            return;
                        }
                    }

                    else if (iTercerDigito == 6)
                    {
                        if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                        {
                            cmbTipoPersona.SelectedValue = 2448;
                            consultarRegistro();
                            return;
                        }

                        else
                        {
                            mensajeValidarCedula();
                            return;
                        }
                    }

                    else if (iTercerDigito <= 5)
                    {
                        if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                        {
                            cmbTipoPersona.SelectedValue = 2447;
                            consultarRegistro();
                            return;
                        }

                        else
                        {
                            mensajeValidarCedula();
                            return;
                        }
                    }

                    else
                    {
                        mensajeValidarCedula();
                        return;
                    }
                }

                else
                {
                    consultarRegistro();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de identificación del proveedor.";
                ok.ShowDialog();
                txtIdentificacion.Focus();
                return;
            }

            if (txtApellidos.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la razón social o apellidos del proveedor.";
                ok.ShowDialog();
                txtApellidos.Focus();
                return;
            }

            if (txtSector.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el sector del proveedor.";
                ok.ShowDialog();
                txtSector.Focus();
                return;
            }

            if (txtCallePrincipal.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la calle principal del proveedor.";
                ok.ShowDialog();
                txtCallePrincipal.Focus();
                return;
            }

            if (txtNumeracion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la numeración del domicilio del proveedor.";
                ok.ShowDialog();
                txtNumeracion.Focus();
                return;
            }

            if (txtCodigoProvincia.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de código de provincia para el teléfono.";
                ok.ShowDialog();
                txtCodigoProvincia.Focus();
                return;
            }

            if (txtTelefono.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese un número convencional del proveedor.";
                ok.ShowDialog();
                txtTelefono.Focus();
                return;
            }

            if (chkObligadoContabilidad.Checked == true)
                iObligadoContabilidad = 1;
            else
                iObligadoContabilidad = 0;

            if (chkContribuyenteEspecial.Checked == true)
                iContribuyenteEspecial = 1;
            else
                iContribuyenteEspecial = 0;

            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                insertarRegistro();
            }
        }

        private void txtCodigoProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            if (txtMail.Text.Trim() != "")
            {
                if (caracter.validarCorreoElectronico(txtMail.Text.Trim().ToLower()) == false)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ha ingresado un correo electrónico inválido. Favor verifique";
                    ok.ShowDialog();
                    txtMail.Focus();
                    return;
                }
            }
        }

        private void cmbTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTipoIdentificacion.SelectedValue) == 178)
            {
                txtIdentificacion.MaxLength = 10;
            }

            else if (Convert.ToInt32(cmbTipoIdentificacion.SelectedValue) == 179)
            {
                txtIdentificacion.MaxLength = 13;
            }

            else
            {
                txtIdentificacion.MaxLength = 15;
            }
        }
    }
}
