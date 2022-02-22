namespace Bot.persistence.Domain.Models;

public class Request : BaseDocument
{
    /// <summary>
    ///     Used command.
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    ///     Error message.
    ///     Note: This is null if the request is successful.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    ///     <see cref="bool" /> for if the request was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    ///     TimeStamp for then the request happened.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    ///     The message id of the request.
    /// </summary>
    public ulong? MessageId { get; set; }

    /// <summary>
    ///     The server id of where the command was used.
    /// </summary>
    public ulong ServerId { get; set; }

    /// <summary>
    ///     The user id of the user that used the command.
    /// </summary>
    public ulong UserId { get; set; }

    /// <summary>
    ///     The amount of time the request took to execute.
    /// </summary>
    public long? ElapsedMilliseconds { get; set; }
}