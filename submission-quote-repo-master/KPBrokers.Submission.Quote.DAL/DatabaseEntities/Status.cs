using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
