using Bot.persistence.Domain.Models;

namespace Bot.persistence.Repositories;

/// <summary>
///     This interface holds all the extra methods to query to the <see cref="Request" /> table/collection.
/// </summary>
public interface IRequestsRepository : IRepository<Request>
{
}