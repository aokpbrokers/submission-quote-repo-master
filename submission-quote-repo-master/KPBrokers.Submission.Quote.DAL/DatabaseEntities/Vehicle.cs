using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string? RegNumber { get; set; }

    public string VIN { get; set; } = null!;

    public decimal Value { get; set; }

    public bool PhysicalDamaged { get; set; }

    public bool LossPayee { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual ICollection<SubmissionUnit> SubmissionUnits { get; set; } = new List<SubmissionUnit>();
}
