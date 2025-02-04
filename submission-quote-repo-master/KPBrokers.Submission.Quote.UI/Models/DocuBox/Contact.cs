using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{

    public class Contact
    {
        public int ContactId { get; set; }
        public string? role { get; set; }

        [Display(Name ="Title")]
        [Required]
        public string? title { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string? lastName { get; set; }

        [Display(Name = "Email address")]
        [Required]
        public string? email { get; set; }

        [Display(Name = "Phone number")]
        public string? phone { get; set; }
    }
}
