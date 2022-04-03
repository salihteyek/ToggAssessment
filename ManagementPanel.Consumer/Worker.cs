using ManagementPanel.Consumer.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace ManagementPanel.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQContext _rabbitMQContext;
        private IModel _channel;

        public Worker(RabbitMQContext rabbitMQContext)
        {
            _rabbitMQContext = rabbitMQContext;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQContext.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQContext.RegisteredUserQueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {

            // Gelen kullanıcıyı servise gönder. ordan data katmanına ordan kaydet
            /*
            var user = JsonSerializer.Deserialize<User>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            Console.WriteLine($"Gelen User : {user.Name}");
            */
            _channel.BasicAck(@event.DeliveryTag, false);

        }
    }
}