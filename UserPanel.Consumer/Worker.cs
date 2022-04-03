using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UserPanel.Consumer.Services.RabbitMQ;
using UserPanel.Core.Models;

namespace UserPanel.Consumer
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
            _channel.BasicConsume(RabbitMQContext.EditedUserQueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var user = JsonSerializer.Deserialize<AppUser>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7190/api/");
            var response = await client.PutAsJsonAsync("manager/update-user", user);
            if (response.IsSuccessStatusCode)
                _channel.BasicAck(@event.DeliveryTag, false);            
        }
    }
}