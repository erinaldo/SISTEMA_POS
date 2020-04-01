using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Inventario
{
    class ClaseReporteInventario
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Inventario.frmVistaPreviaReporteInventario vista;

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sTexto;
        string sProducto;
        string sActual;
        string sIngreso;
        string sEgreso;
        string sStock;

        public bool crearReporteInventario(DataTable dtDatos_P, string sLocalidad_P, string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                sTexto = "";
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "INVENTARIO DE PRODUCTO TERMINADO".PadLeft(36, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                if (sLocalidad_P.Length > 40)
                {
                    sTexto += sLocalidad_P.Substring(0, 40) + Environment.NewLine;
                }

                else
                {
                    sTexto += sLocalidad_P + Environment.NewLine;
                }

                sTexto += "FECHA DESDE: " + sFechaDesde_P + Environment.NewLine;
                sTexto += "FECHA HASTA: " + sFechaHasta_P + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "PRODUCTO      ACT.   ING.   EGR.   STOCK" + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                for (int i = 0; i < dtDatos_P.Rows.Count; i++)
                {
                    sProducto = dtDatos_P.Rows[i]["producto"].ToString().Trim();
                    sActual = dtDatos_P.Rows[i]["cantidad_actual"].ToString().Trim();
                    sIngreso = dtDatos_P.Rows[i]["ingresos"].ToString().Trim();
                    sEgreso = dtDatos_P.Rows[i]["egresos"].ToString().Trim();
                    sStock = dtDatos_P.Rows[i]["stock"].ToString().Trim();

                    if (sProducto.Length > 38)
                    {
                        sTexto += "* " + sProducto.Substring(0, 38) + Environment.NewLine;
                    }
                    else
                    {
                        sTexto += "* " + sProducto + Environment.NewLine;
                    }

                    sTexto += sActual.PadLeft(17, ' ') + sIngreso.PadLeft(7, ' ') + sEgreso.PadLeft(8, ' ') + sStock.PadLeft(8, ' ') + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                }

                sTexto += Environment.NewLine + Environment.NewLine + ".";

                vista = new frmVistaPreviaReporteInventario(sTexto);
                vista.ShowDialog();

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }
    }
}
