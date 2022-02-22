using Bot.persistence.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Bot.persistence.MongoDb.Configurations;

public class ServerCollectionConfigurator : ICollectionConfigurator
{
    /// <inheritdoc />
    public void ConfigureCollection()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Server))) return;

        BsonClassMap.RegisterClassMap<Server>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(c => c.GuildId)
              .SetElementName("GuildId")
              .SetSerializer(new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false)))
              .SetIsRequired(true);
            cm.MapMember(c => c.Name)
              .SetElementName("Name")
              .SetIgnoreIfNull(true)
              .SetIsRequired(false);
            cm.MapMember(c => c.TotalMembers).SetElementName("TotalMembers").SetIsRequired(true);
            cm.MapMember(c => c.JoinDate).SetElementName("JoinDate").SetIsRequired(true);
        });
    }
}