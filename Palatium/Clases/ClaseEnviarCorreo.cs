using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Palatium.Clases
{
    class ClaseEnviarCorreo
    {
        char[] delimitador_cc = { ',' };
        char[] delimitador_adjunto = { '|' };

       // Función para enviar un correo
        public string [] enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc,string bcc, string asunto, string adjuntos, string cuerpo, int iEnableSSL)
        {
            try
            {
                SmtpClient cliente = new SmtpClient(host, puerto);
                MailMessage correo = new MailMessage();

                correo.From = new MailAddress(remitente, nombre);
                correo.Body = cuerpo;
                correo.Subject = asunto;
                if (destinatarios == "") { }
                else
                {
                    string[] cadena = destinatarios.Split(delimitador_cc);
                    foreach (string word in cadena) correo.To.Add(word.Trim());
                }
                if (cc == "") { }
                else
                {
                    string[] cadena1 = cc.Split(delimitador_cc);
                    foreach (string word in cadena1) correo.CC.Add(word.Trim());
                }

                if (bcc == "") { }
                else
                {
                    string[] cadena1 = bcc.Split(delimitador_cc);
                    foreach (string word in cadena1) correo.Bcc.Add(word.Trim());
                }


                if (adjuntos == "") { }
                else
                {
                    string[] cadena2 = adjuntos.Split(delimitador_adjunto);
                    foreach (string word in cadena2) correo.Attachments.Add(new Attachment(word));
                }
                cliente.Credentials = new NetworkCredential(remitente, contraseña);

                if(iEnableSSL == 1)
                    cliente.EnableSsl = true;

                cliente.Send(correo);

                string [] datos = new string [2] { "1", "El correo se ha enviado correctamente" };
                return  datos ;
              
            }
            catch (Exception ex)
            {
                string[] datos = new string[2] { "0", ex.Message };
                return datos;
            }
        }

    }
}
