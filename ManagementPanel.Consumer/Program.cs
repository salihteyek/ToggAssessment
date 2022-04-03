using ManagementPanel.Consumer;
using ManagementPanel.Consumer.Helpers;
using ManagementPanel.Consumer.Services;
using ManagementPanel.Consumer.Services.Grpc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        
        Clients clients = configuration.GetSection("Clients").Get<Clients>();
        services.AddSingleton(clients);
        services.AddTransient<GrpcContext>();
        services.AddTransient<PanelUserGrpcService>();
        services.AddTransient<RabbitMQContext>();
        services.AddTransient(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
