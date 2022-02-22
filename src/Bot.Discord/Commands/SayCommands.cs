using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Bot.Discord.Commands;

/// <summary>
///     The command module for all say commands.
/// </summary>
[UserRateLimit(5, 10)] // Sets the rate limit for this command module to 5 requests per 10 seconds per user.
public class SayCommands : SlashCommandModule
{
    public const string SayCommandName = "say";
    public const string SayCommandDesc = "Say something with the bot!";

    /// <summary>
    ///     A simple say command where the bot will reply back with the message that it is provided.
    /// </summary>
    /// <param name="text">The text that the bot will send in the embed.</param>
    /// <returns>
    ///     An embedded response with the provided message.
    /// </returns>
    [SlashCommand(SayCommandName, SayCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> SayAsync
    (
        [SlashCommandOption("text", "The text that will be returned.", true)]
        string text
    )
    {
        // Build the embedded response.
        var embed = new DiscordEmbedBuilder()
                    .WithDescription(text)
                    .WithColor(Constants.Colors.Successful)
                    .WithTimeStamp()
                    .Build();

        //  Return the response to Discord.
        return Task.FromResult(FromSuccess(embed));
    }
}