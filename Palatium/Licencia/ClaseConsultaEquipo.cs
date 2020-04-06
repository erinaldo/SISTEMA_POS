using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Palatium.Licencia
{
    class ClaseConsultaEquipo
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        public string sMensajeError = "";
        public string sIdEquipo_REC = "";
        public string sSerialEquipo_REC = "";
        public string sNombreEquipo_REC = "";
        public int iVersionDemo_REC;
        public int iCantidadPermitida_REC;
        public int iCantidadDisponible_REC;
        public int iIdPosTerminal_REC;
        public int iVistaPrevia_REC;
        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        SqlParameter[] parametro;

        public int iInsertar;

        public bool consultarEquipoLicencia()
        {
            try
            {
                iInsertar = 0;

                sSql = "";
                sSql += "select isnull(id_registro, '') id_registro, isnull(serial_registro, '') serial_registro," + Environment.NewLine;
                sSql += "demo, isnull(cantidad_permitida, 0) cantidad_permitida," + Environment.NewLine;
                sSql += "isnull(cantidad_usada, 0) cantidad_usada, id_pos_terminal, descripcion," + Environment.NewLine;
                sSql += "isnull(vista_aplicacion, 1) vista_aplicacion" + Environment.NewLine;
                sSql += "from pos_terminal" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and is_active = @is_active" + Environment.NewLine;
                sSql += "and id_registro = @id_registro";

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@is_active";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 1;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_registro";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = Program.sIdEquipo;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    sMensajeError = conexion.sMensajeError;
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iInsertar = 1;
                }

                else
                {
                    sIdEquipo_REC = dtConsulta.Rows[0]["id_registro"].ToString().Trim().ToUpper();
                    sSerialEquipo_REC = dtConsulta.Rows[0]["serial_registro"].ToString().Trim().ToUpper();
                    sNombreEquipo_REC = dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper();
                    iVersionDemo_REC = Convert.ToInt32(dtConsulta.Rows[0]["demo"].ToString());
                    iCantidadPermitida_REC = Convert.ToInt32(dtConsulta.Rows[0]["cantidad_permitida"].ToString());
                    iCantidadDisponible_REC = Convert.ToInt32(dtConsulta.Rows[0]["cantidad_usada"].ToString());
                    iIdPosTerminal_REC = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_terminal"].ToString());
                    iVistaPrevia_REC = Convert.ToInt32(dtConsulta.Rows[0]["vista_aplicacion"].ToString());
                }

                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }
    }
}
