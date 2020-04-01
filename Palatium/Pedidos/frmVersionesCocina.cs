using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmVersionesCocina : Form
    {

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        Clases.ClaseReporteCocinaTextBox cocina = new Clases.ClaseReporteCocinaTextBox();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        DataTable dtConsulta;
        DataTable dtImprimir;

        bool bRespuesta;

        int iVersionImpresion;
        int iIdOrden;
        int iIdImpresionComanda;
        int iIdImpresora;
        int iCortarPapel;
        int iAbrirCajon;
        int iSecuenciaImpresion;
        int iCantidadImpresiones;

        string sSql;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        string sIdOrden;
        string sRetorno;
        string sDescripcionComanda;
        string sNombreImpresora;
        

        public frmVersionesCocina(int iIdOrden, int iVersionImpresion)
        {
            this.iIdOrden = iIdOrden;
            this.iVersionImpresion = iVersionImpresion;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO
        private void llenarCombo()
        {
            try
            {
                List<Item> lista = new List<Item>();

                lista.Add(new Item("Seleccione ítem...", 0));

                for (int i = 1; i <= iVersionImpresion; i++)
                {
                    lista.Add(new Item("VERSION " + i.ToString(), i));
                }

                cmbVersiones.DisplayMember = "sDescripcion";
                cmbVersiones.ValueMember = "iValor";
                cmbVersiones.DataSource = lista;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CLASE AUXILIAR PARA LLENAR EL COMBOBOX
        class Item
        {
            public string sDescripcion { get; set; }
            public int iValor { get; set; }

            public Item(string descripcion, int valor)
            {
                sDescripcion = descripcion;
                iValor = valor;
            }

            public override string ToString()
            {
                return sDescripcion;
            }
        }


        //FUNCION PARA LLENAR LA CAJA DE TEXTP 
        private void llenarTextBox(int iSecuencia)
        {
            try
            {
                txtReporte.Clear();

                sSql = "";
                sSql = sSql + "select * from pos_vw_det_pedido" + Environment.NewLine;
                sSql = sSql + "where id_pedido = " + iIdOrden + Environment.NewLine;
                sSql = sSql + "and estado = 'A'" + Environment.NewLine;

                if (iSecuencia != 0)
                {
                    sSql = sSql + "and secuencia = " + iSecuencia + Environment.NewLine;
                }
                
                sSql = sSql + "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sRetorno = "";
                        sRetorno = cocina.llenarPrecuentaCocina(dtConsulta, iIdOrden.ToString(), "");

                        if (sRetorno == "")
                        {
                            ok.LblMensaje.Text = "No hay datos para imprimir.";
                            ok.ShowDialog();
                        }

                        else
                        {
                            txtReporte.Text = sRetorno;
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        #endregion
        
        #region FUNCION PARA IMPRIMIR COMANDA COMPLETA

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void imprimirReporteCompleto()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select path_url, numero_impresion, puerto_impresora," + Environment.NewLine;
                sSql = sSql + "ip_impresora, descripcion, cortar_papel, abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora" + Environment.NewLine;
                sSql = sSql + "where id_pos_impresora = " + Program.iIdImpresoraReportes;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0][6].ToString());

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();

                        Program.iCortar = 0;

                        imprimir.escritoEspaciadoCorto(txtReporte.Text + Environment.NewLine + Environment.NewLine + ".");

                        if (iCortarPapel == 1)
                        {
                            imprimir.cortarPapel();
                        }

                        imprimir.imprimirReporte(sNombreImpresora);

                        // sRetorno = cocina.encabezadoReporteCocina(dtConsulta);
                        sRetorno = "";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmVersionesCocina_Load(object sender, EventArgs e)
        {
            llenarCombo();
            llenarTextBox(0);
            this.ActiveControl = cmbVersiones;
        }

        private void btnComandaCompleta_Click(object sender, EventArgs e)
        {
            cmbVersiones.SelectedValue = 0;
            //llenarTextBox(0);
        }

        private void cmbVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarTextBox(Convert.ToInt32(cmbVersiones.SelectedValue));
        }

        private void btnImprimirComanda_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbVersiones.SelectedValue) == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione una versión de reimpresión de la comanda.";
                ok.ShowDialog();
            }

            else
            {
                Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdOrden.ToString(), Convert.ToInt32(cmbVersiones.SelectedValue));
                cocina.ShowDialog();
            }
        }

        private void btnImprimirComandaCompleta_Click(object sender, EventArgs e)
        {
            cmbVersiones.SelectedValue = 0;
            imprimirReporteCompleto();
        }

        private void frmVersionesCocina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
