using KPBrokers.Submission.Quote.Common.Models;
using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;

namespace KPBrokers.Submission.Quote.BusinessLogic.Abstracts
{
    public interface ILookUpBusinessLogic
    {
        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetCountriesAsync();

        /// <summary>
        /// Gets the status asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Status>> GetStatusAsync();

        /// <summary>
        /// Gets the titles asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Title>> GetTitlesAsync();

        /// <summary>
        /// Gets the coverages asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Coverage>> GetCoveragesAsync();

        /// <summary>
        /// Gets the lookup data.
        /// </summary>
        /// <returns></returns>
        Task<HttpClientLookup> GetLookupDataAsync();
    }
}
