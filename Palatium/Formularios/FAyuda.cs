using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FAyuda : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdentificacion { get; set; }
        public string sNombre { get; set; }
        public string sIdPersona { get; set; }

        public FAyuda()
        {
            InitializeComponent();          
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";

                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select identificacion as Identificacion, apellidos + ' ' + nombres as Persona, id_persona from tp_personas";
                }

                else if (t_st_datos[0] == "2")
                {
                    t_st_query = "select identificacion as Identificacion, apellidos + ' ' + nombres as Persona, id_persona from tp_personas where identificacion like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                else if (t_st_datos[0] == "3")
                {
                    t_st_query = "select identificacion as Identificacion, apellidos + ' ' + nombres as Persona, id_persona from tp_personas where apellidos like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                if (conexion.GFun_Lo_Rellenar_Grid(t_st_query, "tp_personas") == true)
                {
                    dgv_Datos.DataSource = conexion.ds.Tables["tp_personas"];
                    dgv_Datos.Refresh();
                    dgv_Datos.Columns[0].Width = 130;
                    dgv_Datos.Columns[1].Width = 300;

                    //NICOLE
                    dgv_Datos.Rows[0].Selected = true;
                    dgv_Datos.CurrentCell = dgv_Datos.Rows[0].Cells[1];
                }
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        #endregion

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FAyuda_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
        }

        private void TxtBusqueda_Leave(object sender, EventArgs e)
        {
            TxtBusqueda.LostFocus += new EventHandler(TxtBusqueda_LostFocus);
        }

        private void TxtBusqueda_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (TxtBusqueda.Text == "")
                {
                    string[] t_st_datos = { "1", "asdasdasd" };
                    llenarGrid(t_st_datos);
                }

                else if (Lbl_Busqueda.Text == "IDENTIFICACION")
                {
                    string[] t_st_datos = { "2", TxtBusqueda.Text.Trim() };
                    llenarGrid(t_st_datos);
                }

                else if (Lbl_Busqueda.Text == "PERSONA")
                {
                    string[] t_st_datos = { "3", TxtBusqueda.Text.Trim() };
                    llenarGrid(t_st_datos);
                }
            }

            catch (Exception)
            {

            }
        }

        private void dgv_Datos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Lbl_Busqueda.Text = "IDENTIFICACION";
            }

            else if (e.ColumnIndex == 1)
            {
                Lbl_Busqueda.Text = "PERSONA";
            }
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            FInformacionPersonas FPersona = new FInformacionPersonas();
            sIdentificacion = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
            sNombre = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            DialogResult = DialogResult.OK;
            sIdPersona = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            this.Close();
        }

        private void dgv_Datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FInformacionPersonas FPersona = new FInformacionPersonas();
            sIdentificacion = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
            sNombre = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
            sIdPersona = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
