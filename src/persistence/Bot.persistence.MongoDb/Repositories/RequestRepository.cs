using Bot.persistence.Domain.Models;
using Bot.persistence.Repositories;

namespace Bot.persistence.MongoDb.Repositories;

/// <inheritdoc cref="IRequestsRepository" />
public class RequestRepository : Repository<Request>, IRequestsRepository
{
    /// <summary>
    ///     Creates a new <see cref="RequestRepository" />.
    /// </summary>
    /// <param name="context">The <see cref="MongoContext" /> that will be used to query to the database.</param>
    public RequestRepository(MongoContext context) : base(context, nameof(Request) + "s")
    {
    }
}