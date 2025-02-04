using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.UI.Models.Entities
{

    public class Submission
    {
        public int SubmissionId { get; set; }

        [Display(Name ="Submission Reference")]
        public string Reference { get; set; } = null!;

        [Display(Name ="Broker")]
        public int BrokerId { get; set; }

        [Display(Name = "Agent")]
        public int AgentId { get; set; }

        public int CoverageId { get; set; }

        [Display(Name = "Insured")]
        public int InsuredId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Carrier")]
        public int? CarrierId { get; set; }

        [Display(Name = "Inception date")]
        public DateTime PossibleInceptionDate { get; set; }

        [Display(Name = "Expiry date")]
        public DateTime PossibleExpityDate { get; set; }

        [Display(Name = "Submitted date")]
        public DateTime SubmittedDate { get; set; }

        [Display(Name = "Submitted by")]
        public int SubmittedBy { get; set; }

        [Display(Name = "Premium")]
        public decimal? Premium { get; set; }

        [Display(Name = "Quote Reference")]
        public string? QuotedReference { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
    }
}
