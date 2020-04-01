using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseAbrirCajon
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        string sNombreImpresora;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        DataTable dtImprimir;

        bool bRespuesta;

        //EXTRAER LOS DATOS LAS IMPRESORAS
        public void consultarImpresoraAbrirCajon()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url" + Environment.NewLine;
                sSql = sSql + "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                sSql = sSql + "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql = sSql + "and FF.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FF.id_pos_formato_factura = " + Program.iFormatoFactura;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        imprimir.AbreCajon();
                        imprimir.imprimirReporte(sNombreImpresora);
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
