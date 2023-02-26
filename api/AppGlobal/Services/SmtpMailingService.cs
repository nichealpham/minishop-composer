using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace AppGlobal.Services
{
    public class SmtpMailingService
    {
        private SmtpMailingServiceConfig SmtpConfig { get; set; }

        public SmtpMailingService(SmtpMailingServiceConfig smtpConfig)
        {
            SmtpConfig = smtpConfig;
        }

        public bool SendEmail(EmailSender sender, IList<EmailReceiver> receivers, string subject, string bodyHtml)
        {
            try
            {
                var credentials = new NetworkCredential(sender.SenderName, sender.Password);

                var mail = new MailMessage()
                {
                    From = new MailAddress(sender.Email, sender.SenderName),
                    Subject = subject,
                    Body = bodyHtml,
                    IsBodyHtml = true
                };

                foreach (var receiver in receivers)
                {
                    mail.To.Add(new MailAddress(receiver.Email, receiver.ReceiverName));
                }

                var client = new SmtpClient()
                {
                    Host = SmtpConfig.Host,
                    Port = SmtpConfig.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            
            return true;
        }
    }

    public class EmailReceiver
    {
        public string ReceiverName { get; set; }

        public string Email { get; set; }
    }

    public class EmailSender
    {
        public string SenderName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class SmtpMailingServiceConfig
    {
        public int Port { get; set; }

        public string Host { get; set; }
    }
}
