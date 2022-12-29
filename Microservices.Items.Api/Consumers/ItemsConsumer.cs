using MassTransit;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using MediatR;
using System.Text.Json;
using Microservices.Items.BusinessLogic.Commands;

namespace Microservices.Items.Api.Consumers
{
    public class ItemsConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ItemsConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "itemsToAdd", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var items = JsonSerializer.Deserialize<CreateItemsList>(content);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    await mediator.Send(items);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("itemsToAdd", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
