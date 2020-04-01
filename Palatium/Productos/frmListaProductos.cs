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
    public partial class frmListaProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.claseExportarFormatos exportar = new Clases.claseExportarFormatos();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;

        DataTable dtConsulta;
        DataTable dtAyuda;
        DataTable dtAyuda2;

        bool bRespuesta;

        int iBanderSubCategoria;

        int iIdProducto;
        int iListaPrecio;
        int iPagaIva;

        string sCodigo;
        string sNombre;

        Double dValor;
        Double dIva;
        Double dServicio;
        Double dTotal;

        public frmListaProductos()
        {
            InitializeComponent();
        }

        //DataRow[] dFila = cmbBodega.dt.Select("id_bodega = " + cmbBodega.SelectedValue);
        //cmbOficina.SelectedIndexChanged -= new EventHandler(cmbOficina_SelectedIndexChanged);    

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR()
        private void limpiar()
        {
            cmbCategorias.SelectedIndexChanged -= new EventHandler(cmbCategorias_SelectedIndexChanged);
            cmbCategorias.SelectedIndex = 0;
            cmbCategorias.SelectedIndexChanged += new EventHandler(cmbCategorias_SelectedIndexChanged);
            dgvDatos.Rows.Clear();
            cmbSubCategoria.Visible = false;
        }

        //FUNCION PARA CARGAR EL COMBO DE CATEGORIAS
        private void llenarComboCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select p.id_producto, NP.nombre, P.codigo, P.subcategoria" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbCategorias.llenar(dtConsulta, sSql);

                if (cmbCategorias.Items.Count > 0)
                {
                    cmbCategorias.SelectedIndex = 0;
                }

            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBO DE DEPENDIENTES DE SUBCATEGORIAS
        private void llenarComboSubCategorias()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre, P.codigo" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "where P.id_producto_padre = " + Convert.ToInt32(cmbCategorias.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbSubCategoria.llenar(dtConsulta, sSql);

                if (cmbSubCategoria.Items.Count > 0)
                {
                    cmbSubCategoria.SelectedIndex = 0;
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRIDVIEW
        private void llenarGrid(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre, P.codigo," + Environment.NewLine;
                sSql += "PP.id_lista_precio, PP.valor, P.paga_iva" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON PP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;

                if (iBanderSubCategoria == 0)
                {
                    if (iIdProducto_P == 0)
                    {
                        sSql += "where P.id_producto_padre = " + Convert.ToInt32(cmbCategorias.SelectedValue) + Environment.NewLine;
                    }

                    else
                    {
                        sSql += "where P.id_producto_padre = " + iIdProducto_P + Environment.NewLine;
                    }
                }

                else
                {
                    if (iIdProducto_P == 0)
                    {
                        sSql += "where P.id_producto_padre = " + Convert.ToInt32(cmbSubCategoria.SelectedValue) + Environment.NewLine;
                    }

                    else
                    {
                        sSql += "where P.id_producto_padre = " + iIdProducto_P + Environment.NewLine;
                    }
                }

                sSql += "and id_lista_precio = 4" + Environment.NewLine;
                sSql += "order by P.id_producto, id_lista_precio" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (iIdProducto_P == 0)
                        {
                            DataRow[] dFila = cmbCategorias.dt.Select("id_producto = " + cmbCategorias.SelectedValue);
                            sCodigo = dFila[0][2].ToString();
                            sNombre = dFila[0][1].ToString();
                            dgvDatos.Rows.Add(sCodigo, sNombre, "");
                        }                        

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //iIdProducto = Convert.ToInt32(dtConsulta.Rows[i]["id_producto"]);
                            sNombre = dtConsulta.Rows[i]["nombre"].ToString();
                            sCodigo = dtConsulta.Rows[i]["codigo"].ToString();
                            //iListaPrecio = Convert.ToInt32(dtConsulta.Rows[i]["id_lista_precio"]);
                            dValor = Convert.ToDouble(dtConsulta.Rows[i]["valor"]);
                            iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"]);
                            dIva = 0;
                            dServicio = 0;
                            
                            if (iPagaIva == 1)
                            {
                                dIva = dValor * Program.iva;
                            }

                            if (Program.iManejaServicio == 1)
                            {
                                dServicio = dValor * Program.servicio;
                            }

                            dTotal = dValor + dIva + dServicio;

                            dgvDatos.Rows.Add(sCodigo, sNombre, dTotal.ToString("N2"));
                        }
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

        #endregion

        #region FUNCIONES PARA EXTRAER TODOS LOS REGISTROS

        //EXTRAER TODOS LOS REGISTROS
        private void extraerTodosRegistros()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select p.id_producto, NP.nombre, P.codigo, P.subcategoria" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No existen categorías registradas en el sistema.";
                    ok.ShowDialog();
                    goto fin;
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iBanderSubCategoria = Convert.ToInt32(dtAyuda.Rows[i]["subcategoria"]);

                    if (iBanderSubCategoria == 0)
                    {
                        //INVOCA A FUNCION DE CATEGORIAS                        
                        iIdProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_producto"]);
                        sCodigo = dtAyuda.Rows[i]["codigo"].ToString();
                        sNombre = dtAyuda.Rows[i]["nombre"].ToString();

                        dgvDatos.Rows.Add(sCodigo, sNombre, "");

                        llenarGrid(iIdProducto);
                        dgvDatos.Rows.Add("", "", "");
                    }

                    else
                    {
                        //INVOCA A FUNCIONES DE SUBCATEGORIAS
                        //INVOCA A FUNCION DE CATEGORIAS                        
                        iIdProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_producto"]);
                        sCodigo = dtAyuda.Rows[i]["codigo"].ToString();
                        sNombre = dtAyuda.Rows[i]["nombre"].ToString();

                        dgvDatos.Rows.Add(sCodigo, sNombre, "");
                        extraerSubCategorias(iIdProducto);
                    }
                }

                dgvDatos.Rows.RemoveAt(dgvDatos.Rows.Count - 1);
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            fin: { }
        }

        //EXTRAER TODAS LAS SUBCATEGORIAS 
        private void extraerSubCategorias(int iIdProducto_P)
        {
            try
            {
                sSql = "select P.id_producto, NP.nombre, P.codigo" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "where P.id_producto_padre = " + iIdProducto_P;

                dtAyuda2 = new DataTable();
                dtAyuda2.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda2, sSql);

                if (bRespuesta == true)
                {
                    if (dtAyuda2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAyuda2.Rows.Count; i++)
                        {
                            //INVOCA A FUNCION DE CATEGORIAS                        
                            iIdProducto = Convert.ToInt32(dtAyuda2.Rows[i]["id_producto"]);
                            sCodigo = dtAyuda2.Rows[i]["codigo"].ToString();
                            sNombre = dtAyuda2.Rows[i]["nombre"].ToString();

                            dgvDatos.Rows.Add(sCodigo, sNombre, "");

                            llenarGrid(iIdProducto);
                            dgvDatos.Rows.Add("", "", "");
                        }
                    }
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

            fin: { }
        }

        #endregion

        private void frmListaProductos_Load(object sender, EventArgs e)
        {            
            cmbCategorias.SelectedIndexChanged -= new EventHandler(cmbCategorias_SelectedIndexChanged); 
            llenarComboCategorias();
            cmbCategorias.SelectedIndexChanged += new EventHandler(cmbCategorias_SelectedIndexChanged);
        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dFila = cmbCategorias.dt.Select("id_producto = " + cmbCategorias.SelectedValue);
                iBanderSubCategoria = Convert.ToInt32(dFila[0][3].ToString());

                if (iBanderSubCategoria == 0)
                {
                    cmbSubCategoria.Visible = false;
                }

                else
                {
                    cmbSubCategoria.Visible = true;
                    llenarComboSubCategorias();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (rbTodos.Checked == true)
            {
                extraerTodosRegistros();
            }

            else
            {
                if (Convert.ToInt32(cmbCategorias.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione la categoría.";
                    ok.ShowDialog();
                }

                else
                {
                    if (iBanderSubCategoria == 0)
                    {
                        dgvDatos.Rows.Clear();
                        llenarGrid(0);
                    }

                    else
                    {
                        if (Convert.ToInt32(cmbSubCategoria.SelectedValue) == 0)
                        {
                            ok.LblMensaje.Text = "Favor seleccione la subcategoría categoría.";
                            ok.ShowDialog();
                        }

                        else
                        {
                            dgvDatos.Rows.Clear();
                            llenarGrid(0);
                        }
                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No hay registros para exportar a Excel.";
                ok.ShowDialog();
            }

            else
            {
                this.Cursor = Cursors.WaitCursor;

                if (exportar.exportarExcel(dgvDatos) == false)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al exportar a Excel.";
                    ok.ShowDialog();
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            limpiar();
            if (rbTodos.Checked == true)
            {
                grupoOpciones.Enabled = false;
            }
        }

        private void rbBusqueda_CheckedChanged(object sender, EventArgs e)
        {
            limpiar();
            if (rbBusqueda.Checked == true)
            {
                grupoOpciones.Enabled = true;
            }
        }
    }
}
