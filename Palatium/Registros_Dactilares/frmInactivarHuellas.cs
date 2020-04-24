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
    public partial class frmInactivarHuellas : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;

        bool bRespuesta;

        DataTable dtConsulta;

        int iIdRegistro;
        int iHabilitarEscape;
        int iHabilitado;

        SqlParameter[] parametro;

        public frmInactivarHuellas(int iHabilitarEscape_P)
        {
            this.iHabilitarEscape = iHabilitarEscape_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE EMPRESAS
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

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

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

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select E.id_pos_empleado_cliente, E.is_active_huella," + Environment.NewLine;
                sSql += "isnull(E.huella_dactilar, '') huella_dactilar, TP.identificacion, " + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) nombre_empleado," + Environment.NewLine;
                sSql += "case E.is_active_huella when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = @estado_1" + Environment.NewLine;
                sSql += "and E.estado in (@estado_2, @estado_3)" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = @id_pos_cliente_empresarial" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and (TP.identificacion like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%@buscar%')" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[5];
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
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_cliente_empresarial";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresas.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@buscar";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBuscar.Text.Trim();

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
                                      dtConsulta.Rows[i]["huella_dactilar"].ToString(),
                                      dtConsulta.Rows[i]["is_active_huella"].ToString(),
                                      dtConsulta.Rows[i]["identificacion"].ToString(),
                                      dtConsulta.Rows[i]["nombre_empleado"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString()
                                      );
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

        //FUNCION PARA ACTUALIZAR EL REGISTRO
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
                sSql += "is_active_huella = @is_active_huella" + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = @idRegistro" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active_huella";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iHabilitado;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@idRegistro";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdRegistro;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

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
                ok.lblMensaje.Text = "Estado de la huella dactilar actualizada éxitosamente.";
                ok.ShowDialog();

                limpiar();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            dgvDatos.Rows.Clear();
            txtBuscar.Clear();
            txtDescripcion.Clear();
            txtIdentificacion.Clear();
            txtEstadoHuella.Clear();
            chkHabilitarHuella.Checked = false;
            chkHabilitarHuella.Enabled = false;
            btnGuardar.Enabled = false;
            llenarGrid();
            txtBuscar.Focus();
        }

        #endregion

        private void frmInactivarHuellas_Load(object sender, EventArgs e)
        {
            cmbEmpresas.SelectedIndexChanged -= new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbEmpresas.SelectedIndexChanged += new EventHandler(cmbEmpresas_SelectedIndexChanged);

            limpiar();
            this.ActiveControl = cmbEmpresas;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbEmpresas.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la empresa.";
                ok.ShowDialog();
                return;
            }

            llenarGrid();
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.CurrentRow.Cells["huella_dactilar"].Value.ToString().Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El registro seleccionado no cuenta con una huella dactilar registrada.";
                    ok.ShowDialog();
                    return;
                }

                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_empleado_cliente"].Value.ToString());

                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["nombre_empleado"].Value.ToString();
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells["identificacion"].Value.ToString();
                txtEstadoHuella.Text = dgvDatos.CurrentRow.Cells["estado"].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active_huella"].Value) == 0)
                    chkHabilitarHuella.Checked = false;
                else
                    chkHabilitarHuella.Checked = true;

                chkHabilitarHuella.Enabled = true;
                btnGuardar.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea registrar la huella al registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                if (chkHabilitarHuella.Checked == true)
                    iHabilitado = 1;
                else
                    iHabilitado = 0;

                actualizarRegistro();
            }
        }
    }
}
