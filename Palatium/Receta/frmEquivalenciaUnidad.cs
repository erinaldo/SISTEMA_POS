using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmEquivalenciaUnidad : Form
    {
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sEstado;

        DataTable dtConsulta;

        bool bRespuesta;
        bool bActualizar;

        int iIdRegistro;
        int iIdOrigen;
        int iIdEquivalencia;

        public frmEquivalenciaUnidad()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select EU.id_pos_equivalencia_unidad, EU.id_pos_unidad_inicial," + Environment.NewLine;
                sSql += "EU.id_pos_unidad_final, INICIAL.descripcion + ' A ' + FINAL.descripcion descripcion," + Environment.NewLine;
                sSql += "EU.factor_conversion, case EU.estado when 'A' then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from pos_equivalencia_unidad EU INNER JOIN" + Environment.NewLine;
                sSql += "pos_unidad INICIAL ON INICIAL.id_pos_unidad = EU.id_pos_unidad_inicial" + Environment.NewLine;
                sSql += "and EU.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and INICIAL.estado in ('A', 'N') INNER JOIN" + Environment.NewLine;
                sSql += "pos_unidad FINAL ON FINAL.id_pos_unidad = EU.id_pos_unidad_final" + Environment.NewLine;
                sSql += "and FINAL.estado in ('A', 'N')" + Environment.NewLine;

                if (iBandera == 1)
                {
                    sSql += "where INICIAL.descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or FINAL.descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by INICIAL.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_equivalencia_unidad"].ToString(),
                                              dtConsulta.Rows[i]["id_pos_unidad_inicial"].ToString(),
                                              dtConsulta.Rows[i]["id_pos_unidad_final"].ToString(),
                                              dtConsulta.Rows[i]["descripcion"].ToString(),
                                              dtConsulta.Rows[i]["factor_conversion"].ToString(),
                                              dtConsulta.Rows[i]["estado"].ToString()
                                                  );
                        }

                    }

                    dgvDatos.ClearSelection();
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS COMBOBOX DE UNIDADES ORIGEN
        private void llenarComboOrigen()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_unidad, descripcion" + Environment.NewLine;
                sSql += "from pos_unidad" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbUnidadOrigen.DisplayMember = "descripcion";
                    cmbUnidadOrigen.ValueMember = "id_pos_unidad";
                    cmbUnidadOrigen.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                } 
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS COMBOBOX DE UNIDADES DESTINO
        private void llenarComboDestino()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_unidad, descripcion" + Environment.NewLine;
                sSql += "from pos_unidad" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbUnidadDestino.DisplayMember = "descripcion";
                    cmbUnidadDestino.ValueMember = "id_pos_unidad";
                    cmbUnidadDestino.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }  

        //FUNCION PARA VALIDAR LOS COMBOBOX
        private void concatenarCombos()
        {
            try
            {
                txtDescripcion.Text = cmbUnidadOrigen.Text.Trim().ToUpper() + " - " + cmbUnidadDestino.Text.Trim().ToUpper();

                //if ((Convert.ToInt32(cmbUnidadOrigen.SelectedValue) == 0) && (Convert.ToInt32(cmbUnidadDestino.SelectedValue) == 0))
                //{
                //    txtDescripcion.Clear();
                //}

                //else if ((Convert.ToInt32(cmbUnidadOrigen.SelectedValue) != 0) && (Convert.ToInt32(cmbUnidadDestino.SelectedValue) == 0))
                //{
                //    txtDescripcion.Text = cmbUnidadOrigen.Text.Trim().ToUpper() + " -";
                //}

                //else if ((Convert.ToInt32(cmbUnidadOrigen.SelectedValue) == 0) && (Convert.ToInt32(cmbUnidadDestino.SelectedValue) != 0))
                //{
                //    txtDescripcion.Text = " - " + cmbUnidadDestino.Text.Trim().ToUpper();
                //}

                //else
                //{
                //    txtDescripcion.Text = cmbUnidadOrigen.Text.Trim().ToUpper() + " - " + cmbUnidadDestino.Text.Trim().ToUpper();
                //}
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA VERIFICAR REGISTROS DUPLICADOS
        private int comprobarRegistros()
        {
            try
            {
                int iBandera = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    iIdOrigen = Convert.ToInt32(dgvDatos.Rows[i].Cells[1].Value.ToString());
                    iIdEquivalencia = Convert.ToInt32(dgvDatos.Rows[i].Cells[2].Value.ToString());

                    if ((iIdOrigen == Convert.ToInt32(cmbUnidadOrigen.SelectedValue)) && (iIdEquivalencia == Convert.ToInt32(cmbUnidadDestino.SelectedValue)))
                    {
                        iBandera = 1;
                        break;
                    }
                }

                if (iBandera == 1)
                {
                    if (bActualizar == true)
                    {
                        iBandera = 0;
                    }
                }

                return iBandera;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA LIMPIAR CAMPOS
        private void limpiar()
        {
            cmbUnidadOrigen.SelectedIndexChanged -= new EventHandler(cmbUnidadOrigen_SelectedIndexChanged);
            cmbUnidadDestino.SelectedIndexChanged -= new EventHandler(cmbUnidadEquivalencia_SelectedIndexChanged);
            llenarComboOrigen();
            llenarComboDestino();
            cmbUnidadOrigen.SelectedIndexChanged += new EventHandler(cmbUnidadOrigen_SelectedIndexChanged);
            cmbUnidadDestino.SelectedIndexChanged += new EventHandler(cmbUnidadEquivalencia_SelectedIndexChanged);

            txtDescripcion.Text = cmbUnidadOrigen.Text.Trim().ToUpper() + " - " + cmbUnidadDestino.Text.Trim().ToUpper();

            llenarGrid(0);
            txtBuscar.Clear();
            //txtDescripcion.Clear();
            txtEquivalencia.Clear();
            btnNuevo.Text = "Nuevo";
            cmbEstado.Text = "ACTIVO";
            grupoDatos.Enabled = false;
            btnAnular.Enabled = false;
            bActualizar = false;
            txtBuscar.Focus();
        }

        //Función para guardar el registro
        private void insertarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                iIdOrigen = Convert.ToInt32(cmbUnidadOrigen.SelectedValue);
                iIdEquivalencia = Convert.ToInt32(cmbUnidadDestino.SelectedValue);

                sSql = "";
                sSql = "insert into pos_equivalencia_unidad (" + Environment.NewLine;
                sSql += "id_pos_unidad_inicial, id_pos_unidad_final, factor_conversion," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," +  Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdOrigen + ", " + iIdEquivalencia + ", '" + Convert.ToDecimal(txtEquivalencia.Text.Trim()) + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro Ingresado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //Función para actualizar el registro
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                iIdOrigen = Convert.ToInt32(cmbUnidadOrigen.SelectedValue);
                iIdEquivalencia = Convert.ToInt32(cmbUnidadDestino.SelectedValue);

                sSql = "";
                sSql += "update pos_equivalencia_unidad set" + Environment.NewLine;
                sSql += "id_pos_unidad_inicial = " + iIdOrigen + "," + Environment.NewLine;
                sSql += "id_pos_unidad_final = " + iIdEquivalencia + "," + Environment.NewLine;
                sSql += "factor_conversion = '" + txtEquivalencia.Text.Trim() + "'," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_pos_equivalencia_unidad = " + iIdRegistro + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //Función para anular el registro
        private void eliminarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql = "update pos_equivalencias set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_equivalencias = " + iIdRegistro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }        

        #endregion

        private void txtEquivalencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 5);            
        }

        private void frmEquivalenciaUnidad_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void cmbUnidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnidadDestino.SelectedIndexChanged -= new EventHandler(cmbUnidadEquivalencia_SelectedIndexChanged);
            concatenarCombos();
            cmbUnidadDestino.SelectedIndexChanged += new EventHandler(cmbUnidadEquivalencia_SelectedIndexChanged);
        }

        private void cmbUnidadEquivalencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnidadOrigen.SelectedIndexChanged -= new EventHandler(cmbUnidadOrigen_SelectedIndexChanged);
            concatenarCombos();
            cmbUnidadOrigen.SelectedIndexChanged += new EventHandler(cmbUnidadOrigen_SelectedIndexChanged);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiar();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNuevo.Text == "Nuevo")
                {
                    limpiar();
                    btnNuevo.Text = "Guardar";
                    grupoDatos.Enabled = true;
                    cmbUnidadOrigen.Focus();
                }

                else
                {
                    if (Convert.ToInt32(cmbUnidadOrigen.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione la unidad de origen para la conversión.";
                        ok.ShowDialog();
                        cmbUnidadOrigen.Focus();
                    }

                    else if (Convert.ToInt32(cmbUnidadDestino.SelectedValue) == 0)
                    {
                        ok.lblMensaje.Text = "Favor seleccione la unidad final para la conversión.";
                        ok.ShowDialog();
                        cmbUnidadDestino.Focus();
                    }

                    else if (comprobarRegistros() == 1)
                    {
                        ok.lblMensaje.Text = "Ya existe un valor de equivalencias con las unidades seleccionadas.";
                        ok.ShowDialog();
                        cmbUnidadOrigen.Focus();
                    }

                    else
                    {
                        if (cmbEstado.Text == "ACTIVO")
                        {
                            sEstado = "A";
                        }

                        else
                        {
                            sEstado = "N";
                        }

                        if (btnNuevo.Text == "Guardar")
                        {
                            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                insertarRegistro();
                            }
                        }

                        else if (btnNuevo.Text == "Actualizar")
                        {
                            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                actualizarRegistro();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grupoDatos.Enabled = true;
                iIdRegistro= Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                cmbUnidadOrigen.SelectedValue = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                cmbUnidadDestino.SelectedValue = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                txtEquivalencia.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();
                btnNuevo.Text = "Actualizar";
                btnAnular.Enabled = true;
                cmbEstado.Enabled = true;
                bActualizar = true;
                cmbUnidadOrigen.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
