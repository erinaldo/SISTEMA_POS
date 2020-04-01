using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Palatium.Clases
{
    class ClaseRepartidorExterno
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdRepartidor { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }

        public string sImagen { get; set; }

        public ClaseRepartidorExterno[] repartidor;
        public Int32 iCuenta;
        string sSql;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseRepartidorExterno objRepartidor = new ClaseRepartidorExterno();

            sSql = "";
            sSql = sSql + "select count(*) cuenta" + Environment.NewLine;
            sSql = sSql + "from pos_origen_orden" + Environment.NewLine;
            sSql = sSql + "where estado = 'A'" + Environment.NewLine;
            sSql = sSql + "and repartidor_externo = 1";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSql);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0][0]);
                repartidor = new ClaseRepartidorExterno[iCuenta];

                if (iCuenta != 0)
                {
                    sSql = "";
                    sSql = sSql + "select id_pos_origen_orden, descripcion, codigo, isnull(imagen, '')" + Environment.NewLine;
                    sSql = sSql + "from pos_origen_orden" + Environment.NewLine;
                    sSql = sSql + "where estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "and repartidor_externo = 1";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSql);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objRepartidor = new ClaseRepartidorExterno();
                            objRepartidor.sIdRepartidor = ayuda.Rows[i][0].ToString();
                            objRepartidor.sDescripcion = ayuda.Rows[i][1].ToString();
                            objRepartidor.sCodigo = ayuda.Rows[i][2].ToString();
                            objRepartidor.sImagen = ayuda.Rows[i][3].ToString();
                            repartidor[i] = objRepartidor;
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
