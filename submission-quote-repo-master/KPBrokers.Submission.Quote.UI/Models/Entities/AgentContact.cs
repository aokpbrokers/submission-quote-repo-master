using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.Entities;

public partial class AgentContact
{
    public int AgentContactId { get; set; }

    public int AgentId { get; set; }    

    public string IdentityId { get; set; }

    public int? SecondaryAgentContactId { get; set; }

    [Display(Name ="Title")]
    [Required]
    public int TitleId { get; set; }

    public string TitleName { get; set; }

    [Display(Name ="First name")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "Middle name")]
    public string? MiddleName { get; set; }

    [Display(Name = "Last name")]
    [Required]
    public string LastName { get; set; }

    [Display(Name = "Email address")]
    [Required]
    public string Email { get; set; }

    [Display(Name = "Phone number")]
    [Required]
    public string Phone { get; set; }

    public bool IsActive { get; set; }

    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; }
    
    public int CreatedBy { get; set; }

    [Display(Name = "Created by")]
    public string CreatedByName { get; set; }

    [Display(Name = "Updated date")]
    public DateTime UpdatedDate { get; set; }

    [Display(Name = "Updated by")]
    public string UpdatedByName { get; set; }

    public int UpdatedBy { get; set; }   
    
}
