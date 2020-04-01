using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FInformacionMetodoPago : Form
    {
        //VARIABLES, INSTANCIAS
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeSiNo SiNo;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        bool bRespuesta;

        DataTable dtConsulta;

        string sEstado;
        string sSql;

        int iIdRegistro;
        int iHabilitado;

        public FInformacionMetodoPago()
        {
            InitializeComponent();
        }

        private void FInformacionMetodoPago_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        #region FUNCIONES DEL USUARIO

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            llenarComboFormasPagos();
            iHabilitado = 0;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            llenarGrid();
        }

        //FUNCION COLUMNAS GRID
        private void columnasGrid(bool ok)
        {
            dgvDatos.Columns[0].Visible = ok;
            dgvDatos.Columns[4].Visible = ok;
            dgvDatos.Columns[6].Visible = ok;
            dgvDatos.Columns[1].Width = 75;
            dgvDatos.Columns[2].Width = 150;
            dgvDatos.Columns[3].Width = 75;
            dgvDatos.Columns[5].Width = 150;
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select MP.id_pos_metodo_pago, MP.codigo CÓDIGO, MP.descripcion DESCRIPCIÓN," + Environment.NewLine;
                //sSql += "case MP.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "case MP.is_active when 1 then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "isnull(FP.id_sri_forma_pago, 0) id_sri_forma_pago," + Environment.NewLine;
                sSql += "isnull(FP.descripcion, 'SIN ASIGNAR') FORMA_PAGO," + Environment.NewLine;
                sSql += "isnull(MP.is_active, 0) is_active" + Environment.NewLine;
                sSql += "from pos_metodo_pago MP LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "sri_forma_pago FP ON FP.id_sri_forma_pago = MP.id_sri_forma_pago" + Environment.NewLine;
                sSql += "and MP.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and FP.estado = 'A'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and (MP.codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or MP.descripcion like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by MP.codigo" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    columnasGrid(false);
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE FORMAS DE PAGO
        private void llenarComboFormasPagos()
        {
            try
            {
                sSql = "";
                sSql += "select id_sri_forma_pago, codigo + ' - ' + descripcion forma_pago" + Environment.NewLine;
                sSql += "from sri_forma_pago" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbFormasPagos.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR UN REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ELIMINAR
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_metodo_pago (" + Environment.NewLine;
                sSql += "codigo, descripcion, id_sri_forma_pago, is_active, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.ToString().Trim() + "', '" + txtDescripcion.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbFormasPagos.SelectedValue) + ", 1, 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Registro agregado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }


        //FUNCION PARA ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ELIMINAR
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }


                sSql = "";
                sSql += "update pos_metodo_pago set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "id_sri_forma_pago = " + Convert.ToInt32(cmbFormasPagos.SelectedValue) + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_metodo_pago = " + iIdRegistro + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";


                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA ELIMINAR UN REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ELIMINAR
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_metodo_pago set" + Environment.NewLine;
                sSql += "is_active = 0" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                //sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                //sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_metodo_pago = " + iIdRegistro;


                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            llenarComboFormasPagos();
            iHabilitado = 0;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            grupoDatos.Enabled = false;
            llenarGrid();
        }

        #endregion

        private void Txt_Codigo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_metodo_pago, descripcion, id_sri_forma_pago," + Environment.NewLine;
                //sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "case is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_metodo_pago" + Environment.NewLine;
                sSql += "where codigo = '" + txtCodigo.Text + "'" + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdRegistro = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtDescripcion.Text = dtConsulta.Rows[0][1].ToString().ToUpper();
                        cmbFormasPagos.SelectedValue = dtConsulta.Rows[0][2].ToString();

                        if (Convert.ToInt32(dtConsulta.Rows[0]["is_active"].ToString()) == 1)
                            chkHabilitado.Checked = true;
                        else
                            chkHabilitado.Checked = false;
                        
                        btnAnularMetodoPago.Enabled = true;
                        btnNuevoMetodoPago.Text = "Actualizar";
                        txtCodigo.Enabled = false;
                        chkHabilitado.Enabled = true;
                        txtDescripcion.Focus();
                    }

                    else
                    {
                        iIdRegistro = 0;
                        txtDescripcion.Clear();
                        cmbFormasPagos.SelectedIndex = 0;
                        btnAnularMetodoPago.Enabled = false;
                        chkHabilitado.Checked = true;
                        chkHabilitado.Enabled = false;
                        btnNuevoMetodoPago.Text = "Guardar";
                        txtDescripcion.Focus();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }                
            }
            
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtCodigoCajero_Leave(object sender, EventArgs e)
        {
            txtCodigo.LostFocus += new EventHandler(Txt_Codigo_LostFocus);
        }

        private void btnCerrarMetodoPago_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarMetodoPago_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevoMetodoPago.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscarMetodoPago_Click(object sender, EventArgs e)
        {
            try
            {
                llenarGrid();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnNuevoMetodoPago_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoMetodoPago.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnNuevoMetodoPago.Text = "Guardar";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }

            else 
            {
                if ((txtCodigo.Text == "") && (txtDescripcion.Text == ""))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Debe rellenar todos los campos obligatorios.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtCodigo.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese el código del Método de pago.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese la descripción del Método de pago.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (Convert.ToInt32(cmbFormasPagos.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor seleccione la forma de pago.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else
                {
                    if (chkHabilitado.Checked == true)
                        iHabilitado = 1;
                    else
                        iHabilitado = 0;

                    if (btnNuevoMetodoPago.Text == "Guardar")
                    {
                        insertarRegistro();
                    }

                    else if (btnNuevoMetodoPago.Text == "Actualizar")
                    {
                        actualizarRegistro();
                    }
                }            
            }
        }

        private void btnAnularMetodoPago_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeSiNo();
            SiNo.LblMensaje.Text = "¿Está seguro que desea dar de baja el registro?";
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
                grupoDatos.Enabled = true;
                txtCodigo.Enabled = false;
                btnNuevoMetodoPago.Text = "Actualizar";
                chkHabilitado.Enabled = true;

                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                cmbFormasPagos.SelectedValue = dgvDatos.CurrentRow.Cells[4].Value.ToString();

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value.ToString()) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
