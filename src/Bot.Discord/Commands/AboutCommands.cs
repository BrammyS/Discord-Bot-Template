using System.Reflection;
using System.Runtime.Versioning;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Bot.Discord.Commands;

/// <summary>
///     The command module for all about commands.
/// </summary>
[UserRateLimit(5, 10)] // Sets the rate limit for this command module to 5 requests per 10 seconds per user.
public class AboutCommands : SlashCommandModule
{
    public const string AboutCommandName = "about";
    public const string AboutCommandDesc = "Get some information about the bot!";

    /// <summary>
    ///     Get some misc information about the bot.
    /// </summary>
    /// <returns>
    ///     An embedded response with some information about the bot.
    /// </returns>
    [SlashCommand(AboutCommandName, AboutCommandDesc)]
    public Task<Result<IDiscordInteractionResponse>> AboutAsync()
    {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null)
        {
            var errorEmbed = new DiscordEmbedBuilder()
                             .WithTitle("Error")
                             .WithDescription("Failed to load version data.")
                             .WithColor(Constants.Colors.Error)
                             .Build();

            return Task.FromResult(FromSuccess(errorEmbed));
        }

        var dotNetVersion = assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName.Replace(".NETCoreApp,Version=v", "") ?? "Unknown version";
        var libVersion = Assembly.GetAssembly(typeof(SlashCommandModule))?.GetName().Version?.ToString() ?? "Unknown version";

        var embed = new DiscordEmbedBuilder()
                    .WithTitle("Bot info")
                    .WithColor(Constants.Colors.Neutral)
                    .WithDescription("This bot was made using a [simple bot template](https://github.com/BrammyS/SlashDiscordBotTemplate)!")
                    .WithField(".Net version:", $"v{dotNetVersion}", true)
                    .WithField("Color-Chan.Discord version:", $"[v{libVersion}](https://github.com/Color-Chan/Color-Chan.Discord)", true)
                    .Build();

        return Task.FromResult(FromSuccess(embed));
    }
}