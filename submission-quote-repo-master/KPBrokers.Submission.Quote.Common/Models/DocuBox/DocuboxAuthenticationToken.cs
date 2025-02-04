namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class DocuboxAuthenticationToken
    {
        public string? username { get; set; }
        public string? token { get; set; }
        public string? agentId { get; set; }
        public DateTime expires { get; set; }
    }
}
