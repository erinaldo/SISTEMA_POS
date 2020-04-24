using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Registros_Dactilares
{
    public partial class frmEmpleadosEmpresas : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;

        bool bRespuesta;

        DataTable dtConsulta;

        int iIdPersona;
        int iIdRegistro;
        int iAplicaAlmuerzo;
        int iHabilitado;

        SqlParameter[] parametro;

        public frmEmpleadosEmpresas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE EMPRESAS
        private void llenarComboEmpresas()
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and CE.estado = @estado_1" + Environment.NewLine;
                sSql += "and TP.estado = @estado_2" + Environment.NewLine;
                sSql += "order by apellidos";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtConsulta.NewRow();
                row["id_pos_cliente_empresarial"] = "0";
                row["cliente"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbEmpresas.DisplayMember = "cliente";
                cmbEmpresas.ValueMember = "id_pos_cliente_empresarial";
                cmbEmpresas.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE EMPRESAS
        private void llenarComboEmpresasActualizar()
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and CE.estado = @estado_1" + Environment.NewLine;
                sSql += "and TP.estado = @estado_2" + Environment.NewLine;
                sSql += "order by apellidos";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtConsulta.NewRow();
                row["id_pos_cliente_empresarial"] = "0";
                row["cliente"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbEmpresaCliente.DisplayMember = "cliente";
                cmbEmpresaCliente.ValueMember = "id_pos_cliente_empresarial";
                cmbEmpresaCliente.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void limpiar()
        {
            cmbEmpresas.SelectedIndexChanged -= new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbEmpresas.SelectedIndexChanged += new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresasActualizar();
            txtBuscar.Clear();
            txtIdentificacion.Clear();
            txtDescripcion.Clear();
            btnAgregar.ButtonText = "Agregar";
            btnEliminar.Enabled = false;
            grupoDatos.Enabled = false;
            chkAlmuerzo.Checked = false;

            llenarGrid();
            txtBuscar.Focus();
        }

        private void llenarGrid()
        {
            try
            {
                int iCantidad = 4;

                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select E.id_pos_empleado_cliente, E.id_persona, E.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "E.is_active, E.aplica_almuerzo, TP.identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) empleado," + Environment.NewLine;
                sSql += "case E.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = @estado_1" + Environment.NewLine;
                sSql += "and E.estado in (@estado_2, @estado_3)" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = @id_pos_cliente_empresarial" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "and (TP.identificacion like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%@buscar%')" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[iCantidad];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_1";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_2";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_3";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "N";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cliente_empresarial";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresas.SelectedValue);

                if (iCantidad == 5)
                {
                    a++;
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@buscar";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = txtBuscar.Text.Trim();
                }

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_empleado_cliente"].ToString(),
                                      dtConsulta.Rows[i]["id_persona"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_cliente_empresarial"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["aplica_almuerzo"].ToString(),
                                      dtConsulta.Rows[i]["identificacion"].ToString(),
                                      dtConsulta.Rows[i]["empleado"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString());
                }

                lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";
                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                sSql += "aplica_almuerzo = @aplica_almuerzo," + Environment.NewLine;
                sSql += "is_active = @is_active," + Environment.NewLine;
                sSql += "id_pos_cliente_empresarial = @id_pos_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @id_pos_empleado_cliente";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[4];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@aplica_almuerzo";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iAplicaAlmuerzo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cliente_empresarial";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresaCliente.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_empleado_cliente";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdRegistro;

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();

                txtBuscar.Clear();
                txtIdentificacion.Clear();
                txtDescripcion.Clear();
                btnAgregar.Text = "Agregar";
                btnEliminar.Enabled = false;
                grupoDatos.Enabled = false;
                chkAlmuerzo.Checked = false;
                chkHabilitado.Checked = false;
                llenarGrid();
                txtBuscar.Focus();
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void eliminarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @id_pos_empleado_cliente";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 0;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_empleado_cliente";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdRegistro;

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();

                limpiar();
                llenarGrid();
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmEmpleadosEmpresas_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            txtDescripcion.Clear();
            txtIdentificacion.Clear();
            chkAlmuerzo.Checked = false;
            chkHabilitado.Checked = false;
            grupoDatos.Enabled = false;
            btnAgregar.ButtonText = "Agregar";
            llenarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.ButtonText == "Agregar")
            {
                Registros_Dactilares.frmModalEmpleadoEmpresa modalEmpleadoEmpresa = new Registros_Dactilares.frmModalEmpleadoEmpresa();
                modalEmpleadoEmpresa.ShowDialog();

                if (modalEmpleadoEmpresa.DialogResult == DialogResult.OK)
                {
                    limpiar();
                }
            }

            else
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                SiNo.ShowDialog();
                
                if (SiNo.DialogResult == DialogResult.OK)
                {
                    if (chkHabilitado.Checked == true)
                        iHabilitado = 1;
                    else
                        iHabilitado = 0;

                    if (chkAlmuerzo.Checked == true)
                        iAplicaAlmuerzo = 1;
                    else
                        iAplicaAlmuerzo = 0;

                    actualizarRegistro();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_empleado_cliente"].Value.ToString());
                cmbEmpresaCliente.SelectedValue = dgvDatos.CurrentRow.Cells["id_pos_cliente_empresarial"].Value.ToString();
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells["identificacion"].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["empleado"].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["aplica_almuerzo"].Value) == 1)
                    chkAlmuerzo.Checked = true;
                else
                    chkAlmuerzo.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                btnAgregar.ButtonText = "Actualizar";
                chkHabilitado.Enabled = true;
                btnEliminar.Enabled = true;
                grupoDatos.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
