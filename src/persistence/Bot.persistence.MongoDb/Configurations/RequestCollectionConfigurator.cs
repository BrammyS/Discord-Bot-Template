using Bot.persistence.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Bot.persistence.MongoDb.Configurations;

public class RequestCollectionConfigurator : ICollectionConfigurator
{
    /// <inheritdoc />
    public void ConfigureCollection()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Request))) return;

        BsonClassMap.RegisterClassMap<Request>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(c => c.Command).SetElementName("Command").SetIsRequired(true);
            cm.MapMember(c => c.ErrorMessage).SetElementName("ErrorMessage").SetIgnoreIfNull(true);
            cm.MapMember(c => c.IsSuccessful).SetElementName("IsSuccessful").SetIsRequired(true);
            cm.MapMember(c => c.TimeStamp).SetElementName("TimeStamp").SetIsRequired(true);
            cm.MapMember(c => c.ElapsedMilliseconds)
              .SetElementName("ElapsedMilliseconds")
              .SetIgnoreIfNull(true)
              .SetIsRequired(false);
            cm.MapMember(c => c.MessageId)
              .SetElementName("MessageId")
              .SetIsRequired(false)
              .SetIgnoreIfNull(true)
              .SetSerializer(new NullableSerializer<ulong>(new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false))));
            cm.MapMember(c => c.ServerId).SetElementName("ServerId")
              .SetSerializer(new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false)))
              .SetIsRequired(true);
            cm.MapMember(c => c.UserId).SetElementName("UserId")
              .SetSerializer(new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false)))
              .SetIsRequired(true);
        });
    }
}