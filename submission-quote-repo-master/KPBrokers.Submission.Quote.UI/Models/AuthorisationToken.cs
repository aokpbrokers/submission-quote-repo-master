namespace KPBrokers.Submission.Quote.UI.Models
{
    public class AuthorisationToken
    {
        public required string username { get; set; }
        public required string token { get; set; }
        public required string brokerId { get; set; }
        public DateTime expires { get; set; }
    }
}
