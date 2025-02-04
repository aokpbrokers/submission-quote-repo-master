using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Metadata
{
	public class HttpClientUserAccount
	{
		public int UserId { get; set; }

		public string IdentityId { get; set; } = null!;

		public int ContactId { get; set; }

		public int CompanyId { get; set; }

		public string Fullname { get; set; } = null!;
	}
}
