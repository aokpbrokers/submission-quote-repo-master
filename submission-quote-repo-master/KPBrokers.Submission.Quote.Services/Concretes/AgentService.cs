using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.Services.Concretes
{
    public class AgentService : IAgentService
    {
        private readonly IAgentBusinessLogic _agentBusinessLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentService"/> class.
        /// </summary>
        /// <param name="agentBusinessLogic">The agent business logic.</param>
        public AgentService(IAgentBusinessLogic agentBusinessLogic)
        {
            _agentBusinessLogic = agentBusinessLogic;
        }        
    }
}
