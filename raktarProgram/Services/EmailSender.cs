using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace raktarProgram.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("raktar.412@gmail.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            //https://localhost:5001/Identity/Account/ConfirmEmail?userId=63a1b799-e5c0-4518-b924-b41e54243bfd&amp;code=Q2ZESjhGTUR0bS8razAxTm9OOGdFcTJDMHVKVFYxNGVjYVIxRHVraC9YSytkbFRGcmt5TnFONjBlQ3BWTGM5TWZpODhheVpNdXdYZTNJVjdTRGxPaml2MlBESHhnN1JtRXU4REIrS21jRWM5Tkk1VTl0Q2lJUWk3MXMyazlJNXYrUG85SXJPZHg1eldDQzlybkpkRXpOWEpvbGM1RTRTVFB4V3ZLOEdSUmpHOFVUOEtXZTdVZ09QZllERmxKbG1TUjhXeklVQTE0dUtvSGZkeUhWdzN5bDBaUFQ4WXROR1RIbTJSSUo2L3h1NkhOOG1Yc3ZZVll5ODBXZ2tMengxZXlzTjkyQT09&amp;returnUrl=%2F
            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}