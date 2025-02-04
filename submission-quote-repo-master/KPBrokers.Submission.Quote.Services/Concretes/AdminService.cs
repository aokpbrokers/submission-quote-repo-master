using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KPBrokers.Submission.Quote.Services.Concretes
{
    public class AdminService : IAdminService
    {
        private readonly IAdminBusinessLogic _brokerBusinessLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminService"/> class.
        /// </summary>
        /// <param name="brokerBusinessLogic">The broker business logic.</param>
        public AdminService(IAdminBusinessLogic brokerBusinessLogic)
        {
             _brokerBusinessLogic = brokerBusinessLogic;
        }

        /// <summary>
        /// Gets the agent contacts by agent identifier.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>> GetAgentContactsByAgentId(int agentId)
        {
            return await _brokerBusinessLogic.GetAgentContactsByAgentId(agentId);
        }

        /// <summary>
        /// Gets the broker contacts by broker identifier.
        /// </summary>
        /// <param name="brokerId">The broker identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>> GetBrokerContactsByBrokerId(int brokerId)
        {
            return await _brokerBusinessLogic.GetBrokerContactsByBrokerId(brokerId);
        }

        /// <summary>
        /// Gets the carrier contacts by carrier identifier.
        /// </summary>
        /// <param name="carrierId">The carrier identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>> GetCarrierContactsByCarrierId(int carrierId)
        {
            return await _brokerBusinessLogic.GetCarrierContactsByCarrierId(carrierId);
        }

        /// <summary>
        /// Searches for agent company.
        /// </summary>
        /// <param name="agentCompanyName">Name of the agent company.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>> SearchForAgentCompany(string agentCompanyName)
        {
            return await _brokerBusinessLogic.SearchForAgentCompany(agentCompanyName);
        }

        /// <summary>
        /// Searches for broker company.
        /// </summary>
        /// <param name="brokerCompanyName">Name of the broker company.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>> SearchForBrokerCompany(string brokerCompanyName)
        {
            return await _brokerBusinessLogic.SearchForBrokerCompany(brokerCompanyName);
        }

        /// <summary>
        /// Searches for carrier company.
        /// </summary>
        /// <param name="carrierCompanyName">Name of the carrier company.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>> SearchForCarrierCompany(string carrierCompanyName)
        {
            return await _brokerBusinessLogic.SearchForCarrierCompany(carrierCompanyName);
        }

		/// <summary>
		/// Adds the specified agent.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddAgentAsync(HttpClientAgentDetail agentDetail)
		{
			return await _brokerBusinessLogic.AddAgentAsync(agentDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteAgentAsync(int agentId, int userId)
		{
			return await _brokerBusinessLogic.DeleteAgentAsync(agentId, userId);
		}

		/// <summary>
		/// Gets the agent by identifier asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <returns></returns>
		public async Task<Agent> GetAgentByIdAsync(int agentId)
		{
			return await _brokerBusinessLogic.GetAgentByIdAsync(agentId);
		}

		/// <summary>
		/// Gets the agent company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientAgentDetail>> GetAgentCompanyAsync()
		{
			return await _brokerBusinessLogic.GetAgentCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateAgentAsync(HttpClientAgentDetail agentDetail)
		{
			return await _brokerBusinessLogic.UpdateAgentAsync(agentDetail);
		}

        /// <summary>
        /// Gets the agent contact detail by agent identifier asynchronous.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        public async Task<List<HttpClientAgentContactDetail>> GetAgentContactDetailByAgentIdAsync(int agentId)
        {
            return await _brokerBusinessLogic.GetAgentContactDetailByAgentIdAsync(agentId);
        }

        /// <summary>
        /// Inserts the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        public async Task<int> InsertAgentContactAsync(HttpClientAgentContactDetail agentContact)
        {
            if (string.IsNullOrEmpty(agentContact.IdentityId) || agentContact.AgentId == 0)
                throw new Exception("Error has occurred while validating agent contact inputs");
                
            return await _brokerBusinessLogic.InsertAgentContactAsync(agentContact);
        }

        /// <summary>
        /// Updates the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        public async Task<int> UpdateAgentContactAsync(HttpClientAgentContactDetail agentContact)
        {
            return await _brokerBusinessLogic.UpdateAgentContactAsync(agentContact);
        }


		/// <summary>
		/// Adds the specified broker.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddBrokerAsync(HttpClientBrokerDetail brokerDetail)
		{
			return await _brokerBusinessLogic.AddBrokerAsync(brokerDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteBrokerAsync(int brokerId, int userId)
		{
			return await _brokerBusinessLogic.DeleteBrokerAsync(brokerId, userId);
		}

		/// <summary>
		/// Gets the broker by identifier asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		public async Task<Broker> GetBrokerByIdAsync(int brokerId)
		{
			return await _brokerBusinessLogic.GetBrokerByIdAsync(brokerId);
		}

		/// <summary>
		/// Gets the broker company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientBrokerDetail>> GetBrokerCompanyAsync()
		{
			return await _brokerBusinessLogic.GetBrokerCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateBrokerAsync(HttpClientBrokerDetail brokerDetail)
		{
			return await _brokerBusinessLogic.UpdateBrokerAsync(brokerDetail);
		}

		/// <summary>
		/// Gets the broker contact detail by broker identifier asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		public async Task<List<HttpClientBrokerContactDetail>> GetBrokerContactDetailByBrokerIdAsync(int brokerId)
		{
			return await _brokerBusinessLogic.GetBrokerContactDetailByBrokerIdAsync(brokerId);
		}

		/// <summary>
		/// Inserts the broker contact asynchronous.
		/// </summary>
		/// <param name="brokerContact">The broker contact.</param>
		/// <returns></returns>
		public async Task<int> InsertBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
		{
			if (string.IsNullOrEmpty(brokerContact.IdentityId) || brokerContact.BrokerId == 0)
				throw new Exception("Error has occurred while validating broker contact inputs");

			return await _brokerBusinessLogic.InsertBrokerContactAsync(brokerContact);
		}

		/// <summary>
		/// Adds the specified carrier.
		/// </summary>
		/// <param name="carrier">The carrier.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> AddCarrierAsync(HttpClientCarrierDetail carrierDetail)
		{
			return await _brokerBusinessLogic.AddCarrierAsync(carrierDetail);
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public async Task<int> DeleteCarrierAsync(int carrierId, int userId)
		{
			return await _brokerBusinessLogic.DeleteCarrierAsync(carrierId, userId);
		}

		/// <summary>
		/// Gets the carrier by identifier asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		public async Task<Carrier> GetCarrierByIdAsync(int carrierId)
		{
			return await _brokerBusinessLogic.GetCarrierByIdAsync(carrierId);
		}

		/// <summary>
		/// Gets the carrier company asynchronous.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HttpClientCarrierDetail>> GetCarrierCompanyAsync()
		{
			return await _brokerBusinessLogic.GetCarrierCompanyAsync();
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="carrier">The carrier.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public async Task<int> UpdateCarrierAsync(HttpClientCarrierDetail carrierDetail)
		{
			return await _brokerBusinessLogic.UpdateCarrierAsync(carrierDetail);
		}

		/// <summary>
		/// Gets the carrier contact detail by carrier identifier asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		public async Task<List<HttpClientCarrierContactDetail>> GetCarrierContactDetailByCarrierIdAsync(int carrierId)
		{
			return await _brokerBusinessLogic.GetCarrierContactDetailByCarrierIdAsync(carrierId);
		}

		/// <summary>
		/// Inserts the carrier contact asynchronous.
		/// </summary>
		/// <param name="carrierContact">The carrier contact.</param>
		/// <returns></returns>
		public async Task<int> InsertCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
		{
			if (string.IsNullOrEmpty(carrierContact.IdentityId) || carrierContact.CarrierId == 0)
				throw new Exception("Error has occurred while validating carrier contact inputs");

			return await _brokerBusinessLogic.InsertCarrierContactAsync(carrierContact);
		}

        /// <summary>
        /// Updates the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        public async Task<int> UpdateBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
        {
			return await _brokerBusinessLogic.UpdateBrokerContactAsync(brokerContact);
        }

        /// <summary>
        /// Updates the carrier contact asynchronous.
        /// </summary>
        /// <param name="carrierContact">The carrier contact.</param>
        /// <returns></returns>
        public async Task<int> UpdateCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
        {
            return await _brokerBusinessLogic.UpdateCarrierContactAsync(carrierContact);
        }
    }
}
