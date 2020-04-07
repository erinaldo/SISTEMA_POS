using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmCambioMasivoPropinaProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter;
        Productos.ClaseActualizarPreciosProductos procesar;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sFechaListaMinorista;
        string sFechaInicio;

        DataTable dtConsulta;

        bool bRespuesta;
        bool bPagaIva;
        bool bPagaServicio;

        int iIdListaPrecioMinorista;
        int iPagaIva;
        int iPagaServicio;

        Decimal dbValorOriginal;
        Decimal dbValorRecuperado;
        Decimal dbValorIva;
        Decimal dbValorServicio;
        Decimal dbValorNuevo;
        Decimal dbFactor;
        Decimal dbValorImpuesto;

        SqlParameter[] parametro;

        public frmCambioMasivoPropinaProductos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LA FECHA DEL SISTEMA
        private void fechaSistema()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    this.Cursor = Cursors.Default;
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFechaInicio = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER EL ID_LISTA_PRECIO MINORISTA
        private void obtenerIdListaMinorista()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and lista_minorista = @lista_minorista";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@lista_minorista";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 1;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra configurado el registro de lista minorista.";
                    ok.ShowDialog();
                    return;
                }

                iIdListaPrecioMinorista = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                sFechaListaMinorista = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_fin_validez"].ToString()).ToString("yyyy/MM/dd");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE CATEGORIAS
        private void llenarComboCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = @estado_1" + Environment.NewLine;
                sSql += "and NP.estado = @estado_2" + Environment.NewLine;
                sSql += "where P.nivel = @nivel" + Environment.NewLine;
                sSql += "and P.is_active = @is_active" + Environment.NewLine;
                sSql += "order by NP.nombre";

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@nivel";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = 2;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@is_active";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = 1;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtConsulta.NewRow();
                row["id_producto"] = "0";
                row["nombre"] = "Seleccione la categoría...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbCategorias.ValueMember = "id_producto";
                cmbCategorias.DisplayMember = "nombre";
                cmbCategorias.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_productos_por_categoria" + Environment.NewLine;
                sSql += "where id_lista_precio = @id_lista_precio" + Environment.NewLine;
                sSql += "and id_producto_padre = @id_producto_padre" + Environment.NewLine;
                sSql += "order by nombre";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_lista_precio";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdListaPrecioMinorista;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto_padre";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Convert.ToInt32(cmbCategorias.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    btnGuardar.Enabled = false;
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dbFactor = 0;
                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());
                    iPagaServicio = Convert.ToInt32(dtConsulta.Rows[i]["paga_servicio"].ToString());
                    dbValorRecuperado = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());

                    if (iPagaIva == 1)
                        bPagaIva = true;
                    else
                        bPagaIva = false;

                    if (iPagaServicio == 1)
                        bPagaServicio = true;
                    else
                        bPagaServicio = false;

                    if (Program.iCobrarConSinProductos == 0)
                    {
                        dbValorOriginal = dbValorRecuperado;
                    }

                    else
                    {
                        if (iPagaIva == 1)
                            dbFactor += Convert.ToDecimal(Program.iva);

                        if (iPagaServicio == 1)
                            dbFactor += Convert.ToDecimal(Program.servicio);

                        dbValorImpuesto = dbValorRecuperado * dbFactor;
                        dbValorOriginal = dbValorRecuperado + dbValorImpuesto;
                    }

                    dgvDatos.Rows.Add(false,
                                          dtConsulta.Rows[i]["id_producto"].ToString(),
                                          dbValorRecuperado,
                                          dtConsulta.Rows[i]["nombre"].ToString().Trim().ToString(),
                                          dbValorOriginal.ToString("N2"),
                                          bPagaIva,
                                          bPagaServicio, "0.00");
                }

                cmbCategorias.Enabled = false;
                btnOK.Enabled = false;
                btnGuardar.Enabled = true;
                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter = new Clases.ClaseValidarCaracteres();
            caracter.soloDecimales(sender, e, 2);
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbCategorias.Enabled = true;
            btnOK.Enabled = true;
            llenarComboCategorias();
            dgvDatos.Rows.Clear();
        }
        
        //FUNCION PARA PROCESAR LA ACTUALIZACION
        private void procesarActualizacion()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                dtConsulta.Columns.Add("id_producto");
                dtConsulta.Columns.Add("paga_iva");
                dtConsulta.Columns.Add("paga_servicio");
                dtConsulta.Columns.Add("valor");

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["actualizar"].Value) == true)
                    {
                        if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["paga_iva"].Value) == true)
                            iPagaIva = 1;
                        else
                            iPagaIva = 0;

                        if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["paga_servicio"].Value) == true)
                            iPagaServicio = 1;
                        else
                            iPagaServicio = 0;

                        DataRow row = dtConsulta.NewRow();
                        row["id_producto"] = dgvDatos.Rows[i].Cells["id_producto"].Value;
                        row["paga_iva"] = iPagaIva;
                        row["paga_servicio"] = iPagaServicio;
                        row["valor"] = dgvDatos.Rows[i].Cells["valor_nuevo"].Value;

                        dtConsulta.Rows.Add(row);
                    }
                }

                procesar = new ClaseActualizarPreciosProductos();

                bRespuesta = procesar.actualizarPrecios(dtConsulta, Program.iCobrarConSinProductos, iIdListaPrecioMinorista,
                                                        Convert.ToDecimal(Program.iva), Convert.ToDecimal(Program.servicio),
                                                        sFechaInicio, sFechaListaMinorista, Program.sDatosMaximo[0],
                                                        Program.sDatosMaximo[1]);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = procesar.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registros procesados éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmCambioMasivoPropinaProductos_Load(object sender, EventArgs e)
        {
            if (Program.iCobrarConSinProductos == 0)
            {
                rdbGuardaSinImpuestos.Checked = true;
                rdbGuardaConImpuestos.Checked = false;
            }

            else
            {
                rdbGuardaSinImpuestos.Checked = false;
                rdbGuardaConImpuestos.Checked = true;
            }

            obtenerIdListaMinorista();
            llenarComboCategorias();
        }

        private void dgvDatos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox texto = e.Control as TextBox;

            if (texto != null)
            {
                DataGridViewTextBoxEditingControl dTexto = (DataGridViewTextBoxEditingControl)e.Control;
                dTexto.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
                dTexto.KeyPress += new KeyPressEventHandler(dText_KeyPress);
            }
        }

        private void dgvDatos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.Columns[e.ColumnIndex].Name == "valor_nuevo")
                {
                    if (dgvDatos.Rows[e.RowIndex].Cells["valor_nuevo"].Value == null)
                        dgvDatos.Rows[e.RowIndex].Cells["valor_nuevo"].Value = "0";

                    if (dgvDatos.Rows[e.RowIndex].Cells["valor_nuevo"].Value.ToString() == "")
                        dgvDatos.Rows[e.RowIndex].Cells["valor_nuevo"].Value = "0";

                    Decimal dbValor = Convert.ToDecimal(dgvDatos.Rows[e.RowIndex].Cells["valor_nuevo"].Value);

                    if (dbValor == 0)
                        dgvDatos.Rows[e.RowIndex].Cells["actualizar"].Value = false;
                    else
                        dgvDatos.Rows[e.RowIndex].Cells["actualizar"].Value = true;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCategorias.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la categoría a buscar.";
                ok.ShowDialog();
                return;
            }

            llenarGrid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iSuma = 0;

            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvDatos.Rows[i].Cells["actualizar"].Value) == true)
                    iSuma++;
            }

            if (iSuma == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay registros modificados para procesar.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea procesar la información?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                fechaSistema();
                procesarActualizacion();
            }
        }
    }
}
