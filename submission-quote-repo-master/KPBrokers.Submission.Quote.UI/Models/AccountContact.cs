using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models
{
    public class AccountContact
    {
        public int SecondaryId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? CompanySecondaryId { get; set; }
        public string UserId { get; set; }
		[Display(Name = "Role")]
		public required string ContactRole { get; set; }
        [Required]
        [Display(Name = "Title")]
        public int TitleId { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
		[Display(Name = "Name")]
		public string FullName { get; set; }
		[Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }        
        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }        
        [Display(Name = "Enabled MFA")]
        public bool IsMFAEnabled { get; set; }        
        [Display(Name = "Is User Locked")]
        public bool LockUser { get; set; }
		
		[Display(Name = "Confirmed Email")]
		public bool ConfirmedEmail { get; set; }
		[Display(Name = "Confirmed Email")]
		public int AccessFailedCount { get; set; }
		[Display(Name = "Lockout End Date")]
		public DateTimeOffset? LockoutEndDate { get; set; }
        [Display(Name = "Password Changed Date")]
        public DateTimeOffset? PasswordLastUpdated { get; set; }
        [Display(Name ="User name")]
        public string UserName { get; set; }
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name = "Require Password Change?")]
        public bool RequirePasswordChange { get; set; }
		public int CreatedBy { get; set; }
    }
}
