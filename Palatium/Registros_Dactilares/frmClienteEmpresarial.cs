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
    public partial class frmClienteEmpresarial : Form
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
        int iHabilitado;

        SqlParameter[] parametro;

        public frmClienteEmpresarial()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                int iCantidad = 3;
            
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial, CE.id_persona, CE.is_active, TP.identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) cliente," + Environment.NewLine;
                sSql += "case CE.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = @estado_1" + Environment.NewLine;
                sSql += "and CE.estado in (@estado_2, @estado_3)" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "where TP.identificacion like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%@buscar%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%@buscar%'" + Environment.NewLine;
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

                if (iCantidad == 4)
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
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_cliente_empresarial"].ToString(),
                                      dtConsulta.Rows[i]["id_persona"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["identificacion"].ToString(),
                                      dtConsulta.Rows[i]["cliente"].ToString(),
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarGrid();
            txtBuscar.Clear();
            txtIdentificacion.Clear();
            txtDescripcion.Clear();
            btnAgregar.ButtonText = "Agregar";
            btnEliminar.Enabled = false;
            grupoDatos.Enabled = false;

            chkHabilitado.Checked = false;
            chkHabilitado.Enabled = false;

            txtBuscar.Focus();
        }

        //FUNCION PARA ACTUALIZAR LOS REGISTROS
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
                sSql += "update pos_cliente_empresarial set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = @id_pos_cliente_empresarial";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iHabilitado;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_cliente_empresarial";
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
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ELIMINAR
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
                sSql += "update pos_cliente_empresarial set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = @id_pos_cliente_empresarial";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 0;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_cliente_empresarial";
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

        private void frmClienteEmpresarial_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.ButtonText == "Agregar")
            {
                Empresa.frmModalClienteEmpresarial clienteEmpresarial = new Empresa.frmModalClienteEmpresarial();
                clienteEmpresarial.ShowDialog();

                if (clienteEmpresarial.DialogResult == DialogResult.OK)
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
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_cliente_empresarial"].Value.ToString());
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells["identificacion"].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["cliente"].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                grupoDatos.Enabled = true;
                chkHabilitado.Enabled = true;
                btnAgregar.ButtonText = "Actualizar";
                btnEliminar.Enabled = true;
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
    }
}
