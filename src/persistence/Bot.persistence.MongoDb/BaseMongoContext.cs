using System.Reflection;
using Bot.persistence.MongoDb.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Bot.persistence.MongoDb;

public class BaseMongoContext
{
    /// <summary>
    ///     Creates a new <see cref="BaseMongoContext" />.
    /// </summary>
    protected BaseMongoContext()
    {
        ConfigureCollections();

        // Set up MongoDB conventions
        var pack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        };

        ConventionRegistry.Register("EnumStringConvention", pack, _ => true);
    }

    private void ConfigureCollections()
    {
        // Get all the collection configurators from the assembly
        var collectionConfigurators = Assembly.GetExecutingAssembly().GetTypes()
                                              .Where(x => x.GetInterfaces().Contains(typeof(ICollectionConfigurator)) &&
                                                          x.GetConstructor(Type.EmptyTypes) is not null)
                                              .Select(x => Activator.CreateInstance(x) as ICollectionConfigurator).ToList();

        foreach (var collectionConfigurator in collectionConfigurators)
            collectionConfigurator?.ConfigureCollection();
    }
}