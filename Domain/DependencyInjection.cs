using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<UserService>() // or any type from this project
                .AddClasses(classes => classes.InNamespaces("Domain.Services"))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }