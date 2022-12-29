using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using Microservices.Common.Interfaces;

namespace Microservices.Common.Infrastructure.MessageBrocker
{
    public class PublishService: IPublishService
    {
        private readonly ConnectionFactory _factory;
        private string queueName;

        public PublishService()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void SendMessage<T>(T obj, string queueName)
        {
            var message = JsonSerializer.Serialize(obj);
            this.queueName = queueName;
            SendMessage(message);
        }

        private void SendMessage(string message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: queueName,
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
