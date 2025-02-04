namespace KPBrokers.Submission.Quote.UI.Models.Entities
{
    public class StaticLookup
    {
        public List<Title> Titles { get; set; }
        public List<Country> Countries { get; set; }
        public List<Status> Status { get; set; }
        public List<Coverage> Coverages { get; set; }
    }
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; } 
    }

    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class Coverage
    {
        public int CoverageId { get; private set; }
        public string CoverageName { get; private set; }
    }
}
