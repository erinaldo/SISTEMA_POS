using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ConexionBD;

namespace Palatium
{
    public partial class Domicilio : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public DataTable dtConsulta;
        string sIdPersona;
        string sSql;
        bool bRespuesta = false;
        DataTable dtDireccion;
        DataTable dtValor;

        Double dValorMovilizacion;

        string sIdPersonaRetorno { get; set; }

        public Domicilio()
        {
            InitializeComponent();           
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR EL VALOR DE LA MOVILIZACION
        private void consultarValorMovilizacion()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select PP.valor" + Environment.NewLine;
                sSql = sSql + "from cv403_precios_productos PP, cv401_productos P" + Environment.NewLine;
                sSql = sSql + "where PP.id_producto = P.id_producto" + Environment.NewLine;
                sSql = sSql + "and P.codigo = 'MOVILI'" + Environment.NewLine;
                sSql = sSql + "and PP.id_lista_precio = 4" + Environment.NewLine;
                sSql = sSql + "and P.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and PP.estado = 'A'";

                dtValor = new DataTable();
                dtValor.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtValor, sSql);

                if (bRespuesta == true)
                {
                    if (dtValor.Rows.Count > 0)
                    {
                        dValorMovilizacion = Convert.ToDouble(dtValor.Rows[0][0].ToString());
                        txtValor.Text = (dValorMovilizacion * (1 + Program.iva + Program.servicio)).ToString("N2");
                    }

                    else
                    {
                        txtValor.Text = "0.00";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CONSULTAR DATOS 
        public void consultarRegistroDireccion()
        {
            try
            {
                sSql = "";
                sSql += "select direccion, calle_principal, calle_interseccion, numero_vivienda, referencia" + Environment.NewLine;
                sSql += "from tp_direcciones" + Environment.NewLine;
                sSql += "where id_persona = " + sIdPersona + Environment.NewLine;
                sSql += "and estado = 'A'";
                
                dtDireccion = new DataTable();
                dtDireccion.Clear();
                
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDireccion, sSql);

                if (bRespuesta == true)
                {
                    if (dtDireccion.Rows.Count > 0)
                    {
                        txtSector.Text = dtDireccion.Rows[0][0].ToString(); ;
                        txtCallePrincipal.Text = dtDireccion.Rows[0][1].ToString();
                        txtCalleSecundaria.Text = dtDireccion.Rows[0][2].ToString();
                        txtNumero.Text = dtDireccion.Rows[0][3].ToString();
                        txtReferencia.Text = dtDireccion.Rows[0][4].ToString();

                        llenarDatosAdicionales();
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

        //FUNCION PARA VOLVER A CARGAR LOS DATOS DEL CLIENTE
        private void consultarDatosCliente()
        {
            try
            {
                sSql = "";
                sSql += "select identificacion, nombres, apellidos, correo_electronico, codigo_alterno, id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where id_persona = " + Convert.ToInt32(sIdPersona) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtIdentificacion.Text = dtConsulta.Rows[0][0].ToString();
                        txtNombres.Text = dtConsulta.Rows[0][1].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0][2].ToString();
                        txtMail.Text = dtConsulta.Rows[0][3].ToString();
                        txtNumeroTelefono.Text = dtConsulta.Rows[0][4].ToString();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            fin: { }
        }

        //FUNCION PARA LLENAR LOS CAMPOS EXTRAS EN EL FORMULARIO
        private void llenarDatosAdicionales()
        {
            try
            {
                //LLENAR TEXTBOX DE CLIENTE DESDE
                sSql = "";
                sSql += "select top 1 isnull(fecha_pedido, 'NO HAY REGISTROS')" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + Convert.ToInt32(sIdPersona) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtClienteDesde.Text = dtConsulta.Rows[0][0].ToString().Substring(0, 10);
                    }

                    else
                    {
                        txtClienteDesde.Text = "NO HAY REGISTROS";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                //LLENAR TEXTBOX DE CANTIDAD DE ORDENES DEL CLIENTE
                sSql = "";
                sSql += "select count(*) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + Convert.ToInt32(sIdPersona) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtNumeroOrdenes.Text = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        txtNumeroOrdenes.Text = "NO HAY REGISTROS";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                //LLENAR TEXTBOX DE ULTIMO REGISTRO DE ORDEN
                sSql = "";
                sSql += "select top 1 isnull(fecha_pedido, 'NO HAY REGISTROS')" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_persona = " + Convert.ToInt32(sIdPersona) + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by id_pedido desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtFechaUltimaOrden.Text = dtConsulta.Rows[0][0].ToString().Substring(0, 10);
                    }

                    else
                    {
                        txtFechaUltimaOrden.Text = "NO HAY REGISTROS";
                    }

                    goto fin;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            fin: { }
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void Domicilio_Load(object sender, EventArgs e)
        {
            consultarValorMovilizacion();

            if (dtConsulta.Rows.Count > 0)
            {
                txtIdentificacion.Text = dtConsulta.Rows[0][0].ToString();
                txtNombres.Text = dtConsulta.Rows[0][1].ToString();
                txtApellidos.Text = dtConsulta.Rows[0][2].ToString();
                txtMail.Text = dtConsulta.Rows[0][3].ToString();
                txtNumeroTelefono.Text = dtConsulta.Rows[0][4].ToString();
                txtReferencia.Text = txtReferencia.Text.ToUpper();
                
                sIdPersona = dtConsulta.Rows[0][5].ToString();

                consultarRegistroDireccion();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }      

        private void btn_editar_Click_1(object sender, EventArgs e)
        {
            //Domicilios.Direccion dir = new Domicilios.Direccion(sIdPersona);            
            //AddOwnedForm(dir);
            //dir.txtIdentificacion.Text = txtIdentificacion.Text.Trim();
            //dir.txtNombres.Text = txtNombres.Text.Trim();
            //dir.txtApellidos.Text = txtApellidos.Text.Trim();
            //dir.txtTelefono.Text = txtNumeroTelefono.Text.Trim();
            //dir.txtMail.Text = txtMail.Text.Trim();

            //dir.txtSector.Text = txtSector.Text;
            //dir.txtPrincipal.Text = txtCallePrincipal.Text;
            //dir.txtSecundaria.Text = txtCalleSecundaria.Text;
            //dir.txtNumeracion.Text = txtNumero.Text;
            //dir.txtReferencia.Text = txtReferencia.Text;
            //dir.ShowInTaskbar = false;
            //dir.ShowDialog();

            //if (dir.DialogResult == DialogResult.OK)
            //{
            //    sIdPersona = Program.sIDPERSONA;
            //    consultarDatosCliente();
            //    consultarRegistroDireccion();
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.sIDPERSONA = null;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Domicilio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtIdentificacion.Text == "") || (txtSector.Text == ""))
                {
                    ok.LblMensaje.Text = "Favor rellene los campos para el domicilio";
                    ok.ShowDialog();
                }

                else
                {                    
                    Program.sIDPERSONA = sIdPersona;
                    Program.sIdentificacion = txtIdentificacion.Text.Trim();

                    //ACTUALIZACION ELVIS COMANDA
                    //=======================================================================================================================
                    ComandaNueva.frmComanda or = new ComandaNueva.frmComanda(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Program.CAJERO_ID, Program.iIdMesero, Program.sNombreCajero, "NINGUNA", Program.iIdPosRepartidor, 0, Convert.ToInt32(sIdPersona));
                    or.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    //=======================================================================================================================
                    
                    //Orden or = new Orden(Program.iIdOrigenOrden, Program.sDescripcionOrigenOrden, 0, 0, 0, "", Convert.ToInt32(sIdPersona), Program.CAJERO_ID, Program.CAJERO_ID, Program.sNombreCajero, 0, Program.iIdPosRepartidor);
                    //or.ShowDialog();
                    //this.DialogResult = DialogResult.OK;
                    //this.Close();                   
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }            
        }
    }
}
