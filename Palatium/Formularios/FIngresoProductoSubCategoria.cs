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
    public partial class FIngresoProductoSubCategoria : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt;
        DataTable dtConsulta;
        string sSql;
        string sValReco;
        string sValReco2;
        string sNombreCategoria;
        string sPrecioCompra;
        string sPrecioMinorista;
        int iPrecioCompra = 1;
        int iId_Producto;
        int iModiChe = 0;
        int iPreModChe = 0;
        int iCg_tipoNombre = 5076;
        int iNombInterno = 1;
        int iNumRepliTrig = 1;
        int iNumControRepli = 0;
        int iValor_Porcentaje = 0;
        int iNivel = 4;
        int iId_Padre;
        int iId_Empresa = Program.iIdEmpresa;
        int iPagIva;
        int iPreModific;
        int iExpira;
        int iIdProducto;
        int modific;
        int iUltimoNivel=1;
        double dbId_Lista_Precio = 1;
        double dbValor;
        DataTable dtCompra;
        DataTable dtConsumo;
        DataTable dtColor;
        bool x = false;




        string sTabla;
        string sCampo;
        long iMaximo;

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public FIngresoProductoSubCategoria()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = Dns.GetHostName();
        }

        private void FIngresoProductoSubCategoria_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
            //cmbEstad.Text = "ACTIVO";
            LLenarComboCompra();
            LLenarComboCosumo();
            LLenarComboColor();
        }

        public void Limpiar()
        {
            //dgvProdCategoria.Rows.Clear();
            txtSecuencia.Text = "";
            txtCodigCategoria.Text = "";
            txtNombreCategoria.Text = "";
            txtPrecioMinorista.Text = "";
            cmbColoSubProductos.SelectedIndex = 0;
            txtStockMin.Text = "";
            txtStockMax.Text = "";
            chkPreModifProductos.Checked = false;
            chkExpiraProductos.Checked = false;
            LLenarComboCompra();
            LLenarComboCosumo();
            LLenarComboColor();
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                conexion.ds.Tables.Clear();

                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select P.id_Producto, P.codigo as Código,NP.nombre as Nombre,0.00  as precioCompra,0.00 as Precio_Minorista," +
                    " P.paga_iva as Paga_Iva,P.secuencia as Secuencia, P.id_color as Color, P.stock_min as Stock_Min, P.stock_max as Stock_Max," +
                    " P.precio_modificable as Prec_Modificable, P.expira as Expira from cv401_productos P,cv401_nombre_productos NP " +
                    " where (P.id_Producto = NP.id_Producto and P.estado ='A' and NP.estado='A') and P.id_Producto_padre in " +
                    "(select id_Producto from cv401_productos where codigo ='" + txtIdSubCategoria.Text.Trim() + "' and estado = 'A') " +
                    "and P.nivel = 4";
                }

                else
                {
                    t_st_query = "select P.id_Producto, P.codigo as Código,NP.nombre as Nombre,0 as precioCompra," +
                        "0 as Precio_Minorista,P.paga_iva as Paga_Iva,P.secuencia as Secuencia, P.id_color as Color, P.stock_min as Stock_Min," +
                        " P.stock_max as Stock_Max, P.precio_modificable as Prec_Modificable, P.expira as Expira from cv401_productos P," +
                        "cv401_nombre_productos NP where (P.id_Producto = NP.id_Producto and P.estado ='A' and NP.estado='A') " +
                        "and P.id_Producto_padre in (select id_Producto from cv401_productos where codigo ='" + txtIdSubCategoria.Text.Trim() + "') " +
                        "and P.nivel = 4 " +
                        "and NP.nombre LIKE '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProdCategoria.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProdCategoria.Refresh();
                    dgvProdCategoria.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }


        //llenar el comboBox unidad compra 
        private void LLenarComboCompra()
        {
            try
            {
                dtCompra = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00042' and estado='A'";
                cmbCompraSubCategoria.llenar(dtCompra, sql);
                cmbCompraSubCategoria.SelectedIndex = 24;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un problema al realizar la consulta");
            }
        }

        //llenar el comboBox unidad consumo
        private void LLenarComboCosumo()
        {
            try
            {
                dtConsumo = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00042' and estado='A'";
                cmbConsumoSubCategoria.llenar(dtConsumo, sql);
                cmbConsumoSubCategoria.SelectedIndex = 24;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un problema al realizar la consulta");
            }
        }

        //llenar el comboBox de color 
        private void LLenarComboColor()
        {
            try
            {
                dtColor = new DataTable();
                string sql = "select id_color,descripcion from art_color";
                cmbColoSubProductos.llenar(dtColor, sql);
                //cmbColoProductos.SelectedIndex = 24;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un problema al realizar la consulta");
            }
        }

        private void btnListSubCategoria_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaProdSubcategoria ayuda1 = new Formularios.FAyudaProdSubcategoria();
            ayuda1.ShowDialog();
            iId_Padre = Convert.ToInt32(ayuda1.Padr);
            txtIdSubCategoria.Text = ayuda1.codig;
            txtNomSubCategoria.Text = ayuda1.Nombr;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnSubAgregar.Text = "Nuevo";
            if (txtIdSubCategoria.Text == "" && txtNomSubCategoria.Text == "")
            {
                MessageBox.Show("Favor, seleccionar una categoria");
                btnListSubCategoria.Focus();
            }
            else
            {
                if (dgvProdCategoria.Columns["columna1"] == null)
                {
                }
                else
                {
                    dgvProdCategoria.Columns.Remove("columna1");
                }

                int conta = 0;
                int iConta2 = 0;
                double precioVenta = 0, iva, porcien10;
                //dgvProdCategoria.Rows.Clear();
                Grb_DatoCategoria.Enabled = false;
                //conteo = 1;
                Grb_listReProCategori.Enabled = true;
                Grb_opcioCajero.Enabled = true;
                txtPrecioCompra.Text = "1";

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

                DataGridViewTextBoxColumn columna1 = new DataGridViewTextBoxColumn(); //creo la primera columna de tipo textBox
                columna1.HeaderText = "Precio Final"; //le pongo como titulo Orden
                columna1.Width = 80; //le pongo el tamaño de la celda
                columna1.Name = "columna1";
                columna1.ReadOnly = true; //me permite hacer que la celda no sea editable 
                dgvProdCategoria.Columns.Add(columna1); //agrego en el datagridview la columna1

                //para sacar el preciofinal 
                foreach (DataGridViewRow row in dgvProdCategoria.Rows)
                {
                    precioVenta = Convert.ToDouble(row.Cells[4].Value.ToString());
                    iva = precioVenta * Program.iva;
                    porcien10 = precioVenta * Program.servicio;
                    precioVenta = precioVenta + iva + porcien10;
                    dgvProdCategoria.Rows[conta].Cells[12].Value = precioVenta;
                    conta++;
                }

                dt = new DataTable();
                foreach (DataGridViewRow row2 in dgvProdCategoria.Rows)
                {
                    iIdProducto = Convert.ToInt32(row2.Cells[0].Value.ToString());

                    sSql = "select PR.valor from cv403_precios_productos PR inner join cv401_productos P on PR.id_producto = " +
                        "P.id_producto where id_lista_precio = 1 and P.id_producto = " + iIdProducto.ToString() + " and PR.estado='A'";

                    //dt.Clear();

                    //dt.Columns.Remove("valor");
                    x = conexion.GFun_Lo_Busca_Registro(dt, sSql);
                    if (x == false)
                        MessageBox.Show("Error en la consulta");
                    else
                        //contar cuantos registros me devuelve el datatable
                        if (dt.Rows.Count > 0)
                        {
                            dbValor = Convert.ToDouble(dt.Rows[iConta2].ItemArray[0].ToString());
                            dgvProdCategoria.Rows[iConta2].Cells[3].Value = dbValor;
                        }

                    iConta2++;
                }

                iConta2 = 0;
                dt = new DataTable();
                foreach (DataGridViewRow row3 in dgvProdCategoria.Rows)
                {
                    iIdProducto = Convert.ToInt32(row3.Cells[0].Value.ToString());

                    sSql = "select PR.valor from cv403_precios_productos PR inner join cv401_productos " +
                        " P on PR.id_producto = P.id_producto where id_lista_precio = 4 and P.id_producto = " + iIdProducto.ToString() + " and pr.estado='A'";


                    //dt.Clear();

                    //dt.Columns.Remove("valor");
                    x = conexion.GFun_Lo_Busca_Registro(dt, sSql);
                    if (x == false)
                        MessageBox.Show("Error en la consulta");
                    else
                        //contar cuantos registros me devuelve el datatable
                        if (dt.Rows.Count > 0)
                        {
                            dbValor = Convert.ToDouble(dt.Rows[iConta2].ItemArray[0].ToString());
                            dgvProdCategoria.Rows[iConta2].Cells[4].Value = dbValor;
                        }

                    iConta2++;
                }
            }
        }


        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro(string F_st_query, string F_st_mensaje)
        {
            try
            {

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "cv401_productos");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);


            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);
            }
        }
        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro2(string F_st_query, string F_st_mensaje)
        {
            try
            {

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "cv401_nombre_productos");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);
            }
        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro3(string F_st_query, string F_st_mensaje)
        {
            try
            {

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "cv403_precios_productos");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);
            }
        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro4(string F_st_query, string F_st_mensaje)
        {
            try
            {

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "cv403_precios_productos");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategoria.Enabled = false;
                btnNuevSubCategoria.Text = "Nuevo";
                Limpiar();

                //string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);
            }
        }

        private void btnSubAgregar_Click(object sender, EventArgs e)
        {
            if (btnSubAgregar.Text == "Nuevo")
            {
                grpDatoIngreProduc.Enabled = true;
                Grb_DatoCategoria.Enabled = false;
                Grb_listReProCategori.Enabled = true;
                txtCodigCategoria.Focus();
                btnSubAgregar.Text = "Agregar";
            }
            else
            {
                if (btnSubAgregar.Text == "Agregar")
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

                    dbId_Lista_Precio = 1;
                    dgvProdCategoria.Columns.Remove("columna1");

                    sValReco = txtCodigCategoria.Text.ToString();
                    btnSubAgregar.Text = "Nuevo";
                    grpDatoIngreProduc.Enabled = false;


                    string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                                            + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION 
                    //devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        Limpiar();
                        goto reversa;
                    }
                    else
                    {
                        sSql = "insert into cv401_productos (idempresa,codigo,id_Producto_padre,estado,Nivel,modificable,precio_modificable,paga_iva,secuencia," +
                            "id_color,ultimo_nivel,stock_min,stock_max,Expira,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + iId_Empresa + "','" + sValReco + "', " +
                            "'" + iId_Padre + "','A','" + iNivel + "','" + iModiChe + "','" + iPreModific + "','" + iPagIva + "', " +
                            "'" + txtSecuencia.Text.ToString().Trim() + "','" + cmbColoSubProductos.SelectedValue.ToString() + "','" + iUltimoNivel + "','" + txtStockMin.Text.ToString().Trim() + "'," +
                            "'" + txtStockMax.Text.ToString().Trim() + "','" + iExpira + "', 'A', '" + Program.sDatosMaximo[0] + "', " +
                            " '" + Program.sDatosMaximo[1] + "')";

                        //sisque no me ejuta el query 
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            //hara el rolBAck
                            MessageBox.Show("Ha ingresado un código que ya ha sido registrado anteriormente. Por Favor introduzca uno nuevo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto reversa;

                        }
                        else
                        {

                            dtConsulta = new DataTable();
                            dtConsulta.Clear();


                            //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV401_PRODUCTOS
                            dtConsulta = new DataTable();
                            dtConsulta.Clear();

                            sTabla = "cv401_productos";
                            sCampo = "id_producto";

                            iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                            if (iMaximo == -1)
                            {
                                ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                                ok.ShowInTaskbar = false;
                                ok.ShowDialog();
                                goto reversa;
                            }

                            else
                            {
                                iId_Producto = Convert.ToInt32(iMaximo);
                            }
                        }

                        sSql = "insert into cv401_nombre_productos (id_Producto,cg_tipo_nombre,nombre,nombre_interno,estado,numero_replica_trigger," +
                            "numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + iId_Producto + "','" + iCg_tipoNombre + "'," +
                            "'" + txtNombreCategoria.Text.ToString().Trim() + "','" + iNombInterno + "', 'A', '" + iNumRepliTrig + "','" + iNumControRepli + "', " +
                            "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            //hara el rolBAck
                            goto reversa;
                        }
                        else
                        {

                        }

                        int numero_replica_tri = 1, numero_control_repli = 0;
                        //1 es el dbId_Lista_Precio de la lista base
                        dbId_Lista_Precio = 1;
                        sSql = "insert into cv403_precios_productos (id_Lista_Precio,id_Producto,valor_Porcentaje,valor,fecha_inicio,fecha_final," +
                            "estado,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + dbId_Lista_Precio + "'," +
                            "'" + iId_Producto + "','" + iValor_Porcentaje + "','" + txtPrecioCompra.Text.ToString().Trim() + "', GETDATE(), " +
                            "GETDATE(), 'A', '" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), " +
                            " '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            //hara el rolBAck
                            //conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                            goto reversa;
                        }
                        else
                        {

                        }
                        //4 es el dbId_Lista_Precio de la lista minorista
                        dbId_Lista_Precio = 4;
                        Double dMinorista = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + Program.iva + Program.servicio);
                        sSql = "insert into cv403_precios_productos (id_Lista_Precio,id_Producto,valor_Porcentaje,valor,fecha_inicio,fecha_final,estado," +
                            "numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + dbId_Lista_Precio + "'," +
                            "'" + iId_Producto + "','" + iValor_Porcentaje + "','" + dMinorista + "', GETDATE()," +
                            " GETDATE(), 'A', '" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), " +
                            " '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            goto reversa;
                        }
                        else
                        {
                        }

                        int cg_tipo_unidad = 6142, unidad_compra = 1, numero_replica_tri2 = 1, numero_control_repli2 = 0;
                        sSql = "insert into cv401_unidades_productos (id_Producto,cg_tipo_unidad,cg_unidad,unidad_compra,estado,usuario_creacion," +
                            "terminal_creacion,fecha_creacion,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso," +
                            "terminal_ingreso)values('" + iId_Producto + "','" + cg_tipo_unidad + "','" + cmbCompraSubCategoria.SelectedValue.ToString() + "'," +
                            "'" + unidad_compra + "', 'A', '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE()," +
                            "'" + numero_replica_tri2 + "','" + numero_control_repli2 + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', " +
                            "'" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            goto reversa;
                        }
                        else
                        {

                        }

                        cg_tipo_unidad++; unidad_compra--;
                        sSql = "insert into cv401_unidades_productos (id_Producto,cg_tipo_unidad,cg_unidad,unidad_compra,estado,usuario_creacion," +
                            "terminal_creacion,fecha_creacion,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso," +
                            "terminal_ingreso)values('" + iId_Producto + "','" + cg_tipo_unidad + "','" + cmbConsumoSubCategoria.SelectedValue.ToString() + "', " +
                            "'" + unidad_compra + "', 'A', '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE(), " +
                            "'" + numero_replica_tri2 + "','" + numero_control_repli2 + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', " +
                            " '" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            //hara el rolBAck
                            goto reversa;
                        }
                        else
                        {
                            //si se ejecuta bien hara un commit
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                            MessageBox.Show("Registro ingresado correctamente");
                            Limpiar();
                            btnOK_Click(sender, e);

                            //goto fin;
                        }

                    }
                }


                //SI EL BOTON ESTA EN OPCION ACTUALIZAR
                else if (btnSubAgregar.Text == "Actualizar")
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

                    sValReco2 = txtCodigCategoria.Text.ToString();

                    sSql = "update cv401_productos set codigo = '" + sValReco2 + "' , secuencia = '" + txtSecuencia.Text.ToString().Trim() + "' , " +
                        "paga_iva = '" + iPagIva + "', id_color = '" + cmbColoSubProductos.SelectedValue.ToString() + "', stock_min = '" + txtStockMin.Text.ToString().Trim() + "', " +
                        "stock_max = '" + txtStockMax.Text.ToString().Trim() + "', precio_modificable = '" + iPreModific + "', Expira = '" + iExpira + "' " +
                        " where id_Producto = '" + iId_Producto + "'";

                    //AQUI INICIA PROCESO DE ACTUALIZACION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        Limpiar();
                        goto reversa;
                    }
                    else
                    {
                        string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                                                + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                        int numero_replica_tri = 1, numero_control_repli = 0;

                        //AQUI EMEPZAMOS A ACTUALIZAR LA TABLA cv401_productos
                        //SOLO EN ESTA TABLA SE ACTUALIZA, NO SE VUELVE A INSERTAR

                        sSql = "update cv401_productos set codigo = '" + sValReco2 + "' , secuencia = '" + txtSecuencia.Text.ToString().Trim() + "' ," +
                            " paga_iva = '" + iPagIva + "', id_color = '" + cmbColoSubProductos.SelectedValue.ToString() + "', " +
                            "stock_min = '" + txtStockMin.Text.ToString().Trim() + "', stock_max = '" + txtStockMax.Text.ToString().Trim() + "', " +
                            "precio_modificable = '" + iPreModific + "', Expira = '" + iExpira + "' where id_Producto = '" + iId_Producto + "'";

                        //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            //HARÁ EL ROLLBACK
                            goto reversa;

                        }

                        //AQUI ACTUALIZAMOS LA TABLA cv401_nombre_productos A sEstado ELIMINADO
                        //=================================================================================================
                        if (txtNombreCategoria.Text != sNombreCategoria)
                        {
                            //T_st_sql = "update cv401_nombre_productos set nombre = '" + txtNombreCategoria.Text.ToString().Trim() + "' where iId_Producto = '" + iId_Producto + "'";
                            sSql = "update cv401_nombre_productos set estado = 'E', fecha_anula = GETDATE(), usuario_anula = '" + Program.sDatosMaximo[0] + "'," +
                                " terminal_anula = '" + Program.sDatosMaximo[1] + "' where id_Producto = '" + iId_Producto + "'";

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                //HARÁ EL ROLLBACK
                                goto reversa;
                            }

                            else
                            {
                                //AQUI VAMOS A INSERTAR EL NUEVO REGISTRO
                                //=================================================================================================

                                sSql = "insert into cv401_nombre_productos (id_Producto,cg_tipo_nombre,nombre,nombre_interno,estado,numero_replica_trigger," +
                                    "numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + iId_Producto + "','" + iCg_tipoNombre + "', " +
                                    "'" + txtNombreCategoria.Text.ToString().Trim() + "','" + iNombInterno + "', 'A', '" + iNumRepliTrig + "'," +
                                    " '" + iNumControRepli + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                {
                                    //HARÁ EL ROLLBACK
                                    goto reversa;
                                }
                            }
                        }

                        //AQUI ACTUALIZAMOS LA TABLA cv403_precios_productos A sEstado ELIMINADO
                        //=================================================================================================
                        //verificamos si se modifico el precio de compra  
                        if (txtPrecioCompra.Text != sPrecioCompra)
                        {
                            sSql = "update cv403_precios_productos set estado = 'E', fecha_anula = GETDATE(), usuario_anula = '" + Program.sDatosMaximo[0] + "', " +
                                " terminal_anula = '" + Program.sDatosMaximo[1] + "' where id_Producto = '" + iId_Producto + "' and id_Lista_Precio = 1";
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                //HARÁ EL ROLLBACK
                                goto reversa;
                            }

                            else
                            {
                                //AQUI VAMOS A INSERTAR UN NUEVO REGISTROS

                                //1 es el dbId_Lista_Precio de la lista base
                                dbId_Lista_Precio = 1;
                                sSql = "insert into cv403_precios_productos (id_Lista_Precio,id_Producto,valor_Porcentaje,valor,fecha_inicio,fecha_final, " +
                                    "estado,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + dbId_Lista_Precio + "', " +
                                    "'" + iId_Producto + "','" + iValor_Porcentaje + "','" + txtPrecioMinorista.Text.ToString().Trim() + "', GETDATE(), " +
                                    "GETDATE(), 'A', '" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), " +
                                    "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                {
                                    //hara el rolBAck
                                    //conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                                    goto reversa;
                                }


                            }
                        }

                        //AQUI ACTUALIZAMOS LA TABLA cv403_precios_productos A sEstado ELIMINADO
                        //=================================================================================================
                        //verificamos si se modifico el precio monorista
                        if (txtPrecioMinorista.Text != sPrecioMinorista)
                        {
                            //T_st_sql = "update cv403_precios_productos set valor = '" + txtPrecCosto.Text.ToString().Trim() + "' where iId_Producto = '" + iId_Producto + "' and dbId_Lista_Precio = '4'";
                            sSql = "update cv403_precios_productos set estado = 'E', fecha_anula = GETDATE(), usuario_anula = '" + Program.sDatosMaximo[0] + "'," +
                                " terminal_anula = '" + Program.sDatosMaximo[1] + "' where id_Producto = '" + iId_Producto + "' and id_Lista_Precio = 4";
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                //HARÁ EL ROLLBACK
                                goto reversa;
                            }

                            else
                            {
                                //AQUI INSERTAMOS EL NUEVO REGISTRO
                                //4 es el dbId_Lista_Precio de la lista minorista
                                dbId_Lista_Precio = 4;
                                Double dMinorista = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + Program.iva + Program.servicio);
                                sSql = "insert into cv403_precios_productos (id_Lista_Precio,id_Producto,valor_Porcentaje,valor,fecha_inicio,fecha_final," +
                                    "estado,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + dbId_Lista_Precio + "'," +
                                    "'" + iId_Producto + "','" + iValor_Porcentaje + "','" + dMinorista + "', GETDATE(), " +
                                    "GETDATE(), 'A', '" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), " +
                                    "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                {
                                    goto reversa;
                                }
                            }
                        }
                    }

                    //si se ejecuta bien hara un commit
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    MessageBox.Show("Registro actualizado correctamente");
                    Limpiar();
                    btnOK_Click(sender, e);
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
                Limpiar();
                btnOK_Click(sender, e);
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

        private void btnCerrarSubCajero_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarSubCajero_Click(object sender, EventArgs e)
        {
            grpDatoIngreProduc.Enabled = false;
            btnSubAgregar.Text = "Nuevo";
            Limpiar();
        }

        private void btnNuevSubCategoria_Click(object sender, EventArgs e)
        {
            dgvProdCategoria.Columns.Remove("columna1");
            Limpiar();
            Grb_DatoCategoria.Enabled = true;
            grpDatoIngreProduc.Enabled = false;
            Grb_opcioCajero.Enabled = false;
            txtIdSubCategoria.Text = "";
            txtNomSubCategoria.Text = "";
        }

        private void dgvProdCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSubAgregar.Text = "Actualizar";
            grpDatoIngreProduc.Enabled = true;
            //para separar los codigos y ponerlos en diferentes textbox
            iId_Producto = Convert.ToInt32(dgvProdCategoria.CurrentRow.Cells[0].Value.ToString());
            string sValReco = dgvProdCategoria.CurrentRow.Cells[1].Value.ToString();
            List<String> lista = sValReco.Split(Convert.ToChar(".")).ToList<String>();

            foreach (String item in lista)
            {
                txtCodigCategoria.Text = item;
            }
            txtNombreCategoria.Text = dgvProdCategoria.CurrentRow.Cells[2].Value.ToString();
            sNombreCategoria = dgvProdCategoria.CurrentRow.Cells[2].Value.ToString(); ;

            txtPrecioCompra.Text = dgvProdCategoria.CurrentRow.Cells[3].Value.ToString();
            sPrecioCompra = dgvProdCategoria.CurrentRow.Cells[3].Value.ToString();

            txtPrecioMinorista.Text = dgvProdCategoria.CurrentRow.Cells[4].Value.ToString();
            sPrecioMinorista = dgvProdCategoria.CurrentRow.Cells[4].Value.ToString();

            string chbiPagIva = dgvProdCategoria.CurrentRow.Cells[5].Value.ToString();
            if (chbiPagIva == "1")
                chkPagaIva.Checked = true;
            else chkPagaIva.Checked = false;
            txtSecuencia.Text = dgvProdCategoria.CurrentRow.Cells[6].Value.ToString();
            cmbColoSubProductos.Text = dgvProdCategoria.CurrentRow.Cells[7].Value.ToString();
            txtStockMin.Text = dgvProdCategoria.CurrentRow.Cells[8].Value.ToString();
            txtStockMax.Text = dgvProdCategoria.CurrentRow.Cells[9].Value.ToString();

            string chbPreModi = dgvProdCategoria.CurrentRow.Cells[10].Value.ToString();
            if (chbPreModi == "True")
                chkPreModifProductos.Checked = true;
            else chkPreModifProductos.Checked = false;

            string chbExpi = dgvProdCategoria.CurrentRow.Cells[11].Value.ToString();
            if (chbExpi == "1")
                chkExpiraProductos.Checked = true;
            else chkExpiraProductos.Checked = false;

        }

        private void btnBuscarSubProduc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarProduc.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarProduc.Text.Trim();
                }
                //btnOK_Click(sender, e);
                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategoria.Enabled = true;
                grpDatoIngreProduc.Enabled = false;
                btnSubAgregar.Text = "Nuevo";
                Limpiar();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
                //btnOK_Click(sender, e);
            }
        }

        private void chkMayusculas_CheckedChanged(object sender, EventArgs e)
        {
            if (txtNombreCategoria.Focus() == true)
            {
                if (chkMayusculas.Checked == true)
                {
                    txtNombreCategoria.CharacterCasing = CharacterCasing.Upper;

                }
                else
                {
                    txtNombreCategoria.CharacterCasing = CharacterCasing.Lower;
                }
            }
        }


        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtPrecCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloDecimal(e);
        }

        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloDecimal(e);
        }

        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloDecimal(e);
        }
    }
}
