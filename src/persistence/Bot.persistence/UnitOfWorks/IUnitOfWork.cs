using Bot.persistence.Domain.Models;
using Bot.persistence.Repositories;

namespace Bot.persistence.UnitOfWorks;

/// <summary>
///     This UnitOfWork contains all the Repositories used to query the all the tables/collections.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Contains all the queries to the <see cref="Request" /> table/collection.
    /// </summary>
    IRequestsRepository Requests { get; }

    /// <summary>
    ///     Contains all the queries to the <see cref="DailyStat" /> table/collection.
    /// </summary>
    IDailyStatsRepository DailyStats { get; }

    /// <summary>
    ///     Contains all the queries to the <see cref="Server" /> table/collection.
    /// </summary>
    IServerRepository Servers { get; }
}