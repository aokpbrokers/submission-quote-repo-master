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
    public class AgentBusinessLogic : IAgentBusinessLogic
    {
        private readonly IAgentRepository _agentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentBusinessLogic"/> class.
        /// </summary>
        /// <param name="agentRepository">The agent repository.</param>
        public AgentBusinessLogic(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }    
    }
}
