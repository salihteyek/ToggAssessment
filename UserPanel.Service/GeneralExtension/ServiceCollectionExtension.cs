using Microsoft.Extensions.DependencyInjection;
using UserPanel.Core.Services;
using UserPanel.Core.Repositories;
using UserPanel.Core.UnitOfWork;
using UserPanel.Data.Repositories;
using UserPanel.Data.UnitOfWork;
using UserPanel.Service.Services;
using UserPanel.Core.Models;
using Microsoft.AspNetCore.Identity;
using UserPanel.Data;
using UserPanel.Service.Services.RabbitMQ;
using UserPanel.Core.Services.RabbitMQ;
using UserPanel.Core.Services.Manager;
using UserPanel.Service.Services.Manager;

namespace UserPanel.Service.GeneralExtension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IManagerService, ManagerService>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped(typeof(IService<,>), typeof(Service<,>));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            
            return serviceCollection;
        }

        public static IServiceCollection ConfigureRabbit(this IServiceCollection services) =>
            services.AddSingleton<RabbitMQContext>()
            .AddTransient<IRabbitManager, RabbitManager>();
    }
}
