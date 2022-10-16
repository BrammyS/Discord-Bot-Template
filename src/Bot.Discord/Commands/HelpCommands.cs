using Bot.Discord.Components;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Bot.Discord.Commands;

/// <summary>
///     The command module for all help commands.
/// </summary>
[UserRateLimit(3, 10)]
[GuildRateLimit(18, 60)]
public class HelpCommands : SlashCommandModule
{
    public const string HelpCommandName = "help";
    public const string HelpCommandDesc = "Shows a list with all my commands!";

    /// <summary>
    ///     Get the first help page.
    /// </summary>
    /// <returns>
    ///     The embed of the first help page.
    /// </returns>
    [SlashCommand(HelpCommandName, HelpCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> ShowHelpAsync()
    {
        var response = new InteractionResponseBuilder()
                       .WithEmbed(Page1Embed)
                       .WithComponent(HelpPageButtons.ButtonsNoBack)
                       .Build();

        return Task.FromResult(FromSuccess(response));
    }

    // All the page embeds for the help command.

    #region Page Embeds

    public static readonly IDiscordEmbed Page1Embed =
        new DiscordEmbedBuilder()
            .WithTitle("Help | Basic commands | Page 1")
            .WithField($"{Constants.SlashPrefix}{SayCommands.SayCommandName}", SayCommands.SayCommandDesc)
            .WithField($"{Constants.SlashPrefix}{PongCommands.PingCommandName}", PongCommands.PingCommandDesc)
            .WithColor(Constants.Colors.Neutral)
            .Build();

    public static readonly IDiscordEmbed Page2Embed =
        new DiscordEmbedBuilder()
            .WithTitle("Help | Info commands | Page 2")
            .WithField($"{Constants.SlashPrefix}{HelpCommandName}", HelpCommandDesc)
            .WithColor(Constants.Colors.Neutral)
            .Build();

    public static readonly IDiscordEmbed Page3Embed =
        new DiscordEmbedBuilder()
            .WithTitle("Help | Misc commands | Page 3")
            .WithField($"{Constants.SlashPrefix}{AboutCommands.AboutCommandName}", AboutCommands.AboutCommandDesc)
            .WithField($"{Constants.SlashPrefix}{InviteCommands.InviteCommandName}", InviteCommands.InviteCommandDesc)
            .WithColor(Constants.Colors.Neutral)
            .Build();

    #endregion
}