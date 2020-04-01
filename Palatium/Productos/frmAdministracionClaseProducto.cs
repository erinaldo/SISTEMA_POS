using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmAdministracionClaseProducto : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sEstado;
        string sDatoRegistro;

        DataTable dtConsulta;

        int iIdRegistro;
        int iCuenta;
        int iCantidad;
        int iMateriaPrima;
        int iProductoTerminado;

        bool bRespuesta;

        public frmAdministracionClaseProducto()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el grid
        private void llenarGrid(int iOp)
        {
            try
            {
                dgvRegistro.Rows.Clear();

                sSql = "";
                sSql += "select id_pos_clase_producto, codigo, descripcion," + Environment.NewLine;
                sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end estado," + Environment.NewLine;
                sSql += "aplica_materia_prima, aplica_producto_terminado" + Environment.NewLine;
                sSql += "from pos_clase_producto" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and descripcion like '%" + txtBuscar.Text + "%'" + Environment.NewLine;
                }

                sSql += "order by id_pos_clase_producto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        dgvRegistro.Rows.Add(dtConsulta.Rows[i]["id_pos_clase_producto"].ToString(),
                                             dtConsulta.Rows[i]["codigo"].ToString(),
                                             dtConsulta.Rows[i]["descripcion"].ToString(),
                                             dtConsulta.Rows[i]["estado"].ToString(),
                                             dtConsulta.Rows[i]["aplica_materia_prima"].ToString(),
                                             dtConsulta.Rows[i]["aplica_producto_terminado"].ToString()
                                              );
                    }
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
            txtCodigo.Clear();
            txtBuscar.Clear();
            txtDescripcion.Text = "";
            cmbEstado.Text = "ACTIVO";
            btnNuevo.Text = "Nuevo";

            txtCodigo.Enabled = true;
            grupoDatos.Enabled = false;
            btnAnular.Enabled = false;
            cmbEstado.Enabled = false;

            chkMateriaPrima.Checked = false;
            chkProductoTerminado.Checked = false;

            txtBuscar.Focus();
        }

        //FUNCION PARA COMPROBAR UN CODIGO REPETIDO
        private int contarRegistros()
        {
            try
            {
                iCuenta = 0;

                for (int i = 0; i < dgvRegistro.Rows.Count; i++)
                {
                    if (txtCodigo.Text.Trim().ToUpper() == dgvRegistro.Rows[i].Cells[1].Value.ToString().Trim().ToUpper())
                    {
                        sDatoRegistro = dgvRegistro.Rows[i].Cells[2].Value.ToString().Trim().ToUpper();
                        iCuenta = 1;
                        break;
                    }
                }

                return iCuenta;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //Función para verificar si existen registros asosiados
        private int verificarRegistros()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_pos_clase_producto = " + iIdRegistro + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and nivel in (3, 4)";

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

        //Función para guardar el registro
        private void insertarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiarCampos();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_clase_producto (" + Environment.NewLine;
                sSql += "codigo, descripcion, aplica_materia_prima, aplica_producto_terminado" + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim().ToUpper() + "', '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += iMateriaPrima + ", " + iProductoTerminado + ", ";
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado éxitosamente.";
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
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiarCampos();
                    return;
                }

                sSql = "";
                sSql += "update pos_clase_producto set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "aplica_materia_prima = " + iMateriaPrima + "," + Environment.NewLine;
                sSql += "aplica_producto_terminado = " + iProductoTerminado + "," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_pos_clase_producto = " + iIdRegistro + Environment.NewLine;
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

        //FUNCION PARA ELIMINAR EL REGISTRO 
        private void eliminarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    ok.ShowDialog();
                    limpiarCampos();
                    return;
                }

                sSql = "";
                sSql += "update pos_clase_producto set" + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "." + iIdRegistro + "'," + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_clase_producto = " + iIdRegistro;

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

        #endregion

        private void frmAdministracionClaseProducto_Load(object sender, EventArgs e)
        {
            cmbEstado.Text = "Activo";
            llenarGrid(1);
            cmbEstado.Enabled = false;
        }
        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Limpiar..?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                limpiarCampos();
                btnNuevo.Text = "Nuevo";
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
                }

                else
                {
                    if (txtCodigo.Text.Trim() == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el código para el registro.";
                        ok.ShowDialog();
                        txtCodigo.Focus();
                    }

                    else if (txtDescripcion.Text.Trim() == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese la descripción para el registro.";
                        ok.ShowDialog();
                        txtDescripcion.Focus();
                    }

                    else
                    {
                        if (chkMateriaPrima.Checked == true)
                        {
                            iMateriaPrima = 1;
                        }

                        else
                        {
                            iMateriaPrima = 0;
                        }

                        if (chkProductoTerminado.Checked == true)
                        {
                            iProductoTerminado = 1;
                        }

                        else
                        {
                            iProductoTerminado = 0;
                        }


                        if (btnNuevo.Text == "Guardar")
                        {
                            iCantidad = contarRegistros();

                            if (iCantidad > 0)
                            {
                                ok.lblMensaje.Text = "El código ingresado ya está asignado en el registro " + sDatoRegistro + ". Ingrese otro código.";
                                ok.ShowDialog();
                                txtCodigo.Clear();
                                txtCodigo.Focus();
                            }

                            else if (iCantidad == -1)
                            {
                                return;
                            }

                            else
                            {
                                NuevoSiNo.lblMensaje.Text = "¿Desea guardar el registro...?";
                                NuevoSiNo.ShowDialog();

                                if (NuevoSiNo.DialogResult == DialogResult.OK)
                                {
                                    insertarRegistro();
                                }
                            }
                        }

                        else if (btnNuevo.Text == "Actualizar")
                        {
                            if (cmbEstado.Text == "ACTIVO")
                            {
                                sEstado = "A";
                            }

                            else
                            {
                                sEstado = "N";
                            }

                            NuevoSiNo.lblMensaje.Text = "¿Desea actualizar el registro...?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                actualizarRegistro();
                            }
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

        private void btnAnular_Click(object sender, EventArgs e)
        {
            iCuenta = verificarRegistros();

            if (iCuenta > 0)
            {
                ok.lblMensaje.Text = "Existen productos asociados al registro";
                ok.ShowDialog();
            }

            else if (iCuenta == -1)
            {
                return;
            }

            else
            {
                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
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
                iIdRegistro = Convert.ToInt32(dgvRegistro.CurrentRow.Cells[0].Value);
                txtCodigo.Text = dgvRegistro.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvRegistro.CurrentRow.Cells[2].Value.ToString().ToUpper();
                cmbEstado.Text = dgvRegistro.CurrentRow.Cells[3].Value.ToString().ToUpper();

                if (Convert.ToInt32(dgvRegistro.CurrentRow.Cells[4].Value.ToString()) == 1)
                {
                    chkMateriaPrima.Checked = true;
                }

                else
                {
                    chkMateriaPrima.Checked = false;
                }

                if (Convert.ToInt32(dgvRegistro.CurrentRow.Cells[5].Value.ToString()) == 1)
                {
                    chkProductoTerminado.Checked = true;
                }

                else
                {
                    chkProductoTerminado.Checked = false;
                }

                grupoDatos.Enabled = true;
                txtCodigo.Enabled = false;
                btnAnular.Enabled = true;
                cmbEstado.Enabled = true;
                btnNuevo.Text = "Actualizar";
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
