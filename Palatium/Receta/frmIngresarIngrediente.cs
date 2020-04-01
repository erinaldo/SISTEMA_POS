using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmIngresarIngrediente : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public string sCodigo_R { get; set; }
        public string sDescripcion_R { get; set; }
        public int iCorrelativo_R { get; set; }

        public int iEditar_R;
        public int iUnidad_R { get; set; }
        public int iPorcion_R { get; set; }

        double dbCantidadBruta;
        double dbCantidadNeta;
        double dbPrecioUnitario;
        double dbRendimiento;
        double dbImporte;
        double dbPrecioUnitario_DB;
        double dbPrecioUnitario_Truncado_DB;
        double dbValorEquivalencia_DC;
        double dbCantidadBruta_P;
        public double dbPrecioUnitario_P;
        double dbTotal_P;

        int iIdProducto_DB;
        int iIdUnidadEquivalencia_DC;
        public int iUnidadControl;
        public double dbEquivalencia_DC;
        public int iUnidadProductoReceta;
        public string sUnidadProductoReceta;
        int iAux;

        public frmIngresarIngrediente(int iEditar, int iUnidadControl_P, int iUnidad_R, double dbPUnit_P, double dbValorEquivalencia_P)
        {
            this.iEditar_R = iEditar;
            this.iUnidadControl = iUnidadControl_P;
            this.iUnidad_R = iUnidad_R;
            this.dbPrecioUnitario_DB = dbPUnit_P;
            this.dbValorEquivalencia_DC = dbValorEquivalencia_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CALCULAR EL RENDIMIENTO Y EL IMPORTE
        private void calcularValores()
        {
            try
            {
                dbCantidadBruta = Convert.ToDouble(txtCantidadBruta.Text.Trim());
                dbCantidadNeta = Convert.ToDouble(txtCantidadNeta.Text.Trim());
                dbPrecioUnitario = dbPrecioUnitario_DB;

                if ((dbCantidadBruta == 0) && (dbCantidadNeta == 0))
                {
                    dbRendimiento = 0;
                }

                else if ((dbCantidadBruta == 0) && (dbCantidadNeta != 0))
                {
                    dbRendimiento = 0;
                }

                else
                {
                    dbRendimiento = (dbCantidadNeta / dbCantidadBruta);
                }

                txtRendimiento.Text = dbRendimiento.ToString("N2");

                if (dbPrecioUnitario_DB == 0)
                {
                    txtCostoUnitario.Text = "0.00000";
                }

                else
                {
                    if (rdbUnidades.Checked == true)
                    {
                        txtCostoUnitario.Text = dbPrecioUnitario_DB.ToString("N5");
                        dbPrecioUnitario_P = dbPrecioUnitario_DB;
                    }

                    else if (rdbEquivalencias.Checked == true)
                    {
                        //txtCostoUnitario.Text = (dbPrecioUnitario_DB / dbValorEquivalencia_DC).ToString("N5");
                        txtCostoUnitario.Text = dbPrecioUnitario_DB.ToString("N5");
                        //dbPrecioUnitario_P = dbPrecioUnitario_DB / dbValorEquivalencia_DC;
                        dbPrecioUnitario_P = dbPrecioUnitario_DB;
                    }
                }
                
                dbImporte = Convert.ToDouble(txtCantidadBruta.Text.Trim()) * dbPrecioUnitario_P;
                txtImporteTotal.Text = dbImporte.ToString("N2");
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBOBOX DE UNIDADES DE MEDIDA
        private void llenarComboUnidades()
        {
            try
            {
                sSql = "";
                sSql += "select EQ.id_pos_unidad, U.descripcion, 1 as equivalencia" + Environment.NewLine;
                sSql += "from pos_equivalencias EQ, pos_unidad U" + Environment.NewLine;
                sSql += "where EQ.id_pos_unidad = U.id_pos_unidad" + Environment.NewLine;
                sSql += "and EQ.estado = 'A'" + Environment.NewLine;
                sSql += "and U.estado = 'A'" + Environment.NewLine;
                sSql += "order by U.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbUnidades.llenar(dtConsulta, sSql);

                if (cmbUnidades.Items.Count > 0)
                {
                    cmbUnidades.SelectedIndex = 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE EQUIVALENCIAS
        private void llenarComboEquivalencias()
        {
            try
            {
                sSql = "";
                sSql += "select EQ.id_pos_unidad_equivalencia, U.descripcion, EQ.equivalencia" + Environment.NewLine;
                sSql += "from pos_equivalencias EQ, pos_unidad U" + Environment.NewLine;
                sSql += "where EQ.id_pos_unidad_equivalencia = U.id_pos_unidad" + Environment.NewLine;
                sSql += "and EQ.estado = 'A'" + Environment.NewLine;
                sSql += "and U.estado = 'A'" + Environment.NewLine;
                sSql += "and EQ.id_pos_unidad = " + Convert.ToInt32(cmbUnidades.SelectedValue) + Environment.NewLine;
                sSql += "order by U.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbEquivalencias.llenar(dtConsulta, sSql);

                if (cmbEquivalencias.Items.Count > 0)
                {
                    cmbEquivalencias.SelectedIndex = 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBOBOX DE PORCIONES
        private void llenarComboPorciones()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_porcion, descripcion, codigo" + Environment.NewLine;
                sSql += "from pos_porcion" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_porcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbPorciones.llenar(dtConsulta, sSql);

                if (cmbPorciones.Items.Count > 0)
                {
                    cmbPorciones.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR LA SENTENCIA DE DBAYUDA
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select PRO.Codigo, NOM.Nombre, PRO.Id_Producto, PRO.paga_iva," + Environment.NewLine;
                sSql += "PP.valor, ltrim(str(PP.valor, 15, 5)) valor_1" + Environment.NewLine;
                sSql += "from cv401_productos PRO, cv401_productos PROPADRE," + Environment.NewLine;
                sSql += "cv401_nombre_productos NOM, cv403_precios_productos PP" + Environment.NewLine;
                sSql += "where PRO.Id_Producto = NOM.Id_Producto" + Environment.NewLine;
                sSql += "and PRO.Id_Producto_Padre = PROPADRE.id_producto" + Environment.NewLine;
                sSql += "and PP.id_producto = PRO.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "and NOM.Estado = 'A'" + Environment.NewLine;
                sSql += "and PRO.Estado = 'A'" + Environment.NewLine;
                sSql += "and PRO.Ultimo_Nivel = 1" + Environment.NewLine;
                sSql += "and NOM.Nombre_Interno = 1" + Environment.NewLine;
                sSql += "and PRO.valida_stock = 1" + Environment.NewLine;
                sSql += "and PROPADRE.Codigo Like '1%'" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4" + Environment.NewLine;
                sSql += "order By NOM.Nombre";

                dBAyudaMateria.Ver(sSql, "NOM.Nombre", 2, 0, 1);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE EQUIVALENCIAS
        private void extraerUnidadPadre()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_unidad" + Environment.NewLine;
                sSql += "from pos_equivalencias" + Environment.NewLine;
                sSql += "where id_pos_unidad_equivalencia = " + iUnidad_R + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbUnidades.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        cmbUnidades.SelectedIndex = 0;
                    }
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

        //FUNCION PARA COMPROBAR LOS CAMPOS
        private void comprobarCajasTexto()
        {
            try
            {
                if (txtCantidadBruta.Text.Trim() == "")
                {
                    txtCantidadBruta.Text = "0.00";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else if (txtCantidadBruta.Text.Trim() == ".")
                {
                    txtCantidadBruta.Text = "0.00";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else if (txtCantidadNeta.Text.Trim() == "")
                {
                    txtCantidadNeta.Text = "0.00";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else if (txtCantidadNeta.Text.Trim() == ".")
                {
                    txtCantidadNeta.Text = "0.00";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else if (txtCostoUnitario.Text.Trim() == "")
                {
                    txtCostoUnitario.Text = "0.00000";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else if (txtCostoUnitario.Text.Trim() == ".")
                {
                    txtCostoUnitario.Text = "0.00000";
                    txtRendimiento.Text = "0.00";
                    txtImporteTotal.Text = "0.00";
                }

                else
                {
                    calcularValores();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL PRECIO DEL REGISTRO DBAYUDA
        private void extraerPrecio()
        {
            try
            {
                if (dBAyudaMateria.iId == 0)
                {
                    dbPrecioUnitario_DB = 0;
                    dbPrecioUnitario_Truncado_DB = 0;
                }

                else
                {
                    iIdProducto_DB = dBAyudaMateria.iId;
                    DataRow[] dFila = dBAyudaMateria.dtConsulta.Select("id_producto = " + iIdProducto_DB);

                    if (dFila.Length != 0)
                    {
                        dbPrecioUnitario_DB = Convert.ToDouble(dFila[0][4].ToString());
                        dbPrecioUnitario_Truncado_DB = Convert.ToDouble(dFila[0][5].ToString());
                    }
                }

                if (rdbUnidades.Checked == true)
                {
                    txtCostoUnitario.Text = dbPrecioUnitario_Truncado_DB.ToString("N5");
                }

                else if (rdbEquivalencias.Checked == true)
                {
                    if (Convert.ToInt32(cmbEquivalencias.SelectedValue) == 0)
                    {
                        txtCostoUnitario.Text = "0.00000";
                    }

                    else
                    {
                        iIdUnidadEquivalencia_DC = Convert.ToInt32(cmbEquivalencias.SelectedValue);
                        DataRow[] dFila = cmbEquivalencias.dt.Select("id_pos_unidad_equivalencia = " + iIdUnidadEquivalencia_DC);

                        if (dFila.Length != 0)
                        {
                            dbValorEquivalencia_DC = Convert.ToDouble(dFila[0][2].ToString());
                        }

                        dbPrecioUnitario_DB = dbPrecioUnitario_DB / dbValorEquivalencia_DC;
                        txtCostoUnitario.Text = dbPrecioUnitario_DB.ToString("N5");
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULAR EL TOTAL
        private void recalcularTotal()
        {
            try
            {
                if (txtCantidadBruta.Text.Trim() == "")
                {
                    txtCostoUnitario.Text = "0.00000";
                    return;
                }

                else if (Convert.ToDouble(txtCantidadBruta.Text.Trim()) == 0)
                {
                    txtCostoUnitario.Text = "0.00000";
                    return;
                }

                dbCantidadBruta_P = Convert.ToDouble(txtCantidadBruta.Text.Trim());
                dbTotal_P = Convert.ToDouble(txtImporteTotal.Text.Trim());

                dbPrecioUnitario_P = dbTotal_P / dbCantidadBruta_P;
                txtCostoUnitario.Text = dbPrecioUnitario_P.ToString("N5");

                //if (rdbUnidades.Checked == true)
                //{
                //    dbPrecioUnitario_P = Convert.ToDouble(txtImporteTotal.Text.Trim());
                //    txtCostoUnitario.Text = dbPrecioUnitario_P.ToString("N2");
                //}

                //else if (rdbEquivalencias.Checked == true)
                //{
                //    dbPrecioUnitario_P = dbTotal_P / dbCantidadBruta_P;
                //    txtCostoUnitario.Text = dbPrecioUnitario_P.ToString("N2");
                //}

                btnAceptar.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR E INICIAR EL FORMULARIO
        public void iniciarLimpiar()
        {
            try
            {
                cmbUnidades.SelectedIndexChanged -= new EventHandler(cmbUnidades_SelectedIndexChanged);
                llenarComboUnidades();
                cmbUnidades.SelectedIndexChanged += new EventHandler(cmbUnidades_SelectedIndexChanged);

                llenarComboEquivalencias();

                llenarComboPorciones();
                llenarSentencia();

                if (iEditar_R != 0)
                {
                    dBAyudaMateria.iId = iCorrelativo_R;
                    dBAyudaMateria.txtIdentificacion.Text = sCodigo_R;
                    dBAyudaMateria.txtDatos.Text = sDescripcion_R;

                    cmbUnidades.SelectedValue = iUnidad_R;
                    cmbPorciones.SelectedValue = iPorcion_R;

                    if (iUnidadControl == 0)
                    {
                        rdbUnidades.Checked = true;
                        rdbEquivalencias.Checked = false;
                        cmbUnidades.SelectedValue = iUnidad_R;
                    }

                    else
                    {
                        rdbUnidades.Checked = false;
                        rdbEquivalencias.Checked = true;
                        cmbUnidades.SelectedIndexChanged -= new EventHandler(cmbUnidades_SelectedIndexChanged);
                        extraerUnidadPadre();
                        cmbUnidades.SelectedIndexChanged += new EventHandler(cmbUnidades_SelectedIndexChanged);
                        llenarComboEquivalencias();
                        cmbEquivalencias.SelectedValue = iUnidad_R;
                    }

                    calcularValores();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        #endregion

        private void frmIngresarIngrediente_Load(object sender, EventArgs e)
        {
            iniciarLimpiar();
            iAux = 1;
        }

        private void frmIngresarIngrediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCantidadBruta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                caracteres.soloDecimales(sender, e, 2);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtCantidadNeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                caracteres.soloDecimales(sender, e, 2);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtCostoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                caracteres.soloDecimales(sender, e, 5);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dBAyudaMateria.iId == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún ingrediente para el registro.";
                ok.ShowDialog();
            }

            else if (Convert.ToDouble(txtCantidadBruta.Text.Trim()) == 0)
            {
                ok.lblMensaje.Text = "No puede ingresar un valor de cero en la cantidad bruta.";
                ok.ShowDialog();
                txtCantidadBruta.Focus();
            }

            else if (Convert.ToDouble(txtCantidadNeta.Text.Trim()) == 0)
            {
                ok.lblMensaje.Text = "No puede ingresar un valor de cero en la cantidad neta.";
                ok.ShowDialog();
                txtCantidadNeta.Focus();
            }

            else if (Convert.ToDouble(txtCostoUnitario.Text.Trim()) == 0)
            {
                ok.lblMensaje.Text = "No puede ingresar un valor de cero en el costo unitario.";
                ok.ShowDialog();
                txtCostoUnitario.Focus();
            }

            else if (Convert.ToInt32(cmbUnidades.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione la unidad del producto para el registro";
                ok.ShowDialog();
            }

            else if (Convert.ToInt32(cmbPorciones.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione el tipo de porción del producto para el registro";
                ok.ShowDialog();
            }

            else
            {
                if (rdbUnidades.Checked == true)
                {
                    iUnidadProductoReceta = Convert.ToInt32(cmbUnidades.SelectedValue);
                    sUnidadProductoReceta = cmbUnidades.Text.Trim().ToUpper();
                }

                else
                {
                    iUnidadProductoReceta = Convert.ToInt32(cmbEquivalencias.SelectedValue);
                    sUnidadProductoReceta = cmbEquivalencias.Text.Trim().ToUpper();
                }

                dbEquivalencia_DC = dbValorEquivalencia_DC;
                iCorrelativo_R = dBAyudaMateria.iId;
                sCodigo_R = dBAyudaMateria.txtIdentificacion.Text.Trim();
                sDescripcion_R = dBAyudaMateria.txtDatos.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtCantidadBruta_Leave(object sender, EventArgs e)
        {
            comprobarCajasTexto();
        }

        private void txtCantidadNeta_Leave(object sender, EventArgs e)
        {
            comprobarCajasTexto();
        }

        private void txtCostoUnitario_Leave(object sender, EventArgs e)
        {
            comprobarCajasTexto();
        }

        private void rdbUnidades_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbUnidades.Checked == true)
            {
                cmbEquivalencias.Enabled = false;
                iUnidadControl = 0;
                dbEquivalencia_DC = 1;
            }
        }

        private void rdbEquivalencias_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEquivalencias.Checked == true)
            {
                cmbEquivalencias.Enabled = true;
                iUnidadControl = 1;
            }
        }

        private void cmbUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                llenarComboEquivalencias();

                if (iAux == 1)
                {
                    extraerPrecio();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbEquivalencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iAux == 1)
                {
                    extraerPrecio();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtImporteTotal_Leave(object sender, EventArgs e)
        {
            recalcularTotal();
        }
    }
}
