using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmDirectorios : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        DataTable dtConsulta;

        bool bRespuesta;
        bool bActualizar = false;

        string sSql;

        int iIdDirectorio;
        int iIdComprobante;

        public frmDirectorios()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS TIPOS DE AMBIENTE
        private void llenarTipoComprobante()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_comprobante, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_comprobante";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTipoComprobante.DisplayMember = "nombres";
                        cmbTipoComprobante.ValueMember = "id_tipo_comprobante";
                        cmbTipoComprobante.DataSource = dtConsulta;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "Select D.orden 'No.', D.Codigo 'Código', D.Nombres," + Environment.NewLine;
                sSql += "case (D.Estado) when 'A' then 'ACTIVO' else 'INACTIVO' end Estado," + Environment.NewLine;
                sSql += "D.id_directorio" + Environment.NewLine;
                sSql += "From cel_tipo_comprobante C, cel_directorio D" + Environment.NewLine;
                sSql += "Where D.id_tipo_comprobante = C.id_tipo_comprobante" + Environment.NewLine;
                sSql += "and D.Id_tipo_comprobante = " + Convert.ToInt32(cmbTipoComprobante.SelectedValue) + Environment.NewLine;
                sSql += "and D.Estado ='A'" + Environment.NewLine;
                sSql += "and C.Estado ='A'" + Environment.NewLine;
                sSql += "order by D.orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        dgvDatos.Columns[0].Width = 40;
                        dgvDatos.Columns[1].Width = 150;
                        dgvDatos.Columns[2].Width = 180;
                        dgvDatos.Columns[3].Width = 65;
                        dgvDatos.Columns[4].Visible = false;
                    }
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

        //FUNCION PARA LIMPAR
        private void limpiar()
        {
            txtOrden.Clear();
            txtCodigo.Clear();
            txtDirectorio.Clear();
            txtEstado.Clear();
            iIdDirectorio = 0;
            bActualizar = false;
            txtEstado.Text = "ACTIVO";
            txtOrden.Focus();
        }

        //FUNCION PARA INSERTAR EL REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into cel_directorio(" + Environment.NewLine;
                sSql += "id_tipo_comprobante, orden, codigo," + Environment.NewLine;
                sSql += "nombres, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdComprobante + ", '" + txtOrden.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDirectorio.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTA EL QUERY DE INSERCION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro agregado éxitosamente.";
                ok.ShowDialog();
                llenarGrid();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "Update cel_directorio set" + Environment.NewLine;
                sSql += "orden = '" + txtOrden.Text.Trim() + "'," + Environment.NewLine;
                sSql += "Codigo = '" + txtCodigo.Text.Trim() + "'," + Environment.NewLine;
                sSql += "Nombres = '" + txtDirectorio.Text.Trim() + "'" + Environment.NewLine;
                //sSql += "Usuario_Ingreso = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "Terminal_Ingreso = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                //sSql += "Fecha_Ingreso = GetDate()" + Environment.NewLine;
                sSql += "Where id_directorio = " + iIdDirectorio;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                llenarGrid();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "Update cel_directorio set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()" + Environment.NewLine;
                sSql += "Where id_directorio = " + iIdDirectorio;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                llenarGrid();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        #endregion

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            if (fbRuta.ShowDialog() == DialogResult.OK)
            {
                txtDirectorio.Text = fbRuta.SelectedPath;
            }
        }

        private void frmDirectorios_Load(object sender, EventArgs e)
        {
            llenarTipoComprobante();
            txtEstado.Text = "ACTIVO";
        }

        private void cmbTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiar();
            llenarGrid();
            iIdComprobante = Convert.ToInt32(cmbTipoComprobante.SelectedValue);
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOrden.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtDirectorio.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtEstado.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            iIdDirectorio = Convert.ToInt32(dgvDatos.CurrentRow.Cells[4].Value.ToString());
            bActualizar = true;
            txtOrden.Focus();
            txtOrden.SelectionStart = txtOrden.Text.Trim().Length;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            llenarTipoComprobante();
            limpiar();            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtOrden.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el número de orden a mostrar.";
                ok.ShowDialog();
                txtOrden.Focus();
            }

            else if (txtCodigo.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el código para el registro.";
                ok.ShowDialog();
                txtCodigo.Focus();
            }

            else if (txtDirectorio.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor seleccione un directorio para el registro.";
                ok.ShowDialog();
                txtDirectorio.Focus();
            }

            else
            {
                if (bActualizar == false)
                {
                    SiNo.LblMensaje.Text = "¿Está seguro que dese guardar el registro para el tipo de comprobante: " + cmbTipoComprobante.Text + "?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        insertarRegistro();
                    }
                }

                else
                {
                    actualizarRegistro();
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (iIdDirectorio != 0)
            {
                SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No hay registros para eliminar.";
                ok.ShowDialog();
            }
        }

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }
    }
}
