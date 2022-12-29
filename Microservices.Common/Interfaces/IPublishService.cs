namespace Microservices.Common.Interfaces
{
    public interface IPublishService
    {
        void SendMessage<T>(T obj, string queueName);
    }
}
