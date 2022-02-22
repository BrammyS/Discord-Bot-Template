using Bot.Discord.Commands;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Bot.Discord.Components;

/// <summary>
///     The component module for the help page button requests.
/// </summary>
[UserRateLimit(10, 20)]
[GuildRateLimit(120, 120)]
public class HelpPageButtons : ComponentInteractionModule
{
    private const int MinPageNum = 1;
    private const int MaxPageNum = 3;
    private const string ChangeHelpPageButtonId = "change_help_page";
    private readonly ILogger<HelpPageButtons> _logger;

    /// <summary>
    ///     Initializes a new <see cref="HelpPageButtons" />.
    /// </summary>
    /// <param name="logger">The Logger that will be used to log messages.</param>
    public HelpPageButtons(ILogger<HelpPageButtons> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Change the page of a help page embed.
    /// </summary>
    /// <returns>
    ///     The new help page embed.
    /// </returns>
    /// <exception cref="NullReferenceException">Thrown when the requested page wasn't found.</exception>
    [Component(ChangeHelpPageButtonId, DiscordComponentType.Button, true, true)]
    public Task<Result<IDiscordInteractionResponse>> GetHelpPage()
    {
        var responseBuilder = new InteractionResponseBuilder();

        // Try to get the current embed containing the current page number.
        _ = Context.Message ?? throw new NullReferenceException($"{nameof(Context.Message)} can not be null");
        var embed = Context.Message.Embeds.FirstOrDefault();
        if (embed?.Title is null)
        {
            responseBuilder.WithContent(". . .")
                           .WithComponent(ButtonsAllDisabled);
            return Task.FromResult(FromSuccess(responseBuilder.Build()));
        }

        // Get the requested help page.
        var pageNum = GetRequestedPageNum(embed);
        responseBuilder.WithEmbed(GetHelpPageBuilder(pageNum));

        // Add the correct buttons for the page.
        switch (pageNum)
        {
            case MinPageNum:
                responseBuilder.WithComponent(ButtonsNoBack);
                break;
            case MaxPageNum:
                responseBuilder.WithComponent(ButtonsNoNext);
                break;
            default:
                responseBuilder.WithComponent(Buttons);
                break;
        }

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }

    private int GetRequestedPageNum(IDiscordEmbed embed)
    {
        // Get the current page number from the title.
        var page = embed.Title!.Split(" | ").LastOrDefault();
        if (page is null)
        {
            _logger.LogWarning("pageNum was null for interaction {Id}", Context.InteractionId.ToString());
            throw new NullReferenceException(nameof(page));
        }

        if (!int.TryParse(page.LastOrDefault().ToString(), out var pageNum))
        {
            pageNum = MinPageNum;
        }

        // Get the requested page number.
        switch (Context.Args.Contains("next"))
        {
            case false when pageNum > MinPageNum:
                pageNum--;
                break;
            case true when pageNum < MaxPageNum:
                pageNum++;
                break;
        }

        return pageNum;
    }

    private IDiscordEmbed GetHelpPageBuilder(int pageNum)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug("Loading help page {Page}", pageNum.ToString());

        return pageNum switch
        {
            1 => HelpCommands.Page1Embed,
            2 => HelpCommands.Page2Embed,
            3 => HelpCommands.Page3Embed,
            _ => throw new ArgumentOutOfRangeException(nameof(pageNum), $"The provided page number does not exist. Value {pageNum}.")
        };
    }

    #region Help Buttons

    public static readonly IDiscordComponent ButtonsNoBack =
        new ActionRowComponentBuilder()
            .WithButton("Previous", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}previous", disabled: true)
            .WithButton("Next", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}next")
            .Build();

    public static readonly IDiscordComponent Buttons =
        new ActionRowComponentBuilder()
            .WithButton("Previous", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}previous")
            .WithButton("Next", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}next")
            .Build();

    public static readonly IDiscordComponent ButtonsNoNext =
        new ActionRowComponentBuilder()
            .WithButton("Previous", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}previous")
            .WithButton("Next", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}next", disabled: true)
            .Build();

    public static readonly IDiscordComponent ButtonsAllDisabled =
        new ActionRowComponentBuilder()
            .WithButton("Previous", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}previous", disabled: true)
            .WithButton("Next", DiscordButtonStyle.Primary, $"{ChangeHelpPageButtonId}{Constants.ArgsSeparator}next", disabled: true)
            .Build();

    #endregion
}