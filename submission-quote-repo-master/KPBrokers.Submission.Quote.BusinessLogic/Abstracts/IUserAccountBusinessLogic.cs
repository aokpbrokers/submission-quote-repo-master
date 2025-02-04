using KPBrokers.Submission.Quote.DAL.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.BusinessLogic.Abstracts
{
	public interface IUserAccountBusinessLogic
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

