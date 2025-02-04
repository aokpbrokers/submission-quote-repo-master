using KPBrokers.Submission.Quote.UI.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace KPBrokers.Submission.Quote.UI.Services.Concretes
{
    /// <summary>
    /// Responsible for sending phone short messaging message
    /// </summary>
    public class ShortMessagingService : IShortMessagingService
    {
        private string accountSid = string.Empty;
        private string authToken = string.Empty;
        private string phoneFrom = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortMessagingService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ShortMessagingService(IConfiguration configuration)
        {
            accountSid = configuration["TwilioSMS:SID"] ?? string.Empty;
            authToken = configuration["TwilioSMS:AuthToken"] ?? string.Empty;
            phoneFrom = configuration["TwilioSMS:PhoneFrom"] ?? string.Empty;
        }

        /// <summary>
        /// Sends the SMS message.
        /// </summary>
        /// <param name="textMessage">The text message.</param>
        /// <param name="phoneTo">The phone to.</param>
        public void SendSMSMessage(string textMessage, string phoneTo)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber(phoneTo));
                messageOptions.From = new PhoneNumber(phoneFrom);
                messageOptions.Body = textMessage;
                var message = MessageResource.Create(messageOptions);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
