using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmVentasPorMesero : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;

        int iIdCierreCajero;        
        Double dSuma;

        public frmVentasPorMesero(int iIdCierreCajero_P)
        {   
            this.iIdCierreCajero = iIdCierreCajero_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private void llenarGrid()
        {
            try
            {
                dSuma = 0;

                sSql = "";
                sSql += "select mesero MESERO, " + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 10,2)) TOTAL" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero + Environment.NewLine;
                sSql += "group by mesero" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dSuma = dSuma + Convert.ToDouble(dtConsulta.Rows[i][1].ToString());
                        }

                        dgvDatos.Columns[0].Width = 250;
                        dgvDatos.Columns[1].Width = 100;
                        dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        txtTotal.Text = dSuma.ToString("N2");

                        dgvDatos.ClearSelection();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay registrios para imprimir.";
                ok.ShowDialog();
            }

            else
            {
                ReportesTextbox.frmVentasMesero mesero = new ReportesTextbox.frmVentasMesero(iIdCierreCajero);
                mesero.ShowDialog();
            }
        }

        private void frmVentasPorMesero_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void frmVentasPorMesero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }        
    }
}
