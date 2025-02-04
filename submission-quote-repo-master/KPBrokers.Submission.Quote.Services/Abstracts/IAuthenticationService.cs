using KPBrokers.Submission.Quote.Common;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.Services.Abstracts
{
    /// <summary>
    /// Defines methods for authentication services.
    /// </summary>
    public interface IAuthenticationService
    {     
        /// <summary>
        /// Gets the requester service account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<LoginModel> GetRequesterServiceAccount(string username, string password);
    }
}
