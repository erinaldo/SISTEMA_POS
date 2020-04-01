using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Personal
{
    public partial class frmCajeros : MaterialForm
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        bool bRespuesta = false;
        
        DataTable dtConsulta;
        
        string sSql;
        string sEstado;

        int iIdPersona;
        int iIdCajero;
        int iPermisos;
        int iCuenta;
        int iCuentaMeseros;
        int iCuentaCajeros;
        int iHabilitado;

        public frmCajeros()
        {
            InitializeComponent();
        }

        private void FInformacionCajero_Load(object sender, EventArgs e)
        {
            llenarGrid();
            llenarSentencia();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion, apellidos + ' ' + isnull(nombres, '') nombres " + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (Program.iManejaNomina == 1)
                {
                    sSql += "and estaenroldepagos = 1" + Environment.NewLine;
                }

                dbAyudaPersonal.Ver(sSql, "identificacion", 0, 1, 2);
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            dbAyudaPersonal.limpiar();
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtClaveAcceso.Clear();
            txtCodigo.Enabled = true;
            chkContrasena.Checked = false;
            chkPermisos.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            iIdCajero = 0;
            iIdPersona = 0;
            iCuenta = 0;
            iHabilitado = 0;
            llenarGrid();
        }

        //Función para comprobar si hay un código repetido
        private int comprobarCodigo()
        {
            int iBandera = 0;
            for (int i = 0; i < dgvCajero.Rows.Count; i++)
            {
                if (txtCodigo.Text == dgvCajero.Rows[i].Cells[2].Value.ToString())
                {
                    iBandera = 1;
                    break;
                }
            }

            return iBandera;
        }

        //Función para ver si un registro ya está siendo utilizado
        private bool comprobarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_cajero = " + iIdCajero + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA COMPROBAR LA CLAVE INGRESADA PARA EVITAR DUPLICADOS
        private int devolverConsultaPasswordCajero()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where claveacceso = '" + txtClaveAcceso.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and id_pos_cajero <> " + iIdCajero;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA COMPROBAR LA CLAVE INGRESADA PARA EVITAR DUPLICADOS
        private int devolverConsultaPasswordMesero()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_mesero" + Environment.NewLine;
                sSql += "where claveacceso = '" + txtClaveAcceso.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvCajero.Rows.Clear();

                sSql = "";
                sSql += "select id_pos_cajero, isnull(PER.id_persona,0) id_persona, CAJ.codigo as Código," + Environment.NewLine;
                sSql += "CAJ.descripcion as Descripcion, isnull(CAJ.claveacceso, 0) as Clave_Acceso," + Environment.NewLine;
                //sSql += "case CAJ.estado when 'A' then 'ACTIVO' else 'INACTIVO' end Estado," + Environment.NewLine;
                sSql += "case CAJ.is_active when 1 then 'ACTIVO' else 'INACTIVO' end Estado," + Environment.NewLine;
                sSql += "isnull(PER.identificacion,' ') identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(PER.nombres, '') + ' ' + PER.apellidos) 'Nombre del Cajero'," + Environment.NewLine;
                sSql += "CAJ.administracion, isnull(CAJ.is_active, 0) is_active" + Environment.NewLine;
                sSql += "from tp_personas PER inner join" + Environment.NewLine;
                sSql += "pos_cajero CAJ on CAJ.id_persona = PER.id_persona" + Environment.NewLine;
                sSql += "and CAJ.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and PER.estado = 'A'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and CAJ.descripcion like '%" + txtBuscar.Text.Trim() + "%'";
                }

                sSql += "order by CAJ.codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {

                    if (dtConsulta.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            int iIdCajero = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                            int iIdPersona = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                            string sCodigo = dtConsulta.Rows[i][2].ToString();
                            string sDescripcion = dtConsulta.Rows[i][3].ToString();
                            int iClaveAcceso = Convert.ToInt32(dtConsulta.Rows[i][4].ToString());
                            string sEstado = dtConsulta.Rows[i][5].ToString();
                            string sIdentificacion = dtConsulta.Rows[i][6].ToString();
                            string sNombre = dtConsulta.Rows[i][7].ToString().Trim();
                            iPermisos = Convert.ToInt32(dtConsulta.Rows[i][8].ToString());
                            iHabilitado = Convert.ToInt32(dtConsulta.Rows[i]["is_active"].ToString());

                            dgvCajero.Rows.Add(iIdCajero, iIdPersona, sCodigo, sDescripcion, 
                                               iClaveAcceso, sEstado, sIdentificacion,
                                               sNombre, iPermisos, iHabilitado);
                        }
                    }

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
        }

        //FUNCION PARA INSERTAR EL REGISTRO
        private void insertarRegistro()
        {
            try
            {
                iCuentaCajeros = devolverConsultaPasswordCajero();

                if (iCuentaCajeros == -1)
                {
                    txtClaveAcceso.Focus();
                    return;
                }

                if (iCuentaCajeros > 0)
                {
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    txtClaveAcceso.Focus();
                    return;
                }

                iCuentaMeseros = devolverConsultaPasswordMesero();

                if (iCuentaMeseros == -1)
                {
                    return;
                }

                if (iCuentaMeseros > 0)
                {
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    return;
                }

                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_cajero (" + Environment.NewLine;
                sSql += "id_persona, codigo, descripcion, claveacceso, administracion, is_active," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPersona + ", '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + txtDescripcion.Text.Trim().ToUpper() + "', '" + txtClaveAcceso.Text.Trim() + "'," + Environment.NewLine;
                sSql += iPermisos + ", 1, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                
                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado éxitosamente";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
                return;

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }


        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                iCuentaCajeros = devolverConsultaPasswordCajero();

                if (iCuentaCajeros == -1)
                {
                    txtClaveAcceso.Focus();
                    return;
                }

                if (iCuentaCajeros > 0)
                {
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    txtClaveAcceso.Focus();
                    return;
                }

                iCuentaMeseros = devolverConsultaPasswordMesero();

                if (iCuentaMeseros == -1)
                {
                    return;
                }

                if (iCuentaMeseros > 0)
                {
                    ok.lblMensaje.Text = "La clave ingresada ya está asignada para usuario";
                    ok.ShowDialog();
                    return;
                }

                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim() + "'," + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "Claveacceso = '" + txtClaveAcceso.Text + "'," + Environment.NewLine;
                sSql += "administracion = " + iPermisos + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_cajero = " + iIdCajero;
                sSql += "and estado in ('A', 'N')";

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
                return;

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "is_active = 0" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                //sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                //sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_cajero = " + iIdCajero;

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                Grb_DatoCajero.Enabled = false;
                limpiarTodo();
                return;

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void chkContrasena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContrasena.Checked == true)
            {
                txtClaveAcceso.PasswordChar = '\0';
                txtClaveAcceso.Focus();
            }
            else
            {
                txtClaveAcceso.PasswordChar = '*';
                txtClaveAcceso.Focus();
            }

            txtClaveAcceso.SelectionStart = txtClaveAcceso.Text.Trim().Length;
        }

        private void dgvCajero_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Grb_DatoCajero.Enabled = true;
                txtCodigo.Enabled = false;
                btnNuevo.Text = "Actualizar";

                iIdCajero = Convert.ToInt32(dgvCajero.CurrentRow.Cells[0].Value.ToString());
                iIdPersona = Convert.ToInt32(dgvCajero.CurrentRow.Cells[1].Value.ToString());
                dbAyudaPersonal.iId = Convert.ToInt32(dgvCajero.CurrentRow.Cells[1].Value.ToString());
                txtCodigo.Text = dgvCajero.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvCajero.CurrentRow.Cells[3].Value.ToString();
                txtClaveAcceso.Text = dgvCajero.CurrentRow.Cells[4].Value.ToString();
                dbAyudaPersonal.txtIdentificacion.Text = dgvCajero.CurrentRow.Cells[6].Value.ToString();
                dbAyudaPersonal.txtDatos.Text = dgvCajero.CurrentRow.Cells[7].Value.ToString();

                iPermisos = Convert.ToInt32(dgvCajero.CurrentRow.Cells[8].Value.ToString());

                if (iPermisos == 0)
                {
                    chkPermisos.Checked = false;
                }

                else
                {
                    chkPermisos.Checked = true;
                }

                iHabilitado = Convert.ToInt32(dgvCajero.CurrentRow.Cells[9].Value.ToString());

                if (iHabilitado == 0)
                {
                    chkHabilitado.Checked = false;
                }

                else
                {
                    chkHabilitado.Checked = true;
                }

                chkHabilitado.Enabled = true;
                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Grb_DatoCajero.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            llenarGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                Grb_DatoCajero.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
            }

            else
            {

                if (txtCodigo.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código del cajero.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la descripción del cajero.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (dbAyudaPersonal.iId == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione los datos de la persona.";
                    ok.ShowDialog();
                    dbAyudaPersonal.Focus();
                }

                else if (txtClaveAcceso.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la la clave de acceso para el cajero.";
                    ok.ShowDialog();
                    txtClaveAcceso.Focus();
                }

                else
                {
                    if (chkPermisos.Checked == true)
                    {
                        iPermisos = 1;
                    }

                    else
                    {
                        iPermisos = 0;
                    }

                    if (chkHabilitado.Checked == true)
                    {
                        iHabilitado = 1;
                    }

                    else
                    {
                        iHabilitado = 0;
                    }

                    iIdPersona = dbAyudaPersonal.iId;

                    if (btnNuevo.Text == "Guardar")
                    {
                        iCuenta = comprobarCodigo();

                        if (iCuenta == 0)
                        {
                            insertarRegistro();
                        }

                        else if (iCuenta > 1)
                        {
                            ok.lblMensaje.Text = "Ya existe un registro con el código ingresado.";
                            ok.ShowDialog();
                            txtCodigo.Clear();
                            txtCodigo.Focus();
                        }
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        actualizarRegistro();
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdCajero == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado un registro para verificar la eliminación.";
                ok.ShowDialog();
                return;
            }

            SiNo.lblMensaje.Text = "Esta seguro que desea dar de baja el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
