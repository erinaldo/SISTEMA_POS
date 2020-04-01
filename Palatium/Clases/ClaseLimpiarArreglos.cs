using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Palatium.Clases
{
    class ClaseLimpiarArreglos
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public void limpiarArregloComentarios()
        {
            try
            {
                for (int i = 0; i < Program.iContadorDetalleMximoX; i++)
                {
                    for (int j = 0; j < Program.iContadorDetalleMximoY; j++)
                    {
                        Program.sDetallesItems[i, j] = null;
                    }
                }

                Program.iContadorDetalle = 0;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al limpiar el vector de comentarios de ítems.";
                ok.ShowDialog();
            }
        }
    }
}
