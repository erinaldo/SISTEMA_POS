using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using ConexionBD;
using System.Diagnostics;

namespace Palatium.Formularios
{
    public partial class FReciboDeCobros : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        DataTable dtTipoEmpresa = new DataTable();
        DataTable dtBodega, dtListPrec, dtServidor, dtlocalidad;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        DataTable dtVendedor;
        DataTable dtMoneda;
        string estado = "";
        string T_st_sql = "";
        bool x = false; 

        int iIdPag;
        int iNumeroPag;
        string sNombr;
        string sComentari;
        string sFechaPag;
        string sLocalida;
        string sSeri;
        string sNombreLocalida;


        /*
         * VARIACION DE CODIGOS Y VARIABLES
         * AUTOR: ELVIS GUAIGUA
         * FECHA DE MODIFICACIPON: 2018-07-14
         * CONCEPTO: DEFINICIÓN DE VARIABLES PARA AJUSTARSE AL ESTANDAR ESTABLECIDO
        */

        string sSql;
        DataTable dtConsulta;

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();


        public FReciboDeCobros()
        {
            InitializeComponent();
        }

        private void FReciboDeCobros_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();

        }

        //Llenar combo tipo Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                //sSql = "select idempresa,nombrecomercial from sis_empresa";
                sSql = "select idempresa,isnull(nombrecomercial, razonsocial) nombre_comercial, * from sis_empresa where idempresa = " + Program.iIdEmpresa;
                cmbEmpresa.llenar(dtConsulta, sSql);
                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        private void limpiarTodo()
        {
            txtNumRecibo.Text = "";
            txtNombreRecibo.Text = "";
            txtCliente.Text = "";
            txtObserv.Text = "";
            txtFechaPago.Text = "";
            txtLocalidad.Text = "";
            txtSerie.Text = "";
            txtTotal1.Text = "";
            txtTotal2.Text = "";
            txtTotal3.Text = "";
            txtTotalDocumento.Text = "";
            txtFaltante.Text = "";
            //dgvDocumentosPagados.Rows.Clear();
            //dgvDocumentosPago.Rows.Clear();
        }

        private void btnCerrarReciboCobros_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimirReciboCobros_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            
        }

        private void btnPregunta_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaReciboCobros ayuda1 = new Formularios.FAyudaReciboCobros();
            ayuda1.ShowDialog();

            iIdPag = Convert.ToInt32(ayuda1.IdPago);
            sSeri = ayuda1.Serie;
            txtNumRecibo.Text = ayuda1.NumeroPago.ToString();
            sNombr= ayuda1.Nombre;
            sFechaPag = ayuda1.FechaPago;
            sLocalida = ayuda1.Localidad;
            txtNombreRecibo.Text = ayuda1.Comentario;
            sComentari = ayuda1.Comentario;
        }

        private void btnOKReciboCobros_Click(object sender, EventArgs e)
        {
            if (txtNumRecibo.Text == "" && txtNombreRecibo.Text == "")
            {
                MessageBox.Show("Favor elegir un recibo");
                btnPregunta.Focus();
            }
            else
            {
                txtCliente.Text = sNombr;
                txtObserv.Text = sComentari;
                txtFechaPago.Text = sFechaPag;
                txtLocalidad.Text = sLocalida;
                txtSerie.Text = sSeri;

                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridPagados(t_st_datos);

                string[] t_st_datos1 = { "1", "adsdasdasd" };
                llenarGridCompago(t_st_datos1);
            }
        }

        //FUNCION PARA LLENAR EL GRID cuando no se haya seleccionado ningun cliente 
        private void llenarGridPagados(string[] t_st_datos)
        {
            //int iIdTipoComprovante = 1;

            try
            {
                string t_st_query2 = "";

                if (t_st_datos[0] == "1")
                {
                    t_st_query2 = "Select TIPOS.codigo,DOC.numero_documento,DOC.fecha_vcto,DOC.valor,(Select Sum(PAG.valor) From cv403_documentos_pagados PAG,cv403_pagos PAGOS Where PAG.id_documento_cobrar = DOC.id_documento_cobrar And PAG.estado = 'A' And PAGOS.estado = 'A' And PAGOS.id_pago = PAG.id_pago And PAGOS.fecha_Pago <= Convert(DateTime,'2018/02/28',120) And PAG.id_pago <> "+iIdPag+") Saldo_Anterior,DP.valor Total_Pagado From cv403_documentos_pagados DP,cv403_dctos_por_cobrar DOC,tp_codigos TIPOS Where DP.estado = 'A' And DP.id_pago = "+iIdPag+" And DP.id_documento_cobrar = DOC.id_documento_cobrar And DOC.cg_tipo_documento = TIPOS.correlativo And TIPOS.codigo IN( 'FAC','CH','CU','CHD','MCHD') Order By TIPOS.codigo,DOC.fecha_vcto";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query2, "cv403_documentos_pagados");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvDocumentosPagados.AutoGenerateColumns = false;
                    dgvDocumentosPagados.DataSource = conexion.ds.Tables["cv403_documentos_pagados"];

                    //para poner el formato de la celda
                    dgvDocumentosPagados.Columns[3].DefaultCellStyle.Format = "#,##0.00";
                    dgvDocumentosPagados.Columns[4].DefaultCellStyle.Format = "#,##0.00";
                    dgvDocumentosPagados.Columns[5].DefaultCellStyle.Format = "#,##0.00";
                    //para alinear los datos 
                    dgvDocumentosPagados.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPagados.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPagados.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPagados.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    totalesDocuPagados();
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //FUNCION PARA LLENAR EL GRID cuando no se haya seleccionado ningun cliente 
        private void llenarGridCompago(string[] t_st_datos)
        {
            //int iIdTipoComprovante = 1;

            try
            {
                string t_st_query3 = "";

                if (t_st_datos[0] == "1")
                {
                    t_st_query3 = "Select TIPOS.codigo,DP.fecha_vcto, DP.numero_documento,Case When TIPOS.codigo = 'CH' Then convert(varchar,DP.Numero_Cta) Else convert(varchar,DP.Numero_Tarjeta) End Numero_Cta,DP.valor, Case When TIPOS.codigo = 'CH' Then (Select Valor_Texto From tp_codigos Where Correlativo = DP.Cg_Banco) Else (Select Valor_Texto From tp_codigos Where Correlativo = DP.Cg_Tarjeta) End Banco,DP.Titular,DP.autorizacion From cv403_documentos_pagos DP,tp_codigos TIPOS Where DP.estado = 'A' And DP.id_pago = "+iIdPag+" And TIPOS.correlativo = DP.cg_tipo_documento And (TIPOS.codigo = 'CH' Or TIPOS.codigo = 'VO')";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query3, "cv403_documentos_pagos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvDocumentosPago.AutoGenerateColumns = false;
                    dgvDocumentosPago.DataSource = conexion.ds.Tables["cv403_documentos_pagos"];

                    //para poner el formato de la celda
                    dgvDocumentosPago.Columns[4].DefaultCellStyle.Format = "#,##0.00";

                    //para alinear los datos 
                    dgvDocumentosPago.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPago.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPago.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDocumentosPago.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    totalesDocuPago();
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public void totalesDocuPagados()
        {
            double dbTotalValorFactura = 0;
            double dbTotalSaldoPagar = 0;
            double dbTotalValorPago = 0;

            foreach (DataGridViewRow row in dgvDocumentosPagados.Rows)   //recorrera toda la tabla donde se encuentren valores 
            {
                dbTotalValorFactura += Convert.ToDouble(row.Cells[3].Value); //lo que tenga la columna 5 ira sumando 
                var vValorNull = row.Cells[4].Value;
                if (row.Cells[4].Value == vValorNull)
                {
                    dgvDocumentosPagados.Rows[0].Cells[4].Value = Convert.ToDouble(row.Cells[3].Value);
                }
                dbTotalSaldoPagar += Convert.ToDouble(row.Cells[4].Value);
                dbTotalValorPago += Convert.ToDouble(row.Cells[5].Value);
            }
            //totales del primer GRID
            txtTotal1.Text = dbTotalValorFactura.ToString();
            txtTotal2.Text = dbTotalSaldoPagar.ToString();
            txtTotal3.Text = dbTotalValorPago.ToString();
        }

        public void totalesDocuPago()
        {
            double dbTotalValor = 0;

            foreach (DataGridViewRow row in dgvDocumentosPago.Rows)   //recorrera toda la tabla donde se encuentren valores 
            {
                dbTotalValor += Convert.ToDouble(row.Cells[4].Value);
            }

            //Totales del segundo GRID
            txtTotalDocumento.Text = dbTotalValor.ToString();
        }



    }
}
