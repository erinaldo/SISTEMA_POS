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
    public partial class FSubCategoria : Form
    {

        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dtConsulta;
        string estado = "";
        string T_st_sql = "", T_st_sql2 = "";
        bool x = false; //creamos la variable
        string valor, valor2;
        DataTable dtPadre;
        DataTable dtCompra;
        DataTable dtConsumo;
        DataTable dtColor;
        DataTable dtProPadre;
        DataTable dtEmpresa;

        int modiChe = 0;
        int preModChe = 0;
        int resp;
        int cg_tipoNombre = 5076;
        int nombInterno = 1;
        int numRepliTrig = 1;
        int numControRepli = 0;
        int pagIva;
        string valReco;
        int nivel = 3;
        int codigoID;
        int iSubcategoria = 1;
        int iModificador = 0;
        int iUltimo = 0;
        string item2;



        string sTabla;
        string sCampo;
        long iMaximo;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();




        public FSubCategoria()
        {
            InitializeComponent();
            //toolStripStatusLabel1.Text = Dns.GetHostName();
        }

        private void FSubCategorias_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            
            //cmbEstadoCatego.Text = "ACTIVO";
            LLenarComPadre();
            LLenarComboCompra();
            LLenarComboCosumo();
            ////LLenarComboProPadre();
            LLenarComboEmpresa();
            llenarGrid(t_st_datos);
        }

        #region FUNCIONES DEL USUARIO

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            //txtBuscarCatego.Clear();
            txtCodigoCatego.Enabled = true;
            txtCodigoCatego.Clear();
            txtDescripCateg.Clear();
            cmbEstadoCatego.Text = "ACTIVO";

            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
        }

        //llenar el comboBox Codigo Padre
        private void LLenarComPadre()
        {
            try
            {
                dtPadre = new DataTable();


                string sql = "select P.id_producto ,NP.nombre  from cv401_productos P,cv401_nombre_productos NP where P.id_producto = NP.id_producto  "+
                    "and P.id_producto_padre in (select id_producto from cv401_productos where codigo ='2') and P.nivel = 2 and P.estado ='A' "+
                    "and NP.estado='A' and subcategoria = 1";

                cmbPadre.llenar(dtPadre, sql);
                cmbPadre.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("No existe niguna SubCategoria");
            }
        }

        //llenar el comboBox unidad compra 
        private void LLenarComboCompra()
        {
            try
            {
                dtCompra = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00042' and estado='A'";
                cmbCompra.llenar(dtCompra, sql);
                cmbCompra.SelectedIndex = 24;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta");
            }
        }

        //llenar el comboBox unidad consumo
        private void LLenarComboCosumo()
        {
            try
            {
                dtConsumo = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00042' and estado='A'";
                cmbConsumo.llenar(dtConsumo, sql);
                cmbConsumo.SelectedIndex = 24;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un problema al realizar la consulta");
            }
        }

        //////llenar el comboBox de Producto padre 
        ////private void LLenarComboProPadre()
        ////{

        ////    SqlCommand cmd = new SqlCommand("select id_producto from cv401_productos", ConexionBD.ConexionBD.cnn);
        ////    SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
        ////    DataTable dt = new DataTable();
        ////    adaptad.Fill(dt);
        ////    DataRow fila = dt.NewRow();
        ////    cmbProPadre.ValueMember = "id_producto";
        ////    cmbProPadre.DisplayMember = "id_producto";
        ////    cmbProPadre.DataSource = dt;
        ////}


        //llenar comboBox de Empresa
        private void LLenarComboEmpresa()
        {
            try
            {
                dtEmpresa = new DataTable();
                string sql = "select idempresa,nombrecomercial from sis_empresa";
                cmbEmpresa.llenar(dtEmpresa, sql);
                cmbEmpresa.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un problema al realizar la consulta");
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";

                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select P.id_producto,P.codigo as Codigo,NP.nombre as Nombre,P.modificable as Modificable, " +
                        "P.precio_modificable as Prec_Modificable, P.paga_iva as Paga_Iva,P.secuencia as Secuencia, " +
                        "P.estado as Estado from cv401_productos P,cv401_nombre_productos NP " +
                        "where P.id_producto = NP.id_producto and id_producto_padre = '" + cmbPadre.SelectedValue.ToString() + "' and P.nivel=3 and P.estado='A' " +
                        "and NP.estado='A' and P.modificador = 0 and P.subcategoria=1 and P.ultimo_nivel=0";
                    //t_st_query = "select PRO.codigo as Código,NOM.nombre as Nombre,UNI.cg_unidad as Unidad_Compra,UNI.cg_unidad as Unidad_Consumo,PRO.modificable as Modificable,PRO.precio_modificable from cv401_productos PRO inner join cv401_nombre_productos NOM on PRO.id_producto=NOM.id_producto inner join cv401_unidades_productos UNI on PRO.id_producto=UNI.id_producto";
                }

                else
                {
                    t_st_query = "select P.id_producto,P.codigo as Codigo,NP.nombre as Nombre,P.modificable as Modificable, " +
                        "P.precio_modificable as Prec_Modificable, P.paga_iva as Paga_Iva,P.secuencia as Secuencia, " +
                        "P.estado as Estado from cv401_productos P,cv401_nombre_productos NP " +
                        "where P.id_producto = NP.id_producto and id_producto_padre = '" + cmbPadre.SelectedValue.ToString() + "' and P.nivel=3 and P.estado='A' " +
                        "and NP.estado='A' and P.modificador = 0 and P.subcategoria=1 and P.ultimo_nivel=0 and NP.nombre LIKE '%' + '" + t_st_datos[1] + "' + '%'";
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
                    dgvProductos.Columns[0].Visible = true;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
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

                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
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

                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        #endregion

        private void btnCerrarCategori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarCategori_Click(object sender, EventArgs e)
        {
            Grb_DatoCategori.Enabled = false;
            btnNuevoCategori.Text = "Nuevo";
            limpiarTodo();
        }

        private void cmbEstadoCatego_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoCatego.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoCatego.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void btnNuevoCategori_Click(object sender, EventArgs e)
        {
            //llenarGrid();
            string T_st_query = "";
            string T_st_query2 = "";
            string T_st_query3 = "";
            string T_st_sql3 = "";
            string T_st_sql4 = "";
            string T_st_mensaje = "";

            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoCategori.Text == "Nuevo")
            {
                //limpiarTodo();
                Grb_DatoCategori.Enabled = true;
                txtSecuencia.Text = "";
                chkModificable.Checked = false;
                chkPreModificable.Checked = false;
                chkPagaIva.Checked = false;
                btnNuevoCategori.Text = "Guardar";
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else if (btnNuevoCategori.Text == "Guardar")
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bool bRespuesta = false;

                T_st_sql = "select codigo from cv401_productos where id_producto = " + cmbPadre.SelectedValue;

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, T_st_sql);

                if (bRespuesta == true)
                {
                    valor = dtConsulta.Rows[0].ItemArray[0].ToString();
                }


                valor = valor + "." + txtCodigoCatego.Text;
                    

                if (chkModificable.Checked == true)
                    modiChe = 1;
                else modiChe = 0;
                if (chkPreModificable.Checked == true)
                    preModChe = 1;
                else preModChe = 0;
                if (chkPagaIva.Checked == true)
                    pagIva = 1;
                else pagIva = 0;


               
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
                    T_st_sql = "insert into cv401_productos (idempresa,codigo,id_producto_padre,estado,nivel,modificable,precio_modificable,"+
                        "paga_iva,secuencia,modificador,subcategoria,ultimo_nivel,fecha_ingreso,usuario_ingreso,terminal_ingreso)"+
                        "values('" + cmbEmpresa.SelectedValue.ToString() + "','" + valor + "'," + Convert.ToInt32(cmbPadre.SelectedValue) + ","+
                        "'" + estado + "','" + nivel + "','" + modiChe + "','" + preModChe + "','" + pagIva + "',"+
                        "'" + txtSecuencia.Text.ToString().Trim() + "','" + iModificador + "','" + iSubcategoria + "','" + iUltimo + "', "+
                        "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //sisque no me ejuta el query 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                    {
                        MessageBox.Show("Ha ingresado un código que ya ha sido registrado anteriormente. Por Favor introduzca uno nuevo","Alerta",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        goto reversa;
                    }
                    else
                    {
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
                            resp = Convert.ToInt32(iMaximo);
                        }
                    }

                    T_st_sql2 = "insert into cv401_nombre_productos (id_producto,cg_tipo_nombre,nombre,nombre_interno,estado,numero_replica_trigger,"+
                        "numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + resp + "','" + cg_tipoNombre + "',"+
                        "'" + txtDescripCateg.Text.ToString().Trim() + "','" + nombInterno + "', 'A', '" + numRepliTrig + "',"+
                        "'" + numControRepli + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql2))
                    {
                        goto reversa;
                    }

                    int cg_tipo_unidad = 6142, unidad_compra = 1, numero_replica_tri = 1, numero_control_repli = 0;
                    T_st_sql3 = "insert into cv401_unidades_productos (id_producto,cg_tipo_unidad,cg_unidad,unidad_compra,estado,usuario_creacion,"+
                        "terminal_creacion,fecha_creacion,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)"+
                        "values('" + resp + "','" + cg_tipo_unidad + "','" + cmbCompra.SelectedValue.ToString() + "','" + unidad_compra + "', "+
                        "'A', '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE()," +
                        "'" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', " +
                        "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql3))
                    {
                        goto reversa;
                    }
                    else
                    {
                    }

                    cg_tipo_unidad++; unidad_compra--;
                    T_st_sql4 = "insert into cv401_unidades_productos (id_producto,cg_tipo_unidad,cg_unidad,unidad_compra,estado,usuario_creacion,"+
                        "terminal_creacion,fecha_creacion,numero_replica_trigger,numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)"+
                        "values('" + resp + "','" + cg_tipo_unidad + "','" + cmbConsumo.SelectedValue.ToString() + "','" + unidad_compra + "', "+
                        "'A', '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE()," +
                        "'" + numero_replica_tri + "','" + numero_control_repli + "', GETDATE()', '" + Program.sDatosMaximo[0] + "'," +
                        "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql4))
                    {
                        goto reversa;
                    }
                    else
                    {
                        //si no se ejecuta bien hara un commit
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        MessageBox.Show("Registro ingresado correctamente");
                        Grb_DatoCategori.Enabled = false;
                        btnNuevoCategori.Text = "Nuevo";
                        limpiarTodo();
                        goto fin;
                    }
                }

            
            }


            //SI EL BOTON ESTA EN OPCION ACTUALIZAR
            else if (btnNuevoCategori.Text == "Actualizar")
            {
                if (chkPagaIva.Checked == true)
                    pagIva = 1;
                else pagIva = 0;

                //if (txtDescripCateg.Text == "")
                //{
                //    MessageBox.Show("Favor ingrese la descripción y la seccion mesa del pos_mesa.", "Aviso", MessageBoxButtons.OK);
                //    txtDescripCateg.Focus();
                //}

                //else
                //{
                if (cmbPadre.Text == "2")
                {
                    valor2 = "2." + txtCodigoCatego.Text;

                }

                T_st_query = "update cv401_productos set codigo = '" + valor2 + "' , secuencia = '" + txtSecuencia.Text.ToString().Trim() + "' , "+
                    "paga_iva = '" + pagIva + "' where id_producto = '" + codigoID + "'";

                T_st_query2 = "update cv401_nombre_productos set nombre = '" + txtDescripCateg.Text.ToString().Trim() + "' "+
                    "where id_producto = '" + codigoID + "'";

                //T_st_query3 = "update cv401_nombre_productos set nombre = '" + txtDescripCateg.Text.ToString().Trim() + "' where id_producto = '" + codigoID + "'";
                T_st_mensaje = "Registro Actualizado Ëxitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
                actualizarRegistro2(T_st_query2, T_st_mensaje);
                goto fin;
                //}
            }

        reversa:

            try
            {
                //aqui va la instruccion para hacer el rollback
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //MessageBox.Show("Ocurriò un problema en la transacciòn");
                limpiarTodo();

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
        }

        private void btnAnularCategori_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de bajar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" 
                    + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();

                int f = Convert.ToInt32(dgvProductos.CurrentRow.Index);

                string T_st_query = "update cv401_productos set estado = 'E', codigo = 'codigo." + dgvProductos.Rows[f].Cells[0].Value.ToString() + "' fecha_anula = GETDATE(), usuario_anula = '" + Program.sDatosMaximo[0] + "'," +
                    " terminal_anula = '" + Program.sDatosMaximo[1] + "' where codigo = '" + txtCodigoCatego.Text.ToString().Trim() + "'";

                string T_st_mensaje = "Registro Eliminado Ëxitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
            }
            else
            {
                MessageBox.Show("Se canceló la eliminacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                //llenarGrid(t_st_datos);
            }
        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnNuevoCategori.Text = "Actualizar";
            Grb_DatoCategori.Enabled = true;
            cmbPadre.Enabled = false;

            codigoID = Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value.ToString());
            //para separar los codigos y ponerlos en diferentes textbox
            string valReco = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            List<String> lista = valReco.Split(Convert.ToChar(".")).ToList<String>();

            foreach (String item in lista)
            {
                item2 = item;
            }

            if (item2 == "2")
                txtCodigoCatego.Text = "";
            else txtCodigoCatego.Text = item2;

            txtDescripCateg.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            //cmbCompra.Text = dgvCategoria.CurrentRow.Cells[2].Value.ToString();
            //cmbConsumo.Text = dgvCategoria.CurrentRow.Cells[3].Value.ToString();
            //cmbEmpresa.Text = dgvCategoria.CurrentRow.Cells[4].Value.ToString();
            ////cmbProPadre.Text = dgvCategoria.CurrentRow.Cells[5].Value.ToString();
            ////txtNivPro.Text = dgvCategoria.CurrentRow.Cells[6].Value.ToString();

            string chbModifi = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            if (chbModifi == "True")
                chkModificable.Checked = true;
            else chkModificable.Checked = false;

            string chbPreModifi = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            if (chbPreModifi == "True")
                chkPreModificable.Checked = true;
            else chkPreModificable.Checked = false;

            string chbPagIva = dgvProductos.CurrentRow.Cells[5].Value.ToString();
            if (chbPagIva == "1")
                chkPagaIva.Checked = true;
            else chkPagaIva.Checked = false;
            txtSecuencia.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();
            if (dgvProductos.CurrentRow.Cells[7].Value.ToString() == "A")
            {
                cmbEstadoCatego.Text = "ACTIVO";
            }
            else
            {
                cmbEstadoCatego.Text = "ELIMINADO";
            }
        }

        private void chkMayusculas_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDescripCateg.Focus() == true)
            {
                if (chkMayusculas.Checked == true)
                {
                    txtDescripCateg.CharacterCasing = CharacterCasing.Upper;

                }
                else
                {
                    txtDescripCateg.CharacterCasing = CharacterCasing.Lower;
                }
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarCategoria.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarCategoria.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoCategori.Enabled = false;
                btnNuevoCategori.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPadre.SelectedValue.ToString() == "0")
                {
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    dgvProductos.DataSource = dtConsulta;
                }
                else
                {
                    //LLENAR DATOS EN EL GRID SEGUN SELECCION DEL COMBO PADRE
                    string[] t_st_datos = { "1", "adsdasdasd" };
                    llenarGrid(t_st_datos);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error.");
            }
        }



    }
}
