using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmEjecutarQuery : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        string sSql;

        public frmEjecutarQuery()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtInstruccionSQL.Clear();
            txtInstruccionSQL.Focus();
        }

        //FUNCION PARA EJECUTAR LA INSTRUCCIÓN SQL
        private void ejecutarSQL()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = txtInstruccionSQL.Text.Trim();

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Instrucción ejecutada con éxito.";
                ok.ShowDialog();
                limpiar();
                goto fin;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        fin:
            { }
        }

        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            SiNo.LblMensaje.Text = "¿Desea ejecutar la instrucción SQL ingresada?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                ejecutarSQL();
            }
        }
    }
}
