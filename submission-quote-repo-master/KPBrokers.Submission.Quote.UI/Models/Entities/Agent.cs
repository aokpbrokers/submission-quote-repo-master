using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.Entities;

public partial class Agent
{
	public int AgentId { get; set; }
	public int? SecondaryAgentId { get; set; }
	public string Name { get; set; }
	public string DBA { get; set; }
	public bool IsActive { get; set; }
	public int AddressId { get; set; }
	public string AddressLine1 { get; set; }
	public string AddressLine2 { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string PostalCode { get; set; }
	public int CountryId { get; set; }
	public string CountryName { get; set; }

	[Display(Name="Created date")]
	public DateTime CreatedDate { get; set; }

    [Display(Name = "Created by")]
    public int CreatedBy { get; set; }
	public string CreatedByName { get; set; }

    [Display(Name = "Update date")]
    public DateTime UpdatedDate { get; set; }

    [Display(Name = "Updated by")]
    public int UpdatedBy { get; set; }
	public string UpdatedByName { get; set; }
}