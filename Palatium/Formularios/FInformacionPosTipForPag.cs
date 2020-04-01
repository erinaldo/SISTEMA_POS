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
    public partial class FInformacionPosTipForPag : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dtTipoDocumento;
        string sEstado = "";
        string T_st_sql = "";
        string[] t_st_datos = new string[2];
        bool x = false; //creamos la variable
        
        string sSql;
        string sFecha;
        DataTable dtConsulta;
        bool bRespuesta;

        int iLeePropina;
        int iIdFormaCobro;
        int iHabilitado;

        public FInformacionPosTipForPag()
        {
            InitializeComponent();
        }

        private void FInformacionPosTipForPag_Load(object sender, EventArgs e)
        {
            llenarGrid();
            FListarTipoDocumento();
            llenarMetodosPago();
            llenarTipoVenta();
        }

        #region FUNCIONES DEL USUARIO

       //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            cmbTipoDocumento.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;
            cmbTipoVenta.SelectedIndex = 0;

            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            iHabilitado = 0;
            iIdFormaCobro = 0;
            iLeePropina = 0;
            txtRuta.Clear();

            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

            if (imgLogo.Image != null)
            {
                imgLogo.Image.Dispose();
                imgLogo.Image = null;
            }

            llenarGrid();
        }

        //LLENAR COMBO DE TIPO DE IDENTIFICACICON
        private void FListarTipoDocumento()
        {
            try
            {
                dtTipoDocumento = new DataTable();
                sSql = "";
                sSql += "select correlativo, codigo + ' - ' + valor_texto" + Environment.NewLine;
                sSql += "from tp_vw_tipo_documento_cobro";

                cmbTipoDocumento.llenar(dtTipoDocumento, sSql);

                if (cmbTipoDocumento.Items.Count > 0)
                {
                    cmbTipoDocumento.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE METODOS DE PAGO
        private void llenarMetodosPago()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_pos_metodo_pago, codigo + ' - ' + descripcion as descripcion" + Environment.NewLine;
                sSql += "from pos_metodo_pago" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbMetodoPago.llenar(dtConsulta, sSql);

                if (cmbMetodoPago.Items.Count > 0)
                {
                    cmbMetodoPago.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE TIPOS DE VENTA
        private void llenarTipoVenta()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_pos_tipo_venta, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_venta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbTipoVenta.llenar(dtConsulta, sSql);

                if (cmbTipoVenta.Items.Count > 0)
                {
                    cmbTipoVenta.SelectedIndex = 0;
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
                sSql += "SELECT FC.codigo CÓDIGO, FC.descripcion DESCRIPCION," + Environment.NewLine;
                //sSql += "case FC.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO,"+ Environment.NewLine;
                sSql += "case FC.is_active when 1 then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "FC.cg_tipo_documento, ISNULL(DC.codigo + ' - ' + DC.valor_texto, 'NINGUN REGISTRO') AS 'TIPO DE DOCUMENTO'," + Environment.NewLine;
                sSql += "case FC.lee_propina when 1 then 'SI' else 'NO' end 'LEE PROPINA'," + Environment.NewLine;
                sSql += "FC.id_pos_tipo_forma_cobro, isnull(FC.imagen, '') imagen," + Environment.NewLine;
                sSql += "FC.id_pos_metodo_pago, id_pos_tipo_venta, isnull(FC.is_active, 0) is_active" + Environment.NewLine;
                sSql += "FROM pos_tipo_forma_cobro AS FC LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "tp_vw_tipo_documento_cobro AS DC ON FC.cg_tipo_documento = DC.correlativo" + Environment.NewLine;
                sSql += "and FC.estado ='A'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "where FC.codigo LIKE '%' + '" + txtBuscar.Text.Trim() + "' + %" + Environment.NewLine;
                    sSql += "OR FC.descripcion LIKE '%' + '" + txtBuscar.Text.Trim() + "' + %" + Environment.NewLine;
                }

                sSql += "order by FC.codigo" + Environment.NewLine;


                dtConsulta = new DataTable();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Refresh();
                    dgvDatos.Columns[0].Width = 70;
                    dgvDatos.Columns[1].Width = 200;
                    dgvDatos.Columns[2].Width = 70;
                    dgvDatos.Columns[4].Width = 200;
                    dgvDatos.Columns[5].Width = 80;
                    dgvDatos.Columns[3].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[7].Visible = false;
                    dgvDatos.Columns[8].Visible = false;
                    dgvDatos.Columns[9].Visible = false;
                    dgvDatos.Columns[10].Visible = false;
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

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                //insert into pos_tipo_forma_cobro (codigo,descripcion,lee_propina,estado,fecha_ingreso,usuario_ingreso,terminal_ingreso)"

                sSql = "";
                sSql += "insert into pos_tipo_forma_cobro (codigo, descripcion," + Environment.NewLine;
                sSql += "lee_propina, id_pos_metodo_pago, cg_tipo_documento, imagen, id_pos_tipo_venta," + Environment.NewLine;
                sSql += "is_active, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "', " + Environment.NewLine;
                sSql += iLeePropina + ", " + Convert.ToInt32(cmbMetodoPago.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbTipoDocumento.SelectedValue) + ", '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbTipoVenta.SelectedValue) + ", 1, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
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

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }
                
                sSql = "";
                sSql += "update pos_tipo_forma_cobro set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "lee_propina = " + iLeePropina + "," + Environment.NewLine;
                sSql += "imagen = '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "id_pos_metodo_pago = " + Convert.ToInt32(cmbMetodoPago.SelectedValue) + "," + Environment.NewLine;
                sSql += "cg_tipo_documento = " + Convert.ToInt32(cmbTipoDocumento.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_tipo_venta = " + Convert.ToInt32(cmbTipoVenta.SelectedValue) + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_tipo_forma_cobro = " + iIdFormaCobro;
                
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
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

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "update pos_tipo_forma_cobro set" + Environment.NewLine;
                sSql += "is_active = 0" + Environment.NewLine;
                //sSql += "codigo = '" + txtCodigo.Text.Trim() + "(" + iIdFormaCobro.ToString() + ")'," + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                //sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                //sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_tipo_forma_cobro = " + iIdFormaCobro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
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

        #endregion

        private void Txt_Codigo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                sSql = "";
                sSql += "select descripcion, is_active" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where codigo = '" + txtCodigo.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0) //contar cuantos registros me devuelve el datatable
                    {
                        txtDescripcion.Text = dtConsulta.Rows[0][0].ToString();

                        if (Convert.ToInt32(dtConsulta.Rows[0]["is_active"].ToString()) == 1)
                            chkHabilitado.Checked = true;
                        else
                            chkHabilitado.Checked = false;

                        btnAnular.Enabled = true;
                        btnNuevo.Text = "Actualizar";
                        txtCodigo.Enabled = false;
                        txtBuscar.Focus();
                    }

                    else
                    {
                        txtDescripcion.Focus();
                        btnNuevo.Text = "Guardar";
                        btnAnular.Enabled = false;
                        chkHabilitado.Checked = true;
                        chkHabilitado.Enabled = false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void Txt_CodigoPosTipForPag_Leave(object sender, EventArgs e)
        {
            txtCodigo.LostFocus += new EventHandler(Txt_Codigo_LostFocus);
        }

        private void Btn_CerrarPosTipForPag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarPosTipForPag_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void Btn_BuscarPosTipForPag_Click(object sender, EventArgs e)
        {
            try
            {
                llenarGrid();
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                llenarGrid();
            }
        }

        private void BtnNuevoPosTipForPag_Click(object sender, EventArgs e)
        {
            
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR O ACTUALIZAR
            else
            {
                if (chkPropina.Checked == true)
                {
                    iLeePropina = 1;
                }

                else
                {
                    iLeePropina = 0;
                }

                if ((txtCodigo.Text == "") && (txtDescripcion.Text == ""))
                {
                    ok.LblMensaje.Text = "Debe rellenar todos los campos obligatorios.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtCodigo.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código del tipo forma de pago.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la descripción del tipo forma de pago.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (Convert.ToInt32(cmbTipoDocumento.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione el tipo de documento para el registro.";
                    ok.ShowDialog();
                }

                else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione el método de pago para el registro.";
                    ok.ShowDialog();
                }

                else
                {
                    if (chkHabilitado.Checked == true)
                        iHabilitado = 1;
                    else
                        iHabilitado = 0;

                    if (btnNuevo.Text == "Guardar")
                    {
                        insertarRegistro();
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        actualizarRegistro();
                    }

                }
            }
        }

        private void Btn_AnularPosTipForPag_Click(object sender, EventArgs e)
        {
            if (iIdFormaCobro == 0)
            {
                ok.LblMensaje.Text = "No ha seleccionado ningún registro para eliminar.";
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

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            //abrir.InitialDirectory = "c:\\";
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
                imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
            imgLogo.Image.Dispose();
            imgLogo.Image = null;
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Actualizar";
                txtCodigo.Enabled = false;
                chkHabilitado.Enabled = true;

                iIdFormaCobro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                cmbMetodoPago.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[8].Value.ToString());
                cmbTipoVenta.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[9].Value.ToString());

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[10].Value.ToString()) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                if ((dgvDatos.CurrentRow.Cells[3].Value.ToString() == null) || (dgvDatos.CurrentRow.Cells[3].Value.ToString() == ""))
                {
                    cmbTipoDocumento.SelectedValue = "0";
                }
                else
                {
                    cmbTipoDocumento.SelectedValue = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                }

                if (dgvDatos.CurrentRow.Cells[5].Value.ToString() == "SI")
                {
                    chkPropina.Checked = true;
                }

                else
                {
                    chkPropina.Checked = false;
                }

                txtRuta.Text = dgvDatos.CurrentRow.Cells[7].Value.ToString();

                if (txtRuta.Text.Trim() == "")
                {
                    if (imgLogo.Image != null)
                    {
                        imgLogo.Image.Dispose();
                        imgLogo.Image = null;
                    }
                }

                else
                {
                    if (File.Exists(txtRuta.Text.Trim()))
                    {
                        imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                        imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    else
                    {
                        imgLogo.Image = null;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
