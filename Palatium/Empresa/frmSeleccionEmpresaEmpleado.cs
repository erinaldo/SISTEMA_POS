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
    public partial class frmSeleccionEmpresaEmpleado : Form
    {
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        Button[,] botonEmpresa = new Button[5, 3];
        Button[,] botonEmpleado = new Button[5, 7];

        string sSql;
        string sNombreEmpresa;

        bool bRespuesta;

        DataTable dtEmpresas;
        DataTable dtEmpleados;

        int iPosXEmpresas;
        int iPosYEmpresas;
        int iCuentaAyudaEmpresas;
        int iCuentaEmpresas;
        int iPosXEmpleados;
        int iPosYEmpleados;
        int iCuentaAyudaEmpleados;
        int iCuentaEmpleados;
        int iIdOrigenOrden;
        int iIdEmpresa;
        int iIdClienteEmpresarial;

        Button bempresa;
        Button bempleado;

        public frmSeleccionEmpresaEmpleado(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA MOSTRAR LOS BOTONES DE EMPRESA
        private bool mostrarEmpresas()
        {
            try
            {
                pnlEmpresa.Controls.Clear();
                iPosXEmpresas = 0;
                iPosYEmpresas = 0;
                iCuentaAyudaEmpresas = 0;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        botonEmpresa[i, j] = new Button();
                        botonEmpresa[i, j].Cursor = Cursors.Hand;
                        botonEmpresa[i, j].Click += new EventHandler(boton_clic_Empresa);
                        botonEmpresa[i, j].Size = new Size(130, 105);
                        botonEmpresa[i, j].Location = new Point(iPosXEmpresas, iPosYEmpresas);
                        botonEmpresa[i, j].BackColor = Color.Navy;
                        botonEmpresa[i, j].ForeColor = Color.White;
                        botonEmpresa[i, j].TextAlign = ContentAlignment.BottomCenter;
                        botonEmpresa[i, j].Image = Properties.Resources.icono_boton_empresa;
                        botonEmpresa[i, j].ImageAlign = ContentAlignment.TopCenter;
                        botonEmpresa[i, j].Font = new Font("Maiandra GD", 8.25f, FontStyle.Bold);
                        botonEmpresa[i, j].UseVisualStyleBackColor = false;
                        botonEmpresa[i, j].FlatAppearance.BorderSize = 2;
                        botonEmpresa[i, j].Tag = dtEmpresas.Rows[iCuentaEmpresas]["id_pos_cliente_empresarial"].ToString();
                        botonEmpresa[i, j].Text = dtEmpresas.Rows[iCuentaEmpresas]["empresa"].ToString();
                        botonEmpresa[i, j].AccessibleDescription = dtEmpresas.Rows[iCuentaEmpresas]["id_persona"].ToString();
                        pnlEmpresa.Controls.Add(botonEmpresa[i, j]);

                        iCuentaEmpresas++;
                        iCuentaAyudaEmpresas++;

                        if (j + 1 == 3)
                        {
                            iPosXEmpresas = 0;
                            iPosYEmpresas += 105;
                        }

                        else
                        {
                            iPosXEmpresas += 130;
                        }

                        if (dtEmpresas.Rows.Count == iCuentaEmpresas)
                        {
                            btnSiguienteEmpresa.Enabled = false;
                            break;
                        }
                    }

                    if (dtEmpresas.Rows.Count == iCuentaEmpresas)
                    {
                        btnSiguienteEmpresa.Enabled = false;
                        break;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //INSTRUCCION PARA CARGAR LOS DATOS DE LAS EMPRESAS
        private void cargarEmpresas(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial, TP.id_persona," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) empresa" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CE.estado = 'A'" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (TP.apellidos like '%" + txtFiltrarEmpresas.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%" + txtFiltrarEmpresas.Text.Trim() + "%')" + Environment.NewLine;
                }

                dtEmpresas = new DataTable();
                dtEmpresas.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtEmpresas, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                else
                {
                    iCuentaEmpresas = 0;

                    if (dtEmpresas.Rows.Count > 0)
                    {
                        if (dtEmpresas.Rows.Count > 15)
                        {
                            btnSiguienteEmpresa.Enabled = true;
                            btnAnteriorEmpresa.Visible = true;
                            btnSiguienteEmpresa.Visible = true;
                        }

                        else
                        {
                            btnSiguienteEmpresa.Enabled = false;
                            btnAnteriorEmpresa.Visible = false;
                            btnSiguienteEmpresa.Visible = false;
                        }

                        if (mostrarEmpresas())
                            ;
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentras registros de empresas en el sistema.";
                        ok.ShowDialog();
                        this.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO CLIC DEL BOTON EMPRESA
        public void boton_clic_Empresa(object sender, EventArgs e)
        {
            txtFiltrarEmpleados.Clear();

            bempresa = sender as Button;
            iIdEmpresa = Convert.ToInt32(bempresa.AccessibleDescription);
            sNombreEmpresa = bempresa.Text.Trim().ToUpper();
            lblNombreEmpresa.Text = "EMPRESA: " + bempresa.Text.Trim().ToUpper();
            iIdClienteEmpresarial = Convert.ToInt32(bempresa.Tag);
            cargarEmpleados(Convert.ToInt32(bempresa.Tag), 0);

            txtFiltrarEmpleados.Focus();
        }

        //FUNCION PARA MOSTRAR LOS BOTONES DE EMPLEADOS
        private bool mostrarEmpleados()
        {
            try
            {
                pnlEmpleados.Controls.Clear();
                iPosXEmpleados = 0;
                iPosYEmpleados = 0;
                iCuentaAyudaEmpleados = 0;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        botonEmpleado[i, j] = new Button();
                        botonEmpleado[i, j].Cursor = Cursors.Hand;
                        botonEmpleado[i, j].Click += new EventHandler(boton_clic_Empleado);
                        botonEmpleado[i, j].Size = new Size(145, 105);
                        botonEmpleado[i, j].Location = new Point(iPosXEmpleados, iPosYEmpleados);
                        botonEmpleado[i, j].BackColor = Color.FromArgb(128, 255, 128);
                        botonEmpleado[i, j].ForeColor = Color.Black;
                        botonEmpleado[i, j].TextAlign = ContentAlignment.BottomCenter;
                        botonEmpleado[i, j].Image = Properties.Resources.icono_boton_empleado;
                        botonEmpleado[i, j].ImageAlign = ContentAlignment.TopCenter;
                        botonEmpleado[i, j].Font = new Font("Maiandra GD", 8.25f, FontStyle.Bold);
                        botonEmpleado[i, j].Tag = dtEmpleados.Rows[iCuentaEmpleados]["id_persona"].ToString();
                        botonEmpleado[i, j].Text = dtEmpleados.Rows[iCuentaEmpleados]["empleado"].ToString();
                        botonEmpleado[i, j].AccessibleDescription = dtEmpleados.Rows[iCuentaEmpleados]["id_pos_cliente_empresarial"].ToString();
                        botonEmpleado[i, j].AccessibleName = dtEmpleados.Rows[iCuentaEmpleados]["aplica_almuerzo"].ToString();
                        pnlEmpleados.Controls.Add(botonEmpleado[i, j]);

                        iCuentaEmpleados++;
                        iCuentaAyudaEmpleados++;

                        if (j + 1 == 5)
                        {
                            iPosXEmpleados = 0;
                            iPosYEmpleados += 105;
                        }

                        else
                        {
                            iPosXEmpleados += 145;
                        }

                        if (dtEmpleados.Rows.Count == iCuentaEmpleados)
                        {
                            btnSiguienteEmpleado.Enabled = false;
                            break;
                        }
                    }

                    if (dtEmpleados.Rows.Count == iCuentaEmpleados)
                    {
                        btnSiguienteEmpleado.Enabled = false;
                        break;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CARGAR LOS EMPLEADOS
        private void cargarEmpleados(int iIdClienteEmpresarial_P, int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select EMP.id_persona, EMP.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' +  TP.apellidos) empleado, EMP.aplica_almuerzo" + Environment.NewLine;
                sSql += "from pos_empleado_cliente EMP INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = EMP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and EMP.estado = 'A'" + Environment.NewLine;
                sSql += "where EMP.id_pos_cliente_empresarial = " + iIdClienteEmpresarial_P + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (TP.apellidos like '%" + txtFiltrarEmpleados.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or TP.nombres like '%" + txtFiltrarEmpleados.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by TP.apellidos";
                
                dtEmpleados = new DataTable();
                dtEmpleados.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtEmpleados, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                else
                {
                    pnlEmpleados.Controls.Clear();
                    iCuentaEmpleados = 0;

                    if (dtEmpleados.Rows.Count > 0)
                    {
                        if (dtEmpleados.Rows.Count > 8)
                        {
                            btnSiguienteEmpleado.Enabled = true;
                            btnAnteriorEmpleado.Visible = true;
                            btnSiguienteEmpleado.Visible = true;
                        }

                        else
                        {
                            btnSiguienteEmpleado.Enabled = false;
                            btnAnteriorEmpleado.Visible = false;
                            btnSiguienteEmpleado.Visible = false;
                        }

                        if (mostrarEmpleados())
                            ;
                    }
                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se encuentras registros de empleados en la empresa seleccionada.";
                        ok.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION DEL BOTON EMPLEADOS
        public void boton_clic_Empleado(object sender, EventArgs e)
        {
            this.bempleado = sender as Button;
            Empresa.frmComandaClienteEmpresarial comanda = new frmComandaClienteEmpresarial(Convert.ToInt32(bempleado.Tag), sNombreEmpresa, bempleado.Text.ToUpper(), iIdEmpresa, iIdOrigenOrden);
            comanda.ShowDialog();

            //if (comanda.DialogResult == DialogResult.OK)
            //{
            //    //this.Close();
            //    txtFiltrarEmpleados.Clear();
            //    txtFiltrarEmpleados.Focus();
            //}
        }

        #endregion

        private void frmSeleccionEmpresaEmpleado_Load(object sender, EventArgs e)
        {
            cargarEmpresas(0);
        }

        private void btnSiguienteEmpresa_Click(object sender, EventArgs e)
        {
            btnAnteriorEmpresa.Enabled = true;
            mostrarEmpresas();
        }

        private void btnAnteriorEmpresa_Click(object sender, EventArgs e)
        {
            iCuentaEmpresas -= iCuentaAyudaEmpresas;

            if (iCuentaEmpresas <= 8)
            {
                btnAnteriorEmpresa.Enabled = false;
            }

            btnSiguienteEmpresa.Enabled = true;
            iCuentaEmpresas -= 8;

            mostrarEmpresas();
        }

        private void frmSeleccionEmpresaEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void btnSiguienteEmpleado_Click(object sender, EventArgs e)
        {
            btnAnteriorEmpleado.Enabled = true;
            mostrarEmpleados();
        }

        private void btnAnteriorEmpleado_Click(object sender, EventArgs e)
        {
            iCuentaEmpleados -= iCuentaAyudaEmpleados;

            if (iCuentaEmpleados <= 25)
            {
                btnAnteriorEmpleado.Enabled = false;
            }

            btnSiguienteEmpleado.Enabled = true;
            iCuentaEmpleados -= 25;

            mostrarEmpleados();
        }

        private void txtFiltrarEmpresas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                pnlEmpleados.Controls.Clear();

                if (txtFiltrarEmpresas.Text.Trim() == "")
                {
                    cargarEmpresas(0);
                }

                else
                {
                    cargarEmpresas(1);
                }
            }
        }

        private void txtFiltrarEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (iIdEmpresa == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No ha seleccionado ninguna empresa.";
                    ok.ShowDialog();
                }

                else
                {
                    //pnlEmpleados.Controls.Clear();

                    if (txtFiltrarEmpleados.Text.Trim() == "")
                    {
                        cargarEmpleados(iIdClienteEmpresarial, 0);
                    }

                    else
                    {
                        cargarEmpleados(iIdClienteEmpresarial, 1);
                    }
                }
            }
        }
    }
}
