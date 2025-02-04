namespace KPBrokers.Submission.Quote.DAL.Metadata
{
    public class HttpClientBrokerDetail
    {
		public int BrokerId { get; set; }
		public int? SecondaryBrokerId { get; set; }
		public string Name { get; set; }
		public string? DBA { get; set; }	
		public bool IsActive { get; set; }
		public int AddressId { get; set; }
		public string AddressLine1 { get; set; }
		public string? AddressLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public int CountryId { get; set; }
		public string? CountryName { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedBy { get; set; }
		public string? CreatedByName { get; set; }
		public DateTime UpdatedDate { get; set; }
		public int UpdatedBy { get; set; }
		public string? UpdatedByName { get; set; }
	}
}
