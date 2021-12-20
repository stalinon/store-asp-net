using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shop_Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailSettings MailSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            MailSettings = _configuration.GetSection("Credentials").GetSection("Email").Get<MailSettings>();
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            var from = new MailAddress(MailSettings.Login, MailSettings.Nickname);
            // кому отправляем
            var to = new MailAddress(email);
            // создаем объект сообщения
            var m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = body;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // логин и пароль
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(MailSettings.Login, MailSettings.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
        }
    }
}
