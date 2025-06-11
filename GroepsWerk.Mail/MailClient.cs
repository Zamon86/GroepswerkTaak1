using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Text;

namespace GroepsWerk.Mail
{
    public class MailClient
    {
        UserLogin login;
        public MailClient(UserLogin userLogin)
        {
            this.login = userLogin;
            Task.Run(() => { SendMail(); });

        }

        private async Task SendMail()
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Temp"
            });

            StringBuilder template = new StringBuilder();
            template.AppendLine("Beste @Model.FirstName, ");
            template.AppendLine("<p>Je account is aangemaakt, je wachtwoord is @Model.Wachtwoord.</p>");
            template.AppendLine("Vriendelijke groeten het PDY team");


            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
            var email = await Email
                .From("mail@test.be")
                .To(login.Email, login.LoginNaam)
                .Subject("Je wachtwoord")
                //.Body("")
                .UsingTemplate(template.ToString(), new { FirstName = login.LoginNaam, Wachtwoord = login.Wachtwoord })
                .SendAsync();

        }
    }

}
