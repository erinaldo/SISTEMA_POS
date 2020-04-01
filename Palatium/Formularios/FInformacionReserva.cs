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
    public partial class FInformacionReserva : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false; //creamos la variable
        int iIdPersona;
        int iIdPosReserva;

        public FInformacionReserva()
        {
            InitializeComponent();
        }

        private void FInformacionReserva_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
            cmbEstadoReserva.Text = "ACTIVO";
            Grb_DatoReserva.Enabled = false;
            LLenarComboJornada();
            LLenarComboLocali();

        }

        #region FUNCIONES DEL USUARIO

       //llenar el comboBox de jornada
        private void LLenarComboJornada()
        {

            SqlCommand cmd = new SqlCommand("select id_pos_jornada,descripcion from pos_jornada where estado = 'A'",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);

            

            DataRow fila = dt.NewRow();
            fila["descripcion"] = "Seleccionar item...";
            dt.Rows.InsertAt(fila, 0);

            cmbJornaReserva.ValueMember = "id_pos_jornada";
            cmbJornaReserva.DisplayMember = "descripcion";
            cmbJornaReserva.DataSource = dt;
        }

        //llenar el comboBox de Localidad
        private void LLenarComboLocali()
        {

            SqlCommand cmd = new SqlCommand("select id_localidad,direccion from tp_localidades where estado = 'A'",ConexionBD.ConexionBD.cnn);
            SqlDataAdapter adaptad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptad.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["direccion"] = "Seleccionar item...";
            dt.Rows.InsertAt(fila, 0);

            cmbLocaliReserva.ValueMember = "id_localidad";
            cmbLocaliReserva.DisplayMember = "direccion";
            cmbLocaliReserva.DataSource = dt;
        }

       
        ////LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtBuscarReserva.Clear();
            txtHoraReserva.Clear();
            txtNumPersReserva.Clear();
            txtDescripReserva.Clear();
            txtFechaReserva.Clear();
            cmbJornaReserva.Text = "Seleccionar item...";
            cmbLocaliReserva.Text = "Seleccionar item...";
            cmbEstadoReserva.Text = "ACTIVO";
            Txt_Buscar.Clear();
            Txt_Informacion.Clear();

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
                    t_st_query = "select PE.apellidos as CLIENTE, LO.direccion as LOCALIDAD, JO.descripcion as JORNADA, RE.fecha_hora as FECHA_HORA, "+
                        "RE.descripcion as DESCRIPCIÓN, RE.numero_pax as NUM_PERSONAS, RE.estado as ESTADO, PE.id_persona, PE.identificacion, RE.id_pos_reserva from tp_personas PE " +
                        "inner join pos_reserva RE on RE.id_persona=PE.id_persona  inner join tp_localidades LO on RE.id_localidad=LO.id_localidad "+
                        "inner join pos_jornada JO on RE.id_pos_jornada=JO.id_pos_jornada where RE.estado = 'A'";
                }

                else
                {
                    t_st_query = "select codigo as CODIGO, descripcion AS DESCRIPCION, estado as ESTADO, PE.id_persona, PE.identificacion, RE.id_pos_reserva from pos_cajero " +
                        "where codigo LIKE '%' + '" + t_st_datos[1] + "' OR descripcion like '%' + '" + t_st_datos[1] + "' + '%' and RE.estado = 'A'";
                }
                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "pos_reserva");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvReserva.DataSource = conexion.ds.Tables["pos_reserva"];
                    dgvReserva.Refresh();
                    dgvReserva.Columns[0].Width = 150;
                    dgvReserva.Columns[1].Width = 150;
                    dgvReserva.Columns[2].Width = 100;
                    dgvReserva.Columns[3].Width = 110;
                    dgvReserva.Columns[4].Width = 120;
                    dgvReserva.Columns[5].Width = 100;
                    dgvReserva.Columns[7].Visible = false;
                    dgvReserva.Columns[8].Visible = false;
                    dgvReserva.Columns[9].Visible = false; 
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
            
            try
            {

                string T_st_query = "insert into pos_reserva (id_persona,id_localidad,id_pos_jornada,fecha_hora,descripcion,"+
                    "numero_pax,estado,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + iIdPersona+ "',"+
                    "'" + cmbLocaliReserva.SelectedIndex.ToString()+ "','" + cmbJornaReserva.SelectedIndex.ToString() + "',"+
                    "'" + txtFechaReserva.Text.ToString().Trim()+ "','" + txtDescripReserva.Text.ToString().Trim() + "', "+
                    "'" + txtNumPersReserva.Text.ToString().Trim() + "','A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," +
                    " '" + Program.sDatosMaximo[1] + "')";

                x = conexion.GFun_Lo_Rellenar_Grid(T_st_query, "pos_reserva");
                if (x == true)
                {
                    MessageBox.Show("Registro insertado correctamente");
                }
                else
                {
                    MessageBox.Show("Error al insertar el registro");
                }

                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }

            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
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
                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "pos_reserva");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        #endregion

        private void Txt_Codigo_LostFocus(object sender, EventArgs e)
        {
            dt.Clear();
            bool x = false;
            string t_st_sql = "select descripcion, estado, fecha_hora, numero_pax from pos_reserva where descripcion = '" + txtDescripReserva.Text + "'";
            x = conexion.GFun_Lo_Busca_Registro(dt,t_st_sql);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
            {
                if (dt.Rows.Count > 0) //contar cuantos registros me devuelve el datatable
                {
                    txtDescripReserva.Text = dt.Rows[0].ItemArray[0].ToString();
                    if (dt.Rows[0].ItemArray[1].ToString() == "A")
                    {
                        cmbEstadoReserva.Text = "ACTIVO";
                    }
                    else
                    {
                        cmbEstadoReserva.Text = "ELIMINADO";
                    }

                    btnAnularReserva.Enabled = true;
                    btnNuevoReserva.Text = "Actualizar";
                    txtBuscarReserva.Focus();

                }
                else
                {
                    txtDescripReserva.Focus();
                    btnNuevoReserva.Text = "Guardar";
                    btnAnularReserva.Enabled = false;
                }
            
            }
        }

        private void Txt_DescripReserva_Leave(object sender, EventArgs e)
        {
            txtDescripReserva.LostFocus += new EventHandler(Txt_Codigo_LostFocus);
        }

        private void Btn_CerrarReserva_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarReserva_Click(object sender, EventArgs e)
        {
            Grb_DatoReserva.Enabled = false;
            btnNuevoReserva.Text = "Nuevo";

            limpiarTodo();
        }

        private void Btn_BuscarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarReserva.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarReserva.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void CmbEstadoReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoReserva.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoReserva.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void BtnNuevoReserva_Click(object sender, EventArgs e)
        {
            string T_st_query = "";
            string T_st_mensaje = "";

            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoReserva.Text == "Nuevo")
            {
                limpiarTodo();
                Grb_DatoReserva.Enabled = true;
                btnNuevoReserva.Text = "Guardar";
                cmbJornaReserva.Text = "Seleccionar item...";
                cmbLocaliReserva.Text = "Seleccionar item...";
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else if (btnNuevoReserva.Text == "Guardar")
            {

                if ((txtFechaReserva.Text == "") && (txtHoraReserva.Text == "") && (txtNumPersReserva.Text == "") && (txtDescripReserva.Text == "") && (cmbJornaReserva.Text == "Seleccionar item...") && (cmbLocaliReserva.Text == "Seleccionar item...") )
                {
                    MessageBox.Show("Debe rellenar todos los campos obligatorios.", "Aviso", MessageBoxButtons.OK);
                    txtFechaReserva.Focus();
                }

                else if (txtFechaReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la fecha de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtFechaReserva.Focus();
                }

                else if (txtHoraReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la hora de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtHoraReserva.Focus();
                }
                else if (txtNumPersReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese el numero de personas.", "Aviso", MessageBoxButtons.OK);
                    txtNumPersReserva.Focus();
                }
                else if (txtDescripReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la descripción de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtDescripReserva.Focus();
                }
                else if (cmbJornaReserva.Text == "Seleccionar item...")
                {
                    MessageBox.Show("Favor selecione una opcion de la jornada.", "Aviso", MessageBoxButtons.OK);
                    cmbJornaReserva.Focus();
                }
                else if (cmbLocaliReserva.Text == "Seleccionar item...")
                {
                    MessageBox.Show("Favor selecione una opcion de la localidad.", "Aviso", MessageBoxButtons.OK);
                    cmbJornaReserva.Focus();
                }
                else
                {
                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        limpiarTodo();
                    }
                    else
                    {
                        T_st_sql = "insert into pos_reserva (id_persona,id_localidad,id_pos_jornada,fecha_hora,descripcion,numero_pax,"+
                            "estado,fecha_ingreso,usuario_ingreso,terminal_ingreso)values('" + iIdPersona+ "',"+
                            "'" + cmbLocaliReserva.SelectedValue.ToString() + "','" + cmbJornaReserva.SelectedValue.ToString() + "',"+
                            "'" + txtFechaReserva.Text.ToString().Trim() + "','" + txtDescripReserva.Text.ToString().Trim() + "', "+
                            "'" + txtNumPersReserva.Text.ToString().Trim() + "','A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," +
                            " '" + Program.sDatosMaximo[1] + "')";

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
                            btnNuevoReserva.Text = "Nuevo";
                            Grb_DatoReserva.Enabled = false;
                            limpiarTodo();
                        }
                    }
                }
            }

            //SI EL BOTON ESTA EN OPCION ACTUALIZAR
            else if (btnNuevoReserva.Text == "Actualizar")
            {
                if ((txtFechaReserva.Text == "") && (txtHoraReserva.Text == "") && (txtNumPersReserva.Text == "") && (txtDescripReserva.Text == "") && (cmbJornaReserva.Text == "Seleccionar item...") && (cmbLocaliReserva.Text == "Seleccionar item...") )
                {
                    MessageBox.Show("Debe rellenar todos los campos obligatorios.", "Aviso", MessageBoxButtons.OK);
                    txtFechaReserva.Focus();
                }

                else if (txtFechaReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la fecha de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtFechaReserva.Focus();
                }

                else if (txtHoraReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la hora de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtHoraReserva.Focus();
                }
                else if (txtNumPersReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese el numero de personas.", "Aviso", MessageBoxButtons.OK);
                    txtNumPersReserva.Focus();
                }
                else if (txtDescripReserva.Text == "")
                {
                    MessageBox.Show("Favor ingrese la descripción de la reserva.", "Aviso", MessageBoxButtons.OK);
                    txtDescripReserva.Focus();
                }
                else if (cmbJornaReserva.Text == "Seleccionar item...")
                {
                    MessageBox.Show("Favor selecione una opcion de la jornada.", "Aviso", MessageBoxButtons.OK);
                    cmbJornaReserva.Focus();
                }
                else if (cmbLocaliReserva.Text == "Seleccionar item...")
                {
                    MessageBox.Show("Favor selecione una opcion de la localidad.", "Aviso", MessageBoxButtons.OK);
                    cmbJornaReserva.Focus();
                }
                else
                {
                    T_st_query = "update pos_reserva set id_persona = '" + iIdPersona + "', "+
                        "id_localidad = '" + cmbLocaliReserva.SelectedValue.ToString() + "', id_pos_jornada = '" + cmbJornaReserva.SelectedValue.ToString() + "', "+
                        "fecha_hora = '" + txtFechaReserva.Text.ToString().Trim() + "', descripcion = '" + txtDescripReserva.Text.ToString().Trim() + "', "+
                        "numero_pax = '" + txtNumPersReserva.Text.ToString().Trim() + "' where id_pos_reserva = "+iIdPosReserva;

                    T_st_mensaje = "Registro Actualizado Ëxitosamente";
                    actualizarRegistro(T_st_query, T_st_mensaje);
                }
            }
        }

        private void Btn_AnularReserva_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de bajar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string T_st_query = "update pos_reserva set estado = 'E', fecha_anula = GETDATE(), usuario_anula = '" + Program.sDatosMaximo[0] + "', " +
                    "terminal_anula = '" + Program.sDatosMaximo[1] + "' where id_pos_reserva = " + iIdPosReserva;

                string T_st_mensaje = "Registro Eliminado Ëxitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
            }
            else
            {
                MessageBox.Show("Se canceló la eliminacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Grb_DatoReserva.Enabled = false;
                btnNuevoReserva.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void Dgv_Reserva_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Grb_DatoReserva.Enabled = true;
            btnNuevoReserva.Text = "Actualizar";

            txtFechaReserva.Text = dgvReserva.CurrentRow.Cells[3].Value.ToString();
            cmbJornaReserva.Text = dgvReserva.CurrentRow.Cells[2].Value.ToString();
            cmbLocaliReserva.Text = dgvReserva.CurrentRow.Cells[1].Value.ToString();
            txtNumPersReserva.Text = dgvReserva.CurrentRow.Cells[5].Value.ToString();
            txtDescripReserva.Text = dgvReserva.CurrentRow.Cells[4].Value.ToString();
            iIdPersona = Convert.ToInt32(dgvReserva.CurrentRow.Cells[7].Value.ToString());
            Txt_Informacion.Text = dgvReserva.CurrentRow.Cells[0].Value.ToString();
            Txt_Buscar.Text = dgvReserva.CurrentRow.Cells[8].Value.ToString();
            iIdPosReserva = Convert.ToInt32(dgvReserva.CurrentRow.Cells[9].Value.ToString());

            if (dgvReserva.CurrentRow.Cells[6].Value.ToString() == "A")
            {
                cmbEstadoReserva.Text = "ACTIVO";
            }
            else
            {
                cmbEstadoReserva.Text = "ELIMINADO";
            }
        }

        private void txtNumPersReserva_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void Btn_Abrir_Grid_Click(object sender, EventArgs e)
        {
            Formularios.FAyuda ayuda = new FAyuda();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                Txt_Buscar.Text = ayuda.sIdentificacion;
                Txt_Informacion.Text = ayuda.sNombre;
                iIdPersona = Convert.ToInt32(ayuda.sIdPersona);
            }
        }


        
    }
}
