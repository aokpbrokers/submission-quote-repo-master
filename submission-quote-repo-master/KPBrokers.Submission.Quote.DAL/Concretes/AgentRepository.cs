using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Concretes
{
    public class AgentRepository : IAgentRepository
    {
        private readonly KPBDbContext _dbContext;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public AgentRepository(KPBDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }        
    }
}
