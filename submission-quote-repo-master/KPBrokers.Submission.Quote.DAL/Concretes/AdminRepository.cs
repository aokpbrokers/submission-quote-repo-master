using EllipticCurve;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Concretes
{
    public class AdminRepository : IAdminRepository
    {
        private string baseApiUrl = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;

        private readonly KPBDbContext _dbContext;
        private readonly IUserAccountRepository userAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokerService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AdminRepository(IConfiguration configuration, KPBDbContext dbContext, IUserAccountRepository userAccountRepository)
        {
            baseApiUrl = configuration["DocuboxAPI:APIBaseUrl"] ?? string.Empty;
            username = configuration["DocuboxAPI:username"] ?? string.Empty;
            password = configuration["DocuboxAPI:password"] ?? string.Empty;
            _dbContext = dbContext;
            userAccount = userAccountRepository;
        }

        #region Docubox Search

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        private async Task<KPBrokers.Submission.Quote.Common.Models.DocuBox.DocuboxAuthenticationToken?> GetAuthenticationToken()
        {
            string url = $"{baseApiUrl}/api/access?username={username}&password={password}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("username", username));
            collection.Add(new("password", password));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var token = JsonConvert.DeserializeObject<KPBrokers.Submission.Quote.Common.Models.DocuBox.DocuboxAuthenticationToken>(await response.Content.ReadAsStringAsync());
            if (token != null)
                return token;
            return null;
        }


        /// <summary>
        /// Seraches for agent company.
        /// </summary>
        /// <param name="agentCompanyName">Name of the agent company.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Api failed to generate a token</exception>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>> SearchForAgentCompany(string agentCompanyName)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/searchagentbyname?agentname={agentCompanyName}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var agents = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>>(await response.Content.ReadAsStringAsync());
                if (agents != null)
                    return agents.Distinct();
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentCompany>();
            }
            throw new Exception("Api failed to generate a token");
        }

        /// <summary>
        /// Gets the agent contacts by agent identifier.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Api failed to generate a token</exception>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>> GetAgentContactsByAgentId(int agentId)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/getagentcontactsbyagentid?agentid={agentId}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var contacts = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>>(await response.Content.ReadAsStringAsync());
                if (contacts != null)
                    return contacts.DistinctBy(x=>x.agentContactId);
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.AgentContact>();
            }
            throw new Exception("Api failed to generate a token");
        }

        /// <summary>
        /// Searches for carrier company.
        /// </summary>
        /// <param name="carrierCompanyName">Name of the carrier company.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Api failed to generate a token</exception>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>> SearchForCarrierCompany(string carrierCompanyName)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/searchcarrierbyname?carriername={carrierCompanyName}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var carriers = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>>(await response.Content.ReadAsStringAsync());
                if (carriers != null)
                    return carriers.Distinct();
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierCompany>();
            }
            throw new Exception("Api failed to generate a token");
        }

        /// <summary>
        /// Gets the carrier contacts by carrier identifier.
        /// </summary>
        /// <param name="carrierId">The carrier identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Api failed to generate a token</exception>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>> GetCarrierContactsByCarrierId(int carrierId)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/getcarriercontactsbycarrierid?carrierid={carrierId}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var contacts = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>>(await response.Content.ReadAsStringAsync());
                if (contacts != null)
                    return contacts.DistinctBy(x=>x.carrierContactId);
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.CarrierContact>();
            }
            throw new Exception("Api failed to generate a token");
        }

        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>> SearchForBrokerCompany(string brokerCompanyName)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/searchbrokerbyname?brokername={brokerCompanyName}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var brokers = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>>(await response.Content.ReadAsStringAsync());
                if (brokers != null)
                    return brokers.Distinct();
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerCompany>();
            }
            throw new Exception("Api failed to generate a token");
        }

        /// <summary>
        /// Gets the broker contacts by broker identifier.
        /// </summary>
        /// <param name="brokerId">The broker identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Api failed to generate a token</exception>
        public async Task<IEnumerable<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>> GetBrokerContactsByBrokerId(int brokerId)
        {
            var token = await GetAuthenticationToken();
            if (token != null)
            {
                string bearer = $"Bearer {token.token}";
                string url = $"{baseApiUrl}/api/submissions/broker/getbrokercontactsbybrokerid?brokerid={brokerId}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", bearer);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var contacts = JsonConvert.DeserializeObject<List<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>>(await response.Content.ReadAsStringAsync());
                if (contacts != null)
                    return contacts.DistinctBy(x=>x.brokerContactId);
                return new List<KPBrokers.Submission.Quote.Common.Models.DocuBox.BrokerContact>();
            }
            throw new Exception("Api failed to generate a token");
        }

        #endregion

        #region Agent

        /// <summary>
        /// Gets the agent company asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HttpClientAgentDetail>> GetAgentCompanyAsync()
        {
            List<HttpClientAgentDetail> httpClientAgents = new List<HttpClientAgentDetail>();

            var agents = await _dbContext.Agents.Where(x => x.IsActive)
                                         .Include(x => x.Address)
                                         .ThenInclude(c => c.Country)
                                         .ToListAsync();
            if (agents != null)
            {
                foreach (var agent in agents)
                {
                    httpClientAgents.Add(new HttpClientAgentDetail()
                    {
                        AgentId = agent.AgentId,
                        SecondaryAgentId = agent.SecondaryAgentId,
                        Name = agent.Name,
                        DBA = agent.DBA,
                        IsActive = agent.IsActive,
                        AddressId = agent.AddressId,
                        AddressLine1 = agent.Address.AddressLine1,
                        AddressLine2 = agent.Address.AddressLine2,
                        City = agent.Address.City,
                        State = agent.Address.State,
                        PostalCode = agent.Address.PostalCode,
                        CountryId = agent.Address.CountryId,
                        CountryName = agent.Address.Country.CountryName,
                        CreatedDate = agent.CreatedDate,
                        CreatedBy = agent.CreatedBy,
                        CreatedByName = userAccount.UserAccountName(agent.CreatedBy),
                        UpdatedDate = agent.UpdatedDate,
                        UpdatedBy = agent.UpdatedBy,
                        UpdatedByName = userAccount.UserAccountName(agent.UpdatedBy)
                    });
                }
                return httpClientAgents;
            }
            throw new Exception("Error has occurred while fetching agents detail");
        }

        /// <summary>
        /// Gets the agent by identifier asynchronous.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while location the Agent id: {agentId}</exception>
        public async Task<Agent> GetAgentByIdAsync(int agentId)
        {
            var agent = await _dbContext.Agents.SingleOrDefaultAsync(x => x.AgentId == agentId && x.IsActive);
            if (agent == null)
                throw new Exception($"Error has occurred while location the Agent id: {agentId}");
            return agent;
        }


        /// <summary>
        /// Adds the specified agent.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="address">The address.</param>
        /// <returns>Agent</returns>
        /// <exception cref="System.Exception">
        /// The agent you are trying to create is already exists in the system.
        /// or
        /// Error has occured while adding agent address.
        /// or
        /// Error has occurred while creating the agent
        /// </exception>
        public async Task<int> AddAgentAsync(HttpClientAgentDetail agentDetail)
        {
            var agentExists = _dbContext.Agents.Any(x => x.Name == agentDetail.Name);
            if (agentExists)
                return (-1);

            var address = new Address
            {
                AddressLine1 = agentDetail.AddressLine1,
                AddressLine2 = agentDetail.AddressLine2,
                City = agentDetail.City,
                State = agentDetail.State,
                PostalCode = agentDetail.PostalCode,
                CountryId = agentDetail.CountryId,
                CreatedBy = agentDetail.CreatedBy,
                CreatedDate = agentDetail.CreatedDate,
                UpdatedBy = agentDetail.UpdatedBy,
                UpdatedDate = agentDetail.UpdatedDate,
            };
            _dbContext.Addresses.Add(address);
            int addressId = await _dbContext.SaveChangesAsync();

            if (address.AddressId == 0)
                throw new Exception("Error has occured while adding agent address.");

            var agent = new Agent
            {
                Name = agentDetail.Name,
                DBA = agentDetail.DBA,
                SecondaryAgentId = agentDetail.SecondaryAgentId.HasValue ? agentDetail.SecondaryAgentId.Value : null,
                IsActive = agentDetail.IsActive,
                AddressId = address.AddressId,
                CreatedBy = agentDetail.CreatedBy,
                CreatedDate = agentDetail.CreatedDate,
                UpdatedBy = agentDetail.UpdatedBy,
                UpdatedDate = agentDetail.UpdatedDate
            };

            _dbContext.Agents.Add(agent);

            int result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception("Error has occurred while creating the agent");

            return result;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Error has occurred while trying location agent id: {agent.AgentId}
        /// or
        /// Error has occurred while trying to update agent: {agent.Name}
        /// or
        /// Error has occurred while trying to locate address id {agent.AddressId}
        /// or
        /// Error has occurred while executing agent {agent.Name}'s address update
        /// </exception>
        public async Task<int> UpdateAgentAsync(HttpClientAgentDetail agentDetail)
        {
            var foundAgent = await _dbContext.Agents.FirstOrDefaultAsync(x => x.AgentId == agentDetail.AgentId);
            if (foundAgent == null)
                throw new Exception($"Error has occurred while trying location agent id: {agentDetail.AgentId}");

            foundAgent.Name = agentDetail.Name;
            foundAgent.DBA = agentDetail.DBA;
            foundAgent.UpdatedBy = agentDetail.UpdatedBy;
            foundAgent.UpdatedDate = agentDetail.UpdatedDate;

            int updateResult = await _dbContext.SaveChangesAsync();

            if (updateResult == 0)
                throw new Exception($"Error has occurred while trying to update agent: {agentDetail.Name}");

            var foundAgentAddress = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == agentDetail.AddressId);
            if (foundAgentAddress == null)
                throw new Exception($"Error has occurred while trying to locate address id {agentDetail.AddressId}");

            foundAgentAddress.AddressLine1 = agentDetail.AddressLine1;
            foundAgentAddress.AddressLine2 = agentDetail.AddressLine2;
            foundAgentAddress.City = agentDetail.City;
            foundAgentAddress.State = agentDetail.State;
            foundAgentAddress.PostalCode = agentDetail.PostalCode;
            foundAgentAddress.CountryId = agentDetail.CountryId;
            foundAgentAddress.UpdatedBy = agentDetail.UpdatedBy;
            foundAgentAddress.UpdatedDate = agentDetail.UpdatedDate;

            int updatedAddressResult = await _dbContext.SaveChangesAsync();
            if (updatedAddressResult == 0)
                throw new Exception($"Error has occurred while executing agent {agentDetail.Name}'s address update");

            return foundAgent.AgentId;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Error has occurred while locating the Agent: {agentId}
        /// or
        /// Error has occurred while deleting the agent id : {agentId}
        /// </exception>
        public async Task<int> DeleteAgentAsync(int agentId, int userId)
        {
            var foundAgent = await _dbContext.Agents.FirstOrDefaultAsync(x => x.AgentId == agentId);
            if (foundAgent == null)
                throw new Exception($"Error has occurred while locating the Agent: {agentId}");

            foundAgent.IsActive = false;
            foundAgent.UpdatedDate = DateTime.Now;
            foundAgent.UpdatedBy = userId;

            var result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception($"Error has occurred while deleting the agent id : {agentId}");

            return result;
        }
        #endregion

        #region Agent Contact

        /// <summary>
        /// Gets the agent contact detail by agent identifier.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while location agent contacts for agent id {agentId}</exception>
        public async Task<List<HttpClientAgentContactDetail>> GetAgentContactDetailByAgentIdAsync(int agentId)
        {
            var agents = await _dbContext.AgentContacts.Where(a => a.AgentId == agentId)
                                                        .Include(t => t.Title).ToListAsync();

            if (agents == null || agents.Count == 0)
                return new List<HttpClientAgentContactDetail>();

            var httpClientAgents = new List<HttpClientAgentContactDetail>();
            foreach (var agent in agents)
            {
                httpClientAgents.Add(new HttpClientAgentContactDetail
                {
                    AgentContactId = agent.AgentContactId,
                    SecondaryAgentContactId = agent.SecondaryAgentContactId,
                    AgentId = agent.AgentId,
                    TitleId = agent.TitleId,
                    TitleName = agent.Title.TitleName,
                    FirstName = agent.FirstName,
                    MiddleName = agent.MiddleName,
                    LastName = agent.LastName,
                    Email = agent.Email,
                    Phone = agent.Phone,
                    CreatedDate = agent.CreatedDate,
                    UpdatedDate = agent.UpdatedDate,
                    CreatedByName = userAccount.UserAccountName(agent.CreatedBy),
                    UpdatedByName = userAccount.UserAccountName(agent.UpdatedBy),
                });
            }
            return httpClientAgents;
        }

        /// <summary>
        /// Inserts the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while creating agent contact {agentContact.FirstName + ", " + agentContact.LastName}</exception>
        public async Task<int> InsertAgentContactAsync(HttpClientAgentContactDetail agentContact)
        {
            var findContact = await _dbContext.AgentContacts.SingleOrDefaultAsync(x => x.Email == agentContact.Email);
            if (findContact == null)
            {
                var contact = new AgentContact
                {
                    AgentId = agentContact.AgentId,
                    SecondaryAgentContactId = agentContact.SecondaryAgentContactId,
                    TitleId = agentContact.TitleId,
                    FirstName = agentContact.FirstName,
                    LastName = agentContact.LastName,
                    Email = agentContact.Email,
                    IsActive = agentContact.IsActive,
                    Phone = agentContact.Phone,
                    CreatedDate = agentContact.CreatedDate,
                    UpdatedDate = agentContact.UpdatedDate,
                    CreatedBy = agentContact.CreatedBy,
                    UpdatedBy = agentContact.UpdatedBy
                };

                await _dbContext.AgentContacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();

                int agentContactId = contact.AgentContactId;
                if (agentContactId == 0)
                    throw new Exception($"Error has occurred while creating agent contact {agentContact.FirstName + ", " + agentContact.LastName}");

                InsertUserAccount(new UserAccount
                {
                    CompanyId = agentContact.AgentId,
                    ContactId = agentContactId,
                    Fullname = $"{agentContact.FirstName} {agentContact.LastName}",
                    IdentityId = agentContact.IdentityId

                });
                return agentContactId;
            }
            return (-1);
        }

        /// <summary>
        /// Updates the agent contact asynchronous.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while locating agent contact {agentContact.AgentContactId}</exception>
        public async Task<int> UpdateAgentContactAsync(HttpClientAgentContactDetail agentContact)
        {
            var findAgentContact = await _dbContext.AgentContacts.FindAsync(agentContact.AgentContactId);
            if (findAgentContact == null)
                throw new Exception($"Error has occurred while locating agent contact {agentContact.AgentContactId}");

            findAgentContact.TitleId = agentContact.TitleId;
            findAgentContact.FirstName = agentContact.FirstName;
            findAgentContact.LastName = agentContact.LastName;
            findAgentContact.Email = agentContact.Email;
            findAgentContact.Phone = agentContact.Phone;
            findAgentContact.UpdatedBy = agentContact.UpdatedBy;
            findAgentContact.UpdatedDate = agentContact.UpdatedDate;

            await _dbContext.SaveChangesAsync();

            var userAccount = await _dbContext.UserAccounts.SingleOrDefaultAsync(x => x.IdentityId == agentContact.IdentityId);
            if (userAccount == null)
                return (-1);

            string contactFullname = $"{agentContact.FirstName} {agentContact.LastName}";
            if (userAccount.Fullname != contactFullname)
            {
                userAccount.Fullname = contactFullname;
                await _dbContext.SaveChangesAsync();
            }
            return findAgentContact.AgentContactId;
        }

        #endregion

        #region Common

        /// <summary>
        /// Inserts the user account.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        private async void InsertUserAccount(UserAccount userAccount)
        {
            await _dbContext.UserAccounts.AddAsync(userAccount);
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Broker

        /// <summary>
        /// Gets the broker company asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while fetching brokers detail</exception>
        public async Task<IEnumerable<HttpClientBrokerDetail>> GetBrokerCompanyAsync()
        {
            List<HttpClientBrokerDetail> httpClientBrokers = new List<HttpClientBrokerDetail>();

            var brokers = await _dbContext.Brokers.Where(x => x.IsActive)
                                         .Include(x => x.Address)
                                         .ThenInclude(c => c.Country)
                                         .ToListAsync();
            if (brokers != null)
            {
                foreach (var broker in brokers)
                {
                    httpClientBrokers.Add(new HttpClientBrokerDetail()
                    {
                        BrokerId = broker.BrokerId,
                        SecondaryBrokerId = broker.SecondaryBrokerId,
                        Name = broker.Name,
                        DBA = broker.DBA,
                        IsActive = broker.IsActive,
                        AddressId = broker.AddressId,
                        AddressLine1 = broker.Address.AddressLine1,
                        AddressLine2 = broker.Address.AddressLine2,
                        City = broker.Address.City,
                        State = broker.Address.State,
                        PostalCode = broker.Address.PostalCode,
                        CountryId = broker.Address.CountryId,
                        CountryName = broker.Address.Country.CountryName,
                        CreatedDate = broker.CreatedDate,
                        CreatedBy = broker.CreatedBy,
                        CreatedByName = userAccount.UserAccountName(broker.CreatedBy),
                        UpdatedDate = broker.UpdatedDate,
                        UpdatedBy = broker.UpdatedBy,
                        UpdatedByName = userAccount.UserAccountName(broker.UpdatedBy)
                    });
                }
                return httpClientBrokers;
            }
            throw new Exception("Error has occurred while fetching brokers detail");
        }

        /// <summary>
        /// Gets the broker by identifier asynchronous.
        /// </summary>
        /// <param name="brokerId">The broker identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while location the Broker id: {brokerId}</exception>
        public async Task<Broker> GetBrokerByIdAsync(int brokerId)
        {
            var broker = await _dbContext.Brokers.SingleOrDefaultAsync(x => x.BrokerId == brokerId && x.IsActive);
            if (broker == null)
                throw new Exception($"Error has occurred while location the Broker id: {brokerId}");
            return broker;
        }

        /// <summary>
        /// Adds the broker asynchronous.
        /// </summary>
        /// <param name="brokerDetail">The broker detail.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// The broker you are trying to create is already exists in the system.
        /// or
        /// Error has occured while adding broker address.		
        public async Task<int> AddBrokerAsync(HttpClientBrokerDetail brokerDetail)
        {
            var brokerExists = _dbContext.Brokers.Any(x => x.Name == brokerDetail.Name);
            if (brokerExists)
                throw new Exception("The broker you are trying to create is already exists in the system.");

            var address = new Address
            {
                AddressLine1 = brokerDetail.AddressLine1,
                AddressLine2 = brokerDetail.AddressLine2,
                City = brokerDetail.City,
                State = brokerDetail.State,
                PostalCode = brokerDetail.PostalCode,
                CountryId = brokerDetail.CountryId,
                CreatedBy = brokerDetail.CreatedBy,
                CreatedDate = brokerDetail.CreatedDate,
                UpdatedBy = brokerDetail.UpdatedBy,
                UpdatedDate = brokerDetail.UpdatedDate,
            };
            _dbContext.Addresses.Add(address);
            int addressId = await _dbContext.SaveChangesAsync();

            if (address.AddressId == 0)
                throw new Exception("Error has occured while adding broker address.");

            var broker = new Broker
            {
                Name = brokerDetail.Name,
                DBA = brokerDetail.DBA,
                SecondaryBrokerId = brokerDetail.SecondaryBrokerId.HasValue ? brokerDetail.SecondaryBrokerId.Value : null,
                IsActive = brokerDetail.IsActive,
                AddressId = address.AddressId,
                CreatedBy = brokerDetail.CreatedBy,
                CreatedDate = brokerDetail.CreatedDate,
                UpdatedBy = brokerDetail.UpdatedBy,
                UpdatedDate = brokerDetail.UpdatedDate
            };

            _dbContext.Brokers.Add(broker);

            int result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception("Error has occurred while creating the broker");

            return result;
        }

        /// <summary>
        /// Updates the broker asynchronous.
        /// </summary>
        /// <param name="brokerDetail">The broker detail.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Error has occurred while trying location broker id: {brokerDetail.BrokerId}
        /// or
        /// Error has occurred while trying to update broker: {brokerDetail.Name}
        /// or
        /// Error has occurred while trying to locate address id {brokerDetail.AddressId}
        /// or
        /// Error has occurred while executing broker {brokerDetail.Name}'s address update
        /// </exception>
        public async Task<int> UpdateBrokerAsync(HttpClientBrokerDetail brokerDetail)
        {
            var foundBroker = await _dbContext.Brokers.FirstOrDefaultAsync(x => x.BrokerId == brokerDetail.BrokerId);
            if (foundBroker == null)
                throw new Exception($"Error has occurred while trying location broker id: {brokerDetail.BrokerId}");

            foundBroker.Name = brokerDetail.Name;
            foundBroker.DBA = brokerDetail.DBA;
            foundBroker.UpdatedBy = brokerDetail.UpdatedBy;
            foundBroker.UpdatedDate = brokerDetail.UpdatedDate;

            int updateResult = await _dbContext.SaveChangesAsync();

            if (updateResult == 0)
                throw new Exception($"Error has occurred while trying to update broker: {brokerDetail.Name}");

            var foundBrokerAddress = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == brokerDetail.AddressId);
            if (foundBrokerAddress == null)
                throw new Exception($"Error has occurred while trying to locate address id {brokerDetail.AddressId}");

            foundBrokerAddress.AddressLine1 = brokerDetail.AddressLine1;
            foundBrokerAddress.AddressLine2 = brokerDetail.AddressLine2;
            foundBrokerAddress.City = brokerDetail.City;
            foundBrokerAddress.State = brokerDetail.State;
            foundBrokerAddress.PostalCode = brokerDetail.PostalCode;
            foundBrokerAddress.CountryId = brokerDetail.CountryId;
            foundBrokerAddress.UpdatedBy = brokerDetail.UpdatedBy;
            foundBrokerAddress.UpdatedDate = brokerDetail.UpdatedDate;

            int updatedAddressResult = await _dbContext.SaveChangesAsync();
            if (updatedAddressResult == 0)
                throw new Exception($"Error has occurred while executing broker {brokerDetail.Name}'s address update");

            return foundBroker.BrokerId;
        }

        public async Task<int> DeleteBrokerAsync(int brokerId, int userId)
        {
            var foundBroker = await _dbContext.Brokers.FirstOrDefaultAsync(x => x.BrokerId == brokerId);
            if (foundBroker == null)
                throw new Exception($"Error has occurred while locating the Broker: {brokerId}");

            foundBroker.IsActive = false;
            foundBroker.UpdatedDate = DateTime.Now;
            foundBroker.UpdatedBy = userId;

            var result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception($"Error has occurred while deleting the broker id : {brokerId}");

            return result;
        }

        #endregion

        #region Broker Contact

        /// <summary>
        /// Gets the broker contact detail by broker identifier asynchronous.
        /// </summary>
        /// <param name="brokerId">The broker identifier.</param>
        /// <returns></returns>
        public async Task<List<HttpClientBrokerContactDetail>> GetBrokerContactDetailByBrokerIdAsync(int brokerId)
        {
            var brokers = await _dbContext.BrokerContacts.Where(a => a.BrokerId == brokerId)
                                                        .Include(t => t.Title).ToListAsync();

            if (brokers == null || brokers.Count == 0)
                return new List<HttpClientBrokerContactDetail>();

            var httpClientBrokers = new List<HttpClientBrokerContactDetail>();
            foreach (var broker in brokers)
            {
                httpClientBrokers.Add(new HttpClientBrokerContactDetail
                {
                    BrokerContactId = broker.BrokerContactId,
                    BrokerId = broker.BrokerId,
                    SecondaryBrokerContactId = broker.SecondaryBrokerContactId,
                    TitleId = broker.TitleId,
                    TitleName = broker.Title.TitleName,
                    FirstName = broker.FirstName,
                    MiddleName = broker.MiddleName,
                    LastName = broker.LastName,
                    Email = broker.Email,
                    Phone = broker.Phone,
                    CreatedDate = broker.CreatedDate,
                    UpdatedDate = broker.UpdatedDate,
                    CreatedByName = userAccount.UserAccountName(broker.CreatedBy),
                    UpdatedByName = userAccount.UserAccountName(broker.UpdatedBy)
                });
            }
            return httpClientBrokers;
        }

        /// <summary>
        /// Inserts the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while creating broker contact {brokerContact.FirstName + ", " + brokerContact.LastName}</exception>
        public async Task<int> InsertBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
        {
            var findContact = await _dbContext.BrokerContacts.SingleOrDefaultAsync(x => x.Email == brokerContact.Email);
            if (findContact == null)
            {
                var contact = new BrokerContact
                {
                    BrokerId = brokerContact.BrokerId,
                    TitleId = brokerContact.TitleId,
                    SecondaryBrokerContactId = brokerContact.SecondaryBrokerContactId,
                    FirstName = brokerContact.FirstName,
                    LastName = brokerContact.LastName,
                    Email = brokerContact.Email,
                    IsActive = brokerContact.IsActive,
                    Phone = brokerContact.Phone,
                    CreatedDate = brokerContact.CreatedDate,
                    UpdatedDate = brokerContact.UpdatedDate,
                    CreatedBy = brokerContact.CreatedBy,
                    UpdatedBy = brokerContact.UpdatedBy
                };

                await _dbContext.BrokerContacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();

                int brokerContactId = contact.BrokerContactId;
                if (brokerContactId == 0)
                    throw new Exception($"Error has occurred while creating broker contact {brokerContact.FirstName + ", " + brokerContact.LastName}");

                InsertUserAccount(new UserAccount
                {
                    CompanyId = brokerContact.BrokerId,
                    ContactId = brokerContactId,
                    Fullname = $"{brokerContact.FirstName} {brokerContact.LastName}",
                    IdentityId = brokerContact.IdentityId

                });
                return brokerContactId;
            }
            return (-1);
        }

        /// <summary>
        /// Updates the broker contact asynchronous.
        /// </summary>
        /// <param name="brokerContact">The broker contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while locating Broker contact {brokerContact.BrokerContactId}</exception>
        public async Task<int> UpdateBrokerContactAsync(HttpClientBrokerContactDetail brokerContact)
        {
            var findBrokerContact = await _dbContext.BrokerContacts.FindAsync(brokerContact.BrokerContactId);
            if (findBrokerContact == null)
                throw new Exception($"Error has occurred while locating broker contact {brokerContact.BrokerContactId}");

            findBrokerContact.TitleId = brokerContact.TitleId;
            findBrokerContact.FirstName = brokerContact.FirstName;
            findBrokerContact.LastName = brokerContact.LastName;
            findBrokerContact.Email = brokerContact.Email;
            findBrokerContact.Phone = brokerContact.Phone;
            findBrokerContact.UpdatedBy = brokerContact.UpdatedBy;
            findBrokerContact.UpdatedDate = brokerContact.UpdatedDate;

            await _dbContext.SaveChangesAsync();

            var userAccount = await _dbContext.UserAccounts.SingleOrDefaultAsync(x => x.IdentityId == brokerContact.IdentityId);
            if (userAccount == null)
                return (-1);

            string contactFullname = $"{brokerContact.FirstName} {brokerContact.LastName}";
            if (userAccount.Fullname != contactFullname)
            {
                userAccount.Fullname = contactFullname;
                await _dbContext.SaveChangesAsync();
            }
            return findBrokerContact.BrokerContactId;
        }

        #endregion

        #region Carrier

        /// <summary>
        /// Gets the carrier company asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HttpClientCarrierDetail>> GetCarrierCompanyAsync()
        {
            List<HttpClientCarrierDetail> httpClientCarriers = new List<HttpClientCarrierDetail>();

            var carriers = await _dbContext.Carriers.Where(x => x.IsActive)
                                         .Include(x => x.Address)
                                         .ThenInclude(c => c.Country)
                                         .ToListAsync();
            if (carriers != null)
            {
                foreach (var carrier in carriers)
                {
                    httpClientCarriers.Add(new HttpClientCarrierDetail()
                    {
                        CarrierId = carrier.CarrierId,
                        SecondaryCarrierId = carrier.SecondaryCarrierId,
                        Name = carrier.Name,
                        DBA = carrier.DBA,
                        IsActive = carrier.IsActive,
                        AddressId = carrier.AddressId,
                        AddressLine1 = carrier.Address.AddressLine1,
                        AddressLine2 = carrier.Address.AddressLine2,
                        City = carrier.Address.City,
                        State = carrier.Address.State,
                        PostalCode = carrier.Address.PostalCode,
                        CountryId = carrier.Address.CountryId,
                        CountryName = carrier.Address.Country.CountryName,
                        CreatedDate = carrier.CreatedDate,
                        CreatedBy = carrier.CreatedBy,
                        CreatedByName = userAccount.UserAccountName(carrier.CreatedBy),
                        UpdatedDate = carrier.UpdatedDate,
                        UpdatedBy = carrier.UpdatedBy,
                        UpdatedByName = userAccount.UserAccountName(carrier.UpdatedBy),
                    });
                }
                return httpClientCarriers;
            }
            return [];
        }

        /// <summary>
        /// Gets the carrier by identifier asynchronous.
        /// </summary>
        /// <param name="carrierId">The carrier identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while location the Carrier id: {carrierId}</exception>
        public async Task<Carrier> GetCarrierByIdAsync(int carrierId)
        {
            var carrier = await _dbContext.Carriers.SingleOrDefaultAsync(x => x.CarrierId == carrierId && x.IsActive);
            if (carrier == null)
                throw new Exception($"Error has occurred while locating the carrier id: {carrierId}");
            return carrier;
        }


        /// <summary>
        /// Adds the specified carrier.
        /// </summary>
        /// <param name="carrier">The carrier.</param>
        /// <param name="address">The address.</param>
        /// <returns>Carrier</returns>		
        public async Task<int> AddCarrierAsync(HttpClientCarrierDetail carrierDetail)
        {
            var carrierExists = _dbContext.Carriers.Any(x => x.Name == carrierDetail.Name);
            if (carrierExists)
                return (-1);

            var address = new Address
            {
                AddressLine1 = carrierDetail.AddressLine1,
                AddressLine2 = carrierDetail.AddressLine2,
                City = carrierDetail.City,
                State = carrierDetail.State,
                PostalCode = carrierDetail.PostalCode,
                CountryId = carrierDetail.CountryId,
                CreatedBy = carrierDetail.CreatedBy,
                CreatedDate = carrierDetail.CreatedDate,
                UpdatedBy = carrierDetail.UpdatedBy,
                UpdatedDate = carrierDetail.UpdatedDate,
            };
            _dbContext.Addresses.Add(address);
            int addressId = await _dbContext.SaveChangesAsync();

            if (address.AddressId == 0)
                throw new Exception("Error has occured while adding carrier address.");

            var carrier = new Carrier
            {
                Name = carrierDetail.Name,
                DBA = carrierDetail.DBA,
                SecondaryCarrierId = carrierDetail.SecondaryCarrierId.HasValue ? carrierDetail.SecondaryCarrierId.Value : null,
                IsActive = carrierDetail.IsActive,
                AddressId = address.AddressId,
                CreatedBy = carrierDetail.CreatedBy,
                CreatedDate = carrierDetail.CreatedDate,
                UpdatedBy = carrierDetail.UpdatedBy,
                UpdatedDate = carrierDetail.UpdatedDate
            };

            _dbContext.Carriers.Add(carrier);

            int result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception("Error has occurred while creating the carrier");

            return result;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="carrier">The carrier.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">	
        public async Task<int> UpdateCarrierAsync(HttpClientCarrierDetail carrierDetail)
        {
            var foundCarrier = await _dbContext.Carriers.FirstOrDefaultAsync(x => x.CarrierId == carrierDetail.CarrierId);
            if (foundCarrier == null)
                throw new Exception($"Error has occurred while trying locate carrier id: {carrierDetail.CarrierId}");

            foundCarrier.Name = carrierDetail.Name;
            foundCarrier.DBA = carrierDetail.DBA;
            foundCarrier.UpdatedBy = carrierDetail.UpdatedBy;
            foundCarrier.UpdatedDate = carrierDetail.UpdatedDate;

            int updateResult = await _dbContext.SaveChangesAsync();

            if (updateResult == 0)
                throw new Exception($"Error has occurred while trying to update carrier: {carrierDetail.Name}");

            var foundCarrierAddress = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == carrierDetail.AddressId);
            if (foundCarrierAddress == null)
                throw new Exception($"Error has occurred while trying to locate address id {carrierDetail.AddressId}");

            foundCarrierAddress.AddressLine1 = carrierDetail.AddressLine1;
            foundCarrierAddress.AddressLine2 = carrierDetail.AddressLine2;
            foundCarrierAddress.City = carrierDetail.City;
            foundCarrierAddress.State = carrierDetail.State;
            foundCarrierAddress.PostalCode = carrierDetail.PostalCode;
            foundCarrierAddress.CountryId = carrierDetail.CountryId;
            foundCarrierAddress.UpdatedBy = carrierDetail.UpdatedBy;
            foundCarrierAddress.UpdatedDate = carrierDetail.UpdatedDate;

            int updatedAddressResult = await _dbContext.SaveChangesAsync();
            if (updatedAddressResult == 0)
                throw new Exception($"Error has occurred while executing carrier {carrierDetail.Name}'s address update");

            return foundCarrier.CarrierId;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="carrierId">The carrier identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>	
        public async Task<int> DeleteCarrierAsync(int carrierId, int userId)
        {
            var foundCarrier = await _dbContext.Carriers.FirstOrDefaultAsync(x => x.CarrierId == carrierId);
            if (foundCarrier == null)
                throw new Exception($"Error has occurred while locating the Carrier: {carrierId}");

            foundCarrier.IsActive = false;
            foundCarrier.UpdatedDate = DateTime.Now;
            foundCarrier.UpdatedBy = userId;

            var result = await _dbContext.SaveChangesAsync();
            if (result == 0) throw new Exception($"Error has occurred while deleting the carrier id : {carrierId}");

            return result;
        }

        #endregion

        #region Carrier Contact

        /// <summary>
        /// Gets the carrier contact detail by carrier identifier.
        /// </summary>
        /// <param name="carrierId">The carrier identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while location carrier contacts for carrier id {carrierId}</exception>
        public async Task<List<HttpClientCarrierContactDetail>> GetCarrierContactDetailByCarrierIdAsync(int carrierId)
        {
            var carriers = await _dbContext.CarrierContacts.Where(a => a.CarrierId == carrierId)
                                                        .Include(t => t.Title).ToListAsync();

            if (carriers == null || carriers.Count == 0)
                return new List<HttpClientCarrierContactDetail>();

            var httpClientCarriers = new List<HttpClientCarrierContactDetail>();
            foreach (var carrier in carriers)
            {
                httpClientCarriers.Add(new HttpClientCarrierContactDetail
                {
                    CarrierContactId = carrier.CarrierId,
                    CarrierId = carrier.CarrierId,
                    SecondaryCarrierContactId = carrier.SecondaryCarrierContactId,
                    TitleId = carrier.TitleId,
                    TitleName = carrier.Title.TitleName,
                    FirstName = carrier.FirstName,
                    MiddleName = carrier.MiddleName,
                    LastName = carrier.LastName,
                    Email = carrier.Email,
                    Phone = carrier.Phone,
                    CreatedDate = carrier.CreatedDate,
                    UpdatedDate = carrier.UpdatedDate,
                    CreatedByName = userAccount.UserAccountName(carrier.CreatedBy),
                    UpdatedByName = userAccount.UserAccountName(carrier.UpdatedBy),
                });
            }
            return httpClientCarriers;
        }

        /// <summary>
        /// Inserts the carrier contact asynchronous.
        /// </summary>
        /// <param name="carrierContact">The carrier contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while creating carrier contact {carrierContact.FirstName + ", " + carrierContact.LastName}</exception>
        public async Task<int> InsertCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
        {
            var findContact = await _dbContext.CarrierContacts.SingleOrDefaultAsync(x => x.Email == carrierContact.Email);
            if (findContact == null)
            {
                var contact = new CarrierContact
                {
                    CarrierId = carrierContact.CarrierId,
                    SecondaryCarrierContactId = carrierContact.SecondaryCarrierContactId.HasValue? carrierContact.SecondaryCarrierContactId:null,
                    TitleId = carrierContact.TitleId,
                    FirstName = carrierContact.FirstName,
                    LastName = carrierContact.LastName,
                    Email = carrierContact.Email,
                    IsActive = carrierContact.IsActive,
                    Phone = carrierContact.Phone,
                    CreatedDate = carrierContact.CreatedDate,
                    UpdatedDate = carrierContact.UpdatedDate,
                    CreatedBy = carrierContact.CreatedBy,
                    UpdatedBy = carrierContact.UpdatedBy
                };

                await _dbContext.CarrierContacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();

                int carrierContactId = contact.CarrierContactId;
                if (carrierContactId == 0)
                    throw new Exception($"Error has occurred while creating carrier contact {carrierContact.FirstName + ", " + carrierContact.LastName}");

                InsertUserAccount(new UserAccount
                {
                    CompanyId = carrierContact.CarrierId,
                    ContactId = carrierContactId,
                    Fullname = $"{carrierContact.FirstName} {carrierContact.LastName}",
                    IdentityId = carrierContact.IdentityId

                });
                return carrierContactId;
            }
            return (-1);
        }


        /// <summary>
        /// Updates the carrier contact asynchronous.
        /// </summary>
        /// <param name="carrierContact">The carrier contact.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while locating carrier contact {carrierContact.CarrierContactId}</exception>
        public async Task<int> UpdateCarrierContactAsync(HttpClientCarrierContactDetail carrierContact)
        {
            var findCarrierContact = await _dbContext.CarrierContacts.FindAsync(carrierContact.CarrierContactId);
            if (findCarrierContact == null)
                throw new Exception($"Error has occurred while locating carrier contact {carrierContact.CarrierContactId}");

            findCarrierContact.TitleId = carrierContact.TitleId;
            findCarrierContact.FirstName = carrierContact.FirstName;
            findCarrierContact.LastName = carrierContact.LastName;
            findCarrierContact.Email = carrierContact.Email;
            findCarrierContact.Phone = carrierContact.Phone;
            findCarrierContact.UpdatedBy = carrierContact.UpdatedBy;
            findCarrierContact.UpdatedDate = carrierContact.UpdatedDate;

            await _dbContext.SaveChangesAsync();

            var userAccount = await _dbContext.UserAccounts.SingleOrDefaultAsync(x => x.IdentityId == carrierContact.IdentityId);
            if (userAccount == null)
                return (-1);

            string contactFullname = $"{carrierContact.FirstName} {carrierContact.LastName}";
            if (userAccount.Fullname != contactFullname)
            {
                userAccount.Fullname = contactFullname;
                await _dbContext.SaveChangesAsync();
            }
            return findCarrierContact.CarrierContactId;
        }

        #endregion
    }
}


