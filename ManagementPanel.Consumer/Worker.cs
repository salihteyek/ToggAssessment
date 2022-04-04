using ManagementPanel.Consumer.Services;
using ManagementPanel.Consumer.Services.Grpc;
using ManagementPanel.Core.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ManagementPanel.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly PanelUserGrpcService _panelUserGrpcService;
        private readonly RabbitMQContext _rabbitMQContext;
        
        private IModel _channel;

        public Worker(RabbitMQContext rabbitMQContext, PanelUserGrpcService panelUserGrpcService)
        {
            _rabbitMQContext = rabbitMQContext;
            _panelUserGrpcService = panelUserGrpcService;
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
            var user = JsonSerializer.Deserialize<PanelUser>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            await _panelUserGrpcService.TakeRegisteredUser(user);
            _channel.BasicAck(@event.DeliveryTag, false);
        }
    }
}