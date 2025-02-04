using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Metadata
{
    public class HttpClientAgentContactDetail
    {

        public int AgentContactId { get; set; }

        public int AgentId { get; set; }        

        public int? SecondaryAgentContactId { get; set; }

        public string IdentityId { get; set; }

        public int TitleId { get; set; }

        public string? TitleName { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? CreatedByName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string? UpdatedByName { get; set; }        
    }
}
