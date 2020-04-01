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
    public partial class FInformacionCobro : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false; //creamos la variable
        int resp;
        int iId_pos_cobro;

        string sTabla;
        string sCampo;
        long iMaximo;
        DataTable dtConsulta;

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public FInformacionCobro()
        {
            InitializeComponent();
            LLenarComboOrden();
            btnAgregarCobro.Enabled = false;
            btnGuardDetaCobr.Enabled = false;
        }

        private void FInformacionCobro_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            
            cmbEstadoCobro.Text = "ACTIVO";
            

            dgvPosCobro.AllowUserToAddRows = false; //el usuario no podra agregar filas hasta dar un clic en un boton especifico

            DataGridViewTextBoxColumn columna1 = new DataGridViewTextBoxColumn(); //creo la primera columna de tipo textBox
            columna1.HeaderText = "Cobro"; //le pongo como titulo cobro
            columna1.Width = 80; //le pongo el tamaño de la celda
            columna1.Name = "columna1";
            columna1.ReadOnly = true; //me permite hacer que la celda no sea editable 
            dgvPosCobro.Columns.Add(columna1); //agrego en el datagridview la columna1

            load();
            DataGridViewComboBoxColumn comb = new DataGridViewComboBoxColumn();
            comb.HeaderText = "FORMA_COBRO";
            comb.Name = "Columna2";
            DataRow fila = dt.NewRow();
            comb.ValueMember = "id_pos_tipo_forma_cobro";
            comb.DisplayMember = "descripcion";
            comb.DataSource = dt;
            dgvPosCobro.Columns.AddRange(comb);

            DataGridViewTextBoxColumn columna3 = new DataGridViewTextBoxColumn();
            columna3.HeaderText = "Valor";
            columna3.Width = 80;
            columna3.Name = "columna3";
            dgvPosCobro.Columns.Add(columna3);
        }

        #region FUNCIONES DEL USUARIO

        //llenar el comboBox
        private void LLenarComboOrden()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_orden from pos_orden", ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);

            DataRow fila = dt.NewRow();
            cmbOrdCobro.ValueMember = "id_pos_orden";
            cmbOrdCobro.DisplayMember = "id_pos_orden";
            cmbOrdCobro.DataSource = dt;
            //cmbOrdCobro.SelectedIndex = 0;
        }

        //funcion para cargar los datos de tipo forma cobro 
        private DataTable load()
        {
            SqlDataAdapter adap = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet ds = new DataSet();
            string sql = "select id_pos_tipo_forma_cobro, descripcion  from pos_tipo_forma_cobro";
            cmd = new SqlCommand(sql,ConexionBD.ConexionBD.cnn);
            adap.SelectCommand = cmd;
            adap.Fill(ds);
            dt = ds.Tables[0];

            return dt;
        }
        
        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            
            txtFechCobro.Clear();
            cmbEstadoCobro.Text = "ACTIVO";
            txtFechCobro.Text = "";
            txtTotalOrden.Text = "";

            string[] t_st_datos = { "1", "adsdasdasd" };
            
        }

        #endregion

        private void Btn_LimpiarPosCobro_Click(object sender, EventArgs e)
        {
            Grb_DatoPosCobro.Enabled = false;
            btnNuevoPosCobro.Text = "Nuevo";
            limpiarTodo();
        }


        private void CmbEstadoCobro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoCobro.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoCobro.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void BtnNuevoPosCobro_Click(object sender, EventArgs e)
        {
            try
            {
                string T_st_query = "";
                string T_st_mensaje = "";

                //SI EL BOTON ESTA EN OPCION NUEVO
                if (btnNuevoPosCobro.Text == "Nuevo")
                {
                    limpiarTodo();
                    Grb_DatoPosCobro.Enabled = true;
                    btnNuevoPosCobro.Text = "Guardar";
                    cmbOrdCobro.Focus();
                }

                //SI EL BOTON ESTA EN OPCION GUARDAR
                else if (btnNuevoPosCobro.Text == "OK")
                {
                    dgvPosCobro.Rows.Clear();
                    btnAgregarCobro.Enabled = true;
                    btnGuardDetaCobr.Enabled = true;
                    //dgvPosCobro.Rows.Clear();
                    if (txtFechCobro.Text == "")
                    {
                        MessageBox.Show("Favor llenar la fecha de cobro");
                        txtFechCobro.Focus();
                    }

                    else
                    {
                        string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " "
                            + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                        //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                        if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                        {
                            ok.LblMensaje.Text = "Error al abrir transacción";
                            ok.ShowInTaskbar = false;
                            ok.ShowDialog();
                            goto fin;
                            //limpiarTodo();
                        }
                        else
                        {
                            T_st_sql = "insert into pos_cobro (id_pos_orden,fecha_cobro,estado,fecha_ingreso,usuario_ingreso," +
                                "terminal_ingreso)values('" + cmbOrdCobro.SelectedValue.ToString() + "', '" + txtFechCobro.Text.ToString().Trim() + "', " +
                                "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                            //sisque no me ejuta el query 
                            if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                            {
                                catchMensaje.LblMensaje.Text = T_st_sql;
                                catchMensaje.ShowInTaskbar = false;
                                catchMensaje.ShowDialog();
                                goto reversa;
                            }
                            else
                            {
                                //si no se ejecuta bien hara un commit
                                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                                Grb_DatoPosCobro.Enabled = true;
                                
                                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_COBRO
                                dtConsulta = new DataTable();
                                dtConsulta.Clear();

                                sTabla = "pos_cobro";
                                sCampo = "id_pos_cobro";

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
                                    iId_pos_cobro = Convert.ToInt32(iMaximo);
                                }


                                dgvPosCobro.Rows.Add(iId_pos_cobro);
                                Grb_DatoPosCobro.Enabled = false;
                                Grb_opcioPosCobro.Enabled = false;

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }
        
        private void comboB_OrdCobro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrdCobro.SelectedValue.ToString() != null)
            {
                string id_pos_seccion_mesa = cmbOrdCobro.SelectedValue.ToString();
                tabCon_PosCobro.Enabled = true;
            }
        }

        private void btnCerrCobro_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardDetaCobr_Click(object sender, EventArgs e)
        {
            string estado = "A";
            foreach (DataGridViewRow row in dgvPosCobro.Rows)
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " 
                    + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    MessageBox.Show("Error al abrir transacción");
                    limpiarTodo();
                }
                else
                {
                    int cobro = Convert.ToInt32(row.Cells["columna1"].Value);
                    int forma_cobro = Convert.ToInt32(row.Cells["Columna2"].Value);
                    

                    T_st_sql = "insert into pos_detalle_cobro (id_pos_cobro,id_pos_tipo_forma_cobro,estado,fecha_ingreso,usuario_ingreso,"+
                        "terminal_ingreso)values('" + cobro.ToString().Trim() + "', '" + forma_cobro.ToString().Trim() + "', 'A', "+
                        "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
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
                        btnAgregarCobro.Enabled = false;
                        btnGuardDetaCobr.Enabled = false;
                        limpiarTodo();

                    }
                }
            }

            dgvPosCobro.FirstDisplayedScrollingRowIndex = dgvPosCobro.RowCount - 1; //me sirve para dirigirme al ultimo registro del Grid
            if (dgvPosCobro.FirstDisplayedScrollingRowIndex == 0)
            {
                txtFechCobro.Text = "";
                Grb_opcioPosCobro.Enabled = true;
                btnNuevoPosCobro.Enabled = true;
                Grb_listRePosCobro.Enabled = false;
                Grb_DatoPosCobro.Enabled = true;
                txtFechCobro.Text = "";
            }
        }

        private void btnAgregarCobro_Click(object sender, EventArgs e)
        {
            dgvPosCobro.Rows.Add(iId_pos_cobro);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            dgvPosCobro.Rows.Clear();
        }



    }
}
