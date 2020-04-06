using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using MaterialSkin.Controls;

namespace Palatium.Oficina
{
    public partial class frmTerminales : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        DataTable dtConsulta;
        string sSql;
        string sFecha;
        string sEstado;
        bool bRespuesta;
        int iIdTerminal;
        int iVistaPrograma;
        int iHabilitado;

        public frmTerminales()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
        {
            try
            {
                dtConsulta = new DataTable();
                sSql = "select id_localidad, nombre_localidad from tp_vw_localidades";
                cmbLocalidad.llenar(dtConsulta, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LIMPIAR TODO EL FORMULARIO
        private void limpiarTodo()
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtNombreEquipo.Clear();
            TxtIPAsignada.Clear();
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            btnNuevo.Text = "Nuevo";
            btnEliminar.Enabled = false;
            rdbVistaComandera.Checked = true;
            rdbPantallaEmpresa.Checked = false;

            llenarComboLocalidad();

            grupoDatos.Enabled = false;
            llenarGrid();
        }

        //LIMPIAR SOLO LAS CAJAS DE TEXTO
        private void limpiarCajasTexto()
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtNombreEquipo.Clear();
            TxtIPAsignada.Clear();
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            btnNuevo.Text = "Nuevo";
            btnEliminar.Enabled = false;
            llenarComboLocalidad();
        }

        //EXTRAER LA IP DEL EQUIPO
        private void recuperarIP()
        {
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    TxtIPAsignada.Text = ip.ToString();
                }
            }
        }

        //INSERTAR UN REGISTRO
        private void insertarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_terminal where (codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                sSql += "or nombre_maquina = '" + txtNombreEquipo.Text.Trim() + "'";
                sSql += "or ip_maquina = '" + TxtIPAsignada.Text.Trim() + "')";
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ya existe un registro con el codigo o datos ingresados para el equipo " + dtConsulta.Rows[0].ItemArray[3].ToString();
                    ok.ShowDialog();
                    return;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_terminal (" + Environment.NewLine;
                sSql += "codigo, descripcion, nombre_maquina, ip_maquina, vista_aplicacion," + Environment.NewLine;
                sSql += "is_active, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtNombreEquipo.Text.Trim() + "', '" + TxtIPAsignada.Text.Trim() + "'," + Environment.NewLine;
                sSql += iVistaPrograma + ", 1, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarCajasTexto();
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_terminal set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "nombre_maquina = '" + txtNombreEquipo.Text.Trim() + "'," + Environment.NewLine;
                sSql += "ip_maquina = '" + TxtIPAsignada.Text.Trim() + "'," + Environment.NewLine;
                sSql += "vista_aplicacion = " + iVistaPrograma + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_terminal = " + iIdTerminal;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarCajasTexto();
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";

                sSql += "select id_pos_terminal, is_active, isnull(vista_aplicacion, 0) vista_aplicacion," + Environment.NewLine;
                sSql += "codigo COD, descripcion DESCRIPCION, " + Environment.NewLine;
                sSql += "nombre_maquina 'NOMBRE DEL EQUIPO', ip_maquina 'IP ASIGNADA', " + Environment.NewLine;
                sSql += "case is_active when 1 then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "case vista_aplicacion when 1 then 'COMANDERA' else 'CLIENTE EMPRESARIAL' end 'VISTA APLICACIÓN'" + Environment.NewLine;
                sSql += "from pos_terminal" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (txtBusqueda.Text.Trim() != "")
                {
                    sSql += "and (codigo like '%' + '" + txtBusqueda.Text.Trim() + "' + '%' " + Environment.NewLine;
                    sSql += "or descripcion like '%' + '" + txtBusqueda.Text.Trim() + "' + '%'" + Environment.NewLine;
                    sSql += "or nombre_maquina like '%' + '" + txtBusqueda.Text.Trim() + "' + '%')" + Environment.NewLine;
                }

                sSql += "order by id_pos_terminal";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[3].Width = 50;
                    dgvDatos.Columns[4].Width = 150;
                    dgvDatos.Columns[5].Width = 150;
                    //dgvDatos.Columns[6].Width = 100;
                    dgvDatos.Columns[7].Width = 80;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[1].Visible = false;
                    dgvDatos.Columns[2].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.ClearSelection();
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

        //FUNCION PARA ELIMINAR
        private void eliminarRegistro(int iId)
        {
            try
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    //INICIAMOS UNA NUEVA TRANSACCION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                        return;
                    }

                    sSql = "";
                    sSql += "update pos_terminal set" + Environment.NewLine;
                    sSql += "is_active = 0" + Environment.NewLine;
                    sSql += "where id_pos_terminal = " + iId;

                    //SE EJECUTA LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        #endregion

        private void btnExtraerNombreEquipo_Click(object sender, EventArgs e)
        {
            txtNombreEquipo.Text = Environment.MachineName.ToString();
        }

        private void btnNuevoCanImpre_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarCajasTexto();
                grupoDatos.Enabled = true;
                txtCodigo.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }

            else
            {
                if (txtCodigo.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el código para el terminal.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                    return;
                }

                if (txtDescripcion.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la descripción para el terminal.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                    return;
                }

                if (txtNombreEquipo.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el nombre del equipo.";
                    ok.ShowDialog();
                    txtNombreEquipo.Focus();
                    return;
                }

                if (TxtIPAsignada.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la IP asignada del equipo.";
                    ok.ShowDialog();
                    TxtIPAsignada.Focus();
                    return;
                }

                if (chkHabilitado.Checked == true)
                    iHabilitado = 1;
                else
                    iHabilitado = 0;

                if (rdbVistaComandera.Checked == true)
                    iVistaPrograma = 1;
                else
                    iVistaPrograma = 0;

                if (btnNuevo.Text == "Guardar")
                {
                    insertarRegistro();
                }

                else if (btnNuevo.Text == "Actualizar")
                {
                    actualizarRegistro();
                }
            }
        }

        private void btnExtraerIpAsignada_Click(object sender, EventArgs e)
        {
            recuperarIP();
        }

        private void frmTerminales_Load(object sender, EventArgs e)
        {
            llenarComboLocalidad();
            llenarGrid();
        }

        private void btnCerrarCanImpre_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgvDatos.Columns[0].Visible = true;
                iIdTerminal = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                txtNombreEquipo.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();
                TxtIPAsignada.Text = dgvDatos.CurrentRow.Cells[6].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[1].Value) == 1)
                    chkHabilitado.Checked = true;
                else 
                    chkHabilitado.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[2].Value) == 1)
                    rdbVistaComandera.Checked = true;
                else
                    rdbPantallaEmpresa.Checked = true;

                //dgvDatos.Columns[0].Visible = false;
                chkHabilitado.Enabled = true;
                txtCodigo.ReadOnly = true;
                btnEliminar.Enabled = true;
                btnNuevo.Text = "Actualizar";
                grupoDatos.Enabled = true;
                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdTerminal != 0)
            {
                eliminarRegistro(iIdTerminal);
            }

            else
            {
                dgvDatos.Columns[0].Visible = true;
                iIdTerminal = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
                dgvDatos.Columns[0].Visible = false;
                eliminarRegistro(iIdTerminal);
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
            }
        }
    }
}
