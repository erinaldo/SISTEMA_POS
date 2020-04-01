using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmIngresoProductosCategoria : Form
    {
        //VARIABLES NECESARIAS PARA TRABAJAR EN EL FORMULARIO
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        DataTable dtConsulta;
        
        bool bRespuesta = false;        
        
        Double dSubtotal = 0;

        string sSql;
        string sFecha;
        string sIdCategoria;
        string sFechaListaBase;
        string sFechaListaMinorista;
        string sFechaInicio;

        string sNombreProducto;
        string sPrecioBase;
        string sPrecioMinorista;

        public int iIdCategoria = 0;
        int iIdProducto = 0;
        int iIdUnidadCompra;
        int iIdUnidadConsumo;
        int iPagIva;
        int iPreModific;
        int iExpira;
        int iNivel;
        int iVerCombo;
        int iUltimo = 1;
        int iModificador = 0;
        int iSubcategoria = 0;
        int iCg_tipoNombre = 5076;
        int iIdPadre;
        int iIdListaBase;
        int iIdListaMinorista;
        int iBanderaRecetaInsumo;
        int iIdPosReceta;
        int iIdPosReferenciaInsumo;

        public frmIngresoProductosCategoria(int iNivelRecibido, int iVerCombo)
        {
            this.iVerCombo = iVerCombo;
            this.iNivel = iNivelRecibido;
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
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //HABILITAR GRUPOS
        private void habilitarGrupos(bool ok)
        {
            grupoDatos.Enabled = ok;
            grupoPrecio.Enabled = ok;
            grupoOpciones.Enabled = ok;
            grupoStock.Enabled = ok;
            grupoImpresion.Enabled = ok;
            grupoReceta.Enabled = ok;
        }

        //LLENAR COMBO DE LOCALIDADES
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

                if (cmbDestinoImpresion.Items.Count > 0)
                {
                    cmbDestinoImpresion.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR U OCULTAR LAS COLUMAS DEL GRID
        private void columnasGrid(bool ok)
        {
            dgvProductos.Columns[0].Visible = ok;
            dgvProductos.Columns[12].Visible = ok;
            dgvProductos.Columns[13].Visible = ok;
            dgvProductos.Columns[17].Visible = ok;
            dgvProductos.Columns[18].Visible = ok;
            dgvProductos.Columns[19].Visible = ok;
            dgvProductos.Columns[20].Visible = ok;
            dgvProductos.Columns[21].Visible = ok;
            dgvProductos.Columns[22].Visible = ok;
            dgvProductos.Columns[23].Visible = ok;
            dgvProductos.Columns[24].Visible = ok;
            dgvProductos.Columns[25].Visible = ok;
        }

        //LLENAR EL COMBO DE LAS CATEGORIAS
        private void llenarComboCategorias()
        {
            try
            {
                
                sSql = "";
                sSql += "select P.id_producto ,NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP " + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre in " + Environment.NewLine;
                sSql += "(select id_producto from cv401_productos " + Environment.NewLine;
                sSql += "where codigo = '" + cmbPadre.SelectedValue + "')" + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and subcategoria = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbCategorias.llenar(dtConsulta, sSql);                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION SELECT PARA OBTENER EL NOMBRE DE LA CATEGORIA
        private void consultarNombreCategoria()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto_padre" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + txtIdCategoria.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.lblMensaje.Text = "No existe un registro con el código ingresado.";
                        ok.ShowDialog();
                        txtIdCategoria.Text = "";
                        sIdCategoria = "";
                        txtDescripcion.Text = "";
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre " + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto " + Environment.NewLine;
                sSql += "and P.id_producto_padre in" + Environment.NewLine;
                sSql += "(select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo ='2') " + Environment.NewLine;
                sSql += "and P.nivel = " + (iNivel - 1) + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.codigo = '" + sIdCategoria + "'";
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtDescripcion.Text = dtConsulta.Rows[0].ItemArray[2].ToString();
                        llenarGrid(0);
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No existe ningún registro con el código ingresado.";
                        ok.ShowDialog();
                        limpiar();
                    }
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
                limpiar();
            }
        fin:
            { }
        }

        //FUNCION SELECT PARA OBTENER EL NOMBRE DE LA SUBCATEGORIA
        private void consultarNombreSubCategoria()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto_padre" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + txtIdCategoria.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.lblMensaje.Text = "No existe un registro con el código ingresado.";
                        ok.ShowDialog();
                        txtIdCategoria.Text = "";
                        sIdCategoria = "";
                        txtDescripcion.Text = "";
                        goto fin;
                    }                   
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }


                cmbCategorias.SelectedValue = dtConsulta.Rows[0].ItemArray[0].ToString();

                sSql = "";
                sSql += "select P.id_producto, P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP " + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre = " + Convert.ToInt32(cmbCategorias.SelectedValue) + Environment.NewLine;
                sSql += "and P.nivel = " + (iNivel - 1) + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and subcategoria = 1" + Environment.NewLine;
                sSql += "and P.codigo = '" + sIdCategoria + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtDescripcion.Text = dtConsulta.Rows[0].ItemArray[2].ToString();
                        llenarGrid(0);
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No existe ningún registro con el código ingresado.";
                        ok.ShowDialog();
                        limpiar();
                    }
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
                limpiar();
            }

        fin:
            { }
        }

        //FUNCION LIMPIAR NUEVO
        private void limpiarNuevo()
        {
            iIdProducto = 0;
            //LLenarComboColor();
            LLenarComboCompra();
            LLenarComboConsumo();
            llenarDestinoImpresion();
            txtBuscarProducto.Clear();
            txtCodigoProducto.Clear();
            txtNombreProducto.Clear();
            txtSecuencia.Clear();
            txtPrecioCompra.Clear();
            txtPrecioMinorista.Clear();
            txtStockMin.Clear();
            txtStockMax.Clear();
            txtSecuencia.Clear();
            cmbTipoProducto.SelectedIndex = 0;
            cmbClaseProducto.SelectedIndex = 0;
            //cmbTipoProducto.SelectedValue = buscarRegistro();

            chkExpiraProductos.Checked = false;
            chkPagaIva.Checked = false;
            chkPreModifProductos.Checked = false;

            btnAgregar.Text = "Nuevo";
            txtCodigoProducto.Enabled = true;
            txtCodigoProducto.Focus();
            dbAyudaReceta.txtDatos.Text = "";
            dbAyudaReceta.txtIdentificacion.Text = "";

        }

        //FUNCION PARA LIMPIAR LAS CAJAS DE TEXTO PARA REGRESAR TODO POR DEFAULT
        private void limpiar()
        {
            sIdCategoria = "";
            iIdProducto = 0;
            iIdCategoria = 0;
            //LLenarComboColor();
            LLenarComboCompra();
            LLenarComboConsumo();
            llenarDestinoImpresion();
            txtIdCategoria.Clear();
            txtDescripcion.Clear();
            txtBuscarProducto.Clear();
            txtCodigoProducto.Clear();
            txtNombreProducto.Clear();
            txtSecuencia.Clear();
            txtPrecioCompra.Clear();
            txtPrecioMinorista.Clear();
            txtStockMin.Clear();
            txtStockMax.Clear();
            txtSecuencia.Clear();
            cmbTipoProducto.SelectedIndex = 0;
            cmbClaseProducto.SelectedIndex = 0;
            //cmbTipoProducto.SelectedValue = buscarRegistro();

            dtConsulta = new DataTable();
            dgvProductos.DataSource = dtConsulta;

            habilitarGrupos(false);
            grupoBotones.Enabled = false;

            chkExpiraProductos.Checked = false;
            chkPagaIva.Checked = false;
            chkPreModifProductos.Checked = false;

            btnAgregar.Text = "Nuevo";
            txtCodigoProducto.Enabled = true;
            txtIdCategoria.Focus();

            dbAyudaReceta.txtDatos.Text = "";
            dbAyudaReceta.txtIdentificacion.Text = "";

        }

        //FUNCION PARA LLENAR EL COMBO DE COMPRA
        private void LLenarComboCompra()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbCompra.llenar(dtConsulta, sSql);
                cmbCompra.SelectedIndex = 24;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE CONSUMO
        private void LLenarComboConsumo()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                cmbConsumo.llenar(dtConsulta, sSql);
                cmbConsumo.SelectedIndex = 24;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LOS DATOS EN LOS COMOBOBOX, ESTOS NO SE DEBEN EDITAR
        private void seleccionarDatosCombobox()
        {
            try
            {
                sSql = "";
                sSql += "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                sSql += "from cv401_unidades_productos UP, tp_codigos TC" + Environment.NewLine;
                sSql += "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                sSql += "and UP.id_producto = " + iIdCategoria + Environment.NewLine;
                sSql += " and UP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbCompra.SelectedValue = dtConsulta.Rows[0].ItemArray[0].ToString();
                        cmbCompra.SelectedValue = dtConsulta.Rows[1].ItemArray[0].ToString();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        fin:
            { }
        }

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid(int op)
        {
            try
            {                  
                sSql = "";

                if (iVerCombo == 0)
                {
                    sSql += "select * from pos_vw_lista_productos" + Environment.NewLine;
                }

                else
                {
                    sSql += "select * from pos_vw_lista_productos_subcategoria" + Environment.NewLine;
                }

                sSql += "where id_producto_padre in" + Environment.NewLine;
                sSql += "(select id_Producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + sIdCategoria + "')" + Environment.NewLine;

                if (op == 1)
                {
                    sSql += "and descripcion like '%" + txtBuscarProducto.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvProductos.DataSource = dtConsulta;
                    completarGrid();
                    columnasGrid(false);
                    grupoGrid.Enabled = true;
                    grupoBotones.Enabled = true;
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
                    sSql += "where id_lista_precio = 1" + Environment.NewLine;
                    sSql += "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and PR.estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvProductos.Rows[i].Cells[3].Value = (Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString()) * (1+ Program.iva + Program.servicio)).ToString("n2");
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
                    sSql += "select PR.valor" + Environment.NewLine;
                    sSql += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql += "where id_lista_precio = 4" + Environment.NewLine;
                    sSql += "and P.id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and pr.estado='A'";

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
                    sSql += "select id_producto_padre" + Environment.NewLine;
                    sSql += "from cv401_productos" + Environment.NewLine;
                    sSql += "where id_producto = " + Convert.ToInt32(dgvProductos.Rows[i].Cells[0].Value) + Environment.NewLine;
                    sSql += "and estado = 'A'";

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
                    sSql += "select UP.cg_unidad, TC.valor_texto" + Environment.NewLine;
                    sSql += "from cv401_unidades_productos UP, tp_codigos TC " + Environment.NewLine;
                    sSql += "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                    sSql += "and UP.id_producto = " + iIdPadre + Environment.NewLine;
                    sSql += "and UP.estado = 'A'";
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

                    //OCULTAR COLUMAS Y PONER TAMAÑOS AL DATAGRID VIEW
                    dgvProductos.Columns[1].Width = 75;
                    dgvProductos.Columns[2].Width = 170;
                    dgvProductos.Columns[4].Width = 55;
                    dgvProductos.Columns[6].Width = 75;

                    dgvProductos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgvProductos.Columns[0].Visible = false;
                    dgvProductos.Columns[3].Visible = false;
                    dgvProductos.Columns[5].Visible = false;
                    dgvProductos.Columns[7].Visible = false;
                    dgvProductos.Columns[8].Visible = false;
                    dgvProductos.Columns[9].Visible = false;
                    dgvProductos.Columns[10].Visible = false;
                    dgvProductos.Columns[11].Visible = false;
                    dgvProductos.Columns[12].Visible = false;
                    dgvProductos.Columns[13].Visible = false;
                    dgvProductos.Columns[14].Visible = false;
                    dgvProductos.Columns[15].Visible = false;
                    dgvProductos.Columns[16].Visible = false;
                    dgvProductos.Columns[17].Visible = false;

                    //this.dgvProductos.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
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
                    sSql += "codigo = '" + txtCodigoProducto.Text.Trim() + "(" + iIdProducto + ")'," + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //si se ejecuta bien hara un commit
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiarNuevo();
                    btnAgregar.Text = "Nuevo";
                    llenarGrid(1);
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin:
            { }
        }

        //FUNCION PARA INSERTAR UN NUEVO REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //CONSULTAR EL ID DEL REGISTRO DE LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "select P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos as P, cv401_nombre_productos as NP " + Environment.NewLine;
                sSql += "where NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.codigo = '" + txtCodigoProducto.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ok.lblMensaje.Text = "El código ingresado está o fue asignado para el producto " + dtConsulta.Rows[0].ItemArray[1].ToString() + ". Por Favor introduzca uno nuevo.";
                        ok.ShowDialog();
                        txtCodigoProducto.Clear();
                        txtCodigoProducto.Focus();
                        goto fin;
                    }
                }

                else
                {
                    ok.lblMensaje.Text = sSql;
                    ok.ShowDialog();
                    goto fin;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto fin;
                }
                
                sFechaInicio = Program.sFechaSistema.ToString("yyyy/MM/dd");

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
                sSql += "id_pos_clase_producto, id_pos_impresion_comanda, id_bod_referencia)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ",'" + txtCodigoProducto.Text.Trim() + "'," + Environment.NewLine;
                sSql += iIdCategoria + ", 'A', " + iNivel + ", 0, " + iPreModific + ", " + iPagIva + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + ", " + iModificador + "," + Environment.NewLine;
                sSql += iSubcategoria + ", " + iUltimo + "," + Environment.NewLine;
                sSql += Convert.ToDouble(txtStockMin.Text.ToString().Trim()) + ", " + Convert.ToDouble(txtStockMax.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += iExpira + ", GETDATE(), '" + Program.sDatosMaximo[0] + "', " + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdPosReceta + ", " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbClaseProducto.SelectedValue) + ", " + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + "," + Environment.NewLine;
                sSql += iIdPosReferenciaInsumo + ")";                

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
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
                sSql += iIdProducto + "," + iCg_tipoNombre + ",'" + txtNombreProducto.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                sSql += "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                sSql += "'" + sFechaListaBase + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                sSql += "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                sSql += "'" + sFechaListaMinorista + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql +=" usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + Program.iUnidadCompraConsumo + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + "," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "',"  + Environment.NewLine;
                sSql += "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdProducto + ", " + (Program.iUnidadCompraConsumo + 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + "," + Environment.NewLine;
                sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado correctamente.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                limpiarNuevo();
                habilitarGrupos(false);
                llenarGrid(1);
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin:
            { }
        }

        //FUNCION PARA ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto reversa;
                }

                sFechaInicio = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = '" + txtSecuencia.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql += "paga_iva = " + iPagIva + "," + Environment.NewLine;
                sSql += "modificador = " + iModificador + "," + Environment.NewLine;
                sSql += "subcategoria = " + iSubcategoria + "," + Environment.NewLine;
                sSql += "ultimo_nivel = " + iUltimo + "," + Environment.NewLine;
                sSql += "stock_min = " + Convert.ToInt32(txtStockMin.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "stock_max = " + Convert.ToInt32(txtStockMax.Text.ToString().Trim()) + "," + Environment.NewLine;
                sSql += "precio_modificable = " + iPreModific + "," + Environment.NewLine;
                sSql += "Expira = " + iExpira + "," + Environment.NewLine;

                if (dbAyudaReceta.txtDatos.Text.Trim() == "")
                {
                    sSql += "id_pos_receta = null," + Environment.NewLine;
                }
                else
                {
                    sSql += "id_pos_receta = " + dbAyudaReceta.iId + "," + Environment.NewLine;
                }
                
                sSql += "id_pos_tipo_producto = " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_clase_producto = " + Convert.ToInt32(cmbClaseProducto.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_impresion_comanda = " + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + Environment.NewLine;
                sSql += "where id_Producto = '" + iIdProducto + "'";


                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI HUBO ALGUN CAMBIO EN EL NOMBRE DEL PRODUCTO, SE REALIZA LA ACTUALIZACION
                if (txtNombreProducto.Text.Trim() != sNombreProducto)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_Producto = '" + iIdProducto + "'";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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
                    sSql += iIdProducto + "," + iCg_tipoNombre + ",'" + txtNombreProducto.Text.ToString().Trim() + "'," + Environment.NewLine;
                    sSql += "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                    dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                    sSql += "fecha_inicio, fecha_final, estado, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                    sSql += "'" + sFechaListaBase + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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
                    sSql += "and id_Lista_Precio = " + iIdListaMinorista;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                    dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                    sSql += "fecha_inicio, fecha_final, estado,numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", '" + sFechaInicio + "'," + Environment.NewLine;
                    sSql += "'" + sFechaListaMinorista + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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
                    goto reversa;
                }

                int iBandera1 = 0;


                //SI HUBO CAMBIO EM UNIDAD DE COMPRA
                if (iIdUnidadCompra != Convert.ToInt32(cmbCompra.SelectedValue))
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i].ItemArray[2].ToString() == "1")
                            {
                                iBandera1 = 1;
                                break;
                            }
                        }

                        if (iBandera1 == 1)
                        {
                            sSql = "";
                            sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                            sSql += "estado = 'E'," + Environment.NewLine;
                            sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                            sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                            sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                            sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                            sSql += "and cg_tipo_unidad = " + Program.iUnidadCompraConsumo;

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.lblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
                        }
                    }

                    //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                    //INSERTAR LA UNIDAD DE COMPRA
                    sSql = "";
                    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                    sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                    sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                    sSql +=" usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdProducto + ", " + Program.iUnidadCompraConsumo + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + "," + Environment.NewLine;
                    sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                    sSql += "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                iBandera1 = 0;

                //SI HUBO CAMBIO EN UNIDAD DE CONSUMO
                if (iIdUnidadConsumo != Convert.ToInt32(cmbConsumo.SelectedValue))
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i].ItemArray[2].ToString() == "0")
                            {
                                iBandera1 = 1;
                                break;
                            }
                        }

                        if (iBandera1 == 1)
                        {
                            sSql = "";
                            sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                            sSql += "estado = 'E'," + Environment.NewLine;
                            sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                            sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                            sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                            sSql += "where id_Producto = " + iIdProducto + Environment.NewLine;
                            sSql += "and cg_tipo_unidad = " + (Program.iUnidadCompraConsumo - 1);

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.lblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
                        }
                    }

                    //INSERTAR LA UNIDAD DE CONSUMO
                    sSql = "";
                    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                    sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                    sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdProducto + ", " + (Program.iUnidadCompraConsumo - 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + "," + Environment.NewLine;
                    sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                    sSql += "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }


                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiarNuevo();
                habilitarGrupos(false);
                llenarGrid(1);
                goto fin;
                
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin:
            { }
        }

        #endregion

        private void btnLimpiarCajero_Click(object sender, EventArgs e)
        {
            limpiar();            
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            if (txtBuscarProducto.Text == "")
                llenarGrid(0);
            else
                llenarGrid(1);
        }

        private void frmIngresoProductosCategoria_Load(object sender, EventArgs e)
        {
            datosListas();
            LLenarComboCompra();
            LLenarComboConsumo();
            llenarComboTipoProducto();
            llenarComboClaseProducto();            
            llenarDestinoImpresion();

            cmbPadre.SelectedIndexChanged -= new EventHandler(cmbPadre_SelectedIndexChanged);
            LLenarComPadre();
            cmbPadre.SelectedIndexChanged += new EventHandler(cmbPadre_SelectedIndexChanged);

            llenarSentenciaProductoTerminado();

            if (iVerCombo == 1)
            {
                lblEtiquetaCategoria.Visible = true;
                cmbCategorias.Visible = true;
                //CRECER EL PANEL
                llenarComboCategorias();
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
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbClaseProducto.llenar(dtConsulta, sSql);

                if (cmbClaseProducto.Items.Count > 0)
                {
                    cmbClaseProducto.SelectedIndex = 0;
                }

                //if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                //{
                //    cmbClaseProducto.llenar(dtConsulta, sSql);
                //    cmbClaseProducto.SelectedValue = buscarRegistro();
                //}
            }
            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
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
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbTipoProducto.llenar(dtConsulta, sSql);

                if (cmbTipoProducto.Items.Count > 0)
                {
                    cmbTipoProducto.SelectedIndex = 0;
                }
            }

            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        
        //llenar el comboBox Codigo Padre
        private void LLenarComPadre()
        {
            try
            {
                //string sql = "select id_producto,codigo from cv401_productos where codigo='2'";
                sSql = "";
                sSql += "Select PRD.codigo, '['+PRD.codigo+'] '+ NOM.nombre nombre" + Environment.NewLine;
                sSql += "from cv401_productos PRD, cv401_nombre_productos NOM" + Environment.NewLine;
                sSql += "where PRD.nivel = 1" + Environment.NewLine;
                sSql += "and PRD.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and NOM.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and PRD.id_producto = NOM.id_producto" + Environment.NewLine;
                sSql += "and NOM.nombre_interno = 1" + Environment.NewLine;
                sSql += "order by PRD.codigo ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbPadre.llenar(dtConsulta, sSql);
                cmbPadre.SelectedValue = 2;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentenciaMateriaPrima()
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentenciaProductoTerminado()
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbPadre.SelectedValue) != 0)
                {
                    if ((cmbCategorias.Items.Count == 0) && (iNivel == 4))
                    {
                        ok.lblMensaje.Text = "No se ha registrado una categoría que contenga subcategorías.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        if (iNivel == 3)
                        {
                            sSql = "";
                            sSql += "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre" + Environment.NewLine;
                            sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                            sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                            sSql += "and P.id_producto_padre in (select id_producto from cv401_productos where codigo ='" + cmbPadre.SelectedValue + "')" + Environment.NewLine;
                            sSql += "and P.nivel = " + (iNivel - 1) + Environment.NewLine;
                            sSql += "and P.estado ='A'" + Environment.NewLine;
                            sSql += "and NP.estado='A'" + Environment.NewLine;
                            sSql += "and P.subcategoria = 0" + Environment.NewLine;

                            if (Convert.ToInt32(cmbPadre.SelectedValue) == 2)
                            {
                                sSql += "and P.menu_pos = 1";
                            }
                        }

                        else if (iNivel == 4)
                        {
                            sSql = "";
                            sSql += "select P.id_producto, P.codigo as Codigo,NP.nombre as Nombre" + Environment.NewLine;
                            sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                            sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                            sSql += "and id_producto_padre = " + Convert.ToInt32(cmbCategorias.SelectedValue) + Environment.NewLine;
                            sSql += "and P.nivel = " + (iNivel - 1) + Environment.NewLine;
                            sSql += "and P.estado ='A'" + Environment.NewLine;
                            sSql += "and NP.estado ='A'" + Environment.NewLine;
                        }

                        Oficina.frmLlenarGridInformacion ventana = new Oficina.frmLlenarGridInformacion(sSql, "NP.nombre as Nombre", "P.id_producto");
                        ventana.ShowInTaskbar = false;
                        ventana.ShowDialog();

                        if (ventana.DialogResult == DialogResult.OK)
                        {
                            limpiarNuevo();
                            habilitarGrupos(false);
                            btnAgregar.Text = "Nuevo";
                            iIdCategoria = ventana.iIdCodigo;
                            txtIdCategoria.Text = ventana.sCodigo;
                            sIdCategoria = ventana.sCodigo;
                            txtDescripcion.Text = ventana.sDescripcion;
                            seleccionarDatosCombobox();
                            llenarGrid(0);
                            ventana.Close();
                        }
                        else
                        {
                            ventana.Close();
                        }
                    }
                }
                else
                {
                    ok.lblMensaje.Text = "Advertencia: Primero Debe seleccionar un código padre";
                    ok.ShowDialog();
                }   
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                sIdCategoria = txtIdCategoria.Text.Trim();
                //INSTRUCCIONES SOLO PARA CATEGORIAS
                if (iNivel == 3)
                {
                    if (txtIdCategoria.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el codigo de la categoría a buscar.";
                        ok.ShowDialog();
                        txtIdCategoria.Focus();
                    }

                    else
                    {
                        consultarNombreCategoria();
                    }
                }

                 //INSTRUCCIONES SOLO PARA SUBCATEGORIAS
                else if (iNivel == 4)
                {
                    if (txtIdCategoria.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el codigo de la sub categoría a buscar.";
                        ok.ShowDialog();
                        txtIdCategoria.Focus();
                    }

                    else if (cmbCategorias.SelectedValue.ToString() == "0")
                    {
                        ok.lblMensaje.Text = "Favor seleccione la categoría para realizar la búsqueda de productos.";
                        ok.ShowDialog();
                        cmbCategorias.Focus();
                    }

                    else
                    {
                        consultarNombreSubCategoria();
                    }
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAgregar.Text == "Nuevo")
                {
                    limpiarNuevo();
                    habilitarGrupos(true);
                    btnAgregar.Text = "Guardar";
                    txtCodigoProducto.Focus();
                }

                else
                {
                    if (txtCodigoProducto.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el código del producto.";
                        ok.ShowDialog();
                        txtCodigoProducto.Focus();
                    }

                    else if (txtNombreProducto.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el nombre del producto.";
                        ok.ShowDialog();
                        txtNombreProducto.Focus();
                    }

                    else if (txtPrecioCompra.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el precio de compra del producto.";
                        ok.ShowDialog();
                        txtPrecioCompra.Focus();
                    }

                    else if (txtPrecioMinorista.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese el precio minorista del producto.";
                        ok.ShowDialog();
                        txtPrecioMinorista.Focus();
                    }

                    else if (Convert.ToInt32(cmbCompra.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione la unidad de compra del producto.";
                        ok.ShowDialog();
                        cmbCompra.Focus();
                    }

                    else if (Convert.ToInt32(cmbConsumo.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione la unidad de consumo del producto.";
                        ok.ShowDialog();
                        cmbConsumo.Focus();
                    }

                    else if (Convert.ToInt32(cmbTipoProducto.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione el tipo de producto.";
                        ok.ShowDialog();
                        cmbCompra.Focus();
                    }

                    else if (Convert.ToInt32(cmbClaseProducto.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione la clase de producto.";
                        ok.ShowDialog();
                        cmbConsumo.Focus();
                    }

                    else if (txtSecuencia.Text == "")
                    {
                        ok.lblMensaje.Text = "Favor ingrese la secuencia del producto.";
                        ok.ShowDialog();
                        txtSecuencia.Focus();
                    }

                    //else if (Convert.ToInt32(cmbDestinoImpresion.SelectedValue) == 0)
                    //{
                    //    ok.lblMensaje.Text = "Favor seleccione la impresora donde se agrupará para la impresión de la comanda.";
                    //    ok.ShowDialog();
                    //    cmbDestinoImpresion.Focus();
                    //}

                    else
                    {
                        if (chkPagaIva.Checked == true)
                            iPagIva = 1;
                        else iPagIva = 0;

                        if (chkPreModifProductos.Checked == true)
                            iPreModific = 1;
                        else iPreModific = 0;

                        if (chkExpiraProductos.Checked == true)
                            iExpira = 1;
                        else iExpira = 0;

                        if (txtStockMin.Text == "")
                        {
                            txtStockMin.Text = "0";
                        }

                        if (txtStockMax.Text == "")
                        {
                            txtStockMax.Text = "0";
                        }

                        if (btnAgregar.Text == "Guardar")
                        {
                            SiNo.lblMensaje.Text = "¿Está seguro de guardar el registro?" + Environment.NewLine + "Asegúrese de que el producto tenga un destino de impresión.";
                            SiNo.ShowDialog();

                            if (SiNo.DialogResult == DialogResult.OK)
                            {
                                insertarRegistro();
                            }
                        }

                        else
                        {
                            //ENVIAR A FUNCION ACTUALIZAR
                            actualizarRegistro();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdProducto == 0)
                {
                    ok.lblMensaje.Text = "No hay un registro para eliminar.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

                else
                {
                    SiNo.lblMensaje.Text = "¿Está seguro de eliminar el registro seleccionado?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        eliminarRegistro();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                btnAgregar.Text = "Nuevo";
                limpiarNuevo();
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                columnasGrid(true);

                dbAyudaReceta.limpiar();

                iIdProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value);   
                txtCodigoProducto.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
                txtNombreProducto.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
                sNombreProducto = dgvProductos.CurrentRow.Cells[2].Value.ToString();
                txtPrecioCompra.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
                sPrecioBase = dgvProductos.CurrentRow.Cells[3].Value.ToString();
                txtPrecioMinorista.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
                sPrecioMinorista = dgvProductos.CurrentRow.Cells[4].Value.ToString();
                txtStockMin.Text = dgvProductos.CurrentRow.Cells[7].Value.ToString();
                txtStockMax.Text = dgvProductos.CurrentRow.Cells[8].Value.ToString();
                txtSecuencia.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[5].Value) == 1)
                    chkPagaIva.Checked = true;

                else
                    chkPagaIva.Checked = false;

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[10].Value) == 1)
                    chkPreModifProductos.Checked = true;

                else
                    chkPreModifProductos.Checked = false;

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[11].Value) == 1)
                    chkExpiraProductos.Checked = true;

                else
                    chkExpiraProductos.Checked = false;

                cmbDestinoImpresion.SelectedValue = Convert.ToInt32(dgvProductos.CurrentRow.Cells[17].Value);
                cmbClaseProducto.SelectedValue = Convert.ToInt32(dgvProductos.CurrentRow.Cells[18].Value);
                cmbTipoProducto.SelectedValue = Convert.ToInt32(dgvProductos.CurrentRow.Cells[19].Value);

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[20].Value.ToString()) != 0)
                {
                    dbAyudaReceta.iId = Convert.ToInt32(dgvProductos.CurrentRow.Cells[20].Value.ToString());
                    dbAyudaReceta.txtIdentificacion.Text = dgvProductos.CurrentRow.Cells[21].Value.ToString();
                    dbAyudaReceta.txtDatos.Text = dgvProductos.CurrentRow.Cells[22].Value.ToString();
                    dbAyudaReceta.sCodigo = dgvProductos.CurrentRow.Cells[21].Value.ToString();
                    dbAyudaReceta.sNombre = dgvProductos.CurrentRow.Cells[22].Value.ToString();
                    rdbReferenciaInsumos.Checked = true;
                }

                else
                {
                    dbAyudaReceta.limpiar();
                }
    

                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[16].Value.ToString()) != 0)
                {
                    dbAyudaReceta.iId = Convert.ToInt32(dgvProductos.CurrentRow.Cells[16].Value.ToString());
                    dbAyudaReceta.txtIdentificacion.Text = dgvProductos.CurrentRow.Cells[23].Value.ToString();
                    dbAyudaReceta.txtDatos.Text = dgvProductos.CurrentRow.Cells[24].Value.ToString();
                    dbAyudaReceta.sCodigo = dgvProductos.CurrentRow.Cells[23].Value.ToString();
                    dbAyudaReceta.sNombre = dgvProductos.CurrentRow.Cells[24].Value.ToString();
                    rdbReceta.Checked = true;
                }

                else
                {
                    dbAyudaReceta.limpiar();
                }

                columnasGrid(false);

                habilitarGrupos(true);
                txtCodigoProducto.Enabled = false;
                btnAgregar.Text = "Actualizar";
                txtNombreProducto.Focus();
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para obtener el id de la receta
        private int getIdPosReceta(int iIdProducto)
        {
            sSql = "";
            sSql += "select id_pos_receta" + Environment.NewLine;
            sSql += "from cv401_productos" + Environment.NewLine;
            sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
            sSql += "and id_pos_receta > 0";

            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda,sSql);
            if (bRespuesta == true)
            {
                if (dtAyuda.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtAyuda.Rows[0].ItemArray[0].ToString());
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        //Función para buscar la clase de producto
        private int buscarIdClaseProducto(int iIdProducto)
        {
            sSql = "";
            sSql += "select id_pos_clase_producto" + Environment.NewLine;
            sSql += "from cv401_productos" + Environment.NewLine;
            sSql += "where id_pos_clase_producto > 0" + Environment.NewLine;
            sSql += "and id_producto = " + iIdProducto;

            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
            if (bRespuesta == true)
            {
                if (dtAyuda.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtAyuda.Rows[0].ItemArray[0].ToString());
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        //Funcion para obtener el idPosTipoProducto
        private int getIdPosTipoProducto(int iIdProducto)
        {
            sSql = "";
            sSql += "select id_pos_tipo_producto" + Environment.NewLine;
            sSql += "from cv401_productos where" + Environment.NewLine;
            sSql += "id_producto = " + iIdProducto + Environment.NewLine;
            sSql += "and estado = 'A'" + Environment.NewLine;
            sSql += "and id_pos_tipo_producto > 0 ";

            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
            if (bRespuesta == true)
            {
                if (dtAyuda.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtAyuda.Rows[0].ItemArray[0].ToString());
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        //Función para llenar el dbAyuda
        private void llenarDbAyuda(int iIdPosReceta)
        {
            try
            {
                sSql = "";
                sSql += "select codigo, descripcion ,id_pos_receta" + Environment.NewLine; 
                sSql += "from pos_receta" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_receta = "+iIdPosReceta;

                DataTable dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda,sSql);
                if (bRespuesta == true)
                {
                    if (dtAyuda.Rows.Count > 0)
                    {
                        dbAyudaReceta.txtIdentificacion.Text = dtAyuda.Rows[0].ItemArray[0].ToString();
                        dbAyudaReceta.txtDatos.Text = dtAyuda.Rows[0].ItemArray[1].ToString();
                        dbAyudaReceta.iId = Convert.ToInt32(dtAyuda.Rows[0].ItemArray[2].ToString());
                    }
                    else
                    {
                        dbAyudaReceta.txtIdentificacion.Text = "";
                        dbAyudaReceta.txtDatos.Text = "";
                    }
                }
                else
                {
                    dbAyudaReceta.txtIdentificacion.Text = "";
                    dbAyudaReceta.txtDatos.Text = "";
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iVerCombo == 1)
            {
                llenarComboCategorias();
                limpiar();
            }

            if (Convert.ToInt32(cmbPadre.SelectedValue) == 1)
            {
                iBanderaRecetaInsumo = 0;
            }

            else if (Convert.ToInt32(cmbPadre.SelectedValue) == 2)
            {
                iBanderaRecetaInsumo = 1;
            }
        }

        private void BtnLimpiarDbAyuda_Click(object sender, EventArgs e)
        {
            dbAyudaReceta.limpiar();
        }

        private void rdbReceta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReceta.Checked == true)
            {
                llenarSentenciaProductoTerminado();
            }
        }

        private void rdbReferenciaInsumos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReferenciaInsumos.Checked == true)
            {
                llenarSentenciaMateriaPrima();
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }

        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 4);
        }
    }
}
