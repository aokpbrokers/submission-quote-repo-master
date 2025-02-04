using KPBrokers.Submission.Quote.UI.Models;
using Microsoft.AspNetCore.Identity;

namespace KPBrokers.Submission.Quote.UI.Services.Abstracts
{
    public interface IIdentityService
    {
        /// <summary>
        /// Registers the user account.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        Task<string> RegisterUserAccount(AccountContact contact, HttpContext httpContext);

        /// <summary>
        /// Gets all user account details.
        /// </summary>
        /// <returns></returns>
        Task<List<AccountContact>> GetAllUserAccountDetails();

        /// <summary>
        /// Updates the user account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> UpdateUserAccount(AccountContact model);

        /// <summary>
        /// Resets the user password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> ResetUserPassword(AccountContact model);

        Task<string> GetCurrentLoginUserRole(string user);
    }
}