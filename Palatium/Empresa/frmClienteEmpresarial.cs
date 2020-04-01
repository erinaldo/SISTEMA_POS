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
    public partial class frmClienteEmpresarial : Form
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

        public frmClienteEmpresarial()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial, CE.id_persona, TP.identificacion IDENTIFICACIÓN," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) 'RAZÓN SOCIAL'," + Environment.NewLine;
                sSql += "case CE.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CE.estado in ('A', 'N')" + Environment.NewLine;
                
                if (iOp == 1)
                {
                    sSql += "where TP.identificacion like '%" + this.txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.apellidos like '%" + this.txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%" + this.txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
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
                    dgvDatos.Columns[2].Width = 120;
                    dgvDatos.Columns[3].Width = 200;
                    dgvDatos.Columns[4].Width = 80;
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarGrid(0);
            txtBuscar.Clear();
            txtIdentificacion.Clear();
            txtDescripcion.Clear();
            cmbEstado.SelectedIndex = 0;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
            grupoDatos.Enabled = false;
            txtBuscar.Focus();
        }

        //FUNCION PARA ACTUALIZAR LOS REGISTROS
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                }

                else
                {
                    sSql = "";
                    sSql += "update pos_cliente_empresarial set" + Environment.NewLine;
                    sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                    sSql += "where id_pos_cliente_empresarial = " + iIdRegistro;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                    ok.ShowDialog();
                    limpiar();
                }
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
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
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    int num = (int)ok.ShowDialog();
                }
                else
                {
                    sSql = "";
                    sSql += "update pos_cliente_empresarial set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pos_cliente_empresarial = " + iIdRegistro;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                    ok.ShowDialog();
                    limpiar();
                }
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmClienteEmpresarial_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
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
                txtIdentificacion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();

                if (dgvDatos.CurrentRow.Cells[4].Value.ToString() == "ACTIVO")
                {
                    cmbEstado.Text = "ACTIVO";
                }

                else
                {
                    cmbEstado.Text = "INACTIVO";
                }

                grupoDatos.Enabled = true;
                btnAgregar.Text = "Actualizar";
                btnEliminar.Enabled = true;
            }
            catch (Exception ex)
            {
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
            SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void frmClienteEmpresarial_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
