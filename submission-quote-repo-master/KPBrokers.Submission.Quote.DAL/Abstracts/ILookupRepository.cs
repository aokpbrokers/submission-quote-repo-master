using KPBrokers.Submission.Quote.DAL.DatabaseEntities;
using KPBrokers.Submission.Quote.DAL.Metadata;

namespace KPBrokers.Submission.Quote.DAL.Abstracts
{
    /// <summary>    
    /// Provides an interface for lookup data operations.
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetCountriesAsync();

        /// <summary>
        /// Gets the coverages.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Coverage>> GetCoveragesAsync();

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Status>> GetStatusAsync();

        /// <summary>
        /// Gets the titles.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Title>> GetTitlesAsync();

        /// <summary>
        /// Gets the lookup data.
        /// </summary>
        /// <returns></returns>
        Task<HttpClientLookup> GetLookupDataAsync();
    }
}