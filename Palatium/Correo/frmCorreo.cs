using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Palatium.Correo
{
    public partial class frmCorreo : Form
    {
        char[] delimitador_cc = { ',' };
        char[] delimitador_adjunto = { '|' };
        string sHost = "smtp-mail.outlook.com";
        int iPuerto = 587;

        public frmCorreo()
        {
            InitializeComponent();
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            OpenFileDialog menu_abrir = new OpenFileDialog();
            menu_abrir.Filter = "Todos los archivos|*.*";
            menu_abrir.Title = "Abrir...";
            menu_abrir.InitialDirectory = @"C:\";
            if (menu_abrir.ShowDialog() == DialogResult.OK)
            {
                if (txtAdjuntos.Text == "" || txtAdjuntos.Text == null) txtAdjuntos.Text = menu_abrir.FileName;
                else txtAdjuntos.Text += @"|" + menu_abrir.FileName;
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Clases.ClaseEnviarCorreo correo = new Clases.ClaseEnviarCorreo();
            Cursor = Cursors.WaitCursor;
            string [] sCorreo = correo.enviarCorreo(txtHost.Text, Convert.ToInt32(txtPuerto.Text), txtCorreoRemitente.Text, txtContraseñaRemitente.Text, txtNombreRemitente.Text, txtCorreoDestinatario.Text, txtCC.Text, txtBcc.Text, txtAsunto.Text, txtAdjuntos.Text, txtCuerpo.Text, 1);
            if (sCorreo[0] == "1")
                MessageBox.Show(sCorreo[1],"Correo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show(sCorreo[1],"Correo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Cursor = Cursors.Arrow;
        }

        //Función para enviar el correo
        //public void enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo)
        //{
        //    try
        //    {
        //        SmtpClient cliente = new SmtpClient(host, puerto);
        //        MailMessage correo = new MailMessage();

        //        correo.From = new MailAddress(remitente, nombre);
        //        correo.Body = cuerpo;
        //        correo.Subject = asunto;
        //        if (destinatarios == "") { }
        //        else
        //        {
        //            string[] cadena = destinatarios.Split(delimitador_cc);
        //            foreach (string word in cadena) correo.To.Add(word.Trim());
        //        }
        //        if (cc == "") { }
        //        else
        //        {
        //            string[] cadena1 = cc.Split(delimitador_cc);
        //            foreach (string word in cadena1) correo.CC.Add(word.Trim());
        //        }


        //        if (adjuntos == "") { }
        //        else
        //        {
        //            string[] cadena2 = adjuntos.Split(delimitador_adjunto);
        //            foreach (string word in cadena2) correo.Attachments.Add(new Attachment(word));
        //        }
        //        cliente.Credentials = new NetworkCredential(remitente, contraseña);
        //        if(chkEnableSSL.Checked == true)
        //            cliente.EnableSsl = true;
        //        cliente.Send(correo);

        //        MessageBox.Show("El correo se ha enviado correctamente","Correo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //        limpiarCampos();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //    Cursor = Cursors.Arrow;
        //}

        private void frmCorreo_Load(object sender, EventArgs e)
        {
            txtHost.Text = sHost ;
            txtPuerto.Text = iPuerto.ToString();
            chkEnableSSL.Checked = true;
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            txtNombreRemitente.Text = "";
            txtAdjuntos.Text = "";
            txtAsunto.Text = "";
            txtCC.Text = "";
            txtContraseñaRemitente.Text = "";
            txtCorreoDestinatario.Text = "";
            txtCorreoRemitente.Text = "";
            txtCuerpo.Text = "";
            txtNombreDestinatario.Text = "";
            txtNombreRemitente.Text = "";
        }




        //Fin de la clase
    }
}
