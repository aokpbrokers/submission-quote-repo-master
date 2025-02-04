using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Metadata
{
    public int MetadataId { get; set; }

    public string MetadataName { get; set; } = null!;
}
