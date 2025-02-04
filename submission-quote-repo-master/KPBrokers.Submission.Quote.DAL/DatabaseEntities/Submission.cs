using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public string Reference { get; set; } = null!;

    public int BrokerId { get; set; }

    public int AgentId { get; set; }

    public int CoverageId { get; set; }

    public int InsuredId { get; set; }

    public int StatusId { get; set; }

    public int? CarrierId { get; set; }

    public DateTime PossibleInceptionDate { get; set; }

    public DateTime PossibleExpityDate { get; set; }

    public DateTime SubmittedDate { get; set; }

    public int SubmittedBy { get; set; }

    public decimal? Premium { get; set; }

    public string? QuotedReference { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual ICollection<AuditTrail> AuditTrails { get; set; } = new List<AuditTrail>();

    public virtual Broker Broker { get; set; } = null!;

    public virtual Carrier? Carrier { get; set; }

    public virtual Coverage Coverage { get; set; } = null!;

    public virtual Insured Insured { get; set; } = null!;

    public virtual ICollection<QuoteDocument> QuoteDocuments { get; set; } = new List<QuoteDocument>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<SubmissionUnit> SubmissionUnits { get; set; } = new List<SubmissionUnit>();
}
