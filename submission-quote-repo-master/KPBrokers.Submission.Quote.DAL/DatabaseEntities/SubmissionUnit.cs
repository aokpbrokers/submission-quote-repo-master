using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class SubmissionUnit
{
    public int SubmissionUnitId { get; set; }

    public int SubmissionId { get; set; }

    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
