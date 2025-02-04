using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class AuditTrail
{
    public int AuditTrailId { get; set; }

    public int SubmissionId { get; set; }

    public int ActionId { get; set; }

    public int ReferenceId { get; set; }

    public int ActionPerformedBy { get; set; }

    public DateTime ActionPerformedDate { get; set; }

    public virtual AuditAction Action { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;
}
