using KPBrokers.Submission.Quote.Common;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace KPBrokers.Submission.Quote.DAL.Concretes
{
    /// <summary>
    /// Represents the repository for managing user authentication.
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly KPBDbContext _dbContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public AuthenticationRepository(KPBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the requester service account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<LoginModel> GetRequesterServiceAccount(string username, string password)
        {
            //this should call database and get service account
            var model = new LoginModel() { Username = username, Password = password };
            return await Task.FromResult<LoginModel>(model);
        }
    }
}