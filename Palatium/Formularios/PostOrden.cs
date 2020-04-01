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
using System.Collections;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class PostOrden : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false; //creamos la variable
        string inIdPos = "";
        int resp;
        string inId_pos_orden;



        string sTabla;
        string sCampo;
        long iMaximo;
        DataTable dtConsulta;
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        
        public PostOrden()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = Dns.GetHostName();
            //groupB_PosOrd.Enabled = true;
            LLenarComboMesero();
            
            LLenarComboOriOrden();
            LLenarComboCajero();
            LLenarComboLocali();
            LLenarComboJornad();
            LLenarComboMesa();
            LLenarComboCliente();
        }

        private void PostOrden_Load(object sender, EventArgs e)
        {
            
            dgvPosOrd.AllowUserToAddRows = false; //el usuario no podra agregar filas hasta dar un claick en un boton especifico
            string[] t_st_datos = { "1", "adsdasdasd" };
            //llenarGrid(t_st_datos);
            cmbEstadoPosOrd.Text = "ACTIVO";
            //groupB_PosOrd.Enabled = false;

            DataGridViewTextBoxColumn columna1 = new DataGridViewTextBoxColumn(); //creo la primera columna de tipo textBox
            columna1.HeaderText = "Orden"; //le pongo como titulo Orden
            columna1.Width=80; //le pongo el tamaño de la celda
            columna1.Name = "columna1";
            columna1.ReadOnly = true; //me permite hacer que la celda no sea editable 
            dgvPosOrd.Columns.Add(columna1); //agrego en el datagridview la columna1

            loadData();
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.HeaderText = "Productos";
            combo.Name = "combo";
            DataRow fila = dt.NewRow();
            combo.ValueMember = "correlativo";
            combo.DisplayMember = "descripcion";
            combo.DataSource = dt;
            dgvPosOrd.Columns.AddRange(combo);

            DataGridViewTextBoxColumn columna4 = new DataGridViewTextBoxColumn();
            columna4.HeaderText = "Cantidad";
            columna4.Width = 80;
            columna4.Name = "columna4";
            dgvPosOrd.Columns.Add(columna4);


            DataGridViewTextBoxColumn columna3 = new DataGridViewTextBoxColumn(); 
            columna3.HeaderText = "Precio"; 
            columna3.Width = 80; 
            columna3.Name="columna3";
            dgvPosOrd.Columns.Add(columna3);

            

            DataGridViewTextBoxColumn columna5 = new DataGridViewTextBoxColumn();
            columna5.HeaderText = "Total";
            columna5.Width = 80;
            columna5.Name = "columna5";
            columna5.ReadOnly = true;
            dgvPosOrd.Columns.Add(columna5);

            LLenarComboMesero();
        }

        #region FUNCIONES DEL USUARIO

        //llenar el comboBox de mesero
        private void LLenarComboMesero()
        {
      
            SqlCommand cmd = new SqlCommand("select id_pos_mesero,descripcion from pos_mesero",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);   
            DataRow fila = dt.NewRow();
            cmbMesPosOrd.ValueMember = "id_pos_mesero";
            cmbMesPosOrd.DisplayMember = "descripcion";
            cmbMesPosOrd.DataSource = dt;
            cmbMesPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de origen orden
        private void LLenarComboOriOrden()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_origen_orden,descripcion from pos_origen_orden",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbOriOrdPosOrd.ValueMember = "id_pos_origen_orden";
            cmbOriOrdPosOrd.DisplayMember = "descripcion";
            cmbOriOrdPosOrd.DataSource = dt;
            cmbOriOrdPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de cajero
        private void LLenarComboCajero()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_cajero,descripcion from pos_cajero",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbCajPosOrd.ValueMember = "id_pos_cajero";
            cmbCajPosOrd.DisplayMember = "descripcion";
            cmbCajPosOrd.DataSource = dt;
            cmbCajPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de localidad
        private void LLenarComboLocali()
        {

            SqlCommand cmd = new SqlCommand("select id_localidad,nombre_localidad from tp_vw_localidades",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbLocaliPosOrd.ValueMember = "id_localidad";
            cmbLocaliPosOrd.DisplayMember = "nombre_localidad";
            cmbLocaliPosOrd.DataSource = dt;
            cmbLocaliPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de jornada
        private void LLenarComboJornad()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_jornada,descripcion from pos_jornada",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbJornaPosOrd.ValueMember = "id_pos_jornada";
            cmbJornaPosOrd.DisplayMember = "descripcion";
            cmbJornaPosOrd.DataSource = dt;
            cmbJornaPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de mesa
        private void LLenarComboMesa()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_mesa,descripcion from pos_mesa",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbMesaPosOrd.ValueMember = "id_pos_mesa";
            cmbMesaPosOrd.DisplayMember = "descripcion";
            cmbMesaPosOrd.DataSource = dt;
            cmbMesaPosOrd.SelectedIndex = 0;
        }

        //llenar el comboBox de cliente
        private void LLenarComboCliente()
        {

            SqlCommand cmd = new SqlCommand("select id_persona,apellidos from tp_personas",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);
            DataRow fila = dt.NewRow();
            cmbClientPosOrd.ValueMember = "id_persona";
            cmbClientPosOrd.DisplayMember = "apellidos";
            cmbClientPosOrd.DataSource = dt;
            cmbClientPosOrd.SelectedIndex = 0;
        }


        ////LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            cmbEstadoPosOrd.Text = "ACTIVO";

            string[] t_st_datos = { "1", "adsdasdasd" };
            //llenarGrid(t_st_datos);
        }

        //FUNCION PARA CARGAR LA TABLA DE PRODUCTOS
        private DataTable loadData()
        {
            SqlDataAdapter adaptad = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet ds = new DataSet();

            string sql = "select correlativo,descripcion from cv401_vw_productos";
            //string sql = "select * from cv401_vw_productos";
            cmd = new SqlCommand(sql,ConexionBD.ConexionBD.cnn);

            adaptad.SelectCommand = cmd;
            adaptad.Fill(ds);
            dt = ds.Tables[0];

            return dt;
        }

        private void Dgv_PosOrd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        #endregion

        private void BtnNuevoCajero_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnGuardDetaOrd.Enabled = true;
            string T_st_Max="";
            string T_st_query = "";
            string T_st_mensaje = "";

            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoPosOrd.Text == "Nuevo")
            {
                limpiarTodo();
                
                btnNuevoPosOrd.Text = "Guardar";
                cmbLocaliPosOrd.Focus();
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else if (btnNuevoPosOrd.Text == "OK")
            {
                dgvPosOrd.Rows.Clear();
                if (txtFecha.Text=="")
                {
                    MessageBox.Show("Favor llenar la fecha de la orden");
                    txtFecha.Focus();
                }
                else
                {
                    string feha_ingreso=txtFecha.Text.ToString().Trim();
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
                        T_st_sql = "insert into pos_orden (id_localidad,id_pos_jornada,id_pos_mesa,id_pos_mesero,id_pos_cajero,id_pos_origen_orden,"+
                            "id_persona,estado,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + cmbLocaliPosOrd.SelectedValue.ToString() + "', "+
                            "'" + cmbJornaPosOrd.SelectedValue.ToString() + "', '" + cmbMesaPosOrd.SelectedValue.ToString() + "',"+
                            "'" + cmbMesPosOrd.SelectedValue.ToString() + "','" + cmbCajPosOrd.SelectedValue.ToString() + "',"+
                            "'" + cmbOriOrdPosOrd.SelectedValue.ToString() + "','" + cmbClientPosOrd.SelectedValue.ToString() + "', "+
                            "'" + estado + "', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                        //sisque no me ejuta el query 
                        if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                        {
                            //hara el rolBAck
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        }
                        else
                        {
                            //si ejecuta bien hara un commit
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                            //MessageBox.Show("Registro ingresado correctamente");
                            Grb_listRePosMesa.Enabled = true;




                            //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_ORDEN
                            dtConsulta = new DataTable();
                            dtConsulta.Clear();

                            sTabla = "cv402_cabecera_movimientos";
                            sCampo = "Id_Movimiento_Bodega";

                            iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                            if (iMaximo == -1)
                            {
                                ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                                ok.ShowInTaskbar = false;
                                ok.ShowDialog();
                            }

                            else
                            {
                                resp = Convert.ToInt32(iMaximo);

                                dgvPosOrd.Rows.Add(inId_pos_orden);
                                groupB_PosOrd.Enabled = false;
                                Grb_opcioCajero.Enabled = false;
                            }
                        }
                      }
                    }
                }
            }
        


        private void Btn_CerrPosOrd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarPosOrd_Click(object sender, EventArgs e)
        {
            
            btnNuevoPosOrd.Text = "Nuevo";
            limpiarTodo();
        }

        private void CmbEstadoPosOrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoPosOrd.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoPosOrd.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void btnGuardDetaOrd_Click(object sender, EventArgs e)
        {
            string estado = "A";
            foreach (DataGridViewRow row in dgvPosOrd.Rows)
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
                    int orden = Convert.ToInt32(row.Cells["columna1"].Value);
                    int producto = Convert.ToInt32(row.Cells["combo"].Value);
                    int cantida = Convert.ToInt32(row.Cells["columna3"].Value);
                    double preUni = Convert.ToDouble(row.Cells["columna4"].Value);
                    

                    T_st_sql = "insert into pos_detalle_orden (id_pos_orden,id_producto,precio_unidad,cantidad,estado,fecha_ingreso,"+
                        "usuario_ingreso,terminal_ingreso)values('" + orden.ToString().Trim() + "', '" + producto.ToString().Trim() + "',"+
                        " '" + preUni.ToString().Trim() + "','" + cantida.ToString().Trim() + "', '" + estado + "', GETDATE(), "+
                        "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //sisque no me ejuta el query 
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
                    }
                }
           }

            dgvPosOrd.FirstDisplayedScrollingRowIndex = dgvPosOrd.RowCount - 1; //me sirve para dirigirme al ultimo registro del Grid
            if (dgvPosOrd.FirstDisplayedScrollingRowIndex == 0)
            {
                txtTotal.Text = "";
                Grb_opcioCajero.Enabled = true;
                btnNuevoPosOrd.Enabled = true;
                Grb_listRePosMesa.Enabled = false;
                groupB_PosOrd.Enabled = true;
                txtFecha.Text = "";
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvPosOrd.Rows.Add(inId_pos_orden);

        }

        private void dgvPosOrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int cantidad;
            decimal precio=0, total=0;
            if (dgvPosOrd.Columns[e.ColumnIndex].Name == "columna3")
            {
                precio = decimal.Parse(dgvPosOrd.Rows[e.RowIndex].Cells[3].Value.ToString());
                cantidad = int.Parse(dgvPosOrd.Rows[e.RowIndex].Cells[2].Value.ToString());
                total = cantidad * precio;
                dgvPosOrd.Rows[e.RowIndex].Cells[4].Value = total;
            }

            double totalFinal = 0;
            foreach (DataGridViewRow row in dgvPosOrd.Rows)   //recorrera toda la tabla donde se encuentren valores 
            {
                totalFinal += Convert.ToDouble(row.Cells[4].Value); //lo que tenga la columna 2 ira sumando 
            }
            txtTotal.Text = Convert.ToString(totalFinal); //me mostrara el total de la suma en el textbox
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            dgvPosOrd.Rows.Clear();
        }


    }
}
