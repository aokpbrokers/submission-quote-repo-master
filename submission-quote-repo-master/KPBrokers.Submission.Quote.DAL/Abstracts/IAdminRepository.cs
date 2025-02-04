using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;

namespace KPBrokers.Submission.Quote.DAL.Abstracts
{
    public interface IAdminRepository
    {
		/// <summary>
		/// Searches for agent company.
		/// </summary>
		/// <param name="agentCompanyName">Name of the agent company.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>> SearchForAgentCompany(string agentCompanyName);

		/// <summary>
		/// Gets the agent contacts by agent identifier.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>> GetAgentContactsByAgentId(int agentId);

		/// <summary>
		/// Searches for carrier company.
		/// </summary>
		/// <param name="carrierCompanyName">Name of the carrier company.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>> SearchForCarrierCompany(string carrierCompanyName);

		/// <summary>
		/// Gets the carrier contacts by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>> GetCarrierContactsByCarrierId(int carrierId);

		/// <summary>
		/// Searches for broker company.
		/// </summary>
		/// <param name="brokerCompanyName">Name of the broker company.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>> SearchForBrokerCompany(string brokerCompanyName);

		/// <summary>
		/// Gets the broker contacts by broker identifier.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>> GetBrokerContactsByBrokerId(int brokerId);

		/// <summary>
		/// Adds the specified agent.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Task<int> AddAgentAsync(HttpClientAgentDetail agentDetail);

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<int> DeleteAgentAsync(int agentId, int userId);

		/// <summary>
		/// Gets the agent by identifier asynchronous.
		/// </summary>
		/// <param name="agentId">The agent identifier.</param>
		/// <returns></returns>
		Task<Agent> GetAgentByIdAsync(int agentId);

		/// <summary>
		/// Gets the agent company asynchronous.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<HttpClientAgentDetail>> GetAgentCompanyAsync();

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Task<int> UpdateAgentAsync(HttpClientAgentDetail agentDetail);

        /// <summary>
        /// Gets the agent contact detail by agent identifier.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        Task<List<HttpClientAgentContactDetail>> GetAgentContactDetailByAgentIdAsync(int agentId);

        /// <summary>
        /// Inserts the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        Task<int> InsertAgentContactAsync(HttpClientAgentContactDetail agentContact);

        /// <summary>
        /// Updates the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        Task<int> UpdateAgentContactAsync(HttpClientAgentContactDetail agentContact);

		/// <summary>
		/// Adds the specified broker.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Task<int> AddBrokerAsync(HttpClientBrokerDetail brokerDetail);

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<int> DeleteBrokerAsync(int brokerId, int userId);

		/// <summary>
		/// Gets the broker by identifier asynchronous.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		Task<Broker> GetBrokerByIdAsync(int brokerId);

		/// <summary>
		/// Gets the broker company asynchronous.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<HttpClientBrokerDetail>> GetBrokerCompanyAsync();

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Task<int> UpdateBrokerAsync(HttpClientBrokerDetail brokerDetail);

		/// <summary>
		/// Gets the broker contact detail by broker identifier.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		Task<List<HttpClientBrokerContactDetail>> GetBrokerContactDetailByBrokerIdAsync(int brokerId);

        /// <summary>
        /// Inserts the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        /// <returns></returns>
        Task<int> InsertBrokerContactAsync(HttpClientBrokerContactDetail brokerContact);

        /// <summary>
        /// Updates the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        /// <returns></returns>
        Task<int> UpdateBrokerContactAsync(HttpClientBrokerContactDetail brokerContact);

        /// <summary>
        /// Adds the specified carrier.
        /// </summary>
        /// <param name="carrier">The carrier.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Task<int> AddCarrierAsync(HttpClientCarrierDetail carrierDetail);

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<int> DeleteCarrierAsync(int carrierId, int userId);

		/// <summary>
		/// Gets the carrier by identifier asynchronous.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		Task<Carrier> GetCarrierByIdAsync(int carrierId);

		/// <summary>
		/// Gets the carrier company asynchronous.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<HttpClientCarrierDetail>> GetCarrierCompanyAsync();

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="carrier">The carrier.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Task<int> UpdateCarrierAsync(HttpClientCarrierDetail carrierDetail);

		/// <summary>
		/// Gets the carrier contact detail by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		Task<List<HttpClientCarrierContactDetail>> GetCarrierContactDetailByCarrierIdAsync(int carrierId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="carrierContact"></param>
		/// <returns></returns>
		Task<int> InsertCarrierContactAsync(HttpClientCarrierContactDetail carrierContact);

        /// <summary>
        /// Updates the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        /// <returns></returns>
        Task<int> UpdateCarrierContactAsync(HttpClientCarrierContactDetail carrierContact);
    }
}