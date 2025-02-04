namespace KPBrokers.Submission.Quote.UI.Services.Abstracts
{
    public interface IShortMessagingService
    {
        void SendSMSMessage(string textMessage, string phoneTo);
    }
}