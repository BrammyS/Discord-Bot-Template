using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Bot.Discord.Components;

public static class InviteButtons
{ 
    public static readonly IDiscordComponent InviteMe =
        new ActionRowComponentBuilder()
            .WithButton("Invite me!", DiscordButtonStyle.Link, null, $"https://discord.com/api/oauth2/authorize?client_id={Constants.BotId}&permissions=0&scope=bot%20applications.commands")
            .Build();
}
