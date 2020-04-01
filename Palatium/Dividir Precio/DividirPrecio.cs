using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class DividirPrecio : Form
    {
        int iNumeroPersonasPrecioDividido;
        string sSaldo;
        double dPrecioDividido;
        Label[] label = new Label[50];

        public DividirPrecio(int iNumeroPersonasPrecioDividido, string sSaldo)
        {
            this.iNumeroPersonasPrecioDividido = iNumeroPersonasPrecioDividido;
            this.sSaldo = sSaldo;
            dPrecioDividido = Math.Round((Convert.ToDouble(sSaldo) / this.iNumeroPersonasPrecioDividido),2);
            InitializeComponent();
            crearArregloLabel();
        }

        //Función para crear el arreglo de label
        private void crearArregloLabel()
        {
            double suma = 0;
            for (int i = 0; i < iNumeroPersonasPrecioDividido; i++)
            {
                label[i] = new Label();
                label[i].Width = 300;
                label[i].Height = 30;
                label[i].Top = i * 30;
                if (i == (iNumeroPersonasPrecioDividido - 1))
                {
                    label[i].Text = "Persona " + (i + 1) + ": $" + (Convert.ToDouble(sSaldo)- suma).ToString("N2");
                }
                else
                {
                    label[i].Text = "Persona " + (i + 1) + ": $" + dPrecioDividido.ToString("N2");
                    suma += dPrecioDividido;
                }
                
                label[i].Font = new Font("Comic Sans MS", 15);

                panel1.Controls.Add(label[i]);
            }
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
