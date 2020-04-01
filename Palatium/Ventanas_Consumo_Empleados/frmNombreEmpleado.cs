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
    public partial class frmNombreEmpleado : Form
    {
        ConexionBD.ConexionBD conectar = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public int iIdEmpleado;
        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;

        public frmNombreEmpleado()
        {
            InitializeComponent();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            Ventanas_Consumo_Empleados.frmAyudaConsumoEmpleados ayuda = new Ventanas_Consumo_Empleados.frmAyudaConsumoEmpleados();
            ayuda.ShowInTaskbar = false;
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                txtIdentificacion.Text = ayuda.sIdentificacion;
                txtNombreEmpleado.Text = ayuda.sNombre;
                iIdEmpleado = Convert.ToInt32(ayuda.sIdEmpleado);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text == "" || txtNombreEmpleado.Text == "")
            {
                ok.LblMensaje.Text = "Por favor, seleccione un empleado.";
                ok.ShowDialog();
            }
            else
            {
                consultarDatos("06");
                //Program.dbValorPorcentaje = 25;
                Program.dbDescuento = Program.descuento_empleados / 100;
                frmVerificadorOrigen verificador = new frmVerificadorOrigen(Program.sDescripcionOrigenOrden);
                verificador.ShowDialog();

                if (verificador.DialogResult == DialogResult.OK)
                {
                    //ACTUALIZACION ELVIS COMANDA
                    //=======================================================================================================================
                    this.DialogResult = DialogResult.OK;
                    ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.nombreMesero, "NINGUNA", 0, 0, iIdEmpleado);
                    Program.sIDPERSONA = iIdEmpleado.ToString();
                    or.ShowDialog();
                    verificador.Close();
                    this.Close();
                    //=======================================================================================================================

                    //this.DialogResult = DialogResult.OK;
                    //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", iIdEmpleado, Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, 0, 0);
                    //Program.sIDPERSONA = iIdEmpleado.ToString();
                    //or.ShowDialog();
                    //verificador.Close();
                    //this.Close();
                }
            }
        }

        #region FUNCIONES DEL USUARIO

        //CONSULTA  HABILITARÁ LAS OPCIONES
        private void consultarDatos(string sOpcion)
        {
            try
            {
                Program.iDomicilioEspeciales = 0;
                Program.iModoDelivery = 0;

                Program.dbValorPorcentaje = 25;
                Program.dbDescuento = Program.dbValorPorcentaje / 100;

                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion, genera_factura," + Environment.NewLine;
                sSql += "id_persona, id_pos_modo_delivery, presenta_opcion_delivery, codigo" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where codigo = '" + sOpcion + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    Program.iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    Program.sDescripcionOrigenOrden = dtConsulta.Rows[0].ItemArray[1].ToString();
                    Program.iGeneraFactura = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());

                    if ((dtConsulta.Rows[0].ItemArray[3].ToString() == null) || (dtConsulta.Rows[0].ItemArray[3].ToString() == ""))
                    {
                        Program.iIdPersonaOrigenOrden = 0;
                    }

                    else
                    {
                        Program.iIdPersonaOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[3].ToString());

                    }
                    Program.iIdPosModoDelivery = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString());
                    Program.iPresentaOpcionDelivery = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString());
                    Program.sCodigoAsignadoOrigenOrden = dtConsulta.Rows[0].ItemArray[6].ToString();

                    Program.sIdGrid = "14";
                    Program.sFormaPagoGrid = "CONSUMO EMPLEADO";
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema en realizar la consulta.";
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNombreEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
