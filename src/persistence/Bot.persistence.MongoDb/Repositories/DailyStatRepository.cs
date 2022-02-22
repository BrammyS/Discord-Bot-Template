using Bot.persistence.Domain.Models;
using Bot.persistence.Repositories;
using MongoDB.Driver;

namespace Bot.persistence.MongoDb.Repositories;

/// <inheritdoc cref="IDailyStatsRepository" />
public class DailyStatRepository : Repository<DailyStat>, IDailyStatsRepository
{
    private readonly IMongoCollection<DailyStat> _mongoCollection;

    /// <summary>
    ///     Creates a new <see cref="DailyStatRepository" />.
    /// </summary>
    /// <param name="context">The <see cref="MongoContext" /> that will be used to query to the database.</param>
    public DailyStatRepository(MongoContext context) : base(context, nameof(DailyStat) + "s")
    {
        _mongoCollection = context.GetCollection<DailyStat>(nameof(DailyStat) + "s");
    }

    /// <inheritdoc />
    public async Task<DailyStat> GetOrAddAsync(DateTime date)
    {
        await CheckIfDailyStatsExistsAsync(DateTime.UtcNow).ConfigureAwait(false);
        return await _mongoCollection.Find(x => x.Date == date.Date).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task IncrementCommandsUsedAsync()
    {
        await CheckIfDailyStatsExistsAsync(DateTime.UtcNow).ConfigureAwait(false);
        var filter = Builders<DailyStat>.Filter.Eq(e => e.Date, DateTime.UtcNow.Date);
        var update = Builders<DailyStat>.Update.Inc(x => x.TotalCommandsUsed, 1);
        await _mongoCollection.FindOneAndUpdateAsync(filter, update).ConfigureAwait(false);
    }

    /// <summary>
    ///     Check if the <see cref="DailyStat" /> exists.
    ///     Create one if it doesn't exist.
    /// </summary>
    /// <param name="date">The <see cref="DailyStat.Date" /> that will be used to search for the <see cref="DailyStat" />.</param>
    /// <returns>
    ///     A task that represents the asynchronous search operation.
    /// </returns>
    private async Task CheckIfDailyStatsExistsAsync(DateTime date)
    {
        if (await _mongoCollection.CountDocumentsAsync(x => x.Date == date.Date).ConfigureAwait(false) >= 1) return;
        await AddDefaultAsync(date.Date).ConfigureAwait(false);
    }

    /// <summary>
    ///     Adds the default <see cref="DailyStat" /> to the database.
    /// </summary>
    /// <param name="date">The <see cref="DailyStat.Date" />.</param>
    /// <returns>
    ///     A task that represents the asynchronous add operation.
    /// </returns>
    private Task AddDefaultAsync(DateTime date)
    {
        return AddAsync(new DailyStat
        {
            Date = date,
            TotalCommandsUsed = 0
        });
    }
}