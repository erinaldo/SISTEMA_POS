using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Palatium.Clases_Crear_Comandas
{
    class ClaseObtenerIdOrigenOrden
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseLimpiarArreglos limpiarArreglos;

        string sSql;
        public string sMensajeError;

        public int iIdOrigenOrden;

        DataTable dtConsulta;

        bool bRespuesta;

        SqlParameter[] parametro;

        //CONSULTA ÀRA HABILITAR LAS OPCIONES
        public bool consultarDatos(string sCodigoOrigenOrden_P, string sAuxiliar_P)
        {
            try
            {
                Program.iBanderaConsumoVale = 0;
                Program.iIdPersonaConsumoVale = 0;
                Program.sIDPERSONA = null;
                Program.iIdPersonaFacturador = 0;
                Program.iIdentificacionFacturador = "";

                Program.iDomicilioEspeciales = 0;
                Program.iModoDelivery = 0;
                Program.iIDMESA = 0;

                Program.dbValorPorcentaje = 25;
                Program.dbDescuento = Program.dbValorPorcentaje / 100;

                limpiarArreglos = new Clases.ClaseLimpiarArreglos();
                limpiarArreglos.limpiarArregloComentarios();

                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion, genera_factura," + Environment.NewLine;
                sSql += "id_persona, id_pos_modo_delivery, presenta_opcion_delivery," + Environment.NewLine;
                sSql += "codigo, maneja_servicio" + Environment.NewLine;
                sSql += "from pos_origen_orden where codigo = '" + sCodigoOrigenOrden_P + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    sMensajeError = conexion.sMensajeError;
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    sMensajeError = "No se encuentran registros con los datos enviados.";
                    return false;
                }

                iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                Program.iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_origen_orden"].ToString());
                Program.sDescripcionOrigenOrden = dtConsulta.Rows[0]["descripcion"].ToString();
                Program.iGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0]["genera_factura"].ToString());
                Program.iManejaServicioOrden = Convert.ToInt32(dtConsulta.Rows[0][7].ToString());

                if ((dtConsulta.Rows[0]["id_persona"].ToString() == null) || (dtConsulta.Rows[0]["id_persona"].ToString() == ""))
                    Program.iIdPersonaOrigenOrden = 0;
                else
                {
                    Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                    Program.sIDPERSONA = dtConsulta.Rows[0]["id_persona"].ToString();
                }

                Program.iIdPosModoDelivery = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_modo_delivery"].ToString());
                Program.iPresentaOpcionDelivery = Convert.ToInt32(dtConsulta.Rows[0]["presenta_opcion_delivery"].ToString());
                Program.sCodigoAsignadoOrigenOrden = dtConsulta.Rows[0]["codigo"].ToString();

                //if (Program.iGeneraFactura == 0)
                //{
                //    sSql = "";
                //    sSql += "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                //    sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                //    sSql += "where codigo = '" + sAuxiliar_P + "'";

                //    dtConsulta = new DataTable();
                //    dtConsulta.Clear();

                //    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                //    if (bRespuesta == false)
                //    {
                //        sMensajeError = conexion.sMensajeError;
                //        return false;
                //    }

                //    if (dtConsulta.Rows.Count == 0)
                //    {
                //        sMensajeError = "No se encuentran registros con los datos enviados.";
                //        return false;
                //    }

                //    Program.sIdGrid = dtConsulta.Rows[0][0].ToString();
                //    Program.sFormaPagoGrid = dtConsulta.Rows[0][1].ToString();
                //}

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
