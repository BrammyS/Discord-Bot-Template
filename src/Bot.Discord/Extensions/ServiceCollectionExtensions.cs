using Bot.Discord.Pipelines;
using Bot.persistence.MongoDb.Extensions;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Discord.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscord(this IServiceCollection services)
    {
        // Todo: Replace the publicKey and the botId with yours. https://discord.com/developers/applications.
        const string publicKey = "5d7890af1ce9b286d76e8129891e8acff77782a1b2e6f06e6023fa09557c8c1d";
        const long botId = 541336442979483658;
        
        var token = Environment.GetEnvironmentVariable("BOT_TOKEN");
        if (token is null)
        {
            throw new NullReferenceException("Please set the BOT_TOKEN env variable!");
        }
        
        // Configure Color-Chan.Discord
        var config = new ColorChanConfigurations
        {
            SlashCommandConfigs = slashOptions =>
            {
                slashOptions.SendDefaultErrorMessage = true;
            }
        };
        
        services.AddSlashCommandPipeline<GuildDbPipeline>();
        services.AddSlashCommandPipeline<CommandLoggingPipeline>();
        services.AddComponentInteractionPipeline<ComponentLoggingPipeline>();

        
        services.AddColorChanDiscord(token, publicKey, botId, config);
        services.AddMongoDb();

        return services;
    }
}