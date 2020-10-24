using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace LetsDoIt.MailSender
{    
    using Options;    

    public class MailSender : IMailSender
    {
        private readonly SmtpOptions _smtpOptions;

        public MailSender(IOptions<SmtpOptions> smtpOptions)
        {
            if (smtpOptions == null) throw new ArgumentNullException(nameof(smtpOptions), "SmtpOptions shouldn't be null!");

            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendAsync(string subject, string htmlContent, params string[] toEmails)
        {
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlContent)) throw new ArgumentNullException(nameof(htmlContent));
            if (toEmails == null || toEmails.Length == 0) throw new ArgumentNullException(nameof(toEmails));

            var emailDetails = GetMailMessage(toEmails, subject, htmlContent);

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTls);

                    smtp.Authenticate(_smtpOptions.Username, _smtpOptions.Password);

                    await smtp.SendAsync(emailDetails);
                }
                catch (Exception ex)
                {
                    throw new Exception("Mail can not be sent", ex);
                }
                finally
                {
                    smtp.Disconnect(true);
                }
            }
        }

        public void Send(string subject, string htmlContent, params string[] toEmails)
        {
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlContent)) throw new ArgumentNullException(nameof(htmlContent));
            if (toEmails == null || toEmails.Length == 0) throw new ArgumentNullException(nameof(toEmails));

            var emailDetails = GetMailMessage(toEmails, subject, htmlContent);

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTls);

                    smtp.Authenticate(_smtpOptions.Username, _smtpOptions.Password);

                    smtp.Send(emailDetails);
                }
                catch (Exception ex)
                {
                    throw new Exception("Mail can not be sent", ex);
                }
                finally
                {
                    smtp.Disconnect(true);
                }
            }
        }

        private MimeMessage GetMailMessage(
            IEnumerable<string> toMails,
            string subject,
            string htmlContent)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_smtpOptions.Email)
            };

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlContent
            };

            foreach (var mail in toMails)
            {
                email.To.Add(MailboxAddress.Parse(mail));
            }

            return email;
        }
    }
}
