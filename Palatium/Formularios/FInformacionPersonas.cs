using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FInformacionPersonas : Form
    {
        ///VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        //bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dtConsulta;
        DataTable dtConsulta_Ayuda;
        bool bRespuesta;
        
        bool x = false; //creamos la variable

        DataTable dtTipoPersona = new DataTable();
        DataTable dtTipoIdentificacion = new DataTable();
        DataTable dtPaisResidencia;
        bool bNuevo = true;
        int iIdPersona;
        int iBandera = 1;


        string sTabla;
        string sCampo;
        long iMaximo;
        string sSql;

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public FInformacionPersonas()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = Dns.GetHostName();
        }

        #region FUNCIONES DE AYUDA  

        //FUNCION PARA LLENAR EL GRID TIPO PERSONAS
        private void ComboboxTipoPersona()
        {
            try
            {
                DataTable dtPadre = new DataTable();

                sSql = "select correlativo, valor_texto from tp_codigos where tabla = 'SYS$00056'";

                cmbTipoPersona.llenar(dtPadre, sSql);
                if (cmbTipoPersona.Items.Count > 0)
                {
                    cmbTipoPersona.SelectedIndex = 1;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ocurrió un problema al cargar el combo tipo persona", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //LLENAR COMBO DE TIPO DE PERSONA
        private void FListarTipoPersona()
        {
            try
            {
                dtTipoPersona = new DataTable();
                string sql = "SELECT * FROM cv401_productos where codigo ='2'";

                cmbTipoPersona.llenar(dtTipoPersona, sql);

                if (cmbTipoPersona.Items.Count > 0)
                {
                    cmbTipoPersona.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //LLENAR COMBO DE TIPO DE IDENTIFICACICON
        private void FListarTipoIdentificacion()
        {
            try
            {
                dtTipoIdentificacion = new DataTable();

                string sql = "select correlativo, valor_texto from tp_vw_tipoidentificacion";

                cmbTipoIdentificacion.llenar(dtTipoIdentificacion, sql);

                if (cmbTipoIdentificacion.Items.Count > 0)
                {
                    cmbTipoIdentificacion.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ocurrió un problema al realizar la consulta");
            }
        }

        //LLENAR COMBO DE PAIS DE RESIDENCIA
        private void FListarPaisResidencia()
        {
            try
            {
                dtPaisResidencia = new DataTable();
                sSql = "select correlativo, valor_texto from tp_vw_paisresidencia";
                cmbPaisResidencia.llenar(dtPaisResidencia, sSql);
                cmbPaisResidencia.SelectedValue = obtenerValorCombo("tp_vw_paisresidencia", "P1");
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select codigo as CODIGO, descripcion as DESCRIPCION, estado as ESTADO from tp_personas";
                }

                else
                {
                    t_st_query = "select codigo as CODIGO, descripcion AS DESCRIPCION, estado as ESTADO from tp_personas where codigo LIKE '%' + '" + t_st_datos[1] + "' OR descripcion like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "tp_personas");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    //aqui se llena el grid
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }
        

        //FUNCION PARA LIMPIAR EL FORMULARIO
        private void limpiarTodo(int iBandera)
        {
            Txt_Buscar.Clear();
            if (iBandera != 1)
            { 
                Txt_Identificacion.Clear();
            }
            
            Txt_Codigo_Alterno.Clear();
            Txt_Informacion.Clear();
            Txt_Apellidos.Clear();
            Txt_Nombres.Clear();
            Txt_Contacto_Presentacion.Clear();
            Txt_Contacto_Cobranza.Clear();
            Txt_Mail.Clear();
            Txt_Sitio_Web.Clear();
            dgv_Direcciones.Rows.Clear();
            dgv_Telefonos.Rows.Clear();
            Rdb_Activos.Checked = true;
            Rdb_Todos.Checked = false;
            Chk_Cliente.Checked = false;
            Chk_Proveedor.Checked = false;
            Chk_Contabilidad.Checked = false;
            Chk_Contribuy_Especial.Checked = false;
            bNuevo = true;

            if (iBandera == 2)
            {
                FListarTipoPersona();
                FListarTipoIdentificacion();
            }

            cmbPaisResidencia.SelectedValue = obtenerValorCombo("tp_vw_paisresidencia", "P1");
            //Txt_Buscar.Focus();
            Cmb_Estado.Text = "ACTIVO";
        }

        //CONSULTA DE REGISTRO PROVISIONAL
        public void consultarRegistro(string sIdentificacion)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                string sConsulta = "select id_persona, identificacion, cg_tipo_persona, cg_tipo_identificacion, codigo_alterno, apellidos, "+
                                    "nombres, cliente, proveedor, contacto_empresa, contacto_cobranza,  "+
                                    "cg_pais_residencia, correo_electronico, sitio_web, personanaturalobligadacontabilidad, "+
                                    "contribuyenteespecial, estado from tp_personas where identificacion = '" + sIdentificacion + "'";
                bool bx = false;
                bx = conexion.GFun_Lo_Busca_Registro(dtConsulta, sConsulta);

                if (bx == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        bNuevo = false;
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        Txt_Identificacion.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                        Txt_Buscar.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                        cmbTipoPersona.SelectedValue = dtConsulta.Rows[0].ItemArray[2].ToString();
                        cmbTipoIdentificacion.SelectedValue = dtConsulta.Rows[0].ItemArray[3].ToString();
                        Txt_Codigo_Alterno.Text = dtConsulta.Rows[0].ItemArray[4].ToString();
                        Txt_Apellidos.Text = dtConsulta.Rows[0].ItemArray[5].ToString();
                        Txt_Nombres.Text = dtConsulta.Rows[0].ItemArray[6].ToString();
                        Txt_Informacion.Text = Txt_Apellidos.Text + " " + Txt_Nombres.Text;

                        if (dtConsulta.Rows[0].ItemArray[7].ToString() == "1")
                            Chk_Cliente.Checked = true;
                        else
                            Chk_Cliente.Checked = false;

                        if (dtConsulta.Rows[0].ItemArray[8].ToString() == "1")
                            Chk_Proveedor.Checked = true;
                        else
                            Chk_Proveedor.Checked = false;

                        Txt_Contacto_Presentacion.Text = dtConsulta.Rows[0].ItemArray[9].ToString();
                        Txt_Contacto_Cobranza.Text = dtConsulta.Rows[0].ItemArray[10].ToString();
                        cmbPaisResidencia.SelectedValue = dtConsulta.Rows[0].ItemArray[11].ToString();
                        Txt_Mail.Text = dtConsulta.Rows[0].ItemArray[12].ToString();
                        Txt_Sitio_Web.Text = dtConsulta.Rows[0].ItemArray[13].ToString();

                        if (dtConsulta.Rows[0].ItemArray[14].ToString() == "1")
                            Chk_Contabilidad.Checked = true;
                        else
                            Chk_Contabilidad.Checked = false;

                        if (dtConsulta.Rows[0].ItemArray[15].ToString() == "1")
                            Chk_Contribuy_Especial.Checked = true;
                        else
                            Chk_Contribuy_Especial.Checked = false;

                        if (dtConsulta.Rows[0].ItemArray[16].ToString() == "A")
                            Cmb_Estado.Text = "ACTIVO";
                        else
                            Cmb_Estado.Text = "ELIMINADO";



                       sSql = "select * from tp_telefonos where id_persona = " + iIdPersona + "" +
                       " and estado = 'A'";

                        //1790159752001
                        dtConsulta_Ayuda = new DataTable();
                        dtConsulta_Ayuda.Clear();
                        
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta_Ayuda, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta_Ayuda.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtConsulta_Ayuda.Rows.Count; i++)
                                {
                                    i = dgv_Telefonos.Rows.Add();

                                    cargarCombosTelefonos(1,i);

                                    dgv_Telefonos.Rows[i].Cells[0].Value = tipoTelefono.DisplayMember;
                                    dgv_Telefonos.Rows[i].Cells[1].Value = dtConsulta_Ayuda.Rows[i].ItemArray[3].ToString();
                                    dgv_Telefonos.Rows[i].Cells[2].Value = dtConsulta_Ayuda.Rows[i].ItemArray[4].ToString();
                                    dgv_Telefonos.Rows[i].Cells[3].Value = dtConsulta_Ayuda.Rows[i].ItemArray[5].ToString();
                                    dgv_Telefonos.Rows[i].Cells[4].Value = dtConsulta_Ayuda.Rows[i].ItemArray[6].ToString();
                                    dgv_Telefonos.Rows[i].Cells[5].Value = dtConsulta_Ayuda.Rows[i].ItemArray[7].ToString();
                                    dgv_Telefonos.Rows[i].Cells[6].Value = dtConsulta_Ayuda.Rows[i].ItemArray[8].ToString();
                                    dgv_Telefonos.Rows[i].Cells[7].Value = dtConsulta_Ayuda.Rows[i].ItemArray[9].ToString();
                                    dgv_Telefonos.Rows[i].Cells[8].Value = estadoTelefono.DisplayMember;
                                }
                            }

                        }


                        sSql = "";
                        sSql = "select * from tp_direcciones where id_persona = " + iIdPersona + "" +
                            " and estado = 'A'";

                        DataTable dtConsultaAyuda = new DataTable();
                        dtConsultaAyuda.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsultaAyuda, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsultaAyuda.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtConsultaAyuda.Rows.Count; i++)
                                {

                                    i = dgv_Direcciones.Rows.Add();

                                    cargarCombos(1,i);
                                    dgv_Direcciones.Rows[i].Cells[0].Value = tipo.DisplayMember;
                                    dgv_Direcciones.Rows[i].Cells[1].Value = localidad.DisplayMember;
                                    dgv_Direcciones.Rows[i].Cells[2].Value = dtConsultaAyuda.Rows[i].ItemArray[4].ToString();
                                    dgv_Direcciones.Rows[i].Cells[3].Value = dtConsultaAyuda.Rows[i].ItemArray[5].ToString();
                                    dgv_Direcciones.Rows[i].Cells[4].Value = dtConsultaAyuda.Rows[i].ItemArray[7].ToString();
                                    dgv_Direcciones.Rows[i].Cells[5].Value = dtConsultaAyuda.Rows[i].ItemArray[6].ToString();
                                    dgv_Direcciones.Rows[i].Cells[6].Value = estado.DisplayMember;

                                }
                            }

                        }

                    }
                    else
                    {
                        bNuevo = true;
                        limpiarTodo(1);
                    }
                        
                }
                else
                {
                    FListarTipoPersona();
                    FListarTipoIdentificacion();
                    FListarPaisResidencia();
                    Cmb_Estado.Text = "ACTIVO";
                    cmbTipoPersona.Focus();
                    bNuevo = true;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void FInformacionPersonas_Load(object sender, EventArgs e)
        {
            ComboboxTipoPersona();
            llenarComboIdentificacion();
            Cmb_Estado.Text = "Activo";
            FListarTipoPersona();
            FListarTipoIdentificacion();
            FListarPaisResidencia();
        }

        //LLenar el Combo Identificación
        private void llenarComboIdentificacion()
        {
            try
            {
                DataTable dtPadre = new DataTable();

                sSql = "SELECT correlativo, valor_texto FROM TP_VW_TIPOIDENTIFICACION";
                cmbTipoIdentificacion.llenar(dtPadre, sSql);
                cmbTipoIdentificacion.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                //MessageBox.Show("Ocurrió un problema al cargar el combo identificación", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            limpiarTodo(2);
            bNuevo = true;
        }

        private void Txt_Identificacion_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void Txt_Buscar_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (validarIdentificacion() == true)
            {
                if (comprobarCampos() == false)
                    MessageBox.Show("Por favor, rellene los campos de direcciones", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (comprobarCamposTelefonos() == false)
                    MessageBox.Show("Por favor, rellene los campos de teléfonos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (comprobarCampos() == true && comprobarCamposTelefonos() == true)
                {
                    if (MessageBox.Show("Desea grabar el registro", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (bNuevo == true)
                            guardarNuevoRegistro();
                        else
                            actualizarRegistro();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese una identificación válida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Identificacion.Text = "";
                Txt_Identificacion.Focus();
            }
            

            
        }

        //Función para validar la identificación
        private bool validarIdentificacion()
        { 
            try
            {
                sSql = "select valor_texto, codigo from TP_VW_TIPOIDENTIFICACION where correlativo = "+cmbTipoIdentificacion.SelectedValue;
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        string sValorTexto = dtConsulta.Rows[0].ItemArray[0].ToString();
                        string sCodigo = dtConsulta.Rows[0].ItemArray[1].ToString();

                        //Si la identificación es una cédula
                        if (Txt_Identificacion.Text == "9999999999" || Txt_Identificacion.Text == "9999999999999")
                            return true;
                        else if (sCodigo == "C" && Txt_Identificacion.Text.Length == 10)
                        {
                            ValidarCedula cedula = new ValidarCedula();
                            if (cedula.validarCedulaConsulta(Txt_Identificacion.Text) == "SI")
                                return true;
                            else
                                return false;
                        }
                        else if (sCodigo == "R" && Txt_Identificacion.Text.Length == 13)
                        {
                            if (Txt_Identificacion.Text.Substring(2, 1) == "6")
                            {
                                Clases.ClaseValidarRUC ruc = new Clases.ClaseValidarRUC();
                                if (ruc.validarRucPublico(Txt_Identificacion.Text) == false)
                                    return false;
                                else
                                    return true;
                            }
                            else if (Txt_Identificacion.Text.Substring(2, 1) == "9")
                            {
                                Clases.ClaseValidarRUC ruc = new Clases.ClaseValidarRUC();
                                if (ruc.validarRucPrivado(Txt_Identificacion.Text) == false)
                                    return false;
                                else return true;
                            }
                            else
                            {
                                Clases.ClaseValidarRUC ruc = new Clases.ClaseValidarRUC();
                                if (ruc.validarRucNatural(Txt_Identificacion.Text) == false)
                                    return false;
                                else return true;
                            }
                        }
                        else if (sCodigo != "R" || sCodigo != "C")
                            return true;
                        else
                            return true;   
                    }
                    else
                      return false;
                }     
                else
                {
                    MessageBox.Show("Ocurrió un problema al validar la identificación", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                   
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                return false;
            }
            
        }

        //Funciones para comprobar si estan todos los campos
        private bool comprobarCampos()
        {
            if (dgv_Direcciones.Rows.Count > 0)
            {
                int iBandera = 0;
                for (int i = 0; i < dgv_Direcciones.Rows.Count; i++)
                {
                    if (dgv_Direcciones.Rows[i].Cells[0].Value == "" || dgv_Direcciones.Rows[i].Cells[0].Value == null)
                        iBandera =1;
                    else if (dgv_Direcciones.Rows[i].Cells[1].Value == "" || dgv_Direcciones.Rows[i].Cells[1].Value == null)
                        iBandera = 1;
                    else if (dgv_Direcciones.Rows[i].Cells[2].Value == "" || dgv_Direcciones.Rows[i].Cells[2].Value == null)
                        iBandera = 1;
                    else if (dgv_Direcciones.Rows[i].Cells[3].Value == "" || dgv_Direcciones.Rows[i].Cells[3].Value == null)
                        iBandera =1;
                    else if (dgv_Direcciones.Rows[i].Cells[6].Value == "" || dgv_Direcciones.Rows[i].Cells[6].Value == null)
                        iBandera = 1;
                }
                if (iBandera == 1)
                    return false;
                else
                    return true;

            }
            else
                return true;
            
        }

        //Funciones para comprobar si estan todos los campos
        private bool comprobarCamposTelefonos()
        {
            if (dgv_Telefonos.Rows.Count > 0)
            {
                int iBandera = 0;
                for (int i = 0; i < dgv_Telefonos.Rows.Count; i++)
                {
                    if (dgv_Telefonos.Rows[i].Cells[0].Value == "" || dgv_Telefonos.Rows[i].Cells[0].Value == null)
                        iBandera = 1;
                    else if (dgv_Telefonos.Rows[i].Cells[8].Value == "" || dgv_Telefonos.Rows[i].Cells[8].Value == null)
                        iBandera = 1;
                }
                if (iBandera == 1)
                    return false;
                else
                    return true;

            }
            else
                return true;

        }

        //Función para actualizar un registro
        private void actualizarRegistro()
        {
            try
            {
                int iCliente;
                int iProveedor;
                int iPersonaNatural;
                int iContribuyenteEspecial;

                if (Chk_Cliente.Checked == true)
                {
                    iCliente = 1;
                    iProveedor = 0;
                }
                else if (Chk_Cliente.Checked == false && Chk_Proveedor.Checked == false)
                {
                    iCliente = 0;
                    iProveedor = 0;
                }
                else
                {
                    iCliente = 0;
                    iProveedor = 1;
                }

                if (Chk_Contabilidad.Checked == true)
                {
                    iPersonaNatural = 1;
                    iContribuyenteEspecial = 0;
                }
                else if (Chk_Contabilidad.Checked == false && Chk_Contribuy_Especial.Checked == false)
                {
                    iPersonaNatural = 0;
                    iContribuyenteEspecial = 0;
                }
                else
                {
                    iPersonaNatural = 0;
                    iContribuyenteEspecial = 1;
                }

                string sEstadoPersona = "A";
                if (Cmb_Estado.SelectedIndex == 0)
                    sEstadoPersona = "A";
                else
                    sEstadoPersona = "E";

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "update tp_personas set idempresa = 1, cg_tipo_persona = " + cmbTipoPersona.SelectedValue.ToString() +
                    ", cg_tipo_identificacion = " + cmbTipoIdentificacion.SelectedValue.ToString() + ", " +
                    "identificacion = '" + Txt_Identificacion.Text + "', nombres = '" + Txt_Nombres.Text + "', apellidos = '" + Txt_Apellidos.Text +
                    "', nombre_comercial = null, " +
                    "codigo_alterno = '" + Txt_Codigo_Alterno.Text + "', CG_PAIS_RESIDENCIA = " + cmbPaisResidencia.SelectedValue + ",  " +
                    "contacto_empresa = '" + Txt_Contacto_Presentacion.Text + "', contacto_cobranza = '" + Txt_Contacto_Cobranza.Text +
                    "', correo_electronico = '" + Txt_Mail.Text + "'," +
                    "sitio_web = '" + Txt_Sitio_Web.Text + "', " +
                    "cliente = " + iCliente + ", proveedor = " + iProveedor + ", personanaturalobligadacontabilidad = " + iPersonaNatural +
                    ", contribuyenteespecial= " + iContribuyenteEspecial + ", estado = '"+sEstadoPersona+"' " +
                    "where id_persona = " + iIdPersona + " ";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                    sSql = "update tp_direcciones set estado = 'E' where id_persona = "+iIdPersona;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                      //Si no hay direcciones no hace nada
                    }

                    if (dgv_Direcciones.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgv_Direcciones.Rows.Count; i++)
                        {
                            dtConsulta = new DataTable();
                            dtConsulta.Clear();
                            int iIdTipoEstablecimiento = 0;
                            int iIdLocalidad = 0;
                            bool bRespuesta; 

                            sSql = "select descripcion, idtipoestablecimiento from sistipoestablecimiento where estado = 'A' " +
                                   " and descripcion ='" + dgv_Direcciones.Rows[i].Cells[0].Value.ToString() + "'";
                            dtConsulta = new DataTable();
                            dtConsulta.Clear();

                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                            if (bRespuesta == true)
                            {
                                iIdTipoEstablecimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                            }

                            sSql = "select valor_texto, correlativo from tp_codigos where tabla = 'SYS$00005' " +
                                    "and estado = 'A' and valor_texto = '" + dgv_Direcciones.Rows[i].Cells[1].Value.ToString() + "'";
                            dtConsulta = new DataTable();
                            dtConsulta.Clear();

                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                            if (bRespuesta == true)
                            {
                                iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                            }

                            if (dgv_Direcciones.Rows[i].Cells[2].Value == null || dgv_Direcciones.Rows[i].Cells[2].Value == " ")
                            {
                                dgv_Direcciones.Rows[i].Cells[2].Value = "";
                            }
                            if (dgv_Direcciones.Rows[i].Cells[3].Value == null || dgv_Direcciones.Rows[i].Cells[3].Value == " ")
                            {
                                dgv_Direcciones.Rows[i].Cells[3].Value = "";
                            }
                            if (dgv_Direcciones.Rows[i].Cells[4].Value == null || dgv_Direcciones.Rows[i].Cells[4].Value == " ")
                            {
                                dgv_Direcciones.Rows[i].Cells[4].Value = "";
                            }
                            if (dgv_Direcciones.Rows[i].Cells[5].Value == null || dgv_Direcciones.Rows[i].Cells[5].Value == " ")
                            {
                                dgv_Direcciones.Rows[i].Cells[5].Value = "";
                            }

                            string sEstado = "A";

                            if (dgv_Direcciones.Rows[i].Cells[6].Value.ToString() == "Activo")
                                sEstado = "A";
                            else
                                sEstado = "E";

                            sSql = "Insert into tp_direcciones (Id_Persona,idTipoEstablecimiento,Cg_Localidad,Direccion,calle_principal, " +
                                "numero_vivienda,calle_interseccion,Estado,usuario_ingreso, terminal_ingreso, " +
                                "fecha_ingreso,numero_replica_trigger, numero_control_replica ) " +
                                "values (" + iIdPersona + "," + iIdTipoEstablecimiento + "," + iIdLocalidad + ", " +
                                "'" + dgv_Direcciones.Rows[i].Cells[2].Value.ToString() + "','" + dgv_Direcciones.Rows[i].Cells[3].Value.ToString() + "'," +
                                "'" + dgv_Direcciones.Rows[i].Cells[4].Value.ToString() + "','" + dgv_Direcciones.Rows[i].Cells[5].Value.ToString() + "', " +
                                "'" + sEstado + "', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "',  getdate(),0,0)";

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowInTaskbar = false;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }

                        }
                    }


                    sSql = "update tp_telefonos set estado = 'E' where id_persona = " + iIdPersona;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //Si no hay teléfonos no hace nada
                    }

                    if (dgv_Telefonos.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgv_Telefonos.Rows.Count; i++)
                        {
                            int iIdTipoEstablecimiento = 0;
                            sSql = "select descripcion, idtipoestablecimiento from sistipoestablecimiento where estado = 'A' " +
                                   " and descripcion ='" + dgv_Telefonos.Rows[i].Cells[0].Value.ToString() + "'";
                            dtConsulta = new DataTable();

                            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                            if (bRespuesta == true)
                            {
                                iIdTipoEstablecimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                            }

                            if (dgv_Telefonos.Rows[i].Cells[1].Value == null || dgv_Telefonos.Rows[i].Cells[1].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[1].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[2].Value == null || dgv_Telefonos.Rows[i].Cells[2].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[2].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[3].Value == null || dgv_Telefonos.Rows[i].Cells[3].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[3].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[4].Value == null || dgv_Telefonos.Rows[i].Cells[4].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[4].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[5].Value == null || dgv_Telefonos.Rows[i].Cells[5].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[5].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[6].Value == null || dgv_Telefonos.Rows[i].Cells[6].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[6].Value = "";
                            }
                            if (dgv_Telefonos.Rows[i].Cells[7].Value == null || dgv_Telefonos.Rows[i].Cells[7].Value == " ")
                            {
                                dgv_Telefonos.Rows[i].Cells[7].Value = "";
                            }

                            string sEstadoTelefono = "A";

                            if (dgv_Telefonos.Rows[i].Cells[8].Value.ToString() == "Activo")
                                sEstadoTelefono = "A";
                            else
                                sEstadoTelefono = "E";


                            sSql = "Insert into tp_telefonos (Id_Persona,idTipoEstablecimiento,Codigo_Area, Oficina, " +
                                "Celular, Domicilio, Fax, Adicional1, Adicional2, Estado, fecha_ingreso, usuario_ingreso, terminal_ingreso, " +
                            "numero_replica_trigger, numero_control_replica) " +
                            "values (" + iIdPersona + "," + iIdTipoEstablecimiento + ",'" + dgv_Telefonos.Rows[i].Cells[1].Value.ToString() +
                            "','" + dgv_Telefonos.Rows[i].Cells[2].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[3].Value.ToString() +
                            "','" + dgv_Telefonos.Rows[i].Cells[4].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[5].Value.ToString() +
                            "','" + dgv_Telefonos.Rows[i].Cells[6].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[7].Value.ToString() +
                            "','" + sEstadoTelefono + "', getdate(),'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "',0,0)";

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowInTaskbar = false;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }

                        }

                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.LblMensaje.Text = "Registro Actualizado Correctamente";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    limpiarTodo(0);
                    goto fin;


            }
            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            

            #region Funciones de Ayuda
        reversa:
            {

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                limpiarTodo(0);
            }

        fin:
            {
                
            }
            #endregion

        }

        //Función para guardar un nuevo registro
        private void guardarNuevoRegistro()
        {
            try
            {
                int iIdPersona = 0;
                int iCliente;
                int iProveedor;
                int iPersonaNatural;
                int iContribuyenteEspecial;

                if (Chk_Cliente.Checked == true)
                {
                    iCliente = 1;
                    iProveedor = 0;
                }
                else if (Chk_Cliente.Checked == false && Chk_Proveedor.Checked == false)
                {
                    iCliente = 0;
                    iProveedor = 0;
                }
                else
                {
                    iCliente = 0;
                    iProveedor = 1;
                }

                if (Chk_Contabilidad.Checked == true)
                {
                    iPersonaNatural = 1;
                    iContribuyenteEspecial = 0;
                }
                else if (Chk_Contabilidad.Checked == false && Chk_Contribuy_Especial.Checked == false)
                {
                    iPersonaNatural = 0;
                    iContribuyenteEspecial = 0;
                }
                else
                {
                    iPersonaNatural = 0;
                    iContribuyenteEspecial = 1;
                }

                string sEstadoPersona = "A";
                if (Cmb_Estado.SelectedIndex == 0)
                    sEstadoPersona = "A";
                else
                    sEstadoPersona = "E";


                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }


                sSql = "Insert into tp_personas (idempresa,cg_tipo_persona,cg_tipo_identificacion,identificacion "+
                    ",nombres,apellidos,codigo_alterno, CG_PAIS_RESIDENCIA,contacto_empresa,contacto_cobranza ,"+
                    "correo_electronico,sitio_web,Cliente,proveedor,personanaturalobligadacontabilidad,  "+
                    "contribuyenteespecial,porcentaje_descuento,hacerlaretencionfuenteir,contador,usuario_ingreso,terminal_ingreso, "+
                    "fecha_ingreso,estado,numero_replica_trigger, numero_control_replica ) "+
                    "values (1,"+cmbTipoPersona.SelectedValue.ToString()+","+cmbTipoIdentificacion.SelectedValue.ToString()+
                    ",'"+Txt_Identificacion.Text+"','"+Txt_Nombres.Text+"','"+Txt_Apellidos.Text+"','"+Txt_Codigo_Alterno.Text+
                    "',"+cmbPaisResidencia.SelectedValue+",'"+Txt_Contacto_Presentacion.Text+"','"+Txt_Contacto_Cobranza.Text+
                    "','"+Txt_Mail.Text+"', "+ 
                    "'"+Txt_Sitio_Web.Text+"',"+iCliente+","+iProveedor+","+iPersonaNatural+","+iContribuyenteEspecial+
                    ",0,0,0,'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "',getdate(),'" + sEstadoPersona + "',0,0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (dgv_Direcciones.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_Direcciones.Rows.Count; i++)
                    {
                        //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA TP_PERSONAS
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "tp_personas";
                        sCampo = "Id_persona";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowInTaskbar = false;
                            ok.ShowDialog();
                            goto reversa;
                        }

                        else
                        {
                            iIdPersona = Convert.ToInt32(iMaximo);
                        }

                        int iIdTipoEstablecimiento = 0;
                        int iIdLocalidad = 0;

                        sSql = "select descripcion, idtipoestablecimiento from sistipoestablecimiento where estado = 'A' " +
                               " and descripcion ='" + dgv_Direcciones.Rows[i].Cells[0].Value.ToString() + "'";
                        dtConsulta = new DataTable();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                        if (bRespuesta == true)
                        {
                            iIdTipoEstablecimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }

                        sSql = "select valor_texto, correlativo from tp_codigos where tabla = 'SYS$00005' " +
                                "and estado = 'A' and valor_texto = '" + dgv_Direcciones.Rows[i].Cells[1].Value.ToString() + "'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                        if (bRespuesta == true)
                        {
                            iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }

                        if (dgv_Direcciones.Rows[i].Cells[2].Value == null || dgv_Direcciones.Rows[i].Cells[2].Value.ToString() == " ")
                        {
                            dgv_Direcciones.Rows[i].Cells[2].Value = "";
                        }
                        if (dgv_Direcciones.Rows[i].Cells[3].Value == null || dgv_Direcciones.Rows[i].Cells[3].Value.ToString() == " ")
                        {
                            dgv_Direcciones.Rows[i].Cells[3].Value = "";
                        }
                        if (dgv_Direcciones.Rows[i].Cells[4].Value == null || dgv_Direcciones.Rows[i].Cells[4].Value.ToString() == " ")
                        {
                            dgv_Direcciones.Rows[i].Cells[4].Value = "";
                        }
                        if (dgv_Direcciones.Rows[i].Cells[5].Value == null || dgv_Direcciones.Rows[i].Cells[5].Value.ToString() == " ")
                        {
                            dgv_Direcciones.Rows[i].Cells[5].Value = "";
                        }
                       
                        string sEstado = "A";

                        if (dgv_Direcciones.Rows[i].Cells[6].Value.ToString() == "Activo")
                            sEstado = "A";
                        else
                            sEstado = "E";
                       
                        sSql = "Insert into tp_direcciones (Id_Persona,idTipoEstablecimiento,Cg_Localidad,Direccion,calle_principal, " +
                            "numero_vivienda,calle_interseccion,Estado,usuario_ingreso, terminal_ingreso, " +
                            "fecha_ingreso,numero_replica_trigger, numero_control_replica ) " +
                            "values (" + iIdPersona + "," + iIdTipoEstablecimiento + "," + iIdLocalidad + ", "+
                            "'" + dgv_Direcciones.Rows[i].Cells[2].Value.ToString() + "','" + dgv_Direcciones.Rows[i].Cells[3].Value.ToString() + "'," +
                            "'" + dgv_Direcciones.Rows[i].Cells[4].Value.ToString() + "','" + dgv_Direcciones.Rows[i].Cells[5].Value.ToString() + "', " +
                            "'" + sEstado + "', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "',  getdate(),0,0)";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowInTaskbar = false;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                    }
                        
                }


                if (dgv_Telefonos.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_Telefonos.Rows.Count; i++)
                    {
                        int iIdTipoEstablecimiento = 0;
                        sSql = "select descripcion, idtipoestablecimiento from sistipoestablecimiento where estado = 'A' " +
                               " and descripcion ='" + dgv_Telefonos.Rows[i].Cells[0].Value.ToString() + "'";
                        dtConsulta = new DataTable();

                        bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                        if (bRespuesta == true)
                        {
                            iIdTipoEstablecimiento = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        }

                        if (dgv_Telefonos.Rows[i].Cells[1].Value == null || dgv_Telefonos.Rows[i].Cells[1].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[1].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[2].Value == null || dgv_Telefonos.Rows[i].Cells[2].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[2].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[3].Value == null || dgv_Telefonos.Rows[i].Cells[3].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[3].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[4].Value == null || dgv_Telefonos.Rows[i].Cells[4].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[4].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[5].Value == null || dgv_Telefonos.Rows[i].Cells[5].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[5].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[6].Value == null || dgv_Telefonos.Rows[i].Cells[6].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[6].Value = "";
                        }
                        if (dgv_Telefonos.Rows[i].Cells[7].Value == null || dgv_Telefonos.Rows[i].Cells[7].Value == " ")
                        {
                            dgv_Telefonos.Rows[i].Cells[7].Value = "";
                        }
                        string sEstadoTelefono = "A";

                        if (dgv_Telefonos.Rows[i].Cells[8].Value.ToString() == "Activo")
                            sEstadoTelefono = "A";
                        else
                            sEstadoTelefono = "E";

                        sSql = "Insert into tp_telefonos (Id_Persona,idTipoEstablecimiento,Codigo_Area, Oficina, "+
                            "Celular, Domicilio, Fax, Adicional1, Adicional2, Estado, fecha_ingreso, usuario_ingreso, terminal_ingreso, "+
                        "numero_replica_trigger, numero_control_replica) "+
                        "values ("+iIdPersona+","+iIdTipoEstablecimiento+",'"+dgv_Telefonos.Rows[i].Cells[1].Value.ToString()+
                        "','" + dgv_Telefonos.Rows[i].Cells[2].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[3].Value.ToString() +
                        "','" + dgv_Telefonos.Rows[i].Cells[4].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[5].Value.ToString() +
                        "','" + dgv_Telefonos.Rows[i].Cells[6].Value.ToString() + "','" + dgv_Telefonos.Rows[i].Cells[7].Value.ToString() +
                        "','" + sEstadoTelefono + "', getdate(),'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "',0,0)";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowInTaskbar = false;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                    }

                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text  = "Registro ingresado correctamente";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                limpiarTodo(0);

                goto fin;

            }
            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            #region Funciones de Ayuda
        reversa:
            {
                //MessageBox.Show("Ocurrió un problema al guardar el registro", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
            #endregion


        }

        private void Txt_Codigo_Alterno_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void Txt_Identificacion_Leave(object sender, EventArgs e)
        {
           // Txt_Identificacion.LostFocus += new EventHandler(Txt_Identificacion_LostFocus);
            Txt_Identificacion_LostFocus(sender, e);
        }

        private void Txt_Identificacion_LostFocus(object sender, EventArgs e)
        {
            dgv_Direcciones.Rows.Clear();
            dgv_Telefonos.Rows.Clear();
            consultarRegistro(Txt_Identificacion.Text.Trim());
        }

        private void Cmb_Seguimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Abrir_Grid_Click(object sender, EventArgs e)
        {
            limpiarTodo(0);
            FAyuda FAyuda = new FAyuda();
            FAyuda.ShowDialog();

            if (FAyuda.DialogResult == DialogResult.OK)
            {
                string sIdentificacion = FAyuda.sIdentificacion;
                Txt_Informacion.Text = FAyuda.sNombre;
                consultarRegistro(sIdentificacion);
                bNuevo = false;
                //iBandera = 1;
            }
        }

        private void Txt_Buscar_Leave(object sender, EventArgs e)
        {
            Txt_Buscar_LostFocus(sender, e);
        }

        private void Txt_Buscar_LostFocus(object sender, EventArgs e)
        {
            dgv_Direcciones.Rows.Clear();
            dgv_Telefonos.Rows.Clear();
            consultarRegistro(Txt_Buscar.Text.Trim());
        }

        private void Btn_Eliminar_Direccion_Click(object sender, EventArgs e)
        {
            cargarCombos(0, 0);
            dgv_Direcciones.Rows.Add(tipo.DisplayMember,localidad.DisplayMember,"","","","",estado.DisplayMember);
        }

        private void Btn_Agregar_Direccion_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_Direcciones.Rows.RemoveAt((dgv_Direcciones.Rows.Count - 1));
            }
            catch (Exception)
            {
                MessageBox.Show("No hay direcciones para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        //Función para cargar los combos de dgvDirecciones
        private void cargarCombos(int iBandera, int iPosicion)
        {
            try
            {
                sSql = "select valor_texto, correlativo from tp_codigos where tabla = 'SYS$00005' "+
                            "and estado = 'A'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                
                localidad.Items.Clear();
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    localidad.Items.Add(dtConsulta.Rows[i].ItemArray[0].ToString());
                }

                localidad.DisplayMember = obtenerValor("tp_codigos", "GYE.");

                #region Recuperar los combos Localidades DIrecciones
                if (iBandera == 1)
                {
                    string sSqlLocalidad = "select cg_localidad, idtipoestablecimiento from tp_direcciones " +
                                           "where id_persona = " + iIdPersona + " and estado = 'A'";

                    DataTable data = new DataTable();
                    data.Clear();
                    bool bAyuda = conexion.GFun_Lo_Busca_Registro(data, sSqlLocalidad);

                    if (bRespuesta == true)
                    {
                        int cg_localidad =  Convert.ToInt32(data.Rows[iPosicion].ItemArray[0].ToString());
                        string sQuery = "select valor_texto, correlativo from tp_codigos where tabla = 'SYS$00005' "+
                                            "and estado = 'A' and correlativo = "+cg_localidad+"";

                        DataTable dt = new DataTable();
                        dt.Clear();
                        bool bCorrelativo = conexion.GFun_Lo_Busca_Registro(dt,sQuery);

                        if (bCorrelativo == true)
                        {
                            localidad.DisplayMember = dt.Rows[0].ItemArray[0].ToString();
                        }

                    }

                }
                #endregion

            }



            sSql = "select descripcion, idtipoestablecimiento from sistipoestablecimiento where estado = 'A'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                tipo.Items.Clear();
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    tipo.Items.Add(dtConsulta.Rows[i].ItemArray[0].ToString());
                }

                tipo.DisplayMember = obtenerEstablecimiento("001");

                #region Recuperar los combos Tipo establecimiento direcciones
                if (iBandera == 1)
                {
                    string sSqlLocalidad = "select cg_localidad, idtipoestablecimiento from tp_direcciones " +
                                           "where id_persona = " + iIdPersona + " and estado = 'A'";

                    DataTable data = new DataTable();
                    data.Clear();
                    bool bAyuda = conexion.GFun_Lo_Busca_Registro(data, sSqlLocalidad);

                    if (bRespuesta == true)
                    {
                        int cg_localidad = Convert.ToInt32(data.Rows[iPosicion].ItemArray[1].ToString());
                        string sQuery = @" select descripcion, idtipoestablecimiento from sistipoestablecimiento
                                        where estado = 'A' and idtipoestablecimiento = "+cg_localidad+"";

                        DataTable dt = new DataTable();
                        dt.Clear();
                        bool bCorrelativo = conexion.GFun_Lo_Busca_Registro(dt, sQuery);

                        if (bCorrelativo == true)
                        {
                            tipo.DisplayMember = dt.Rows[0].ItemArray[0].ToString();
                        }

                    }

                }
                #endregion
            }

            #region Recuperar Estado
            estado.Items.Clear();
            estado.Items.Add("Activo");
            estado.Items.Add("Cerrado");
            estado.Items.Add("Eliminado");
            estado.Items.Add("Aprobado");
            estado.Items.Add("Modificado");
            estado.Items.Add("Pendiente");
            estado.Items.Add("Reactivado");
            estado.Items.Add("Suspendido");
            estado.Items.Add("En revisión");
            estado.Items.Add("Mayorizado");

            estado.DisplayMember = "Activo";
            #endregion

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            

        }

        //Función para cargar los datos en los combos del grid
        private string obtenerValor(string sNombreTabla, string sCodigo)
        {
            try
            {
                sSql = "select valor_texto from "+sNombreTabla+" where tabla = 'SYS$00005' and estado = 'A' and codigo = '"
                                +sCodigo+"'";
                DataTable dtAyuda = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
                if (bRespuesta == true)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
                else
                    return "Guayaquil";
            }
            catch(Exception exc)
            {
                return "Guayaquil";
            }
        }

        //Función para cargar los datos en los combos del grid
        private string obtenerEstablecimiento(string sCodigo)
        {
            try
            {
                sSql = "select descripcion from sistipoestablecimiento "+
                            "where estado = 'A' and codigo = '"+sCodigo+"'";

                DataTable dtAyuda = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
                if (bRespuesta == true)
                    return dtAyuda.Rows[0].ItemArray[0].ToString();
                else
                    return "Matriz";
            }
            catch (Exception exc)
            {
                return "Matriz";
            }
        }

        private void dgv_Direcciones_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
     
                if (e.Control is TextBox)
                {

                    if (dgv_Direcciones.CurrentCell.ColumnIndex !=0 || dgv_Direcciones.CurrentCell.ColumnIndex != 1)

                        ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;

                    else

                        ((TextBox)e.Control).CharacterCasing = CharacterCasing.Normal;
                }

        }

        private void Btn_Eliminar_Telefono_Click(object sender, EventArgs e)
        {
            cargarCombosTelefonos(0, 0);
            dgv_Telefonos.Rows.Add(tipoTelefono.DisplayMember,"","","","","","","",estadoTelefono.DisplayMember);
            
        }

        //Función para cargar el combo de teléfono
        private void cargarCombosTelefonos(int iBandera, int iPosicion)
        {
            sSql = " select descripcion from sistipoestablecimiento where estado = 'A'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                tipoTelefono.Items.Clear();

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    tipoTelefono.Items.Add(dtConsulta.Rows[i].ItemArray[0].ToString());
                }

                tipoTelefono.DisplayMember = obtenerEstablecimiento("001");


                #region Recuperar los combos tipo establecimiento teléfonos
                if (iBandera == 1)
                {
                    string sSqlLocalidad = "select correlativo, idtipoestablecimiento from tp_telefonos " +
                                           "where id_persona = " + iIdPersona + " and estado = 'A'";

                    DataTable data = new DataTable();
                    data.Clear();
                    bool bAyuda = conexion.GFun_Lo_Busca_Registro(data, sSqlLocalidad);

                    if (bRespuesta == true)
                    {
                        int idTipoEstablecimiento = Convert.ToInt32(data.Rows[iPosicion].ItemArray[1].ToString());
                        string sQuery = @" select descripcion, idtipoestablecimiento from sistipoestablecimiento
                                        where estado = 'A' and idtipoestablecimiento = " + idTipoEstablecimiento + "";

                        DataTable dt = new DataTable();
                        dt.Clear();
                        bool bCorrelativo = conexion.GFun_Lo_Busca_Registro(dt, sQuery);

                        if (bCorrelativo == true)
                        {
                            tipoTelefono.DisplayMember = dt.Rows[0].ItemArray[0].ToString();
                        }

                    }

                }
                #endregion

                
            }

            estadoTelefono.Items.Clear();
            estadoTelefono.Items.Add("Activo");
            estadoTelefono.Items.Add("Cerrado");
            estadoTelefono.Items.Add("Eliminado");
            estadoTelefono.Items.Add("Aprobado");
            estadoTelefono.Items.Add("Modificado");
            estadoTelefono.Items.Add("Pendiente");
            estadoTelefono.Items.Add("Reactivado");
            estadoTelefono.Items.Add("Suspendido");
            estadoTelefono.Items.Add("En revisión");
            estadoTelefono.Items.Add("Mayorizado");
            estadoTelefono.DisplayMember = "Activo";
        }

        private void Btn_Agregar_Telefono_Click(object sender, EventArgs e)
        {
            try 
            {
                dgv_Telefonos.Rows.RemoveAt((dgv_Telefonos.Rows.Count - 1));
            }
            catch(Exception)
            {
                MessageBox.Show("No hay filas para ser eliminadas","Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void dgv_Telefonos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {

                if (dgv_Telefonos.CurrentCell.ColumnIndex != 0 || dgv_Telefonos.CurrentCell.ColumnIndex != 1)

                    ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;

                else

                    ((TextBox)e.Control).CharacterCasing = CharacterCasing.Normal;

            }
        }

        //Funcion para poner los combos en un valor determindado
        private int obtenerValorCombo(string sNombreTabla, string sCodigo)
        {
            string sQuery = "select * from " + sNombreTabla + " where codigo = '" + sCodigo + "'";
            DataTable dtAyuda = new DataTable();
            if (conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery) == true)
                return Convert.ToInt32(dtAyuda.Rows[0].ItemArray[2].ToString());
            else return 1;
        }

        private void FInformacionPersonas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
