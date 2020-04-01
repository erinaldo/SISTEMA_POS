using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmMateriaPrima : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        string sSql;
        string sFechaListaBase;
        string sFechaListaMinorista;
        string sNombreProducto;
        string sPrecioBase;
        string sPrecioMinorista;
        string sCodigo;
        string sFechaInicio;
        string sTabla;
        string sCampo;

        DataTable dtConsulta;

        bool bRespuesta;

        decimal dPrecioCompra;
        decimal dPrecioMinorista;
        decimal dPresentacion;
        decimal dPrecioUnitario;
        decimal dIva;
        decimal dServicio;
        decimal dSubtotal;

        int iIdListaBase;
        int iIdListaMinorista;
        int iIdCategoria;
        int iIdPadre;
        int iIdProducto;
        int iIdPosReceta;
        int iIdPosReferenciaInsumo;
        int iPreModific;
        int iPagIva;
        int iModificador = 0;
        int iSubcategoria = 0;
        int iUltimo = 1;
        int iExpira;
        int iCg_tipoNombre = 5076;
        int iIdUnidadCompra;
        int iIdUnidadConsumo;

        public frmMateriaPrima()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER EL PRECIO UNITARIO
        private void calcularPrecioUnitario()
        {
            try
            {
                if (txtPrecioCompra.Text.Trim() == "")
                {
                    dPrecioCompra = 0;
                }

                else if (Convert.ToDecimal(txtPrecioCompra.Text.Trim()) == 0)
                {
                    dPrecioCompra = 0;
                }

                else
                {
                    dPrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text.Trim());
                }

                if (txtPresentacion.Text.Trim() == "")
                {
                    dPresentacion = 0;
                }

                else if (Convert.ToDecimal(txtPresentacion.Text.Trim()) == 0)
                {
                    dPresentacion = 0;
                }

                else
                {
                    dPresentacion = Convert.ToDecimal(txtPresentacion.Text.Trim());
                }

                if (dPresentacion == 0)
                {
                    txtPrecioUnitario.Text = "0.0000";
                    dPrecioUnitario = 0;
                }

                else
                {
                    dPrecioUnitario = dPrecioCompra / dPresentacion;
                    txtPrecioUnitario.Text = dPrecioUnitario.ToString("N4");
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER LOS DATOS DE LA LISTA BASE Y MINORISTA
        private void datosListas()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_base = 1" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_minorista = 1" + Environment.NewLine;
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
                        iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[1]["id_lista_precio"]);
                        sFechaListaMinorista = Convert.ToDateTime(dtConsulta.Rows[1]["fecha_fin_validez"].ToString()).ToString("yyyy/MM/dd");
                    }
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

        //FUNCION PARA LLENAR EL COMBO DE COMPRA
        private void llenarComboCompra()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbCompra.DisplayMember = "valor_texto";
                    cmbCompra.ValueMember = "correlativo";
                    cmbCompra.DataSource = dtConsulta;

                    //if (cmbCompra.Items.Count > 22)
                    //{
                    //    cmbCompra.SelectedIndex = 22;
                    //}
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

        //FUNCION PARA LLENAR EL COMBO DE CONSUMO
        private void llenarComboConsumo()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbConsumo.DisplayMember = "valor_texto";
                    cmbConsumo.ValueMember = "correlativo";
                    cmbConsumo.DataSource = dtConsulta;

                    //if (cmbConsumo.Items.Count > 22)
                    //{
                    //    cmbConsumo.SelectedIndex = 22;
                    //}
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

        //FUNCION PARA LLENAR EL COMBO DE CLASE DE PRODUCTO
        private void llenarComboClaseProducto()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_clase_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_clase_producto" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and aplica_materia_prima = 1" + Environment.NewLine;
                sSql += "and aplica_producto_terminado = 0";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbClaseProducto.DisplayMember = "descripcion";
                    cmbClaseProducto.ValueMember = "id_pos_clase_producto";
                    cmbClaseProducto.DataSource = dtConsulta;

                    //if (cmbConsumo.Items.Count > 22)
                    //{
                    //    cmbConsumo.SelectedIndex = 22;
                    //}
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                //cmbClaseProducto.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE TIPO DE PRODUCTO
        private void llenarComboTipoProducto()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbTipoProducto.DisplayMember = "descripcion";
                    cmbTipoProducto.ValueMember = "id_pos_tipo_producto";
                    cmbTipoProducto.DataSource = dtConsulta;

                    //if (cmbConsumo.Items.Count > 22)
                    //{
                    //    cmbConsumo.SelectedIndex = 22;
                    //}
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                //cmbTipoProducto.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DB AYUDA DE CATEGORIAS
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto as Id_producto, P.codigo as Código, NP.nombre as Nombre" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto from cv401_productos " + Environment.NewLine;
                sSql += "where codigo = '1')" + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.subcategoria = 0" + Environment.NewLine;

                dBAyudaCategorias.Ver(sSql, "NP.nombre", 0, 1, 2);
                dBAyudaCategorias.limpiar();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA HABILITAR LOS CONTROLES DEL FORMULARIO
        private void habilitarControles(bool ok)
        {
            try
            {
                grupoDatos.Enabled = ok;
                grupoPrecio.Enabled = ok;
                grupoOpciones.Enabled = ok;
                grupoStock.Enabled = ok;
                grupoReceta.Enabled = ok;
                //grupoGrid.Enabled = ok;
                rdbReceta.Enabled = !ok;
                rdbReferenciaInsumos.Enabled = ok;
                //btnAgregar.Enabled = ok;
                //btnLimpiar.Enabled = ok;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del receta
        private void llenarSentenciaReceta()
        {
            try
            {
                sSql = "";
                sSql += "select codigo, descripcion, id_pos_receta" + Environment.NewLine;
                sSql += "from pos_receta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dbAyudaReceta.Ver(sSql, "codigo", 2, 0, 1);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda Referencias
        private void llenarSentenciaReferencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_bod_referencia, codigo, descripcion" + Environment.NewLine;
                sSql += "from bod_referencia" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dbAyudaReceta.Ver(sSql, "codigo", 0, 1, 2);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboClaseProducto();
            llenarComboTipoProducto();
            llenarComboCompra();
            llenarComboConsumo();

            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioMinorista.Clear();
            txtPresentacion.Clear();
            txtRendimiento.Clear();
            txtPrecioUnitario.Clear();
            txtStockMinimo.Clear();
            txtStockMaximo.Clear();
            txtSecuencia.Clear();
            chkPagaIva.Checked = false;
            chkPreModifProductos.Checked = false;
            chkExpiraProductos.Checked = false;

            rdbReceta.Checked = true;
            rdbReferenciaInsumos.Checked = false;
            dbAyudaReceta.limpiar();

            txtCodigo.Enabled = true;
            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            llenarGrid(0);
        }

        //FUNCION PARA LIMPIAR EL FORMULARIO
        private void limpiarTodo()
        {
            llenarSentencias();
            dBAyudaCategorias.limpiar();
            dbAyudaReceta.limpiar();
            llenarComboTipoProducto();
            llenarComboClaseProducto();

            txtBuscar.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioMinorista.Clear();
            txtPresentacion.Clear();
            txtRendimiento.Clear();
            txtPrecioUnitario.Clear();
            txtSecuencia.Clear();

            chkPagaIva.Checked = false;
            chkExpiraProductos.Checked = false;
            chkPreModifProductos.Checked = false;

            grupoDatos.Enabled = false;
            grupoReceta.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            dgvProductos.DataSource = dtConsulta;

            iIdProducto = 0;
            iIdCategoria = 0;

            lblNombreCategoria.Text = "NINGUNA";
        }

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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_lista_productos" + Environment.NewLine;
                sSql += "where id_producto_padre = " + iIdCategoria + Environment.NewLine;

                if (op == 1)
                {
                    sSql += "and descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvProductos.DataSource = dtConsulta;

                    if (dtConsulta.Rows.Count > 0)
                    {
                        completarGrid();
                    }

                    columnasGrid(false);
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                limpiar();
            }
        }

        //FUNCION PARA RELLENAR EL DATAGRID
        private void completarGrid()
        {
            try
            {
                for (int i = 0; i < dgvProductos.Rows.Count; i++)
                {
                    //INSTRUCCION PARA REEMPLAZAR EL VALOR DE LA COLUMNA LISTA BASE
                    sSql = "";
                    sSql += "select PR.valor" + Environment.NewLine;
                    sSql += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "where id_lista_precio = " + iIdListaBase + Environment.NewLine;
                    sSql += "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and PR.estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dgvProductos.Rows[i].Cells[5].Value) == 1)
                            {
                                dgvProductos.Rows[i].Cells[3].Value = (Convert.ToDouble(dtConsulta.Rows[0][0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("N5");
                                dgvProductos.Rows[i].Cells[29].Value = (Convert.ToDouble(dtConsulta.Rows[0][0].ToString()) * (1 + Program.iva + Program.servicio)).ToString();
                            }

                            else
                            {
                                dgvProductos.Rows[i].Cells[3].Value = Convert.ToDouble(dtConsulta.Rows[0][0].ToString()).ToString("N5");
                                dgvProductos.Rows[i].Cells[29].Value = Convert.ToDouble(dtConsulta.Rows[0][0].ToString()).ToString();
                            }
                        }

                        else
                        {
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 1.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }


                    //INSTRUCCION PARA REEMPLAZAR EL VALOR DE LA COLUMNA LISTA MINORISTA
                    sSql = "";
                    sSql += "select PR.valor" + Environment.NewLine;
                    sSql += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "where id_lista_precio = "+ iIdListaMinorista + Environment.NewLine;
                    sSql += "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and pr.estado='A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dgvProductos.Rows[i].Cells[5].Value) == 1)
                            {
                                dgvProductos.Rows[i].Cells[4].Value = (Convert.ToDouble(dtConsulta.Rows[0][0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("N5");
                            }

                            else
                            {
                                //dgvProductos.Rows[i].Cells[4].Value = Convert.ToDouble(dtConsulta.Rows[0][0].ToString()).ToString("N5");
                                dgvProductos.Rows[i].Cells[4].Value = Convert.ToDouble(dtConsulta.Rows[0][0].ToString()).ToString();
                            }
                        }

                        else
                        {
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    //RECUPERANDO LA UNIDAD DE COMPRA
                    sSql = "";
                    sSql += "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                    sSql += "from cv401_unidades_productos UP INNER JOIN" + Environment.NewLine;
                    sSql += "tp_codigos TC ON TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                    sSql += "and UP.estado = 'A'" + Environment.NewLine;
                    sSql += "where UP.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and UP.unidad_compra = 1";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvProductos.Rows[i].Cells[12].Value = dtConsulta.Rows[0]["cg_unidad"].ToString();
                            dgvProductos.Rows[i].Cells[14].Value = dtConsulta.Rows[0]["valor_texto"].ToString();
                        }

                        else
                        {
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    //RECUPERANDO LA UNIDAD DE CONSUMO
                    sSql = "";
                    sSql += "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                    sSql += "from cv401_unidades_productos UP INNER JOIN" + Environment.NewLine;
                    sSql += "tp_codigos TC ON TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                    sSql += "and UP.estado = 'A'" + Environment.NewLine;
                    sSql += "where UP.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and UP.unidad_compra = 0";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvProductos.Rows[i].Cells[13].Value = dtConsulta.Rows[0]["cg_unidad"].ToString();
                            dgvProductos.Rows[i].Cells[15].Value = dtConsulta.Rows[0]["valor_texto"].ToString();
                        }

                        else
                        {
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }               
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

        //FUNCION DE LAS COLUMNAS DEL GRID
        private void columnasGrid(bool ok)
        {
            //OCULTAR COLUMAS Y PONER TAMAÑOS AL DATAGRID VIEW
            dgvProductos.Columns[1].Width = 75;
            dgvProductos.Columns[2].Width = 200;
            dgvProductos.Columns[4].Width = 55;
            dgvProductos.Columns[6].Width = 75;

            dgvProductos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProductos.Columns[0].Visible = ok;
            dgvProductos.Columns[3].Visible = ok;
            dgvProductos.Columns[5].Visible = ok;
            dgvProductos.Columns[7].Visible = ok;
            dgvProductos.Columns[8].Visible = ok;
            dgvProductos.Columns[9].Visible = ok;
            dgvProductos.Columns[10].Visible = ok;
            dgvProductos.Columns[11].Visible = ok;
            dgvProductos.Columns[12].Visible = ok;
            dgvProductos.Columns[13].Visible = ok;
            dgvProductos.Columns[14].Visible = ok;
            dgvProductos.Columns[15].Visible = ok;
            dgvProductos.Columns[16].Visible = ok;
            dgvProductos.Columns[17].Visible = ok;
            dgvProductos.Columns[18].Visible = ok;
            dgvProductos.Columns[19].Visible = ok;
            dgvProductos.Columns[20].Visible = ok;
            dgvProductos.Columns[21].Visible = ok;
            dgvProductos.Columns[22].Visible = ok;
            dgvProductos.Columns[23].Visible = ok;
            dgvProductos.Columns[24].Visible = ok;
            dgvProductos.Columns[25].Visible = ok;
            dgvProductos.Columns[26].Visible = ok;
            dgvProductos.Columns[27].Visible = ok;
            dgvProductos.Columns[28].Visible = ok;
            dgvProductos.Columns[29].Visible = ok;
            dgvProductos.Columns[30].Visible = ok;
            dgvProductos.Columns[31].Visible = ok;

            dgvProductos.ClearSelection();
        }

        //FUNCION PARA INSERTAR UN NUEVO REGISTRO
        private void insertarRegistro()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //CONSULTAR EL ID DEL REGISTRO DE LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "select P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos as P, cv401_nombre_productos as NP " + Environment.NewLine;
                sSql += "where NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.codigo = '" + sCodigo + "'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "El código ingresado está o fue asignado para el producto " + dtConsulta.Rows[0][1].ToString() + ". Por Favor introduzca uno nuevo.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        return;
                    }
                }

                else
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    ok.ShowDialog();
                    return;
                }

                dIva = Convert.ToDecimal(Program.iva);
                dServicio = Convert.ToDecimal(Program.servicio);

                fechaSistema();

                if (sFechaInicio == "0001/01/01")
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Se ha encontrado una fecha inválida. Favor reinicie la aplicación para solucionar el inconveniente. Si el problema persiste, favor comuníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                if (rdbReceta.Checked == true)
                {
                    if (dbAyudaReceta.iId == 0)
                    {
                        iIdPosReceta = 0;
                        iIdPosReferenciaInsumo = 0;
                    }

                    else
                    {
                        iIdPosReceta = dbAyudaReceta.iId;
                        iIdPosReferenciaInsumo = 0;
                    }
                }

                else if (rdbReferenciaInsumos.Checked == true)
                {
                    if (dbAyudaReceta.iId == 0)
                    {
                        iIdPosReceta = 0;
                        iIdPosReferenciaInsumo = 0;
                    }

                    else
                    {
                        iIdPosReferenciaInsumo = dbAyudaReceta.iId;
                        iIdPosReceta = 0;
                    }
                }
                
                //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_productos (" + Environment.NewLine;
                sSql += "idempresa, codigo, id_Producto_padre, estado, Nivel," + Environment.NewLine;
                sSql += "modificable, precio_modificable, paga_iva, secuencia," + Environment.NewLine;
                sSql += "modificador, subcategoria, ultimo_nivel," + Environment.NewLine;
                sSql += "stock_min, stock_max, Expira, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, id_pos_receta, id_pos_tipo_producto," + Environment.NewLine;
                sSql += "id_pos_clase_producto, id_bod_referencia, presentacion, rendimiento, uso_receta)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ",'" + sCodigo + "'," + Environment.NewLine;
                sSql += iIdCategoria + ", 'A', 3, 0, " + iPreModific + ", " + iPagIva + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + ", " + iModificador + "," + Environment.NewLine;
                sSql += iSubcategoria + ", " + iUltimo + "," + Environment.NewLine;
                sSql += Convert.ToDouble(txtStockMinimo.Text.ToString().Trim()) + ", " + Convert.ToDouble(txtStockMaximo.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += iExpira + ", GETDATE(), '" + Program.sDatosMaximo[0] + "', " + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdPosReceta + ", " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbClaseProducto.SelectedValue) + ", " + iIdPosReferenciaInsumo + "," + Environment.NewLine;
                sSql += Convert.ToDecimal(txtPresentacion.Text.Trim()) + ", " + Convert.ToDecimal(txtRendimiento.Text.Trim()) + ", 1)";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv401_productos";
                sCampo = "id_Producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos.";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdProducto = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_NOMBRE_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + "," + iCg_tipoNombre + ",'" + txtNombre.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                if (iPagIva == 1)
                {                                      
                    dSubtotal = dPrecioUnitario / (1 + (dIva + dServicio));
                }

                else
                {
                    dSubtotal = dPrecioUnitario;
                }

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                sSql += "fecha_inicio, fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                sSql += "'" + sFechaListaBase + "', 'A', 0, 0, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                if (iPagIva == 1)
                {               
                    dSubtotal = Convert.ToDecimal(txtPrecioMinorista.Text) / (1 + (dIva + dServicio));
                }

                else
                {
                    dSubtotal = Convert.ToDecimal(txtPrecioMinorista.Text);
                }

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                sSql += "fecha_inicio, fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                sSql += "'" + sFechaListaMinorista + "', 'A', 0, 0, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + Program.iUnidadCompraConsumo + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + "," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GETDATE(), GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + (Program.iUnidadCompraConsumo + 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + "," + Environment.NewLine;
                sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GETDATE(), GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                this.Cursor = Cursors.Default;
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado correctamente.";
                ok.ShowDialog();
                limpiar();
                habilitarControles(false);
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { this.Cursor = Cursors.Default; conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {                
                dIva = Convert.ToDecimal(Program.iva);
                dServicio = Convert.ToDecimal(Program.servicio);

                this.Cursor = Cursors.WaitCursor;
                fechaSistema();

                if (sFechaInicio == "0001/01/01")
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Se ha encontrado una fecha inválida. Favor reinicie la aplicación para solucionar el inconveniente. Si el problema persiste, favor comuníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return;
                }

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }                

                if (rdbReceta.Checked == true)
                {
                    if (dbAyudaReceta.iId == 0)
                    {
                        iIdPosReceta = 0;
                        iIdPosReferenciaInsumo = 0;
                    }

                    else
                    {
                        iIdPosReceta = dbAyudaReceta.iId;
                        iIdPosReferenciaInsumo = 0;
                    }
                }

                else if (rdbReferenciaInsumos.Checked == true)
                {
                    if (dbAyudaReceta.iId == 0)
                    {
                        iIdPosReceta = 0;
                        iIdPosReferenciaInsumo = 0;
                    }

                    else
                    {
                        iIdPosReferenciaInsumo = dbAyudaReceta.iId;
                        iIdPosReceta = 0;
                    }
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = '" + txtSecuencia.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql += "paga_iva = " + iPagIva + "," + Environment.NewLine;
                sSql += "modificador = " + iModificador + "," + Environment.NewLine;
                sSql += "subcategoria = " + iSubcategoria + "," + Environment.NewLine;
                sSql += "ultimo_nivel = " + iUltimo + "," + Environment.NewLine;
                sSql += "stock_min = " + Convert.ToDecimal(txtStockMinimo.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "stock_max = " + Convert.ToDecimal(txtStockMaximo.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "precio_modificable = " + iPreModific + "," + Environment.NewLine;
                sSql += "Expira = " + iExpira + "," + Environment.NewLine;
                sSql += "id_pos_receta = " + iIdPosReceta + "," + Environment.NewLine;
                sSql += "id_bod_referencia = " + iIdPosReferenciaInsumo + "," +  Environment.NewLine;
                sSql += "id_pos_tipo_producto = " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_clase_producto = " + Convert.ToInt32(cmbClaseProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "presentacion = " + Convert.ToDecimal(txtPresentacion.Text.Trim()) + "," + Environment.NewLine;
                sSql += "rendimiento = " + Convert.ToDecimal(txtRendimiento.Text.Trim()) + "," + Environment.NewLine;
                sSql += "uso_receta = 1" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI HUBO ALGUN CAMBIO EN EL NOMBRE DEL PRODUCTO, SE REALIZA LA ACTUALIZACION
                if (txtNombre.Text.Trim().ToUpper() != sNombreProducto)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = '" + iIdProducto + "'";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_NOMBRE_PRODUCTOS
                    sSql = "";
                    sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                    sSql += "id_Producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                    sSql += "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdProducto + "," + iCg_tipoNombre + ",'" + txtNombre.Text.ToString().Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI HUBO ALGUN CAMBIO EN EL PRECIO BASE, SE REALIZA LA ACTUALIZACION
                if (txtPrecioCompra.Text.Trim() != sPrecioBase)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv403_precios_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and id_Lista_Precio = " + iIdListaBase;

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                    if (iPagIva == 1)
                    {
                        dSubtotal = dPrecioUnitario / (1 + (dIva + dServicio));
                    }

                    else
                    {
                        dSubtotal = dPrecioUnitario;
                    }

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                    sSql += "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                    sSql += "'" + sFechaListaBase + "', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI HUBO ALGUN CAMBIO EN EL PRECIO MINORISTA, SE REALIZA LA ACTUALIZACION
                if (Convert.ToDecimal(txtPrecioMinorista.Text.Trim()) != dPrecioMinorista)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv403_precios_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and id_Lista_Precio = " + iIdListaMinorista;

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                    if (iPagIva == 1)
                    {
                        dSubtotal = Convert.ToDecimal(txtPrecioMinorista.Text) / (1 + (dIva + dServicio));
                    }

                    else
                    {
                        dSubtotal = Convert.ToDecimal(txtPrecioMinorista.Text);
                    }

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                    sSql += "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                    sSql += "'" + sFechaListaMinorista + "', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                sSql = "";
                sSql += "select cg_tipo_unidad, cg_unidad, unidad_compra" + Environment.NewLine;
                sSql += "from cv401_unidades_productos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                int iBandera1 = 0;

                //ACTUALIZAR LAS UNIDADES PRODUCTOS
                sSql = "";
                sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + Program.iUnidadCompraConsumo + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + "," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GETDATE(), GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + (Program.iUnidadCompraConsumo + 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + "," + Environment.NewLine;
                sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GETDATE(), GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                this.Cursor = Cursors.Default;
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiar();
                habilitarControles(false);
                return;

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { this.Cursor = Cursors.Default; conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto reversa;
                }
                else
                {
                    //Hoy es mi último día de trabajo (18/08/2018) - DANNY AGUIRRE

                    //ELIMINACION DEL PRODUCTO EN CV401_PRODUCTOS
                    sSql = "";
                    sSql += "update cv401_productos set" + Environment.NewLine;
                    sSql += "codigo = '" + txtCodigo.Text.Trim() + "(" + iIdProducto + ")'," + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ELIMINACION DEL PRODUCTO EN CV401_NOMBRE_PRODUCTOS
                    sSql = "";
                    sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    this.Cursor = Cursors.Default;
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiar();
                    habilitarControles(false);
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { this.Cursor = Cursors.Default; conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtRendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void frmMateriaPrima_Load(object sender, EventArgs e)
        {
            datosListas();
            llenarSentencias();
            llenarComboClaseProducto();
            llenarComboTipoProducto();
            llenarComboCompra();
            llenarComboConsumo();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dBAyudaCategorias.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la categoría de la materia prima para registrar registros.";
                ok.ShowDialog();
            }

            else
            {
                limpiar();
                btnAgregar.Enabled = true;
                grupoGrid.Enabled = true;
                iIdCategoria = dBAyudaCategorias.iId;
                lblNombreCategoria.Text = dBAyudaCategorias.txtDatos.Text.Trim().ToUpper();
                grupoBotones.Enabled = true;
                llenarGrid(0);
                txtBuscar.Focus();
            }
        }

        private void btnLimpiarTodo_Click(object sender, EventArgs e)
        {
            dBAyudaCategorias.limpiar();
            habilitarControles(false);
            grupoBotones.Enabled = false;
            grupoGrid.Enabled = false;
            limpiarTodo();
        }

        private void rdbReceta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReceta.Checked == true)
            {
                llenarSentenciaReceta();
            }
        }

        private void rdbReferenciaInsumos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReferenciaInsumos.Checked == true)
            {
                llenarSentenciaReferencia();
            }
        }

        private void BtnLimpiarDbAyuda_Click(object sender, EventArgs e)
        {
            dbAyudaReceta.limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Nuevo")
            {
                habilitarControles(true);
                btnAgregar.Text = "Guardar";
                txtCodigo.Focus();                
            }

            else
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el código del producto.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtNombre.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el nombre del producto.";
                    ok.ShowDialog();
                    txtNombre.Focus();
                }

                else if (Convert.ToInt32(cmbTipoProducto.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de producto para el registro";
                    ok.ShowDialog();
                    cmbTipoProducto.Focus();
                }

                else if (Convert.ToInt32(cmbClaseProducto.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la clase de producto para el registro";
                    ok.ShowDialog();
                    cmbClaseProducto.Focus();
                }

                else if ((txtPrecioCompra.Text.Trim() == "") || (Convert.ToDecimal(txtPrecioCompra.Text.Trim()) == 0))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese un precio de compra válido para el registro.";
                    ok.ShowDialog();
                    txtPrecioCompra.Focus();
                }

                else if ((txtPresentacion.Text.Trim() == "") || (Convert.ToDecimal(txtPresentacion.Text.Trim()) == 0))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la presentación del producto a registrar.";
                    ok.ShowDialog();
                    txtPresentacion.Focus();
                }

                else if ((txtRendimiento.Text.Trim() == "") || (Convert.ToDecimal(txtRendimiento.Text.Trim()) == 0))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el rendimiento del producto a registrar.";
                    ok.ShowDialog();
                    txtRendimiento.Focus();
                }

                else if (txtSecuencia.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la secuencia del registro.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                }

                else if (Convert.ToInt32(cmbCompra.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione unidad de compra para el registro";
                    ok.ShowDialog();
                    cmbClaseProducto.Focus();
                }

                else if (Convert.ToInt32(cmbConsumo.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la unidad de consumo para el registro";
                    ok.ShowDialog();
                    cmbClaseProducto.Focus();
                }

                else if (txtStockMinimo.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el stock mínimo del producto.";
                    ok.ShowDialog();
                    txtStockMinimo.Focus();
                }

                else if (txtStockMaximo.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el stock máximo del producto.";
                    ok.ShowDialog();
                    txtStockMaximo.Focus();
                }

                else
                {
                    sCodigo = txtCodigo.Text.Trim();

                    if (chkPagaIva.Checked == true)
                        iPagIva = 1;
                    else iPagIva = 0;

                    if (chkPreModifProductos.Checked == true)
                        iPreModific = 1;
                    else iPreModific = 0;

                    if (chkExpiraProductos.Checked == true)
                        iExpira = 1;
                    else iExpira = 0;

                    if (btnAgregar.Text == "Guardar")
                    {
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Desea guardar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else if (btnAgregar.Text == "Actualizar")
                    {
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Desea actualizar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                llenarGrid(0);
            }
            else
            {
                llenarGrid(1);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            habilitarControles(false);
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdProducto == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado un registro para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro.?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //RECUPERACION DE DATOS
                iIdProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value);
                txtCodigo.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString().Trim();
                txtNombre.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString().Trim();
                sNombreProducto = dgvProductos.CurrentRow.Cells[2].Value.ToString();

                cmbClaseProducto.SelectedValue = dgvProductos.CurrentRow.Cells[18].Value.ToString();
                cmbTipoProducto.SelectedValue = dgvProductos.CurrentRow.Cells[19].Value.ToString();

                dPrecioUnitario = Convert.ToDecimal(dgvProductos.CurrentRow.Cells[29].Value.ToString());
                dPresentacion = Convert.ToDecimal(dgvProductos.CurrentRow.Cells[27].Value.ToString());
                dPrecioCompra = dPrecioUnitario * dPresentacion;
                
                txtPrecioCompra.Text = dPrecioCompra.ToString("N4");
                //txtPresentacion.Text = dPresentacion.ToString("N4");
                txtPresentacion.Text = dgvProductos.CurrentRow.Cells[27].Value.ToString();
                txtRendimiento.Text = dgvProductos.CurrentRow.Cells[28].Value.ToString();
                txtPrecioUnitario.Text = dPrecioUnitario.ToString("N4");

                sPrecioBase = dgvProductos.CurrentRow.Cells[3].Value.ToString();
                txtPrecioMinorista.Text = Convert.ToDouble(dgvProductos.CurrentRow.Cells[4].Value.ToString()).ToString();
                dPrecioMinorista = Convert.ToDecimal(dgvProductos.CurrentRow.Cells[4].Value.ToString());
                sPrecioMinorista = dgvProductos.CurrentRow.Cells[4].Value.ToString();

                cmbCompra.SelectedValue = dgvProductos.CurrentRow.Cells[12].Value.ToString();
                cmbConsumo.SelectedValue = dgvProductos.CurrentRow.Cells[13].Value.ToString();

                //CHECKED IVA
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[5].Value) == 1)
                {
                    chkPagaIva.Checked = true;
                }

                else
                {
                    chkPagaIva.Checked = false;
                }
                //-----------------------------------------------------------------------------------

                txtSecuencia.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();

                //CHECKED PRECIO MODIFICABLE
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[10].Value) == 1)
                {
                    chkPreModifProductos.Checked = true;
                }

                else
                {
                    chkPreModifProductos.Checked = false;
                }
                //-----------------------------------------------------------------------------------

                //CHECKED EXPIRA
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[11].Value) == 1)
                {
                    chkExpiraProductos.Checked = true;
                }

                else
                {
                    chkExpiraProductos.Checked = false;
                }
                //-----------------------------------------------------------------------------------

                txtStockMinimo.Text = dgvProductos.CurrentRow.Cells[8].Value.ToString();
                txtStockMaximo.Text = dgvProductos.CurrentRow.Cells[9].Value.ToString();
                txtSecuencia.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[16].Value.ToString()) == 0)
                {
                    dbAyudaReceta.limpiar();
                }

                else
                {
                    dbAyudaReceta.iId = Convert.ToInt32(dgvProductos.CurrentRow.Cells[16].Value.ToString());
                    dbAyudaReceta.txtIdentificacion.Text = dgvProductos.CurrentRow.Cells[23].Value.ToString();
                    dbAyudaReceta.txtDatos.Text = dgvProductos.CurrentRow.Cells[24].Value.ToString();
                }

                btnAgregar.Text = "Actualizar";
                btnEliminar.Enabled = true;
                txtCodigo.Enabled = false;                
                habilitarControles(true);
                txtNombre.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
        {
            calcularPrecioUnitario();
        }

        private void txtPresentacion_Leave(object sender, EventArgs e)
        {
            calcularPrecioUnitario();
        }
    }
}
