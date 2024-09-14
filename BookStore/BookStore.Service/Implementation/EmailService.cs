using BookStore.Domain;
using BookStore.Domain.Email;
using BookStore.Service.Interface;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(EmailMessage allMails)
        {
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress("BookSAM", "bookstore_sam@outlook.com"),
                Subject = allMails.Subject
            };

            emailMessage.From.Add(new MailboxAddress("EShop Application", "bookstore_sam@outlook.com"));

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = allMails.Content };

            emailMessage.To.Add(new MailboxAddress(allMails.MailTo, allMails.MailTo));

            Debug.Print(allMails.MailTo);
            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(emailSettings.SmtpServer, 587, socketOptions);

                    if (!string.IsNullOrEmpty(emailSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(emailSettings.SmtpUserName, emailSettings.SmtpPassword);
                    }
                    await smtp.SendAsync(emailMessage);


                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}

