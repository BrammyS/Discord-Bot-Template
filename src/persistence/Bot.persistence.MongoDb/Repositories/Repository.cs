using System.Linq.Expressions;
using Bot.persistence.Domain;
using Bot.persistence.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Bot.persistence.MongoDb.Repositories;

/// <inheritdoc />
public class Repository<T> : IRepository<T> where T : BaseDocument
{
    private readonly IMongoCollection<T> _mongoCollection;

    public Repository(MongoContext context, string collectionName)
    {
        CollectionName = collectionName;
        _mongoCollection = context.GetCollection<T>(CollectionName);
    }

    public string CollectionName { get; }

    /// <inheritdoc />
    public Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return _mongoCollection.Find(predicate).FirstOrDefaultAsync()!;
    }

    /// <inheritdoc />
    public Task<List<T>> GetAllAsync()
    {
        return _mongoCollection.Find(FilterDefinition<T>.Empty).ToListAsync();
    }

    /// <inheritdoc />
    public Task<List<T>> GetAllAsync(int page, int pageAmount)
    {
        return _mongoCollection.Find(FilterDefinition<T>.Empty)
                               .Skip((page - 1) * pageAmount)
                               .Limit(pageAmount)
                               .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _mongoCollection.Find(predicate).ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task AddAsync(T document)
    {
        if (string.IsNullOrEmpty(document.BsonObjectId))
        {
            document.BsonObjectId = ObjectId.GenerateNewId().ToString();
        }

        return _mongoCollection.InsertOneAsync(document);
    }

    /// <inheritdoc />
    public Task AddAsync(List<T> documents)
    {
        foreach (var document in documents)
        {
            if (string.IsNullOrEmpty(document.BsonObjectId))
            {
                document.BsonObjectId = ObjectId.GenerateNewId().ToString();
            }
        }

        return _mongoCollection.InsertManyAsync(documents);
    }

    /// <inheritdoc />
    public async Task<long> RemoveAsync(string objectId)
    {
        var filter = Builders<T>.Filter.Eq(x => x.BsonObjectId, objectId);
        var result = await _mongoCollection.DeleteOneAsync(filter).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public async Task<long> RemoveAsync(IEnumerable<string> objectIds)
    {
        var filter = Builders<T>.Filter.In(x => x.BsonObjectId, objectIds);
        var result = await _mongoCollection.DeleteManyAsync(filter).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public async Task<long> RemoveWhereAsync(Expression<Func<T, bool>> predicateSearch)
    {
        var result = await _mongoCollection.DeleteManyAsync(predicateSearch).ConfigureAwait(false);
        return result.DeletedCount;
    }

    /// <inheritdoc />
    public Task UpdateValueAsync(Expression<Func<T, object>> predicateSearch, object searchValue,
                                 Expression<Func<T, object?>> predicateNew, object? newValue)
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        var update = Builders<T>.Update.Set(predicateNew, newValue);
        return _mongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task UpdateValueAsync(string objectId, Expression<Func<T, object?>> predicateNew, object? newValue)
    {
        var filter = Builders<T>.Filter.Eq(x => x.BsonObjectId, objectId);
        var update = Builders<T>.Update.Set(predicateNew, newValue);
        return _mongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task IncrementValueAsync(string objectId, Expression<Func<T, long>> predicateIncrement,
                                    long incrementAmount = 1)
    {
        var filter = Builders<T>.Filter.Eq(e => e.BsonObjectId, objectId);
        var update = Builders<T>.Update.Inc(predicateIncrement, incrementAmount);
        return _mongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public Task IncrementValueAsync(Expression<Func<T, object>> predicateSearch, object searchValue,
                                    Expression<Func<T, long>> predicateIncrement, long incrementAmount = 1)
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        var update = Builders<T>.Update.Inc(predicateIncrement, incrementAmount);
        return _mongoCollection.UpdateOneAsync(filter, update);
    }

    /// <inheritdoc />
    public async Task<List<T>> GetLastDocumentsAsync(int count)
    {
        var documents = await _mongoCollection.AsQueryable().OrderByDescending(x => x.AddedAtUtc).Take(count)
                                              .ToListAsync().ConfigureAwait(false);
        return new List<T>(documents.OrderBy(x => x.AddedAtUtc));
    }

    /// <inheritdoc />
    public Task<long> DocumentCountAsync()
    {
        return _mongoCollection.EstimatedDocumentCountAsync();
    }

    /// <inheritdoc />
    public Task<long> CountWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return _mongoCollection.CountDocumentsAsync(predicate);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        var docCount = await _mongoCollection.CountDocumentsAsync(predicate).ConfigureAwait(false);
        return docCount > 0;
    }

    /// <inheritdoc />
    public Task ReplaceOneAsync(Expression<Func<T, object>> predicateSearch, object searchValue, T newValue)
    {
        var filter = Builders<T>.Filter.Eq(predicateSearch, searchValue);
        return _mongoCollection.ReplaceOneAsync(filter, newValue);
    }

    /// <inheritdoc />
    public Task ReplaceOneAsync(string objectId, T newValue)
    {
        var filter = Builders<T>.Filter.Eq(e => e.BsonObjectId, objectId);
        return _mongoCollection.ReplaceOneAsync(filter, newValue);
    }

    /// <inheritdoc />
    public async Task<T?> GetRandomDocument()
    {
        var result = await _mongoCollection.AsQueryable().Sample(1).FirstOrDefaultAsync();
        return result;
    }
}