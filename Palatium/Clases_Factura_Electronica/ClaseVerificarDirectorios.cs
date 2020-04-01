using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases_Factura_Electronica
{
    class ClaseVerificarDirectorios
    {
        public bool crearDirectorio(string sPath_P)
        {
            try
            {
                if (!Directory.Exists(sPath_P))
                {
                    DirectoryInfo carpeta = Directory.CreateDirectory(sPath_P);
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}
