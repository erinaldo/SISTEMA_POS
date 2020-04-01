using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    class ClaseCategorias
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string iCodigo { get; set; }
        public string iDescripcion { get; set; }
        public string sCodigo { get; set; }
        public string sSubcategoria { get; set; }
        public ClaseCategorias[] categorias;
        public  Int32 cuenta;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            string sSqlQuery;

            ClaseCategorias objCategoria = new ClaseCategorias();

            sSqlQuery = "";
            sSqlQuery = sSqlQuery + "select count(*) cuenta" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "where P.id_producto = NP.id_producto" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and id_producto_padre in" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "(select id_producto from cv401_productos where codigo ='2')" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and P.nivel = 2" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and P.estado = 'A'" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and NP.estado = 'A'" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and P.menu_pos = 1" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "and modificador = 0";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                categorias = new ClaseCategorias[cuenta];
                if (cuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery = sSqlQuery + "select P.id_producto,NP.nombre as Nombres, P.codigo, P.subcategoria" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "where P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and id_producto_padre in" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "(select id_producto from cv401_productos where codigo ='2')" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and P.nivel = 2" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and P.estado = 'A'" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and NP.estado='A'" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and modificador = 0" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "and P.menu_pos = 1" + Environment.NewLine;
                    sSqlQuery = sSqlQuery + "order by secuencia";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objCategoria = new ClaseCategorias();
                            objCategoria.iCodigo = ayuda.Rows[i].ItemArray[0].ToString();
                            objCategoria.iDescripcion = ayuda.Rows[i].ItemArray[1].ToString();
                            objCategoria.sCodigo = ayuda.Rows[i].ItemArray[2].ToString();
                            objCategoria.sSubcategoria = ayuda.Rows[i].ItemArray[3].ToString();
                            categorias[i] = objCategoria;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

    }
}
