using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Carrier
{
    public int CarrierId { get; set; }

    public int? SecondaryCarrierId { get; set; }

    public string Name { get; set; } = null!;

    public string? DBA { get; set; }

    public int AddressId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<CarrierContact> CarrierContacts { get; set; } = new List<CarrierContact>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
