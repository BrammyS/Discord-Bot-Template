using Bot.persistence.Domain.Models;
using Bot.persistence.Repositories;
using MongoDB.Driver;

namespace Bot.persistence.MongoDb.Repositories;

/// <inheritdoc cref="IServerRepository" />
public class ServerRepository : Repository<Server>, IServerRepository
{
    private readonly IMongoCollection<Server> _mongoCollection;

    /// <summary>
    ///     Creates a new <see cref="ServerRepository" />.
    /// </summary>
    /// <param name="context">The <see cref="MongoContext" /> that will be used to query to the database.</param>
    public ServerRepository(MongoContext context) : base(context, nameof(Server) + "s")
    {
        _mongoCollection = context.GetCollection<Server>(nameof(Server) + "s");
    }

    /// <inheritdoc />
    public Task<Server> GetServerAsync(ulong id)
    {
        return _mongoCollection.Find(x => x.GuildId == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<Server> GetOrAddServerAsync(ulong id, string serverName, int memberCount)
    {
        var result = await _mongoCollection.Find(x => x.GuildId == id).FirstOrDefaultAsync().ConfigureAwait(false);
        if (result is not null) return result;
        await AddAsync(new Server
        {
            GuildId = id,
            Name = serverName,
            TotalMembers = memberCount,
            JoinDate = DateTime.UtcNow
        }).ConfigureAwait(false);
        return await _mongoCollection.Find(x => x.GuildId == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}