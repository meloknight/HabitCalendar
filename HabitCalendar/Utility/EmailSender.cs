using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace HabitCalendar.Utility
{
    public class EmailSender:IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync( string email, string subject, string htmlMessage )
        {
            var emailSettings = _configuration.GetSection( "EmailSettings" );

            using ( var client = new SmtpClient() )
            {
                client.Host = emailSettings["SmtpServer"];
                client.Port = int.Parse( emailSettings["SmtpPort"] );
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(
                    emailSettings["SmtpUsername"],
                    emailSettings["SmtpPassword"] );

                var mailMessage = new MailMessage
                {
                    From = new MailAddress( emailSettings["FromAddress"], emailSettings["FromName"] ),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add( email );

                await client.SendMailAsync( mailMessage );
            }
            // return Task.CompletedTask;
        }
    }
}
