using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace raktarProgram.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            this.configuration = configuration;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            string apiKey = this.configuration["raktarProgamAPI"] ;
                /*Environment.GetEnvironmentVariable("windir");*/
            //raktarAPI
            //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

            return Execute(apiKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            //raktarProgamAPI
            //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

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("raktar.412@outlook.hu"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}