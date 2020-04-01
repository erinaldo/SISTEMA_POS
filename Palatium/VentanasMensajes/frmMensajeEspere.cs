using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace Palatium.VentanasMensajes
{
    public partial class frmMensajeEspere : Form
    {
        public string sTitulo { get; set; }
        public string sMensaje { get; set; }
        public Image iImagen { get; set; }

        public Action AccionEjecutar { get; set; }

        Timer timer;

        public frmMensajeEspere()
        {
            InitializeComponent();            
        }

        private void taskCompleted()
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    Close();
                    DialogResult = DialogResult.OK;
                }));
            }
        }

        private void frmMensajeEspere_Load(object sender, EventArgs e)
        {
            //timer = new Timer();
            //timer.Interval = 5;

            //timer.Tick += timer_Tick;
            //this.Show();
            //timer.Start();

            Task.Factory.StartNew(AccionEjecutar).ContinueWith((t) => taskCompleted());
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //timer.Stop();
        }
    }
}
