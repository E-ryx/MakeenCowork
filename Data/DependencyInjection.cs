using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // Register EF Core with SQLite
        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CoWork")));

        // Register all repositories using Scrutor
        services.Scan(scan => scan
            .FromAssemblyOf<UserRepository>() // or typeof(IUserRepository)
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            );

        services.Scan(scan => scan
            .FromAssemblyOf<IUserRepository>() // or typeof(IUserRepository)
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    } 
}