using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.BusinessLogic.Concretes
{
	public class UserAccountBusinessLogic : IUserAccountBusinessLogic
	{
		private	readonly IUserAccountRepository _userAccountRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserAccountBusinessLogic"/> class.
		/// </summary>
		/// <param name="userAccountRepository">The user account repository.</param>
		public UserAccountBusinessLogic(IUserAccountRepository userAccountRepository )
		{
		 _userAccountRepository = userAccountRepository;
		}

		/// <summary>
		/// Gets the user account asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<HttpClientUserAccount?> GetUserAccountAsync(string userId)
		{
			return await _userAccountRepository.GetUserAccountAsync(userId);
		}

		/// <summary>
		/// Users the name of the account.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public string UserAccountName(int userId)
		{
			return _userAccountRepository.UserAccountName(userId);
		}
	}
}
