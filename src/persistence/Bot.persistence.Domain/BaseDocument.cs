namespace Bot.persistence.Domain;

/// <summary>
///     This class represents a basic document that can be stored in MongoDb.
/// </summary>
public class BaseDocument
{
    /// <summary>
    ///     Creates a new <see cref="BaseDocument" />
    /// </summary>
    public BaseDocument()
    {
        AddedAtUtc = DateTime.UtcNow;
        BsonObjectId = string.Empty;
    }

    /// <summary>
    ///     Creates a new <see cref="BaseDocument" />
    /// </summary>
    /// <param name="bsonObjectId">The object id that will be used for the document.</param>
    public BaseDocument(string bsonObjectId)
    {
        AddedAtUtc = DateTime.UtcNow;
        BsonObjectId = bsonObjectId;
    }

    /// <summary>
    ///     The object id of the document.
    /// </summary>
    public string BsonObjectId { get; set; }

    /// <summary>
    ///     The <see cref="DateTime" /> of when the <see cref="BaseDocument" /> was added to the collection.
    /// </summary>
    public DateTime AddedAtUtc { get; set; }
}