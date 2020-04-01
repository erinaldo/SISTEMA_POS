using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases_Factura_Electronica
{
    class ClaseObtenerLogo
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        public string sRespuesta;

        bool bRespuesta;

        DataTable dtcConsulta;

        public bool obtenerRutaLogo()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(archivologo, '') archivologo" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtcConsulta = new DataTable();
                dtcConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtcConsulta, sSql);

                if (bRespuesta == false)
                {
                    sRespuesta = "Error en la consulta al servidor. Favor comuníquese con el administrador.";
                    return false;
                }

                sRespuesta = dtcConsulta.Rows[0][0].ToString().Trim();

                return true;
            }

            catch (Exception ex)
            {
                sRespuesta = ex.Message;
                return false;
            }
        }
    }
}
