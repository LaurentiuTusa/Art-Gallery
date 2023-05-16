using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Art_Gallery.DAL.Email_Logs
{
    public class EmailSender
    {
        public void SendEmail(string email, string subject, string body)
        {
            string sender = ConstantStrings.sender;
            string password = ConstantStrings.password;
            string smtpHost = ConstantStrings.smtpHost;
            int smtpPort = 587;

            using (var message = new MailMessage(sender, email))
            {
                message.Subject = subject;
                message.Body = body;

                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(sender, password);
                    client.Send(message);
                }
            }
        }
    }
}
