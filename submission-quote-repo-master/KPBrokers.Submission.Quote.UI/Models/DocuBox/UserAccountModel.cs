using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{
    public class UserAccountModel
    {
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? Title { get; set; }
        [Display(Name ="First name")]
        [Required]
        public string? FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string? LastName { get; set; }
        public string? Username { get; set; }

        [Display(Name = "Email address")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        public string? Phone { get; set; }
        public string? EmailConfirmed { get; set; }
        public string? LockUser { get; set; }
        public string? TwoFactorEnabled { get; set; }
        public string? IsAdmin { get; set; }
    }
}
