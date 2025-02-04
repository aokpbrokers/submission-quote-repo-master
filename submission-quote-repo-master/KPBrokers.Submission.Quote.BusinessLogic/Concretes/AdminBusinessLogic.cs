using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.Concretes;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;
using KPBrokers.Submission.Quote.Services.Concretes;

namespace KPBrokers.Submission.Quote.BusinessLogic.Concretes
{
	public class AdminBusinessLogic : IAdminBusinessLogic
	{
		private readonly IAdminRepository _adminRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="AdminBusinessLogic"/> class.
		/// </summary>
		/// <param name="brokerService">The broker service.</param>
		public AdminBusinessLogic(IAdminRepository adminRepository)
		{
			_adminRepository = adminRepository;
		}

		/// <summary>
		///  Method to search for Agent Company by name
		/// </summary>
		/// <param name="agentCompanyName"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>> SearchForAgentCompany(string agentCompanyName)
		{
			if (string.IsNullOrEmpty(agentCompanyName))
				throw new ArgumentException("Agent company name cannot be null or empty", nameof(agentCompanyName));

			var agentCompanies = await _adminRepository.SearchForAgentCompany(agentCompanyName);
			return agentCompanies;
		}

		/// <summary>
		/// Method to get agent contacts by agent ID
		/// </summary>
		/// <param name="agentId"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>        
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>> GetAgentContactsByAgentId(int agentId)
		{
			var agentContacts = await _adminRepository.GetAgentContactsByAgentId(agentId);
			return agentContacts;
		}

		/// <summary>
		/// Searches for carrier company.
		/// </summary>
		/// <param name="carrierCompanyName">Name of the carrier company.</param>
		/// <returns></returns>
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>> SearchForCarrierCompany(string carrierCompanyName)
		{
			return await _adminRepository.SearchForCarrierCompany(carrierCompanyName);
		}

		/// <summary>
		/// Gets the carrier contacts by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>> GetCarrierContactsByCarrierId(int carrierId)
		{
			return await _adminRepository.GetCarrierContactsByCarrierId(carrierId);
		}

		/// <summary>
		/// Searches for broker company.
		/// </summary>
		/// <param name="brokerCompanyName">Name of the broker company.</param>
		/// <returns></returns>
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>> SearchForBrokerCompany(string brokerCompanyName)
		{
			return await _adminRepository.SearchForBrokerCompany(brokerCompanyName);
		}

		/// <summary>
		/// Gets the broker contacts by broker identifier.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>> GetBrokerContactsByBrokerId(int brokerId)
		{
			return await _adminRepository.GetBrokerContactsByBrokerId(brokerId);
		}

