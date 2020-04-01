using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReorden
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdOrden { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }

        public ClaseReorden[] reOrden;
        public Int32 iCuenta;

        string sSql;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseReorden objReorden = new ClaseReorden();

            sSql = "";
            sSql += "select count(*) cuenta" + Environment.NewLine;
            sSql += "from pos_secuencia_entrega" + Environment.NewLine;
            sSql += "where estado = 'A'";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSql);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                reOrden = new ClaseReorden[iCuenta];

                if (iCuenta != 0)
                {
                    sSql = "";
                    sSql += "select id_pos_secuencia_entrega, codigo, descripcion" + Environment.NewLine;
                    sSql += "from pos_secuencia_entrega" + Environment.NewLine;
                    sSql += "where estado = 'A'";
                    
                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSql);
                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objReorden = new ClaseReorden();
                            objReorden.sIdOrden = ayuda.Rows[i].ItemArray[0].ToString();
                            objReorden.sCodigo = ayuda.Rows[i].ItemArray[1].ToString();
                            objReorden.sDescripcion = ayuda.Rows[i].ItemArray[2].ToString();
                            reOrden[i] = objReorden;
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
