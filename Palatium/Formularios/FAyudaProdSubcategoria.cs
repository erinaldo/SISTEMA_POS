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
    public partial class FAyudaProdSubcategoria : Form
    {

        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string codigo1;
        string nombre1;
        string padre1;
        string codigo2;
        string nombre2;
        string padre2;
        DataTable dtCategorias;
        DataTable dtConsulta;

        public FAyudaProdSubcategoria()
        {
            InitializeComponent();
        }

        private void FAyudaProdSubcategoria_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarComboCategorias();
            llenarGrid(t_st_datos);
        }

        //Función para llenar el combo de categorías
        private void llenarComboCategorias()
        {
            try
            {
                dtCategorias = new DataTable();
                string sSql = "select P.id_producto ,NP.nombre  from cv401_productos P,cv401_nombre_productos NP "+
                    "where P.id_producto = NP.id_producto  and P.id_producto_padre in (select id_producto from cv401_productos "+
                    "where codigo ='2') and P.nivel = 2 and P.estado ='A' and NP.estado='A' and subcategoria = 1 ";
                cmbCategorias.llenar(dtCategorias, sSql);
                cmbCategorias.SelectedIndex = 1;

            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un problema al cargar el combo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    t_st_query = "select P.id_producto,P.codigo as Codigo,NP.nombre as Nombre" +
                        " from cv401_productos P,cv401_nombre_productos NP " +
                        "where P.id_producto = NP.id_producto and id_producto_padre = '" + cmbCategorias.SelectedValue.ToString() + "' and P.nivel=3 and P.estado='A' " +
                        "and NP.estado='A'"; ;
                }

                else
                {
                    t_st_query = "select P.id_producto,P.codigo ,NP.nombre  from cv401_productos P,cv401_nombre_productos NP"+
                                 "where P.id_producto = NP.id_producto  and P.id_producto_padre in "+
                                 "(select id_producto from cv401_productos where codigo ='2') "+
                                 "and P.nivel = 2 and P.estado ='A' and NP.estado='A' and subcategoria = 1"+
                                 "and NP.nombre LIKE '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    //dgvCategoria.Columns[0].Visible = true;
                    dgvSubCategoria.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvSubCategoria.Refresh();
                    dgvSubCategoria.Columns[0].Width = 100;
                    dgvSubCategoria.Columns[1].Width = 200;

                    //NICOLE
                    dgvSubCategoria.Rows[0].Selected = true;
                    dgvSubCategoria.CurrentCell = dgvSubCategoria.Rows[0].Cells[1];

                    dgvSubCategoria.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarSubCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaSubCategoria.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaSubCategoria.Text.Trim();
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

        private void dgvSubCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            padre1 = dgvSubCategoria.CurrentRow.Cells[0].Value.ToString();
            codigo1 = dgvSubCategoria.CurrentRow.Cells[1].Value.ToString();
            nombre1 = dgvSubCategoria.CurrentRow.Cells[2].Value.ToString();
        }

        public string Padr
        {
            get { return padre2; }
        }

        public string codig //creamos un metodo 
        {
            get { return codigo2; }
        }

        public string Nombr
        {
            get { return nombre2; }
        }

        private void btnAceptarSubCategoria_Click(object sender, EventArgs e)
        {
            dgvSubCategoria.Columns[0].Visible = true;
            padre2 = padre1;
            codigo2 = codigo1;
            nombre2 = nombre1;
            this.Close();
        }

        private void btnSalirSubCategoria_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSubCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            padre1 = dgvSubCategoria.CurrentRow.Cells[0].Value.ToString();
            codigo1 = dgvSubCategoria.CurrentRow.Cells[1].Value.ToString();
            nombre1 = dgvSubCategoria.CurrentRow.Cells[2].Value.ToString();
            padre2 = padre1;
            codigo2 = codigo1;
            nombre2 = nombre1;
            this.Close();
        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategorias.SelectedValue == "0")
                {
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    dgvSubCategoria.DataSource = dtConsulta;
                }
                else
                {
                    string [] sDatos = { "1", "adasasas" };
                    llenarGrid(sDatos);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el la tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



    }
}
