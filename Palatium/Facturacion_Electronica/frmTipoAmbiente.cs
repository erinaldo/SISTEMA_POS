using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmTipoAmbiente : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sSql;
        string sEstado;

        DataTable dtConsulta;

        bool bRespuesta;
        bool bActualizar;

        int iIdRegistro;
        int iBandera;
        int iCuenta;

        public frmTipoAmbiente()
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
                sSql += "select id_tipo_ambiente, codigo CODIGO, nombres DSCRIPCION," + Environment.NewLine;
                sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO" + Environment.NewLine;
                sSql += "from cel_tipo_ambiente" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or nombres like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by id_tipo_ambiente";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;

                    dgvDatos.Columns[1].Width = 75;
                    dgvDatos.Columns[2].Width = 150;
                    dgvDatos.Columns[3].Width = 75;
                    dgvDatos.Columns[0].Visible = false; ;
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

        //FUNCION PARA CONTAR LOS REGISTROS DUPLICADOS
        private int contarRegistros()
        {
            try
            {
                iBandera = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (txtCodigo.Text.Trim() == dgvDatos.Rows[i].Cells[1].Value.ToString())
                    {
                        iBandera = 1;
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

        //FUNCION PARA INSERTAR UN REGISTRO 
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into cel_tipo_ambiente (" + Environment.NewLine;
                sSql += "codigo, nombres, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim().ToUpper() + "', '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro agregado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(0);
                limpiar(0);
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR UN REGISTRO 
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cel_tipo_ambiente set" + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "nombres = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_tipo_ambiente = " + iIdRegistro;

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(0);
                limpiar(0);
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ELIMINAR UN REGISTRO 
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cel_tipo_ambiente set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_tipo_ambiente = " + iIdRegistro;

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(0);
                limpiar(0);
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar(int iOp)
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();

            if (iOp == 0)
            {
                txtBuscar.Clear();
            }

            cmbEstado.SelectedIndex = 0;
            bActualizar = false;
            btnAnular.Enabled = false;
            txtCodigo.Enabled = false;
            cmbEstado.Enabled = false;
            llenarGrid(0);
            iIdRegistro = 0;
            txtBuscar.Focus();
        }

        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar(0);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiar(1);

            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
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
                if (bActualizar == true)
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

                else
                {
                    iCuenta = contarRegistros();

                    if (iCuenta == 1)
                    {
                        ok.lblMensaje.Text = "Ya existe el código ingresado en un registro. Favor ingrese un nuevo código.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                    }

                    else if (iCuenta == 0)
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea guardar el registro...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (iIdRegistro == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro para eliminar.";
                ok.ShowDialog();
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

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();

                txtCodigo.Enabled = false;
                cmbEstado.Enabled = true;
                btnAnular.Enabled = true;
                cmbEstado.Enabled = true;
                bActualizar = true;
                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmTipoAmbiente_Load(object sender, EventArgs e)
        {
            limpiar(0);
        }
    }
}
