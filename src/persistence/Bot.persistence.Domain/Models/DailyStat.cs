namespace Bot.persistence.Domain.Models;

public class DailyStat : BaseDocument
{
    /// <summary>
    ///     Date of the stats.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     total commands used.
    /// </summary>
    public int TotalCommandsUsed { get; set; }
}