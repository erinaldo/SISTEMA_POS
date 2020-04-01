using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmRegistroEmpresa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();
        ValidarRUC ruc = new ValidarRUC();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        bool bRespuesta;
        bool bActualizar;

        DataTable dtConsulta;

        string sSql;

        int iIdEmpresa;
        int iIdContabilidad;

        public frmRegistroEmpresa()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VALIDAR EL RUC
        private void validarRuc()
        {
            if (txtRUC.Text.Trim().Length == 13)
            {
                if (txtRUC.Text.Substring(2, 1) == "9")
                {
                    if (ruc.validarRucJuridico(txtRUC.Text.Trim()) == "SI")
                    {
                        txtCodigoEmpresa.Focus();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "El RUC ingresado es inválido.";
                        ok.ShowDialog();
                        txtRUC.Clear();
                        txtRUC.Focus();
                    }
                }

                else if (txtRUC.Text.Substring(2, 1) == "6")
                {
                    if (ruc.validarRucPublico(txtRUC.Text.Trim()) == "SI")
                    {
                        txtCodigoEmpresa.Focus();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "El RUC ingresado es inválido.";
                        ok.ShowDialog();
                        txtRUC.Clear();
                        txtRUC.Focus();
                    }
                }

                else
                {
                    if (ruc.validarRucNatural(txtRUC.Text.Trim()) == "SI")
                    {
                        txtCodigoEmpresa.Focus();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "El RUC ingresado es inválido.";
                        ok.ShowDialog();
                        txtRUC.Clear();
                        txtRUC.Focus();
                    }
                }
            }

            else
            {
                ok.lblMensaje.Text = "El RUC ingresado es inválido.";
                ok.ShowDialog();
                txtRUC.Clear();
                txtRUC.Focus();
            }
        }

        //FUNCION PARA CARGAR LOS TIPOS DE AMBIENTE
        private void llenarTipoAmbiente()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_ambiente, nombres" + Environment.NewLine;
                sSql += "From cel_tipo_ambiente" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_ambiente";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTipoAmbiente.DisplayMember = "nombres";
                        cmbTipoAmbiente.ValueMember = "id_tipo_ambiente";
                        cmbTipoAmbiente.DataSource = dtConsulta;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS TIPOS DE EMISION
        private void llenarTipoEmision()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_emision, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_emision" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_emision";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbEmision.DisplayMember = "nombres";
                        cmbEmision.ValueMember = "id_tipo_emision";
                        cmbEmision.DataSource = dtConsulta;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS TIPOS DE CERTIFICADOS DIGITALES
        private void llenarCertificadoDigital()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_certificado_digital, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_certificado_digital" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_certificado_digital";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbCertificadoDigital.DisplayMember = "nombres";
                        cmbCertificadoDigital.ValueMember = "id_tipo_certificado_digital";
                        cmbCertificadoDigital.DataSource = dtConsulta;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CARGAR INFORMACION DE LA BASE DE DATOS
        private void cargarInformacion()
        {
            try
            {
                sSql = "";
                sSql += "select Codigo, razonSocial, numeroRuc, numeroPatronal," + Environment.NewLine;
                sSql += "gerenteGeneral, contadorGeneral, rucContador," + Environment.NewLine;
                sSql += "matriculaContador, sectorMunicipal, actividadEconomica," + Environment.NewLine;
                sSql += "direccionMatriz, Telefono, Fax, Ciudad, Pais, nombrecomercial," + Environment.NewLine;
                sSql += "numeroresolucioncontribuyenteespecial, obligadollevarcontabilidad," + Environment.NewLine;
                sSql += "archivologo, id_tipo_emision, tiempodeespera, id_tipo_ambiente," + Environment.NewLine;
                sSql += "id_tipo_certificado_digital, Estado, IdEmpresa, direccion_corta" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado ='A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtCodigoEmpresa.Text = dtConsulta.Rows[0][0].ToString();
                        txtRazonSocial.Text = dtConsulta.Rows[0][1].ToString();
                        txtRUC.Text = dtConsulta.Rows[0][2].ToString();
                        txtNumeroPatronal.Text = dtConsulta.Rows[0][3].ToString();
                        txtGerenteGeneral.Text = dtConsulta.Rows[0][4].ToString();
                        txtContadorGeneral.Text = dtConsulta.Rows[0][5].ToString();
                        txtRUCContador.Text = dtConsulta.Rows[0][6].ToString();
                        txtMatriculaContador.Text = dtConsulta.Rows[0][7].ToString();
                        txtSectorMunicipal.Text = dtConsulta.Rows[0][8].ToString();
                        txtActividadEconomica.Text = dtConsulta.Rows[0][9].ToString();
                        txtDireccionMatriz.Text = dtConsulta.Rows[0][10].ToString();
                        txtTelefono.Text = dtConsulta.Rows[0][11].ToString();
                        txtFax.Text = dtConsulta.Rows[0][12].ToString();
                        txtCiudad.Text = dtConsulta.Rows[0][13].ToString();
                        txtPais.Text = dtConsulta.Rows[0][14].ToString();
                        txtNombreComercial.Text = dtConsulta.Rows[0][15].ToString();
                        txtContribuyenteEspecial.Text = dtConsulta.Rows[0][16].ToString();

                        if (dtConsulta.Rows[0][17].ToString() == "0")
                        {
                            chkContabilidad.Checked = false;
                        }

                        else
                        {
                            chkContabilidad.Checked = true;
                        }

                        txtLogo.Text = dtConsulta.Rows[0][18].ToString();

                        cmbEmision.SelectedValue = dtConsulta.Rows[0][19].ToString();
                        txtEspera.Text = dtConsulta.Rows[0][20].ToString();
                        cmbTipoAmbiente.SelectedValue = dtConsulta.Rows[0][21].ToString();
                        cmbCertificadoDigital.SelectedValue = dtConsulta.Rows[0][22].ToString();

                        if (dtConsulta.Rows[0][23].ToString() == "A")
                            cmbEstado.SelectedIndex = 0;
                        else
                            cmbEstado.SelectedIndex = 1;

                        iIdEmpresa = Convert.ToInt32(dtConsulta.Rows[0][24].ToString());
                        txtDireccionCorta.Text = dtConsulta.Rows[0]["direccion_corta"].ToString().ToUpper();
                        bActualizar = true;
                        txtRUC.Focus();

                        txtRUC.SelectionStart = txtRUC.Text.Trim().Length;
                    }

                    else
                    {
                        bActualizar = false;
                        limpiar();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR LAS CAJAS DE TEXTO
        private void limpiar()
        {
            txtRUC.Clear();
            txtCodigoEmpresa.Clear();
            txtRazonSocial.Clear();
            txtNombreComercial.Clear();
            txtDireccionMatriz.Clear();
            txtTelefono.Clear();
            txtFax.Clear();
            txtContribuyenteEspecial.Clear();
            txtLogo.Clear();
            txtEspera.Clear();
            txtNumeroPatronal.Clear();
            txtActividadEconomica.Clear();
            txtSectorMunicipal.Clear();
            txtCiudad.Clear();
            txtPais.Clear();
            txtGerenteGeneral.Clear();
            txtContadorGeneral.Clear();
            txtRUCContador.Clear();
            txtMatriculaContador.Clear();
            txtDireccionCorta.Clear();
            chkContabilidad.Checked = false;
            cmbEstado.SelectedIndex = 0;
            llenarTipoAmbiente();
            llenarTipoEmision();
            llenarCertificadoDigital();
            iIdContabilidad = 0;
            iIdEmpresa = 0;
            txtRUC.Focus();
        }

        //FUNCION PARA INSERTAR UN REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into sis_empresa(" + Environment.NewLine;
                sSql += "numeroruc, codigo, razonsocial, nombrecomercial, direccionmatriz," + Environment.NewLine;
                sSql += "telefono, fax, numeroresolucioncontribuyenteespecial, archivologo," + Environment.NewLine;
                sSql += "id_tipo_emision, tiempodeespera, obligadollevarcontabilidad," + Environment.NewLine;
                sSql += "id_tipo_certificado_digital, id_tipo_ambiente, numeropatronal," + Environment.NewLine;
                sSql += "actividadeconomica, sectormunicipal, ciudad, pais, gerentegeneral," + Environment.NewLine;
                sSql += "contadorgeneral, ruccontador, matriculacontador, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, direccion_corta)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtRUC.Text.Trim() + "', '" + txtCodigoEmpresa.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtRazonSocial.Text.Trim() + "', '" + txtNombreComercial.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtDireccionMatriz.Text.Trim() + "', '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtFax.Text.Trim() + "', ";

                if (txtContribuyenteEspecial.Text.Trim() == "")
                {
                    sSql += "null," + Environment.NewLine;
                }

                else
                {
                    sSql += Convert.ToInt32(txtContribuyenteEspecial.Text.Trim()) + "," + Environment.NewLine;
                }                
                
                sSql += "'" + txtLogo.Text.Trim() + "', " + Convert.ToInt32(cmbEmision.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtEspera.Text.Trim()) + ", " + iIdContabilidad + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbCertificadoDigital.SelectedValue) + ", " + Convert.ToInt32(cmbTipoAmbiente.SelectedValue) + "," + Environment.NewLine;
                sSql += "'" + txtNumeroPatronal.Text.Trim() + "', '" + txtActividadEconomica.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtSectorMunicipal.Text.Trim() + "', '" + txtCiudad.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtPais.Text.Trim() + "', '" + txtGerenteGeneral.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtContadorGeneral.Text.Trim() + "', '" + txtRUCContador.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtMatriculaContador.Text.Trim() + "', 'A', GETDATE(), "  + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', '" + txtDireccionCorta.Text.Trim().ToUpper() + "')";

                //EJECUTA EL QUERY DE INSERCIÓN
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro insertado éxitosamente.";
                ok.ShowDialog();
                cargarInformacion();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update sis_empresa Set" + Environment.NewLine;
                sSql += "numeroRuc = '" + txtRUC.Text.Trim() + "'," + Environment.NewLine;
                sSql += "codigo = '" + txtCodigoEmpresa.Text.Trim() + "'," + Environment.NewLine;
                sSql += "razonSocial = '" + txtRazonSocial.Text.Trim() + "'," + Environment.NewLine;
                sSql += "nombrecomercial ='" + txtNombreComercial.Text.Trim() + "'," + Environment.NewLine;
                sSql += "direccionMatriz = '" + txtDireccionMatriz.Text.Trim() + "'," + Environment.NewLine;
                sSql += "telefono = '" + txtTelefono.Text.Trim() + "', " + Environment.NewLine;
                sSql += "fax = '" + txtFax.Text.Trim() + "'," + Environment.NewLine;

                if (txtContribuyenteEspecial.Text.Trim() == "")
                {
                    sSql += "numeroresolucioncontribuyenteespecial = null," + Environment.NewLine;
                }

                else
                {
                    sSql += "numeroresolucioncontribuyenteespecial = " + Convert.ToInt32(txtContribuyenteEspecial.Text.Trim()) + "," + Environment.NewLine;
                }

                sSql += "archivologo = '" + txtLogo.Text.Trim() + "'," + Environment.NewLine;
                sSql += "id_tipo_emision = " + Convert.ToInt32(cmbEmision.SelectedValue) + "," + Environment.NewLine;
                sSql += "tiempodeespera = " + Convert.ToInt32(txtEspera.Text.Trim()) + "," + Environment.NewLine;
                sSql += "obligadollevarcontabilidad = " + iIdContabilidad + "," + Environment.NewLine;
                sSql += "id_tipo_certificado_digital = " + Convert.ToInt32(cmbCertificadoDigital.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_tipo_ambiente = " + Convert.ToInt32(cmbTipoAmbiente.SelectedValue) + "," + Environment.NewLine;
                sSql += "numeroPatronal = '" + txtNumeroPatronal.Text.Trim() + "'," + Environment.NewLine;
                sSql += "actividadEconomica = '" + txtActividadEconomica.Text.Trim() + "'," + Environment.NewLine;
                sSql += "sectorMunicipal = '" + txtSectorMunicipal.Text.Trim() + "'," + Environment.NewLine;
                sSql += "ciudad = '" + txtCiudad.Text.Trim() + "'," + Environment.NewLine;
                sSql += "pais = '" + txtPais.Text.Trim() + "'," + Environment.NewLine;
                sSql += "gerenteGeneral = '" + txtGerenteGeneral.Text.Trim() + "'," + Environment.NewLine;
                sSql += "contadorGeneral = '" + txtContadorGeneral.Text.Trim() + "'," + Environment.NewLine;
                sSql += "rucContador = '" + txtRUCContador.Text.Trim() + "'," + Environment.NewLine;
                sSql += "matriculaContador = '" + txtMatriculaContador.Text.Trim() + "'," + Environment.NewLine;
                sSql += "direccion_corta = '" + txtDireccionCorta.Text.Trim().ToUpper() + "'" + Environment.NewLine;
                //sSql += "usuario_ingreso = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "terminal_ingreso= '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                //sSql += "fecha_ingreso= GETDATE()" + Environment.NewLine;
                sSql += "where idEmpresa = " + iIdEmpresa;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                cargarInformacion();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return;  }
        }


        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update sis_empresa Set" + Environment.NewLine;
                sSql += "estado = 'N'," + txtRUC.Text.Trim() + "'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()" + Environment.NewLine;
                sSql += "where codigo ='" + txtCodigoEmpresa.Text.Trim() + "'";
                
                //EJECUTA EL QUERY DE ELIMINACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                cargarInformacion();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void frmRegistroEmpresa_Load(object sender, EventArgs e)
        {
            llenarTipoAmbiente();
            llenarTipoEmision();
            llenarCertificadoDigital();
            cargarInformacion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cargarInformacion();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtRUC.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el número de RUC del emisor.";
                ok.ShowDialog();
                txtRUC.Focus();
            }

            else if (txtCodigoEmpresa.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el código de la empresa.";
                ok.ShowDialog();
                txtCodigoEmpresa.Focus();
            }

            else if (txtRazonSocial.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la razón social del emisor.";
                ok.ShowDialog();
                txtRazonSocial.Focus();
            }

            else if (txtDireccionMatriz.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese la dirección de la matriz de la empresa.";
                ok.ShowDialog();
                txtDireccionMatriz.Focus();
            }

            else if (txtTelefono.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el teléfono de la empresa.";
                ok.ShowDialog();
                txtTelefono.Focus();
            }

            else if (txtEspera.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "Favor ingrese el tiempo de espera para la autorización de comprobantes.";
                ok.ShowDialog();
                txtEspera.Focus();
            }

            else
            {
                if (chkContabilidad.Checked == true)
                {
                    iIdContabilidad = 1;
                }

                else
                {
                    iIdContabilidad = 0;
                }

                if (bActualizar == true)
                {
                    //ENVIAR A FUNCION PARA ACTUALIZAR EL REGISTRO
                    actualizarRegistro();
                }

                else
                {
                    //ENVIAR A FUNCION PARA INSERTAR EL REGISTRO
                    insertarRegistro();
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro de la empresa?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                validarRuc();
            }

            else
            {
                caracteres.soloNumeros(e);
            }
        }

        private void txtCodigoEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtContribuyenteEspecial_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtEspera_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtNumeroPatronal_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtRUCContador_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void btnAbrirModalLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtLogo.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnLimpiarLogo_Click(object sender, EventArgs e)
        {
            txtLogo.Clear();
        }
    }
}
