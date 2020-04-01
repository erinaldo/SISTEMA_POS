using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmEmpleadosEmpresas : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sEstado;

        bool bRespuesta;

        DataTable dtConsulta;

        int iIdPersona;
        int iIdRegistro;
        int iAplicaAlmuerzo;

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
                sSql += "and CE.estado = 'A'" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "order by apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbEmpresas.llenar(dtConsulta, sSql);
                dtConsulta = new DataTable();

                dtConsulta.Clear();
                cmbEmpresaCliente.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void limpiar()
        {
            cmbEmpresas.SelectedIndexChanged -= new EventHandler(cmbEmpresas_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbEmpresas.SelectedIndexChanged += new EventHandler(cmbEmpresas_SelectedIndexChanged);
            txtBuscar.Clear();
            txtIdentificacion.Clear();
            txtDescripcion.Clear();
            cmbEstado.SelectedIndex = 0;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
            grupoDatos.Enabled = false;
            chkAlmuerzo.Checked = false;
            llenarGrid(0);
            txtBuscar.Focus();
        }

        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select E.id_pos_empleado_cliente, E.id_persona, E.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "TP.identificacion IDENTIFICACIÓN, " + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) 'NOMBRE EMPLEADO'," + Environment.NewLine;
                sSql += "case E.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO, E.aplica_almuerzo" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and E.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = " + Convert.ToInt32(cmbEmpresas.SelectedValue) + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and TP.identificacion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[1].Visible = false;
                    dgvDatos.Columns[2].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[3].Width = 120;
                    dgvDatos.Columns[4].Width = 200;
                    dgvDatos.Columns[5].Width = 80;
                    dgvDatos.ClearSelection();
                    lblRegistros.Text = dtConsulta.Rows.Count.ToString() + " Registros Encontrados";
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
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
                sSql += "aplica_almuerzo = " + iAplicaAlmuerzo + "," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'," + Environment.NewLine;
                sSql += "id_pos_cliente_empresarial = " + Convert.ToInt32(cmbEmpresaCliente.SelectedValue) + Environment.NewLine;
                sSql += "where id_pos_empleado_cliente = " + iIdRegistro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
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
                cmbEstado.SelectedIndex = 0;
                btnAgregar.Text = "Agregar";
                btnEliminar.Enabled = false;
                grupoDatos.Enabled = false;
                chkAlmuerzo.Checked = false;
                llenarGrid(0);
                txtBuscar.Focus();
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
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
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    ok.ShowDialog();
                }
                else
                {
                    sSql = "";
                    sSql += "update pos_empleado_cliente set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pos_empleado_cliente = " + iIdRegistro;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                    }
                    else
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                        ok.ShowDialog();

                        limpiar();
                        llenarGrid(0);
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(3);
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
            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
            {
                frmModalEmpleadoEmpresa modalEmpleadoEmpresa = new frmModalEmpleadoEmpresa();
                modalEmpleadoEmpresa.ShowDialog();

                if (modalEmpleadoEmpresa.DialogResult == DialogResult.OK)
                {
                    limpiar();
                }
            }

            else
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                SiNo.ShowDialog();
                
                if (SiNo.DialogResult == DialogResult.OK)
                {
                    if (cmbEstado.Text.Trim() == "ACTIVO")
                    {
                        sEstado = "A";
                    }

                    else
                    {
                        sEstado = "N";
                    }

                    if (chkAlmuerzo.Checked == true)
                    {
                        iAplicaAlmuerzo = 1;
                    }

                    else
                    {
                        iAplicaAlmuerzo = 0;
                    }

                    actualizarRegistro();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                cmbEmpresaCliente.SelectedValue = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();

                if (dgvDatos.CurrentRow.Cells[5].Value.ToString() == "ACTIVO")
                {
                    cmbEstado.Text = "ACTIVO";
                }

                else
                {
                    cmbEstado.Text = "INACTIVO";
                }

                chkAlmuerzo.Checked = Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value.ToString()) == 1;
                btnAgregar.Text = "Actualizar";
                btnEliminar.Enabled = true;
                grupoDatos.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
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
