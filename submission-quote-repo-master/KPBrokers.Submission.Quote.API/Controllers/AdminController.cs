
using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KPBrokers.Submission.Quote.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokerController"/> class.
        /// </summary>
        /// <param name="broker">The broker.</param>
        /// <param name="logger">The logger.</param>
        public AdminController(IAdminService adminService, ILoggerService logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

		#region Docubox Integration

		[HttpGet("agent/search")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SearchForAgentCompany(string companyName)
		{
			if (string.IsNullOrEmpty(companyName))
				return BadRequest("Company name cannot be null or empty");

			try
			{
				var agentCompanies = await _adminService.SearchForAgentCompany(companyName);
				return Ok(agentCompanies);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while searching for agent company");
				return StatusCode(500, "Internal server error");
			}
		}

		// GET api/agent/contacts/{agentId}
		// Retrieves contacts associated with a specific agent by ID
		[HttpGet("agent/contacts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAgentContactsByAgentId(int agentId)
		{
			try
			{
				var agentContacts = await _adminService.GetAgentContactsByAgentId(agentId);
				return Ok(agentContacts);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while getting agent contacts");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Searches for carrier company.
		/// </summary>
		/// <param name="carrierCompanyName">Name of the carrier company.</param>
		/// <returns></returns>
		[HttpGet("carrier/search")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SearchForCarrierCompany(string carrierCompanyName)
		{
			try
			{
				var carriers = await _adminService.SearchForCarrierCompany(carrierCompanyName);
				return Ok(carriers);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while executing carrier request");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Gets the carrier contact by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		[HttpGet("carrier/contacts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCarrierContactByCarrierId(int carrierId)
		{
			try
			{
				var carrierContacts = await _adminService.GetCarrierContactsByCarrierId(carrierId);				
				return Ok(carrierContacts);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while execuring carrier contacts request");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Searches for broker company.
		/// </summary>
		/// <param name="brokerCompanyName">Name of the broker company.</param>
		/// <returns></returns>
		[HttpGet("broker/search")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SearchForBrokerCompany(string brokerCompanyName)
		{
			try
			{
				var brokers = await _adminService.SearchForBrokerCompany(brokerCompanyName);				
				return Ok(brokers);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while executing carrier request");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("broker/contacts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetBrokerContactByBrokerId(int brokerId)
		{
			try
			{
				var brokerContacts = await _adminService.GetBrokerContactsByBrokerId(brokerId);
				return Ok(brokerContacts);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while execuring broker contacts request");
				return StatusCode(500, "Internal server error");
			}
		}

		#endregion

		#region Agent
		/// <summary>
		/// Adds the agent.
		/// </summary>
		/// <param name="agent">The agent.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		[HttpPost("agent/createagent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAgent(HttpClientAgentDetail agentDetail)
        {
            try
            {
                if (agentDetail == null)
                    return BadRequest("the Agent detail cannot be null");

                var agentCreated = await _adminService.AddAgentAsync(agentDetail);               
                return Ok(agentCreated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating the agent company");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates the agent.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("agent/updateagent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAgent(HttpClientAgentDetail agentDetail)
        {

            if (agentDetail == null || agentDetail.AgentId == 0 || agentDetail.AddressId == 0)
                return BadRequest("Both the Agent and Address ids cannot be null or zero");

            try
            {
                var agentUpdated = await _adminService.UpdateAgentAsync(agentDetail);               
                return Ok(agentUpdated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating the agent company");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets the agents.
        /// </summary>
        /// <returns></returns>
        [HttpGet("agent/getagents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgents()
        {
            try
            {
                var agents = await _adminService.GetAgentCompanyAsync();                
                return Ok(agents);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching the agent company");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region Agent Contact

        /// <summary>
        /// Gets the agent contact detail by agent identifier.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns></returns>
        [HttpGet("agentcontact/contacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentContactDetailByAgentId(int agentId)
        {
            if (agentId == 0)
                return BadRequest("The Agent id cannot be null or 0");

            try
            {
                var agentContacts = await _adminService.GetAgentContactDetailByAgentIdAsync(agentId);
                return Ok(agentContacts);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching the agent contacts");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Adds the agent contact.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        [HttpPost("agentcontact/addagentcontact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAgentContact(HttpClientAgentContactDetail agentContact)
        {
            if (agentContact == null)
                return BadRequest("The Agent contact cannot be null");

            try
            {
                var result = await _adminService.InsertAgentContactAsync(agentContact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating agent contact");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates the agent contact.
        /// </summary>
        /// <param name="agentContact">The agent contact.</param>
        /// <returns></returns>
        [HttpPost("agentcontact/updateagentcontact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAgentContact(HttpClientAgentContactDetail agentContact)
        {
            if (agentContact == null)
                return BadRequest("The Agent contact cannot be null");

            try
            {
                var result = await _adminService.UpdateAgentContactAsync(agentContact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating agent contact");
                return StatusCode(500, "Internal server error");
            }
        }

		#endregion

		#region Broker

		/// <summary>
		/// Adds the broker.
		/// </summary>
		/// <param name="broker">The broker.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		[HttpPost("broker/createbroker")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddBroker(HttpClientBrokerDetail brokerDetail)
		{
			try
			{
				if (brokerDetail == null)
					return BadRequest("the Broker detail cannot be null");

				var brokerCreated = await _adminService.AddBrokerAsync(brokerDetail);
				return Ok(brokerCreated);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while creating the broker company");
				return StatusCode(500, ex.Message);
			}
		}

		/// <summary>
		/// Updates the broker.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost("broker/updatebroker")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateBroker(HttpClientBrokerDetail brokerDetail)
		{

			if (brokerDetail == null || brokerDetail.BrokerId == 0 || brokerDetail.AddressId == 0)
				return BadRequest("Both the Broker and Address ids cannot be null or zero");

			try
			{
				var brokerUpdated = await _adminService.UpdateBrokerAsync(brokerDetail);
				return Ok(brokerUpdated);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while updating the broker company");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Gets the brokers.
		/// </summary>
		/// <returns></returns>
		[HttpGet("broker/getbrokers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetBrokers()
		{
			try
			{
				var brokers = await _adminService.GetBrokerCompanyAsync();
				return Ok(brokers);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while fetching the broker company");
				return StatusCode(500, "Internal server error");
			}
		}

		#endregion

		#region Broker Contact


		/// <summary>
		/// Gets the broker contact detail by broker identifier.
		/// </summary>
		/// <param name="brokerId">The broker identifier.</param>
		/// <returns></returns>
		[HttpGet("brokercontact/contacts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetBrokerContactDetailByBrokerId(int brokerId)
		{
			if (brokerId == 0)
				return BadRequest("The Broker id cannot be null or 0");

			try
			{
				var brokerContacts = await _adminService.GetBrokerContactDetailByBrokerIdAsync(brokerId);
				return Ok(brokerContacts);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while fetching the broker contacts");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Adds the broker contact.
		/// </summary>
		/// <param name="brokerContact">The broker contact.</param>
		/// <returns></returns>
		[HttpPost("brokercontact/addbrokercontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddBrokerContact(HttpClientBrokerContactDetail brokerContact)
		{
			if (brokerContact == null)
				return BadRequest("The Broker contact cannot be null");

			try
			{
				var result = await _adminService.InsertBrokerContactAsync(brokerContact);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while creating broker contact");
				return StatusCode(500, "Internal server error");
			}
		}

        [HttpPost("brokercontact/updatebrokercontact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBrokerContact(HttpClientBrokerContactDetail brokerContact)
        {
            if (brokerContact == null)
                return BadRequest("The broker contact cannot be null");

            try
            {
                var result = await _adminService.UpdateBrokerContactAsync(brokerContact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating broker contact");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region Carrier
        /// <summary>
        /// Adds the carrier.
        /// </summary>
        /// <param name="carrier">The carrier.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        [HttpPost("carrier/createcarrier")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddCarrier(HttpClientCarrierDetail carrierDetail)
		{
			try
			{
				if (carrierDetail == null)
					return BadRequest("the Carrier detail cannot be null");

				var carrierCreated = await _adminService.AddCarrierAsync(carrierDetail);
				return Ok(carrierCreated);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while creating the carrier company");
				return StatusCode(500, ex.Message);
			}
		}

		/// <summary>
		/// Updates the carrier.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost("carrier/updatecarrier")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateCarrier(HttpClientCarrierDetail carrierDetail)
		{

			if (carrierDetail == null || carrierDetail.CarrierId == 0 || carrierDetail.AddressId == 0)
				return BadRequest("Both the Carrier and Address ids cannot be null or zero");

			try
			{
				var carrierUpdated = await _adminService.UpdateCarrierAsync(carrierDetail);
				return Ok(carrierUpdated);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while updating the carrier company");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Gets the carriers.
		/// </summary>
		/// <returns></returns>
		[HttpGet("carrier/getcarriers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCarriers()
		{
			try
			{
				var carriers = await _adminService.GetCarrierCompanyAsync();
				return Ok(carriers);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while fetching the carrier company");
				return StatusCode(500, "Internal server error");
			}
		}

		#endregion

		#region Carrier Contact

		/// <summary>
		/// Gets the carrier contact detail by carrier identifier.
		/// </summary>
		/// <param name="carrierId">The carrier identifier.</param>
		/// <returns></returns>
		[HttpGet("carriercontact/contacts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCarrierContactDetailByCarrierId(int carrierId)
		{
			if (carrierId == 0)
				return BadRequest("The Carrier id cannot be null or 0");

			try
			{
				var carrierContacts = await _adminService.GetCarrierContactDetailByCarrierIdAsync(carrierId);
				return Ok(carrierContacts);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while fetching the carrier contacts");
				return StatusCode(500, "Internal server error");
			}
		}

		/// <summary>
		/// Adds the carrier contact.
		/// </summary>
		/// <param name="carrierContact">The carrier contact.</param>
		/// <returns></returns>
		[HttpPost("carriercontact/addcarriercontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddCarrierContact(HttpClientCarrierContactDetail carrierContact)
		{
			if (carrierContact == null)
				return BadRequest("The Carrier contact cannot be null");

			try
			{
				var result = await _adminService.InsertCarrierContactAsync(carrierContact);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occurred while creating carrier contact");
				return StatusCode(500, "Internal server error");
			}
		}

        /// <summary>
        /// Updates the carrier contact.
        /// </summary>
        /// <param name="carrierContact">The carrier contact.</param>
        /// <returns></returns>
        [HttpPost("carriercontact/updatecarriercontact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCarrierContact(HttpClientCarrierContactDetail carrierContact)
        {
            if (carrierContact == null)
                return BadRequest("The Carrier contact cannot be null");

            try
            {
                var result = await _adminService.UpdateCarrierContactAsync(carrierContact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating carrier contact");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
    }
}
        