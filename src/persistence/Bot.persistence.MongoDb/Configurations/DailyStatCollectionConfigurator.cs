using Bot.persistence.Domain.Models;
using MongoDB.Bson.Serialization;

namespace Bot.persistence.MongoDb.Configurations;

public class DailyStatCollectionConfigurator : ICollectionConfigurator
{
    /// <inheritdoc />
    public void ConfigureCollection()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(DailyStat))) return;

        BsonClassMap.RegisterClassMap<DailyStat>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(c => c.Date).SetElementName("Date").SetIsRequired(true);
            cm.MapMember(c => c.TotalCommandsUsed).SetElementName("TotalCommandsUsed").SetIsRequired(true);
        });
    }
}