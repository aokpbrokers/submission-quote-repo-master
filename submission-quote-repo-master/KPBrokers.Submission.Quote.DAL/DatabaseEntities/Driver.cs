using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Driver
{
    public int DriverId { get; set; }

    public int TitleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string DriverLicenceNumber { get; set; } = null!;

    public DateTime? DriverLicenceIssued { get; set; }

    public DateTime? DriverLicenceExpiry { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual ICollection<SubmissionUnit> SubmissionUnits { get; set; } = new List<SubmissionUnit>();
}
