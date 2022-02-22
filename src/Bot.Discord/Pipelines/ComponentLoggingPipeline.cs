using System.Diagnostics;
using Bot.persistence.Domain.Models;
using Bot.persistence.UnitOfWorks;
using Color_Chan.Discord.Commands;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Bot.Discord.Pipelines;

/// <summary>
///     A pipeline that will measure the performance of the component commands
///     and log the component command requests.
/// </summary>
public class ComponentLoggingPipeline : IComponentInteractionPipeline
{
    private readonly ILogger<ComponentLoggingPipeline> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///     Initializes a new instance of <see cref="ComponentLoggingPipeline" />.
    /// </summary>
    /// <param name="logger">The logger that will log the performance of the component commands to the console.</param>
    /// <param name="unitOfWork">The <see cref="IUnitOfWork" /> that will executed queries on the DB.</param>
    public ComponentLoggingPipeline(ILogger<ComponentLoggingPipeline> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, ComponentInteractionHandlerDelegate next)
    {
        var sw = new Stopwatch();

        sw.Start();

        // Run the command
        var result = await next().ConfigureAwait(false);

        sw.Stop();

        await LogComponentRequestAsync(context, result, sw);

        return result;
    }

    private async Task LogComponentRequestAsync(IInteractionContext context, Result<IDiscordInteractionResponse> result, Stopwatch sw)
    {
        var request = new Request
        {
            Command = context.Data.CustomId ?? context.MethodName,
            IsSuccessful = result.IsSuccessful,
            ErrorMessage = result.ErrorResult?.ErrorMessage,
            MessageId = context.InteractionId,
            ServerId = context.GuildId ?? context.ChannelId,
            TimeStamp = DateTime.UtcNow,
            UserId = context.User.Id,
            AddedAtUtc = DateTime.UtcNow,
            ElapsedMilliseconds = sw.ElapsedMilliseconds,
            BsonObjectId = ObjectId.GenerateNewId().ToString()
        };

        await _unitOfWork.Requests.AddAsync(request).ConfigureAwait(false);
        await _unitOfWork.DailyStats.IncrementCommandsUsedAsync().ConfigureAwait(false);

        _logger.Log(sw.ElapsedMilliseconds > 1500 ? LogLevel.Warning : LogLevel.Debug, "Executed component command {ComponentCommandName} in {ElapsedMilliseconds}ms - {IsSuccessful}",
                    request.Command,
                    sw.ElapsedMilliseconds.ToString(),
                    request.IsSuccessful ? "Successfully" : "Unsuccessfully");
    }
}