using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FInformacionPrecProduc : Form
    {
        string listBa;
        string lisMo;
        string estados;
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;

        DataTable dtConsulta;
        string sSql;
        //string sFecha;

        int iIdCodigoProducto;
        int iIdProducto;
        string iCodigo;
        string T_st_sql = "";
        string T_st_sql2 = "";
        string T_st_sql8 = "";
        string T_st_sql7 = "";
        string T_st_sql9 = "";
        string T_st_sql10 = "";
        string iFechaInicio;
        string iFechaFinal;
        string sEstado = "A";
        string sCambio;
        double iPreCompra;
        double iPreMinorista;
        double dbValor;
        double dbId_Lista_Precio;
        double dbValorNuevo;
        int iConta2;
        int iEsSubCate;
        int iIdPadre;
        string iCodigoCategoria;
        int iIdCategoria;
        int iEsModifcador;
        int iIdProduc;
        int iIdPrecioProducto;
        int iultimoNivel;
        int iIdProductoActualizar;
        int iValor_Porcentaje = 0;
        int numero_replica_tri = 1;
        int numero_control_repli = 0;
        int iActivoX;
        int iActivoY;

        public FInformacionPrecProduc()
        {
            InitializeComponent();
        }

        private void FInformacionPrecProduc_Load(object sender, EventArgs e)
        {
            txtId.Text = "4";
            txtDescripcion.Text = "Lista Minorista";
        }

        private void btnListPrecio_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaLisPrec ayuda1 = new Formularios.FAyudaLisPrec();
            ayuda1.ShowDialog();

            txtId.Text = ayuda1.Id;
            txtDescripcion.Text = ayuda1.Descripcion;
            txtMoneda.Text = ayuda1.Moneda;
            txtFecIniVa.Text = ayuda1.fechaIni;
            txtFecFinVa.Text = ayuda1.fechaFin;
            listBa = ayuda1.lisBase;

            if (txtId.Text != "" && txtDescripcion.Text != "")
            {
                grpDatosProducto.Enabled = true;
                if (listBa == "True")
                    chkListBase.Checked = true;
                else chkListBase.Checked = false;

                lisMo = ayuda1.lisModi;
                if (lisMo == "True")
                    chkListModi.Checked = true;
                else chkListModi.Checked = false;

                estados = ayuda1.Estad;
                if (estados == "A")
                {
                    cmbEstadoListPrecios.Text = "ACTIVO";
                }
                else
                {
                    cmbEstadoListPrecios.Text = "ELIMINADO";
                }
            }
            else
            {
                MessageBox.Show("No se ha elegido una lista de precio");
            }
        }

        public void limpiar()
        {
            //txtId.Text = "";
            //txtDescripcion.Text = "";
            txtMoneda.Text = "";
            txtFecFinVa.Text = "";
            txtFecIniVa.Text = "";
            chkListModi.Checked = false;
            chkListBase.Checked = false;
            txtIdCategoria.Text = "";
            txtNomCategoria.Text = "";
            txtIdProduc.Text = "";
            txtDescripcionProduc.Text = "";
            grpDatosProducto.Enabled = false;
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            if (iIdCategoria == 0)
            {
                Formularios.FAyudaPreciosProductosProduc ayuda1 = new Formularios.FAyudaPreciosProductosProduc(0, "");
                ayuda1.ShowDialog();

                iIdProduc = ayuda1.IdProducto;
                txtIdProduc.Text = ayuda1.CodigoProducto;
                txtDescripcionProduc.Text = ayuda1.NombreProducto;
            }

            else
            {
                Formularios.FAyudaPreciosProductosProduc ayuda1 = new Formularios.FAyudaPreciosProductosProduc(iIdCategoria, iCodigoCategoria);
                ayuda1.ShowDialog();

                iIdProduc = ayuda1.IdProducto;
                txtIdProduc.Text = ayuda1.CodigoProducto;
                txtDescripcionProduc.Text = ayuda1.NombreProducto;
            }
        }

        private void btnListCategoria_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaPrecioProducCatego ayuda1 = new Formularios.FAyudaPrecioProducCatego();
            ayuda1.ShowDialog();
            iIdCategoria = Convert.ToInt32(ayuda1.IdCategoria);
            iCodigoCategoria = (ayuda1.CodiCategoria);
            //iId_Padre = Convert.ToInt32(ayuda1.Padr);
            txtIdCategoria.Text = ayuda1.CodiCategoria;
            txtNomCategoria.Text = ayuda1.NombrCategoria;
        }

        //FUNCION PARA LLENAR EL GRID solo cuando se haya seleccionado alguna categoria 
        private void llenarGridCategorias()
        {
            int iConta2 = 0;
            dtConsulta = new DataTable();
            dtConsulta.Clear();

            sSql = "select 0 as id_precio_producto,PRO.id_producto,PRO.codigo,NOM.nombre,0.00  as precioCompra, "+
                   "0.00 Valor_porcentaje,0.00 as Precio_Minorista,'' as Fecha_Desde,'' as Fecha_Hasta from cv401_productos PRO "+
                   "inner join cv401_nombre_productos NOM on PRO.id_producto = NOM.id_producto and NOM.estado = 'A' and PRO.estado = 'A' where PRO.id_producto_padre = " + iIdCategoria + " "+
                   "and PRO.ultimo_nivel = 1";

            x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            //x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
            if (x == false)
            {
                MessageBox.Show("Error en la consulta");
            }
            else
            {
                dgvListPreciosProductos.AutoGenerateColumns = false;
                dgvListPreciosProductos.DataSource = dtConsulta;
                //dgvListPreciosProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                dgvListPreciosProductos.Refresh();
                //para poner el formato de la celda
                dgvListPreciosProductos.Columns[4].DefaultCellStyle.Format = "#,##0.00";
                dgvListPreciosProductos.Columns[5].DefaultCellStyle.Format = "#,##0.00";
                dgvListPreciosProductos.Columns[6].DefaultCellStyle.Format = "#,##0.00";

                //para alinear los datos 
                dgvListPreciosProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvListPreciosProductos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvListPreciosProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvListPreciosProductos.Columns[6].ReadOnly = false;
                dgvListPreciosProductos.Columns[7].ReadOnly = false;
                dgvListPreciosProductos.Columns[8].ReadOnly = false;
                dgvListPreciosProductos.Columns[0].Visible = false;
                dgvListPreciosProductos.Columns[1].Visible = false;
            }

            int iContador3=0;
            //dt3 = new DataTable();
            
            //dt3.Clear();
            foreach (DataGridViewRow row4 in dgvListPreciosProductos.Rows)
            {
                iIdProducto = Convert.ToInt32(row4.Cells[1].Value.ToString());

                sSql = "select PR.valor, PR.id_precio_producto,PR.fecha_inicio,PR.fecha_final from cv403_precios_productos PR "+
                       "inner join cv401_productos P on PR.id_producto = " +
                       "P.id_producto where id_lista_precio = 4 and P.id_producto = " + iIdProducto.ToString() + " and PR.estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (x == false)
                    MessageBox.Show("Error en la consulta");
                else
                    //contar cuantos registros me devuelve el datatable
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbValor = Convert.ToDouble(dtConsulta.Rows[iContador3].ItemArray[0].ToString());
                        iIdPrecioProducto = Convert.ToInt32(dtConsulta.Rows[iContador3].ItemArray[1].ToString());
                        iFechaInicio = Convert.ToDateTime(dtConsulta.Rows[iContador3].ItemArray[2]).ToString("dd/MM/yyyy");
                        iFechaFinal = Convert.ToDateTime(dtConsulta.Rows[iContador3].ItemArray[3]).ToString("dd/MM/yyyy");
                        dgvListPreciosProductos.Rows[iConta2].Cells[4].Value = dbValor;
                        dgvListPreciosProductos.Rows[iConta2].Cells[6].Value = dbValor;
                        dgvListPreciosProductos.Rows[iConta2].Cells[0].Value = iIdPrecioProducto;
                        dgvListPreciosProductos.Rows[iConta2].Cells[7].Value = iFechaInicio;
                        dgvListPreciosProductos.Rows[iConta2].Cells[8].Value = iFechaFinal;
                    }

                iConta2++;
                //iContador3++;
            }

        }


        //FUNCION PARA LLENAR EL grid cuando se haya selecionado una categoria y un producto o no se haya selecionado un producto
        private void llenarGridProductos()
        {
            //aqui verificara si el item selecionado es una subcategoria o no 
            dtConsulta = new DataTable();
            sSql = "select ultimo_nivel from cv401_productos where id_producto=" + iIdProduc + "";

            x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
                //contar cuantos registros me devuelve el datatable
                if (dtConsulta.Rows.Count > 0)
                {
                    iultimoNivel = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0]);
                }


            if (iultimoNivel == 1)
            {
                int iConta2 = 0;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "select 0 as id_precio_producto, P.id_Producto, P.codigo,NP.nombre as Nombre,0.00  as precioCompra,"+
                       "0.00 Valor_porcentaje,0.00 as Precio_Minorista, '' as Fecha_Desde,'' as Fecha_Hasta from cv401_productos P "+
                       "inner join cv401_nombre_productos NP  on P.id_producto = NP.id_producto and NP.estado='A' and P.estado ='A' where " +
                       "P.id_producto = " + iIdProduc + " and P.subcategoria=0 and P.ultimo_nivel=1 order by P.id_producto";

                x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                //x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvListPreciosProductos.AutoGenerateColumns = false;
                    dgvListPreciosProductos.DataSource = dtConsulta;
                    //dgvListPreciosProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvListPreciosProductos.Refresh();
                    dgvListPreciosProductos.Columns[4].DefaultCellStyle.Format = "#,##0.00";
                    dgvListPreciosProductos.Columns[5].DefaultCellStyle.Format = "#,##0.00";
                    dgvListPreciosProductos.Columns[6].DefaultCellStyle.Format = "#,##0.00";

                    //para alinear los datos 
                    dgvListPreciosProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvListPreciosProductos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvListPreciosProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgvListPreciosProductos.Columns[5].ReadOnly = false;
                    dgvListPreciosProductos.Columns[6].ReadOnly = false;
                    dgvListPreciosProductos.Columns[7].ReadOnly = false;
                    dgvListPreciosProductos.Columns[8].ReadOnly = false;
                    dgvListPreciosProductos.Columns[0].Visible = false;
                    dgvListPreciosProductos.Columns[1].Visible = false;
                }

                dtConsulta = new DataTable();

                foreach (DataGridViewRow row2 in dgvListPreciosProductos.Rows)
                {
                    iIdProducto = Convert.ToInt32(row2.Cells[1].Value.ToString());

                    sSql = "select PR.valor, PR.id_precio_producto,PR.fecha_inicio,PR.fecha_final from cv403_precios_productos PR "+
                           "inner join cv401_productos P on PR.id_producto = " +
                           "P.id_producto where id_lista_precio = 4 and P.id_producto = " + iIdProducto.ToString() + " and PR.estado='A'";

                    //dt.Clear();

                    //dt.Columns.Remove("valor");
                    x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (x == false)
                        MessageBox.Show("Error en la consulta");
                    else
                        //contar cuantos registros me devuelve el datatable
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dbValor = Convert.ToDouble(dtConsulta.Rows[iConta2].ItemArray[0].ToString());
                            iIdPrecioProducto = Convert.ToInt32(dtConsulta.Rows[iConta2].ItemArray[1].ToString());
                            iFechaInicio = Convert.ToDateTime(dtConsulta.Rows[iConta2].ItemArray[2]).ToString("dd/MM/yyyy");
                            iFechaFinal = Convert.ToDateTime(dtConsulta.Rows[iConta2].ItemArray[3]).ToString("dd/MM/yyyy");
                            dgvListPreciosProductos.Rows[iConta2].Cells[4].Value = dbValor;
                            dgvListPreciosProductos.Rows[iConta2].Cells[6].Value = dbValor;
                            dgvListPreciosProductos.Rows[iConta2].Cells[0].Value = iIdPrecioProducto;
                            dgvListPreciosProductos.Rows[iConta2].Cells[7].Value = iFechaInicio;
                            dgvListPreciosProductos.Rows[iConta2].Cells[8].Value = iFechaFinal;
                        }

                    iConta2++;
                }

            }
            else
            {
                int iConta2 = 0;
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "select 0 as id_precio_producto,PRO.id_producto,PRO.codigo,NOM.nombre,0.00  as precioCompra, "+
                       "0.00 Valor_porcentaje,0.00 as Precio_Minorista,'' as Fecha_Desde,'' as Fecha_Hasta from cv401_productos PRO "+
                       "inner join cv401_nombre_productos NOM on PRO.id_producto = NOM.id_producto and dbo.cv401_nombre_productos.estado = 'A' and PRO.estado = 'A' " +
                       "where PRO.id_producto_padre = " + iIdProduc + " and PRO.ultimo_nivel = 1";

                x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                //x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvListPreciosProductos.AutoGenerateColumns = false;
                    dgvListPreciosProductos.DataSource = dtConsulta;
                    //dgvListPreciosProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvListPreciosProductos.Refresh();
                    dgvListPreciosProductos.Columns[4].DefaultCellStyle.Format = "#,##0.00";
                    dgvListPreciosProductos.Columns[5].DefaultCellStyle.Format = "#,##0.00";
                    dgvListPreciosProductos.Columns[6].DefaultCellStyle.Format = "#,##0.00";
                    //para alinear los datos 
                    dgvListPreciosProductos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvListPreciosProductos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvListPreciosProductos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgvListPreciosProductos.Columns[5].ReadOnly = false;
                    dgvListPreciosProductos.Columns[6].ReadOnly = false;
                    dgvListPreciosProductos.Columns[7].ReadOnly = false;
                    dgvListPreciosProductos.Columns[8].ReadOnly = false;
                    dgvListPreciosProductos.Columns[0].Visible = false;
                    dgvListPreciosProductos.Columns[1].Visible = false;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                foreach (DataGridViewRow row2 in dgvListPreciosProductos.Rows)
                {
                    iIdProducto = Convert.ToInt32(row2.Cells[1].Value.ToString());

                    sSql = "select PR.valor, PR.id_precio_producto,PR.fecha_inicio,PR.fecha_final from cv403_precios_productos PR "+
                           "inner join cv401_productos P on PR.id_producto = " +
                           "P.id_producto where id_lista_precio = 4 and P.id_producto = " + iIdProducto.ToString() + " and PR.estado='A'";

                    x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (x == false)
                        MessageBox.Show("Error en la consulta");
                    else
                        //contar cuantos registros me devuelve el datatable
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dbValor = Convert.ToDouble(dtConsulta.Rows[iConta2].ItemArray[0].ToString());
                            iIdPrecioProducto = Convert.ToInt32(dtConsulta.Rows[iConta2].ItemArray[1].ToString());
                            iFechaInicio = Convert.ToDateTime(dtConsulta.Rows[iConta2].ItemArray[2]).ToString("dd/MM/yyyy");
                            iFechaFinal = Convert.ToDateTime(dtConsulta.Rows[iConta2].ItemArray[3]).ToString("dd/MM/yyyy");
                            dgvListPreciosProductos.Rows[iConta2].Cells[4].Value = dbValor;
                            dgvListPreciosProductos.Rows[iConta2].Cells[6].Value = dbValor;
                            dgvListPreciosProductos.Rows[iConta2].Cells[0].Value = iIdPrecioProducto;
                            dgvListPreciosProductos.Rows[iConta2].Cells[7].Value = iFechaInicio;
                            dgvListPreciosProductos.Rows[iConta2].Cells[8].Value = iFechaFinal;
                        }

                    iConta2++;
                }



            }
        }

        public void lista_mayorista()
        {
            dtConsulta = new DataTable();
            dtConsulta.Clear();

            sSql = "select  PRE.id_lista_precio, PRE.descripcion as Descripción, COD.valor_texto as Moneda, PRE.fecha_inicio_validez, "+
                   "PRE.fecha_fin_validez, PRE.lista_base, PRE.lista_modificable, PRE.estado from  cv403_listas_precios PRE "+
                   "inner join tp_codigos COD on PRE.cg_moneda = COD.correlativo where PRE.id_lista_precio = 4";

            x = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
                //contar cuantos registros me devuelve el datatable
                if (dtConsulta.Rows.Count > 0)
                {
                    txtMoneda.Text = dtConsulta.Rows[0].ItemArray[2].ToString();
                    txtFecIniVa.Text = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[3]).ToString("dd/MM/yyyy");
                    txtFecFinVa.Text = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[4]).ToString("dd/MM/yyyy");
                    string chkListaBa = dtConsulta.Rows[0].ItemArray[5].ToString();
                    if (chkListaBa == "True")
                    {
                        chkListBase.Checked = true;
                    }
                    string chkListaModi = dtConsulta.Rows[0].ItemArray[6].ToString();
                    if (chkListaModi == "True")
                    {
                        chkListModi.Checked = true;
                    }
                    string sEstado = dtConsulta.Rows[0].ItemArray[7].ToString();
                    if (sEstado == "A")
                    {
                        cmbEstadoListPrecios.Text = "Activo";
                    }
                    else
                    {
                        cmbEstadoListPrecios.Text = "Eliminado";
                    }
                }
        }

        private void btnNuevoPosOrd_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" && txtDescripcion.Text == "")
            {
                MessageBox.Show("Elegir una lista de precio");
                txtIdCategoria.Text = "";
                txtNomCategoria.Text = "";
                btnListPrecio.Focus();
            }
            else
            {
                if (txtIdProduc.Text == "" && txtDescripcionProduc.Text == "" && txtIdCategoria.Text == "" && txtNomCategoria.Text == "")
                {
                    MessageBox.Show("Favor elegir una categoria o un producto");
                    btnListCategoria.Focus();
                }
                else
                {
                    if (txtIdProduc.Text == "" && txtDescripcionProduc.Text == "")
                    {
                        if (txtId.Text == "4")
                        {
                            llenarGridCategorias();
                            lista_mayorista();
                        }
                        else
                        {
                            llenarGridCategorias();
                        }
                        
                    }
                    else
                    {
                        if (txtIdCategoria.Text == "" && txtNomCategoria.Text == "")
                        {
                            if (txtId.Text == "4")
                            {
                                llenarGridProductos();
                                lista_mayorista();
                            }
                            else
                            {
                                llenarGridProductos();
                            }

                        }
                        else
                        {
                            if (txtId.Text == "4")
                            {
                                llenarGridProductos();
                                lista_mayorista();
                            }
                            else
                            {
                                llenarGridProductos();
                            }
                        }
                    }
                }
                
            }

        }

        //para comprovar si se realizo algun cambio en una celda del GRID
        private void dgvListPreciosProductos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            sCambio = dgvListPreciosProductos.CurrentCell.Value.ToString();
        }

        private void dgvListPreciosProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cellVaue = dgvListPreciosProductos.CurrentCell.Value.ToString();
            if (cellVaue == sCambio)
            {
                MessageBox.Show("No se realizo ningun cambio en la celda");
            }
            else
            {
                //enc caso de que se haya modificado la celda del GRID realizara la siguientes acciones
                var activoX = dgvListPreciosProductos.CurrentCellAddress.X;
                var activoy = dgvListPreciosProductos.CurrentCellAddress.Y;
                dgvListPreciosProductos.Rows[activoy].Cells[9].Value = true;
            }
        }

        private void btnNuevoListPrecios_Click(object sender, EventArgs e)
        {
            if (btnNuevoListPrecios.Text == "Nuevo")
            {
                btnNuevoListPrecios.Text = "Agregar";
            }
            else
            {
                if (btnNuevoListPrecios.Text == "Guardar")
                {
                    //string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                    //                        + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                    //sFecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION 
                    //devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        limpiar();
                        goto reversa;
                    }
                    else
                    {

                        foreach (DataGridViewRow row2 in dgvListPreciosProductos.Rows)
                        {
                            bool iActivo = Convert.ToBoolean(row2.Cells[9].Value);
                            if (iActivo == true)
                            {
                                iIdPrecioProducto = Convert.ToInt32(row2.Cells[0].Value.ToString());
                                iIdProductoActualizar = Convert.ToInt32(row2.Cells[1].Value.ToString());
                                dbValorNuevo = Convert.ToDouble(row2.Cells[6].Value.ToString());

                                T_st_sql = "update cv403_precios_productos set estado = 'E', fecha_anula = GETDATE(), "+
                                    "usuario_anula = '" + Program.sDatosMaximo[0] + "', terminal_anula = '" + Program.sDatosMaximo[1] + "' " +
                                    "where id_precio_producto = '" + iIdPrecioProducto + "' and id_Lista_Precio = 4 and id_producto = " + iIdProductoActualizar + "";
                                if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                                {
                                    //HARÁ EL ROLLBACK
                                    goto reversa;
                                }

                                else
                                {
                                    //AQUI VAMOS A INSERTAR UN NUEVO REGISTRO

                                    //1 es el dbId_Lista_Precio de la lista base
                                    dbId_Lista_Precio = 4;
                                    T_st_sql = "insert into cv403_precios_productos (id_Lista_Precio,id_Producto,valor_Porcentaje,valor,fecha_inicio,fecha_final, " +
                                        "estado,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + dbId_Lista_Precio + "', " +
                                        "'" + iIdProductoActualizar + "','" + iValor_Porcentaje + "','" + dbValorNuevo + "', GETDATE(), " +
                                        "GETDATE(), 'A', '" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), " +
                                        "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                                    if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                                    {
                                        //hara el rolBAck
                                        //conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                                        goto reversa;
                                    }


                                }
                            }
                        }
                        //si se ejecuta bien hara un commit
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        MessageBox.Show("Registros actualizados correctamente");
                        limpiar();
                        //btnOK_Click(sender, e);
                        //goto fin;
                    }
                }
                goto fin;

                #region FUNCIONES ADICIONALES REVERSA Y FIN
            reversa:
                try
                {
                    //aqui va la instruccion para hacer el rollback
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    MessageBox.Show("Ocurriò un problema en la transacciòn");
                    //Limpiar();
                    //btnOK_Click(sender, e);
                }
                catch (Exception)
                {

                }


            fin:
                try
                {

                }
                catch (Exception)
                {

                }
                #endregion
            }

        }

        private void btnCerrarListPrecios_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarListPrecios_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAnularListPrecios_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea anular el registro selecionado?", "Aviso",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvListPreciosProductos.Rows[iActivoY].Cells[4].Value = 0.00;
                dgvListPreciosProductos.Rows[iActivoY].Cells[5].Value = 0.00;
                dgvListPreciosProductos.Rows[iActivoY].Cells[6].Value = 0.00;
                dgvListPreciosProductos.Rows.Clear();

            }
            
        }

        private void dgvListPreciosProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iActivoX = dgvListPreciosProductos.CurrentCellAddress.X;
            iActivoY = dgvListPreciosProductos.CurrentCellAddress.Y;
        }

    }
}
