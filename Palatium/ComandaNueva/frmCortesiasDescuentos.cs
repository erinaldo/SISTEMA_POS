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
    public partial class frmCortesiasDescuentos : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        DataTable dtOrigen;
        DataTable dtDestino;
        public DataTable dt;

        Decimal dbCantidad;
        Decimal dbPrecioUnitario;
        Decimal dbValorDescuento;

        int iBanderaDescuento;
        int iFilaGrid = -1;
        int iContadorRegistros;

        //VARIABLES PARA AUXLIAR DEL DATATABLE DE REGRESO
        Decimal dbCantidad_G;
        Decimal dbValorUnitario_G;
        Decimal dbValorTotal_G;
        Decimal dbValorDescuento_G;
        Decimal dbPorcentajeDescuento_G;
        
        string sNombreProducto_G;
        string sPagaIva_G;
        string sCodigoProducto_G;
        string sSecuenciaImpresion_G;
        string sMotivoCortesia_G;
        string sMotivoDescuento_G;
        string sIdMascara_G;
        string sIdOrdenamiento_G;
        string sOrdenamiento_G;
        string sBanderaComentario_G;        

        int iIdProducto_G;
        int iBanderaCortesia_G;
        int iBanderaDescuento_G;
        int iBanderaInsercion_G;

        public frmCortesiasDescuentos(DataTable dtOrigen_P, int iBanderaDescuento_P)
        {
            this.dtOrigen = dtOrigen_P;
            this.iBanderaDescuento = iBanderaDescuento_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CREAR EL DATATABLE DEL GRID
        private void construirDataTable()
        {
            try
            {
                dtDestino = new DataTable();
                dtDestino.Clear();

                dtDestino.Columns.Add("cantidad");
                dtDestino.Columns.Add("nombre_producto");
                dtDestino.Columns.Add("valor_unitario");
                dtDestino.Columns.Add("valor_total");
                dtDestino.Columns.Add("id_producto");
                dtDestino.Columns.Add("paga_iva");
                dtDestino.Columns.Add("codigo_producto");
                dtDestino.Columns.Add("secuencia_impresion");
                dtDestino.Columns.Add("bandera_cortesia");
                dtDestino.Columns.Add("motivo_cortesia");
                dtDestino.Columns.Add("bandera_descuento");
                dtDestino.Columns.Add("motivo_descuento");
                dtDestino.Columns.Add("id_mascara");
                dtDestino.Columns.Add("id_ordenamiento");
                dtDestino.Columns.Add("ordenamiento");
                dtDestino.Columns.Add("porcentaje_descuento");
                dtDestino.Columns.Add("bandera_comentario");
                dtDestino.Columns.Add("valor_descuento");
                dtDestino.Columns.Add("bandera_insercion");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    dbCantidad = Convert.ToDecimal(dtOrigen.Rows[i]["cantidad"].ToString());

                    if (dbCantidad > 1)
                    {                        
                        for (int j = 0; j < dbCantidad; j++)
                        {
                            dgvPedido.Rows.Add("1",
                                               dtOrigen.Rows[i]["nombre_producto"].ToString().Trim(),
                                               dtOrigen.Rows[i]["valor_unitario"].ToString().Trim(),
                                               Convert.ToDecimal(dtOrigen.Rows[i]["valor_unitario"].ToString().Trim()).ToString("N2"),
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
                                               dtOrigen.Rows[i]["valor_descuento"].ToString().Trim()
                                );
                        }
                    }

                    else
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
                                           dtOrigen.Rows[i]["valor_descuento"].ToString().Trim()
                                );
                    }
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

        //FUNCION PARA LIMPIAR LA SELECCION
        private void limpiarSeleccion()
        {
            lblNombreProducto.Text = "Seleccione";
            lblPrecio.Text = "0.00";
            txtMotivo.Clear();
            iFilaGrid = -1;
            dbPrecioUnitario = 0;
            txtMotivo.ReadOnly = true;

            //  Parámetro: iBanderaDescuento
            //  Opción 0 para cortesías
            //  Opción 1 para descuentos

            if (iBanderaDescuento == 0)
            {
                txtPorcentajeDescuento.Text = "100";
                txtPorcentajeDescuento.ReadOnly = true;
            }

            else
            {
                txtPorcentajeDescuento.Text = "0";
                txtPorcentajeDescuento.ReadOnly = false;
            }

            dgvPedido.ClearSelection();
        }

        //FUNCION PARA CONSTRUIR EL DATATABLE
        private void retornarInformacion_2()
        {
            try
            {
                construirDataTable();

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iBanderaCortesia_G = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString());
                    iBanderaDescuento_G = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString());

                    if ((iBanderaCortesia_G == 0) && (iBanderaDescuento_G == 0))
                    {
                        DataRow row = dtDestino.NewRow();
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
                        row["bandera_insercion"] = "0";

                        dtDestino.Rows.Add(row);
                    }
                }

                construirDataTableRetorno();

                //ALGORITMO DE AGRUPACION SIN LINQ
                for (int i = 0; i < dtDestino.Rows.Count; i++)
                {
                    iBanderaInsercion_G = Convert.ToInt32(dtDestino.Rows[i]["bandera_insercion"].ToString());
                    dtDestino.Rows[i]["bandera_insercion"] = "1";

                    if (iBanderaInsercion_G == 0)
                    {
                        dbCantidad_G = Convert.ToDecimal(dtDestino.Rows[i]["cantidad"].ToString());
                        sNombreProducto_G = dtDestino.Rows[i]["nombre_producto"].ToString();
                        dbValorUnitario_G = Convert.ToDecimal(dtDestino.Rows[i]["valor_unitario"].ToString());
                        dbValorTotal_G = Convert.ToDecimal(dtDestino.Rows[i]["valor_total"].ToString());
                        iIdProducto_G = Convert.ToInt32(dtDestino.Rows[i]["id_producto"].ToString());
                        sPagaIva_G = dtDestino.Rows[i]["paga_iva"].ToString();
                        sCodigoProducto_G = dtDestino.Rows[i]["codigo_producto"].ToString();
                        sSecuenciaImpresion_G = dtDestino.Rows[i]["secuencia_impresion"].ToString();
                        iBanderaCortesia_G = Convert.ToInt32(dtDestino.Rows[i]["bandera_cortesia"].ToString());
                        sMotivoCortesia_G = dtDestino.Rows[i]["motivo_cortesia"].ToString();
                        iBanderaDescuento_G = Convert.ToInt32(dtDestino.Rows[i]["bandera_descuento"].ToString());
                        sMotivoDescuento_G = dtDestino.Rows[i]["motivo_descuento"].ToString();
                        sIdMascara_G = dtDestino.Rows[i]["id_mascara"].ToString();
                        sIdOrdenamiento_G = dtDestino.Rows[i]["id_ordenamiento"].ToString();
                        sOrdenamiento_G = dtDestino.Rows[i]["ordenamiento"].ToString();
                        dbPorcentajeDescuento_G = Convert.ToDecimal(dtDestino.Rows[i]["porcentaje_descuento"].ToString());
                        sBanderaComentario_G = dtDestino.Rows[i]["bandera_comentario"].ToString();
                        dbValorDescuento_G = Convert.ToDecimal(dtDestino.Rows[i]["valor_descuento"].ToString());

                        for (int j = 0; j < dtDestino.Rows.Count; j++)
                        {
                            iContadorRegistros = 0;
                            int iIdProducto_Aux = Convert.ToInt32(dtDestino.Rows[j]["id_producto"].ToString());

                            if (i != j)
                            {
                                if (iIdProducto_Aux == iIdProducto_G)
                                {
                                    dbCantidad_G += Convert.ToDecimal(dtDestino.Rows[j]["cantidad"].ToString());
                                    dbValorDescuento_G += Convert.ToDecimal(dtDestino.Rows[j]["valor_descuento"].ToString());
                                    dbPorcentajeDescuento_G += Convert.ToDecimal(dtDestino.Rows[j]["porcentaje_descuento"].ToString());
                                    dtDestino.Rows[j]["bandera_insercion"] = "1";

                                    iContadorRegistros++;
                                }
                            }
                        }

                        //AQUI LLENAR EL DATATABLE CON LOS CALCULOS DE PORCENTAJES E INSERTAR EL REGISTRO
                        DataRow row = dt.NewRow();
                        row["cantidad"] = dbCantidad_G.ToString();
                        row["nombre_producto"] = sNombreProducto_G;
                        row["valor_unitario"] = dbValorUnitario_G.ToString();
                        row["valor_total"] = (dbCantidad_G * dbValorUnitario_G).ToString("N2");
                        row["id_producto"] = iIdProducto_G.ToString();
                        row["paga_iva"] = sPagaIva_G;
                        row["codigo_producto"] = sCodigoProducto_G;
                        row["secuencia_impresion"] = sSecuenciaImpresion_G;
                        row["bandera_cortesia"] = iBanderaCortesia_G.ToString();
                        row["motivo_cortesia"] = sMotivoCortesia_G;
                        row["bandera_descuento"] = iBanderaDescuento_G.ToString();
                        row["motivo_descuento"] = sMotivoDescuento_G;
                        row["id_mascara"] = sIdMascara_G;
                        row["id_ordenamiento"] = sIdOrdenamiento_G;
                        row["ordenamiento"] = sOrdenamiento_G;
                        row["porcentaje_descuento"] = dbPorcentajeDescuento_G.ToString();
                        row["bandera_comentario"] = sBanderaComentario_G;
                        row["valor_descuento"] = dbValorDescuento_G.ToString();
                        dt.Rows.Add(row);                        
                    }
                }

                //RECORRER EL GRID CON LAS CORTESIAS Y DESCUENTOS
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    iBanderaCortesia_G = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_cortesia"].Value.ToString());
                    iBanderaDescuento_G = Convert.ToInt32(dgvPedido.Rows[i].Cells["bandera_descuento"].Value.ToString());

                    if ((iBanderaCortesia_G == 1) || (iBanderaDescuento_G == 1))
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

        private void frmCortesiasDescuentos_Load(object sender, EventArgs e)
        {
            //  Parámetro: iBanderaDescuento
            //  Opción 0 para cortesías
            //  Opción 1 para descuentos

            if (iBanderaDescuento == 0)
            {
                txtPorcentajeDescuento.Text = "100";
                txtPorcentajeDescuento.ReadOnly = true;
            }

            else
            {
                txtPorcentajeDescuento.Text = "0";
                txtPorcentajeDescuento.ReadOnly = false;
            }

            llenarGrid();
        }

        private void dgvPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iFilaGrid = dgvPedido.CurrentRow.Index;
                lblNombreProducto.Text = dgvPedido.CurrentRow.Cells["nombre_producto"].Value.ToString();
                lblPrecio.Text = dgvPedido.CurrentRow.Cells["valor_total"].Value.ToString();
                dbPrecioUnitario = Convert.ToDecimal(dgvPedido.CurrentRow.Cells["valor_unitario"].Value);

                txtMotivo.ReadOnly = false;

                //  Parámetro: iBanderaDescuento
                //  Opción 0 para cortesías
                //  Opción 1 para descuentos

                if (iBanderaDescuento == 0)
                {
                    txtMotivo.Clear();
                    txtMotivo.Focus();
                }

                else
                {
                    txtPorcentajeDescuento.Text = "0";
                    txtMotivo.Clear();
                    txtPorcentajeDescuento.Focus();
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

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (iFilaGrid == -1)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione un registro para editar.";
                ok.ShowDialog();
                return;
            }

            if (txtPorcentajeDescuento.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el porcentaje de descuento a aplicar en el producto.";
                ok.ShowDialog();
                txtPorcentajeDescuento.Focus();
                return;
            }

            if (Convert.ToDecimal(txtPorcentajeDescuento.Text.Trim()) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el porcentaje de descuento diferente a cero.";
                ok.ShowDialog();
                txtPorcentajeDescuento.Focus();
                return;
            }

            //  Parámetro: iBanderaDescuento
            //  Opción 0 para cortesías
            //  Opción 1 para descuentos

            if (iBanderaDescuento == 0)
            {
                dgvPedido.Rows[iFilaGrid].Cells["bandera_cortesia"].Value = "1";
                dgvPedido.Rows[iFilaGrid].Cells["motivo_cortesia"].Value = txtMotivo.Text.Trim().ToUpper();

                dgvPedido.Rows[iFilaGrid].Cells["bandera_descuento"].Value = "0";
                dgvPedido.Rows[iFilaGrid].Cells["motivo_descuento"].Value = "";

                dgvPedido.Rows[iFilaGrid].Cells["porcentaje_descuento"].Value = "100";
                dgvPedido.Rows[iFilaGrid].Cells["valor_descuento"].Value = dgvPedido.Rows[iFilaGrid].Cells["valor_unitario"].Value;
                dgvPedido.Rows[iFilaGrid].Cells["valor_total"].Value = "0.00";
                dgvPedido.Rows[iFilaGrid].Cells["bandera_comentario"].Value = "1";
                dgvPedido.Rows[iFilaGrid].Cells["nombre_producto"].Value += " - CORTESÍA";
                limpiarSeleccion();
            }

            else
            {
                Decimal dbPorcentaje_D = Convert.ToDecimal(txtPorcentajeDescuento.Text.Trim());
                dbPorcentaje_D = dbPorcentaje_D / 100;
                dbValorDescuento = dbPorcentaje_D * dbPrecioUnitario;

                dgvPedido.Rows[iFilaGrid].Cells["bandera_descuento"].Value = "1";
                dgvPedido.Rows[iFilaGrid].Cells["motivo_descuento"].Value = txtMotivo.Text.Trim().ToUpper();

                dgvPedido.Rows[iFilaGrid].Cells["bandera_cortesia"].Value = "0";
                dgvPedido.Rows[iFilaGrid].Cells["motivo_cortesia"].Value = "";

                dgvPedido.Rows[iFilaGrid].Cells["porcentaje_descuento"].Value = txtPorcentajeDescuento.Text.Trim();
                dgvPedido.Rows[iFilaGrid].Cells["valor_descuento"].Value = dbValorDescuento.ToString();
                dgvPedido.Rows[iFilaGrid].Cells["valor_total"].Value = (dbPrecioUnitario - dbValorDescuento).ToString("N2");
                dgvPedido.Rows[iFilaGrid].Cells["bandera_comentario"].Value = "1";
                dgvPedido.Rows[iFilaGrid].Cells["nombre_producto"].Value += " - DESCUENTO";
                limpiarSeleccion();
            }
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
                retornarInformacion_2();
            }
        }
    }
}