		/// <summary>
		/// Adds the specified agent.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddAgentAsync(HttpClientAgentDetail agentDetail)
		{
			return await _adminRepository.AddAgentAsync(agentDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteAgentAsync(int agentId, int userId)
		{
			return await _adminRepository.DeleteAgentAsync(agentId, userId);
		}

		/// <summary>
		/// Gets the agent by identifier asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <returns></returns>
		public async Task<Agent> GetAgentByIdAsync(int agentId)
		{
			return await _adminRepository.GetAgentByIdAsync(agentId);
		}

		/// <summary>
		/// Gets the agent company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientAgentDetail>> GetAgentCompanyAsync()
		{
			return await _adminRepository.GetAgentCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateAgentAsync(HttpClientAgentDetail agentDetail)
		{
			return await _adminRepository.UpdateAgentAsync(agentDetail);
		}

		/// <summary>
		/// Gets the agent contact detail by agent identifier.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <returns></returns>
		public async Task<List<HttpClientAgentContactDetail>> GetAgentContactDetailByAgentIdAsync(int agentId)
		{
			return await _adminRepository.GetAgentContactDetailByAgentIdAsync(agentId);
		}

		/// <summary>
		/// Inserts the agent contact asynchronous.
		/// </summary>
		/// <param name="agentContact">The agent contact.</param>
		/// <returns></returns>
		public async Task<int> InsertAgentContactAsync(HttpClientAgentContactDetail agentContact)
		{
			return await _adminRepository.InsertAgentContactAsync(agentContact);
		}

		/// <summary>
		/// Updates the agent contact asynchronous.
		/// </summary>
		/// <param name="agentContact">The agent contact.</param>
		/// <returns></returns>
		public async Task<int> UpdateAgentContactAsync(HttpClientAgentContactDetail agentContact)
		{
			return await _adminRepository.UpdateAgentContactAsync(agentContact);
		}

		/// <summary>
		/// Adds the specified broker.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddBrokerAsync(HttpClientBrokerDetail brokerDetail)
		{
			return await _adminRepository.AddBrokerAsync(brokerDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteBrokerAsync(int brokerId, int userId)
		{
			return await _adminRepository.DeleteBrokerAsync(brokerId, userId);
		}

		/// <summary>
		/// Gets the broker by identifier asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		public async Task<Broker> GetBrokerByIdAsync(int brokerId)
		{
			return await _adminRepository.GetBrokerByIdAsync(brokerId);
		}

		/// <summary>
		/// Gets the broker company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientBrokerDetail>> GetBrokerCompanyAsync()
		{
			return await _adminRepository.GetBrokerCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateBrokerAsync(HttpClientBrokerDetail brokerDetail)
		{
			return await _adminRepository.UpdateBrokerAsync(brokerDetail);
		}

		/// <summary>
		/// Gets the broker contact detail by broker identifier.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		public async Task<List<HttpClientBrokerContactDetail>> GetBrokerContactDetailByBrokerIdAsync(int brokerId)
		{
			return await _adminRepository.GetBrokerContactDetailByBrokerIdAsync(brokerId);
		}

		/// <summary>
		/// Inserts the broker contact asynchronous.
		/// </summary>
		/// <param name="brokerContact">The broker contact.</param>
		/// <returns></returns>
		public async Task<int> InsertBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
		{
			return await _adminRepository.InsertBrokerContactAsync(brokerContact);
		}

		/// <summary>
		/// Adds the specified carrier.
		/// </summary>
		/// <param name="carrier">The carrier.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddCarrierAsync(HttpClientCarrierDetail carrierDetail)
		{
			return await _adminRepository.AddCarrierAsync(carrierDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteCarrierAsync(int carrierId, int userId)
		{
			return await _adminRepository.DeleteCarrierAsync(carrierId, userId);
		}

		/// <summary>
		/// Gets the carrier by identifier asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		public async Task<Carrier> GetCarrierByIdAsync(int carrierId)
		{
			return await _adminRepository.GetCarrierByIdAsync(carrierId);
		}

		/// <summary>
		/// Gets the carrier company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientCarrierDetail>> GetCarrierCompanyAsync()
		{
			return await _adminRepository.GetCarrierCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="carrier">The carrier.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateCarrierAsync(HttpClientCarrierDetail carrierDetail)
		{
			return await _adminRepository.UpdateCarrierAsync(carrierDetail);
		}

		/// <summary>
		/// Gets the carrier contact detail by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		public async Task<List<HttpClientCarrierContactDetail>> GetCarrierContactDetailByCarrierIdAsync(int carrierId)
		{
			return await _adminRepository.GetCarrierContactDetailByCarrierIdAsync(carrierId);
		}

		/// <summary>
		/// Inserts the carrier contact asynchronous.
		/// </summary>
		/// <param name="carrierContact">The carrier contact.</param>
		/// <returns></returns>
		public async Task<int> InsertCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
		{
			return await _adminRepository.InsertCarrierContactAsync(carrierContact);
		}

		/// <summary>
		/// Updates the broker contact asynchronous.
		/// </summary>
		/// <param name="brokerContact">The broker contact.</param>
		/// <returns></returns>
		public async Task<int> UpdateBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
		{
			return await _adminRepository.UpdateBrokerContactAsync(brokerContact);
		}

        /// <summary>
        /// Updates the broker contact asynchronous.
        /// </summary>
        /// <param name="carrierContact"></param>
        /// <returns></returns>
        public async Task<int> UpdateCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
		{
			return await _adminRepository.UpdateCarrierContactAsync(carrierContact);
		}
	}
}
