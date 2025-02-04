using KPBrokers.Submission.Quote.Common;

namespace KPBrokers.Submission.Quote.DAL.Abstracts
{
    /// <summary>
    /// Provides an interface for authentication data operations.
    /// </summary>
    public interface IAuthenticationRepository
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
