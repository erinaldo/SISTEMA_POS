using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Formularios
{
    public partial class FAyudaReciboCobros : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        int iIdPago;
        int iNumeroPago;
        string sNombre;
        string sComentario;
        string sFechaPago;
        string sLocalidad;
        string sSerie;
        string sNombreLocalidad;

        int iIdPago2;
        int iNumeroPago2;
        string sNombre2;
        string sComentario2;
        string sFechaPago2;
        string sLocalidad2;
        string sSerie2;
        string sNombreLocalidad2;

        public FAyudaReciboCobros()
        {
            InitializeComponent();
        }

        private void FAyudaReciboCobros_Load(object sender, EventArgs e)
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
                    t_st_query = "select PAG.id_pago, NUM.serie as Serie, NUM.numero_pago as Numero, PER.apellidos as Cliente, PAG.fecha_pago as Fecha_Pago, LOC.nombre_localidad, PAG.comentarios as Comentarios from cv403_pagos PAG inner join  cv403_numeros_pagos NUM  on PAG.id_pago = NUM.id_pago inner join tp_personas PER on PER.id_persona = PAG.id_persona inner join tp_vw_localidades LOC on LOC.id_localidad = PAG.id_localidad order by NUM.numero_pago desc";
                }

                else
                {
                    t_st_query = "select PAG.id_pago, NUM.serie as Serie, NUM.numero_pago as Numero, PER.apellidos as Cliente, PAG.fecha_pago as Fecha_Pago, LOC.nombre_localidad, PAG.comentarios as Comentarios from cv403_pagos PAG inner join  cv403_numeros_pagos NUM  on PAG.id_pago = NUM.id_pago inner join tp_personas PER on PER.id_persona = PAG.id_persona inner join tp_vw_localidades LOC on LOC.id_localidad = PAG.id_localidad order by NUM.numero_pago desc where NUM.numero_pago LIKE '%' + '" + t_st_datos[1] + "' and PER.apellidos LIKE '%' + '" + t_st_datos[1] + "'+ '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv403_pagos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvRecibo.DataSource = conexion.ds.Tables["cv403_pagos"];
                    dgvRecibo.Refresh();

                    dgvRecibo.Rows[0].Selected = true;
                    dgvRecibo.CurrentCell = dgvRecibo.Rows[0].Cells[1];
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }

        private void btnBuscarRecibo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarRecibo.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscarRecibo.Text.Trim();
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

        private void dgvRecibo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdPago = Convert.ToInt32(dgvRecibo.CurrentRow.Cells[0].Value.ToString());
            sSerie = dgvRecibo.CurrentRow.Cells[1].Value.ToString();
            iNumeroPago = Convert.ToInt32(dgvRecibo.CurrentRow.Cells[2].Value.ToString());
            sNombre = dgvRecibo.CurrentRow.Cells[3].Value.ToString();
            sFechaPago = dgvRecibo.CurrentRow.Cells[4].Value.ToString();
            sNombreLocalidad = dgvRecibo.CurrentRow.Cells[5].Value.ToString();
            sComentario = dgvRecibo.CurrentRow.Cells[6].Value.ToString();
        }

        public int IdPago //creamos un metodo 
        {
            get { return iIdPago2; }
        }

        public string Serie
        {
            get { return sSerie2; }
        }

        public int NumeroPago
        {
            get { return iNumeroPago2; }
        }

        public string Nombre //creamos un metodo 
        {
            get { return sNombre2; }
        }

        public string FechaPago
        {
            get { return sFechaPago2; }
        }

        public string Localidad
        {
            get { return sNombreLocalidad2; }
        }

        public string Comentario
        {
            get { return sComentario2; }
        }
    

        private void btnAceptarRecibo_Click(object sender, EventArgs e)
        {
            iIdPago2 = iIdPago;
            sSerie2 = sSerie;
            iNumeroPago2 = iNumeroPago;
            sNombre2 = sNombre;
            sFechaPago2 = sFechaPago;
            sNombreLocalidad2 = sNombreLocalidad;
            sComentario2 = sComentario;
            //sNombre2 = sNombre1;
            this.Close();
        }

        private void btnSalirRecibo_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
