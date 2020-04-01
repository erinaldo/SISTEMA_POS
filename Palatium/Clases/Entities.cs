using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Palatium.Clases
{
    public class PaisEntity
    {
        private string idPais;

        public string IdPais
        {
            get { return idPais; }
            set { idPais = value; }
        }

        private string sDescripcion;

        public string SDescripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }
    }
}
