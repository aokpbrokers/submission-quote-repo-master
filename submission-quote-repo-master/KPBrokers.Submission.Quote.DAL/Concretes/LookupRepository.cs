using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.Concretes
{
    /// <summary>
    /// Lookup class is responsible for the referencial entities in  the database    
    /// </summary>
    public class LookupRepository : ILookupRepository
    {
        private readonly KPBDbContext _dbContext;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public LookupRepository(KPBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get Countries
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetStatusAsync()
        {
            return await _dbContext.Statuses.ToListAsync();
        }

        /// <summary>
        /// Get Titles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Title>> GetTitlesAsync()
        {
            return await _dbContext.Titles.ToListAsync();
        }



        /// <summary>
        /// GetCoverages
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Coverage>> GetCoveragesAsync()
        {
            return await _dbContext.Coverages.ToListAsync();
        }

        /// <summary>
        /// Gets the lookup data.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpClientLookup> GetLookupDataAsync()
        {
            var titles = await _dbContext.Titles.ToListAsync();
            var status = await _dbContext.Statuses.ToListAsync();
            var countries = await _dbContext.Countries.ToListAsync();
            var coverages = await _dbContext.Coverages.ToListAsync();

            var httpClientLookup = new HttpClientLookup()
            {
                Titles = titles,
                Status = status,
                Countries = countries,
                Coverages = coverages
            };
            return httpClientLookup;
        }
    }
}
