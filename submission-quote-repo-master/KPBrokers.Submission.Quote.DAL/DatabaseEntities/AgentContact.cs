using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class AgentContact
{
    public int AgentContactId { get; set; }

    public int AgentId { get; set; }

    public int? SecondaryAgentContactId { get; set; }

    public int TitleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
