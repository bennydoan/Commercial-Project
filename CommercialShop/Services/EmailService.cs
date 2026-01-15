using System.Net;
using System.Net.Mail;

namespace CommercialShop.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var from = _configuration["EmailSettings:From"];
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = int.Parse(_configuration["EmailSettings:Port"]!);
            var username = _configuration["EmailSettings:UserName"];
            var password = _configuration["EmailSettings:Password"];

            var message = new MailMessage(from!, email, subject, body)
            {
                IsBodyHtml = true
            };

            using var client = new SmtpClient(smtpServer!, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
