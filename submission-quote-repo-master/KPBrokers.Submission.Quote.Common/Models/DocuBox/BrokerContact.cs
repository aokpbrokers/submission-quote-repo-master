namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class BrokerContact
    {
        public int brokerContactId { get; set; }
        public required string brokerName { get; set; }
        public required string title { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public string? phone { get; set; }
    }
}

