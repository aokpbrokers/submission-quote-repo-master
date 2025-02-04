﻿using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{
    public class DocuboxCarrierContact
    {
        public int CarrierContactId { get; set; }      

        public string? CarrierName { get; set; }
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