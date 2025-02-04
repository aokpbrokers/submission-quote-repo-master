using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Concretes
{
    public class UserAccountRepository : IUserAccountRepository
	{
		private readonly KPBDbContext _dbContext;

		public UserAccountRepository(KPBDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		/// <summary>
		/// Users the name of the account.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public string UserAccountName(int userId)
		{
			var name = _dbContext.UserAccounts.SingleOrDefault(a => a.UserId == userId);
			if (name == null)
				return string.Empty;
			return name.Fullname;
		}

		/// <summary>
		/// Gets the user account asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<HttpClientUserAccount?> GetUserAccountAsync(string userId)
		{
			var account = await _dbContext.UserAccounts.SingleOrDefaultAsync(x => x.IdentityId == userId);
			if (account == null)
				return null;
			return new HttpClientUserAccount
			{
				UserId = account.UserId,
				IdentityId = account.IdentityId,
				CompanyId = account.CompanyId,
				ContactId = account.ContactId,
				Fullname = account.Fullname
			};
		}
	}
}
	
