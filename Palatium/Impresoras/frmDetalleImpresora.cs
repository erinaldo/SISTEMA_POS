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
    public partial class frmDetalleImpresora : MaterialForm
    {
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        DataTable dtConsulta;

        string sSql;

        bool bRespuesta;

        int iCortarPapel;
        int iAbrirCajon;
        int iIdPosCanalImpresion;

        public frmDetalleImpresora()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION  PARA LIMPIAR
        private void limpiar()
        {
            llenarComboLocalidades();
            llenarComboLocalidadesRegistro();
            llenarComboImpresoras();
            llenarGrid(0);

            iCortarPapel = 0;
            iAbrirCajon = 0;

            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtBuscar.Clear();
            txtNumeroImpresiones.Clear();
            chkAbrirCajon.Checked = false;
            chkCortarPapel.Checked = false;

            grupoDatos.Enabled = false;
            btnAgregar.Text = "Nuevo";

            txtBuscar.Focus();
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

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_localidad, nombre_localidad from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbLocalidad.llenar(dtConsulta, sSql);

                if (cmbLocalidad.Items.Count > 0)
                {
                    cmbLocalidad.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE LOCALIDADES
        private void llenarComboLocalidadesRegistro()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_localidad,nombre_localidad from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbLocalidades.llenar(dtConsulta, sSql);

                if (cmbLocalidades.Items.Count > 0)
                {
                    cmbLocalidades.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select CI.id_pos_canal_impresion, CI.id_localidad," + Environment.NewLine;
                sSql += "CI.codigo, I.descripcion as DESCRIPCIÓN," + Environment.NewLine;
                sSql += "I.path_url as 'URL IMPRESORA', CI.cortar_papel," + Environment.NewLine;
                sSql += "CI.abrir_cajon, isnull(CI.id_pos_impresora, 0) id_pos_impresora," + Environment.NewLine;
                sSql += "CI.numero_impresion" + Environment.NewLine;
                sSql += "from pos_canal_impresion CI, pos_impresora I" + Environment.NewLine;
                sSql += "where CI.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql += "and I.estado = 'A'" + Environment.NewLine;
                sSql += "and CI.estado = 'A'" + Environment.NewLine;
                sSql += "and CI.id_localidad = " + Convert.ToInt32(cmbLocalidad.SelectedValue) + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "(and CI.codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or I.codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or I.descrpcion like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by CI.id_pos_canal_impresion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;

                    dgvDatos.Columns[3].Width = 100;
                    dgvDatos.Columns[4].Width = 200;

                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[1].Visible = false;
                    dgvDatos.Columns[2].Visible = false;
                    dgvDatos.Columns[5].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[7].Visible = false;
                    dgvDatos.Columns[8].Visible = false;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                lblRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados";
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
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
                sSql += "insert into pos_canal_impresion (" + Environment.NewLine;
                sSql += "id_localidad, codigo, numero_impresion," + Environment.NewLine;
                sSql += "cortar_papel, abrir_cajon, id_pos_impresora," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Convert.ToInt32(cmbLocalidades.SelectedValue) + ", '" + txtCodigo.Text.Trim() + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(txtNumeroImpresiones.Text.Trim()) + ", " + iCortarPapel + "," + Environment.NewLine;
                sSql += iAbrirCajon + ", " + Convert.ToInt32(cmbImpresoras.SelectedValue) + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

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

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }


        //FUNCION PARA ACTUALIZAR EL REGISTRO
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
                sSql += "update pos_canal_impresion set" + Environment.NewLine;
                sSql += "id_localidad = " + Convert.ToInt32(cmbLocalidades.SelectedValue) + "," + Environment.NewLine;
                sSql += "numero_impresion = " + Convert.ToInt32(txtNumeroImpresiones.Text.Trim()) + "," + Environment.NewLine;
                sSql += "cortar_papel = " + iCortarPapel + "," + Environment.NewLine;
                sSql += "abrir_cajon = " + iAbrirCajon + "," + Environment.NewLine;
                sSql += "id_pos_impresora = " + Convert.ToInt32(cmbImpresoras.SelectedValue) + Environment.NewLine;
                sSql += "where id_pos_canal_impresion = " + iIdPosCanalImpresion + Environment.NewLine;
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
                sSql = sSql + "update pos_canal_impresion set" + Environment.NewLine;
                sSql = sSql + "estado = 'E'," + Environment.NewLine;
                sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql = sSql + "where id_pos_impresora = " + iIdPosCanalImpresion;

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

        #endregion


        private void frmDetalleImpresora_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            llenarGrid(0);
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
                if (Convert.ToInt32(cmbLocalidades.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione la localidad.";
                    ok.ShowDialog();
                    cmbLocalidades.Focus();
                }

                else if (txtCodigo.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código del registro.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (Convert.ToInt32(cmbImpresoras.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione la impresora.";
                    ok.ShowDialog();
                    cmbLocalidades.Focus();
                }                

                else if (txtNumeroImpresiones.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor seleccione el path de ubicación de la impresora.";
                    ok.ShowDialog();
                    txtNumeroImpresiones.Focus();
                }

                else
                {
                    if (chkCortarPapel.Checked == true)
                    {
                        iCortarPapel = 1;
                    }

                    else
                    {
                        iCortarPapel = 0;
                    }


                    if (chkAbrirCajon.Checked == true)
                    {
                        iAbrirCajon = 1;
                    }

                    else
                    {
                        iAbrirCajon = 0;
                    }


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

        private void txtNumeroImpresiones_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdPosCanalImpresion == 0)
            {
                ok.LblMensaje.Text = "No hay un registro seleccionado para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                //if (consultarRegistro() > 0)
                //{
                //    ok.LblMensaje.Text = "Existen registros relacionados con la selección. No puede eliminar el registro.";
                //    ok.ShowDialog();
                //}

                //else if (consultarRegistro() == -1)
                //{
                //    ok.LblMensaje.Text = "Existe un error al consultar si el registro se utilizó en otros registros.";
                //    ok.ShowDialog();
                //}

                //else
                //{
                SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar el registro seleccionado?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
                //}
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdPosCanalImpresion = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
            cmbLocalidades.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[1].Value);
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[5].Value) == 1)
            {
                chkCortarPapel.Checked = true;
            }

            else
            {
                chkCortarPapel.Checked = false;
            }

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value) == 1)
            {
                chkAbrirCajon.Checked = true;
            }

            else
            {
                chkAbrirCajon.Checked = false;
            }

            cmbImpresoras.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[7].Value);
            txtNumeroImpresiones.Text = dgvDatos.CurrentRow.Cells[8].Value.ToString();

            grupoDatos.Enabled = true;
            txtCodigo.Enabled = false;
            txtDescripcion.Enabled = false;
            btnAgregar.Text = "Actualizar";

            cmbImpresoras.Focus();
        }
    }
}
