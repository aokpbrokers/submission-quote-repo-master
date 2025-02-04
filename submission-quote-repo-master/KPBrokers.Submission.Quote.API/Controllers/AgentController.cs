using KPBrokers.Submission.Quote.API.Models;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPBrokers.Submission.Quote.API.Controllers
{
    [Route("api/agent")]
    [ApiController]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly ILoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentController"/> class.
        /// </summary>
        /// <param name="agentService">The agent service.</param>
        /// <param name="loggerService">The logger service.</param>
        public AgentController(IAgentService agentService, ILoggerService loggerService)
        {
            _agentService = agentService;
            _logger = loggerService;
        }       
    }
}
