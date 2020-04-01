using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmIngresoSubProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        bool bRespuesta = false;

        DataTable dtConsulta;

        int iIdPadre;
        int iIdProducto;
        int iPagaIva;
        int iExpira;
        int iPrecioModificable;
        int iIdPosReceta;
        int iUltimo = 1;
        int iModificador = 0;
        int iSubcategoria = 0;
        int iCg_tipoNombre = 5076;

        int iIdReceta;

        int iIdCategoria;
        int iIdUnidadCompra;
        int iIdUnidadConsumo;
        int iTipoUnidadCompra;
        int iTipoUnidadConsumo;
        int iCuenta;

        double dSubtotal;

        string sSql;
        string sNombreProducto;
        string sPrecioBase;
        string sPrecioMinorista;

        public frmIngresoSubProductos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el combo de clase de producto
        private void llenarComboClaseProducto()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_clase_producto, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_clase_producto" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbClaseProducto.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de tipo de producto
        private void llenarComboTipoProducto()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_tipo_producto" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbTipoProducto.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combobox de subcategorías
        private void llenarComboSubCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre as Nombre, P.codigo as Codigo" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and id_producto_padre = " + iIdCategoria + Environment.NewLine;
                sSql += "and P.nivel = 3" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbSubcategoria.llenar(dtConsulta, sSql);

                if (cmbSubcategoria.Items.Count > 1)
                {
                    cmbSubcategoria.SelectedIndex = 1;
                }

                else
                {
                    cmbSubcategoria.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '2')" + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado ='A'" + Environment.NewLine;
                sSql += "and NP.estado='A'" + Environment.NewLine;
                sSql += "and P.subcategoria = 1" + Environment.NewLine;
                sSql += "and P.menu_pos = 1";
                dBAyudaCategorias.Ver(sSql, "NP.nombre", 0, 1, 2);

                sSql = "";
                sSql += "select codigo, descripcion, id_pos_receta" + Environment.NewLine;
                sSql += "from pos_receta" + Environment.NewLine;
                sSql += "where estado = 'A' ";
                dbAyudaReceta.Ver(sSql, "codigo", 2, 0, 1);
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE DESTINOS DE IMPRESION
        private void llenarDestinoImpresion()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_impresion_comanda, descripcion" + Environment.NewLine;
                sSql += "from pos_impresion_comanda" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pos_impresion_comanda";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbDestinoImpresion.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA VERIFICAR SI EL PRODUCTO HA SIDO UTILIZADO EN COMANDAS
        private int verificarProductoComanda()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = "ERROR:" + Environment.NewLine + "No se pudo contabilizar los ítems de las comandas generadas";
                        catchMensaje.ShowDialog();
                        return -1;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA LIMPIAR LAS CAJAS DE TEXTO
        private void limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtSecuencia.Clear();
            txtPrecioCompra.Text = "1.00";
            txtPrecioMinorista.Clear();
            txtBuscar.Clear();

            chkPagaIVA.Checked = true;
            chkExpira.Checked = false;
            chkPrecioModificable.Checked = false;

            grupoDatos.Enabled = false;
            grupoReceta.Enabled = false;

            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            txtCodigo.Enabled = true;

            llenarComboTipoProducto();
            llenarComboClaseProducto();
            llenarDestinoImpresion();
            llenarComboSubCategorias();
            iIdProducto = 0;
            cmbSubcategoria.Enabled = true;

            llenarGrid(0);
        }

        //FUNCION PARA LIMPIAR TODO EL FORMULARIO
        private void limpiarTodo()
        {
            llenarSentencias();
            dBAyudaCategorias.limpiar();
            dbAyudaReceta.limpiar();
            llenarComboTipoProducto();
            llenarComboClaseProducto();
            llenarComboSubCategorias();
            llenarDestinoImpresion();

            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecioCompra.Text = "1.00";
            txtPrecioMinorista.Clear();
            txtSecuencia.Clear();

            chkPagaIVA.Checked = true;
            chkExpira.Checked = false;
            chkPrecioModificable.Checked = false;

            grupoRegistros.Enabled = false;
            grupoDatos.Enabled = false;
            grupoReceta.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            dgvProductos.DataSource = dtConsulta;

            iIdProducto = 0;
            iIdCategoria = 0;

            cmbSubcategoria.SelectedIndex = 0;
            cmbSubcategoria.Enabled = true;

            lblNombreCategoria.Text = "NINGUNA";
            lblRegistros.Text = "0 Registros Encontrados";
        }

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_lista_sub_productos" + Environment.NewLine;
                sSql += "where id_producto_padre = " + Convert.ToInt32(cmbSubcategoria.SelectedValue) + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by codigo" + Environment.NewLine;

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
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    limpiar();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                    sSql = sSql + "select PR.valor" + Environment.NewLine;
                    sSql = sSql + "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql = sSql + "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql = sSql + "where id_lista_precio = 1" + Environment.NewLine;
                    sSql = sSql + "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql = sSql + "and PR.estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvProductos.Rows[i].Cells[3].Value = (Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("n2");
                        }

                        else
                        {
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 1.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }


                    //INSTRUCCION PARA REEMPLAZAR EL VALOR DE LA COLUMNA LISTA MINORISTA
                    sSql = "";
                    sSql = sSql + "select PR.valor" + Environment.NewLine;
                    sSql = sSql + "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql = sSql + "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql = sSql + "where id_lista_precio = 4" + Environment.NewLine;
                    sSql = sSql + "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql = sSql + "and pr.estado='A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvProductos.Rows[i].Cells[4].Value = (Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("n2");
                        }

                        else
                        {
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                            ok.ShowDialog();
                            return;
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    sSql = "";
                    sSql = sSql + "select id_producto_padre" + Environment.NewLine;
                    sSql = sSql + "from cv401_productos" + Environment.NewLine;
                    sSql = sSql + "where id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql = sSql + "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count == 0)
                        {
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 3.";
                            ok.ShowDialog();
                            return;
                        }

                        else
                        {
                            iIdPadre = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    sSql = "";
                    sSql = sSql + "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                    sSql = sSql + "from cv401_unidades_productos UP, tp_codigos TC " + Environment.NewLine;
                    sSql = sSql + "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                    sSql = sSql + "and UP.id_producto = " + iIdPadre + Environment.NewLine;
                    sSql = sSql + "and UP.estado = 'A'";
                    //sSql = "select * from cv401_unidades_productos where id_producto = " + iIdPadre + " and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count == 0)
                        {
                            ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 4.";
                            ok.ShowDialog();
                            return;
                        }

                        else
                        {
                            dgvProductos.Rows[i].Cells[12].Value = dtConsulta.Rows[0].ItemArray[0].ToString();
                            dgvProductos.Rows[i].Cells[13].Value = dtConsulta.Rows[1].ItemArray[0].ToString();

                            dgvProductos.Rows[i].Cells[14].Value = dtConsulta.Rows[0].ItemArray[1].ToString();
                            dgvProductos.Rows[i].Cells[15].Value = dtConsulta.Rows[1].ItemArray[1].ToString();
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
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

            lblRegistros.Text = dgvProductos.Rows.Count.ToString() + " Registros Encontrados";
        }

        //FUNCION PARA EXTRAER LAS UNIDADES PRODUCTOS
        private void extraerUnidadesProductosPadre(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select cg_tipo_unidad, cg_unidad, unidad_compra" + Environment.NewLine;
                sSql += "from cv401_unidades_productos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by unidad_compra desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count >= 2)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][2].ToString() == "1")
                            {
                                iIdUnidadCompra = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                                iTipoUnidadCompra = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                            }

                            else
                            {
                                iIdUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                                iTipoUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                            }
                        }
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No se pudo consultar la unidad de la categoría seleccionada.";
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

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //CONSULTAR EL ID DEL REGISTRO DE LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "select P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos as P, cv401_nombre_productos as NP " + Environment.NewLine;
                sSql += "where NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ok.lblMensaje.Text = "El código ingresado está asignado para el producto " + dtConsulta.Rows[0].ItemArray[1].ToString() + ". Por Favor introduzca uno nuevo.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        return;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //PROCESO DE INSERCIÓN
                //--------------------------------------------------------------------------------------------------

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                if (dbAyudaReceta.iId == 0)
                {
                    iIdReceta = 0;
                }

                else
                {
                    iIdReceta = dbAyudaReceta.iId;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql = sSql + "insert into cv401_productos (" + Environment.NewLine;
                sSql = sSql + "idempresa, codigo, id_Producto_padre, estado, Nivel," + Environment.NewLine;
                sSql = sSql + "modificable, precio_modificable, paga_iva, secuencia," + Environment.NewLine;
                sSql = sSql + "modificador, subcategoria, ultimo_nivel," + Environment.NewLine;
                sSql = sSql + "stock_min, stock_max, Expira, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql = sSql + "terminal_ingreso, id_pos_receta, id_pos_tipo_producto," + Environment.NewLine;
                sSql = sSql + "id_pos_clase_producto, id_pos_impresion_comanda)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + Program.iIdEmpresa + ",'" + txtCodigo.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + iIdPadre + ", 'A', 4, 0, " + iPrecioModificable + ", " + iPagaIva + "," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + ", " + iModificador + "," + Environment.NewLine;
                sSql = sSql + iSubcategoria + ", " + iUltimo + ",0, 0, " + iExpira + ", GETDATE(), '" + Program.sDatosMaximo[0] + "', " + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[1] + "', " + iIdPosReceta + ", " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(cmbClaseProducto.SelectedValue) + ", " + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + ")";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                string sTabla = "cv401_productos";
                string sCampo = "id_Producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
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
                sSql += "id_Producto, cg_tipo_nombre, nombre, nombre_interno, estado, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + "," + iCg_tipoNombre + ", '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor, fecha_inicio," + Environment.NewLine;
                sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "1, " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                sSql += "GETDATE(), 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor, fecha_inicio," + Environment.NewLine;
                sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "4, " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                sSql += "GETDATE(), 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + iIdUnidadCompra + ", " + iTipoUnidadCompra + "," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + iIdUnidadConsumo + ", " + iTipoUnidadConsumo + "," + Environment.NewLine;
                sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')" + Environment.NewLine;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                if (dbAyudaReceta.iId == 0)
                {
                    iIdReceta = 0;
                }

                else
                {
                    iIdReceta = dbAyudaReceta.iId;
                }

                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = " + Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "paga_iva = " + iPagaIva + "," + Environment.NewLine;
                sSql += "modificador = " + iModificador + "," + Environment.NewLine;
                sSql += "subcategoria = " + iSubcategoria + "," + Environment.NewLine;
                sSql += "ultimo_nivel = " + iUltimo + "," + Environment.NewLine;
                sSql += "precio_modificable = " + iPrecioModificable + "," + Environment.NewLine;
                sSql += "Expira = " + iExpira + "," + Environment.NewLine;
                sSql += "id_pos_receta = " + iIdReceta + "," + Environment.NewLine;
                sSql += "id_pos_tipo_producto = " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_clase_producto = " + Convert.ToInt32(cmbClaseProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_impresion_comanda = " + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto;

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI HUBO ALGUN CAMBIO EN EL NOMBRE DEL PRODUCTO, SE REALIZA LA ACTUALIZACION
                if (txtDescripcion.Text.Trim() != sNombreProducto)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = " + iIdProducto;

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_NOMBRE_PRODUCTOS
                    sSql = "";
                    sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                    sSql += "id_Producto, cg_tipo_nombre, nombre, nombre_interno, estado, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdProducto + "," + iCg_tipoNombre + ", '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    sSql += "and id_Lista_Precio = 1";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                    dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor, fecha_inicio," + Environment.NewLine;
                    sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += "1, " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                    sSql += "GETDATE(), 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }


                //SI HUBO ALGUN CAMBIO EN EL PRECIO MINORISTA, SE REALIZA LA ACTUALIZACION
                if (txtPrecioMinorista.Text.Trim() != sPrecioMinorista)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv403_precios_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and id_Lista_Precio = 4";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                    dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor, fecha_inicio," + Environment.NewLine;
                    sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += "4, " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                    sSql += "GETDATE(), 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ELIMINACION DEL PRODUCTO EN CV401_PRODUCTOS
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "(" + iIdProducto + ")'," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ELIMINACION DEL PRODUCTO EN CV401_NOMBRE_PRODUCTOS
                sSql = "";
                sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += " terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ELIMINACION DEL PRODUCTO EN CV401_UNIDADES_PRODUCTOS
                sSql = "";
                sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ELIMINACION DEL PRODUCTO EN CV403_PRECIOS_PRODUCTOS
                sSql = "";
                sSql += "update cv403_precios_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //si se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion
        
        private void frmIngresoSubProductos_Load(object sender, EventArgs e)
        {
            limpiarTodo();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSubcategoria.SelectedValue) == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione la Subcategoría para ingresar registros.";
                ok.ShowDialog();
                cmbSubcategoria.Focus();
                return;
            }

            if (btnAgregar.Text == "Nuevo")
            {
                grupoDatos.Enabled = true;
                grupoReceta.Enabled = true;
                btnAgregar.Text = "Guardar";
                cmbSubcategoria.Enabled = false;
                txtCodigo.Focus();
            }

            else
            {
                iIdPadre = Convert.ToInt32(cmbSubcategoria.SelectedValue);

                if (txtCodigo.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese un código para el producto.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese un nombre para el producto.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (txtPrecioCompra.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el precio de compra para el producto.";
                    ok.ShowDialog();
                    txtPrecioCompra.Focus();
                }

                else if (txtPrecioMinorista.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el precio de venta para el producto.";
                    ok.ShowDialog();
                    txtPrecioMinorista.Focus();
                }

                else if (txtSecuencia.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese una secuencia de orden para el producto.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                }

                else if (Convert.ToInt32(cmbTipoProducto.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione el tipo de producto.";
                    ok.ShowDialog();
                    cmbTipoProducto.Focus();
                }

                else if (Convert.ToInt32(cmbClaseProducto.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione la clase de producto.";
                    ok.ShowDialog();
                    cmbClaseProducto.Focus();
                }

                else
                {
                    if (btnAgregar.Text == "Guardar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea guardar el registro...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else if (btnAgregar.Text == "Actualizar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea actualizar el registro...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
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
            iCuenta = verificarProductoComanda();

            if (iCuenta == 0)
            {
                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            else if (iCuenta > 0)
            {
                ok.lblMensaje.Text = "No puede eliminar el registro, ya que está en uso dentro del sistema.";
                ok.ShowDialog();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
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
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dBAyudaCategorias.iId == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione la categoría para verificar los productos.";
                ok.ShowDialog();
            }

            else
            {
                limpiar();
                iIdCategoria = dBAyudaCategorias.iId;
                lblNombreCategoria.Text = dBAyudaCategorias.txtDatos.Text.Trim().ToUpper();
                extraerUnidadesProductosPadre(iIdCategoria);
                llenarComboSubCategorias();
                txtBuscar.Clear();
                grupoRegistros.Enabled = true;
                btnAgregar.Enabled = true;
                txtBuscar.Focus();
            }
        }

        private void btnLimpiarTodo_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void cmbSubcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid(0);
        }
    }
}
