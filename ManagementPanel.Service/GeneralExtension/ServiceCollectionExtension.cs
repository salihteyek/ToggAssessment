using ManagementPanel.Core.Models;
using ManagementPanel.Core.Repositories;
using ManagementPanel.Core.Services;
using ManagementPanel.Core.Services.RabbitMQ;
using ManagementPanel.Core.UnitOfWork;
using ManagementPanel.Data;
using ManagementPanel.Data.Repositories;
using ManagementPanel.Data.UnitOfWork;
using ManagementPanel.Publisher.Services;
using ManagementPanel.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementPanel.Service.GeneralExtension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPanelUserService, PanelUserService>();
            serviceCollection.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddIdentity<ManagerUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ManagementDbContext>().AddDefaultTokenProviders();

            return serviceCollection;
        }

        public static IServiceCollection ConfigureRabbit(this IServiceCollection services) =>
            services.AddSingleton<RabbitMQContext>()
            .AddTransient<IRabbitManager, RabbitManager>();
    }
}
