using ManagementPanel.Data;
using ManagementPanel.GrpcUI.Services;
using ManagementPanel.Service.GeneralExtension;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.LoadMyServices();
builder.Services.ConfigureRabbit();
builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
builder.Services.AddDbContext<ManagementDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("ConStr").ToString(), o =>
    {
        o.MigrationsAssembly("ManagementPanel.Data");
    });
});

var app = builder.Build();

app.MapGrpcService<PanelUserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
