namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class CarrierCompany
    {
        public int carrierId { get; set; }
        public string carrierName { get; set; }
        public string dba { get; set; }
        public string accountEmail { get; set; }
        public int addressId { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
    }
}
