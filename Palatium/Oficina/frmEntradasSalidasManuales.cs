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
    public partial class frmEntradasSalidasManuales : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;

        int iOp;
        int iIdPosCierreCajeroParametro;
        string sFecha;
        Double dSuma;

        public frmEntradasSalidasManuales(int iOp, string sFecha, int iIdPosCierreCajero_P)
        {
            this.iOp = iOp;
            this.sFecha = sFecha;
            this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select concepto as CONCEPTO, ltrim(str(valor, 6, 2)) as VALOR  from pos_movimiento_caja" + Environment.NewLine;
                //sSql += "where fecha = '" + sFecha + "'" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and tipo_movimiento = " + iOp + Environment.NewLine;
                sSql += "and id_documento_pago is null" + Environment.NewLine;
                //sSql += "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        dSuma = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dSuma = dSuma + Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                        }

                        txtTotal.Text = dSuma.ToString("N2");
                    }

                    else
                    {
                        dgvDatos.DataSource = dtConsulta;

                        txtTotal.Text = "0.00";
                    }

                    dgvDatos.Columns[0].Width = 475;
                    dgvDatos.Columns[1].Width = 100;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmEntradasSalidasManuales_Load(object sender, EventArgs e)
        {
            llenarGrid();

            if (iOp == 1)
            {
                this.Text = "Entradas Manuales";
            }

            else
            {
                this.Text = "Salidas Manuales";
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No hay ítems para imprimir.";
                ok.ShowDialog();
            }

            else
            {
                Pedidos.frmVerMovimientosAgrupados ver = new Pedidos.frmVerMovimientosAgrupados(iOp, sFecha, 1);
                ver.ShowDialog();
            }
        }

        private void frmEntradasSalidasManuales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
