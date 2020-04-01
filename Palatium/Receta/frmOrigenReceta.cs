using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmOrigenReceta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sEstado;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdPosOrigenReceta;

        public frmOrigenReceta()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el grid
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgvRegistro.Rows.Clear();

                sSql = "";
                sSql += "select id_pos_origen_receta, codigo, descripcion," + Environment.NewLine;
                sSql += "case when estado = 'A' then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_origen_receta" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;

                if (iBandera == 1)
                {
                    sSql += "and descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by id_pos_origen_receta";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvRegistro.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
                                                dtConsulta.Rows[i].ItemArray[1].ToString(),
                                                dtConsulta.Rows[i].ItemArray[2].ToString(),
                                                dtConsulta.Rows[i].ItemArray[3].ToString()
                                                  );
                        }

                    }

                    dgvRegistro.ClearSelection();
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            llenarGrid(0);
            cmbEstado.Text = "ACTIVO";
            txtCodigo.Text = "";
            txtBuscar.Text = "";
            txtDescripcion.Text = "";
            btnNuevo.Text = "Nuevo";
            grupoDatos.Enabled = false;
            btnAnular.Enabled = false;
            cmbEstado.Enabled = false;
        }

        //Función para comprobar código repetido
        private int comprobarCodigoRepetido()
        {
            try
            {
                int iBandera = 0;

                for (int i = 0; i < dgvRegistro.Rows.Count; i++)
                {
                    if (dgvRegistro.Rows[i].Cells[1].Value.ToString().Trim().ToUpper() == txtCodigo.Text.Trim().ToUpper())
                    {
                        iBandera++;
                        break;
                    }
                }

                return iBandera;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //Función para guardar el registro
        private void insertarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }
                
                sSql = "";
                sSql += "insert into pos_origen_receta (" + Environment.NewLine;
                sSql += "codigo, descripcion, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim().ToUpper() + "','" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro Ingresado éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
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

        //Función para actualizar el registro
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_origen_receta set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_pos_origen_receta = " + iIdPosOrigenReceta + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
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

        //Función para anular el registro
        private void eliminarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql = "update pos_origen_receta set" + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "." + iIdPosOrigenReceta + "'," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_origen_receta = " + iIdPosOrigenReceta;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
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

        //Función para verificar si existen registros asociados
        private int verificarRegistros()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_receta" + Environment.NewLine;
                sSql += "where id_pos_origen_receta = " + iIdPosOrigenReceta + Environment.NewLine;
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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        #endregion

        private void frmOrigenReceta_Load(object sender, EventArgs e)
        {
            limpiarCampos();
        }     

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiarCampos();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNuevo.Text == "Nuevo")
                {
                    limpiarCampos();
                    btnNuevo.Text = "Guardar";
                    grupoDatos.Enabled = true;
                    txtCodigo.Enabled = true;
                    txtCodigo.Focus();
                }

                else
                {
                    if (txtCodigo.Text.Trim() == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el código para el registro.";
                        ok.ShowDialog();
                        txtCodigo.Focus();
                        return;
                    }

                    else if (txtDescripcion.Text.Trim() == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese la descripción para el registro.";
                        ok.ShowDialog();
                        txtDescripcion.Focus();
                        return;
                    }

                    if (cmbEstado.Text == "ACTIVO")
                    {
                        sEstado = "A";
                    }

                    else
                    {
                        sEstado = "N";
                    }

                    if (btnNuevo.Text == "Guardar")
                    {
                        if (comprobarCodigoRepetido() == -1)
                        {
                            return;
                        }

                        else if (comprobarCodigoRepetido() > 0)
                        {
                            ok.lblMensaje.Text = "El código ingresado ya se encuentra registrado. Favor ingrese un nuevo código.";
                            ok.ShowDialog();
                            txtCodigo.Clear();
                            txtCodigo.Focus();
                        }

                        else
                        {
                            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                insertarRegistro();
                            }
                        }
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar variables
        private void llenarVariables()
        {
            try
            {
                grupoDatos.Enabled = true;
                iIdPosOrigenReceta = Convert.ToInt32(dgvRegistro.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvRegistro.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvRegistro.CurrentRow.Cells[2].Value.ToString();
                string sEstado = dgvRegistro.CurrentRow.Cells[3].Value.ToString();
                if (sEstado == "A")
                    cmbEstado.Text = "Activo";
                btnNuevo.Text = "Actualizar";

                txtDescripcion.Focus();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (verificarRegistros() == -1)
            {
                return;
            }

            if (verificarRegistros() > 0)
            {
                ok.lblMensaje.Text = "No se puede eliminar el registro ya que está asociado una receta";
                ok.ShowDialog();
                return;
            }

            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {            
            this.Close();
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

        private void dgvRegistro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grupoDatos.Enabled = true;
                iIdPosOrigenReceta   = Convert.ToInt32(dgvRegistro.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvRegistro.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvRegistro.CurrentRow.Cells[2].Value.ToString();
                cmbEstado.Text = dgvRegistro.CurrentRow.Cells[3].Value.ToString();
                btnNuevo.Text = "Actualizar";
                txtCodigo.Enabled = false;
                btnAnular.Enabled = true;
                cmbEstado.Enabled = true;
                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
