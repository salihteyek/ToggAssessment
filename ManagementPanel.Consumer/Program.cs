using ManagementPanel.Consumer;
using ManagementPanel.Consumer.Services;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddSingleton<RabbitMQContext>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
