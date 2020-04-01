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
    public partial class FModificarCodigos : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string estado = "";
        string T_st_sql = "";
        bool x = false;
        int iIdProduct;
        

        public FModificarCodigos()
        {
            InitializeComponent();
        }

        public void limpiarTodo()
        {
            txtCodigoActual.Text = "";
            txtCodigoBarras.Text = "";
            txtNombreProducto.Text = "";
            txtNuevoCodigo.Text = "";
            btnBuscarCodigo.Focus();
        }

        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaModificarCodigos ayuda1 = new Formularios.FAyudaModificarCodigos();
            ayuda1.ShowDialog();

            iIdProduct = ayuda1.idProducto;
            txtCodigoActual.Text = ayuda1.codigo;
            txtNombreProducto.Text = ayuda1.nombre;
            txtCodigoBarras.Text = ayuda1.codigoBarra;
            txtNuevoCodigo.Text = ayuda1.codigo;
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

                limpiarTodo();

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);

                limpiarTodo();
            }
        }

        private void btnGuardarCodigo_Click(object sender, EventArgs e)
        {
            if (comprobarCodigo() == false)
            {
                string T_st_query = "";
                string T_st_mensaje = "";
                if (txtCodigoActual.Text == "" && txtNombreProducto.Text == "")
                {
                    MessageBox.Show("Favor elegir un código a modificar");
                    btnBuscarCodigo.Focus();
                }
                else
                {
                    T_st_query = "update cv401_productos set codigo = '" + txtNuevoCodigo.Text.Trim() + "',codigo_barra = '" + txtCodigoBarras.Text.Trim() + "'" +
                        " where id_producto = '" + iIdProduct + "'";

                    T_st_mensaje = "Registro Actualizado Ëxitosamente";
                    actualizarRegistro(T_st_query, T_st_mensaje);
                }
            }
            else
            {
                MessageBox.Show("Ya existe un producto con el código ingresado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoActual.Text = "";
            }
                
            
        }

        //comprobar codigo
        private bool comprobarCodigo()
        {
            string sSql = "select * from cv401_productos where codigo = '"+txtNuevoCodigo.Text+"'";
            DataTable dtConsutla = new DataTable();
            dtConsutla.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsutla, sSql);
            if (bRespuesta == true)
            {
                if (dtConsutla.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            return 
                false;
        }

        private void btnCerrarCodigo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarCodigo_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        


    }
}
