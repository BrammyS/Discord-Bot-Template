using System.Reflection;
using Bot.persistence.MongoDb.Configurations;
using NUnit.Framework;

namespace Bot.persistence.MongoDb.Tests.Configurations;

[TestFixture]
public class CollectionConfiguratorTests
{
    [TestCaseSource(nameof(GetAllConfigurators))]
    public void Should_register_collection(ICollectionConfigurator configurator)
    {
        Assert.DoesNotThrow(configurator.ConfigureCollection);
    }

    public static IEnumerable<ICollectionConfigurator> GetAllConfigurators()
    {
        // Get all the collection configurators from the assembly
        var collectionConfigurators = Assembly.GetAssembly(typeof(ICollectionConfigurator))?.GetTypes()
                                              .Where(x => x.GetInterfaces().Contains(typeof(ICollectionConfigurator)) &&
                                                          x.GetConstructor(Type.EmptyTypes) is not null)
                                              .Select(x => Activator.CreateInstance(x) as ICollectionConfigurator).ToList();

        if (collectionConfigurators == null) yield break;

        foreach (var collectionConfigurator in collectionConfigurators)
            yield return collectionConfigurator!;
    }
}