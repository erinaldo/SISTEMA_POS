using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Palatium.Clases
{
    class ClaseValidarCaracteres
    {
        //INSTRUCCION SOLO PARA VALIDAR NUMEROS
        public void soloNumeros(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void soloDecimales(object sender, KeyPressEventArgs e, int iDecimal)
        {
            TextBox txtValor = sender as TextBox;

            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            if (e.KeyChar == 127)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtValor.Text.Trim().Length; i++)
            {
                if (txtValor.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= iDecimal)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                e.Handled = false;
            }

            else if (e.KeyChar == 46)
            {
                e.Handled = (IsDec) ? true : false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool validarCorreoElectronico(string correoValidar)
        {
            try
            {
                String sFormato;

                sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correoValidar, sFormato))
                {
                    if (Regex.Replace(correoValidar, sFormato, String.Empty).Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }
    }
}
