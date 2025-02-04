namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class UserAccountModel
    {
        public string? Role { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? EmailConfirmed { get; set; }
        public string? LockUser { get; set; }
        public string? TwoFactorEnabled { get; set; }
        public string? IsAdmin { get; set; }
    }
}
