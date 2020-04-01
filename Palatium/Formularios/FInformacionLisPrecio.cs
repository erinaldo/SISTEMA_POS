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
    public partial class FInformacionLisPrecio : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false; //creamos la variable
        int id_lista_precio=0;
        int listBase = 0;
        int listCombo = 0;
        int listEspecial = 0;
        int listModifi = 0;
        int restrLocali = 0;
        int listMinorista = 0;
        int restriccion_localidad = 0;
        int lista_diferenciada = 0;
        int cg_estado_lista = 7546;
        int numero_replica_trigger = 0;
        int numero_control_replica=0;
        DataTable dtMoneda;
        DataTable dtcliente;

        public FInformacionLisPrecio()
        {
            InitializeComponent();
        }

        private void FInformacionLisPrecio_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
            cmbEstadoListPrecios.Text = "ACTIVO";
            cmbMoned();
            cmbClien();
            
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR COMBO moneda
        private void cmbMoned()
        {
            try
            {
                dtMoneda = new DataTable();
                string sql = "select correlativo,valor_texto from tp_codigos where tabla='SYS$00021' and estado='A'";
                cmbMoneda.llenar(dtMoneda,sql);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //LLENAR COMBO cliente
        private void cmbClien()
        {
            try
            {
                dtcliente = new DataTable();
                string sql = "SELECT correlativo,valor_texto FROM tp_codigos where tabla='SYS$01325' and estado = 'A'";
                cmbCliente.llenar(dtcliente, sql);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurriò un problema al realizar la consulta");
            }
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtBuscarListPre.Clear();
            txtNombreCrear.Clear();
            txtFecIniVaCrear.Clear();
            txtFecFinVaCrear.Clear();
            cmbEstadoListPrecios.Text = "ACTIVO";

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
                    t_st_query = "select id_lista_precio,descripcion as DESCRIPCIÓN,CO.valor_texto AS MONEDA,CLI.valor_texto AS CLIENTE,"+
                        "fecha_inicio_validez AS FECHA_INICIO,fecha_fin_validez AS FECHA_FIN,lista_base AS LISTA_BASE,lista_combo AS LISTA_COMBO,"+
                        "lista_especial AS LISTA_ESPECIAL,lista_modificable AS LISTA_MODIFIC,restriccion_localidad AS RESTRICCIÓN, "+
                        "LI.estado as ESTADO from cv403_listas_precios LI inner join tp_codigos CO on LI.cg_moneda = CO.correlativo "+
                        "inner join tp_codigos CLI on  LI.cg_tipo_cliente = CLI.correlativo";
                }

                else
                {
                    t_st_query = "select id_lista_precio,descripcion as DESCRIPCIÓN,CO.valor_texto AS MONEDA,CLI.valor_texto AS CLIENTE,"+
                        "fecha_inicio_validez AS FECHA_INICIO,fecha_fin_validez AS FECHA_FIN,lista_base AS LISTA_BASE,lista_combo AS LISTA_COMBO,"+
                        "lista_especial AS LISTA_ESPECIAL,lista_modificable AS LISTA_MODIFIC,restriccion_localidad AS RESTRICCIÓN, "+
                        "LI.estado as ESTADO from cv403_listas_precios LI inner join tp_codigos CO on LI.cg_moneda = CO.correlativo "+
                        "inner join tp_codigos CLI on  LI.cg_tipo_cliente = CLI.correlativo where descripcion LIKE '%' + '" + t_st_datos[1] + "'"+
                        " OR CO.valor_texto like '%' + '" + t_st_datos[1] + "' OR CLI.valor_texto like '%' + '" + t_st_datos[1] + "' "+
                        "OR fecha_inicio_validez like '%' + '" + t_st_datos[1] + "' OR fecha_fin_validez like '%' + '" + t_st_datos[1] + "'"+
                        " OR lista_base like '%' + '" + t_st_datos[1] + "' OR lista_combo like '%' + '" + t_st_datos[1] + "'"+
                        " OR lista_especial like '%' + '" + t_st_datos[1] + "' OR lista_modificable like '%' + '" + t_st_datos[1] + "' "+
                        "OR restriccion_localidad like '%' + '" + t_st_datos[1] + "' OR estado like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv403_listas_precios");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvListPrecios.DataSource = conexion.ds.Tables["cv403_listas_precios"];
                    dgvListPrecios.Refresh();
                    dgvListPrecios.Columns[0].Visible = false;
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

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "cv403_listas_precios");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                grpDatoListPrecios.Enabled = false;
                btnNuevoListPrecios.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                grpDatoListPrecios.Enabled = false;
                btnNuevoListPrecios.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        #endregion

        private void btnCerrarListPrecios_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarListPrecios_Click(object sender, EventArgs e)
        {
            grpDatoListPrecios.Enabled = false;
            btnNuevoListPrecios.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscarListPrecios_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarListPre.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarListPre.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                grpDatoListPrecios.Enabled = false;
                btnNuevoListPrecios.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void cmbEstadoCrear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadoListPrecios.Text.Trim().Equals("ACTIVO"))
            {
                estado = "A";
            }
            else if (cmbEstadoListPrecios.Text.Trim().Equals("ELIMINADO"))
            {
                estado = "E";
            }
        }

        private void btnNuevoListPrecios_Click(object sender, EventArgs e)
        {
            string T_st_query = "";
            string T_st_mensaje = "";

            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevoListPrecios.Text == "Nuevo")
            {
                limpiarTodo();
                grpDatoListPrecios.Enabled = true;
                btnNuevoListPrecios.Text = "Guardar";
                txtNombreCrear.Focus();
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else if (btnNuevoListPrecios.Text == "Guardar")
            {
                if (rdbLisBasCrear.Checked == true)
                    listBase = 1;
                else listBase = 0;

                if (rdbLisComCrear.Checked == true)
                    listCombo = 1;
                else listCombo = 0;

                if (rdbLisEspCrear.Checked == true)
                    listEspecial = 1;
                else listEspecial = 0;

                if (chkListModiCrear.Checked == true)
                    listModifi = 1;
                else listModifi = 0;

                if (chkRestrLocalidad.Checked == true)
                    restrLocali = 1;
                else restrLocali = 0;

                //if (rdbListMinorista.Checked == true)
                //    listMinorista = 1;
                //else listMinorista = 0;


                //if ((txtNombreCrear.Text == "") && (cmbMonedaCrear.Text == "Seleccionar item...") && (cmbTipClieCrear.Text == "Seleccionar item...") && (txtFecIniVaCrear.Text == "") && (txtFecFinVaCrear.Text == ""))
                //{
                //    MessageBox.Show("Debe rellenar todos los campos obligatorios.", "Aviso", MessageBoxButtons.OK);
                //    txtCodigoCajero.Focus();
                //}

                //else if (txtCodigoCajero.Text == "")
                //{
                //    MessageBox.Show("Favor ingrese el código del cajero.", "Aviso", MessageBoxButtons.OK);
                //    txtCodigoCajero.Focus();
                //}

                //else if (txtDescripCajero.Text == "")
                //{
                //    MessageBox.Show("Favor ingrese la descripción del cajero.", "Aviso", MessageBoxButtons.OK);
                //    txtDescripCajero.Focus();
                //}

                //else
                //{
                    string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        limpiarTodo();
                    }
                    else
                    {
                        T_st_sql = "insert into cv403_listas_precios (descripcion,cg_moneda,cg_tipo_cliente,fecha_inicio_validez,"+
                            "fecha_fin_validez,lista_base,lista_minorista,lista_combo,lista_especial,restriccion_localidad,"+
                            "lista_modificable,lista_diferenciada,cg_estado_lista,estado,numero_replica_trigger,"+
                            "numero_control_replica,fecha_ingreso,usuario_ingreso,terminal_ingreso)values"+
                            "('" + txtNombreCrear.Text.ToString().Trim() + "', " + Convert.ToInt32(cmbMoneda.SelectedValue) + ","+
                            " " + Convert.ToInt32(cmbCliente.SelectedValue)+ ", '" + Convert.ToDateTime(txtFecIniVaCrear.Text.ToString()) + "',"+
                            " '" + Convert.ToDateTime(txtFecFinVaCrear.Text.ToString()) + "', '" + listBase + "',"+
                            " '" + listMinorista + "' , '" + listCombo + "' , '" + listEspecial + "' , '" + restriccion_localidad + "', "+
                            "'" + listModifi + "','" + lista_diferenciada + "','" + cg_estado_lista + "', 'A' , " +
                            "'" + numero_replica_trigger + "','" + numero_control_replica + "' , "+
                            " GETDATE()', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";


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
                            btnNuevoListPrecios.Text = "Nuevo";
                            grpDatoListPrecios.Enabled = false;

                            limpiarTodo();
                        }
                    }
                //}
            }

            //SI EL BOTON ESTA EN OPCION ACTUALIZAR
            else if (btnNuevoListPrecios.Text == "Actualizar")
            {
                if (rdbLisBasCrear.Checked == true)
                    listBase = 1;
                else listBase = 0;

                if (rdbLisComCrear.Checked == true)
                    listCombo = 1;
                else listCombo = 0;

                if (rdbLisEspCrear.Checked == true)
                    listEspecial = 1;
                else listEspecial = 0;

                if (chkListModiCrear.Checked == true)
                    listModifi = 1;
                else listModifi = 0;

                if (chkRestrLocalidad.Checked == true)
                    restrLocali = 1;
                else restrLocali = 0;

                //if (rdbListMinorista.Checked == true)
                //    listMinorista = 1;
                //else listMinorista = 0;

                //if (txtDescripCajero.Text == "")
                //{
                //    MessageBox.Show("Favor ingrese la descripción del cajero.", "Aviso", MessageBoxButtons.OK);
                //    txtDescripCajero.Focus();
                //}

                //else
                //{
                T_st_query = "update cv403_listas_precios set descripcion = '" + txtNombreCrear.Text.Trim() + "', cg_moneda = '" + cmbMoneda.SelectedValue.ToString() + "',  cg_tipo_cliente = '" + cmbCliente.SelectedValue.ToString() + "',  fecha_inicio_validez = '" + txtFecIniVaCrear.Text.ToString().Trim() + "',  fecha_fin_validez = '" + txtFecFinVaCrear.Text.ToString().Trim() + "',  lista_base = '" + listBase + "',  lista_combo = '" + listCombo + "',  lista_especial = '" + listEspecial + "',  lista_modificable = '" + listModifi + "',  restriccion_localidad = '" + restrLocali + "' where id_lista_precio = '" + id_lista_precio + "'";   
                T_st_mensaje = "Registro Actualizado Ëxitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
                //}
            }
        }

        private void dgvListPrecios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grpDatoListPrecios.Enabled = true;
            btnNuevoListPrecios.Text = "Actualizar";

            id_lista_precio = Convert.ToInt32(dgvListPrecios.CurrentRow.Cells[0].Value.ToString());
            txtNombreCrear.Text = dgvListPrecios.CurrentRow.Cells[1].Value.ToString();
            cmbMoneda.Text = dgvListPrecios.CurrentRow.Cells[2].Value.ToString();
            cmbCliente.Text = dgvListPrecios.CurrentRow.Cells[3].Value.ToString();
            txtFecIniVaCrear.Text = dgvListPrecios.CurrentRow.Cells[4].Value.ToString();
            txtFecFinVaCrear.Text = dgvListPrecios.CurrentRow.Cells[5].Value.ToString();
            
            string lisBase = dgvListPrecios.CurrentRow.Cells[6].Value.ToString();
            if (lisBase == "True")
                rdbLisBasCrear.Checked = true;
            else rdbLisBasCrear.Checked = false;

            string lisCombo = dgvListPrecios.CurrentRow.Cells[7].Value.ToString();
            if (lisCombo == "True")
                rdbLisComCrear.Checked = true;
            else rdbLisComCrear.Checked = false;

            string lisEspecia = dgvListPrecios.CurrentRow.Cells[8].Value.ToString();
            if (lisEspecia == "True")
                rdbLisEspCrear.Checked = true;
            else rdbLisEspCrear.Checked = false;

            string lisModif = dgvListPrecios.CurrentRow.Cells[9].Value.ToString();
            if (lisModif == "True")
                chkListModiCrear.Checked = true;
            else chkListModiCrear.Checked = false;

            string restric = dgvListPrecios.CurrentRow.Cells[10].Value.ToString();
            if (restric == "True")
                chkRestrLocalidad.Checked = true;
            else chkRestrLocalidad.Checked = false;

            if (dgvListPrecios.CurrentRow.Cells[11].Value.ToString() == "A")
            {
                cmbEstadoListPrecios.Text = "ACTIVO";
            }
            else
            {
                cmbEstadoListPrecios.Text = "ELIMINADO";
            }
        }

        private void btnAnularListPrecios_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de baja el registro?", "Mensaje", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" 
                    + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();

                string T_st_query = "update cv403_listas_precios set estado = 'E', fecha_anula = GETDATE(), "+
                    "usuario_anula = '" + Program.sDatosMaximo[0] + "', terminal_anula = '" + Program.sDatosMaximo[1] + "' " +
                    "where descripcion = '" + txtNombreCrear.Text.ToString().Trim() + "'";
                string T_st_mensaje = "Registro Eliminado Exitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
            }
            else
            {
                MessageBox.Show("Se canceló la eliminacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grpDatoListPrecios.Enabled = false;
                btnNuevoListPrecios.Text = "Nuevo";
                limpiarTodo();

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }



    }
}
