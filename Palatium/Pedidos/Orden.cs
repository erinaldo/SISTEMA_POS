using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Palatium
{
    public partial class Orden : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseCategorias vector = new Clases.ClaseCategorias();
        Clases.ClaseLimpiarArreglos limpiar = new Clases.ClaseLimpiarArreglos();
        Clases.ClaseItems items;
        Clases.ClaseSubcategoria subcategoria;
        Clases.ClasePreciosItems precios;
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        Bodega.ClaseMovimimentosBodega movimientos = new Bodega.ClaseMovimimentosBodega();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        DataGridView dgvOrigenPedido;

        DataTable dtConsulta;
        DataTable dtItems;
        DataTable dtSubReceta;
        DataTable dtReceta;
        DataTable dtLocalidad;

        Cursor cursor;

        bool bRespuesta;
        bool guardado;

        int iIdMesa;
        int iSecuenciaOrden;
        int iSecuenciaImpresion;
        int iSecuenciaEntrega;
        int iNumeroPedido;
        int iNumeroPedidoOrden;
        int iIdCabDespachos;
        int iIdEventoCobro;
        int iIdPedido;
        int iIdDespachoPedido;
        int iCgTipoDocumento = 2725;
        int icg_estado_dcto = 7460;
        int iCuentaDiaria;
        int iBandera = 0;
        int iIdMascaraItem;
        int iIdCabPedido_M;
        int iIdCabDespacho_M;
        int iIdDespachoPedido_M;
        int iIdEventoCobro_M;
        int iControlarSecuencia = 0;
        int controlPagoTarjetas = 0;
        int contadorCodigo = 0;
        int iIdOrigenOrden;
        int iIdDetPedido;
        int iConsumoAlimentos;
        int iNumeroPersonas;
        int iLongi;
        int iIdMovimientoBodega;
        int iIdPosReceta;
        int iIdBodega;
        int iCgClienteProveedor;
        int iTipoMovimiento;
        int iBandera2;
        int p, q;
        public int ayudante;
        int iIdProducto = 0;
        int iIdOrden_P;
        int iIdProducto_P;
        int iIdDocumentoCobrar;
        int iCuenta;
        int iIdPago;
        int iIdDocumentoPagado;
        int iVersionImpresionComanda;
        //int iControlImpresionCocina;
        int iIdPersona;
        int iIdCajero;
        int iIdMesero;
        int iIdPosSubReceta;
        int iBanderaDescargaStock;
        int iIdMovimientoStock;
        int iCgClienteProveedor_Sub;
        int iCgTipoMovimiento_Sub;
        int iIdLocalidadBodega;
        int iValorActualMovimiento;
        int iIdCabeceraMovimiento;
        int iIdBodegaInsumos;
        int iIdPromotor;
        int iIdRepartidor;
        
        //int[] iRespuesta;

        Int32 iIdCliente;

        string sTabla;
        string sCampo;
        string sPorcentajeDescuento = "0";
        string sfechaOrden;
        string referencia;
        string sSql;
        string sFecha;
        string sFechaConsulta;
        string reabrir;
        string sDescripcionOrigen;
        string sNombreMesero;
        string sAsignarNombreMesa = "";
        string sPagaIva_P;
        string sNombreProducto_P;
        string sCodigoClaseProducto;

        string sGuardarComentario;
        string sAnio;
        string sMes;        
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sNombreSubReceta;
        string sReferenciaExterna_Sub;
        string sNumeroMovimientoSecuencial;
        
        long iMaximo;

        public Button boton10;
        Button[,] boton  = new Button[20, 2];        
        Button[,] boton1 = new Button[Program.iArregloMenuFila, Program.iArregloMenuColumna];
        Button[,] boton2 = new Button[3, 2];
        Button[,] boton3 = new Button[8, 3];
        Button[,] boton4 = new Button[3, 2];
        Button[,] boton5 = new Button[10, 4];
        Button[,] btnSubcategoria = new Button[10, 2];
        Button botonMesa;
        Button botonMesa2;
        Button botonSel;
        Button botonSel1;
        Button botonSel4;

        Button botonIngresa;
        Button botonSalida;
        
        public double subtotal1 = 0;
        public double subtotal = 0;
        public double iva = 0;
        public double recargo = 0;
        public double total = 0;
        double dbPrecioProducto = 0;
        double dPrecioUnitario_P;
        double dCantidad_P;
        double dDescuento_P;
        double dIVA_P;
        double dServicio;
        double valPorcentajeDescuento;
        double dbCantidad;
        
        double dValorDescuento;
        double dbCantidadRecalcular;
        double dbPrecioRecalcular;
        double dbValorTotalRecalcular;

        Double dPorcentajeDescuento;
        Double dPorcentajeCalculado;

        //CONSTRUCTOR GENERAL PARA TODOS LOS ORIGENES
        public Orden(int iIdOrigenOrden, string sDescripcionOrigen, int iNumeroPersonas, int iIdMesa, int iIdPedido, string reabrir, int iIdPersona, int iIdCajero, int iIdMesero, string sNombreMesero, int iIdPromotor_P, int iIdRepartidor_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden;
            this.sDescripcionOrigen = sDescripcionOrigen;
            this.iNumeroPersonas = iNumeroPersonas;
            this.iIdMesa = iIdMesa;
            this.iIdPedido = iIdPedido;
            this.reabrir = reabrir;
            this.iIdPersona = iIdPersona;
            this.iIdCajero = iIdCajero;
            this.iIdMesero = iIdMesero;
            this.sNombreMesero = sNombreMesero;
            this.iIdRepartidor = iIdRepartidor_P;
            this.iIdPromotor = iIdPromotor_P;

            InitializeComponent();

            mostrarBotonesCategorias();
            extraerNumeroOrden();
            cargarParametrosComanda();
            //txt_numeropersonas.Text = iNumeroPedido.ToString();

            
        }

       /*   ACTUALIZACION 29-10-2018
        *   ELIMINACION DE CONSTRUCTORES
        *   FUNCIONES NUEVAS:
        *   - CARGAR PARAMETROS DE LA ORDEN
        */

        private void cargarParametrosComanda()
        {
            try
            {
                lblTipoOrden.Text = sDescripcionOrigen.ToUpper();
                txt_numeropersonas.Text = iNumeroPersonas.ToString();
                txt_mesero.Text = sNombreMesero.ToUpper();

                if (iIdMesa == 0)
                {
                    txt_numeromesa.Text = "NINGUNA";
                }

                else
                {
                    txt_numeromesa.Text = Program.sNombreMesa.ToUpper();
                }

                if (iIdPedido != 0)
                {
                    consultarDatosOrden();
                    calcularTotales();
                }

                //txt_numeromesa.Text = "MESA " + inum
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #region FUNCIONES PARA ANIMACION DE BOTONES EN LA COMANDA

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.MediumBlue;
            btnProceso.ForeColor = Color.White;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.DeepSkyBlue;
            btnProceso.ForeColor = Color.Black;
        }

        #endregion

        #region FUNCIONES PARA CREAR EL ARREGLO DE BOTONES

        //FUNCION PARA CREAR LOS BOTONES DE LAS CATEGORÍAS
        private void mostrarBotonesCategorias()
        {
            try
            {
                int contador = 0;
                pnlProductos.Controls.Clear();
                pnlCategorias.Controls.Clear();

                if (vector.llenarDatos() == true)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {

                            boton[i, j] = new Button();
                            boton[i, j].Cursor = Cursors.Hand;
                            boton[i, j].Click += boton_clic_categorias;
                            boton[i, j].Width = 110;
                            boton[i, j].Height = 60;
                            boton[i, j].Top = i * 60;
                            boton[i, j].Left = j * 110;

                            boton[i, j].BackColor = Color.Lime;
                            boton[i, j].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                            if (contador == vector.cuenta)
                            {
                                break;
                            }

                            boton[i, j].Tag = vector.categorias[contador].sSubcategoria;
                            boton[i, j].Text = vector.categorias[contador].iDescripcion;
                            boton[i, j].Name = vector.categorias[contador].sCodigo;
                            boton[i, j].AccessibleName = vector.categorias[contador].iCodigo;


                            this.Controls.Add(boton[i, j]);
                            pnlCategorias.Controls.Add(boton[i, j]);
                            contador++;
                        }
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS BOTONES DE LOS PRODUCTOS
        private void crearBotonesProductos(Palatium.Clases.ClaseItems items)
        {
            try
            {
                pnlProductos.Controls.Clear();
                pnlProductos.Visible = true;
                int h = 0;

                if (items.llenarDatos() == true)
                {

                    for (int i = 0; i < Program.iArregloMenuFila; i++)
                    {

                        for (int j = 0; j < Program.iArregloMenuColumna; j++)
                        {
                            boton1[i, j] = new Button();
                            boton1[i, j].Cursor = Cursors.Hand;
                            boton1[i, j].Click += boton_clic_productos;
                            boton1[i, j].Width = 140;
                            boton1[i, j].Height = 80;
                            boton1[i, j].Top = i * 80;
                            boton1[i, j].Left = j * 140;
                            boton1[i, j].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                            if (h == items.cuenta)
                            {
                                break;
                            }

                            boton1[i, j].Tag = items.items[h].iCodigo;
                            boton1[i, j].AccessibleName = items.items[h].iCodigoTipoProducto;
                            boton1[i, j].AccessibleDescription = items.items[h].iPagaIva;
                            boton1[i, j].BackColor = Color.FromArgb(255, 255, 128);

                            boton1[i, j].Text = items.items[h].iDescripcion;

                            this.Controls.Add(boton1[i, j]);

                            pnlProductos.Controls.Add(boton1[i, j]);
                            h++;
                        }
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para crear botones de subcategorias
        public void crearBotonesSubCategorias(Palatium.Clases.ClaseSubcategoria subcategoria)
        {
            try
            {
                pnlProductos.Controls.Clear();
                pnlProductos.Visible = true;
                //pnlProductos.Visible = false;
                int h = 0;

                if (subcategoria.llenarDatos() == true)
                {

                    //for (int i = 0; i < 10; i++)
                    for (int i = 0; i < 20; i++)
                    {

                        //for (int j = 0; j < 2; j++)
                        for (int j = 0; j < 1; j++)
                        {
                            btnSubcategoria[i, j] = new Button();
                            btnSubcategoria[i, j].Cursor = Cursors.Hand;
                            btnSubcategoria[i, j].Click += boton_clic_items_subcategorias;
                            btnSubcategoria[i, j].Width = 140;
                            btnSubcategoria[i, j].Height = 80;
                            btnSubcategoria[i, j].Top = i * 80;
                            btnSubcategoria[i, j].Left = j * 140;
                            btnSubcategoria[i, j].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                            if (h == subcategoria.cuenta)
                            {
                                break;
                            }

                            btnSubcategoria[i, j].Tag = h;                            
                            btnSubcategoria[i, j].BackColor = Color.FromArgb(255, 255, 128);
                            btnSubcategoria[i, j].Name = subcategoria.Subcategoria[h].sCodigo;
                            btnSubcategoria[i, j].Text = subcategoria.Subcategoria[h].sDescripcion;

                            this.Controls.Add(btnSubcategoria[i, j]);

                            pnlProductos.Controls.Add(btnSubcategoria[i, j]);
                            h++;
                        }

                        if (h == subcategoria.cuenta)
                        {
                            break;
                        }
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR LOS ITEMS DE LAS SUBCATEGORIAS
        public void crearBotonesItemsSubcategorias(Palatium.Clases.ClaseItems items)
        {
            try
            {
                int h = 0;

                if (items.llenarDatos() == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (h == items.cuenta)
                            {
                                break;
                            }

                            boton5[i, j] = new Button();
                            boton5[i, j].Cursor = Cursors.Hand;
                            boton5[i, j].Click += boton_clic_productos;
                            boton5[i, j].Width = 130;
                            boton5[i, j].Height = 80;
                            boton5[i, j].Top = i * 80;
                            boton5[i, j].Left = j * 130;
                            boton5[i, j].Visible = true;
                            boton5[i, j].Tag = items.items[h].iCodigo;
                            boton5[i, j].BackColor = Color.FromArgb(255, 146, 36);
                            boton5[i, j].Text = items.items[h].iDescripcion;
                            boton5[i, j].AccessibleName = items.items[h].iCodigoTipoProducto;
                            boton5[i, j].AccessibleDescription = items.items[h].iPagaIva;
                            boton5[i, j].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                            this.Controls.Add(boton5[i, j]);

                            panelItemsSubcategoria.Controls.Add(boton5[i, j]);
                            h++;
                        }

                        if (h == items.cuenta)
                        {
                            break;
                        }
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO CLIC DE LOS BOTONES DE LAS CATEGORÍAS
        private void boton_clic_categorias(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                botonSel = sender as Button;

                if (Convert.ToInt32(botonSel.Tag) == 0)
                {
                    lblEtiquetaFamilia.Text = botonSel.Text;
                    pnlProductos.Controls.Clear();
                    items = new Clases.ClaseItems(botonSel.Name, 3);
                    crearBotonesProductos(items);
                }
                else
                {
                    pnlProductos.Controls.Clear();
                    subcategoria = new Clases.ClaseSubcategoria(botonSel.AccessibleName);
                    crearBotonesSubCategorias(subcategoria);
                }

                this.Cursor = Cursors.Default;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO CLIC DE LOS BOTONES DE PRODUCTOS
        private void boton_clic_productos(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int subindice, subindice1;                
                botonSel1 = sender as Button;
                btn_mitad.BackColor = Color.FromName("DeepSkyBlue");

                precios = new Clases.ClasePreciosItems(Convert.ToInt32(botonSel1.Tag));

                subindice1 = int.Parse(botonSel.Tag.ToString());
                int existe = 0;
                float cantidad = 0;
                float valoru = 0;

                if (precios.llenarDatos() == true)
                {
                    subindice = int.Parse(botonSel1.Tag.ToString());

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        if (dgvPedido.Rows[i].Cells["producto"].Value.Equals(botonSel1.Text.ToString().Trim()) && Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) != 0.5
                             && Program.factorPrecio == 1 && Convert.ToInt32(dgvPedido.Rows[i].Cells["colSecuenciaImpresion"].Value) == iVersionImpresionComanda)
                        {
                            cantidad = float.Parse(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString().Trim());
                            cantidad = cantidad + 1;

                            dgvPedido.Rows[i].Cells["cantidad"].Value = cantidad;
                            valoru = float.Parse(dgvPedido.Rows[i].Cells["valuni"].Value.ToString().Trim());
                            dgvPedido.Rows[i].Cells["valor"].Value = (cantidad * valoru * Program.factorPrecio).ToString("N2");

                            Program.factorPrecio = 1;

                            existe = 1;

                        }
                    }


                    if (existe == 0)
                    {
                        //if (Program.sCodigoAsignadoOrigenOrden != "03")
                        //{
                            int x = 0;
                            x = dgvPedido.Rows.Add();
                            dgvPedido.Rows[x].Cells["cod"].Value = "1"; //contadorCodigo;
                            contadorCodigo++;
                            dgvPedido.Rows[x].Cells["producto"].Value = botonSel1.Text.ToString().Trim();
                            sNombreProducto_P = botonSel1.Text.ToString().Trim();
                            dgvPedido.Rows[x].Cells["cantidad"].Value = 1;
                            dgvPedido.Rows[x].Cells["guardada"].Value = 0;
                            dgvPedido.Rows[x].Cells["idProducto"].Value = botonSel1.Tag.ToString();
                            dgvPedido.Rows[x].Cells["cortesia"].Value = 0;
                            dgvPedido.Rows[x].Cells["motivoCortesia"].Value = "";
                            dgvPedido.Rows[x].Cells["cancelar"].Value = 0;
                            dgvPedido.Rows[x].Cells["motivoCancelacion"].Value = "";
                            dgvPedido.Rows[x].Cells["colIdMascara"].Value = "";
                            dgvPedido.Rows[x].Cells["colSecuenciaImpresion"].Value = iVersionImpresionComanda.ToString();
                            dgvPedido.Rows[x].Cells["colOrdenamiento"].Value = "";
                            dgvPedido.Rows[x].Cells["colIdOrden"].Value = "";
                            sPagaIva_P = botonSel1.AccessibleDescription.ToString().Trim();
                            dgvPedido.Rows[x].Cells["pagaIva"].Value = sPagaIva_P;
                            dgvPedido.Rows[x].Cells["tipoProducto"].Value = botonSel1.AccessibleName.ToString();

                            if (sPagaIva_P == "1")
                            {
                                dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Blue;
                                dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                                dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                                dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                            }

                            else
                            {
                                dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Purple;
                                dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                                dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                                dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                            }

                            cantidad = float.Parse("1");

                            dgvPedido.Rows[x].Cells["valuni"].Value = precios.precios[0].sPreciosItems;
                            valoru = float.Parse(precios.precios[0].sPreciosItems);
                            dgvPedido.Rows[x].Cells["valor"].Value = (cantidad * valoru * Program.factorPrecio).ToString("N2");

                            if (Program.factorPrecio != 1)
                            {
                                dgvPedido.Rows[x].Cells["cantidad"].Value = 0.5;
                                cantidad = 0.5f;
                            }
                        //}

                        //else
                        //{
                        //    valoru = float.Parse(precios.precios[0].sPreciosItems);

                        //    if (Program.factorPrecio != 1)
                        //    {
                        //        cantidad = float.Parse("0.5");
                        //        dgvPedido.Rows.Insert((dgvPedido.Rows.Count - 1), 0, 0.5, botonSel1.Text.ToString().Trim(), precios.precios[0].sPreciosItems, (cantidad * valoru * Program.factorPrecio).ToString("N2"), 1, 0, botonSel1.Tag.ToString(), 0, "", 0, "", "", iVersionImpresionComanda.ToString(), "", "", botonSel1.AccessibleName.ToString());
                        //        cantidad = 0.5f;

                        //        Program.factorPrecio = 1;

                        //    }
                        //    else
                        //    {
                        //        cantidad = float.Parse("1");
                        //        dgvPedido.Rows.Insert((dgvPedido.Rows.Count - 1), 0, 1, botonSel1.Text.ToString().Trim(), precios.precios[0].sPreciosItems, (cantidad * valoru * Program.factorPrecio).ToString("N2"), 1, 0, botonSel1.Tag.ToString(), 0, "", 0, "", "", iVersionImpresionComanda.ToString(), "", "", botonSel1.AccessibleName.ToString());

                        //    }
                        //}
                    }

                    Program.factorPrecio = 1;
                    dgvPedido.ClearSelection();

                    //CAMBIAMOS EL CODIGO A FUNCION
                    calcularTotales();

                }
                else
                {
                    ok.LblMensaje.Text = "No hay precio en este producto.";
                    ok.ShowDialog();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EVENTO CLIC DE LOS BOTONES DE SUBCATEGORIAS
        private void boton_clic_items_subcategorias(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                botonSel4 = sender as Button;

                //botonSel4.BackColor = Color.Cyan;
                panelItemsSubcategoria.Controls.Clear();
                lblEtiquetaFamilia.Text = botonSel4.Text;

                panelItemsSubcategoria.Visible = true;
                items = new Clases.ClaseItems(botonSel4.Name, 4);
                crearBotonesItemsSubcategorias(items);
                pnlProductos.Controls.Add(panelItemsSubcategoria);

                this.Cursor = Cursors.Default;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }      

        #endregion

        #region FUNCION PARA CREAR UN EGRESO DE PRODUCTO TERMINADO

        //FUNCION PARA RECUPERAR LOS DATOS DE LA LOCALIDAD
        private bool recuperarDatosLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select * from tp_localidades" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad;

                dtLocalidad = new DataTable();
                dtLocalidad.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtLocalidad, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //AQUI SE RECUPERA LA LOCALIDAD INSUMO
                sSql = "";
                sSql += "select id_localidad_insumo" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdLocalidadBodega = 0;
                }

                else
                {
                    iIdLocalidadBodega = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                //AQUI SE RECUPERA EL ID DE LA BODEGA DE INSUMOS
                sSql = "";
                sSql += "select id_bodega" + Environment.NewLine;
                sSql += "from tp_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidadBodega + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    iIdBodegaInsumos = 0;
                }

                else
                {
                    iIdBodegaInsumos = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;

            }
        }

        //FUNCION PARA CREAR EL NUMERO DE MOVIMIENTO
        private bool devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAnio, string sMes, string sCodigoCorrelativo)
        {
            try
            {
                iValorActualMovimiento = 0;
                sCodigo = "";
                sAnioCorto = sAnio.Substring(2, 2);

                if (sMes.Substring(0, 1) == "0")
                {
                    sMesCorto = sMes.Substring(1, 1);
                }

                else
                {
                    sMesCorto = sMes;
                }

                sSql = "";
                sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
                sSql += "where id_bodega = " + iIdBodega;

                dtConsulta = new DataTable();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCodigo = dtConsulta.Rows[0][0].ToString();

                string sReferencia;

                sReferencia = sTipoMovimiento + sCodigo + "_" + sAnio + "_" + sMesCorto + "_" + Program.iCgEmpresa;

                sSql = "";
                sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
                sSql += "where referencia = '" + sReferencia + "'" + Environment.NewLine;
                sSql += "and codigo_correlativo = '" + sCodigoCorrelativo + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    int iCorrelativo;

                    sSql = "";
                    sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                    sSql += "where codigo = 'BD'" + Environment.NewLine;
                    sSql += "and tabla = 'SYS$00022'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    iValorActualMovimiento = 1;
                    string sFechaDesde = sAnio + "-01-01";
                    string sFechaHasta = sAnio + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                    sSql = "";
                    sSql += "insert into tp_correlativos (" + Environment.NewLine;
                    sSql += "cg_sistema, codigo_correlativo, referencia, valido_desde," + Environment.NewLine;
                    sSql += "valido_hasta, valor_actual, desde, hasta, estado, origen_dato," + Environment.NewLine;
                    sSql += "numero_replica_trigger, estado_replica, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iCorrelativo + ",'" + sCodigoCorrelativo + "','" + sReferencia + "'," + Environment.NewLine;
                    sSql += "'" + sFechaDesde + "','" + sFechaHasta + "', " + (iValorActualMovimiento + 1) + "," + Environment.NewLine;
                    sSql += "0, 0, 'A', 1," + (iValorActualMovimiento + 1).ToString("N0") + ", 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    iValorActualMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual = " + (iValorActualMovimiento + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                sNumeroMovimientoSecuencial = sTipoMovimiento + sCodigo + sAnioCorto + sMes + iValorActualMovimiento.ToString().PadLeft(4, '0');

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA ELIMINAR LOS MOVIMIENTOS DE BODEGA
        private bool eliminarMovimientosBodega(int iIdPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_movimiento_bodega" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    int iIdRegistroMovimiento = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());

                    sSql = "";
                    sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where Id_Movimiento_Bodega=" + iIdRegistroMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR LOS MOVIMIENTOS DE PRODUCTO TERMINADO
        private bool insertarMovimientoProductoNoProcesado(Decimal dbCantidad_P)
        {
            try
            {
                string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = Program.sFechaSistema.ToString("yyyy");
                sMes = Program.sFechaSistema.ToString("MM");
                int iIdBodega_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_bodega"].ToString());

                if (devuelveCorrelativo("EG", iIdBodega_P, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }
                
                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_PT"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_PT"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_PT"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_PT"].ToString());
                string sReferenciaExterna_P = "ITEMS - ORDEN " + txt_Orden.Text.Trim();

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", " + iIdBodega_P + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencial + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);
                
                sSql = "";
                sSql += "insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdProducto_P + ", " + iIdCabeceraMovimiento + ", 546," + (dbCantidad_P * -1) + ", 'A')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }
        
        #endregion

        #region FUNCIONES CREAR UN EGRESO DE MATERIA PRIMA

        //FUNCION PARA OBTENER EL ID DE LA RECETA
        private bool consultarIdReceta(int iIdProducto_P, Decimal dbCantidadProductos_P, string sNombreProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(id_pos_receta, 0) id_pos_receta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iIdPosReceta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_receta"].ToString());

                if (iIdPosReceta == 0)
                {
                    return true;
                }

                sSql = "";
                sSql += "select * from pos_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdPosReceta + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtReceta = new DataTable();
                dtReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtReceta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

                string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = Program.sFechaSistema.ToString("yyyy");
                sMes = Program.sFechaSistema.ToString("MM");

                if (devuelveCorrelativo("EG", iIdBodegaInsumos, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }

                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_receta"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_receta"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega_receta"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_receta"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_receta"].ToString());
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + txt_Orden.Text.Trim();

                string sNumeroMovimientoSecuencialOriginal = sNumeroMovimientoSecuencial;

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdLocalidadBodega + ", " + iIdBodegaInsumos + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencialOriginal + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                iIdCabeceraMovimiento = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtReceta.Rows[i]["cantidad_bruta"].ToString());
                    iIdPosSubReceta = 0;

                    //VARIABLE PARA COCNSULTAR SI TIENE SUBRECETA
                    int iSubReceta_R = consultarSubReceta(iIdProducto_R);

                    if (iSubReceta_R == -1)
                    {
                        return false;
                    }

                    if (iSubReceta_R == 0)
                    {
                        sSql = "";
                        sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                        sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                        sSql += "Values (" + Environment.NewLine;
                        sSql += iIdProducto_R + ", " + iIdCabeceraMovimiento + ", 546," + (dbCantidadMateriaPrima_R * dbCantidadProductos_P * -1) + ", 'A')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }

                    else
                    {
                        if (insertarComponentesSubReceta(iSubReceta_R, dbCantidadProductos_P, sNombreProducto_P) == false)
                        {
                            return false;
                        }
                    }

                }


                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA VERIFICAR SI EL ITEM TIENE SUBRECETA
        private int consultarSubReceta(int iIdProducto_P)
        {
            try
            {
                sSql = "";
                sSql += "select TR.complementaria, R.id_pos_receta, R.descripcion" + Environment.NewLine;
                sSql += "from cv401_productos P, pos_receta R," + Environment.NewLine;
                sSql += "pos_tipo_receta TR" + Environment.NewLine;
                sSql += "where P.id_pos_receta = R.id_pos_receta" + Environment.NewLine;
                sSql += "and R.id_pos_tipo_receta = TR.id_pos_tipo_receta" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and R.estado = 'A'" + Environment.NewLine;
                sSql += "and TR.estado = 'A'" + Environment.NewLine;
                sSql += "and P.id_producto = " + iIdProducto_P;

                dtSubReceta = new DataTable();
                dtSubReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == true)
                {
                    if (dtSubReceta.Rows.Count > 0)
                    {
                        iIdPosSubReceta = Convert.ToInt32(dtSubReceta.Rows[0][1].ToString());
                        sNombreSubReceta = dtSubReceta.Rows[0][2].ToString().ToUpper();
                        return Convert.ToInt32(dtSubReceta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR LOS ITEMS DE LA SUBRECETA
        private bool insertarComponentesSubReceta(int iIdPosSubReceta_P, Decimal dbCantidadPedida_P, string sNombreProducto_P)
        {
            try
            {                
                sSql = "";
                sSql += "select id_producto, cantidad_bruta" + Environment.NewLine;
                sSql += "from pos_detalle_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdPosSubReceta_P + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtSubReceta = new DataTable();
                dtSubReceta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSubReceta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtSubReceta.Rows.Count == 0)
                {
                    return true;
                }

                //INSERTAR UNA CABECERA MOVIMIENTO PARA EL ITEM
                //-------------------------------------------------------------------------------------------------------------

                string sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sAnio = Program.sFechaSistema.ToString("yyyy");
                sMes = Program.sFechaSistema.ToString("MM");

                if (devuelveCorrelativo("EG", iIdBodegaInsumos, sAnio, sMes, "MOV") == false)
                {
                    return false;
                }

                int iCgClienteProveedor_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_cliente_proveedor_receta"].ToString());
                int iCgTipoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_tipo_movimiento_receta"].ToString());
                int iCgMotivoMovimiento_P = Convert.ToInt32(dtLocalidad.Rows[0]["cg_motivo_movimiento_bodega_receta"].ToString());
                int iIdAuxiliarSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_auxiliar_salida_receta"].ToString());
                int iIdPersonaSalida_P = Convert.ToInt32(dtLocalidad.Rows[0]["id_persona_salida_receta"].ToString());
                string sReferenciaExterna_P = sNombreProducto_P + " - ORDEN " + txt_Orden.Text.Trim();

                sSql = "";
                sSql += "insert into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad, id_bodega, cg_cliente_proveedor," + Environment.NewLine;
                sSql += "cg_tipo_movimiento, numero_movimiento, fecha, cg_moneda_base," + Environment.NewLine;
                sSql += "referencia_externa, externo, estado, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, id_pedido, cg_motivo_movimiento_bodega, orden_trabajo, orden_diseno," + Environment.NewLine;
                sSql += "Nota_Entrega, Observacion, id_auxiliar, id_persona, usuario_creacion, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdLocalidadBodega + ", " + iIdBodegaInsumos + "," + Environment.NewLine;
                sSql += iCgClienteProveedor_P + ", " + iCgTipoMovimiento_P + ", '" + sNumeroMovimientoSecuencial + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", '" + sReferenciaExterna_P + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[1] + "', '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', " + iIdPedido + ", " + iCgMotivoMovimiento_P + ", '', '', '', '', " + iIdAuxiliarSalida_P + ", " + Environment.NewLine;
                sSql += iIdPersonaSalida_P + ", '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCampo = "id_movimiento_bodega";
                sTabla = "cv402_cabecera_movimientos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                int iIdCabeceraMovimientoSubReceta = Convert.ToInt32(iMaximo);

                //RECORRER EL GRID DE LOS INGREDIENTES DE LA RECETA
                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i < dtSubReceta.Rows.Count; i++)
                {
                    int iIdProducto_R = Convert.ToInt32(dtSubReceta.Rows[i]["id_producto"].ToString());
                    Decimal dbCantidadMateriaPrima_R = Convert.ToDecimal(dtSubReceta.Rows[i]["cantidad_bruta"].ToString());

                    sSql = "";
                    sSql += "insert into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "id_producto, id_movimiento_bodega, cg_unidad_compra, cantidad, estado)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto_R + ", " + iIdCabeceraMovimientoSubReceta + ", 546," + (dbCantidadMateriaPrima_R * dbCantidadPedida_P * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }
        
        #endregion

        #region FUNCIONES NECESARIAS PARA RECUPERAR DATOS DE LA BASE

        //FUNCION PARA CONSULTAR EL PRODUCTO MOVILIZACION
        private void consultarMovilizacion()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, NP.nombre, PP.valor, P.paga_iva" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_precios_productos PP ON P.id_producto = PP.id_producto" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.id_producto = " + Program.iIdProductoDomicilio + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No se encuentra configurado el registro de Domicilio.";
                    ok.ShowDialog();
                    return;
                }

                int x = 0;
                x = dgvPedido.Rows.Add();
                dgvPedido.Rows[x].Cells["cod"].Value = "1"; //contadorCodigo;
                contadorCodigo++;
                dgvPedido.Rows[x].Cells["producto"].Value = dtConsulta.Rows[0]["nombre"].ToString().Trim().ToUpper();
                sNombreProducto_P = dtConsulta.Rows[0]["nombre"].ToString().Trim().ToUpper();
                dgvPedido.Rows[x].Cells["cantidad"].Value = 1;
                dgvPedido.Rows[x].Cells["guardada"].Value = 0;
                dgvPedido.Rows[x].Cells["idProducto"].Value = dtConsulta.Rows[0]["id_producto"].ToString().Trim();
                dgvPedido.Rows[x].Cells["cortesia"].Value = 0;
                dgvPedido.Rows[x].Cells["motivoCortesia"].Value = "";
                dgvPedido.Rows[x].Cells["cancelar"].Value = 0;
                dgvPedido.Rows[x].Cells["motivoCancelacion"].Value = "";
                dgvPedido.Rows[x].Cells["colIdMascara"].Value = "";
                dgvPedido.Rows[x].Cells["colSecuenciaImpresion"].Value = iVersionImpresionComanda.ToString();
                dgvPedido.Rows[x].Cells["colOrdenamiento"].Value = "";
                dgvPedido.Rows[x].Cells["colIdOrden"].Value = "";
                sPagaIva_P = dtConsulta.Rows[0]["paga_iva"].ToString().Trim();
                dgvPedido.Rows[x].Cells["pagaIva"].Value = sPagaIva_P;
                dgvPedido.Rows[x].Cells["tipoProducto"].Value = "00";

                if (sPagaIva_P == "1")
                {
                    dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Blue;
                    dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                    dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                    dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                }

                else
                {
                    dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Purple;
                    dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                    dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                    dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                }

                dgvPedido.Rows[x].Cells["valuni"].Value = dtConsulta.Rows[0]["valor"].ToString().Trim();
                Decimal dbValorUni_R = Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString().Trim());
                dgvPedido.Rows[x].Cells["valor"].Value = dbValorUni_R.ToString("N2");

                calcularTotales();

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE VERSION DE LA COMANDA
        private void versionImpresion()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(max(isnull(secuencia, 1)), 0) maximo" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iVersionImpresionComanda = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
                    }

                    else
                    {
                        iVersionImpresionComanda = 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL ULTIMO NUMERO DE DE CUENTA INGRESADO
        private void extraerNumeroCuenta()
        {
            try
            {
                //sFechaConsulta = DateTime.Now.ToString("yyyy/MM/dd");
                sFechaConsulta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select isnull(max(cuenta), 0) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + sFechaConsulta + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) + 1;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL NUMERO DE ORDEN QUE SIGUE
        private void extraerNumeroOrden()
        {
            try
            {
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txt_Orden.Text = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la extraer el número de pedido";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    this.Close();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        //CONSULTA DE DATOS PARA LLENAR LAS CAJAS DE TEXTO
        private void consultarDatosOrden()
        {
            try
            {
                sSql = "";
                sSql += "select NP.numero_pedido, O.descripcion,CP. id_pos_mesa, isnull(M.descripcion,'NINGUNA') descripcion_mesa," + Environment.NewLine;
                sSql += "CP.numero_personas, CP.id_pos_cajero, C.descripcion, CP.estado_orden, CP.porcentaje_dscto," + Environment.NewLine;
                sSql += "CP.id_pos_mesero, MS.descripcion, isnull(CP.consumo_alimentos, 0) consumo_alimentos" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos as CP inner join" + Environment.NewLine;
                sSql += "pos_origen_orden as O on O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and O.estado = 'A' inner join" + Environment.NewLine;
                sSql += "pos_cajero as C on C.id_pos_cajero = CP.id_pos_cajero" + Environment.NewLine;
                sSql += "and C.estado = 'A' inner join" + Environment.NewLine;
                sSql += "pos_mesero as MS on MS.id_pos_mesero = CP.id_pos_mesero" + Environment.NewLine;
                sSql += "and MS.estado = 'A' inner join" + Environment.NewLine;
                sSql += "cv403_numero_cab_pedido as NP on NP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and NP.estado = 'A' left outer join" + Environment.NewLine;
                sSql += "pos_mesa as M on M.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and M.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "order by CP.id_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                int iPorcentaje;

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txt_Orden.Text = dtConsulta.Rows[0][0].ToString();
                        txt_numeromesa.Text = dtConsulta.Rows[0][3].ToString();
                        txt_numeropersonas.Text = dtConsulta.Rows[0][4].ToString();
                        txt_mesero.Text = dtConsulta.Rows[0][10].ToString();
                        iPorcentaje = Convert.ToInt32(dtConsulta.Rows[0][8].ToString());
                        sPorcentajeDescuento = dtConsulta.Rows[0][8].ToString();
                        lblPorcentajeDescuento.Text = iPorcentaje.ToString() + "%";
                        iConsumoAlimentos = Convert.ToInt32(dtConsulta.Rows[0][11].ToString());

                        if ((dtConsulta.Rows[0][2].ToString() == null) || (dtConsulta.Rows[0][2].ToString() == ""))
                        {
                            iIdMesa = 0;
                        }
                        else
                        {
                            iIdMesa = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                        }

                        //ENVIAMOS A LA FUNCION PARA LLENAR EL GRID
                        cargarDetalleGrid();

                        dgvPedido.ClearSelection();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = sSql;
                    ok.ShowDialog();
                    this.Close();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL DETALLE DE LA ORDEN EN EL DATAGRID
        private void cargarDetalleGrid()
        {
            sSql = "";
            sSql += "select * from pos_vw_recuperar_comanda" + Environment.NewLine;
            sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
            sSql += "order by id_det_pedido";

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    int x = 0;
                    Double suma = 0;
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        x = dgvPedido.Rows.Add();
                        //int cantidad = Convert.ToInt32();

                        if ((dtConsulta.Rows[i][10].ToString() == "") && (dtConsulta.Rows[i][13].ToString() == "0"))
                        {
                            dgvPedido[2, x].Value = dtConsulta.Rows[i][3].ToString();
                            sNombreProducto_P = dtConsulta.Rows[i][3].ToString();
                        }

                        else
                        {
                            dgvPedido[2, x].Value = dtConsulta.Rows[i][10].ToString();
                            sNombreProducto_P = dtConsulta.Rows[i][10].ToString();
                        }

                        //double precio = Convert.ToDouble(dtConsulta.Rows[i][5].ToString());

                        Double precio = 0;

                        int longitud = lblPorcentajeDescuento.Text.Length;

                        //EN ESTA SECCION VALIDAR LOS DATOS A MOSTRAR CON CORTESIAS, DESCUENTOS, CANCELACION DE PRODUCTOS

                        precio = Convert.ToDouble(dtConsulta.Rows[i][1].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][4].ToString());


                        //dgvPedido[0, x].Value = precio.ToString("N2");
                        dgvPedido[0, x].Value = Convert.ToDouble(dtConsulta.Rows[i][5].ToString());

                        dgvPedido[5, x].Value = Convert.ToInt32(dtConsulta.Rows[i][0].ToString()).ToString();
                        dgvPedido[1, x].Value = dtConsulta.Rows[i][1].ToString();
                        dgvPedido[7, x].Value = dtConsulta.Rows[i][2].ToString();
                        dgvPedido[3, x].Value = dtConsulta.Rows[i][4].ToString();

                        dgvPedido[4, x].Value = precio.ToString("N2");
                        dgvPedido[8, x].Value = dtConsulta.Rows[i][11].ToString();
                        dgvPedido[9, x].Value = dtConsulta.Rows[i][7].ToString();
                        dgvPedido[10, x].Value = dtConsulta.Rows[i][12].ToString();
                        dgvPedido[11, x].Value = dtConsulta.Rows[i][9].ToString();
                        dgvPedido[12, x].Value = dtConsulta.Rows[i][13].ToString();
                        dgvPedido[13, x].Value = dtConsulta.Rows[i][14].ToString();
                        dgvPedido[14, x].Value = dtConsulta.Rows[i][15].ToString();
                        dgvPedido[15, x].Value = dtConsulta.Rows[i][16].ToString();

                        sPagaIva_P = dtConsulta.Rows[i][17].ToString();
                        dgvPedido[16, x].Value = sPagaIva_P;

                        dgvPedido[17, x].Value = dtConsulta.Rows[i]["codigo"].ToString();

                        if (sPagaIva_P == "1")
                        {
                            dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Blue;
                            dgvPedido[1, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                            dgvPedido[2, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                            dgvPedido[4, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                        }

                        else
                        {
                            dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Purple;
                            dgvPedido[1, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                            dgvPedido[2, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                            dgvPedido[4, x].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                        }

                        suma = suma + Convert.ToDouble(dtConsulta.Rows[i][4].ToString());

                        //LLEVAR LA MATRIZ DE DETALLE ITEMS CON LOS DATOS INGRESADOS EN LOS DETALLES EN CASO DE QUE SI HAYA

                        //sSql = "select detalle from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + " and estado = 'A'";
                        sSql = "";
                        sSql += "select PD.detalle, P.id_producto" + Environment.NewLine;
                        sSql += "from pos_det_pedido_detalle PD, cv403_det_pedidos DP, cv401_productos P" + Environment.NewLine;
                        sSql += "where PD.id_det_pedido = DP.id_det_pedido " + Environment.NewLine;
                        sSql += "and DP.id_producto = P.id_producto " + Environment.NewLine;
                        sSql += "and PD.id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                        sSql += "and P.estado = 'A'" + Environment.NewLine;
                        sSql += "and DP.estado = 'A'" + Environment.NewLine;
                        sSql += "and PD.estado = 'A'";

                        dtItems = new DataTable();
                        dtItems.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtItems, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtItems.Rows.Count > 0)
                            {
                                Program.sDetallesItems[Program.iContadorDetalle, 0] = dtItems.Rows[0][1].ToString();

                                for (int j = 1; j <= dtItems.Rows.Count; j++)
                                {
                                    Program.sDetallesItems[Program.iContadorDetalle, j] = dtItems.Rows[j - 1][0].ToString();
                                }

                                Program.iContadorDetalle++;
                            }
                        }

                        else
                        {

                        }

                    }

                    if (Program.dbValorPorcentaje != 0)
                    {
                        //    txt_subtotal.Text = suma.ToString("N2");
                        //    txt_iva.Text = (suma * Program.iva).ToString("N2");
                        //    txt_servicio.Text = (suma * Program.recargo).ToString("N2");
                        //    txt_total.Text = (suma + (suma * Program.iva) + (suma * Program.recargo)).ToString("N2");
                        //}

                        //else
                        //{
                        Program.dbDescuento = Program.dbValorPorcentaje / 100;
                        Double dNuevoTotal = suma * Program.dbDescuento;
                        //txt_subtotal.Text = suma.ToString("N2");
                        //txt_iva.Text = (dNuevoTotal * Program.iva).ToString("N2");
                        //txt_servicio.Text = (suma * Program.recargo).ToString("N2");
                        //txt_total.Text = (dNuevoTotal + (dNuevoTotal * Program.iva) + (dNuevoTotal * Program.recargo)).ToString("N2");

                        //txtDescuento.Text = (suma - dNuevoTotal).ToString("N2");

                        //txtDescuento.Visible = true;
                        //lblDescuento.Visible = true;

                    }

                    if (Program.iReimprimirCocina == 0)
                    {
                        chkImprimirCocina.Checked = false;
                    }

                    else
                    {
                        chkImprimirCocina.Checked = true;
                    }
                }
            }

            else
            {
                ok.LblMensaje.Text = sSql;
                ok.ShowDialog();
                this.Close();
            }
        }

        #endregion

        #region FUNCIONES IMPORTANTES PARA INSERTAR EN LA BASE DE DATOS

        //FUNCION PARA VERIFICAR SI EXISTEN YA PAGOS INGRESADOS EN LA ORDEN PADRE
        private bool verificarPagosExistente(int iIdOrdenRecibir)
        {
            try
            {
                //EXTRAER EL ID DE LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdOrdenRecibir + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el id de la tabla" + Environment.NewLine + "cv403_dctos_por_cobrar.";
                    ok.ShowDialog();
                    return false;
                }


                //VERIFICAR SI EXISTE UN DOCUMENTO PAGADO PARA DAR DE BAJA SUS DEPENDIENTES
                iCuenta = 0;

                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from  cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al extraer el número de registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                    ok.ShowDialog();
                    return false;
                }

                if (iCuenta > 0)
                {
                    /* SE PROCEDE A DAR DE BAJA LOS REGISTROS DE LAS TABLAS:
                     * CV403_PAGOS
                     * CV403_DOCUMENTOS_PAGOS
                     * CV403_NUMEROS_PAGOS
                     * CV403_DOCUMENTOS_PAGADOS
                    */

                    sSql = "";
                    sSql += "select id_pago, id_documento_pagado" + Environment.NewLine;
                    sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                    sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iIdPago = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                            iIdDocumentoPagado = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al extraer los registros de la tabla" + Environment.NewLine + "cv403_documentos_pagados.";
                        ok.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_PAGOS
                    sSql = "";
                    sSql += "update cv403_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_NUMEROS_PAGOS
                    sSql = "";
                    sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_pago = " + iIdPago;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZAR A ESTADO "E" EN LA TABLA CV403_DOCUMENTOS_PAGADOS
                    sSql = "";
                    sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_documento_pagado = " + iIdDocumentoPagado;

                    //EJECUTA LA INSTRUCCION DE ACTUALIZACION (ELIMINACION)
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //Función para cancelar un producto
        private void cancelarItems(int contador)
        {
            try
            {
                if (reabrir == "OK")
                {
                    sSql = "";
                    sSql += "select id_det_pedido" + Environment.NewLine;
                    sSql += "from CV403_det_pedidos where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and id_producto = " + Convert.ToInt32(dgvPedido.Rows[contador].Cells["idProducto"].Value) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and comentario like '%CANCELADO%'";
                }
                else
                {
                    sSql = "";
                    sSql += "select id_det_pedido" + Environment.NewLine;
                    sSql += "from CV403_det_pedidos where id_pedido = " + Convert.ToInt32(iIdPedido) + Environment.NewLine;
                    sSql += "and id_producto = " + Convert.ToInt32(dgvPedido.Rows[contador].Cells["idProducto"].Value) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and comentario like '%CANCELADO%'";
                }

                DataTable dtCortesia = new DataTable();
                dtCortesia.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCortesia, sSql);

                if (bRespuesta == true)
                {
                    int iIdDetPedido = Convert.ToInt32(dtCortesia.Rows[0][0].ToString());

                    if (reabrir == "OK" || reabrir == "DIVIDIDO")
                    {
                        sSql = "";
                        sSql += "insert into pos_cancelacion_productos" + Environment.NewLine;
                        sSql += "(id_pedido, id_det_pedido, motivo_cancelacion, estado," + Environment.NewLine;
                        sSql += "usuario_ingreso, terminal_ingreso, fecha_ingreso)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += iIdPedido + ", " + iIdDetPedido + "," + Environment.NewLine;
                        sSql += "'" + dgvPedido.Rows[contador].Cells["motivoCancelacion"].Value.ToString() + "', 'A'," + Environment.NewLine;
                        sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE())";
                    }
                    else
                    {
                        sSql = "";
                        sSql += "insert into pos_cancelacion_productos" + Environment.NewLine;
                        sSql += "(id_pedido, id_det_pedido, motivo_cancelacion, estado," + Environment.NewLine;
                        sSql += "usuario_ingreso, terminal_ingreso, fecha_ingreso)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += iIdPedido + ", " + iIdDetPedido + "," + Environment.NewLine;
                        sSql += "'" + dgvPedido.Rows[contador].Cells["motivoCancelacion"].Value.ToString() + "', 'A'," + Environment.NewLine;
                        sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE())";
                    }

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }
                else
                {
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                goto fin;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                ok.LblMensaje.Text = "La Orden todavía no ha sido guardada";
                ok.ShowDialog();
                Program.iBanderaCortesia = 0;
            }

        fin:
            {
                Program.iBanderaCortesia = 1;
                //this.Close();
            }

        }

        //Función para insertar una Cortesía
        private void insertarCortesia(int contador)
        {
            try
            {
                if ((reabrir == "OK") || (reabrir == "DIVIDIDO"))
                {
                    sSql = "";
                    sSql += "select id_det_pedido" + Environment.NewLine;
                    sSql += "from CV403_det_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and id_producto = " + Convert.ToInt32(dgvPedido.Rows[contador].Cells["idProducto"].Value) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and comentario like '%CORTESIA%'";
                }
                else
                {
                    sSql = "";
                    sSql += "select id_det_pedido" + Environment.NewLine;
                    sSql += "from CV403_det_pedidos" + Environment.NewLine;
                    sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                    sSql += "and id_producto = " + Convert.ToInt32(dgvPedido.Rows[contador].Cells["idProducto"].Value) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "and comentario like '%CORTESIA%' ";
                }

                DataTable dtCortesia = new DataTable();
                dtCortesia.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtCortesia, sSql);

                if (bRespuesta == true)
                {
                    int iIdDetPedido = Convert.ToInt32(dtCortesia.Rows[0][0].ToString());

                    if (reabrir == "OK")
                    {
                        sSql = "";
                        sSql += "insert into pos_cortesia" + Environment.NewLine;
                        sSql += "(id_pedido, id_det_pedido, motivo_cortesia, estado," + Environment.NewLine;
                        sSql += "usuario_ingreso, terminal_ingreso, fecha_ingreso)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += iIdPedido + ", " + iIdDetPedido + "," + Environment.NewLine;
                        sSql += "'" + dgvPedido.Rows[contador].Cells["motivoCortesia"].Value.ToString() + "', 'A'," + Environment.NewLine;
                        sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE())";
                    }
                    else
                    {
                        sSql = "";
                        sSql += "insert into pos_cortesia" + Environment.NewLine;
                        sSql += "(id_pedido, id_det_pedido, motivo_cortesia, estado," + Environment.NewLine;
                        sSql += "usuario_ingreso, terminal_ingreso, fecha_ingreso)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += iIdPedido + ", " + iIdDetPedido + "," + Environment.NewLine;
                        sSql += "'" + dgvPedido.Rows[contador].Cells["motivoCortesia"].Value.ToString() + "', 'A'," + Environment.NewLine;
                        sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', GETDATE())";
                    }

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }
                else
                {
                    goto reversa;
                }

                //conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                goto fin;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

            fin: { }
        }

        //FUNCION PARA ACTUALIZAR LA ORDEN
        private bool actualizarComanda(int iMensaje)
        {
            try
            {
                //ACTUALIZACION
                //FECHA: 2019-10-04
                //ELIMINACION DE MOVIMIENTOS DE BODEGA

                if (eliminarMovimientosBodega(iIdPedido) == false)
                {
                    return false;
                }

                //QUERY PARA ACTUALIZAR LA ORDEN EN CASO DE QUE SOLICITEN CONSUMO DE ALIMENTOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;                
                sSql += "porcentaje_dscto = " + lblPorcentajeDescuento.Text.Substring(0, iLongi - 1) + "," + Environment.NewLine;

                if (sAsignarNombreMesa != "")
                {
                    sSql += "comentarios = '" + sAsignarNombreMesa.Trim().ToUpper() + "'," + Environment.NewLine;
                }

                if (iIdMesa != 0)
                {
                    sSql += "id_pos_mesa = " + iIdMesa + "," + Environment.NewLine;
                    sSql += "numero_personas = " + iNumeroPersonas + "," + Environment.NewLine;
                }

                sSql += "recargo_tarjeta = 0," + Environment.NewLine;
                sSql += "remover_iva = 0," + Environment.NewLine;
                sSql += "estado_orden = 'Abierta'," + Environment.NewLine;
                sSql += "consumo_alimentos = " + iConsumoAlimentos + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUCIÓN DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }
              
                //QUERY PARA MODIFICAR EL VALOR DEL TOTAL DE LA ORDEN EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                sSql += "valor = " + Convert.ToDouble(txt_total.Text) + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA PONER EN ESTADO 'E' LOS ITEMS ACTUALES DEL PEDIDO                
                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //RECORRER EL DATAGRID EN CASO DE QUE EL SISTEMA ESTÉ HABILITADO PARA DESCARGAR EL INVENTARIO
                //if (Program.iUsarReceta == 1)
                //{
                //    //FUNCIONES PARA LA BODEGA
                //    //--------------------------------------------------------------------------------
                //    if (eliminarMovimientos(iIdPedido) == false)
                //    {
                //        return false;
                //    }
                //}
                //--------------------------------------------------------------------------------

                //QUERY PARA BUSCAR LOS DETALLES DE LOS ITEMS DEL PEDIDO Y PONERLOS EN ESTADO 'E'
                sSql = "";
                sSql += "select DPD.* from cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP, pos_det_pedido_detalle DPD" + Environment.NewLine;
                sSql += "where DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DPD.id_det_pedido = DP.id_det_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DPD.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //QUERY PARA CAMBIAR A ESTADO 'E' LOS DETALLES DE LOS ITEMS DE LA ORDEN
                            sSql = "";
                            sSql += "update pos_det_pedido_detalle set" + Environment.NewLine;
                            sSql += "estado = 'E'," + Environment.NewLine;
                            sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                            sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                            sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                            sSql += "where id_pos_det_pedido_detalle = " + Convert.ToInt32(dtConsulta.Rows[i]["id_pos_det_pedido_detalle"].ToString()) + Environment.NewLine;
                            sSql += "and estado = 'A'";

                            //EJECUCION DE INSTRUCCION SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA VERIFICAR LOS DESCUENTOS INGRESADOS
                sSql = "";
                sSql += "select * from pos_descuento" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //INSTRUCCION SQL PARA CAMBIAR A ESTADO 'E' LOS DESCUENTOS DE LA ORDEN
                        sSql = "";
                        sSql += "update pos_descuento set" + Environment.NewLine;
                        sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedido;

                        //EJECUCION DE INSTRUCCION SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return false;
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                dPorcentajeDescuento = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, iLongi - 1)) / 100;
                valPorcentajeDescuento = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, lblPorcentajeDescuento.Text.Length - 1));

                //INSERTAMOS UN NUEVO REGISTRO EN LA TABLA CV403_DET_PEDIDOS
                //=======================================================================================================
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    /* SE REALIZA UNA ACTUALIZACION DE CODIGO PARA MEJOR ENTENDIMIENTO Y ORDEN
                     * OBJETIVO: OBTENER LAS VARIABLES PARA REALIZAR UN INSERT MAS EFECTIVO
                     */

                    iIdOrden_P = iIdPedido;
                    iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    iSecuenciaImpresion = Convert.ToInt32(dgvPedido.Rows[i].Cells["colSecuenciaImpresion"].Value);
                    dValorDescuento = Convert.ToDouble(dgvPedido.Rows[i].Cells["guardada"].Value);
                    sPagaIva_P = dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString();
                    sCodigoClaseProducto = dgvPedido.Rows[i].Cells["tipoProducto"].Value.ToString();
                    sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    dServicio = 0;

                    //ACTUALIZACION DE CODIGO PARA RECALCULAR EL PORCENTAJE DE SERVICIO
                    if (Program.iManejaServicio == 1)
                    {
                        if (dCantidad_P < 1)
                        {
                            dServicio = ((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) * Program.servicio;
                        }
                        
                        else
                        {
                            //dServicio = (dPrecioUnitario_P - dValorDescuento) * dCantidad_P * Program.servicio;
                            dServicio = (((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) / dCantidad_P) * Program.servicio;
                        }
                    }

                    if (dgvPedido.Rows[i].Cells["colIdOrden"].Value.ToString() == "")
                    {
                        iSecuenciaEntrega = 0;
                    }

                    else
                    {
                        iSecuenciaEntrega = Convert.ToInt32(dgvPedido.Rows[i].Cells["colIdOrden"].Value);
                    }

                    iIdMascaraItem = 0;

                    if (valPorcentajeDescuento == 0)
                    {
                        if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            dDescuento_P = 0;
                        }
                    }

                    else
                    {
                        if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            dDescuento_P = dPrecioUnitario_P * dPorcentajeDescuento;
                        }
                    }

                    if (sPagaIva_P == "1")
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    //CONTROL DE CONSUMO ALIMENTOS,CORTESIAS Y CANCELACION ITEM
                    if ((dgvPedido.Rows[i].Cells["colIdMascara"].Value.ToString() != "0") && (dgvPedido.Rows[i].Cells["colIdMascara"].Value.ToString() != ""))
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                        iIdMascaraItem = Convert.ToInt32(dgvPedido.Rows[i].Cells["colIdMascara"].Value);
                    }

                    else if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else if (dgvPedido.Rows[i].Cells["idProducto"].Value.ToString() == Program.iIdProductoNuevoItem.ToString())
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else
                    {
                        sGuardarComentario = null;
                    }

                    //INSTRUCCION SQL PARA GUARDAR EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva ,Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia, " + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, Estado,numero_replica_trigger,numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdOrden_P + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdMascaraItem + "," + Environment.NewLine;
                    sSql += iSecuenciaImpresion + ", " + Environment.NewLine;

                    if (iSecuenciaEntrega == 0)
                    {
                        sSql += "null, ";
                    }

                    else
                    {
                        sSql += iSecuenciaEntrega + ", ";
                    }

                    sSql += "'A', 0, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    if (sCodigoClaseProducto.Trim() == "02")
                    {
                        if (Program.iDescargarProductosNoProcesados == 1)
                        {
                            if (insertarMovimientoProductoNoProcesado(Convert.ToDecimal(dCantidad_P)) == false)
                            {
                                return false;
                            }
                        }
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-05
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                    if (sCodigoClaseProducto.Trim() == "03")
                    {
                        if (Program.iDescargarReceta == 1)
                        {
                            if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                            {
                                return false;
                            }
                        }
                    }

                    iBandera2 = 0;
                    iCuenta = 0;

                    //INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    for (p = 0; p < Program.iContadorDetalle; p++)
                    {
                        if (Program.sDetallesItems[p, 0] == dgvPedido.Rows[i].Cells["idProducto"].Value.ToString())
                        {
                            iBandera2 = 1;
                            break;
                        }
                    }

                    if (iBandera2 == 1)
                    {
                        //INSERTAMOS LOS ITEMS EN LA TABLA pos_det_pedido_detalle

                        for (q = 1; q < Program.iContadorDetalleMximoY; q++)
                        {
                            if (Program.sDetallesItems[p, q] == null)
                            {
                                break;
                            }
                            else
                            {
                                iCuenta++;
                            }
                        }

                        //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_det_pedidos";
                        sCampo = "id_det_pedido";

                        long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            return false;
                        }

                        else
                        {
                            iIdDetPedido = Convert.ToInt32(iMaximo);
                        }

                        for (q = 1; q <= iCuenta; q++)
                        {
                            //QUERY PARA INSERTAR LOS DETALLES DE CADA ITEM EN CASO DE QUE SE HAYA INGRESADO
                            sSql = "";
                            sSql += "insert into pos_det_pedido_detalle " + Environment.NewLine;
                            sSql += "(id_det_pedido, detalle, estado, fecha_ingreso," + Environment.NewLine;
                            sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                            sSql += "values(" + Environment.NewLine;
                            sSql += iIdDetPedido + ", '" + Program.sDetallesItems[p, q] + "', " + Environment.NewLine;
                            sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                            //EJECUCION DE INSTRUCCION SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }
                    }
                }


                ////RECORRER EL DATAGRID EN CASO DE QUE EL SISTEMA ESTÉ HABILITADO PARA DESCARGAR EL INVENTARIO
                //if (Program.iUsarReceta == 1)
                //{
                //    iIdBodega = obtenerIdBodega(Program.iIdLocalidad);

                //    if (iIdBodega == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    iCgClienteProveedor = obtenerCgClienteProveedor();
                //    iTipoMovimiento = obtenerCorrelativoTipoMovimiento();

                //    if (iCgClienteProveedor == 0 || iTipoMovimiento == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    iRespuesta = buscarDatos();

                //    if (iRespuesta[0] == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                //    {
                //        string sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString().Trim();
                //        iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                //        dCantidad_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                //        iIdPosReceta = obteneridReceta(iIdProducto_P);

                //        if (iIdPosReceta == -1)
                //        {
                //            return false;
                //        }

                //        else
                //        {
                //            if (crearEgreso(sNombreProducto_P + " - ORDEN " + txt_Orden.Text.Trim(), iCgClienteProveedor,
                //                        iTipoMovimiento, iIdPosReceta, iIdProducto_P, dCantidad_P) == false)
                //            {
                //                return false;
                //            }
                //        }
                //    }
                //}

                iIdMovimientoStock = 0;
                iBanderaDescargaStock = 0;
                continuar_proceso: { }

                //PARA INGRESAR EL MOTIVO DEL DESCUENTO
                //================================================================================================
                if (iBandera == 1)
                {
                    //QUERY PARA INSERTAR EL MOTIVO DE DESCUENTO
                    sSql = "";
                    sSql += "insert into pos_descuento (" + Environment.NewLine;
                    sSql += "id_pedido, motivo_descuento, estado, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPedido + ", '" + Program.sMotivoDescuento + "', 'A'," + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                //QUERY PARA ACTUALIZAR LAS CORTESIAS A ESTADO 'E'
                sSql = "";
                sSql += "update pos_cortesia set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //for (int i = 0; i < dgvPedido.Rows.Count; i++)
                //{
                //    if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "")
                //    {
                //        dgvPedido.Rows[i].Cells["cortesia"].Value = "0";
                //    }

                //    if (Convert.ToDouble(dgvPedido.Rows[i].Cells["cortesia"].Value) == 1)
                //    {
                //        insertarCortesia(i);
                //    }
                //}


                //QUERY PARA ACTUALIZAR LAS CANCELACIONES A ESTADO 'E'
                sSql = "";
                sSql += "update pos_cancelacion_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }


                //for (int i = 0; i < dgvPedido.Rows.Count; i++)
                //{
                //    if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "")
                //    {
                //        dgvPedido.Rows[i].Cells["cancelar"].Value = "0";
                //    }insertarcortesia

                //    if (Convert.ToDouble(dgvPedido.Rows[i].Cells["cancelar"].Value) == 1)
                //    {
                //        cancelarItems(i);
                //    }
                //}

                //LLAMAR A FUNCIONES DE RECORRIDO DE CORTESIAS, CANCELACIONES
                if (recorrerCortesiasCancelaciones() == false)
                {
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                limpiar.limpiarArregloComentarios();


                if ((Program.iImprimeOrden == 1) && (iMensaje == 1))
                {
                    SiNo.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + ". ¿Desea imprimir la orden generada?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(iIdPedido.ToString(), 1, "Abierta");
                        precuenta.ShowInTaskbar = false;
                        precuenta.ShowDialog();

                        if (Program.iHabilitarDestinosImpresion == 1)
                        {

                            if (chkImprimirCocina.Checked == true)
                            {
                                Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion);
                                cocina.ShowDialog();
                            }
                        }
                    }
                }

                else
                {
                    if (Program.iHabilitarDestinosImpresion == 1)
                    {
                        if (chkImprimirCocina.Checked == true)
                        {
                            Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion);
                            cocina.ShowDialog();
                        }

                        ok.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + ".";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + "." + Environment.NewLine + "No se imprimirá comanda en cocina.";
                    }

                    ok.ShowDialog();
                }

                return true;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR UNA NUEVA ORDEN
        private bool insertarNuevaComanda(int iMensaje)
        {
            try
            {
                sfechaOrden = Convert.ToDateTime(Program.sFechaSistema).ToString("yyyy/MM/dd");
                extraerNumeroCuenta();

                //if ((Program.sIDPERSONA == null) || (Program.sIDPERSONA == ""))
                //{
                //    iIdCliente = Program.iIdPersona;
                //}

                //else
                //{
                //    iIdCliente = Convert.ToInt32(Program.sIDPERSONA);
                //}

                //EXTRAER EL PORCENTAJE DE DESCUENTO
                dPorcentajeDescuento = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, iLongi - 1)) / 100;
                valPorcentajeDescuento = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, lblPorcentajeDescuento.Text.Length - 1));

                //IQUERY PARA INSERTAR UNA NUEVA ORDEN EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "insert into cv403_cab_pedidos(" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_localidad,fecha_pedido,id_persona, " + Environment.NewLine;
                sSql += "cg_tipo_cliente, cg_moneda, porcentaje_iva, id_vendedor, cg_estado_pedido, porcentaje_dscto, " + Environment.NewLine;
                sSql += "cg_facturado, fecha_ingreso, usuario_ingreso, terminal_ingreso,cuenta, id_pos_mesa, id_pos_cajero, " + Environment.NewLine;
                sSql += "id_pos_origen_orden,id_pos_orden_dividida, id_pos_jornada, fecha_orden, fecha_apertura_orden, " + Environment.NewLine;
                sSql += "fecha_cierre_orden, estado_orden, numero_personas, origen_dato, numero_replica_trigger, " + Environment.NewLine;
                sSql += "estado_replica, numero_control_replica, estado, idtipoestablecimiento, comentarios, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "id_pos_mesero, id_pos_terminal, porcentaje_servicio, consumo_alimentos, id_pos_promotor," +Environment.NewLine;
                sSql += "id_pos_repartidor, id_pos_cierre_cajero) " + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Program.iIdEmpresa + "," + Program.iCgEmpresa + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                //sSql += "'" + sfechaOrden + "', " + iIdCliente + ",8032," + Program.iMoneda + "," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', " + iIdPersona + ",8032," + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + "," + Program.iIdVendedor + ",6967," + lblPorcentajeDescuento.Text.Substring(0, iLongi - 1) + ",7471," + Environment.NewLine;
                sSql += "GETDATE(),'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + iCuentaDiaria + ",";

                sSql += iIdMesa + "," + Environment.NewLine;
                //if (iIdMesa == 0)
                //{
                //    sSql += "null," + Environment.NewLine;
                //}

                //else
                //{
                //    sSql += iIdMesa + "," + Environment.NewLine;
                //}

                sSql += iIdCajero + "," + iIdOrigenOrden + ", 0," + Program.iJORNADA + "," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', GETDATE(), null, 'Abierta'," + Environment.NewLine;
                sSql += iNumeroPersonas + ", 1, 1, 0, 0, 'A', 1, ";

                if (sAsignarNombreMesa == "")
                {
                    sSql += "null,";
                }

                else
                {
                    sSql += "'" + sAsignarNombreMesa.Trim().ToUpper() + "',";
                }                

                if (Program.iModoDelivery == 0)
                {
                    sSql += "null," + Environment.NewLine;
                }
                    
                else
                {
                    sSql += Program.iModoDelivery + "," + Environment.NewLine;
                }

                sSql += iIdMesero + ", " + Program.iIdPosTerminal + ", " + (Program.servicio * 100) + ", " + iConsumoAlimentos + ", " + Environment.NewLine;
                sSql += iIdPromotor + ", " + iIdRepartidor + ", " + Program.iIdPosCierreCajero + ")";

                Program.iBanderaCliente = 0;

                //EJECUCION DE INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA INSERTAR EN CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "insert into cv403_cab_despachos (" + Environment.NewLine;
                sSql += "idempresa, id_persona, cg_empresa, id_localidad, fecha_despacho," + Environment.NewLine;
                sSql += "cg_motivo_despacho, id_destinatario, punto_partida, cg_ciudad_entrega," + Environment.NewLine;
                sSql += "direccion_entrega, id_transportador, fecha_inicio_transporte," + Environment.NewLine;
                sSql += "fecha_fin_transporte, cg_estado_despacho, punto_venta, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                //sSql += Program.iIdEmpresa + ", " + iIdCliente + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', " + Program.iCgMotivoDespacho + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += "'" + Program.sPuntoPartida + "', " + Program.iCgCiudadEntrega + ", '" + Program.sDireccionEntrega + "'," + Environment.NewLine;
                sSql += "'" + Program.iIdPersona + "', '" + sfechaOrden + "', '" + sfechaOrden + "', " + Program.iCgEstadoDespacho + "," + Environment.NewLine;
                sSql += "1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //OBTENEMOS EL MAX ID DE LA TABLA CV403_CAB_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    return false;
                }

                else
                {
                    iIdPedido = Convert.ToInt32(iMaximo);
                }

                //PROCEDIMIENTO PARA EXTRAER EL NUMERO DE PEDIDO
                sSql = "";
                sSql += "select numero_pedido" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iNumeroPedidoOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }                    

                //QUERY PARA ACTUALIZAR EL NUMERO DE PEDIDO EN LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numero_pedido = numero_pedido + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //QUERY PARA PODER INSERTAR REGISTRO EN LA TABLA CV403_NUMERO_CAB_PEDIDO
                sSql = "";
                sSql += "insert into cv403_numero_cab_pedido (" + Environment.NewLine;
                sSql += "idtipocomprobante,id_pedido, numero_pedido," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "estado, numero_control_replica, numero_replica_trigger)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "1, " + iIdPedido + ", " + iNumeroPedidoOrden + ", GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_despachos";
                sCampo = "id_despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return false;
                }

                else
                {
                    iIdCabDespachos = Convert.ToInt32(iMaximo);
                }

                //QUERY PARA INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "insert into cv403_despachos_pedidos (" + Environment.NewLine;
                sSql += "id_despacho, id_pedido, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCabDespachos + "," + iIdPedido + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_despachos_pedidos";
                sCampo = "id_despacho_pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return false;
                }

                else
                {
                    iIdDespachoPedido = Convert.ToInt32(iMaximo);
                }

                //QUERY PARA INSERTAR UN NUEVO REGISTRO EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "insert into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, id_persona, id_localidad, cg_evento_cobro," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                //sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdCliente + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + iIdPersona + "," + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "7466, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_eventos_cobros";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return false;
                }

                else
                {
                    iIdEventoCobro = Convert.ToInt32(iMaximo);
                }

                //QUERY PARA INSERTAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "insert into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, id_pedido, cg_tipo_documento, fecha_vcto, cg_moneda," + Environment.NewLine;
                sSql += "valor, cg_estado_dcto, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdEventoCobro + ", " + iIdPedido + ", " + iCgTipoDocumento + "," + Environment.NewLine;
                sSql += "'" + sfechaOrden + "', " + Program.iMoneda + ", " + Convert.ToDouble(txt_total.Text) + "," + Environment.NewLine;
                sSql += icg_estado_dcto + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 1, 0)";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //EJECUCION DE INSTRUCCION SQL
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                
                //INSTRUCCIONES PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    /* SE REALIZA UNA ACTUALIZACION DE CODIGO PARA MEJOR ENTENDIMIENTO Y ORDEN
                     * OBJETIVO: OBTENER LAS VARIABLES PARA REALIZAR UN INSERT MAS EFECTIVO
                     */
                    iIdMascaraItem = 0;
                    iIdOrden_P = iIdPedido;
                    iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                    dPrecioUnitario_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value);
                    dCantidad_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                    iSecuenciaImpresion = Convert.ToInt32(dgvPedido.Rows[i].Cells["colSecuenciaImpresion"].Value);
                    dValorDescuento = Convert.ToDouble(dgvPedido.Rows[i].Cells["guardada"].Value);
                    sPagaIva_P = dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString();
                    sCodigoClaseProducto = dgvPedido.Rows[i].Cells["tipoProducto"].Value.ToString();
                    sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    dServicio = 0;

                    //ACTUALIZACION DE CODIGO PARA RECALCULAR EL PORCENTAJE DE SERVICIO
                    if (Program.iManejaServicio == 1)
                    {
                        //dServicio = (dPrecioUnitario_P - dValorDescuento) * Program.servicio;

                        if (dCantidad_P < 1)
                        {
                            dServicio = ((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) * Program.servicio;
                        }

                        else
                        {
                            //dServicio = (dPrecioUnitario_P - dValorDescuento) * Program.servicio;
                            dServicio = (((dPrecioUnitario_P * dCantidad_P) - dValorDescuento) / dCantidad_P) * Program.servicio;
                        }
                    }


                    if (dgvPedido.Rows[i].Cells["colIdOrden"].Value.ToString() == "")
                    {
                        iSecuenciaEntrega = 0;
                    }

                    else
                    {
                        iSecuenciaEntrega = Convert.ToInt32(dgvPedido.Rows[i].Cells["colIdOrden"].Value);
                    }

                    if (valPorcentajeDescuento == 0)
                    {
                        if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            dDescuento_P = 0;
                        }
                    }

                    else
                    {
                        if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                        {
                            dDescuento_P = dPrecioUnitario_P;
                        }

                        else
                        {
                            dDescuento_P = dPrecioUnitario_P * dPorcentajeDescuento;
                        }
                    }

                    if (sPagaIva_P == "1")
                    {
                        dIVA_P = (dPrecioUnitario_P - dDescuento_P) * Program.iva;
                    }

                    else
                    {
                        dIVA_P = 0;
                    }

                    //CONTROL DE CONSUMO ALIMENTOS,CORTESIAS Y CANCELACION ITEM
                    if ((dgvPedido.Rows[i].Cells["colIdMascara"].Value.ToString() != "0") && (dgvPedido.Rows[i].Cells["colIdMascara"].Value.ToString() != ""))
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                        iIdMascaraItem = Convert.ToInt32(dgvPedido.Rows[i].Cells["colIdMascara"].Value);
                    }

                    else if (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "1")
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else if (dgvPedido.Rows[i].Cells["idProducto"].Value.ToString() == Program.iIdProductoNuevoItem.ToString())
                    {
                        sGuardarComentario = dgvPedido.Rows[i].Cells["producto"].Value.ToString();
                    }

                    else
                    {
                        sGuardarComentario = null;
                    }

                    //INSTRUCCION SQL PARA GUARDAR EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "Insert Into cv403_det_pedidos(" + Environment.NewLine;
                    sSql += "Id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                    sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva, Valor_otro," + Environment.NewLine;
                    sSql += "comentario, Id_Definicion_Combo, fecha_ingreso," + Environment.NewLine;
                    sSql += "Usuario_Ingreso, Terminal_ingreso, id_pos_mascara_item, secuencia," + Environment.NewLine;
                    sSql += "id_pos_secuencia_entrega, estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iIdOrden_P + ", " + iIdProducto_P + ", 546, " + dPrecioUnitario_P + ", " + Environment.NewLine;
                    sSql += dCantidad_P + ", " + dDescuento_P + ", 0, " + dIVA_P + ", " + dServicio + ", " + Environment.NewLine;
                    sSql += "'" + sGuardarComentario + "', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', " + iIdMascaraItem + "," + Environment.NewLine;
                    sSql += iSecuenciaImpresion + "," + Environment.NewLine;

                    if (iSecuenciaEntrega == 0)
                    {
                        sSql += "null, ";
                    }

                    else
                    {
                        sSql += iSecuenciaEntrega + ", ";
                    }

                    sSql += "'A', 0, 0)";

                    //FUNCION PARA EJECUTAR LA INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-04
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS NO PROCESADOS DE INVENTARIO

                    if (sCodigoClaseProducto.Trim() == "02")
                    {
                        if (Program.iDescargarProductosNoProcesados == 1)
                        {
                            if (insertarMovimientoProductoNoProcesado(Convert.ToDecimal(dCantidad_P)) == false)
                            {
                                return false;
                            }
                        }
                    }

                    //ACTUALIZACION
                    //FECHA: 2019-10-05
                    //OBJETIVO: IMPLEMENTAR EL DESCARGO DE PRODUCTOS POR MATERIA PRIMA

                    if (sCodigoClaseProducto.Trim() == "03")
                    {
                        if (Program.iDescargarReceta == 1)
                        {
                            if (consultarIdReceta(iIdProducto_P, Convert.ToDecimal(dCantidad_P), sNombreProducto_P) == false)
                            {
                                return false;
                            }
                        }
                    }


                    iBandera2 = 0;
                    iCuenta = 0;

                    //INSTRUCCIONES PARA INSERTAR LOS DETALLES DE CADA LINEA EN CASO DE HABER INGRESADO
                    for (p = 0; p < Program.iContadorDetalle; p++)
                    {
                        if (Program.sDetallesItems[p, 0] == dgvPedido.Rows[i].Cells["idProducto"].Value.ToString())
                        {
                            iBandera2 = 1;
                            break;
                        }
                    }

                    if (iBandera2 == 1)
                    {
                        //INSERTAMOS LOS ITEMS EN LA TABLA pos_det_pedido_detalle

                        for (q = 1; q < Program.iContadorDetalleMximoY; q++)
                        {
                            if (Program.sDetallesItems[p, q] == null)
                            {
                                break;
                            }
                            else
                            {
                                iCuenta++;
                            }
                        }

                        //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        sTabla = "cv403_det_pedidos";
                        sCampo = "id_det_pedido";

                        iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                        if (iMaximo == -1)
                        {
                            ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                            ok.ShowDialog();
                            return false;
                        }

                        else
                        {
                            iIdDetPedido = Convert.ToInt32(iMaximo);
                        }

                        for (q = 1; q <= iCuenta; q++)
                        {
                            sSql = "";
                            sSql += "insert into pos_det_pedido_detalle ";
                            sSql += "(id_det_pedido, detalle, estado, fecha_ingreso, ";
                            sSql += "usuario_ingreso, terminal_ingreso) ";
                            sSql += "values(" + iIdDetPedido + ", '" + Program.sDetallesItems[p, q] + "', ";
                            sSql += "'A', getdate(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "' )";

                            //EJECUTA SQL
                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }
                    }

                    //QUERY PARA INSERTAR EN LA TABLA CV403_CANTIDADES_DESPACHADAS
                    sSql = "";
                    sSql += "insert into cv403_cantidades_despachadas(" + Environment.NewLine;
                    sSql += "id_despacho_pedido, id_producto, cantidad, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdDespachoPedido + ", " + dgvPedido.Rows[i].Cells["idProducto"].Value + "," + Environment.NewLine;
                    sSql += dgvPedido.Rows[i].Cells["cantidad"].Value + ", 'A', 1, 0)";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                ////RECORRER EL DATAGRID EN CASO DE QUE EL SISTEMA ESTÉ HABILITADO PARA DESCARGAR EL INVENTARIO
                //if (Program.iUsarReceta == 1)
                //{
                //    iIdBodega = obtenerIdBodega(Program.iIdLocalidad);

                //    if (iIdBodega == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    iCgClienteProveedor = obtenerCgClienteProveedor();
                //    iTipoMovimiento = obtenerCorrelativoTipoMovimiento();

                //    if (iCgClienteProveedor == 0 || iTipoMovimiento == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    iRespuesta = buscarDatos();

                //    if (iRespuesta[0] == 0)
                //    {
                //        goto continuar_proceso;
                //    }

                //    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                //    {
                //        string sNombreProducto_P = dgvPedido.Rows[i].Cells["producto"].Value.ToString().Trim();
                //        iIdProducto_P = Convert.ToInt32(dgvPedido.Rows[i].Cells["idProducto"].Value);
                //        dCantidad_P = Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value);
                //        iIdPosReceta = obteneridReceta(iIdProducto_P);

                //        if (iIdPosReceta == -1)
                //        {
                //            return false;
                //        }

                //        else
                //        {
                //            if (crearEgreso(sNombreProducto_P + " - ORDEN " + iNumeroPedidoOrden.ToString(), iCgClienteProveedor, 
                //                        iTipoMovimiento, iIdPosReceta, iIdProducto_P, dCantidad_P) == false)
                //            {
                //                return false;
                //            }
                //        }
                //    }
                //}

                iIdMovimientoStock = 0;
                iBanderaDescargaStock = 0;
                continuar_proceso: { }

                if (iBandera == 1)
                {
                    //QUERY PARA INSERTAR EN LA TABLA POS_DESCUENTO
                    sSql = "";
                    sSql += "insert into pos_descuento (" + Environment.NewLine;
                    sSql += "id_pedido, motivo_descuento, estado, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPedido + ", '" + Program.sMotivoDescuento + "', 'A'," + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //EJECUCION DE INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                }

                //LLAMAR A FUNCIONES DE RECORRIDO DE CORTESIAS, CANCELACIONES
                if (recorrerCortesiasCancelaciones() == false)
                {
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                limpiar.limpiarArregloComentarios();

                Program.iCuentaDiaria++;

                if ((Program.iImprimeOrden == 1) && (iMensaje == 1))
                {
                    SiNo.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + ". ¿Desea imprimir la orden generada?";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        if (Program.iEjecutarImpresion == 1)
                        {
                            Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(iIdPedido.ToString(), 1, "Abierta");
                            precuenta.ShowDialog();
                        }

                        if (Program.iHabilitarDestinosImpresion == 1)
                        {

                            if (chkImprimirCocina.Checked == true)
                            {
                                if (Program.iEjecutarImpresion == 1)
                                {
                                    Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion);
                                    cocina.ShowDialog();
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (Program.iHabilitarDestinosImpresion == 1)
                    {
                        if (chkImprimirCocina.Checked == true)
                        {
                            if (Program.iEjecutarImpresion == 1)
                            {
                                Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iSecuenciaImpresion);
                                cocina.ShowDialog();
                            }
                        }

                        ok.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + ".";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Guardado en la orden: " + txt_Orden.Text + "." + Environment.NewLine + "No se imprimirá comanda en cocina.";
                    }

                    ok.ShowDialog();
                }

                return true;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA RECORRER LAS CORTESIAS
        private bool recorrerCortesiasCancelaciones()
        {
            try
            {
                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    if ((dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "") || (dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == null))
                    {
                        dgvPedido.Rows[i].Cells["cortesia"].Value = "0";
                    }

                    if (Convert.ToDouble(dgvPedido.Rows[i].Cells["cortesia"].Value) == 1)
                    {
                        insertarCortesia(i);
                    }
                }

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    if (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "")
                    {
                        dgvPedido.Rows[i].Cells["cancelar"].Value = "0";
                    }

                    if (Convert.ToDouble(dgvPedido.Rows[i].Cells["cancelar"].Value) == 1)
                    {
                        cancelarItems(i);
                    }
                }

                return true;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarComanda(int iOp, int iMensaje)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                sfechaOrden = Program.sFechaSistema.ToString("yyyy/MM/dd");
                iLongi = lblPorcentajeDescuento.Text.Length;
                dPorcentajeCalculado = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, iLongi - 1)) / 100;

                if ((txt_numeropersonas == null) || (txt_numeropersonas.Text == ""))
                {
                    iNumeroPersonas = 0;
                }

                else
                {
                    iNumeroPersonas = Convert.ToInt32(txt_numeropersonas.Text);
                }

                //AQUI CONSULTAMOS LOS VALORES DE LA TABLA TP_LOCALIDADES
                if (recuperarDatosLocalidad() == false)
                {
                    goto fin;
                }

                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                /*  LA VARIABLE IOP CONTROLA UNA NUEVA ORDEN O ACTUALIZA
                 *  IOP 1: ACTUALIZA EL PEDIDO
                 *  IOP 0: INSERTAR UN NUEVO PEDIDO
                 */
                if (iOp == 1)
                {
                    if (actualizarComanda(iMensaje) == false)
                    {
                        goto reversa;
                    }

                    goto fin;
                }

                else
                {
                    if (insertarNuevaComanda(iMensaje) == false)
                    {
                        goto reversa;
                    }

                    reabrir = "OK";

                    if (Program.iReimprimirCocina == 0)
                    {
                        chkImprimirCocina.Checked = false;
                    }

                    else
                    {
                        chkImprimirCocina.Checked = true;
                    }

                    goto fin;
                }

                
            }

            catch(Exception ex)
            {
                Cursor = Cursors.Default;
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

            fin: { this.Cursor = Cursors.Default; }

        }
        
        #endregion

        #region FUNCIONES PARA CALCULOS DE TOTALES CON DESCUENTOS

        //función para llenar arreglo de pre-orden
        private void llenarArregloReOrden()
        {
            if (dgvPedido.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No ha ingresado ningún producto.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                pnlReOrden.Visible = false;
            }
            else
            {
                Button[,] boton = new Button[100, 2];
                int iContador = 0;

                for (int i = 0; i < 100; i++)
                {

                    for (int j = 0; j < 2; j++)
                    {

                        boton[i, j] = new Button();
                        boton[i, j].Width = 300;
                        boton[i, j].Height = 100;
                        boton[i, j].Top = i * 110;
                        boton[i, j].Left = j * 310;
                        boton[i, j].Font = new Font("Arial", 14);
                        boton[i, j].Click += botonClicReorden;
                        boton[i, j].BackColor = Color.FromArgb(255, 255, 128);

                        if (iContador == dgvPedido.Rows.Count)
                        {
                            break;
                        }
                        boton[i, j].Tag = dgvPedido.Rows[iContador].Cells[7].Value;
                        boton[i, j].AccessibleName = dgvPedido.Rows[iContador].Cells[1].Value.ToString();
                        boton[i, j].Text = dgvPedido.Rows[iContador].Cells[1].Value.ToString() + "    " + dgvPedido.Rows[iContador].Cells[2].Value.ToString();

                        if (dgvPedido.Rows[iContador].Cells[2].Value.ToString() != "MOVILIZACION")
                        {
                            pnlItems.Controls.Add(boton[i, j]);
                        }

                        iContador++;
                    }

                }
            }

        }

        private void botonClicReorden(object sender, EventArgs e)
        {
            Button botonReorden = sender as Button;
            float cantidad = 0;
            float valoru = 0;
            int existe = 0;

            for (int i = 0; i < dgvPedido.Rows.Count; i++)
            {
                if (dgvPedido.Rows[i].Cells[7].Value.ToString() == botonReorden.Tag && botonReorden.AccessibleName != "0.5")
                {

                    cantidad = float.Parse(dgvPedido.Rows[i].Cells[1].Value.ToString().Trim());
                    cantidad = cantidad + 1;

                    dgvPedido.Rows[i].Cells["cantidad"].Value = cantidad;
                    valoru = float.Parse(dgvPedido.Rows[i].Cells["valuni"].Value.ToString().Trim());
                    dgvPedido.Rows[i].Cells["valor"].Value = (cantidad * valoru * Program.factorPrecio).ToString("N2");

                    Program.factorPrecio = 1;

                    existe = 1;
                    break;
                }

            }

            if (existe == 0)
            {

                int iBandera = 0;
                int j = 0;
                for (j = 0; j < dgvPedido.Rows.Count; j++)
                {
                    if (dgvPedido.Rows[j].Cells[1].Value.ToString() == botonReorden.AccessibleName)
                    {
                        iBandera = 1;
                        break;
                    }
                }

                if (iBandera == 1)
                {
                    int x = dgvPedido.Rows.Add();
                    dgvPedido.Rows[x].Cells[1].Value = dgvPedido.Rows[j].Cells[1].Value;
                    dgvPedido.Rows[x].Cells[0].Value = dgvPedido.Rows[j].Cells[0].Value;
                    dgvPedido.Rows[x].Cells[2].Value = dgvPedido.Rows[j].Cells[2].Value;
                    dgvPedido.Rows[x].Cells[3].Value = dgvPedido.Rows[j].Cells[3].Value;
                    dgvPedido.Rows[x].Cells[4].Value = dgvPedido.Rows[j].Cells[4].Value;
                    dgvPedido.Rows[x].Cells[5].Value = dgvPedido.Rows[j].Cells[5].Value;
                    dgvPedido.Rows[x].Cells[6].Value = dgvPedido.Rows[j].Cells[6].Value;
                    dgvPedido.Rows[x].Cells[7].Value = dgvPedido.Rows[j].Cells[7].Value;
                    dgvPedido.Rows[x].Cells[8].Value = dgvPedido.Rows[j].Cells[8].Value;
                    dgvPedido.Rows[x].Cells[9].Value = dgvPedido.Rows[j].Cells[9].Value;
                    dgvPedido.Rows[x].Cells[10].Value = dgvPedido.Rows[j].Cells[10].Value;
                    dgvPedido.Rows[x].Cells[11].Value = dgvPedido.Rows[j].Cells[11].Value;
                    dgvPedido.Rows[x].Cells[12].Value = dgvPedido.Rows[j].Cells[12].Value;
                    dgvPedido.Rows[x].Cells[13].Value = dgvPedido.Rows[j].Cells[13].Value;
                    dgvPedido.Rows[x].Cells[14].Value = dgvPedido.Rows[j].Cells[14].Value;
                    dgvPedido.Rows[x].Cells[15].Value = dgvPedido.Rows[j].Cells[15].Value;
                    dgvPedido.Rows[x].Cells[16].Value = dgvPedido.Rows[j].Cells[16].Value;
                }

            }

            calcularTotales();
        }

        //CALCULAR DATOS PARA LLENAR EN LAS CAJAS DE TEXTO
        public void calcularTotales()
        {
            Decimal dSubtotalConIva = 0;
            Decimal dSubtotalCero = 0;
            Decimal dDescuentoConIva = 0;
            Decimal dDescuentoCero = 0;
            Decimal dSubtotalNeto = 0;
            Decimal dIva = 0;
            Decimal dServicio = 0;
            Decimal dTotalDebido = 0;

            //INSTRUCCIONES PARA SUMAR LOS VALORES DEL GRID
            for (int i = 0; i < dgvPedido.Rows.Count; i++)
            {
                if ((dgvPedido.Rows[i].Cells["cortesia"].Value.ToString() == "0") && (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString() == "0"))
                {
                    if (Program.sCodigoAsignadoOrigenOrden == "06")
                    {
                        dgvPedido[0, i].Value = (Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value) * (Program.dbValorPorcentaje / 100)).ToString();
                    }

                    else
                    {
                        dgvPedido[0, i].Value = (Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value) * (Convert.ToDouble(sPorcentajeDescuento) / 100)).ToString();
                    }
                }

                else
                {
                    dgvPedido[0, i].Value = (Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value)).ToString();
                }

                if (dgvPedido.Rows[i].Cells["pagaIva"].Value.ToString() == "0")
                {
                    dSubtotalCero += (Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString()));
                    dDescuentoCero += Convert.ToDecimal(dgvPedido.Rows[i].Cells["guardada"].Value.ToString());
                }

                else
                {
                    dSubtotalConIva += (Convert.ToDecimal(dgvPedido.Rows[i].Cells["cantidad"].Value.ToString()) * Convert.ToDecimal(dgvPedido.Rows[i].Cells["valuni"].Value.ToString()));
                    dDescuentoConIva += Convert.ToDecimal(dgvPedido.Rows[i].Cells["guardada"].Value.ToString());
                }

                
            }
            //=======================================================================================================

            //INSTRUCCIONES PARA LLENAR EL DESCUENTO
            //=======================================================================================================
            int iExtraePorcentaje = lblPorcentajeDescuento.Text.Length;
            Double dPorcentajeIngresado = Convert.ToDouble(lblPorcentajeDescuento.Text.Substring(0, iExtraePorcentaje - 1)) / 100;


            //INSTRUCCION PARA LLENAR EL SUBTOTAL NETO
            dSubtotalNeto = dSubtotalConIva + dSubtotalCero - dDescuentoConIva - dDescuentoCero;
            dIva = (dSubtotalConIva - dDescuentoConIva) * Convert.ToDecimal(Program.iva);
            dServicio = (dSubtotalNeto) * Convert.ToDecimal(Program.servicio);
            dTotalDebido = dSubtotalNeto + dIva + dServicio;

            txt_subtotal.Text = dSubtotalConIva.ToString("N2");
            txtSubtotalCero.Text = dSubtotalCero.ToString("N2");
            txtDescuento.Text = (dDescuentoCero + dDescuentoConIva).ToString("N2");
            txt_subtotal_descuento.Text = dSubtotalNeto.ToString("N2");
            txt_iva.Text = dIva.ToString("N2");
            txt_servicio.Text = dServicio.ToString("N2");
            txt_total.Text = dTotalDebido.ToString("N2");

            lblCantidadDebida.Text = "$ " + dTotalDebido.ToString("N2");
        }

        #endregion

        #region FUNCIONES PARA COPIAR UN DATAGRIDVIEW

        //FUNCION PARA CREAR EL DATAGRIDVIEW
        private void crearDataGrid()
        {
            try
            {
                dgvOrigenPedido = new DataGridView();

                dgvOrigenPedido.AllowUserToAddRows = false;
                dgvOrigenPedido.AllowUserToDeleteRows = false;
                dgvOrigenPedido.AllowUserToResizeColumns = false;
                dgvOrigenPedido.AllowUserToResizeRows = false;
                dgvOrigenPedido.MultiSelect = false;

                DataGridViewTextBoxColumn guardada = new DataGridViewTextBoxColumn();
                guardada.HeaderText = "guardada";
                guardada.Visible = false;
                DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();
                cantidad.HeaderText = "CANT.";
                cantidad.Width = 65;
                cantidad.Visible = true;
                DataGridViewTextBoxColumn producto = new DataGridViewTextBoxColumn();
                producto.HeaderText = "Producto";
                producto.Width = 193;
                producto.Visible = true;
                DataGridViewTextBoxColumn valuni = new DataGridViewTextBoxColumn();
                valuni.HeaderText = "valuni";
                valuni.Visible = false;
                DataGridViewTextBoxColumn valor = new DataGridViewTextBoxColumn();
                valor.HeaderText = "valor";
                valor.Visible = false;
                DataGridViewTextBoxColumn cod = new DataGridViewTextBoxColumn();
                cod.HeaderText = "cod";
                cod.Visible = false;
                DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
                ID.HeaderText = "ID";
                ID.Visible = false;
                DataGridViewTextBoxColumn idProducto = new DataGridViewTextBoxColumn();
                idProducto.HeaderText = "idProducto";
                idProducto.Visible = false;
                DataGridViewTextBoxColumn cortesia = new DataGridViewTextBoxColumn();
                cortesia.HeaderText = "cortesia";
                cortesia.Visible = false;
                DataGridViewTextBoxColumn motivoCortesia = new DataGridViewTextBoxColumn();
                motivoCortesia.HeaderText = "motivoCortesia";
                motivoCortesia.Visible = false;
                DataGridViewTextBoxColumn cancelar = new DataGridViewTextBoxColumn();
                cancelar.HeaderText = "cancelar";
                cancelar.Visible = false;
                DataGridViewTextBoxColumn motivoCancelacion = new DataGridViewTextBoxColumn();
                motivoCancelacion.HeaderText = "motivoCancelacion";
                motivoCancelacion.Visible = false;
                DataGridViewTextBoxColumn iIdMascaraItem = new DataGridViewTextBoxColumn();
                iIdMascaraItem.HeaderText = "Mascara";
                iIdMascaraItem.Visible = false;
                DataGridViewTextBoxColumn colSecuenciaImpresion = new DataGridViewTextBoxColumn();
                colSecuenciaImpresion.HeaderText = "Secuencia";
                colSecuenciaImpresion.Visible = false;
                DataGridViewTextBoxColumn colOrdenamiento = new DataGridViewTextBoxColumn();
                colOrdenamiento.HeaderText = "Ordenamiento";
                colOrdenamiento.Visible = false;
                DataGridViewTextBoxColumn colIdOrden = new DataGridViewTextBoxColumn();
                colIdOrden.HeaderText = "IdOrden";
                colIdOrden.Visible = false;
                DataGridViewTextBoxColumn colPagaIva = new DataGridViewTextBoxColumn();
                colIdOrden.HeaderText = "PagaIva";
                colIdOrden.Visible = false;


                dgvOrigenPedido.Columns.Add(guardada);
                dgvOrigenPedido.Columns.Add(cantidad);
                dgvOrigenPedido.Columns.Add(producto);
                dgvOrigenPedido.Columns.Add(valuni);
                dgvOrigenPedido.Columns.Add(valor);
                dgvOrigenPedido.Columns.Add(cod);
                dgvOrigenPedido.Columns.Add(ID);
                dgvOrigenPedido.Columns.Add(idProducto);
                dgvOrigenPedido.Columns.Add(cortesia);
                dgvOrigenPedido.Columns.Add(motivoCortesia);
                dgvOrigenPedido.Columns.Add(cancelar);
                dgvOrigenPedido.Columns.Add(motivoCancelacion);
                dgvOrigenPedido.Columns.Add(iIdMascaraItem);
                dgvOrigenPedido.Columns.Add(colSecuenciaImpresion);
                dgvOrigenPedido.Columns.Add(colOrdenamiento);
                dgvOrigenPedido.Columns.Add(colIdOrden);
                dgvOrigenPedido.Columns.Add(colPagaIva);
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btn_dividir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((reabrir == "OK") || (reabrir == "DIVIDIDO"))
                {
                    crearDataGrid();

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        double numeroMaximo = Convert.ToDouble(dgvPedido[1, i].Value);
                        double dbValorUnitario_P;
                        double dbCantidad_P;
                        double dbIvaObtenido;
                        double dbTotalConSinIva;

                        if (numeroMaximo > 1)
                        {
                            for (int j = 0; j < numeroMaximo; j++)
                            {
                                dbCantidad_P = Convert.ToDouble(dgvPedido[1, i].Value);
                                dbValorUnitario_P = Convert.ToDouble(dgvPedido[3, i].Value);

                                if (Convert.ToInt32(dgvPedido[16, i].Value) == 1)
                                {
                                    dbIvaObtenido = dbValorUnitario_P * Program.iva;
                                    dbTotalConSinIva = dbValorUnitario_P + dbIvaObtenido;
                                }

                                else
                                {
                                    dbTotalConSinIva = dbCantidad_P * dbValorUnitario_P;
                                }

                                dgvOrigenPedido.Rows.Add(new string[] {
                                    Convert.ToString(dgvPedido[0, i].Value),
                                    Convert.ToString(1),
                                    Convert.ToString(dgvPedido[2, i].Value),
                                    Convert.ToString(dgvPedido[3, i].Value),
                                    //Convert.ToString(dgvPedido[3, i].Value),
                                    Convert.ToString(dbTotalConSinIva.ToString("N2")),
                                    Convert.ToString(dgvPedido[5, i].Value),
                                    Convert.ToString(dgvPedido[6, i].Value),
                                    Convert.ToString(dgvPedido[7, i].Value),
                                    Convert.ToString(dgvPedido[8, i].Value),
                                    Convert.ToString(dgvPedido[9, i].Value),
                                    Convert.ToString(dgvPedido[10, i].Value),
                                    Convert.ToString(dgvPedido[11, i].Value),
                                    Convert.ToString(dgvPedido[12, i].Value),
                                    Convert.ToString(dgvPedido[13, i].Value),
                                    Convert.ToString(dgvPedido[14, i].Value),
                                    Convert.ToString(dgvPedido[15, i].Value),
                                    Convert.ToString(dgvPedido[16, i].Value)
                                });
                            }
                        }
                        else
                        {
                            //dbCantidad_P = Convert.ToDouble(dgvPedido[1, i].Value);
                            dbValorUnitario_P = Convert.ToDouble(dgvPedido[3, i].Value);

                            if (Convert.ToInt32(dgvPedido[16, i].Value) == 1)
                            {
                                dbIvaObtenido = dbValorUnitario_P * Program.iva;
                                dbTotalConSinIva = dbValorUnitario_P + dbIvaObtenido;
                            }

                            else
                            {
                                dbTotalConSinIva = dbValorUnitario_P;
                            }

                            dgvOrigenPedido.Rows.Add(new string[] {
                                Convert.ToString(dgvPedido[0, i].Value),
                                Convert.ToString(dgvPedido[1, i].Value),
                                Convert.ToString(dgvPedido[2, i].Value),
                                Convert.ToString(dgvPedido[3, i].Value),
                                //Convert.ToString(dgvPedido[4, i].Value),
                                Convert.ToString(dbTotalConSinIva.ToString("N2")),
                                Convert.ToString(dgvPedido[5, i].Value),
                                Convert.ToString(dgvPedido[6, i].Value),
                                Convert.ToString(dgvPedido[7, i].Value),
                                Convert.ToString(dgvPedido[8, i].Value),
                                Convert.ToString(dgvPedido[9, i].Value),
                                Convert.ToString(dgvPedido[10, i].Value),
                                Convert.ToString(dgvPedido[11, i].Value),
                                Convert.ToString(dgvPedido[12, i].Value),
                                Convert.ToString(dgvPedido[13, i].Value),
                                Convert.ToString(dgvPedido[14, i].Value),
                                Convert.ToString(dgvPedido[15, i].Value),
                                Convert.ToString(dgvPedido[16, i].Value)
                             });
                        }

                    }

                    Pedidos.frmDividirCuenta d = new Pedidos.frmDividirCuenta(dgvOrigenPedido, iIdPedido.ToString(), sPorcentajeDescuento, iIdCajero, iIdMesero, iIdMesa, iIdOrigenOrden, Convert.ToInt32(txt_Orden.Text.Trim()));
                    AddOwnedForm(d);
                    d.ShowDialog();

                    if (d.DialogResult == DialogResult.OK)
                    {
                        d.Close();
                        this.Close();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "La orden aún no ha sido guardada.";
                    ok.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
         
        private void btn_pagar_Click(object sender, EventArgs e)
        {
            if (Program.iPuedeCobrar == 1)
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No hay pedidos para realizar el cobro.";
                    ok.ShowDialog();
                    goto fin;
                }

                else
                {
                    try
                    {
                        sSql = "";
                        sSql += "select count(*) cuenta" + Environment.NewLine;
                        sSql += "from cv403_cab_pedidos" + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedido;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                //PPREGUNTAMOS SI EXISTE O NO EL REGISTRO
                                //SI YA EXISTE EL REGISTRO
                                if (dtConsulta.Rows[0][0].ToString() == "0")
                                {
                                    //INSERTAMOS LA ORDEN NUEVA Y ABRIMOS EL EL FORMULARIO DE PAGOS
                                    insertarComanda(0, 0);


                                    //SI iControlarSecuencia es igual a cero continua el ciclo de inserciones
                                    if (iControlarSecuencia == 0)
                                    {
                                        //SI iControlarSecuencia es igual a cero envia a abrir el formulario de cobros
                                        if (iControlarSecuencia == 0)
                                        {
                                            goto abrir_pagos;
                                        }

                                        else
                                        {
                                            goto reversa;
                                        }
                                    }

                                    else
                                    {
                                        goto reversa;
                                    }
                                }

                                else
                                {
                                    //ACTUALIZAMOS EL PEDIDO
                                    insertarComanda(1, 0);

                                    //SI iControlarSecuencia es igual a cero envia a abrir el formulario de cobros
                                    if (iControlarSecuencia == 0)
                                    {
                                        goto abrir_pagos;
                                    }

                                    else
                                    {
                                        goto reversa;
                                    }
                                }
                            }

                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta 1.";
                                ok.ShowDialog();
                                this.Close();
                            }
                        }

                        else
                        {
                            goto reversa;
                        }
                    }

                    catch (Exception)
                    {
                        goto reversa;
                    }
                }
            }

            else
            {
                ok.LblMensaje.Text = "Su usuario no le permite realizar el cobro de la cuenta.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                goto fin;
            }

        abrir_pagos:
            {
                //ABRIMOS EL FORMULARIO DE PAGOS
                //PagoTarjetas t;
                //t = new PagoTarjetas(iIdPedido.ToString(), Convert.ToDouble(txt_total.Text));
                //AddOwnedForm(t);
                //t.ShowInTaskbar = false;
                //t.ShowDialog();

                //if (t.DialogResult == DialogResult.OK)
                //{
                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //}

                Pedidos.frmCobros t = new Pedidos.frmCobros(iIdPedido.ToString());
                AddOwnedForm(t);
                t.ShowDialog();

                if (t.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                goto fin;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta 2.";
                ok.ShowDialog();
                this.Close();
            }

        fin: { }
        }

        private void btn_cliente_Click(object sender, EventArgs e)
        {
            Pedidos.FDatosCliente cliente = new Pedidos.FDatosCliente(iIdPedido.ToString());
            cliente.ShowDialog();

            if (cliente.DialogResult == DialogResult.OK)
            {
                sAsignarNombreMesa = cliente.txtNombreMesa.Text;
                cliente.Close();
            }
        }

        private void btn_pesonas_Click(object sender, EventArgs e)
        {
            agregarPersonas personas = new agregarPersonas(txt_numeropersonas.Text.Trim());
            personas.ShowDialog();

            if (personas.DialogResult == DialogResult.OK)
            {
                txt_numeropersonas.Text = personas.txt_valor.Text.Trim();
                iNumeroPersonas = Convert.ToInt32(personas.txt_valor.Text.Trim());
                personas.Close();
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No hay ítems en la comanda.";
                    ok.ShowDialog();
                }

                else
                {
                    if (dgvPedido.SelectedRows.Count > 0)
                    {

                        if (Program.iPuedeCobrar == 1)
                        {
                            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                            NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                dgvPedido.Rows.Remove(dgvPedido.CurrentRow);
                                calcularTotales();
                                NuevoSiNo.Close();
                                dgvPedido.ClearSelection();
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Su usuario no le permite remover el ítem. Póngase en contacto con el administrador.";
                            ok.ShowDialog();
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No se ha seleccionado una línea para remover.";
                        ok.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
                       
        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No hay productos ingresados en la comanda.";
                    ok.ShowDialog();
                }

                else
                {       
                    if (reabrir == "OK" || reabrir == "DIVIDIDO")
                    {
                        insertarComanda(1, 1);
                        guardado = true;
                        this.Close();
                    }

                    //===================================================================================
                    //AQUI INSERTA UNA NUEVA ORDEN
                    //===================================================================================
                    else
                    {
                        //INSERCION DE PROMOTOR
                        if (Program.iManejaPromotor == 1)
                        {
                            Promotores.frmSeleccionarPromotor promotor = new Promotores.frmSeleccionarPromotor();
                            promotor.ShowDialog();

                            if (promotor.DialogResult == DialogResult.OK)
                            {
                                iIdPromotor = promotor.iIdPromotor;
                                promotor.Close();
                            }
                        }

                        insertarComanda(0, 1);
                        guardado = true;
                        this.Close();
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btn_mitad_Click(object sender, EventArgs e)
        {
            if (btn_mitad.BackColor == Color.Red)
            {
                Program.factorPrecio = 1;
                btn_mitad.BackColor = Color.FromName("HighLight");
            }
            else
            {
                btn_mitad.BackColor = Color.Red;
                Program.factorPrecio = 0.50;
            }
        }

        private void btnReorden_Click(object sender, EventArgs e)
        {
            try
            {
                Pedidos.frmOpcionesReorden reordenar = new Pedidos.frmOpcionesReorden();
                reordenar.ShowDialog();

                if (reordenar.DialogResult == DialogResult.OK)
                {
                    reordenar.Close();
                    pnlReOrden.Controls.Clear();
                    pnlReOrden.Visible = true;
                    pnlItems.Controls.Clear();
                    pnlReOrden.Controls.Add(pnlItems);
                    pnlReOrden.Controls.Add(btnRegresar);
                    llenarArregloReOrden();
                }

                else if (reordenar.DialogResult == DialogResult.Yes)
                {
                    reordenar.Close();
                    //ACTUALIZACION DE LAS OPCIONES DE REORDEN
                    Pedidos.frmReordenarItems reorden = new Pedidos.frmReordenarItems();
                    AddOwnedForm(reorden);
                    Double dValorConsumo = 0;
                    string sCantidadReorden;
                    string sProductoReorden;
                    string sValorReorden;
                    string sOrdenamientoReorden;
                    string sIdOrdenReorden;

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        sCantidadReorden = (dgvPedido.Rows[i].Cells[1].Value).ToString();
                        sProductoReorden = (dgvPedido.Rows[i].Cells[2].Value).ToString();
                        dValorConsumo = Convert.ToDouble(dgvPedido.Rows[i].Cells[4].Value.ToString());
                        sOrdenamientoReorden = (dgvPedido.Rows[i].Cells[14].Value).ToString();
                        sIdOrdenReorden = (dgvPedido.Rows[i].Cells[15].Value).ToString();
                        sValorReorden = dValorConsumo.ToString("N2");

                        reorden.dgvPedido.Rows.Add(sCantidadReorden, sProductoReorden, sValorReorden, sOrdenamientoReorden, sIdOrdenReorden);
                    }

                    reorden.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.iIdProductoModificador == 0)
                {
                    ok.LblMensaje.Text = "No se encuentra configurado los productos modificadores.";
                    ok.ShowDialog();
                }

                else
                {
                    Extras ex = new Extras(iVersionImpresionComanda);
                    AddOwnedForm(ex);
                    ex.ShowDialog();

                    if (ex.DialogResult == DialogResult.OK)
                    {
                        calcularTotales();
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btn_consumo_alimentos_Click(object sender, EventArgs e)
        {
            try
            {
                Pedidos.frmOpcionesConsumoAlimentos consumo = new Pedidos.frmOpcionesConsumoAlimentos(iConsumoAlimentos);
                consumo.ShowDialog();

                //PARA CONVERTIR CADA LINEA DE PRODUCTO EN CONSUMO DE ALIMENTOS
                if (consumo.DialogResult == DialogResult.OK)
                {
                    consumo.Close();
                    ConsumoAlimentos p = new ConsumoAlimentos();
                    AddOwnedForm(p);
                    Double dValorConsumo = 0;

                    for (int i = 0; i < dgvPedido.Rows.Count; i++)
                    {
                        string cantidad = (dgvPedido.Rows[i].Cells[1].Value).ToString();
                        string producto = (dgvPedido.Rows[i].Cells[2].Value).ToString();
                        dValorConsumo = Convert.ToDouble(dgvPedido.Rows[i].Cells[4].Value.ToString());
                        string valor = dValorConsumo.ToString("N2");

                        p.dgvPedido.Rows.Add(cantidad, producto, valor);
                    }

                    p.ShowDialog();

                    if (p.DialogResult == DialogResult.OK)
                    {
                        calcularTotales();
                    }
                }

                //PARA APLICAR CONSUMO DE ALIMENTOS A TODA LA ORDEN
                else if (consumo.DialogResult == DialogResult.Yes)
                {
                    iConsumoAlimentos = consumo.iSeleccion;

                    if (iConsumoAlimentos == 1)
                    {
                        btn_consumo_alimentos.BackColor = Color.Yellow;
                    }

                    else
                    {
                        btn_consumo_alimentos.BackColor = Color.DeepSkyBlue;
                    }

                    consumo.Close();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnDescuentos_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No hay items seleccionado en la orden.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
                else
                {
                    frmDescuentos descuentos = new frmDescuentos();
                    descuentos.ShowDialog();

                    if (descuentos.DialogResult == DialogResult.OK)
                    {
                        //REALIZAR LAS OPERACIONES PARA LOS DESCUENTOS EN EL GRID
                        iBandera = 1;
                        lblPorcentajeDescuento.Text = Program.dbValorPorcentaje.ToString() + "%";
                        sPorcentajeDescuento = Program.dbValorPorcentaje.ToString();

                        for (int i = 0; i < dgvPedido.Rows.Count; i++)
                        {
                            if ((dgvPedido.Rows[i].Cells["cortesia"].Value.ToString () == "0") && (dgvPedido.Rows[i].Cells["cancelar"].Value.ToString () == "0"))
                            {
                                dgvPedido[0, i].Value = (Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value) * (Program.dbValorPorcentaje / 100)).ToString();
                            }

                            else
                            {
                                dgvPedido[0, i].Value = (Convert.ToDouble(dgvPedido.Rows[i].Cells["cantidad"].Value) * Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value)).ToString();
                            }                            
                        }
                        
                        calcularTotales();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btn_mesas_Click(object sender, EventArgs e)
        {
            try
            {
                Áreas.frmCambioMesa mesas = new Áreas.frmCambioMesa();
                AddOwnedForm(mesas);
                mesas.ShowDialog();

                if (mesas.DialogResult == DialogResult.OK)
                {
                    iIdMesa = mesas.iIdMesa;
                    txt_numeromesa.Text = mesas.sDescripcionMesa.ToUpper();
                    mesas.Close();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void Orden_Load(object sender, EventArgs e)
        {
            //Clases.ClaseRedimension redimension = new Clases.ClaseRedimension();
            //redimension.ResizeForm(this, Program.iLargoPantalla, Program.iAnchoPantalla);

            this.Text = Program.sEtiqueta;

            //ALMACENAMOS LA APERTURA DE LA ORDEN
            if (Program.sCodigoAsignadoOrigenOrden != "01")
            {
                grupoParaMesas.Visible = false;
            }
            else
            {
                grupoParaMesas.Visible = true;
            }          

            if (Program.sCodigoAsignadoOrigenOrden == "06")
            {
                lblPorcentajeDescuento.Text = Program.dbValorPorcentaje.ToString() + "%";
            }

            else
            {
                lblPorcentajeDescuento.Text = sPorcentajeDescuento + "%";
            }

            if (Program.iManejaServicio == 1)
            {
                lblEtiquetaServicio.Visible = true;
                txt_servicio.Visible = true;
                lblEtiquetaServicio.Text = "SERVICIO " + (Program.servicio * 100).ToString() + "%";
            }

            else
            {
                txt_servicio.Visible = false;
                lblEtiquetaServicio.Visible = false;
            }

            if (reabrir == "OK" || reabrir == "DIVIDIDO")
            {
                //CONSULTAR EL REGISTRO PARA OBTENER EL NUMERO DE VERSION DE IMPRESION DE LA COMANDA
                versionImpresion();
            }

            else
            {
                iVersionImpresionComanda = 1;

                if (Program.sCodigoAsignadoOrigenOrden == "03")
                {
                    consultarMovilizacion();
                }
            }

            lblEtiquetaSubtotal.Text = "SUBTOTAL " + (Program.iva * 100).ToString() + "%";
            lblEtiquetaIVA.Text = "IVA " + (Program.iva * 100).ToString() + "%";

            dgvPedido.ClearSelection();

            this.ActiveControl = label6;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlReOrden.Visible = false;
        }

        private void btnReimprimirCocina_Click(object sender, EventArgs e)
        {
            try
            {
                if ((reabrir == "OK") || (reabrir == "DIVIDIDO"))
                {
                    if ((iVersionImpresionComanda - 1) == 1)
                    {
                        Pedidos.frmVerReporteCocinaTextBox cocina = new Pedidos.frmVerReporteCocinaTextBox(iIdPedido.ToString(), iVersionImpresionComanda - 1);
                        cocina.ShowDialog();
                    }

                    else
                    {
                        Pedidos.frmVersionesCocina cocina = new Pedidos.frmVersionesCocina(iIdPedido, iVersionImpresionComanda - 1);
                        cocina.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "La orden aún no ha sido guardada.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.iIdProductoNuevoItem == 0)
                {
                    ok.LblMensaje.Text = "No se encuentra configurado la sección Ítem. Favor comúniquese con el administrador.";
                    ok.ShowDialog();
                }

                else
                {
                    Items i = new Items(iVersionImpresionComanda);
                    AddOwnedForm(i);
                    i.ShowDialog();

                    if (i.DialogResult == DialogResult.OK)
                    {
                        calcularTotales();
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnCortesias_Click(object sender, EventArgs e)
        {
            try
            {
                Cortesias a = new Cortesias(iIdPedido.ToString());
                AddOwnedForm(a);

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    Double dValor;
                    double numeroMaximo = Convert.ToDouble(dgvPedido[1, i].Value);
                    if (numeroMaximo > 1)
                    {
                        for (int j = 0; j < numeroMaximo; j++)
                        {
                            dValor = Convert.ToDouble(dgvPedido[3, i].Value);
                            a.dgvPedido.Rows.Add(new string[] {
                                Convert.ToString(dgvPedido[0, i].Value),
                                Convert.ToString(1),
                                Convert.ToString(dgvPedido[2, i].Value),
                                Convert.ToString(dgvPedido[3, i].Value),
                                dValor.ToString("N2"),
                                Convert.ToString(dgvPedido[5, i].Value),
                                Convert.ToString(dgvPedido[6, i].Value),
                                Convert.ToString(dgvPedido[7, i].Value),
                                Convert.ToString(dgvPedido[8, i].Value),
                                Convert.ToString(dgvPedido[9, i].Value),
                                Convert.ToString(dgvPedido[10, i].Value),
                                Convert.ToString(dgvPedido[11, i].Value),
                                Convert.ToString(dgvPedido[12, i].Value),
                                Convert.ToString(dgvPedido[13, i].Value),
                                Convert.ToString(dgvPedido[14, i].Value),
                                Convert.ToString(dgvPedido[15, i].Value),
                                Convert.ToString(dgvPedido[16, i].Value)
                            });
                        }
                    }
                    else
                    {
                        dValor = Convert.ToDouble(dgvPedido[4, i].Value);
                        a.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            dValor.ToString("N2"),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                            Convert.ToString(dgvPedido[8, i].Value),
                            Convert.ToString(dgvPedido[9, i].Value),
                            Convert.ToString(dgvPedido[10, i].Value),
                            Convert.ToString(dgvPedido[11, i].Value),
                            Convert.ToString(dgvPedido[12, i].Value),
                            Convert.ToString(dgvPedido[13, i].Value),
                            Convert.ToString(dgvPedido[14, i].Value),
                            Convert.ToString(dgvPedido[15, i].Value),
                            Convert.ToString(dgvPedido[16, i].Value)
                        });
                    }

                }

                a.ShowDialog();

                if (a.DialogResult == DialogResult.OK)
                {
                    calcularTotales();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnCancelarItems_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarPedido p = new CancelarPedido(iIdPedido, referencia);
                AddOwnedForm(p);

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    Double dValor;
                    double numeroMaximo = Convert.ToDouble(dgvPedido[1, i].Value);
                    if (numeroMaximo > 1)
                    {
                        for (int j = 0; j < numeroMaximo; j++)
                        {
                            dValor = Convert.ToDouble(dgvPedido[3, i].Value);
                            p.dgvPedido.Rows.Add(new string[] {
                                Convert.ToString(dgvPedido[0, i].Value),
                                Convert.ToString(1),
                                Convert.ToString(dgvPedido[2, i].Value),
                                Convert.ToString(dgvPedido[3, i].Value),
                                dValor.ToString("N2"),
                                Convert.ToString(dgvPedido[5, i].Value),
                                Convert.ToString(dgvPedido[6, i].Value),
                                Convert.ToString(dgvPedido[7, i].Value),
                                Convert.ToString(dgvPedido[8, i].Value),
                                Convert.ToString(dgvPedido[9, i].Value),
                                Convert.ToString(dgvPedido[10, i].Value),
                                Convert.ToString(dgvPedido[11, i].Value),
                                Convert.ToString(dgvPedido[12, i].Value),
                                Convert.ToString(dgvPedido[13, i].Value),
                                Convert.ToString(dgvPedido[14, i].Value),
                                Convert.ToString(dgvPedido[15, i].Value),
                                Convert.ToString(dgvPedido[16, i].Value)
                            });
                        }
                    }
                    else
                    {
                        dValor = Convert.ToDouble(dgvPedido[4, i].Value);
                        p.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            dValor.ToString("N2"),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                            Convert.ToString(dgvPedido[8, i].Value),
                            Convert.ToString(dgvPedido[9, i].Value),
                            Convert.ToString(dgvPedido[10, i].Value),
                            Convert.ToString(dgvPedido[11, i].Value),
                            Convert.ToString(dgvPedido[12, i].Value),
                            Convert.ToString(dgvPedido[13, i].Value),
                            Convert.ToString(dgvPedido[14, i].Value),
                            Convert.ToString(dgvPedido[15, i].Value),
                            Convert.ToString(dgvPedido[16, i].Value)
                         });
                    }

                }

                p.ShowDialog();

                if (p.DialogResult == DialogResult.OK)
                {
                    //dgvPedido.Rows.Clear();

                    //for (int i = 0; i < p.dgvDatos.Rows.Count; i++)
                    //{
                    //    dgvPedido.Rows.Add();
                    //    for (int j = 0; j < p.dgvDatos.Columns.Count; j++)
                    //    {
                    //        dgvPedido.Rows[i].Cells[j].Value = p.dgvDatos.Rows[i].Cells[j].Value.ToString();
                    //    }
                    //}                   
                    
                    calcularTotales();
                    p.Close();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgvPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iFila = dgvPedido.CurrentCell.RowIndex;

            Pedidos.frmAumentaRemueveItems sumar = new Pedidos.frmAumentaRemueveItems(iFila);
            sumar.lblItem.Text = dgvPedido.CurrentRow.Cells["producto"].Value.ToString();
            sumar.lblCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.txtCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
            sumar.ShowDialog();

            if (sumar.DialogResult == DialogResult.OK)
            {
                dgvPedido.Rows[iFila].Cells["cantidad"].Value = sumar.sValorRetorno;
                dbCantidadRecalcular = Convert.ToDouble(dgvPedido.Rows[iFila].Cells["cantidad"].Value.ToString());
                dbPrecioRecalcular = Convert.ToDouble(dgvPedido.Rows[iFila].Cells["valuni"].Value.ToString());
                dbValorTotalRecalcular = dbCantidadRecalcular * dbPrecioRecalcular;
                dgvPedido.Rows[iFila].Cells["valor"].Value = dbValorTotalRecalcular.ToString("N2");
                calcularTotales();
                sumar.Close();
                dgvPedido.ClearSelection();
            }            
        }

        private void btnEditarItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedido.Rows.Count > 0)
                {
                    Pedidos.frmEditarItems item = new Pedidos.frmEditarItems();
                    item.txtProducto.Text = dgvPedido.CurrentRow.Cells["producto"].Value.ToString();
                    item.txtCantidad.Text = dgvPedido.CurrentRow.Cells["cantidad"].Value.ToString();
                    item.txtTotal.Text = dgvPedido.CurrentRow.Cells["valor"].Value.ToString();
                    item.iIdProducto = Convert.ToInt32(dgvPedido.CurrentRow.Cells["idProducto"].Value);
                    item.ShowDialog();
                }

                else
                {
                    ok.LblMensaje.Text = "No hay ningún item ingresado para realizar variaciones.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_modificar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_modificar);
        }

        private void btn_modificar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn_modificar);
        }

        private void btnReimprimirCocina_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReimprimirCocina);
        }

        private void btnReimprimirCocina_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReimprimirCocina);
        }

        private void btn_mitad_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_mitad);
        }

        private void btn_mitad_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn_mitad);
        }

        private void btn_consumo_alimentos_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_consumo_alimentos);
        }

        private void btn_consumo_alimentos_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn_consumo_alimentos);
        }

        private void btnReorden_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReorden);
        }

        private void btnReorden_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReorden);
        }

        private void btnCortesias_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCortesias);
        }

        private void btnCortesias_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCortesias);
        }

        private void btnItems_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnItems);
        }

        private void btnItems_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnItems);
        }

        private void btnDescuentos_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDescuentos);
        }

        private void btnDescuentos_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDescuentos);
        }

        private void btn_dividir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_dividir);
        }

        private void btn_dividir_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn_dividir);
        }

        private void btnCancelarItems_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelarItems);
        }

        private void btnCancelarItems_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelarItems);
        }

        private void btnEditarItems_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEditarItems);
        }

        private void btnEditarItems_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEditarItems);
        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSalir);
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.OrangeRed;
            btnSalir.ForeColor = Color.Black;
        }

        private void btn_pagar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_pagar);
        }

        private void btn_pagar_MouseLeave(object sender, EventArgs e)
        {
            btn_pagar.BackColor = Color.Lime;
            btn_pagar.ForeColor = Color.Black;
        }

        private void btn_aceptar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_aceptar);
        }

        private void btn_aceptar_MouseLeave(object sender, EventArgs e)
        {
            btn_aceptar.BackColor = Color.Lime;
            btn_aceptar.ForeColor = Color.Black;
        }

        private void btn_eliminar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_eliminar);
        }

        private void btn_eliminar_MouseLeave(object sender, EventArgs e)
        {
            btn_eliminar.BackColor = Color.Orange;
            btn_eliminar.ForeColor = Color.Black;
        }

        private void btn_mesas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_mesas);
        }

        private void btn_mesas_MouseLeave(object sender, EventArgs e)
        {
            btn_mesas.BackColor = Color.Yellow;
            btn_mesas.ForeColor = Color.Black;
        }

        private void btn_cliente_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_cliente);
        }

        private void btn_cliente_MouseLeave(object sender, EventArgs e)
        {
            btn_cliente.BackColor = Color.Yellow;
            btn_cliente.ForeColor = Color.Black;
        }

        private void btn_pesonas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn_pesonas);
        }

        private void btn_pesonas_MouseLeave(object sender, EventArgs e)
        {
            btn_pesonas.BackColor = Color.Yellow;
            btn_pesonas.ForeColor = Color.Black;
        }

        private void Orden_KeyDown(object sender, KeyEventArgs e)
        {
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

            if (e.Control && e.KeyCode == Keys.S)
            {
                btn_aceptar_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                btn_pagar_Click(sender, e);
            }

        }        
    }
}