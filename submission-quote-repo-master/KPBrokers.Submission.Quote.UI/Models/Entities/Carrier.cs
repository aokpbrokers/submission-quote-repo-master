
using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.Entities;

public partial class Carrier
{
    public int CarrierId { get; set; }
    public int? SecondaryCarrierId { get; set; }
    public string Name { get; set; }

    [Display(Name="Doing business as")]
    public string? DBA { get; set; }
    public int AddressId { get; set; }
    public bool IsActive { get; set; }

    [Display(Name="Address line 1")]
    public string AddressLine1 { get; set; }

    [Display(Name = "Address line 2")]
    public string AddressLine2 { get; set; }
   
    public string City { get; set; }
    public string State { get; set; }

    [Display(Name = "Postal code")]
    public string PostalCode { get; set; }

    [Display(Name = "Country")]
    public int CountryId { get; set; }
    [Display(Name = "Country")]
    public string CountryName { get; set; }
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    [Display(Name = "Created by")]
    public string CreatedByName { get; set; }
    [Display(Name = "Updated date")]
    public DateTime UpdatedDate { get; set; }
    public int UpdatedBy { get; set; }

    [Display(Name="Updated by")]
    public string UpdatedByName { get; set; }

}

