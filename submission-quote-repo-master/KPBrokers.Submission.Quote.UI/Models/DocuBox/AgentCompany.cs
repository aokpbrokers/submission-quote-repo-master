﻿using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.DocuBox
{
    public class AgentCompany
    {
        [Display(Name = "id")]
        public int AgentId { get; set; }

        [Display(Name = "name")]
        public string? AgentName { get; set; }
        public string? DBA { get; set; }
        public string? AccountEmail { get; set; }
        public int AddressId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}

