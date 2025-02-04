using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.Common.Models;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;

namespace KPBrokers.Submission.Quote.BusinessLogic.Concretes
{
    public class LookUpBusinessLogic : ILookUpBusinessLogic
    {
        private readonly ILookupRepository _lookupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpBusinessLogic"/> class.
        /// </summary>
        /// <param name="lookupRepository">The lookup repository.</param>
        public LookUpBusinessLogic(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;            
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _lookupRepository.GetCountriesAsync();
        }

        /// <summary>
        /// Gets the status asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetStatusAsync()
        {
                return await _lookupRepository.GetStatusAsync();
        }

        /// <summary>
        /// Gets the titles asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Title>> GetTitlesAsync()
        {
            return await _lookupRepository.GetTitlesAsync();
        }

        /// <summary>
        /// Gets the coverages asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Coverage>> GetCoveragesAsync()
        {           
                return await _lookupRepository.GetCoveragesAsync();            
        }

        /// <summary>
        /// Gets the lookup data.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpClientLookup> GetLookupDataAsync()
        {
            return await _lookupRepository.GetLookupDataAsync();
        }
    }
}
