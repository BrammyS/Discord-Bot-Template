using Bot.persistence.Domain.Models;

namespace Bot.persistence.Repositories;

/// <summary>
///     This interface holds all the extra methods to query to the <see cref="DailyStat" /> table/collection.
/// </summary>
public interface IDailyStatsRepository : IRepository<DailyStat>
{
    /// <summary>
    ///     Get or add a <see cref="DailyStat" />.
    /// </summary>
    /// <param name="date">The <see cref="DailyStat.Date" /> of the </param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     The task result contains the requested <see cref="DailyStat" />.
    /// </returns>
    Task<DailyStat> GetOrAddAsync(DateTime date);

    /// <summary>
    ///     Increments the <see cref="DailyStat.TotalCommandsUsed" /> value of today's <see cref="DailyStat" />.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous increment operation.
    /// </returns>
    Task IncrementCommandsUsedAsync();
}