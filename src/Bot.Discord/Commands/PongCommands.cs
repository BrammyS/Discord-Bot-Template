using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Bot.Discord.Commands;

/// <summary>
///     The command module for all pong commands.
/// </summary>
public class PongCommands : SlashCommandModule
{
    public const string PingCommandName = "ping";
    public const string PingCommandDesc = "Ping Pong!";

    /// <summary>
    ///     A simple Ping Pong command.
    /// </summary>
    /// <returns>
    ///     A response with "Pong!".
    /// </returns>
    [UserRateLimit(5, 10)] // Sets the rate limit for this command to 5 requests per 10 seconds per user.
    [SlashCommand(PingCommandName, PingCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> PongAsync()
    {
        //  Return the response to Discord.
        const string message = "Pong!";
        return Task.FromResult(FromSuccess(message));
    }
}