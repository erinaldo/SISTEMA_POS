using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.MotivosCancelacion
{
    public partial class frmMotivoAnulacionFactura : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;

        int iIdFactura;

        public string sMotivoAnulacion;

        public frmMotivoAnulacionFactura()
        {
            //this.iIdFactura = iIdFactura;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_anulacion_factura (" + Environment.NewLine;
                sSql += "id_factura, motivo_anulacion, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura + ", '" + txtMotivo.Text.Trim() + "', 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                //ok.LblMensaje.Text = "La orden ha sido cancelada éxitosamente.";
                //ok.ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
            
        }

        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el motivo de anulación de la factura.";
                ok.ShowDialog();
            }

            else
            {
                //insertarRegistro();
                sMotivoAnulacion = txtMotivo.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmMotivoAnulacionFactura_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtMotivo;
        }
    }
}
