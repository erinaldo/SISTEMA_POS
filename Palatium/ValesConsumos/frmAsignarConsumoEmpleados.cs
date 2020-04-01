using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ValesConsumos
{
    public partial class frmAsignarConsumoEmpleados : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iHabilitado;
        int iIdRegistro;

        public frmAsignarConsumoEmpleados()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_consumo_empleados" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "where identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or apellidos like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or nombres like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                        dtConsulta.Rows[i]["id_pos_empleado"].ToString(),
                                        dtConsulta.Rows[i]["id_persona"].ToString(),
                                        dtConsulta.Rows[i]["id_empleado"].ToString(),
                                        dtConsulta.Rows[i]["id_pos_area_consumo_empleados"].ToString(),
                                        dtConsulta.Rows[i]["is_active"].ToString(),
                                        dtConsulta.Rows[i]["identificacion"].ToString(),
                                        dtConsulta.Rows[i]["apellidos"].ToString(),
                                        dtConsulta.Rows[i]["nombres"].ToString(),
                                        dtConsulta.Rows[i]["empleado"].ToString(),
                                        dtConsulta.Rows[i]["en_nomina"].ToString(),
                                        dtConsulta.Rows[i]["estado"].ToString()
                                     );
                }

                dgvDatos.ClearSelection();

                lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";
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
            txtBuscar.Clear();
            txtIdentificacion.Clear();
            txtApellidos.Clear();
            txtNombres.Clear();

            chkHabilitado.Checked = false;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
            grupoDatos.Enabled = false;

            llenarComboAreas();
            llenarGrid();

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
                sSql += "update pos_empleado set" + Environment.NewLine;
                sSql += "id_pos_area_consumo_empleados = " + cmbAreas.SelectedValue + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_empleado = " + iIdRegistro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
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
                sSql += "update pos_empleado set" + Environment.NewLine;
                sSql += "is_active = 0" + Environment.NewLine;
                sSql += "where id_pos_empleado = " + iIdRegistro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro inhabilitado éxitosamente.";
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

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboAreas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_area_consumo_empleados, descripcion" + Environment.NewLine;
                sSql += "from pos_area_consumo_empleados" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbAreas.DisplayMember = "descripcion";
                    cmbAreas.ValueMember = "id_pos_area_consumo_empleados";
                    cmbAreas.DataSource = dtConsulta;
                    //cmbAreas.Items.Insert(0, "Seleccione...!!!");
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmAsignarConsumoEmpleados_Load(object sender, EventArgs e)
        {
            llenarComboAreas();
            llenarGrid();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
            {
                ValesConsumos.frmModalSeleccionValesConsumo modal = new frmModalSeleccionValesConsumo();
                modal.ShowDialog();

                if (modal.DialogResult == DialogResult.OK)
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea inhabilitar el registro seleccionado?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_consumo_empleados"].Value.ToString());
                cmbAreas.SelectedValue = dgvDatos.CurrentRow.Cells["id_pos_area_consumo_empleados"].Value;
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells["identificacion"].Value.ToString();
                txtApellidos.Text = dgvDatos.CurrentRow.Cells["apellidos"].Value.ToString();
                txtNombres.Text = dgvDatos.CurrentRow.Cells["nombres"].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["habilitado"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                grupoDatos.Enabled = true;
                btnAgregar.Text = "Actualizar";
                btnEliminar.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
