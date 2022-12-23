using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Bot.Discord.Commands;

/// <summary>
///     The command module for inviting the bot to other Discord's servers.
/// </summary>
[UserRateLimit(5, 10)] // Sets the rate lcimit for this command module to 5 requests per 10 seconds per user.
public class InviteCommands : SlashCommandModule
{
    public const string InviteCommandName = "invite";
    public const string InviteCommandDesc = "Invite the bot somewhere else!";

    public static readonly IDiscordComponent InviteMe =
        new ActionRowComponentBuilder()
            .WithButton("Invite me!", DiscordButtonStyle.Link, null, $"https://discord.com/api/oauth2/authorize?client_id={Constants.BotId}&permissions=0&scope=bot%20applications.commands")
            .Build();

    /// <summary>
    ///     An invitation command where the bot will reply back with a link to be added to other servers.
    /// </summary>
    /// <returns>
    ///     An embedded response with the provided message.
    /// </returns>
    [SlashCommand(InviteCommandName, InviteCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> InviteAsync()
    {
        var inviteMeEmbed = new DiscordEmbedBuilder()
                            .WithTitle("Invite me")
                            .WithDescription("I need more friends, add me to other Discord servers!")
                            .WithColor(Constants.Colors.Successful)
                            .WithTimeStamp()
                            .Build();

        // Build the embedded response.
        var inviteResponse = new InteractionResponseBuilder()
                             .WithEmbed(inviteMeEmbed)
                             .WithComponent(InviteMe)
                             .Build();

        //  Return the response to Discord.
        return Task.FromResult(FromSuccess(inviteResponse));
    }
}