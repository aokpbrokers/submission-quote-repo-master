using System.Threading.Tasks;
using System;
using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.Services.Abstracts;
using KPBrokers.Submission.Quote.Common;
using KPBrokers.Submission.Quote.DAL.Abstracts;

namespace KPBrokers.Submission.Quote.BusinessLogic.Concretes
{
    /// <summary>
    /// Handles business logic for user authentication.
    /// </summary>
    public class AuthenticationBusinessLogic : IAuthenticationBusinessLogic
    {
        
        private readonly IAuthenticationRepository _authenticationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationBusinessLogic"/> class with the specified authentication service.
        /// </summary>
        /// <param name="authenticationService">Service to interact with user data.</param>
        /// <param name="logger">Service to log application events and errors.</param>
        public AuthenticationBusinessLogic(IAuthenticationRepository authenticationService)
        {
            _authenticationRepository = authenticationService;            
        }

        /// <summary>
        /// Gets the requester service account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<LoginModel> GetRequesterServiceAccount(string username, string password)
        {
            return await _authenticationRepository.GetRequesterServiceAccount(username, password);
        }
    }
}
