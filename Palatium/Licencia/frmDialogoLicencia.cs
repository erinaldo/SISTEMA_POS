using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;

namespace Palatium.Licencia
{
    public partial class frmDialogoLicencia : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sId_P;
        string sPass_P;
        string sValor;
        string sSql;
        string sAuxiliar;
        string sIpEquipo;
        string sVerificarSerial;
        string sTabla;
        string sCampo;
        string []sDatosMaximo = new string[5];

        DataTable dtConsulta;

        bool bRespuesta;
        bool bInsertar;

        int iCantidadDisponible;
        int iInsertar;
        int iCodigo;
        int iVer;

        long iMaximo;

        SqlParameter[] parametro;

        public frmDialogoLicencia(int iCantidadDisponible_P, int iInsertar_P, int iVer_P)
        {
            this.iCantidadDisponible = iCantidadDisponible_P;
            this.iInsertar = iInsertar_P;
            this.iVer = iVer_P;
            InitializeComponent();
        }

        #region funciones

        //FUNCION PARA CONSULTAR LA LICENCIA
        private void verificar()
        {
            try
            {
                Licencia.ClaseFuncionesLicencia lic = new Licencia.ClaseFuncionesLicencia();
                sValor = lic.GetSystemInfo("PALATIUM");
                sId_P = lic.sId;
                sPass_P = lic.sPass;
                int i = 0;

                if (sId_P.Length == 25)
                {
                    txtID_1.Text = sId_P.Substring(i, 5);
                    i += 5;
                    txtID_2.Text = sId_P.Substring(i, 5);
                    i += 5;
                    txtID_3.Text = sId_P.Substring(i, 5);
                    i += 5;
                    txtID_4.Text = sId_P.Substring(i, 5);
                    i += 5;
                    txtID_5.Text = sId_P.Substring(i, 5);
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el ID del equipo.";
                    ok.ShowDialog();
                    return;
                }

                //i = 0;
                //if (sPass_P.Length == 25)
                //{
                //    txtPass_1.Text = sPass_P.Substring(i, 5);
                //    i += 5;
                //    txtPass_2.Text = sPass_P.Substring(i, 5);
                //    i += 5;
                //    txtPass_3.Text = sPass_P.Substring(i, 5);
                //    i += 5;
                //    txtPass_4.Text = sPass_P.Substring(i, 5);
                //    i += 5;
                //    txtPass_5.Text = sPass_P.Substring(i, 5);
                //}
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CONSULTAR EN LA BASE DE DATOS, EL ID OBTENIDO
        private void consultarID()
        {
            try
            {
                sSql = "";
                sSql += "" + Environment.NewLine;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //SEPARAR EL SERIAL EN 5 CAJAS DE TEXTO
        private void pegarSerial()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                string sPegar = (String)iData.GetData(DataFormats.Text);

                txtPass_1.Text = "";

                if (sPegar.Length > 5)
                {
                    txtPass_1.Text = sPegar.Substring(0, 5);
                    //sAuxiliar = sPegar.Substring(0, 5);
                }

                else
                {
                    txtPass_1.Text = sPegar.Substring(0);
                    //sAuxiliar = sPegar.Substring(0);
                    return;
                }

                if (sPegar.Length > 10)
                    txtPass_2.Text = sPegar.Substring(5, 5);
                else
                {
                    txtPass_2.Text = sPegar.Substring(5);
                    return;
                }

                if (sPegar.Length > 15)
                    txtPass_3.Text = sPegar.Substring(10, 5);
                else
                {
                    txtPass_3.Text = sPegar.Substring(10);
                    return;
                }

                if (sPegar.Length > 20)
                    txtPass_4.Text = sPegar.Substring(15, 5);
                else
                {
                    txtPass_4.Text = sPegar.Substring(15);
                    return;
                }

                if (sPegar.Length > 25)
                    txtPass_5.Text = sPegar.Substring(20, 5);
                else
                {
                    txtPass_5.Text = sPegar.Substring(20);
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL ID
        private void consultarMaximoCodigo()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 isnull(codigo, 0) codigo" + Environment.NewLine;
                sSql += "from pos_terminal" + Environment.NewLine;
                sSql += "order by id_pos_terminal desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                    iCodigo = 1;
                else
                    iCodigo = Convert.ToInt32(dtConsulta.Rows[0]["codigo"].ToString()) + 1;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EXTRAER LA IP DEL EQUIPO
        private void recuperarIP()
        {
            IPHostEntry host;
            sIpEquipo = "";

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    sIpEquipo = ip.ToString();
                }
            }
        }

        //FUNCION PARA REGISTRAR EL EQUIPO
        private void insertarRegistro(int iOpDemo_P)
        {
            try
            {
                int iCantDisp_P = 0;
                int iCantPerm_P = 0;

                if (iOpDemo_P == 1)
                    iCantPerm_P = 50;

                consultarMaximoCodigo();
                recuperarIP();

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_terminal (" + Environment.NewLine;
                sSql += "codigo, descripcion, nombre_maquina, ip_maquina, id_registro," + Environment.NewLine;
                sSql += "serial_registro, demo, cantidad_permitida, cantidad_usada," + Environment.NewLine;
                sSql += "is_active, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@codigo, @descripcion, @nombre_maquina, @ip_maquina, @id_registro," + Environment.NewLine;
                sSql += "@serial_registro, @demo, @cantidad_permitida, @cantidad_usada," + Environment.NewLine;
                sSql += "@is_active, @estado, getdate(), @usuario_ingreso, @terminal_ingreso)";

                int i = 0;
                parametro = new SqlParameter[13];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@codigo";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = iCodigo.ToString();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@descripcion";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "EQUIPO_" + iCodigo.ToString();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@nombre_maquina";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = Environment.MachineName.ToString();
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@ip_maquina";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sIpEquipo;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_registro";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sId_P;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@serial_registro";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sVerificarSerial;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@demo";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iOpDemo_P;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_permitida";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iCantPerm_P;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_usada";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = iCantDisp_P;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@is_active";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 1;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[2];
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@usuario_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[0];
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@terminal_ingreso";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sDatosMaximo[1];

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                //OBTENER EL ID DEL EQUIPO
                sCampo = "id_pos_terminal";
                sTabla = "pos_terminal";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", sDatosMaximo);

                if (iMaximo == -1)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return;
                }

                Program.iIdPosTerminal = Convert.ToInt32(iMaximo);

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                if (iOpDemo_P == 1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La versión de prueba ha iniciado con éxito.";
                    ok.ShowDialog();
                }

                else
                {
                    Program.iVersionDemo = 0;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La aplicación se ha registrado con éxito.";
                    ok.ShowDialog();
                }

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR LA LICENCIA
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_terminal set" + Environment.NewLine;
                sSql += "id_registro = @id_registro," + Environment.NewLine;
                sSql += "serial_registro = @serial_registro," + Environment.NewLine;
                sSql += "demo = @demo," + Environment.NewLine;
                sSql += "cantidad_permitida = @cantidad_permitida," + Environment.NewLine;
                sSql += "cantidad_usada = @cantidad_usada" + Environment.NewLine;
                sSql += "where id_pos_terminal = @id_pos_terminal" + Environment.NewLine;
                sSql += "and estado = @estado";

                int i = 0;
                parametro = new SqlParameter[7];
                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_registro";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sId_P;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@serial_registro";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = sVerificarSerial;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@demo";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_permitida";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@cantidad_usada";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = 0;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@id_pos_terminal";
                parametro[i].SqlDbType = SqlDbType.Int;
                parametro[i].Value = Program.iIdPosTerminal;
                i++;

                parametro[i] = new SqlParameter();
                parametro[i].ParameterName = "@estado";
                parametro[i].SqlDbType = SqlDbType.VarChar;
                parametro[i].Value = "A";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La aplicación se ha registrado con éxito.";
                ok.ShowDialog();
                this.DialogResult = DialogResult.OK;
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

        private void frmDialogoLicencia_Load(object sender, EventArgs e)
        {
            sDatosMaximo[0] = "ADMINISTRADOR";
            sDatosMaximo[1] = Environment.MachineName.ToString();
            sDatosMaximo[2] = "A";

            verificar();

            if (iInsertar == 1)
                lblRestantes.Text = "50";
            else
                lblRestantes.Text = iCantidadDisponible.ToString();

            if (iVer == 1)
                btnEjecutarDemo.Visible = true;
            else
                btnEjecutarDemo.Visible = false;
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Program.sIdEquipo, true);

            ok = new VentanasMensajes.frmMensajeNuevoOk();
            ok.lblMensaje.Text = "El ID se ha copiado.";
            ok.ShowDialog();
        }

        private void btnPegar_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text))
            {
                pegarSerial();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se pudieron recuperar datos del portapapeles.";
                ok.ShowDialog();
            }
        }

        private void txtPass_1_KeyDown(object sender, KeyEventArgs e)
        {
            bool pegar = (e.KeyData == (Keys.Control | Keys.V));
            
            if (pegar == true)
            {
                IDataObject iData = Clipboard.GetDataObject();

                if (iData.GetDataPresent(DataFormats.Text))
                {
                    pegarSerial();
                    //txtPass_1.Text = sAuxiliar;
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudieron recuperar datos del portapapeles.";
                    ok.ShowDialog();
                }
            }
        }

        private void btnEjecutarDemo_Click(object sender, EventArgs e)
        {
            sVerificarSerial = "";

            if (iInsertar == 1)
            {
                insertarRegistro(1);
            }

            else
            {
                if (iCantidadDisponible == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La versión de prueba ha finalizado. Se recomienda activar el producto para poder continuar utilizando el sistema.";
                    ok.ShowDialog();
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Se encuentra utilizando una versión de prueba." + Environment.NewLine + "Dispone de " + iCantidadDisponible.ToString() + " transacciones disponibles.";
                    ok.ShowDialog();
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            string sLicencia = txtPass_1.Text.Trim() + txtPass_2.Text.Trim() + txtPass_3.Text.Trim() + txtPass_4.Text.Trim() + txtPass_5.Text.Trim();

            if ((txtPass_1.Text.Trim() == "") && (txtPass_2.Text.Trim() == "") && (txtPass_3.Text.Trim() == "") && (txtPass_4.Text.Trim() == "") && (txtPass_5.Text.Trim() == ""))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de serie para activar el sistema.";
                ok.ShowDialog();
                return;
            }

            if (txtPass_1.Text.Trim().Length < 5)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (txtPass_2.Text.Trim().Length < 5)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (txtPass_3.Text.Trim().Length < 5)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (txtPass_4.Text.Trim().Length < 5)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (txtPass_5.Text.Trim().Length < 5)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (sLicencia.Length != 25)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            if (sLicencia != sPass_P)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de serie es incorrecto. Favor verifique.";
                ok.ShowDialog();
                return;
            }

            sVerificarSerial = sLicencia;

            if (iInsertar == 1)
                insertarRegistro(0);
            else
                actualizarRegistro();
        }
    }
}
