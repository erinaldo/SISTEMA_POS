using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmIngresoSubProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        bool bRespuesta;

        SqlParameter[] parametro;

        DataTable dtConsulta;
        DataTable dtSubCategorias;

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
        int iIdListaBase;
        int iIdListaMinorista;
        int iPagaServicio;
        int iHabilitado;

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
        string sFechaListaBase;
        string sFechaListaMinorista;
        string sTabla;
        string sCampo;
        string sFechaInicio;
        string sNombreInterno = "1";

        long iMaximo;

        public frmIngresoSubProductos()
        {
            InitializeComponent();
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
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
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

        //Función para llenar el combo de clase de producto
        private void llenarComboClaseProducto()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_clase_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_clase_producto" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "order by descripcion";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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
                row["id_pos_clase_producto"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbClaseProducto.DisplayMember = "descripcion";
                cmbClaseProducto.ValueMember = "id_pos_clase_producto";
                cmbClaseProducto.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "order by descripcion";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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
                row["id_pos_tipo_producto"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoProducto.DisplayMember = "descripcion";
                cmbTipoProducto.ValueMember = "id_pos_tipo_producto";
                cmbTipoProducto.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                sSql += "select P.id_producto, NP.nombre, P.codigo" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and id_producto_padre = @id_producto_padre" + Environment.NewLine;
                sSql += "and P.nivel = @nivel" + Environment.NewLine;
                sSql += "and P.estado = @estado_1" + Environment.NewLine;
                sSql += "and NP.estado = @estado_2";

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_producto_padre";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdCategoria;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@nivel";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 3;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_1";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado_2";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

                dtSubCategorias = new DataTable();
                dtSubCategorias.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtSubCategorias, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtSubCategorias.NewRow();
                row["id_producto"] = "0";
                row["nombre"] = "Seleccione...!!!";
                dtSubCategorias.Rows.InsertAt(row, 0);

                cmbSubcategoria.DisplayMember = "nombre";
                cmbSubcategoria.ValueMember = "id_producto";
                cmbSubcategoria.DataSource = dtSubCategorias;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                sSql += "and P.modificador = 0" + Environment.NewLine;
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
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "order by descripcion";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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
                row["id_pos_impresion_comanda"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbDestinoImpresion.DisplayMember = "descripcion";
                cmbDestinoImpresion.ValueMember = "id_pos_impresion_comanda";
                cmbDestinoImpresion.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
            txtPrecioMinorista.Text = "1.00";
            txtPresentacion.Text = "1";
            txtRendimiento.Text = "1";
            txtBuscar.Clear();

            chkPagaIVA.Checked = true;
            chkExpira.Checked = false;
            chkPrecioModificable.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

            if (Program.iManejaServicio == 1)
                chkPagaServicio.Checked = true;
            else
                chkPagaServicio.Checked = false;

            grupoDatos.Enabled = false;
            grupoReceta.Enabled = false;

            btnAgregar.Text = "Nuevo";
            btnEliminar.Enabled = false;
            txtCodigo.Enabled = true;

            llenarComboTipoProducto();
            llenarComboClaseProducto();
            llenarDestinoImpresion();
            //llenarComboSubCategorias();
            iIdProducto = 0;
            cmbSubcategoria.Enabled = true;

            llenarGrid();
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
            txtPrecioMinorista.Text = "1.00";
            txtPresentacion.Text = "1";
            txtRendimiento.Text = "1";
            txtSecuencia.Clear();

            chkPagaIVA.Checked = true;
            chkExpira.Checked = false;
            chkPrecioModificable.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

            if (Program.iManejaServicio == 1)
                chkPagaServicio.Checked = true;
            else
                chkPagaServicio.Checked = false;

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

            cmbSubcategoria.SelectedValue = 0;
            cmbSubcategoria.Enabled = true;

            lblNombreCategoria.Text = "NINGUNA";
            lblRegistros.Text = "0 Registros Encontrados";
        }

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid()
        {
            try
            {
                int a = 1;

                sSql = "";
                sSql += "select * from pos_vw_lista_sub_productos" + Environment.NewLine;
                sSql += "where id_producto_padre = @id_producto_padre" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    a++;
                    sSql += "and descripcion like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by codigo" + Environment.NewLine;

                parametro = new SqlParameter[a];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_producto_padre";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Convert.ToInt32(cmbSubcategoria.SelectedValue);

                if (a == 2)
                {
                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@buscar";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = txtBuscar.Text.Trim();
                }

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

                dgvProductos.DataSource = dtConsulta;

                if (dtConsulta.Rows.Count > 0)
                    completarGrid();

                columnasGrid(false);
                dgvProductos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                    sSql += "select PR.valor" + Environment.NewLine;
                    sSql += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "where id_lista_precio = @id_lista_precio" + Environment.NewLine;
                    sSql += "and P.id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and PR.estado = @estado";

                    parametro = new SqlParameter[3];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_lista_precio";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iIdListaBase;

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@id_producto";
                    parametro[1].SqlDbType = SqlDbType.Int;
                    parametro[1].Value = Convert.ToInt32(dgvProductos.Rows[i].Cells["id_producto"].Value);

                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@estado";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = "A";

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

                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (Program.iCobrarConSinProductos == 1)
                            dgvProductos.Rows[i].Cells["precioCompra"].Value = (Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()) * (1 + Program.iva)).ToString("N2");
                        else
                            dgvProductos.Rows[i].Cells["precioCompra"].Value = (Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString())).ToString("N2");
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No se encuentran registros 1.";
                        ok.ShowDialog();
                        return;
                    }

                    //INSTRUCCION PARA REEMPLAZAR EL VALOR DE LA COLUMNA LISTA MINORISTA
                    sSql = "";
                    sSql += "select PR.valor" + Environment.NewLine;
                    sSql += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "where id_lista_precio = @id_lista_precio" + Environment.NewLine;
                    sSql += "and P.id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and PR.estado = @estado";

                    parametro = new SqlParameter[3];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_lista_precio";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iIdListaMinorista;

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@id_producto";
                    parametro[1].SqlDbType = SqlDbType.Int;
                    parametro[1].Value = Convert.ToInt32(dgvProductos.Rows[i].Cells["id_producto"].Value);

                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@estado";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = "A";

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

                    if (dtConsulta.Rows.Count > 0)
                    {
                        iPagaIva = Convert.ToInt32(dgvProductos.Rows[i].Cells["paga_iva"].Value);
                        iPagaServicio = Convert.ToInt32(dgvProductos.Rows[i].Cells["paga_servicio"].Value);

                        if (Program.iCobrarConSinProductos == 1)
                        {
                            if (iPagaServicio == 1)
                            {
                                if (iPagaIva == 1)
                                    dgvProductos.Rows[i].Cells["PVP"].Value = (Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()) * (1 + Program.iva + Program.servicio)).ToString("N2");
                                else
                                    dgvProductos.Rows[i].Cells["PVP"].Value = (Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()) * (1 + Program.servicio)).ToString("N2");
                            }

                            else
                            {
                                if (iPagaIva == 1)
                                    dgvProductos.Rows[i].Cells["PVP"].Value = (Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()) * (1 + Program.iva)).ToString("N2");
                                else
                                    dgvProductos.Rows[i].Cells["PVP"].Value = Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()).ToString("N2");
                            }
                        }

                        else
                        {
                            dgvProductos.Rows[i].Cells["PVP"].Value = Convert.ToDouble(dtConsulta.Rows[0]["valor"].ToString()).ToString("N2");
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                        ok.ShowDialog();
                        return;
                    }

                    sSql = "";
                    sSql += "select id_producto_padre" + Environment.NewLine;
                    sSql += "from cv401_productos" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and estado = @estado";

                    parametro = new SqlParameter[2];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_producto";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value);

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@estado";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = "A";

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
                        ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 3.";
                        ok.ShowDialog();
                        return;
                    }

                    else
                        iIdPadre = Convert.ToInt32(dtConsulta.Rows[0]["id_producto_padre"].ToString());

                    sSql = "";
                    sSql += "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                    sSql += "from cv401_unidades_productos UP, tp_codigos TC " + Environment.NewLine;
                    sSql += "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                    sSql += "and UP.id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and UP.estado = @estado";

                    parametro = new SqlParameter[2];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@id_producto";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iIdPadre;

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@estado";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = "A";

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
                        ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta 4.";
                        ok.ShowDialog();
                        return;
                    }

                    else
                    {
                        dgvProductos.Rows[i].Cells[12].Value = dtConsulta.Rows[0]["cg_unidad"].ToString();
                        dgvProductos.Rows[i].Cells[13].Value = dtConsulta.Rows[1]["cg_unidad"].ToString();

                        dgvProductos.Rows[i].Cells[14].Value = dtConsulta.Rows[0]["valor_texto"].ToString();
                        dgvProductos.Rows[i].Cells[15].Value = dtConsulta.Rows[1]["valor_texto"].ToString();
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
            dgvProductos.Columns[2].Width = 170;
            dgvProductos.Columns[4].Width = 55;
            dgvProductos.Columns[6].Width = 75;
            dgvProductos.Columns[33].Width = 30;

            dgvProductos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns[33].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            dgvProductos.Columns[32].Visible = ok;
            dgvProductos.Columns[34].Visible = ok;

            dgvProductos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        //FUNCION PARA CONTAR SI EXISTEN LO REGISTROS
        private int contarCodigoRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = txtCodigo.Text.Trim().ToUpper();

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
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

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
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
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                if (dbAyudaReceta.iId == 0)
                    iIdReceta = 0;
                else
                    iIdReceta = dbAyudaReceta.iId;

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_productos (" + Environment.NewLine;
                sSql += "idempresa, codigo, id_Producto_padre, estado, Nivel, modificable," + Environment.NewLine;
                sSql += "precio_modificable, paga_iva, secuencia, subcategoria," + Environment.NewLine;
                sSql += "ultimo_nivel, stock_min, stock_max, Expira, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, id_pos_receta, id_pos_tipo_producto," + Environment.NewLine;
                sSql += "id_pos_clase_producto, id_pos_impresion_comanda, " + Environment.NewLine;
                sSql += "presentacion, rendimiento, is_active, paga_servicio)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@idempresa, @codigo, @id_producto_padre, @estado, @nivel, @modificable," + Environment.NewLine;
                sSql += "@precio_modificable, @paga_iva, @secuencia, @subcategoria," + Environment.NewLine;
                sSql += "@ultimo_nivel, @stock_min, @stock_max, @expira, getdate(), @usuario_ingreso," + Environment.NewLine;
                sSql += "@terminal_ingreso, @id_pos_receta, @id_pos_tipo_producto," + Environment.NewLine;
                sSql += "@id_pos_clase_producto, @id_pos_impresion_comanda," + Environment.NewLine;
                sSql += "@presentacion, @rendimiento, @is_active, @paga_servicio)";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[24];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@idempresa";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Program.iIdEmpresa;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtCodigo.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto_padre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdPadre;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 4;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@precio_modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPrecioModificable;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@paga_iva";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagaIva;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@secuencia";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtSecuencia.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@subcategoria";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@ultimo_nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@stock_min";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@stock_max";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@expira";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iExpira;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_receta";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdPosReceta;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_tipo_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbTipoProducto.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_clase_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbClaseProducto.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_impresion_comanda";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbDestinoImpresion.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@presentacion";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtPresentacion.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@rendimiento";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtRendimiento.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@paga_servicio";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagaServicio;

                #endregion

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                sTabla = "cv401_productos";
                sCampo = "id_Producto"; ;

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdProducto = Convert.ToInt32(iMaximo);

                //INSERTAR EN LA TABLA CV401_NOMBRES_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_nombre, @nombre, @nombre_interno, @estado," + Environment.NewLine;
                sSql += "@numero_replica_trigger, @numero_control_replica, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[9];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_nombre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iCg_tipoNombre;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre_interno";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sNombreInterno;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                if (Program.iCobrarConSinProductos == 1)
                    dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + Program.iva);
                else
                    dSubtotal = Convert.ToDouble(txtPrecioCompra.Text);

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_lista_precio, id_producto, valor_porcentaje, valor, fecha_inicio," + Environment.NewLine;
                sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@id_lista_precio, @id_producto, @valor_porcentaje, @valor, @fecha_inicio," + Environment.NewLine;
                sSql += "@fecha_final, @estado, @numero_replica_trigger, @numero_control_replica," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_lista_precio";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdListaBase;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@valor_porcentaje";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@valor";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = dSubtotal;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_inicio";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaInicio;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_final";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaListaBase;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                if (Program.iCobrarConSinProductos == 1)
                {
                    if (chkPagaServicio.Checked == true)
                    {
                        if (chkPagaIVA.Checked == true)
                            dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + Program.iva + Program.servicio);
                        else
                            dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + Program.servicio);
                    }

                    else
                    {
                        if (chkPagaIVA.Checked == true)
                            dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + Program.iva);
                        else
                            dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text);
                    }
                }

                else
                {
                    dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text);
                }

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_lista_precio, id_producto, valor_porcentaje, valor, fecha_inicio," + Environment.NewLine;
                sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso,usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@id_lista_precio, @id_producto, @valor_porcentaje, @valor, @fecha_inicio," + Environment.NewLine;
                sSql += "@fecha_final, @estado, @numero_replica_trigger, @numero_control_replica," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_lista_precio";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdListaMinorista;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@valor_porcentaje";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@valor";
                parametro[a].SqlDbType = SqlDbType.Decimal;
                parametro[a].Value = dSubtotal;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_inicio";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaInicio;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_final";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaListaMinorista;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                
                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdUnidadCompra;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iTipoUnidadCompra;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@unidad_compra";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdUnidadConsumo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iTipoUnidadConsumo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@unidad_compra";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                if (dbAyudaReceta.iId == 0)
                    iIdReceta = 0;
                else
                    iIdReceta = dbAyudaReceta.iId;

                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = " + Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "paga_iva = " + iPagaIva + "," + Environment.NewLine;
                sSql += "paga_servicio = " + iPagaServicio + "," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + "," + Environment.NewLine;
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

            if (Program.iManejaServicio == 1)
            {
                chkPagaServicio.Checked = true;
                chkPagaServicio.Enabled = true;
            }

            else
            {
                chkPagaServicio.Checked = false;
                chkPagaServicio.Enabled = false;
            }

            datosListas();
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSubcategoria.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
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
                return;
            }

            else
            {
                iIdPadre = Convert.ToInt32(cmbSubcategoria.SelectedValue);

                if (txtCodigo.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese un código para el producto.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                    return;
                }

                if (txtDescripcion.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese un nombre para el producto.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                    return;
                }

                if (txtPrecioCompra.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el precio de compra para el producto.";
                    ok.ShowDialog();
                    txtPrecioCompra.Focus();
                    return;
                }

                if (txtPrecioMinorista.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el precio de venta para el producto.";
                    ok.ShowDialog();
                    txtPrecioMinorista.Focus();
                    return;
                }

                if (txtSecuencia.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese una secuencia de orden para el producto.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                    return;
                }

                if (Convert.ToInt32(cmbTipoProducto.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el tipo de producto.";
                    ok.ShowDialog();
                    cmbTipoProducto.Focus();
                    return;
                }

                if (Convert.ToInt32(cmbClaseProducto.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la clase de producto.";
                    ok.ShowDialog();
                    cmbClaseProducto.Focus();
                    return;
                }

                if (chkPagaIVA.Checked == true)
                    iPagaIva = 1;
                else
                    iPagaIva = 0;

                if (chkExpira.Checked == true)
                    iExpira = 1;
                else
                    iExpira = 0;

                if (chkPrecioModificable.Checked == true)
                    iPrecioModificable = 1;
                else
                    iPrecioModificable = 0;

                if (chkPagaServicio.Checked == true)
                    iPagaServicio = 1;
                else
                    iPagaServicio = 0;

                if (chkHabilitado.Checked == true)
                    iHabilitado = 1;
                else
                    iHabilitado = 0;

                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

                if (btnAgregar.Text == "Guardar")
                {
                    int iCuenta_P = contarCodigoRegistro();

                    if (iCuenta_P == -1)
                        return;

                    if (iCuenta_P > 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "El código ingresado ya existe en el sistema. Favor ingrese un nuevo código.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        return;
                    }

                    NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                        insertarRegistro();
                }

                else if (btnAgregar.Text == "Actualizar")
                {
                    NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                        actualizarRegistro();
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
                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            else if (iCuenta > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No puede eliminar el registro, ya que está en uso dentro del sistema.";
                ok.ShowDialog();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
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
                ok = new VentanasMensajes.frmMensajeNuevoOk();
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
            if (Convert.ToInt32(cmbSubcategoria.SelectedValue) == 0)
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                dgvProductos.DataSource = dtConsulta;
                cmbSubcategoria.Focus();
                return;
            }

            llenarGrid();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //RECUPERACION DE DATOS
                iIdProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["id_producto"].Value);
                txtCodigo.Text = dgvProductos.CurrentRow.Cells["codigo"].Value.ToString().Trim();
                txtDescripcion.Text = dgvProductos.CurrentRow.Cells["descripcion"].Value.ToString().Trim();
                sNombreProducto = dgvProductos.CurrentRow.Cells["descripcion"].Value.ToString().Trim().ToUpper();
                txtPrecioCompra.Text = dgvProductos.CurrentRow.Cells["precioCompra"].Value.ToString();
                sPrecioBase = dgvProductos.CurrentRow.Cells["precioCompra"].Value.ToString();
                txtPrecioMinorista.Text = dgvProductos.CurrentRow.Cells["PVP"].Value.ToString();
                sPrecioMinorista = dgvProductos.CurrentRow.Cells["PVP"].Value.ToString();

                //CHECKED IVA
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["paga_iva"].Value) == 1)
                    chkPagaIVA.Checked = true;
                else
                    chkPagaIVA.Checked = false;
                //-----------------------------------------------------------------------------------

                txtSecuencia.Text = dgvProductos.CurrentRow.Cells["secuencia"].Value.ToString();

                //CHECKED PRECIO MODIFICABLE
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["Prec_Modificable"].Value) == 1)
                    chkPrecioModificable.Checked = true;
                else
                    chkPrecioModificable.Checked = false;
                //-----------------------------------------------------------------------------------

                //CHECKED EXPIRA
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["Expira"].Value) == 1)
                    chkExpira.Checked = true;
                else
                    chkExpira.Checked = false;
                //-----------------------------------------------------------------------------------

                cmbDestinoImpresion.SelectedValue = dgvProductos.CurrentRow.Cells["id_pos_impresion_comanda"].Value.ToString();
                cmbClaseProducto.SelectedValue = dgvProductos.CurrentRow.Cells["id_pos_clase_producto"].Value.ToString();
                cmbTipoProducto.SelectedValue = dgvProductos.CurrentRow.Cells["id_pos_tipo_producto"].Value.ToString();

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["id_pos_receta"].Value.ToString()) == 0)
                    dbAyudaReceta.limpiar();
                else
                {
                    dbAyudaReceta.iId = Convert.ToInt32(dgvProductos.CurrentRow.Cells["id_pos_receta"].Value.ToString());
                    dbAyudaReceta.txtIdentificacion.Text = dgvProductos.CurrentRow.Cells["codigo_receta"].Value.ToString();
                    dbAyudaReceta.txtDatos.Text = dgvProductos.CurrentRow.Cells["descripcion_receta"].Value.ToString();
                }

                //CHECKED HABILITADO
                //-----------------------------------------------------------------------------------
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["is_active"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;
                //-----------------------------------------------------------------------------------

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells["paga_servicio"].Value) == 1)
                    chkPagaServicio.Checked = true;
                else
                    chkPagaServicio.Checked = false;

                txtPresentacion.Text = dgvProductos.CurrentRow.Cells["presentacion"].Value.ToString();
                txtRendimiento.Text = dgvProductos.CurrentRow.Cells["rendimiento"].Value.ToString();

                cmbSubcategoria.Enabled = false;
                btnAgregar.Text = "Actualizar";
                btnEliminar.Enabled = true;
                txtCodigo.Enabled = false;
                txtDescripcion.Focus();
                grupoDatos.Enabled = true;
                chkHabilitado.Enabled = true;
                chkHabilitado.Enabled = true;
                grupoReceta.Enabled = true;
                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
        {
            if (txtPrecioCompra.Text.Trim() == "")
                txtPrecioCompra.Text = "1.00";
        }

        private void txtPrecioMinorista_Leave(object sender, EventArgs e)
        {
            if (txtPrecioMinorista.Text.Trim() == "")
                txtPrecioMinorista.Text = "1.00";
        }

        private void txtPresentacion_Leave(object sender, EventArgs e)
        {
            if (txtPresentacion.Text.Trim() == "")
            {
                txtPresentacion.Text = "1";
            }

            else if (Convert.ToDecimal(txtPresentacion.Text.Trim()) == 0)
            {
                txtPresentacion.Text = "1";
            }
        }

        private void txtPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtRendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtRendimiento_Leave(object sender, EventArgs e)
        {
            if (txtRendimiento.Text.Trim() == "")
            {
                txtRendimiento.Text = "1";
            }

            else if (Convert.ToDecimal(txtRendimiento.Text.Trim()) == 0)
            {
                txtRendimiento.Text = "1";
            }
        }
    }
}
