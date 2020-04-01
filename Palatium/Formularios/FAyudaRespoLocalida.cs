using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FAyudaRespoLocalida : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string id1;
        string identifi1;
        string nombre1;
        string apellido1;
        string correo1;
        string id2;
        string identifi2;
        string nombre2;
        string apellido2;
        string correo2;

        public FAyudaRespoLocalida()
        {
            InitializeComponent();
        }

        private void FAyudaRespoLocalida_Load(object sender, EventArgs e)
        {
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
                    t_st_query = "select id_persona,identificacion,nombres,apellidos,correo_electronico from tp_personas";
                }

                else
                {
                    t_st_query = "select id_persona,identificacion,nombres,apellidos,correo_electronico from tp_personas "+
                        "where id_persona LIKE '%' + '" + t_st_datos[1] + "' OR identificacion like '%' + '" + t_st_datos[1] + "' "+
                        "OR nombres like '%' + '" + t_st_datos[1] + "' OR apellidos like '%' + '" + t_st_datos[1] + "' "+
                        "OR correo_electronico like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "tp_personas");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvRespon.DataSource = conexion.ds.Tables["tp_personas"];
                    dgvRespon.Refresh();

                    dgvRespon.Columns[0].Width = 100;
                    dgvRespon.Columns[1].Width = 200;
                    dgvRespon.Columns[2].Width = 100;
                    dgvRespon.Columns[3].Width = 100;
                    dgvRespon.Columns[4].Width = 100;

                    dgvRespon.Rows[0].Selected = true;
                    dgvRespon.CurrentCell = dgvRespon.Rows[0].Cells[1];

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarRespon_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaRespon.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaRespon.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void dgvRespon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id1 = dgvRespon.CurrentRow.Cells[0].Value.ToString();
            identifi1 = dgvRespon.CurrentRow.Cells[1].Value.ToString();
            //nombre1 = dgvRespon.CurrentRow.Cells[2].Value.ToString();
            apellido1 = dgvRespon.CurrentRow.Cells[3].Value.ToString();
            //correo1 = dgvRespon.CurrentRow.Cells[4].Value.ToString();
        }

        public string IdResponsable //creamos un metodo 
        {
            get { return id2; }
        }

        public string identiResponsable
        {
            get { return identifi2; }
        }

        //public string nombreResponsable //creamos un metodo 
        //{
        //    get { return nombre2; }
        //}

        public string apelliResponsable
        {
            get { return apellido2; }
        }


        private void btnAceptarRespon_Click(object sender, EventArgs e)
        {
            id2 = id1;
            identifi2 = identifi1;
            apellido2 = apellido1;
            this.Close();
        }

        private void btnSalirRespon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvRespon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id1 = dgvRespon.CurrentRow.Cells[0].Value.ToString();
            identifi1 = dgvRespon.CurrentRow.Cells[1].Value.ToString();
            //nombre1 = dgvRespon.CurrentRow.Cells[2].Value.ToString();
            apellido1 = dgvRespon.CurrentRow.Cells[3].Value.ToString();
            //correo1 = dgvRespon.CurrentRow.Cells[4].Value.ToString();
            id2 = id1;
            identifi2 = identifi1;
            apellido2 = apellido1;
            this.Close();
        }



    }
}
