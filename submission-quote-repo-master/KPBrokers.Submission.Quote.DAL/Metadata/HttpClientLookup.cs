using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Metadata
{
    public class HttpClientLookup
    {
        public List<Title> Titles { get; set; }
        public List<Country> Countries { get; set; }
        public List<Status> Status { get; set; }
        public List<Coverage> Coverages { get; set; }
    }
}
