using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Security;

namespace CUCMarca.BusinessServices
{

    public enum SendMailResult : int
    { Success = 0, Failed = 1 }

    public class SendMailResponse
    { 
        public string Message { get; set; }
        public SendMailResult Result { get; set; }
    }

    public class SendMail
    {
        public SendMail()
        { }

        public SendMailResponse SendEmail(string message, string subject, string to, bool isHtml)
        {
            string server = ConfigurationManager.AppSettings["server"];
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            bool auth = bool.Parse(ConfigurationManager.AppSettings["enableAuth"]);
            bool ssl = bool.Parse(ConfigurationManager.AppSettings["enableSSL"]);
            string from = ConfigurationManager.AppSettings["mailFrom"];
            string nameFrom = ConfigurationManager.AppSettings["nameFrom"];
            return SendEmail(server, port, username, password, auth, ssl, message, subject, from, to, nameFrom, isHtml);
        }

        public SendMailResponse SendEmail(string server, int port, string username, string password,
            bool auth, bool ssl, string message, string subject, string from, string to, string nameFrom,
            bool isHtml)
        {
            try
            {
                SmtpClient client = new SmtpClient(server, port);
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate,
                      X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                client.EnableSsl = ssl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                if (auth)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(username, password);
                }
                MailAddress mailFrom = new MailAddress(from, nameFrom, Encoding.UTF8);
                MailAddress mailTo = new MailAddress(to);
                MailMessage mailMessage = new MailMessage(mailFrom, mailTo)
                {
                    Body = message,
                    IsBodyHtml = isHtml,
                    BodyEncoding = Encoding.UTF8,
                    Subject = subject,
                    SubjectEncoding = Encoding.UTF8,
                    Priority = MailPriority.High,
                };
                //if (!string.IsNullOrEmpty(attachment1.Trim()))
                //{
                //    Attachment att = new Attachment(attachment1);
                //    mailMessage.Attachments.Add(att);
                //}
                //if (!string.IsNullOrEmpty(attachment2.Trim()))
                //{
                //    Attachment att = new Attachment(attachment2);
                //    mailMessage.Attachments.Add(att);
                //}
                //if (!string.IsNullOrEmpty(attachment3.Trim()))
                //{
                //    Attachment att = new Attachment(attachment3);
                //    mailMessage.Attachments.Add(att);
                //}
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

                client.Send(mailMessage);
                return new SendMailResponse() 
                { 
                    Message = "Se ha enviado correo a la dirección indicada.",
                    Result = SendMailResult.Success
                };
            }
            catch (Exception exc)
            {
                return new SendMailResponse()
                {
                    Message = "Error enviando correo. " + exc.Message + exc.StackTrace,
                    Result = SendMailResult.Failed
                };
            }
        }

        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
