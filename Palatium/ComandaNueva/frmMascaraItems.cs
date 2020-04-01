using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmMascaraItems : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;

        DataTable dtConsulta;
        DataTable dtOrigen;
        public DataTable dt;

        bool bRespuesta;

        int iNumeroFila = -1;

        public frmMascaraItems(DataTable dtOrigen_P)
        {
            dtOrigen = dtOrigen_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE MASCARAS
        private void llenarCombo()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_mascara_item, descripcion" + Environment.NewLine;
                sSql += "from pos_mascara_item" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbMascaras.DisplayMember = "descripcion";
                    cmbMascaras.ValueMember = "id_pos_mascara_item";
                    cmbMascaras.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
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
                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    dgvPedido.Rows.Add(dtOrigen.Rows[i]["cantidad"].ToString().Trim(),
                                       dtOrigen.Rows[i]["nombre_producto"].ToString().Trim(),
                                       dtOrigen.Rows[i]["valor_unitario"].ToString().Trim(),
                                       dtOrigen.Rows[i]["valor_total"].ToString().Trim(),
                                       dtOrigen.Rows[i]["id_producto"].ToString().Trim(),
                                       dtOrigen.Rows[i]["paga_iva"].ToString().Trim(),
                                       dtOrigen.Rows[i]["codigo_producto"].ToString().Trim(),
                                       dtOrigen.Rows[i]["secuencia_impresion"].ToString().Trim(),
                                       dtOrigen.Rows[i]["bandera_cortesia"].ToString().Trim(),
                                       dtOrigen.Rows[i]["motivo_cortesia"].ToString().Trim(),
                                       dtOrigen.Rows[i]["bandera_descuento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["motivo_descuento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["id_mascara"].ToString().Trim(),
                                       dtOrigen.Rows[i]["id_ordenamiento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["ordenamiento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["porcentaje_descuento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["bandera_comentario"].ToString().Trim(),
                                       dtOrigen.Rows[i]["valor_descuento"].ToString().Trim(),
                                       dtOrigen.Rows[i]["nombre_producto"].ToString().Trim()
                                    );
                }

                dgvPedido.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA RECUPERAR LOS NOMBRES ORIGINALES DE LOS ITEMS
        private void recuperarNombresOriginales()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto, nombre" + Environment.NewLine;
                sSql += "from cv401_nombre_productos" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_producto in (";

                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    if (i + 1 == dtOrigen.Rows.Count)
                    {
                        sSql += dtOrigen.Rows[i]["id_producto"].ToString() + ")";
                        break;
                    }

                    else
                    {
                        sSql += dtOrigen.Rows[i]["id_producto"].ToString() + "," + Environment.NewLine;
                    }
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA CREAR EL DATATABLE DE RETORNO
        private void construirDataTableRetorno()
        {
            try
            {
                dt = new DataTable();
                dt.Clear();

                dt.Columns.Add("cantidad");
                dt.Columns.Add("nombre_producto");
                dt.Columns.Add("valor_unitario");
                dt.Columns.Add("valor_total");
                dt.Columns.Add("id_producto");
                dt.Columns.Add("paga_iva");
                dt.Columns.Add("codigo_producto");
                dt.Columns.Add("secuencia_impresion");
                dt.Columns.Add("bandera_cortesia");
                dt.Columns.Add("motivo_cortesia");
                dt.Columns.Add("bandera_descuento");
                dt.Columns.Add("motivo_descuento");
                dt.Columns.Add("id_mascara");
                dt.Columns.Add("id_ordenamiento");
                dt.Columns.Add("ordenamiento");
                dt.Columns.Add("porcentaje_descuento");
                dt.Columns.Add("bandera_comentario");
                dt.Columns.Add("valor_descuento");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSTRUIR EL DATATABLE
        private void retornarInformacion()
        {
            try
            {
                construirDataTableRetorno();

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row["cantidad"] = dgvPedido.Rows[i].Cells["cantidad"].Value.ToString();
                    row["nombre_producto"] = dgvPedido.Rows[i].Cells["nombre_producto"].Value.ToString();
                    row["valor_unitario"] = dgvPedido.Rows[i].Cells["valor_unitario"].Value.ToString();
                    row["valor_total"] = dgvPedido.Rows[i].Cells["valor_total"].Value.ToString();
                    row["id_producto"] = dgvPedido.Rows[i].Cells["id_producto"].Value.ToString();
                    row["paga_iva"] = dgvPedido.Rows[i].Cells["paga_iva"].Value.ToString();
                    row["codigo_producto"] = dgvPedido.Rows[i].Cells["codigo_producto"].Value.ToString();
                    row["secuencia_impresion"] = dgvPedido.Rows[i].Cells["secuencia_impresion"].Value.ToString();
                    row["bandera_cortesia"] = dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString();
                    row["motivo_cortesia"] = dgvPedido.Rows[i].Cells["motivo_cortesia"].Value.ToString();
                    row["bandera_descuento"] = dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString();
                    row["motivo_descuento"] = dgvPedido.Rows[i].Cells["motivo_descuento"].Value.ToString();
                    row["id_mascara"] = dgvPedido.Rows[i].Cells["id_mascara"].Value.ToString();
                    row["id_ordenamiento"] = dgvPedido.Rows[i].Cells["id_ordenamiento"].Value.ToString();
                    row["ordenamiento"] = dgvPedido.Rows[i].Cells["ordenamiento"].Value.ToString();
                    row["porcentaje_descuento"] = dgvPedido.Rows[i].Cells["porcentaje_descuento"].Value.ToString();
                    row["bandera_comentario"] = dgvPedido.Rows[i].Cells["bandera_comentario"].Value.ToString();
                    row["valor_descuento"] = dgvPedido.Rows[i].Cells["valor_descuento"].Value.ToString();

                    dt.Rows.Add(row);
                }

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        #endregion

        private void frmMascaraItems_Load(object sender, EventArgs e)
        {
            llenarCombo();
            recuperarNombresOriginales();
            llenarGrid();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dgvPedido.Rows[i].Cells["id_mascara"].Value = cmbMascaras.SelectedValue.ToString();
                    dgvPedido.Rows[i].Cells["nombre_producto"].Value = cmbMascaras.Text.ToUpper();
                }
            }

            else
            {
                if (dgvPedido.SelectedRows.Count > 0)
                {
                    int f = Convert.ToInt32(dgvPedido.CurrentRow.Index);
                    dgvPedido.Rows[f].Cells["id_mascara"].Value = cmbMascaras.SelectedValue.ToString();
                    dgvPedido.Rows[f].Cells["nombre_producto"].Value = cmbMascaras.Text.ToUpper();
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No ha seleccionado ninguna fila.";
                    ok.ShowDialog();
                    return;
                }
            }

            dgvPedido.ClearSelection();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    dgvPedido.Rows[i].Cells["id_mascara"].Value = "0";

                    DataRow[] dFila = dtConsulta.Select("id_producto = " + dgvPedido.Rows[i].Cells["id_producto"].Value.ToString().Trim());
                    
                    if (dFila.Length != 0)
                    {
                        dgvPedido.Rows[i].Cells["nombre_producto"].Value = dFila[0][1].ToString().Trim().ToUpper();
                    }
                }
            }

            else
            {
                if (dgvPedido.SelectedRows.Count > 0)
                {
                    iNumeroFila = dgvPedido.CurrentRow.Index;
                    dgvPedido.Rows[iNumeroFila].Cells["id_mascara"].Value = "0";

                    DataRow[] dFila = dtConsulta.Select("id_producto = " + dgvPedido.Rows[iNumeroFila].Cells["id_producto"].Value.ToString().Trim());

                    if (dFila.Length != 0)
                    {
                        dgvPedido.Rows[iNumeroFila].Cells["nombre_producto"].Value = dFila[0][1].ToString().Trim().ToUpper();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No ha seleccionado ninguna fila.";
                    ok.ShowDialog();
                    return;
                }                
            }

            dgvPedido.ClearSelection();
            iNumeroFila = -1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar los cambios?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                retornarInformacion();
            }
        }

        private void frmMascaraItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
