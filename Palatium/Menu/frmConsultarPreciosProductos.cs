using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Menú
{
    public partial class frmConsultarPreciosProductos : Form
    {
        Clases.ClaseCategorias vector = new Clases.ClaseCategorias();
        Clases.ClaseLimpiarArreglos limpiar = new Clases.ClaseLimpiarArreglos();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseItems items;
        Clases.ClaseSubcategoria subcategoria;
        Clases.ClasePreciosItems precios;

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Button botonSel;
        Button botonSel1;
        Button botonSel4;
        Button[,] boton1 = new Button[15, 5];
        Button[,] boton2 = new Button[3, 2];
        Button[,] boton3 = new Button[8, 3];
        Button[,] boton4 = new Button[3, 2];
        Button[,] boton5 = new Button[10, 3];
        Button[,] btnSubcategoria = new Button[10, 2];

        Button[,] boton = new Button[20, 2];

        public frmConsultarPreciosProductos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        public void mostrarBotones()
        {
            int contador = 0;
            pnlFamilias.Controls.Clear();
            pnlItemsCategorias.Controls.Clear();

            if (vector.llenarDatos() == true)
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {

                        boton[i, j] = new Button();
                        boton[i, j].Click += boton_clic;
                        boton[i, j].Width = 110;
                        boton[i, j].Height = 65;
                        boton[i, j].Top = i * 65;
                        boton[i, j].Left = j * 110;
                        boton[i, j].BackColor = Color.Lime;
                        boton[i, j].Font = new Font("Arial", 10, FontStyle.Bold);

                        if (contador == vector.cuenta)
                        {
                            break;
                        }

                        boton[i, j].Tag = vector.categorias[contador].sSubcategoria;
                        boton[i, j].Text = vector.categorias[contador].iDescripcion;
                        boton[i, j].Name = vector.categorias[contador].sCodigo;
                        boton[i, j].AccessibleName = vector.categorias[contador].iCodigo;


                        this.Controls.Add(boton[i, j]);
                        pnlFamilias.Controls.Add(boton[i, j]);
                        contador++;
                    }

                }
            }
        }


        //FUNCION PARA CARGAR LOS ITEMS DE LAS CATEGORIAS
        private void boton_clic(object sender, EventArgs e)
        {

            botonSel = sender as Button;

            //botonSel.BackColor = Color.Blue;

            if (Convert.ToInt32(botonSel.Tag) == 0)
            {
                lblEtiquetaFamilia.Text = botonSel.Text;
                pnlItemsCategorias.Controls.Clear();
                items = new Clases.ClaseItems(botonSel.Name, 3);
                crearBotones(items);
            }
            else
            {
                pnlItemsCategorias.Controls.Clear();
                subcategoria = new Clases.ClaseSubcategoria(botonSel.AccessibleName);
                crearBotones2(subcategoria);
            }
        }

        //FUNCION PARA CREAR LOS BOTONES DE CATEGORIAS
        public void crearBotones(Palatium.Clases.ClaseItems items)
        {
            pnlItemsCategorias.Controls.Clear();
            pnlItemsCategorias.Visible = true;
            int h = 0;

            if (items.llenarDatos() == true)
            {

                for (int i = 0; i < 15; i++)
                {

                    for (int j = 0; j < 5; j++)
                    {
                        boton1[i, j] = new Button();
                        boton1[i, j].Click += boton_clic1;
                        boton1[i, j].Width = 120;
                        boton1[i, j].Height = 70;
                        boton1[i, j].Top = i * 70;
                        boton1[i, j].Left = j * 120;
                        boton1[i, j].Font = new Font("Arial", 10, FontStyle.Bold);

                        if (h == items.cuenta)
                        {
                            break;
                        }

                        boton1[i, j].Tag = h;
                        boton1[i, j].AccessibleName = items.items[h].iCodigo;
                        //boton1[i, j].BackColor = Color.GhostWhite;
                        boton1[i, j].BackColor = Color.FromArgb(255, 255, 128);
                        //255; 255; 128

                        boton1[i, j].Text = items.items[h].iDescripcion;

                        this.Controls.Add(boton1[i, j]);

                        pnlItemsCategorias.Controls.Add(boton1[i, j]);
                        h++;
                    }
                }
            }
        }


        //Función para crear botones de subcategorias
        public void crearBotones2(Palatium.Clases.ClaseSubcategoria subcategoria)
        {
            pnlItemsCategorias.Controls.Clear();
            pnlItemsCategorias.Visible = true;
            int h = 0;

            if (subcategoria.llenarDatos() == true)
            {

                for (int i = 0; i < 10; i++)
                {

                    for (int j = 0; j < 2; j++)
                    {
                        btnSubcategoria[i, j] = new Button();
                        btnSubcategoria[i, j].Click += boton_clic2;
                        btnSubcategoria[i, j].Width = 120;
                        btnSubcategoria[i, j].Height = 70;
                        btnSubcategoria[i, j].Top = i * 70;
                        btnSubcategoria[i, j].Left = j * 120;

                        if (h == subcategoria.cuenta)
                        {
                            break;
                        }

                        btnSubcategoria[i, j].Tag = h;
                        //btnSubcategoria[i, j].BackColor = Color.GhostWhite;
                        btnSubcategoria[i, j].BackColor = Color.FromArgb(255, 255, 128);
                        btnSubcategoria[i, j].Name = subcategoria.Subcategoria[h].sCodigo;
                        btnSubcategoria[i, j].Text = subcategoria.Subcategoria[h].sDescripcion;

                        this.Controls.Add(btnSubcategoria[i, j]);

                        pnlItemsCategorias.Controls.Add(btnSubcategoria[i, j]);
                        h++;
                    }
                }
            }
        }


        private void boton_clic2(object sender, EventArgs e)
        {
            botonSel4 = sender as Button;

            botonSel4.BackColor = Color.Cyan;
            pnlItemsSubcategoria.Controls.Clear();

            pnlItemsSubcategoria.Visible = true;
            items = new Clases.ClaseItems(botonSel4.Name, 4);
            crearBotonesItemsSubcategorias(items);
            pnlItemsCategorias.Controls.Add(pnlItemsSubcategoria);
        }


        //FUNCION PARA CREAR LOS BOTONES PARA MOSTRAR LOS ITEMS DE SUBCATEGORIAS
        public void crearBotonesItemsSubcategorias(Palatium.Clases.ClaseItems items)
        {

            int h = 0;

            if (items.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (h == items.cuenta)
                        {
                            break;
                        }

                        boton5[i, j] = new Button();
                        boton5[i, j].Click += boton_clic1;
                        boton5[i, j].Width = 120;
                        boton5[i, j].Height = 63;
                        boton5[i, j].Top = i * 63;
                        boton5[i, j].Left = j * 120;
                        boton5[i, j].Visible = true;
                        boton5[i, j].Tag = h;
                        //boton5[i, j].BackColor = Color.GhostWhite;
                        boton5[i, j].BackColor = Color.FromArgb(255, 255, 128);
                        boton5[i, j].Text = items.items[h].iDescripcion;
                        boton5[i, j].AccessibleName = items.items[h].iCodigo;

                        this.Controls.Add(boton5[i, j]);

                        pnlItemsSubcategoria.Controls.Add(boton5[i, j]);
                        h++;
                    }

                }
            }
        }


        private void boton_clic1(object sender, EventArgs e)
        {
            try
            {
                botonSel1 = sender as Button;
                precios = new Clases.ClasePreciosItems(Convert.ToInt32(botonSel1.AccessibleName));

                Menu.frmConsultarPrecio precio = new Menu.frmConsultarPrecio(Convert.ToInt32(botonSel1.AccessibleName));
                precio.ShowDialog();
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmConsultarPreciosProductos_Load(object sender, EventArgs e)
        {
            mostrarBotones();
        }

        private void frmConsultarPreciosProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
