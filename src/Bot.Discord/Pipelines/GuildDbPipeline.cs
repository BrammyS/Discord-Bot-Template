using Bot.persistence.Domain.Models;
using Bot.persistence.UnitOfWorks;
using Color_Chan.Discord.Commands;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Bot.Discord.Pipelines;

public class GuildDbPipeline : IInteractionPipeline
{
    private readonly ILogger<GuildDbPipeline> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///     Initializes a new instance of <see cref="GuildDbPipeline" />.
    /// </summary>
    /// <param name="logger">The logger that will log the messages.</param>
    /// <param name="unitOfWork">The <see cref="IUnitOfWork" /> that will executed queries on the DB.</param>
    public GuildDbPipeline(ILogger<GuildDbPipeline> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, InteractionHandlerDelegate next)
    {
        // Skip commands that were used outside of a guild.
        if (context.GuildId is null)
        {
            return await next();
        }

        // Add a new guild to the database if it doesn't exist.
        var exists = await _unitOfWork.Servers.ExistsAsync(x => x.GuildId == context.GuildId.Value).ConfigureAwait(false);
        if (!exists)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Adding a new guild to the database ID: {Id}", context.GuildId.Value.ToString());

            await _unitOfWork.Servers.AddAsync(new Server
            {
                GuildId = context.GuildId.Value,
                Name = context.Guild?.Name ?? "Unknown guild name",
                TotalMembers = context.Guild?.MemberCount ?? 0
            }).ConfigureAwait(false);
        }

        return await next();
    }
}