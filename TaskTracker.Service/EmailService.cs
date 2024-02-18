using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TaskTracker.Contract;

namespace TaskTracker.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailInfo");
            var emailServer = emailSettings.GetSection("email").Value;
            var appKey = emailSettings.GetSection("appKey").Value;

            var emailMessage = new MimeMessage()
            {
                Body = new TextPart(MimeKit.Text.TextFormat.Text)
                {
                    Text = message,
                }
            };
            emailMessage.From.Add(new MailboxAddress("Task Tracker", emailServer));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(emailServer, appKey);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public string GenerateCode()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
