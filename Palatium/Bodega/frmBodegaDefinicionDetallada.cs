using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmBodegaDefinicionDetallada : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        string sSql;
        string sTipo;
        string sCategoria;

        bool bRespuesta;
        bool bActualizar;

        DataTable dtConsulta;
        

        public frmBodegaDefinicionDetallada()
        {
            InitializeComponent();
        }

        private void frmBodegaDefinicionDetallada_Load(object sender, EventArgs e)
        {
            llenarcomboEmpresa();
            llenarSentencias(0);
            llenarSenteciasProductos();
            llenarGrid();
            cmbEstado.Text = "ACTIVO";
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el combo de moneda
        private void llenarcomboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, razonSocial, Codigo" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencias(int iBandera)
        {
            try
            {
                sSql = "";
                sSql += "select identificacion codigo, apellidos + ' ' + isnull(Nombres,'') descripcion," + Environment.NewLine;
                sSql += "id_persona correlativo" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dbAyudaResponsable.Ver(sSql, "codigo", 2, 0, 1);

                //dtConsulta = new DataTable();
                //dtConsulta.Clear();
                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSentencia);
                //if (bRespuesta == true)
                //{
                //    dbAyudaResponsable.Ver(sSentencia, "codigo", 2, 0, 1);

                //    if (iBandera == 1)
                //    {
                //        dbAyudaResponsable.txtIdentificacion.Text = dtConsulta.Rows[0][0].ToString();
                //        dbAyudaResponsable.txtDatos.Text = dtConsulta.Rows[0][1].ToString();
                //    }

                //}
                //else
                //{
                //    catchMensaje.LblMensaje.Text = sSentencia;
                //    catchMensaje.ShowDialog();
                //}
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbayuda de productos
        private void llenarSenteciasProductos()
        {
            try
            {
                sSql = "";
                sSql += "select codigo, nombre Descripcion, 1 Correlativo" + Environment.NewLine;
                sSql += "from cv402_vw_categorias ";

                dbAyudaCategoria.Ver(sSql, "Codigo", 2, 0, 1);

                //dtConsulta = new DataTable();
                //dtConsulta.Clear();
                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSentencia);
                //if (bRespuesta == true)
                //{
                //    if (dtConsulta.Rows.Count > 0)
                //    {
                //       dbAyudaCategoria.Ver(sSentencia,"Codigo",2,0,1);
                //       dbAyudaCategoria.txtIdentificacion.Text = dtConsulta.Rows[0][0].ToString();
                //       dbAyudaCategoria.txtDatos.Text = dtConsulta.Rows[0][1].ToString();

                //    }
                //}
                //else
                //{
                //    catchMensaje.LblMensaje.Text = sSentencia;
                //    catchMensaje.ShowDialog();
                //}
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el grid
        private void llenarGrid()
        {
            try
            {
                dgvBodega.Rows.Clear();

                sSql = "";
                sSql += "Select EMP.razonsocial empresa, BOD.codigo, BOD.Descripcion, BOD.Categoria," + Environment.NewLine;
                sSql += "case BOD.estado when 'A' then 'ACTIVO' else 'INACTIVO' end estado, " + Environment.NewLine;
                sSql += "BOD.ID_PERSONA, BOD.idEmpresa, BOD.id_bodega, BOD.Tipo" + Environment.NewLine;
                sSql += "From cv402_bodegas BOD, sis_empresa EMP" + Environment.NewLine;
                sSql += "where BOD.idempresa = EMP.idempresa" + Environment.NewLine;
                sSql += "and EMP.estado ='A'" + Environment.NewLine;
                sSql += "and BOD.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "order by EMP.razonsocial, BOD.Descripcion ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvBodega.Rows.Add(dtConsulta.Rows[i][0].ToString(),
                                                dtConsulta.Rows[i][1].ToString(),
                                                dtConsulta.Rows[i][2].ToString(),
                                                mostrarCategoria(dtConsulta.Rows[i][3].ToString()),
                                                dtConsulta.Rows[i][4].ToString(),
                                                dtConsulta.Rows[i][5].ToString(),
                                                dtConsulta.Rows[i][6].ToString(),
                                                dtConsulta.Rows[i][7].ToString(),
                                                dtConsulta.Rows[i][8].ToString()
                                                );
                        }

                        this.dgvBodega.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.ToString();
                catchMensaje.ShowDialog();
            }
        }

        //Función para mostrar el nombre de la categoría
        private string mostrarCategoria(string sCodigo_P)
        {
            sSql = "";
            sSql += "select nombre Descripcion" + Environment.NewLine;
            sSql += "from cv402_vw_categorias" + Environment.NewLine;
            sSql += "where codigo = '" + sCodigo_P + "'";

            DataTable dtConsulta2 = new DataTable();
            dtConsulta2.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta2, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta2.Rows.Count > 0)
                {
                    return dtConsulta2.Rows[0][0].ToString();
                }

                else
                {
                    return "Error";
                }
            }

            else
            {
                return "Error";
            }
        }

        private string mostrarCodigo(string sNombre_P)
        {
            sSql = "";
            sSql += "select codigo Descripcion" + Environment.NewLine;
            sSql += "from cv402_vw_categorias" + Environment.NewLine;
            sSql += "where Nombre = '" + sNombre_P + "'";

            DataTable dtConsulta2 = new DataTable();
            dtConsulta2.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta2, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta2.Rows.Count > 0)
                {
                    return dtConsulta2.Rows[0][0].ToString();
                }

                else
                {
                    return "Error";
                }
            }

            else
            {
                return "Error";
            }
        }

        //Función para llenar las cajas de texto cuando se da doble click en el datagridview
        private void llenarVariables()
        {
            try
            {
                cmbEstado.Text = dgvBodega.CurrentRow.Cells[4].Value.ToString();
                txtCodigo.Text = dgvBodega.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvBodega.CurrentRow.Cells[2].Value.ToString();
                int iIdpersona = Convert.ToInt32(dgvBodega.CurrentRow.Cells[5].Value.ToString());
                string[] sResponsable = obtenerResponsable(iIdpersona);
                dbAyudaResponsable.txtIdentificacion.Text = sResponsable[0];
                dbAyudaResponsable.txtDatos.Text = sResponsable[1];
                string sCodigoTipo = dgvBodega.CurrentRow.Cells[8].Value.ToString();
                string[] sRespuesta = obtenerTipoCategoria(sCodigoTipo);
                dbAyudaCategoria.txtIdentificacion.Text = sRespuesta[0];
                dbAyudaCategoria.txtDatos.Text = sRespuesta[1];
                bActualizar = true;
                txtCodigo.Enabled = false;
                grupoDatos.Enabled = true;
                btnGrabar.Text = "Guardar";
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para retornar la persona responsable de la bodega
        private string[] obtenerResponsable(int iIdPersona_P)
        {
            string[] sRespuesta = new string[2];
            sRespuesta[0] = "Error";
            sRespuesta[1] = "Error";

            sSql = "";
            sSql += "select identificacion codigo, Apellidos+' '+ isnull(Nombres,'') descripcion," + Environment.NewLine;
            sSql += "id_persona correlativo" + Environment.NewLine;
            sSql += "from tp_personas" + Environment.NewLine;
            sSql += "where estado = 'A'" + Environment.NewLine;
            sSql += "and id_persona = " + iIdPersona_P;

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                sRespuesta[0] = dtConsulta.Rows[0][0].ToString();
                sRespuesta[1] = dtConsulta.Rows[0][1].ToString();
                return sRespuesta;
            }

            else
            {
                return sRespuesta;
            }
        }

        //Función para retornar la categria de producto
        private string[] obtenerTipoCategoria(string sCodigo_P)
        {
            string[] sRespuesta = new string[2];
            sRespuesta[0] = "Error";
            sRespuesta[1] = "Error";

            sSql = "";
            sSql += "select Codigo, Nombre Descripcion, 1 Correlativo" + Environment.NewLine;
            sSql += "from cv402_vw_categorias" + Environment.NewLine;
            sSql += "where codigo = " + sCodigo_P;

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                sRespuesta[0] = dtConsulta.Rows[0][0].ToString();
                sRespuesta[1] = dtConsulta.Rows[0][1].ToString();
                return sRespuesta;
            }
            else
            {
                return sRespuesta;
            }
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            txtCodigo.Enabled = true;
            grupoDatos.Enabled = false;
            bActualizar = false;
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            cmbEstado.Text = "ACTIVO";
            btnGrabar.Text = "Nuevo";
            dbAyudaCategoria.txtDatos.Text = "";
            dbAyudaCategoria.txtIdentificacion.Text = "";
            dbAyudaResponsable.txtDatos.Text = "";
            dbAyudaResponsable.txtIdentificacion.Text = "";
        }

        //Funcion para comprobar campos
        private bool comprobarCampos()
        {
            int iBandera = 0;

            if (txtCodigo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error: Debe ingresar el código de la bodega.";
                ok.ShowDialog();
                txtCodigo.Focus();
                iBandera = 1;
            }

            else if (txtDescripcion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error: Debe ingresar la descirpción de la bodega.";
                ok.ShowDialog();
                txtDescripcion.Focus();
                iBandera = 1;
            }

            else if (dbAyudaResponsable.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error: Debe seleccionar el responsable de la bodega.";
                ok.ShowDialog();
                dbAyudaResponsable.Focus();
                iBandera = 1;
            }

            else if (dbAyudaCategoria.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error: Debe seleccionar la categoría de la bodega.";
                ok.ShowDialog();
                dbAyudaCategoria.Focus();
                iBandera = 1;
            }

            if (iBandera == 1) return false; else return true;
        }

        //FUNCION PARA INSERTAR UN REGISTRO 
        private void insertarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo iniciar la transacción para guardar la información.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into cv402_bodegas (" + Environment.NewLine;
                sSql += "idempresa, id_persona, cg_empresa, codigo, descripcion," + Environment.NewLine;
                sSql += "tipo, categoria, estado, usuario_creacion, terminal_creacion," + Environment.NewLine;
                sSql += "fecha_creacion, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Convert.ToInt32(cmbEmpresa.SelectedValue) + ", " + dbAyudaResponsable.iId + ", ";
                sSql += Program.iCgEmpresa + ", '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + txtDescripcion.Text.Trim() + "', '" + sTipo + "', '" + sCategoria + "', 'A'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Código [" + txtCodigo.Text + "] ingresar éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
                llenarGrid();
                return; 
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                limpiarCampos();
                llenarGrid();
                return;
            }
        }

        //Función para actualizar el registro
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo iniciar la transacción para actualizar la información.";
                    ok.ShowDialog();
                    return;
                }

                int iIdPersona = obtenerIdPersona(dbAyudaResponsable.txtIdentificacion.Text);
                int iIdBodega = Convert.ToInt32(dgvBodega.CurrentRow.Cells[7].Value.ToString());
                string sCategoria = mostrarCodigo(dgvBodega.CurrentRow.Cells[3].Value.ToString());
                string sTipo = dbAyudaCategoria.txtIdentificacion.Text;

                sSql = "";
                sSql += "update cv402_bodegas set" + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "categoria = '" + sCategoria.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "tipo ='" + sTipo.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "usuario_ingreso= '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_ingreso= '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_ingreso = GETDATE()" + Environment.NewLine;
                sSql += "Where id_bodega = " + iIdBodega;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Código [" + txtCodigo.Text + "] actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
                llenarGrid();
                return; 
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { 
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        limpiarCampos();
                        llenarGrid();
                        return; 
                    }
        }

        //Función para obtener el id de la persona responsable de la bodega
        private int obtenerIdPersona(string sIdentificacion_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where Estado = 'A'" + Environment.NewLine;
                sSql += "and identificacion = '" + sIdentificacion_P + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //Función para anular el registro
        private void anularRegistro()
        {

            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo iniciar la transacción para eliminar la información.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update cv402_Bodegas set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "usuario_anula= '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula= '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula= GETDATE()" + Environment.NewLine;
                sSql += "where Codigo = '" + txtCodigo.Text.Trim() + "'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
                llenarGrid();
                return;                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                limpiarCampos();
                llenarGrid();
                return;
            }
        }

        #endregion

        //Función para enumerar las filas del grid
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvBodega.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        
        //Método cuando se da doble click
        private void dgvBodega_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            llenarVariables();
        }
        
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (btnGrabar.Text == "Nuevo")
            {
                bActualizar = false;
                grupoDatos.Enabled = true;
                txtCodigo.Focus();
                btnGrabar.Text = "Grabar";
            }

            else
            {
                if (comprobarCampos() == true)
                {
                    sCategoria = dbAyudaCategoria.txtIdentificacion.Text.Trim();

                    if (rdbMateriaPrima.Checked == true)
                    {
                        sTipo = "1";
                    }

                    else
                    {
                        sTipo = "2";
                    }


                    if (bActualizar == false)
                    {
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Desea Guardar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else if (bActualizar == true)
                    {
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Desea Actualizar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error: debe seleccionar el item a Anular.";
                ok.ShowDialog();
            }

            else
            {
                NuevoSiNo.lblMensaje.Text = "¿Desea Anular...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    anularRegistro();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
    }
}
