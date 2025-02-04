using System;
using System.Collections.Generic;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class Title
{
    public int TitleId { get; set; }

    public string TitleName { get; set; } = null!;

    public virtual ICollection<AgentContact> AgentContacts { get; set; } = new List<AgentContact>();

    public virtual ICollection<BrokerContact> BrokerContacts { get; set; } = new List<BrokerContact>();

    public virtual ICollection<CarrierContact> CarrierContacts { get; set; } = new List<CarrierContact>();
}
