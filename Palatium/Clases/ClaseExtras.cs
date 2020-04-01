using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    class ClaseExtras
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdExtras { get; set; }
        public string sDescripcion { get; set; }
        public ClaseExtras[] extras;
        public Int32 iCuenta;

        string sSqlQuery;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseExtras objExtras = new ClaseExtras();

            sSqlQuery = "select count(*) from art_vw_letrainicialModificador";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                extras = new ClaseExtras[iCuenta];

                if (iCuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "SELECT UPPER(SUBSTRING(CODIGO, 1, 1)) AS LETRA, COUNT(*) CUENTA" + Environment.NewLine;
                    sSqlQuery += "FROM CV401_PRODUCTOS" + Environment.NewLine;
                    sSqlQuery += "WHERE ID_PRODUCTO_PADRE IN (" + Environment.NewLine;
                    sSqlQuery += "SELECT CAT.ID_PRODUCTO" + Environment.NewLine;
                    sSqlQuery += "FROM CV401_PRODUCTOS AS CAT" + Environment.NewLine;
                    sSqlQuery += "WHERE MODIFICADOR = 1)" + Environment.NewLine;
                    sSqlQuery += "AND ESTADO = 'A'" + Environment.NewLine;
                    sSqlQuery += "and len(codigo) = 3" + Environment.NewLine;
                    sSqlQuery += "GROUP BY UPPER(SUBSTRING(CODIGO, 1, 1))";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objExtras = new ClaseExtras();
                            objExtras.sDescripcion = ayuda.Rows[i].ItemArray[0].ToString();
                            extras[i] = objExtras;
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
