using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmEgresoDeReceta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        string sFecha;
        DataTable dtConsulta;
        bool bRespuesta;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public frmEgresoDeReceta()
        {
            InitializeComponent();
        }

        private void frmEgresoDeReceta_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaAplicacion.Text = sFecha;
            llenarSentencias();
            cargarComboBodega();
            cargarComboOficina();
        }
        
        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = @"select id_bodega, descripcion from cv402_bodegas
                            where categoria =1 ";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.llenar(dtConsulta, sSql);
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo de oficina
        private void cargarComboOficina()
        {
            try
            {
                sSql = @"select id_localidad, nombre_localidad from tp_vw_localidades
                            where codigo in (00004,00005,00006) ";
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbOficina.llenar(dtConsulta, sSql);
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        private void TxtNumeroItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                if (!char.IsPunctuation(e.KeyChar))
                    e.Handled = true;
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencias()
        {
            try
            {
                sSql = "select P.codigo, NP.nombre, P.id_pos_receta from cv401_productos P inner join cv401_nombre_productos NP "+
                        "on P.id_producto = NP.id_producto "+
                        "where P.id_pos_receta >0 and P.estado = 'A' and NP.estado = 'A'";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                    dbAyudaReceta.Ver(sSql, "codigo", 2, 0, 1);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (comprobarCampos() == true)
            { 
            }
        }

        //Función para comprobar los campos
        private bool comprobarCampos()
        {
            int iBandera = 0;

            if (TxtNumeroItems.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, ingrese el número de items","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                iBandera = 1;
                TxtNumeroItems.Focus();
            }
            else if (dbAyudaReceta.txtDatos.Text.Trim() == "" || dbAyudaReceta.txtIdentificacion.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, seleccione una receta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
                dbAyudaReceta.Focus();
            }
            else if (txtRefenciaExterna.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, ingrese una referencia", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
                txtRefenciaExterna.Focus();
            }

            if (iBandera == 1) return false; else return true;
        }        

        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOficina.SelectedIndex > 0)
            {
                sSql = @"select id_bodega from tp_vw_localidades where id_localidad = " + cmbOficina.SelectedValue;
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.SelectedValue = dtConsulta.Rows[0].ItemArray[0];
                }
            }
        }

        private void btnGrabar_Click_1(object sender, EventArgs e)
        {
            if (comprobarFecha() == true)
            {
                if (comprobarCampos() == true)
                {
                    if (MessageBox.Show("¿Desea Grabar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Clases.generacionDeEgresos egreso = new Clases.generacionDeEgresos();
                        int iCgClienteProveedor = obtenerCgClienteProveedor();
                        int iTipoMovimiento = obtenerCorrelativoTipoMovimiento();
                        if (iCgClienteProveedor != 0 || iTipoMovimiento != 0)
                        {
                            bool bConfirmacion = egreso.generarIngreso(txtFechaAplicacion.Text, Convert.ToInt32(cmbBodega.SelectedValue), 16,
                                                          iCgClienteProveedor, iTipoMovimiento,
                                                           51, txtRefenciaExterna.Text, 1, dbAyudaReceta.iId, Convert.ToInt32(TxtNumeroItems.Text),10);
                            if (bConfirmacion == true)
                                MessageBox.Show("Registro ingresado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Ocurrió un problema al ingresar el registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else
                            MessageBox.Show("Ocurrió un problema al encontrar el correlativo del clienteProveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Ingrese una fecha con el formato 'aaaa/mm/dd'");
                sFecha =  Program.sFechaSistema.ToString("dd/MM/yyyy");
                txtFechaAplicacion.Text = sFecha;
            }
           
        }

        //Función para obtener cg_cliente_proveedor
        private int obtenerCgClienteProveedor()
        {
            try
            {
                sSql = "select correlativo from tp_codigos where tabla = 'SYS$00642' and codigo = '02' and estado = 'A'";
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                        return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    else
                        return 0;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Función para obtener tipo de movimiento
        private int obtenerCorrelativoTipoMovimiento()
        {
            try
            {
                sSql = "select correlativo from tp_codigos where tabla = 'SYS$00648' and codigo = 'EMP' and estado = 'A'";
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                        return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    else
                        return 0;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Función para comprobar la fecha
        private bool comprobarFecha()
        {
            int iBandera = 0;
            try
            {
                Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                iBandera = 1;
            }

            if (iBandera == 1) return false; else return true;
        }
       
        //Fin de la clase
    }
}
