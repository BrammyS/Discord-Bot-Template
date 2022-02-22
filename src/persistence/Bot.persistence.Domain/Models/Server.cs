namespace Bot.persistence.Domain.Models;

public class Server : BaseDocument
{
    /// <summary>
    ///     THe server id.
    /// </summary>
    public ulong GuildId { get; set; }

    /// <summary>
    ///     Server name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Total member count.
    /// </summary>
    public int TotalMembers { get; set; }

    /// <summary>
    ///     The date of when the bot joined the server.
    /// </summary>
    public DateTime JoinDate { get; set; }
}