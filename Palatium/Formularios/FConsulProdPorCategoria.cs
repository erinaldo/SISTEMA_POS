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
    public partial class FConsulProdPorCategoria : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dtTipoEmpresa;
        DataTable dtMaximo;
        DataTable dtCategorias;
        DataTable dtProducCategoria;
        DataTable dtMaximoProduc;
        DataTable dtCategoriasCodigo;
        DataTable dtProducCategoriaCodigo;
        DataTable dtMaximoConCodigo;
        string estado = "";
        string T_st_sql = "";
        string T_st_sql2 = "";
        string T_st_sql3 = "";
        string T_st_sql4 = "";
        string T_st_sql5 = "";
        string T_st_sql6 = "";
        bool x = false; //creamos la variable
        int iMaximoNivel=0;
        string sNombre;
        string sCodigo;
        string sNombrePro;
        string sCodigoPro;
        int iNivel;
        int iIdProducto;
        int iMaximoProducto;
        int iMaximoNivelConCodigo;

        

        /*
         * VARIACION DE CODIGOS Y VARIABLES
         * AUTOR: ELVIS GUAIGUA
         * FECHA DE MODIFICACIPON: 2018-07-14
         * CONCEPTO: DEFINICIÓN DE VARIABLES PARA AJUSTARSE AL ESTANDAR ESTABLECIDO
        */

        string sSql;
        DataTable dtConsulta;
        string sTabla;
        string sCampo;
        long iMaximo;

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public FConsulProdPorCategoria()
        {
            InitializeComponent();
        }

        private void FConsulProdPorCategoria_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();
        }

        //Llenar combo tipo Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select idempresa,isnull(nombrecomercial, razonsocial) nombre_comercial, *" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            //try
            //{
            //    dtTipoEmpresa = new DataTable();
            //    string sql = "select idempresa,nombrecomercial from sis_empresa";
            //    cmbEmpresa.llenar(dtTipoEmpresa, sql);
            //    cmbEmpresa.SelectedIndex = 1;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Ocurrió un problema al realizar la consulta");
            //}
        }

       
        private void limpiar()
        {
            txtCodigo.Text = "";

            //string[] t_st_datos = { "1", "adsdasdasd" };
            //llenarGrid(t_st_datos);
        }

        //FUNCION PARA LLENAR EL GRID
        private void maximo()
        {
            dtMaximo = new DataTable();
            dtMaximo.Clear();

            T_st_sql2 = "SELECT max(PRD.NIVEL) maximo_nivel From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =0 AND PRD_PADRE.CODIGO LIKE '%' ";

            x = conexion.GFun_Lo_Busca_Registro(dtMaximo, T_st_sql2);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
            {
                //contar cuantos registros me devuelve el datatable
                if (dtMaximo.Rows.Count > 0)
                {
                    iMaximoNivel = Convert.ToInt32(dtMaximo.Rows[0].ItemArray[0].ToString());
                }
            }
        }

        private void maximoConCodigo()
        {
            dtMaximoConCodigo = new DataTable();
            dtMaximoConCodigo.Clear();

            T_st_sql2 = "SELECT max(PRD.NIVEL) maximo_nivel From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =0 AND PRD_PADRE.CODIGO LIKE '"+txtCodigo.Text.ToString().Trim()+"%' ";

            x = conexion.GFun_Lo_Busca_Registro(dtMaximoConCodigo, T_st_sql2);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
            {
                //contar cuantos registros me devuelve el datatable
                if (dtMaximoConCodigo.Rows.Count > 0)
                {
                    iMaximoNivelConCodigo = Convert.ToInt32(dtMaximoConCodigo.Rows[0].ItemArray[0].ToString());
                }
            }
        }


        //FUNCION PARA LLENAR EL GRID 
        private void llenarGrid()
        {
            dtCategorias = new DataTable();
            dtCategorias.Clear();
            int iConta2 = 0;
            int iConta4 = 0;
                T_st_sql3 = "SELECT NOM.NOMBRE Nombre,PRD.Codigo Codigo,PRD.nivel,PRD.id_producto From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =0 AND PRD.CODIGO LIKE '%' order by PRD.Codigo";

                x = conexion.GFun_Lo_Busca_Registro(dtCategorias, T_st_sql3);
                if (x == false)
                    MessageBox.Show("Error en la consulta");
                else
                    foreach (DataRow row2 in dtCategorias.Rows)
                    {
                        dgvConProPorCategoria.Rows.Add("");
                        //contar cuantos registros me devuelve el datatable
                        if (dtCategorias.Rows.Count > 0)
                        {
                            sNombre = dtCategorias.Rows[iConta2].ItemArray[0].ToString();
                            dgvConProPorCategoria.Rows[iConta4].Cells[1].Value = sNombre;

                            sCodigo = dtCategorias.Rows[iConta2].ItemArray[1].ToString();
                            dgvConProPorCategoria.Rows[iConta4].Cells[0].Value = sCodigo;

                            iNivel = Convert.ToInt32(dtCategorias.Rows[iConta2].ItemArray[2].ToString());
                            iIdProducto = Convert.ToInt32(dtCategorias.Rows[iConta2].ItemArray[3].ToString());


                            if (iNivel == iMaximoNivel)
                            {
                                int iContador3 = 0;
                                
                                    dtProducCategoria = new DataTable();
                                    dtProducCategoria.Clear();
                                    T_st_sql4 = "SELECT NOM.NOMBRE Nombre,PRD.Codigo Codigo,PRD.nivel From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =1 AND PRD.ID_PRODUCTO_PADRE =  " + iIdProducto + " order by NOM.NOMBRE ";
                                    
                                    dtMaximoProduc = new DataTable();
                                    dtMaximoProduc.Clear();

                                    T_st_sql5 = "SELECT count(PRD.Codigo) From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =1 AND PRD.ID_PRODUCTO_PADRE =  " + iIdProducto + "";

                                    x = conexion.GFun_Lo_Busca_Registro(dtMaximoProduc, T_st_sql5);
                                    if (x == false)
                                        MessageBox.Show("Error en la consulta");
                                    else
                                    {
                                        //contar cuantos registros me devuelve el datatable
                                        if (dtMaximoProduc.Rows.Count > 0)
                                        {
                                            iMaximoProducto = Convert.ToInt32(dtMaximoProduc.Rows[0].ItemArray[0].ToString());
                                        }
                                    }

                                    
                                    x = conexion.GFun_Lo_Busca_Registro(dtProducCategoria, T_st_sql4);
                                    if (x == false)
                                        MessageBox.Show("Error en la consulta");
                                    else
                                    {
                                        foreach (DataRow row3 in dtProducCategoria.Rows)
                                        {
                                            dgvConProPorCategoria.Rows.Add("");
                                            iConta4++;
                                            sNombrePro = dtProducCategoria.Rows[iContador3].ItemArray[0].ToString();
                                            dgvConProPorCategoria.Rows[iConta4].Cells[1].Value = sNombrePro;

                                            sCodigoPro = dtProducCategoria.Rows[iContador3].ItemArray[1].ToString();
                                            dgvConProPorCategoria.Rows[iConta4].Cells[0].Value = sCodigoPro;
                                            iContador3++;
                                            

                                        }
                                        //iConta4 = iConta4 + iMaximoProducto;
                                    }
                                    
                            }

                        }
                        iConta4++;
                        //MessageBox.Show(sNombre);
                        //MessageBox.Show(sNombrePro); 
                        iConta2++;
                    }
        }


        //FUNCION PARA LLENAR EL GRID cuando se haya escrito un codigo
        private void llenarGridConCodigo()
        {
            dtCategoriasCodigo = new DataTable();
            dtCategoriasCodigo.Clear();
            int iConta2 = 0;
            int iConta4 = 0;
            T_st_sql6 = "SELECT NOM.NOMBRE Nombre,PRD.Codigo Codigo,PRD.nivel,PRD.id_producto From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =0 AND PRD.CODIGO LIKE '"+txtCodigo.Text.ToString().Trim()+"%' order by PRD.Codigo";

            x = conexion.GFun_Lo_Busca_Registro(dtCategoriasCodigo, T_st_sql6);
            if (x == false)
                MessageBox.Show("Error en la consulta");
            else
                foreach (DataRow row6 in dtCategoriasCodigo.Rows)
                {
                    dgvConProPorCategoria.Rows.Add("");
                    //contar cuantos registros me devuelve el datatable
                    if (dtCategoriasCodigo.Rows.Count > 0)
                    {
                        sNombre = dtCategoriasCodigo.Rows[iConta2].ItemArray[0].ToString();
                        dgvConProPorCategoria.Rows[iConta4].Cells[1].Value = sNombre;

                        sCodigo = dtCategoriasCodigo.Rows[iConta2].ItemArray[1].ToString();
                        dgvConProPorCategoria.Rows[iConta4].Cells[0].Value = sCodigo;

                        iNivel = Convert.ToInt32(dtCategoriasCodigo.Rows[iConta2].ItemArray[2].ToString());
                        iIdProducto = Convert.ToInt32(dtCategoriasCodigo.Rows[iConta2].ItemArray[3].ToString());


                        if (iNivel == iMaximoNivelConCodigo)
                        {
                            int iContador3 = 0;

                            dtProducCategoriaCodigo = new DataTable();
                            dtProducCategoriaCodigo.Clear();
                            T_st_sql4 = "SELECT NOM.NOMBRE Nombre,PRD.Codigo Codigo,PRD.nivel From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =1 AND PRD.ID_PRODUCTO_PADRE =  " + iIdProducto + " order by NOM.NOMBRE ";

                            dtMaximoProduc = new DataTable();
                            dtMaximoProduc.Clear();

                            T_st_sql5 = "SELECT count(PRD.Codigo) From cv401_nombre_productos NOM,cv401_productos PRD,cv401_productos PRD_PADRE Where NOM.ID_PRODUCTO = PRD.ID_PRODUCTO AND PRD.ID_PRODUCTO_PADRE = PRD_PADRE.ID_PRODUCTO AND PRD.ESTADO ='A' AND NOM.ESTADO='A' AND NOM.NOMBRE_INTERNO =1 AND PRD.ULTIMO_NIVEL =1 AND PRD.ID_PRODUCTO_PADRE =  " + iIdProducto + "";

                            x = conexion.GFun_Lo_Busca_Registro(dtMaximoProduc, T_st_sql5);
                            if (x == false)
                                MessageBox.Show("Error en la consulta");
                            else
                            {
                                //contar cuantos registros me devuelve el datatable
                                if (dtMaximoProduc.Rows.Count > 0)
                                {
                                    iMaximoProducto = Convert.ToInt32(dtMaximoProduc.Rows[0].ItemArray[0].ToString());
                                }
                            }


                            x = conexion.GFun_Lo_Busca_Registro(dtProducCategoriaCodigo, T_st_sql4);
                            if (x == false)
                                MessageBox.Show("Error en la consulta");
                            else
                            {
                                foreach (DataRow row3 in dtProducCategoriaCodigo.Rows)
                                {
                                    dgvConProPorCategoria.Rows.Add("");
                                    iConta4++;
                                    sNombrePro = dtProducCategoriaCodigo.Rows[iContador3].ItemArray[0].ToString();
                                    dgvConProPorCategoria.Rows[iConta4].Cells[1].Value = sNombrePro;

                                    sCodigoPro = dtProducCategoriaCodigo.Rows[iContador3].ItemArray[1].ToString();
                                    dgvConProPorCategoria.Rows[iConta4].Cells[0].Value = sCodigoPro;
                                    iContador3++;


                                }
                                //iConta4 = iConta4 + iMaximoProducto;
                            }

                        }

                    }
                    iConta4++;
                    //MessageBox.Show(sNombre);
                    //MessageBox.Show(sNombrePro); 
                    iConta2++;
                }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnOkCategoria_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                dgvConProPorCategoria.Rows.Clear();
                maximo();
                llenarGrid();
            }
            else
            {
                dgvConProPorCategoria.Rows.Clear();
                maximoConCodigo();
                llenarGridConCodigo();
            }
        }


    }
}
