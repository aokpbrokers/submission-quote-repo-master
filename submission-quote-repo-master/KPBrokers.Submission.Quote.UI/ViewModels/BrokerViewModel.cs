using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Models.DocuBox;
using KPBrokers.Submission.Quote.UI.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KPBrokers.Submission.Quote.UI.ViewModels
{
    public class BrokerViewModel
    {
        public Company Company { get; set; }
        public List<Company> Companies { get; set; }
        public Contact Contact { get; set; }
        public List<Contact> Contacts { get; set; }
        public KPBrokers.Submission.Quote.UI.Models.Entities.Submission? Submission { get; set; }
        public List<KPBrokers.Submission.Quote.UI.Models.Entities.Submission>? Submissions { get; set; }
        public Agent Agent { get; set; }
        public List<Agent> Agents { get; set; }
        public Address Address { get; set; }
        public string DisplayMessage { get; set; }
        public AccountContact UserAccount { get; set; }
        public List<AccountContact> UserAccounts { get; set; }
        public Broker Broker { get; set; }
        public List<Broker> Brokers { get; set; }
        public Carrier Carrier { get; set; }
        public List<Carrier> Carriers { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public DocuboxAgentContact DocuboxAgentContact { get; set; }
        public List<DocuboxAgentContact> DocuboxAgentContacts { get; set; }
        public List<DocuboxBrokerContact> DocuboxBrokerContacts { get; set; }
        public DocuboxBrokerContact DocuboxBrokerContact { get; set; }
        public AgentContact AgentContact { get; set; }
        public List<AgentContact> AgentContacts { get; set; }
        public List<SelectListItem> Titles { get; set; }   
        public List<BrokerContact> BrokerContacts { get; set; }
        public BrokerContact BrokerContact { get; set; }
        public CarrierContact CarrierContact { get; set; }
        public List<CarrierContact> CarrierContacts { get; set; }
        public DocuboxCarrierContact DocuboxCarrierContact { get; set; }
        public List<DocuboxCarrierContact> DocuboxCarrierContacts { get;set; }
    }
}
