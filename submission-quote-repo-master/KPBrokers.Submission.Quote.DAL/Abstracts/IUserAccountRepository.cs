using KPBrokers.Submission.Quote.DAL.Metadata;

namespace KPBrokers.Submission.Quote.DAL.Abstracts
{
    public interface IUserAccountRepository
    {
		/// <summary>
		/// Gets the user account asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<HttpClientUserAccount?> GetUserAccountAsync(string userId);

		/// <summary>
		/// Users the name of the account.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		string UserAccountName(int userId);
    }
}