using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmFormatosFactura : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        DataTable dtConsulta;
        bool bRespuesta = false;
        int iIdRegistro;

        public frmFormatosFactura()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtBusqueda.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            cmbEstado.Text = "ACTIVO";
            cmbEstado.Enabled = false;
            llenarGrid(0);
            llenarComboImpresoras();
            grupoDatos.Enabled = false;
            txtCodigo.ReadOnly = false;
            btnNuevo.Text = "Nuevo";
            iIdRegistro = 0;
            btnEliminar.Enabled = false;
            txtBusqueda.Focus();
        }

        //LLENAR COMBO DE IMPRESORAS
        private void llenarComboImpresoras()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_impresora, codigo + ' | ' + descripcion as descripcion" + Environment.NewLine;
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

        //FUNCION PARA CONSULTAR LOS REGISTROS
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_formato_factura, codigo as CÓDIGO," + Environment.NewLine;
                sSql = sSql +  "descripcion as DESCRIPCIÓN," + Environment.NewLine;
                sSql = sSql + "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO," + Environment.NewLine;
                sSql = sSql + "isnull(id_pos_impresora, 0)" + Environment.NewLine;
                sSql = sSql + "from pos_formato_factura" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'" + Environment.NewLine;

                if (op == 1)
                {
                    sSql = sSql + "(and codigo like '%' + '" + txtBusqueda.Text.Trim() + "' + '%' ";
                    sSql = sSql + "or descripcion like '%' + '" + txtBusqueda.Text.Trim() + "' + '%')";
                }

                sSql = sSql + " order by id_pos_formato_factura";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[1].Width = 70;
                    dgvDatos.Columns[2].Width = 120;
                    dgvDatos.Columns[3].Width = 70;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[4].Visible = false;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "insert into pos_formato_factura (" + Environment.NewLine;
                sSql = sSql + "codigo, descripcion, id_pos_impresora, estado, " + Environment.NewLine;
                sSql = sSql + "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values (" + Environment.NewLine;
                sSql = sSql + "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(cmbImpresoras.SelectedValue) + ", 'A', GETDATE()," + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //SE EJECUTA LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        //FUNCION PARA ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "update pos_formato_factura set" + Environment.NewLine;
                sSql = sSql + "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "id_pos_impresora = " + Convert.ToInt32(cmbImpresoras.SelectedValue) + Environment.NewLine;
                sSql = sSql + "where id_pos_formato_factura = " + iIdRegistro + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                //SE EJECUTA LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        //FUNCION PARA ELIMINAR
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "update pos_formato_factura set" + Environment.NewLine;
                sSql = sSql + "estado = 'E'," + Environment.NewLine;
                sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql = sSql + "where id_pos_formato_factura = " + iIdRegistro;

                //SE EJECUTA LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        #endregion

        private void frmFormatosFactura_Load(object sender, EventArgs e)
        {
            cmbEstado.Text = "ACTIVO";
            limpiar();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBusqueda.Text == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            cmbEstado.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            cmbImpresoras.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[4].Value);

            txtCodigo.ReadOnly = true;
            btnEliminar.Enabled = true;
            btnNuevo.Text = "Actualizar";
            grupoDatos.Enabled = true;
            txtDescripcion.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                grupoDatos.Enabled = true;
                txtCodigo.Focus();
                btnNuevo.Text = "Guardar";
            }

            else
            {
                if (txtCodigo.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese un código para el registro.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
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
                    if (btnNuevo.Text == "Guardar")
                    {
                        sSql = "";
                        sSql = sSql + "select count(*) cuenta" + Environment.NewLine;
                        sSql = sSql + "from pos_formato_factura" + Environment.NewLine;
                        sSql = sSql + "where codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                        sSql = sSql + "and estado = 'A'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                if (dtConsulta.Rows[0].ItemArray[0].ToString() == "0")
                                {
                                    //INSERTAR EL REGISTRO

                                    SiNo.LblMensaje.Text = "¿Está seguro que desea guardar la información?";
                                    SiNo.ShowDialog();

                                    if (SiNo.DialogResult == DialogResult.OK)
                                    {
                                        insertarRegistro();
                                    }
                                }

                                else
                                {
                                    ok.LblMensaje.Text = "Ya existe un registro con el código ingresado.";
                                    ok.ShowDialog();
                                }
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = sSql;
                            ok.ShowDialog();
                        }
                    }

                    else if (btnNuevo.Text == "Actualizar")
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
            if (iIdRegistro == 0)
            {
                ok.LblMensaje.Text = "No hay un registro seleccionado para eliminar.";
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

        private void btnBuscarCanImpre_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "")
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
