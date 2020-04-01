using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    public class ClasePreciosItems
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sPreciosItems { get; set; }
        public ClasePreciosItems[] precios;
        public Int32 cuenta;
        public int iIdProducto;

        string sSqlQuery;

        DataTable dtConsulta;

        bool bRespuesta;

        public ClasePreciosItems(int sIdProducto)
        {
            this.iIdProducto = sIdProducto;
        }

        public bool llenarDatos()
        {
            ClasePreciosItems objPrecios = new ClasePreciosItems(iIdProducto);

            sSqlQuery = "";
            sSqlQuery += "select count (*) cuenta" + Environment.NewLine;
            sSqlQuery += "from cv403_precios_productos PR inner join" + Environment.NewLine;
            sSqlQuery += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
            sSqlQuery += "and PR.estado='A'" + Environment.NewLine;
            sSqlQuery += "and P.estado = 'A'" + Environment.NewLine;
            sSqlQuery += "where id_lista_precio = 4" + Environment.NewLine;
            sSqlQuery += "and P.id_producto = " + iIdProducto;

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0]);
                precios = new ClasePreciosItems[cuenta];

                if (cuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "select PR.valor" + Environment.NewLine;
                    sSqlQuery += "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSqlQuery += "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSqlQuery += "and PR.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "and P.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "where id_lista_precio = 4" + Environment.NewLine;
                    sSqlQuery += "and P.id_producto = " + iIdProducto + Environment.NewLine;                    
                    sSqlQuery += "order by secuencia";

                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objPrecios = new ClasePreciosItems(iIdProducto);
                            objPrecios.sPreciosItems = dtAyuda.Rows[i].ItemArray[0].ToString();
                            precios[i] = objPrecios;
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
    }
}
