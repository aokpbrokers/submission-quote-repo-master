using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class UserAccount
{
    [Key]
    public int UserId { get; set; }

    public string IdentityId { get; set; } = null!;

    public int ContactId { get; set; }

    public int CompanyId { get; set; }

    public string Fullname { get; set; } = null!;    
}
