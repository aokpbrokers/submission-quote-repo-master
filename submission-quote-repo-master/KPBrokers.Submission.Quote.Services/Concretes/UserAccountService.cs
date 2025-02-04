using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.Services.Concretes
{
	public class UserAccountService : IUserAccountService
	{
		private readonly IUserAccountBusinessLogic _userAccountRepository;
        public UserAccountService(IUserAccountBusinessLogic userAccountBusinessLogic )
		{
			_userAccountRepository = userAccountBusinessLogic;
		}
		public async Task<HttpClientUserAccount?> GetUserAccountAsync(string userId)
		{
			return await _userAccountRepository.GetUserAccountAsync(userId);
		}

		public string UserAccountName(int userId)
		{
			return _userAccountRepository.UserAccountName(userId);
		}
	}
}
