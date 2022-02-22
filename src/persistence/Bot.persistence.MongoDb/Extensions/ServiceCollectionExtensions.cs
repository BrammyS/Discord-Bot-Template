using Bot.persistence.MongoDb.Repositories;
using Bot.persistence.MongoDb.UnitOfWorks;
using Bot.persistence.Repositories;
using Bot.persistence.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.persistence.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<MongoContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IRequestsRepository, RequestRepository>();
        services.AddSingleton<IServerRepository, ServerRepository>();
        services.AddSingleton<IDailyStatsRepository, DailyStatRepository>();

        return services;
    }
}