using KPBrokers.Submission.Quote.UI.Models;
using KPBrokers.Submission.Quote.UI.Services.Abstracts;

namespace KPBrokers.Submission.Quote.UI.Services.Concretes
{
    public class SenderEmailService : ISenderEmailService
    {
        private readonly ILogger<SenderEmailService> _logger;
        private readonly IClientFactoryService _clientFactoryService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SenderEmailService"/> class.
        /// </summary>
        /// <param name="clientFactoryService">The client factory service.</param>
        public SenderEmailService(IClientFactoryService clientFactoryService, ILogger<SenderEmailService> logger, IConfiguration configuration)
        {
            _clientFactoryService = clientFactoryService;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is email enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is email enabled; otherwise, <c>false</c>.
        /// </value>
        private bool IsEmailEnabled
        {
            get { return Convert.ToBoolean(_configuration["Email:IsEnable"]); }
        }

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public async Task SendEmailAsync(EmailEntity model)
        {
            if (IsEmailEnabled)
            {
                string url = $"common/sendemailasync"; //?toEmail={toEmail}&subject={subject}&message={body}";
                var result = await _clientFactoryService.ExecutePostRequestAsync(url, model);
                if (result == "Ok")
                    _logger.LogInformation($"An email has been successfully sent to {model.EmailTo}");

            }
        }
    }
}
