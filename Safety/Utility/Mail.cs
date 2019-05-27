using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace HelpDeskApp.Models
{
    public static class Mail
    {
        private static SmtpClient server = new SmtpClient();
        private static MailMessage mailMessage;
        private static string user;
        private static string password;
        public static string HelpDeskMail { get; set; }
        
        static Mail()
        {
            user = "cau@hidroelectrica.gov.do";
            password = "as123456";

            HelpDeskMail = "Mesadeayuda@hidroelectrica.gov.do";

            //If you need to authenticate
            server.Credentials = new NetworkCredential(user, password);
            server.Port = 587;
            server.Host = "smtp.office365.com";
            server.EnableSsl = true;
        }

        public static void Send(string to, string subject, string body)
        {
            mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(user);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            SendMessage();
        }

        public static void Send(string subject, string body, List<string> to)
        {
            mailMessage = new MailMessage();

            //Put all the emails and verify are unique.
            foreach (var item in to.Distinct().ToList())
            {
                mailMessage.To.Add(new MailAddress(item));
            }

            mailMessage.From = new MailAddress(user);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            SendMessage();
        }

        public static void Send(string subject, string body, params string [] to)
        {
            mailMessage = new MailMessage();

            //Put all the emails and verify are unique.
            foreach (var item in to.Distinct().ToList())
            {
                mailMessage.To.Add(new MailAddress(item));
            }

            mailMessage.From = new MailAddress(user);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            SendMessage();
        }

        private static bool SendMessage()
        {
            bool result = true;
            try
            {
                server.Send(mailMessage);
            }
            catch (SmtpFailedRecipientException error)
            {
                result = false;
                Debug.Write("error: " + error.Message + "\nFailing recipient: " + error.FailedRecipient);
            }
            return result;
        }
    }
}