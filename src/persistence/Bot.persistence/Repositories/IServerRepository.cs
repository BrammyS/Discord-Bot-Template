using Bot.persistence.Domain.Models;

namespace Bot.persistence.Repositories;

/// <summary>
///     This interface holds all the extra methods to query to the <see cref="Server" /> table/collection.
/// </summary>
public interface IServerRepository : IRepository<Server>
{
    /// <summary>
    ///     Get a <see cref="Server" /> from the table/collection.
    /// </summary>
    /// <param name="id">The Id of the guild that you want get.</param>
    /// <returns>
    ///     A task that represents the asynchronous get operation.
    ///     The task result contains the requested <see cref="Server" />.
    /// </returns>
    Task<Server> GetServerAsync(ulong id);

    /// <summary>
    ///     Get or add a <see cref="Server" /> from the table/collection.
    /// </summary>
    /// <param name="id">The Id of the guild that you want get.</param>
    /// <param name="serverName">The Name of the guild that you want to get.</param>
    /// <param name="memberCount">The Member count of the guild that you want to get.</param>
    /// <returns>
    ///     A task that represents the asynchronous get or add operation.
    ///     The task result contains the requested <see cref="Server" />.
    /// </returns>
    Task<Server> GetOrAddServerAsync(ulong id, string serverName, int memberCount);
}