using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Palatium.Clases
{
    class ClaseComprimirArchivos
    {
        public string sMensajeError;

        public bool comprimirArchivo(string sRutaComprimir_P)
        {
            try
            {
                string sRutaFinal = sRutaComprimir_P + ".zip";

                if (File.Exists(sRutaFinal))
                {
                    File.Delete(sRutaFinal);
                }

                ZipFile.CreateFromDirectory(sRutaComprimir_P, sRutaFinal);
                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }

        public bool descomprimirArchivo(string sRutaArchivo_P, string sRutaDescomprimir_P)
        {
            try
            {
                if (File.Exists(sRutaArchivo_P))
                {
                    ZipFile.ExtractToDirectory(sRutaArchivo_P, sRutaDescomprimir_P);
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
