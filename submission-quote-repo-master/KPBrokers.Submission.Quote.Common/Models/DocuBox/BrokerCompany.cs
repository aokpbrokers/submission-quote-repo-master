using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class BrokerCompany
    {
      
        public int brokerId { get; set; }      
        public string brokerName { get; set; }
        public string dba { get; set; }
        public string? accountEmail { get; set; }
        public int addressId { get; set; }
        public string addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
    }
}

