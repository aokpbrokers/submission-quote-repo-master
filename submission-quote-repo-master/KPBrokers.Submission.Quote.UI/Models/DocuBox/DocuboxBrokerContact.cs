using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{
    public class DocuboxBrokerContact
    {
        public int BrokerContactId { get; set; }
        public string? BrokerName { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        public bool? InSystem { get; set; }
    }

}
