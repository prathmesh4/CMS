using CustMgmSys.Abstract;
using CustMgmSys.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


namespace CustMgmSys.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string from, string to, string subject, string body, EmailAttachment attachment = null)
        {
            // Create a new MailMessage object
            using var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            // Set the recipient(s)
            message.To.Add(to);

            // Add the attachment if one was provided
            if (attachment != null && attachment.Data?.Length > 0)
            {
                var stream = new MemoryStream(attachment.Data);
                message.Attachments.Add(new Attachment(stream, MediaTypeNames.Application.Octet)
                {
                    Name = attachment.FileName,
                    ContentType = new ContentType(MediaTypeNames.Application.Octet)
                });

            }

            // Configure the SMTP client
            using var smtp = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            // Send the email
            await smtp.SendMailAsync(message);
        }
    }

}