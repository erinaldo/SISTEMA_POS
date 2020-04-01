using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmLogoFacturacion : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        Byte[] logoImagen { get; set; }

        public frmLogoFacturacion()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR SI EXISTE UNA IMAGEN EN LOS PARAMETROS
        private void obtenerImagen()
        {
            try
            {
                sSql = "";
                sSql += "select logo_comprobantes" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    imgLogo.Image = Properties.Resources.tu_logo;
                    txtRuta.Clear();

                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    imgLogo.Image = Properties.Resources.tu_logo;
                    txtRuta.Clear();
                }

                if (dtConsulta.Rows[0]["logo_comprobantes"].ToString() == "")
                {
                    imgLogo.Image = Properties.Resources.tu_logo;
                    txtRuta.Clear();
                }

                else
                {
                    byte[] logo = new byte[0];
                    logo = (byte[])dtConsulta.Rows[0]["logo_comprobantes"];
                    MemoryStream ms = new MemoryStream(logo);
                    imgLogo.Image = Image.FromStream(ms);
                    txtRuta.Clear();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL ID DE LOS PARAMETROS DE FACTURACION ELECTRONICA
        private int obtenerIdParametro()
        {
            try
            {
                sSql = "";
                sSql += "select id_cel_parametro" + Environment.NewLine;
                sSql += "from cel_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return 0;
                }

                return Convert.ToInt32(dtConsulta.Rows[0]["id_cel_parametro"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        public byte[] imagenAByte(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //FUNCION PARA ACTUALIZAR LA BASE DE DATOS
        private void actualizaRegistro(int iIdParametro_P)
        {
            try
            {
                logoImagen = imagenAByte(imgLogo.Image);

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo iniciar la transacción para guardar el registro. Consulte con el administardor del sistema.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cel_parametro set" + Environment.NewLine;
                sSql += "logo_comprobantes = convert(varbinary(8000), " + logoImagen + ")" + Environment.NewLine;
                sSql += "where id_cel_parametro = " + iIdParametro_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnModal_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Seleccione imagen |*.jpg; *.jpeg; *.png";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                imgLogo.ImageLocation = abrir.FileName;
                imgLogo.SizeMode = PictureBoxSizeMode.Normal;
                txtRuta.Text = abrir.FileName;
            }

            abrir.Dispose();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            imgLogo.Image = Properties.Resources.tu_logo;
            txtRuta.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtRuta.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la imagen para proceder a guardar.";
                ok.ShowDialog();
                return;
            }

            int iIdParametro = obtenerIdParametro();

            if (iIdParametro == -1)
            {
                return;
            }

            if (iIdParametro == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se encuentran configurados los parámetros de facturación electrónica.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                actualizaRegistro(iIdParametro);
            }
        }

        private void frmLogoFacturacion_Load(object sender, EventArgs e)
        {
            obtenerImagen();
        }
    }
}
