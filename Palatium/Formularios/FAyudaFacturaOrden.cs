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

namespace InicioAplicacion.Formularios
{
    public partial class FAyudaFacturaOrden : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string sId1;
        string sApellido1;
        string sTotal1;
        string sId2;
        string sApellido2;
        string sTotal2; 

        public FAyudaFacturaOrden()
        {
            InitializeComponent();
        }

        private void FAyudaFacturaOrden_Load(object sender, EventArgs e)
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
                    t_st_query = "select td.id_pos_orden,c.apellidos,td.total from tp_personas as c join (select f.*, "+
                        "(select sum(d.precio_unidad*cantidad) from pos_detalle_orden as d "+
                        "where f.id_pos_orden=d.id_pos_detalle_orden) as total from pos_orden as f) as td "+
                        "on td.id_persona=c.id_persona";
                }

                else
                {
                    t_st_query = "select  id_lista_precio, descripcion, cg_moneda, fecha_inicio_validez, fecha_fin_validez,"+
                        " lista_base, lista_modificable, estado from cv403_listas_precios where id_lista_precio LIKE '%' + "+
                        "'" + t_st_datos[1] + "' OR descripcion like '%' + '" + t_st_datos[1] + "' OR cg_moneda like '%' + "+
                        "'" + t_st_datos[1] + "' OR fecha_inicio_validez like '%' + '" + t_st_datos[1] + "' "+
                        "OR fecha_fin_validez like '%' + '" + t_st_datos[1] + "' OR lista_base like '%' + '" + t_st_datos[1] + "' "+
                        " OR lista_modificable like '%' + '" + t_st_datos[1] + "' OR estado like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv403_listas_precios");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvFactOrd.DataSource = conexion.ds.Tables["cv403_listas_precios"];
                    dgvFactOrd.Refresh();

                    //NICOLE
                    dgvFactOrd.Rows[0].Selected = true;
                    dgvFactOrd.CurrentCell = dgvFactOrd.Rows[0].Cells[1];

                    dgvFactOrd.Columns[0].Width = 100;
                    dgvFactOrd.Columns[1].Width = 200;
                    dgvFactOrd.Columns[2].Width = 100;
                    dgvFactOrd.Columns[3].Width = 100;
                    dgvFactOrd.Columns[4].Width = 100;
                    dgvFactOrd.Columns[5].Width = 100;
                    dgvFactOrd.Columns[6].Width = 100;
                    dgvFactOrd.Columns[7].Width = 100;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarFactOrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaFactOrd.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaFactOrd.Text.Trim();
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

        private void dgvFactOrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sId1 = dgvFactOrd.CurrentRow.Cells[0].Value.ToString();
            sApellido1 = dgvFactOrd.CurrentRow.Cells[1].Value.ToString();
            sTotal1 = dgvFactOrd.CurrentRow.Cells[2].Value.ToString();
        }

        public string Id //creamos un metodo 
        {
            get { return sId1; }
        }

        public string Descripcion
        {
            get { return sApellido2; }
        }

        public string Moneda
        {
            get { return sTotal2; }
        }

        private void btnAceptarFactOrd_Click(object sender, EventArgs e)
        {
            sId2 = sId1;
            sApellido2 = sApellido1;
            sTotal2 = sTotal1;

            this.Close();
        }

        private void btnSalirFactOrd_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
