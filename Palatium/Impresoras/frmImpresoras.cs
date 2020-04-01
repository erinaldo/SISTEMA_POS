using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Impresoras
{
    public partial class frmImpresoras : MaterialForm
    {
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;

        DataTable dtConsulta;

        int iIdImpresora;
        int iCortarPapel;
        int iAbrirCajon;

        bool bRespuesta;

        public frmImpresoras()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_localidad,nombre_localidad from tp_vw_localidades";

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
                    cmbLocalidades.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBOBOX DE IMPRESORAS
        private void impresorasCombo()
        {
            try
            {
                String pkInstalledPrinters;
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cmbImpresora.Items.Add(pkInstalledPrinters);
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtBuscar.Clear();
            txtCodigo.Clear();
            cmbImpresora.SelectedIndex = 0;
            txtDescripcion.Clear();
            txtPath.Clear();
            txtPuertoImpresora.Clear();
            txtIpImpresora.Clear();
            txtNumeroImpresiones.Clear();
            txtEstado.Text = "ACTIVO";
            iIdImpresora = 0;

            grupoDatos.Enabled = false;
            llenarGrid(0);

            llenarComboLocalidades();
            llenarComboLocalidadesRegistro();

            chkCortarPapel.Checked = false;
            chkAbrirCajon.Checked = false;
            iCortarPapel = 0;
            iAbrirCajon = 0;
            iIdImpresora = 0;

            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            txtCodigo.Enabled = true;
            txtBuscar.Focus();
        }


        //FUNCION PARA LIMPIAR REGISTROS
        private void limpiarRegistro()
        {
            txtBuscar.Clear();
            txtCodigo.Clear();
            cmbImpresora.SelectedIndex = 0;
            txtDescripcion.Clear();
            txtPath.Clear();
            txtPuertoImpresora.Clear();
            txtIpImpresora.Clear();
            txtEstado.Text = "ACTIVO";
            iIdImpresora = 0;

            grupoDatos.Enabled = false;

            llenarComboLocalidadesRegistro();

            chkCortarPapel.Checked = false;
            chkAbrirCajon.Checked = false;
            iCortarPapel = 0;
            iAbrirCajon = 0;
            iIdImpresora = 0;

            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            txtCodigo.Enabled = true;
            txtBuscar.Focus();
        }

        //FUNCION PARA INSERTAR
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
                sSql = sSql + "insert into pos_impresora (" + Environment.NewLine;
                sSql = sSql + "codigo, descripcion, path_url, puerto_impresora," + Environment.NewLine;
                sSql = sSql + "ip_impresora, id_localidad, numero_impresion," + Environment.NewLine;
                sSql = sSql + "cortar_papel, abrir_cajon, estado, fecha_ingreso," + Environment.NewLine;
                sSql = sSql + "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'" + txtPath.Text.Trim() + "', '" + txtPuertoImpresora.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "'" + txtIpImpresora.Text.Trim() + "', " + Convert.ToInt32(cmbLocalidades.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(txtNumeroImpresiones.Text.Trim()) + ", " + iCortarPapel + "," + Environment.NewLine;
                sSql = sSql + iAbrirCajon + ", 'A', GETDATE()," + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

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
                goto reversa;
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
                sSql = sSql + "update pos_impresora set" + Environment.NewLine;
                sSql = sSql + "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "path_url = '" + txtPath.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "puerto_impresora = '" + txtPuertoImpresora.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "ip_impresora = '" + txtIpImpresora.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "id_localidad = " + Convert.ToInt32(cmbLocalidades.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + "numero_impresion = " + Convert.ToInt32(txtNumeroImpresiones.Text.Trim()) + "," + Environment.NewLine;
                sSql = sSql + "cortar_papel = " + iCortarPapel + "," + Environment.NewLine;
                sSql = sSql + "abrir_cajon = " + iAbrirCajon + Environment.NewLine; 
                sSql = sSql + "where id_pos_impresora = " + iIdImpresora + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

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
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
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
                sSql = sSql + "update pos_impresora set" + Environment.NewLine;
                sSql = sSql + "estado = 'E'," + Environment.NewLine;
                sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql = sSql + "where id_pos_impresora = " + iIdImpresora;

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
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

        }

        //FUNCION PARA LLENAR EL DATAGRIDVIEW
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_impresora as 'ID', codigo as CÓDIGO, descripcion as DESCRIPCIÓN," + Environment.NewLine;
                sSql = sSql + "path_url, puerto_impresora,  ip_impresora, cortar_papel, abrir_cajon," + Environment.NewLine;
                sSql = sSql + "numero_impresion, id_localidad," + Environment.NewLine;
                sSql = sSql + "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO" + Environment.NewLine;
                sSql = sSql + "from pos_impresora" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and id_localidad = " + Convert.ToInt32(cmbLocalidad.SelectedValue) + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql = sSql + "and (codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql = sSql + "or descripcion like '%" + txtBuscar.Text.Trim() + "%')";
                }

                sSql = sSql + "order by id_pos_impresora";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[1].Width = 60;
                    dgvDatos.Columns[2].Width = 160;
                    dgvDatos.Columns[3].Width = 90;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[3].Visible = false;
                    dgvDatos.Columns[4].Visible = false;
                    dgvDatos.Columns[5].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[7].Visible = false;
                    dgvDatos.Columns[8].Visible = false;
                    dgvDatos.Columns[9].Visible = false;
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

        #endregion

        private void frmImpresoras_Load(object sender, EventArgs e)
        {
            impresorasCombo();
            limpiar();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() != "")
            {
                string sAuxiliar = txtPath.Text.Trim();
                txtPath.Text = @"\\" + Environment.MachineName.ToString() + @"\" + sAuxiliar;
            }
        }

        private void cmbImpresora_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPath.Text = cmbImpresora.Text;
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

                else if (txtDescripcion.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la descripción del registro.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (txtPath.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor seleccione el path de ubicación de la impresora.";
                    ok.ShowDialog();
                    txtPath.Focus();
                }

                else if (txtNumeroImpresiones.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el número de impresiones.";
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdImpresora == 0)
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

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdImpresora = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtPath.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            txtPuertoImpresora.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
            txtIpImpresora.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();

            //CORTAR PAPEL
            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value) == 1)
            {
                chkCortarPapel.Checked = true;
            }

            else
            {
                chkCortarPapel.Checked = false;
            }

            //ABRIR CAJON
            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[7].Value) == 1)
            {
                chkAbrirCajon.Checked = true;
            }

            else
            {
                chkAbrirCajon.Checked = false;
            }

            txtNumeroImpresiones.Text = dgvDatos.CurrentRow.Cells[8].Value.ToString();
            cmbLocalidades.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[9].Value);

            txtEstado.Text = dgvDatos.CurrentRow.Cells[10].Value.ToString();
            txtCodigo.Enabled = false;
            btnAgregar.Text = "Actualizar";
            btnEliminar.Enabled = true;
            grupoDatos.Enabled = true;
            txtDescripcion.Focus();
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarRegistro();
            llenarGrid(0);
        }
    }
}
