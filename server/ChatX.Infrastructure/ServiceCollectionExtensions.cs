using ChatX.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChatX.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("Postgres");
        services.AddNpgsql<ChatDbContext>(postgresConnectionString);

        var redisConnectionString = configuration.GetConnectionString("Redis");
        services.AddSingleton(_ =>
        {
            var mux = ConnectionMultiplexer.Connect(redisConnectionString);
            return mux.GetDatabase();
        });

        return services;
    }
}