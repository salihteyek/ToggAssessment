namespace UserPanel.Core.Services.RabbitMQ
{
    public interface IRabbitManager
    {
        void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey, string queueName) where T : class;
    }
}
