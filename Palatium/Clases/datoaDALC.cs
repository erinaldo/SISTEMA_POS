using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    public class datoaDALC
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;
        

        public List<PaisEntity> listarPais()
        {
            List<PaisEntity> lista = new List<PaisEntity>();

            sSql = "select correlativo, valor_texto from tp_codigos where tabla = 'SYS$00005'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        PaisEntity todos = new PaisEntity()
                        {
                            IdPais = dtConsulta.Rows[i].ItemArray[0].ToString(),
                            SDescripcion = dtConsulta.Rows[i].ItemArray[1].ToString()
                        };
                        lista.Add(todos);
                    }
                }
            }

            return lista;
        }
    }
}
