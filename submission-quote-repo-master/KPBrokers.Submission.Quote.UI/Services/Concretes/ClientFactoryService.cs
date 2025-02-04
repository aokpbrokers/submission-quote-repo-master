using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using KPBrokers.Submission.Quote.UI.Services.Caching;
using Newtonsoft.Json;
using System.Text;

namespace KPBrokers.Submission.Quote.UI.Services.Concretes
{
    public class ClientFactoryService : IClientFactoryService
    {
        private IConfiguration _configuration;
        private ICacheService _cacheService;

        private string baseUrl = string.Empty;
        private string oauthUsername = string.Empty;
        private string oauthPassword = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactoryService"/> class.
        /// </summary>
        /// <param name="_configuration">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">_configuration</exception>
        public ClientFactoryService(IConfiguration configuration, ICacheService cacheService)
        {
            _configuration = configuration;
            baseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty;
            oauthUsername = _configuration["ApiSettings:OAuthUsername"] ?? string.Empty;
            oauthPassword = _configuration["ApiSettings:OAuthPassword"] ?? string.Empty;
            _cacheService = cacheService;
        }

        private string CachedKey { get { return "KPBAPITOKEN"; } }

        /// <summary>
        /// Tokens this instance.
        /// </summary>
        /// <returns></returns>
        private async Task<AuthorisationToken?> Token()
        {
            if (!_cacheService.Exists(CachedKey))
            {
                string url = $"{baseUrl}authentication?username={oauthUsername}&password={oauthPassword}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var token = JsonConvert.DeserializeObject<AuthorisationToken>(await response.Content.ReadAsStringAsync());

                _cacheService.Save(CachedKey, token);

                return token;
            }
            else
            {
                var storedToken = (AuthorisationToken)_cacheService.Get(CachedKey);

                var tokenExpiryTime = storedToken.expires.TimeOfDay;

                if (DateTime.Now.TimeOfDay > tokenExpiryTime)
                {
                    _cacheService.Remove(CachedKey);
                    return await Token();
                }
                return storedToken;
            }
        }                

        

        /// <summary>
        /// Executes the get request asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public async Task<string> ExecutePostRequestAsync<T>(string url, T model)
        {
            var _baseUrl = $"{baseUrl}{url}";
            var token = await Token();
            if (token != null)
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl) { Content = content };
                request.Headers.Add("Authorization", $"Bearer {token.token}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            throw new Exception("Error has occurred while requesting api token");
        }

        /// <summary>
        /// Executes the get request asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error has occurred while requesting api token</exception>
        public async Task<string> ExecuteGetRequestAsync(string url)
        {
            var _baseUrl = $"{baseUrl}{url}";
            var token = await Token();
            if (token != null)
            {               
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
                request.Headers.Add("Authorization", $"Bearer {token.token}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            throw new Exception("Error has occurred while requesting api token");
        }
    }
}


