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

namespace Palatium.Personal
{
    public partial class frmInactivarHuellasCajeros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        bool bRespuesta;

        int iIdRegistro;
        int iHabilitado;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        public frmInactivarHuellasCajeros()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvCajero.Rows.Clear();
                int iCantidad = 2;

                sSql = "";
                sSql += "select id_pos_cajero, is_active_huella, codigo, descripcion," + Environment.NewLine;
                sSql += "case is_active_huella when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where estado in (@estado_1, @estado_2)" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    iCantidad++;
                    sSql += "and descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by codigo";

                #region PARAMETROS

                parametro = new SqlParameter[iCantidad];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "N";

                if (iCantidad == 3)
                {
                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@buscar";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = txtBuscar.Text.Trim();
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
                    dgvCajero.Rows.Add(dtConsulta.Rows[i]["id_pos_cajero"].ToString(),
                                       dtConsulta.Rows[i]["is_active_huella"].ToString(),
                                       dtConsulta.Rows[i]["codigo"].ToString(),
                                       dtConsulta.Rows[i]["descripcion"].ToString(),
                                       dtConsulta.Rows[i]["estado"].ToString());
                }

                dgvCajero.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //ACTUALIZAR LA HUELLA EN EL SISTEMA
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

                //imagen = imagenABytes(imgHuellaCapturada.Image);

                sSql = "";
                sSql += "update pos_cajero set" + Environment.NewLine;
                sSql += "is_active_huella = @is_active_huella" + Environment.NewLine;
                sSql += "where id_pos_cajero = @id_pos_cajero" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active_huella";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iHabilitado;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_pos_cajero";
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

        //FUNCION LIMPIAR
        private void limpiar()
        {
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtEstado.Clear();
            grupoDatos.Enabled = false;
            chkHabilitado.Checked = false;

            llenarGrid();
        }

        #endregion

        private void frmInactivarHuellasCajeros_Load(object sender, EventArgs e)
        {
            limpiar();
            this.ActiveControl = txtBuscar;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void dgvCajero_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvCajero.CurrentRow.Cells["id_pos_cajero"].Value.ToString());

                txtCodigo.Text = dgvCajero.CurrentRow.Cells["codigo"].Value.ToString();
                txtDescripcion.Text = dgvCajero.CurrentRow.Cells["descripcion"].Value.ToString();
                txtEstado.Text = dgvCajero.CurrentRow.Cells["estado"].Value.ToString();

                if (Convert.ToInt32(dgvCajero.CurrentRow.Cells["is_active_huella"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                grupoDatos.Enabled = true;
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
                if (chkHabilitado.Checked == true)
                    iHabilitado = 1;
                else
                    iHabilitado = 0;

                actualizarRegistro();
            }
        }

    }
}
