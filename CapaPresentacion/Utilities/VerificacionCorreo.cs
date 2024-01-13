using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace CapaPresentacion.Utilities
{
    public class VerificacionCorreo
    {
        public int Enviar(string emisor, string clave, string receptor)
        {
            Random oRandom = new Random();
            int numero = oRandom.Next(100000, 1000000);
            MailMessage msg = new MailMessage();
            msg.To.Add(receptor);
            msg.Subject = "Correo de verificación";
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = "Su codigo de verificacion es " + numero + "ingrese este numero en el sistema";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emisor);
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(emisor, clave);
            client.Port = 587;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";

            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo enviar el correo, por favor, verifique");
                numero = 0;
            }

            return numero;
        }
    }
}
