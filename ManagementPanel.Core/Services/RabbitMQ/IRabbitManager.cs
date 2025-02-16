﻿namespace ManagementPanel.Core.Services.RabbitMQ
{
    public interface IRabbitManager
    {
        Task Publish<T>(T message, string exchangeName, string exchangeType, string routeKey, string queueName) where T : class;
    }
}
