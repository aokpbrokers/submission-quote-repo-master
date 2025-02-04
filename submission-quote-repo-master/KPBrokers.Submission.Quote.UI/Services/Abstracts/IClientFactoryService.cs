using KPBrokers.Submission.Quote.UI.Models;

namespace KPBrokers.Submission.Quote.UI.Services.Abstracts
{
    public interface IClientFactoryService
    {
        Task<string> ExecutePostRequestAsync<T>(string url, T model);
        Task<string> ExecuteGetRequestAsync(string url);
    }
}