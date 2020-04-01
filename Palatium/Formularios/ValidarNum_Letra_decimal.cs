using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms; //agregaos esta libreria 
using System.Globalization; //para validar CultureInfo

namespace Palatium.Formularios
{
    class ValidarNum_Letra_decimal
    {
        public static void soloLetras(KeyPressEventArgs v) //v es una variable cualkiera
        {
            if (char.IsLetter(v.KeyChar))
            {
                v.Handled = false;
            }
            else if(char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Solo se permite letras");
            }
        }

        public static void soloNumeros(KeyPressEventArgs v) //v es una variable cualkiera
        {
            if (char.IsDigit(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Solo se permite números");
            }
        }

        public static void soloDecimal(KeyPressEventArgs v) //v es una variable cualkiera
        {
            if (char.IsDigit(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (v.KeyChar.ToString().Equals("."))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Solo se permite números con punto decimal");
            }
        }

    }
}
