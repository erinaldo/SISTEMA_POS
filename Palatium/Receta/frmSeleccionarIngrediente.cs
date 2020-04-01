using MaterialSkin;
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

namespace Palatium.Receta
{
    public partial class frmSeleccionarIngrediente : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        string sSql;
        string sFechaListaBase;
        string []sDatos_R = new string[20];

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdListaBase;
        int iUnidadConsumo;
        public int iUnidadConsumo_R;

        decimal dPrecioUnitario;
        decimal dPrecioUnitarioCompleto;
        decimal dPresentacion;
        decimal dRendimiento;
        decimal dPorcentajeRendimiento;
        decimal dCantidadBruta;
        decimal dCantidadNeta;
        decimal dPrecioTotal;
        decimal dPrecioTotalCompleto;
        public decimal dRendimiento_R;
        public decimal dPrecioUnitarioCompleto_R;

        public string sCodigo_R { get; set; }
        public string sDescripcion_R { get; set; }
        public int iCorrelativo_R { get; set; }

        public int iEditar_R;
        int iIdProducto_DB;

        public frmSeleccionarIngrediente(int iEditar, string []sDatos)
        {
            this.iEditar_R = iEditar;
            this.sDatos_R = sDatos;

            InitializeComponent();

            //MaterialSkinManager material = MaterialSkinManager.Instance;
            //material.AddFormToManage(this);
            //material.Theme = MaterialSkinManager.Themes.LIGHT;

            //material.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue500, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER LOS DATOS DE LA LISTA BASE Y MINORISTA
        private void datosListas()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_base = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdListaBase = Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"]);
                        sFechaListaBase = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_fin_validez"].ToString()).ToString("yyyy/MM/dd");
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CALCULAR EL RENDIMIENTO Y EL IMPORTE
        private void calcularValores()
        {
            try
            {
                dCantidadBruta = Convert.ToDecimal(txtCantidadBruta.Text.Trim());
                dPrecioTotal = dPrecioUnitario * dCantidadBruta;
                dCantidadNeta = (dCantidadBruta * dPorcentajeRendimiento) / 100;
                txtCantidadNeta.Text = dCantidadNeta.ToString("N4");
                dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
                txtImporteTotal.Text = dPrecioTotal.ToString("N4");
            }

            catch (Exception ex)
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
                    cmbPorciones.SelectedIndex = 1;
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
                sSql += "select PRO.Id_Producto, PRO.Codigo, NOM.Nombre, " + Environment.NewLine;
                sSql += "PRO.Presentacion, PRO.Rendimiento" + Environment.NewLine;
                sSql += "from cv401_productos PRO, cv401_productos PROPADRE," + Environment.NewLine;
                sSql += "cv401_nombre_productos NOM" + Environment.NewLine;
                sSql += "where PRO.Id_Producto = NOM.Id_Producto" + Environment.NewLine;
                sSql += "and PRO.Id_Producto_Padre = PROPADRE.id_producto" + Environment.NewLine;
                sSql += "and NOM.Estado = 'A'" + Environment.NewLine;
                sSql += "and PRO.Estado = 'A'" + Environment.NewLine;
                sSql += "and PRO.Ultimo_Nivel = 1" + Environment.NewLine;
                sSql += "and NOM.Nombre_Interno = 1" + Environment.NewLine;
                sSql += "and PRO.valida_stock = 1" + Environment.NewLine;
                sSql += "and PROPADRE.Codigo Like '1%'" + Environment.NewLine;
                sSql += "order By NOM.Nombre";

                dBAyudaMateria.Ver(sSql, "NOM.Nombre", 0, 1, 2);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSUTAR LOS DATOS DEL PRODUCTO
        private void consultarPrecioUnidad(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select PP.valor, ltrim(str(PP.valor, 15, 5)) valor_1 ," + Environment.NewLine;
                sSql += "UP.cg_unidad, TP.valor_texto" + Environment.NewLine;
                sSql += "from cv403_precios_productos PP, cv401_productos P," + Environment.NewLine;
                sSql += "cv401_unidades_productos UP, tp_codigos TP" + Environment.NewLine;
                sSql += "where PP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and UP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and TP.correlativo = UP.cg_unidad" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "and UP.estado = 'A'" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = " + iIdListaBase + Environment.NewLine;
                sSql += "and unidad_compra = 1" + Environment.NewLine;
                sSql += "and P.id_producto = " + iIdProducto_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dPrecioUnitarioCompleto = Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                        dPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[0][1].ToString());
                        iUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                        lblUnidadConsumo.Text = dtConsulta.Rows[0][3].ToString();

                        txtCostoUnitario.Text = dPrecioUnitario.ToString();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No se ha configurado el precio del producto. Favor comuníquese con el administrador.";
                        ok.ShowDialog();
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

        //FUNCION PARA LIMPIAR E INICIAR EL FORMULARIO
        public void iniciarLimpiar()
        {
            try
            {
                datosListas();
                llenarComboPorciones();
                llenarSentencia();

                if (iEditar_R != 0)
                {
                    consultarIngrediente();
                    //calcularValores();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS DATOS DEL INGREDIENTE

        //FUNCION PARA CARGAR LOS VALORES DEL INGREDIENTE
        private void consultarIngrediente()
        {
            try
            {
                dBAyudaMateria.iId = Convert.ToInt32(sDatos_R[9]);
                dBAyudaMateria.txtIdentificacion.Text = sDatos_R[0];
                dBAyudaMateria.txtDatos.Text = sDatos_R[1];
                lblUnidadConsumo.Text = sDatos_R[4];
                cmbPorciones.SelectedValue = Convert.ToInt32(sDatos_R[11]);
                txtCostoUnitario.Text = sDatos_R[6];
                txtCantidadBruta.Text = sDatos_R[2];
                txtCantidadNeta.Text = sDatos_R[3];

                dPrecioUnitario = Convert.ToDecimal(sDatos_R[6]);
                dRendimiento = Convert.ToDecimal(sDatos_R[7]) * 100;
                lblRendimiento.Text = dRendimiento.ToString("N2");

                dCantidadBruta = Convert.ToDecimal(sDatos_R[2]);
                dPrecioTotal = dPrecioUnitario * dCantidadBruta;
                dCantidadNeta = (dCantidadBruta * dPorcentajeRendimiento) / 100;
                dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
                txtImporteTotal.Text = dPrecioTotal.ToString("N4");

                dPrecioUnitarioCompleto = Convert.ToDecimal(sDatos_R[6]);
                dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        private void consultarIngrediente2()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_detalle_receta_normal" + Environment.NewLine;
                //sSql += "where id_pos_detalle_receta = " + iIdPosDetalleReceta_R + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dBAyudaMateria.iId = Convert.ToInt32(dtConsulta.Rows[0]["id_producto"].ToString());
                        dBAyudaMateria.txtIdentificacion.Text = dtConsulta.Rows[0]["codigo"].ToString();
                        dBAyudaMateria.txtDatos.Text = dtConsulta.Rows[0]["nombre"].ToString();
                        lblUnidadConsumo.Text = dtConsulta.Rows[0]["descripcion_unidad"].ToString();
                        cmbPorciones.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_porcion"].ToString());
                        txtCostoUnitario.Text = dtConsulta.Rows[0]["costo_unitario"].ToString();
                        txtCantidadBruta.Text = dtConsulta.Rows[0]["cantidad_bruta"].ToString();
                        txtCantidadNeta.Text = dtConsulta.Rows[0]["cantidad_neta"].ToString();

                        dPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[0]["costo_unitario"].ToString());
                        dRendimiento = Convert.ToDecimal(dtConsulta.Rows[0]["rendimiento"].ToString()) * 100;
                        lblRendimiento.Text = dRendimiento.ToString("N2");

                        dCantidadBruta = Convert.ToDecimal(dtConsulta.Rows[0]["cantidad_bruta"].ToString());
                        dPrecioTotal = dPrecioUnitario * dCantidadBruta;
                        dCantidadNeta = (dCantidadBruta * dPorcentajeRendimiento) / 100;
                        dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
                        txtImporteTotal.Text = dPrecioTotal.ToString("N4");

                        dPrecioUnitarioCompleto = Convert.ToDecimal(dtConsulta.Rows[0]["costo_unitario"].ToString());
                        dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No se pudo cargar la información del ingrediente de la receta.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmSeleccionarIngrediente_Load(object sender, EventArgs e)
        {
            iniciarLimpiar();
        }

        private void frmSeleccionarIngrediente_KeyDown(object sender, KeyEventArgs e)
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
                caracteres.soloDecimales(sender, e, 2);
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

            else if (Convert.ToDouble(txtCostoUnitario.Text.Trim()) == 0)
            {
                ok.lblMensaje.Text = "No puede ingresar un valor de cero en el costo unitario.";
                ok.ShowDialog();
                txtCostoUnitario.Focus();
            }

            else if (Convert.ToInt32(cmbPorciones.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione el tipo de porción del producto para el registro";
                ok.ShowDialog();
            }

            else
            {
                iCorrelativo_R = dBAyudaMateria.iId;
                sCodigo_R = dBAyudaMateria.txtIdentificacion.Text.Trim();
                sDescripcion_R = dBAyudaMateria.txtDatos.Text.Trim();

                dRendimiento_R = dPorcentajeRendimiento / 100;
                iUnidadConsumo_R = iUnidadConsumo;
                dPrecioUnitarioCompleto_R = dPrecioUnitarioCompleto;

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dBAyudaMateria.iId == 0)
                {
                    ok.lblMensaje.Text = "No ha seleccionado el ingrediente.";
                    ok.ShowDialog();
                }

                else
                {
                    txtCantidadBruta.Text = "1";
                    txtCantidadNeta.Text = "1";

                    iIdProducto_DB = dBAyudaMateria.iId;
                    DataRow[] dFila = dBAyudaMateria.dtConsulta.Select("id_producto = " + iIdProducto_DB);

                    if (dFila.Length != 0)
                    {
                        dPresentacion = Convert.ToDecimal(dFila[0][3].ToString());
                        dRendimiento = Convert.ToDecimal(dFila[0][4].ToString());

                        if (dPresentacion != 0)
                        {
                            dPorcentajeRendimiento = (dRendimiento * 100) / dPresentacion;
                        }

                        else
                        {
                            dPorcentajeRendimiento = 0;
                        }

                        lblRendimiento.Text = dPorcentajeRendimiento.ToString("N2") + " %";
                    }

                    consultarPrecioUnidad(iIdProducto_DB);

                    dCantidadBruta = 1;
                    dPrecioTotal = dPrecioUnitario * dCantidadBruta;
                    dPrecioTotalCompleto = dPrecioUnitarioCompleto * dCantidadBruta;
                    dCantidadNeta = (dCantidadBruta * dPorcentajeRendimiento) / 100;
                    txtCantidadNeta.Text = dCantidadNeta.ToString("N4");
                    txtImporteTotal.Text = dPrecioTotal.ToString("N4");
                    txtCantidadBruta.Focus();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtCantidadBruta_Leave(object sender, EventArgs e)
        {
            calcularValores();
        }

        private void txtCantidadBruta_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            caracteres.soloDecimales(sender, e, 4);
        } 
    }
}
