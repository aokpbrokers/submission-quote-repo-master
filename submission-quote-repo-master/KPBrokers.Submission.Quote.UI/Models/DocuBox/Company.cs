using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{
    public class Company
    {
        [Display(Name = "Docubox Id")]
        public int Id { get; set; }       
        
        public required string Name { get; set; }
        [Display(Name="Doing Business As")]
        public string? DBA { get; set; }

        [Display(Name="Primary Email")]
        public string? AccountEmail { get; set; }       
        public int AddressId { get; set; }
        [Display(Name = "Address line 1")]
        public required string AddressLine1 { get; set; }
        [Display(Name = "Address line 2")]
        public string? AddressLine2 { get; set; }

        [Display(Name = "City")]
        public required string City { get; set; }
        [Display(Name = "State")]
        public required string State { get; set; }
        [Display(Name = "Postal code")]
        public required string PostalCode { get; set; }
        [Display(Name = "Country")]
        public required string Country { get; set; }
        [Display(Name = "Type")]
        public string? Role { get; set; }
    }
}

