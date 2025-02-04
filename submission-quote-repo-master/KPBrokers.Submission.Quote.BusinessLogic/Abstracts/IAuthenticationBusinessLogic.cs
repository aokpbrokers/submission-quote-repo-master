using KPBrokers.Submission.Quote.Common;

namespace KPBrokers.Submission.Quote.BusinessLogic.Abstracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationBusinessLogic
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
