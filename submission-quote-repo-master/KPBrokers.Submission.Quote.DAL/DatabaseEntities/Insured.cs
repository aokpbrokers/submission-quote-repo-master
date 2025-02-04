using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Insured
{
    public int InsuredId { get; set; }

    public string? DBA { get; set; }

    public string InsuredName { get; set; } = null!;

    public string? MCDocketNumber { get; set; }

    public int AddressId { get; set; }

    public string MainContactName { get; set; } = null!;

    public string? MainContactPhone { get; set; }

    public string MainContactEmail { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
