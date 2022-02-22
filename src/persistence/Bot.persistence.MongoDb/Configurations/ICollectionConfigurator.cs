namespace Bot.persistence.MongoDb.Configurations;

public interface ICollectionConfigurator
{
    /// <summary>
    ///     Configures the mapping of a domain object so it can be stored in as a collection.
    /// </summary>
    void ConfigureCollection();
}