using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Palatium.Revisar
{
    public partial class Revisar : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeEspere espere = new VentanasMensajes.frmMensajeEspere();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        int iCoordenadaX;
        int iCoordenadaY;
        int iIdPedido;
        int iNumeroPedido;
        int iNumeroPersonas;
        int iNumeroCuentaDiaria;
        int iOp;
        int iOrdenesJornada;
        int iCuenta;
        int iGeneraFactura;
        
        string iIdPosMesa;
        string sNombreCajero;
        string sNombreMesero;
        string sTipoOrden;
        string sFechaIngresoOrden;
        string sEstadoOrden;
        string sNombreMesa;
        string sSql;
        string sFechaActual;
        string sFechaActual2;
        string sFechaOrdenComanda;
        string sFechaAperturaCajero_P;
        string sCodigoOrigen_P;
        
        DataTable dtConsultaMesa;
        DataTable dtConsulta;
        DataTable dtComandas;

        Double DSumaDetalleOrden;

        double dbCantidad;
        double dbPrecioUnitario;
        double dbDescuento;
        double dbIva;
        double dbServicio;

        bool bRespuesta;

        //VARIABLES PARA RECUPERAR LA COMANDA
        double dbTotal;

        int iIdPosOrigenOrden_P;
        int iNumeroPersonas_P;
        int iIdMesa_P;
        int iIdPersona_P;
        int iIdCajero_P;
        int iIdMesero_P;
        int iIdJornada_P;
        int iCuentaComandas;
        int iCuentaAyudaComanda;
        int iPosXComanda;
        int iPosYComanda;
        int iPosXEditar;
        int iPosXPagar;
        int iPosXPrecuenta;

        string sOrigenOrden_P;
        string sNombreMesero_P;
        string sFechaOrden_P;

        Button[] botonComandas = new Button[4];
        Button[] botonPrecuenta = new Button[4];
        Button[] botonEditar = new Button[4];
        Button[] botonPagar = new Button[4];

        delegate void mostrarBotonesDelegado();
        
        public Revisar()
        {
            //Encerar el número de cuentas
            Program.TotalCuentasCanceladas = 0;
            Program.iTotalCuentasDomicilio = 0;
            Program.iTotalCuentasLlevar = 0;
            Program.iTotalCuentasMesa = 0;
            iOp = 1;

            InitializeComponent();
            ScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Scroll += (sender, e) => { pnlOrdenes.VerticalScroll.Value = vScrollBar1.Value; };

            pnlOrdenes.Controls.Add(vScrollBar1);

            //EXTRAMOS LOS REGISTROS DEL DIA CON LA JORNADA INGRESADA
            //sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");
            sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");            
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //FUNCION PARA RECUPERAR LOS DATOS DE LA COMANDA
        private void recuperarComanda(int iIdPedido_P)
        {
            try
            {
                //ACTUALIZACION ELVIS COMANDA
                //=======================================================================================================================
                ComandaNueva.frmComanda o = new ComandaNueva.frmComanda(iIdPedido_P, "OK");
                o.ShowDialog();
                this.Close();
                //=======================================================================================================================

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA REABRIR LA COMANDA
        private void reabrirComanda(int iIdPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "select fecha_orden, id_pos_jornada" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sFechaOrden_P = Convert.ToDateTime(dtConsulta.Rows[0][0].ToString()).ToString("yyyy/MM/dd");
                        iIdJornada_P = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo cargar la información de la comanda. Favor comuníquese con el administrador.";
                        ok.ShowDialog();

                        return;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFechaAperturaCajero_P = Convert.ToDateTime(Program.sFechaAperturaCajero).ToString("yyyy/MM/dd");

                if ((sFechaOrden_P == sFechaAperturaCajero_P) && (iIdJornada_P == Program.iJornadaCajero) && (Program.sEstadoCajero == "Abierta"))
                {
                    frmOpcionesReabrir r = new frmOpcionesReabrir(iIdPedido_P.ToString(), dbTotal);
                    AddOwnedForm(r);
                    r.ShowDialog();

                    if (r.DialogResult == DialogResult.OK)
                    {
                        if (Program.iBanderaReabrir == 1)
                        {
                            Program.iBanderaReabrir = 0;
                            this.Close();
                        }
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ya se encuentra un cierre de caja registrado para esta orden.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            } 
        }

        #endregion

        #region FUNCIONES NECESARIAS DEL USUARIO

        //FUNCION ACTIVA TECLADO
        private void activaTeclado()
        {
            //this.TecladoVirtual.SetShowTouchKeyboard(this.txtBusqueda, DevComponents.DotNetBar.Keyboard.TouchKeyboardStyle.Floating);
        }

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtBusqueda.Text = txtBusqueda.Text + sValor;
                txtBusqueda.Focus();
                txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA CONTAR CUANTAS ORDENES VA EN LA JORNADA EL CAJERO
        private void contarOrdenesCreadas()
        {
            try
            {
                //NECESITAMOS EL NUMERO DE CUENTAS REALIZADAS EN LA JORNADA YA SEA DIURNA O NOCTURA
                //Y QUE SEAN DE LA FECHA ACTUAL
                sFechaActual2 = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_jornada = " + Program.iJORNADA + Environment.NewLine;
                sSql += "and fecha_orden = '" + sFechaActual2 +"'" + Environment.NewLine;
                //sSql += "and id_localidad = " + Program.iIdLocalidad;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iOrdenesJornada = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES PARA MOSTRAR LOS BOTONES

        //FUNCION PARA CONSULTAR LAS COMANDAS
        private void cargarComandas()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    mostrarBotonesDelegado delegado = new mostrarBotonesDelegado(cargarComandas);
                    this.Invoke(delegado);
                }

                else
                {
                    sSql = "";
                    sSql += "select * from pos_vw_revisar_comandas" + Environment.NewLine;
                    sSql += "where fecha_pedido = '" + sFechaActual + "'" + Environment.NewLine;
                    sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;

                    if (iOp == 2)
                        sSql += "and estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;

                    else if (iOp == 3)
                        sSql += "and numero_pedido = " + Convert.ToInt32(txtBusqueda.Text.Trim()) + Environment.NewLine;

                    else if (iOp == 4)
                        sSql += "and codigo = '03'" + Environment.NewLine;

                    else if (iOp == 5)
                        sSql += "and codigo = '01'" + Environment.NewLine;

                    else if (iOp == 6)
                        sSql += "and codigo = '02'" + Environment.NewLine;

                    else if (iOp == 7)
                        sSql += "and cuenta = " + Convert.ToInt32(txtBusqueda.Text.Trim()) + Environment.NewLine;

                    else if (iOp == 8)
                        sSql += "and estado ='N'" + Environment.NewLine;

                    sSql += "order by id_pedido desc";

                    dtComandas = new DataTable();
                    dtComandas.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtComandas, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }

                    iCuentaComandas = 0;

                    if (dtComandas.Rows.Count == 0)
                    {
                        btnSubirComandas.Visible = false;
                        btnBajarComandas.Visible = false;
                        return;
                    }

                    if (dtComandas.Rows.Count > 4)
                    {
                        btnSubirComandas.Visible = true;
                        btnBajarComandas.Visible = true;
                        btnBajarComandas.Enabled = true;
                        btnSubirComandas.Enabled = false;
                    }

                    else
                    {
                        btnSubirComandas.Visible = false;
                        btnBajarComandas.Visible = false;
                        btnBajarComandas.Enabled = false;
                    }

                    if (crearBotones() == false)
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No se pudo crear los botones de las comandas.";
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

        //FUNCION PARA CREAR LOS BOTONES
        private bool crearBotones()
        {
            try
            {
                pnlOrdenes.Controls.Clear();
                iPosXComanda = 0;
                iPosYComanda = 0;
                iPosXEditar = 556;
                iPosXPrecuenta = 651;
                iPosXPagar = 746;
                iCuentaAyudaComanda = 0;

                for (int i = 0; i < 4; i++)
                {
                    iIdPedido = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["id_pedido"].ToString());
                    iNumeroPedido = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["numero_pedido"].ToString());
                    iIdPosMesa = dtComandas.Rows[iCuentaComandas]["id_pos_mesa"].ToString();
                    sNombreMesa = dtComandas.Rows[iCuentaComandas]["descripcion_mesa"].ToString();
                    sNombreCajero = dtComandas.Rows[iCuentaComandas]["cajero"].ToString();
                    sTipoOrden = dtComandas.Rows[iCuentaComandas]["tipo_comanda"].ToString();
                    sFechaIngresoOrden = dtComandas.Rows[iCuentaComandas]["fecha_apertura_orden"].ToString();
                    sEstadoOrden = dtComandas.Rows[iCuentaComandas]["estado_orden"].ToString();
                    iNumeroPersonas = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["numero_personas"].ToString());
                    iNumeroCuentaDiaria = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["cuenta"].ToString());
                    sNombreMesero = dtComandas.Rows[iCuentaComandas]["mesero"].ToString();
                    sFechaOrdenComanda = dtComandas.Rows[iCuentaComandas]["fecha_pedido"].ToString();
                    sCodigoOrigen_P = dtComandas.Rows[iCuentaComandas]["codigo"].ToString();
                    iGeneraFactura = Convert.ToInt32(dtComandas.Rows[iCuentaComandas]["genera_factura"].ToString());

                    sSql = "";
                    sSql += "select isnull(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto + DP.valor_iva + DP.valor_otro)), 0) valor" + Environment.NewLine;
                    sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado in ('A', 'N')" + Environment.NewLine;
                    sSql += "and DP.estado in ('A', 'N')" + Environment.NewLine;
                    sSql += "where CP.id_pedido = " + iIdPedido;

                    dtConsultaMesa = new DataTable();
                    dtConsultaMesa.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsultaMesa, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    Decimal dbTotalRecuperado;

                    if (dtConsultaMesa.Rows.Count == 0)
                        dbTotalRecuperado = 0;
                    else
                        dbTotalRecuperado = Convert.ToDecimal(dtConsultaMesa.Rows[0][0].ToString());

                    string sTextoBoton = "";
                    sTextoBoton += "N° Cuenta: " + iNumeroCuentaDiaria.ToString() + "   N° Orden: " + iNumeroPedido.ToString() + "  Mesero: " + sNombreMesero + Environment.NewLine;
                    sTextoBoton += "Total: " + "$ " + dbTotalRecuperado.ToString("N2") + Environment.NewLine;
                    sTextoBoton += "Fecha y Hora de la Orden: " + sFechaIngresoOrden + Environment.NewLine;

                    if (sCodigoOrigen_P == "01")
                        sTextoBoton += sTipoOrden + " # : " + sNombreMesa + " -  Nº de Personas: " + iNumeroPersonas.ToString() + Environment.NewLine;

                    else
                        sTextoBoton += sTipoOrden + Environment.NewLine;

                    sTextoBoton += "Orden " + sEstadoOrden;

                    botonComandas[i] = new Button();
                    botonComandas[i].Name = iIdPedido.ToString();
                    botonComandas[i].Click += boton_clic;
                    botonComandas[i].Size = new Size(555, 100);
                    botonComandas[i].Location = new Point(iPosXComanda, iPosYComanda);
                    botonComandas[i].Font = new Font("Maiandra GD", 11);
                    botonComandas[i].BackColor = Color.FromArgb(255, 224, 192);
                    botonComandas[i].Image = Palatium.Properties.Resources.comanda_revisar;
                    botonComandas[i].ImageAlign = ContentAlignment.MiddleLeft;
                    botonComandas[i].Text = sTextoBoton;

                    botonEditar[i] = new Button();
                    botonEditar[i].Name = iIdPedido.ToString();
                    botonEditar[i].Click += botonEditar_Click;
                    botonEditar[i].Image = Palatium.Properties.Resources.editar_comanda_revisar;
                    botonEditar[i].ImageAlign = ContentAlignment.TopCenter;
                    botonEditar[i].Size = new Size(95, 100);
                    botonEditar[i].Location = new Point(iPosXEditar, iPosYComanda);
                    botonEditar[i].Font = new Font("Maiandra GD", 11);
                    botonEditar[i].TextAlign = ContentAlignment.BottomCenter;
                    botonEditar[i].BackColor = Color.FromArgb(192, 192, 255);
                    botonEditar[i].AccessibleName = sEstadoOrden.ToUpper();
                    botonEditar[i].AccessibleDescription = sFechaOrdenComanda.ToUpper();

                    if ((sEstadoOrden.ToUpper()) == "PAGADA" || (sEstadoOrden.ToUpper() == "CERRADA"))
                    {
                        botonEditar[i].Text = "Reabrir" + Environment.NewLine + "Comanda";
                        botonEditar[i].Tag = 1;
                    }

                    if (sEstadoOrden.ToUpper() == "CANCELADA")
                    {
                        botonEditar[i].Text = "Reabrir" + Environment.NewLine + "Comanda";
                        botonEditar[i].Tag = 3;
                    }

                    else if ((sEstadoOrden.ToUpper() == "ABIERTA") || (sEstadoOrden.ToUpper() == "PRE-CUENTA"))
                    {
                        botonEditar[i].Text = "Editar" + Environment.NewLine + "Comanda";
                        botonEditar[i].Tag = 2;
                    }

                    botonPrecuenta[i] = new Button();
                    botonPrecuenta[i].Name = iIdPedido.ToString();
                    botonPrecuenta[i].Click += botonImprimir_clic;
                    botonPrecuenta[i].Image = Palatium.Properties.Resources.impresora_icono;
                    botonPrecuenta[i].ImageAlign = ContentAlignment.TopCenter;
                    botonPrecuenta[i].Size = new Size(95, 100);
                    botonPrecuenta[i].Location = new Point(iPosXPrecuenta, iPosYComanda);
                    botonPrecuenta[i].Font = new Font("Maiandra GD", 11);
                    botonPrecuenta[i].Text = "Imprimir" + Environment.NewLine + "Precuenta";
                    botonPrecuenta[i].TextAlign = ContentAlignment.BottomCenter;
                    botonPrecuenta[i].BackColor = Color.FromArgb(192, 255, 255);

                    botonPagar[i] = new Button();
                    botonPagar[i].Name = iIdPedido.ToString();
                    botonPagar[i].Click += botonPagar_clic;
                    botonPagar[i].Image = Palatium.Properties.Resources.icono_boton_pagar_cuenta;
                    botonPagar[i].ImageAlign = ContentAlignment.TopCenter;
                    botonPagar[i].Size = new Size(95, 100);
                    botonPagar[i].Location = new Point(iPosXPagar, iPosYComanda);
                    botonPagar[i].Font = new Font("Maiandra GD", 11);
                    botonPagar[i].Text = "Cobrar" + Environment.NewLine + "Comanda";
                    botonPagar[i].TextAlign = ContentAlignment.BottomCenter;
                    botonPagar[i].BackColor = Color.White;
                    botonPagar[i].AccessibleName = iGeneraFactura.ToString();

                    if ((sEstadoOrden.ToUpper() == "ABIERTA") || (sEstadoOrden.ToUpper() == "PRE-CUENTA"))
                        botonPagar[i].AccessibleDescription = "1";
                    else
                        botonPagar[i].AccessibleDescription = "0";

                    pnlOrdenes.Controls.Add(botonComandas[i]);
                    pnlOrdenes.Controls.Add(botonEditar[i]);
                    pnlOrdenes.Controls.Add(botonPrecuenta[i]);
                    pnlOrdenes.Controls.Add(botonPagar[i]);

                    iCuentaComandas++;
                    iCuentaAyudaComanda++;

                    if (dtComandas.Rows.Count == iCuentaComandas)
                    {
                        btnBajarComandas.Enabled = false;
                        break;
                    }

                    iPosYComanda += 100;
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

        //private void mostrarBotones()
        //{
        //    try
        //    {

        //        if (this.InvokeRequired)
        //        {
        //            mostrarBotonesDelegado delegado = new mostrarBotonesDelegado(mostrarBotones);
        //            this.Invoke(delegado);
        //        }

        //        else
        //        {
        //            int h = 1;
        //            int controlBoton1 = 1;
        //            //iCuenta = 0;
        //            //pnlOrdenes.Controls.Clear();
        //            iCoordenadaX = 0;
        //            iCoordenadaY = 0;

        //            //INSTRUCCIONES SQL UTILIZADAS EN EL FORMULARIO
        //            //EXTRAMOS LOS REGISTROS DEL DIA CON LA JORNADA INGRESADA

        //            //EXTRAMOS LOS REGISTROS DEL DIA CON LA JORNADA INGRESADA
                    


        //            dtConsulta = new DataTable();
        //            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //            if (bRespuesta == true)
        //            {
        //                if (dtConsulta.Rows.Count > 0)
        //                {
        //                    iCuenta = dtConsulta.Rows.Count;

        //                    Button[] boton = new Button[iCuenta];
        //                    Button[] boton1 = new Button[iCuenta];
        //                    Button[] botonEditar = new Button[iCuenta];


        //                    for (int i = 0; i < iCuenta; i++)
        //                    {
        //                        //DATOS CARGADOS EN EL DATATABLE RECUPERADO DE LA BASE DE DATOS
        //                        //USAREMOS VARIABLES PARA QUE EVITAR COLOCAR EL NOMBRE DE TODA LA INSTRUCCIÒN DEL DATATABLE
        //                        //==========================================================================================================

        //                        //POSICIÒN 0 = orden.id_pedido
        //                        iIdPedido = Convert.ToInt32(dtConsulta.Rows[i]["id_pedido"].ToString());
        //                        //POSICION 1 = numero_pedido                        
        //                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i]["numero_pedido"].ToString());
        //                        //POSICION 2 = id_pos_mesa
        //                        iIdPosMesa = dtConsulta.Rows[i]["id_pos_mesa"].ToString();
        //                        //POSICION 3 = mesa.descripcion
        //                        sNombreMesa = dtConsulta.Rows[i]["descripcion_mesa"].ToString();
        //                        //POSICION 4 = nombre del cajero
        //                        sNombreCajero = dtConsulta.Rows[i]["cajero"].ToString();
        //                        //POSICION 5 = tipo de orden
        //                        sTipoOrden = dtConsulta.Rows[i]["tipo_comanda"].ToString();
        //                        //POSICION 6 = fecha de apertura de la orden 
        //                        sFechaIngresoOrden = dtConsulta.Rows[i]["fecha_apertura_orden"].ToString();
        //                        //POSICION 7 = orden.estado_orden
        //                        sEstadoOrden = dtConsulta.Rows[i]["estado_orden"].ToString();
        //                        //POSICION 8 = numero_personas
        //                        iNumeroPersonas = Convert.ToInt32(dtConsulta.Rows[i]["numero_personas"].ToString());
        //                        //POSICION 9 = cuenta diarias
        //                        iNumeroCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[i]["cuenta"].ToString());
        //                        //POSICION 10 = nombre del mesero
        //                        sNombreMesero = dtConsulta.Rows[i]["mesero"].ToString();
        //                        //POSICION 11 = fecha_orden comanda
        //                        sFechaOrdenComanda = dtConsulta.Rows[i]["fecha_pedido"].ToString();
        //                        //codigo origen orden
        //                        sCodigoOrigen_P = dtConsulta.Rows[i]["codigo"].ToString();
        //                        //==========================================================================================================

        //                        boton[i] = new Button();
        //                        boton[i].Name = iIdPedido.ToString();
        //                        boton[i].Click += boton_clic;
        //                        boton[i].Size = new Size(620, 100);
        //                        boton[i].Location = new Point(iCoordenadaX, iCoordenadaY);
        //                        boton[i].Text = "" + h;
        //                        boton[i].Font = new Font("Maiandra GD", 11);
        //                        boton[i].BackColor = Color.FromArgb(255, 224, 192);
        //                        boton[i].Image = Palatium.Properties.Resources.comanda_revisar;
        //                        boton[i].ImageAlign = ContentAlignment.MiddleLeft;

        //                        iCoordenadaX += 621;

        //                        botonEditar[i] = new Button();
        //                        botonEditar[i].Name = iIdPedido.ToString();
        //                        botonEditar[i].Click += botonEditar_Click;
        //                        botonEditar[i].Image = Palatium.Properties.Resources.editar_comanda_revisar;
        //                        botonEditar[i].ImageAlign = ContentAlignment.TopCenter;
        //                        botonEditar[i].Size = new Size(95, 100);
        //                        botonEditar[i].Location = new Point(iCoordenadaX, iCoordenadaY);
        //                        botonEditar[i].Font = new Font("Maiandra GD", 11);
        //                        botonEditar[i].TextAlign = ContentAlignment.BottomCenter;
        //                        botonEditar[i].BackColor = Color.FromArgb(192, 192, 255);
        //                        botonEditar[i].AccessibleName = sEstadoOrden.ToUpper();
        //                        botonEditar[i].AccessibleDescription = sFechaOrdenComanda.ToUpper();

        //                        if ((sEstadoOrden.ToUpper()) == "PAGADA" || (sEstadoOrden.ToUpper() == "CERRADA"))
        //                        {
        //                            botonEditar[i].Text = "Reabrir" + Environment.NewLine + "Comanda";
        //                            botonEditar[i].Tag = 1;
        //                        }

        //                        if (sEstadoOrden.ToUpper() == "CANCELADA")
        //                        {
        //                            botonEditar[i].Text = "Reabrir" + Environment.NewLine + "Comanda";
        //                            botonEditar[i].Tag = 3;
        //                        }

        //                        else if ((sEstadoOrden.ToUpper() == "ABIERTA") || (sEstadoOrden.ToUpper() == "PRE-CUENTA"))
        //                        {
        //                            botonEditar[i].Text = "Editar" + Environment.NewLine + "Comanda";
        //                            botonEditar[i].Tag = 2;
        //                        }

        //                        iCoordenadaX += 95;

        //                        boton1[i] = new Button();
        //                        boton1[i].Name = iIdPedido.ToString();
        //                        boton1[i].Click += botonImprimir_clic;
        //                        boton1[i].Image = Palatium.Properties.Resources.impresora_icono;
        //                        boton1[i].ImageAlign = ContentAlignment.TopCenter;
        //                        boton1[i].Size = new Size(95, 100);
        //                        boton1[i].Location = new Point(iCoordenadaX, iCoordenadaY);
        //                        boton1[i].Font = new Font("Maiandra GD", 11);
        //                        boton1[i].Text = "Imprimir" + Environment.NewLine + "Precuenta";
        //                        boton1[i].TextAlign = ContentAlignment.BottomCenter;
        //                        boton1[i].BackColor = Color.FromArgb(192, 255, 255);
        //                        controlBoton1++;

        //                        iCoordenadaX = 0;
        //                        iCoordenadaY += 102;

        //                        sSql = "";
        //                        sSql += "select DP.cantidad * (DP.precio_unitario - DP.valor_dscto + DP.valor_iva + DP.valor_otro) valor" + Environment.NewLine;
        //                        sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
        //                        sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
        //                        sSql += "and CP.estado in ('A', 'N')" + Environment.NewLine;
        //                        sSql += "and DP.estado in ('A', 'N')" + Environment.NewLine;
        //                        sSql += "where CP.id_pedido = " + iIdPedido;

        //                        dtConsultaMesa = new DataTable();
        //                        dtConsultaMesa.Clear();

        //                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsultaMesa, sSql);

        //                        if (bRespuesta == false)
        //                        {
        //                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
        //                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
        //                            catchMensaje.ShowDialog();
        //                            return;
        //                        }

        //                        Decimal dbTotalRecuperado;

        //                        if (dtConsultaMesa.Rows.Count == 0)
        //                            dbTotalRecuperado = 0;
        //                        else
        //                            dbTotalRecuperado = Convert.ToDecimal(dtConsultaMesa.Rows[0][0].ToString());

        //                        string sTextoBoton = "";
        //                        sTextoBoton += "N° Cuenta: " + iNumeroCuentaDiaria.ToString() + "   N° Orden: " + iNumeroPedido.ToString() + "  Mesero: " + sNombreMesero + Environment.NewLine;
        //                        sTextoBoton += "Total: " + "$ " + dbTotalRecuperado.ToString("N2") + Environment.NewLine;
        //                        sTextoBoton += "Fecha y Hora de la Orden: " + sFechaIngresoOrden + Environment.NewLine;

        //                        if (sCodigoOrigen_P == "01")
        //                            sTextoBoton += sTipoOrden + " # : " + sNombreMesa + " -  Nº de Personas: " + iNumeroPersonas.ToString() + Environment.NewLine;

        //                        else
        //                            sTextoBoton += sTipoOrden + Environment.NewLine;

        //                        sTextoBoton += "Orden " + sEstadoOrden;
        //                        boton[i].Text = sTextoBoton;                              

        //                        pnlOrdenes.Controls.Add(boton1[i]);
        //                        pnlOrdenes.Controls.Add(botonEditar[i]);
        //                        pnlOrdenes.Controls.Add(boton[i]);
        //                        h++;
        //                    }
        //                }

        //                else
        //                {
        //                    //SE PUEDE OMITIR RELLENAR ESTA LINEA YA QUE NO DEVUELVE INFORMACION
        //                    //btnTotalOrdenes.Text = "Total de Órdenes \n0";
        //                }
        //            }

        //            else
        //            {
        //                catchMensaje.LblMensaje.Text = sSql;
        //                catchMensaje.ShowDialog();
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        catchMensaje.LblMensaje.Text = ex.Message;
        //        catchMensaje.ShowDialog();
        //    }
        //}

        private void botonImprimir_clic(object sender, EventArgs e)
        {
            //PARA ABRIR EL FORMULARIO ORIGINAL
            Button botonsel = sender as Button;

            sSql = "";
            sSql += "select estado_orden" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where id_pedido = " + Convert.ToInt32(botonsel.Name);

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(botonsel.Name, 1, dtConsulta.Rows[0][0].ToString());
                    precuenta.ShowDialog();    
                }
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ocurrió un problema al imprimir la precuenta.";
                ok.ShowDialog();
            }
        }

        private void botonEditar_Click(object sender, EventArgs e)
        {
            //PARA ABRIR EL FORMULARIO ORIGINAL
            Button btnEditarComanda = sender as Button;
            
            if (Convert.ToInt32(btnEditarComanda.Tag) == 1)
            {
                if (Program.iPuedeCobrar == 1)
                {
                    reabrirComanda(Convert.ToInt32(btnEditarComanda.Name));
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Su usuario no le permite reabrir la cuenta.";
                    ok.ShowDialog();
                }
            }

            else if (Convert.ToInt32(btnEditarComanda.Tag) == 3)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No se puede reabrir una comanda que ha sido cancelada o eliminada.";
                ok.ShowDialog();
            }

            else if (Convert.ToInt32(btnEditarComanda.Tag) == 2)
            {
                recuperarComanda(Convert.ToInt32(btnEditarComanda.Name));
            }
        }
        
        private void boton_clic(object sender, EventArgs e)
        {
            //PARA ABRIR EL FORMULARIO ORIGINAL
            Button botonsel = sender as Button;
            //Prueba p = new Prueba(botonsel.Name);
            Pedidos.frmVerReporteRevisar p = new Pedidos.frmVerReporteRevisar(botonsel.Name);
            p.ShowDialog();

            if (p.DialogResult == DialogResult.OK)
            {
                p.Close();
                this.Close();
            }
        }

        private void botonPagar_clic(object sender, EventArgs e)
        {
            if (Program.iPuedeCobrar == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No tiene permisos para ingresar en esta opción.";
                ok.ShowDialog();
                return;
            }

            //PARA ABRIR EL FORMULARIO ORIGINAL
            Button btnPagar_P = sender as Button;

            if (Convert.ToInt32(btnPagar_P.AccessibleDescription) == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "La comanda fue cobrada o cancelada." + Environment.NewLine + "No puede realizar cobros.";
                ok.ShowDialog();
                return;
            }

            if (Convert.ToInt32(btnPagar_P.AccessibleName) == 1)
            {
                Pedidos.frmCobros t = new Pedidos.frmCobros(btnPagar_P.Name);
                t.ShowDialog();

                if (t.DialogResult == DialogResult.OK)
                {
                    pnlOrdenes.Controls.Clear();
                    espere.AccionEjecutar = cargarComandas;
                    espere.ShowDialog();
                    btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
                }
            }

            else
            {
                Pedidos.frmCobrosEspeciales t = new Pedidos.frmCobrosEspeciales(btnPagar_P.Name);
                t.ShowDialog();

                if (t.DialogResult == DialogResult.OK)
                {
                    pnlOrdenes.Controls.Clear();
                    espere.AccionEjecutar = cargarComandas;
                    espere.ShowDialog();
                    btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
                }
            }
        }

        #endregion

        private void Revisar_Load(object sender, EventArgs e)
        {
            //Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            //redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);

            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            Program.dbDescuento = 0;
            Program.dbValorPorcentaje = 0;
            btnOrdenes.Text = "Órdenes Abiertas";
            sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");
            //btnFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);

            this.ActiveControl = txtBusqueda;
            pnlOrdenes.Controls.Clear();

            //using (VentanasMensajes.frmMensajeEspere espere = new VentanasMensajes.frmMensajeEspere())
            //{
            //    espere.AccionEjecutar = mostrarBotones;

            //    if (espere.ShowDialog() != DialogResult.OK)
            //    {
            //        MessageBox.Show("ERROR");
            //    }
            //}

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void Revisar_KeyDown(object sender, KeyEventArgs e)
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

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            btnOrdenes.Text = "Todas las Órdenes";

            if (txtBusqueda.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese datos para la búsqueda.";
                ok.ShowDialog();
            }

            else
            {
                sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");

                if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
                {
                    iOp = 3;
                }

                else
                {
                    iOp = 7;
                }

                pnlOrdenes.Controls.Clear();
                //mostrarBotones();

                //espere.AccionEjecutar = mostrarBotones;
                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();

                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();

                txtBusqueda.Text = "";
            }
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            //btnFecha.Text = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            pnlOrdenes.Controls.Clear();
            iOp = 1;
            
            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(-1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            //btnFecha.Text = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            pnlOrdenes.Controls.Clear();
            iOp = 1;

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnOrdenes_Click(object sender, EventArgs e)
        {
            sFechaActual = DateTime.Now.ToString("yyyy/MM/dd");
            //btnFecha.Text = sFechaActual.Substring(8, 2) + "/" + sFechaActual.Substring(5, 2) + "/" + sFechaActual.Substring(0, 4);
            btnFecha.Text = Convert.ToDateTime(sFechaActual).ToString("dd/MM/yyyy");

            if (btnOrdenes.Text == "Todas las Órdenes")
            {
                iOp = 1;
                pnlOrdenes.Controls.Clear();

                //espere.AccionEjecutar = mostrarBotones;
                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();

                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();

                btnOrdenes.Text = "Órdenes Abiertas";

            }
            else
            {
                pnlOrdenes.Controls.Clear();
                iOp = 2;

                //espere.AccionEjecutar = mostrarBotones;
                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();

                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();

                btnOrdenes.Text = "Todas las Órdenes";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDomicilio_Click_1(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 4;

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnMesa_Click_1(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 5;

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnLlevar_Click_1(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 6;

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnCanceladas_Click(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();
            iOp = 8;

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtBusqueda.Text.Length > 0)
            {

                str = txtBusqueda.Text.Substring(txtBusqueda.Text.Length - 1);
                loc = txtBusqueda.Text.Length;
                txtBusqueda.Text = txtBusqueda.Text.Remove(loc - 1, 1);
            }

            txtBusqueda.Focus();
            txtBusqueda.SelectionStart = txtBusqueda.Text.Trim().Length;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBusqueda_Click(sender, e);
            }
        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(btnFecha.Text);
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                btnFecha.Text = calendario.txtFecha.Text;
                sFechaActual = btnFecha.Text.Substring(6, 4) + "/" + btnFecha.Text.Substring(3, 2) + "/" + btnFecha.Text.Substring(0, 2);
                pnlOrdenes.Controls.Clear();
                iOp = 1;

                //espere.AccionEjecutar = mostrarBotones;
                espere.AccionEjecutar = cargarComandas;
                espere.ShowDialog();

                btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
            }
        }

        private void btnBusquedaOrdenCuenta_Click(object sender, EventArgs e)
        {
            if (btnBusquedaOrdenCuenta.Text == "Por número de orden")
            {
                btnBusquedaOrdenCuenta.Text = "Por número de cuenta";
            }

            else
            {
                btnBusquedaOrdenCuenta.Text = "Por número de orden";
            }
        }

        private void btnTotalOrdenes_Click(object sender, EventArgs e)
        {
            iOp = 1;
            pnlOrdenes.Controls.Clear();

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnMisOrdenes_Click(object sender, EventArgs e)
        {
            Pedidos.frmCobrarAlmuerzos almuerzo = new Pedidos.frmCobrarAlmuerzos();
            almuerzo.ShowDialog();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlOrdenes.Controls.Clear();

            //espere.AccionEjecutar = mostrarBotones;
            espere.AccionEjecutar = cargarComandas;
            espere.ShowDialog();

            btnTotalOrdenes.Text = "Total de Órdenes" + Environment.NewLine + dtComandas.Rows.Count.ToString();
        }

        private void btnSubirComandas_Click(object sender, EventArgs e)
        {
            iCuentaComandas -= iCuentaAyudaComanda;

            if (iCuentaComandas <= 4)
            {
                btnSubirComandas.Enabled = false;
            }

            btnBajarComandas.Enabled = true;
            iCuentaComandas -= 4;

            crearBotones();
        }

        private void btnBajarComandas_Click(object sender, EventArgs e)
        {
            btnSubirComandas.Enabled = true;
            crearBotones();
        }
    }
}