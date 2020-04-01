using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Impresoras
{
    public partial class frmImpresionComanda : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        DataTable dtConsulta;

        string sSql;

        bool bRespuesta;

        int iIdImpresionComanda;

        public frmImpresionComanda()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL DATAGRIDVIEW
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_impresion_comanda as 'ID', codigo as CÓDIGO, descripcion as DESCRIPCION," + Environment.NewLine;
                sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO, isnull(id_pos_impresora, 0)" + Environment.NewLine;
                sSql += "from pos_impresion_comanda" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or descripcion like '%" + txtBuscar.Text.Trim() + "%')";
                }

                sSql += "order by id_pos_impresion_comanda";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[1].Width = 60;
                    dgvDatos.Columns[2].Width = 185;
                    dgvDatos.Columns[3].Width = 60;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[4].Visible = false;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                lblRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados";
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE IMPRESORAS
        private void llenarComboImpresoras()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_impresora," + Environment.NewLine;
                sSql += "descripcion + ' - (' + path_url + ')' as impresora" + Environment.NewLine;
                sSql += "from pos_impresora" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbImpresoras.llenar(dtConsulta, sSql);

                if (cmbImpresoras.Items.Count > 0)
                {
                    cmbImpresoras.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            grupoDatos.Enabled = false;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtEstado.Text = "ACTIVO";

            llenarGrid(0);
            llenarComboImpresoras();
            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            txtCodigo.Enabled = true;
            txtBuscar.Focus();
        }

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_impresion_comanda (" + Environment.NewLine;
                sSql += "codigo, descripcion, id_pos_impresora, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbImpresoras.SelectedValue) + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA ACTUALIZAR EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "update pos_impresion_comanda set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "id_pos_impresora = " + Convert.ToInt32(cmbImpresoras.SelectedValue) + Environment.NewLine;
                sSql += "where id_pos_impresion_comanda = " + iIdImpresionComanda + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA ELIMINAR EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "update pos_impresion_comanda set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_impresion_comanda = " + iIdImpresionComanda;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA CONSULTAR SI EL REGISTRO YA SE UTILIZÓ EN UNA TRANSACCION
        private int consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_pos_impresion_comanda = " + iIdImpresionComanda + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        private void frmImpresionComanda_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Nuevo")
            {
                grupoDatos.Enabled = true;
                txtCodigo.Focus();
                btnAgregar.Text = "Guardar";
            }

            else
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código del registro.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la descripción del registro.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (Convert.ToInt32(cmbImpresoras.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione la impresora.";
                    ok.ShowDialog();
                    cmbImpresoras.Focus();
                }

                else
                {
                    if (btnAgregar.Text == "Guardar")
                    {
                        insertarRegistro();
                    }

                    else if (btnAgregar.Text == "Actualizar")
                    {
                        actualizarRegistro();
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdImpresionComanda == 0)
            {
                ok.LblMensaje.Text = "No hay un registro seleccionado para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                if (consultarRegistro() > 0)
                {
                    ok.LblMensaje.Text = "Existen registros relacionados con la selección. No puede eliminar el registro.";
                    ok.ShowDialog();
                }

                else if (consultarRegistro() == -1)
                {
                    ok.LblMensaje.Text = "Existe un error al consultar si el registro se utilizó en otros registros.";
                    ok.ShowDialog();
                }

                else
                {
                    SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        eliminarRegistro();
                    }
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
            iIdImpresionComanda = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtEstado.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            cmbImpresoras.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[4].Value);
            txtCodigo.Enabled = false;
            btnAgregar.Text = "Actualizar";
            btnEliminar.Enabled = true;
            grupoDatos.Enabled = true;
            txtDescripcion.Focus();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
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
        }
    }
}
