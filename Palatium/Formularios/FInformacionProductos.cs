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
    public partial class FInformacionProductos : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false; //creamos la variable
        int id_empresa = Program.iIdEmpresa;
        int id_mascara = 2;
        int preModChe = 0;
        int modiChe = 0;
        int pagIceChe = 0, pagIvaChe = 0, ExirChe = 0;

        public FInformacionProductos()
        {
            InitializeComponent();
            LLenarComboColor();
            LLenarComboProPadre();
            //llenaIdCajero();
        }

        private void FInformacionProductos_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
            cmbEstadoProductos.Text = "ACTIVO";
        }

        #region FUNCIONES DEL USUARIO

        //llenar el comboBox de color 
        private void LLenarComboColor()
        {

            SqlCommand cmd = new SqlCommand("select id_color,descripcion from art_color",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["descripcion"] = "Seleccionar item...";
            dt.Rows.InsertAt(fila, 0);

            cmbColoProductos.ValueMember = "id_color";
            cmbColoProductos.DisplayMember = "descripcion";
            cmbColoProductos.DataSource = dt;
        }

        //llenar el comboBox de Producto padre 
        private void LLenarComboProPadre()
        {

            SqlCommand cmd = new SqlCommand("select id_producto from cv401_productos", ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["id_producto"] = 0;
            dt.Rows.InsertAt(fila, 0);

            cmbProPadreProductos.ValueMember = "id_producto";
            cmbProPadreProductos.DisplayMember = "id_producto";
            cmbProPadreProductos.DataSource = dt;
        }


        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtBuscarProductos.Clear();
            txtCodigoProductos.Enabled = true;
            txtCodigoProductos.Clear();
            txtDescripProductos.Clear();
            txtNivProProductos.Clear();
            txtStockMax.Clear();
            txtStockMin.Clear();
            cmbEstadoProductos.Text = "ACTIVO";

            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select PRO.idempresa as EMPRESA, PRO.codigo as CODIGO, PRO.id_producto_padre as PRODUC_PADRE, "+
                        "PRO.descripcion_uso as DESCRIP_USO, PRO.expira as EXPIRA, PRO.id_color as COLOR, PRO.estado as ESTADO, "+
                        "PRO.nivel as NIV_PRODUC, PRO.modificable as MODIFICABLE, PRO.paga_ice as PAGA_ICE, PRO.paga_iva as PAGA_IVA, "+
                        "PRO.precio_modificable as PRECIO_MODIFI, PRO.stock_min as STOCK_MIN, PRO.stock_max as STOCK_MAX, "+
                        "PRO.id_mascara as MASCARA from cv401_productos PRO";
                }

                else
                {
                    t_st_query = "select PRO.idempresa as EMPRESA, PRO.codigo as CODIGO, PRO.id_producto_padre as PRODUC_PADRE, "+
                        "PRO.descripcion_uso as DESCRIP_USO, PRO.expira as EXPIRA, PRO.id_color as COLOR, PRO.estado as ESTADO, "+
                        "PRO.nivel as NIV_PRODUC, PRO.modificable as MODIFICABLE, PRO.paga_ice as PAGA_ICE, PRO.paga_iva as PAGA_IVA,"+
                        " PRO.precio_modificable as PRECIO_MODIFI, PRO.stock_min as STOCK_MIN, PRO.stock_max as STOCK_MAX, "+
                        "PRO.id_mascara as MASCARA from cv401_productos PRO where PRO.idempresa LIKE '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.codigo like '%' + '" + t_st_datos[1] + "' OR PRO.id_producto_padre like '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.descripcion_uso like '%' + '" + t_st_datos[1] + "' OR PRO.expira like '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.id_color like '%' + '" + t_st_datos[1] + "' OR PRO.estado like '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.nivel like '%' + '" + t_st_datos[1] + "' OR PRO.modificable like '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.paga_ice like '%' + '" + t_st_datos[1] + "' OR PRO.paga_iva like '%' + '" + t_st_datos[1] + "' "+
                        "OR PRO.precio_modificable like '%' + '" + t_st_datos[1] + "'  OR PRO.stock_min like '%' + '" + t_st_datos[1] + "'  "+
                        "OR PRO.stock_max like '%' + '" + t_st_datos[1] + "'  OR PRO.id_mascara like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProductos.Refresh();

                    dgvProductos.Columns[2].Width = 100;
                    dgvProductos.Columns[3].Width = 200;
                    dgvProductos.Columns[4].Width = 60;
                    dgvProductos.Columns[5].Width = 60;
                    dgvProductos.Columns[6].Width = 60;
                    dgvProductos.Columns[7].Width = 60;
                    dgvProductos.Columns[8].Width = 80;
                    dgvProductos.Columns[9].Width = 60;
                    dgvProductos.Columns[10].Width = 60;
                    dgvProductos.Columns[11].Width = 100;
                    dgvProductos.Columns[12].Width = 80;
                    dgvProductos.Columns[13].Width = 80;
                    dgvProductos.Columns[14].Width = 60;
                    dgvProductos.Columns[15].Width = 100;
                    dgvProductos.Columns[16].Width = 100;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            
            if(chkModifiProductos.Checked==true) 
                modiChe=1;
            else modiChe = 0;
            if(chkPagIceProductos.Checked==true)
                pagIceChe=1;
            else pagIceChe = 0;
            if(chkPagIvaProductos.Checked==true)
                pagIvaChe=1;
            else pagIvaChe = 0;
            if(chkPreModifProductos.Checked==true)
                preModChe=1;
            else preModChe = 0;
            if(chkExpiraProductos.Checked==true)
                ExirChe=1;
            else ExirChe = 0;

            try
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" 
                    + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                string T_st_query = "insert into cv401_productos (idempresa,codigo,id_producto_padre,descripcion_uso,expira,id_color,"+
                    "estado,nivel,modificable,paga_ice,paga_iva,precio_modificable,stock_min,stock_max,id_mascara,fecha_ingreso,usuario_ingreso,"+
                    "terminal_ingreso)values('" + id_empresa + "','" + txtCodigoProductos.Text.ToString().Trim() + "',"+
                    "'" + cmbProPadreProductos.SelectedValue.ToString() + "','" + txtDescripProductos.Text.ToString().Trim() + "',"+
                    "'" + ExirChe + "','" + cmbColoProductos.SelectedValue.ToString() + "','" + estado + "', '" + txtNivProProductos.Text.ToString().Trim() + "',"+
                    "'" + modiChe + "','" + pagIceChe + "','" + pagIvaChe + "','" + preModChe + "', '" + txtStockMin.Text.ToString().Trim() + "',"+
                    "'" + txtStockMax.Text.ToString().Trim() + "', '" + id_mascara + "', '" + t_st_fecha + "', '" + Program.sNombreUsuario + "', "+
                    "'" + Environment.MachineName.ToString() + "')";

                x = conexion.GFun_Lo_Rellenar_Grid(T_st_query, "cv401_productos");
                if (x == true)
                {
                    MessageBox.Show("Registro insertado correctamente");
                }
                else
                {
                    MessageBox.Show("Error al insertar el registro");
                }

                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }

            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
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

                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        #endregion

        private void Btn_CerrarProductos_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarProductos_Click(object sender, EventArgs e)
        {
            Grb_DatoProductos.Enabled = false;
            btnNuevoProductos.Text = "Nuevo";
            limpiarTodo();
        }

       

        private void CmbEstadoProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoProductos.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoProductos.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void BtnNuevoProductos_Click(object sender, EventArgs e)
        {
            

            string T_st_query = "";
            string T_st_mensaje = "";

            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoProductos.Text == "Nuevo")
            {
                limpiarTodo();
                Grb_DatoProductos.Enabled = true;
                btnNuevoProductos.Text = "Guardar";
                txtCodigoProductos.Focus();
                cmbColoProductos.Text = "Seleccionar item...";
                cmbProPadreProductos.Text = "Seleccionar item...";
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else if (btnNuevoProductos.Text == "Guardar")
            {
                if (chkModifiProductos.Checked == true)
                    modiChe = 1;
                else modiChe = 0;
                if (chkPagIceProductos.Checked == true)
                    pagIceChe = 1;
                else pagIceChe = 0;
                if (chkPagIvaProductos.Checked == true)
                    pagIvaChe = 1;
                else pagIvaChe = 0;
                if (chkPreModifProductos.Checked == true)
                    preModChe = 1;
                else preModChe = 0;
                if (chkExpiraProductos.Checked == true)
                    ExirChe = 1;
                else ExirChe = 0;

                if ((txtCodigoProductos.Text == "") && (txtDescripProductos.Text == "") && (cmbProPadreProductos.Text == "Seleccionar item") && (cmbColoProductos.Text == "Seleccionar item") && (txtNivProProductos.Text == "") && (txtStockMax.Text == "") && (txtStockMin.Text == ""))
                {
                    MessageBox.Show("Debe rellenar todos los campos obligatorios.", "Aviso", MessageBoxButtons.OK);
                    txtCodigoProductos.Focus();
                }

                else if (txtCodigoProductos.Text == "")
                {
                    MessageBox.Show("Favor ingrese el código del producto.", "Aviso", MessageBoxButtons.OK);
                    txtCodigoProductos.Focus();
                }

                else if (txtDescripProductos.Text == "")
                {
                    MessageBox.Show("Favor ingrese la descripción del producto.", "Aviso", MessageBoxButtons.OK);
                    txtDescripProductos.Focus();
                }
                else if (cmbProPadreProductos.Text == "0")
                {
                    MessageBox.Show("Favor selecione una opcion del producto padre.", "Aviso", MessageBoxButtons.OK);
                    cmbProPadreProductos.Focus();
                }
                else if (cmbColoProductos.Text == "Seleccionar item")
                {
                    MessageBox.Show("Favor selecione una opcion de color.", "Aviso", MessageBoxButtons.OK);
                    cmbColoProductos.Focus();
                }
                else if (txtStockMax.Text == "")
                {
                    MessageBox.Show("Favor ingrese el Stock_Max del producto.", "Aviso", MessageBoxButtons.OK);
                    txtStockMax.Focus();
                }
                else if (txtStockMin.Text == "")
                {
                    MessageBox.Show("Favor ingrese el Stock_Min del producto.", "Aviso", MessageBoxButtons.OK);
                    txtStockMin.Focus();
                }
                else
                {
                    string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" 
                        + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        limpiarTodo();
                    }
                    else
                    {
                        T_st_sql = "insert into cv401_productos (idempresa,codigo,id_producto_padre,descripcion_uso,expira,id_color,estado,"+
                            "nivel,modificable,paga_ice,paga_iva,precio_modificable,stock_min,stock_max,id_mascara,fecha_ingreso,usuario_ingreso,"+
                            "terminal_ingreso)values('" + id_empresa + "', '" + txtCodigoProductos.Text.ToString().Trim() + "', "+
                            "'" + cmbProPadreProductos.SelectedValue.ToString() + "', '" + txtDescripProductos.Text.ToString().Trim() + "',"+
                            " '" + ExirChe + "', '" + cmbColoProductos.SelectedValue.ToString() + "','" + estado + "', "+
                            "'" + txtNivProProductos.Text.ToString().Trim() + "','" + modiChe + "','" + pagIceChe + "','" + pagIvaChe + "',"+
                            "'" + preModChe + "', '" + txtStockMin.Text.ToString().Trim() + "','" + txtStockMax.Text.ToString().Trim() + "', "+
                            "'" + id_mascara + "', '" + t_st_fecha + "', '" + Program.sNombreUsuario + "', '" + Environment.MachineName.ToString() + "')";

                        //sisque no me ejuta el query 
                        MessageBox.Show(T_st_sql);
                        if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                        {
                            //hara el rolBAck
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        }
                        else
                        {
                            //si no se ejecuta bien hara un commit
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                            MessageBox.Show("Registro ingresado correctamente");
                            btnNuevoProductos.Text = "Nuevo";
                            Grb_DatoProductos.Enabled = false;
                            limpiarTodo();
                        }
                    }
                }
            }

            //SI EL BOTON ESTA EN OPCION ACTUALIZAR
            else if (btnNuevoProductos.Text == "Actualizar")
            {
                if (chkModifiProductos.Checked == true)
                    modiChe = 1;
                else modiChe = 0;
                if (chkPagIceProductos.Checked == true)
                    pagIceChe = 1;
                else pagIceChe = 0;
                if (chkPagIvaProductos.Checked == true)
                    pagIvaChe = 1;
                else pagIvaChe = 0;
                if (chkPreModifProductos.Checked == true)
                    preModChe = 1;
                else preModChe = 0;
                if (chkExpiraProductos.Checked == true)
                    ExirChe = 1;
                else ExirChe = 0;

                if (txtDescripProductos.Text == "")
                {
                    MessageBox.Show("Favor ingrese la descripción del producto.", "Aviso", MessageBoxButtons.OK);
                    txtDescripProductos.Focus();
                }

                else
                {
                    T_st_query = "update cv401_productos set idempresa = '" + id_empresa + "', codigo = '" + txtCodigoProductos.Text.ToString().Trim() + "',"+
                        " id_producto_padre = '" + cmbProPadreProductos.SelectedValue.ToString() + "' , descripcion_uso = '" + txtDescripProductos.Text.ToString().Trim() + "', "+
                        "expira = '" + ExirChe + "', id_color = '" + cmbColoProductos.SelectedValue.ToString() + "', estado = '" + estado + "',"+
                        " nivel = '" + txtNivProProductos.Text.ToString().Trim() + "', modificable = '" + modiChe + "', paga_ice = '" + pagIceChe + "', "+
                        "paga_iva = '" + pagIvaChe + "', precio_modificable = '" + preModChe + "', stock_min = '" + txtStockMin.Text.ToString().Trim() + "', "+
                        "stock_max = '" + txtStockMax.Text.ToString().Trim() + "', id_mascara = '" + id_mascara + "' "+
                        "where codigo = '" + txtCodigoProductos.Text.ToString().Trim() + "'";

                    T_st_mensaje = "Registro Actualizado Ëxitosamente";
                    actualizarRegistro(T_st_query, T_st_mensaje);
                }
            }
        }

        private void Btn_AnularProductos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de bajar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" 
                    + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                string T_st_query = "update cv401_productos set estado = 'E', fecha_anula = '" + t_st_fecha + "', usuario_anula = '" + Program.sNombreUsuario + "', "+
                    "terminal_anula = '" + Environment.MachineName.ToString() + "' where codigo = '" + txtCodigoProductos.Text.ToString().Trim() + "'";
                string T_st_mensaje = "Registro Eliminado Ëxitosamente";

                actualizarRegistro(T_st_query, T_st_mensaje);
            }
            else
            {
                MessageBox.Show("Se canceló la eliminacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }


        private void Cmb_ProPadreProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProPadreProductos.SelectedValue.ToString() != null)
            {
                string id_producto_padre = cmbProPadreProductos.SelectedValue.ToString();
                tabCon_Productos.Enabled = true;
            }
        }

        private void Cmb_ColoProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbColoProductos.SelectedValue.ToString() != null)
            {
                string id_producto_padre = cmbColoProductos.SelectedValue.ToString();
                tabCon_Productos.Enabled = true;
            }
        }

        private void Dgv_Productos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Grb_DatoProductos.Enabled = true;
            txtCodigoProductos.Enabled = false;
            btnNuevoProductos.Text = "Actualizar";

            Grb_DatoProductos.Enabled = true;
            //chkModifiProductos.Text = dgvProductos.CurrentRow.Cells[8].Value.ToString();
            string chbModifi = dgvProductos.CurrentRow.Cells[8].Value.ToString();
            if (chbModifi == "True")
                chkModifiProductos.Checked = true;
            else chkModifiProductos.Checked = false;

            //chkPreModifProductos.Text = dgvProductos.CurrentRow.Cells[11].Value.ToString();
            string chbPreModifi = dgvProductos.CurrentRow.Cells[11].Value.ToString();
            if (chbPreModifi == "True")
                chkPreModifProductos.Checked = true;
            else chkPreModifProductos.Checked = false;

            //chkPagIceProductos.Text = dgvProductos.CurrentRow.Cells[9].Value.ToString();
            string chbPagIce = dgvProductos.CurrentRow.Cells[9].Value.ToString();
            if (chbPagIce == "1")
                chkPagIceProductos.Checked = true;
            else chkPagIceProductos.Checked = false;

            //chkPagIvaProductos.Text = dgvProductos.CurrentRow.Cells[10].Value.ToString();
            string chbPagIva = dgvProductos.CurrentRow.Cells[10].Value.ToString();
            if (chbPagIva == "1")
                chkPagIvaProductos.Checked = true;
            else chkPagIvaProductos.Checked = false;
            
            //chkExpiraProductos.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            string chbExpira = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            if (chbExpira == "1")
                chkExpiraProductos.Checked = true;
            else chkExpiraProductos.Checked = false;

            txtCodigoProductos.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            txtDescripProductos.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            cmbProPadreProductos.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            cmbColoProductos.Text = dgvProductos.CurrentRow.Cells[5].Value.ToString();
            txtNivProProductos.Text = dgvProductos.CurrentRow.Cells[7].Value.ToString();
            txtStockMin.Text = dgvProductos.CurrentRow.Cells[12].Value.ToString();
            txtStockMax.Text = dgvProductos.CurrentRow.Cells[13].Value.ToString();
            

            if (dgvProductos.CurrentRow.Cells[6].Value.ToString() == "A")
            {
                cmbEstadoProductos.Text = "ACTIVO";
            }
            else
            {
                cmbEstadoProductos.Text = "ELIMINADO";
            }
        }

        private void btnBuscarProductos_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtBuscarProductos.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarProductos.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoProductos.Enabled = false;
                btnNuevoProductos.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        
        }

        

    }
}
