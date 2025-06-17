using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace GroepsWerk.Mail
{
    public class MailClient
    {
        private readonly I_UserLogin _login;

        public MailClient(I_UserLogin userLogin)
        {
            _login = userLogin;
        }

        public async Task SendAsync()
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient
            {
                EnableSsl = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Temp" // Use consistent, working path
            });


            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("mail@test.be")
                .To(_login.Email, _login.LoginNaam)
                .Subject("Je wachtwoord")
                .Body("Beste " + _login.LoginNaam + "," + Environment.NewLine + Environment.NewLine +
      "Je account is aangemaakt, je wachtwoord is " + _login.Wachtwoord + "." + Environment.NewLine +
      "Vriendelijke groeten," + Environment.NewLine + "Het PDY team")
           
                .SendAsync();

            if (!email.Successful)
            {
                Debug.WriteLine("Email failed: " + string.Join(", ", email.ErrorMessages));
            }
            else
            {
                Debug.WriteLine("Email sent successfully.");
            }
        }
    }

   

}
