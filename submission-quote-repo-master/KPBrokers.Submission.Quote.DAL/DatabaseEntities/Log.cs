using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Log
{
    public int LogID { get; set; }

    public DateTime EventDateTime { get; set; }

    public string EventLevel { get; set; } = null!;

    public string? UserName { get; set; }

    public string? MachineName { get; set; }

    public string EventMessage { get; set; } = null!;

    public string? ErrorSource { get; set; }

    public string? ErrorClass { get; set; }

    public string? ErrorMethod { get; set; }

    public string? ErrorMessage { get; set; }

    public string? InnerErrorMessage { get; set; }
}
