namespace KPBrokers.Submission.Quote.UI.Models
{
    public class EmailEntity
    {
        public required string EmailTo { get; set; }
        public required string EmailSubject { get; set; }
        public required string EmailBody { get; set; }
    }
}
