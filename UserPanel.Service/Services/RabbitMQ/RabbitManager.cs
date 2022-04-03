using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserPanel.Core.Services.RabbitMQ;

namespace UserPanel.Service.Services.RabbitMQ
{
    public class RabbitManager: IRabbitManager
    {
        private readonly RabbitMQContext _rabbitMQContext;
        public RabbitManager(RabbitMQContext rabbitMQContext)
        {
            _rabbitMQContext = rabbitMQContext;
        }

        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey, string queueName) where T : class
        {
            if (message == null)
                return;

            var channel = _rabbitMQContext.Connect();
            try
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: true, autoDelete: false, arguments: null);

                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(exchange: exchangeName, queue: queueName, routingKey: routeKey);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                channel.BasicPublish(exchange: exchangeName, routingKey: routeKey ?? String.Empty, basicProperties: properties, body: body);

                Console.WriteLine("Kullanıcı Gönderildi...");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
