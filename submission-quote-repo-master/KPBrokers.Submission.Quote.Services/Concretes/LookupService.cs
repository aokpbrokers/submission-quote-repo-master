using KPBrokers.Submission.Quote.BusinessLogic.Abstracts;
using KPBrokers.Submission.Quote.Common.Abstracts;
using KPBrokers.Submission.Quote.Common.Concretes;
using KPBrokers.Submission.Quote.DAL.Abstracts;
using KPBrokers.Submission.Quote.DAL.Concretes;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;
using KPBrokers.Submission.Quote.Services.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.Services.Concretes
{
    /// <summary>
    /// Responsibles for lookup data
    /// </summary>
    /// <seealso cref="KPBrokers.Submission.Quote.Services.Abstracts.ILookupService" />
    public class LookupService : ILookupService
    {
        private ILookUpBusinessLogic _lookupBusinessLogic;


        /// <summary>
        /// Initializes a new instance of the <see cref="LookupService"/> class.
        /// </summary>
        /// <param name="_lookupBusinessLogic">The lookup repository.</param>
        public LookupService(ILookUpBusinessLogic lookupBusinessLogic)
        {
            _lookupBusinessLogic = lookupBusinessLogic;
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _lookupBusinessLogic.GetCountriesAsync();
        }

        /// <summary>
        /// Gets the status asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetStatusAsync()
        {
            return await _lookupBusinessLogic.GetStatusAsync();
        }

        /// <summary>
        /// Gets the titles asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Title>> GetTitlesAsync()
        {
            return await _lookupBusinessLogic.GetTitlesAsync();
        }

        /// <summary>
        /// Gets the coverages asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Coverage>> GetCoveragesAsync()
        {
            return await _lookupBusinessLogic.GetCoveragesAsync();
        }

        /// <summary>
        /// Gets the lookup data.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpClientLookup> GetLookupDataAsync()
        {
            return await _lookupBusinessLogic.GetLookupDataAsync();
        }
    }
}
