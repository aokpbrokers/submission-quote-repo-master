using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.Services.Abstracts;

namespace KPBrokers.Submission.Quote.Services.Concretes
{
    /// <summary>
    /// Provides services for user authentication.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationBusinessLogic _authenticationBusinessLogic;        

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class with the specified authentication repository and logger.
        /// </summary>
        /// <param name="authenticationBusinessLogic">The repository that handles user data retrieval for authentication purposes.</param>
        /// <param name="logger">The logger service for logging application events and errors.</param>
        public AuthenticationService(IAuthenticationBusinessLogic authenticationBusinessLogic)
        {
            _authenticationBusinessLogic = authenticationBusinessLogic;            
        }

        /// <summary>
        /// Gets the requester service account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<LoginModel> GetRequesterServiceAccount(string username, string password)
        {
           return await _authenticationBusinessLogic.GetRequesterServiceAccount(username, password);
        }       
    }
}
