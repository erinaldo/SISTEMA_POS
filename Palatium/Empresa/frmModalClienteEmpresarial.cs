using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmModalClienteEmpresarial : Form
    {
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sEstado;

        bool bRespuesta;

        DataTable dtConsulta;

        int iBandera;
        int iCuenta;

        public frmModalClienteEmpresarial()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR LA SENTENCIA DEL DBAYUDA
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select identificacion, ltrim(isnull(nombres, '') + '  ' + apellidos) razon_social, id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dBAyudaPersonas.Ver(sSql, "identificacion", 2, 0, 1);                
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL REGISTRO EN LA BASE DE DATOS
        private int consultarRegistroBase()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and id_persona = " + dBAyudaPersonas.iId;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCICÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CONTAR LOS REGISTROS EN EL DATAGRID
        private int contarRegistrosGrid()
        {
            try
            {
                int iCuenta_P = 0;
                int iId = dBAyudaPersonas.iId;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value.ToString()) == iId)
                    {
                        iCuenta_P++;
                        break;
                    }
                }

                return iCuenta_P;
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistros()
        {
            try
            {
                iBandera = 0;

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    return;
                }
                
                for (int index = 0; index < dgvDatos.Rows.Count; ++index)
                {
                    sSql = "";
                    sSql += "insert into pos_cliente_empresarial (" + Environment.NewLine;
                    sSql += "id_persona, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += dgvDatos.Rows[index].Cells[0].Value.ToString() + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        iBandera = 1;
                        break;
                    }
                }
                
                if (iBandera == 1)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                }
                
                else
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "Registros agregados éxitosamente.";
                    ok.ShowDialog();
                    DialogResult = DialogResult.OK;
                }                    
            }
            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmModalClienteEmpresarial_Load(object sender, EventArgs e)
        {
            llenarSentencias();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dBAyudaPersonas.txtIdentificacion.Text.Trim() == "")
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro.";
                ok.ShowDialog();
            }

            else
            {
                iCuenta = contarRegistrosGrid();

                if (iCuenta == -1)
                {
                    return;
                }

                if (iCuenta > 0)
                {
                    ok.lblMensaje.Text = "EL registro ha ingresar ya se encuentra seleccionado.";
                    ok.ShowDialog();
                }

                else
                {
                    iCuenta = consultarRegistroBase();

                    if (iCuenta == -1)
                    {
                        return;
                    }

                    if (iCuenta > 0)
                    {
                        ok.lblMensaje.Text = "EL registro a ingresar ya se encuentra registrado en el sistema.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        dgvDatos.Rows.Add(dBAyudaPersonas.iId.ToString(), dBAyudaPersonas.sCodigo, dBAyudaPersonas.txtDatos.Text);
                        dBAyudaPersonas.limpiar();
                        dgvDatos.ClearSelection();
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro.";
                ok.ShowDialog();
            }

            else
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea guardar los registros?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    insertarRegistros();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmModalClienteEmpresarial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                dgvDatos.ClearSelection();
            }

            else
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro para remover.";
                ok.ShowDialog();
            }
        }
    }
}
