using Bot.persistence.Repositories;
using Bot.persistence.UnitOfWorks;

namespace Bot.persistence.MongoDb.UnitOfWorks;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IRequestsRepository requestsRepository, IDailyStatsRepository dailyStatsRepository, IServerRepository serverRepository)
    {
        Requests = requestsRepository;
        DailyStats = dailyStatsRepository;
        Servers = serverRepository;
    }

    /// <inheritdoc />
    public IRequestsRepository Requests { get; }

    /// <inheritdoc />
    public IDailyStatsRepository DailyStats { get; }

    /// <inheritdoc />
    public IServerRepository Servers { get; }
}