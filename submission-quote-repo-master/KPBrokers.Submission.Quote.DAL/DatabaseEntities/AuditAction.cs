using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class AuditAction
{
    public int ActionId { get; set; }

    public string ActionName { get; set; } = null!;

    public virtual ICollection<AuditTrail> AuditTrails { get; set; } = new List<AuditTrail>();
}
