using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseSubcategoria
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sId { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }
        public ClaseSubcategoria[] Subcategoria;
        public Int32 cuenta;
        public string sId_padre;

        DataTable dt;

        string sSqlQuery;

        public ClaseSubcategoria(string sCodigo_padre)
        {
            this.sId_padre = sCodigo_padre;
        }

        public bool llenarDatos()
        {
            ClaseSubcategoria objSubcategoria = new ClaseSubcategoria(sId_padre);

            sSqlQuery = "";
            sSqlQuery += "select count(*) cuenta" + Environment.NewLine;
            sSqlQuery += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
            sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
            sSqlQuery += "and P.id_producto_padre = '" + sId_padre + "'" + Environment.NewLine;
            sSqlQuery += "and P.nivel = 3" + Environment.NewLine;
            sSqlQuery += "and P.estado = 'A'" + Environment.NewLine;
            sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
            sSqlQuery += "and P.is_active = 1" + Environment.NewLine;

            dt = new DataTable();
            dt.Clear();

            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                Subcategoria = new ClaseSubcategoria[cuenta];

                if (cuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "select P.id_producto,P.codigo as Codigo,NP.nombre as Nombre" + Environment.NewLine;
                    sSqlQuery += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                    sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSqlQuery += "and id_producto_padre = '" + sId_padre + "'" + Environment.NewLine;
                    sSqlQuery += "and P.nivel = 3" + Environment.NewLine;
                    sSqlQuery += "and P.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "and NP.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "and P.is_active = 1" + Environment.NewLine;
                    sSqlQuery += "order by secuencia";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);
                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objSubcategoria = new ClaseSubcategoria(sId_padre);
                            objSubcategoria.sId = ayuda.Rows[i].ItemArray[0].ToString();
                            objSubcategoria.sCodigo = ayuda.Rows[i].ItemArray[1].ToString();
                            objSubcategoria.sDescripcion = ayuda.Rows[i].ItemArray[2].ToString();
                            Subcategoria[i] = objSubcategoria;

                        }
                        return true;
                    }
                    else
                        return false;

                }
                else
                    return false;
            }
            else
                return false;


        }


        //Fin de la clase
    }
}
