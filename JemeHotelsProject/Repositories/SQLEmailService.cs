using System.Net;
using System.Net.Mail;

namespace JemeHotelsProject.Repositories
{
    public class SQLEmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public SQLEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var server = _configuration["EmailSettings:Server"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);
            var senderName = _configuration["EmailSettings:SenderName"];
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            var message = new MailMessage(email, toEmail, subject, body);
            message.IsBodyHtml = true;

            using var client = new SmtpClient(server, port)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
