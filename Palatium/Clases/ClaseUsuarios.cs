using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    public class ClaseUsuarios
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sDescripcion { get; set; }
        public string sIdCajero { get; set; }
        public string sIdMesero { get; set; }

        public ClaseUsuarios[] usuarios;
        public Int32 iCuenta;
        
        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            string sSqlQuery;

            ClaseUsuarios objUsuario = new ClaseUsuarios();

            sSqlQuery = "";
            sSqlQuery = sSqlQuery + "select count(*) cuenta" + Environment.NewLine;
            sSqlQuery = sSqlQuery + "from pos_vw_usuario";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                usuarios = new ClaseUsuarios[iCuenta];
                if (iCuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery = sSqlQuery + "select id_pos_cajero, id_pos_mesero, descripcion" + Environment.CommandLine;
                    sSqlQuery = sSqlQuery + "from pos_vw_usuario";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objUsuario = new ClaseUsuarios();
                            objUsuario.sIdCajero = ayuda.Rows[i].ItemArray[0].ToString();
                            objUsuario.sIdMesero = ayuda.Rows[i].ItemArray[1].ToString();
                            objUsuario.sDescripcion = ayuda.Rows[i].ItemArray[2].ToString();
                            usuarios[i] = objUsuario;
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
