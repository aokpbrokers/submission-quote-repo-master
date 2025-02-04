using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class QuoteDocument
{
    public int QuoteDocumentId { get; set; }

    public string DocumentName { get; set; } = null!;

    public string DocumentSize { get; set; } = null!;

    public byte[] DocumentBinary { get; set; } = null!;

    public int SubmissionId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
