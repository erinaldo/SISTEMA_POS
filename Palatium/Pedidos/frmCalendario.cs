using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmCalendario : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtConsulta;

        string sFecha = "";
        string sDia = "";
        string sMes = "";
        string sAnio = "";
        string sSql;

        Int32 iDiaLimite = 0;

        public frmCalendario(string sFechaRecibida)
        {
            InitializeComponent();
            txtFecha.Text = sFechaRecibida;
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el Combo de Jornada
        private void llenarComboJornada()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_jornada, descripcion" + Environment.NewLine;
                sSql += "from pos_jornada" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbJornada.llenar(dtConsulta, sSql);

                if (cmbJornada.Items.Count == 0)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.\n\nComunìquese con el administrador en caso de presentar el mismo inconveniente.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
                else
                {
                    cmbJornada.SelectedIndex = 1;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //SEPARACION DE DATOS
        private void separarFecha()
        {
            sFecha = txtFecha.Text;
            sDia = sFecha.Substring(0, 2);
            sMes = sFecha.Substring(3, 2);
            sAnio = sFecha.Substring(6, 4);
        }

        //CASO DEL DIA
        private void CasoDia()
        {
            if ((sMes == "01") || (sMes == "03") || (sMes == "05") || (sMes == "07") || (sMes == "08") || (sMes == "10") || (sMes == "12"))
            {
                iDiaLimite = 31;
            }

            else if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 == 0))
            {
                iDiaLimite = 29;
            }

            else if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 != 0))
            {
                iDiaLimite = 28;
            }

            else if ((sMes == "04") || (sMes == "06") || (sMes == "09") || (sMes == "11"))
            {
                iDiaLimite = 30;
            }
        }



        #endregion
        
        private void btnSubirDia_Click(object sender, EventArgs e)
        {
            separarFecha();
            CasoDia();

            if (Convert.ToInt32(sDia) > iDiaLimite - 1)
            {
                sDia = "01";
            }

            else
            {
                sDia = (Convert.ToInt32(sDia) + 1).ToString();
            }

            if (sDia.Length == 1)
            {
                sDia = "0" + sDia;
            }

            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;

        }

        private void btnSubirAnio_Click(object sender, EventArgs e)
        {
            separarFecha();
            sAnio = (Convert.ToInt32(sAnio) + 1).ToString();
            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;
        }

        private void btnBajarAnio_Click(object sender, EventArgs e)
        {
            separarFecha();
            sAnio = (Convert.ToInt32(sAnio) - 1).ToString();
            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;
        }

        private void btnSubirMes_Click(object sender, EventArgs e)
        {
            separarFecha();

            if (Convert.ToInt32(sMes) > 11)
            {
                sMes = "01";
            }
            else if (Convert.ToInt32(sMes) < 1)
            {
                sMes = "12";
            }
            else
            {
                sMes = (Convert.ToInt32(sMes) + 1).ToString();
            }     

            if (sMes.Length == 1)
            {
                sMes = "0" + sMes;
            }

            CasoDia();
            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;

            separarFecha();

            if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 == 0) && (Convert.ToInt32(sDia) > iDiaLimite))
            {
                sDia = iDiaLimite.ToString();
            }

            else if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 != 0) && (Convert.ToInt32(sDia) > iDiaLimite))
            {
                sDia = iDiaLimite.ToString();
            }

            if (sMes.Length == 1)
            {
                sMes = "0" + sMes;
            }

            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;
        }

        private void btnBajarMes_Click(object sender, EventArgs e)
        {
            separarFecha();

            if (Convert.ToInt32(sMes) > 12)
            {
                sMes = "01";
            }
            else if (Convert.ToInt32(sMes) < 2)
            {
                sMes = "12";
            }
            else
            {
                sMes = (Convert.ToInt32(sMes) - 1).ToString();
            }

            if (sMes.Length == 1)
            {
                sMes = "0" +  sMes;
            }

            CasoDia();
            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;

            separarFecha();

            if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 == 0) && (Convert.ToInt32(sDia) > iDiaLimite))
            {
                sDia = iDiaLimite.ToString();
            }

            else if ((sMes == "02") && (Convert.ToInt32(sAnio) % 4 != 0) && (Convert.ToInt32(sDia) > iDiaLimite))
            {
                sDia = iDiaLimite.ToString();
            }

            if (sMes.Length == 1)
            {
                sMes = "0" + sMes;
            }

            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBajarDia_Click(object sender, EventArgs e)
        {
            separarFecha();
            CasoDia();

            if (Convert.ToInt32(sDia) < 2)
            {
                sDia = iDiaLimite.ToString(); ;
            }

            else
            {
                sDia = (Convert.ToInt32(sDia) - 1).ToString();
            }

            if (sDia.Length == 1)
            {
                sDia = "0" + sDia;
            }

            txtFecha.Text = sDia + "/" + sMes + "/" + sAnio;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (Program.iMostrarJornada == 1)
            {
                if (Convert.ToInt32(cmbJornada.SelectedValue) == 0)
                {
                    ok.LblMensaje.Text = "Favor seleccione la jornada.";
                    ok.ShowDialog();
                }

                else
                {
                    Program.iMostrarJornada = 0;
                    Program.iJornadaRecuperada = Convert.ToInt32(cmbJornada.SelectedValue);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            else
            {
                Program.iMostrarJornada = 0;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmCalendario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmCalendario_Load(object sender, EventArgs e)
        {
            llenarComboJornada();

            if (Program.iMostrarJornada == 1)
            {
                this.Height = 316;
            }

            else
            {
                this.Height = 269;
            }
        }
    }
}
