using KPBrokers.Submission.Quote.UI.Models;

namespace KPBrokers.Submission.Quote.UI.Services.Abstracts
{
    public interface ISenderEmailService
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task SendEmailAsync(EmailEntity email);
    }
}