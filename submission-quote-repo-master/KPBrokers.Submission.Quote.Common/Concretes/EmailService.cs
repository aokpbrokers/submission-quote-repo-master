using KPBrokers.Submission.Quote.Common.Abstracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace KPBrokers.Submission.Quote.Common.Concretes
{
    /// <summary>
    /// Service for sending emails using SendGrid.
    /// </summary>
    public class EmailService : IEmailService
    {
        // SendGrid API key, sender email address, and sender name
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private ILoggerService _loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// Configuration settings are loaded from IConfiguration.
        /// </summary>
        /// <param name="configuration">Application configuration to access SendGrid settings.</param>
        public EmailService(IConfiguration configuration, ILoggerService loggerService)
        {
            _apiKey = configuration["SendGrid:ApiKey"]!;
            _fromEmail = configuration["SendGrid:FromEmail"]!;
            _fromName = configuration["SendGrid:FromName"]!;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Sends an email containing exception details.
        /// </summary>
        /// <param name="exception">The exception to include in the email content.</param>
        /// <param name="message">Additional optional message content.</param>
        public async Task<string> SendEmailAsync(string toEmail, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail);            
            var plainTextContent  = string.Empty;
            var htmlContent = message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);                    

            try
            {
                // Send the email asynchronously
                var response = await client.SendEmailAsync(msg);                
                _loggerService.Info($"Email sent! Status code: {response.StatusCode}");
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted) {
                    return ("Ok");
                }
                return ("Failed");
            }
            catch (Exception ex)
            {
                _loggerService.Error($"Error sending email: {ex.Message}");
            }
            return ("Error");
        }
    }
}
