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
///     The command module for inviting the bot to other Discord's servers.
/// </summary>
[UserRateLimit(5, 10)] // Sets the rate lcimit for this command module to 5 requests per 10 seconds per user.
public class InviteCommands : SlashCommandModule
{
    public const string InviteCommandName = "invite";
    public const string InviteCommandDesc = "Invite the bot somewhere else!";


    /// <summary>
    ///     An invitation command where the bot will reply back with a link to be added to other servers.
    /// </summary>
    /// <returns>
    ///     An embedded response with the provided message.
    /// </returns>
    [SlashCommand(InviteCommandName, InviteCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> InviteAsync()
    {
        // Build the embedded response.
        var inviteResponse = new InteractionResponseBuilder()
                    .WithEmbed(InviteMeEmbed)
                    .WithComponent(InviteButtons.InviteMe)
                    .Build();

        //  Return the response to Discord.
        return Task.FromResult(FromSuccess(inviteResponse));
    }

    public static readonly IDiscordEmbed InviteMeEmbed =
        new DiscordEmbedBuilder()
            .WithTitle("Invite me")
            .WithDescription("I need more friends, add me to other Discord servers!")
            .WithColor(Constants.Colors.Successful)
            .WithTimeStamp()
            .Build();
}