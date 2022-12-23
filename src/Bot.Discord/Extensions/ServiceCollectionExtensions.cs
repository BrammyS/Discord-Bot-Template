using Bot.Discord.Pipelines;
using Bot.persistence.MongoDb.Extensions;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Discord.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
    {
        var token = configuration["BOT_TOKEN"];
        if (token is null)
        {
            throw new NullReferenceException("Please set the BOT_TOKEN configuration!");
        }

        // Configure Color-Chan.Discord
        var config = new ColorChanConfigurations
        {
            SlashCommandConfigs = slashOptions => { slashOptions.SendDefaultErrorMessage = true; }
        };

        services.AddInteractionPipeline<GuildDbPipeline>();
        services.AddInteractionPipeline<InteractionLoggingPipeline>();

        services.AddColorChanDiscord(token, Constants.PublicKey, Constants.BotId, config);
        services.AddMongoDb();

        return services;
    }
}