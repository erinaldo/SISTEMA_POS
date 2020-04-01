using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;
using System.IO;

namespace Palatium.Utilitarios
{
    public partial class frmHuellas : Form
    {
        string huellaBase64 = "";
        byte[] huellaByte = null;
        Image huellaImagen = null;

        string copiaBase64 = "";
        byte[] copiaByte = null;
        Image copiaImagen = null;

        AxZKFPEngX fp = new AxZKFPEngX();
        
        int fpcHandle = 0;

        public frmHuellas()
        {
            InitializeComponent();
        }

        private void frmHuellas_Load(object sender, EventArgs e)
        {
            btnConectar.Enabled = true;
            huellas_1.SizeMode = PictureBoxSizeMode.StretchImage;
            huellas_2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void botonPresionado(string nombre = "")
        {
            foreach (var ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    if (((Button)ctrl).Name == nombre)
                    {
                        ((Button)ctrl).BackColor = Color.LightBlue;
                    }

                    else
                    {
                        ((Button)ctrl).BackColor = Control.DefaultBackColor;
                    }
                }
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            botonPresionado("btnConectar");

            fp.SensorIndex = 0;

            if (rdbV9.Checked == true)
                fp.FPEngineVersion = "9";
            else
                fp.FPEngineVersion = "10";

            if (fp.InitEngine() == 0)
            {
                foreach (var ctr in this.Controls)
                {
                    if (ctr is Button)
                    {
                        ((Button)ctr).Enabled = true;
                    }
                }

                btnConectar.Enabled = false;

                fpcHandle = fp.CreateFPCacheDB();

                fp.OnImageReceived += fp_OnImageReceived;
                fp.OnCapture += fp_OnCapture;
            }
        }

        private void fp_OnImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            object imgdata = new object();
            string base64 = fp.GetTemplateAsString();
            bool b = fp.GetFingerImage(ref imgdata);

            if (b == true)
            {
                byte[] data = (byte[])imgdata;
                MemoryStream ms = new MemoryStream(data);
                Image image = Image.FromStream(ms);

                huellas_1.Image = image;

                huellaByte = data;
                huellaImagen = image;
            }
        }

        private void fp_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            huellaBase64 = fp.GetTemplateAsString();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            botonPresionado("btnDesconectar");

            fp.OnImageReceived -= fp_OnImageReceived;
            fp.OnCapture -= fp_OnCapture;
            fp.FreeFPCacheDB(fpcHandle);
            fp.EndEngine();

            foreach (var ctr in this.Controls)
            {
                if (ctr is Button)
                {
                    ((Button)ctr).Enabled = true;
                }
            }

            btnConectar.Enabled = true;
            //btnLimpiar_Click(null, null);
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            bool ARegFeatureChanged = true;
            bool result = false;

            //result = fp.VerFingerFromStr(huellaBase64, copiaBase64, false, ARegFeatureChanged);

            if (result)
                MessageBox.Show("Base 64: Huellas coinciden.");
        }




    }
}
